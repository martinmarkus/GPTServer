using GPTServer.Api.Clients;
using GPTServer.Common.Core.ContextInfo;
using GPTServer.Common.Core.DTOs.GPT;
using GPTServer.Common.Core.GPT.DTOs;
using GPTServer.Common.DataAccess.Repositories.Interfaces;
using GPTServer.Common.DomainLogic.Interfaces;
using GPTServer.Common.Utils.GPTClient.DataObjects;

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
		string apiKey = await _apiKeyRepo.GetActiveApiKeyAsync(_contextInfo.UserId.HasValue ? _contextInfo.UserId.Value : default);

		if (string.IsNullOrEmpty(apiKey.Trim()))
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
}
