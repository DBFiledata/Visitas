<%@ Page Language="C#" AutoEventWireup="true" Title="busqueda de Usuario" MasterPageFile="~/viewsAdmin/admin.Master" CodeBehind="buscarUsuario.aspx.cs" Inherits="paginaOficial.viewsAdmin.buscarUsuario" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <div class="container py-5">
        <div class="d-flex align-items-center justify-content-center">
            <div class="col-md-8  bg-dark mt-4 pb-5 rounded">
                <div class="col-12 user-img d-flex align-items-center justify-content-center mt-4">
                    <img class="imagenPequeña" src="../userscontent/img/logoAsamblea.png" />
                </div>
                <h2 class="text-center mb-1 mt-1 text-white">Asamblea Nacional</h2>
                <div class="row">
                    <div class="col-md-10 mx-auto">
                        <div class="card border-secondary">
                            <div class="card-header">
                                <h3 class=" text-center">Busqueda de Usuario</h3>
                            </div>
                            <div class="card-body">

                                <div class="form-group">
                                    <h3 class=" text-center pt-2">ID de Usuario</h3>
                                    <asp:TextBox ID="txtId" CssClass="form-control text-center"
                                        Width="95%"
                                        ToolTip="👨‍💼 Deve ingresar el ID del usuario a buscar &#10;✔Al encontarlo puede realizar los siguientes procesos: &#10;🛠edicion o  🗑eliminacion"
                                        placeholder="👨‍💼" runat="server">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group d-flex align-items-center justify-content-center">
                                    <asp:Button ID="btnRegistrar" CssClass="btn btn-success btn-lg float-right mr-1" runat="server" Text="Buscar🔎" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
