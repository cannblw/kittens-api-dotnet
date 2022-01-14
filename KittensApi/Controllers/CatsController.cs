using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using KittensApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KittensApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CatsController : ControllerBase
    {
        private readonly ILogger<CatsController> _logger;
        private readonly ICatsService _catsService;

        public CatsController(ILogger<CatsController> logger, ICatsService catsService)
        {
            _logger = logger;
            _catsService = catsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUpsideDownCat()
        {
            _logger.LogInformation("Getting upside down cat");
            
            var image = await _catsService.GetUpsideDownCat();

            if (image == null) return StatusCode((int)HttpStatusCode.InternalServerError);
            
            return File(image, MediaTypeNames.Image.Jpeg);
        }
    }
}
