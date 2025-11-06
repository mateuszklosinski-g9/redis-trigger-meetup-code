using G9.Redis.Trigger;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(RedisBindingStartupExtension))]
namespace G9.Redis.Trigger;

public class RedisBindingStartupExtension : IWebJobsStartup
{
    public void Configure(IWebJobsBuilder builder)
    {
		builder.Services.AddTransient(sp =>
		{
			var configuration = sp.GetRequiredService<IConfiguration>();

			var redisTriggerOptions = new RedisTriggerOptions();

			configuration.Bind(RedisTriggerOptions.ConfigSection, redisTriggerOptions);

			return redisTriggerOptions;
		});

		builder.Services.AddSingleton<RedisTriggerBindingProvider>();

        builder.AddExtension<RedisExtensionConfigProvider>();
    }
}