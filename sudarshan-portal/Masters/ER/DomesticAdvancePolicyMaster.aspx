<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DomesticAdvancePolicyMaster.aspx.cs" Inherits="DomesticAdvancePolicyMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <title>Domestic Advance Policy Master</title>
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
                        <h4 class="panel-title">Domestic Advance Policy</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal bordered-group">
                            <div class="form-group">
                                <label class="col-md-2 control-label">Designation</label>
                                <div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="ddlDesignation" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>

                                <label class="col-md-1 control-label">City Class</label>
                                <div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="ddlCityClass" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">---Select One---</asp:ListItem>
                                        <asp:ListItem Value="A" Selected="True">A</asp:ListItem>
                                        <asp:ListItem Value="B">B</asp:ListItem>
                                        <asp:ListItem Value="C">C</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <label class="col-md-2 control-label">Amount</label>
                                <div class="col-md-2 ui-sortable">
                                    <input id="txt_Amount" runat="server" type="text" onkeypress="return isNumberKey(event)" class="form-control" style="text-align: right" />
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
                        <h4 class="panel-title">Domestic Advance Policy Details</h4>
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
        if ($("#ddlDesignation option:selected").index() < 1) {
            alert("Please Select Designation...!");
            return false;
        }
        else if ($("#ddlCityClass option:selected").index() < 1) {
            alert("Please Select City Class...!");
            return false;
        }

        else if ($("#txt_Amount").val() == "") {
            alert("Please Enter Amount...!");
            return false;
        }
        return true;
    }

    function disablediv(rowIndex) {
        document.getElementById("ddlDesignation").disabled = true;
        document.getElementById("ddlCityClass").disabled = true;
    }

    function getData(index) {
        var pk_dom_id = $("#pk_dom_id_" + index).val();
        var designation = $("#designation_" + index).val();
        var cityClass = $("#cityClass_" + index).val();
        var amount = $("#amount_" + index).val();

        $("#txt_PkId").val(pk_dom_id);
        $("#ddlDesignation").val(designation);
        $("#ddlCityClass").val(cityClass);
        $("#txt_Amount").val(amount);
        $('#dv_add').hide();
        $('#dv_update').show();
    }

    function deleteData(index) {
        var pk_id = $("#pk_dom_id_" + index).val();
        var user = $("#txt_UserName").val();
        var IsDelete = confirm("Are you sure you want delete selected row ?");
        if (IsDelete) {
            DomesticAdvancePolicyMaster.updateDomesticAdvData(pk_id, user, OnDelete);
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
