using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Mikkelhm_F1.Functions.Syncronization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mikkelhm_F1.Functions
{
    public class HourlyF1DataSyncFunction
    {
        private readonly IDataSyncronizer _dataSyncronizer;

        public HourlyF1DataSyncFunction(IDataSyncronizer dataSyncronizer)
        {
            _dataSyncronizer = dataSyncronizer;
        }

        [FunctionName(nameof(HourlyF1DataSyncFunction))]
        public async Task Run([TimerTrigger("0 0 * * * *")] TimerInfo myTimer, ILogger log)
        {

        }
    }
}
