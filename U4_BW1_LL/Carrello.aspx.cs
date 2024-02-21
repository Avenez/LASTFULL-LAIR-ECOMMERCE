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

        
        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<int, int> cartMap = (Dictionary<int, int>)Session["cart"];
            if (Session["cart"] != null && cartMap.Keys.Count > 0)
            {
                foreach (int id in cartMap.Keys)
                {
                    Product prodotto = SelectProductById(id, cartMap[id]);
                    products.Add(prodotto);
                }

                cartRow.Attributes.Remove("style");
                CartRepeater.DataSource = products;
                CartRepeater.DataBind();
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

            Response.Redirect("Carrello");

        }

        protected void ButtonAcquista_Click(object sender, EventArgs e)
        {
            Dictionary<int, int> cartMap = (Dictionary<int, int>)Session["cart"];
            DateTime ordineDateTime = DateTime.Now;
            int IDCliente = int.Parse(Request.Cookies["LOGIN_COOKIEUTENTE"]["IDUtente"]);


            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            { 
            conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var item in cartMap)
                {
                    cmd.Parameters.Clear(); // Pulisci i parametri prima di aggiungerli di nuovo
                    cmd.CommandText = $"INSERT INTO Ordini (IDProdotto, IDCliente, Qta, DataOrdine) VALUES (@IDProd, @IDClient, @QtaProd, @DataOrdine)";
                    cmd.Parameters.AddWithValue("@IDProd", item.Key);
                    cmd.Parameters.AddWithValue("@IDClient", IDCliente);
                    cmd.Parameters.AddWithValue("@QtaProd", item.Value);
                    cmd.Parameters.AddWithValue("@DataOrdine", ordineDateTime);

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

            Response.Redirect("Carrello");
        }
    }
}