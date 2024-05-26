using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Dto
{
    public class ProductoDto
    {
        public int Prod_Id { get; set; }
        [Required]
        public string? Prod_Descripcion { get; set; }
        [Required]
        public decimal Prod_Precio { get; set; }
        [Required]
        public decimal Prod_Costo { get; set; }
        [Required]
        public decimal Prod_UM { get; set; }
    }
}
