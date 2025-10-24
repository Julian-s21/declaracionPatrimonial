using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using declaracionPatrimonial.Data;
using declaracionPatrimonial.Models;

namespace declaracionPatrimonial.Controllers
{
    [Route("OtroPasivo")]
    public class OtroPasivoController : Controller
    {
        private readonly ILogger<OtroPasivoController> _logger;
        private readonly MyDbContext _context;

        public OtroPasivoController(ILogger<OtroPasivoController> logger, MyDbContext context)
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

            var pasivos = await _context.otroPasivo
                .Include(c => c.cuentaBancaria)
                .Include(c => c.Usuario)
                .Where(p => p.IDUsuario == userId)
                .ToListAsync();

            return View(pasivos);
        }

   
        [HttpGet("Create")]
        public IActionResult Create()
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            // Cargar cuentas bancarias para el select
            ViewBag.CuentasBancarias = _context.cuentaBancaria
                .Where(c => c.IDUsuario == userId)
                .ToList();

            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(otroPasivo pasivo)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            if (!ModelState.IsValid)
            {
                ViewBag.CuentasBancarias = _context.cuentaBancaria
                    .Where(c => c.IDUsuario == userId)
                    .ToList();
                return View(pasivo);
            }

            pasivo.IDUsuario = userId.Value;
            _context.otroPasivo.Add(pasivo);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Pasivo registrado correctamente.";
            return RedirectToAction("Index");
        }


        [HttpPost("LoadEdit")]
        [ValidateAntiForgeryToken]
        public IActionResult LoadEdit(int id)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            var pasivo = _context.otroPasivo
                .Include(p => p.cuentaBancaria)
                .FirstOrDefault(p => p.IDPasivo == id && p.IDUsuario == userId);

            if (pasivo == null)
                return NotFound();

            ViewBag.CuentasBancarias = _context.cuentaBancaria
                .Where(c => c.IDUsuario == userId)
                .ToList();

            return View("Edit", pasivo);
        }



        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(otroPasivo pasivo)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            if (!ModelState.IsValid)
            {
                ViewBag.CuentasBancarias = _context.cuentaBancaria
                    .Where(c => c.IDUsuario == userId)
                    .ToList();
                return View(pasivo);
            }

            var existente = _context.otroPasivo
                .FirstOrDefault(p => p.IDPasivo == pasivo.IDPasivo && p.IDUsuario == userId);

            if (existente == null)
                return NotFound();

            existente.Cantidad_aprobadaPasivo = pasivo.Cantidad_aprobadaPasivo;
            existente.Motivo_Pasivo = pasivo.Motivo_Pasivo;
            existente.IDCuentaBancaria = pasivo.IDCuentaBancaria;

            _context.Update(existente);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Pasivo actualizado correctamente.";
            return RedirectToAction("Index");
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            var pasivo = _context.otroPasivo
                .FirstOrDefault(p => p.IDPasivo == id && p.IDUsuario == userId);

            if (pasivo == null)
                return NotFound();

            _context.otroPasivo.Remove(pasivo);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Pasivo eliminado correctamente.";
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
