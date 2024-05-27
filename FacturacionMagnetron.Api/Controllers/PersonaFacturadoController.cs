using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Repository;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionMagnetron.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaFacturadoController : ControllerBase
    {
        private readonly IViewService<VistaPersonaFacturadoDto> _viewPersonaFacturadoService;

        public PersonaFacturadoController(IViewService<VistaPersonaFacturadoDto> genericService)
        {
            _viewPersonaFacturadoService = genericService;
        }

        [HttpGet("GetVistaPersonaFacturadoAsync")]
        public async Task<ActionResult<ResponseDto<IEnumerable<PersonaDto>>>> VistaPersonaFacturadoAsync()
        {

            var response = await _viewPersonaFacturadoService.GetAll();
            return Ok(response);

        }
    }
}
