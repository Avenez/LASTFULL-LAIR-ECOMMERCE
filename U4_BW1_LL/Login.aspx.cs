using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace U4_BW1_LL
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {





            //Creo un Cookie di Login per verificare l'accesso dell'utente
            HttpCookie loginCookie = new HttpCookie("LOGIN_COOKIE");
            loginCookie.Values["userName"] = UserName.Text;
            loginCookie.Values["password"] = passWord.Text;
            loginCookie.Expires = DateTime.Now.AddDays(10);
            loginCookie.Secure = true;
            Response.Cookies.Add(loginCookie);


            // Se non trova un cookie relativo al carrelo lo crea
            if (Request.Cookies["CART_COOKIE"] == null)
            {
                //Se non esiste creo il cookie della cart al login
                // creo un arraylist e il cookie
                ArrayList cart = new ArrayList();
                HttpCookie cartCookie = new HttpCookie("CART_COOKIE");

                //creo un converter in json
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string cartJson = serializer.Serialize(cart);

                //aggiungo il json al cookie
                cartCookie.Values["cart"] = cartJson;
                Response.Cookies.Add(cartCookie);

            }

            //passo alla prima pagina
            Response.Redirect("Default");

        }


        protected bool CheckAdmin(string user, string password) {
            bool admin = false;
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try 
            {
            conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"SELECT * FROM Utenti WHERE Username = {user} AND Password = {password}";

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    admin = reader.GetBoolean(3);
                    Response.Write("Utente Admin Trovato");
                }
            }
            catch  (Exception ex)
            {
                Response.Write("Erroe");
                Response.Write(ex.Message);
            }
            finally 
            {
            conn.Close();
            }

            return admin;
        }
    }
}