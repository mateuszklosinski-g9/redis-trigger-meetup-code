namespace G9.Redis.Trigger;

public sealed class RedisTriggerContext
{
    public IServiceProvider ServiceProvider { get; }
    public RedisTriggerOptions Options { get; }
    public string Channel { get; }

    public RedisTriggerContext(IServiceProvider serviceProvider, RedisTriggerOptions options, string subscriptionKey)
    {
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        Options = options ?? throw new ArgumentNullException(nameof(options));
        Channel = subscriptionKey ?? throw new ArgumentNullException(nameof(subscriptionKey));
    }
}