using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionMagnetron.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VistaProductosPorUtilidadController : ControllerBase
    {
        private readonly IViewService<VistaProductosPorUtilidadDto> _viewService;

        public VistaProductosPorUtilidadController(IViewService<VistaProductosPorUtilidadDto> viewService)
        {
            _viewService = viewService;
        }

        [HttpGet("VistaProductosPorUtilidadAsync")]
        public async Task<ActionResult<ResponseDto<IEnumerable<VistaProductosPorUtilidadDto>>>> VistaProductosPorUtilidadAsync()
        {

            var response = await _viewService.GetAll();
            return Ok(response);

        }
    }
}
