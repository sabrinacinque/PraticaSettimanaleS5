using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PraticaSettimanaleS5.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace PraticaSettimanaleS5.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IConfiguration _configuration;

        public ShipmentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<StatoSpedizione> GetShipmentUpdates(string idNumber, string trackingNumber)
        {
            List<StatoSpedizione> updates = new List<StatoSpedizione>();
            string connectionString = _configuration.GetConnectionString("EShipping");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT ss.DataAggiornamento, ss.Stato, ss.Luogo, ss.Descrizione
                    FROM StatiSpedizioni ss
                    JOIN Spedizioni s ON ss.SpedizioneId = s.SpedizioneId
                    JOIN Clienti c ON s.ClienteId = c.ClienteId
                    WHERE (c.CodiceFiscale = @idNumber OR c.PartitaIVA = @idNumber)
                    AND s.NumeroTracking = @trackingNumber
                    ORDER BY ss.DataAggiornamento DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@idNumber", SqlDbType.NVarChar) { Value = idNumber });
                    command.Parameters.Add(new SqlParameter("@trackingNumber", SqlDbType.NVarChar) { Value = trackingNumber });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            updates.Add(new StatoSpedizione
                            {
                                DataAggiornamento = reader.GetDateTime(0),
                                Stato = reader.GetString(1),
                                Luogo = reader.GetString(2),
                                Descrizione = reader.IsDBNull(3) ? null : reader.GetString(3)
                            });
                        }
                    }
                }
            }

            return updates;
        }
    }
}
