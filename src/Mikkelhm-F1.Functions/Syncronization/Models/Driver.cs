using System.Text.Json.Serialization;

namespace Mikkelhm_F1.Functions.Syncronization.Models;

public class Driver
{
    public string GivenName { get; set; } = null!;
    public string FamilyName { get; set; } = null!;
    public string DateOfBirth { get; set; } = null!;
    public string Nationality { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string DriverId { get; set; } = null!;
}
