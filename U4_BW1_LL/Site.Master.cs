using System;
using System.Web;
using System.Web.UI;

namespace U4_BW1_LL
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Benvenuto.InnerHtml = $"Benvenuto, {Request.Cookies["LOGIN_COOKIEUTENTE"]["Username"]}";
        }

        protected void LogOut(object sender, EventArgs e)
        {
            HttpCookie userData = Request.Cookies["LOGIN_COOKIEUTENTE"];
            userData.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(userData);
            Response.Redirect("Login.aspx");
        }
    }
}