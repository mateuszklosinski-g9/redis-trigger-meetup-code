using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace G9.Redis.Trigger;

public sealed class RedisListener : IListener
{
    private readonly ITriggeredFunctionExecutor executor;
    private readonly RedisTriggerContext triggerContext;
    private readonly IServiceScope scope;
	private readonly ISubscriber subscriber;

	public RedisListener(ITriggeredFunctionExecutor executor, RedisTriggerContext triggerContext)
    {
        this.executor = executor;
        this.triggerContext = triggerContext;

        scope = this.triggerContext.ServiceProvider.CreateScope();
        var connectionMultiplexer = scope.ServiceProvider.GetRequiredService<ConnectionMultiplexer>();
        subscriber = connectionMultiplexer.GetSubscriber();
	}

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return subscriber.SubscribeAsync(triggerContext.Channel, async (channel, message) =>
        {
            var triggerData = new TriggeredFunctionData
            {
                TriggerValue = message.ToString()
            };

            await executor.TryExecuteAsync(triggerData, default);
        });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Cancel();
        return Task.CompletedTask;
    }

    public void Cancel() => subscriber.UnsubscribeAll();

    public void Dispose() => scope.Dispose();
}