using System.Text;

namespace GPTServer.Common.Core.Utils.GeneralUtils.Exceptions;

public static class ExceptionExtensions
{
    public static string ToStringWithInner(this Exception exception, int indent = 0)
    {
        StringBuilder sb = new();

        string lineStart = indent > 0 ? new string('\t', indent) : string.Empty;

        sb.Append(lineStart).Append(exception.GetType().Name).Append(';');

        if (exception.Message != null)
        {
            sb.Append(lineStart).Append(nameof(Exception.Message)).Append(';');

            foreach (string messageLine in exception.Message
                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList())
            {
                sb.Append(lineStart).Append("   ").Append(messageLine).Append(';');
            }
        }

        if (exception is AggregateException ae)
        {
            foreach (var aeInnerEx in ae.Flatten().InnerExceptions)
            {
                sb.Append(aeInnerEx.ToStringWithInner(indent + 1) + ";");
            }
        }
        else if (exception.InnerException != null)
        {
            sb.Append(exception.InnerException.ToStringWithInner(indent + 1)).Append(';');
        }

        return sb.ToString();
    }
    public static string ToStringInnerStackTrace(this Exception exception, int indent = 0)
    {
        StringBuilder sb = new();

        string lineStart = indent > 0 ? new string('\t', indent) : string.Empty;

        sb.Append(lineStart).Append(exception.GetType().Name).Append(';');

        if (exception.Message != null)
        {
            sb.Append(lineStart).Append(nameof(Exception.Message)).Append(';');

            foreach (string messageLine in exception.Message
                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList())
            {
                sb.Append(lineStart).Append("   ").Append(messageLine).Append(';');
            }
        }
        if (exception.StackTrace != null)
        {
            sb.Append($"{lineStart}{nameof(Exception.StackTrace)};");

            foreach (string stackTraceLine in exception.StackTrace
                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList())
            {
                sb.Append(lineStart).Append(stackTraceLine).Append(';');
            }
        }

        if (exception is AggregateException ae)
        {
            foreach (var aeInnerEx in ae.Flatten().InnerExceptions)
            {
                sb.Append(aeInnerEx.ToStringInnerStackTrace(indent + 1)).Append(';');
            }
        }
        else if (exception.InnerException != null)
        {
            sb.Append(exception.InnerException.ToStringInnerStackTrace(indent + 1) + ";");
        }

        return sb.ToString();
    }
}