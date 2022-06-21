﻿namespace NetCord;

public class GuildChannelMention : Entity
{
    private readonly JsonModels.JsonGuildChannelMention _jsonModel;

    public override Snowflake Id => _jsonModel.Id;
    public Snowflake GuildId => _jsonModel.GuildId;
    public ChannelType Type => _jsonModel.Type;
    public string Name => _jsonModel.Name;

    public GuildChannelMention(JsonModels.JsonGuildChannelMention jsonModel)
    {
        _jsonModel = jsonModel;
    }
}