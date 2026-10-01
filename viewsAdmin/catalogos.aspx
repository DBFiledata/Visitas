<%@ Page Language="C#" AutoEventWireup="true" Title="Catálogos" MasterPageFile="~/viewsAdmin/admin.Master" CodeBehind="catalogos.aspx.cs" Inherits="paginaOficial.viewsAdmin.catalogos" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        // El code-behind abre los modales con openModal{Crear|Editar|Eliminar}{Area|Lugar|Motivo}()
        ['Area', 'Lugar', 'Motivo'].forEach(function (cat) {
            ['Crear', 'Editar', 'Eliminar'].forEach(function (accion) {
                window['openModal' + accion + cat] = function () { $('#modal' + accion + cat).modal('show'); };
            });
        });

        // Filtro de busqueda en el navegador sobre las filas de la grilla (ignora la columna Acciones)
        function filtrarTabla(input, tableId) {
            var q = input.value.toLowerCase();
            var filas = document.getElementById(tableId).rows;
            for (var i = 1; i < filas.length; i++) {
                var texto = '';
                for (var j = 1; j < filas[i].cells.length; j++) texto += ' ' + filas[i].cells[j].textContent;
                filas[i].style.display = texto.toLowerCase().indexOf(q) === -1 ? 'none' : '';
            }
        }

        // Recuerda la pestaña activa: cada alta/edicion/baja es un postback y recargaba siempre en "Área"
        window.addEventListener('load', function () {
            try {
                var guardada = sessionStorage.getItem('catalogosTab');
                if (guardada) $('#tabsCatalogos a[href="' + guardada + '"]').tab('show');
                $('#tabsCatalogos a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                    sessionStorage.setItem('catalogosTab', $(e.target).attr('href'));
                });
            } catch (ex) { }
        });
    </script>
    <style>
        .reg-section-title { text-transform: uppercase; letter-spacing: .05em; font-size: .8rem; color: #6c757d; border-bottom: 1px solid #dee2e6; padding-bottom: .35rem; margin-bottom: 1rem; }
        /* Pestañas en el encabezado de la tarjeta, la activa se funde con el cuerpo (mismo estilo que Registro de Visitas) */
        .tabs-catalogo { border-bottom: 0; margin-bottom: -1px; }
        .tabs-catalogo .nav-link { margin: 0 .25rem 0 0; padding: .5rem 1.25rem; font-weight: 500; color: #007bff; border: 1px solid transparent; border-radius: .25rem .25rem 0 0; }
        .tabs-catalogo .nav-link:hover { border-color: #e9ecef #e9ecef #dee2e6; }
        .tabs-catalogo .nav-link.active { color: #495057; background-color: #fff; border-color: #dee2e6 #dee2e6 #fff; }
        .tabla-catalogo th { text-transform: capitalize; font-size: .8rem; letter-spacing: .04em; color: #6c757d; background: #f8f9fa; border-top: 0; white-space: nowrap; }
        .tabla-catalogo td { vertical-align: middle; }
        .tabla-catalogo th:first-child, .tabla-catalogo td:first-child { width: 1%; white-space: nowrap; }
        .tabla-catalogo th:nth-child(2), .tabla-catalogo td:nth-child(2) { width: 80px; }
        /* Lugar: el id del Area padre es dato interno (el code-behind lo lee), se muestra solo el nombre del Area */
        #tab-lugar .tabla-catalogo th:nth-child(6), #tab-lugar .tabla-catalogo td:nth-child(6) { display: none; }
    </style>

    <div class="container py-2">
        <div class="bg-dark rounded p-3">
            <div class="card border-secondary shadow-sm">
                <div class="card-header pb-0 d-flex flex-wrap align-items-end justify-content-between">
                    <h3 class="mb-2">Catálogos</h3>
                    <ul class="nav nav-tabs tabs-catalogo" id="tabsCatalogos" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="tab-area-link" data-toggle="tab" href="#tab-area" role="tab" aria-controls="tab-area" aria-selected="true">Áreas</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="tab-lugar-link" data-toggle="tab" href="#tab-lugar" role="tab" aria-controls="tab-lugar" aria-selected="false">Lugares</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="tab-motivo-link" data-toggle="tab" href="#tab-motivo" role="tab" aria-controls="tab-motivo" aria-selected="false">Motivos</a>
                        </li>
                    </ul>
                </div>
                <div class="card-body">
                    <div class="tab-content" id="tabsCatalogosContent">

            <!-- Pestaña Área -->
            <div class="tab-pane fade show active" id="tab-area" role="tabpanel" aria-labelledby="tab-area-link">
                <h6 class="reg-section-title">Listado de áreas</h6>
                <div class="d-flex flex-wrap justify-content-between align-items-center mb-3">
                    <input type="search" class="form-control mb-2 mb-md-0" style="max-width: 320px;" placeholder="Buscar en áreas..."
                        oninput="filtrarTabla(this, '<%= GridViewArea.ClientID %>')" />
                    <button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalCrearArea">+ Crear área</button>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GridViewArea" CssClass="table table-hover table-striped tabla-catalogo mb-0" Width="100%" CellSpacing="0" GridLines="None"
                        EmptyDataText="No hay registros activos." runat="server" OnRowCommand="GridViewArea_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditarArea" CommandName="editar" Text="Editar" CssClass="btn btn-outline-primary btn-sm" ToolTip="Editar área" runat="server" />
                                    <asp:Button ID="btnEliminarArea" CommandName="eliminar" Text="Dar de baja" CssClass="btn btn-outline-danger btn-sm ml-1" ToolTip="Dar de baja el área" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- Pestaña Lugar -->
            <div class="tab-pane fade" id="tab-lugar" role="tabpanel" aria-labelledby="tab-lugar-link">
                <h6 class="reg-section-title">Listado de lugares</h6>
                <div class="d-flex flex-wrap justify-content-between align-items-center mb-3">
                    <input type="search" class="form-control mb-2 mb-md-0" style="max-width: 320px;" placeholder="Buscar en lugares..."
                        oninput="filtrarTabla(this, '<%= GridViewLugar.ClientID %>')" />
                    <button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalCrearLugar">+ Crear lugar</button>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GridViewLugar" CssClass="table table-hover table-striped tabla-catalogo mb-0" Width="100%" CellSpacing="0" GridLines="None"
                        EmptyDataText="No hay registros activos." runat="server" OnRowCommand="GridViewLugar_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditarLugar" CommandName="editar" Text="Editar" CssClass="btn btn-outline-primary btn-sm" ToolTip="Editar lugar" runat="server" />
                                    <asp:Button ID="btnEliminarLugar" CommandName="eliminar" Text="Dar de baja" CssClass="btn btn-outline-danger btn-sm ml-1" ToolTip="Dar de baja el lugar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- Pestaña Motivo -->
            <div class="tab-pane fade" id="tab-motivo" role="tabpanel" aria-labelledby="tab-motivo-link">
                <h6 class="reg-section-title">Listado de motivos</h6>
                <div class="d-flex flex-wrap justify-content-between align-items-center mb-3">
                    <input type="search" class="form-control mb-2 mb-md-0" style="max-width: 320px;" placeholder="Buscar en motivos..."
                        oninput="filtrarTabla(this, '<%= GridViewMotivo.ClientID %>')" />
                    <button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalCrearMotivo">+ Crear motivo</button>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GridViewMotivo" CssClass="table table-hover table-striped tabla-catalogo mb-0" Width="100%" CellSpacing="0" GridLines="None"
                        EmptyDataText="No hay registros activos." runat="server" OnRowCommand="GridViewMotivo_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditarMotivo" CommandName="editar" Text="Editar" CssClass="btn btn-outline-primary btn-sm" ToolTip="Editar motivo" runat="server" />
                                    <asp:Button ID="btnEliminarMotivo" CommandName="eliminar" Text="Dar de baja" CssClass="btn btn-outline-danger btn-sm ml-1" ToolTip="Dar de baja el motivo" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Crear Área -->
    <div id="modalCrearArea" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="tituloCrearArea" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-light">
                    <h5 class="modal-title" id="tituloCrearArea">Crear área</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="font-weight-bold" for="<%= txtNombreCrearArea.ClientID %>">Nombre</label>
                        <asp:TextBox ID="txtNombreCrearArea" CssClass="form-control" MaxLength="150" runat="server"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblErrorCrearArea" runat="server" Text="" CssClass="text-danger small d-block"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearArea" CssClass="btn btn-success" runat="server" Text="Crear" OnClick="btnCrearArea_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Editar Área -->
    <div id="modalEditarArea" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="tituloEditarArea" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-light">
                    <h5 class="modal-title" id="tituloEditarArea">Editar área</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="font-weight-bold mb-0">Id</label>
                        <asp:Label ID="lblIdEditarArea" CssClass="form-control-plaintext font-weight-bold" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-group">
                        <label class="font-weight-bold" for="<%= txtNombreEditarArea.ClientID %>">Nombre</label>
                        <asp:TextBox ID="txtNombreEditarArea" CssClass="form-control" MaxLength="150" runat="server"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblErrorEditarArea" runat="server" Text="" CssClass="text-danger small d-block"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarEditarArea" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardarEditarArea_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Dar de baja Área -->
    <div id="modalEliminarArea" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="tituloEliminarArea" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-light">
                    <h5 class="modal-title" id="tituloEliminarArea">Dar de baja el área</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <p class="mb-2">¿Desea dar de baja el área?</p>
                    <p class="mb-2"><strong><asp:Label ID="lblNombreEliminarArea" runat="server" Text=""></asp:Label></strong>
                        <span class="text-muted">(Id <asp:Label ID="lblIdEliminarArea" runat="server" Text=""></asp:Label>)</span></p>
                    <p class="text-muted small mb-0">Dejará de aparecer al registrar visitas, pero el historial de visitas que ya lo usan se conserva.</p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCancelarEliminarArea" CssClass="btn btn-secondary" runat="server" Text="Cancelar" data-dismiss="modal" OnClientClick="return false;" />
                    <asp:Button ID="btnConfirmarEliminarArea" CssClass="btn btn-danger" runat="server" Text="Dar de baja" OnClick="btnConfirmarEliminarArea_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Crear Lugar -->
    <div id="modalCrearLugar" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="tituloCrearLugar" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-light">
                    <h5 class="modal-title" id="tituloCrearLugar">Crear lugar</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="font-weight-bold" for="<%= txtNombreCrearLugar.ClientID %>">Nombre</label>
                        <asp:TextBox ID="txtNombreCrearLugar" CssClass="form-control" MaxLength="150" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="font-weight-bold" for="<%= ddlAreaCrearLugar.ClientID %>">Área</label>
                        <asp:DropDownList ID="ddlAreaCrearLugar" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <asp:Label ID="lblErrorCrearLugar" runat="server" Text="" CssClass="text-danger small d-block"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearLugar" CssClass="btn btn-success" runat="server" Text="Crear" OnClick="btnCrearLugar_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Editar Lugar -->
    <div id="modalEditarLugar" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="tituloEditarLugar" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-light">
                    <h5 class="modal-title" id="tituloEditarLugar">Editar lugar</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="font-weight-bold mb-0">Id</label>
                        <asp:Label ID="lblIdEditarLugar" CssClass="form-control-plaintext font-weight-bold" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-group">
                        <label class="font-weight-bold" for="<%= txtNombreEditarLugar.ClientID %>">Nombre</label>
                        <asp:TextBox ID="txtNombreEditarLugar" CssClass="form-control" MaxLength="150" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="font-weight-bold" for="<%= ddlAreaEditarLugar.ClientID %>">Área</label>
                        <asp:DropDownList ID="ddlAreaEditarLugar" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <asp:Label ID="lblErrorEditarLugar" runat="server" Text="" CssClass="text-danger small d-block"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarEditarLugar" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardarEditarLugar_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Dar de baja Lugar -->
    <div id="modalEliminarLugar" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="tituloEliminarLugar" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-light">
                    <h5 class="modal-title" id="tituloEliminarLugar">Dar de baja el lugar</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <p class="mb-2">¿Desea dar de baja el lugar?</p>
                    <p class="mb-2"><strong><asp:Label ID="lblNombreEliminarLugar" runat="server" Text=""></asp:Label></strong>
                        <span class="text-muted">(Id <asp:Label ID="lblIdEliminarLugar" runat="server" Text=""></asp:Label>)</span></p>
                    <p class="text-muted small mb-0">Dejará de aparecer al registrar visitas, pero el historial de visitas que ya lo usan se conserva.</p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCancelarEliminarLugar" CssClass="btn btn-secondary" runat="server" Text="Cancelar" data-dismiss="modal" OnClientClick="return false;" />
                    <asp:Button ID="btnConfirmarEliminarLugar" CssClass="btn btn-danger" runat="server" Text="Dar de baja" OnClick="btnConfirmarEliminarLugar_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Crear Motivo -->
    <div id="modalCrearMotivo" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="tituloCrearMotivo" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-light">
                    <h5 class="modal-title" id="tituloCrearMotivo">Crear motivo</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="font-weight-bold" for="<%= txtNombreCrearMotivo.ClientID %>">Nombre</label>
                        <asp:TextBox ID="txtNombreCrearMotivo" CssClass="form-control" MaxLength="75" runat="server"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblErrorCrearMotivo" runat="server" Text="" CssClass="text-danger small d-block"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearMotivo" CssClass="btn btn-success" runat="server" Text="Crear" OnClick="btnCrearMotivo_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Editar Motivo -->
    <div id="modalEditarMotivo" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="tituloEditarMotivo" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-light">
                    <h5 class="modal-title" id="tituloEditarMotivo">Editar motivo</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="font-weight-bold mb-0">Id</label>
                        <asp:Label ID="lblIdEditarMotivo" CssClass="form-control-plaintext font-weight-bold" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-group">
                        <label class="font-weight-bold" for="<%= txtNombreEditarMotivo.ClientID %>">Nombre</label>
                        <asp:TextBox ID="txtNombreEditarMotivo" CssClass="form-control" MaxLength="75" runat="server"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblErrorEditarMotivo" runat="server" Text="" CssClass="text-danger small d-block"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarEditarMotivo" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardarEditarMotivo_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Dar de baja Motivo -->
    <div id="modalEliminarMotivo" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="tituloEliminarMotivo" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-light">
                    <h5 class="modal-title" id="tituloEliminarMotivo">Dar de baja el motivo</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <p class="mb-2">¿Desea dar de baja el motivo?</p>
                    <p class="mb-2"><strong><asp:Label ID="lblNombreEliminarMotivo" runat="server" Text=""></asp:Label></strong>
                        <span class="text-muted">(Id <asp:Label ID="lblIdEliminarMotivo" runat="server" Text=""></asp:Label>)</span></p>
                    <p class="text-muted small mb-0">Dejará de aparecer al registrar visitas, pero el historial de visitas que ya lo usan se conserva.</p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCancelarEliminarMotivo" CssClass="btn btn-secondary" runat="server" Text="Cancelar" data-dismiss="modal" OnClientClick="return false;" />
                    <asp:Button ID="btnConfirmarEliminarMotivo" CssClass="btn btn-danger" runat="server" Text="Dar de baja" OnClick="btnConfirmarEliminarMotivo_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
