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
            riepilogoOrdini.Visible = false;
            alertInserisciDati.Visible = false;
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

            if (Request.Cookies["LOGIN_COOKIEUTENTE"] == null)
            {
                Response.Redirect("PreSite.aspx");
            }

            nomeProfilo.Text = Request.Cookies["LOGIN_COOKIEUTENTE"]["Username"];
        }

        //Metodo che prende l'immagine di profilo dell'utente e la mostra in pagina
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
            infoAlCaricamento.Visible = true;
        }

        protected void showChangeImg(object sender, EventArgs e)
        {
            changePropic.Visible = false;
            divCambiaURL.Visible = true;

        }

        //Metodo che prende una nuova URL e la imposta come URL dell'immagine di profilo
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

        //Metodo che cambia username dell'utente
        protected void ChangeUsername(object sender, EventArgs e)
        {
            //Si richiama il metodo che fa i vari controlli sul nuovo username
            int resp = CheckExistingUsername(nomeProfilo.Text);

            //Se il numero che ritorna è 1 (quindi > 0), significa che ha superato i controlli e chiama la funziona che cambia username nel db
            if (resp > 0)
            {
                UpdateName();
            }
        }

        //Metodo per i vari controlli sul nuovo username che si vuole usare
        //Controlla in particolare:
        //se è uguale a quello precedente o se già esiste nel database un utente con quel nome, in questo caso ritorna -1
        //altrimenti ritorna 1
        protected int CheckExistingUsername(string username)
        {
            if (username == Request.Cookies["LOGIN_COOKIEUTENTE"]["Username"])
            {
                alertInserisciDati.Visible = true;
                feedbackalert.InnerText = "Stai già usando questo nickname.";
                InjectSetTimeout("MainContent_alertInserisciDati");
                return -1;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            string query;
            try
            {

                string IdUtente = Request.Cookies["LOGIN_COOKIEUTENTE"]["IDUtente"];

                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                query = $"SELECT username FROM Utenti";
                cmd.CommandText = query;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nomeEstrattoDB = reader["Username"].ToString();

                    if (username == nomeEstrattoDB)
                    {
                        alertInserisciDati.Visible = true;
                        feedbackalert.InnerText = "Nickname già in uso.";
                        InjectSetTimeout("MainContent_alertInserisciDati");
                        conn.Close();
                        return -1;
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
                if (conn != null)
                    conn.Close();
            }
            return 1;
        }

        //Metodo che cambia username nel DB
        protected void UpdateName()
        {
            string IdUtente = Request.Cookies["LOGIN_COOKIEUTENTE"]["IDUtente"];
            string nomeUtente = Request.Cookies["LOGIN_COOKIEUTENTE"]["username"];
            string newName = nomeProfilo.Text;

            // chiama il DB cambia il nome utente di chi ha fatto la richiesta e aggiorna anche il nome presente nel cookie 
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE Utenti SET username = @nuovoNome WHERE IDUtente = @IdUtente";

                cmd.Parameters.AddWithValue("@nuovoNome", newName);
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

                HttpCookie cookie = Request.Cookies["LOGIN_COOKIEUTENTE"];
                cookie["username"] = newName;
                cookie.Expires = DateTime.Now.AddDays(5);
                Response.Cookies.Add(cookie);

                alertInserisciDati.Visible = true;
                feedbackalert.InnerText = "Nickname modificato con successo.";
                InjectSetTimeout("MainContent_alertInserisciDati");

            }
        }

        //Metodo per iniettare codice JS. Questo specifico metodo serve a rendere invisibile un div passato come parametro dopo 3 secondi
        protected void InjectSetTimeout(string IdDiv)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", $"setTimeout(function() {{ document.getElementById('{IdDiv}').style.display = 'none'; }}, 3000);", true);
        }

        //Metodo per modificare password
        protected void ModificaPassword(object sender, EventArgs e)
        {
            //Controlla se la password una delle due password è nulla o vuota
            if (string.IsNullOrEmpty(insertPassword.Text) || string.IsNullOrEmpty(confirmPassword.Text))
            {
                alertErroreCambiaPassword.Visible = true;
                P1alertCambiaPassword.InnerText = "Riempi correttamente i campi.";
                InjectSetTimeout("MainContent_alertErroreCambiaPassword");
            }
            else
            {
                //Controlla se sono coincidono
                if (insertPassword.Text == confirmPassword.Text)
                {
                    //Controlla se è uguale a quella già in uso
                    if (insertPassword.Text == Request.Cookies["LOGIN_COOKIEUTENTE"]["Password"])
                    {
                        alertErroreCambiaPassword.Visible = true;
                        P1alertCambiaPassword.InnerText = "Prova con una password diversa.";
                        InjectSetTimeout("MainContent_alertErroreCambiaPassword");
                    }
                    else
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

                            HttpCookie cookie = Request.Cookies["LOGIN_COOKIEUTENTE"];
                            cookie["Password"] = confirmPassword.Text;
                            cookie.Expires = DateTime.Now.AddDays(5);
                            Response.Cookies.Add(cookie);

                            alertErroreCambiaPassword.Visible = true;
                            P1alertCambiaPassword.InnerText = "Password modificata con successo.";
                            InjectSetTimeout("MainContent_alertErroreCambiaPassword");
                        }
                    }
                }
                else
                {
                    // password non coincidono
                    alertErroreCambiaPassword.Visible = true;
                    P1alertCambiaPassword.InnerText = "Le password non coincidono.";
                    InjectSetTimeout("MainContent_alertErroreCambiaPassword");
                }
            }
        }



        //Metodo che mostra tutti gli ordini e i relativi dettagli associati all'id utente
        protected void ShowOrders(object sender, EventArgs e)
        {
            infoAlCaricamento.Visible = false;
            riepilogoOrdini.Visible = true;

            //Creo una lista di ordini a partire da quella che ritorna la funzione SelectOrdersById
            List<Order> orders = SelectOrdersById();

            if (orders.Count > 0)
            {
                //Se la lista contiene elementi scorre uno ad uno gli ordini e per ognuno cerca i dettagliOrdine associati
                foreach (Order order in orders)
                {
                    List<OrderDetails> orderDetails = new List<OrderDetails>();
                    //Trovo tutti i dettagli ordine associati all'ordine corrente
                    orderDetails = SelectOrderDetails(order.Id);

                    //Per ogni dettaglioOrdine cerco il prodotto associato per prenderne le informazioni come immagine e nome
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
                    //Uso SetOrderDetails per passare la lista di orderDetails all'ordine corrente
                    order.SetOrderDetails(orderDetails);
                }
                //Finiti i due foreach avrò una lista di ordini dove ognuno conterrà come proprietà una lista di dettagliOrdine associata
                //E la mando al repeater esterno
                OrderRepeater.DataSource = orders;
                OrderRepeater.DataBind();
            }
            else
            {
                noOrder.Attributes.Remove("style");
            }
        }


        //Metodo che ritorna la lista di tutti gli ordini associati all'id utente
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

        //Metodo che ritorna la lista di dettagli ordine di un ordine tramite il suo id
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

        //Metodo che trova l'ordine corrente nel repeater esterno e vi associa la sua stessa lista di dettagliOrdine
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