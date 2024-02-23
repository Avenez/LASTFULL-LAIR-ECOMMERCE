using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace U4_BW1_LL
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            cartBadge.Visible = false;
            if (Request.Cookies["LOGIN_COOKIEUTENTE"] != null)
                Benvenuto.InnerHtml = $"Benvenuto, {Request.Cookies["LOGIN_COOKIEUTENTE"]["Username"]}";
            Dictionary<int, int> cartMap = (Dictionary<int, int>)Session["cart"];

            if (cartMap != null && cartMap.Count == 0)
            {
                // La mappa del carrello è vuota
            }
            else if (cartMap != null)
            {
                // La mappa del carrello non è vuota, fai qualcosa con i suoi elementi
                int count = 0;
                foreach (var item in cartMap)
                {
                    count += item.Value;
                }
                cartBadge.InnerText = count.ToString();
                cartBadge.Visible = true;

            }


        }

        protected void LogOut(object sender, EventArgs e)
        {
            HttpCookie userData = Request.Cookies["LOGIN_COOKIEUTENTE"];
            userData.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(userData);

            if (Session["cart"] != null)
            {
                Session["cart"] = null;
            }

            Response.Redirect("Login.aspx");
        }
    }
}