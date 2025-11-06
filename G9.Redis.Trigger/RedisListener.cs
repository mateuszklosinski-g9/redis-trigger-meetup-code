using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;
using StackExchange.Redis;

namespace G9.Redis.Trigger;

internal sealed class RedisListener : IListener
{
    private readonly ITriggeredFunctionExecutor executor;
    private readonly RedisTriggerContext triggerContext;
    private readonly ConnectionMultiplexer connectionMultiplexer;
    private readonly ISubscriber subscriber;

	public RedisListener(ITriggeredFunctionExecutor executor, RedisTriggerContext triggerContext)
    {
        this.executor = executor;
        this.triggerContext = triggerContext;

        connectionMultiplexer = ConnectionMultiplexer.Connect(triggerContext.Options.ConnectionString);
        subscriber = connectionMultiplexer.GetSubscriber();
	}

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var redisChannel = new RedisChannel(triggerContext.Channel, RedisChannel.PatternMode.Literal);

		return subscriber.SubscribeAsync(redisChannel, async (channel, message) =>
        {
            var triggerData = new TriggeredFunctionData
            {
                TriggerValue = message.ToString()
            };

            await executor.TryExecuteAsync(triggerData, cancellationToken);
        });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Cancel();
        return Task.CompletedTask;
    }

    public void Cancel() => subscriber.UnsubscribeAll();

    public void Dispose()
    {
        connectionMultiplexer.Dispose();
    }
}