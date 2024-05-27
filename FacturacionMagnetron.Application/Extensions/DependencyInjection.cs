using FacturacionMagnetron.Application.Services;
using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Repository;
using FacturacionMagnetron.Domain.Interfaces.Services;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FacturacionMagnetron.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddServices2(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<PersonaDto>), typeof(PersonaService));
            services.AddScoped(typeof(IGenericService<ProductoDto>), typeof(ProductoService));
            services.AddScoped(typeof(IGenericService<FacturaDto>), typeof(FacturaService));
            services.AddScoped(typeof(IViewService<VistaPersonaFacturadoDto>), typeof(VistaPersonaFacturadoService));
            services.AddScoped(typeof(IViewService<VistaPersonaProductoMasCaroDto>), typeof(VistaPersonaProductoMasCaroService));
        }
    }
}
