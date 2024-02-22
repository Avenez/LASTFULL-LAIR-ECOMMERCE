using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace U4_BW1_LL
{
    public partial class ProfilePage : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            messaggio_Errore.Visible = false;


            divCambiaURL.Visible = false;
            changePropic.Visible = true;

            divInsertNomePassword.Visible = false;
            alertInserisciDati.Visible = false;
            divFinaleCambioNome.Visible = false;
            riepilogoOrdini.Visible = false;
            divCambiaPassword.Visible = false;
            alertErroreCambiaPassword.Visible = false;

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
            changeName.Visible = true;
            infoAlCaricamento.Visible = true;
            changePassword.Visible = true;
        }

        protected void showChangeImg(object sender, EventArgs e)
        {
            changePropic.Visible = false;
            divCambiaURL.Visible = true;

        }

        protected void Cambia_ImmagineProfilo(object sender, EventArgs e)
        {
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


        protected void btnconfermaNomePassword_Click(object sender, EventArgs e)
        {
            divInsertNomePassword.Visible = true;
            changeName.Visible = false;


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

            string IdUtente = Request.Cookies["LOGIN_COOKIEUTENTE"]["IDUtente"];
            string nomeUtente = Request.Cookies["LOGIN_COOKIEUTENTE"]["username"];

            if (string.IsNullOrEmpty(nuovoNome))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "showAlert", $"window.alert(' Hai lasciato il campo vuoto. Inserisci un nuovo nome utente');", true);
            }
            else if (nuovoNome != nomeUtente)
            {   // chiama il DB cambia il nome utente di chi ha fatto la richiesta e aggiorna anche il nome presente nel cookie 
                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                try
                {

                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $"UPDATE Utenti SET username = @nuovoNome WHERE IDUtente = @IdUtente";

                    cmd.Parameters.AddWithValue("@nuovoNome", nuovoNome);
                    cmd.Parameters.AddWithValue("@IdUtente", IdUtente);

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Response.Write("Errore ");
                    Response.Write(ex.Message);
                }
                finally
                {
                    conn.Close();

                    HttpCookie cookie = new HttpCookie("LOGIN_COOKIEUTENTE");
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);

                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "showAlert", $"window.alert(' Hai inserito lo stesso nome utente. Scegline uno diverso.');", true);

            }
        }

        protected void InjectSetTimeout(string IdDiv)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", $"setTimeout(function() {{ document.getElementById('{IdDiv}').style.display = 'none'; }}, 3000);", true);
        }

        protected void cambiaPassword_Click(object sender, EventArgs e)
        {
            changePassword.Visible = false;
            divCambiaPassword.Visible = true;

        }

        protected void ModificaPassword(object sender, EventArgs e)
        {
            divCambiaPassword.Visible = true;

            if (string.IsNullOrEmpty(insertPassword.Text) || string.IsNullOrEmpty(confirmPassword.Text))
            {
                alertErroreCambiaPassword.Visible = true;
                P1alertCambiaPassword.InnerText = " Riempi correttamente i campi.";
                InjectSetTimeout("MainContent_P1alertCambiaPassword");
            }
            else
            {
                if (insertPassword.Text == confirmPassword.Text)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                    SqlConnection conn = new SqlConnection(connectionString);

                    try
                    {
                        string idUtente = Request.Cookies["LOGIN_COOKIEUTENTE"]["IDUtente"];

                        conn.Open();

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = $"UPDATE Utenti SET password = @nuovaPassword WHERE IDUtente = @IdUtente";

                        cmd.Parameters.AddWithValue("@nuovaPassword", confirmPassword.Text);
                        cmd.Parameters.AddWithValue("@IdUtente", idUtente);

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Errore ");
                        Response.Write(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                        HttpCookie cookie = new HttpCookie("LOGIN_COOKIEUTENTE");
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(cookie);

                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    // password non coincidono
                    alertErroreCambiaPassword.Visible = true;
                    P1alertCambiaPassword.InnerText = "Le password non coincidono.";
                    InjectSetTimeout("MainContent_P1alertCambiaPassword");
                }
            }
        }











        protected void ShowOrders(object sender, EventArgs e)
        {
            infoAlCaricamento.Visible = false;
            riepilogoOrdini.Visible = true;
            List<Order> orders = SelectOrdersById();

            if (orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    List<OrderDetails> orderDetails = new List<OrderDetails>();
                    orderDetails = SelectOrderDetails(order.Id);

                    foreach (OrderDetails orderDetail in orderDetails)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                        SqlConnection conn = new SqlConnection(connectionString);

                        try
                        {
                            conn.Open();
                            string query = "SELECT * FROM Prodotti WHERE IDProdotto = '" + orderDetail.Id + "'";

                            SqlCommand cmd = new SqlCommand(query, conn);

                            SqlDataReader reader = cmd.ExecuteReader();

                            if (reader.Read())
                            {
                                orderDetail.ImgUrl = reader["ImgUrl"].ToString();
                                orderDetail.Name = reader["Nome"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                        finally { conn.Close(); }
                    }

                    order.SetOrderDetails(orderDetails);
                }

                OrderRepeater.DataSource = orders;
                OrderRepeater.DataBind();
            }
            else
            {
                noOrder.Attributes.Remove("style");
            }
        }



        protected List<Order> SelectOrdersById()
        {
            List<Order> orders = new List<Order>();
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string query = "SELECT * FROM Ordini WHERE IDUtente = '" + Request.Cookies["LOGIN_COOKIEUTENTE"]["IDUtente"] + "'";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Order order = new Order(Convert.ToInt16(reader["IDOrdine"]), Convert.ToDouble(reader["PrezzoTotale"]), Convert.ToDateTime(reader["DataOrdine"]));
                    orders.Add(order);
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { conn.Close(); }

            return orders;
        }

        protected List<OrderDetails> SelectOrderDetails(int orderId)
        {
            List<OrderDetails> orders = new List<OrderDetails>();
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string query = "SELECT * FROM DettagliOrdine WHERE IDOrdine = '" + orderId + "'";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrderDetails order = new OrderDetails(Convert.ToInt16(reader["IDProdotto"]), Convert.ToInt16(reader["Qta"]), Convert.ToDouble(reader["PrezzoQta"]));
                    orders.Add(order);
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { conn.Close(); }

            return orders;
        }

        protected void rptOrdini_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Controlla se l'elemento è un elemento dati
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Ottieni l'ordine corrente dall'elemento dati
                Order order = (Order)e.Item.DataItem;

                // Trova il repeater interno per i dettagli dell'ordine
                Repeater orderDetailsRepeater = (Repeater)e.Item.FindControl("OrderDetailsRepeater");

                // Associa la lista di dettagli dell'ordine al repeater interno
                orderDetailsRepeater.DataSource = order.GetOrderDetails();
                orderDetailsRepeater.DataBind();
            }
        }


    }
}