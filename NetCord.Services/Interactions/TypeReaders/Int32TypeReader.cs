﻿namespace NetCord.Services.Interactions.TypeReaders;

public class Int32TypeReader<TContext> : InteractionTypeReader<TContext> where TContext : InteractionContext
{
    public override Task<object?> ReadAsync(string input, TContext context, InteractionParameter<TContext> parameter, InteractionServiceOptions<TContext> options) => Task.FromResult((object?)int.Parse(input, options.CultureInfo));
}