using Microsoft.AspNetCore.Mvc;
using PraticaSettimanaleS5.Models;
using System.Diagnostics;

namespace PraticaSettimanaleS5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SubmitContact(string firstName, string lastName, string email, string message)
        {
            // Non fare nulla con i dati al momento
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
