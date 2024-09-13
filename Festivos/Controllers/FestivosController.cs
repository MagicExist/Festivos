using Festivos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Festivos.API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class FestivosController : ControllerBase
    {
        private readonly FestivosService _festivoService;
        public FestivosController(FestivosService festivosService)
        {
            _festivoService = festivosService;
        }

        [HttpGet]
        public async Task<IActionResult> IsHoliday([FromQuery] string date)
        {
            if (!DateTime.TryParse(date,out DateTime parsedDate))
            {
                return BadRequest("Wrong date format");
            }
            var response = await _festivoService.isHoliday(parsedDate);
            if (response)
            {
                return Ok("Yeah Is Holiday!!");
            }
            else { return Ok("No, is not holiday"); }
        }
    }
}
