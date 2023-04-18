using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPTServer.Common.Core.Models;

[Table("users")]
public class User : BaseEntity
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string UniqueId { get; set; }

    public virtual ICollection<ApiKey> ApiKeys { get; set; }
}
