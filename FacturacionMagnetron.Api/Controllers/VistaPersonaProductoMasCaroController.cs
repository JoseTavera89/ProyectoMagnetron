using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionMagnetron.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VistaPersonaProductoMasCaroController : ControllerBase
    {
        private readonly IViewService<VistaPersonaProductoMasCaroDto> _viewService;

        public VistaPersonaProductoMasCaroController(IViewService<VistaPersonaProductoMasCaroDto> genericService)
        {
            _viewService = genericService;
        }

        [HttpGet("GetVistaPersonaProductoMasCaroAsync")]
        public async Task<ActionResult<ResponseDto<IEnumerable<VistaPersonaProductoMasCaroDto>>>> VistaPersonaProductoMasCaroDto()
        {

            var response = await _viewService.GetAll();
            return Ok(response);

        }
    }
}
