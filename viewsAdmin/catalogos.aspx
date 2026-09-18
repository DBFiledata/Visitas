<%@ Page Language="C#" AutoEventWireup="true" Title="Catalogos" MasterPageFile="~/viewsAdmin/admin.Master" CodeBehind="catalogos.aspx.cs" Inherits="paginaOficial.viewsAdmin.catalogos" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        function openModalCrearArea() {
            $('#modalCrearArea').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModalEditarArea() {
            $('#modalEditarArea').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModalEliminarArea() {
            $('#modalEliminarArea').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModalCrearLugar() {
            $('#modalCrearLugar').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModalEditarLugar() {
            $('#modalEditarLugar').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModalEliminarLugar() {
            $('#modalEliminarLugar').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModalCrearMotivo() {
            $('#modalCrearMotivo').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModalEditarMotivo() {
            $('#modalEditarMotivo').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModalEliminarMotivo() {
            $('#modalEliminarMotivo').modal('show');
        }
    </script>

    <div class="card-header bg-gradient-secondary">
        <h3 class="text-center">Catalogos</h3>
    </div>
    <div class="card-body">

        <ul class="nav nav-tabs" id="tabsCatalogos" role="tablist">
            <li class="nav-item">
                <a class="nav-link active" id="tab-area-link" data-toggle="tab" href="#tab-area" role="tab" aria-controls="tab-area" aria-selected="true">Area</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="tab-lugar-link" data-toggle="tab" href="#tab-lugar" role="tab" aria-controls="tab-lugar" aria-selected="false">Lugar</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" id="tab-motivo-link" data-toggle="tab" href="#tab-motivo" role="tab" aria-controls="tab-motivo" aria-selected="false">Motivo</a>
            </li>
        </ul>

        <div class="tab-content mt-3" id="tabsCatalogosContent">

            <!-- Tab Area (funcional - Slice 1) -->
            <div class="tab-pane fade show active" id="tab-area" role="tabpanel" aria-labelledby="tab-area-link">
                <div class="card-header bg-light d-flex justify-content-between align-items-center">
                    <span>Areas</span>
                    <button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalCrearArea">Crear Area</button>
                </div>
                <div class="form-group table-responsive">
                    <div class="table-responsive">
                        <asp:GridView ID="GridViewArea" CssClass="table table-striped table-dark table-bordered" Width="100%" CellSpacing="0" runat="server" OnRowCommand="GridViewArea_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditarArea" CommandName="editar" Text="📝" CssClass="btn btn-link" ToolTip="Editar Area" runat="server" />

                                        <asp:Button ID="btnEliminarArea" CommandName="eliminar" Text="🧺" CssClass="btn btn-link" ToolTip="Dar de baja el Area" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <!-- Tab Lugar (funcional - Slice 2) -->
            <div class="tab-pane fade" id="tab-lugar" role="tabpanel" aria-labelledby="tab-lugar-link">
                <div class="card-header bg-light d-flex justify-content-between align-items-center">
                    <span>Lugares</span>
                    <button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalCrearLugar">Crear Lugar</button>
                </div>
                <div class="form-group table-responsive">
                    <div class="table-responsive">
                        <asp:GridView ID="GridViewLugar" CssClass="table table-striped table-dark table-bordered" Width="100%" CellSpacing="0" runat="server" OnRowCommand="GridViewLugar_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditarLugar" CommandName="editar" Text="📝" CssClass="btn btn-link" ToolTip="Editar Lugar" runat="server" />

                                        <asp:Button ID="btnEliminarLugar" CommandName="eliminar" Text="🧺" CssClass="btn btn-link" ToolTip="Dar de baja el Lugar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <!-- Tab Motivo (funcional - Slice 3) -->
            <div class="tab-pane fade" id="tab-motivo" role="tabpanel" aria-labelledby="tab-motivo-link">
                <div class="card-header bg-light d-flex justify-content-between align-items-center">
                    <span>Motivos</span>
                    <button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalCrearMotivo">Crear Motivo</button>
                </div>
                <div class="form-group table-responsive">
                    <div class="table-responsive">
                        <asp:GridView ID="GridViewMotivo" CssClass="table table-striped table-dark table-bordered" Width="100%" CellSpacing="0" runat="server" OnRowCommand="GridViewMotivo_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditarMotivo" CommandName="editar" Text="📝" CssClass="btn btn-link" ToolTip="Editar Motivo" runat="server" />

                                        <asp:Button ID="btnEliminarMotivo" CommandName="eliminar" Text="🧺" CssClass="btn btn-link" ToolTip="Dar de baja el Motivo" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <!-- Modal Crear Area -->
    <div id="modalCrearArea" class="modal fade mt-5" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog mt-5">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-center" id="myModalLabelCrearArea">Crear Area</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group">
                                        <label>Nombre:</label>
                                        <asp:TextBox ID="txtNombreCrearArea" CssClass="form-control text-center" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Button ID="btnCrearArea" CssClass="btn btn-primary" runat="server" Text="Crear ✔" OnClick="btnCrearArea_Click" />
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Label ID="lblErrorCrearArea" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Editar Area -->
    <div id="modalEditarArea" class="modal fade mt-1 col-12" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-center" id="myModalLabelEditarArea">Editar Area</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body mt-n3">
                    <div class="row">
                        <div class="col-md-12 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group">
                                        <label>Id:</label>
                                        <asp:Label ID="lblIdEditarArea" CssClass="form-control text-center" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <label>Nombre:</label>
                                        <asp:TextBox ID="txtNombreEditarArea" CssClass="form-control text-center" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Button ID="btnGuardarEditarArea" CssClass="btn btn-primary" runat="server" Text="Guardar ✔" OnClick="btnGuardarEditarArea_Click" />
                                    </div>
                                </div>
                                <div class="form-group d-flex align-items-center justify-content-center">
                                    <asp:Label ID="lblErrorEditarArea" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Eliminar Area -->
    <div id="modalEliminarArea" class="modal fade mt-5" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog mt-5">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-center" id="myModalLabelEliminarArea">Dar de baja Area</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-10 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group text-center">
                                        <h6>Desea dar de baja el area
                                            <br />
                                            <asp:Label ID="lblIdEliminarArea" runat="server" Text="Label"></asp:Label><br />
                                            Con Nombre:
                                                <asp:Label ID="lblNombreEliminarArea" runat="server" Text="Label"></asp:Label>
                                        </h6>
                                        <asp:Button ID="btnConfirmarEliminarArea" CssClass="btn btn-primary" runat="server" Text="Aceptar ✔" OnClick="btnConfirmarEliminarArea_Click" />
                                        <asp:Button ID="btnCancelarEliminarArea" CssClass="btn btn-secondary" runat="server" Text="Cancelar ❌" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Crear Lugar -->
    <div id="modalCrearLugar" class="modal fade mt-5" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog mt-5">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-center" id="myModalLabelCrearLugar">Crear Lugar</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group">
                                        <label>Nombre:</label>
                                        <asp:TextBox ID="txtNombreCrearLugar" CssClass="form-control text-center" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Area:</label>
                                        <asp:DropDownList ID="ddlAreaCrearLugar" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Button ID="btnCrearLugar" CssClass="btn btn-primary" runat="server" Text="Crear ✔" OnClick="btnCrearLugar_Click" />
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Label ID="lblErrorCrearLugar" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Editar Lugar -->
    <div id="modalEditarLugar" class="modal fade mt-1 col-12" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-center" id="myModalLabelEditarLugar">Editar Lugar</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body mt-n3">
                    <div class="row">
                        <div class="col-md-12 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group">
                                        <label>Id:</label>
                                        <asp:Label ID="lblIdEditarLugar" CssClass="form-control text-center" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <label>Nombre:</label>
                                        <asp:TextBox ID="txtNombreEditarLugar" CssClass="form-control text-center" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Area:</label>
                                        <asp:DropDownList ID="ddlAreaEditarLugar" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Button ID="btnGuardarEditarLugar" CssClass="btn btn-primary" runat="server" Text="Guardar ✔" OnClick="btnGuardarEditarLugar_Click" />
                                    </div>
                                </div>
                                <div class="form-group d-flex align-items-center justify-content-center">
                                    <asp:Label ID="lblErrorEditarLugar" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Eliminar Lugar -->
    <div id="modalEliminarLugar" class="modal fade mt-5" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog mt-5">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-center" id="myModalLabelEliminarLugar">Dar de baja Lugar</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-10 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group text-center">
                                        <h6>Desea dar de baja el lugar
                                            <br />
                                            <asp:Label ID="lblIdEliminarLugar" runat="server" Text="Label"></asp:Label><br />
                                            Con Nombre:
                                                <asp:Label ID="lblNombreEliminarLugar" runat="server" Text="Label"></asp:Label>
                                        </h6>
                                        <asp:Button ID="btnConfirmarEliminarLugar" CssClass="btn btn-primary" runat="server" Text="Aceptar ✔" OnClick="btnConfirmarEliminarLugar_Click" />
                                        <asp:Button ID="btnCancelarEliminarLugar" CssClass="btn btn-secondary" runat="server" Text="Cancelar ❌" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Crear Motivo -->
    <div id="modalCrearMotivo" class="modal fade mt-5" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog mt-5">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-center" id="myModalLabelCrearMotivo">Crear Motivo</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group">
                                        <label>Nombre:</label>
                                        <asp:TextBox ID="txtNombreCrearMotivo" CssClass="form-control text-center" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Button ID="btnCrearMotivo" CssClass="btn btn-primary" runat="server" Text="Crear ✔" OnClick="btnCrearMotivo_Click" />
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Label ID="lblErrorCrearMotivo" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Editar Motivo -->
    <div id="modalEditarMotivo" class="modal fade mt-1 col-12" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-center" id="myModalLabelEditarMotivo">Editar Motivo</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body mt-n3">
                    <div class="row">
                        <div class="col-md-12 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group">
                                        <label>Id:</label>
                                        <asp:Label ID="lblIdEditarMotivo" CssClass="form-control text-center" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <label>Nombre:</label>
                                        <asp:TextBox ID="txtNombreEditarMotivo" CssClass="form-control text-center" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Button ID="btnGuardarEditarMotivo" CssClass="btn btn-primary" runat="server" Text="Guardar ✔" OnClick="btnGuardarEditarMotivo_Click" />
                                    </div>
                                </div>
                                <div class="form-group d-flex align-items-center justify-content-center">
                                    <asp:Label ID="lblErrorEditarMotivo" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Eliminar Motivo -->
    <div id="modalEliminarMotivo" class="modal fade mt-5" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog mt-5">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-center" id="myModalLabelEliminarMotivo">Dar de baja Motivo</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-10 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group text-center">
                                        <h6>Desea dar de baja el motivo
                                            <br />
                                            <asp:Label ID="lblIdEliminarMotivo" runat="server" Text="Label"></asp:Label><br />
                                            Con Nombre:
                                                <asp:Label ID="lblNombreEliminarMotivo" runat="server" Text="Label"></asp:Label>
                                        </h6>
                                        <asp:Button ID="btnConfirmarEliminarMotivo" CssClass="btn btn-primary" runat="server" Text="Aceptar ✔" OnClick="btnConfirmarEliminarMotivo_Click" />
                                        <asp:Button ID="btnCancelarEliminarMotivo" CssClass="btn btn-secondary" runat="server" Text="Cancelar ❌" />
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
