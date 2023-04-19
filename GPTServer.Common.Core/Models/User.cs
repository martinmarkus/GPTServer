using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPTServer.Common.Core.Models;

[Table("users")]
public class User : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MaxLength(36)]
    public string UniqueId { get; set; }

	[MaxLength(400)]
	public string UserAgent { get; set; } = string.Empty;

	public virtual ICollection<ApiKey> ApiKeys { get; set; }

    public virtual ICollection<ClientIP> ClienIPs { get; set; }
    
    public virtual ICollection<GPTInteraction> GPTInteractions { get; set; }
}
