using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            
            MyConnection myConnection = MyConnection.getInstance();

            using (SqlConnection conn = new SqlConnection(myConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("StoredProcedure", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDProdotto", IDProdotto);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                           
                            lblNomeProdotto.Text = reader["Nome"].ToString();
                            lblPrezzoProdotto.Text = string.Format("{0:C}", reader["Prezzo"]);
                            lblDescrizioneProdotto.Text = reader["Descrizione"].ToString();
                            imgProdotto.ImageUrl = reader["Immagine"].ToString();
                        }
                    }
                }
            }
        }

        protected void btnTornaIndietro_Click(object sender, EventArgs e)
        {
            // Reindirizza alla pagina principale
            Response.Redirect("Default.aspx");
        }
    }
}