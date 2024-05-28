using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
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
    public  class VistaProductosPorMargenGananciaService : IViewService<VistaProductosPorMargenGananciaDto>
    {
        private readonly IUowMagnetron _uowMagnetron;

        public VistaProductosPorMargenGananciaService(IUowMagnetron uowMagnetron)
        {
            _uowMagnetron = uowMagnetron;
        }

        public async Task<ResponseDto<IEnumerable<VistaProductosPorMargenGananciaDto>>> GetAll()
        {
            var responseViews = await _uowMagnetron.VistaProductosPorMargenGanancia.GetAll();

            return ResponseDto<IEnumerable<VistaProductosPorMargenGananciaDto>>.Success(responseViews.Adapt<IEnumerable<VistaProductosPorMargenGananciaDto>>());

        }
    }
}
