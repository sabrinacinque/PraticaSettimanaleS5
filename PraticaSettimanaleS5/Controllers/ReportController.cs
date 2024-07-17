using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraticaSettimanaleS5.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PraticaSettimanaleS5.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly string _connectionString;

        public ReportController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EShipping");
        }

        public IActionResult Index()
        {
            var inDeliveryShipments = GetInDeliveryShipments();
            var pendingShipmentsCount = GetPendingShipmentsCount();
            var shipmentsByCity = GetShipmentsByCity();

            var model = new ReportViewModel
            {
                TodayShipments = inDeliveryShipments,
                PendingShipmentsCount = pendingShipmentsCount,
                ShipmentsByCity = shipmentsByCity
            };

            return View(model);
        }

        private List<Spedizione> GetInDeliveryShipments()
        {
            var inDeliveryShipments = new List<Spedizione>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT s.SpedizioneId, s.ClienteId, s.NumeroTracking, s.DataSpedizione, s.Peso, s.CittàDestinazione, s.IndirizzoDestinazione, s.NomeDestinatario, s.CostoSpedizione, s.DataConsegnaPrevista
                    FROM Spedizioni s
                    JOIN (
                        SELECT SpedizioneId, Stato, DataAggiornamento
                        FROM StatiSpedizioni ss
                        WHERE DataAggiornamento = (
                            SELECT MAX(DataAggiornamento)
                            FROM StatiSpedizioni
                            WHERE SpedizioneId = ss.SpedizioneId
                        )
                    ) AS ultimiAggiornamenti ON s.SpedizioneId = ultimiAggiornamenti.SpedizioneId
                    WHERE ultimiAggiornamenti.Stato = 'In consegna'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            inDeliveryShipments.Add(new Spedizione
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
                                DataConsegnaPrevista = reader.GetDateTime(9)
                            });
                        }
                    }
                }
            }
            return inDeliveryShipments;
        }

        private int GetPendingShipmentsCount()
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT COUNT(*) FROM Spedizioni
                    WHERE SpedizioneId NOT IN (
                        SELECT SpedizioneId FROM StatiSpedizioni
                        WHERE Stato = 'Consegnato')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    count = (int)cmd.ExecuteScalar();
                }
            }
            return count;
        }

        private Dictionary<string, int> GetShipmentsByCity()
        {
            var shipmentsByCity = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT CittàDestinazione, COUNT(*) AS NumeroSpedizioni
                    FROM Spedizioni
                    GROUP BY CittàDestinazione";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shipmentsByCity[reader.GetString(0)] = reader.GetInt32(1);
                        }
                    }
                }
            }
            return shipmentsByCity;
        }
    }
}
