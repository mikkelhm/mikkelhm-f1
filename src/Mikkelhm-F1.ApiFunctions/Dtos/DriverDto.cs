namespace Mikkelhm_F1.ApiFunctions.Dtos;

public class DriverDto
{
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public string DriverId { get; set; }
    public MetaInformationDto MetaInformation { get; set; }
    public WikipediaInformationDto WikipediaInformation { get; set; }
}