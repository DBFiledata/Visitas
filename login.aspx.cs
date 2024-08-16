using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace paginaOficial
{
    public partial class login : System.Web.UI.Page
    {
        Controlers.BaseDatos odbusuario ;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                ViewState["LoginErrors"] = 0;
        }

        protected void Login_Authenticate1(object sender, AuthenticateEventArgs e)
        {
            if (Login.Password != string.Empty & Login.UserName != string.Empty)
            {
                odbusuario=new Controlers.BaseDatos();
                if (odbusuario.login(Login.UserName, Login.Password))
                {
                    try { 
                    string[] datos = new string[3];
                    datos= odbusuario.getDatosSession(Login.UserName, Login.Password);
                    Session["id"] = Login.UserName;
                    Session["username"] = datos[0];
                    Session["type"] = datos[1];//especificar el tipode user almacenado para el perfil para redireccionarlo a su inicio dependiendo del tipo de perfil
                    Session["id_user"] = datos[2];
                    FormsAuthentication.SetAuthCookie(Login.UserName, Login.RememberMeSet);
                    FormsAuthentication.RedirectFromLoginPage(Login.UserName, Login.RememberMeSet);
                    string nombre = User.Identity.Name;
                    if (((string)Session["type"]) == "administrador")
                    {//si es admin
                        Response.Redirect("~/viewsAdmin/inicioAdmin.aspx");
                    }
                    else
                    {
                        e.Authenticated = false;
                    }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    e.Authenticated = false;
                }
                

            }
            else
            {
                //MessageLabel.Text = "deve completar todos los campos";
            }
        }
        
        protected void Login_LoginError(object sender, AuthenticateEventArgs e)
        {
            if (ViewState["LoginErrors"] == null)
                ViewState["LoginErrors"] = 0;
            int ErrorCount = (int)ViewState["LoginErrors"] + 1;
            ViewState["LoginErrors"] = ErrorCount;
            if ((ErrorCount > 3) && (Login.PasswordRecoveryUrl != string.Empty))
                Response.Redirect(Login.PasswordRecoveryUrl);
        }

    }
}
