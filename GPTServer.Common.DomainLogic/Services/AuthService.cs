using GPTServer.Common.Core.Configurations;
using GPTServer.Common.Core.Constants;
using GPTServer.Common.Core.ContextInfo;
using GPTServer.Common.Core.DTOs.Authentication;
using GPTServer.Common.Core.DTOs.General;
using GPTServer.Common.Core.Utils.GeneralUtils.String;
using GPTServer.Common.DataAccess.Repositories.Interfaces;
using GPTServer.Common.DomainLogic.Interfaces;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace GPTServer.Common.DomainLogic.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepo _userRepo;
    private readonly IApiKeyRepo _apiKeyRepo;
    private readonly IContextInfo _contextInfo;

    private readonly ILogService _logService;
    private readonly IAuthTokenService _authTokenService;
    private readonly ISecureHashGeneratorService _secureHashGeneratorService;

    private readonly BaseOptions _baseOptions;

    public AuthService(
        IUserRepo userRepo,
        IContextInfo contextInfo,
        IOptions<BaseOptions> baseOptions,
        ILogService logService,
        ISecureHashGeneratorService secureHashGeneratorService,
        IAuthTokenService authTokenService,
        IApiKeyRepo apiKeyRepo)
    {
        _userRepo = userRepo;
        _contextInfo = contextInfo;
        _baseOptions = baseOptions.Value;
        _logService = logService;
        _secureHashGeneratorService = secureHashGeneratorService;
        _authTokenService = authTokenService;
        _apiKeyRepo = apiKeyRepo;
    }

    public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto)
    {
        if (dto is null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
        {
            return new()
            {
                ResponseType = Core.Enums.ResponseType.MissingParam
            };
        }

        var existingUser = await _userRepo.GetByEmailAsync(dto.Email);
        // INFO: User is not registered
        if (existingUser is null)
        {
            return new()
            {
                ResponseType = Core.Enums.ResponseType.Conflict
            };
        }

        string assertHash = _secureHashGeneratorService.CreateHash(
            dto.Password,
            existingUser.PasswordSalt);

        // INFO: Password mismatch
        if (string.IsNullOrEmpty(assertHash) ||
            !string.Equals(assertHash.Trim(), existingUser.PasswordHash.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            return new()
            {
                ResponseType = Core.Enums.ResponseType.Unauthorized
            };
        }

        // INFO: Generate new auth token
        string jwtToken = _authTokenService.GenerateJwtToken(new List<Claim>()
        {
            new Claim(ClaimConstants.Email, existingUser.Email),
            new Claim(ClaimConstants.Id, existingUser.Id.ToString()),
            new Claim(ClaimConstants.UniqueId, existingUser.UniqueId),
        });

        return new()
        {
            AuthToken = jwtToken,
            ResponseType = Core.Enums.ResponseType.Success
        };
    }

    public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO dto)
    {
        if (dto is null)
        {
            return new()
            {
                ResponseType = Core.Enums.ResponseType.MissingParam
            };
        }

        var existingUser = await _userRepo.GetByEmailAsync(dto.Email.ToLower());

        // INFO: User is taken
        if (existingUser is not null)
        {
            return new()
            {
                ResponseType = Core.Enums.ResponseType.Conflict
            };
        }

        // INFO: New password hashing
        string newSalt = _secureHashGeneratorService.CreateSalt();
        string newHash = _secureHashGeneratorService.CreateHash(dto.Password, newSalt);

        await _userRepo.AddAsync(new()
        {
            Email = dto.Email.ToLower(),
            PasswordHash = newHash,
            PasswordSalt = newSalt,
            UniqueId = StringGenerator.GetRandomAlphanumericString(20),
            LastAuthDate = DateTime.Now,
            LastAuthRoutingEnv = string.Empty, // INFO: At registration do not save routing environment
            UserAgent = string.Empty, // INFO: At registration do not save user agent
            HasExtensionPermission = false, // INFO: By default hsa no permission
        });

        return new()
        {
            ResponseType = Core.Enums.ResponseType.Success
        };
    }

    public async Task<UserResponseDTO> GetUserAsync()
    {
        var user = await _userRepo.GetByIdAsync(_contextInfo.UserId.HasValue ? _contextInfo.UserId.Value : default);
        if (user is null)
        {
            return new()
            {
                ResponseType = Core.Enums.ResponseType.MissingParam
            };
        }

        return new()
        {
            Email = user.Email,
            ApiKeysResponseDTO = new()
            {
                Keys = await _apiKeyRepo.GetAllByUserIdAsync(
                    _contextInfo.UserId.HasValue ? _contextInfo.UserId.Value : default)
            },
            ErrorMessage = string.Empty,
            ResponseType = Core.Enums.ResponseType.Success
        };
    }

    public async Task<BaseResponseDTO> ConfirmCustomerAsync(ConfirmCustomerRequestDTO dto)
    {
        if (dto is null || string.IsNullOrEmpty(dto.Email))
        {
            return new()
            {
                ResponseType = Core.Enums.ResponseType.MissingParam
            };
        }

        await _userRepo.SetExtensionPermissionASync(dto.Email.ToLower(), true);

        return new()
        {
            ResponseType = Core.Enums.ResponseType.Success
        };
    }
}
