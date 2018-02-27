<%@ Page ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true" Language="C#" Async="true" CodeFile="Unapproved_Funds_Report.aspx.cs" Inherits="Unapproved_Funds_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Unapproved Funds Report</title>
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
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
          <div id="divIns" runat="server" style="display:none">
            <div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent.gif" style="background-color:transparent;position:fixed; top:40%; left:46%;"/></div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div class="btn-group pull-right" style="margin-top: -5px;">
                            <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_Export_onClick" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" Style="margin-left: 10px" OnClick="btnCancel_Click" />
                        </div>
                        <h3 class="panel-title"><b>Unapproved Funds Report</b></h3>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-md-2 control-label">Voucher Type</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlVoucherType" runat="server" class="form-control">
                            </asp:DropDownList>

                        </div>
                        <label class="col-md-2 control-label">Payment Mode</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlPayMode" runat="server" class="form-control" onchange="enable_disable()">
                            </asp:DropDownList>
                        </div>
                        <div id="div_payment" runat="server">
                            <label class="col-md-2 control-label">Payment Location</label>

                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlLocation" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                       </div>
                    </div>

                    <%--</div>--%>
                    <div class="form-group">
                        <div class="col-md-1"></div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" class="btn btn-danger btn-rounded" Style="margin-left: 10px" OnClientClick="return check()" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btn_Clear" runat="server" class="btn btn-danger btn-rounded" Text="Clear" OnClick="BtnClear_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="panel panel-grey">
                <div class="panel-heading">
                    <h4 class="panel-title">Unapproved Funds Report</h4>

                </div>
                <div class="panel-body">
                    <b><span id="spn_pay" runat="server">Payable Amount : </span></b>
                    <b><span id="spn_payable" runat="server">0</span></b>
                    &nbsp; &nbsp; &nbsp; &nbsp;
                    <b><span id="spn_rec" runat="server">Recovery Amount : </span></b>
                    <b><span id="spn_recovery" runat="server">0</span></b>
                    <div class="panel pagination-success">
                        <div id="divdata" runat="server" class="table-responsive"></div>
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
            <asp:TextBox ID="txt_paylamt" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_recamt" runat="server"></asp:TextBox>
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
                if ($("#ddlPayMode option:selected").text().toUpperCase() == "CASH") {
                    $("#div_payment").show();
                }
                else if (($("#ddlPayMode option:selected").text().toUpperCase() == "BANK") || ($("#ddlPayMode option:selected").text() == "--Select All--")) {
                    $("#div_payment").hide();
                }
            });
            function check() {
                var pay = $('#ddlPayMode').val();
                var loc = $('#ddlLocation').val();
                var vou = $('#ddlVoucherType').val();

               // if (pay == 0 && pay=) {
                 //   alert("Please Select Payment Mode");
                  //  return false;
                //}
                if (pay == 1) {
                    if (loc == 0) {
                        alert("Please Select Payment Location");
                        return false;
                    }
                }
 		
                $("#divIns").show();
                return true;
            }
            function enable_disable() {
                //alert($("#ddlPayMode option:selected").text())
                if ($("#ddlPayMode option:selected").text().toUpperCase() == "CASH") {
                    $("#div_payment").show();
                }
                else if (($("#ddlPayMode option:selected").text().toUpperCase() == "BANK") || ($("#ddlPayMode option:selected").text() == "--Select All--")) {
                    $("#div_payment").hide();
                }
            }
        </script>
    </form>
</body>
</html>
