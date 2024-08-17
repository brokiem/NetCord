using NetCord.JsonModels;
using NetCord.Rest;

namespace NetCord;

public class GuildPropertiesV2(JsonGuildPropertiesV2 jsonModel) : IJsonModel<JsonGuildPropertiesV2>
{
    JsonGuildPropertiesV2 IJsonModel<JsonGuildPropertiesV2>.JsonModel => _jsonModel;
    
    private protected readonly JsonGuildPropertiesV2 _jsonModel = jsonModel;

    /// <summary>
    /// The name of the <see cref="RestGuild"/>. Must be between 2 and 100 characters. Leading and trailing whitespace are trimmed.
    /// </summary>
    public string Name => _jsonModel.Name;
    
    /// <summary>
    /// The <see cref="RestGuild"/>'s icon hash.
    /// </summary>
    public string? IconHash => _jsonModel.IconHash;
    
    /// <summary>
    /// The <see cref="RestGuild"/>'s splash hash.
    /// </summary>
    public string? SplashHash => _jsonModel.SplashHash;
    
    /// <summary>
    /// The <see cref="RestGuild"/>'s discovery splash hash.
    /// </summary>
    public string? DiscoverySplashHash => _jsonModel.DiscoverySplashHash;
    
    /// <summary>
    /// The ID of the <see cref="RestGuild"/>'s owner.
    /// </summary>
    public ulong OwnerId => _jsonModel.OwnerId;
    
    /// <summary>
    /// ID of the <see cref="RestGuild"/>'s AFK channel.
    /// </summary>
    public ulong? AfkChannelId => _jsonModel.AfkChannelId;
    
    /// <summary>
    /// How long in seconds to wait before moving users to the AFK channel.
    /// </summary>
    public int AfkTimeout => _jsonModel.AfkTimeout;
    
    /// <summary>
    /// The <see cref="NetCord.VerificationLevel"/> required for the <see cref="RestGuild"/>.
    /// </summary>
    public VerificationLevel VerificationLevel => _jsonModel.VerificationLevel;
    
    /// <summary>
    /// The <see cref="RestGuild"/>'s <see cref="NetCord.DefaultMessageNotificationLevel"/>.
    /// </summary>
    public DefaultMessageNotificationLevel DefaultMessageNotificationLevel => _jsonModel.DefaultMessageNotificationLevel;

    /// <summary>
    /// The <see cref="RestGuild"/>'s <see cref="NetCord.ContentFilter"/>.
    /// </summary>
    public ContentFilter ContentFilter => _jsonModel.ContentFilter;
    
    /// <summary>
    /// A list of <see cref="RestGuild"/> feature strings, representing what features are currently enabled.
    /// </summary>
    public IReadOnlyList<string> Features => _jsonModel.Features;
    
    /// <summary>
    /// The <see cref="RestGuild"/>'s required Multi-Factor Authentication level.
    /// </summary>
    public MfaLevel MfaLevel => _jsonModel.MfaLevel;
    
    /// <summary>
    /// The <see cref="RestGuild"/> creator's application ID, if it was created by a bot.
    /// </summary>
    public ulong? ApplicationId => _jsonModel.ApplicationId;
    
    /// <summary>
    /// The ID of the channel where <see cref="RestGuild"/> notices such as welcome messages and boost events are posted.
    /// </summary>
    public ulong? SystemChannelId => _jsonModel.SystemChannelId;
    
    /// <summary>
    /// Represents the <see cref="RestGuild"/>'s current system channels settings.
    /// </summary>
    public SystemChannelFlags SystemChannelFlags => _jsonModel.SystemChannelFlags;
    
    /// <summary>
    /// The ID of the channel where community guilds can display their rules and/or guidelines.
    /// </summary>
    public ulong? RulesChannelId => _jsonModel.RulesChannelId;
    
    /// <summary>
    /// The maximum number of <see cref="GuildUser"/>s for the <see cref="RestGuild"/>.
    /// </summary>
    public int? MaxUsers => _jsonModel.MaxUsers;
    
    /// <summary>
    /// The <see cref="RestGuild"/>'s vanity invite URL code.
    /// </summary>
    public string? VanityUrlCode => _jsonModel.VanityUrlCode;
    
    /// <summary>
    /// The <see cref="RestGuild"/>'s description, shown in the 'Discovery' tab.
    /// </summary>
    public string? Description => _jsonModel.Description;
    
    /// <summary>
    /// The <see cref="RestGuild"/>'s banner hash.
    /// </summary>
    public string? BannerHash => _jsonModel.BannerHash;
    
    /// <summary>
    /// The <see cref="RestGuild"/>'s current server boost level.
    /// </summary>
    public int PremiumTier => _jsonModel.PremiumTier;
    
    /// <summary>
    /// The preferred locale of a community <see cref="RestGuild"/>, used for the 'Discovery' tab and in notices from Discord, also sent in interactions. Defaults to <c>en-US</c>.
    /// </summary>
    public string PreferredLocale => _jsonModel.PreferredLocale;
    
    /// <summary>
    /// The ID of the channel where admins and moderators of community guilds receive notices from Discord.
    /// </summary>
    public ulong? PublicUpdatesChannelId => _jsonModel.PublicUpdatesChannelId;

    /// <summary>
    /// The maximum amount of users in a video channel.
    /// </summary>
    public int? MaxVideoChannelUsers => _jsonModel.MaxVideoChannelUsers;

    /// <summary>
    /// The maximum amount of users in a stage video channel.
    /// </summary>
    public int? MaxStageVideoChannelUsers => _jsonModel.MaxStageVideoChannelUsers;
    
    /// <summary>
    /// The <see cref="RestGuild"/>'s set NSFW level.
    /// </summary>
    public NsfwLevel NsfwLevel => _jsonModel.NsfwLevel;
    
    /// <summary>
    /// Whether the <see cref="RestGuild"/> has the boost progress bar enabled.
    /// </summary>
    public bool PremiumProgressBarEnabled => _jsonModel.PremiumProgressBarEnabled;

    /// <summary>
    /// The ID of the channel where admins and moderators of community guilds receive safety alerts from Discord.
    /// </summary>
    public ulong? SafetyAlertsChannelId => _jsonModel.SafetyAlertsChannelId;
}
