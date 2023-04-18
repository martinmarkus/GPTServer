using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GPTServer.Common.Core.Models;

public class BaseEntity
{
    [Key]
    [Required]
    [MaxLength(36)]
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [JsonIgnore]
    public bool IsDeleted { get; set; }

    [Required]
    [JsonIgnore]
    public DateTime CreationDate { get; set; } = DateTime.Now;
}
