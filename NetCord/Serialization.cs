﻿using System.Text.Json.Serialization;

using NetCord.Gateway;
using NetCord.Gateway.JsonModels;
using NetCord.Gateway.JsonModels.EventArgs;
using NetCord.Gateway.Voice;
using NetCord.Gateway.Voice.JsonModels;
using NetCord.JsonConverters;
using NetCord.JsonModels;
using NetCord.Rest;
using NetCord.Rest.JsonModels;

namespace NetCord;

[JsonSourceGenerationOptions(Converters = [typeof(UInt64Converter), typeof(NullableInt64Converter), typeof(PermissionsConverter)])]
[JsonSerializable(typeof(JsonMessage))]
[JsonSerializable(typeof(VoicePayloadProperties<int>))]
[JsonSerializable(typeof(VoicePayloadProperties<ProtocolProperties>))]
[JsonSerializable(typeof(JsonReady))]
[JsonSerializable(typeof(VoicePayloadProperties<VoiceResumeProperties>))]
[JsonSerializable(typeof(VoicePayloadProperties<SpeakingProperties>))]
[JsonSerializable(typeof(JsonClientDisconnect))]
[JsonSerializable(typeof(JsonHello))]
[JsonSerializable(typeof(JsonSpeaking))]
[JsonSerializable(typeof(JsonSessionDescription))]
[JsonSerializable(typeof(VoicePayloadProperties<VoiceIdentifyProperties>))]
[JsonSerializable(typeof(ulong))]
[JsonSerializable(typeof(JsonEntitlement))]
[JsonSerializable(typeof(JsonWebhooksUpdateEventArgs))]
[JsonSerializable(typeof(JsonVoiceServerUpdateEventArgs))]
[JsonSerializable(typeof(JsonVoiceState))]
[JsonSerializable(typeof(JsonUser))]
[JsonSerializable(typeof(JsonTypingStartEventArgs))]
[JsonSerializable(typeof(JsonStageInstance))]
[JsonSerializable(typeof(JsonPresence))]
[JsonSerializable(typeof(JsonMessageReactionRemoveEmojiEventArgs))]
[JsonSerializable(typeof(JsonMessageReactionRemoveAllEventArgs))]
[JsonSerializable(typeof(JsonMessageReactionRemoveEventArgs))]
[JsonSerializable(typeof(JsonMessageReactionAddEventArgs))]
[JsonSerializable(typeof(JsonMessageDeleteBulkEventArgs))]
[JsonSerializable(typeof(JsonMessageDeleteEventArgs))]
[JsonSerializable(typeof(JsonInviteDeleteEventArgs))]
[JsonSerializable(typeof(JsonInvite))]
[JsonSerializable(typeof(JsonInteraction))]
[JsonSerializable(typeof(JsonGuildIntegrationDeleteEventArgs))]
[JsonSerializable(typeof(JsonIntegration))]
[JsonSerializable(typeof(JsonGuildScheduledEventUserEventArgs))]
[JsonSerializable(typeof(JsonGuildScheduledEvent))]
[JsonSerializable(typeof(JsonRoleDeleteEventArgs))]
[JsonSerializable(typeof(JsonRoleEventArgs))]
[JsonSerializable(typeof(JsonGuildUserChunkEventArgs))]
[JsonSerializable(typeof(JsonGuildUserRemoveEventArgs))]
[JsonSerializable(typeof(JsonGuildUser))]
[JsonSerializable(typeof(JsonGuildIntegrationsUpdateEventArgs))]
[JsonSerializable(typeof(JsonGuildStickersUpdateEventArgs))]
[JsonSerializable(typeof(JsonGuildEmojisUpdateEventArgs))]
[JsonSerializable(typeof(JsonGuildBanEventArgs))]
[JsonSerializable(typeof(JsonAuditLogEntry))]
[JsonSerializable(typeof(JsonGuild))]
[JsonSerializable(typeof(JsonGuildThreadUsersUpdateEventArgs))]
[JsonSerializable(typeof(JsonThreadUser))]
[JsonSerializable(typeof(JsonGuildThreadListSyncEventArgs))]
[JsonSerializable(typeof(JsonChannel))]
[JsonSerializable(typeof(JsonChannelPinsUpdateEventArgs))]
[JsonSerializable(typeof(JsonAutoModerationActionExecutionEventArgs))]
[JsonSerializable(typeof(JsonAutoModerationRule))]
[JsonSerializable(typeof(JsonApplicationCommandGuildPermission))]
[JsonSerializable(typeof(JsonReadyEventArgs))]
[JsonSerializable(typeof(JsonReadySupplementalEventArgs))]
[JsonSerializable(typeof(JsonReadySupplementalEventArgs.JsonReadySupplementalGuild))]
[JsonSerializable(typeof(JsonReadySupplementalEventArgs.JsonReadySupplementalMember))]
[JsonSerializable(typeof(GatewayPayloadProperties<GuildUsersRequestProperties>))]
[JsonSerializable(typeof(GatewayPayloadProperties<PresenceProperties>))]
[JsonSerializable(typeof(GatewayPayloadProperties<VoiceStateProperties>))]
[JsonSerializable(typeof(JsonPayload))]
[JsonSerializable(typeof(GatewayPayloadProperties<int>))]
[JsonSerializable(typeof(GatewayPayloadProperties<GatewayResumeProperties>))]
[JsonSerializable(typeof(GatewayPayloadProperties<GatewayIdentifyProperties>))]
[JsonSerializable(typeof(JsonRole[]))]
[JsonSerializable(typeof(IEnumerable<JsonRole>))]
[JsonSerializable(typeof(JsonChannel[]))]
[JsonSerializable(typeof(IEnumerable<JsonChannel>))]
[JsonSerializable(typeof(JsonStageInstance[]))]
[JsonSerializable(typeof(IEnumerable<JsonStageInstance>))]
[JsonSerializable(typeof(JsonGuildScheduledEvent[]))]
[JsonSerializable(typeof(IEnumerable<JsonGuildScheduledEvent>))]
[JsonSerializable(typeof(JsonVoiceState[]))]
[JsonSerializable(typeof(IEnumerable<JsonVoiceState>))]
[JsonSerializable(typeof(JsonGuildUser[]))]
[JsonSerializable(typeof(IEnumerable<JsonGuildUser>))]
[JsonSerializable(typeof(JsonPresence[]))]
[JsonSerializable(typeof(IEnumerable<JsonPresence>))]
[JsonSerializable(typeof(JsonEntity[]))]
[JsonSerializable(typeof(TextInputProperties))]
[JsonSerializable(typeof(IEnumerable<ulong>))]
[JsonSerializable(typeof(IReadOnlyDictionary<string, string>))]
[JsonSerializable(typeof(SlashCommandProperties))]
[JsonSerializable(typeof(UserCommandProperties))]
[JsonSerializable(typeof(MessageCommandProperties))]
[JsonSerializable(typeof(ButtonProperties))]
[JsonSerializable(typeof(LinkButtonProperties))]
[JsonSerializable(typeof(PremiumButtonProperties))]
[JsonSerializable(typeof(IEnumerable<IButtonProperties>))]
[JsonSerializable(typeof(StringMenuProperties))]
[JsonSerializable(typeof(UserMenuProperties))]
[JsonSerializable(typeof(RoleMenuProperties))]
[JsonSerializable(typeof(MentionableMenuProperties))]
[JsonSerializable(typeof(ChannelMenuProperties))]
[JsonSerializable(typeof(ForumGuildThreadProperties))]
[JsonSerializable(typeof(InteractionCallback<InteractionMessageProperties>))]
[JsonSerializable(typeof(InteractionCallback<MessageOptions>))]
[JsonSerializable(typeof(InteractionCallback<InteractionCallbackChoicesDataProperties>))]
[JsonSerializable(typeof(InteractionCallback<ModalProperties>))]
[JsonSerializable(typeof(InteractionCallback))]
[JsonSerializable(typeof(InteractionMessageProperties))]
[JsonSerializable(typeof(MessageOptions))]
[JsonSerializable(typeof(MessageProperties))]
[JsonSerializable(typeof(JsonApplication))]
[JsonSerializable(typeof(CurrentApplicationOptions))]
[JsonSerializable(typeof(IEnumerable<JsonApplicationRoleConnectionMetadata>))]
[JsonSerializable(typeof(IEnumerable<ApplicationRoleConnectionMetadataProperties>))]
[JsonSerializable(typeof(JsonAuditLog))]
[JsonSerializable(typeof(JsonAutoModerationRule[]))]
[JsonSerializable(typeof(AutoModerationRuleProperties))]
[JsonSerializable(typeof(AutoModerationRuleOptions))]
[JsonSerializable(typeof(GroupDMChannelOptions))]
[JsonSerializable(typeof(GuildChannelOptions))]
[JsonSerializable(typeof(JsonMessage[]))]
[JsonSerializable(typeof(JsonUser[]))]
[JsonSerializable(typeof(BulkDeleteMessagesProperties))]
[JsonSerializable(typeof(PermissionOverwriteProperties))]
[JsonSerializable(typeof(JsonRestInvite[]))]
[JsonSerializable(typeof(InviteProperties))]
[JsonSerializable(typeof(JsonRestInvite))]
[JsonSerializable(typeof(FollowAnnouncementGuildChannelProperties))]
[JsonSerializable(typeof(JsonFollowedChannel))]
[JsonSerializable(typeof(GroupDMChannelUserAddProperties))]
[JsonSerializable(typeof(GuildThreadFromMessageProperties))]
[JsonSerializable(typeof(GuildThreadProperties))]
[JsonSerializable(typeof(JsonThreadUser[]))]
[JsonSerializable(typeof(JsonRestGuildThreadPartialResult))]
[JsonSerializable(typeof(JsonEmoji[]))]
[JsonSerializable(typeof(JsonEmoji))]
[JsonSerializable(typeof(GuildEmojiProperties))]
[JsonSerializable(typeof(ApplicationEmojiProperties))]
[JsonSerializable(typeof(GuildEmojiOptions))]
[JsonSerializable(typeof(ApplicationEmojiOptions))]
[JsonSerializable(typeof(JsonGateway))]
[JsonSerializable(typeof(JsonGatewayBot))]
[JsonSerializable(typeof(GuildProperties))]
[JsonSerializable(typeof(GuildOptions))]
[JsonSerializable(typeof(GuildChannelProperties))]
[JsonSerializable(typeof(IEnumerable<GuildChannelPositionProperties>))]
[JsonSerializable(typeof(JsonRestGuildThreadResult))]
[JsonSerializable(typeof(GuildUserProperties))]
[JsonSerializable(typeof(GuildUserOptions))]
[JsonSerializable(typeof(CurrentGuildUserOptions))]
[JsonSerializable(typeof(JsonGuildBan[]))]
[JsonSerializable(typeof(JsonGuildBan))]
[JsonSerializable(typeof(GuildBanProperties))]
[JsonSerializable(typeof(JsonGuildBulkBan))]
[JsonSerializable(typeof(GuildBulkBanProperties))]
[JsonSerializable(typeof(JsonRole))]
[JsonSerializable(typeof(RoleProperties))]
[JsonSerializable(typeof(IEnumerable<RolePositionProperties>))]
[JsonSerializable(typeof(RoleOptions))]
[JsonSerializable(typeof(GuildMfaLevelProperties))]
[JsonSerializable(typeof(JsonGuildMfaLevel))]
[JsonSerializable(typeof(JsonGuildPruneCountResult))]
[JsonSerializable(typeof(GuildPruneProperties))]
[JsonSerializable(typeof(JsonGuildPruneResult))]
[JsonSerializable(typeof(JsonVoiceRegion[]))]
[JsonSerializable(typeof(JsonIntegration[]))]
[JsonSerializable(typeof(JsonGuildWidgetSettings))]
[JsonSerializable(typeof(GuildWidgetSettingsOptions))]
[JsonSerializable(typeof(JsonGuildWidget))]
[JsonSerializable(typeof(JsonGuildVanityInvite))]
[JsonSerializable(typeof(JsonGuildWelcomeScreen))]
[JsonSerializable(typeof(GuildWelcomeScreenOptions))]
[JsonSerializable(typeof(JsonGuildOnboarding))]
[JsonSerializable(typeof(GuildOnboardingOptions))]
[JsonSerializable(typeof(CurrentUserVoiceStateOptions))]
[JsonSerializable(typeof(VoiceStateOptions))]
[JsonSerializable(typeof(JsonGuildTemplate))]
[JsonSerializable(typeof(GuildFromGuildTemplateProperties))]
[JsonSerializable(typeof(JsonGuildTemplate[]))]
[JsonSerializable(typeof(GuildTemplateProperties))]
[JsonSerializable(typeof(GuildTemplateOptions))]
[JsonSerializable(typeof(JsonApplicationCommand[]))]
[JsonSerializable(typeof(ApplicationCommandProperties))]
[JsonSerializable(typeof(JsonApplicationCommand))]
[JsonSerializable(typeof(ApplicationCommandOptions))]
[JsonSerializable(typeof(IEnumerable<ApplicationCommandProperties>))]
[JsonSerializable(typeof(JsonApplicationCommandGuildPermissions[]))]
[JsonSerializable(typeof(JsonApplicationCommandGuildPermissions))]
[JsonSerializable(typeof(ApplicationCommandGuildPermissionsProperties))]
[JsonSerializable(typeof(JsonEntitlement[]))]
[JsonSerializable(typeof(JsonSku[]))]
[JsonSerializable(typeof(JsonAuthorizationInformation))]
[JsonSerializable(typeof(StageInstanceProperties))]
[JsonSerializable(typeof(StageInstanceOptions))]
[JsonSerializable(typeof(JsonSticker))]
[JsonSerializable(typeof(JsonStickerPacks))]
[JsonSerializable(typeof(JsonSticker[]))]
[JsonSerializable(typeof(GuildStickerOptions))]
[JsonSerializable(typeof(GoogleCloudPlatformStorageBucketsProperties))]
[JsonSerializable(typeof(CurrentUserOptions))]
[JsonSerializable(typeof(JsonGuild[]))]
[JsonSerializable(typeof(DMChannelProperties))]
[JsonSerializable(typeof(GroupDMChannelProperties))]
[JsonSerializable(typeof(JsonConnection[]))]
[JsonSerializable(typeof(JsonApplicationRoleConnection))]
[JsonSerializable(typeof(ApplicationRoleConnectionProperties))]
[JsonSerializable(typeof(WebhookProperties))]
[JsonSerializable(typeof(JsonWebhook))]
[JsonSerializable(typeof(JsonWebhook[]))]
[JsonSerializable(typeof(WebhookOptions))]
[JsonSerializable(typeof(WebhookMessageProperties))]
[JsonSerializable(typeof(GuildScheduledEventProperties))]
[JsonSerializable(typeof(GuildScheduledEventOptions))]
[JsonSerializable(typeof(JsonGuildScheduledEventUser[]))]
[JsonSerializable(typeof(TestEntitlementProperties))]
[JsonSerializable(typeof(JsonCreateGoogleCloudPlatformStorageBucketResult))]
[JsonSerializable(typeof(IReadOnlyList<RestErrorDetail>))]
[JsonSerializable(typeof(IRestErrorGroup))]
[JsonSerializable(typeof(RestError))]
[JsonSerializable(typeof(IReadOnlyDictionary<string, IRestErrorGroup>))]
[JsonSerializable(typeof(UserIdsGuildUsersSearchQuery))]
[JsonSerializable(typeof(UsernamesGuildUsersSearchQuery))]
[JsonSerializable(typeof(SafetySignalsGuildUsersSearchQuery))]
[JsonSerializable(typeof(GuildUsersSearchTimestamp))]
[JsonSerializable(typeof(JsonGuildUserSearchResult))]
[JsonSerializable(typeof(GuildUsersSearchPaginationProperties))]
[JsonSerializable(typeof(JsonGuildJoinRequestUpdateEventArgs))]
[JsonSerializable(typeof(JsonGuildJoinRequestDeleteEventArgs))]
[JsonSerializable(typeof(JsonMessagePollAnswerVotersResult))]
[JsonSerializable(typeof(JsonMessagePollVoteEventArgs))]
[JsonSerializable(typeof(JsonGuildPropertiesV2))]
internal partial class Serialization : JsonSerializerContext
{
}
