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
    public class CircuitsFunction
    {
        private readonly ICircuitRepository _circuitRepository;

        public CircuitsFunction(ICircuitRepository circuitRepository)
        {
            _circuitRepository = circuitRepository;
        }

        [OpenApiOperation(operationId: "GetCircuits", tags: new[] { "circuits" })]
        [OpenApiResponseWithBody(statusCode:HttpStatusCode.OK, contentType:"application/json", bodyType:typeof(List<CircuitDto>))]
        [FunctionName(nameof(CircuitsFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "circuits")]
            HttpRequest req,
            ILogger log)
        {
            var circuits = await _circuitRepository.GetAll();

            return new OkObjectResult(circuits.Select(circuit => circuit.ToDto()));
        }
    }
}