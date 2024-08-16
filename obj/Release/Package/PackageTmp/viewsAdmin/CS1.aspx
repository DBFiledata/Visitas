<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CS1.aspx.cs" Inherits="paginaOficial.viewsAdmin.CS1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
</head>
<body>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="Webcam_Plugin/jquery.webcam.js" type="text/javascript"></script>
    <script type="text/javascript">
        var pageUrl = "CS1.aspx";
        $(function () {
            jQuery("#webcam").webcam({
                width: 320,
                height: 240,
                mode: "save",
                swffile: "/Webcam_Plugin/jscam.swf",
                debug: function (type, status) {
                    $('#camStatus').append(type + ": " + status + '<br /><br />');
                },
                onSave: function (data) {
                    $.ajax({
                        type: "POST",
                        url: pageUrl + "/GetCapturedImage",
                        data: '',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            $("[id*=imgCapture]").css("visibility", "visible");
                            $("[id*=imgCapture]").attr("src", r.d);
                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
                },
                onCapture: function () {
                    webcam.save(pageUrl);
                }


            });
        });
        function Capture() {
            webcam.capture();
            return false;
        }
    </script>
    <form id="form1" runat="server">
        <asp:Table ID="Table1" runat="server" BorderStyle="None" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                                                    <u>Camara</u>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                                                    <u>Captura de Camara</u>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                                                    <div id="webcam">
                                                    </div>
                </asp:TableCell>
                <asp:TableCell>&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:Image ID="imgCapture" runat="server" BackColor="YellowGreen" Style="visibility: hidden; width: 320px; height: 240px" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:Button ID="btnCapture" Text="Capture" runat="server" OnClientClick="return Capture();" />
        <br />
        <span id="camStatus"></span>
    </form>
</body>
</html>
