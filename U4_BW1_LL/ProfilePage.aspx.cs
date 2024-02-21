using System;
using System.Configuration;
using System.Data.SqlClient;

namespace U4_BW1_LL
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            inputConfermaPassword.Visible = false;
            inputinserisciNomeEPassword.Visible = false;
            scegliCosaCambiare.Visible = false;

            if (Request.Cookies["LOGIN_COOKIEUTENTE"] != null)
            {
                string name = Request.Cookies["LOGIN_COOKIEUTENTE"]["Username"];
                string idutente = Request.Cookies["LOGIN_COOKIEUTENTE"]["IDUtente"];

                if (Request.Cookies["LOGIN_COOKIEUTENTE"]["Admin"] == "True")
                {
                    BtnBackOffice.Visible = true;
                }
                else
                {
                    BtnBackOffice.Visible = false;
                }

                TakeProfileImage(name, idutente);
            }


        }

        protected void TakeProfileImage(string name, string idutente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            string query;
            try
            {
                conn.Open();
                query = $"SELECT immagineprofilo from utenti where IDUtente = {idutente} ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = query;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        reader.GetString(0);
                        ImmagineProfilo.ImageUrl = reader.GetString(0);
                        nomeProfilo.InnerText = name;
                    }
                    else
                    {
                        ImmagineProfilo.ImageUrl = "https://www.w3schools.com/howto/img_avatar.png";
                    }
                }



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

        protected void BtnBackOffice_Click(object sender, EventArgs e)
        {
            Response.Redirect("Backoffice.aspx");
        }

        protected void SettingsClick(object sender, EventArgs e)
        {
            infoAlCaricamento.Visible = false;
            inputConfermaPassword.Visible = false;
            inputinserisciNomeEPassword.Visible = false;
            scegliCosaCambiare.Visible = true;
        }

        protected void Cambia_ImmagineProfilo(object sender, EventArgs e)
        {

        }
    }
}