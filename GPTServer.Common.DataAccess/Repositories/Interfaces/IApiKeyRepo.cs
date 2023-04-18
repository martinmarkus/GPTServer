using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.Repositores.Interfaces;

namespace GPTServer.Common.DataAccess.Repositories.Interfaces;
public interface IApiKeyRepo : IAsyncRepo<ApiKey>
{
}
