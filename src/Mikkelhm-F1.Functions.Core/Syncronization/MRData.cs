using Mikkelhm_F1.Functions.Core.Syncronization.Resposes;
using System.Text.Json.Serialization;

namespace Mikkelhm_F1.Functions.Core.Syncronization;

public abstract class BaseData
{
    [JsonPropertyName("series")]
    public string Series { get; set; }
    public string Url { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public int Total { get; set; }
}

public class Root<T> where T : BaseData
{
    [JsonPropertyName("MRData")]
    public T MRData { get; set; }
}