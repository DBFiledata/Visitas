<%@ Page Language="C#" AutoEventWireup="true" Title="Editar Perfil" MasterPageFile="~/viewsAdmin/admin.Master" CodeBehind="editarPerfilAdmin.aspx.cs" Inherits="paginaOficial.viewsAdmin.editarPerfilAdmin" %>


<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container py-1 pb-2 ">
                <div class="d-flex align-items-center justify-content-center">
                    <div class="col-md-12  bg-dark mt-4 pb-5 pt-5 rounded">
                        <div class="row">
                            <div class="col-md-12 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-header">
                                        <h3 class=" text-center">Editar Perfil</h3>
                                    </div>
                                    <div class="card-body">
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <div class="form-group col-4">
                                                <asp:Label ID="Label7" runat="server" Text="Label">User name</asp:Label>
                                                <asp:TextBox ID="txtId" CssClass="form-control"
                                                    ToolTip="Introdusca el ID (cedula) &#10;"
                                                    placeholder="📇" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-4">
                                                <asp:Label ID="Label1" runat="server" Text="Label">Nombre</asp:Label>
                                                <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="📝" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-4">
                                                <asp:Label ID="Label2" runat="server" Text="Label">Apellido</asp:Label>
                                                <asp:TextBox ID="txtApellido" CssClass="form-control" placeholder="📝" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group pl-3">
                                            <asp:Label ID="Label6" runat="server" Text="Label">Tipo de usuario</asp:Label>
                                            <asp:DropDownList ID="listTipo" CssClass="form-control" ToolTip="👤 Escoja el tipo de usuario" runat="server" Width="98%">
                                                <asp:ListItem Value="0">👥 Seleccionar</asp:ListItem>
                                                <asp:ListItem Value="1">agente</asp:ListItem>
                                                <asp:ListItem Value="2">administrador</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <asp:Button ID="btnRegistrar" CssClass="btn btn-success btn-lg float-right mr-1" runat="server" Text="Confirmar ✔" OnClick="btnRegistrar_Click" />
                                            <asp:Button ID="btnCancelar" CssClass="btn btn-danger btn-lg float-right ml-1" runat="server" Text="Cancelar ❌" OnClick="btnCancelar_Click" CausesValidation="False" />
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Label ID="lblerror" runat="server" Text="" Visiblefalse="false" ForeColor="Red"></asp:Label>
                                    </div>

                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:RequiredFieldValidator CssClass="text-center" ID="RequiredFieldValidatorId" runat="server" ControlToValidate="txtId" ErrorMessage="Debe Ingresar un Id" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>
<%--                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorId" runat="server" ControlToValidate="txtId" ErrorMessage="El Id no concuerda con un numero de cedula valido" ForeColor="Red" ValidationExpression="^(PE|E|N|[23456789](?:AV|PI)?|1[0123]?(?:AV|PI)?)-(\d{1,4})-(\d{1,6})$" Display="None"></asp:RegularExpressionValidator>--%>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorIdNombre" runat="server" ErrorMessage="Debe ingresar el nombre" ControlToValidate="txtNombre" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorApellido" runat="server" ErrorMessage="Debe ingresar el Apellido" ControlToValidate="txtApellido" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorListTipo" runat="server" ErrorMessage="Debe seleccionar el area a la que pertenecera el usuario" ControlToValidate="listTipo" InitialValue="0" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>

                                        <asp:ValidationSummary ID="ValidationSummary" CssClass="" runat="server" ForeColor="Red" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
