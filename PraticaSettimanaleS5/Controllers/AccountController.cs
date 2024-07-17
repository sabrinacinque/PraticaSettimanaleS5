using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PraticaSettimanaleS5.Services;
using PraticaSettimanaleS5.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PraticaSettimanaleS5.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authenticationService;

        public AccountController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser user)
        {
            try
            {
                var u = _authenticationService.Login(user.Username, user.Password);
                if (u == null) return RedirectToAction("Login");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, u.Username),
                    new Claim("FriendlyName", u.FriendlyName ?? string.Empty)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
            }
            catch (Exception ex)
            {
                // Gestisci l'eccezione (log, ecc.)
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
