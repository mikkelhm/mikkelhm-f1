using System.Text.Json.Serialization;

namespace Mikkelhm_F1.Functions.Core.Syncronization.Models;

public class Season
{
    [JsonPropertyName("season")]
    public string Name { get; set; } = null!;
    public int Year => int.Parse(Name);
}
