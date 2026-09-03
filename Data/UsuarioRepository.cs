using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
using ReservasApp.Models;

namespace ReservasApp.Data
{
    public class UsuarioRepository
    {
        public Usuario ValidarLogin(string username, string password)
        {
            Usuario usuario = null;

            using (SqlConnection connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                string sql = @"
                    SELECT UsuarioId, Username, Password, NombreCompleto
                    FROM Usuarios
                    WHERE Username = @Username
                    AND Password = @Password";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                NombreCompleto = reader["NombreCompleto"].ToString()
                            };
                        }
                    }
                }
            }

            return usuario;
        }
    }
}