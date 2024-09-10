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
        public async Task<String> IsHoliday([FromQuery] DateTime date)
        {
            var response = await _festivoService.isHoliday(date);
            if (response)
            {
                return "Yeah is holiday";
            }
            else { return "No, is not holiday"; }
        }
    }
}
