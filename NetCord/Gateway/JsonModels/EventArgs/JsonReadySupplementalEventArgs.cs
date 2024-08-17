using System.Text.Json.Serialization;

using NetCord.JsonModels;

namespace NetCord.Gateway.JsonModels.EventArgs;

public class JsonReadySupplementalEventArgs
{
    // Missing properties
    //  merged_presences
    //  lazy_private_channels
    //  game_invites

    public class JsonReadySupplementalGuild : JsonEntity
    {
        [JsonPropertyName("voice_states")]
        public JsonVoiceState[] VoiceStates { get; set; }
    }
    
    public class JsonReadySupplementalMember : JsonEntity
    {
        [JsonPropertyName("roles")]
        public ulong[] RoleIds { get; set; }
        
        [JsonPropertyName("premium_since")]
        public DateTimeOffset? GuildBoostStart { get; set; }
        
        [JsonPropertyName("pending")]
        public bool? IsPending { get; set; }
        
        [JsonPropertyName("nick")]
        public string? Nickname { get; set; }
        
        [JsonPropertyName("mute")]
        public bool Muted { get; set; }
        
        [JsonPropertyName("joined_at")]
        public DateTimeOffset JoinedAt { get; set; }
        
        [JsonPropertyName("flags")]
        public GuildUserFlags GuildFlags { get; set; }
        
        [JsonPropertyName("deaf")]
        public bool Deafened { get; set; }
        
        [JsonPropertyName("communication_disabled_until")]
        public DateTimeOffset? TimeOutUntil { get; set; }
        
        [JsonPropertyName("banner")]
        public string? BannerHash { get; set; }
        
        [JsonPropertyName("avatar")]
        public string? AvatarHash { get; set; }
    }

    [JsonPropertyName("merged_members")]
    public JsonReadySupplementalMember[][] MergedMembers { get; set; }
    
    [JsonPropertyName("guilds")]
    public JsonReadySupplementalGuild[] Guilds { get; set; }

    [JsonPropertyName("disclose")]
    public string[] Disclose { get; set; }
}
