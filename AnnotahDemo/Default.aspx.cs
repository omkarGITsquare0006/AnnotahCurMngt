using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        //Search button click to get po data in table
        protected void GetPoDetails(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            StringBuilder htmlTable = new StringBuilder();
            using (SqlConnection oConn = DBConnection.DBConnConfig.GetConnection())
            {
                SqlCommand oCmd = new SqlCommand("SELECT p.POHNUM_0,p.ITMREF_0,i.ITMDES1_0,i.ITMDES2_0 " +
                    "FROM[x3v11].[PILOT].[PORDER] as o " +
                    "LEFT JOIN[x3v11].[PILOT].[PORDERP] as p " +
                    "ON p.POHNUM_0 = o.POHNUM_0 " +
                    "LEFT JOIN[x3v11].[PILOT].[ITMMASTER] as i " +
                    "ON p.ITMREF_0 = i.ITMREF_0 " +
                    "WHERE p.POHNUM_0 = '"+po.Text+"'", oConn);
                da = new SqlDataAdapter(oCmd);
                da.Fill(ds);
                oCmd.ExecuteNonQuery();
                
                if (!object.Equals(ds.Tables[0], null))
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            htmlTable.Append("<tr>");
                            htmlTable.Append("<th scope='row'>" + ds.Tables[0].Rows[i][0] + "</th>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i][1] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i][2] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i][3] + "</td>");
                            htmlTable.Append("</tr>");
                        }
                        DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });
                    }
                    else
                    {
                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td align='center' colspan='4'>There are no items for order number<span class='font-weight-bold ml-1'>" + po.Text+"</span></td>");
                        htmlTable.Append("</tr>");
                        DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });
                    }
                }
            }
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