using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionMagnetron.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VistaProductosPorCantidadController : ControllerBase
    {
        private readonly IViewService<VistaProductosPorCantidadDto> _viewService;

        public VistaProductosPorCantidadController(IViewService<VistaProductosPorCantidadDto> viewService)
        {
            _viewService = viewService;
        }

        [HttpGet("VistaProductosPorCantidadAsync")]
        public async Task<ActionResult<ResponseDto<IEnumerable<VistaProductosPorCantidadDto>>>> VistaProductosPorCantidadAsync()
        {

            var response = await _viewService.GetAll();
            return Ok(response);

        }
    }
}
