﻿namespace NetCord.Rest;

public partial class RestClient
{
    public async Task<Webhook> CreateWebhookAsync(ulong channelId, WebhookProperties webhookProperties, RequestProperties? properties = null)
        => Webhook.CreateFromJson((await SendRequestAsync(HttpMethod.Post, $"/channels/{channelId}/webhooks", new JsonContent<WebhookProperties>(webhookProperties, WebhookProperties.WebhookPropertiesSerializerContext.WithOptions.WebhookProperties), properties).ConfigureAwait(false)).ToObject(JsonModels.JsonWebhook.JsonWebhookSerializerContext.WithOptions.JsonWebhook), this);

    public async Task<IReadOnlyDictionary<ulong, Webhook>> GetChannelWebhooksAsync(ulong channelId, RequestProperties? properties = null)
        => (await SendRequestAsync(HttpMethod.Get, $"/channels/{channelId}/webhooks", properties).ConfigureAwait(false)).ToObject(JsonModels.JsonWebhook.JsonWebhookArraySerializerContext.WithOptions.JsonWebhookArray).ToDictionary(w => w.Id, w => Webhook.CreateFromJson(w, this));

    public async Task<IReadOnlyDictionary<ulong, Webhook>> GetGuildWebhooksAsync(ulong guildId, RequestProperties? properties = null)
        => (await SendRequestAsync(HttpMethod.Get, $"/guilds/{guildId}/webhooks", properties).ConfigureAwait(false)).ToObject(JsonModels.JsonWebhook.JsonWebhookArraySerializerContext.WithOptions.JsonWebhookArray).ToDictionary(w => w.Id, w => Webhook.CreateFromJson(w, this));

    public async Task<Webhook> GetWebhookAsync(ulong webhookId, RequestProperties? properties = null)
        => Webhook.CreateFromJson((await SendRequestAsync(HttpMethod.Get, $"/webhooks/{webhookId}", properties).ConfigureAwait(false)).ToObject(JsonModels.JsonWebhook.JsonWebhookSerializerContext.WithOptions.JsonWebhook), this);

    public async Task<Webhook> GetWebhookWithTokenAsync(ulong webhookId, string webhookToken, RequestProperties? properties = null)
        => Webhook.CreateFromJson((await SendRequestAsync(HttpMethod.Get, $"/webhooks/{webhookId}/{webhookToken}", properties).ConfigureAwait(false)).ToObject(JsonModels.JsonWebhook.JsonWebhookSerializerContext.WithOptions.JsonWebhook), this);

    public async Task<Webhook> ModifyWebhookAsync(ulong webhookId, Action<WebhookOptions> action, RequestProperties? properties = null)
    {
        WebhookOptions webhookOptions = new();
        action(webhookOptions);
        return Webhook.CreateFromJson((await SendRequestAsync(HttpMethod.Patch, $"/webhooks/{webhookId}", new JsonContent<WebhookOptions>(webhookOptions, WebhookOptions.WebhookOptionsSerializerContext.WithOptions.WebhookOptions), properties).ConfigureAwait(false)).ToObject(JsonModels.JsonWebhook.JsonWebhookSerializerContext.WithOptions.JsonWebhook), this);
    }

    public async Task<Webhook> ModifyWebhookWithTokenAsync(ulong webhookId, string webhookToken, Action<WebhookOptions> action, RequestProperties? properties = null)
    {
        WebhookOptions webhookOptions = new();
        action(webhookOptions);
        return Webhook.CreateFromJson((await SendRequestAsync(HttpMethod.Patch, $"/webhooks/{webhookId}/{webhookToken}", new JsonContent<WebhookOptions>(webhookOptions, WebhookOptions.WebhookOptionsSerializerContext.WithOptions.WebhookOptions), properties).ConfigureAwait(false)).ToObject(JsonModels.JsonWebhook.JsonWebhookSerializerContext.WithOptions.JsonWebhook), this);
    }

    public Task DeleteWebhookAsync(ulong webhookId, RequestProperties? properties = null)
        => SendRequestAsync(HttpMethod.Delete, $"/webhooks/{webhookId}", properties);

    public Task DeleteWebhookWithTokenAsync(ulong webhookId, string webhookToken, RequestProperties? properties = null)
        => SendRequestAsync(HttpMethod.Delete, $"/webhooks/{webhookId}/{webhookToken}", properties);

    public async Task<RestMessage?> ExecuteWebhookAsync(ulong webhookId, string webhookToken, WebhookMessageProperties messageProperties, bool wait = false, ulong? threadId = null, RequestProperties? properties = null)
    {
        if (wait)
            return new((await SendRequestAsync(HttpMethod.Post, threadId.HasValue ? $"/webhooks/{webhookId}/{webhookToken}?wait=True&thread_id={threadId.GetValueOrDefault()}" : $"/webhooks/{webhookId}/{webhookToken}?wait=True", new(RateLimits.RouteParameter.ExecuteWebhookModifyDeleteWebhookMessage), messageProperties.Build(), properties).ConfigureAwait(false)).ToObject(JsonModels.JsonMessage.JsonMessageSerializerContext.WithOptions.JsonMessage), this);
        else
        {
            await SendRequestAsync(HttpMethod.Post, threadId.HasValue ? $"/webhooks/{webhookId}/{webhookToken}?wait=False&thread_id={threadId.GetValueOrDefault()}" : $"/webhooks/{webhookId}/{webhookToken}?wait=False", new(RateLimits.RouteParameter.ExecuteWebhookModifyDeleteWebhookMessage), messageProperties.Build(), properties).ConfigureAwait(false);
            return null;
        }
    }

    public async Task<WebhookMessage> GetWebhookMessageAsync(ulong webhookId, string webhookToken, ulong messageId, RequestProperties? properties = null)
        => new((await SendRequestAsync(HttpMethod.Get, $"/webhooks/{webhookId}/{webhookToken}/messages/{messageId}", new RateLimits.Route(RateLimits.RouteParameter.ExecuteWebhookModifyDeleteWebhookMessage), properties).ConfigureAwait(false)).ToObject(JsonModels.JsonMessage.JsonMessageSerializerContext.WithOptions.JsonMessage), this);

    public async Task<WebhookMessage> ModifyWebhookMessageAsync(ulong webhookId, string webhookToken, ulong messageId, Action<MessageOptions> action, ulong? threadId = null, RequestProperties? properties = null)
    {
        MessageOptions messageOptions = new();
        action(messageOptions);
        return new((await SendRequestAsync(HttpMethod.Patch, threadId.HasValue ? $"/webhooks/{webhookId}/{webhookToken}/messages/{messageId}?thread_id={threadId.GetValueOrDefault()}" : $"/webhooks/{webhookId}/{webhookToken}/messages/{messageId}", new(RateLimits.RouteParameter.ExecuteWebhookModifyDeleteWebhookMessage), messageOptions.Build(), properties).ConfigureAwait(false)).ToObject(JsonModels.JsonMessage.JsonMessageSerializerContext.WithOptions.JsonMessage), this);
    }

    public Task DeleteWebhookMessageAsync(ulong webhookId, string webhookToken, ulong messageId, ulong? threadId = null, RequestProperties? properties = null)
        => SendRequestAsync(HttpMethod.Delete, threadId.HasValue ? $"/webhooks/{webhookId}/{webhookToken}/messages/{messageId}?thread_id={threadId.GetValueOrDefault()}" : $"/webhooks/{webhookId}/{webhookToken}/messages/{messageId}", new RateLimits.Route(RateLimits.RouteParameter.ExecuteWebhookModifyDeleteWebhookMessage), properties);
}
