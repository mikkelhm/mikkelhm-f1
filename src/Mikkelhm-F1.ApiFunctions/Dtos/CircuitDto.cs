namespace Mikkelhm_F1.ApiFunctions.Dtos;

public class CircuitDto
{
    public string CircuitName { get; set; }
    public LocationDto Location { get; set; }
    public string CircuitId { get; set; }
    public MetaInformationDto MetaInformation { get; set; }
    public WikipediaInformationDto WikipediaInformation { get; set; }
}