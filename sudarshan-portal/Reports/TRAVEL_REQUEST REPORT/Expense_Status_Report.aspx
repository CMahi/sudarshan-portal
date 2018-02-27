<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expense_Status_Report.aspx.cs" Inherits="Expense_Status_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />

    <title>Request Status Report</title>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker3.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div id="divIns" runat="server" style="display: none">
            <div style="background-color: #E6E6E6; position: absolute; top: 0; left: 0; width: 100%; height: 300%; z-index: 1001; -moz-opacity: 0.8; opacity: .80; filter: alpha(opacity=80);">
                <img src="../../images/loading_transparent.gif" style="background-color: transparent; position: fixed; top: 40%; left: 46%;" /></div>
        </div>
        <div class="modal fade" id="req_Details">
            <div class="modal-dialog" style="height: auto; width: 95%; margin-left: 3%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Request Details</font></h4>
                    </div>
                    <div class="modal-body" id="divReq_Details" runat="server">
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="doc_details">
            <div class="modal-dialog" style="height: auto; width: 95%; margin-left: 3%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Documents Details</font></h4>
                    </div>
                    <div class="modal-body" id="divDoc_Details" runat="server">
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="Documents">
            <div class="modal-dialog" style="height: auto; width: 45%; margin-left: 25%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Documents</font></h4>
                    </div>
                    <div class="modal-body" id="DivDocs" runat="server" data-scrollbar="true" data-height="425px">
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 10px">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">

                        <div class="panel-heading-btn">
                            <div class="btn-group pull-right" style="margin-top: -5px;">
                                <asp:Button ID="Button1" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_Export_onClick" />
                                <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" Style="margin-left: 10px" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                        <h4 class="panel-title">EXPENSE STATUS REPORT</h4>
                    </div>
                    <div class="panel-body">
                        <div class="panel pagination-danger">
                            <div class="row">
                                <div class="form-group">

                                    <label class="col-md-2 control-label">Voucher Type</label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlVoucherType" runat="server" class="form-control">
                                        </asp:DropDownList>

                                    </div>

                                    <label class="col-md-2">From Date</label>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type='text' class="form-control datepicker-dropdown" id="txt_f_date" runat="server" readonly="" />
                                        </div>
                                    </div>

                                    <label class="col-md-2">To Date</label>
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type='text' class="form-control datepicker-dropdown" id="txt_t_date" runat="server" readonly="" />
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="col-md-2">Status</label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" class="btn btn-danger btn-rounded" Style="margin-left: 10px" OnClientClick="return check()" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btn_Clear" runat="server" class="btn btn-danger btn-rounded" Text="Clear" OnClick="BtnClear_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <b><span id="spn_amt" runat="server">Total Amount : </span></b>
                                    <b><span id="spn_amount" runat="server">0</span></b>
                                </div>
                                <div class="col-md-2">
                                    <b><span id="span_ramt" runat="server">Rejected Amount : </span></b>
                                    <b><span id="spn_rejamount" runat="server">0</span></b>
                                </div>
                            </div>
                            <div class="table-responsive" id="divdata" runat="server">
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div id="conditionButton" style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_totalamt" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_rejectedamt" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtdata" runat="server" Width="1px"></asp:TextBox>
            <input id="ddlUser" runat="Server" type="text" />
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
        <script src="../../JS/validator.js"></script>
        <script src="../../assets/js/Vaildation.js"></script>
    </form>
    <script>

        $(document).ready(function () {
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' });
            //get_data();

        });
        $('#txt_f_date').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
            daydiffrence();
        });

        $('#txt_t_date').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
            daydiffrence();
        });

        function daydiffrence() {
            if (document.getElementById('txt_f_date').value == "") {
                return false;
            }
            if (document.getElementById('txt_t_date').value == "") {
                return false;
            }
            var start = $('#txt_f_date').val();
            var stard = start.substring(0, 2);
            var starm = start.substring(3, 6);
            var stary = start.substring(7, 11);

            var end = $('#txt_t_date').val();
            var endd = end.substring(0, 2);
            var endm = end.substring(3, 6);
            var endy = end.substring(7, 11);
            var d1 = new Date(stary, getMonthFromString(starm) - 1, stard);
            var d2 = new Date(endy, getMonthFromString(endm) - 1, endd);
            if (d1 > d2) {
                alert('To Date must be greater than from Date');
                $('#txt_t_date').focus();
                $('#txt_t_date').val('');
                return false;
            }

        }
        function check() {
            var fdate = $('#txt_f_date').val();
            var tdate = $('#txt_t_date').val();
            var status = $('#ddlStatus').val();

            if (fdate == "") {
                alert("Please Select From Date");
                return false;
            }
            if (tdate == "") {
                alert("Please Select To Date");
                return false;
            }

            $("#divIns").show();
            return true;
        }
        function getRequestDetails(index) {
            var process_name = $("#pname" + index).val();
            var req_no = $("#h_info" + index).val();
            Expense_Status_Report.getDetails(req_no, process_name, callback_BindRequestData);
        }
        $('body').on('click', '.reqno', function () {
            var table = $('#data-table1').DataTable();
            var data = table.row($(this).parents('tr')).data();
            var p_name = data[3];
            var requ_no = data[2];
            Expense_Status_Report.getDetails(requ_no, p_name, callback_BindRequestData);
        })
        $('body').on('click', '.doc', function () {
            var table = $('#data-table1').DataTable();
            var data = table.row($(this).parents('tr')).data();
            var p_name = data[3];
            var requ_no = data[2];
            Expense_Status_Report.getDocDetails(requ_no, p_name, callback_BindDocData);
        })
        function getDocDetails(index) {
            var process_name = $("#pname" + index).val();
            var req_no = $("#h_info" + index).val();
            Expense_Status_Report.getDocDetails(req_no, process_name, callback_BindDocData);
        }
        function callback_BindRequestData(response) {
            $("#divReq_Details").html(response.value);
        }
        function callback_BindDocData(response) {
            $("#divDoc_Details").html(response.value);
        }
        g_serverpath = '/Sudarshan-Portal';
        function downloadfiles(req, index) {
            window.open(g_serverpath + '/Common/FileDownload.aspx?indentno=' + req + '&filename=' + $("#uploadTable tr")[index].cells[2].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
        }
        function download_Link(link, req_no) {
            var result = Expense_Status_Report.check_avail(req_no);
            if (result.value == 'true') {
                window.open(link, 'Download', 'toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
            }
            else {
                alert('Request Is Still Pending For Approval');
            }
        }
    </script>
</body>
</html>
