<%@ Page ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true" Language="C#" Async="true" CodeFile="Vendor_Usage_Report.aspx.cs" Inherits="Vendor_Usage_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Vendor Portal Usage Report</title>
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
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div class="btn-group pull-right" style="margin-top: -5px;">
                            <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_Export_onClick" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" Style="margin-left: 10px" OnClick="btnCancel_Click" />
                        </div>
                        <h3 class="panel-title"><b>Vendor Portal Usage Report</b></h3>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="panel panel-grey">
                <div class="panel-heading">
                    <h4 class="panel-title">Vendor Portal Usage Report</h4>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-1"></div>
                            <label class="col-md-2">From Date</label>
                            <div class="col-md-2">

                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    <input type='text' class="form-control datepicker-dropdown" id="txt_f_date" runat="server" readonly="" />
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-2">To Date</label>
                            <div class="col-md-2">

                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    <input type='text' class="form-control datepicker-dropdown" id="txt_t_date" runat="server" readonly="" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                            <div class="col-md-12 text-center">
                                <asp:Button type="button" ID="btnSubmit" Text="SUBMIT" runat="server" class="btn btn-danger btn-rounded" OnClientClick="return daydiff()" OnClick="btnsubmit_Click" />
                            </div>
                            <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                        <div class="form-group">
                            <div class="col-md-2"></div>
                            <div class="col-md-8">
                                <div id="div_po" runat="server" class="table-responsive"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="acknow">
            <div class="modal-dialog" style="width: 60%;">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Acknowlegded PO Details </font></h4>
                    </div>

                    <div class="modal-body" data-scrollbar="true" data-height="425px">
                        <asp:Button ID="btn_export_acknow" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_acknow_onClick" />
                        <div class="table-responsive" id="div_acknow" runat="server">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="dispatch">
            <div class="modal-dialog" style="width: 70%;">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Dispatched PO Details </font></h4>
                    </div>
                    <asp:Button ID="btn_export_dispatch" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_dispatch_onClick" />
                    <div class="table-responsive" id="div_dispatch" runat="server" data-scrollbar="true" data-height="425px">
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="login">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Login Details </font></h4>
                    </div>
                    <asp:Button ID="btn_export_login" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_login_onClick" />

                    <div class="table-responsive" id="div_login" runat="server" data-scrollbar="true" data-height="425px">
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div id="conditionButton" style="display: none">
            <asp:TextBox ID="txtCreatedBy" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txt_PONO" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txtdata" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txtdate" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txt_daydiff" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txt_exp_ack_data" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txt_exp_dis_data" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txt_exp_login_data" runat="server" Width="1px"></asp:TextBox>
             <asp:TextBox ID="txt_date" runat="server" Width="1px"></asp:TextBox>
             <asp:TextBox ID="txt_dateac" runat="server" Width="1px"></asp:TextBox>
        </div>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/jquery/jquery-migrate-1.1.0.min.js"></script>
        <script src="../../assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
        <script src="../../assets/js/demo.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../JS/Vendor_Usage_Report.js"></script>
        <script src="../../JS/validator.js"></script>
        <script src="../../assets/js/Vaildation.js"></script>
        <!-- ================== END PAGE LEVEL JS ================== -->

    </form>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            App.init();
            Demo.init();
            PageDemo.init();
        });

        function viewData(index) {
            try {
                var app_path = $("#app_Path").val();
                var po_val = $("#encrypt_po" + (index)).val();
                window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
            }
            catch (exception) {
            }
        }
        $('body').on('click', '.acknow', function () {
            var table = $('#data-table1').DataTable();
            var data = table.row($(this).parents('tr')).data();
            $('#txt_dateac').val(data[0]);
            Vendor_Usage_Report.bindReport(data[0], 1, call_details);
        });
        $('body').on('click', '.dispatch', function () {
            var table = $('#data-table1').DataTable();
            var data = table.row($(this).parents('tr')).data();
            $('#txt_date').val(data[0]);
            Vendor_Usage_Report.bindReport(data[0], 2, dis_details);
        });
        $('body').on('click', '.logindata', function () {
            var table = $('#data-table1').DataTable();
            var data = table.row($(this).parents('tr')).data();
            Vendor_Usage_Report.bindReport(data[0], 3, login_details);
        });

        function login_details(response) {
            $("#div_login").html(response.value);
            $("#txt_exp_login_data").val(response.value);
        }
        function dis_details(response) {
            $("#div_dispatch").html(response.value);
            $("#txt_exp_dis_data").val(response.value);
        }
        function call_details(response) {
            $("#div_acknow").html(response.value);
            $("#txt_exp_ack_data").val(response.value);
        }
    </script>

</body>
</html>
