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
        public object[] EmbeddedActivities { get; }

        public ReadySupplementalGuild(JsonReadySupplementalEventArgs.JsonReadySupplementalGuild jsonModel, RestClient client)
        {
            _jsonModel = jsonModel;
            VoiceStates = _jsonModel.VoiceStates.Select(v => new VoiceState(v, v.GuildId, client)).ToArray();
            EmbeddedActivities = _jsonModel.EmbeddedActivities;
        }
    }

    JsonReadySupplementalEventArgs IJsonModel<JsonReadySupplementalEventArgs>.JsonModel => _jsonModel;
    private readonly JsonReadySupplementalEventArgs _jsonModel;

    public GuildUser[][] MergedMembers { get; }

    public ReadySupplementalGuild[] Guilds { get; }

    public string[] Disclose => _jsonModel.Disclose;

    public ReadySupplementalEventArgs(JsonReadySupplementalEventArgs jsonModel, RestClient client)
    {
        _jsonModel = jsonModel;
        Guilds = _jsonModel.Guilds.Select(g => new ReadySupplementalGuild(g, client)).ToArray();
        MergedMembers = new GuildUser[_jsonModel.MergedMembers.Length][];
        
        for (int i = 0; i < _jsonModel.Guilds.Length; i++)
        {
            var guildId = _jsonModel.Guilds[i].Id;
            MergedMembers[i] = new GuildUser[_jsonModel.MergedMembers[i].Length];

            for (int j = 0; j < _jsonModel.MergedMembers[i].Length; j++)
            {
                var member = _jsonModel.MergedMembers[i][j];
                MergedMembers[i][j] = new GuildUser(member, guildId, client);
            }
        }
    }
}
