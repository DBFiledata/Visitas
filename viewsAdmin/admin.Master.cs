using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace paginaOficial.viewsAdmin
{
    public partial class admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCerrarsesion_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect("~/login.aspx");
        }
    }
}