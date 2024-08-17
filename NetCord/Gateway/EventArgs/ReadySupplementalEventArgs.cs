using NetCord.Gateway.JsonModels.EventArgs;
using NetCord.Rest;

namespace NetCord.Gateway;

public class ReadySupplementalEventArgs : IJsonModel<JsonReadySupplementalEventArgs>
{
    public class ReadySupplementalGuild : IJsonModel<JsonReadySupplementalEventArgs.JsonReadySupplementalGuild>
    {
        JsonReadySupplementalEventArgs.JsonReadySupplementalGuild IJsonModel<JsonReadySupplementalEventArgs.JsonReadySupplementalGuild>.JsonModel => _jsonModel;
        internal readonly JsonReadySupplementalEventArgs.JsonReadySupplementalGuild _jsonModel;

        public ulong Id => _jsonModel.Id;
        public VoiceState[] VoiceStates { get; }

        public ReadySupplementalGuild(JsonReadySupplementalEventArgs.JsonReadySupplementalGuild jsonModel, RestClient client)
        {
            _jsonModel = jsonModel;
            VoiceStates = _jsonModel.VoiceStates.Select(v => new VoiceState(v, v.GuildId, client)).ToArray();
        }
    }

    public class ReadySupplementalMember(JsonReadySupplementalEventArgs.JsonReadySupplementalMember jsonModel, ulong guildId) : IJsonModel<JsonReadySupplementalEventArgs.JsonReadySupplementalMember>
    {
        JsonReadySupplementalEventArgs.JsonReadySupplementalMember IJsonModel<JsonReadySupplementalEventArgs.JsonReadySupplementalMember>.JsonModel => _jsonModel;
        internal readonly JsonReadySupplementalEventArgs.JsonReadySupplementalMember _jsonModel = jsonModel;
        
        public ulong GuildId { get; } = guildId;

        public IReadOnlyList<ulong> RoleIds => _jsonModel.RoleIds;
        public DateTimeOffset? GuildBoostStart => _jsonModel.GuildBoostStart;
        public bool? IsPending => _jsonModel.IsPending;
        public string? Nickname => _jsonModel.Nickname;
        public bool Muted => _jsonModel.Muted;
        public DateTimeOffset JoinedAt => _jsonModel.JoinedAt;
        public GuildUserFlags GuildFlags => _jsonModel.GuildFlags;
        public bool Deafened => _jsonModel.Deafened;
        public DateTimeOffset? TimeOutUntil => _jsonModel.TimeOutUntil;
        public string? BannerHash => _jsonModel.BannerHash;
        public string? AvatarHash => _jsonModel.AvatarHash;
    }

    JsonReadySupplementalEventArgs IJsonModel<JsonReadySupplementalEventArgs>.JsonModel => _jsonModel;
    private readonly JsonReadySupplementalEventArgs _jsonModel;

    public ReadySupplementalMember[][] MergedMembers { get; }

    public ReadySupplementalGuild[] Guilds { get; }

    public string[] Disclose => _jsonModel.Disclose;

    public ReadySupplementalEventArgs(JsonReadySupplementalEventArgs jsonModel, RestClient client)
    {
        _jsonModel = jsonModel;
        Guilds = _jsonModel.Guilds.Select(g => new ReadySupplementalGuild(g, client)).ToArray();
        MergedMembers = new ReadySupplementalMember[_jsonModel.MergedMembers.Length][];
        
        for (int i = 0; i < _jsonModel.Guilds.Length; i++)
        {
            var guildId = _jsonModel.Guilds[i].Id;
            MergedMembers[i] = new ReadySupplementalMember[_jsonModel.MergedMembers[i].Length];

            for (int j = 0; j < _jsonModel.MergedMembers[i].Length; j++)
            {
                var member = _jsonModel.MergedMembers[i][j];
                MergedMembers[i][j] = new ReadySupplementalMember(member, guildId);
            }
        }
    }
}
