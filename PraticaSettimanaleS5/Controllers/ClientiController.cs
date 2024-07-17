using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraticaSettimanaleS5.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PraticaSettimanaleS5.Controllers
{
    [Authorize]
    public class ClientiController : Controller
    {
        private readonly string _connectionString;

        public ClientiController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EShipping");
        }

        public IActionResult Index()
        {
            List<Cliente> clienti = new List<Cliente>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Clienti";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clienti.Add(new Cliente
                            {
                                ClienteId = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                ÈAzienda = reader.GetBoolean(2),
                                CodiceFiscale = reader.IsDBNull(3) ? null : reader.GetString(3),
                                PartitaIVA = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Indirizzo = reader.GetString(5),
                                Città = reader.GetString(6),
                                NumeroTelefono = reader.GetString(7),
                                Email = reader.GetString(8)
                            });
                        }
                    }
                }
            }
            return View(clienti);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Clienti (Nome, ÈAzienda, CodiceFiscale, PartitaIVA, Indirizzo, Città, NumeroTelefono, Email)
                        VALUES (@Nome, @ÈAzienda, @CodiceFiscale, @PartitaIVA, @Indirizzo, @Città, @NumeroTelefono, @Email)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                        cmd.Parameters.AddWithValue("@ÈAzienda", cliente.ÈAzienda);
                        cmd.Parameters.AddWithValue("@CodiceFiscale", (object)cliente.CodiceFiscale ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PartitaIVA", (object)cliente.PartitaIVA ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Indirizzo", cliente.Indirizzo);
                        cmd.Parameters.AddWithValue("@Città", cliente.Città);
                        cmd.Parameters.AddWithValue("@NumeroTelefono", cliente.NumeroTelefono);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }
    }
}
