using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionMagnetron.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VistaProductosPorMargenGananciaController : ControllerBase
    {
        private readonly IViewService<VistaProductosPorMargenGananciaDto> _viewService;

        public VistaProductosPorMargenGananciaController(IViewService<VistaProductosPorMargenGananciaDto> viewService)
        {
            _viewService = viewService;
        }

        [HttpGet("VistaProductosPorMargenGananciaAsync")]
        public async Task<ActionResult<ResponseDto<IEnumerable<VistaProductosPorMargenGananciaDto>>>> VistaProductosPorMargenGananciaAsync()
        {

            var response = await _viewService.GetAll();
            return Ok(response);

        }
    }
}
