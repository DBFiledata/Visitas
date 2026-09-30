<%@ Page Language="C#" Title="Registro de Visitas" MasterPageFile="~/viewsAdmin/admin.Master" AutoEventWireup="true" CodeBehind="registroVisitas.aspx.cs" Inherits="paginaOficial.viewsAdmin.registroVisitas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script src="../Scripts/jsQR.min.js"></script>
    <script type="text/javascript">
        var pageUrl = "registroVisitas.aspx";
        (async function () {
            await navigator.mediaDevices.getUserMedia({ video: true }).then((mediaStream) => {
                document.getElementById("toPick").srcObject = mediaStream;

                const track = mediaStream.getVideoTracks()[0];
                //imageCapture = new ImageCapture(track);
            });
            //const userVideoTrack = userVideo.getVideoTracks()[0];
        })();

        async function TomarFoto() {
            await navigator.mediaDevices.getUserMedia({ video: true }).then(async (mediaStream) => {
                const track = mediaStream.getVideoTracks()[0];
                const photoBlob = await new ImageCapture(track).takePhoto();
                var img = document.getElementById("contenido_imgFoto");
                img.src = URL.createObjectURL(photoBlob);

                var up = document.getElementById('contenido_blobImg');

                var reader = new FileReader();
                reader.readAsDataURL(photoBlob);
                reader.onloadend = function () {
                    var base64data = reader.result;
                    up.value = base64data;
                }
            });


            document.getElementById("saveFoto").style.display = 'block';
        }

        var qrScan = {
            stream: null,
            rafId: null,
            video: null,
            canvas: null,
            ctx: null,

            start: function () {
                var self = this;
                var errorEl = document.getElementById('qrScanError');
                var btnScan = document.getElementById('<%= btnEscanearQR.ClientID %>');
                var btnCancel = document.getElementById('<%= btnCancelarQR.ClientID %>');

                errorEl.style.display = 'none';
                errorEl.textContent = '';

                if (!navigator.mediaDevices || !navigator.mediaDevices.getUserMedia || typeof jsQR !== 'function') {
                    errorEl.textContent = 'El escaneo de QR no es compatible con este navegador. Ingrese la cédula manualmente.';
                    errorEl.style.display = 'block';
                    return;
                }

                this.video = document.getElementById('qrScanVideo');
                this.canvas = document.getElementById('qrScanCanvas');
                this.ctx = this.canvas.getContext('2d');

                navigator.mediaDevices.getUserMedia({ video: { facingMode: 'environment' } })
                    .then(function (mediaStream) {
                        self.stream = mediaStream;
                        self.video.srcObject = mediaStream;
                        self.video.play();
                        btnScan.style.display = 'none';
                        btnCancel.style.display = 'inline-block';
                        self.rafId = requestAnimationFrame(function () { self.tick(); });
                    })
                    .catch(function () {
                        errorEl.textContent = 'No se pudo acceder a la cámara. Verifique los permisos del navegador.';
                        errorEl.style.display = 'block';
                        self.stop();
                    });
            },

            // La cedula codifica el texto en ISO-8859-1 (Á=193, Ñ=209), no UTF-8: jsQR asume UTF-8
            // en code.data y lo mutila. Se decodifica desde los bytes crudos: UTF-8 estricto si es
            // valido, y si no Latin-1 (cada byte es directamente un code point).
            decode: function (code) {
                var bytes = code.binaryData;
                if (!bytes || !bytes.length) return code.data;
                try {
                    return new TextDecoder('utf-8', { fatal: true }).decode(new Uint8Array(bytes));
                } catch (e) {
                    var s = '';
                    for (var i = 0; i < bytes.length; i++) s += String.fromCharCode(bytes[i]);
                    return s;
                }
            },

            tick: function () {
                var self = this;

                if (this.video && this.video.readyState === this.video.HAVE_ENOUGH_DATA) {
                    this.canvas.width = this.video.videoWidth;
                    this.canvas.height = this.video.videoHeight;
                    this.ctx.drawImage(this.video, 0, 0, this.canvas.width, this.canvas.height);

                    var imageData = this.ctx.getImageData(0, 0, this.canvas.width, this.canvas.height);
                    var code = jsQR(imageData.data, imageData.width, imageData.height, { inversionAttempts: 'dontInvert' });

                    if (code && code.data) {
                        var qrText = this.decode(code);
                        // El QR trae "cedula|nombres|apellidos||sexo|pais|AAAAMMDD|nacionalidad|...":
                        // la cedula va al campo, la cadena completa la parsea el servidor.
                        document.getElementById('<%= hfQrData.ClientID %>').value = qrText;
                        document.getElementById('<%= txtId.ClientID %>').value = qrText.split('|')[0].trim();
                        this.stop();
                        document.getElementById('<%= btnBuscarInfo.ClientID %>').click();
                        return;
                    }
                }

                this.rafId = requestAnimationFrame(function () { self.tick(); });
            },

            stop: function () {
                if (this.rafId) {
                    cancelAnimationFrame(this.rafId);
                    this.rafId = null;
                }
                if (this.stream) {
                    this.stream.getTracks().forEach(function (t) { t.stop(); });
                    this.stream = null;
                }
                if (this.video) {
                    this.video.srcObject = null;
                }

                var btnScan = document.getElementById('<%= btnEscanearQR.ClientID %>');
                var btnCancel = document.getElementById('<%= btnCancelarQR.ClientID %>');
                if (btnScan) btnScan.style.display = 'inline-block';
                if (btnCancel) btnCancel.style.display = 'none';
            }
        };

        window.addEventListener('pagehide', function () { qrScan.stop(); });

    </script>


    <style>
        /* Radios en linea, con la altura de un form-control para alinearse con los inputs vecinos */
        .radio-campo { display: flex; align-items: center; min-height: calc(1.5em + .75rem + 2px); }
        .radio-campo input { margin-right: .35rem; }
        .radio-campo label { margin: 0 1.25rem 0 0; cursor: pointer; font-weight: 500; }

        /* Selector de modalidad como pestañas (los radios quedan ocultos, la etiqueta hace de pestaña) */
        .tabs-modo { display: flex; margin-bottom: -1px; }
        .tabs-modo input { position: absolute; opacity: 0; width: 0; height: 0; }
        .tabs-modo label { margin: 0 .25rem 0 0; padding: .5rem 1.25rem; cursor: pointer; font-weight: 500; color: #007bff;
            border: 1px solid transparent; border-radius: .25rem .25rem 0 0; }
        .tabs-modo label:hover { border-color: #e9ecef #e9ecef #dee2e6; }
        .tabs-modo input:checked + label { color: #495057; background-color: #fff; border-color: #dee2e6 #dee2e6 #fff; }
        .tabs-modo input:focus-visible + label { outline: 2px solid #80bdff; }
        .reg-section-title { text-transform: uppercase; letter-spacing: .05em; font-size: .8rem; color: #6c757d; border-bottom: 1px solid #dee2e6; padding-bottom: .35rem; margin-bottom: 1rem; }
    </style>
    <asp:Panel ID="PanelInfoVisitante" runat="server">
        <div class="container py-2">
            <div class="bg-dark rounded p-3">
                <div class="card border-secondary shadow-sm">
                    <div class="card-header pb-0 d-flex flex-wrap align-items-end justify-content-between">
                        <h3 class="mb-2">Registro de Visitas</h3>
                        <asp:RadioButtonList ID="rblModoVisita" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="tabs-modo"
                            AutoPostBack="true" OnSelectedIndexChanged="rblModoVisita_SelectedIndexChanged">
                            <asp:ListItem Value="individual" Selected="True">Individual</asp:ListItem>
                            <asp:ListItem Value="grupal">Visita grupal</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="card-body">
                        <div class="row">

                            <%-- Foto del visitante (solo registro individual) --%>
                            <asp:Panel ID="pnlFoto" runat="server" CssClass="col-md-4 text-center mb-3">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Content/img/control1.png" CssClass="img-fluid rounded border" />
                                <asp:Button ID="btnFotoNew" runat="server" Text="📷 Actualizar foto" CssClass="btn btn-outline-secondary btn-sm mt-2" OnClick="btnFotoNew_Click" Visible="false"
                                    ToolTip="Actualizar foto &#10;" />
                            </asp:Panel>

                            <div class="col-md">

                                <h6 class="reg-section-title">Datos del visitante</h6>

                                <%-- Cedula + busqueda + QR (solo registro individual) --%>
                                <asp:Panel ID="pnlCedula" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" CssClass="font-weight-bold" AssociatedControlID="txtId" Text="Cedula"></asp:Label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtId" CssClass="form-control" ToolTip="Introdusca cedula  &#10;" placeholder="📇 8-000-000" runat="server"></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:Button ID="btnBuscarInfo" CssClass="btn btn-success" runat="server" Text="🔎 Buscar"
                                                    ToolTip="Buscar informacion &#10;" OnClick="btnBuscarInfo_Click" CausesValidation="false" />
                                                <asp:Button ID="btnEscanearQR" CssClass="btn btn-primary" runat="server" Text="📷 QR"
                                                    ToolTip="Escanear cédula por QR &#10;" OnClientClick="qrScan.start(); return false;" CausesValidation="false" />
                                                <asp:Button ID="btnCancelarQR" CssClass="btn btn-danger" runat="server" Text="Cancelar ❌"
                                                    ToolTip="Cancelar escaneo &#10;" OnClientClick="qrScan.stop(); return false;" CausesValidation="false"
                                                    Style="display:none;" />
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hfQrData" runat="server" />
                                        <div id="qrScanError" class="text-danger small mt-1" style="display:none;"></div>
                                        <video id="qrScanVideo" autoplay playsinline muted style="display:none;"></video>
                                        <canvas id="qrScanCanvas" style="display:none;"></canvas>
                                    </div>
                                </asp:Panel>

                                <div class="form-row">
                                    <div class="form-group col-md-7">
                                        <asp:Label ID="Label4" runat="server" CssClass="font-weight-bold" AssociatedControlID="txtNombre" Text="Nombre"></asp:Label>
                                        <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="📝" runat="server"></asp:TextBox>
                                    </div>
                                    <%-- Solo visita grupal --%>
                                    <asp:Panel ID="pnlCantidad" runat="server" Visible="false" CssClass="form-group col-md-5">
                                        <asp:Label ID="lblCantidad" runat="server" CssClass="font-weight-bold" AssociatedControlID="txtCantidad" Text="Cantidad de personas"></asp:Label>
                                        <asp:TextBox ID="txtCantidad" CssClass="form-control" placeholder="👥" runat="server" TextMode="Number" min="1"></asp:TextBox>
                                    </asp:Panel>
                                    <%-- Solo registro individual --%>
                                    <asp:Panel ID="pnlNacionalidad" runat="server" CssClass="form-group col-md-5">
                                        <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold d-block mb-2" Text="Nacionalidad"></asp:Label>
                                        <asp:RadioButtonList ID="RBtnNacionalidad" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio-campo">
                                            <asp:ListItem>Panameña</asp:ListItem>
                                            <asp:ListItem>Extranjera</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </asp:Panel>
                                </div>

                                <h6 class="reg-section-title mt-2">Destino de la visita</h6>

                                <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <asp:Label ID="Label12" runat="server" CssClass="font-weight-bold" AssociatedControlID="listTipoArea" Text="Area de visita"></asp:Label>
                                        <asp:DropDownList ID="listTipoArea" CssClass="form-control" ToolTip="Motivo el area de visita" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="listTipoAreaSelected">
                                            <asp:ListItem Value="0">📝 Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <asp:Label ID="Label6" runat="server" CssClass="font-weight-bold d-block mb-2" Text="Lugar de visita"></asp:Label>
                                        <telerik:radmultiselect ID="RadMultiSelect1" Width="100%" Height="38px" runat="server" DataTextField="nombre" Enabled="false" DataValueField="idtypeOfPlaces" MaxSelectedItems="1" AutoPostBack="true" OnSelectionChanged="listTipoSelect">
                                        </telerik:radmultiselect>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <asp:Label ID="Label1" runat="server" CssClass="font-weight-bold" AssociatedControlID="listMotivo" Text="Motivo"></asp:Label>
                                        <asp:DropDownList ID="listMotivo" CssClass="form-control" ToolTip="Escoja el motivo de visita" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="listMotivoSelected">
                                            <asp:ListItem Value="0">📝 Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <asp:Label ID="Label17" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer text-right">
                        <asp:Button ID="btnRegistrar" CssClass="btn btn-success btn-lg" runat="server" Text="Registrar ✔" OnClick="btnRegistrar_Click" Visible="false" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%-- Historial de visitas anteriores: solo se muestra si la persona buscada ya tiene visitas --%>
    <asp:Panel ID="pnlHistorial" runat="server" Visible="false">
        <div class="container py-1 pb-2">
            <div class="col-md-12 bg-dark rounded p-3">
                <h5 class="text-white text-center">Historial de visitas anteriores</h5>
                <asp:GridView ID="gvHistorialVisitas" runat="server" CssClass="table table-striped table-dark table-bordered" Width="100%"></asp:GridView>
            </div>
        </div>
    </asp:Panel>


    <asp:Panel ID="PanelInfoRegitro" runat="server" Visible="false">
        <div class="container py-2">
            <div class="bg-dark rounded p-3">
                <div class="card border-secondary shadow-sm">
                    <div class="card-header">
                        <h3 class="mb-0">Registro de datos</h3>
                        <small class="text-muted">Visitante nuevo: complete sus datos personales</small>
                    </div>
                    <div class="card-body">
                        <h6 class="reg-section-title">Datos personales</h6>
                        <div class="form-row">
                            <div class="form-group col-md-5">
                                <asp:Label ID="Label3" runat="server" CssClass="font-weight-bold" AssociatedControlID="txtCedulaCrear" Text="Cedula"></asp:Label>
                                <asp:TextBox ID="txtCedulaCrear" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-7">
                                <asp:Label ID="Label5" runat="server" CssClass="font-weight-bold" AssociatedControlID="txtNombreCrear" Text="Nombre completo"></asp:Label>
                                <asp:TextBox ID="txtNombreCrear" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <asp:Label ID="Label8" runat="server" CssClass="font-weight-bold d-block mb-2" Text="Nacionalidad"></asp:Label>
                                <asp:RadioButtonList ID="RBtnNacionalidadCrear" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio-campo">
                                    <asp:ListItem>Panameña</asp:ListItem>
                                    <asp:ListItem>Extranjera</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="form-group col-md-4">
                                <asp:Label ID="Label11" runat="server" CssClass="font-weight-bold d-block mb-2" Text="Sexo"></asp:Label>
                                <asp:RadioButtonList ID="RbtnSexoCrear" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio-campo">
                                    <asp:ListItem>M</asp:ListItem>
                                    <asp:ListItem>F</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="form-group col-md-4">
                                <asp:Label ID="Label9" runat="server" CssClass="font-weight-bold" AssociatedControlID="txtNacimientoCrear" Text="Fecha de nacimiento"></asp:Label>
                                <asp:TextBox ID="txtNacimientoCrear" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Label ID="Label14" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    </div>
                    <div class="card-footer d-flex justify-content-between">
                        <asp:Button ID="btnRegresarInfo" CssClass="btn btn-outline-secondary" runat="server" Text="◀ Regresar" OnClick="btnRegresarInfo_Click" ToolTip="Regresar  &#10;" />
                        <asp:Button ID="btnCrearDatos" CssClass="btn btn-success" runat="server" Text="Guardar y continuar ▶" OnClick="btnCrearDatos_Click" ToolTip="Siguiente  &#10;" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="PanelFotoRegistro" runat="server" Visible="false" Enabled="false">
        <div class="container py-2">
            <div class="bg-dark rounded p-3">
                <div class="card border-secondary shadow-sm">
                    <div class="card-header">
                        <h3 class="mb-0">Captura de Foto</h3>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <h6 class="reg-section-title">Imagen en vivo</h6>
                                <%--contenedor de camara en vivo--%>
                                <asp:HiddenField ID="blobImg" runat="server" />
                                <video id="toPick" autoplay class="w-100 rounded border"></video>
                            </div>
                            <div class="col-md-6 mb-3">
                                <h6 class="reg-section-title">Foto</h6>
                                <asp:Image ID="imgFoto" runat="server" CssClass="w-100 rounded border" />
                            </div>
                        </div>
                    </div>
                    <div class="card-footer d-flex flex-wrap justify-content-center align-items-center">
                        <asp:Button ID="btnCancelarFoto" CssClass="btn btn-danger btn-lg mr-2" runat="server" Text="Cancelar ❌" OnClick="btnCancelarFoto_Click" />
                        <asp:Button ID="btnFoto" Text="📷 Tomar foto" CssClass="btn btn-primary btn-lg mr-2" runat="server" OnClientClick="TomarFoto(); return false;" />
                        <div id="saveFoto" style="display: none;">
                            <asp:Button ID="BtnGuardarFoto" CssClass="btn btn-success btn-lg" runat="server" Text="Guardar ✔" OnClick="BtnGuardarFoto_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
