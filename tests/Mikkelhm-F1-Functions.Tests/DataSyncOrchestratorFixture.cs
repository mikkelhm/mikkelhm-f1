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
            var orchestrator = new DataSyncOrchestrator(httpClient, null, null, null, null, null);
            var seasons = await orchestrator.GetAllSeasons();
            Assert.IsTrue(seasons.Any());
        }

        [Test]
        public async Task Test_DataSyncOrchestrator_CanGetDriversData()
        {
            var httpClient = new HttpClient();
            var orchestrator = new DataSyncOrchestrator(httpClient, null, null, null, null, null);
            var drivers = await orchestrator.GetAllDrivers();
            Assert.IsTrue(drivers.Any());
        }


        [Test]
        public async Task Test_DataSyncOrchestrator_CanGetCircuitsData()
        {
            var httpClient = new HttpClient();
            var orchestrator = new DataSyncOrchestrator(httpClient, null, null, null, null, null);
            var drivers = await orchestrator.GetAllCircuits();
            Assert.IsTrue(drivers.Any());
        }

        [Test]
        public async Task Test_DataSyncOrchestrator_CanGetRacesData()
        {
            var httpClient = new HttpClient();
            var orchestrator = new DataSyncOrchestrator(httpClient, null, null, null, null, null);
            var races = await orchestrator.GetAllRaces();
            Assert.IsTrue(races.Any());
        }
    }
}