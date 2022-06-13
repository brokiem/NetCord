﻿namespace NetCord;

public class ImageUrl
{
    private string _url;

    public bool HasSize { get; private set; }

    private ImageUrl(string partialUrl, string extension)
    {
        _url = $"{Discord.CDNUrl}{partialUrl}.{extension}?";
    }

    public override string ToString() => _url[..^1];

    public string ToString(int size) => $"{_url}size={size}";

    public void AddSize(int size)
    {
        if (HasSize)
            throw new InvalidOperationException("Size is already added");

        HasSize = true;
        _url += $"size={size}&";
    }

    private static string GetExtension(string hash, ImageFormat? format)
    {
        return format.HasValue
            ? GetFormat(format.GetValueOrDefault())
            : hash.StartsWith("a_") ? "gif" : "png";
    }

    internal static string GetFormat(ImageFormat format)
    {
        return format switch
        {
            ImageFormat.Jpeg => "jpg",
            ImageFormat.Png => "png",
            ImageFormat.WebP => "webp",
            ImageFormat.Gif => "gif",
            ImageFormat.Lottie => "json",
            _ => throw new System.ComponentModel.InvalidEnumArgumentException("Invalid image format")
        };
    }

    public static ImageUrl CustomEmoji(Snowflake emojiId, ImageFormat format)
    {
        return new($"/emojis/{emojiId}", GetFormat(format));
    }

    public static ImageUrl GuildIcon(Snowflake guildId, string iconHash, ImageFormat? format)
    {
        return new($"/icons/{guildId}/{iconHash}", GetExtension(iconHash, format));
    }

    public static ImageUrl GuildSplash(Snowflake guildId, string splashHash, ImageFormat format)
    {
        return new($"/splashes/{guildId}/{splashHash}/guild_splash", GetFormat(format));
    }

    public static ImageUrl GuildDiscoverySplash(Snowflake guildId, string discoverySplashHash, ImageFormat format)
    {
        return new($"/discovery-splashes/{guildId}/{discoverySplashHash}/guild_splash", GetFormat(format));
    }

    public static ImageUrl GuildBanner(Snowflake guildId, string bannerHash, ImageFormat? format)
    {
        return new($"/banners/{guildId}/{bannerHash}", GetExtension(bannerHash, format));
    }

    public static ImageUrl UserBanner(Snowflake userId, string bannerHash, ImageFormat? format)
    {
        return new($"/banners/{userId}/{bannerHash}", GetExtension(bannerHash, format));
    }

    public static ImageUrl DefaultUserAvatar(ushort discriminator)
    {
        return new($"/embed/avatars/{discriminator % 5}", "png");
    }

    public static ImageUrl UserAvatar(Snowflake userId, string avatarHash, ImageFormat? format)
    {
        return new($"/avatars/{userId}/{avatarHash}", GetExtension(avatarHash, format));
    }

    public static ImageUrl GuildUserAvatar(Snowflake guildId, Snowflake userId, string avatarHash, ImageFormat? format)
    {
        return new($"/guilds/{guildId}/users/{userId}/avatars/{avatarHash}", GetExtension(avatarHash, format));
    }

    public static ImageUrl ApplicationIcon(Snowflake applicationId, string iconHash, ImageFormat format)
    {
        return new($"/app-icons/{applicationId}/{iconHash}", GetFormat(format));
    }

    public static ImageUrl ApplicationCover(Snowflake applicationId, string coverHash, ImageFormat format)
    {
        return new($"/app-icons/{applicationId}/{coverHash}", GetFormat(format));
    }

    public static ImageUrl ApplicationAsset(Snowflake applicationId, Snowflake assetId, ImageFormat format)
    {
        return new($"/app-icons/{applicationId}/{assetId}", GetFormat(format));
    }

    public static ImageUrl AchievementIcon(Snowflake applicationId, Snowflake achievementId, string iconHash, ImageFormat format)
    {
        return new($"/app-assets/{applicationId}/achievements/{achievementId}/icons/{iconHash}", GetFormat(format));
    }

    public static ImageUrl AchievementIcon(Snowflake stickerPackBannerAssetId, ImageFormat format)
    {
        return new($"/app-assets/710982414301790216/store/{stickerPackBannerAssetId}", GetFormat(format));
    }

    public static ImageUrl ApplicationAsset(Snowflake teamId, string iconHash, ImageFormat format)
    {
        return new($"/team-icons/{teamId}/{iconHash}", GetFormat(format));
    }

    public static ImageUrl TeamIcon(Snowflake teamId, string iconHash, ImageFormat format)
    {
        return new($"/team-icons/{teamId}/{iconHash}", GetFormat(format));
    }

    public static ImageUrl Sticker(Snowflake stickerId, ImageFormat format)
    {
        return new($"/stickers/{stickerId}", GetFormat(format));
    }

    public static ImageUrl RoleIcon(Snowflake roleId, ImageFormat format)
    {
        return new($"/role-icons/{roleId}/role_icon", GetFormat(format));
    }

    public static ImageUrl GuildScheduledEventCover(Snowflake scheduledEventId, string coverHash, ImageFormat format)
    {
        return new($"/guild-events/{scheduledEventId}/{coverHash}", GetFormat(format));
    }

    public static ImageUrl GuildUserBanner(Snowflake guildId, Snowflake userId, string bannerHash, ImageFormat? format)
    {
        return new($"/guilds/{guildId}/users/{userId}/banners/{bannerHash}", GetExtension(bannerHash, format));
    }
}