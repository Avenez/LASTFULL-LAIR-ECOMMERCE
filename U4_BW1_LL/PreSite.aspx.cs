using System;
using System.Web.UI;

namespace U4_BW1_LL
{
    public partial class PreSite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void EnterTheLair_Click(object sender, EventArgs e)
        {
            string script1 = "document.getElementById('imgTop').style.transition = 'opacity 2s ease'; " +
                "setTimeout(() => {document.getElementById('imgTop').style.opacity = 0;}, 200);";
            string script2 = "setTimeout(() => {document.getElementById('disclaimer').classList.add('fadeOut');}, 2000);" +
                "setTimeout(() => {window.location.replace(\"Login.aspx\");}, 2800);";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "unlock", script1, true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "fadeDisclaimer", script2, true);
        }
    }
}