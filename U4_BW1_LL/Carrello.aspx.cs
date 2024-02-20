using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

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
    }
}