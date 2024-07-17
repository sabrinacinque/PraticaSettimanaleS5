using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraticaSettimanaleS5.Models;
using System;
using System.Data.SqlClient;

namespace PraticaSettimanaleS5.Controllers
{
    [Authorize]
    public class StatiSpedizioniController : Controller
    {
        private readonly string _connectionString;

        public StatiSpedizioniController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EShipping");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateStatus(StatoSpedizione statoSpedizione)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO StatiSpedizioni (SpedizioneId, DataAggiornamento, Stato, Luogo, Descrizione)
                        SELECT s.SpedizioneId, @DataAggiornamento, @Stato, @Luogo, @Descrizione
                        FROM Spedizioni s
                        WHERE s.NumeroTracking = @NumeroTracking";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NumeroTracking", statoSpedizione.NumeroTracking);
                        cmd.Parameters.AddWithValue("@DataAggiornamento", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Stato", statoSpedizione.Stato);
                        cmd.Parameters.AddWithValue("@Luogo", statoSpedizione.Luogo);
                        cmd.Parameters.AddWithValue("@Descrizione", (object)statoSpedizione.Descrizione ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Index", statoSpedizione);
        }
    }
}
