<%@ Page ValidateRequest="false" EnableEventValidation="false" AutoEventWireup="true" Language="C#" Async="true" CodeFile="Vendor_Discount_Rate_Master.aspx.cs" Inherits="Vendor_Discount_Rate_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Vendor Plant</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />

    <!-- ================== END BASE CSS STYLE ================== -->
</head>
<body style="overflow-x: hidden">
    <form id="frm_report" runat="server">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title">Vendor Discount Rate</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal bordered-group">
                            <div class="form-group">


                                <label class="col-md-3 control-label">Vendor Name</label>
                                <div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="ddlvendor" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-1 control-label">Dicount Rate</label>
                                <div class="col-md-2 ui-sortable">
                                    <asp:TextBox ID="txt_discount_rate" CssClass="form-control" runat="server" onkeypress="return validateDecimal(event,this)"></asp:TextBox>&nbsp;
                                    
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-5"></div>
                                <div class="col-md-7">
                                    <asp:Button ID="btn_Add" runat="server" Text="Save" OnClick="btn_New_onClick"
                                        OnClientClick="return validatetype();" class="btn btn-danger btn-rounded"></asp:Button>
                                    <asp:Button ID="btn_Home" runat="server" Text="Cancel" class="btn btn-danger btn-rounded"  OnClick="btnCancel_Click" ForeColor="White"></asp:Button>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-2"></div>
            <div class="col-md-12" id="div_ShipperDetails" runat="server">
                <div class="panel panel-danger" id="div_Details" runat="server">
                    <div class="panel-heading">
                        <h4 class="panel-title">Vendor Discount Rate</h4>
                    </div>
                    <div class="panel-body">
                        <div class="panel pagination-danger">
                            <div class="table-responsive" style="width: 100%" id="divalldata" runat="server">
                                <div class="clearfix"></div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="edit" tabindex="-1" role="dialog" aria-labelledby="edit" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="panel-title" id="Heading">Edit Your Detail</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label>Vendor Name</label>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList ID="ddleditvendor" runat="server" class="form-control" >
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Discount Rate</label>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_edit_drate" CssClass="form-control" runat="server" onkeypress="return validateDecimal(event,this)"></asp:TextBox>
                        </div>
                        <div class="control-group" style="width: 100%; top: auto; text-align: center">
                            <asp:Button ID="btn_upd" runat="server" Text="Update" OnClick="btn_Edit_onClick"
                                OnClientClick="return validatetypeedit();" class="btn btn-grey btn-rounded m-b-5"></asp:Button>

                        </div>
                    </div>

                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>


        <!-- /.modal-dialog -->

        <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-labelledby="edit" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="panel-title" id="H1">Delete this entry</h4>
                    </div>

                    <div class="modal-body" style="width: 100%; text-align: center">

                        <div class="alert alert-warning" style="width: 100%; text-align: left">Are you sure you want to delete this record?</div>
                        <button id="Button2" align="center" type="button" class="btn btn-danger" runat="server" onserverclick="btn_Delete_onClick">Yes</button>
                        <button id="btn_del" type="button" class="btn btn-danger" data-dismiss="modal" aria-hidden="true">No</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <div id="conditionButton" style="display: none">
            <asp:TextBox ID="txtHeadIdID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_DomainName" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_procedure" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_SearchString" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtCondition" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtParameterID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtCreatedBy" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtExpenseTypeID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_AMDDate" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Description" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ActionObjectID" runat="server"></asp:TextBox>
        </div>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.4.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <!-- ================== END PAGE LEVEL JS ================== -->
    </form>
</body>
<script language="javascript" type="text/javascript">
    var searchValue = '';
    SearchArray = new Array(3);
    DisplayArray = new Array(3);
    SearchArray[0] = new Array(5);
    DisplayArray[0] = new Array(10);

    $(document).ready(function () {
        App.init();
    });
    function callback_Success(response) {
        if (response.value != "") {
            var sp_data = (response.value).split("@@");
            $("#txt_edit_drate").val(sp_data[0]);
            $("#ddleditvendor").val(sp_data[1]);
        }
        else {
            alert("Invalid Record...!");
            $("#edit").hide();
        }
    }
    function editdatails(data) {
        $("#txtHeadIdID").val(data);
        Vendor_Discount_Rate_Master.getVendorData(data, callback_Success);
    }
    function deletedetails(data) {
        $("#txtHeadIdID").val(data);
        Vendor_Plant_Master.getLocationData(data, callback_Success);
    }

    function btn_Search_onClick(id) {
        searchValue = id;
        if (searchValue == 1) {
            SearchArray[0][0] = "select";
            SearchArray[0][1] = "VENDOR_DISCOUNT";
            SearchArray[0][2] = "Vendor Name";
            SearchArray[0][3] = "TextBox";
        }
    }
    function validatetype() {

        var drate = $("#txt_discount_rate").val();
        var vname = $("#ddlvendor").val();
        if (vname == 0) {
            alert("Please Select Vendor Name...!");
            return false;
        }
        if (drate == "") {
            alert("Please Enter Discount Rate...!");
            return false;
        }
    }
    function validatetypeedit() {
        var drate = $("#txt_edit_drate").val();
        var plantname = $("#ddleditvendor").val();
        if (plantname == 0) {
            alert("Please Select Vendor Name...!");
            return false;
        }

        if (drate == "") {
            alert("Please Enter Discount Rate...!");
            return false;
        }
    }
    function getdata() {
        var char = document.getElementById("txt_search").value;
        document.getElementById("txt_SearchString").value = "%" + char + "%";

    }
    function callback_data(response) {
        divalldata.innerHTML = response.value;
    }
    function validateDecimal(eve, myid) {
        var charCode = (eve.which) ? eve.which : event.keyCode;
        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode != 46) {
            return false
        }
        if (myid.value.indexOf(".") !== -1) {
            if (myid.value != "") {
            }
            else {
                var number = myid.value.split('.');
                if (number.length == 2 && number[1].length > 1)
                    return false;
            }
        }
        return true
    }
</script>
</html>
