using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using NetCord.Rest;

namespace NetCord.Gateway;

public partial class ConnectionPropertiesProperties
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("os")]
    public string? Os { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("browser")]
    public string? Browser { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("device")]
    public string? Device { get; set; }
    
    [JsonPropertyName("release_channel")]
    public string? ReleaseChannel { get; set; }

    [JsonPropertyName("client_version")]
    public string? ClientVersion { get; set; }

    [JsonPropertyName("os_version")]
    public string? OsVersion { get; set; }

    [JsonPropertyName("os_arch")]
    public string? OsArch { get; set; }

    [JsonPropertyName("app_arch")]
    public string? AppArch { get; set; }

    [JsonPropertyName("system_locale")]
    public string? SystemLocale { get; set; }

    [JsonPropertyName("browser_user_agent")]
    public string? BrowserUserAgent { get; set; }

    [JsonPropertyName("browser_version")]
    public string? BrowserVersion { get; set; }

    [JsonPropertyName("client_build_number")]
    public int? ClientBuildNumber { get; set; }

    [JsonPropertyName("native_build_number")]
    public int? NativeBuildNumber { get; set; }

    [JsonPropertyName("client_event_source")]
    public string? ClientEventSource { get; set; }
}
