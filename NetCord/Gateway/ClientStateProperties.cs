using System.Text.Json.Serialization;

namespace NetCord.Gateway;

public partial class ClientStateProperties
{
    public static ClientStateProperties Default => new()
    {
        ApiCodeVersion = 0,
        GuildVersions = [],
        HighestLastMessageId = "0",
        PrivateChannelsVersion = "0",
        ReadStateVersion = 0,
        UserGuildSettingsVersion = -1,
        UserSettingsVersion = -1
    };

    [JsonPropertyName("api_code_version")]
    public int ApiCodeVersion { get; set; }

    [JsonPropertyName("guild_versions")]
    public IEnumerable<object>? GuildVersions { get; set; }

    [JsonPropertyName("highest_last_message_id")]
    public string? HighestLastMessageId { get; set; }

    [JsonPropertyName("private_channels_version")]
    public string? PrivateChannelsVersion { get; set; }

    [JsonPropertyName("read_state_version")]
    public int ReadStateVersion { get; set; }

    [JsonPropertyName("user_guild_settings_version")]
    public int UserGuildSettingsVersion { get; set; }

    [JsonPropertyName("user_settings_version")]
    public int UserSettingsVersion { get; set; }
}
