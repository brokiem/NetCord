using System.Collections.Immutable;

namespace NetCord.Rest;

public class GuildPreview : ClientEntity, IJsonModel<NetCord.JsonModels.JsonGuild>
{
    NetCord.JsonModels.JsonGuild IJsonModel<NetCord.JsonModels.JsonGuild>.JsonModel => _jsonModel;
    private readonly NetCord.JsonModels.JsonGuild _jsonModel;

    public GuildPreview(NetCord.JsonModels.JsonGuild jsonModel, RestClient client) : base(client)
    {
        _jsonModel = jsonModel;
        Emojis = _jsonModel.Emojis.ToImmutableDictionaryOrEmpty(e => e.Id.GetValueOrDefault(), e => new GuildEmoji(e, Id, client));
    }

    public override ulong Id => _jsonModel.Id;

    public string Name => _jsonModel.Properties.Name;

    public string? IconHash => _jsonModel.Properties.IconHash;

    public string? SplashHash => _jsonModel.Properties.SplashHash;

    public string? DiscoverySplashHash => _jsonModel.Properties.DiscoverySplashHash;

    public ImmutableDictionary<ulong, GuildEmoji> Emojis { get; }

    public IReadOnlyList<string> Features => _jsonModel.Properties.Features;

    public int ApproximateUserCount => _jsonModel.ApproximateUserCount.GetValueOrDefault();

    public int ApproximatePresenceCount => _jsonModel.ApproximatePresenceCount.GetValueOrDefault();

    public string? Description => _jsonModel.Properties.Description;
}
