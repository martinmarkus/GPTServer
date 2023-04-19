using System.ComponentModel.DataAnnotations.Schema;

namespace GPTServer.Common.Core.Models;

[Table("admin_keys")]
public class AdminKey : BaseEntity
{
    public Guid Key { get; set; }
}
