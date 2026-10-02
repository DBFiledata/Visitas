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
            OcultarHistorial();

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
                    // Visitante ya registrado: el formulario sale de persons, pero si se escaneo
                    // el documento igual se valida contra el padron.
                    if (qrData != string.Empty)
                    {
                        ValidarQrContraPadron(qrData, ConsultarPadron(txtId.Text));
                    }
                }
                else
                {
                    // Visitante nuevo: siempre pasa por el alta, prellenada desde el QR y/o el padron.
                    // El alta la confirma el operador (registroPersons); el padron solo se lee.
                    this.PanelInfoVisitante.Visible = false;
                    this.PanelFotoRegistro.Visible = false;
                    this.PanelFotoRegistro.Enabled = false;
                    this.PanelInfoRegitro.Visible = true;
                    this.txtCedulaCrear.Text = txtId.Text;

                    DataRow padron = ConsultarPadron(txtId.Text);
                    if (qrData != string.Empty)
                    {
                        PrellenarRegistroDesdeQr(qrData);
                        ValidarQrContraPadron(qrData, padron);
                    }
                    else if (padron != null)
                    {
                        MostrarAvisoPadron("alert-info", "&#10004; Datos cargados desde el padr&oacute;n",
                            "Verifique los datos con el visitante antes de guardar.");
                    }
                    else
                    {
                        MostrarAvisoPadron("alert-secondary", "C&eacute;dula no encontrada en el padr&oacute;n",
                            "Complete los datos del visitante manualmente.");
                    }
                    // Lo que el QR no trajo (o todo, si la cedula se digito) se completa con el padron.
                    PrellenarRegistroDesdePadron(padron);
                    RadMultiSelect1.Enable = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Debe completar el campo (cedula)');", true);
            }
        }

        // Oculta todo lo que corresponde al visitante buscado (historial, alerta y aviso del padron).
        private void OcultarHistorial()
        {
            pnlHistorial.Visible = false;
            pnlAlertado.Visible = false;
            pnlPadron.Visible = false;
        }

        // Muestra el historial de visitas anteriores de la cedula solo si tiene alguna, y avisa si el
        // visitante esta alertado (tiene un incidente registrado, ver migracion 0010).
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

            MostrarAlertaVisitante(cedula);
        }

        // Banner + alert del navegador cuando la cedula tiene incidentes activos.
        private void MostrarAlertaVisitante(string cedula)
        {
            odbusuario = new Controlers.BaseDatos();
            DataSet dsAlerta = odbusuario.alertaVisitante(cedula);
            bool alertado = dsAlerta.Tables.Count > 0 && dsAlerta.Tables[0].Rows.Count > 0;
            pnlAlertado.Visible = alertado;
            if (!alertado) return;

            DataTable incidentes = dsAlerta.Tables[0];
            DataRow reciente = incidentes.Rows[0]; // el SP los devuelve del mas reciente al mas antiguo
            string detalle = "Incidente en la visita #" + reciente["visita"] + " ("
                + Convert.ToDateTime(reciente["fecha"]).ToString("dd/MM/yyyy") + "): " + reciente["descripcion"];
            if (incidentes.Rows.Count > 1)
            {
                detalle += " (y " + (incidentes.Rows.Count - 1) + " incidente(s) anterior(es))";
            }

            lblAlertado.Text = Server.HtmlEncode(detalle);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertaVisitante",
                "alert('" + HttpUtility.JavaScriptStringEncode("VISITANTE ALERTADO" + Environment.NewLine + Environment.NewLine + detalle) + "');", true);
        }

        // Historial del visitante: se marca SOLO la visita del incidente (fila resaltada + icono).
        // Las columnas auxiliares incidente/nota_incidente (las dos ultimas del SP) se ocultan por CSS.
        protected void gvHistorialVisitas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.DataItem is DataRowView))
            {
                DataRowView fila = (DataRowView)e.Row.DataItem;
                if (!fila.DataView.Table.Columns.Contains("incidente") || Convert.ToInt32(fila["incidente"]) != 1)
                {
                    return;
                }

                e.Row.CssClass = "fila-incidente";
                e.Row.ToolTip = "Incidente en esta visita: " + Convert.ToString(fila["nota_incidente"]);
                e.Row.Cells[0].Text = "<span class=\"text-danger font-weight-bold\" title=\"Visita del incidente\">⚠</span> " + e.Row.Cells[0].Text;
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
            if (TryFechaQr(partes[6], out nacimiento))
                txtNacimientoCrear.Text = nacimiento.ToString("yyyy-MM-dd");

            RBtnNacionalidadCrear.SelectedValue = partes[7].Trim().ToUpper().StartsWith("PANAME") ? "Panameña" : "Extranjera";
        }

        // ---- Padron (migracion 0011, SP consultaPadron) ----

        private DataRow ConsultarPadron(string cedula)
        {
            odbusuario = new Controlers.BaseDatos();
            DataSet ds = odbusuario.consultaPadron(cedula);
            return (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) ? ds.Tables[0].Rows[0] : null;
        }

        // Completa SOLO los campos del alta que siguen vacios: con cedula digitada llena todo, y
        // despues de un QR no pisa lo que el documento trajo.
        private void PrellenarRegistroDesdePadron(DataRow padron)
        {
            if (padron == null) return;

            if (txtNombreCrear.Text.Trim() == string.Empty)
                txtNombreCrear.Text = (CorregirEnie(Convert.ToString(padron["nombres"])) + " "
                    + CorregirEnie(Convert.ToString(padron["apellidos"]))).Trim();

            string sexo = Convert.ToString(padron["sexo"]);
            if (RbtnSexoCrear.SelectedIndex < 0 && RbtnSexoCrear.Items.FindByValue(sexo) != null)
                RbtnSexoCrear.SelectedValue = sexo;

            DateTime nacimiento;
            if (txtNacimientoCrear.Text == string.Empty && TryFechaPadron(Convert.ToString(padron["nacimiento"]), out nacimiento))
                txtNacimientoCrear.Text = nacimiento.ToString("yyyy-MM-dd");

            // Mismo criterio que el SP original datosPadron: quien esta en el padron es panameño.
            if (RBtnNacionalidadCrear.SelectedIndex < 0)
                RBtnNacionalidadCrear.SelectedValue = "Panameña";
        }

        // Compara los datos leidos del QR contra el padron y muestra el resultado. Un campo vacio
        // de cualquiera de los dos lados no se compara (no hay dato que contradiga).
        private void ValidarQrContraPadron(string qrData, DataRow padron)
        {
            string[] partes = qrData.Split('|');
            if (partes.Length < 8) return; // QR con solo la cedula: no hay datos que comparar

            if (padron == null)
            {
                MostrarAvisoPadron("alert-warning", "&#9888; C&eacute;dula no encontrada en el padr&oacute;n",
                    "No se pudieron validar los datos del documento escaneado: verif&iacute;quelos con el visitante.");
                return;
            }

            List<string> diferencias = new List<string>();
            string nombresPadron = CorregirEnie(Convert.ToString(padron["nombres"]));
            string apellidosPadron = CorregirEnie(Convert.ToString(padron["apellidos"]));
            if (!NombresCoinciden(partes[1], nombresPadron))
                diferencias.Add(Diferencia("Nombres", partes[1], nombresPadron));
            if (!NombresCoinciden(partes[2], apellidosPadron))
                diferencias.Add(Diferencia("Apellidos", partes[2], apellidosPadron));

            string sexoQr = partes[4].Trim().ToUpper();
            string sexoPadron = Convert.ToString(padron["sexo"]);
            if ((sexoQr == "M" || sexoQr == "F") && (sexoPadron == "M" || sexoPadron == "F") && sexoQr != sexoPadron)
                diferencias.Add(Diferencia("Sexo", sexoQr, sexoPadron));

            DateTime nacimientoQr, nacimientoPadron;
            if (TryFechaQr(partes[6], out nacimientoQr)
                && TryFechaPadron(Convert.ToString(padron["nacimiento"]), out nacimientoPadron)
                && nacimientoQr != nacimientoPadron)
                diferencias.Add(Diferencia("Fecha de nacimiento", nacimientoQr.ToString("dd/MM/yyyy"), nacimientoPadron.ToString("dd/MM/yyyy")));

            if (diferencias.Count == 0)
            {
                MostrarAvisoPadron("alert-success", "&#10004; Documento verificado",
                    "Los datos del QR coinciden con el padr&oacute;n.");
                return;
            }

            MostrarAvisoPadron("alert-warning", "&#9888; Los datos del QR no coinciden con el padr&oacute;n",
                string.Join("<br />", diferencias.Select(d => Server.HtmlEncode(d))));
            ScriptManager.RegisterStartupScript(this, GetType(), "padronNoCoincide",
                "alert('" + HttpUtility.JavaScriptStringEncode("LOS DATOS DEL QR NO COINCIDEN CON EL PADRÓN"
                + Environment.NewLine + Environment.NewLine + string.Join(Environment.NewLine, diferencias)) + "');", true);
        }

        private static string Diferencia(string campo, string valorQr, string valorPadron)
        {
            return campo + ": QR \"" + valorQr.Trim() + "\" / padrón \"" + valorPadron.Trim() + "\"";
        }

        // titulo y detalle ya vienen como HTML seguro (los datos externos se codifican antes).
        private void MostrarAvisoPadron(string claseAlerta, string titulo, string detalle)
        {
            pnlPadron.Visible = true;
            pnlPadronAviso.CssClass = "alert shadow-sm mb-0 " + claseAlerta;
            lblPadron.Text = "<h6 class=\"alert-heading mb-1\">" + titulo + "</h6>" + detalle;
        }

        // Coinciden si todas las palabras de uno estan en el otro: el QR o el padron pueden traer
        // solo el primer nombre/apellido, o el apellido de casada. Sin dato de un lado = no se compara.
        private static bool NombresCoinciden(string a, string b)
        {
            string[] palabrasA = NormalizarNombre(a);
            string[] palabrasB = NormalizarNombre(b);
            if (palabrasA.Length == 0 || palabrasB.Length == 0) return true;
            return !palabrasA.Except(palabrasB).Any() || !palabrasB.Except(palabrasA).Any();
        }

        // Mayusculas, sin tildes ni enie, separado en palabras.
        private static string[] NormalizarNombre(string valor)
        {
            string descompuesto = CorregirEnie(valor ?? "").Normalize(System.Text.NormalizationForm.FormD);
            var sinTildes = new System.Text.StringBuilder();
            foreach (char c in descompuesto)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sinTildes.Append(c);
            }
            return sinTildes.ToString().ToUpperInvariant()
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        }

        // En el padron parte de las enies quedaron guardadas como el byte 0xB1, que se lee como '±'.
        // Se usa la mayuscula si la letra vecina esta en mayuscula (PE±A -> PEÑA, Gudi±o -> Gudiño).
        private static string CorregirEnie(string valor)
        {
            if (string.IsNullOrEmpty(valor) || valor.IndexOf('±') < 0) return valor;
            char[] letras = valor.ToCharArray();
            for (int i = 0; i < letras.Length; i++)
            {
                if (letras[i] != '±') continue;
                char vecina = (i > 0 && char.IsLetter(letras[i - 1])) ? letras[i - 1]
                    : (i + 1 < letras.Length ? letras[i + 1] : 'a');
                letras[i] = char.IsUpper(vecina) ? 'Ñ' : 'ñ';
            }
            return new string(letras);
        }

        // QR: AAAAMMDD (se toleran separadores).
        private static bool TryFechaQr(string valor, out DateTime fecha)
        {
            return DateTime.TryParseExact(valor.Trim().Replace("/", "").Replace("-", ""), "yyyyMMdd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);
        }

        // Padron: MM/dd/yyyy (o vacio).
        private static bool TryFechaPadron(string valor, out DateTime fecha)
        {
            return DateTime.TryParseExact(valor.Trim(), new[] { "MM/dd/yyyy", "M/d/yyyy" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);
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
            OcultarHistorial();
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
                    OcultarHistorial();

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

            OcultarHistorial();
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