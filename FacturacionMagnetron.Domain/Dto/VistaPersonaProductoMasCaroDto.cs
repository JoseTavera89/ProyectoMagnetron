using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Dto
{
    public class VistaPersonaProductoMasCaroDto
    {
        public int Per_Id { get; set; }
        public string? Per_Nombre { get; set; }
        public string? Per_Apellido { get; set; }
        public decimal PrecioProductoMasCaro { get; set; }
    }
}
