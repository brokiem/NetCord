﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using NetCord.Rest;
using NetCord.Services.Helpers;

namespace NetCord.Services.ApplicationCommands;

public class SlashCommandInfo<TContext> : ApplicationCommandInfo<TContext>, IAutocompleteInfo where TContext : IApplicationCommandContext
{
    internal SlashCommandInfo(MethodInfo method, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type declaringType, SlashCommandAttribute attribute, ApplicationCommandServiceConfiguration<TContext> configuration) : base(attribute, configuration)
    {
        MethodHelper.EnsureMethodReturnTypeValid(method);

        Description = attribute.Description;

        var descriptionTranslationsProviderType = attribute.DescriptionTranslationsProviderType;
        if (descriptionTranslationsProviderType is not null)
            DescriptionTranslationsProvider = (ITranslationsProvider)Activator.CreateInstance(descriptionTranslationsProviderType)!;

        var parameters = Parameters = SlashCommandParametersHelper.GetParameters(method, configuration);
        ParametersDictionary = parameters.ToDictionary(p => p.Name);

        Preconditions = PreconditionsHelper.GetPreconditions<TContext>(declaringType, method);
        _invokeAsync = InvocationHelper.CreateDelegate<TContext>(method, declaringType, parameters.Select(p => p.Type));
    }

    public string Description { get; }
    public ITranslationsProvider? DescriptionTranslationsProvider { get; }
    public IReadOnlyList<SlashCommandParameter<TContext>> Parameters { get; }
    public IReadOnlyDictionary<string, SlashCommandParameter<TContext>> ParametersDictionary { get; }
    public IReadOnlyList<PreconditionAttribute<TContext>> Preconditions { get; }

    private readonly Func<object?[]?, TContext, IServiceProvider?, Task> _invokeAsync;

    public override async Task InvokeAsync(TContext context, ApplicationCommandServiceConfiguration<TContext> configuration, IServiceProvider? serviceProvider)
    {
        await PreconditionsHelper.EnsureCanExecuteAsync(Preconditions, context, serviceProvider).ConfigureAwait(false);

        var parameters = await SlashCommandParametersHelper.ParseParametersAsync(context, ((SlashCommandInteraction)context.Interaction).Data.Options, Parameters, configuration, serviceProvider).ConfigureAwait(false);
        await _invokeAsync(parameters, context, serviceProvider).ConfigureAwait(false);
    }

    public override ApplicationCommandProperties GetRawValue()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        return new SlashCommandProperties(Name, Description)
        {
            NameLocalizations = NameTranslationsProvider?.Translations,
            DefaultGuildUserPermissions = DefaultGuildUserPermissions,
            DMPermission = DMPermission,
            DefaultPermission = DefaultPermission,
            Nsfw = Nsfw,
            DescriptionLocalizations = DescriptionTranslationsProvider?.Translations,
            Options = Parameters.Select(p => p.GetRawValue()),
        };
#pragma warning restore CS0618 // Type or member is obsolete
    }

    public Task<IEnumerable<ApplicationCommandOptionChoiceProperties>?> InvokeAutocompleteAsync<TAutocompleteContext>(TAutocompleteContext context, IReadOnlyList<ApplicationCommandInteractionDataOption> options, IServiceProvider? serviceProvider) where TAutocompleteContext : IAutocompleteInteractionContext
    {
        var option = options.First(o => o.Focused);
        if (ParametersDictionary.TryGetValue(option.Name, out var parameter))
            return parameter.InvokeAutocompleteAsync(context, option, serviceProvider);

        throw new AutocompleteNotFoundException();
    }

    void IAutocompleteInfo.InitializeAutocomplete<TAutocompleteContext>()
    {
        var parameters = Parameters;
        var count = parameters.Count;
        for (int i = 0; i < count; i++)
            parameters[i].InitializeAutocomplete<TAutocompleteContext>();
    }
}