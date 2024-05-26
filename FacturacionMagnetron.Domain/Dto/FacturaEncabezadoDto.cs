using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Dto
{
    public class FacturaEncabezadoDto
    {
        public int FEnc_Id { get; set; }
        [Required]
        public int FEnc_Numero { get; set; }
        [Required]
        public DateTime FEnc_Fecha { get; set; }
        [Required]
        public int Per_Id { get; set; }
    }
}
