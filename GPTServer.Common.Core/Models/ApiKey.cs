using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPTServer.Common.Core.Models;

[Table("api_keys")]
public class ApiKey : BaseEntity
{
    [Required]
    public string Key { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public virtual User User { get; set; }
}
