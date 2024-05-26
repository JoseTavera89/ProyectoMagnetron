using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Entities
{
    public  class Persona
    {
        [Key]
        public int Per_Id { get; set; }
        public string? Per_Nombre { get; set; }
        public string? Per_Apellido { get; set; }
        public string? Per_TipoDocumento { get; set; }
        public int Per_Documento { get; set; }
    }
}
