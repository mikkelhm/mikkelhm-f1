using Mikkelhm_F1.SyncFunctions.Syncronization.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mikkelhm_F1.SyncFunctions.Syncronization.Resposes;

public class RaceResponse : BaseData
{
    [JsonPropertyName("RaceTable")]
    public RaceTable RaceTable { get; set; }
}

public class RaceTable
{
    [JsonPropertyName("Races")]
    public List<Race> Races { get; set; }
}