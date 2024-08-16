using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace paginaOficial.viewsAdmin
{
    public partial class cambioContraseñaAdmin : System.Web.UI.Page
    {
        Controlers.BaseDatos odbusuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/Login.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            lblerror.Visible = false;
            if (txtPass.Text != string.Empty & txtPassnueva.Text != string.Empty & txtPassnueva2.Text != string.Empty)
            {
                odbusuario = new Controlers.BaseDatos();
                if (txtPassnueva.Text == txtPassnueva2.Text & odbusuario.login((string)Session["id"], txtPass.Text))
                {
                    odbusuario = new Controlers.BaseDatos();
                    if (odbusuario.ActualizarPassw((string)Session["id"], txtPassnueva.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Contraseña actualizada satisfactoriamente');", true);
                        txtPass.Text = "";
                        txtPassnueva.Text = "";
                        txtPassnueva2.Text = "";
                    }
                    else
                    {
                        lblerror.Text = "No se pudo actualizar la contraseña";
                        lblerror.Visible = true;

                    }
                }
                else
                {
                    lblerror.Text = "No se pudo actualizar la contraseña<br/>-Verifique si la contraseña actual sea correcta<br/>-Verifique si la contreseña nueva sea igual a la de confirmacion";
                    lblerror.Visible = true;

                }


            }
            else
            {
                lblerror.Text = "debe completar los campos necesarios";
                lblerror.Visible = true;
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtPass.Text = "";
            txtPassnueva.Text = "";
            txtPassnueva2.Text = "";
        }
    }
}