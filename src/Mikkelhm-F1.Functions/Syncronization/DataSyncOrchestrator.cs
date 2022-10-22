using Microsoft.Extensions.Logging;
using Mikkelhm_F1.Domain.Interface;
using Mikkelhm_F1.Functions.Syncronization.Models;
using Mikkelhm_F1.Functions.Syncronization.Resposes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mikkelhm_F1.Functions.Syncronization;

public class DataSyncOrchestrator : IDataSyncronizer
{
    private readonly HttpClient _httpClient;
    private readonly ISeasonRepository _seasonRepository;
    private readonly ILogger _logger;

    public DataSyncOrchestrator(HttpClient httpClient,
        ISeasonRepository seasonRepository,
        ILogger logger)
    {
        _httpClient = httpClient;
        _seasonRepository = seasonRepository;
        _logger = logger;
    }

    public async Task SyncSeasons()
    {
        var seasons = await GetAllSeasons();
        var allCurrentSeasons = await _seasonRepository.GetAll();
        foreach (var season in seasons)
        {
            if (allCurrentSeasons.Any(x => x.Year == season.Year))
                continue;
            await _seasonRepository.Save(new Domain.Season(Guid.NewGuid().ToString(), season.Year, season.Url));
            //_logger.LogInformation($"Season: {season.Year}, synced");
        }
    }

    public async Task<List<Season>> GetAllSeasons()
    {
        var allSeasons = new List<Season>();
        var offset = 0;
        while (true)
        {
            var seasonResponse = await GetSeasons(offset);
            if (seasonResponse.MRData.SeasonTable.Seasons.Count == 0)
                break;

            allSeasons.AddRange(seasonResponse.MRData.SeasonTable.Seasons);
            offset = allSeasons.Count;
        }
        return allSeasons;
    }

    public async Task<Root<SeasonResponse>> GetSeasons(int offset)
    {
        var response = await _httpClient.GetAsync($"{Constants.SyncronizationEndpoints.Seasons}?offset={offset}");
        var responseString = await response.Content.ReadAsStringAsync();
        var serializeOptions = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var seasonResponse = JsonSerializer.Deserialize<Root<SeasonResponse>>(responseString, serializeOptions);
        return seasonResponse;
    }
}
