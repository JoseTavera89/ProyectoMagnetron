using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Repository;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionMagnetron.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VistaPersonaFacturadoController : ControllerBase
    {
        private readonly IViewService<VistaPersonaFacturadoDto> _viewService;

        public VistaPersonaFacturadoController(IViewService<VistaPersonaFacturadoDto> genericService)
        {
            _viewService = genericService;
        }

        [HttpGet("GetVistaPersonaFacturadoAsync")]
        public async Task<ActionResult<ResponseDto<IEnumerable<VistaPersonaFacturadoDto>>>> VistaPersonaFacturadoAsync()
        {

            var response = await _viewService.GetAll();
            return Ok(response);

        }
    }
}
