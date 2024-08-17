using System.Collections.Immutable;
using System.Text.Json.Serialization;

using NetCord.Gateway.JsonModels;
using NetCord.Rest;

namespace NetCord.JsonModels;

public class JsonGuild : JsonEntity
{
    [JsonPropertyName("icon_hash")]
    public string? IconHashTemplate { get; set; }

    [JsonPropertyName("owner")]
    public bool IsOwner { get; set; }

    [JsonPropertyName("permissions")]
    public Permissions? Permissions { get; set; }

    [JsonPropertyName("widget_enabled")]
    public bool? WidgetEnabled { get; set; }

    [JsonPropertyName("widget_channel_id")]
    public ulong? WidgetChannelId { get; set; }

    [JsonConverter(typeof(JsonConverters.JsonRoleArrayToImmutableDictionaryConverter))]
    [JsonPropertyName("roles")]
    public ImmutableDictionary<ulong, JsonRole> Roles { get; set; }
    
    [JsonPropertyName("properties")]
    public JsonGuildPropertiesV2 Properties { get; set; }

    [JsonPropertyName("emojis")]
    public ImmutableArray<JsonEmoji> Emojis { get; set; }

    [JsonPropertyName("joined_at")]
    public DateTimeOffset JoinedAt { get; set; }

    [JsonPropertyName("large")]
    public bool IsLarge { get; set; }

    [JsonPropertyName("unavailable")]
    public bool IsUnavailable { get; set; }

    [JsonPropertyName("member_count")]
    public int UserCount { get; set; }

    [JsonConverter(typeof(JsonConverters.JsonVoiceStateArrayToImmutableDictionaryConverter))]
    [JsonPropertyName("voice_states")]
    public ImmutableDictionary<ulong, JsonVoiceState> VoiceStates { get; set; }

    [JsonConverter(typeof(JsonConverters.JsonGuildUserArrayToImmutableDictionaryConverter))]
    [JsonPropertyName("members")]
    public ImmutableDictionary<ulong, JsonGuildUser> Users { get; set; }

    [JsonConverter(typeof(JsonConverters.JsonChannelArrayToImmutableDictionaryConverter))]
    [JsonPropertyName("channels")]
    public ImmutableDictionary<ulong, JsonChannel> Channels { get; set; }

    [JsonConverter(typeof(JsonConverters.JsonChannelArrayToImmutableDictionaryConverter))]
    [JsonPropertyName("threads")]
    public ImmutableDictionary<ulong, JsonChannel> ActiveThreads { get; set; }

    [JsonConverter(typeof(JsonConverters.JsonPresenceArrayToImmutableDictionaryConverter))]
    [JsonPropertyName("presences")]
    public ImmutableDictionary<ulong, JsonPresence> Presences { get; set; }

    [JsonPropertyName("max_presences")]
    public int? MaxPresences { get; set; }

    [JsonPropertyName("premium_subscription_count")]
    public int? PremiumSubscriptionCount { get; set; }

    [JsonPropertyName("approximate_member_count")]
    public int? ApproximateUserCount { get; set; }

    [JsonPropertyName("approximate_presence_count")]
    public int? ApproximatePresenceCount { get; set; }

    [JsonPropertyName("welcome_screen")]
    public JsonGuildWelcomeScreen? WelcomeScreen { get; set; }

    [JsonConverter(typeof(JsonConverters.JsonStageInstanceArrayToImmutableDictionaryConverter))]
    [JsonPropertyName("stage_instances")]
    public ImmutableDictionary<ulong, JsonStageInstance> StageInstances { get; set; }

    [JsonPropertyName("stickers")]
    public ImmutableArray<JsonSticker> Stickers { get; set; }

    [JsonConverter(typeof(JsonConverters.JsonGuildScheduledEventArrayToImmutableDictionaryConverter))]
    [JsonPropertyName("guild_scheduled_events")]
    public ImmutableDictionary<ulong, JsonGuildScheduledEvent> ScheduledEvents { get; set; }
}
