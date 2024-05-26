using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionMagnetron.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
 
        private readonly IGenericService<ProductoDto> _genericService;

        public ProductoController(IGenericService<ProductoDto> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<IEnumerable<ProductoDto>>>> GetFacturasAsync()
        {
            var response = await _genericService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ProductoDto>>> GetFacturaAsync(int id)
        {
            var response = await _genericService.Get(id);
            if (response.Value == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ProductoDto>>> AddAFacturaAsync(ProductoDto producto)
        {
            var response = await _genericService.Add(producto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> PutUsuario(int id, ProductoDto producto)
        {
            if (id != producto.Prod_Id)
            {
                return BadRequest();
            }
            var response = await _genericService.Update(producto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteUsuario(ProductoDto producto)
        {
            var response = await _genericService.Delete(producto);
            return Ok(response);
        }
    }
}
