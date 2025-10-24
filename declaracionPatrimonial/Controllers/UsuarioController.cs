// ...existing code...
using System.Linq;
using System.Threading.Tasks;
using declaracionPatrimonial.Data;
using declaracionPatrimonial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace declaracionPatrimonial.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly MyDbContext _context;

        public UsuarioController(ILogger<UsuarioController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuario.ToListAsync();
            return View(usuarios);
        }

       
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View(); // Usará Views/Usuario/Login.cshtml
        }


        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Correo_Usuario == model.Correo_Usuario
                                       && u.Contrasena_Usuario == model.Contrasena_Usuario);

            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View(model);
            }

            
            HttpContext.Session.SetInt32("UserId", usuario.IDUsuario);
            HttpContext.Session.SetString("UserName", usuario.Nombre_Usuario ?? string.Empty);
            HttpContext.Session.SetString("UserRole", usuario.Rol_Usuario ?? string.Empty);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Usuario model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Verificar si ya existe un usuario con el mismo correo
            var exists = await _context.Usuario.AnyAsync(u => u.Correo_Usuario == model.Correo_Usuario);
            if (exists)
            {
                ViewBag.Error = "El correo ya está registrado.";
                return View(model);
            }

            // Asignar rol Ciudadano automáticamente
            model.Rol_Usuario = "Ciudadano";

            _context.Usuario.Add(model);
            await _context.SaveChangesAsync();


            TempData["SuccessMessage"] = "Usuario registrado exitosamente. Ya puedes iniciar sesión.";

            return RedirectToAction(nameof(Login));
        }
        
        // ===============================
        // LOGOUT
        // ===============================
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            // Limpiar sesión
            HttpContext.Session.Clear();

            // Redirigir al Login
            return RedirectToAction("Login");
        }
    }
}
