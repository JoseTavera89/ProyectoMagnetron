using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Infrastructure.Persistense
{
    public class MagnetronDBContext:DbContext
    {
        public MagnetronDBContext() : base() { }
        public MagnetronDBContext(DbContextOptions<MagnetronDBContext> options) : base(options) { }

        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<FacturaDetalle> FacturaDetalle { get; set; }
        public virtual DbSet<FacturaEncabezado> FacturaEncabezado { get; set; }
        public virtual DbSet<VistaPersonaFacturado> VistaPersonaFacturado { get; set; }
        public virtual DbSet<VistaPersonaProductoMasCaro> VistaPersonaProductoMasCaro { get; set; }
        public virtual DbSet<VistaProductosPorCantidad> VistaProductosPorCantidad { get; set; }
        public virtual DbSet<VistaProductosPorUtilidad> VistaProductosPorUtilidad { get; set; }
        public virtual DbSet<VistaProductosPorMargenGanancia> VistaProductosPorMargenGanancia { get; set; }
        


    }
}
