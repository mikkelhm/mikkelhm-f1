using System;

namespace Mikkelhm_F1.Domain;

public class Race
{
    public const string PartitionKeyValue = nameof(Race);
    public string PartitionKey { get; } = PartitionKeyValue;
    public string Id { get; set; }
    public int Year { get; set; }
    public DateTime RaceDate { get; set; }
    public string CircuitId { get; set; }
    public MetaInformation MetaInformation { get; set; }
    public WikipediaInformation WikipediaInformation { get; set; }

    public Race(string id, int year, DateTime raceDate, string circuitId, string wikipediaLink = "")
    {
        Id = id;
        Year = year;
        RaceDate = raceDate;
        CircuitId = circuitId;
        MetaInformation = new MetaInformation();
        WikipediaInformation = new WikipediaInformation(wikipediaLink);
    }
}
