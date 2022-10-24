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
    private readonly ICircuitRepository _circuitRepository;
    private readonly ILogger<DataSyncOrchestrator> _logger;

    public DataSyncOrchestrator(HttpClient httpClient,
        ISeasonRepository seasonRepository,
        IDriverRepository driverRepository,
        ICircuitRepository circuitRepository,
        ILogger<DataSyncOrchestrator> logger)
    {
        _httpClient = httpClient;
        _seasonRepository = seasonRepository;
        _driverRepository = driverRepository;
        _circuitRepository = circuitRepository;
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
        var allDrivers = new List<Driver>();
        var offset = 0;
        while (true)
        {
            var driversResponse = await GetDrivers(offset);
            if (driversResponse.MRData.DriverTable.Drivers.Count == 0)
                break;

            allDrivers.AddRange(driversResponse.MRData.DriverTable.Drivers);
            offset = allDrivers.Count;
            // We have fair usage limitations on the api - max 4 requests pr second, 200 requests pr hour
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
        return allDrivers;
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

    public async Task SyncCircuits()
    {
        var circuits = await GetAllCircuits();
        var allCurrentCircuits = await _circuitRepository.GetAll();
        foreach (var circuit in circuits)
        {
            if (allCurrentCircuits.Any(x => x.CircuitId == circuit.CircuitId))
                continue;
            await _circuitRepository.Save(new Domain.Circuit(Guid.NewGuid().ToString(), circuit.CircuitName, circuit.CircuitId, new Domain.Location(double.Parse(circuit.Location.Lat), double.Parse(circuit.Location.Long), circuit.Location.Locality, circuit.Location.Country), circuit.Url));
            _logger.LogInformation($"Circuit: {circuit.CircuitName}, synced");
        }
    }


    public async Task<List<Circuit>> GetAllCircuits()
    {
        var allCircuits = new List<Circuit>();
        var offset = 0;
        while (true)
        {
            var circuitsResponse = await GetCircuits(offset);
            if (circuitsResponse.MRData.CircuitTable.Circuits.Count == 0)
                break;

            allCircuits.AddRange(circuitsResponse.MRData.CircuitTable.Circuits);
            offset = allCircuits.Count;
            // We have fair usage limitations on the api - max 4 requests pr second, 200 requests pr hour
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
        return allCircuits;
    }

    public async Task<Root<CircuitResponse>> GetCircuits(int offset)
    {
        var response = await _httpClient.GetAsync($"{Constants.SyncronizationEndpoints.Circuits}?limit=1000&offset={offset}");
        var responseString = await response.Content.ReadAsStringAsync();
        var serializeOptions = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var circuitsResponse = JsonSerializer.Deserialize<Root<CircuitResponse>>(responseString, serializeOptions);
        return circuitsResponse;
    }
}
