using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Services;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using Mapster;

namespace FacturacionMagnetron.Application.Services
{
    public class VistaProductosPorUtilidadService: IViewService<VistaProductosPorUtilidadDto>
    {
        private readonly IUowMagnetron _uowMagnetron;

        public VistaProductosPorUtilidadService(IUowMagnetron uowMagnetron)
        {
            _uowMagnetron = uowMagnetron;
        }

        public async Task<ResponseDto<IEnumerable<VistaProductosPorUtilidadDto>>> GetAll()
        {
            var responseViews = await _uowMagnetron.VistaProductosPorUtilidad.GetAll();

            return ResponseDto<IEnumerable<VistaProductosPorUtilidadDto>>.Success(responseViews.Adapt<IEnumerable<VistaProductosPorUtilidadDto>>());

        }
    }
}
