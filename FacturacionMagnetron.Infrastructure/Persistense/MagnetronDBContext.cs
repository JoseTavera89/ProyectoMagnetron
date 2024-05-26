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
    }
}
