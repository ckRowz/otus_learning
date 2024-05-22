using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace VirtualAssistant.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> MockGet1(CancellationToken token)
        {
            if (new Random().NextDouble() > 0.95)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            
            await Task.Delay(new Random().Next(50, 100), token);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> MockGet2(CancellationToken token)
        {
            if (new Random().NextDouble() > 0.98)
                return StatusCode((int)HttpStatusCode.InternalServerError);

            await Task.Delay(new Random().Next(100, 150), token);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> MockPost1(CancellationToken token)
        {
            if (new Random().NextDouble() > 0.95)
                return StatusCode((int)HttpStatusCode.InternalServerError);

            await Task.Delay(new Random().Next(150, 200), token);

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> MockPost2(CancellationToken token)
        {
            if (new Random().NextDouble() > 0.9)
                return StatusCode((int)HttpStatusCode.InternalServerError);

            await Task.Delay(new Random().Next(800, 1500), token);

            return Ok();
        }
    }
}
