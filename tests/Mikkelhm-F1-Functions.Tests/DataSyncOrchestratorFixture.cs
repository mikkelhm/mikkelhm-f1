using Mikkelhm_F1.Functions.Syncronization;

namespace Mikkelhm_F1_Functions.Tests
{
    [TestFixture]
    public class DataSyncOrchestratorFixture
    {
        [Test]
        public async Task Test_DataSyncOrchestrator_CanGetSeasonData()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://ergast.com");
            var orchestrator = new DataSyncOrchestrator(httpClient, null, null, null);
            var seasons = await orchestrator.GetAllSeasons();
            Assert.IsTrue(seasons.Any());
        }
        
        [Test]
        public async Task Test_DataSyncOrchestrator_CanGetDriversData()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://ergast.com");
            var orchestrator = new DataSyncOrchestrator(httpClient, null, null, null);
            var drivers = await orchestrator.GetAllDrivers();
            Assert.IsTrue(drivers.Any());
        }
    }
}