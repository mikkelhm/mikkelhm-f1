namespace Mikkelhm_F1.Domain;

public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Locality { get; set; }
    public string Country { get; set; }

    public Location(double latitude, double longitude, string locality, string country)
    {
        Latitude = latitude;
        Longitude = longitude;
        Locality = locality;
        Country = country;
    }
}
