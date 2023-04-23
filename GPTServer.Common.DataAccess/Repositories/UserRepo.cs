using GPTServer.Common.Core.DataObjects.Users;
using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.DbContexts;
using GPTServer.Common.DataAccess.Repositores;
using GPTServer.Common.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GPTServer.Common.DataAccess.Repositories;

public class UserRepo : AsyncRepo<User>, IUserRepo
{
    public UserRepo(GPTDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<UserGetByEmailResult> GetByEmailAsync(string email) =>
        await _dbContext.Users
            .Include(x => x.ApiKeys)
            .Where(x =>
                !x.IsDeleted
                &&
                string.Equals(x.Email, email))
            .Select(x => new UserGetByEmailResult()
            {
                Id = x.Id,
                Email = x.Email ?? string.Empty,
                UniqueId = x.UniqueId ?? string.Empty,
                UserAgent = x.UserAgent ?? string.Empty,
                CreationDate = x.CreationDate,
                LastAuthDate = x.LastAuthDate,
                LastAuthRoutingEnv = x.LastAuthRoutingEnv ?? string.Empty,
                PasswordHash = x.PasswordHash,
                PasswordSalt = x.PasswordSalt,
                ApiKey = x.ApiKeys.FirstOrDefault(key => key.IsActive),
                HasExtensionPermission = x.HasExtensionPermission
            })
            .FirstOrDefaultAsync();

    public async Task SetExtensionPermissionASync(string email, bool hasExtensionPermission)
    {
        var user = await _dbContext.Users
            .Include(x => x.ApiKeys)
            .Where(x =>
                !x.IsDeleted
                &&
                string.Equals(x.Email, email))
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return;
        }

        user.HasExtensionPermission = hasExtensionPermission;

        await UpdateAsync(user);
    }
}
