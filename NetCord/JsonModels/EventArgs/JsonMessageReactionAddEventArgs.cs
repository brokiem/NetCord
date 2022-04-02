﻿using System.Text.Json.Serialization;

namespace NetCord.JsonModels.EventArgs;

internal record JsonMessageReactionAddEventArgs
{
    [JsonPropertyName("user_id")]
    public Snowflake UserId { get; init; }

    [JsonPropertyName("channel_id")]
    public Snowflake ChannelId { get; init; }

    [JsonPropertyName("message_id")]
    public Snowflake MessageId { get; init; }

    [JsonPropertyName("guild_id")]
    public Snowflake? GuildId { get; init; }

    [JsonPropertyName("member")]
    public JsonGuildUser? User { get; init; }

    [JsonPropertyName("emoji")]
    public JsonEmoji Emoji { get; init; }
}