using System;
using System.Web.UI;

namespace U4_BW1_LL
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // come richiamo la sessione
            Benvenuto.InnerHtml = $"Benvenuto {Request.Cookies["LOGIN_COOKIEUTENTE"]["Username"]}";


        }
    }
}