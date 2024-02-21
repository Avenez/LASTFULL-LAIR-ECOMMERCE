using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace U4_BW1_LL
{
    public partial class Carrello : System.Web.UI.Page
    {
        List<Product> products = new List<Product>();
        double totalPrice = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<int, int> cartMap = (Dictionary<int, int>)Session["cart"];
            

            if (Session["cart"] != null && cartMap.Keys.Count > 0)
            {
                foreach (int id in cartMap.Keys)
                {
                    Product prodotto = SelectProductById(id, cartMap[id]);
                    products.Add(prodotto);
                    totalPrice += prodotto.Prezzo;
                }

                cartRow.Attributes.Remove("style");
                CartRepeater.DataSource = products;
                CartRepeater.DataBind();
                totalPriceP.InnerText = "Prezzo totale: " + totalPrice + "€";
            }
            else
            {
                emptyCart.Attributes.Remove("style");
            }

            if (IsPostBack)
            {
                // Registra il pulsante "btnEsempio" per la convalida degli eventi
                RegisterPostBackControl();
            }



        }


        private void RegisterPostBackControl()
        {
            foreach (RepeaterItem item in CartRepeater.Items)
            {
                Button removeButton = (Button)item.FindControl("ButtonRemove");
                if (removeButton != null)
                {
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(removeButton);
                }
            }
        }

        protected Product SelectProductById(int id, int qta)
        {
            Product prodotto = null;

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string query = "SELECT * FROM Prodotti WHERE IDProdotto = '" + id + "'";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    prodotto = new Product(
                        Convert.ToInt16(rdr["IDProdotto"]),
                        rdr["Nome"].ToString(),
                        rdr["Descrizione"].ToString(),
                        rdr["ImgUrl"].ToString(),
                        Convert.ToDouble(rdr["Prezzo"]) * qta,
                        qta
                        );
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { conn.Close(); }

            return prodotto;
        }

        protected void btnSvuotaCarrello_Click(object sender, EventArgs e)
        {
            Session["cart"] = null;
            Response.Redirect("Carrello.aspx");
        }

        protected void ButtonRemove_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            // Creiamo una nuova lista per memorizzare gli elementi da rimuovere
            List<Product> itemsToRemove = new List<Product>();

            // Iteriamo sulla lista dei prodotti
            foreach (var item in products)
            {
                if (item.Id.ToString() == button.CommandArgument)
                {
                    // Aggiungiamo l'elemento da rimuovere alla lista apposita
                    itemsToRemove.Add(item);
                }
            }

            // Rimuoviamo gli elementi dalla lista dei prodotti
            foreach (var itemToRemove in itemsToRemove)
            {
                products.Remove(itemToRemove);
            }

            //---------------------------------------------------------------------

            Dictionary<int, int> cartMap = (Dictionary<int, int>)Session["cart"];

            int key = int.Parse(button.CommandArgument);

            cartMap.Remove(key);
            Session["cart"] = cartMap;
            Response.Redirect("Carrello");

        }

        protected void ButtonAcquista_Click(object sender, EventArgs e)
        {
            Dictionary<int, int> cartMap = (Dictionary<int, int>)Session["cart"];
            DateTime ordineDateTime = DateTime.Now;
            int IDCliente = int.Parse(Request.Cookies["LOGIN_COOKIEUTENTE"]["IDUtente"]);


            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            int idOrdine = CreateOrder(IDCliente, totalPrice, ordineDateTime);

            Response.Write(idOrdine);

            
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                

                foreach (var item in cartMap)
                {
                    double selectedPrice = 0;

                    foreach (var product in products)
                    {


                        if (item.Key == product.Id) { 
                        
                         selectedPrice = product.Prezzo;
                        }
                        
                    }

                    /*
                    cmd.CommandText = $"UPDATE Prodotti SET Qta = Qta - @Qta WHERE IDProdotto = @IDProdotto";
                    cmd.Parameters.Clear(); // Pulisci i parametri prima di aggiungerli di nuovo
                    cmd.Parameters.AddWithValue("@Qta", item.Value);
                    cmd.Parameters.AddWithValue("@IDProdotto", item.Key);
                    cmd.ExecuteNonQuery();
                    */
                    

                    cmd.Parameters.Clear(); // Pulisci i parametri prima di aggiungerli di nuovo
                    cmd.CommandText = $"INSERT INTO DettagliOrdine (IDOrdine, IDProdotto, Qta, PrezzoQta) VALUES (@IDOrdine, @IDProdotto, @Qta, @PrezzoQta)";
                    cmd.Parameters.AddWithValue("@IDOrdine", idOrdine);
                    cmd.Parameters.AddWithValue("@IDProdotto", item.Key);
                    cmd.Parameters.AddWithValue("@Qta", item.Value);
                    cmd.Parameters.AddWithValue("@PrezzoQta", selectedPrice);

                    cmd.ExecuteNonQuery();

                    

                }

                Response.Write("Acquisto avvenuto con successo");
                Session["cart"] = null;
                Response.Redirect("Carrello.aspx");


            }
            catch (Exception ex)
            {
                Response.Write("Errore ");
                Response.Write(ex);
            }
            finally
            {
                conn.Close();
            }
            

        }


        public int CreateOrder(int idUtente, double PrezzoTotale, DateTime DataOrdine)
        {
            int id = -1; 

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            
                
                    

                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("CreateOrder", conn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IDUtente", idUtente); // Correct parameter name
                        cmd.Parameters.AddWithValue("@PrezzoTotale", PrezzoTotale);
                        cmd.Parameters.AddWithValue("@DataOrdine", DataOrdine);

                        // Adding output parameter for IDOrdine
                        SqlParameter outputParameter = new SqlParameter();
                        outputParameter.ParameterName = "@IDOrdine";
                        outputParameter.SqlDbType = System.Data.SqlDbType.Int;
                        outputParameter.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(outputParameter);

                        cmd.ExecuteNonQuery();

                        // Retrieving the value of the output parameter
                        id = Convert.ToInt32(outputParameter.Value);
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex); // Use appropriate logging mechanism
                    }
                    finally 
                    { 
                    conn.Close();
                    }
                
            

            return id;
        }



        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            Dictionary<int, int> cartMap = (Dictionary<int, int>)Session["cart"];

            Button buttonChange = sender as Button;

            string commandArgument = buttonChange.CommandArgument;
            string[] parametri = commandArgument.Split('*');

            string command = parametri[0];
            int idProdotto = int.Parse(parametri[1]);

            for (int i = products.Count - 1; i >= 0; i--)
            {
                var item = products[i];
                if (item.Id == idProdotto)
                {
                    if (command == "Sum")
                    {
                        cartMap[idProdotto] += 1;
                        Response.Write("Feed plus");
                    }
                    else
                    {
                        cartMap[idProdotto] -= 1;
                        if (cartMap[idProdotto] == 0)
                        {
                            cartMap.Remove(idProdotto);
                        }
                        Response.Write("Feed minus");

                    }
                }
            }
            Session["cart"] = cartMap;
            Response.Redirect("Carrello");
        }
    }
}