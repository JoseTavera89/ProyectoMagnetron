using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Repository;
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
    public  class VistaPersonaFacturadoService: IViewService<VistaPersonaFacturadoDto>
    {
        private readonly IUowMagnetron _uowMagnetron;

        public VistaPersonaFacturadoService(IUowMagnetron uowMagnetron)
        {
            _uowMagnetron = uowMagnetron;
        }

        public async Task<ResponseDto<IEnumerable<VistaPersonaFacturadoDto>>> GetAll()
        {
            var response = await _uowMagnetron.VistaPersonaFacturado.GetAll();
            return ResponseDto<IEnumerable<VistaPersonaFacturadoDto>>.Success(response.Adapt<IEnumerable<VistaPersonaFacturadoDto>>());
        }
    }
}
