using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace U4_BW1_LL
{
    public partial class Backoffice : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack) {
                PickProducts();
            //}
            
        }


        protected void SearchButton_Click(object sender, EventArgs e) { 
        

            
             string typeSerch = SearchType.Value;
             string key = SearchKey.Value;

                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();

                SqlConnection conn = new SqlConnection(connectionString);

                try
                {
                    conn.Open();
                    string query = $"SELECT * FROM Prodotti WHERE {typeSerch} = {key} ";
                    


                    SqlCommand cmd = new SqlCommand(query, conn);
                   
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    BackOfficeProductsRepaeter.DataSource = dt;
                    BackOfficeProductsRepaeter.DataBind();



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

        protected void SubmitChageButton_Click(object sender, EventArgs e) {
        
        
        }



        protected void PickProducts() {

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();

            SqlConnection conn = new SqlConnection(connectionString);

            try 
            { 
                conn.Open();
                //string query = $"SELECT * FROM Prodotti WHERE {typeSerch} = @key ";
                string query = $"SELECT * FROM Prodotti";


                SqlCommand cmd = new SqlCommand(query, conn);
                //cmd.Parameters.AddWithValue("@key", key);
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                BackOfficeProductsRepaeter.DataSource = dt;
                BackOfficeProductsRepaeter.DataBind();
             
            

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

        protected void modifyProduct_Click(object sender, EventArgs e)
        {
            Button Button1 = (Button)sender;
            string commandArgument = Button1.CommandArgument;
            string[] parametri = commandArgument.Split(',');

            string nome = parametri[0];
            string descrizione = parametri[1];
            string imgUrl = parametri[2];
            string prezzo = parametri[3];
            //int qta = int.Parse(parametri[4]);
            string qta = parametri[4];

            FormName.Value = nome;
            FormDescrizione.Value = descrizione;
            FormImg.Value = imgUrl;
            FormPrezzo.Value = prezzo;
            FormQta.Value = qta;

        }
    }
}