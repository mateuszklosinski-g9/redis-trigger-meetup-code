namespace G9.Redis.Trigger;

public sealed record RedisTriggerOptions
{
    public const string ConfigSection = "RedisTrigger";

    public string ConnectionString { get; init; } = default;
}