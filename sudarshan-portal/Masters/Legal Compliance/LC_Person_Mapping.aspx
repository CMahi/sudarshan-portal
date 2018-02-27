<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LC_Person_Mapping.aspx.cs" Inherits="LC_Person_Mapping" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>State_Master Master</title>
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
                        <h4 class="panel-title"><b>Person Mapping Master</b></h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-3 control-label"><b>Category Name</b><font color="red">*</font></label>
                                    <div class="col-md-3 ui-sortable">
                                        <asp:DropDownList ID="ddl_Add_Cat" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-3 control-label"><b>Compliance Name</b><font color="red">*</font></label>
                                    <div class="col-md-3 ui-sortable">
                                        <asp:DropDownList ID="ddl_Add_Cmp" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">

                                    <label class="col-md-3 control-label"><b>Level Name</b><font color="red">*</font></label>
                                    <div class="col-md-3 ui-sortable">
                                        <asp:DropDownList ID="ddl_Add_Level" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-3 control-label"><b>Person Name</b><font color="red">*</font></label>
                                    <div class="col-md-3 ui-sortable">
                                        <asp:DropDownList ID="ddl_Add_Person" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
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
            <div class="modal-dialog" style="height: auto; width: 50%; margin-left: 25%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Person Mapping Details </font></h4>
                    </div>

                    <div class="modal-body" id="div_header" runat="server">

                        <div class="col-md-12">
                            <div class="col-md-2"></div>
                            <div class="col-md-3">
                                <b>Category Name</b>
                            </div>
                            <div class="col-md-5">
                                <asp:DropDownList ID="ddl_Edit_cat" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                        <div>
                            &nbsp;
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2"></div>
                            <div class="col-md-3">
                                <b>Compliance Name</b>
                            </div>
                            <div class="col-md-5">
                                <asp:DropDownList ID="ddl_Edit_Cmp" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                        <div>
                            &nbsp;
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-2"></div>
                            <div class="col-md-3">
                                <b>Level Name</b>
                            </div>
                            <div class="col-md-5">
                                <asp:DropDownList ID="ddl_Edit_level" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                        <div>
                            &nbsp;
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2"></div>
                            <div class="col-md-3">
                                <b>Person Name</b>
                            </div>
                            <div class="col-md-5">
                                <asp:DropDownList ID="ddl_Edit_Person" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>
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
                        <h4 class="modal-title"><font color="white">Person Mapping Details </font></h4>
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
                    <h4 class="panel-title"><b>Person Mapping Master Details</b></h4>
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
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Add_Cmp_ID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Edit_Cmp_ID" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
<script type="text/javascript">

    //Validation for 
    $("#btn_AddNew").click(function () {
        if ($("#ddl_Add_Cat").val() == "") {
            alert('Validation Error:Please Select Compliance Category..!');
            return false;
        }

        if ($("#ddl_Add_Cmp").val() == "") {
            alert('Validation Error:Please Select Compliance Name..!');
            return false;
        }
        if ($("#ddl_Add_Level").val() == "") {
            alert('Validation Error:Please Select Level Name..!');
            return false;
        }
        if ($("#ddl_Add_Person").val() == "") {
            alert('Validation Error:Please Select Person Name..!');
            return false;
        }

    });

    //Validation Edit
    $("#btn_Edit_onClick").click(function () {
        if ($("#ddl_Edit_cat").val() == "") {
            alert('Validation Error:Please Select Compliance Category..!');
            return false;
        }

        if ($("#ddl_Edit_Cmp").val() == "") {
            alert('Validation Error:Please Select Compliance Name..!');
            return false;
        }
        if ($("#ddl_Edit_level").val() == "") {
            alert('Validation Error:Please Select Level Name..!');
            return false;
        }
        if ($("#ddl_Edit_Person").val() == "") {
            alert('Validation Error:Please Select Person Name..!');
            return false;
        }

    });
    //Search
    function getdata() {
        var Authority_Name = $("#txt_search").val();
        LC_Person_Mapping.bindData("5", Authority_Name, callbackuser_data);
    }

    function callbackuser_data(response) {
        div_Details.innerHTML = response.value;
    }

    //Update
    function bindData(index) {
        $("#ddl_Edit_cat").val($("#txt_Cat_ID" + index).val());
        $("#ddl_Edit_Cmp").val($("#txt_Cmp_ID" + index).val());
        $("#ddl_Edit_level").val($("#txt_Level_ID" + index).val());
        $("#ddl_Edit_Person").val($("#txt_Person_ID" + index).val());
        $("#txt_PK_ID").val($("#txt_pkid" + index).val());
    }

    //Delete
    function deleteCity(index) {
        $("#txt_PK_ID").val($("#txt_pkid" + index).val());
    }

    $("#ddl_Add_Cat").change(function () {
        if ($("#ddl_Add_Cat").val() != "0") {
            var dataString = JSON.stringify({
                FK_CompType_ID: $("#ddl_Add_Cat").val()
            });

            $.ajax({
                type: "POST",
                url: "LC_Person_Mapping.aspx/getcompliance",
                contentType: "application/json; charset=utf-8",
                data: dataString,
                dataType: "json",
                success: function (response) {

                    var appr = document.getElementById("ddl_Add_Cmp");

                    for (i = appr.options.length - 1; i >= 0; i--) {
                        appr.options.remove(i);
                    }

                    approver = new Array();
                    app = new Array();
                    approver = response.d[0].split(",");
                    addOption(document.getElementById("ddl_Add_Cmp"), "---Select One---", "");
                    for (var l = 0; l < approver.length - 1; l++) {
                        app = approver[l].split("||");
                        addOption(document.getElementById("ddl_Add_Cmp"), app[1], app[0]);
                    }
                    if (appr.options.length == 1) {
                        alert('Validation Error:No Compliance Name Found..!');
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
    });

    $("#ddl_Edit_cat").change(function () {
        if ($("#ddl_Edit_cat").val() != "0") {
            var dataString = JSON.stringify({
                FK_CompType_ID: $("#ddl_Edit_cat").val()
            });

            $.ajax({
                type: "POST",
                url: "LC_Person_Mapping.aspx/getcompliance",
                contentType: "application/json; charset=utf-8",
                data: dataString,
                dataType: "json",
                success: function (response) {

                    var appr = document.getElementById("ddl_Edit_Cmp");

                    for (i = appr.options.length - 1; i >= 0; i--) {
                        appr.options.remove(i);
                    }

                    approver = new Array();
                    app = new Array();
                    approver = response.d[0].split(",");
                    addOption(document.getElementById("ddl_Edit_Cmp"), "---Select One---", "");
                    for (var l = 0; l < approver.length - 1; l++) {
                        app = approver[l].split("||");
                        addOption(document.getElementById("ddl_Edit_Cmp"), app[1], app[0]);
                    }
                    if (appr.options.length == 1) {
                        alert('Validation Error:No Compliance Name Found..!');
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
    });
    function addOption(selectbox, text, value) {
        var optn = document.createElement("OPTION");
        optn.text = text;
        optn.value = value;
        selectbox.options.add(optn);
    }

    $("#ddl_Add_Cmp").change(function () {
        if ($("#ddl_Add_Cmp").val() != "") {
            $("#txt_Add_Cmp_ID").val($("#ddl_Add_Cmp").val());
        }
    });
    $("#ddl_Edit_Cmp").change(function () {
        if ($("#ddl_Edit_Cmp").val() != "") {
            $("#txt_Edit_Cmp_ID").val($("#ddl_Edit_Cmp").val());
        }
    });

</script>
</html>
