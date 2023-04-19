using GPTServer.Common.Core.ContextInfo;
using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.Repositories.Interfaces;
using GPTServer.Common.DomainLogic.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;

namespace GPTServer.Common.DomainLogic.Services;

public class LogService : ILogService
{
	private readonly ILogger<LogService> _fileLogger;
	private readonly ILogRepo _logRepo;
	private readonly IContextInfo _contextInfo;

	public LogService(
		ILogger<LogService> fileLogger,
		ILogRepo logRepo,
		IContextInfo contextInfo)
	{
		_fileLogger = fileLogger;
		_logRepo = logRepo;
		_contextInfo = contextInfo;
	}

	public async Task LogAsync(Log log)
	{
		StringBuilder fileLogContentSb = new($" {log.Message}");

		if (!string.IsNullOrEmpty(_contextInfo.AuthToken) || !string.IsNullOrEmpty(_contextInfo.Email))
		{
			fileLogContentSb.Append($";\nContextInfo: {_contextInfo}");
		}

		switch (log.LogLevel)
		{
			case Core.Enums.LogLevel.Info:
				_fileLogger.LogInformation(fileLogContentSb.ToString());
				break;
			case Core.Enums.LogLevel.Warn:
				_fileLogger.LogWarning(fileLogContentSb.ToString());
				break;
			case Core.Enums.LogLevel.Error:
				_fileLogger.LogError(fileLogContentSb.ToString());
				break;
			default:
				_fileLogger.LogInformation(fileLogContentSb.ToString());
				break;
		}

		if (log.CreationDate == default)
		{
			log.CreationDate = DateTime.Now;
		}

		if (string.IsNullOrWhiteSpace(log.ClientIP))
		{
			log.ClientIP = _contextInfo.ClientIP ?? string.Empty;
		}

		await _logRepo.AddAsync(log);
	}
}
