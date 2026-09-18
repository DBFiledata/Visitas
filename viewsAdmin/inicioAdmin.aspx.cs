using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace paginaOficial.viewsAdmin
{
    public partial class inicioAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated | ((string)Session["type"]) != "administrador")
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                Controlers.BaseDatos bd = new Controlers.BaseDatos();
                DataSet ds = bd.ultimasVisitas(10);
                gvUltimasVisitas.DataSource = ds.Tables[0];
                gvUltimasVisitas.DataBind();
            }
        }
    }
}