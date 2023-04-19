using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPTServer.Common.Core.Models;

[Table("client_ips")]
public class ClientIP : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string IP { get; set; }
    
    [Required]
	[MaxLength(36)]
	public Guid UserId { get; set; }


    public virtual User User { get; set; }

}
