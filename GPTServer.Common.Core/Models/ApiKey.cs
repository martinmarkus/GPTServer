﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPTServer.Common.Core.Models;

[Table("api_keys")]
public class ApiKey : BaseEntity
{
    [Required]
	[MaxLength(200)]
    public string Key { get; set; }

    [Required]
	[MaxLength(200)]
    public string KeyName { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
	[MaxLength(36)]
	public Guid UserId { get; set; }

    public virtual User User { get; set; }
}
