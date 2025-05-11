using Microsoft.Azure.WebJobs.Description;

namespace G9.Redis.Trigger;

[AttributeUsage(AttributeTargets.Parameter)]
[Binding]
public sealed class RedisTriggerAttribute : Attribute
{
    public string Channel { get; }

    public RedisTriggerAttribute(string channel)
    {
        Channel = channel;
    }
}