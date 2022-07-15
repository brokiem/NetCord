using System.Text.Json;

using DeepEqual.Syntax;

using NetCord;
using NetCord.Gateway;
using NetCord.JsonModels;

namespace SerializationAndDeserializationTest;

[TestClass]
public class Test
{
    [TestMethod]
    public async Task TestAsync()
    {
        GatewayClient client = new(new(TokenType.Bot, Environment.GetEnvironmentVariable("OriginalTurboBoat")!), new()
        {
            Intents = GatewayIntent.All,
        });
        Guild? guild = null;
        TaskCompletionSource completionSource = new();
        client.GuildCreate += g =>
        {
            if (g.GuildId == 819892011364122624)
            {
                if (g.Guild == null)
                    throw new Exception($"{nameof(g.Guild)} was null");
                client.Dispose();
                guild = g.Guild;
                completionSource.SetResult();
            }
            return default;
        };
        await client.StartAsync().ConfigureAwait(false);
        await completionSource.Task.ConfigureAwait(false);
        var model = ((IJsonModel<JsonGuild>)guild!).JsonModel;
        Console.WriteLine($"old: {model}");
        var json = JsonSerializer.Serialize(model, Serialization.Options);
        var deserialized = JsonSerializer.Deserialize<JsonGuild>(json, Serialization.Options)!;
        Console.WriteLine($"new: {deserialized}");
        Guild newGuild = new(deserialized, client.Rest);

        Assert.IsTrue(model.IsDeepEqual(deserialized));
    }
}