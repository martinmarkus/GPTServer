using GPTServer.Common.Core.Models;

namespace GPTServer.Common.Core.DataObjects.Users;

public class UserGetByEmailResult
{
    public string Email { get; set; }

    public Guid Id { get; set; }

    public string UniqueId { get; set; }

    public string UserAgent { get; set; }

    public string LastAuthRoutingEnv { get; set; }

    public string PasswordHash { get; set; }

    public string PasswordSalt { get; set; }

    public bool HasExtensionPermission { get; set; }

    public ApiKey ApiKey { get; set; }

    public DateTime LastAuthDate { get; set; }

    public DateTime CreationDate { get; set; }
}