namespace Mikkelhm_F1.Functions.Syncronization.Models;

public class Circuit
{
    public string CircuitId { get; set; } = null!;
    public string CircuitName { get; set; } = null!;
    public string Url { get; set; } = null!;
    public Location Location { get; set; } = null!;
}

public class Location
{
    public string Lat { get; set; }
    public string Long { get; set; }
    public string Locality { get; set; }
    public string Country { get; set; }

}
