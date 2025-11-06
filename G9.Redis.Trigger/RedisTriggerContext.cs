using StackExchange.Redis;

namespace G9.Redis.Trigger;

internal sealed class RedisTriggerContext
{
    public RedisTriggerOptions Options { get; }
    public string Channel { get; }

    public RedisTriggerContext(RedisTriggerOptions options, string channel)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options));
        Channel = channel ?? throw new ArgumentNullException(nameof(channel));
    }
}