using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Dto
{
    public class FacturaDetalleDto
    {
        public int FDet_Id { get; set; }
        [Required]
        public int FDet_Linea { get; set; }
        [Required]
        public int FDetCantidad { get; set; }
        [Required]
        public int Prod_Id { get; set; }
        [Required]
        public int FEnc_Id { get; set; }
    }
}
