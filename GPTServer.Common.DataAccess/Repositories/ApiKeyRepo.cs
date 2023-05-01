using GPTServer.Common.Core.DTOs.GPT;
using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.DbContexts;
using GPTServer.Common.DataAccess.Repositores;
using GPTServer.Common.DataAccess.Repositories.Interfaces;
using GPTServer.Common.DataAccess.Transactions;
using Microsoft.EntityFrameworkCore;

namespace GPTServer.Common.DataAccess.Repositories;

public class ApiKeyRepo : AsyncRepo<ApiKey>, IApiKeyRepo
{
    private readonly IDatabaseTransaction _transaction;

    public ApiKeyRepo(GPTDbContext dbContext, IDatabaseTransaction transaction) : base(dbContext)
    {
        _transaction = transaction;
    }

    public async Task AutoSelectNewActiveKeyAsync(Guid? userId)
    {
        var key = await _dbContext.ApiKeys
            .FirstOrDefaultAsync(x => !x.IsDeleted && x.UserId == userId.Value && !x.IsActive);

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
        var activeKeys = await _dbContext.ApiKeys
            .Where(x =>
                !x.IsDeleted
                && x.UserId == userId.Value
                && x.IsActive)
            .ToListAsync();

        // INFO: No active key or the active key did not change
        if (activeKeys is null
            || activeKeys.Any(key => string.Equals(key.Key.Trim(), apiKey.Trim(), StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        await _transaction.RunAsync(async () =>
        {
            foreach (var key in activeKeys)
            {
                key.IsActive = false;
                await UpdateAsync(key);
            }

            var newActiveKey = await _dbContext.ApiKeys
               .FirstOrDefaultAsync(x =>
                   !x.IsDeleted
                   && x.UserId == userId.Value
                   && string.Equals(x.Key.Trim(), apiKey.Trim()));

            if (newActiveKey is null)
            {
                throw new Exception();
            }

            // INFO: Set the new active key
            newActiveKey.IsActive = true;
            await UpdateAsync(newActiveKey);
        });
    }

    public async Task<IList<ApiKey>> GetActiveApiKeysAsync(Guid userId) =>
        await _dbContext.ApiKeys
            ?.Where(x =>
                !x.IsDeleted
                &&
                x.UserId == userId
            )
            ?.ToListAsync();

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
                IsActive = x.IsActive,
                CreationDate = x.CreationDate
            })
            ?.OrderByDescending(x => x.CreationDate)
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

        var activeKeys = await GetActiveApiKeysAsync(userId.Value);
        if (activeKeys is not null)
        {
            foreach (var activeKey in activeKeys)
            {
                activeKey.IsActive = false;
                await UpdateAsync(activeKey);
            }
        }
    }
}
