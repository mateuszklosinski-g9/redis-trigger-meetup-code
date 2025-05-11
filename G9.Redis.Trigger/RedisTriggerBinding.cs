using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace G9.Redis.Trigger;

internal sealed class RedisTriggerBinding : ITriggerBinding
{
    private readonly RedisTriggerContext context;

    public Type TriggerValueType => typeof(string);

    public IReadOnlyDictionary<string, Type> BindingDataContract => new Dictionary<string, Type>
    {
        { "data", typeof(string) }
    };

    public RedisTriggerBinding(RedisTriggerContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
    {
        var valueProvider = new RedisValueBinder(value);
        var bindingData = new Dictionary<string, object>
        {
            { "data", value }
        };

        var triggerData = new TriggerData(valueProvider, bindingData);

        return Task.FromResult<ITriggerData>(triggerData);
    }

    public Task<IListener> CreateListenerAsync(ListenerFactoryContext context) => Task.FromResult<IListener>(new RedisListener(context.Executor, this.context));

    public ParameterDescriptor ToParameterDescriptor()
    {
        return new TriggerParameterDescriptor
        {
            Name = "Redis Trigger",
            DisplayHints = new ParameterDisplayHints
            {
                Prompt = "Redis Trigger",
                Description = "Trigger to subscribe for messages from a Pub/Sub Topic."
            }
        };
    }
}