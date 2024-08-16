using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace paginaOficial
{
    public partial class registro : System.Web.UI.Page
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
            if (txtId.Text != string.Empty & txtPass.Text != string.Empty & txtNombre.Text != string.Empty & txtApellido.Text != string.Empty &
                    txtPass2.Text != string.Empty & listTipo.SelectedIndex > 0 & listTipo.SelectedIndex < 3)
            {
                    int num = listTipo.SelectedIndex;
                    odbusuario = new Controlers.BaseDatos();
                 if (odbusuario.RegistrarUsuario(txtId.Text, txtPass.Text, txtNombre.Text, txtApellido.Text, listTipo.SelectedItem.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Registro completado satisfactoriamente');", true);
                        txtId.Text = "";
                        txtPass.Text = "";
                        txtPass2.Text = "";
                        txtNombre.Text = "";
                        txtApellido.Text = "";
                        listTipo.ClearSelection();

                    }
                else
                    {
                        lblerror.Text = "No se pudo registrar el ususario <br />-verifique si la informacion esta ingresada correctamente <br />-verifique que al id que intenta registrar no este creado";
                        lblerror.Visible = true;
                    }
             }
             else
             {
                lblerror.Text = "debe completar los campo requeridos";
                lblerror.Visible = true;
             }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtPass.Text = "";
            txtPass2.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            listTipo.ClearSelection();
        }
    }
}