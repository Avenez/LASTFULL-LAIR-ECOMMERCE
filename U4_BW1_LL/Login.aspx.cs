using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Web;

namespace U4_BW1_LL
{
    public partial class Login : System.Web.UI.Page
    {
        private object conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            alert.Visible = false;
            notFound_OrSuccess.Visible = false;
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
                                LoginSuccessful(accediNome.Text, accediPassword.Text);
                                Thread.Sleep(2000);
                                MakeFieldsEmpty();

                            }

                            Response.Redirect("Default.aspx");
                        }
                        else if (!reader.HasRows)
                        {
                            // Utente non trovato
                            UtenteNonTrovato(accediNome.Text);

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
                ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", "setTimeout(function() { document.getElementById('alert').style.display = 'none'; }, 3000);", true);

            }

        }


        protected void MakeFieldsEmpty()
        {
            accediNome.Text = "";
            accediPassword.Text = "";
        }

        protected void LoginSuccessful(string nome, string cognome)
        {
            notFound_OrSuccess.Visible = true;
            notFound_OrSuccess.InnerText = $"Bentornato Mr.{nome} {cognome}...";
            ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", "setTimeout(function() { document.getElementById('alertNot_foundOrSuccess').style.display = 'none'; }, 3000);", true);
        }

        protected void UtenteNonTrovato(string nome)
        {
            notFound_OrSuccess.Visible = true;
            notFound_OrSuccess.InnerText = $"Utente {nome} non trovato. Reinserisci Nome e Password Tesoro...";
            ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", "setTimeout(function() { document.getElementById('alertNot_foundOrSuccess').style.display = 'none'; }, 3000);", true);
        }

        protected void BtnPerRegistrarti_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(accediNome.Text) || !string.IsNullOrEmpty(accediPassword.Text))
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
            }
        }


        //protected void Login_Click(object sender, EventArgs e)
        //{
        //    Response.Write("Login cliccato");
        //    bool Admin = CheckAdmin(accediNome.Text, accediCognome.Text);


        //    if (Admin)
        //    {
        //        //Creo un Cookie di Login per verificare l'accesso dell'utente
        //        HttpCookie loginCookie = new HttpCookie("LOGIN_COOKIE");
        //        loginCookie.Values["userName"] = accediNome.Text;
        //        loginCookie.Values["password"] = accediCognome.Text;
        //        loginCookie.Values["admin"] = (Admin ? "true" : "false");

        //        loginCookie.Expires = DateTime.Now.AddDays(10);
        //        loginCookie.Secure = true;
        //        Response.Cookies.Add(loginCookie);


        //        // Se non trova un cookie relativo al carrelo lo crea
        //        /*
        //            if (Request.Cookies["CART_COOKIE"] == null)
        //            {
        //                //Se non esiste creo il cookie della cart al login

        //                // creo un arraylist e il cookie
        //                ArrayList cart = new ArrayList();
        //                HttpCookie cartCookie = new HttpCookie("CART_COOKIE");

        //                //creo un converter in json
        //                JavaScriptSerializer serializer = new JavaScriptSerializer();
        //                string cartJson = serializer.Serialize(cart);

        //                //aggiungo il json al cookie
        //                cartCookie.Values["cart"] = cartJson;
        //                Response.Cookies.Add(cartCookie);

        //            }
        //        */

        //        //passo alla prima pagina
        //        Response.Redirect("Default.aspx");
        //    }

        //}





        //protected bool CheckAdmin(string user, string password)
        //{
        //    bool admin = false;

        //    string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.Connection = conn;
        //            cmd.CommandText = "SELECT * FROM Utenti WHERE Username = @username AND Password = @password";
        //            cmd.Parameters.AddWithValue("@username", user);
        //            cmd.Parameters.AddWithValue("@password", password);

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {

        //                    int adminValue = Convert.ToInt16(reader["Admin"]);
        //                    admin = (adminValue == 1 ? true : false);
        //                    Response.Write("Utente Admin Trovato");
        //                    Response.Write(admin);

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log dell'errore e messaggio all'utente
        //        Response.Write("Errore durante l'autenticazione.");
        //        Response.Write(ex.Message);

        //    }

        //    finally
        //    {

        //        connectionString.Clone();
        //    }



        //    return admin;
        //}



        //protected void RegistrationButton_Click(object sender, EventArgs e)
        //{

        //    string username = TextBoxUsername.Text;
        //    string password = TextBoxPass.Text;
        //    string passwordControl = TextBoxPassControl.Text;


        //    if (CheckUsername(username))
        //    {

        //        if (password == passwordControl)
        //        {


        //            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDB"].ConnectionString.ToString();
        //            SqlConnection conn = new SqlConnection(connectionString);

        //            try
        //            {

        //                conn.Open();
        //                SqlCommand cmd = new SqlCommand();
        //                cmd.Connection = conn;
        //                cmd.CommandText = "INSERT INTO Utenti (Username, Password, Admin) VALUES ( @username , @password, 0)";
        //                cmd.Parameters.AddWithValue("@username", username);
        //                cmd.Parameters.AddWithValue("@password", password);

        //                cmd.ExecuteNonQuery();

        //                //RegistrationModalBody.InnerText = "Woo-hoo, Registrazione avvenuta!";
        //                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MostraModale", "$('#RegistrationModal').modal('show');", true);
        //                string messaggio = "Benvenuto!";
        //                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('" + messaggio + "');", true);


        //            }
        //            catch (Exception ex)
        //            {
        //                // Log dell'errore e messaggio all'utente
        //                Response.Write("Errore durante la registrazione.");
        //                Response.Write(ex.Message);

        //                // RegistrationModalBody.InnerText = "Ci dispiace ma il le password devono essere uguali!";
        //                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MostraModale", "$('#RegistrationModal').modal('show');", true);
        //                string messaggio = "NO!";
        //                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('" + messaggio + "');", true);

        //            }
        //            finally
        //            {
        //                conn.Close();

        //            }

        //        }

        //        else
        //        {
        //            //RegistrationModalBody.InnerText = "Arg, Registrazione fallita!";
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MostraModale", "$('#RegistrationModal').modal('show');", true);
        //            string messaggio = "No2!";
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('" + messaggio + "');", true);

        //        }




        //    }



        //}



        //protected bool CheckUsername(string user)
        //{

        //    bool validUsername = true;

        //    string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDB"].ConnectionString.ToString();

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.Connection = conn;
        //            cmd.CommandText = "SELECT * FROM Utenti WHERE Username = @username";
        //            cmd.Parameters.AddWithValue("@username", user);


        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {

        //                    validUsername = false;
        //                    conn.Close();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log dell'errore e messaggio all'utente
        //        Response.Write("Errore durante la ricerca del nome.");
        //        Response.Write(ex.Message);

        //    }

        //    return validUsername;

        //}
    }
}