using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace declaracionPatrimonial.Models
{
    public class tipoInmueble
    {
        [Key]
        public int IDtipoInmueble { get; set; }
        public string? Nombre_tipoInmueble { get; set; }

        // Relaciones
        public ICollection<Bienes>? Bienes { get; set; }
    }
}