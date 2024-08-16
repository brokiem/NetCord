using System.Text.Json.Serialization;

namespace NetCord.JsonModels;

public class JsonEntity
{
    [JsonPropertyName("id")]
    public virtual ulong Id { get; set; }

    // present in merged_members
    [JsonPropertyName("user_id")]
    private ulong UserId { set { Id = value; } }
}
