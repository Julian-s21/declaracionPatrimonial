using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace declaracionPatrimonial.Models
{
    public class otroPasivo
    {
        [Key]
        public int IDPasivo { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Cantidad_aprobadaPasivo { get; set; }
        public string? Motivo_Pasivo { get; set; }

        // FK
        public int IDUsuario { get; set; }
        public int IDCuentaBancaria { get; set; }

        // Navegación
        public Usuario? Usuario { get; set; }
        public cuentaBancaria? cuentaBancaria { get; set; }
    }
}