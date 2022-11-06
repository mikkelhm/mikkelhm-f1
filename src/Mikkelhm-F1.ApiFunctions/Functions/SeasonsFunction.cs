using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Mikkelhm_F1.ApiFunctions.Dtos;
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

        [OpenApiOperation(operationId: "GetSeasons", tags: new[] { "seasons" })]
        [OpenApiResponseWithBody(statusCode:HttpStatusCode.OK, contentType:"application/json", bodyType:typeof(List<SeasonDto>))]
        [FunctionName(nameof(SeasonsFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "seasons")]
            HttpRequest req,
            ILogger log)
        {
            var seasons = await _seasonRepository.GetAll();

            return new OkObjectResult(seasons.Select(season => season.ToDto()));
        }
    }
}