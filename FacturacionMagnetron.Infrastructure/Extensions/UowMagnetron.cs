using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.Repository;
using FacturacionMagnetron.Domain.Interfaces.Services;
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
        public IGenericRepository<FacturaDetalle> _facturaDetalle;
        public IViewRepository<VistaPersonaFacturado> _vistaPersonaFacturado;
        public IViewRepository<VistaPersonaProductoMasCaro> _vistaPersonaProductoMasCaro;
        public IViewRepository<VistaProductosPorCantidad> _vistaProductosPorCantidad;
        public IViewRepository<VistaProductosPorUtilidad> _vistaProductosPorUtilidad;
        public IViewRepository<VistaProductosPorMargenGanancia> _vistaProductosPorMargenGanancia;
        


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

        public IViewRepository<VistaPersonaFacturado> VistaPersonaFacturado
        {
            get
            {
                return _vistaPersonaFacturado == null ?
                       _vistaPersonaFacturado = new ViewRepository<VistaPersonaFacturado>(_magnetronDBContext) : _vistaPersonaFacturado;
            }
        }

        public IViewRepository<VistaPersonaProductoMasCaro> VistaPersonaProductoMasCaro
        {
            get
            {
                return _vistaPersonaProductoMasCaro == null ?
                       _vistaPersonaProductoMasCaro = new ViewRepository<VistaPersonaProductoMasCaro>(_magnetronDBContext) : _vistaPersonaProductoMasCaro;
            }
        }
        public IViewRepository<VistaProductosPorCantidad> VistaProductosPorCantidad
        {
            get
            {
                return _vistaProductosPorCantidad == null ?
                       _vistaProductosPorCantidad = new ViewRepository<VistaProductosPorCantidad>(_magnetronDBContext) : _vistaProductosPorCantidad;
            }
        }

        public IViewRepository<VistaProductosPorUtilidad> VistaProductosPorUtilidad
        {
            get
            {
                return _vistaProductosPorUtilidad == null ?
                       _vistaProductosPorUtilidad = new ViewRepository<VistaProductosPorUtilidad>(_magnetronDBContext) : _vistaProductosPorUtilidad;
            }
        }
        public IViewRepository<VistaProductosPorMargenGanancia> VistaProductosPorMargenGanancia
        {
            get
            {
                return _vistaProductosPorMargenGanancia == null ?
                       _vistaProductosPorMargenGanancia = new ViewRepository<VistaProductosPorMargenGanancia>(_magnetronDBContext) : _vistaProductosPorMargenGanancia;
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
