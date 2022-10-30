using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mikkelhm_F1.Domain.Interface;

namespace Mikkelhm_F1.ApiFunctions.Functions
{
    public class SeasonsFunction
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonsFunction(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        [FunctionName(nameof(SeasonsFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "seasons")] HttpRequest req,
            ILogger log)
        {
            var seasons = await _seasonRepository.GetAll();

            return new OkObjectResult(seasons);
        }
    }
}
