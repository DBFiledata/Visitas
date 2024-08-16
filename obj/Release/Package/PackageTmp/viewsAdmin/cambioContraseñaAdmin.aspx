<%@ Page Language="C#" AutoEventWireup="true" Title="cambio de contraseña" MasterPageFile="~/viewsAdmin/admin.Master" CodeBehind="cambioContraseñaAdmin.aspx.cs" Inherits="paginaOficial.viewsAdmin.cambioContraseñaAdmin" %>


<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container py-1">
                <div class="d-flex align-items-center justify-content-center">
                    <div class="col-md-8  bg-dark mt-4 pb-5 pt-5 rounded">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-header">
                                        <h3 class="mb-0 my-2 text-center">Cambio de Contraseña</h3>
                                    </div>
                                    <div class="card-body">

                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" Text="Label">Contraseña Actual</asp:Label>
                                            <asp:TextBox ID="txtPass" CssClass="form-control"
                                                ToolTip="ingrese su contraseña vigente "
                                                placeholder="🔑 (Vigente)" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Label">Contraseña Nueva</asp:Label>
                                            <asp:TextBox ID="txtPassnueva" CssClass="form-control"
                                                ToolTip="Ingrese nueva su contraseña &#10;Deve contener almenos 1 letra minuscula , 1 Mayuscula y 1 Caracter especial"
                                                placeholder="🔑 (Nueva)" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" Text="Label">Verficacion de contraseña</asp:Label>
                                            <asp:TextBox ID="txtPassnueva2" CssClass="form-control" ToolTip="⟳ Repita su nueva contraseña " placeholder="🔑 (Repetir)" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>

                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <asp:Button ID="btnRegistrar" CssClass="btn btn-success btn-lg float-right mr-1" runat="server" Text="Registrar ✔" OnClick="btnRegistrar_Click" />
                                            <asp:Button ID="btnCancelar" CssClass="btn btn-danger btn-lg float-right ml-1" runat="server" Text="Cancelar ❌" OnClick="btnCancelar_Click" />
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center justify-content-center">
                                        <asp:Label ID="lblerror" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
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
