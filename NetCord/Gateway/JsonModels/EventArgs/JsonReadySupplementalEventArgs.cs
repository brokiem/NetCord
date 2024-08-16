using System.Text.Json.Serialization;

using NetCord.JsonModels;

namespace NetCord.Gateway.JsonModels.EventArgs;

public class JsonReadySupplementalEventArgs
{
    // Missing properties
    //  merged_presences
    //  lazy_private_channels
    //  game_invites

    public partial class JsonReadySupplementalGuild : JsonEntity
    {
        [JsonPropertyName("voice_states")]
        public JsonVoiceState[] VoiceStates { get; set; }

        [JsonPropertyName("embedded_activities")]
        public object[] EmbeddedActivities { get; set; }
    }

    [JsonPropertyName("merged_members")]
    public JsonGuildUser[][] MergedMembers { get; set; }
    
    [JsonPropertyName("guilds")]
    public JsonReadySupplementalGuild[] Guilds { get; set; }

    [JsonPropertyName("disclose")]
    public string[] Disclose { get; set; }
}
