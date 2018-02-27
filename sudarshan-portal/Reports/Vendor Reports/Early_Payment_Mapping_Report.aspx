<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Early_Payment_Mapping_Report.aspx.cs" Inherits="Early_Payment_Mapping_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Early Payment Mapping Report</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="modal fade" id="paymentterm">
            <div class="modal-dialog" style="height: auto; width: 98%; margin-left: 1%">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Early Payment Mapping Report</font></h4>
                    </div>

                    <div class="modal-body" id="div_header" runat="server" data-scrollbar="true" data-height="425px">
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div class="btn-group pull-right" style="margin-top: -5px;">
                            <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_Export_onClick"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" Style="margin-left: 10px"  OnClick="btnCancel_Click" />
                        </div>
                        <h3 class="panel-title">Early Payment Mapping Report</h3>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="margin-top: -8px;">
                <div class="panel panel-grey">
                    <div class="panel-heading">

                        <h4 class="panel-title">Early Payment Details</h4>
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
                            <div class='form-group'>
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Vendor Name</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddl_Vendor" class="form-control" runat="server" ></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12 text-center">
                                    <asp:Button type="button" ID="btnSubmit" Text="SUBMIT" runat="server" class="btn btn-danger btn-rounded" OnClick="btnsubmit_Click" OnClientClick="return Early()"/>
                                    <asp:Button type="button" ID="btnClear" Text="Clear" runat="server" class="btn btn-danger btn-rounded" OnClick="btnClear_Click" OnClientClick="return Clear()" />
                                </div>
                            </div>
                            <div class="panel pagination-danger">
                                <div class="table-responsive" id="div_ReportDetails" runat="server" style="overflow: visible"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
     
            <div style="display: none">
                <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
                <asp:TextBox ID="app_Authority" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtdata" runat="server" Width="1px"></asp:TextBox>
            </div>
            <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
            <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
            <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
            <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
            <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
            <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
            <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
            <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
            <script src="../../JS/Early_Payment_Request.js"></script>
            <script src="../../JS/validator.js"></script>
            <script src="../../assets/js/Vaildation.js"></script>
            <script src="../../JS/Early_Payment_Mapping_Report.js"></script>
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
            </script>
    </form>
</body>
</html>
