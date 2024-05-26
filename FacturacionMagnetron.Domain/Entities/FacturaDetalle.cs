using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Entities
{
    public class FacturaDetalle
    {
        [Key]
        public int FDet_Id { get; set; }
        public int FDet_Linea { get; set; }
        public int FDet_Cantidad { get; set; }
        public int Prod_Id { get; set; }
        public int FEnc_Id { get; set; }

    }
}
