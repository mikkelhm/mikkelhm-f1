namespace Mikkelhm_F1.Domain;

public class Driver
{
    public const string PartitionKeyValue = nameof(Season);
    public string PartitionKey { get; } = PartitionKeyValue;
    public string Id { get; set; }
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public string DriverId { get; set; }
    public MetaInformation MetaInformation { get; set; }
    public WikipediaInformation WikipediaInformation { get; set; }

    public Driver(string id, string givenName, string familyName, string dateOfBirth, string nationality, string driverId, string wikipediaLink = "")
    {
        this.Id = id;
        GivenName = givenName;
        FamilyName = familyName;
        DateOfBirth = dateOfBirth;
        Nationality = nationality;
        DriverId = driverId;
        MetaInformation = new MetaInformation();
        WikipediaInformation = new WikipediaInformation(wikipediaLink);
    }
}
