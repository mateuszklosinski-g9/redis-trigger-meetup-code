using Microsoft.Azure.WebJobs.Host.Bindings;

namespace G9.Redis.Trigger;

internal sealed class RedisValueBinder : IValueBinder
{
    private object value;

    public Type Type => typeof(string);

    public RedisValueBinder(object value) => this.value = value;

	public Task<object> GetValueAsync() => Task.FromResult(value);

	public Task SetValueAsync(object value, CancellationToken cancellationToken)
    {
        this.value = value;
        return Task.CompletedTask;
    }

    public string ToInvokeString() => value.ToString() ?? string.Empty;
}
