using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace declaracionPatrimonial.Models
{
    public class Usuario
    {
        [Key]
        public int IDUsuario { get; set; }
        public string? Nombre_Usuario { get; set; }
        public string? Apellido_Usuario { get; set; }
        public string? Correo_Usuario { get; set; }
        public string? Contrasena_Usuario { get; set; }
        public string? Rol_Usuario { get; set; }

        // Relaciones
        public ICollection<cuentaBancaria>? cuentasBancaria { get; set; }

        public ICollection<Bienes>? Bienes { get; set; }
        public ICollection<creditoBancario>? creditoBancario { get; set; }
        public ICollection<otroPasivo>? otroPasivo { get; set; }
    }
}