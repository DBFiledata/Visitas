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
    public partial class colaImpresion : System.Web.UI.Page
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
                DataSet ds = odbusuario.listaImprecion();
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();
            }
            else
            {


            }

        }

        protected void btnfiltrar_Click(object sender, EventArgs e)
        {
            odbusuario = new Controlers.BaseDatos();
            string estado="";
            if (txtNumero.Text != string.Empty & listTipo.SelectedIndex > 0 & listTipo.SelectedIndex < 3)
            {
                
                if (listTipo.SelectedIndex==1)
                {
                    estado = "N";
                }
                else if (listTipo.SelectedIndex == 2)
                {
                    estado = "Y";
                }

                    DataSet ds = odbusuario.filtroColaImprsion(estado, Convert.ToInt32(txtNumero.Text));
                    this.GridView1.DataSource = ds.Tables[0];
                    this.GridView1.DataBind();

            }
            else if (txtNumero.Text != string.Empty & listTipo.SelectedIndex == 0)
            {

                    DataSet ds = odbusuario.filtroColaImprsion( null, Convert.ToInt32(txtNumero.Text));
                    this.GridView1.DataSource = ds.Tables[0];
                    this.GridView1.DataBind();
                
            }
            else if (txtNumero.Text == string.Empty & listTipo.SelectedIndex > 0 & listTipo.SelectedIndex < 3)
            {

                if (listTipo.SelectedIndex == 1)
                {
                    estado = "N";
                }
                else if (listTipo.SelectedIndex == 2)
                {
                    estado = "Y";
                }
                DataSet ds = odbusuario.filtroColaImprsion(estado, 0);
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();
            }
            else if (txtNumero.Text == string.Empty & listTipo.SelectedIndex == 0)
            {
                DataSet ds = odbusuario.filtroColaImprsion(null, 0);
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                odbusuario = new Controlers.BaseDatos();
                if (e.CommandName.Equals("editar", StringComparison.CurrentCultureIgnoreCase))
                {
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string eleccion = gvr.Cells[1].Text.ToString().Trim();
                    Session["visita"] = eleccion;
                    lblNumVisita.Text = eleccion;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEditar()", true);
                }

            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Erorr al ingresar al proceso');", true);

            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            odbusuario = new Controlers.BaseDatos();
            if (odbusuario.ImprimirVisitas(Convert.ToInt32((string)Session["visita"])))
            {
                odbusuario = new Controlers.BaseDatos();
                DataSet ds = odbusuario.listaImprecion();
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('Cambio de estado de impresion realizado');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alert('no se pudo realizar el proceso');", true);

            }

        }
    }



}