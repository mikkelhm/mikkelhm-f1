using System.Text.Json.Serialization;

namespace Mikkelhm_F1.SyncFunctions.Syncronization.Models;

public class Circuit
{
    public string CircuitId { get; set; } = null!;
    public string CircuitName { get; set; } = null!;
    public string Url { get; set; } = null!;
    [JsonPropertyName("Location")]
    public Location Location { get; set; } = null!;
}

public class Location
{
    public string Lat { get; set; }
    public string Long { get; set; }
    public string Locality { get; set; }
    public string Country { get; set; }

}
