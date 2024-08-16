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
                odbusuario = new Controlers.BaseDatos();
                DataSet ds = odbusuario.listaVisitasActivas();
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();
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
                odbusuario = new Controlers.BaseDatos();
                DataSet ds = odbusuario.listaVisitasActivas();
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo finalizar la visita');", true);

            }

        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            odbusuario = new Controlers.BaseDatos();
            DataSet ds = odbusuario.listaVisitasActivas();
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }

    }

}