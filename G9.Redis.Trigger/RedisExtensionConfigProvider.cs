using Microsoft.Azure.WebJobs.Host.Config;

namespace G9.Redis.Trigger;

internal sealed class RedisExtensionConfigProvider : IExtensionConfigProvider
{
    private readonly RedisTriggerBindingProvider triggerBindingProvider;

    public RedisExtensionConfigProvider(RedisTriggerBindingProvider triggerBindingProvider) 
        => this.triggerBindingProvider = triggerBindingProvider ?? throw new ArgumentNullException(nameof(triggerBindingProvider));

    public void Initialize(ExtensionConfigContext context)
    {
        var triggerRule = context.AddBindingRule<RedisTriggerAttribute>();
        triggerRule.BindToTrigger(triggerBindingProvider);
    }
}
