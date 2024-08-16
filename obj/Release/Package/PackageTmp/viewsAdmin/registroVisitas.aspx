<%@ Page Language="C#" Title="Registro de Visitas" MasterPageFile="~/viewsAdmin/admin.Master" AutoEventWireup="true" CodeBehind="registroVisitas.aspx.cs" Inherits="paginaOficial.viewsAdmin.registroVisitas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                        <div class="form-group d-flex align-items-center justify-content-center mt-n3">
                                            <div class="form-group col-12">
                                                <asp:Label ID="Label7" runat="server" Text="Label">Cedula</asp:Label>
                                                <asp:TextBox ID="txtId" CssClass="form-control"
                                                    ToolTip="Introdusca cedula  &#10;"
                                                    placeholder="📇" runat="server">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group d-flex justify-content-center mb-n2 mt-n2">
                                            <asp:Button ID="btnBuscarInfo" CssClass="btn btn-success btn-lg" runat="server" Text="🔎"
                                                ToolTip="Buscar informacion &#10;" OnClick="btnBuscarInfo_Click" CausesValidation="false" />
                                        </div>

                                        <div class="form-group d-flex align-items-center justify-content-center mb-n2">
                                            <div class="form-group col-12">
                                                <asp:Label ID="Label4" runat="server" Text="Label">Nombre</asp:Label>
                                                <asp:TextBox ID="txtNombre" CssClass="form-control mt-2" placeholder="📝" runat="server"></asp:TextBox>
                                                <asp:Label ID="Label2" runat="server" Text="Label">Nacionalidad</asp:Label>
                                                <asp:RadioButtonList ID="RBtnNacionalidad" runat="server" RepeatDirection="Horizontal" CellPadding="2">
                                                    <asp:ListItem>Panameña</asp:ListItem>
                                                    <asp:ListItem>Extranjera</asp:ListItem>
                                                </asp:RadioButtonList>
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
                                            <asp:TextBox ID="txtCedulaCrear" CssClass="form-control text-center" runat="server" Enabled="false"></asp:TextBox>
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
