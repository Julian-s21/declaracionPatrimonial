using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace declaracionPatrimonial.Models
{
    public class tipoPropiedad
    {
        [Key]
        public int IDtipoPropiedad { get; set; }
        public string? Nombre_tipoPropiedad { get; set; }

        // Relaciones
        public ICollection<Bienes>? Bienes { get; set; }
    }
}