namespace Mikkelhm_F1.Domain;

public class Circuit
{
    public const string PartitionKeyValue = nameof(Circuit);
    public string PartitionKey { get; } = PartitionKeyValue;
    public string Id { get; set; }
    public string CircuitName { get; set; }
    public Location Location { get; set; }
    public string CircuitId { get; set; }
    public MetaInformation MetaInformation { get; set; }
    public WikipediaInformation WikipediaInformation { get; set; }

    public Circuit(string id, string circuitName, string circuitId, Location location, string wikipediaLink = "")
    {
        Id = id;
        CircuitName = circuitName;
        CircuitId = circuitId;
        Location = location;
        MetaInformation = new MetaInformation();
        WikipediaInformation = new WikipediaInformation(wikipediaLink);
    }
}
