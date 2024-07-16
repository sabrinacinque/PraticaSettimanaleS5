using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PraticaSettimanaleS5.Models;
using PraticaSettimanaleS5.Services;
using System.Collections.Generic;
using System.Diagnostics;

namespace PraticaSettimanaleS5.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShipmentService _shipmentService;

        public HomeController(IShipmentService shipmentService)
        {
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

        [HttpPost]
        public IActionResult SubmitContact(string firstName, string lastName, string email, string message)
        {
            // Non fare nulla con i dati al momento
            return Ok();
        }

        public IActionResult Search()
        {
            return View();
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
