using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Mikkelhm_F1.Functions.Syncronization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mikkelhm_F1.Functions
{
    public class WeeklyF1DataSyncFunction
    {
        private readonly IDataSyncronizer _dataSyncronizer;

        public WeeklyF1DataSyncFunction(IDataSyncronizer dataSyncronizer)
        {
            _dataSyncronizer = dataSyncronizer;
        }

        [FunctionName(nameof(WeeklyF1DataSyncFunction))]
        public async Task Run([TimerTrigger("0 0 2 * * MON")] TimerInfo myTimer, ILogger log)
        {
            await _dataSyncronizer.SyncSeasons();
            await _dataSyncronizer.SyncDrivers();
            await _dataSyncronizer.SyncCircuits();
        }
    }
}
