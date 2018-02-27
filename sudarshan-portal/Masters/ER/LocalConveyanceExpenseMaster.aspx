<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocalConveyanceExpenseMaster.aspx.cs" Inherits="LocalConveyanceExpenseMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <title>Local_conveyance_Expense_Master</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
</head>
<body style="overflow-x: hidden">
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title">Local Conveyance Expense</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal bordered-group">
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2 control-label">Vehicle Type</label>
                                <div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Selected="True">---Select One---</asp:ListItem>
                                        <asp:ListItem Value="Two Wheeler">Two Wheeler</asp:ListItem>
                                        <asp:ListItem Value="Four Wheeler">Four Wheeler</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <label class="col-md-2 control-label">Rate Per KM</label>
                                <div class="col-md-2 ui-sortable">
                                    <input id="txt_Rate" runat="server" type="text" value="" class="form-control" style="text-align: right" onkeypress="return isNumberKey(event)" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-2"></div>
                                <label class="col-md-1">From Date<font color="#ff0000"></font></label>
                                <div class="col-md-2">
                                    <div class="input-group" id="datetimepicker6">
                                        <input type='text' class="form-control" id="fromDate" runat="server" readonly />
                                        <span class="input-group-btn">
                                            <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-1">To Date<font color="#ff0000"></font></label>
                                <div class="col-md-2">
                                    <div class="input-group" id="datetimepicker7">
                                        <input type='text' class="form-control" id="toDate" runat="server" readonly />
                                        <span class="input-group-btn">
                                            <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                            </div>

                        </div>
                        <div id="dv_add" runat="server" style="text-align: center">
                            <asp:Button ID="btn_add" runat="server" Text="Save"
                                OnClientClick="return validateType();" class="btn btn-danger btn-rounded m-b-5" OnClick="btnSaveOnClick"></asp:Button>
                            <asp:Button ID="btn_Home" runat="server" Text="Cancel" class="btn btn-danger btn-rounded m-b-5" Width="86px" ForeColor="White" OnClick="btnHomeOnClick"></asp:Button>
                        </div>
                        <div id="dv_update" runat="server" style="text-align: center; display: none">
                            <asp:Button ID="btn_update" runat="server" Text="Update" OnClientClick="return validateType();" class="btn btn-danger btn-rounded m-b-5" OnClick="btnUpdateOnClick" />
                            <asp:Button ID="but_Home" runat="server" Text="Cancel" class="btn btn-danger btn-rounded m-b-5" OnClick="btnHomeOnClick" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title">Local Conveyance Expense Details</h4>
                    </div>
                    <div class="panel-body">
                        <div id="dv_dtl" runat="server" class="panel pagination-danger">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="conditionButton" style="display: none">
            <asp:TextBox ID="txt_PkId" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtHeadIdID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_DomainName" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_procedure" runat="server" Text="BUDGETHEAD"></asp:TextBox>
            <asp:TextBox ID="txtCondition" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtParameterID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtCreatedBy" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ActionObjectID" runat="server"></asp:TextBox>
        </div>

        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
    </form>
</body>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $('#fromDate').datepicker({ format: 'dd-M-yy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
            setToDate(ev);
        });

        $('#toDate').datepicker({ format: 'dd-M-yy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
            dayDiffrence();
        });
    });

    function validateType() {
        if ($("#ddlVehicleType option:selected").index() < 1) {
            alert("Please Select Vehicle Type...!");
            return false;
        }
        else if ($("#txt_Rate").val() == "") {
            alert("Please Enter Rate...!");
            return false;
        }
        else if ($("#fromDate").val() == "") {
            alert("Select Travel From Date...!");
            return false;
        }
        else if ($("#toDate").val() == "") {
            alert("Select Travel To Date...!");
            return false;
        }
        return true;
    }

    function setToDate(fromDate) {
        $('#toDate').val('');
        $('#toDate').datepicker('setStartDate', fromDate.date);
    }

    function dayDiffrence() {
        var fromDate = $('#fromDate').val();
        var toDate = $('#toDate').val();

        var ddl_type = $('#ddlVehicleType').val();
        var dataString = JSON.stringify({
            start: fromDate,
            end: toDate,
            ddl_text: ddl_type
        });

        $.ajax({
            type: "POST",
            url: "LocalConveyanceExpenseMaster.aspx/getValidDates",
            contentType: "application/json; charset=utf-8",
            data: dataString,
            dataType: "json",
            success: function (data) {
                validDateCallBack(data);
            },
            failure: function (response) {
                alert(response.d);
            }
        });

        function validDateCallBack(data) {
            if (data.d != "false" && data.d != "true") {
                $('#toDate').val("");
                $('#fromDate').val("");
                alert(data.d);
            }
        }
    }

    function getData(index) {
        var pk_id = $("#pk_id_" + index).val();
        var vehicleType = $("#vehicle_type_" + index).val();
        var rateperkm = $("#rateper_KM_" + index).val();
        var fromdate = $("#from_date_" + index).val();
        var todate = $("#to_date_" + index).val();

        $("#txt_PkId").val(pk_id);
        $("#ddlVehicleType").val(vehicleType);
        $("#txt_Rate").val(rateperkm);
        $("#fromDate").val(fromdate);
        $("#toDate").val(todate);
        $('#dv_add').hide();
        $('#dv_update').show();
    }

    function deleteData(index) {
        var pk_id = $("#pk_id_" + index).val();
        var user = $("#txt_UserName").val();
        var IsDelete = confirm("Are you sure you want delete selected row ?");
        if (IsDelete) {
            LocalConveyanceExpenseMaster.updateLocalConveyanceData(pk_id, user, OnDelete);
        }
    }

    function OnDelete(response) {

        if (response.value != "false") {
            $("#dv_dtl").html(response.value);
            $('#tbl_Update').dataTable();
            alert('Project data has been deleted...!');
            return true;
        }
        else {
            alert('Project data not deleted...!');
            return false;
        }
    }

    function isNumberKey(evt) {
        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {
            alert("Enter numeric value only.....!");
            return false;
        }
        return true;
    }

</script>
</html>
