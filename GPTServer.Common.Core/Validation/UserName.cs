using System.ComponentModel.DataAnnotations;

namespace GPTServer.Common.Core.Validation;

public class UserName : RegularExpressionAttribute
{
    public UserName() : base("^(?=[a-zA-Z0-9._]{4,20}$)(?!.*[_.]{2})[^_.].*[^_.]$")
    {
    }
}
