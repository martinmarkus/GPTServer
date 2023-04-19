using GPTServer.Common.Core.Models;

namespace GPTServer.Common.DomainLogic.Interfaces;
public interface ILogService
{
	Task LogAsync(Log log);
}
