using Mikkelhm_F1.Functions.Core.Syncronization;

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
            var orchestrator = new DataSyncOrchestrator(httpClient);
            var seasons = await orchestrator.GetSeasons();
            Assert.IsTrue(seasons.Any());
        }
    }
}