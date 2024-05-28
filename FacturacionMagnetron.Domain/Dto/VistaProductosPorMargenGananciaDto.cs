using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Dto
{
    public class VistaProductosPorMargenGananciaDto
    {
        [Key]
        public int Prod_Id { get; set; }
        public string? Prod_Descripcion { get; set; }
        public decimal Prod_Precio { get; set; }
        public decimal Prod_Costo { get; set; }
        public string Prod_UM { get; set; }
        public decimal MargenGananciaPromedio { get; set; }
    }
}
