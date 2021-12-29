﻿using NetCord.Commands;

namespace NetCord.Test.Commands.Administrative;

public class OtherCommands : CommandModule
{
    [Command("kick", RequiredUserPermissions = Permission.KickUsers, RequiredBotPermissions = Permission.KickUsers)]
    public static Task Kick(GuildUser user, [Remainder] string reason = null)
    {
        return user.KickAsync(new() { AuditLogReason = reason });
    }

    [Command("clear", "purge", RequiredBotChannelPermissions = Permission.ManageMessages, RequiredUserChannelPermissions = Permission.ManageMessages)]
    public async Task Clear(int count)
    {
        if (count < 1)
        {
            await ReplyAsync("To few messages!");
            return;
        }
        await Context.Client.Rest.Message.DeleteAsync(Context.Channel, GetMessagesToRemove());
        if (count == 1)
            await SendAsync("**Deleted 1 message!**");
        else
            await SendAsync($"**Deleted {count} messages!**");

        async IAsyncEnumerable<DiscordId> GetMessagesToRemove()
        {
            int i = 0;
            await foreach (var message in Context.Channel.GetMessagesAsync())
            {
                if (i == count || (DateTimeOffset.UtcNow - message.CreatedAt).TotalDays > 14)
                    break;
                yield return message;
                i++;
            }
            count = i;
        }
    }
}