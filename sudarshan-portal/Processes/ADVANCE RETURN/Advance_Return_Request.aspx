<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Advance_Return_Request.aspx.cs" Inherits="Advance_Return_Request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Bulk Approve</title>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />

    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
      <div id="divIns" runat="server" style="display:none">
            <div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent.gif" style="background-color:transparent;position:fixed; top:40%; left:46%;"/></div>
        </div>
           
        <div class="row" style="margin-top: 10px">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div class="btn-group pull-right" style="margin-top: -5px;">
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" Font-Bold="true" OnClick="btnCancel_Click" />
                        </div>
                        <h4 class="panel-title">OPEN ADVANCES</h4>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-md-2" style="text-align:right"><b>Advance Type : </b></div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlAdvType" runat="server" CssClass="form-control input-sm" onchange="bindReturnAdvance()">
                                        <asp:ListItem Value="1">Domestic/Other Advance</asp:ListItem>
                                        <asp:ListItem Value="2">Foreign Advance</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                        </div>
                        <div class="panel pagination-danger">
                            <div class="table-responsive" id="div_InvoiceDetails" runat="server" style="height: auto; overflow: hidden">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Username1" runat="server"></asp:TextBox>
            <asp:TextBox ID="lnkText" runat="server"></asp:TextBox>
            <asp:TextBox ID="ddlText" runat="server"></asp:TextBox>
            <asp:TextBox ID="ddlText1" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Condition" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Dispatch" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="split_data" runat="server"></asp:TextBox>
            <input id="ddlUser" runat="Server" type="text" />

            <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Initiator" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_designation" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="pmode" runat="server"></asp:TextBox>
            <asp:TextBox ID="plocation" runat="server"></asp:TextBox>
            <asp:TextBox ID="pageno" runat="server" Text="1"></asp:TextBox>
            <asp:TextBox ID="txtApproverEmail" runat="server"></asp:TextBox>
           <asp:TextBox ID="No_of_records" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="selected_rec" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="chk_records" runat="server"></asp:TextBox>
            <asp:TextBox ID="rec_data" runat="server"></asp:TextBox>
        </div>

        <!-- ================== BEGIN BASE JS ================== -->

        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>

        <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
        <script src="../../assets/js/demo.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>

        <script src="../../JS/Advance_Return_Request.js"></script>

             <script>
                 $(document).ready(function () {
                     App.init();
                     Demo.init();
                     PageDemo.init();
                 });
            </script>
    </form>
</body>
</html>
