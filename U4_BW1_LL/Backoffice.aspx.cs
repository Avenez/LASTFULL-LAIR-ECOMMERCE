using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace U4_BW1_LL
{
    public partial class Backoffice : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            string admin = Request.Cookies["LOGIN_COOKIEUTENTE"]["Admin"];

            if (admin == "True")
            {

                if (!IsPostBack)
                {
                    PickProducts();
                }

                RegisterPostBackControl();


            }
            else
            {

                Response.Redirect("Default");

            }



        }


        private void RegisterPostBackControl()
        {
            foreach (RepeaterItem item in BackOfficeProductsRepaeter.Items)
            {
                Button modifyButton = (Button)item.FindControl("modifyProduct");
                if (modifyButton != null)
                {
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(modifyButton);
                }
            }
        }


        protected void SearchButton_Click(object sender, EventArgs e)
        {



            string typeSerch = SearchType.Value;
            string key = SearchKey.Value;

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();

            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                string query;
                conn.Open();

                if (typeSerch == "Nome")
                {
                    query = $"SELECT * FROM Prodotti WHERE {typeSerch} LIKE '{key}%' ORDER BY {typeSerch} ASC ";

                }
                else if (typeSerch == "Prezzo")
                {
                    query = $"SELECT * FROM Prodotti WHERE {typeSerch} BETWEEN 0 AND {key} ORDER BY {typeSerch} DESC ";

                }

                else


                { query = $"SELECT * FROM Prodotti WHERE {typeSerch} = {key} "; }

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


        protected void SubmitChageButton_Click(object sender, EventArgs e)
        {

            string nome = FormName.Value;
            string descrizione = FormDescrizione.Value;
            string imgUrl = FormImg.Value;
            string prezzo = FormPrezzo.Value;
            string qta = FormQta.Value;
            string idProdotto = FormId.InnerText;



            if (nome != "" && descrizione != "" && imgUrl != "" && prezzo != "" && qta != "" && idProdotto != "")
            {

                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $" UPDATE Prodotti SET Nome = @Nome, Descrizione  = @Descrizione , ImgUrl = @ImgUrl , Prezzo = @Prezzo , Qta = @Qta  WHERE IDProdotto = @IDProdotto";

                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Descrizione", descrizione);
                    cmd.Parameters.AddWithValue("@ImgUrl", imgUrl);
                    cmd.Parameters.AddWithValue("@Prezzo", prezzo);
                    cmd.Parameters.AddWithValue("@Qta", qta);
                    cmd.Parameters.AddWithValue("@IDProdotto", idProdotto);

                    cmd.ExecuteNonQuery();


                    controllo.InnerText = "Modifica avvenuta con successo";
                    //ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", "setTimeout(function() { document.getElementById('controllo').innerText = ''; }, 3000);", true);
                    PickProducts();

                }
                catch (Exception ex)
                {
                    Response.Write("Errore ");
                    Response.Write(ex.Message);
                    controllo.InnerText = "Errore nel caricamento dei dati";

                }
                finally
                { conn.Close(); }

            }
            else
            {
                controllo.InnerText = "Per modificare è necessario inserire un prodotto";



            }

        }


        protected void PickProducts()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();

            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = $"SELECT * FROM Prodotti";


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

        protected void modifyProduct_Click(object sender, EventArgs e)
        {
            Button Button1 = (Button)sender;
            string commandArgument = Button1.CommandArgument;
            string[] parametri = commandArgument.Split('*');

            string nome = parametri[0];
            string descrizione = parametri[1];
            string imgUrl = parametri[2];
            string prezzo = parametri[3];
            //int qta = int.Parse(parametri[4]);
            string qta = parametri[4];
            string idProdotto = parametri[5];

            FormName.Value = nome;
            FormDescrizione.Value = descrizione;
            FormImg.Value = imgUrl;
            FormPrezzo.Value = Converter(prezzo);
            FormQta.Value = qta;
            FormId.InnerText = idProdotto;

            AddButton.Visible = false;
            DeleteButton.Visible = true;
            SubmitChageButton2.Visible = true;
            ReturnButton.Visible = true;


        }

        protected void ButtonAll_Click(object sender, EventArgs e)
        {
            PickProducts();
        }

        protected string Converter(string number)
        {
            // Rimuovi gli zeri inutili
            int indiceVirgola = number.IndexOf(',');
            number = number.Substring(0, indiceVirgola + 3);
            //number = number.TrimEnd('0');

            // Sostituisci la virgola con il punto
            string numberDot = number.Replace(",", ".");
            string numeroFormattato = numberDot; // Definisci la variabile all'esterno del blocco if


            return numeroFormattato;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {

            string idProdotto = FormId.InnerText;

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $" DELETE FROM Prodotti  WHERE IDProdotto = @IDProdotto";


                cmd.Parameters.AddWithValue("@IDProdotto", idProdotto);

                cmd.ExecuteNonQuery();


                controllo.InnerText = "Eliminazione avvenuta con successo";
                //ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", "setTimeout(function() { document.getElementById('controllo').innerText = ''; }, 3000);", true);
                PickProducts();

            }
            catch (Exception ex)
            {
                Response.Write("Errore ");
                Response.Write(ex.Message);
                controllo.InnerText = "Errore nel caricamento dei dati";

            }
            finally
            { conn.Close(); }

        }






        protected void AddButton_Click(object sender, EventArgs e)
        {

            string nome = FormName.Value;
            string descrizione = FormDescrizione.Value;
            string imgUrl = FormImg.Value;
            string prezzo = FormPrezzo.Value;
            string qta = FormQta.Value;




            if (nome != "" && descrizione != "" && imgUrl != "" && prezzo != "" && qta != "")
            {

                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $" INSERT INTO Prodotti (Nome, Descrizione, ImgUrl, Prezzo, Qta) VALUES ( @Nome, @Descrizione, @ImgUrl, @Prezzo, @Qta)";

                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Descrizione", descrizione);
                    cmd.Parameters.AddWithValue("@ImgUrl", imgUrl);
                    cmd.Parameters.AddWithValue("@Prezzo", prezzo);
                    cmd.Parameters.AddWithValue("@Qta", qta);


                    cmd.ExecuteNonQuery();


                    controllo.InnerText = "Prodotto aggiunto con successo";
                    //ClientScript.RegisterStartupScript(this.GetType(), "hideAlert", "setTimeout(function() { document.getElementById('controllo').innerText = ''; }, 3000);", true);
                    PickProducts();

                }
                catch (Exception ex)
                {
                    Response.Write("Errore ");
                    Response.Write(ex.Message);
                    controllo.InnerText = "Errore nel caricamento dei dati";

                }
                finally
                { conn.Close(); }

            }
            else
            {
                controllo.InnerText = "Per Aggiungere un prodotto è necessario inserire tutti i campi";



            }

        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            AddButton.Visible = true;
            DeleteButton.Visible = false;
            SubmitChageButton2.Visible = false;
            ReturnButton.Visible = false;

            FormName.Value = "";
            FormDescrizione.Value = "";
            FormImg.Value = "";
            FormPrezzo.Value = "";
            FormQta.Value = "";
            FormId.InnerText = "";
        }
    }
}