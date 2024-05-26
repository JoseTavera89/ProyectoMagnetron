using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Entities
{
    public class FacturaEncabezado
    {
        [Key]
        public int FEnc_Id { get; set; }
        public int FEnc_Numero { get; set; }
        public DateTime FEnc_Fecha { get; set; }
        public int Per_Id { get; set; }
    }
}
