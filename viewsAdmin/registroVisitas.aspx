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


    <asp:Panel ID="PanelInfoVisitante" runat="server">
        <div class="container py-1 pb-2 ">
            <div class="d-flex align-items-center justify-content-center">
                <div class="col-md-12  bg-dark mt-2 pb-5 pt-5 rounded">
                    <div class="row">
                        <div class="col-md-12 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-header">
                                    <h3 class=" text-center">Registro de Visitas</h3>
                                </div>
                                <div class="card-body col-12 d-flex mb-2">
                                    <div class=" col-6 mb-3">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Content/img/control1.png" class="w-100 h-100" />

                                        <asp:Button ID="btnFotoNew" runat="server" Text="📷" OnClick="btnFotoNew_Click" Visible="false"
                                            ToolTip="Actualizar foto &#10;" />
                                        <%--<asp:HiddenField ID="HiddenField1" runat="server"/>--%>
                                    </div>

                                    <div class="card-body col-6 mb-3">
                                        <div class="form-group d-flex justify-content-center mt-n3">
                                            <asp:RadioButtonList ID="rblModoVisita" runat="server" RepeatDirection="Horizontal" CellPadding="4"
                                                AutoPostBack="true" OnSelectedIndexChanged="rblModoVisita_SelectedIndexChanged">
                                                <asp:ListItem Value="individual" Selected="True">Individual</asp:ListItem>
                                                <asp:ListItem Value="grupal">Visita grupal</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <asp:Panel ID="pnlCedula" runat="server">
                                        <div class="form-group d-flex align-items-center justify-content-center mt-n3">
                                            <div class="form-group col-12">
                                                <asp:Label ID="Label7" runat="server" Text="Label">Cedula</asp:Label>
                                                <asp:TextBox ID="txtId" CssClass="form-control"
                                                    ToolTip="Introdusca cedula  &#10;"
                                                    placeholder="📇" runat="server">
                                                </asp:TextBox>
                                                <asp:HiddenField ID="hfQrData" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group d-flex justify-content-center mb-n2 mt-n2">
                                            <asp:Button ID="btnBuscarInfo" CssClass="btn btn-success btn-lg" runat="server" Text="🔎"
                                                ToolTip="Buscar informacion &#10;" OnClick="btnBuscarInfo_Click" CausesValidation="false" />
                                            <asp:Button ID="btnEscanearQR" CssClass="btn btn-primary btn-lg ml-1" runat="server" Text="📷 QR"
                                                ToolTip="Escanear cédula por QR &#10;" OnClientClick="qrScan.start(); return false;" CausesValidation="false" />
                                            <asp:Button ID="btnCancelarQR" CssClass="btn btn-danger btn-lg ml-1" runat="server" Text="Cancelar ❌"
                                                ToolTip="Cancelar escaneo &#10;" OnClientClick="qrScan.stop(); return false;" CausesValidation="false"
                                                Style="display:none;" />
                                        </div>

                                        <video id="qrScanVideo" autoplay playsinline muted style="display:none;"></video>
                                        <canvas id="qrScanCanvas" style="display:none;"></canvas>

                                        <div class="form-group d-flex align-items-center justify-content-center mb-n2">
                                            <div id="qrScanError" class="text-danger text-center small" style="display:none;"></div>
                                        </div>
                                        </asp:Panel>

                                        <div class="form-group d-flex align-items-center justify-content-center mb-n2">
                                            <div class="form-group col-12">
                                                <asp:Label ID="Label4" runat="server" Text="Label">Nombre</asp:Label>
                                                <asp:TextBox ID="txtNombre" CssClass="form-control mt-2" placeholder="📝" runat="server"></asp:TextBox>
                                                <asp:Panel ID="pnlCantidad" runat="server" Visible="false">
                                                    <asp:Label ID="lblCantidad" runat="server" Text="Cantidad de personas"></asp:Label>
                                                    <asp:TextBox ID="txtCantidad" CssClass="form-control mt-2" placeholder="👥" runat="server" TextMode="Number" min="1"></asp:TextBox>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlNacionalidad" runat="server">
                                                <asp:Label ID="Label2" runat="server" Text="Label">Nacionalidad</asp:Label>
                                                <asp:RadioButtonList ID="RBtnNacionalidad" runat="server" RepeatDirection="Horizontal" CellPadding="2">
                                                    <asp:ListItem>Panameña</asp:ListItem>
                                                    <asp:ListItem>Extranjera</asp:ListItem>
                                                </asp:RadioButtonList>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                        <div class="form-group pl-3">
                                            <asp:Label ID="Label12" runat="server" Text="Label">Area de visita</asp:Label>
                                            <asp:DropDownList ID="listTipoArea" CssClass="form-control" ToolTip="Motivo el area de visita" runat="server" Width="98%" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="listTipoAreaSelected">
                                                <asp:ListItem Value="0">📝 Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group pl-3">
                                            <asp:Label ID="Label6" runat="server" Text="Label">Lugar de visita</asp:Label>
                                            <telerik:radmultiselect ID="RadMultiSelect1" Width="98.5%" Height="45px" runat="server" DataTextField="nombre" Enabled="false" DataValueField="idtypeOfPlaces"  MaxSelectedItems="1" AutoPostBack="true" OnSelectionChanged="listTipoSelect">
                                            </telerik:radmultiselect>

                                        </div>

                                        <div class="form-group pl-3">
                                            <asp:Label ID="Label1" runat="server" Text="Label">Motivo</asp:Label>
                                            <asp:DropDownList ID="listMotivo" CssClass="form-control" ToolTip="Escoja el motivo de visita" runat="server" Width="98%" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="listMotivoSelected">
                                                <asp:ListItem Value="0">📝 Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        
                                        
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <asp:Button ID="btnRegistrar" CssClass="btn btn-success btn-lg float-right mr-1" runat="server" Text="Registrar ✔" OnClick="btnRegistrar_Click" Visible="false" />
                                            
                                        </div>
                                    </div>
                                    

                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Label ID="Label17" runat="server" Text="" Visiblefalse="false" ForeColor="Red"></asp:Label>
                                    </div>

                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


    <asp:Panel ID="PanelInfoRegitro" runat="server" Visible="false">
        <div class="container py-1 pb-2 ">
            <div class="d-flex align-items-center justify-content-center">
                <div class="col-md-12  bg-dark mt-2 pb-5 pt-5 rounded">
                    <div class="row">
                        <div class="col-md-12 mx-auto">
                            <div class="card border-secondary ">
                                <div class="card-header">
                                    <h3 class=" text-center">Registro de datos</h3>
                                </div>
                                <div class="d-flex align-items-center justify-content-center">
                                    <div class="card-body mt-n3 col-8">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Text="Label">Cedula:</asp:Label>
                                            <asp:TextBox ID="txtCedulaCrear" CssClass="form-control text-center" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" Text="Label">Nombre Compreto:</asp:Label>
                                            <asp:TextBox ID="txtNombreCrear" CssClass="form-control text-center" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" Text="Label">Nacionalidad</asp:Label>
                                            <asp:RadioButtonList ID="RBtnNacionalidadCrear" runat="server" RepeatDirection="Horizontal" CellPadding="2">
                                                <asp:ListItem>Panameña</asp:ListItem>
                                                <asp:ListItem>Extranjera</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:Label ID="Label11" runat="server" Text="Label">Sexo</asp:Label>
                                            <asp:RadioButtonList ID="RbtnSexoCrear" runat="server" RepeatDirection="Horizontal" CellPadding="2">
                                                <asp:ListItem>M</asp:ListItem>
                                                <asp:ListItem>F</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label9" runat="server" Text="Label">Fecha de nacimiento:</asp:Label>
                                            <asp:TextBox ID="txtNacimientoCrear" runat="server" CssClass="form-control text-center" Text="Label" TextMode="Date"></asp:TextBox>
                                        </div>
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <asp:Button ID="btnRegresarInfo" class="btn btn-primary mr-1" runat="server" Text="◀" OnClick="btnRegresarInfo_Click" ToolTip="Regresar  &#10;" />
                                            <asp:Button ID="btnCrearDatos" class="btn btn-primary ml-1" runat="server" Text="▶" OnClick="btnCrearDatos_Click" ToolTip="Siguiente  &#10;" />
                                            <%--OnClick="btnCrearDatos_Click"--%>
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Label ID="Label14" runat="server" Text="" Visiblefalse="false" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="PanelFotoRegistro" runat="server" Visible="false" Enabled="false">
        <div class="container py-1 pb-2 ">
            <div class="d-flex align-items-center justify-content-center">
                <div class="col-md-12  bg-dark mt-2 pb-5 pt-5 rounded">
                    <div class="row">
                        <div class="col-md-12 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-header">
                                    <h3 class=" text-center">Captura de Foto</h3>
                                </div>
                                <div class="card-body col-12 d-flex">
                                    <div class=" col-6">
                                        <h5 class=" text-center">Imagen en vivo</h5>
                                        <%--contenedor de camara en vivo--%>
                                        <asp:HiddenField ID="blobImg" runat="server" />
                                        <video id="toPick" autoplay class="w-100 h-100">
                                        </video>

                                        <%--<button id="btnFoto" onclick="TomarFoto()"></button>--%>
                                    </div>

                                    <div class="card-body col-6">
                                        <h5 class=" text-center">Foto</h5>
                                        <asp:Image ID="imgFoto" runat="server" class="w-100 h-100" />

                                    </div>

                                </div>

                                <div class="form-group d-flex align-items-center justify-content-center">
                                    <asp:Button ID="btnCancelarFoto" CssClass="btn btn-danger btn-lg float-right mr-1" runat="server" Text="Cancelar ❌" OnClick="btnCancelarFoto_Click" />
                                    <asp:Button ID="btnFoto" Text="📷" class="btn btn-primary btn-lg mr-1" runat="server" OnClientClick="TomarFoto(); return false;" />
                                    <%--<asp:Panel ID="saveFoto" runat="server" Style="display: none;">

                                        <asp:Button ID="BtnGuardarFoto" CssClass="btn btn-success btn-lg float-right mr-1" runat="server" Text="Guardar ✔" OnClick="BtnGuardarFoto_Click" />
                                    </asp:Panel>--%>

                                    <div ID="saveFoto" Style="display: none;">
                                        <asp:Button ID="BtnGuardarFoto" CssClass="btn btn-success btn-lg float-right mr-1" runat="server" Text="Guardar ✔" OnClick="BtnGuardarFoto_Click" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
