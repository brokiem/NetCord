using NetCord;
using NetCord.Gateway;
using NetCord.Services.ApplicationCommands;

GatewayClient client = new(new Token(TokenType.Bot, "Token from Discord Developer Portal"), new GatewayClientConfig()
{
    Intents = default,
});

ApplicationCommandService<SlashCommandContext> applicationCommandService = new();
applicationCommandService.AddModules(System.Reflection.Assembly.GetEntryAssembly()!);

client.Log += message =>
{
    Console.WriteLine(message);
    return default;
};
await client.StartAsync();
await client.ReadyAsync;
await applicationCommandService.CreateCommandsAsync(client.Rest, client.ApplicationId!.Value);

client.InteractionCreate += async interaction =>
{
    if (interaction is SlashCommandInteraction slashCommandInteraction)
    {
        try
        {
            await applicationCommandService.ExecuteAsync(new SlashCommandContext(slashCommandInteraction, client));
        }
        catch (Exception ex)
        {
            try
            {
                await interaction.SendResponseAsync(InteractionCallback.ChannelMessageWithSource($"Error: {ex.Message}"));
            }
            catch
            {
            }
        }
    }
};
await Task.Delay(-1);