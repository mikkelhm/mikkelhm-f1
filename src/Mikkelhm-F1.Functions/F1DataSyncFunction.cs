using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Mikkelhm_F1.Functions
{
    public class F1DataSyncFunction
    {
        [FunctionName(nameof(F1DataSyncFunction))]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
