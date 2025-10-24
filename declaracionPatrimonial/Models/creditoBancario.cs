using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace declaracionPatrimonial.Models
{
    public class creditoBancario
    {
        [Key]
        public int IDCreditoBancario { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Cantidad_AprobadaCredito { get; set; }
        public string? Motivo_creditoBancario { get; set; }

        // FK
        public int IDUsuario { get; set; }
        public int IDBanco { get; set; }
        public int IDtipoCuenta { get; set; }
                [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public int IDCuentaBancaria { get; set; }

        // Navegación
        public Usuario? Usuario { get; set; }
        public Banco? Banco { get; set; }
        public tipoCuenta? tipoCuenta { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public cuentaBancaria? cuentaBancaria { get; set; }
    }
}