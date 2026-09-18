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
    public partial class catalogos : System.Web.UI.Page
    {
        Controlers.BaseDatos odbCatalogos;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                CargarGridArea();
                CargarComboAreasLugar();
                CargarGridLugar();
                CargarGridMotivo();
            }
        }

        // ===== Area (Slice 1) =====

        private void CargarGridArea()
        {
            odbCatalogos = new Controlers.BaseDatos();
            DataSet ds = odbCatalogos.ListaAreasAdmin();
            this.GridViewArea.DataSource = ds.Tables[0];
            this.GridViewArea.DataBind();
        }

        protected void GridViewArea_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                odbCatalogos = new Controlers.BaseDatos();
                if (e.CommandName.Equals("editar", StringComparison.CurrentCultureIgnoreCase))
                {
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string id = gvr.Cells[1].Text.ToString().Trim();
                    string nombre = gvr.Cells[2].Text.ToString().Trim();
                    Session["catalogoAreaId"] = id;
                    lblIdEditarArea.Text = id;
                    txtNombreEditarArea.Text = nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditarArea()", true);
                }
                if (e.CommandName.Equals("eliminar", StringComparison.CurrentCultureIgnoreCase))
                {
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string id = gvr.Cells[1].Text.ToString().Trim();
                    string nombre = gvr.Cells[2].Text.ToString().Trim();
                    Session["catalogoAreaId"] = id;
                    lblIdEliminarArea.Text = id;
                    lblNombreEliminarArea.Text = nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEliminarArea()", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", true);
            }
        }

        protected void btnCrearArea_Click(object sender, EventArgs e)
        {
            lblErrorCrearArea.Text = "";
            if (string.IsNullOrWhiteSpace(txtNombreCrearArea.Text))
            {
                lblErrorCrearArea.Text = "El nombre es obligatorio";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModalCrearArea()", true);
                return;
            }

            odbCatalogos = new Controlers.BaseDatos();
            if (odbCatalogos.CrearArea(txtNombreCrearArea.Text.Trim()))
            {
                txtNombreCrearArea.Text = "";
                CargarGridArea();
                CargarComboAreasLugar();
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('area creada satisfactoriamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo crear el area');", true);
            }
        }

        protected void btnGuardarEditarArea_Click(object sender, EventArgs e)
        {
            lblErrorEditarArea.Text = "";
            if (string.IsNullOrWhiteSpace(txtNombreEditarArea.Text))
            {
                lblErrorEditarArea.Text = "El nombre es obligatorio";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModalEditarArea()", true);
                return;
            }

            odbCatalogos = new Controlers.BaseDatos();
            if (odbCatalogos.ActualizarArea(((string)Session["catalogoAreaId"]), txtNombreEditarArea.Text.Trim()))
            {
                CargarGridArea();
                CargarComboAreasLugar();
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "alert('area actualizada satisfactoriamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "alert('la actualizacion no se a podido completar');", true);
            }
        }

        protected void btnConfirmarEliminarArea_Click(object sender, EventArgs e)
        {
            odbCatalogos = new Controlers.BaseDatos();
            if (odbCatalogos.EliminarArea(((string)Session["catalogoAreaId"])))
            {
                CargarGridArea();
                CargarComboAreasLugar();
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('area eliminada satisfactoriamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo eliminar el area');", true);
            }
        }

        // ===== Lugar (Slice 2) =====
        // GridViewLugar columnas auto-generadas por listaLugaresAdmin, en orden:
        // 0=Acciones(template), 1='#' (idtypeOfPlaces), 2=nombre, 3=abreviado,
        // 4=oficina, 5=idIdentifier (id del Area padre), 6=area (nombre del Area padre)

        private void CargarGridLugar()
        {
            odbCatalogos = new Controlers.BaseDatos();
            DataSet ds = odbCatalogos.ListaLugaresAdmin();
            this.GridViewLugar.DataSource = ds.Tables[0];
            this.GridViewLugar.DataBind();
        }

        private void CargarComboAreasLugar()
        {
            odbCatalogos = new Controlers.BaseDatos();
            DataSet dsAreas = odbCatalogos.ListaAreasAdmin();

            ddlAreaCrearLugar.DataSource = dsAreas.Tables[0];
            ddlAreaCrearLugar.DataTextField = "nombre";
            ddlAreaCrearLugar.DataValueField = "#";
            ddlAreaCrearLugar.DataBind();

            ddlAreaEditarLugar.DataSource = dsAreas.Tables[0];
            ddlAreaEditarLugar.DataTextField = "nombre";
            ddlAreaEditarLugar.DataValueField = "#";
            ddlAreaEditarLugar.DataBind();
        }

        protected void GridViewLugar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                odbCatalogos = new Controlers.BaseDatos();
                if (e.CommandName.Equals("editar", StringComparison.CurrentCultureIgnoreCase))
                {
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string id = gvr.Cells[1].Text.ToString().Trim();
                    string nombre = gvr.Cells[2].Text.ToString().Trim();
                    string idArea = gvr.Cells[5].Text.ToString().Trim();
                    Session["catalogoLugarId"] = id;
                    lblIdEditarLugar.Text = id;
                    txtNombreEditarLugar.Text = nombre;
                    if (ddlAreaEditarLugar.Items.FindByValue(idArea) != null)
                    {
                        ddlAreaEditarLugar.ClearSelection();
                        ddlAreaEditarLugar.Items.FindByValue(idArea).Selected = true;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditarLugar()", true);
                }
                if (e.CommandName.Equals("eliminar", StringComparison.CurrentCultureIgnoreCase))
                {
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string id = gvr.Cells[1].Text.ToString().Trim();
                    string nombre = gvr.Cells[2].Text.ToString().Trim();
                    Session["catalogoLugarId"] = id;
                    lblIdEliminarLugar.Text = id;
                    lblNombreEliminarLugar.Text = nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEliminarLugar()", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", true);
            }
        }

        protected void btnCrearLugar_Click(object sender, EventArgs e)
        {
            lblErrorCrearLugar.Text = "";
            if (string.IsNullOrWhiteSpace(txtNombreCrearLugar.Text))
            {
                lblErrorCrearLugar.Text = "El nombre es obligatorio";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModalCrearLugar()", true);
                return;
            }
            if (string.IsNullOrEmpty(ddlAreaCrearLugar.SelectedValue))
            {
                lblErrorCrearLugar.Text = "El area es obligatoria";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModalCrearLugar()", true);
                return;
            }

            odbCatalogos = new Controlers.BaseDatos();
            if (odbCatalogos.CrearLugar(txtNombreCrearLugar.Text.Trim(), ddlAreaCrearLugar.SelectedValue))
            {
                txtNombreCrearLugar.Text = "";
                CargarGridLugar();
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('lugar creado satisfactoriamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo crear el lugar');", true);
            }
        }

        protected void btnGuardarEditarLugar_Click(object sender, EventArgs e)
        {
            lblErrorEditarLugar.Text = "";
            if (string.IsNullOrWhiteSpace(txtNombreEditarLugar.Text))
            {
                lblErrorEditarLugar.Text = "El nombre es obligatorio";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModalEditarLugar()", true);
                return;
            }
            if (string.IsNullOrEmpty(ddlAreaEditarLugar.SelectedValue))
            {
                lblErrorEditarLugar.Text = "El area es obligatoria";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModalEditarLugar()", true);
                return;
            }

            odbCatalogos = new Controlers.BaseDatos();
            if (odbCatalogos.ActualizarLugar(((string)Session["catalogoLugarId"]), txtNombreEditarLugar.Text.Trim(), ddlAreaEditarLugar.SelectedValue))
            {
                CargarGridLugar();
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "alert('lugar actualizado satisfactoriamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "alert('la actualizacion no se a podido completar');", true);
            }
        }

        protected void btnConfirmarEliminarLugar_Click(object sender, EventArgs e)
        {
            odbCatalogos = new Controlers.BaseDatos();
            if (odbCatalogos.EliminarLugar(((string)Session["catalogoLugarId"])))
            {
                CargarGridLugar();
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('lugar eliminado satisfactoriamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo eliminar el lugar');", true);
            }
        }

        // ===== Motivo (Slice 3) =====
        // Motivo es un catalogo plano (sin relacion jerarquica a otro catalogo).

        private void CargarGridMotivo()
        {
            odbCatalogos = new Controlers.BaseDatos();
            DataSet ds = odbCatalogos.ListaMotivosAdmin();
            this.GridViewMotivo.DataSource = ds.Tables[0];
            this.GridViewMotivo.DataBind();
        }

        protected void GridViewMotivo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                odbCatalogos = new Controlers.BaseDatos();
                if (e.CommandName.Equals("editar", StringComparison.CurrentCultureIgnoreCase))
                {
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string id = gvr.Cells[1].Text.ToString().Trim();
                    string nombre = gvr.Cells[2].Text.ToString().Trim();
                    Session["catalogoMotivoId"] = id;
                    lblIdEditarMotivo.Text = id;
                    txtNombreEditarMotivo.Text = nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditarMotivo()", true);
                }
                if (e.CommandName.Equals("eliminar", StringComparison.CurrentCultureIgnoreCase))
                {
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string id = gvr.Cells[1].Text.ToString().Trim();
                    string nombre = gvr.Cells[2].Text.ToString().Trim();
                    Session["catalogoMotivoId"] = id;
                    lblIdEliminarMotivo.Text = id;
                    lblNombreEliminarMotivo.Text = nombre;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEliminarMotivo()", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Error al ingresar al proceso');", true);
            }
        }

        protected void btnCrearMotivo_Click(object sender, EventArgs e)
        {
            lblErrorCrearMotivo.Text = "";
            if (string.IsNullOrWhiteSpace(txtNombreCrearMotivo.Text))
            {
                lblErrorCrearMotivo.Text = "El nombre es obligatorio";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModalCrearMotivo()", true);
                return;
            }

            odbCatalogos = new Controlers.BaseDatos();
            if (odbCatalogos.CrearMotivo(txtNombreCrearMotivo.Text.Trim()))
            {
                txtNombreCrearMotivo.Text = "";
                CargarGridMotivo();
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('motivo creado satisfactoriamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo crear el motivo');", true);
            }
        }

        protected void btnGuardarEditarMotivo_Click(object sender, EventArgs e)
        {
            lblErrorEditarMotivo.Text = "";
            if (string.IsNullOrWhiteSpace(txtNombreEditarMotivo.Text))
            {
                lblErrorEditarMotivo.Text = "El nombre es obligatorio";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModalEditarMotivo()", true);
                return;
            }

            odbCatalogos = new Controlers.BaseDatos();
            if (odbCatalogos.ActualizarMotivo(((string)Session["catalogoMotivoId"]), txtNombreEditarMotivo.Text.Trim()))
            {
                CargarGridMotivo();
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "alert('motivo actualizado satisfactoriamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "pop", "alert('la actualizacion no se a podido completar');", true);
            }
        }

        protected void btnConfirmarEliminarMotivo_Click(object sender, EventArgs e)
        {
            odbCatalogos = new Controlers.BaseDatos();
            if (odbCatalogos.EliminarMotivo(((string)Session["catalogoMotivoId"])))
            {
                CargarGridMotivo();
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('motivo eliminado satisfactoriamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo eliminar el motivo');", true);
            }
        }
    }
}
