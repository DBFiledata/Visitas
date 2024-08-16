<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/viewsAdmin/admin.Master" Title="Historial de visitas" CodeBehind="buscarVisita.aspx.cs" Inherits="paginaOficial.viewsAdmin.buscarVisita" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <script src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        function openModalInfo() {
            $('#modalInfo').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModalEditar() {
            $('#modalEditar').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModalEliminar() {
            $('#modalEliminar').modal('show');
        }
    </script>


    <div class="card-header bg-gradient-secondary">
        <h3 class=" text-center">Historial de Visitas</h3>
    </div>
    <div class="card-body">
        <div class="card-header bg-light">
            <asp:Label ID="Label6" runat="server" Text="Label">Area</asp:Label>
            <asp:DropDownList ID="listTipo" CssClass="active" ToolTip="Escoja el area de visita" runat="server" AutoPostBack="true" OnSelectedIndexChanged="listTipoAreaSelected">
                <asp:ListItem Value="0">📝 Seleccionar</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label9" runat="server" Text="Label">Lugar</asp:Label>
            <asp:DropDownList ID="listLugar" CssClass="active" ToolTip="Escoja el lugar de visita" runat="server" Enabled="false">
                <asp:ListItem Value="0">📝 Seleccionar</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label7" runat="server" Text="Label">Desde</asp:Label>
            <asp:TextBox ID="txtDesde" CssClass="active" runat="server" ToolTip="📅 inicio de rango de busqueda" TextMode="Date"></asp:TextBox>
            <asp:Label ID="Label8" runat="server" Text="Label">Hasta</asp:Label>
            <asp:TextBox ID="TxtHasta" CssClass="active" runat="server" ToolTip="📅 final de rago de busqueda" TextMode="Date"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Label">#Visita</asp:Label>
            <asp:TextBox ID="txtNumero" CssClass="active" runat="server"></asp:TextBox>
            <asp:Button ID="btnfiltrar" CssClass="btn btn-success btn-lg  mr-1" ToolTip="Aplicar Filtro" runat="server" Text="🔎" OnClick="btnfiltrar_Click" />
            <asp:Button ID="btnReporte" CssClass="btn btn-primary btn-lg  mr-1" ToolTip="Generar Reporte" runat="server" Text="📥" OnClick="btnReporte_Click" />



        </div>
        <div class="form-group table-responsive">
            <div class="table-responsive">
                <asp:GridView ID="GridView1" CssClass="table table-striped table-dark table-bordered" Width="100%" CellSpacing="0" runat="server" OnRowCommand="GridView1_RowCommand">
                    <%--<Columns>
                        <asp:TemplateField HeaderText="Manteniniento">
                            <ItemTemplate>
                                <asp:Button ID="Button1" CommandName="info" Text="👁‍🗨" CssClass="btn btn-link" ToolTip="Informacion detallada del usuario" runat="server" />

                                <asp:Button ID="Button2" CommandName="editar" Text="📝" CssClass="btn btn-link" ToolTip="Editar informacion del usuario" runat="server" />

                                <asp:Button ID="Button3" CommandName="eliminar" Text="🧺" CssClass="btn btn-link" ToolTip="Eliminar cuenta del usuario" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>--%>
                </asp:GridView>
            </div>
        </div>
    </div>

    <div id="modalInfo" class="modal fade mt-5" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog mt-5">
            <div class="modal-content">
                <div class=" modal-header">
                    <h4 class="modal-title text-center" id="myModalLabels">Informacion de Cuenta</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" Text="Label">Id:</asp:Label>
                                        <asp:Label ID="lblIdInfo" CssClass="form-control text-center" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label13" runat="server" Text="Label">Nombre:</asp:Label>
                                        <asp:Label ID="lblNombreInfo" CssClass="form-control text-center" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label25" runat="server" Text="Label">Tipo ded usuario:</asp:Label>
                                        <asp:Label ID="lblTipoInfo" runat="server" CssClass="form-control text-center" Text="Label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="modalEditar" class="modal fade mt-1 col-12" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-center" id="myModalLabel">Edicion de Perfil</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body mt-n3">
                    <div id="modal-content">
                        <div class="row">
                            <div class="col-md-12 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="Label">Id:</asp:Label>
                                            <asp:TextBox ID="txtIdEditar" CssClass="form-control text-center" runat="server">Id a mostar</asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" Text="Label">Nombre:</asp:Label>
                                            <asp:TextBox ID="txtNombreEditar" CssClass="form-control text-center" Text="Nombre a mostar" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Text="Label">Apellido:</asp:Label>
                                            <asp:TextBox ID="txtApellidoEditar" CssClass="form-control text-center" runat="server">Apellido a mostar</asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <div class="form-group pl-3">
                                                <asp:Label ID="Label5" runat="server" Text="Label">Tipo ded usuario</asp:Label>
                                                <asp:DropDownList ID="listTipoEditar" CssClass="form-control" ToolTip="👷‍ Escoja el tipo de usuario" runat="server">
                                                    <asp:ListItem Value="0">👥 Seleccionar</asp:ListItem>
                                                    <asp:ListItem Value="1">agente</asp:ListItem>
                                                    <asp:ListItem Value="2">administrador</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <asp:Button ID="btnEditar" class="btn btn-primary" runat="server" Text="Editar ✔" OnClick="btnEditar_Click" />
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Label ID="lblerror" runat="server" Text="" Visiblefalse="false" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modalEliminar" class="modal fade mt-5" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog mt-5">
            <div class="modal-content">
                <div class=" modal-header">
                    <h4 class="modal-title text-center">Eliminar de Perfil</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-10 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group text-center">
                                        <h6>Desea elimiar   al usuario
                                            <br />
                                            <asp:Label ID="lblIdEliminar" runat="server" Text="Label">Cedula</asp:Label><br />
                                            Con Nombre:
                                                <asp:Label ID="lblNombreEliminar" runat="server" Text="Label">nombre</asp:Label>
                                        </h6>
                                        <asp:Button ID="btnEiminaruenta" class="btn btn-primary" runat="server" Text="Aceptar ✔" OnClick="btnEiminaruenta_Click" />
                                        <asp:Button ID="btnCancelar" class="btn btn-secondary" runat="server" Text="Cancelar ❌" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

