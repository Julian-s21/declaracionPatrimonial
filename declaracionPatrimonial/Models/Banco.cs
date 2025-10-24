using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace declaracionPatrimonial.Models
{
public class Banco
    {
        [Key]
        public int IDBanco { get; set; }
        public string? Nombre_Banco { get; set; }

        // Relaciones
        public ICollection<cuentaBancaria>? cuentasBancaria { get; set; }
        public ICollection<creditoBancario>? creditoBancario { get; set; }
    }
}