using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace G9.Redis.Trigger;

internal sealed class RedisTriggerBinding : ITriggerBinding
{
    private readonly RedisTriggerContext context;

    public Type TriggerValueType => typeof(string);

    public IReadOnlyDictionary<string, Type> BindingDataContract => new Dictionary<string, Type>();

    public RedisTriggerBinding(RedisTriggerContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
    {
		if (value is not string stringValue)
        {
			throw new InvalidOperationException("Redis trigger only supports string values.");
		}

		var valueProvider = new RedisValueBinder(stringValue);
        var triggerData = new TriggerData(valueProvider, new Dictionary<string, object>());

        return Task.FromResult<ITriggerData>(triggerData);
    }

    public Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
        => Task.FromResult<IListener>(new RedisListener(context.Executor, this.context));

    public ParameterDescriptor ToParameterDescriptor()
    {
        return new TriggerParameterDescriptor
        {
            Name = "Redis Trigger",
            DisplayHints = new ParameterDisplayHints
            {
                Prompt = "Redis Trigger",
                Description = "Trigger to subscribe for messages from a Pub/Sub Channel."
            }
        };
    }
}