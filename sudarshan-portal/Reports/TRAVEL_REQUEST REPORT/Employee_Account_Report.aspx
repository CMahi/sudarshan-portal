<%@ Page ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true" Language="C#" Async="true" CodeFile="Employee_Account_Report.aspx.cs" Inherits="Employee_Account_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Vendor Listing Report</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />

      <!-- ================== END BASE CSS STYLE ================== -->
</head>
<body style="overflow-x: hidden">
    <form id="frm_report" runat="server">
        <%--<div id="page-container" class="fade page-container page-header-fixed page-sidebar-fixed page-with-two-sidebar page-with-footer">
        --%>

        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                 	<div class="btn-group pull-right" style="margin-top: -5px;">
                            <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_Export_onClick" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" style="margin-left:10px" OnClick="btnCancel_Click" />
                        </div>
                        <h3 class="panel-title"><b>Employee Account Report	</b></h3>
                    </div>
</div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="col-md-2 control-label">Voucher Type</label>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlVoucherType" runat="server" class="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlVoucherType_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-12">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        
                        <h4 class="panel-title">Employee Account Details</h4>
                    </div>
                    <div class="panel-body">
                        <div class="panel pagination-success">
                            <div id="divdata" runat="server" class="table-responsive"></div>
                        </div>
                    </div>
                     

                </div>
            </div>
        </div>

        <div id="conditionButton" style="display: none">
            <asp:TextBox ID="txt_DomainName" runat="server" Width="0.5px"></asp:TextBox>
            <asp:TextBox ID="txt_procedure" runat="server" Width="0.5px"></asp:TextBox>
            <asp:TextBox ID="txt_SearchString" runat="server" Width="0.5px" Text="%"></asp:TextBox>
            <asp:TextBox ID="txtCondition" runat="server" Width="0.5px"></asp:TextBox>
            <asp:TextBox ID="txtParameterID" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txtCreatedBy" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_PONO" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txtdata" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server" Width="1px"></asp:TextBox>
        </div>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-1.9.1.min.js"></script>
        <script src="../../assets/plugins/jquery/jquery-migrate-1.1.0.min.js"></script>
        <script src="../../assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>

        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
        <script src="../../assets/js/demo.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>

   
        <!-- ================== END PAGE LEVEL JS ================== -->
        <script>
            $(document).ready(function () {
                App.init();
                Demo.init();
                PageDemo.init();
            });
            //$("#ddlVoucherType").change(function () {
            //    var voufor = $("#ddlVoucherType").val();
            //    Employee_Account_Report.changetype(voufor, callback_open_detail);
            //});
            //function callback_open_detail(response) {
            //    $("#divdata").html(response.value);
            //    $("#txtdata").val(response.value);
            //    alert($("#txtdata").val());
            //}
        </script>
    </form>
</body>
</html>
