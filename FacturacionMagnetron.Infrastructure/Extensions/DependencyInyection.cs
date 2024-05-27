using FacturacionMagnetron.Domain.Interfaces.Repository;
using FacturacionMagnetron.Domain.Interfaces.Services;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using FacturacionMagnetron.Infrastructure.Persistense;
using FacturacionMagnetron.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Infrastructure.Extensions
{
    public static class DependencyInyection
    {
        public static void AddDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            string ConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                                      configuration.GetConnectionString("MagnetronDbContext");
            services.AddDbContext<MagnetronDBContext>(options => { options.UseSqlServer(ConnectionString); });
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUowMagnetron, UowMagnetron>();

        }
    }
}
