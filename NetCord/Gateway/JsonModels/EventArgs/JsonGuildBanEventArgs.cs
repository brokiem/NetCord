﻿using System.Text.Json.Serialization;

using NetCord.JsonModels;

namespace NetCord.Gateway.JsonModels.EventArgs;

public partial class JsonGuildBanEventArgs
{
    [JsonPropertyName("guild_id")]
    public ulong GuildId { get; set; }

    [JsonPropertyName("user")]
    public JsonUser User { get; set; }

    [JsonSerializable(typeof(JsonGuildBanEventArgs))]
    public partial class JsonGuildBanEventArgsSerializerContext : JsonSerializerContext
    {
        public static JsonGuildBanEventArgsSerializerContext WithOptions { get; } = new(Serialization.Options);
    }
}