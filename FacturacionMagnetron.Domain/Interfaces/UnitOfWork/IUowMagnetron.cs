using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Domain.Entities;
using FacturacionMagnetron.Domain.Interfaces.Repository;
using FacturacionMagnetron.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Interfaces.UnitOfWork
{
    public interface IUowMagnetron : IDisposable
    {
        void SaveChanges();

        IGenericRepository<Persona> Persona { get; }
        IGenericRepository<Producto> Producto { get; }
        IGenericRepository<FacturaEncabezado> FacturaEncabezado { get; }
        IGenericRepository<FacturaDetalle> FacturaDetalle { get; }

        IViewRepository<VistaPersonaFacturado> VistaPersonaFacturado { get; }
        IViewRepository<VistaPersonaProductoMasCaro> VistaPersonaProductoMasCaro { get; }
        IViewRepository<VistaProductosPorCantidad> VistaProductosPorCantidad { get; }
        IViewRepository<VistaProductosPorUtilidad> VistaProductosPorUtilidad { get; }
        IViewRepository<VistaProductosPorMargenGanancia> VistaProductosPorMargenGanancia { get; }

    }
}
