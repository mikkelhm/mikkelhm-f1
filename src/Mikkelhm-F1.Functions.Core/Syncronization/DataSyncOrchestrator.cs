using Mikkelhm_F1.Functions.Core.Syncronization.Models;
using Mikkelhm_F1.Functions.Core.Syncronization.Resposes;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mikkelhm_F1.Functions.Core.Syncronization;

public class DataSyncOrchestrator
{
    private readonly HttpClient _httpClient;

    public DataSyncOrchestrator(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Season>> GetSeasons()
    {
        var response = await _httpClient.GetAsync(Constants.SyncronizationEndpoints.Seasons);
        var responseString = await response.Content.ReadAsStringAsync();
        var serializeOptions = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var seasonResponse = JsonSerializer.Deserialize<Root<SeasonResponse>>(responseString, serializeOptions);
        return seasonResponse.MRData.SeasonTable.Seasons;
    }
}
