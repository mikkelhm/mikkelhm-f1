using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Mikkelhm_F1.SyncFunctions.Syncronization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mikkelhm_F1.SyncFunctions
{
    public class DailyF1DataSyncFunction
    {
        private readonly IDataSyncronizer _dataSyncronizer;

        public DailyF1DataSyncFunction(IDataSyncronizer dataSyncronizer)
        {
            _dataSyncronizer = dataSyncronizer;
        }

        [FunctionName(nameof(DailyF1DataSyncFunction))]
        public async Task Run([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer, ILogger log)
        {

        }
    }
}
