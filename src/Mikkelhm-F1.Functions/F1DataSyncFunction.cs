using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Mikkelhm_F1.Functions.Syncronization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mikkelhm_F1.Functions
{
    public class F1DataSyncFunction
    {
        private readonly IDataSyncronizer _dataSyncronizer;

        public F1DataSyncFunction(IDataSyncronizer dataSyncronizer)
        {
            _dataSyncronizer = dataSyncronizer;
        }

        [FunctionName(nameof(F1DataSyncFunction))]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            await _dataSyncronizer.SyncSeasons();
        }
    }
}
