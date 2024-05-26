using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionMagnetron.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IGenericService<FacturaDto> _genericService;

        public FacturaController(IGenericService<FacturaDto> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<IEnumerable<FacturaDto>>>> GetFacturasAsync()
        {
            var response = await _genericService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<FacturaDto>>> GetFacturaAsync(int id)
        {
            var response = await _genericService.Get(id);
            if (response.Value == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<FacturaDto>>> AddAFacturaAsync(FacturaDto factura)
        {
            var response = await _genericService.Add(factura);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> PutFactura(int id, FacturaDto factura)
        {
            if (id != factura.FacturaEncabezado.FEnc_Id)
            {
                return BadRequest();
            }
            var response = await _genericService.Update (factura);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteFactura(FacturaDto factura)
        {
            var response = await _genericService.Delete(factura);
            return Ok(response);
        }
    }
}
