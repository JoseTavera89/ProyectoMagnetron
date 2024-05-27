using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionMagnetron.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaProductoMasCaroController : ControllerBase
    {
        private readonly IViewService<VistaPersonaProductoMasCaroDto> _vistaPersonaProductoMasCaroDto;

        public PersonaProductoMasCaroController(IViewService<VistaPersonaProductoMasCaroDto> genericService)
        {
            _vistaPersonaProductoMasCaroDto = genericService;
        }

        [HttpGet("GetVistaPersonaProductoMasCaroAsync")]
        public async Task<ActionResult<ResponseDto<IEnumerable<PersonaDto>>>> VistaPersonaProductoMasCaroDto()
        {

            var response = await _vistaPersonaProductoMasCaroDto.GetAll();
            return Ok(response);

        }
    }
}
