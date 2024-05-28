using FacturacionMagnetron.Application.Services;
using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped(typeof(IViewService<VistaProductosPorCantidadDto>), typeof(VistaProductosPorCantidadService));
            services.AddScoped(typeof(IViewService<VistaProductosPorUtilidadDto>), typeof(VistaProductosPorUtilidadService));
            services.AddScoped(typeof(IViewService<VistaProductosPorMargenGananciaDto>), typeof(VistaProductosPorMargenGananciaService));
        }
    }
}
