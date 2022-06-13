﻿using NetCord.JsonModels;

namespace NetCord;

internal class AuditLog : IJsonModel<JsonAuditLog>
{
    JsonAuditLog IJsonModel<JsonAuditLog>.JsonModel => _jsonModel;
    private readonly JsonAuditLog _jsonModel;

    public AuditLog(JsonAuditLog jsonModel)
    {
        _jsonModel = jsonModel;
    }
}