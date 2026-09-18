<%@ Page Language="C#" AutoEventWireup="true" Title="Inicio" MasterPageFile="~/viewsAdmin/admin.Master" CodeBehind="inicioAdmin.aspx.cs" Inherits="paginaOficial.viewsAdmin.inicioAdmin" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <header class="masthead bg-info text-white text-center align-items-stretch h-auto">
        <div class="container d-flex align-items-center flex-column">

            <!-- Masthead Avatar Image -->
            <img class="mb-4" style="margin-top: -120px;" height="300" width="380" src="../Content/img/control2.png"alt="" />
            <!-- Masthead Heading -->
            <h1 class="masthead-heading text-uppercase mb-0">Le damos la bienvenida</h1>

            <!-- Icon Divider -->
            <div class="divider-custom divider-light">
                <div class="divider-custom-line"></div>
                <div class="divider-custom-icon">
                    <i class="fa fa-users"></i>
                    <i class="fa fa-shield-alt"></i>
                    <i class="fa fa-desktop"></i>
                </div>

                <div class="divider-custom-line"></div>
            </div>

            <!-- Masthead Subheading -->
            <p class="masthead-subheading font-weight-light mb-0">Sistema de registro de visitantes</p>

        </div>
    </header>

    <div class="container mt-4">
        <h3>Últimas visitas</h3>
        <asp:GridView ID="gvUltimasVisitas" CssClass="table table-striped table-dark table-bordered" Width="100%" runat="server" EmptyDataText="Sin visitas recientes">
        </asp:GridView>
    </div>
</asp:Content>
