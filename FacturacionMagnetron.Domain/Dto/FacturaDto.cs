using FacturacionMagnetron.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Dto
{
    public class FacturaDto
    {
        public FacturaEncabezadoDto FacturaEncabezado { get; set; }
        public FacturaDetalleDto FacturaDetalle { get; set; }
    }
}
