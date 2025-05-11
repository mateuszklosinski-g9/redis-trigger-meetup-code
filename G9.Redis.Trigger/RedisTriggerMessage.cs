namespace G9.Redis.Trigger;

public sealed class RedisTriggerMessage
{
    public string Channel { get; }
    public string Message { get; }

    public RedisTriggerMessage(string channel, string message)
    {
        Channel = channel;
        Message = message;
    }
}
