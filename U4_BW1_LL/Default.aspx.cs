using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace U4_BW1_LL
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["PageLoadedBefore"] = null;

            if (Request.Cookies["LOGIN_COOKIEUTENTE"] == null)
            {
                Response.Redirect("PreSite.aspx");
            }
            else
            {
                //Seleziona tutti i prodotti e li manda al repeater
                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Prodotti";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    ProductsRepeater.DataSource = dt;
                    ProductsRepeater.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally { conn.Close(); }
            }
        }
    }
}