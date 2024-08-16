using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace paginaOficial.viewsAdmin
{
    public partial class infoCuentaAdmin : System.Web.UI.Page
    {
        Controlers.BaseDatos odbusuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated) { 
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                odbusuario = new Controlers.BaseDatos();
                string[] datos = new string[6];
                string id = (string)Session["id"];
                datos = odbusuario.infoCuenta((string)Session["id"]);
                lblId.Text = datos[0];
                lblNombre.Text = datos[1];
                lblAapellido.Text = datos[2];
                lblTipo.Text = datos[3];
            }

        }
    }
}