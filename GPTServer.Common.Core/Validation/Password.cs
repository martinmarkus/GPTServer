using System.ComponentModel.DataAnnotations;

namespace GPTServer.Common.Core.Validation;

public class Password : RegularExpressionAttribute
{
    // INFO: Min 1 upper case letter, min 1 lower case letter, min 1 number, min 8 chars
    public Password() : base("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")
    {
    }
}
