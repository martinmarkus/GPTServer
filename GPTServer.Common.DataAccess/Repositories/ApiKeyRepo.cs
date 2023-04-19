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

    public async Task<string> GetActiveApiKeyAsync(Guid userId) =>
        await _dbContext.ApiKeys
            ?.Where(x =>
                !x.IsDeleted
                &&
                x.IsDeleted
                &&
                x.UserId == userId
            )
            ?.Select(x => x.Key)
            ?.FirstOrDefaultAsync()
            ?? string.Empty;
}
