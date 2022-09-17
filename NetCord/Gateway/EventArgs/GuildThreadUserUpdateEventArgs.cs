﻿namespace NetCord.Gateway;

public class GuildThreadUserUpdateEventArgs
{
    public GuildThreadUserUpdateEventArgs(ThreadUser user, Snowflake guildId)
    {
        User = user;
        GuildId = guildId;
    }

    public ThreadUser User { get; }

    public Snowflake GuildId { get; }
}