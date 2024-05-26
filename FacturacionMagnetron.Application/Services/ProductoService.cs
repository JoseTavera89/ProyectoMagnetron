using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.Services;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using Mapster;

namespace FacturacionMagnetron.Application.Services
{
    public class ProductoService : IGenericService<ProductoDto>
    {
        private readonly IUowMagnetron _uowMagnetron;

        public ProductoService(IUowMagnetron uowMagnetron)
        {
            _uowMagnetron = uowMagnetron;
        }

        public async Task<ResponseDto<bool>> Add(ProductoDto obj)
        {
            var data = obj.Adapt<Producto>();
            var response = await _uowMagnetron.Producto.Add(data);
            SaveChanges();
            var nuevo = data.Prod_Id;
            return ResponseDto<bool>.Success(response);
        }

        public async Task<ResponseDto<bool>> Delete(ProductoDto obj)
        {
            var data = await _uowMagnetron.Producto.Get(obj.Prod_Id);
            if (data != null)
            {
                await _uowMagnetron.Producto.Delete(data);
                SaveChanges();
                return ResponseDto<bool>.Success(true);
            }
            return ResponseDto<bool>.Failure("No existe el producto");
        }

        public async Task<ResponseDto<ProductoDto>> Get(int id)
        {
            var response = await _uowMagnetron.Producto.Get(id);
            return ResponseDto<ProductoDto>.Success(response.Adapt<ProductoDto>());
        }

        public async Task<ResponseDto<IEnumerable<ProductoDto>>> GetAll()
        {
            var response = await _uowMagnetron.Producto.GetAll();
            return ResponseDto<IEnumerable<ProductoDto>>.Success(response.Adapt<IEnumerable<ProductoDto>>());
        }

        public async Task<ResponseDto<bool>> Update(ProductoDto obj)
        {
            var objSend = obj.Adapt<Producto>();
            var data = await _uowMagnetron.Producto.Get(obj.Prod_Id);
            if (data != null)
            {
                await _uowMagnetron.Producto.Update(objSend);
                SaveChanges();
                return ResponseDto<bool>.Success(true);
            }
            return ResponseDto<bool>.Failure("No existe el producto");
        }

        private void SaveChanges()
        {
            _uowMagnetron.SaveChanges();
        }
    }
}
