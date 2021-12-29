﻿using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;

namespace NetCord.Commands;

public class CommandService : CommandService<CommandContext>
{
    public CommandService(CommandServiceOptions<CommandContext>? options = null) : base(options)
    {
    }
}

public partial class CommandService<TContext> where TContext : ICommandContext
{
    private readonly CommandServiceOptions<TContext> _options;
    private readonly Dictionary<string, List<CommandInfo<TContext>>> _commands = new();

    public IReadOnlyDictionary<string, ReadOnlyCollection<CommandInfo<TContext>>> Commands
    {
        get
        {
            lock (_commands)
                return _commands.ToDictionary(v => v.Key, v => v.Value.AsReadOnly());
        }
    }

    public CommandService(CommandServiceOptions<TContext>? options = null)
    {
        _options = options ?? new();
    }

    public void AddModules(Assembly assembly)
    {
        Type baseType = typeof(BaseCommandModule<TContext>);
        IEnumerable<MethodInfo[]> methodsIEnumerable = assembly.GetTypes().Where(x => x.IsAssignableTo(baseType)).Select(x => x.GetMethods());
        lock (_commands)
        {
            foreach (MethodInfo[] methods in methodsIEnumerable)
            {
                foreach (MethodInfo method in methods)
                {
                    CommandAttribute? commandAttribute = method.GetCustomAttribute<CommandAttribute>();
                    if (commandAttribute == null)
                        continue;
                    CommandInfo<TContext> commandInfo = new(method, commandAttribute, _options);
                    foreach (var alias in commandAttribute.Aliases)
                    {
                        if (!_commands.TryGetValue(alias, out var list))
                        {
                            list = new();
                            _commands.Add(alias, list);
                        }
                        list.Add(commandInfo);
                        list.Sort((ci1, ci2) =>
                        {
                            int ci1Priority = ci1.Priority;
                            int ci2Priority = ci2.Priority;
                            if (ci1Priority > ci2Priority)
                                return -1;
                            if (ci1Priority < ci2Priority)
                                return 1;

                            int ci1CommandParametersLength = ci1.CommandParameters.Length;
                            int ci2CommandParametersLength = ci2.CommandParameters.Length;
                            if (ci1CommandParametersLength > ci2CommandParametersLength)
                                return -1;
                            if (ci1CommandParametersLength < ci2CommandParametersLength)
                                return 1;
                            return 0;
                        });
                    }
                }
            }
        }
    }

    public void AddModule(Type type)
    {
        if (!type.IsAssignableTo(typeof(BaseCommandModule<TContext>)))
            throw new InvalidOperationException($"Modules must inherit from {nameof(BaseCommandModule<TContext>)}");

        foreach (var method in type.GetMethods())
        {
            CommandAttribute? commandAttribute = method.GetCustomAttribute<CommandAttribute>();
            if (commandAttribute == null)
                continue;
            CommandInfo<TContext> commandInfo = new(method, commandAttribute, _options);
            foreach (var alias in commandAttribute.Aliases)
            {
                if (!_commands.TryGetValue(alias, out var list))
                {
                    list = new();
                    _commands.Add(alias, list);
                }
                list.Add(commandInfo);
                list.Sort((ci1, ci2) =>
                {
                    int ci1Priority = ci1.Priority;
                    int ci2Priority = ci2.Priority;
                    if (ci1Priority > ci2Priority)
                        return -1;
                    if (ci1Priority < ci2Priority)
                        return 1;

                    int ci1CommandParametersLength = ci1.CommandParameters.Length;
                    int ci2CommandParametersLength = ci2.CommandParameters.Length;
                    if (ci1CommandParametersLength > ci2CommandParametersLength)
                        return -1;
                    if (ci1CommandParametersLength < ci2CommandParametersLength)
                        return 1;
                    return 0;
                });
            }
        }
    }

    public async Task ExecuteAsync(int prefixLength, TContext context)
    {
        var messageContentWithoutPrefix = context.Message.Content[prefixLength..];
        bool ignoreCase = _options.IgnoreCase;
        var separator = _options.ParamSeparator;
        IEnumerable<KeyValuePair<string, List<CommandInfo<TContext>>>> commands;
        lock (_commands)
            commands = _commands.Where(x => messageContentWithoutPrefix.StartsWith(x.Key, ignoreCase, CultureInfo.InvariantCulture));

        if (!commands.Any())
            throw new CommandNotFoundException();

        var c = commands.MaxBy(x => x.Key.Length);
        int commandLength = c.Key.Length;
        var commandInfos = c.Value;
        string baseArguments;
        if (messageContentWithoutPrefix.Length > commandLength)
        {
            // example: command: "wzium" message: "!wziumy"
            if (messageContentWithoutPrefix[commandLength] != separator)
                throw new CommandNotFoundException();
            baseArguments = messageContentWithoutPrefix[(commandLength + 1)..].TrimStart(separator);
        }
        else
            baseArguments = string.Empty;

        //object[] parametersToPass = null;
        //CommandInfo<TContext> commandInfo = null;
        int maxIndex = commandInfos.Count - 1;

        var guild = context.Guild;

        ValueTuple<CommandInfo<TContext>, object[]> v;
        if (guild != null)
        {
            var everyonePermissions = guild.EveryoneRole.Permissions;
            var channelPermissionOverwrites = ((IGuildChannel)context.Message.Channel).PermissionOverwrites;
            var roles = context.Guild!.Roles.Values;

            if (context.Client.User == null)
                throw new NullReferenceException($"{nameof(context)}.{nameof(context.Client)}.{nameof(context.Client.User)} cannot be null");

            CalculatePermissions(guild.Users[context.Client.User],
                everyonePermissions,
                channelPermissionOverwrites,
                roles,
                out Permission botPermissions,
                out Permission botChannelPermissions,
                out var botAdministrator);
            CalculatePermissions((GuildUser)context.Message.Author,
                everyonePermissions,
                channelPermissionOverwrites,
                roles,
                out Permission userPermissions,
                out Permission userChannelPermissions,
                out var userAdministrator);

            v = await GetMethodAndParametersWithPermissionCheckAsync(context, separator, commandInfos, baseArguments, maxIndex, botPermissions, botChannelPermissions, userPermissions, userChannelPermissions, botAdministrator, userAdministrator).ConfigureAwait(false);

        }
        else
        {
            v = await GetMethodAndParametersAsync(context, separator, commandInfos, baseArguments, maxIndex).ConfigureAwait(false);
        }

        var methodClass = (BaseCommandModule<TContext>)Activator.CreateInstance(v.Item1.DeclaringType)!;
        methodClass.Context = context;

        await v.Item1.InvokeAsync(methodClass, v.Item2).ConfigureAwait(false);
    }

    private async Task<(CommandInfo<TContext> commandInfo, object[] parametersToPass)> GetMethodAndParametersWithPermissionCheckAsync(TContext context, char separator, List<CommandInfo<TContext>> commandInfos, string baseArguments, int maxIndex, Permission botPermissions, Permission botChannelPermissions, Permission userPermissions, Permission userChannelPermissions, bool botAdministrator, bool userAdministrator)
    {
        CommandInfo<TContext>? commandInfo = null;
        object?[]? parametersToPass = null;
        for (int i = 0; i <= maxIndex; i++)
        {
            commandInfo = commandInfos[i];
            bool lastCommand = i == maxIndex;

            #region Checking Permissions
            if (!botAdministrator)
            {
                if (!botPermissions.HasFlag(commandInfo.RequiredBotPermissions))
                {
                    if (lastCommand)
                    {
                        var missingPermissions = commandInfo.RequiredBotPermissions & ~botPermissions;
                        throw new PermissionException("Required bot permissions: " + missingPermissions, missingPermissions);
                    }
                    else
                        continue;
                }
                if (!botChannelPermissions.HasFlag(commandInfo.RequiredBotChannelPermissions))
                {
                    if (lastCommand)
                    {
                        var missingPermissions = commandInfo.RequiredBotChannelPermissions & ~botChannelPermissions;
                        throw new PermissionException("Required bot channel permissions: " + missingPermissions, missingPermissions);
                    }
                    else
                        continue;
                }
            }
            if (!userAdministrator)
            {
                if (!userPermissions.HasFlag(commandInfo.RequiredUserPermissions))
                {
                    if (lastCommand)
                    {
                        var missingPermissions = commandInfo.RequiredUserPermissions & ~userPermissions;
                        throw new PermissionException("Required user permissions: " + missingPermissions, missingPermissions);
                    }
                    else
                        continue;
                }
                if (!userChannelPermissions.HasFlag(commandInfo.RequiredUserChannelPermissions))
                {
                    if (lastCommand)
                    {
                        var missingPermissions = commandInfo.RequiredUserChannelPermissions & ~userChannelPermissions;
                        throw new PermissionException("Required user channel permissions: " + missingPermissions, missingPermissions);
                    }
                    else
                        continue;
                }
            }
            #endregion

            CommandParameter<TContext>[] commandParameters = commandInfo.CommandParameters;
            var commandParametersLength = commandParameters.Length;
            string arguments = baseArguments;
            parametersToPass = new object[commandParametersLength];
            bool isLastArgGood = false;
            string? currentArg = null;

            int commandParamIndex = 0;
            int maxCommandParamIndex = commandParametersLength - 1;
            while (commandParamIndex <= maxCommandParamIndex)
            {
                CommandParameter<TContext> parameter = commandParameters[commandParamIndex];

                if (!parameter.Params)
                {
                    if (parameter.Remainder)
                        currentArg = arguments;
                    else if (isLastArgGood == false)
                    {
                        int index = arguments.IndexOf(separator);
                        currentArg = index == -1 ? arguments : arguments[..index];
                    }

                    int currentArgLength = currentArg!.Length;
                    if (currentArgLength != 0)
                    {
                        try
                        {
                            parametersToPass[commandParamIndex] = await parameter.ReadAsync!(currentArg, context, parameter, _options).ConfigureAwait(false);
                            arguments = arguments[currentArgLength..].TrimStart(separator);
                            isLastArgGood = false;
                        }
                        catch
                        {
                            //is not last parameter
                            if (commandParamIndex != maxCommandParamIndex && parameter.HasDefaultValue)
                            {
                                parametersToPass[commandParamIndex] = parameter.DefaultValue;
                                isLastArgGood = true;
                            }
                            else if (lastCommand)
                                throw;
                            else
                                goto Continue;
                        }
                    }
                    else
                    {
                        if (parameter.HasDefaultValue)
                        {
                            parametersToPass[commandParamIndex] = parameter.DefaultValue;
                            isLastArgGood = true;
                        }
                        else if (lastCommand)
                            throw new ParameterCountException("Too few parameters");
                        else
                            goto Continue;
                    }
                }
                else
                {
                    if (arguments.Length != 0)
                    {
                        try
                        {
                            var args = arguments.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            var len = args.Length;
                            var o = Array.CreateInstance(parameter.Type, len);

                            for (var a = 0; a < len; a++)
                                o.SetValue(await parameter.ReadAsync!(args[a], context, parameter, _options).ConfigureAwait(false), a);

                            parametersToPass[commandParamIndex] = o;
                            goto Break;
                        }
                        catch
                        {
                            if (lastCommand)
                                throw;
                            else
                                goto Continue;
                        }
                    }
                    else
                    {
                        if (parameter.HasDefaultValue)
                            parametersToPass[commandParamIndex] = parameter.DefaultValue;
                        else if (lastCommand)
                            throw new ParameterCountException("Too few parameters");
                        else
                            goto Continue;
                    }
                }
                commandParamIndex++;
            }
            if (arguments.Length != 0)
            {
                if (lastCommand)
                    throw new ParameterCountException("Too many parameters");
                else
                    goto Continue;
            }
            Break:
            break;
            Continue:;
        }
        return (commandInfo, parametersToPass)!;
    }

    private async Task<(CommandInfo<TContext> commandInfo, object[] parametersToPass)> GetMethodAndParametersAsync(TContext context, char separator, List<CommandInfo<TContext>> commandInfos, string baseArguments, int maxIndex)
    {
        CommandInfo<TContext>? commandInfo = null;
        object?[]? parametersToPass = null;
        for (int i = 0; i <= maxIndex; i++)
        {
            commandInfo = commandInfos[i];
            bool lastCommand = i == maxIndex;

            CommandParameter<TContext>[] commandParameters = commandInfo.CommandParameters;
            var commandParametersLength = commandParameters.Length;
            string arguments = baseArguments;
            parametersToPass = new object[commandParametersLength];
            bool isLastArgGood = false;
            string? currentArg = null;

            int commandParamIndex = 0;
            int maxCommandParamIndex = commandParametersLength - 1;
            while (commandParamIndex <= maxCommandParamIndex)
            {
                CommandParameter<TContext> parameter = commandParameters[commandParamIndex];

                if (!parameter.Params)
                {
                    if (parameter.Remainder)
                        currentArg = arguments;
                    else if (isLastArgGood == false)
                    {
                        int index = arguments.IndexOf(separator);
                        currentArg = index == -1 ? arguments : arguments[..index];
                    }

                    int currentArgLength = currentArg!.Length;
                    if (currentArgLength != 0)
                    {
                        try
                        {
                            parametersToPass[commandParamIndex] = await parameter.ReadAsync!(currentArg, context, parameter, _options).ConfigureAwait(false);
                            arguments = arguments[currentArgLength..].TrimStart(separator);
                            isLastArgGood = false;
                        }
                        catch
                        {
                            //is not last parameter
                            if (commandParamIndex != maxCommandParamIndex && parameter.HasDefaultValue)
                            {
                                parametersToPass[commandParamIndex] = parameter.DefaultValue;
                                isLastArgGood = true;
                            }
                            else if (lastCommand)
                                throw;
                            else
                                goto Continue;
                        }
                    }
                    else
                    {
                        if (parameter.HasDefaultValue)
                        {
                            parametersToPass[commandParamIndex] = parameter.DefaultValue;
                            isLastArgGood = true;
                        }
                        else if (lastCommand)
                            throw new ParameterCountException("Too few parameters");
                        else
                            goto Continue;
                    }
                }
                else
                {
                    if (arguments.Length != 0)
                    {
                        try
                        {
                            var args = arguments.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            var len = args.Length;
                            var o = Array.CreateInstance(parameter.Type, len);

                            for (var a = 0; a < len; a++)
                                o.SetValue(await parameter.ReadAsync!(args[a], context, parameter, _options).ConfigureAwait(false), a);

                            parametersToPass[commandParamIndex] = o;
                            goto Break;
                        }
                        catch
                        {
                            if (lastCommand)
                                throw;
                            else
                                goto Continue;
                        }
                    }
                    else
                    {
                        if (parameter.HasDefaultValue)
                            parametersToPass[commandParamIndex] = parameter.DefaultValue;
                        else if (lastCommand)
                            throw new ParameterCountException("Too few parameters");
                        else
                            goto Continue;
                    }
                }
                commandParamIndex++;
            }
            if (arguments.Length != 0)
            {
                if (lastCommand)
                    throw new ParameterCountException("Too many parameters");
                else
                    goto Continue;
            }
            Break:
            break;
            Continue:;
        }
        return (commandInfo, parametersToPass)!;
    }

    private static void CalculatePermissions(GuildUser user, Permission everyonePermissions, IReadOnlyDictionary<DiscordId, PermissionOverwrite> permissionOverwrites, IEnumerable<Role> guildRoles, out Permission permissions, out Permission channelPermissions, out bool administrator)
    {
        permissions = everyonePermissions;
        foreach (var role in guildRoles)
        {
            if (user.RolesIds.Contains(role))
                permissions |= role.Permissions;
        }

        administrator = permissions.HasFlag(Permission.Administrator);
        if (!administrator)
        {
            Permission denied = default;
            Permission allowed = default;
            foreach (var r in user.RolesIds)
            {
                if (permissionOverwrites.TryGetValue(r, out var permission))
                {
                    denied |= permission.Denied;
                    allowed |= permission.Allowed;
                }
            }
            if (permissionOverwrites.TryGetValue(user.Id, out var p))
            {
                denied |= p.Denied;
                allowed |= p.Allowed;
            }
            channelPermissions = (permissions & ~denied) | allowed;
        }
        else
            channelPermissions = default;
    }
}