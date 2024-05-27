using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Services;
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

        [HttpGet("GetProductosAsync")]
        public async Task<ActionResult<ResponseDto<IEnumerable<ProductoDto>>>> GetProductosAsync()
        {
            var response = await _genericService.GetAll();
            return Ok(response);
        }

        [HttpGet("GetProductoAsync/{id}")]
        public async Task<ActionResult<ResponseDto<ProductoDto>>> GetProductoAsync(int id)
        {
            var response = await _genericService.Get(id);
            if (response.Value == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("AddAProductoAsync")]
        public async Task<ActionResult<ResponseDto<ProductoDto>>> AddAProductoAsync(ProductoDto producto)
        {
            var response = await _genericService.Add(producto);
            return Ok(response);
        }

        [HttpPut("PutProducto/{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> PutProducto(int id, ProductoDto producto)
        {
            if (id != producto.Prod_Id)
            {
                return BadRequest();
            }
            var response = await _genericService.Update(producto);
            return Ok(response);
        }

        [HttpDelete("DeleteProducto/{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteProducto(ProductoDto producto)
        {
            var response = await _genericService.Delete(producto);
            return Ok(response);
        }
    }
}
