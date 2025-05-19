using StackExchange.Redis;

namespace G9.Redis.Trigger;

public sealed class RedisTriggerContext
{
    public ConnectionMultiplexer ConnectionMultiplexer { get; }
    public RedisTriggerOptions Options { get; }
    public string Channel { get; }

    public RedisTriggerContext(ConnectionMultiplexer connectionMultiplexer, RedisTriggerOptions options, string subscriptionKey)
    {
        ConnectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
        Options = options ?? throw new ArgumentNullException(nameof(options));
        Channel = subscriptionKey ?? throw new ArgumentNullException(nameof(subscriptionKey));
    }
}