using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PraticaSettimanaleS5.Models;
using PraticaSettimanaleS5.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PraticaSettimanaleS5.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login([FromQuery] string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser model, [FromQuery] string returnUrl = "/")
        {
            var user = _authService.Login(model.Username, model.Password);
            if (user != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
                return Redirect(returnUrl);
            }
            ViewData["ReturnUrl"] = returnUrl;
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
