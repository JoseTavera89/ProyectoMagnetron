using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.Services;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using Mapster;

namespace FacturacionMagnetron.Application.Services
{
    public class FacturaService : IGenericService<FacturaDto>
    {
        private readonly IUowMagnetron _uowMagnetron;

        public FacturaService(IUowMagnetron uowMagnetron)
        {
            _uowMagnetron = uowMagnetron;
        }

        public async Task<ResponseDto<bool>> Add(FacturaDto obj)
        {
            var persona = await _uowMagnetron.Persona.Get(obj.FacturaEncabezado.Per_Id);
            if (persona == null)
            {
                return ResponseDto<bool>.Failure("El Id de la persona no se encuentra en nuestras bases de datos");
            }

            var producto = await _uowMagnetron.Producto.Get(obj.FacturaDetalle.Prod_Id);
            if (producto == null)
            {
                return ResponseDto<bool>.Failure("El Id del producto no se encuentra en nuestras bases de datos");
            }
            var facturaEncabezado = obj.FacturaEncabezado.Adapt<FacturaEncabezado>();
            var facturaDetalle = obj.FacturaDetalle.Adapt<FacturaDetalle>();
            await _uowMagnetron.FacturaEncabezado.Add(facturaEncabezado);
            facturaDetalle.FEnc_Id = facturaEncabezado.FEnc_Id;
            await _uowMagnetron.FacturaDetalle.Add(facturaDetalle);
            SaveChanges();

            return ResponseDto<bool>.Success(true);
        }

        public async Task<ResponseDto<bool>> Delete(FacturaDto obj)
        {
            var facturaEncabezado = await _uowMagnetron.FacturaEncabezado.Get(obj.FacturaEncabezado.FEnc_Id);

            if (facturaEncabezado == null)
            {
                return ResponseDto<bool>.Failure("No existe la factura a eliminar");
            }
            await _uowMagnetron.FacturaDetalle.Delete(obj.FacturaDetalle.Adapt<FacturaDetalle>());

            await _uowMagnetron.FacturaEncabezado.Delete(facturaEncabezado);

            SaveChanges();

            return ResponseDto<bool>.Success(true);
        }

        public async Task<ResponseDto<FacturaDto>> Get(int id)
        {
            var facturaEncabezadoTask = _uowMagnetron.FacturaEncabezado.Get(id);
            var facturasDetallesTask = _uowMagnetron.FacturaDetalle.GetAll();

            await Task.WhenAll(facturaEncabezadoTask, facturasDetallesTask);

            var facturaEncabezado = await facturaEncabezadoTask;
            var facturasDetalles = await facturasDetallesTask;

            var facturaDetalle = facturasDetalles.FirstOrDefault(x => x.FEnc_Id == id);

            var response = new FacturaDto
            {
                FacturaEncabezado = facturaEncabezado?.Adapt<FacturaEncabezadoDto>(),
                FacturaDetalle = facturaDetalle?.Adapt<FacturaDetalleDto>()
            };

            return ResponseDto<FacturaDto>.Success(response);
        }



        public async Task<ResponseDto<IEnumerable<FacturaDto>>> GetAll()
        {
            var facturasEncabezado = await _uowMagnetron.FacturaEncabezado.GetAll();
            var facturassDetalles = await _uowMagnetron.FacturaDetalle.GetAll();

            var facturas = facturasEncabezado.Select(encabezado =>
            {
                var detalle = facturassDetalles.FirstOrDefault(det => det.FEnc_Id == encabezado.FEnc_Id);
                return new FacturaDto
                {
                    FacturaEncabezado = encabezado.Adapt<FacturaEncabezadoDto>(),
                    FacturaDetalle = detalle?.Adapt<FacturaDetalleDto>()
                };
            }).ToList();

            return ResponseDto<IEnumerable<FacturaDto>>.Success(facturas);
        }

        public async Task<ResponseDto<bool>> Update(FacturaDto obj)
        {
            var dataFacturaEncabezado = obj.FacturaEncabezado.Adapt<FacturaEncabezado>();
            var dataFacturaDetalle = obj.FacturaDetalle.Adapt<FacturaDetalle>();
            var data = await _uowMagnetron.FacturaEncabezado.Get(obj.FacturaEncabezado.FEnc_Id);
            if (data != null)
            {
                dataFacturaEncabezado = obj.Adapt(data);
                await _uowMagnetron.FacturaEncabezado.Update(dataFacturaEncabezado);
                await _uowMagnetron.FacturaDetalle.Update(dataFacturaDetalle);
                SaveChanges();
                return ResponseDto<bool>.Success(true);
            }
            return ResponseDto<bool>.Failure("No existe la Factura");
        }

        private void SaveChanges()
        {
            _uowMagnetron.SaveChanges();
        }
    }
}
