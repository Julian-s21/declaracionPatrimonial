
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using declaracionPatrimonial.Data;
using declaracionPatrimonial.Models;
using Microsoft.Extensions.Logging;

namespace declaracionPatrimonial.Controllers
{
    [Route("CuentaBancaria")]
    public class CuentaBancariaController : Controller
    {
        private readonly ILogger<CuentaBancariaController> _logger;
        private readonly MyDbContext _context;

        public CuentaBancariaController(ILogger<CuentaBancariaController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        private int? GetUserId() => HttpContext.Session.GetInt32("UserId");

        // funcion para mostrar en la vista una lista de las cuetntas
        [HttpGet("")]
        public async Task<IActionResult> IndexAsync()
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            
            var cuentas = await _context.cuentaBancaria
                .Include(c => c.Banco)
                .Include(c => c.TipoCuenta)
                .Include(c => c.Usuario)
                .Where(c => c.IDUsuario == userId)
                .ToListAsync();

            return View(cuentas);
        }

        // funcion para crear un nuevo registro 
        [HttpGet("Create")]
        public IActionResult Create()
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            ViewBag.Bancos = _context.Banco.ToList();
            ViewBag.TiposCuenta = _context.tipoCuenta.ToList();
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(cuentaBancaria cuenta)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            if (!ModelState.IsValid)
            {
                ViewBag.Bancos = _context.Banco.ToList();
                ViewBag.TiposCuenta = _context.tipoCuenta.ToList();
                return View(cuenta);
            }

            cuenta.IDUsuario = userId.Value;
            _context.cuentaBancaria.Add(cuenta);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Cuenta creada correctamente.";
            return RedirectToAction("Index");
        }

        // funcion para obtener datos de la cuenta a editar
        [HttpPost("LoadEdit")]
        [ValidateAntiForgeryToken]
        public IActionResult LoadEdit(int id)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            var cuenta = _context.cuentaBancaria
                .Include(c => c.Banco)
                .Include(c => c.TipoCuenta)
                .FirstOrDefault(c => c.IDCuentaBancaria == id && c.IDUsuario == userId);

            if (cuenta == null)
                return NotFound();

            ViewBag.Bancos = _context.Banco.ToList();
            ViewBag.TiposCuenta = _context.tipoCuenta.ToList();

            return View("Edit", cuenta);
        }

        // Funcion para editar es un post
        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(cuentaBancaria cuenta)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            if (!ModelState.IsValid)
            {
                ViewBag.Bancos = _context.Banco.ToList();
                ViewBag.TiposCuenta = _context.tipoCuenta.ToList();
                return View(cuenta);
            }

            var existente = _context.cuentaBancaria
                .FirstOrDefault(c => c.IDCuentaBancaria == cuenta.IDCuentaBancaria && c.IDUsuario == userId);

            if (existente == null)
                return NotFound();

            existente.Numero_cuentaBancaria = cuenta.Numero_cuentaBancaria;
            existente.Saldo_cuentaBancaria = cuenta.Saldo_cuentaBancaria;
            existente.IDBanco = cuenta.IDBanco;
            existente.IDtipoCuenta = cuenta.IDtipoCuenta;

            _context.Update(existente);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Cuenta actualizada correctamente.";
            return RedirectToAction("Index");
        }

        // Funcion para eliminar

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            var cuenta = _context.cuentaBancaria
                .FirstOrDefault(c => c.IDCuentaBancaria == id && c.IDUsuario == userId);

            if (cuenta == null)
                return NotFound();

            _context.cuentaBancaria.Remove(cuenta);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Cuenta eliminada correctamente.";
            return RedirectToAction("Index");
        }

        // funcion por defecto error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        

    }
}
