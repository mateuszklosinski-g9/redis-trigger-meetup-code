﻿using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;

namespace G9.Redis.Trigger;

internal sealed class RedisTriggerBindingProvider : ITriggerBindingProvider
{
    private readonly ConnectionMultiplexer connectionMultiplexer;
    private readonly RedisTriggerOptions options;

    public RedisTriggerBindingProvider(ConnectionMultiplexer connectionMultiplexer, RedisTriggerOptions options)
    {
        this.connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
    {
        var parameter = context.Parameter;
        var attribute = parameter.GetCustomAttribute<RedisTriggerAttribute>(false);

        if (attribute is null)
        {
            return Task.FromResult<ITriggerBinding>(null!);
        }

        if (parameter.ParameterType != typeof(string))
        {
            throw new InvalidOperationException($"Invalid parameter type: {parameter.ParameterType.Name}, parameter must be of type: {nameof(String)}");
        }

        var triggerBinding = new RedisTriggerBinding(new RedisTriggerContext(connectionMultiplexer, options, attribute.Channel));

        return Task.FromResult<ITriggerBinding>(triggerBinding);
    }
}