using Mikkelhm_F1.Functions.Syncronization.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mikkelhm_F1.Functions.Syncronization.Resposes;

public class DriverResponse : BaseData
{
    [JsonPropertyName("DriverTable")]
    public DriverTable DriverTable { get; set; }
}

public class DriverTable
{
    [JsonPropertyName("Drivers")]
    public List<Driver> Drivers { get; set; }
}