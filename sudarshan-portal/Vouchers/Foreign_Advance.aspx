<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Foreign_Advance.aspx.cs" Inherits="Foreign_Advance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Foreign Advance Voucher</title>
    <meta name="viewport" content="width=device-width, minimum-scale=1.0, maximum-scale=1.0" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style.min.css" rel="stylesheet" />

    <script type="text/javascript">
        window.setTimeout(function () {
            $("#alert_message").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 3000);
    </script>
     <script type = "text/javascript">
         function PrintPanel() {
             var panel = document.getElementById("<%=pnlContents.ClientID %>");
             var printWindow = window.open('', '', 'height=400,width=700');             
             printWindow.document.write('<html><head>');
             printWindow.document.write('<style type="text/css">.clsBorder {font-weight: bold;border: 1px solid black;height: 25px;padding-top: 2px;}.clsBorder1 {border: 1px solid black;height: 25px;padding-top: 2px;} .td1 {width: 10%;}.td2 { width: 20%;}</style>');
             printWindow.document.write('<link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />');
             printWindow.document.write('<link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />');
             printWindow.document.write('<link href="../assets/css/style.min.css" rel="stylesheet" />');
             printWindow.document.write('</head><body >');
             printWindow.document.write(panel.innerHTML);
             printWindow.document.write('</body></html>');
             printWindow.document.close();
             setTimeout(function () {
                 printWindow.print();
             }, 500);
             return false;
         }
    </script>
    <style type="text/css">
        .clsBorder {
            font-weight: bold;
            border: 1px solid black;
            height: 25px;
            padding-top: 2px;
        }
        .clsBorder1 {
            border: 1px solid black;
            height: 25px;
            padding-top: 2px;
        }
        .td1 {
            width: 10%;
        }

        .td2 {
            width: 20%;
        }
    </style>

</head>

<body class="pace-top" style="overflow-x: hidden">
    <form id="frm1" runat="server">
     <asp:Panel id="pnlContents" runat = "server">
    <div class="row" style="border: 1px solid white; background: #ffffff;">
        <table style="background: #ffffff; padding-top: 7px; height:20px">
            <tr>
            <td style="width:25%; text-align:center">
                <img src="../Img/133649.png" style="width:150px; height:25px">
            </td>
            <td style="width:75%; text-align:center">
                    <font style="font-size:larger; font-weight:bold">SUDARSHAN CHEMICAL INDUSTRIES LIMITED</font>
                <br />
                <font style="font-size:medium; font-weight:bold">Foreign Travel Advance Requisition</font>
                    
                
            </td>
           </tr>
        </table>
        <div class="row">
            
                <table style="font-size:smaller; margin-left:5%; width:90%; text-align:center">
                    <tbody>
                        <tr>
                            <td colspan="4"></td>
                            <td>Date : </td>
                            <td><span id="spn_date" runat="server"></span></td>
                        </tr>
                        <tr>
                            <td class="clsBorder td1" style="border: 1px solid black;">Unique No.</td>
                            <td class="clsBorder td2" style="border: 1px solid black;"><span id="spn_Unq_No" runat="server"></span></td>

                            <td class="clsBorder td1" style="border: 1px solid black;">Document Type</td>
                            <td class="clsBorder td2" style="border: 1px solid black;"><span id="spn_Doc_Type" runat="server">HA</span></td>

                            <td class="clsBorder td1" style="border: 1px solid black;">Document No.</td>
                            <td class="clsBorder td2" style="border: 1px solid black;"><span id="spn_Doc_No" runat="server"></span></td>
                        </tr>
                        <tr>
                            <td class="clsBorder td1" style="border: 1px solid black;">Employee Code</td>
                            <td class="clsBorder td2" style="border: 1px solid black;"><span id="spn_Emp_Code" runat="server"></span></td>

                            <td class="clsBorder td1" style="border: 1px solid black;">Employee Name</td>
                            <td class="clsBorder  td2" style="border: 1px solid black;"><span id="spn_Emp_Name" runat="server"></span></td>
                        </tr>
                        
                    </tbody>
                </table>
            <br />
                <div id="tbl_Data" runat="server"  style="font-size:smaller; margin-left:5%"></div>
          
            <br />
            <br />
            <br />
            <table style="width:90%">
                <tr style="font-weight: bold; text-align: center">
                    <td class="col-md-1"></td>
                    <td class="col-md-2"><span id="spn_Emp_Name2" runat="server"></span></td>
                    <td class="col-md-2"></td>
                    <td class="col-md-2"><span id="spn_Approver" runat="server"></span></td>
                    <td class="col-md-2"></td>
                    <td class="col-md-2" id="tdDev1" runat="server"><span id="spn_Dev_Approver" runat="server"></span></td>
                </tr>
                <tr style="text-align: center">
                    <td class="col-md-1"></td>
                    <td class="col-md-2">Prepared By</td>
                    <td class="col-md-2"></td>
                    <td class="col-md-2">Approved By</td>
                    <td class="col-md-2"></td>
                    <td class="col-md-2" id="tdDev2" runat="server">Approved Under Deviation</td>
                </tr>
            </table>
            <br />
		<p style="font-weight:bold; width:100%; text-align:center">This is a system generated voucher and does not required signature.</p>
            <div style="width:100%; border-top:1px dotted black"></div>
            <br />
            <div class="col-md-12">
                <div  style='width: 90%; margin-left:5%; font-size:smaller' id="div_audit" runat="server"></div>
            </div>
        </div>

    </div>
    <!-- ================== BEGIN BASE JS ================== -->
    <script src="../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script src="../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <!-- ================== END PAGE LEVEL JS ================== -->
</asp:Panel>
        <div class="row" style="text-align:right">
            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick = "return PrintPanel();" />
        </div>
        </form>
</body>

</html>
