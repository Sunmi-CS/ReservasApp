using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using ReservasApp.Models;

namespace ReservasApp.Data
{
    public class AulaData
    {
        // =====================================================
        // AULAS - DATATABLE - DESCONECTADO
        // =====================================================

        public DataTable ListarAulasDataTable()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection connection = ConnectionHelper.GetConnection())
            {
                string sql = @"
                    SELECT AulaId, Nombre, Capacidad
                    FROM Aulas
                    ORDER BY AulaId";

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(sql, connection))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }


        // =====================================================
        // AULAS - OBJETOS - CONECTADO
        // =====================================================

        public List<Aula> ListarAulasObjetos()
        {
            List<Aula> lista = new List<Aula>();

            using (SqlConnection connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                string sql = @"
                    SELECT AulaId, Nombre, Capacidad
                    FROM Aulas
                    ORDER BY AulaId";

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Aula aula = new Aula
                            {
                                AulaId = Convert.ToInt32(reader["AulaId"]),
                                Nombre = reader["Nombre"].ToString(),
                                Capacidad = Convert.ToInt32(reader["Capacidad"])
                            };

                            lista.Add(aula);
                        }
                    }
                }
            }

            return lista;
        }


        // =====================================================
        // BUSCAR AULA - CONECTADO
        // =====================================================

        public List<Aula> BuscarAulas(string nombre)
        {
            List<Aula> lista = new List<Aula>();

            using (SqlConnection connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                string sql = @"
                    SELECT AulaId, Nombre, Capacidad
                    FROM Aulas
                    WHERE Nombre LIKE @Nombre
                    ORDER BY Nombre";

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Nombre", "%" + nombre + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Aula aula = new Aula
                            {
                                AulaId = Convert.ToInt32(reader["AulaId"]),
                                Nombre = reader["Nombre"].ToString(),
                                Capacidad = Convert.ToInt32(reader["Capacidad"])
                            };

                            lista.Add(aula);
                        }
                    }
                }
            }

            return lista;
        }
    }
}