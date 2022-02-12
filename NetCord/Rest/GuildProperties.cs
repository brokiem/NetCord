﻿using System.Text.Json.Serialization;

namespace NetCord;

public class GuildProperties
{
    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("icon")]
    public Image? Icon { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("verification_level")]
    public VerificationLevel? VerificationLevel { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("default_message_notifications")]
    public DefaultMessageNotificationLevel? DefaultMessageNotificationLevel { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("explicit_content_filter")]
    public ContentFilter? ContentFilter { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("roles")]
    public IEnumerable<GuildRoleProperties>? Roles { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("channels")]
    public IEnumerable<GuildChannelProperties>? Channels { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("afk_channel_id")]
    public DiscordId? AfkChannelId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("afk_timeout")]
    public int? AfkTimeout { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("system_channel_id")]
    public DiscordId? SystemChannelId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("system_channel_flags")]
    public SystemChannelFlags? SystemChannelFlags { get; set; }

    public GuildProperties(string name)
    {
        Name = name;
    }
}