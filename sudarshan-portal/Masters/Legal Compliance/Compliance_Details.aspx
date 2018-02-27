<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Compliance_Details.aspx.cs" Inherits="Compliance_Details" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Legal Compliance Master</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row" style="margin-top: 10px" id="div_AddCountryDetails">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title"><b>Legal Compliance Master</b></h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Compliance Category</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:DropDownList ID="ddl_Cmp_Cat" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Compliance Name</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:TextBox ID="txt_Complaince_Name" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Compliance Description </b></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:TextBox ID="txt_Desc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Compliance Section/Reference  </b></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:TextBox ID="txt_Section" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Consequences of Non-Compliance </b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:TextBox ID="txt_Consequences" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Frequency of Occurance </b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:DropDownList ID="ddl_Add_Frequency" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="">---Select One---</asp:ListItem>
                                            <asp:ListItem Value="Daily">Daily</asp:ListItem>
                                            <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                            <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                            <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                            <asp:ListItem Value="Annualy">Annualy</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">

                                    <label class="col-md-2 control-label"><b>Last Date of Submission</b><font color="red">*</font></label>
                                    <label class="col-md-1 control-label"><b>Month</b></label>
                                    <div class="col-md-3 ui-sortable">
                                        <asp:DropDownList ID="ddl_Add_SMonth" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">---Select One---</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Day</b></label>
                                    <div class="col-md-3 ui-sortable">
                                        <asp:DropDownList ID="ddl_Add_SDay" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Grace Date</b><font color="red">*</font></label>
                                    <label class="col-md-1 control-label"><b>Month</b></label>
                                    <div class="col-md-3 ui-sortable">
                                        <asp:DropDownList ID="ddl_GMonth" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">---Select One---</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Day</b></label>
                                    <div class="col-md-3 ui-sortable">
                                        <asp:DropDownList ID="ddl_Grace_Days" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Sate</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:DropDownList ID="ddl_State" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Applicable Office Name </b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:DropDownList ID="ddl_Appicable_office_Name" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label" id="Assign_Name" runat="server"><b>Assign To</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable" id="Assign_Value" runat="server">
                                        <asp:DropDownList ID="ddl_Add_Assign_to" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-md-3"></div>
                        <div class="col-md-6" style="text-align: center">
                            <asp:Button ID="btn_AddNew" runat="server" Text="Add New" class="btn btn-grey btn-rounded m-b-5" OnClick="btn_AddNew_Click" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-danger btn-rounded m-b-5" OnClick="btnClear_Click" />
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="CountryPanel">
            <div class="modal-dialog" style="height: auto; width: 90%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Legal Compliance Master Details </font></h4>
                    </div>
                    <div class="col-lg-12">
                        <div class="panel panel-danger">
                            <div class="modal-body" id="div_header" runat="server">
                                <div class="panel-body">
                                    <div class="col-md-12">
                                        <div class="form-horizontal bordered-group">
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><b>Compliance Category</b><font color="red">*</font></label>
                                                <div class="col-md-4 ui-sortable">
                                                    <asp:DropDownList ID="ddl_Edit_type" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <label class="col-md-2 control-label"><b>Compliance Name</b><font color="red">*</font></label>
                                                <div class="col-md-4 ui-sortable">
                                                    <asp:TextBox ID="txt_Edit_Complaince_Name" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-horizontal bordered-group">
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><b>Compliance Description </b></label>
                                                <div class="col-md-4 ui-sortable">
                                                    <asp:TextBox ID="txt_Edit_Cmp_Desc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                                <label class="col-md-2 control-label"><b>Compliance Section/Reference  </b></label>
                                                <div class="col-md-4 ui-sortable">
                                                    <asp:TextBox ID="txt_EditSectiom" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-horizontal bordered-group">
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><b>Consequences of Non-Compliance </b><font color="red">*</font></label>
                                                <div class="col-md-4 ui-sortable">
                                                    <asp:TextBox ID="txt_Edit_Consequences" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                                <label class="col-md-2 control-label"><b>Frequency of Occurance </b><font color="red">*</font></label>
                                                <div class="col-md-4 ui-sortable">
                                                    <asp:DropDownList ID="ddl_Edit_Frequency" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="">---Select One---</asp:ListItem>
                                                        <asp:ListItem Value="Daily">Daily</asp:ListItem>
                                                        <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                                        <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                                        <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                                        <asp:ListItem Value="Annualy">Annualy</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-horizontal bordered-group">
                                            <div class="form-group">

                                                <label class="col-md-2 control-label"><b>Last Date of Submission</b><font color="red">*</font></label>
                                                <label class="col-md-1 control-label"><b>Month</b></label>
                                                <div class="col-md-3 ui-sortable">
                                                    <asp:DropDownList ID="ddl_Edit_LMonth" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0">---Select One---</asp:ListItem>
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="col-md-2 control-label"><b>Day</b></label>
                                                <div class="col-md-3 ui-sortable">
                                                    <asp:DropDownList ID="ddl_Edit_LDay" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-horizontal bordered-group">
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><b>Grace Date</b><font color="red">*</font></label>
                                                <label class="col-md-1 control-label"><b>Month</b></label>
                                                <div class="col-md-3 ui-sortable">
                                                    <asp:DropDownList ID="ddl_Edit_GMonth" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0">---Select One---</asp:ListItem>
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="col-md-2 control-label"><b>Day</b></label>
                                                <div class="col-md-3 ui-sortable">
                                                    <asp:DropDownList ID="ddl_Edit_GDay" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-horizontal bordered-group">
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><b>Sate</b><font color="red">*</font></label>
                                                <div class="col-md-4 ui-sortable">
                                                    <asp:DropDownList ID="ddl_Edit_State" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <label class="col-md-2 control-label"><b>Applicable Office Name </b><font color="red">*</font></label>
                                                <div class="col-md-4 ui-sortable">
                                                    <asp:DropDownList ID="ddl_EditOffice" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-horizontal bordered-group">
                                            <div class="form-group">
                                                <label class="col-md-2 control-label" id="Label1" runat="server"><b>Assign To</b><font color="red">*</font></label>
                                                <div class="col-md-4 ui-sortable" id="Div1" runat="server">
                                                    <asp:DropDownList ID="ddl_Edit_Assign_to" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="modal-footer">
                        <asp:Button ID="btn_upd" class="btn btn-sm btn-grey btn-rounded m-b-5" Text="Update" OnClick="btn_Edit_onClick" runat="server"></asp:Button>
                        <a href="javascript:;" class="btn btn-sm btn-danger btn-rounded m-b-5" data-dismiss="modal" id="btnClose">Close</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="DeletePanel">
            <div class="modal-dialog" style="height: auto; width: 36%; margin-left: 25%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Delete Legal Compliance Details </font></h4>
                    </div>

                    <div class="modal-body" id="div2" runat="server">
                        <div class="col-md-12">
                            <div class="col-md-2"></div>
                            <div class="col-md-8">
                                <div class="alert alert-warning" style="width: 100%; text-align: left">Are you sure you want to delete this Record?</div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                        <div class="row">&nbsp;</div>
                    </div>
                    <div class="modal-footer">
                        <button id="Button2" align="center" type="button" class="btn btn-sm btn-danger btn-rounded m-b-5" runat="server" onserverclick="btn_Delete_onClick">Yes</button>
                        <a href="javascript:;" class="btn btn-sm btn-danger btn-rounded m-b-5" data-dismiss="modal" id="deleteClose">Close</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12" id="div_ListCountryDetails">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h4 class="panel-title"><b>Legal Compliance Master Details</b></h4>
                    <div class="btn-group pull-right" style="margin-top: -31px">
                        <div class="col-md-12">
                            <input id="txt_search" style="float: right" runat="server" onkeyup="getdata();" type="text" placeholder="Compliance Name to Search" class="form-control" />
                        </div>
                    </div>

                </div>
                <div class="panel-body">
                    <div class="panel pagination-danger" id="div_Details" runat="server">
                    </div>
                </div>
            </div>
        </div>

        <div style="display: none">
            <asp:TextBox ID="txt_PK_ID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Fk_Authority_ID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Edit_Fk_Authority_ID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
<script type="text/javascript">

    //Validation for Add
    $("#btn_AddNew").click(function () {
        if ($("#ddl_Cmp_Cat").val() == "") {
            alert('Validation Error:Please Select Compliance Type..!');
            return false;
        }
        if ($("#txt_Complaince_Name").val() == "") {
            alert('Validation Error:Please Enter Complaince Name..!');
            return false;
        }

        else if ($("#ddl_Add_Frequency").val() == "") {
            alert('Validation Error:Please Select Frequency of Occurance..!');
            return false;
        }
        else if ($("#ddl_Add_SMonth").val() == "0") {
            alert('Validation Error:Please Select Month of Last Date of Submission..!');
            return false;
        }

        else if ($("#ddl_Add_SDay").val() == "0") {
            alert('Validation Error:Please Select Day of Last Date of Submission..!');
            return false;
        }
        else if ($("#ddl_GMonth").val() == "0") {
            alert('Validation Error:Please Select Month of Grace Date..!');
            return false;
        }

        else if ($("#ddl_Grace_Days").val() == "0") {
            alert('Validation Error:Please Select Day of Grace Date..!');
            return false;
        }
        else if ($("#txt_Consequences").val() == "") {
            alert('Validation Error:Please Enter Consequences of Non-Compliance..!');
            return false;
        }
        else if ($("#ddl_State").val() == "0") {
            alert('Validation Error:Please Select State Name..!');
            return false;
        }
        else if ($("#ddl_Appicable_office_Name").val() == "") {
            alert('Validation Error:Please Select Applicable Office Name..!');
            return false;
        }
        else if ($("#ddl_Add_Assign_to").val() == "") {
            alert('Validation Error:Please Select Assign To Name..!');
            return false;
        }
        return true;
    });


    //Validation for Update 
    $("#btn_upd").click(function () {
        if ($("#ddl_Edit_type").val() == "") {
            alert('Validation Error:Please Select Compliance Type..!');
            return false;
        }

        if ($("#txt_Edit_Complaince_Name").val() == "") {
            alert('Validation Error:Please Enter Complaince Name..!');
            return false;
        }
        else if ($("#ddl_Edit_State").val() == "0") {
            alert('Validation Error:Please Select State Name..!');
            return false;
        }

        else if ($("#ddl_Edit_LMonth").val() == "0") {
            alert('Validation Error:Please Select Month of Last Date of Submission..!');
            return false;
        }

        else if ($("#ddl_Edit_LDay").val() == "0") {
            alert('Validation Error:Please Select Day of Last Date of Submission..!');
            return false;
        }
        else if ($("#ddl_Edit_GMonth").val() == "0") {
            alert('Validation Error:Please Select Month of Grace Date..!');
            return false;
        }

        else if ($("#ddl_Edit_GDay").val() == "0") {
            alert('Validation Error:Please Select Day of Grace Date..!');
            return false;
        }
        else if ($("#txt_Edit_Consequences").val() == "") {
            alert('Validation Error:Please Enter Consequences of Non-Compliance..!');
            return false;
        }
        else if ($("#ddl_EditOffice").val() == "") {
            alert('Validation Error:Please Select Applicable Office Name..!');
            return false;
        }
        else if ($("#ddl_Edit_Frequency").val() == "") {
            alert('Validation Error:Please Select Frequency of Occurance..!');
            return false;
        }
        else if ($("#ddl_Edit_Assign_to").val() == "") {
            alert('Validation Error:Please Select Assign To Name..!');
            return false;
        }
        
        return true;
    });

    //Search
    function getdata() {
        var CMP_Name = $("#txt_search").val();
        Compliance_Details.bindData("6", CMP_Name, callback_data);
    }

    function callback_data(response) {
        div_Details.innerHTML = response.value;
    }

    //Applicable Office Details for Add
    $("#ddl_State").change(function () {
        if ($("#ddl_State").val() != "0") {
            Compliance_Details.getOffice($("#ddl_State").val(), callback_officedata);
        }
    });

    function callback_officedata(response) {
        var appr = document.getElementById("ddl_Appicable_office_Name");

        for (i = appr.options.length - 1; i >= 0; i--) {
            appr.options.remove(i);
        }

        approver = new Array();
        app = new Array();
        approver = response.value[0].split(",");
        addOption(document.getElementById("ddl_Appicable_office_Name"), "---Select One---", "");
        for (var l = 0; l < approver.length - 1; l++) {
            app = approver[l].split("||");
            addOption(document.getElementById("ddl_Appicable_office_Name"), app[1], app[0]);
        }
        if (appr.options.length == 1) {
            alert('Validation Error:No Applicable Office Name Found!');
        }
    }

    function addOption(selectbox, text, value) {
        var optn = document.createElement("OPTION");
        optn.text = text;
        optn.value = value;
        selectbox.options.add(optn);
    }
    $("#ddl_Appicable_office_Name").change(function () {
        $("#txt_Fk_Authority_ID").val($("#ddl_Appicable_office_Name").val());

    });

    //Applicable Office Details for Update
    $("#ddl_Edit_State").change(function () {
        if ($("#ddl_State").val() != "0") {
            Compliance_Details.getOffice($("#ddl_Edit_State").val(), callback_editofficedata);
        }
    });

    function callback_editofficedata(response) {
        var appr = document.getElementById("ddl_EditOffice");

        for (i = appr.options.length - 1; i >= 0; i--) {
            appr.options.remove(i);
        }

        approver = new Array();
        app = new Array();
        approver = response.value[0].split(",");
        addOption(document.getElementById("ddl_EditOffice"), "---Select One---", "");
        for (var l = 0; l < approver.length - 1; l++) {
            app = approver[l].split("||");
            addOption(document.getElementById("ddl_EditOffice"), app[1], app[0]);
        }
        if (appr.options.length == 1) {
            alert('Validation Error:No Applicable Office Name Found!');
        }
    }

    $("#ddl_EditOffice").change(function () {
        $("#txt_Edit_Fk_Authority_ID").val($("#ddl_EditOffice").val());

    });
    //Update
    function bindData(index) {
        var Last_Date = $("#txt_Cmp_Last_Date" + index).val().split('-');
        if (Last_Date[1] == 'January') {
            Last_Date[1] = 1;
        }
        else if (Last_Date[1] == 'February') {
            Last_Date[1] = 2;
        }
        else if (Last_Date[1] == 'March') {
            Last_Date[1] = 3;
        }
        else if (Last_Date[1] == 'April') {
            Last_Date[1] = 4;
        }
        else if (Last_Date[1] == 'May') {
            Last_Date[1] = 5;
        }
        else if (Last_Date[1] == 'June') {
            Last_Date[1] = 6;
        }
        else if (Last_Date[1] == 'July') {
            Last_Date[1] = 7;
        }
        else if (Last_Date[1] == 'August') {
            Last_Date[1] = 8;
        }
        else if (Last_Date[1] == 'September') {
            Last_Date[1] = 9;
        }
        else if (Last_Date[1] == 'October') {
            Last_Date[1] = 10;
        }
        else if (Last_Date[1] == 'November') {
            Last_Date[1] = 11;
        }
        else if (Last_Date[1] == 'December') {
            Last_Date[1] = 12;
        }

        var Grace_Date = $("#txt_Cmp_Gress_Period" + index).val().split('-');
        if (Grace_Date[1] == 'January') {
            Grace_Date[1] = 1;
        }
        else if (Grace_Date[1] == 'February') {
            Grace_Date[1] = 2;
        }
        else if (Grace_Date[1] == 'March') {
            Grace_Date[1] = 3;
        }
        else if (Grace_Date[1] == 'April') {
            Grace_Date[1] = 4;
        }
        else if (Grace_Date[1] == 'May') {
            Grace_Date[1] = 5;
        }
        else if (Grace_Date[1] == 'June') {
            Grace_Date[1] = 6;
        }
        else if (Grace_Date[1] == 'July') {
            Grace_Date[1] = 7;
        }
        else if (Grace_Date[1] == 'August') {
            Grace_Date[1] = 8;
        }
        else if (Grace_Date[1] == 'September') {
            Grace_Date[1] = 9;
        }
        else if (Grace_Date[1] == 'October') {
            Grace_Date[1] = 10;
        }
        else if (Grace_Date[1] == 'November') {
            Grace_Date[1] = 11;
        }
        else if (Grace_Date[1] == 'December') {
            Grace_Date[1] = 12;
        }

        $("#ddl_Edit_type").val($("#txt_FK_CMP_ID" + index).val());
        $("#txt_Edit_Complaince_Name").val($("#txt_Cmp_Task_Name" + index).val());
        $("#txt_Edit_Cmp_Desc").val($("#txt_Cmp_Des" + index).val());
        $("#txt_EditSectiom").val($("#txt_Cmp_Section" + index).val());
        $("#ddl_Edit_State").val($("#txt_FK_State_ID" + index).val());
        $("#ddl_EditOffice").val($("#txt_FK_Autho_ID" + index).val());
        $("#ddl_Edit_Frequency").val($("#txt_Cmp_Frequency" + index).val());
        $("#ddl_Edit_LMonth").val(Last_Date[1]);
        $("#ddl_Edit_LDay").val(Last_Date[0]);
        $("#ddl_Edit_GMonth").val(Grace_Date[1]);
        $("#ddl_Edit_GDay").val(Grace_Date[0]);
        $("#txt_Edit_Consequences").val($("#txt_Cmp_Consequence" + index).val());
        $("#txt_PK_ID").val($("#txt_pkid" + index).val());
        $("#ddl_Edit_Assign_to").val($("#txt_Assign_ID" + index).val());
    }

    //Delete
    function deleteData(index) {
        $("#txt_PK_ID").val($("#txt_pkid" + index).val());
    }




</script>
</html>
