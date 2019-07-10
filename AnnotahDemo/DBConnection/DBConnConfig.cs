using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AnnotahDemo.DBConnection
{
    public class DBConnConfig
    {
        public static SqlConnection GetConnection()
        {
            try
            {
                string strConnection = ConfigurationManager.ConnectionStrings["ANNDEMOConnection"].ConnectionString;
                SqlConnection conn = new SqlConnection(strConnection);
                conn.Open();
                return conn;
            }
            catch (SqlException e)
            {
                Console.WriteLine("SQL Error is: " + e);
                return null;
            }

        }
    }
}