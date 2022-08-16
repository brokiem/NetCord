﻿using System.Reflection;

namespace NetCord.Services.Interactions.TypeReaders;

public class EnumTypeReader<TContext> : InteractionTypeReader<TContext> where TContext : InteractionContext
{
    public override Task<object?> ReadAsync(ReadOnlyMemory<char> input, TContext context, InteractionParameter<TContext> parameter, InteractionServiceOptions<TContext> options)
    {
        var span = input.Span;
        var type = parameter.Type;
        if (parameter.Attributes.ContainsKey(typeof(AllowByValueAttribute)) || type.IsDefined(typeof(AllowByValueAttribute))) // by value or by name
        {
            if (Enum.TryParse(type, span, options.IgnoreCase, out var value) && Enum.IsDefined(type, value!))
                return Task.FromResult(value);
        }
        else // by name
        {
            if (((uint)span[0] - '0') > 9 && Enum.TryParse(type, span, options.IgnoreCase, out var value))
                return Task.FromResult(value);
        }

        throw new FormatException($"Invalid {type.Name}.");
    }
}