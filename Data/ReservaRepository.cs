using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using ReservasApp.Models;

namespace ReservasApp.Data
{
    public class ReservaData
    {
        // =====================================================
        // RESERVAS - DATATABLE - DESCONECTADO
        // =====================================================

        public DataTable ListarReservasDataTable()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection connection = ConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT
                        r.ReservaId,
                        a.Nombre AS Aula,
                        u.NombreCompleto AS Usuario,
                        r.Fecha,
                        r.Hora,
                        r.Motivo
                    FROM Reservas r
                    INNER JOIN Aulas a
                        ON r.AulaId = a.AulaId
                    INNER JOIN Usuarios u
                        ON r.UsuarioId = u.UsuarioId
                    ORDER BY r.Fecha, r.Hora";

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(sql, connection))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }


        // =====================================================
        // RESERVAS - OBJETOS - CONECTADO
        // =====================================================

        public List<Reserva> ListarReservasObjetos()
        {
            List<Reserva> lista = new List<Reserva>();

            using (SqlConnection connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                string sql = @"
                    SELECT
                        r.ReservaId,
                        r.AulaId,
                        r.UsuarioId,
                        r.Fecha,
                        r.Hora,
                        r.Motivo,
                        a.Nombre AS NombreAula,
                        u.NombreCompleto AS NombreUsuario
                    FROM Reservas r
                    INNER JOIN Aulas a
                        ON r.AulaId = a.AulaId
                    INNER JOIN Usuarios u
                        ON r.UsuarioId = u.UsuarioId
                    ORDER BY r.Fecha, r.Hora";

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reserva reserva = new Reserva
                            {
                                ReservaId = Convert.ToInt32(reader["ReservaId"]),
                                AulaId = Convert.ToInt32(reader["AulaId"]),
                                UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                                Fecha = Convert.ToDateTime(reader["Fecha"]),
                                Hora = (TimeSpan)reader["Hora"],
                                Motivo = reader["Motivo"].ToString(),
                                NombreAula = reader["NombreAula"].ToString(),
                                NombreUsuario = reader["NombreUsuario"].ToString()
                            };

                            lista.Add(reserva);
                        }
                    }
                }
            }

            return lista;
        }


        // =====================================================
        // BUSCAR RESERVAS POR FECHA - CONECTADO
        // =====================================================

        public List<Reserva> BuscarReservasPorFecha(DateTime fecha)
        {
            List<Reserva> lista = new List<Reserva>();

            using (SqlConnection connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                string sql = @"
                    SELECT
                        r.ReservaId,
                        r.AulaId,
                        r.UsuarioId,
                        r.Fecha,
                        r.Hora,
                        r.Motivo,
                        a.Nombre AS NombreAula,
                        u.NombreCompleto AS NombreUsuario
                    FROM Reservas r
                    INNER JOIN Aulas a
                        ON r.AulaId = a.AulaId
                    INNER JOIN Usuarios u
                        ON r.UsuarioId = u.UsuarioId
                    WHERE r.Fecha = @Fecha
                    ORDER BY r.Hora";

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Fecha", fecha.Date);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reserva reserva = new Reserva
                            {
                                ReservaId = Convert.ToInt32(reader["ReservaId"]),
                                AulaId = Convert.ToInt32(reader["AulaId"]),
                                UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                                Fecha = Convert.ToDateTime(reader["Fecha"]),
                                Hora = (TimeSpan)reader["Hora"],
                                Motivo = reader["Motivo"].ToString(),
                                NombreAula = reader["NombreAula"].ToString(),
                                NombreUsuario = reader["NombreUsuario"].ToString()
                            };

                            lista.Add(reserva);
                        }
                    }
                }
            }

            return lista;
        }


        // =====================================================
        // VERIFICAR SI YA EXISTE UNA RESERVA
        // =====================================================

        public bool ExisteReserva(int aulaId, DateTime fecha, TimeSpan hora)
        {
            using (SqlConnection connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                string sql = @"
                    SELECT COUNT(*)
                    FROM Reservas
                    WHERE AulaId = @AulaId
                    AND Fecha = @Fecha
                    AND Hora = @Hora";

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@AulaId", aulaId);
                    command.Parameters.AddWithValue("@Fecha", fecha.Date);
                    command.Parameters.AddWithValue("@Hora", hora);

                    int cantidad = Convert.ToInt32(command.ExecuteScalar());

                    return cantidad > 0;
                }
            }
        }


        // =====================================================
        // INSERTAR NUEVA RESERVA
        // =====================================================

        public bool InsertarReserva(
            int aulaId,
            int usuarioId,
            DateTime fecha,
            TimeSpan hora,
            string motivo)
        {
            using (SqlConnection connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                string sql = @"
                    INSERT INTO Reservas
                    (
                        AulaId,
                        UsuarioId,
                        Fecha,
                        Hora,
                        Motivo
                    )
                    VALUES
                    (
                        @AulaId,
                        @UsuarioId,
                        @Fecha,
                        @Hora,
                        @Motivo
                    )";

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@AulaId", aulaId);
                    command.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    command.Parameters.AddWithValue("@Fecha", fecha.Date);
                    command.Parameters.AddWithValue("@Hora", hora);
                    command.Parameters.AddWithValue("@Motivo", motivo);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}