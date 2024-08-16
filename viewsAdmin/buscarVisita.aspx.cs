using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace paginaOficial.viewsAdmin
{
    public partial class buscarVisita : System.Web.UI.Page
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
                DataSet ds = odbusuario.listaVisitas();
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();

                odbusuario = new Controlers.BaseDatos();
                DataSet ds2 = odbusuario.listaAreas();
                DataTable dt = ds2.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    listTipo.Items.Add(new ListItem(r[1].ToString(), r[0].ToString()));
                }
            }
            else
            {


            }

        }

        protected void btnfiltrar_Click(object sender, EventArgs e)
        {
            odbusuario = new Controlers.BaseDatos();
            string  desde, hasta;
            int numero, tipo, lugar;

            if (listTipo.SelectedIndex > 0)
            {
                tipo= Convert.ToInt32(listTipo.SelectedValue);
            }
            else
            {
                tipo = 0;
            }
            if (listLugar.SelectedIndex > 0)
            {
                lugar = Convert.ToInt32(listLugar.SelectedValue);
            }
            else
            {
                lugar = 0;
            }
            if (txtDesde.Text != string.Empty)
            {
                desde = txtDesde.Text;
            }
            else
            {
                desde = "0000-00-00";
            }
            if (TxtHasta.Text != string.Empty)
            {
                hasta = TxtHasta.Text;
            }
            else
            {
                hasta = "0000-00-00";
            }
            if (txtNumero.Text != string.Empty)
            {
                numero = Convert.ToInt32(txtNumero.Text);
            }
            else
            {
                numero = 0;
            }
            DataSet ds = odbusuario.filtroVisitas(tipo, lugar, numero, desde, hasta);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            odbusuario = new Controlers.BaseDatos();
            string desde, hasta;
            int numero, tipo, lugar;

            if (listTipo.SelectedIndex > 0)
            {
                tipo = Convert.ToInt32(listTipo.SelectedValue);
            }
            else
            {
                tipo = 0;
            }
            if (listLugar.SelectedIndex > 0)
            {
                lugar = Convert.ToInt32(listLugar.SelectedValue);
            }
            else
            {
                lugar = 0;
            }
            if (txtDesde.Text != string.Empty)
            {
                desde = txtDesde.Text;
            }
            else
            {
                desde = "0000-00-00";
            }
            if (TxtHasta.Text != string.Empty)
            {
                hasta = TxtHasta.Text;
            }
            else
            {
                hasta = "0000-00-00";
            }
            if (txtNumero.Text != string.Empty)
            {
                numero = Convert.ToInt32(txtNumero.Text);
            }
            else
            {
                numero = 0;
            }

            //exportacion a exel
            DataSet ds = odbusuario.filtroVisitas(tipo, lugar, numero, desde, hasta);
            var grid = new GridView();
            grid.DataSource = ds.Tables[0];
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=reporte.xls");
            Response.ContentType = "application/exel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }


        protected void listTipoAreaSelected(object sender, EventArgs e)
        {
            odbusuario = new Controlers.BaseDatos();
            if (Convert.ToInt32(listTipo.SelectedValue) > 0)
            {
                this.listLugar.Items.Clear();
                listLugar.Items.Insert(0, "📝 Seleccionar");
                DataSet ds2 = odbusuario.listaAreasVisitas(Convert.ToInt32(listTipo.SelectedValue.ToString()));
                DataTable dt = ds2.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    listLugar.Items.Add(new ListItem(r[1].ToString(), r[0].ToString()));
                }
                listLugar.Enabled = true;
                this.listLugar.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                odbusuario = new Controlers.BaseDatos();
                if (e.CommandName.Equals("info", StringComparison.CurrentCultureIgnoreCase))
                {
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string eleccion = gvr.Cells[1].Text.ToString().Trim();
                    Session["cedula"] = eleccion;
                    string[] datos = new string[4];
                    datos = odbusuario.infoCuenta(eleccion);
                    lblIdInfo.Text = datos[0];
                    lblNombreInfo.Text = datos[1] + " " + datos[2];
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
                if (e.CommandName.Equals("eliminar", StringComparison.CurrentCultureIgnoreCase))
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