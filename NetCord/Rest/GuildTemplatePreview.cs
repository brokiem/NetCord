using NetCord.JsonModels;

namespace NetCord.Rest;

public class GuildTemplatePreview(JsonGuild jsonModel, RestClient client) : IJsonModel<JsonGuild>
{
    JsonGuild IJsonModel<JsonGuild>.JsonModel => jsonModel;

    public string Name => jsonModel.Properties.Name;
    public string? IconHash => jsonModel.IconHashTemplate;
    public string? Description => jsonModel.Properties.Description;
    public VerificationLevel VerificationLevel => jsonModel.Properties.VerificationLevel;
    public DefaultMessageNotificationLevel DefaultMessageNotificationLevel => jsonModel.Properties.DefaultMessageNotificationLevel;
    public ContentFilter ContentFilter => jsonModel.Properties.ContentFilter;
    public string PreferredLocale => jsonModel.Properties.PreferredLocale;
    public ulong? AfkChannelId => jsonModel.Properties.AfkChannelId;
    public int AfkTimeout => jsonModel.Properties.AfkTimeout;
    public ulong? SystemChannelId => jsonModel.Properties.SystemChannelId;
    public SystemChannelFlags SystemChannelFlags => jsonModel.Properties.SystemChannelFlags;
    public IReadOnlyDictionary<ulong, Role> Roles { get; } = jsonModel.Roles.ToDictionaryOrEmpty(r => new Role(r, 0, client));
    public IReadOnlyDictionary<ulong, IGuildChannel> Channels { get; } = jsonModel.Channels.ToDictionaryOrEmpty(c => IGuildChannel.CreateFromJson(c, 0, client));
}
