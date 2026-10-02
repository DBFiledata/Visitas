using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace paginaOficial.viewsAdmin
{
    public partial class finalizarVisitas : System.Web.UI.Page
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
                CargarVisitas();
            }
            else
            {
                this.Timer1.Enabled = true;
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                odbusuario = new Controlers.BaseDatos();
                if (e.CommandName.Equals("finalisarVisita", StringComparison.CurrentCultureIgnoreCase))
                {
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string eleccion = gvr.Cells[1].Text.ToString().Trim();
                    Session["idVisita"] = eleccion;
                    string datos;
                    datos = odbusuario.buscarVisitaDatos(eleccion);
                    lblIdFinalizar.Text = datos;
                    this.mpe.Show();
                    this.Timer1.Enabled = false;
                }

            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Erorr al ingresar al proceso');", true);

            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.mpe.Hide();
            odbusuario = new Controlers.BaseDatos();
            if (odbusuario.finalizarVisita(Convert.ToInt32((string)Session["idVisita"])))
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('se finalizo la visita satisfactoriamente');", true);
                CargarVisitas();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo finalizar la visita');", true);

            }

        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            CargarVisitas();
        }

        // Cierre masivo: finaliza SOLO las visitas abiertas creadas hoy. El limite del dia lo
        // aplica el SP finalizarVisitasDelDia (migracion 0012), no la lista de la pantalla.
        protected void btnCerrarVisitasHoy_Click(object sender, EventArgs e)
        {
            odbusuario = new Controlers.BaseDatos();
            int cerradas = odbusuario.finalizarVisitasDelDia();
            string mensaje = cerradas < 0 ? "No se pudieron cerrar las visitas de hoy"
                : cerradas == 0 ? "No hay visitas abiertas de hoy para cerrar"
                : "Se cerraron " + cerradas + " visita(s) de hoy";
            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('" + mensaje + "');", true);
            CargarVisitas();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CargarVisitas();
        }

        // Carga SOLO la pagina actual (SP listaVisitasActivasPagina, migracion 0013). El Timer y
        // los cierres recargan la misma pagina en la que esta el operador.
        private void CargarVisitas()
        {
            odbusuario = new Controlers.BaseDatos();
            DataSet ds = odbusuario.listaVisitasActivasPagina(GridView1.PageIndex, GridView1.PageSize);
            if (ds.Tables.Count < 2) return; // fallo la consulta: se deja la grilla como estaba

            int total = Convert.ToInt32(ds.Tables[1].Rows[0]["total"]);
            int ultimaPagina = Math.Max((total - 1) / GridView1.PageSize, 0);
            if (GridView1.PageIndex > ultimaPagina)
            {
                // Al cerrar visitas la pagina actual puede dejar de existir: se va a la ultima.
                GridView1.PageIndex = ultimaPagina;
                ds = odbusuario.listaVisitasActivasPagina(GridView1.PageIndex, GridView1.PageSize);
                if (ds.Tables.Count < 2) return;
            }

            GridView1.VirtualItemCount = total;
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();

            int desde = GridView1.PageIndex * GridView1.PageSize;
            lblResumenVisitas.Text = total == 0 ? "No hay visitas activas"
                : "Mostrando " + (desde + 1).ToString("N0") + " a " + (desde + ds.Tables[0].Rows.Count).ToString("N0")
                  + " de " + total.ToString("N0") + " visitas activas";
            ActualizarBotonCierre();
        }

        // El boton muestra cuantas visitas de hoy siguen abiertas y pide confirmacion con esa cifra.
        private void ActualizarBotonCierre()
        {
            odbusuario = new Controlers.BaseDatos();
            int abiertasHoy = odbusuario.visitasAbiertasHoy();
            btnCerrarVisitasHoy.Text = "🔐 Cerrar visitas de hoy" + (abiertasHoy >= 0 ? " (" + abiertasHoy + ")" : "");
            btnCerrarVisitasHoy.Enabled = abiertasHoy != 0;
            btnCerrarVisitasHoy.OnClientClick = "return confirm('" + HttpUtility.JavaScriptStringEncode(
                "¿Cerrar " + (abiertasHoy > 0 ? "las " + abiertasHoy + " visitas abiertas" : "todas las visitas abiertas")
                + " de hoy?" + Environment.NewLine + Environment.NewLine
                + "Las visitas de días anteriores no se modifican.") + "');";
        }

    }

}