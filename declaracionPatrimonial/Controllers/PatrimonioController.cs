using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using declaracionPatrimonial.Data;
using declaracionPatrimonial.Models;
using declaracionPatrimonial.ViewModel;
using System.Linq;
using System.Threading.Tasks;

public class PatrimonioController : Controller
{
    private readonly MyDbContext _context;

    public PatrimonioController(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // tomar id de la sesion
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            TempData["Error"] = "Debes iniciar sesión para acceder al patrimonio.";
            return RedirectToAction("Login", "Usuario");
        }

        // ver al usuario actual 
        var usuario = await _context.Usuario.FindAsync(userId);
        if (usuario == null)
        {
            TempData["Error"] = "Usuario no encontrado.";
            return RedirectToAction("Login", "Usuario");
        }

        //patrimonio
        var datos = await (from u in _context.Usuario
                           where u.IDUsuario == userId
                           select new PatrimonioViewModel
                           {
                               IDUsuario = u.IDUsuario,
                               NombreUsuario = u.Nombre_Usuario + " " + u.Apellido_Usuario,

                               TotalActivos =
                                   (decimal)(_context.cuentaBancaria
                                       .Where(c => c.IDUsuario == u.IDUsuario)
                                       .Sum(c => (decimal?)c.Saldo_cuentaBancaria) ?? 0)
                                   +
                                   (decimal)(_context.Bienes
                                       .Where(b => b.IDUsuario == u.IDUsuario)
                                       .Sum(b => (decimal?)b.Precio_Bien) ?? 0),

                               TotalPasivos =
                                   (decimal)(_context.creditoBancario
                                       .Where(c => c.IDUsuario == u.IDUsuario)
                                       .Sum(c => (decimal?)c.Cantidad_AprobadaCredito ?? 0)
                                   +
                                   (decimal)(_context.otroPasivo
                                       .Where(o => o.IDUsuario == u.IDUsuario)
                                       .Sum(o => (decimal?)o.Cantidad_aprobadaPasivo) ?? 0))
                           }).ToListAsync();

        return View(datos);
    }
}
