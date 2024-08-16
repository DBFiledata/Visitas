<%@ Page Language="C#" AutoEventWireup="true" Title="Estadisticas diarias" MasterPageFile="~/viewsAdmin/admin.Master" CodeBehind="estadisticasDiarias.aspx.cs" Inherits="paginaOficial.viewsAdmin.estadisticasDiarias" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <%--<div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="10000">
            </asp:Timer>

        </ContentTemplate>
    </asp:UpdatePanel>--%>



    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>--%>
    <div class="container py-1 pb-2 ">
        <div class="d-flex align-items-center justify-content-center">
            <div class="col-md-12  bg-dark mt-2 pb-5 pt-5 rounded">
                <div class="row">
                    <div class="col-md-12 mx-auto">
                        <div class="card border-secondary">
                            <div class="card-header">
                                <h3 class=" text-center">Estadisticas Diarias</h3>
                            </div>
                            <div class="card-body">

                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-xl-12 col-md-12 mb-2">
                                            <div class="card border-left-primary shadow h-100 py-2">
                                                <div class="card-body">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2">
                                                            <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">No se han generado visitas para el dia de hoy</div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <i class="fa fa-ticket-alt fa-2x text-primary"></i>
                                                            <i class="fas fa-calendar fa-2x text-primary"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="row">
                                        <!-- Earnings (Monthly) Card Example -->
                                        <div class="col-xl-12 col-md-12 mb-2">
                                            <div class="card border-left-primary shadow h-100 py-2">
                                                <div class="card-body">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2">
                                                            <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">Visitas totales para el dia de hoy</div>
                                                            <div class="h5 mb-0 font-weight-bold text-gray-800">
                                                                <asp:Label ID="labelVisitas" runat="server" Text=""> visitas del dia de hoy</asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <i class="fa fa-users fa-2x text-gray-300"></i>
                                                            <i class="fas fa-calendar fa-2x text-gray-300"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group d-flex align-items-center justify-content-center">
                                            <div class="form-group col-6">
                                                <telerik:RadHtmlChart ID="pieChartArea" runat="server">
                                                </telerik:RadHtmlChart>
                                            </div>
                                            <div class="form-group col-6">
                                                <telerik:RadHtmlChart runat="server" ID="BarChart1" Transitions="true">
                                                </telerik:RadHtmlChart>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
