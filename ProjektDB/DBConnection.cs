using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDB
{
    class DBConnection
    {

        public SqlConnection connection { get; set; }

        public static string connectionstring { get; set; }




        public DBConnection()
        {
            connection = new SqlConnection();

        }

        public bool ConnectStr()
        {

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.ConnectionString = connectionstring;
                connection.Open();
            }

            return true;
        }

        public bool Connect()
        {

            bool ret = true;

            if (connection.State != System.Data.ConnectionState.Open)
            {
                // connection string is stored in file App.config or Web.config
                ret = ConnectStr();
            }
            return ret;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
