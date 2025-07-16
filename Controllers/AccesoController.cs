using casa_codigo_cursos.Context;
using casa_codigo_cursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace casa_codigo_cursos.Controllers
{
    public class AccesoController : Controller
    {
        private readonly CasaCodigoDbContext _context;

        public AccesoController(CasaCodigoDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            // Buscamos al usuario por su email
            var usuario = await _context.Usuarios
                                        .FirstOrDefaultAsync(u => u.Email == modelo.Email);

            
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(modelo.Password, usuario.Password))
            {
                ModelState.AddModelError(string.Empty, "Usuario o Contraseña inválidos.");
                return View(modelo);
            }

            // Si todo está bien, creamos la identidad (Claims) y la cookie
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()), 
            };

            var claimsIdentity = new ClaimsIdentity(claims, Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            
            return RedirectToAction("Index", "Home");
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }
    }
}