using System;
using System.Configuration;
using System.Data.SqlClient;

namespace U4_BW1_LL
{
    public partial class ProfilePage : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            messaggio_Errore.Visible = false;
            sceglicosaCambiare.Visible = false;
            divCambiaURL.Visible = false;
            buttonApriModale.Visible = false;
            divInsertNomePassword.Visible = false;
            alertInserisciDati.Visible = false;
            divFinaleCambioNome.Visible = false;

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
                query = $"SELECT ImmagineProfilo FROM Utenti WHERE IDUtente = {idutente} ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = query;
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ImmagineProfilo.ImageUrl = reader.GetString(0);
                    nomeProfilo.InnerText = name;
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
            sceglicosaCambiare.Visible = true;
        }

        protected void Cambia_ImmagineProfilo(object sender, EventArgs e)
        {
            divCambiaURL.Visible = true;
            buttonApriModale.Visible = true;

            string IdUtente = Request.Cookies["LOGIN_COOKIEUTENTE"]["IDUtente"];

            if (string.IsNullOrEmpty(TextBoxURLImmagine.Text) || !TextBoxURLImmagine.Text.StartsWith("https://"))
            {
                messaggio_Errore.Visible = true;
                urlNonValido.InnerText = "Inserisci un URL valido";
                InjectSetTimeout("MainContent_messaggio_Errore");
            }
            else
            {
                string nuovoURL = TextBoxURLImmagine.Text;

                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                string query;
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    query = $"UPDATE Utenti SET Immagineprofilo = '{nuovoURL}' WHERE IDUtente = '{IdUtente}'";

                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    Response.Redirect("ProfilePage.aspx");

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
        }

        protected void makeDivChangeImgVisible_Click(object sender, EventArgs e)
        {
            infoAlCaricamento.Visible = true;
            divCambiaURL.Visible = true;
            buttonApriModale.Visible = true;
        }

        protected void btnconfermaNomePassword_Click(object sender, EventArgs e)
        {
            divInsertNomePassword.Visible = true;

            if (string.IsNullOrEmpty(textBoxVecchioNomeUtente.Text) || string.IsNullOrEmpty(textBoxPassword.Text))
            {

                alertInserisciDati.Visible = true;
                feedbackalert.InnerText = "inserisci un nome e una password valide.";
                InjectSetTimeout("MainContent_alertInserisciDati");
            }

            else
            {
                string vecchioNomeUtente = textBoxVecchioNomeUtente.Text;
                string password = textBoxPassword.Text;

                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                string query;
                try
                {

                    string IdUtente = Request.Cookies["LOGIN_COOKIEUTENTE"]["IDUtente"];

                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    query = $"SELECT username , password FROM Utenti WHERE IDUtente = '{IdUtente}' ";
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string nomeEstrattoDB = reader.GetString(0);
                        string passwordEstrattaDB = reader.GetString(1);

                        if (vecchioNomeUtente == nomeEstrattoDB && password == passwordEstrattaDB)
                        {
                            divInsertNomePassword.Visible = false;
                            divFinaleCambioNome.Visible = true;

                        }
                        else if (vecchioNomeUtente != nomeEstrattoDB || password != passwordEstrattaDB)
                        {
                            alertInserisciDati.Visible = true;
                            feedbackalert.InnerText = "nome o password non coincidono. Riprova";
                            InjectSetTimeout("MainContent_alertInserisciDati");
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

        }

        protected void FinalNameChange_click(object sender, EventArgs e)
        {
            divFinaleCambioNome.Visible = true;
            string nuovoNome = TxtNuovoNome.Text;


            if (string.IsNullOrEmpty(nuovoNome))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "showAlert", $"window.alert('inserisci un nuovo nome utente');", true);
            }
            else
            {
                Response.Write("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
        }

        protected void makeDivChangeNomeVisible(object sender, EventArgs e)
        {
            divInsertNomePassword.Visible = true;
        }
        protected void InjectSetTimeout(string IdDiv)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", $"setTimeout(function() {{ document.getElementById('{IdDiv}').style.display = 'none'; }}, 3000);", true);
        }


    }
}