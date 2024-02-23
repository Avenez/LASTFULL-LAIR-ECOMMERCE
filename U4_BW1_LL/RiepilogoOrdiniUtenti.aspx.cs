using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace U4_BW1_LL
{
    public partial class RiepilogoOrdiniUtenti : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["LOGIN_COOKIEUTENTE"] == null)
            {
                Response.Redirect("PreSite.aspx");
            }
            if (Request.Cookies["LOGIN_COOKIEUTENTE"]["Admin"] != "True")
            {
                Response.Redirect("Default.aspx");
            }

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                string query;
                conn.Open();
                query = "SELECT U.idUtente, U.username, O.idordine , PrezzoTotale , DataOrdine FROM utenti AS U INNER JOIN ordini AS O ON U.idUtente = O.idutente";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                repeaterRiepilogo.DataSource = dt;
                repeaterRiepilogo.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("Errore ");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void tornaIndietro_Click(object sender, EventArgs e)
        {
            Response.Redirect("BackOffice.aspx");
        }
    }
}