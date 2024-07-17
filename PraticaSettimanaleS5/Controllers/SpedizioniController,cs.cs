using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraticaSettimanaleS5.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PraticaSettimanaleS5.Controllers
{
    [Authorize]
    public class SpedizioniController : Controller
    {
        private readonly string _connectionString;

        public SpedizioniController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EShipping");
        }

        public IActionResult Index()
        {
            var spedizioni = GetSpedizioniWithLastStatus();
            return View(spedizioni);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Spedizione spedizione)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Spedizioni (ClienteId, NumeroTracking, DataSpedizione, Peso, CittàDestinazione, IndirizzoDestinazione, NomeDestinatario, CostoSpedizione, DataConsegnaPrevista)
                        VALUES (@ClienteId, @NumeroTracking, @DataSpedizione, @Peso, @CittàDestinazione, @IndirizzoDestinazione, @NomeDestinatario, @CostoSpedizione, @DataConsegnaPrevista)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClienteId", spedizione.ClienteId);
                        cmd.Parameters.AddWithValue("@NumeroTracking", spedizione.NumeroTracking);
                        cmd.Parameters.AddWithValue("@DataSpedizione", spedizione.DataSpedizione);
                        cmd.Parameters.AddWithValue("@Peso", spedizione.Peso);
                        cmd.Parameters.AddWithValue("@CittàDestinazione", spedizione.CittàDestinazione);
                        cmd.Parameters.AddWithValue("@IndirizzoDestinazione", spedizione.IndirizzoDestinazione);
                        cmd.Parameters.AddWithValue("@NomeDestinatario", spedizione.NomeDestinatario);
                        cmd.Parameters.AddWithValue("@CostoSpedizione", spedizione.CostoSpedizione);
                        cmd.Parameters.AddWithValue("@DataConsegnaPrevista", spedizione.DataConsegnaPrevista);
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(spedizione);
        }

        private List<Spedizione> GetSpedizioniWithLastStatus()
        {
            var spedizioni = new List<Spedizione>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT s.SpedizioneId, s.ClienteId, s.NumeroTracking, s.DataSpedizione, s.Peso, s.CittàDestinazione, s.IndirizzoDestinazione, s.NomeDestinatario, s.CostoSpedizione, s.DataConsegnaPrevista,
                           c.Nome AS ClienteNome, ultimiAggiornamenti.Stato AS UltimoStato
                    FROM Spedizioni s
                    JOIN Clienti c ON s.ClienteId = c.ClienteId
                    LEFT JOIN (
                        SELECT SpedizioneId, Stato, DataAggiornamento
                        FROM StatiSpedizioni ss
                        WHERE DataAggiornamento = (
                            SELECT MAX(DataAggiornamento)
                            FROM StatiSpedizioni
                            WHERE SpedizioneId = ss.SpedizioneId
                        )
                    ) AS ultimiAggiornamenti ON s.SpedizioneId = ultimiAggiornamenti.SpedizioneId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var spedizione = new Spedizione
                            {
                                SpedizioneId = reader.GetInt32(0),
                                ClienteId = reader.GetInt32(1),
                                NumeroTracking = reader.GetString(2),
                                DataSpedizione = reader.GetDateTime(3),
                                Peso = reader.GetDecimal(4),
                                CittàDestinazione = reader.GetString(5),
                                IndirizzoDestinazione = reader.GetString(6),
                                NomeDestinatario = reader.GetString(7),
                                CostoSpedizione = reader.GetDecimal(8),
                                DataConsegnaPrevista = reader.GetDateTime(9),
                                Cliente = new Cliente { Nome = reader.GetString(10) },
                                UltimoStato = reader.IsDBNull(11) ? null : reader.GetString(11)
                            };
                            spedizioni.Add(spedizione);
                        }
                    }
                }
            }
            return spedizioni;
        }
    }
}
