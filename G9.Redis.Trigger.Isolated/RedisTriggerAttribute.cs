using Microsoft.Azure.Functions.Worker.Extensions.Abstractions;

[assembly: ExtensionInformation("G9.Redis.Trigger", "1.0.4")]

namespace G9.Redis.Trigger.Isolated;

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class RedisTriggerAttribute : TriggerBindingAttribute
{
    public string Channel { get; }

    public RedisTriggerAttribute(string channel)
    {
		Channel = channel;
    }
}