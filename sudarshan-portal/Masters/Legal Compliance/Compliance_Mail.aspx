<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Compliance_Mail.aspx.cs" Inherits="Compliance_Mail" ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Compliance Reminder Mail Master</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="row" style="margin-top: 10px" id="div_AddCountryDetails">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title"><b>Compliance Reminder Mail Master</b></h4>
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
                    </div>

                </div>
            </div>
        </div>

        <div class="col-md-12" id="div_ListCountryDetails">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h4 class="panel-title"><b>Compliance Reminder Mail Master Details</b></h4>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <div class="tab-pane fade active in" id="nav-pills-tab-1" style="padding-top: 0px">
                <div class="panel panel-inverse">
                    <div class="panel-body " id="div5" runat="server">

                        <ul class="nav nav-pills">
                            <li><a href="#Before_Tab" data-toggle="tab">Before</a></li>
                            <li><a href="#After_Tab" data-toggle="tab">After</a></li>

                        </ul>
                        <div class="tab-content " style="padding-top: 0px">
                            <div id="Before_Tab" class="tab-pane fade table-responsive" runat="server">
                                
                                <div id="Exist_Before_Details" runat="server" class="panel pagination-danger">
                                </div>

                            </div>

                            <div id="After_Tab" class="tab-pane fade table-responsive" runat="server">
                                
                                <div id="Exist_After_Details" runat="server" class="panel pagination-danger" >
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
            </div>
        </div>

                <div id="hiddenContent">
                <asp:Button ID="btnTemps" runat="server" Style="display: none" OnClientClick="javascript:void(0);" />
                <asp:ModalPopupExtender ID="MP_Loading" DropShadow="false" runat="server" TargetControlID="btnTemps"
                    PopupControlID="pnlPopups" BackgroundCssClass="modalBackground" />
                <asp:Panel ID="pnlPopups" runat="server" Style="display: none; width: 30%; background-color: White"
                    BorderColor="Red">
                    <asp:UpdatePanel ID="updatePanels1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div align="center" style="border: solid 2px Black">
                                <div>
                                    <img alt="Processing" id="img_Progress" src="/Portal/Img/loading_transparent.gif" />
                                </div>
                                <div>
                                    <font color="red"><b>Request Is Processing</font>
                                </div>
                            </div>
                            </b>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>



        <div style="display: none">
            <asp:TextBox ID="txt_PK_ID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Before_Check" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Before_XML" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_After_Check" runat="server"></asp:TextBox>
            <asp:TextBox ID="txr_After_XML" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Created_date" runat="server"></asp:TextBox>
             <asp:TextBox ID="txt_Compliance_ID" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
<script type="text/javascript">

    //Validation
    $("#btn_AddNew").click(function () {
        if ($("#ddl_Add_Cat").val() == "") {
            alert('Validation Error:Please Select Compliance Category..!');
            return false;
        }
        if ($("#ddl_Add_Cmp").val() == "") {
            alert('Validation Error:Please Enter Compliance Name..!');
            return false;
        }
        //Before XML
        $("#txt_Compliance_ID").val($("#ddl_Add_Cmp").val());
        var  Before_XML = "<ROWSET>";
        var tbl1 = document.getElementById("tbl_Before");
        var lastRow1 = tbl1.rows.length;
        if (lastRow1 > 1) {
            for (var j = 0; j < lastRow1 - 1; j++) {
                if ($("#txt_BeDays" + (j + 1) + "").val().trim() == "") {
                    alert("Validation Error:Please Enter Before Days At Row " + (j + 1) + " ...!");
                    document.getElementById("txt_BeDays" + (j + 1) + "").focus();
                    return false;
                }
                if ($("#ddl_BeLevels" + (j + 1) + "").val() == "") {
                    alert("Validation Error:Please Select Before To client At Row " + (j + 1) + " ...!");
                    document.getElementById("ddl_BeLevels" + (j + 1) + "").focus();
                    return false;
                }
                if ($("#ddl_BeEmail_Type" + (j + 1) + "").val() == "") {
                    alert("Validation Error:Please Select Before Email Type At Row " + (j + 1) + " ...!");
                    document.getElementById("ddl_BeEmail_Type" + (j + 1) + "").focus();
                    return false;
                }
                    $("#txt_Before_Check").val("Before");
                    Before_XML += "<ROW>";
                    Before_XML += "<FK_COMPLIANCE_ID>" + $("#ddl_Add_Cmp").val() + "</FK_COMPLIANCE_ID>";
                    Before_XML += "<MAIL_CATEGORY>Before</MAIL_CATEGORY>";
                    Before_XML += "<DAYS>" + $("#txt_BeDays" + (j + 1) + "").val() + "</DAYS>";
                    Before_XML += "<FK_LEVEL_ID>" + $("#ddl_BeLevels" + (j + 1) + "").val() + "</FK_LEVEL_ID>";
                    Before_XML += "<FK_EMAIL_TYPE>" + $("#ddl_BeEmail_Type" + (j + 1) + "").val() + "</FK_EMAIL_TYPE>";
                    Before_XML += "<ISACTIVE>1</ISACTIVE>";
                    Before_XML += "<CREATED_BY>" + $("#txt_Username").val() + "</CREATED_BY>";
                    Before_XML += "<CREATED_DATE>" + $("#txt_Created_date").val() + "</CREATED_DATE>";
                    Before_XML += "</ROW>";
            }
            Before_XML += "</ROWSET>";
            document.getElementById("txt_Before_XML").value = Before_XML;
        }

        //After XML
        var After_XML = "<ROWSET>";
        var tbl1 = document.getElementById("tbl_After");
        var lastRow1 = tbl1.rows.length;
        if (lastRow1 > 1) {
            for (var j = 0; j < lastRow1 - 1; j++) {
                if ($("#txt_Af_Days" + (j + 1) + "").val().trim() == "") {
                    alert("Validation Error:Please Enter After Days At Row " + (j + 1) + " ...!");
                    document.getElementById("txt_Af_Days" + (j + 1) + "").focus();
                    return false;
                }
                if ($("#ddl_AfLevels" + (j + 1) + "").val() == "") {
                    alert("Validation Error:Please Select After To client At Row " + (j + 1) + " ...!");
                    document.getElementById("ddl_AfLevels" + (j + 1) + "").focus();
                    return false;
                }
                if ($("#ddl_AfEmailType" + (j + 1) + "").val() == "") {
                    alert("Validation Error:Please Select After Email Type At Row " + (j + 1) + " ...!");
                    document.getElementById("ddl_AfEmailType" + (j + 1) + "").focus();
                    return false;
                }
                $("#txt_After_Check").val("Before");
                After_XML += "<ROW>";
                After_XML += "<FK_COMPLIANCE_ID>" + $("#ddl_Add_Cmp").val() + "</FK_COMPLIANCE_ID>";
                After_XML += "<MAIL_CATEGORY>After</MAIL_CATEGORY>";
                After_XML += "<DAYS>" + $("#txt_Af_Days" + (j + 1) + "").val() + "</DAYS>";
                After_XML += "<FK_LEVEL_ID>" + $("#ddl_AfLevels" + (j + 1) + "").val() + "</FK_LEVEL_ID>";
                After_XML += "<FK_EMAIL_TYPE>" + $("#ddl_AfEmailType" + (j + 1) + "").val() + "</FK_EMAIL_TYPE>";
                After_XML += "<ISACTIVE>1</ISACTIVE>";
                After_XML += "<CREATED_BY>" + $("#txt_Username").val() + "</CREATED_BY>";
                After_XML += "<CREATED_DATE>" + $("#txt_Created_date").val() + "</CREATED_DATE>";
                After_XML += "</ROW>";
            }
            After_XML += "</ROWSET>";
            document.getElementById("txr_After_XML").value = After_XML;
            $find("MP_Loading").show();
        }

    });

    $("#ddl_Add_Cmp").change(function () {
        if ($("#ddl_Add_Cmp").val() != "") {
            $("#txt_PK_ID").val($("#ddl_Add_Cmp").val());
            Compliance_Mail.getComplianceDetails($("#ddl_Add_Cat").val(), $("#ddl_Add_Cmp").val(), callback_ComplianceDtl);
        }
    });

    function callback_ComplianceDtl(response) {
        if (response.value[0] != "") {
            Exist_Before_Details.innerHTML = response.value[0];
        }
        if (response.value[1] != "") {
            Exist_After_Details.innerHTML = response.value[1];
        }
    }

    $("#ddl_Add_Cat").change(function () {
     Compliance_Mail.getcompliance($("#ddl_Add_Cat").val(), callback_Complianceddl);
    });

    function callback_Complianceddl(response) {

        var appr = document.getElementById("ddl_Add_Cmp");

        for (i = appr.options.length - 1; i >= 0; i--) {
            appr.options.remove(i);
        }

        approver = new Array();
        app = new Array();
        approver = response.value[0].split(",");
        addOption(document.getElementById("ddl_Add_Cmp"), "---Select One---", "");
        for (var l = 0; l < approver.length - 1; l++) {
            app = approver[l].split("||");
            addOption(document.getElementById("ddl_Add_Cmp"), app[1], app[0]);
        }
        if (appr.options.length == 1) {
            alert('Validation Error:No Compliance Name Found..!');
        }
    }
    function addOption(selectbox, text, value) {
        var optn = document.createElement("OPTION");
        optn.text = text;
        optn.value = value;
        selectbox.options.add(optn);
    }

    //Add Before and Afer New Row
    function newRow(type) {
        if (type == 'Before') {
            $("#txt_Before_Check").val(type);
            var BeforeHtml = "";
            var rowIndex = $('#tbl_Before tr').length;
            for (var i = 0; i < rowIndex - 1; i++) {
                if ($("#txt_BeDays" + (i + 1) + "").val() == "") {
                    alert('Validation Error:Please Enter the Days..!');
                    return false;
                }
                if ($("#ddl_BeLevels" + (i + 1) + "").val() == "") {
                    alert('Validation Error:Please Select To Client..!');
                    return false;
                }

                if ($("#ddl_BeEmail_Type" + (i + 1) + "").val() == "") {
                    alert('Validation Error:Please Select Email Type..!');
                    return false;
                }
            
            }
            BeforeHtml = '<tr>';
            BeforeHtml += '<td  width="2%"><img id=Be_add' + (rowIndex) + ' style="max-width:none;" src="/Sudarshan-Portal/Img/MoveUp.GIF"  onclick=newRow("Before") alt="Click here to Add New Row." /></td>';
            BeforeHtml += '<td  width="3%"><img id=Be_delete' + (rowIndex) + ' style="max-width:none;" src="/Sudarshan-Portal/Img/button_cancel.png"  onclick=DeleteRow("Before",' + (rowIndex) + ') alt="Click here to Delete Row." /></td>';
            BeforeHtml += '<td  width="8%"><input type="text" onkeyup="return chknumeric(this.id);"   id=txt_BeDays' + (rowIndex) + ' class="form-control input-md"  /></td>';
            BeforeHtml += '<td  width="15%"><select name="select" id=ddl_BeLevels' + (rowIndex) + '  class="form-control input-md" >"' + getBeforeLevels(rowIndex) + '"</select></td>';
            BeforeHtml += '<td  width="15%"><select name="select" id=ddl_BeEmail_Type' + (rowIndex) + '  class="form-control input-md" >"' + getBeforeLevels(rowIndex) + '"</select></td>';
            BeforeHtml += '</tr>';
            $("#tbl_Before").append(BeforeHtml);

        }
        else {
            $("#txt_Before_Check").val(type);
            var AfterHtml = "";
            var rowIndex = $('#tbl_After tr').length;
            for (var i = 0; i < rowIndex - 1; i++) {
                if ($("#txt_Af_Days" + (i + 1) + "").val() == "") {
                    alert('Validation Error:Please Enter the Days..!');
                    return false;
                }
                if ($("#ddl_AfLevels" + (i + 1) + "").val() == "") {
                    alert('Validation Error:Please Select To Client..!');
                    return false;
                }

                if ($("#ddl_AfEmailType" + (i + 1) + "").val() == "") {
                    alert('Validation Error:Please Select Email Type..!');
                    return false;
                }
            }
            AfterHtml = '<tr>';
            AfterHtml += '<td  width="2%"><img id=Af_add' + (rowIndex) + ' style="max-width:none;" src="/Sudarshan-Portal/Img/MoveUp.GIF"  onclick=newRow("After") alt="Click here to Add New Row." /></td>';
            AfterHtml += '<td  width="3%"><img id=Af_delete' + (rowIndex) + ' style="max-width:none;" src="/Sudarshan-Portal/Img/button_cancel.png"  onclick=DeleteRow("After",' + (rowIndex) + ') alt="Click here to Delete Row." /></td>';
            AfterHtml += '<td  width="8%"><input type="text" onkeyup="return chknumeric(this.id);"   id=txt_Af_Days' + (rowIndex) + ' class="form-control input-md"  /></td>';
            AfterHtml += '<td  width="15%"><select name="select" id=ddl_AfLevels' + (rowIndex) + '  class="form-control input-md" >"' + getAfterLevels(rowIndex) + '"</select></td>';
            AfterHtml += '<td  width="15%"><select name="select" id=ddl_AfEmailType' + (rowIndex) + '  class="form-control input-md" >"' + getAfterLevels(rowIndex) + '"</select></td>';
            AfterHtml += '</tr>';
            $("#tbl_After").append(AfterHtml);
        }

    }

    //Before Level and Email Type
    function getBeforeLevels(id) {
        Compliance_Mail.getLevel(id, callback_getLevel);
    }


    function callback_getLevel(response) {
        if (response.value[0] != "") {
            var Row_id = response.value[2];
            var tab = document.getElementById("tbl_Before");
            var lastRow = tab.rows.length;
            if (lastRow > 1) {
                var appr = document.getElementById("ddl_BeLevels" + (Row_id));
                for (i = appr.options.length - 1; i >= 0; i--) {
                    appr.options.remove(i);
                }
                approver = new Array();
                app = new Array();
                approver = response.value[0].split(",");
                addOption(document.getElementById("ddl_BeLevels" + (Row_id)), "---Select One---", "");
                for (var l = 0; l < approver.length - 1; l++) {
                    app = approver[l].split("||");
                    addOption(document.getElementById("ddl_BeLevels" + (Row_id)), app[1], app[0]);
                }
                if (appr.options.length == 1) {
                    alert('No Levels Found!');
                }
            }
        }
        if (response.value[1] != "") {
            var tab = document.getElementById("tbl_Before");
            var Row_id = response.value[2];
            if (lastRow > 1) {
                var appr = document.getElementById("ddl_BeEmail_Type" + (Row_id));
                for (i = appr.options.length - 1; i >= 0; i--) {
                    appr.options.remove(i);
                }
                approver = new Array();
                app = new Array();
                approver = response.value[1].split(",");
                addOption(document.getElementById("ddl_BeEmail_Type" + (Row_id)), "---Select One---", "");
                for (var l = 0; l < approver.length - 1; l++) {
                    app = approver[l].split("||");
                    addOption(document.getElementById("ddl_BeEmail_Type" + (Row_id)), app[1], app[0]);
                }
                if (appr.options.length == 1) {
                    alert('No Email Type Found!');
                }
            }
        }
    }
    //After Level and Email Type
    function getAfterLevels(id) {
        Compliance_Mail.getLevel(id, callback_AftergetLevel);
    }


    function callback_AftergetLevel(response) {
        if (response.value[0] != "") {
            var Row_id = response.value[2];
            var tab = document.getElementById("tbl_After");
            var lastRow = tab.rows.length;
            if (lastRow > 1) {
                var appr = document.getElementById("ddl_AfLevels" + (Row_id));
                for (i = appr.options.length - 1; i >= 0; i--) {
                    appr.options.remove(i);
                }
                approver = new Array();
                app = new Array();
                approver = response.value[0].split(",");
                addOption(document.getElementById("ddl_AfLevels" + (Row_id)), "---Select One---", "");
                for (var l = 0; l < approver.length - 1; l++) {
                    app = approver[l].split("||");
                    addOption(document.getElementById("ddl_AfLevels" + (Row_id)), app[1], app[0]);
                }
                if (appr.options.length == 1) {
                    alert('No Levels Found!');
                }
            }
        }
        if (response.value[1] != "") {
            var tab = document.getElementById("tbl_After");
            var Row_id = response.value[2];
            if (lastRow > 1) {
                var appr = document.getElementById("ddl_AfEmailType" + (Row_id));
                for (i = appr.options.length - 1; i >= 0; i--) {
                    appr.options.remove(i);
                }
                approver = new Array();
                app = new Array();
                approver = response.value[1].split(",");
                addOption(document.getElementById("ddl_AfEmailType" + (Row_id)), "---Select One---", "");
                for (var l = 0; l < approver.length - 1; l++) {
                    app = approver[l].split("||");
                    addOption(document.getElementById("ddl_AfEmailType" + (Row_id)), app[1], app[0]);
                }
                if (appr.options.length == 1) {
                    alert('No Email Type Found!');
                }
            }
        }
    }


    function addOption(selectbox, text, value) {
        var optn = document.createElement("OPTION");
        optn.text = text;
        optn.value = value;
        selectbox.options.add(optn);
    }

    //Check Numeric

    function chknumeric(id) {
        var a = isNaN(document.getElementById(id).value);
        if (a == true) {
            alert("Validation Error:Enter Only Numbers.!.");
            document.getElementById(id).value = "";
            document.getElementById(id).focus();
            return false;
        }
    }

    //to delete Row
    function DeleteRow(type, RowIndex) {
        try {
            if (type=='Before') {
                var tbl = document.getElementById("tbl_Before");
                var lastRow = tbl.rows.length;
                if (lastRow <= 2) {
                    alert("Validation Error:You have to Enter atleast one record..!");
                    return false;
                   }
                for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
                    document.getElementById("Be_add" + (parseInt(contolIndex) + 1)).onclick = new Function("newRow('Before')");
                    document.getElementById("Be_add" + (parseInt(contolIndex) + 1)).id = "Be_add" + contolIndex;
                    document.getElementById("Be_delete" + (parseInt(contolIndex) + 1)).onclick = new Function("DeleteRow('Before'," + contolIndex + ")");
                    document.getElementById("Be_delete" + (parseInt(contolIndex) + 1)).id = "Be_delete" + contolIndex;
                    document.getElementById("txt_BeDays" + (parseInt(contolIndex) + 1)).id = "txt_BeDays" + contolIndex;
                    document.getElementById("ddl_BeLevels" + (parseInt(contolIndex) + 1)).id = "ddl_BeLevels" + contolIndex;
                    document.getElementById("ddl_BeEmail_Type" + (parseInt(contolIndex) + 1)).id = "ddl_BeEmail_Type" + contolIndex;
                }
                tbl.deleteRow(RowIndex);
            }
            else {
                var tbl1 = document.getElementById("tbl_After");
                var lastRow1 = tbl1.rows.length;
                if (lastRow1 <= 2) {
                    alert("Validation Error:You have to Enter atleast one record..!");
                    return false;
                }
                for (var contolIndex = RowIndex; contolIndex < lastRow1 - 1; contolIndex++) {
                    document.getElementById("Af_add" + (parseInt(contolIndex) + 1)).onclick = new Function("newRow('After')");
                    document.getElementById("Af_add" + (parseInt(contolIndex) + 1)).id = "Af_add" + contolIndex;
                    document.getElementById("Af_delete" + (parseInt(contolIndex) + 1)).onclick = new Function("DeleteRow('After'," + contolIndex + ")");
                    document.getElementById("Af_delete" + (parseInt(contolIndex) + 1)).id = "Af_delete" + contolIndex;
                    document.getElementById("txt_Af_Days" + (parseInt(contolIndex) + 1)).id = "txt_Af_Days" + contolIndex;
                    document.getElementById("ddl_AfLevels" + (parseInt(contolIndex) + 1)).id = "ddl_AfLevels" + contolIndex;
                    document.getElementById("ddl_AfEmailType" + (parseInt(contolIndex) + 1)).id = "ddl_AfEmailType" + contolIndex;
                }
                tbl1.deleteRow(RowIndex);
            }
       
        }
        catch (Exc) { }
    }
</script>


</html>

