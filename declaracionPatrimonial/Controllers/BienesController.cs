using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using declaracionPatrimonial.Data;
using declaracionPatrimonial.Models;

namespace declaracionPatrimonial.Controllers
{
    [Route("Bienes")]
    public class BienesController : Controller
    {
        private readonly ILogger<BienesController> _logger;
        private readonly MyDbContext _context;

        public BienesController(ILogger<BienesController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        private int? GetUserId() => HttpContext.Session.GetInt32("UserId");

        // Funcion donde listaremos en el index
        [HttpGet("")]
        public async Task<IActionResult> IndexAsync()
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            var bienes = await _context.Bienes
                .Include(b => b.tipoInmueble)
                .Include(b => b.tipoPropiedad)
                .Include(b => b.Usuario)
                .Where(b => b.IDUsuario == userId)
                .ToListAsync();

            return View(bienes);
        }

        // Funcion del controlador donde crea nuevos objetos
        [HttpGet("Create")]
        public IActionResult Create()
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            ViewBag.TiposInmueble = _context.tipoInmueble.ToList();
            ViewBag.TiposPropiedad = _context.tipoPropiedad.ToList();

            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bienes bien)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            if (!ModelState.IsValid)
            {
                ViewBag.TiposInmueble = _context.tipoInmueble.ToList();
                ViewBag.TiposPropiedad = _context.tipoPropiedad.ToList();
                return View(bien);
            }

            bien.IDUsuario = userId.Value;
            _context.Bienes.Add(bien);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Bien registrado correctamente.";
            return RedirectToAction("Index");
        }

        // Funcion del controlador para editar un objeto
        [HttpPost("LoadEdit")]
        [ValidateAntiForgeryToken]
        public IActionResult LoadEdit(int id)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            var bien = _context.Bienes
                .Include(b => b.tipoInmueble)
                .Include(b => b.tipoPropiedad)
                .FirstOrDefault(b => b.IDBienes == id && b.IDUsuario == userId);

            if (bien == null)
                return NotFound();

            ViewBag.TiposInmueble = _context.tipoInmueble.ToList();
            ViewBag.TiposPropiedad = _context.tipoPropiedad.ToList();

            return View("Edit", bien);
        }

        // Funcion para editar 
        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bienes bien)
        {
            int? userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Usuario");

            if (!ModelState.IsValid)
            {
                ViewBag.TiposInmueble = _context.tipoInmueble.ToList();
                ViewBag.TiposPropiedad = _context.tipoPropiedad.ToList();
                return View(bien);
            }

            var existente = _context.Bienes
                .FirstOrDefault(b => b.IDBienes == bien.IDBienes && b.IDUsuario == userId);

            if (existente == null)
                return NotFound();

            existente.Nombre_Bienes = bien.Nombre_Bienes;
            existente.Fecha_adquisicionBien = bien.Fecha_adquisicionBien;
            existente.Descripcion_Bien = bien.Descripcion_Bien;
            existente.Forma_adquisicionBien = bien.Forma_adquisicionBien;
            existente.Precio_Bien = bien.Precio_Bien;
            existente.IDtipoInmueble = bien.IDtipoInmueble;
            existente.IDtipoPropiedad = bien.IDtipoPropiedad;

            _context.Update(existente);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Bien actualizado correctamente.";
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

            var bien = _context.Bienes
                .FirstOrDefault(b => b.IDBienes == id && b.IDUsuario == userId);

            if (bien == null)
                return NotFound();

            _context.Bienes.Remove(bien);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Bien eliminado correctamente.";
            return RedirectToAction("Index");
        }

        // Funcion de error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
