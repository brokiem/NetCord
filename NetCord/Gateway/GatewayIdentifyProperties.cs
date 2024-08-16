using System.Text.Json.Serialization;

namespace NetCord.Gateway;

internal class GatewayIdentifyProperties(string token)
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = token;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("properties")]
    public ConnectionPropertiesProperties? ConnectionProperties { get; set; }

    //[JsonPropertyName("compress")]
    //public bool? Compress { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("large_threshold")]
    public int? LargeThreshold { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("shard")]
    public Shard? Shard { get; set; }

    [JsonPropertyName("presence")]
    public PresenceProperties? Presence { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("intents")]
    public GatewayIntents? Intents { get; set; }
    
    [JsonPropertyName("client_state")]
    public ClientStateProperties? ClientState { get; set; }

    [JsonPropertyName("capabilities")]
    public int? Capabilities { get; set; }
}
