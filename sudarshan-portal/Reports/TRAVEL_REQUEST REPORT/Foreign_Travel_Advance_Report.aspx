<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Foreign_Travel_Advance_Report.aspx.cs" Inherits="Foreign_Travel_Advance_Report" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Foreign Travel Expense Report</title>
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
<div id="divIns" runat="server" style="display:none">
            <div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent.gif" style="background-color:transparent;position:fixed; top:40%; left:46%;"/></div>
        </div>
        <div class="modal fade" id="div_Details">
            <div class="modal-dialog" style="height: auto; width: 90%; margin-left: 5%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> REQUEST DETAILS</font></h4>
                    </div>
                    <div class="modal-body" id="req_deta" runat="server">
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                             <h4 class="panel-title">Foreign Travel Advance Report</h4>
                    </div>
                    <div class="panel-body">
                                <div class="row">
                                <table style="float:right">
                                    <tr>
                                        <td style="padding-right:3px"><b>Status : </b></td>
                                        <td>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control input-sm" onchange="getData()">
                                                <asp:ListItem>Open</asp:ListItem>
                                                <asp:ListItem>Close</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:36px"></td>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_Export_onClick" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" Style="margin-left: 10px" OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div><hr />
                            <div class="row"><div id="divdata" runat="server" class="table-responsive"></div></div>
                        </div>
                    
                </div>
            </div>
        </div>

        <div id="conditionButton" style="display: none">
            <asp:TextBox ID="dData" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
        </div>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
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
                getData();
            });

            function view_details(processid,instanceid)
            {
                Foreign_Travel_Advance_Report.getRequestDetails(processid,instanceid,callBack_Data);
            }
            function callBack_Data(response)
            {
                $("#req_deta").html(response.value);
            }

            function close_Child_Popup() {
                $('#div_UploadDocument').modal('hide');
            }
            function download_files(req_no,file_name) {
                var app_path = $("#app_Path").val();
                window.open(app_path + '/Common/FileDownload.aspx?indentno=' + req_no + '&filename=' + file_name + '&filetag=', 'Download',

            'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
            }

            function getData()
            {
                try {
                    $("#divIns").show();
                    var status = $("#ddlStatus Option:selected").text();
                    Foreign_Travel_Advance_Report.getExpenseData(status, callBackSuccess);
                }
                catch (ex) {
                    $("#divIns").hide();
                }
            }
            function callBackSuccess(response) {
                $("#divIns").hide();
                $("#divdata").html(response.value);
                $("#dData").val(response.value);
                $('#data-table1').dataTable();
            }
        </script>
    </form>
</body>

</html>
