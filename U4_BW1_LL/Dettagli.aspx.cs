using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace U4_BW1_LL
{
    public partial class Dettagli : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            noStock.Visible = false;
            sectionalertAddTocart.Visible = false;

            if (Request.QueryString["IDProdotto"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if (Request.Cookies["LOGIN_COOKIEUTENTE"] == null)
            {
                Response.Redirect("PreSite.aspx");
            }

            if (!IsPostBack)
            {
                // Recupera l'ID del prodotto dalla query string
                if (Request.QueryString["IDProdotto"] != null)
                {
                    int IDProdotto;
                    if (int.TryParse(Request.QueryString["IDProdotto"], out IDProdotto))
                    {
                        // Carica e visualizza i dettagli del prodotto
                        CaricaDettagliProdotto(IDProdotto);
                    }
                }

                // Controlla se la pagina è stata caricata almeno una volta
                if (Session["PageLoadedBefore"] != null && (bool)Session["PageLoadedBefore"] == true)
                {
                    InjectSetTimeout("MainContent_sectionalertAddTocart");
                }
                else
                {
                    // Imposta il flag nella sessione per indicare che la pagina è stata caricata
                    Session["PageLoadedBefore"] = true;
                }
            }
        }


        //Metodo che cerca il prodotto sul database tramite l'id e stampa a schermo i dettagli
        private void CaricaDettagliProdotto(int IDProdotto)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "SELECT * FROM Prodotti WHERE IDProdotto = '" + IDProdotto + "'";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                {
                    if (reader.Read())
                    {
                        lblNomeProdotto.Text = reader["Nome"].ToString();
                        lblPrezzoProdotto.Text = string.Format("{0:C}", reader["Prezzo"]);
                        lblDescrizioneProdotto.InnerText = reader["Descrizione"].ToString();
                        imgProdotto.ImageUrl = reader["ImgUrl"].ToString();
                        selectedQuantity.Attributes["max"] = reader["Qta"].ToString();

                        if (reader["Qta"].ToString() == "0")
                        {
                            noStock.Visible = true;
                            dettagliAquisto.Visible = false;
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { conn.Close(); }
        }

        protected void btnTornaIndietro_Click(object sender, EventArgs e)
        {
            // Reindirizza alla pagina principale
            Response.Redirect("Default.aspx");
        }

        //Metodo che aggiunge il prodotto al carrello con la quantità richiesta
        protected void AddToCart(object sender, EventArgs e)
        {
            int idProduct = int.Parse(Request.QueryString["IDProdotto"]);
            int quantity = int.Parse(selectedQuantity.Text);

            //Se la quantità è 0 non accade nulla
            if (quantity > 0)
            {
                //Se la sessione non esiste, ne crea una
                if (Session["cart"] == null)
                {
                    Session["cart"] = new Dictionary<int, int>();
                }

                //Usiamo il dictionary che ci permette di mappare idProdotto e quantità
                Dictionary<int, int> cartMap = (Dictionary<int, int>)Session["cart"];

                //Se il prodotto non è presente nel dictionary, lo aggiunge, altrimenti aggiorna la quantità
                if (!cartMap.Keys.Contains(idProduct))
                {
                    cartMap.Add(idProduct, quantity);
                }
                else if (cartMap.Keys.Count > 0 && cartMap.Keys.Contains(idProduct))
                {
                    cartMap[idProduct] += quantity;
                }

                Session["cart"] = cartMap;
                //InjectSetTimeout("MainContent_sectionalertAddTocart");
                Response.Redirect($"Dettagli.aspx?IDProdotto={idProduct}");


            }
        }

        protected void InjectSetTimeout(string IdDiv)
        {
            sectionalertAddTocart.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", $"setTimeout(function() {{ document.getElementById('{IdDiv}').style.display = 'none'; }}, 2000);", true);
        }
    }
}
