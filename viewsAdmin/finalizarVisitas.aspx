<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="finalizarVisitas.aspx.cs" Title="Finalizar Visitas" MasterPageFile="~/viewsAdmin/admin.Master" Inherits="paginaOficial.viewsAdmin.finalizarVisitas" %>

<asp:Content ID="BodyContenido" ContentPlaceHolderID="contenido" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            display: none;
            width: 100%;
            height: 100%;
            overflow: hidden;
        }

        .modal-dialog {
            position: relative;
            width: auto;
        }



        .modal-dialog-scrollable {
            display: flex;
        }

        .modal-header,
        .modal-footer {
            flex-shrink: 0;
        }

        .modal-body {
            overflow-y: auto;
        }

        }

        .modal-dialog-centered {
            display: flex;
            align-items: center;
        }

        &.modal-dialog-scrollable {
            flex-direction: column;
            justify-content: center;
            height: 100%;
        }

        &::before {
            content: none;
        }


        .modal-content {
            position: relative;
            display: flex;
            flex-direction: column;
            width: 100%;
            pointer-events: auto;
        }

        .modal-backdrop {
            position: fixed;
            top: 0;
            left: 0;
            width: 100vw;
            height: 100vh;
        }

        .modal-header {
            display: flex;
            align-items: flex-start;
        }

        }

        .modal-title {
            margin-bottom: 0;
        }

        .modal-body {
            position: relative;
        }

        .modal-footer {
            display: flex;
            align-items: center;
            margin-left: .25rem;
        }



        .modal-scrollbar-measure {
            position: absolute;
            top: -9999px;
            width: 50px;
            height: 50px;
            overflow: scroll;
        }



        .modal-dialog-scrollable {
            max-height: calc(100% - #{$modal-dialog-margin-y-sm-up * 2});
            max-height: calc(100vh - #{$modal-dialog-margin-y-sm-up * 2});
        }



        .modal-dialog-centered {
            min-height: calc(100% - #{$modal-dialog-margin-y-sm-up * 2});
            height: calc(100vh - #{$modal-dialog-margin-y-sm-up * 2});
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>
    <%-- Bloque aparte: el <style> anterior tiene llaves sueltas y sintaxis SCSS que podrian arrastrar estas reglas --%>
    <style type="text/css">
        /* Paginador del listado: el GridView lo renderiza como una tabla anidada dentro de la fila del pager */
        .pager-visitas table { margin: .25rem auto; }
        .pager-visitas td { border: 0 !important; padding: 0 !important; }
        .pager-visitas a, .pager-visitas span { display: inline-block; min-width: 2.25rem; margin: .1rem; padding: .3rem .6rem;
            border: 1px solid #6c757d; border-radius: .25rem; color: #fff; text-decoration: none; }
        .pager-visitas a:hover { background-color: #495057; }
        .pager-visitas span { background-color: #007bff; border-color: #007bff; font-weight: bold; }
    </style>
    <script type="text/javascript">
        function iniciarTimer() {
            var b = $find('<%= Timer1.ClientID %>');
            if (b) {
                //b._stopTimer(); //stop the timer
                b._startTimer();//start the timer
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="HiddenField1" runat="server" />
            <ajaxtoolkit:modalpopupextender id="mpe" runat="server" popupcontrolid="Panel1" targetcontrolid="HiddenField1"
                cancelcontrolid="btnClose" oncancelscript="iniciarTimer();" backgroundcssclass="modalBackground">
            </ajaxtoolkit:modalpopupextender>

            <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick">
            </asp:Timer>

            <asp:Panel ID="Panel1" runat="server" CssClass="modal-dialog" align="center" Style="display: none; width: 600px;">

                <div class="modal-content">
                    <div class=" modal-header">
                        <h4 class="modal-title text-center">finalizar Visitas</h4>
                        <asp:Button ID="btnClose" runat="server" Text="x" class="close" aria-hidden="true" />
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-10 mx-auto">
                                <div class="card border-secondary">
                                    <div class="card-body mt-n3">
                                        <div class="form-group text-center">
                                            <h6>Desea finalizar la visita
                                            <br />
                                                #
                                                <asp:Label ID="lblIdFinalizar" runat="server" Font-Size="100px" Text="Label">#</asp:Label>
                                            </h6>
                                            <asp:Button ID="btnConfirmar" class="btn btn-primary" runat="server" Text="Aceptar ✔" OnClick="btnConfirmar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>



            <div class="card-header bg-gradient-secondary">
                <h3>Visitas</h3>
            </div>
            <div class="card-body">
                <div class="card-header bg-light d-flex flex-wrap align-items-center justify-content-between">
                    <h5 class="mb-0">Listado de Visitas Activas</h5>
                    <%-- Cierre masivo: solo visitas abiertas creadas hoy (SP finalizarVisitasDelDia) --%>
                    <asp:Button ID="btnCerrarVisitasHoy" runat="server" CssClass="btn btn-warning btn-sm my-1" Text="🔐 Cerrar visitas de hoy"
                        ToolTip="Finaliza todas las visitas abiertas registradas hoy. Las de días anteriores no se modifican."
                        OnClick="btnCerrarVisitasHoy_Click" />
                </div>
                <div class="form-group table-responsive">
                    <div class="table-responsive">
                        <%-- Paginacion del lado del servidor: el SP trae solo la pagina pedida (AllowCustomPaging + VirtualItemCount) --%>
                        <asp:GridView ID="GridView1" CssClass="table table-striped table-dark table-bordered  " Font-Size="Small" Width="100%" CellSpacing="0" runat="server" OnRowCommand="GridView1_RowCommand"
                            AllowPaging="true" AllowCustomPaging="true" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging">
                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" PageButtonCount="10" FirstPageText="« Primera" LastPageText="Última »" />
                            <PagerStyle CssClass="pager-visitas" HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="Finalizar">
                                    <ItemTemplate>

                                        <asp:Button ID="Button2" CommandName="finalisarVisita" Text="🔐" CssClass="btn btn-link" ToolTip="Finalizar Visita" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblResumenVisitas" runat="server" CssClass="d-block text-center text-muted small"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

