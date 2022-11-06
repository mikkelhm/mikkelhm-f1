using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mikkelhm_F1.ApiFunctions.Dtos;
using Mikkelhm_F1.Domain.Interface;

namespace Mikkelhm_F1.ApiFunctions.Functions
{
    public class RacesFunction
    {
        private readonly IRaceRepository _raceRepository;

        public RacesFunction(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        [FunctionName(nameof(RacesFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "races")] HttpRequest req,
            ILogger log)
        {
            var races = await _raceRepository.GetAll();

            return new OkObjectResult(races.Select(race => race.ToDto()));
        }
    }
}
