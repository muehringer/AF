using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using InventoryManagementSystem.Infrastructure.Enums;

namespace InventoryManagementSystem.Infrastructure.Data.Connections
{
    public class ConnectionDb : IConnectionDb
    {
        public static IConfigurationRoot configuration { get; set; }
        public IDbConnection IConn { get; private set; }

        public ConnectionDb()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");

            configuration = builder.Build();
        }

        private IDbConnection SelectConnection()
        {
            var typeDb = (DataBaseType)Enum.Parse(typeof(DataBaseType), configuration["TypeDb"], true);

            switch (typeDb)
            {
                case DataBaseType.MySql:
                    return new MySqlConnection(configuration["ConnectionDb"]);
                default:
                    return new MySqlConnection(configuration["ConnectionDb"]);
            }
        }

        public IDbConnection OpenConnection()
        {
            IConn = SelectConnection();

            if (IConn != null && IConn.State != ConnectionState.Open)
                IConn.Open();

            return IConn;
        }

        public void CloseConnection()
        {
            if (IConn != null && IConn.State == ConnectionState.Open)
            {
                IConn.Close();
                IConn.Dispose();
            }
        }

        public void Dispose()
            => CloseConnection();
    }
}
