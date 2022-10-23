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
    private readonly IDriverRepository _driverRepository;
    private readonly ILogger _logger;

    public DataSyncOrchestrator(HttpClient httpClient,
        ISeasonRepository seasonRepository,
        IDriverRepository driverRepository,
        ILogger logger)
    {
        _httpClient = httpClient;
        _seasonRepository = seasonRepository;
        _driverRepository = driverRepository;
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
            _logger.LogInformation($"Season: {season.Year}, synced");
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
            // We have fair usage limitations on the api - max 4 requests pr second, 200 requests pr hour
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
        return allSeasons;
    }

    public async Task<Root<SeasonResponse>> GetSeasons(int offset)
    {
        var response = await _httpClient.GetAsync($"{Constants.SyncronizationEndpoints.Seasons}?limit=1000&offset={offset}");
        var responseString = await response.Content.ReadAsStringAsync();
        var serializeOptions = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var seasonResponse = JsonSerializer.Deserialize<Root<SeasonResponse>>(responseString, serializeOptions);
        return seasonResponse;
    }

    public async Task SyncDrivers()
    {
        var drivers = await GetAllDrivers();
        var allCurrentDrivers = await _driverRepository.GetAll();
        foreach (var driver in drivers)
        {
            if (allCurrentDrivers.Any(x => x.DriverId == driver.DriverId))
                continue;
            await _driverRepository.Save(new Domain.Driver(Guid.NewGuid().ToString(), driver.GivenName, driver.FamilyName, driver.DateOfBirth, driver.Nationality, driver.DriverId, driver.Url));
            _logger.LogInformation($"Driver: {driver.GivenName} {driver.FamilyName}, synced");
        }
    }

    public async Task<List<Driver>> GetAllDrivers()
    {
        var allSeasons = new List<Driver>();
        var offset = 0;
        while (true)
        {
            var seasonResponse = await GetDrivers(offset);
            if (seasonResponse.MRData.DriverTable.Drivers.Count == 0)
                break;

            allSeasons.AddRange(seasonResponse.MRData.DriverTable.Drivers);
            offset = allSeasons.Count;
            // We have fair usage limitations on the api - max 4 requests pr second, 200 requests pr hour
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
        return allSeasons;
    }

    public async Task<Root<DriverResponse>> GetDrivers(int offset)
    {
        var response = await _httpClient.GetAsync($"{Constants.SyncronizationEndpoints.Drivers}?limit=1000&offset={offset}");
        var responseString = await response.Content.ReadAsStringAsync();
        var serializeOptions = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var driverResponse = JsonSerializer.Deserialize<Root<DriverResponse>>(responseString, serializeOptions);
        return driverResponse;
    }
}
