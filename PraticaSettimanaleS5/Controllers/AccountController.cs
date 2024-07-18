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
            if (user != null)//Se l'autenticazione ha successo (user != null), viene creato un elenco di claim contenente il nome utente e il ruolo dell'utente.
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//Viene creata una ClaimsIdentity con i claim e lo schema di autenticazione.
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,//L'utente viene autenticato utilizzando HttpContext.SignInAsync, che crea una sessione di autenticazione basata su cookie.
                    new ClaimsPrincipal(identity));
                return Redirect(returnUrl);//L'utente viene reindirizzato all'URL di ritorno specificato.
            }
            ViewData["ReturnUrl"] = returnUrl;
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");//Se l'autenticazione fallisce, viene impostato un messaggio di errore nel ModelState e viene restituita la vista di login con il modello corrente.
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {//HttpContext.SignOutAsync viene chiamato per terminare la sessione di autenticazione dell'utente.
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
