using G9.Redis.Trigger;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace G9.Redis.Subscriber.Debugger
{
    public static class RedisDebuggerFunction
    {
        [FunctionName("RedisDebugger")]
        public static Task ReceiveRedisMessages(
			[RedisTrigger("test")] string message,
			ILogger log)
		{
			log.LogInformation($"[Redis Subscriber Debugger] Received message: {message}");
			return Task.CompletedTask;
		}
	}
}
