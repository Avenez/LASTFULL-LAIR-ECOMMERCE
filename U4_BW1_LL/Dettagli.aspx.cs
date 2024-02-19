using System;
using System.Configuration;
using System.Data.SqlClient;

namespace U4_BW1_LL
{
    public partial class Dettagli : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            }
        }

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
                        lblDescrizioneProdotto.Text = reader["Descrizione"].ToString();
                        imgProdotto.ImageUrl = reader["ImgUrl"].ToString();
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
    }
}
