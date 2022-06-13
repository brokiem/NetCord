﻿using System.Text.Json.Serialization;

namespace NetCord.JsonModels.EventArgs;

public record JsonWebhooksUpdateEventArgs
{
    [JsonPropertyName("guild_id")]
    public Snowflake GuildId { get; init; }

    [JsonPropertyName("channel_id")]
    public Snowflake ChannelId { get; init; }
}