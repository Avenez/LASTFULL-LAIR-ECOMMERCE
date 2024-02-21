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

    }
}