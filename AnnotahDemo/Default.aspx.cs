using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;

namespace AnnotahDemo
{
    public class clsRow
    {
        public string POHNUM_0 { get; set; }
        public DateTime ORDDAT_0 { get; set; }
        public string POHFCY_0 { get; set; }
    }
    public class clsTable
    {
        public clsTable()
        {
            Rows = new List<clsRow>();
        }
        public List<clsRow> Rows { get; set; }
    }

    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod()]
        public static clsTable GetData_Modal()
        {
            clsTable oTable = new clsTable();
            using (SqlConnection oConn = DBConnection.DBConnConfig.GetConnection())
            {
                //oConn.Open();
                SqlCommand oCmd = new SqlCommand("SELECT * FROM PILOT.PORDER", oConn);
                SqlDataReader oReader = oCmd.ExecuteReader();
                while (oReader.Read() == true)
                {
                    clsRow oRow = new clsRow();
                    oRow.POHNUM_0 = oReader["POHNUM_0"].ToString();
                    oRow.ORDDAT_0 = Convert.ToDateTime(oReader["ORDDAT_0"]);
                    oRow.POHFCY_0 = oReader["POHFCY_0"].ToString();
                    oTable.Rows.Add(oRow);
                }
                oReader.Close();
            }
            return oTable;
        }
    }
}