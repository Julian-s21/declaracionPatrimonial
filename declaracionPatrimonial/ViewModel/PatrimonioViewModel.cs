using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace declaracionPatrimonial.ViewModel
{
    public class PatrimonioViewModel
    {
    public int IDUsuario { get; set; }
    public string? NombreUsuario { get; set; }
    public decimal TotalActivos { get; set; }
    public decimal TotalPasivos { get; set; }
    public decimal Patrimonio => TotalActivos - TotalPasivos;
    }
}