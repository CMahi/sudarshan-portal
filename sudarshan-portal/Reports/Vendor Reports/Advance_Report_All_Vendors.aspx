<%@ Page ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true" Language="C#" Async="true" CodeFile="Advance_Report_All_Vendors.aspx.cs" Inherits="Advance_Report_All_Vendors" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Advance Request Detail Report</title>
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
        <div id="divIns" runat="server" style="display: none">
            <div style="background-color: #E6E6E6; position: absolute; top: 0; left: 0; width: 100%; height: 300%; z-index: 1001; -moz-opacity: 0.8; opacity: .80; filter: alpha(opacity=80);">
                <img src="../../images/loading_transparent.gif" style="background-color: transparent; position: fixed; top: 40%; left: 46%;" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div class="btn-group pull-right" style="margin-top: -5px;">
                            <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_Export_onClick" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" Style="margin-left: 10px" OnClick="btnCancel_Click" />
                        </div>
                        <h3 class="panel-title"><b>Advance Request Details</b></h3>
                    </div>
                </div>

            </div>
        </div>
        <div class="col-md-12">
            <div class="panel panel-grey">
                <div class="panel-heading">
                    <h4 class="panel-title">Advance Payment Details</h4>
                </div>
                <div class="panel-body">
                    <div class="form-group">

                        <label class="col-md-2">Vendor Name</label>
                        <div class="col-md-2">
                            <div class="input-group">
                                <asp:DropDownList ID="ddlVendor" runat="server" Width="150px" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </div>
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
                        <div class="col-md-12 text-center">
                            <asp:Button type="button" ID="btnSubmit" Text="SUBMIT" runat="server" class="btn btn-danger btn-rounded" OnClientClick="return check()" OnClick="btnSubmit_Click" />
                            <div class="table-responsive" id="divdata" runat="server">
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="modal fade" id="po" >
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">PO Advance Details</font></h4>
                    </div>
                    <div class="table-responsive" id="div_po" runat="server" style='overflow-x: scroll; width: 600px'>
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
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

        <!-- ================== END PAGE LEVEL JS ================== -->
        <script>
            function check() {
                var fdate = $('#txt_f_date').val();
                var tdate = $('#txt_t_date').val();
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
            $(document).ready(function () {
                $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' });
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
            function viewData(index) {
                try {
                    var po_val = "";
                    po_val = $("#encrypt_po" + index).val();
                    var app_path = $("#app_Path").val();
                    window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no,directories=no');

                }
                catch (exception) {

                }
            }
            $('body').on('click', '.clspo', function () {
                $("#divIns").show();
                var table = $('#data-table1').DataTable();
                var data = table.row($(this).parents('tr')).data();
                Advance_Report_All_Vendors.getPODetails(data[2], po_details);
            })
            function po_details(response) {
                $("#divIns").hide();
                $("#div_po").html(response.value);
            }
        </script>
    </form>
</body>
</html>
