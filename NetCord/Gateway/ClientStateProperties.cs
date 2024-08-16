using System.Text.Json.Serialization;

namespace NetCord.Gateway;

public partial class ClientStateProperties
{
    public static ClientStateProperties Default => new()
    {
        ApiCodeVersion = 0,
        GuildVersions = new { },
        HighestLastMessageId = "0",
        PrivateChannelsVersion = "0",
        ReadStateVersion = 0,
        UserGuildSettingsVersion = -1,
        UserSettingsVersion = -1
    };

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("api_code_version")]
    public int ApiCodeVersion { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("guild_versions")]
    public object? GuildVersions { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("highest_last_message_id")]
    public string? HighestLastMessageId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("private_channels_version")]
    public string? PrivateChannelsVersion { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("read_state_version")]
    public int ReadStateVersion { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("user_guild_settings_version")]
    public int UserGuildSettingsVersion { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("user_settings_version")]
    public int UserSettingsVersion { get; set; }
}
