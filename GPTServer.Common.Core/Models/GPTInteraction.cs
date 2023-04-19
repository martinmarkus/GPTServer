using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPTServer.Common.Core.Models;

[Table("gpt_interactions")]
public class GPTInteraction : BaseEntity
{
    [Required]
    public bool Success { get; set; }

    [Required]
    public int ResponseMs { get; set; }

    [Required]
    [MaxLength(36)]
    public Guid UserId { get; set; }

    public virtual User User { get; set; }
}
