﻿using Mikkelhm_F1.SyncFunctions.Syncronization.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mikkelhm_F1.SyncFunctions.Syncronization.Resposes;

public class SeasonResponse : BaseData
{
    [JsonPropertyName("SeasonTable")]
    public SeasonTable SeasonTable { get; set; }
}

public class SeasonTable
{
    [JsonPropertyName("Seasons")]
    public List<Season> Seasons { get; set; }
}