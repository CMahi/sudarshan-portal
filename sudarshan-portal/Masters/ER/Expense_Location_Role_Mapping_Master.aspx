<%@ Page ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true" Language="C#" Async="true" CodeFile="Expense_Location_Role_Mapping_Master.aspx.cs" Inherits="Expense_Location_Role_Mapping_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <title>Expense Location Role Mapping Master</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />
</head>
<body style="overflow-x: hidden">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title">Expense Location Role Mapping Master</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal bordered-group">
                            <div class="form-group">
                                <label class="col-md-1 control-label">User Id</label>
                                <div class="col-md-2 ui-sortable">
                                    <asp:TextBox ID="txt_user_Id" CssClass="form-control" runat="server"></asp:TextBox>&nbsp;
                                     <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txt_user_Id"
                                         MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="getUser">
                                     </asp:AutoCompleteExtender>
                                </div>
                                <label class="col-md-1 control-label">Role</label>
                                <div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="ddlrole" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>

                                <label class="col-md-1 control-label">Payment Mode</label>
                                <div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="ddlpaymode" runat="server" class="form-control" onchange="enable_disable()">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-1 control-label">Location</label>
                                <div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="ddllocation" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div id="dv_add" runat="server" style="text-align: center">
                            <asp:Button ID="btn_add" runat="server" Text="Save"
                                OnClientClick="return validateType();" class="btn btn-danger btn-rounded m-b-5" OnClick="btnSaveOnClick"></asp:Button>
                            <asp:Button ID="btn_Home" runat="server" Text="Cancel" class="btn btn-danger btn-rounded m-b-5" Width="86px" OnClick="btnHomeOnClick" ForeColor="White"></asp:Button>
                        </div>
                        <div id="dv_update" runat="server" style="text-align: center; display: none">
                            <asp:Button ID="btn_update" runat="server" Text="Update" OnClientClick="return validateType();" class="btn btn-danger btn-rounded m-b-5"
                                OnClick="btnUpdateOnClick" />
                            <asp:Button ID="but_Home" runat="server" Text="Cancel" class="btn btn-danger btn-rounded m-b-5" OnClick="btnHomeOnClick" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title">Expense Location Role Mapping Details</h4>
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
    </form>
</body>

<script language="javascript" type="text/javascript">

    function validateType() {
        var userid = $("#txt_user_Id").val();
        var location = $("#ddllocation").val();
        var role = $("#ddlrole").val();
        var paymode = $("#ddlpaymode").val();

        if (userid == "") {
            alert("Please Enter User ID ...!");
            return false;
        }
        if (role == "") {

            alert("Please Select Role ...!");
            return false;
        }
        if (paymode == "") {

            alert("Please Select Paymode ...!");
            return false;
        }
        else {
            if ($("#ddlpaymode option:selected").text().toUpperCase() == "CASH" && location == "") {
                alert("Please Select Location ...!");
                return false;
            }
        }
    }

    function enable_disable() {
        $("#ddllocation").prop('selectedIndex', 0);
        if ($("#ddlpaymode option:selected").text().toUpperCase() == "CASH") {
            $("#ddllocation").removeAttr('disabled');
        }
        else {
            $("#ddllocation").attr('disabled', 'disabled');
        }
    }

    function getData(index) {
        var pk_mapp_id = $("#pk_mapp_id_" + index).val();
        var empName = $("#emp_name_" + index).val();
        var accessRoleName = $("#accessRole_name_" + index).val();
        var payMode = $("#paymode_" + index).val();
        var cashLoc = $("#cashLoc_" + index).val();

        $("#txt_PkId").val(pk_mapp_id);
        $("#txt_user_Id").val(empName);
        $("#ddlrole").val(accessRoleName);
        $("#ddlpaymode").val(payMode);
        $("#ddllocation").val(cashLoc);
        if ($("#ddlpaymode option:selected").text().toUpperCase() == "CASH") {
            $("#ddllocation").removeAttr('disabled');
        }
        else {
            $("#ddllocation").attr('disabled', 'disabled');
        }
        $('#dv_add').hide();
        $('#dv_update').show();
    }

    function deleteData(index) {
        var pk_id = $("#pk_mapp_id_" + index).val();
        var user = $("#txt_UserName").val();
        var IsDelete = confirm("Are you sure you want delete selected row ?");
        if (IsDelete) {
            Expense_Location_Role_Mapping_Master.updateExpLocRoleMapping(pk_id, user, OnDelete);
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

</script>
</html>

