<%@ Page ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true" Language="C#" Async="true" CodeFile="Po_List_Status_Report.aspx.cs" Inherits="Po_List_Status_Report" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Po List Status Report</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
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
<body style="overflow-x: hidden">
    <form id="frm_report" runat="server">

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="modal fade" id="paymentterm">
            <div class="modal-dialog" style="height: auto; width: 98%; margin-left: 1%">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Dispatch Request Details </font></h4>
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
                            <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_Export_onClick" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" Style="margin-left: 10px" OnClick="btnCancel_Click" />
                        </div>
                        <h3 class="panel-title"><b>Po Listing Report</b></h3>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="panel panel-grey">
                <div class="panel-heading">                   
                    <h4 class="panel-title">Po Status Details</h4>
                </div>
          
                    <div class="panel-body">
                        <div class="panel pagination-danger">
                            <div class="row">
                <div class="form-group">

                       <label class="col-md-2">PO Status </label>
                        <div class="col-md-2" style="text-align: left">
                            <asp:DropDownList ID="ddlType" runat="server" Width="105px" CssClass="form-control input-sm"  >
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
                     <div class="col-md-1"></div>
                                    <div class="col-md-12 text-center">
                                       <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" class="btn btn-danger btn-rounded" Style="margin-left: 10px" OnClientClick="return check()" OnClick="btnSubmit_Click" />
                                                
                                          </div>
                                 
                </div>   
                   
                            </div>  <div id="div_po" runat="server" class="table-responsive"></div>  </div> 

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
        <script src="../../assets/js/Vaildation.js"></script>
        <script lang="javascript" type="text/javascript">

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

                if (fdate == "") {
                    alert("Please select from date");
                    return false;
                }
                if (tdate == "") {
                    alert("Please select to date");
                    return false;
                }
                return true;
            }

            function viewData(index) {
                try {
                    var app_path = $("#app_Path").val();
                    var po_val = $("#encrypt_po_" + (index)).val();
                    window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
                }
                catch (exception) {

                }
            }
            $("#btn_File").click(function () {
                $.ajax({
                    type: "POST",
                    url: "~/Reports/Vendor Reports/Po_List_Status_Report.aspx/btn_File",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({}),
                    dataType: "json",
                });
            });
        </script>
    </form>
</body>

</html>


