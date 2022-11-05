using System;

namespace Mikkelhm_F1.ApiFunctions.Dtos;

public class RaceDto
{
    public int Year { get; set; }
    public DateTime RaceDate { get; set; }
    public string CircuitId { get; set; }
    public MetaInformationDto MetaInformation { get; set; }
    public WikipediaInformationDto WikipediaInformation { get; set; }
}