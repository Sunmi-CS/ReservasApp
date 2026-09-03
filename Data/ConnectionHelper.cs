using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;

namespace ReservasApp.Data
{
    public static class ConnectionHelper
    {
        private static readonly string connectionString =
            @"Server=DESKTOP-5RMNQ7V\SQLEXPRESS;
              Database=ReservasDB;
              Trusted_Connection=True;
              TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
