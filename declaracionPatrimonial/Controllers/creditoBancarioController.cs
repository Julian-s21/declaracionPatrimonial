using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using declaracionPatrimonial.Data;
using declaracionPatrimonial.Models;

namespace declaracionPatrimonial.Controllers
{
    [Route("CreditoBancario")]
    public class CreditoBancarioController : Controller
    {
        private readonly ILogger<CreditoBancarioController> _logger;
        private readonly MyDbContext _context;

        public CreditoBancarioController(ILogger<CreditoBancarioController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        private int? GetUserId() => HttpContext.Session.GetInt32("UserId");


        [HttpGet("")]
        public async Task<IActionResult> IndexAsync()
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            var creditos = await _context.creditoBancario
                .Include(c => c.Banco)
                .Include(c => c.tipoCuenta)
                .Include(c => c.cuentaBancaria)
                .Include(c => c.Usuario)
                .Where(c => c.IDUsuario == userId)
                .ToListAsync();

            return View(creditos);
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            ViewBag.Usuarios = _context.Usuario.ToList();
            ViewBag.Bancos = _context.Banco.ToList();
            ViewBag.TiposCuenta = _context.tipoCuenta.ToList();
            ViewBag.CuentasBancarias = _context.cuentaBancaria
                .Where(c => c.IDUsuario == userId)
                .ToList();

            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(creditoBancario credito)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            if (!ModelState.IsValid)
            {
                ViewBag.Usuarios = _context.Usuario.ToList();
                ViewBag.Bancos = _context.Banco.ToList();
                ViewBag.TiposCuenta = _context.tipoCuenta.ToList();
                ViewBag.CuentasBancarias = _context.cuentaBancaria
                    .Where(c => c.IDUsuario == userId)
                    .ToList();

                return View(credito);
            }

            credito.IDUsuario = userId.Value;
            _context.creditoBancario.Add(credito);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Crédito bancario registrado correctamente.";
            return RedirectToAction("Index");
        }


        [HttpPost("LoadEdit")]
        [ValidateAntiForgeryToken]
        public IActionResult LoadEdit(int id)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            var credito = _context.creditoBancario
                .Include(c => c.Banco)
                .Include(c => c.tipoCuenta)
                .Include(c => c.cuentaBancaria)
                .FirstOrDefault(c => c.IDCreditoBancario == id && c.IDUsuario == userId);

            if (credito == null)
                return NotFound();

            ViewBag.Bancos = _context.Banco.ToList();
            ViewBag.TiposCuenta = _context.tipoCuenta.ToList();
            ViewBag.CuentasBancarias = _context.cuentaBancaria
                .Where(c => c.IDUsuario == userId)
                .ToList();

            return View("Edit", credito);
        }


        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(creditoBancario credito)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            if (!ModelState.IsValid)
            {
                ViewBag.Bancos = _context.Banco.ToList();
                ViewBag.TiposCuenta = _context.tipoCuenta.ToList();
                ViewBag.CuentasBancarias = _context.cuentaBancaria
                    .Where(c => c.IDUsuario == userId)
                    .ToList();

                return View(credito);
            }

            var existente = _context.creditoBancario
                .FirstOrDefault(c => c.IDCreditoBancario == credito.IDCreditoBancario && c.IDUsuario == userId);

            if (existente == null)
                return NotFound();

            existente.Cantidad_AprobadaCredito = credito.Cantidad_AprobadaCredito;
            existente.Motivo_creditoBancario = credito.Motivo_creditoBancario;
            existente.IDBanco = credito.IDBanco;
            existente.IDtipoCuenta = credito.IDtipoCuenta;
            existente.IDCuentaBancaria = credito.IDCuentaBancaria;

            _context.Update(existente);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Crédito bancario actualizado correctamente.";
            return RedirectToAction("Index");
        }


        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            var credito = _context.creditoBancario
                .FirstOrDefault(c => c.IDCreditoBancario == id && c.IDUsuario == userId);

            if (credito == null)
                return NotFound();

            _context.creditoBancario.Remove(credito);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Crédito bancario eliminado correctamente.";
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
