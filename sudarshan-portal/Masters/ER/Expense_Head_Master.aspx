<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expense_Head_Master.aspx.cs" Inherits="Expense_Head_Master" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Expense Head</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />
</head>
<body style="overflow-x: hidden">
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title">Expense Head</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal bordered-group">
                            <div class="form-group" id="tbl_showMyAddPanel" style="display: block" runat="server">
                                <label class="col-md-2 control-label">Expense Head</label>
                                <div class="col-md-2 ui-sortable">
                                    <input id="txt_exp_head" runat="server" name="txt_name" type="text" class="form-control" />
                                </div>
                                <label class="col-md-1 control-label">Expense Description</label>
                                <div class="col-md-2 ui-sortable">
                                    <input id="txt_exp_desc" runat="server" type="text" value="0" class="form-control" style="text-align: right" />
                                </div>
                                <label class="col-md-2 control-label">SAP GLCode</label>
                                <div class="col-md-3 ui-sortable">
                                    <input id="txt_glcode" runat="server" name="txt_desc" maxlength="10" type="text" class="form-control" />
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
        </div>

        <div class="col-md-12">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h4 class="panel-title">Expense Head Details</h4>
                </div>
                <div class="panel-body">
                    <div id="dv_dtl" runat="server" class="panel pagination-danger">
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
            <asp:TextBox ID="txt_SearchString" runat="server" Text="%"></asp:TextBox>
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
        var exp_headName = $("#txt_exp_head").val();
        var exp_desc = $("#txt_exp_desc").val();
        var glCode = $("#txt_glcode").val();

        if (exp_headName == "") {
            alert("Please Enter Parameter Name...!");
            return false;
        }
        if (exp_desc == "") {
            alert("Please Enter Value...!");
            return false;
        }
        if (glCode == "") {
            alert("Please Enter Expense Description...!");
            return false;
        }
        return true;
    }

    function getData(index) {
        var pk_id = $("#pk_id_" + index).val();
        var expHead_name = $("#expHead_name_" + index).val();
        var exp_desc = $("#exp_desc_" + index).val();
        var glCode = $("#glCode_" + index).val();

        $("#txt_PkId").val(pk_id);
        $("#txt_exp_head").val(expHead_name);
        $("#txt_exp_desc").val(exp_desc);
        $("#txt_glcode").val(glCode);
        $('#dv_add').hide();
        $('#dv_update').show();
    }

    function deleteData(index) {
        var pk_id = $("#pk_id_" + index).val();
        var user = $("#txt_UserName").val();
        var IsDelete = confirm("Are you sure you want delete selected row ?");
        if (IsDelete) {
            Expense_Head_Master.updateExpenseData(pk_id, user, OnDelete);
        }
    }

    function OnDelete(response) {
        if (response.value != "false") {
            $("#dv_dtl").html(response.value);
            $('#tbl_update').dataTable();
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
