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
    public class DriversFunction
    {
        private readonly IDriverRepository _driverRepository;

        public DriversFunction(IDriverRepository DriverRepository)
        {
            _driverRepository = DriverRepository;
        }
        
        [OpenApiOperation(operationId: "GetDrivers", tags: new[] { "drivers" })]
        [OpenApiResponseWithBody(statusCode:HttpStatusCode.OK, contentType:"application/json", bodyType:typeof(List<DriverDto>))]
        [FunctionName(nameof(DriversFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "drivers")] HttpRequest req,
            ILogger log)
        {
            var drivers = await _driverRepository.GetAll();

            return new OkObjectResult(drivers.Select(driver => driver.ToDto()));
        }
    }
}
