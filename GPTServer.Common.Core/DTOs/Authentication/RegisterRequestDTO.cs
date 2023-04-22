using GPTServer.Common.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace GPTServer.Common.Core.DTOs.Authentication;
public class RegisterRequestDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Password]
    public string Password { get; set; }
}
