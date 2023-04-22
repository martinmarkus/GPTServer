using GPTServer.Common.Core.DTOs.GPT;
using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.DbContexts;
using GPTServer.Common.DataAccess.Repositores;
using GPTServer.Common.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPTServer.Common.DataAccess.Repositories;

public class ApiKeyRepo : AsyncRepo<ApiKey>, IApiKeyRepo
{
    public ApiKeyRepo(GPTDbContext dbContext) : base(dbContext)
    {
    }

    public async Task AutoSelectNewActiveKeyAsync(Guid? userId)
    {
        var key = await _dbContext.ApiKeys
            .FirstOrDefaultAsync(x => !x.IsDeleted && x.UserId == userId.Value);

        if (key is not null)
        {
            key.IsActive = true;
            await UpdateAsync(key);
        }
    }

    public async Task SelectNewActiveKeyAsync(Guid? userId, string apiKey)
    {
        apiKey = apiKey?.Trim() ?? string.Empty;

        // INFO: Get the single active key
        var key = await _dbContext.ApiKeys
            .FirstOrDefaultAsync(x =>
                !x.IsDeleted
                && x.UserId == userId.Value
                && x.IsActive);

        // INFO: No active key or the active key did not change
        if (key is null
            || string.Equals(key.Key.Trim(), apiKey.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        key.IsActive = false;
        await UpdateAsync(key);

        var newActiveKey = await _dbContext.ApiKeys
           .FirstOrDefaultAsync(x =>
               !x.IsDeleted
               && x.UserId == userId.Value
               && string.Equals(x.Key.Trim(), apiKey.Trim())
               && x.IsActive);

        // INFO: Set the new active key
        newActiveKey.IsActive = true;
        await UpdateAsync(newActiveKey);
    }

    public async Task<ApiKey> GetActiveApiKeyAsync(Guid userId) =>
        await _dbContext.ApiKeys
            ?.Where(x =>
                !x.IsDeleted
                &&
                x.UserId == userId
            )
            ?.FirstOrDefaultAsync();

    public async Task<IReadOnlyList<ApiKeyResponseDTO>> GetAllByUserIdAsync(Guid? userId) =>
        await _dbContext.ApiKeys
            ?.Where(x =>
                !x.IsDeleted
                && x.UserId == userId.Value
            )
            ?.Select(x => new ApiKeyResponseDTO()
            {
                Key = x.Key,
                KeyName = x.KeyName,
                IsActive = x.IsActive
            })
            ?.ToListAsync();

    public async Task<ApiKey> GetByApiKeyAsync(Guid? userId, string apiKey) =>
        await _dbContext.ApiKeys
            ?.FirstOrDefaultAsync(x =>
                !x.IsDeleted
                && x.UserId == userId.Value
                && string.Equals(x.Key.Trim(), apiKey.Trim())
            );

    public async Task ResetActiveKeyStateAsync(Guid? userId)
    {
        if (!userId.HasValue)
        {
            return;
        }

        var activeKey = await GetActiveApiKeyAsync(userId.Value);
        if (activeKey is not null)
        {
            activeKey.IsActive = false;
            await UpdateAsync(activeKey);
        }
    }
}
