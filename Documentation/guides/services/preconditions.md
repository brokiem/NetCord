# Preconditions

Preconditions determine whether a command or interaction can be invoked. They are represented by attributes. They can be applied on Modules and Commands.

## Built-in Precondition Attributes

> [!NOTE]
> The attributes are generic. Generic attributes are only supported with `LangVersion` `preview` at this time.

- @NetCord.Services.InteractionRequireBotChannelPermissionAttribute`1
- @NetCord.Services.InteractionRequireUserChannelPermissionAttribute`1
- @NetCord.Services.RequireUserPermissionAttribute`1
- @NetCord.Services.RequireBotPermissionAttribute`1
- @NetCord.Services.RequireContextAttribute`1
- @NetCord.Services.RequireNsfwAttribute`1

## Creating a Custom Precondition Attribute
```cs
using NetCord.Services;

namespace MyBot;

public class RequireDiscriminatorAttribute<TContext> : PreconditionAttribute<TContext> where TContext : IContext, IUserContext // We use generics to make our attribute usable in text commands, application commands and interactions at the same time
{
    private readonly ushort _discriminator;
    
    public RequireDiscriminatorAttribute(ushort discriminator)
    {
        _discriminator = discriminator;
    }

    public override ValueTask EnsureCanExecuteAsync(TContext context)
    {
        // Throw exception if invalid discriminator
        if (context.User.Discriminator != _discriminator)
            throw new($"You need {_discriminator:D4} discriminator to use this command.");

        return default;
    }
}
```

### Example usage
```cs
[RequireDiscriminator<CommandContext>(1234)]
public class FirstModule : CommandModule<CommandContext>
{
    // All commands here will require 1234 discriminator
}
```