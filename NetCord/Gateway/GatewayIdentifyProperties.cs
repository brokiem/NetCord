using System.Text.Json.Serialization;

namespace NetCord.Gateway;

internal class GatewayIdentifyProperties(string token)
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = token;

    [JsonPropertyName("capabilities")]
    public int? Capabilities { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("properties")]
    public ConnectionPropertiesProperties? ConnectionProperties { get; set; }

    [JsonPropertyName("presence")]
    public PresenceProperties? Presence { get; set; }
    
    [JsonPropertyName("compress")]
    public bool? Compress { get; set; }
    
    [JsonPropertyName("client_state")]
    public ClientStateProperties? ClientState { get; set; }

}
