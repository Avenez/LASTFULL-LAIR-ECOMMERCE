using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace U4_BW1_LL
{
    public partial class Login : System.Web.UI.Page
    {
        private object conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            alert.Visible = false;
            alertNot_foundOrSuccess.Visible = false;
            alertRegistrationSuccess.Visible = false;
            if (Request.Cookies["LOGIN_COOKIEUTENTE"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void EnterTheLair_Click(object sender, EventArgs e)
        {
            string script1 = "document.getElementById('imgTop').style.transition = 'opacity 2s ease'; " +
                "setTimeout(() => {document.getElementById('imgTop').style.opacity = 0;}, 200);";
            string script2 = "setTimeout(() => {document.getElementById('disclaimer').classList.add('fadeOut');}, 2000);" +
                "setTimeout(() => {document.getElementById('disclaimer').classList.add('d-none');}, 4000);";
            string script3 = "setTimeout(() => {document.getElementById('mainLoginContent').classList.add('fadeIn');" +
                "document.getElementById('mainLoginContent').classList.remove('d-none');}, 4000);";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "unlock", script1, true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "fadeDisclaimer", script2, true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "fadeInMain", script3, true);
        }

        protected void accediBtn_Click(object sender, EventArgs e)
        {
            bool userFounded = false;

            if (!string.IsNullOrEmpty(accediNome.Text) || !string.IsNullOrEmpty(accediPassword.Text))
            {
                // accedi al DB e verifica se l'utente esiste
                //stringa di connessione al DB
                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                try
                {

                    SqlCommand cmd = new SqlCommand("SELECT * FROM Utenti WHERE Username = @username AND Password = @password", conn);
                    cmd.Parameters.AddWithValue("@username", accediNome.Text);
                    cmd.Parameters.AddWithValue("@password", accediPassword.Text);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Se il reader ha delle righe, allora esiste un utente con quel nome e password
                        if (reader.HasRows)
                        {
                            userFounded = true;
                            // Utente trovato
                            while (reader.Read())
                            {
                                HttpCookie loginCookie = new HttpCookie("LOGIN_COOKIEUTENTE");
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    loginCookie.Values[reader.GetName(i)] = reader[i].ToString();
                                }
                                loginCookie.Expires = DateTime.Now.AddDays(10);
                                Response.Cookies.Add(loginCookie);
                            }

                            if (userFounded)
                            {
                                MakeFieldsEmpty();
                                LoginSuccessful();
                            }

                            // Response.Redirect("Default.aspx");
                        }
                        else if (!reader.HasRows)
                        {
                            // Utente non trovato
                            UtenteNonTrovato(accediNome.Text);
                            MakeFieldsEmpty();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Errore");
                    Response.Write(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                alert.Visible = true;
                InjectSetTimeout("alert");

            }

        }

        protected void BtnPerRegistrarti_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(accediNome.Text) && !string.IsNullOrEmpty(accediPassword.Text))
            {

                // inserisci il nuovo utente nel DB con admin false 
                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Utenti  (username , password , admin) VALUES (@username , @password , 0)", conn);
                    cmd.Parameters.AddWithValue("@username", accediNome.Text);
                    cmd.Parameters.AddWithValue("@password", accediPassword.Text);

                    cmd.ExecuteNonQuery();
                    alertRegistrationSuccess.Visible = true;
                    registrationSuccessTxt.InnerText = "Registrazione avvenuta con successo. Benvenuto nella tana del piacere!";
                    InjectSetTimeout("registrationSuccessTxt");
                    MakeFieldsEmpty();
                }
                catch (SqlException)
                {
                    alertRegistrationSuccess.Visible = true;
                    registrationSuccessTxt.InnerText = "Nickname già in uso.";
                    MakeFieldsEmpty();
                    InjectSetTimeout("registrationSuccessTxt");
                }
                catch (Exception ex)
                {
                    Response.Write("Errore");
                    Response.Write(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                alert.Visible = true;
                InjectSetTimeout("alert");
            }
        }

        protected void MakeFieldsEmpty()
        {
            accediNome.Text = "";
            accediPassword.Text = "";
        }

        protected void InjectSetTimeout(string IdDiv)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", $"setTimeout(function() {{ document.getElementById('{IdDiv}').style.display = 'none'; }}, 3000);", true);
        }

        protected void LoginSuccessful()
        {
            Response.Redirect("Default.aspx");
        }

        protected void UtenteNonTrovato(string nome)
        {
            alertNot_foundOrSuccess.Visible = true;
            notFound_OrSuccess.InnerText = $"Dati inseriti non corretti. Riprovare.";
            ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", "setTimeout(function() { document.getElementById('alertNot_foundOrSuccess').style.display = 'none'; }, 3000);", true);
        }
    }

}