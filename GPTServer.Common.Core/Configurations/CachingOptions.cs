namespace GPTServer.Common.Core.Configurations;

public class CachingOptions
{
    public bool UseRedis { get; set; }

    public string RedisUrl { get; set; }

    public int DefaultMinutesToLive { get; set; }
}