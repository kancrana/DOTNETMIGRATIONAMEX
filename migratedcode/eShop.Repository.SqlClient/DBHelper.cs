using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace eShop.Repository.SqlClient
{
    public class DBHelper
    {
        public static string GetConnection(DB db)
        {
            string connectionString = string.Empty;
            switch (db)
            {
                case DB.Product:
                    connectionString = ConfigurationManager.ConnectionStrings["ProductCatalog"].ToString();
                    break;
            }
            return connectionString;
        }
    }

    public enum DB
    {
        Product,
        Buyer,
        Orders,
        Payments
    }
}
