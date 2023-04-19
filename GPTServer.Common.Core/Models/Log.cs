using GPTServer.Common.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPTServer.Common.Core.Models;

[Table("logs")]
public class Log : BaseEntity
{
    [Required]
    [MaxLength(8000)]
    public string Message { get; set; }

    [Required]    public LogLevel LogLevel { get; set; } = LogLevel.Info;


	[Required]
	[MaxLength(50)]
	public string ClientIP { get; set; }

    [Required]
    [MaxLength(80)]
    public string ExecutorId { get; set; } = string.Empty;
}
