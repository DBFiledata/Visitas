using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;
using System.Net;
using Telerik.Windows.Documents.Core.Imaging.Jpeg.Encoder;
using System.Data;
using System.Globalization;
using Telerik.Web.UI;

namespace paginaOficial.viewsAdmin
{
    public partial class registroVisitas : System.Web.UI.Page
    {
        Controlers.BaseDatos odbusuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }
            this.btnRegistrar.Visible = false;
            this.btnFotoNew.Visible = false;
            if (!this.IsPostBack)
            {

                odbusuario = new Controlers.BaseDatos();
                //DataSet ds2 = odbusuario.listaAreas();
                //DataTable dt = ds2.Tables[0];
                //foreach (DataRow r in dt.Rows)
                //{
                //    listTipo.Items.Add(new ListItem(r[1].ToString(), r[0].ToString()));
                //}


                DataSet ds = odbusuario.listaTipoAreas();
                DataTable dt = ds.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    listTipoArea.Items.Add(new ListItem(r[1].ToString(), r[0].ToString()));
                }
            }
        }

        
        protected void listTipoAreaSelected(object sender, EventArgs e)
        {
            if (Convert.ToInt32(listTipoArea.SelectedValue) > 0)
            {
                //this.listTipo.Items.Clear();
                //this.listTipo.Items.Insert(0, "📝 Seleccionar");
                RadMultiSelect1.Items.Clear();
                odbusuario = new Controlers.BaseDatos();
                DataSet ds2 = odbusuario.listaAreasVisitas(Convert.ToInt32(listTipoArea.SelectedValue));
                DataTable dt = ds2.Tables[0];
                RadMultiSelect1.Items.Add(new MultiSelectItem("📝 Seleccionar", "0"));
                foreach (DataRow r in dt.Rows)
                {
                    RadMultiSelect1.Items.Add(new MultiSelectItem(r[1].ToString(), r[0].ToString()));
                }
                RadMultiSelect1.Enabled = true;
            }

            if (Convert.ToInt32(listTipoArea.SelectedValue) > 0)
            {
                this.RadMultiSelect1.Items.Clear();
                odbusuario = new Controlers.BaseDatos();
                DataSet ds = odbusuario.listaAreasVisitas(Convert.ToInt32(listTipoArea.SelectedValue));
                this.RadMultiSelect1.DataSource = ds;
                this.RadMultiSelect1.DataBind();
                RadMultiSelect1.Enable = true;
            }


        }

        

        protected void listTipoSelect(object sender, EventArgs e)
        {
            try {
                odbusuario = new Controlers.BaseDatos();
                if (RadMultiSelect1.MaxSelectedItems > 0)
            {
                     this.listMotivo.Items.Clear();
                    listMotivo.Items.Insert(0, "📝 Seleccionar");
                    DataSet ds2 = odbusuario.listaRazonesVisitas();
                    DataTable dt = ds2.Tables[0];
                    foreach (DataRow r in dt.Rows)
                    {                   
                        listMotivo.Items.Add(new ListItem(r[1].ToString(), r[0].ToString()));
                    }
                listMotivo.Enabled = true;
                this.listMotivo.DataBind();
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('error:" + ex.Message + " valor: "+ listTipoArea.SelectedValue.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Debe completar el campo (cedula)');", true);
            }
        }

        protected void listMotivoSelected(object sender, EventArgs e)
        {

            if (Convert.ToInt32(listTipoArea.SelectedValue.ToString()) > 0 & Convert.ToInt32(listMotivo.SelectedValue.ToString()) > 0)
            {
                this.btnRegistrar.Enabled = true;
                this.btnRegistrar.Visible = true;
            }
        }

        protected void btnBuscarInfo_Click(object sender, EventArgs e)
        {
            // Cadena cruda del QR (vacia si la cedula se tipeo a mano); se consume una sola vez.
            string qrData = hfQrData.Value;
            hfQrData.Value = "";
            txtId.Text = txtId.Text.Trim();
            pnlHistorial.Visible = false;

            odbusuario = new Controlers.BaseDatos();
            string[] datos = new string[7];
            datos = odbusuario.infoVisitante(this.txtId.Text);
            if (txtId.Text != string.Empty)
            {
                if (datos != null)
                {
                    this.txtNombre.Text = datos[0];
                    this.RBtnNacionalidad.SelectedValue = datos[2];
                    this.Image1.ImageUrl = "~/carnetPics/" + datos[6];
                    this.listTipoArea.Enabled = true;
                    this.btnFotoNew.Enabled = true;
                    this.btnFotoNew.Visible = true;
                    CargarHistorial(txtId.Text);
                }
                else
                {
                    // Visitante nuevo: siempre pasa por el alta (prellenada desde el QR si lo hay).
                    // No se consulta el padron: esa rama saltaba a la foto sin insertar en persons,
                    // y el flujo quedaba trabado al volver al panel principal.
                    this.PanelInfoVisitante.Visible = false;
                    this.PanelFotoRegistro.Visible = false;
                    this.PanelFotoRegistro.Enabled = false;
                    this.PanelInfoRegitro.Visible = true;
                    this.txtCedulaCrear.Text = txtId.Text;
                    PrellenarRegistroDesdeQr(qrData);
                    RadMultiSelect1.Enable = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Debe completar el campo (cedula)');", true);
            }
        }

        // Muestra el historial de visitas anteriores de la cedula solo si tiene alguna.
        private void CargarHistorial(string cedula)
        {
            odbusuario = new Controlers.BaseDatos();
            DataSet dsHistorial = odbusuario.historialVisitasPersona(cedula);
            bool tieneVisitas = dsHistorial.Tables.Count > 0 && dsHistorial.Tables[0].Rows.Count > 0;
            pnlHistorial.Visible = tieneVisitas;
            if (tieneVisitas)
            {
                gvHistorialVisitas.DataSource = dsHistorial.Tables[0];
                gvHistorialVisitas.DataBind();
            }
        }

        // Formato del QR de la cedula:
        // cedula|nombres|apellidos||sexo|pais|AAAAMMDD|nacionalidad|...
        // Si la cadena no tiene ese formato (ej. un QR con solo la cedula) no prellena nada.
        private void PrellenarRegistroDesdeQr(string qrData)
        {
            if (string.IsNullOrEmpty(qrData)) return;
            string[] partes = qrData.Split('|');
            if (partes.Length < 8) return;

            txtNombreCrear.Text = (partes[1].Trim() + " " + partes[2].Trim()).Trim();

            string sexo = partes[4].Trim().ToUpper();
            if (RbtnSexoCrear.Items.FindByValue(sexo) != null)
                RbtnSexoCrear.SelectedValue = sexo;

            DateTime nacimiento;
            string fecha = partes[6].Trim().Replace("/", "").Replace("-", "");
            if (DateTime.TryParseExact(fecha, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out nacimiento))
                txtNacimientoCrear.Text = nacimiento.ToString("yyyy-MM-dd");

            RBtnNacionalidadCrear.SelectedValue = partes[7].Trim().ToUpper().StartsWith("PANAME") ? "Panameña" : "Extranjera";
        }



        // Alterna entre registro individual y visita grupal. En modo grupal el campo Nombre
        // captura la entidad, se oculta cedula/QR/nacionalidad/foto y se pide la cantidad de
        // personas; Area/Lugar/Motivo funcionan igual (el area queda habilitada de entrada, ya
        // que no hay busqueda de persona que la habilite).
        protected void rblModoVisita_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool grupal = rblModoVisita.SelectedValue == "grupal";

            pnlCedula.Visible = !grupal;
            pnlNacionalidad.Visible = !grupal;
            pnlCantidad.Visible = grupal;
            pnlFoto.Visible = !grupal;
            pnlHistorial.Visible = false;
            Label4.Text = grupal ? "Empresa / Asociaci&oacute;n / Instituci&oacute;n" : "Nombre";

            txtId.Text = "";
            hfQrData.Value = "";
            txtNombre.Text = "";
            txtCantidad.Text = "";
            RBtnNacionalidad.ClearSelection();

            listTipoArea.ClearSelection();
            listTipoArea.Enabled = grupal;
            listMotivo.ClearSelection();
            listMotivo.Enabled = false;
            RadMultiSelect1.Items.Clear();
            RadMultiSelect1.Enabled = false;
            btnRegistrar.Visible = false;
        }

        private void RegistrarVisitaGrupal(int lugar)
        {
            string entidad = txtNombre.Text.Trim();
            int cantidad;
            if (entidad == string.Empty || !int.TryParse(txtCantidad.Text.Trim(), out cantidad) || cantidad < 1
                || listTipoArea.SelectedIndex == 0 || listMotivo.SelectedIndex == 0 || lugar == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Debe completar los campo requeridos (la cantidad de personas debe ser mayor a 0)');", true);
                return;
            }

            odbusuario = new Controlers.BaseDatos();
            if (odbusuario.crearVisitaGrupal(entidad, cantidad, Convert.ToInt32(listTipoArea.SelectedValue), lugar,
                Convert.ToInt32(listMotivo.SelectedValue), ((string)Session["id_user"])))
            {
                txtNombre.Text = "";
                txtCantidad.Text = "";
                btnRegistrar.Visible = false;
                listTipoArea.ClearSelection();
                listTipoArea.Enabled = true;
                listMotivo.ClearSelection();
                listMotivo.Enabled = false;
                RadMultiSelect1.Items.Clear();
                RadMultiSelect1.Enabled = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Visita grupal registrada satisfactoriamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se pudo registrar la visita grupal <br />-verifique si la informacion esta ingresada correctamente');", true);
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            var selectedValues = RadMultiSelect1.Value;
            int Lugar = 0;
            foreach (int value in selectedValues)
            {
                string valor = Convert.ToString( value);
                MultiSelectItem selectedItem = RadMultiSelect1.Items.FindChildByValue(valor);
                Lugar = Convert.ToInt32(selectedItem.Value);
            }

                if (rblModoVisita.SelectedValue == "grupal")
            {
                RegistrarVisitaGrupal(Lugar);
                return;
            }

                if (txtId.Text != string.Empty & txtNombre.Text != string.Empty & listTipoArea.SelectedIndex != 0 &
                listMotivo.SelectedIndex != 0 & RBtnNacionalidad.SelectedValue !=null & Lugar != 0)
            {
                //int num = Convert.ToInt32(RadMultiSelect1.Value);
                odbusuario = new Controlers.BaseDatos();
                if (odbusuario.crearVisitas(Convert.ToInt32(listTipoArea.SelectedValue), Convert.ToInt32(listMotivo.SelectedValue), Lugar, listMotivo.SelectedValue, ((string)Session["id_user"]), txtId.Text))
                {
                    txtId.Text = "";
                    txtNombre.Text = "";
                    RBtnNacionalidad.SelectedValue = null;
                    this.btnRegistrar.Visible = false;
                    this.listTipoArea.Enabled = false;
                    this.listMotivo.Enabled = false;
                    this.RadMultiSelect1.Enabled = false;
                    this.listTipoArea.ClearSelection();
                    this.listMotivo.ClearSelection();
                    this.RadMultiSelect1.Items.Clear();
                    this.btnFotoNew.Visible = false;
                    this.Image1.ImageUrl = "~/Content/img/control1.png";
                    this.pnlHistorial.Visible = false;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se pudo registrar la visita <br />-verifique si la informacion esta ingresada correctamente');", true);
                }
            }
            else
            {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Debe completar los campo requeridos');", true);
            }
        }

        protected void btnCrearDatos_Click(object sender, EventArgs e)
        {
            odbusuario = new Controlers.BaseDatos();
            if (txtCedulaCrear.Text != string.Empty & txtNombreCrear.Text != string.Empty & txtNacimientoCrear.Text != string.Empty & RBtnNacionalidadCrear.SelectedValue != null & RbtnSexoCrear.SelectedValue != null)
            {
                if (odbusuario.registroPersons(txtCedulaCrear.Text.Trim(), txtNombreCrear.Text, RBtnNacionalidadCrear.SelectedValue, txtNacimientoCrear.Text, RbtnSexoCrear.SelectedValue))
                {
                    // La cedula ahora es editable en el alta: la foto y la vuelta al panel
                    // principal usan txtId, asi que se sincroniza con lo que realmente se guardo.
                    txtId.Text = txtCedulaCrear.Text.Trim();
                    txtCedulaCrear.Text = "";
                    txtNombreCrear.Text = "";
                    txtNacimientoCrear.Text = "";
                    RBtnNacionalidadCrear.SelectedValue = null;
                    RbtnSexoCrear.SelectedValue = null;
                    this.btnCancelarFoto.Visible = false;
                    this.PanelInfoVisitante.Visible = false;
                    this.PanelFotoRegistro.Visible = true;
                    this.PanelFotoRegistro.Enabled = true;
                    this.PanelInfoRegitro.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se pudo registrar la visitante <br />-verifique si la informacion esta ingresada correctamente');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Debe completar los campo requeridos');", true);
            }
        }

        public System.Drawing.Image ConvertStringtoImage(string commands)
        {

            byte[] photoarray = Convert.FromBase64String(commands);
            MemoryStream ms = new MemoryStream(photoarray, 0, photoarray.Length);
            ms.Write(photoarray, 0, photoarray.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            return image;

        }

        protected void btnFotoNew_Click(object sender, EventArgs e)
        {
            if (Request.InputStream.Length > 0)
            {
                using (StreamReader reader = new StreamReader(Request.InputStream))
                {
                    this.PanelInfoVisitante.Visible = false;
                    this.PanelFotoRegistro.Visible = true;
                    this.PanelFotoRegistro.Enabled = true;
                    this.PanelInfoRegitro.Visible = false;
                }
            }
        }

        protected void BtnGuardarFoto_Click(object sender, EventArgs e)
        {
            string base64 = blobImg.Value;

            var image = ConvertStringtoImage(base64.Replace("data:image/png;base64,", ""));

            image.Save(Server.MapPath("~/carnetPics/" +this.txtId.Text+".jpg"));
            
            odbusuario = new Controlers.BaseDatos();
            string[] datos = new string[7];
            datos = odbusuario.infoVisitante(this.txtId.Text);
            if (datos != null)
            {
                this.txtNombre.Text = datos[0];
                this.RBtnNacionalidad.SelectedValue = datos[2];
                this.Image1.ImageUrl = "~/carnetPics/" + datos[6];
                this.btnFotoNew.Visible = true;
                //this.btnRegistrar.Visible = true;
                this.listTipoArea.Enabled = true;

                this.btnCancelarFoto.Visible = true;

                this.PanelFotoRegistro.Visible = false;
                this.PanelFotoRegistro.Enabled = false;
                this.PanelInfoRegitro.Visible = false;
                this.PanelInfoVisitante.Visible = true;
                CargarHistorial(this.txtId.Text);
            }
        }

        

        protected void btnCancelarFoto_Click(object sender, EventArgs e)
        {
            imgFoto.ImageUrl = null;
            //this.btnRegistrar.Visible = true;
            this.listTipoArea.Enabled = true;
            this.btnFotoNew.Visible = true;
            PanelFotoRegistro.Visible = false;
            PanelInfoVisitante.Visible = true;
        }

        protected void btnRegresarInfo_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            RBtnNacionalidad.SelectedValue = null;
            this.btnFotoNew.Visible = false;
            this.btnRegistrar.Visible = false;
            this.listTipoArea.Enabled = false;
            this.listMotivo.Enabled = false;
            this.RadMultiSelect1.Enabled = false;
            this.Image1.ImageUrl = "~/Content/img/control1.png";

            this.pnlHistorial.Visible = false;
            this.PanelInfoVisitante.Visible = true;
            this.PanelInfoRegitro.Visible = false;
            this.PanelFotoRegistro.Enabled = false;
            this.txtCedulaCrear.Text = "";
            this.txtNombreCrear.Text = "";
            this.txtNacimientoCrear.Text = "";
            this.RBtnNacionalidadCrear.SelectedValue= null;
            this.RbtnSexoCrear.SelectedValue = null;

        }
    }
}