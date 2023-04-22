namespace GPTServer.Common.Core.Enums;
public enum ResponseType
{
    Success = 0,
    NotFound,
    Unauthorized,
    Forbidden,
    MissingParam,
    Conflict,
    InternalError
}
