<%@ Page Language="C#" AutoEventWireup="true" Title="Infromacion de cuenta" MasterPageFile="~/viewsAdmin/admin.Master" CodeBehind="infoCuentaAdmin.aspx.cs" Inherits="paginaOficial.viewsAdmin.infoCuentaAdmin" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="contenido" runat="server">
    <div class="container py-1 pb-2 ">
        <div class="d-flex align-items-center justify-content-center">
            <div class="col-md-12  bg-dark mt-4 pb-5 pt-5 rounded">
                <div class="row">
                    <div class="col-md-12 mx-auto">
                        <div class="card border-secondary">
                            <div class="card-header">
                                <h3 class="mb-0 my-2 text-center">Informacion de Cuenta</h3>
                            </div>
                            <div class="card-body">
                                <div class="form-group d-flex align-items-center justify-content-center">
                                    <div class="form-group col-4">
                                        <asp:Label ID="Label4" runat="server" Text="Label">User name:</asp:Label>
                                        <asp:Label ID="lblId" CssClass="form-control text-center" runat="server" Text="Label">user name a mostar</asp:Label>
                                    </div>
                                    <div class="form-group col-4">
                                        <asp:Label ID="Label1" runat="server" Text="Label">Nombre:</asp:Label>
                                        <asp:Label ID="lblNombre" CssClass="form-control text-center" runat="server" Text="Label">nombre a mostar</asp:Label>
                                    </div>
                                    <div class="form-group col-4">
                                        <asp:Label ID="Label2" runat="server" Text="Label">Apellido:</asp:Label>
                                        <asp:Label ID="lblAapellido" runat="server" CssClass="form-control text-center" Text="Label">apellido a mostrar</asp:Label>
                                    </div>
                                </div>
                                <div class="form-group pl-3">
                                    <asp:Label ID="Label6" runat="server" Text="Label">Tipo ded usuario:</asp:Label>
                                    <asp:Label ID="lblTipo" runat="server" CssClass="form-control text-center" Text="Label">usuario estandar, tecnico,...</asp:Label>
                                </div>
                                <div class="form-group text-center">
                                    <asp:Label ID="lbl" runat="server" Text="Label">Para modificar o corregir esta informacion dirijase a edicion de perfil</asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
