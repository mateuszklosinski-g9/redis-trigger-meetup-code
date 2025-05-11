using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace G9.Redis.Trigger;

internal sealed class RedisTriggerBindingProvider : ITriggerBindingProvider
{
    private readonly IServiceProvider serviceProvider;
    private readonly RedisTriggerOptions options;

    public RedisTriggerBindingProvider(IServiceProvider serviceProvider, RedisTriggerOptions options)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
    {
        var parameter = context.Parameter;
        var attribute = parameter.GetCustomAttribute<RedisTriggerAttribute>(false);

        if (attribute is null)
        {
            return Task.FromResult<ITriggerBinding>(null!);
        }

        if (parameter.ParameterType != typeof(string))
        {
            throw new InvalidOperationException($"Invalid parameter type: {parameter.ParameterType.Name}, parameter must be of type: {nameof(String)}");
        }

        var triggerBinding = new RedisTriggerBinding(new RedisTriggerContext(serviceProvider, options, attribute.Channel));

        return Task.FromResult<ITriggerBinding>(triggerBinding);
    }
}