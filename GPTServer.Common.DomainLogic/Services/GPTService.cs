using GPTServer.Common.Core.ContextInfo;
using GPTServer.Common.Core.DTOs.General;
using GPTServer.Common.Core.DTOs.GPT;
using GPTServer.Common.Core.GPT.DTOs;
using GPTServer.Common.DataAccess.Repositories.Interfaces;
using GPTServer.Common.DomainLogic.Interfaces;
using GPTServer.Common.Utils.GPTClient.DataObjects;
using GPTServer.Common.Utils.GPTClient.Interfaces;

namespace GPTServer.Common.DomainLogic.Services;

public class GPTService : IGPTService
{
    private readonly IGPTClient _gptClient;
    private readonly ILogService _logService;
    private readonly IApiKeyRepo _apiKeyRepo;
    private readonly IUserRepo _userRepo;
    private readonly IContextInfo _contextInfo;
    private readonly IGPTInteractionRepo _gptInteractionRepo;

    public GPTService(
        IGPTClient gptClient,
        IContextInfo contextInfo,
        ILogService logService,
        IUserRepo userRepo,
        IApiKeyRepo apiKeyRepo,
        IGPTInteractionRepo gptInteractionRepo)
    {
        _userRepo = userRepo;
        _gptClient = gptClient;
        _logService = logService;
        _apiKeyRepo = apiKeyRepo;
        _contextInfo = contextInfo;
        _gptInteractionRepo = gptInteractionRepo;
    }

    public async Task<GPTAnswerResponseDTO> GetGPTAnswerAsync(GPTQuestionRequestDTO @params)
    {
        var apiKeys = await _apiKeyRepo.GetActiveApiKeysAsync(_contextInfo.UserId.HasValue ? _contextInfo.UserId.Value : default);

        if (apiKeys is null || apiKeys.Count == 0)
        {
            await _logService.LogAsync(new Core.Models.Log()
            {
                Message = "No active GPT API key was found."
            });

            return new()
            {
                ResponseType = Core.Enums.ResponseType.NotFound,
                ErrorMessage = "You do not have any active GPT API key."
            };
        }

        var result = await _gptClient.CreateCompletionAsync(new GPTCompletionRequest());

        if (result is null || string.IsNullOrEmpty(result.ResponseMessage))
        {
            await _logService.LogAsync(new Core.Models.Log()
            {
                Message = "An error has occurred during the GPT API request."
            });

            return new()
            {
                ResponseType = Core.Enums.ResponseType.InternalError,
                ErrorMessage = "An error has occurred during the GPT API request."
            };
        }

        return new()
        {
            ResponseMessage = result.ResponseMessage
        };
    }

    public async Task<ApiKeysResponseDTO> GetOwnApiKeysAsync()
    {
        var keys = await _apiKeyRepo.GetAllByUserIdAsync(_contextInfo.UserId);

        return new()
        {
            Keys = keys,
            ResponseType = Core.Enums.ResponseType.Success,
        };
    }

    public async Task<ApiKeysResponseDTO> RemoveApiKeyAsync(ApiKeyRequestDTO dto)
    {
        if (dto is null || string.IsNullOrEmpty(dto.ApiKey))
        {
            return new()
            {
                ResponseType = Core.Enums.ResponseType.MissingParam,
            };
        }

        var existingApiKey = await _apiKeyRepo.GetByApiKeyAsync(_contextInfo.UserId, dto.ApiKey.Trim());
        if (existingApiKey is null)
        {
            return new()
            {
                ResponseType = Core.Enums.ResponseType.NotFound,
            };
        }

        // INFO: Auto-select the first key as new active
        await _apiKeyRepo.AutoSelectNewActiveKeyAsync(_contextInfo.UserId);

        // INFO: If the active key will be deleted
        if (existingApiKey.IsActive)
        {
            // INFO: Remove the active state
            existingApiKey.IsActive = false;
            await _apiKeyRepo.UpdateAsync(existingApiKey);
        }

        // INFO: Remove the key
        await _apiKeyRepo.RemoveAsync(existingApiKey.Id);

        // INFO: Return new key list
        return await GetOwnApiKeysAsync();
    }

    public async Task<ApiKeysResponseDTO> SetActiveApiKeyAsync(ApiKeyRequestDTO dto)
    {
        if (dto is null || string.IsNullOrEmpty(dto.ApiKey))
        {
            return new()
            {
                ResponseType = Core.Enums.ResponseType.MissingParam,
            };
        }

        await _apiKeyRepo.SelectNewActiveKeyAsync(_contextInfo.UserId, dto.Id);

        return await GetOwnApiKeysAsync();
    }

    public async Task<ApiKeysResponseDTO> AddOrUpdateActiveApiKeyAsync(ApiKeyRequestDTO dto)
    {
        if (dto is null || string.IsNullOrEmpty(dto.ApiKey))
        {
            return new()
            {
                ResponseType = Core.Enums.ResponseType.MissingParam,
            };
        }

        var existingKey = await _apiKeyRepo.GetByIdAsync(dto.Id);

        if (existingKey is not null)
        {
            existingKey.IsActive = true;
            existingKey.KeyName = dto.ApiKeyName ?? string.Empty;
            existingKey.Key = dto.ApiKey ?? string.Empty;

            await _apiKeyRepo.UpdateAsync(existingKey);

            return await GetOwnApiKeysAsync();
        }

        await _apiKeyRepo.ResetActiveKeyStateAsync(_contextInfo.UserId);

        await _apiKeyRepo.AddAsync(new Core.Models.ApiKey()
        {
            Key = dto.ApiKey ?? string.Empty,
            KeyName = dto.ApiKeyName ?? string.Empty,
            UserId = _contextInfo.UserId.HasValue ? _contextInfo.UserId.Value : default,
            IsActive = true // INFO: New key will be active automatically
        });

        return await GetOwnApiKeysAsync();
    }

}
