using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PraticaSettimanaleS5.Models;
using System;

namespace PraticaSettimanaleS5.Services
{
    public class AuthService : IAuthService
    {
        private string _connectionString;

        private const string LOGIN_COMMAND = "SELECT Id, FriendlyName FROM Users WHERE Username = @user AND Password = @pass";

        public AuthService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("EShipping")!;
        }

        public ApplicationUser Login(string username, string password)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();
                using var cmd = new SqlCommand(LOGIN_COMMAND, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    return new ApplicationUser
                    {
                        Id = r.GetInt32(0),
                        Password = password,
                        Username = username,
                        FriendlyName = r.GetString(1)
                    };
                }
            }
            catch (Exception ex)
            {
                // Gestisci l'eccezione (log, ecc.)
            }
            return null;
        }
    }
}
