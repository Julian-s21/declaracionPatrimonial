using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace declaracionPatrimonial.Models
{
    public class Bienes
    {
        [Key]
        public int IDBienes { get; set; }
        public string? Nombre_Bienes { get; set; }
        public DateTime? Fecha_adquisicionBien { get; set; }
        public string? Descripcion_Bien { get; set; }
        public string? Forma_adquisicionBien { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal? Precio_Bien { get; set; }

        // FK
        public int IDUsuario { get; set; }
        public int? IDtipoInmueble { get; set; }
        public int? IDtipoPropiedad { get; set; }

        // Navegación
        public Usuario? Usuario { get; set; }
        public tipoInmueble? tipoInmueble { get; set; }
        public tipoPropiedad? tipoPropiedad { get; set; }
    }
}