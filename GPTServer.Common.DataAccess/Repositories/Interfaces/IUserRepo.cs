using GPTServer.Common.Core.DataObjects.Users;
using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.Repositores.Interfaces;

namespace GPTServer.Common.DataAccess.Repositories.Interfaces;
public interface IUserRepo : IAsyncRepo<User>
{
    Task<UserGetByEmailResult> GetByEmailAsync(string email);
    Task SetExtensionPermissionASync(string email, bool hasExtensionPermission);
}
