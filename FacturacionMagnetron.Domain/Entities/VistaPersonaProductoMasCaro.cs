using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Entities
{
    public  class VistaPersonaProductoMasCaro
    {
        [Key]
        public int Per_Id { get; set; }
        public string? Per_Nombre { get; set; }
        public string? Per_Apellido { get; set; }
        public decimal PrecioProductoMasCaro { get; set; }
    }
}
