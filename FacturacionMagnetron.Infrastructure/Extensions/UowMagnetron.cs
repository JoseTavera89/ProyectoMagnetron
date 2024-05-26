using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.Repository;
using FacturacionMagnetron.Domain.Interfaces.UnitOfWork;
using FacturacionMagnetron.Infrastructure.Persistense;
using FacturacionMagnetron.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Infrastructure.Extensions
{
    public class UowMagnetron : IUowMagnetron
    {
        private readonly MagnetronDBContext _magnetronDBContext;
        public IGenericRepository<Persona> _persona;
        public IGenericRepository<Producto> _producto;
        public IGenericRepository<FacturaEncabezado> _facturaEncabezado;
        IGenericRepository<FacturaDetalle> _facturaDetalle;
        public UowMagnetron(MagnetronDBContext magnetronDBContext)
        {
            _magnetronDBContext = magnetronDBContext;
        }

        public IGenericRepository<Persona> Persona
        {
            get
            {
                return _persona == null ?
                       _persona = new GenericRepository<Persona>(_magnetronDBContext) : _persona;
            }
        }

        public IGenericRepository<Producto> Producto
        {
            get
            {
                return _producto == null ?
                       _producto = new GenericRepository<Producto>(_magnetronDBContext) : _producto;
            }
        }

        public IGenericRepository<FacturaEncabezado> FacturaEncabezado
        {
            get
            {
                return _facturaEncabezado == null ?
                       _facturaEncabezado = new GenericRepository<FacturaEncabezado>(_magnetronDBContext) : _facturaEncabezado;
            }
        }

        public IGenericRepository<FacturaDetalle> FacturaDetalle
        {
            get
            {
                return _facturaDetalle == null ?
                       _facturaDetalle = new GenericRepository<FacturaDetalle>(_magnetronDBContext) : _facturaDetalle;
            }
        }

        public void Dispose()
        {
            _magnetronDBContext.Dispose();
        }

        public void SaveChanges()
        {
            _magnetronDBContext.SaveChanges();
        }
    }
}
