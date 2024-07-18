using Microsoft.AspNetCore.Mvc;
using PraticaSettimanaleS5.Models;
using PraticaSettimanaleS5.Services;
using System.Diagnostics;

namespace PraticaSettimanaleS5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShipmentService _shipmentService;

        public HomeController(ILogger<HomeController> logger, IShipmentService shipmentService)
        {
            _logger = logger;
            _shipmentService = shipmentService;
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
            // Non voglio fare nulla di che in questa pagina,semplicemente la conferma dell'invio
            return Ok();
        }

        [HttpPost]
        public IActionResult Search(string idNumber, string trackingNumber)
        {
            var updates = _shipmentService.GetShipmentUpdates(idNumber, trackingNumber);
            if (updates.Count == 0)
            {
                ViewBag.ErrorMessage = "Cliente o spedizione non trovati.";
            }
            else
            {
                ViewBag.Updates = updates;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
