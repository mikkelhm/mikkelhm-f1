using Mikkelhm_F1.SyncFunctions.Syncronization.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mikkelhm_F1.SyncFunctions.Syncronization.Resposes;

public class CircuitResponse : BaseData
{
    [JsonPropertyName("CircuitTable")]
    public CircuitTable CircuitTable { get; set; }
}

public class CircuitTable
{
    [JsonPropertyName("Circuits")]
    public List<Circuit> Circuits { get; set; }
}