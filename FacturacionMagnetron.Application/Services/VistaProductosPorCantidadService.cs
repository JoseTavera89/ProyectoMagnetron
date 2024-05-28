using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Services;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Application.Services
{
    public class VistaProductosPorCantidadService : IViewService<VistaProductosPorCantidadDto>
    {
        private readonly IUowMagnetron _uowMagnetron;

        public VistaProductosPorCantidadService(IUowMagnetron uowMagnetron)
        {
            _uowMagnetron = uowMagnetron;
        }

        public async Task<ResponseDto<IEnumerable<VistaProductosPorCantidadDto>>> GetAll()
        {
            var responseViews = await _uowMagnetron.VistaProductosPorCantidad.GetAll();

            return ResponseDto<IEnumerable<VistaProductosPorCantidadDto>>.Success(responseViews.Adapt<IEnumerable<VistaProductosPorCantidadDto>>());

        }
    }
}
