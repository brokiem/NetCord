using System.Collections.Immutable;

using NetCord.Rest;

namespace NetCord.Gateway;

public class ReadyEventArgs : IJsonModel<JsonModels.EventArgs.JsonReadyEventArgs>
{
    JsonModels.EventArgs.JsonReadyEventArgs IJsonModel<JsonModels.EventArgs.JsonReadyEventArgs>.JsonModel => _jsonModel;
    private readonly JsonModels.EventArgs.JsonReadyEventArgs _jsonModel;

    public ApiVersion Version => _jsonModel.Version;
    
    public User[] Users { get; }

    public CurrentUser User { get; }

    public ImmutableDictionary<ulong, Guild> Guilds { get; }

    public string SessionId => _jsonModel.SessionId;

    public string ResumeGatewayUrl => _jsonModel.ResumeGatewayUrl;

    public Shard? Shard => _jsonModel.Shard;

    public ulong ApplicationId => _jsonModel.Application is null ? default : _jsonModel.Application.Id;

    public ApplicationFlags ApplicationFlags => _jsonModel.Application is null ? default : _jsonModel.Application.Flags.GetValueOrDefault();

    public ReadyEventArgs(JsonModels.EventArgs.JsonReadyEventArgs jsonModel, RestClient client)
    {
        _jsonModel = jsonModel;
        Users = _jsonModel.Users.Select(u => new User(u, client)).ToArray();
        User = new(jsonModel.User, client);
        Guilds = _jsonModel.Guilds.Select(g => new Guild(g, jsonModel.User.Id, client)).ToImmutableDictionary(g => g.Id);
    }
}
