using G9.Redis.Trigger.Isolated;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace G9.Redis.Subscriber;

public class RedisSubscriberFunction
{
    private readonly ILogger<RedisSubscriberFunction> _logger;

    public RedisSubscriberFunction(ILogger<RedisSubscriberFunction> logger)
    {
        _logger = logger;
    }

	[Function("RedisSubscriber")]
	public Task HandleRedisMessage([RedisTrigger("test")] string message)
	{
		_logger.LogInformation($"[Redis Subscriber]: Received message: {message}");
        return Task.CompletedTask;
	}
}