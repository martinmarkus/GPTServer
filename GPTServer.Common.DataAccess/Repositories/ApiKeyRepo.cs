using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.DbContexts;
using GPTServer.Common.DataAccess.Repositores;
using GPTServer.Common.DataAccess.Repositories.Interfaces;

namespace GPTServer.Common.DataAccess.Repositories;
public class ApiKeyRepo : AsyncRepo<ApiKey>, IApiKeyRepo
{
    public ApiKeyRepo(GPTDbContext dbContext) : base(dbContext)
    {
    }
}
