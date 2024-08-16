using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace paginaOficial.viewsAdmin
{
    public partial class listaUsuarios : System.Web.UI.Page
    {
        Controlers.BaseDatos odbusuario;
      
          
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }
           if (!Page.IsPostBack)
            {
                odbusuario = new Controlers.BaseDatos();
                DataSet ds = odbusuario.ListaTotal();
                this.GridView1.DataSource =  ds.Tables[0];
                this.GridView1.DataBind();
            }
            else
            {


            }

        }

        protected void btnfiltrar_Click(object sender, EventArgs e)
        {
            odbusuario = new Controlers.BaseDatos();
            string texto = txtNombre.Text + " ";
            string[] datos = texto.Split(' ');
            if (txtNombre.Text != string.Empty & listTipo.SelectedIndex > 0 & listTipo.SelectedIndex < 3)
            {
                if (datos[1] != null & datos[1] != string.Empty)
                {
                    DataSet ds = odbusuario.filtroUsuario(datos[0], datos[1], listTipo.SelectedItem.Text);
                    this.GridView1.DataSource = ds.Tables[0];
                    this.GridView1.DataBind();
                }
                else
                {
                    DataSet ds = odbusuario.filtroUsuario(datos[0], null, listTipo.SelectedItem.Text);
                    this.GridView1.DataSource = ds.Tables[0];
                    this.GridView1.DataBind();
                }

            }
            else if (txtNombre.Text != string.Empty & listTipo.SelectedIndex == 0)
            {
                if (datos[0] != null & datos[1] != string.Empty)
                {
                    DataSet ds = odbusuario.filtroUsuario(datos[0], datos[1], null);
                    this.GridView1.DataSource = ds.Tables[0];
                    this.GridView1.DataBind();
                }
                else
                {
                    DataSet ds = odbusuario.filtroUsuario(datos[0], null, null);
                    this.GridView1.DataSource = ds.Tables[0];
                    this.GridView1.DataBind();
                }
            }
            else if (txtNombre.Text == string.Empty & listTipo.SelectedIndex > 0 & listTipo.SelectedIndex < 3)
            {
                DataSet ds = odbusuario.filtroUsuario(null, null, listTipo.SelectedItem.Text);
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();
            }
            else if (txtNombre.Text == string.Empty & listTipo.SelectedIndex == 0)
            {
                DataSet ds = odbusuario.filtroUsuario(null, null, null);
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try { 
            odbusuario = new Controlers.BaseDatos();
            if (e.CommandName.Equals("info",StringComparison.CurrentCultureIgnoreCase))
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                string eleccion = gvr.Cells[1].Text.ToString().Trim();
                Session["cedula"] = eleccion;
                string[] datos = new string[4];
                datos = odbusuario.infoCuenta(eleccion);
                lblIdInfo.Text = datos[0];
                lblNombreInfo.Text = datos[1]+" "+ datos[2];
                lblTipoInfo.Text = datos[3];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalInfo()", true);
                }
                if (e.CommandName.Equals("editar", StringComparison.CurrentCultureIgnoreCase))
                {
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string eleccion = gvr.Cells[1].Text.ToString().Trim();
                    Session["cedula"] = eleccion;
                    string[] datos = new string[4];
                    datos = odbusuario.infoCuenta(eleccion);
                    txtIdEditar.Text = datos[0];
                    txtNombreEditar.Text = datos[1];
                    txtApellidoEditar.Text = datos[2];
                    listTipoEditar.ClearSelection();
                    listTipoEditar.Items.FindByText(datos[3]).Selected = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditar()", true);

                }
            if (e.CommandName.Equals("eliminar",StringComparison.CurrentCultureIgnoreCase))
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                string eleccion = gvr.Cells[1].Text.ToString().Trim();
                Session["cedula"] = eleccion;
                string[] datos = new string[4];
                datos = odbusuario.infoCuenta(eleccion);
                lblIdEliminar.Text = datos[0];
                lblNombreEliminar.Text = datos[1] + " " + datos[2];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEliminar()", true);
                }

            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Erorr al ingresar al proceso');", true);

            }
        }

        protected void btnEiminaruenta_Click(object sender, EventArgs e)
        {
            odbusuario = new Controlers.BaseDatos();
            if (odbusuario.EliminarUsuario(((string)Session["cedula"])))
            {
                odbusuario = new Controlers.BaseDatos();
                DataSet ds = odbusuario.ListaTotal();
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('usuario eliminado satisfactoriamente');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo eliminar al usuario');", true);

            }

        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            lblerror.Visible = false;
            odbusuario = new Controlers.BaseDatos();
            if (odbusuario.ActualizarUsuario(((string)Session["cedula"]), txtIdEditar.Text, txtNombreEditar.Text, txtApellidoEditar.Text, listTipoEditar.SelectedItem.Text))
            {
                odbusuario = new Controlers.BaseDatos();
                DataSet ds = odbusuario.ListaTotal();
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "alert('usuario actualizado satisfactoriamente');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "alert('la actualizacion no se a podido completar');", true);

            }
        
        }
        }
        


    }


  
