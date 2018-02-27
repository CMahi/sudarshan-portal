<%@ Page ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true" Language="C#" Async="true" CodeFile="Vendor_Matrix_Report.aspx.cs" Inherits="Vendor_Matrix_Report" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Vendor Matrix Report</title>
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
<body style="overflow-x: hidden">
    <form id="frm_report" runat="server">

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
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
                        <h3 class="panel-title"><b>Vendor Matrix Report</b></h3>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="panel panel-grey">

                <div class="panel-heading">
                </div>
                <div class="panel-body">

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
                        <div class="col-md-12 text-center">
                            <asp:Button type="button" ID="btnSubmit" Text="SUBMIT" runat="server" class="btn btn-danger btn-rounded" OnClientClick="check()" />
                            <%--    <asp:Button type="button" ID="btnClear" Text="Clear" runat="server" class="btn btn-danger btn-rounded" OnClick="btnClear_Click" />--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <%--<label class="col-md-2 control-label"><b>Summary</b></label>--%>
                        <div>
                            <div id="div_count" runat="server"></div>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-md-2 control-label">
                            <b>Total No of Vendors :<asp:Label ID="lblvcount" runat="server"></asp:Label>
                            </b>
                        </label>
                        <label class="col-md-2 control-label"><b>Vendor Name</b></label>
                        <asp:DropDownList ID="ddlVendor" runat="server" Width="200px" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <div class="panel pagination-danger">
                            <div id="div_po" runat="server" class="table-responsive"></div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="modal fade" id="vendor">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Total No of Vendors</font></h4>
                    </div>
                    <asp:Button ID="btn_export_vendor" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_vendor_onClick" />

                    <div class="table-responsive" id="div_vendor" runat="server" data-scrollbar="true" data-height="425px">
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
                        <h4 class="modal-title"><font color="white"> Logged Vendors </font></h4>
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
        <div class="modal fade" id="nlogin">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Not Logged Vendors </font></h4>
                    </div>
                    <asp:Button ID="btn_export_nlogin" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_nlogin_onClick" />

                    <div class="table-responsive" id="div_nlogin" runat="server" data-scrollbar="true" data-height="425px">
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="portal">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Used Portal Vendors</font></h4>
                    </div>
                    <asp:Button ID="btn_export_portal" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_portal_onClick" />

                    <div class="table-responsive" id="div_portal" runat="server" data-scrollbar="true" data-height="425px">
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="dispatch">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Dispatch Vendor Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_dispatch" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_dispatch_onClick" />

                    <div class="table-responsive" id="div_dispatch" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="rejected">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Rejected Status Dispatch Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_reject" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_reject_onClick" />
                    <div class="table-responsive" id="div_reject" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="grn">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">GRN Status Dispatch Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_grn" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_grn_onClick" />

                    <div class="table-responsive" id="div_grn" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="parta">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Part1 Status Dispatch Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_part1" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_part1_onClick" />

                    <div class="table-responsive" id="div_pa1" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="partb">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Part2 Status Dispatch Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_part2" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_part2_onClick" />

                    <div class="table-responsive" id="div_pa2" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="quality">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Quality Checking Completed Status Dispatch Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_quality" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_quality_onClick" />
                    <div class="table-responsive" id="div_quality" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="bill">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Bill Book Completed Status Dispatch Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_bill" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_bill_onClick" />
                    <div class="table-responsive" id="div_bill" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="payment">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Payments Processed Status Dispatch Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_pay" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_pay_onClick" />
                    <div class="table-responsive" id="div_pay" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="deviation">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Quality Control Complete with Deviation & Deduction Status Dispatch Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_devia" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_devia_onClick" />

                    <div class="table-responsive" id="div_deviation" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="nulls">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Null Status Dispatch Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_null" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_null_onClick" />

                    <div class="table-responsive" id="div_null" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="acknow">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Acknowledge PO Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_ack" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_ack_onClick" />
                    <div class="panel pagination-danger">
                        <div class="table-responsive" id="div_ack" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="created">
            <div class="modal-dialog">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">PO Details</font></h4>
                    </div>
                    <asp:Button ID="btn_export_pod" align="left" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_export_pod_onClick" />
                    <div class="panel pagination-danger">
                        <div class="table-responsive" id="div_pod" runat="server" style='overflow-x: scroll; height: 425px; width: 600px'>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="paymentterm">
            <div class="modal-dialog" style="height: auto; width: 98%; margin-left: 1%">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Dispatch Request Details </font></h4>
                    </div>

                    <div class="modal-body" id="div_header" runat="server"  style='overflow-x: scroll; height: 425px;' >
                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
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
            <asp:TextBox ID="txt_vendor" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txt_exp_login_data" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txt_exp_nlogin_data" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txt_exp_portal_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_dispatch_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_reject_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_grn_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_p1_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_p2_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_qual_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_bill_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_pay_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_devia_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_null_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_ack_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_pod_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_exp_vendor_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtdatacnt" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txtdata" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server" Width="1px"></asp:TextBox>
        </div>
        <asp:Button ID="btn_File" runat="server" Style="display: none" />
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
         <script src="../../JS/Early_Payment_Request.js"></script>
        <script src="../../assets/js/Vaildation.js"></script>
        <script lang="javascript" type="text/javascript">

            function check() {
                if (document.getElementById('txt_f_date').value == "") {
                    alert("Select From Date");
                    return false;
                }
                if (document.getElementById('txt_t_date').value == "") {
                    alert("Select To Date");
                    return false;
                }
                var vendor = $("#ddlVendor").val();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                $("#divIns").show();
                //  Vendor_Matrix_Report.getVendorData(vendor,start,end, callback_Detail);
            }
            $(document).ready(function () {
                $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' });
                var vendor = $("#ddlVendor").val();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                Vendor_Matrix_Report.getVendorData(vendor, start, end, callback_Detail);

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
            $("#ddlVendor").change(function () {
                var vendor = $("#ddlVendor").val();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                Vendor_Matrix_Report.getVendorData(vendor, start, end, callback_Detail);
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
            function callback_Detail(response) {
                var strDate = response.value.split("&&&");
                $('#div_po').html(strDate[0]);
                $('#txtdata').val(strDate[0]);
                $('#txtdatacnt').val(strDate[1]);
                $('#div_count').html(strDate[1]);
                $('#lblvcount').text(strDate[2]);
                $('#data-table1').dataTable({ 'bSort': false });
                $('#table2').dataTable({ 'bSort': false, "scrollX": true, "paging": true, "searching": false });
                $("#divIns").hide();
            }
            $('body').on('click', '.vendors', function () {
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var table = $('#table2').DataTable();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 1, vendor_details);
            })
            $('body').on('click', '.loggeda', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 5, loginn_details);
            })
            $('body').on('click', '.nloggeda', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 6, nloginn_details);
            })
            $('body').on('click', '.UsePortala', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 2, portal_details);
            })
            $('body').on('click', '.dispatcha', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 4, dispatch_details);
            })
            $('body').on('click', '.Rejecteda', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 7, reject_details);
            })
            $('body').on('click', '.grna', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 8, grn_details);
            })
            $('body').on('click', '.part1a', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 9, pa1_details);
            })
            $('body').on('click', '.part2a', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 10, pa2_details);
            })
            $('body').on('click', '.qualitya', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 11, qual_details);
            })
            $('body').on('click', '.billa', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 12, bill_details);
            })
            $('body').on('click', '.paymenta', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 13, pay_details);
            })
            $('body').on('click', '.deviationa', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 14, devia_details);
            })
            $('body').on('click', '.nullsa', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 15, null_details);
            })
            $('body').on('click', '.acknowa', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 16, ack_details);
            })
            $('body').on('click', '.createda', function () {
                var table = $('#table2').DataTable();
                var start = $('#txt_f_date').val();
                var end = $('#txt_t_date').val();
                var data = table.row($(this).parents('tr')).data();
                Vendor_Matrix_Report.bindReport(start, end, 17, po_details);
            })
            function vendor_details(response) {
                $("#div_vendor").html(response.value);
                $("#logintable1").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_vendor_data").val(response.value);
            }
            function loginn_details(response) {
                $("#div_login").html(response.value);
                $("#logintable5").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_login_data").val(response.value);
            }
            function nloginn_details(response) {
                $("#div_nlogin").html(response.value);
                $("#logintable6").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_nlogin_data").val(response.value);
            }
            function portal_details(response) {
                $("#div_portal").html(response.value);
                $("#logintable2").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_portal_data").val(response.value);
            }
            function dispatch_details(response) {
                $("#div_dispatch").html(response.value);
                $("#vendordata4").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_dispatch_data").val(response.value);
            }
            function reject_details(response) {
                $("#div_reject").html(response.value);
                $("#vendordata7").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_reject_data").val(response.value);
            }
            function grn_details(response) {
                $("#div_grn").html(response.value);
                $("#vendordata8").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_grn_data").val(response.value);
            }
            function pa1_details(response) {
                $("#div_pa1").html(response.value);
                $("#vendordata9").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_p1_data").val(response.value);
            }
            function pa2_details(response) {
                $("#div_pa2").html(response.value);
                $("#vendordata10").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_p2_data").val(response.value);
            }
            function qual_details(response) {
                $("#div_quality").html(response.value);
                $("#vendordata11").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_qual_data").val(response.value);
            }
            function bill_details(response) {
                $("#div_bill").html(response.value);
                $("#vendordata12").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_bill_data").val(response.value);
            }
            function pay_details(response) {
                $("#div_pay").html(response.value);
                $("#vendordata13").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_pay_data").val(response.value);
            }
            function devia_details(response) {
                $("#div_deviation").html(response.value);
                $("#vendordata14").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_devia_data").val(response.value);
            }
            function null_details(response) {
                $("#div_null").html(response.value);
                $("#vendordata15").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_null_data").val(response.value);
            }
            function ack_details(response) {
                $("#div_ack").html(response.value);
                $("#vendordata16").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_ack_data").val(response.value);
            }
            function po_details(response) {
                $("#div_pod").html(response.value);
                $("#vendordata17").dataTable({ 'bSort': false, "searching": false });
                $("#txt_exp_pod_data").val(response.value);
            }
            function setSelectedNote(req_note) {
                try {
                    Vendor_Matrix_Report.GetCurrentTime(req_note, OnSuccess);
                }
                catch (exception) {
                }
            }
            function OnSuccess(response) {
                document.getElementById("div_header").innerHTML = response.value;
            }
        </script>
    </form>
</body>

</html>


