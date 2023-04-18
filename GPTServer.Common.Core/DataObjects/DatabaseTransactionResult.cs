namespace GPTServer.Common.Core.DataObjects;

public class DatabaseTransactionResult
{
    public bool IsSuccess { get; set; }
    public Exception Exception { get; set; }
}