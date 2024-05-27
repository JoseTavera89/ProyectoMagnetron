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
    public  class VistaPersonaProductoMasCaroService : IViewService<VistaPersonaProductoMasCaroDto>
    {
        private readonly IUowMagnetron _uowMagnetron;

        public VistaPersonaProductoMasCaroService(IUowMagnetron uowMagnetron)
        {
            _uowMagnetron = uowMagnetron;
        }

        public async Task<ResponseDto<IEnumerable<VistaPersonaProductoMasCaroDto>>> GetAll()
        {
            var responseviews = await _uowMagnetron.VistaPersonaProductoMasCaro.GetAll();
            var personaProductoMasCaro = responseviews.OrderByDescending(x => x.PrecioProductoMasCaro).FirstOrDefault();

            var response = new List<VistaPersonaProductoMasCaroDto>();
            if (personaProductoMasCaro != null)
            {
                response.Add(personaProductoMasCaro.Adapt<VistaPersonaProductoMasCaroDto>());
            }
            return ResponseDto<IEnumerable<VistaPersonaProductoMasCaroDto>>.Success(response);

        }
    }
}
