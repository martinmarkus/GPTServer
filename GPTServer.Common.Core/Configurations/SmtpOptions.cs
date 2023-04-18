namespace GPTServer.Common.Core.Configurations;

public class SmtpOptions
{
    public string Host { get; set; }

    public int Port { get; set; }

    public bool EnableSsl { get; set; }
}