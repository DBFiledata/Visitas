<%@ Page Language="C#" AutoEventWireup="true" Title="Impresiones" MasterPageFile="~/viewsAdmin/admin.Master"  CodeBehind="colaImpresion.aspx.cs" Inherits="paginaOficial.viewsAdmin.colaImpresion" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <script src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        function openModalEditar() {
            $('#modalEditar').modal('show');
        }
    </script>

    <div class="card-header bg-gradient-secondary">
        <h3 class=" text-center">Cola de Impresion</h3>
    </div>
    <div class="card-body">
        <div class="card-header bg-light">
            <asp:Label ID="Label6" runat="server" Text="Label">Estado</asp:Label>
            <asp:DropDownList ID="listTipo" CssClass="active" ToolTip="🖨 Escoja el estado de impresion" runat="server">
                <asp:ListItem Value="0">🖨 Seleccionar</asp:ListItem>
                <asp:ListItem Value="1">no impreso</asp:ListItem>
                <asp:ListItem Value="2">impreso</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label1" runat="server" Text="Label">Numero de visita</asp:Label>
            <asp:TextBox ID="txtNumero" CssClass="active" runat="server"></asp:TextBox>
            <asp:Button ID="btnfiltrar" CssClass="btn btn-success btn-lg  mr-1" ToolTip="Aplicar Filtro" runat="server" Text="🔎" OnClick="btnfiltrar_Click" />
        </div>
        <div class="form-group table-responsive">
            <div class="table-responsive">
                <asp:GridView ID="GridView1" CssClass="table table-striped table-dark table-bordered" Width="100%" CellSpacing="0" runat="server" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Imprimir">
                            <ItemTemplate>
                                <asp:Button ID="Button1" CommandName="editar" Text="🖨" CssClass="btn btn-link" ToolTip="imprimir visita" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

   
    <div id="modalEditar" class="modal fade mt-5" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog mt-5">
            <div class="modal-content">
                <div class=" modal-header">
                    <h4 class="modal-title text-center">Imprimir visita</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-10 mx-auto">
                            <div class="card border-secondary">
                                <div class="card-body mt-n3">
                                    <div class="form-group text-center">
                                        <h6>Desea imprimir la Visita
                                            <br />
                                            #
                                                <asp:Label ID="lblNumVisita" runat="server" Text="Label">nombre</asp:Label>
                                        </h6>
                                        <asp:Button ID="btnEditar" class="btn btn-primary" runat="server" Text="Aceptar ✔" OnClick="btnEditar_Click" />
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
