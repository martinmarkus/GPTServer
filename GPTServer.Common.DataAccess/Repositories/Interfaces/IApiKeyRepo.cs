using GPTServer.Common.Core.DTOs.GPT;
using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.Repositores.Interfaces;

namespace GPTServer.Common.DataAccess.Repositories.Interfaces;
public interface IApiKeyRepo : IAsyncRepo<ApiKey>
{
    Task AutoSelectNewActiveKeyAsync(Guid? userId);
    Task<IList<ApiKey>> GetActiveApiKeysAsync(Guid userId);
    Task<IReadOnlyList<ApiKeyResponseDTO>> GetAllByUserIdAsync(Guid? userId);
    Task<ApiKey> GetByApiKeyAsync(Guid? userId, string apiKey);
    Task ResetActiveKeyStateAsync(Guid? userId);
    Task SelectNewActiveKeyAsync(Guid? userId, Guid id);
}
