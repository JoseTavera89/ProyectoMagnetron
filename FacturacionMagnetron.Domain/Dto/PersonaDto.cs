using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Dto
{
    public class PersonaDto
    {
        public int Per_Id { get; set; }
        [Required]
        public string? Per_Nombre { get; set; }
        [Required]
        public string? Per_Apellido { get; set; }
        [Required]
        public string? Per_TipoDocumento { get; set; }
        [Required]
        public int Per_Documento { get; set; }
    }
}
