namespace Mikkelhm_F1.Domain;

public class Season
{
    public const string PartitionKeyValue = nameof(Season);
    public string PartitionKey { get; } = PartitionKeyValue;
    public string Id { get; set; }
    public int Year { get; set; }
    public MetaInformation MetaInformation { get; set; }
    public WikipediaInformation WikipediaInformation { get; set; }

    public Season(string id, int year, string wikipediaLink = "")
    {
        this.Id = id;
        Year = year;
        MetaInformation = new MetaInformation();
        WikipediaInformation = new WikipediaInformation(wikipediaLink);
    }
}
