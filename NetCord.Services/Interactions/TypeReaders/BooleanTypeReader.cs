﻿namespace NetCord.Services.Interactions.TypeReaders;

public class BooleanTypeReader<TContext> : InteractionTypeReader<TContext> where TContext : InteractionContext
{
    public override Task<object?> ReadAsync(string input, TContext context, InteractionParameter<TContext> parameter, InteractionServiceOptions<TContext> options) => Task.FromResult((object?)bool.Parse(input));
}