using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace declaracionPatrimonial.Models
{
    public class cuentaBancaria
    {
        [Key]
        public int IDCuentaBancaria { get; set; }
        public string? Numero_cuentaBancaria { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Saldo_cuentaBancaria { get; set; }

        // FK
        public int IDUsuario { get; set; }
        public int IDBanco { get; set; }
        public int IDtipoCuenta { get; set; }

        // Navegación
        public Usuario? Usuario { get; set; }
        public Banco? Banco { get; set; }
        public tipoCuenta? TipoCuenta { get; set; }

        public ICollection<creditoBancario>? creditoBancario { get; set; }
        public ICollection<otroPasivo>? otroPasivo { get; set; }

       
    }
    
}