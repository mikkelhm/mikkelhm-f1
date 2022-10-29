using System;
using System.Text.Json.Serialization;

namespace Mikkelhm_F1.Functions.Syncronization.Models;

public class Race
{
    [JsonPropertyName("raceName")]
    public string Name { get; set; } = null!;
    public string Season { get; set; } = null!;
    public int Year => int.Parse(Season);
    public string Date { get; set; } = null!;
    public string Time { get; set; } = null!;
    public DateTime RaceDate => DateTime.ParseExact($"{Date} {(string.IsNullOrEmpty(Time) ? "00:00:00Z" : Time)}", "yyyy-MM-dd HH:mm:ssZ", null);

    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;
    [JsonPropertyName("Circuit")]
    public Circuit Circuit { get; set; }
}
