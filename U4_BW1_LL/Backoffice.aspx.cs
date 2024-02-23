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

        //All'avvio della pagina controlliamo che l'utente abbia i permessi di admin all'interno del cookie
        //-In caso positivo, la prima volta che la pagina viene caricata recuperiamo tutti i proditti dal db in
        //modo che possano essere mostrati e modificati
        //-In caso negativo l'utente viene riportato alla pagina di default
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["LOGIN_COOKIEUTENTE"] == null)
            {
                Response.Redirect("PreSite.aspx");
            }

            if (!IsPostBack)
            {
                string admin = Request.Cookies["LOGIN_COOKIEUTENTE"]["Admin"];

                if (admin == "True")
                {
                    PickProducts();
                }
                else
                {
                    Response.Redirect("Default");
                }
            }
            RegisterPostBackControl();
        }


        // Questa funzione assicura che ogni pulsante "modifyProduct" all'interno del repeater venga gestito correttamente per i postback utilizzando il ScriptManager. 
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



        //Questa funzione assicura la ricerca dei prodotti in base al criterio scelto nel select e d esegue una query in funzione di quel parametro
        //Recuperato il dataset lo passa al pepeate come datasoruce 
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
                { query = $"SELECT * FROM Prodotti WHERE {typeSerch} LIKE '%{key}%' ORDER BY {typeSerch} ASC "; }

                else if (typeSerch == "Prezzo")
                { query = $"SELECT * FROM Prodotti WHERE {typeSerch} BETWEEN 0 AND {key} ORDER BY {typeSerch} DESC "; }

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


        //Questa funzione si occupa dell'invio dei dati al DB per la modifica dei campi di un prodotto
        //assicurandosi che i campi siano tutti compilati e svuotando il campo feedback dopo 4s

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

            string script2 = "setTimeout(() => { " + "document.getElementById('MainContent_controllo').innerText = '';"
                                                   + "}, 4000);";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "changeFeed2", script2, true);

        }


        //Questa è la funzione che esegue la query iniziale che recupera tutti i prodotti dal db
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

        //Funzione che inserisce i campi di un prodotto selezionato all'interno dei campi di testo adibiti alla modifica
        //Controlla anche la visualizzazione dei comandi della modifica e dell'inserimento dei prodotti
        protected void modifyProduct_Click(object sender, EventArgs e)
        {
            Button Button1 = (Button)sender;
            string commandArgument = Button1.CommandArgument;
            string[] parametri = commandArgument.Split('*');

            string nome = parametri[0];
            string descrizione = parametri[1];
            string imgUrl = parametri[2];
            string prezzo = parametri[3];
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


        //Semplice comando per il recupero di tutti i prodotti
        protected void ButtonAll_Click(object sender, EventArgs e)
        {
            PickProducts();
        }


        //Funzione che prende in entrata il prezzo in money dal campo Prezzo e lo converte con massimo due cifre decimali ed il punto al posto della virgola
        //Questo restituisce all'utente una cifra in formato nn.nn per evitare che si usi la vigola che viene letta come migliaia e non decimale.
        protected string Converter(string number)
        {
            int indiceVirgola = number.IndexOf(',');
            number = number.Substring(0, indiceVirgola + 3);

            string numberDot = number.Replace(",", ".");
            string numeroFormattato = numberDot; // Definisci la variabile all'esterno del blocco if

            return numeroFormattato;
        }


        //Funzione che si occupa dell'eliminazione di un prodotto selezionato, restitutendo un feedback all'utente
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
                PickProducts();

                FormName.Value = "";
                FormDescrizione.Value = "";
                FormImg.Value = "";
                FormPrezzo.Value = "";
                FormQta.Value = "";
                FormId.InnerText = "";

            }
            catch (Exception ex)
            {
                Response.Write("Errore ");
                Response.Write(ex.Message);
                controllo.InnerText = "Errore nel caricamento dei dati";
            }
            finally
            { conn.Close(); }

            string script2 = "setTimeout(() => { " + "document.getElementById('MainContent_controllo').innerText = '';"
                                                    + "}, 4000);";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "changeFeed3", script2, true);

        }





        //Funzione per l'aggiunta di un prodotto al DB fornendo un feed all'utente
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
                    PickProducts();

                    FormName.Value = "";
                    FormDescrizione.Value = "";
                    FormImg.Value = "";
                    FormPrezzo.Value = "";
                    FormQta.Value = "";
                    FormId.InnerText = "";
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

            string script2 = "setTimeout(() => { " + "document.getElementById('MainContent_controllo').innerText = '';"
                                                    + "}, 4000);";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "changeFeed4", script2, true);
        }

        //Funzione di controllo dei comandi del BackOffice 
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

        protected void BTNvaiAOrdiniUtente_Click(object sender, EventArgs e)
        {
            Response.Redirect("RiepilogoOrdiniUtenti.aspx");
        }
    }
}