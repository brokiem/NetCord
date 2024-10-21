# Setting Up Your First C# Discord Bot with NetCord

> [!NOTE]
> This guide assumes you have already created a project with NetCord installed and a bot set up in the [Discord Developer Portal](https://discord.com/developers/applications). If you haven't, please return to the previous guides for setup instructions.

<<<<<<< HEAD
Before we start, you need a token of your bot... so you need to go to the [Discord Developer Portal](https://discord.com/developers/applications) and get it.

- Go to `Bot` section and click `Reset Token`.
  ![Go to 'Bot' section and click 'Reset Token'](../../images/coding_Token_1.png)

- Then click `Copy` to copy the token to the clipboard.
  ![Click 'Copy' to copy the token to the clipboard](../../images/coding_Token_2.png)
=======
This guide will walk you through setting up your first C# Discord bot using NetCord. It will show you how to create a simple bot that connects to Discord. Nothing too fancy, just the basics to get you started. Let's dive in!
>>>>>>> 14fd5c38c7dc847d510b1c3781e25654bba5a223

## [Generic Host](#tab/generic-host)

<<<<<<< HEAD
## [Generic Host](#tab/generic-host)
=======
> [!NOTE]
> The generic host way requires the following packages:
> - [Microsoft.Extensions.Hosting](https://www.nuget.org/packages/Microsoft.Extensions.Hosting)
> - [NetCord.Hosting](https://www.nuget.org/packages/NetCord.Hosting)
>>>>>>> 14fd5c38c7dc847d510b1c3781e25654bba5a223

With the generic host, you can just use @NetCord.Hosting.Gateway.GatewayClientServiceCollectionExtensions.AddDiscordGateway(Microsoft.Extensions.DependencyInjection.IServiceCollection) to add your bot to the host. Quite easy, right?
[!code-cs[Program.cs](CodingHosting/Program.cs)]

Also note that the token should be stored in the configuration. You can for example use `appsettings.json` file. It should look like this:
[!code-json[appsettings.json](CodingHosting/appsettings.json)]

<<<<<<< HEAD
Now, when you run the code, your bot should be online!
![Bot being online](../../images/coding_BotOnline.png)

## [Bare Bones](#tab/bare-bones)

Add the following lines to file `Program.cs`.
=======
## [Bare Bones](#tab/bare-bones)

Add the following lines to the `Program.cs` file to create the @NetCord.Gateway.GatewayClient.
>>>>>>> 14fd5c38c7dc847d510b1c3781e25654bba5a223
[!code-cs[Program.cs](Coding/Program.cs#L1-L4)]

Then add logging, to know what your bot is doing.
[!code-cs[Program.cs](Coding/Program.cs#L6-L10)]

Now it's time to finally... make the bot online! To do it, add the following lines to your code.
[!code-cs[Program.cs](Coding/Program.cs#L12-L13)]

<<<<<<< HEAD
Now, when you run the code, your bot should be online!
![Bot being online](../../images/coding_BotOnline.png)

### The Final Product
[!code-cs[Program.cs](Coding/Program.cs)]
=======
### The Final Product
[!code-cs[Program.cs](Coding/Program.cs)]

***

Now, when you run the code, your bot should be online!
![Bot being online](../../images/coding_BotOnline.png)
>>>>>>> 14fd5c38c7dc847d510b1c3781e25654bba5a223
