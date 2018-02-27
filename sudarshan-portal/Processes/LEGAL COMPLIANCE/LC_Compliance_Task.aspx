<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LC_Compliance_Task.aspx.cs" Inherits="LC_Compliance_Task" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <meta charset="utf-8" />
    <title>Legal Compliance</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <!-- ================== END BASE CSS STYLE ================== -->
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="row" style="margin-top: 10px" id="Close_div" runat="server">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title"><b>Legal Compliance Details</b></h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Compliance Category</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:DropDownList ID="ddl_Comp_Cat" CssClass="form-control" runat="server" Width="70%" BackColor="#ffffff"></asp:DropDownList>
                                    </div>

                                    <label class="col-md-2 control-label"><b>Compliance Name</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:DropDownList ID="ddl_Compliance_Name" CssClass="form-control" runat="server" Width="70%" BackColor="#ffffff"></asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Compliance Description </b></label>
                                    <div class="col-md-4 ui-sortable" style="overflow: auto; height: 40px">
                                        <label class="control-label" id="lbl_Cmp_Desc" runat="server"></label>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Compliance Section/Reference  </b></label>
                                    <div class="col-md-4 ui-sortable" style="overflow: auto; height: 40px;">
                                        <label class="control-label" id="lbl_Cmp_Conse" runat="server"></label>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">

                                    <label class="col-md-2 control-label"><b>Last Date of Submission</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:TextBox ID="txt_Last_Date" runat="server" CssClass="form-control" BackColor="#ffffff" Width="70%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalenderEx1" runat="server" Format="dd/MMM/yyyy" TargetControlID="txt_Last_Date">
                                        </asp:CalendarExtender>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Grace Date</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:TextBox ID="txt_Grace_Date" runat="server" CssClass="form-control" BackColor="#ffffff" Width="70%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MMM/yyyy" TargetControlID="txt_Grace_Date">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">

                                    <label class="col-md-2 control-label"><b>Submission Date</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:TextBox ID="txt_Submission_Date" runat="server" CssClass="form-control" BackColor="#ffffff" Width="70%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MMM/yyyy" TargetControlID="txt_Submission_Date">
                                        </asp:CalendarExtender>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Email Type</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:DropDownList ID="ddl_Email_Type" CssClass="form-control" runat="server" Width="70%"></asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">

                                    <label class="col-md-2 control-label"><b>Approver Name</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:DropDownList ID="ddl_Approver" CssClass="form-control" runat="server" Width="70%"></asp:DropDownList>
                                    </div>

                                       <label class="col-md-2 control-label" id="Assign_Name" runat="server"><b>Assign To</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable" id="Assign_Value" runat="server">
                                        <asp:DropDownList ID="ddl_Assign_to" CssClass="form-control" runat="server" Width="70%"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>

                <div style="display: none">
                    <asp:TextBox ID="txt_amd_by" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedByEmail" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Step_ID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_row_no" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtXMLFiles" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Compliance_ID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Compliance_Text" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Request_No" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_wiid" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title"><b>Remarks</b></h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Remark</b></label>
                                    <div class="col-md-10 ui-sortable">
                                        <asp:TextBox runat="server" ID="txt_Remarks" CssClass="form-control" TextMode="MultiLine" Width="70%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title">Action</h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-5"></div>
                        <div class="col-md-7">
                            <asp:Button ID="btn_AddNew" runat="server" Text="Save" class="btn btn-grey btn-rounded m-b-5" OnClick="btn_AddNew_Click" />
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
        </div>
    </form>
</body>
<script type="text/javascript">
    $("#ddl_Comp_Cat").change(function () {
        if ($("#ddl_Comp_Cat").val() != "0") {
            var dataString = JSON.stringify({
                FK_CompType_ID: $("#ddl_Comp_Cat").val()
            });

            $.ajax({
                type: "POST",
                url: "LC_Compliance_Task.aspx/getcompliance",
                contentType: "application/json; charset=utf-8",
                data: dataString,
                dataType: "json",
                success: function (response) {

                    var appr = document.getElementById("ddl_Compliance_Name");

                    for (i = appr.options.length - 1; i >= 0; i--) {
                        appr.options.remove(i);
                    }

                    approver = new Array();
                    app = new Array();
                    approver = response.d[0].split(",");
                    addOption(document.getElementById("ddl_Compliance_Name"), "---Select One---", "");
                    for (var l = 0; l < approver.length - 1; l++) {
                        app = approver[l].split("||");
                        addOption(document.getElementById("ddl_Compliance_Name"), app[1], app[0]);
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

    //  Compliance Details
    $("#ddl_Compliance_Name").change(function () {

        if ($("#ddl_Compliance_Name").val() != "0") {
            $("#txt_Compliance_ID").val($("#ddl_Compliance_Name").val())
            $("#txt_Compliance_Text").val($("#ddl_Compliance_Name option:selected").text())
            var dataString = JSON.stringify({
                FK_Compliance_ID: $("#ddl_Compliance_Name").val(),
                Complaice_Type: $("#ddl_Comp_Cat").val()
            });

            $.ajax({
                type: "POST",
                url: "LC_Compliance_Task.aspx/getcomplaincedata",
                contentType: "application/json; charset=utf-8",
                data: dataString,
                dataType: "json",
                success: function (response) {
                    document.getElementById("lbl_Cmp_Desc").innerText = response.d[0];
                    document.getElementById("lbl_Cmp_Conse").innerText = response.d[1];
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
    });
    //For Save Validation
    $("#btn_AddNew").click(function () {
        if ($("#ddl_Comp_Cat").val() == "") {
            alert('Validation Error:Please Select Compliance Category..!');
            document.getElementById("ddl_Comp_Cat").focus();
            return false;
        }
        if ($("#ddl_Compliance_Name").val() == "") {
            alert('Validation Error:Please Select Compliance Name..!');
            document.getElementById("ddl_Compliance_Name").focus();
            return false;
        }
        else if ($("#txt_Last_Date").val() == "") {
            alert('Validation Error:Please Select Last Date of Submission..!');
            document.getElementById("txt_Last_Date").focus();
            return false;
        }
        else if ($("#txt_Grace_Date").val() == "") {
            alert('Validation Error:Please Select Grace Date..!');
            document.getElementById("txt_Grace_Date").focus();
            return false;
        }
        else if ($("#txt_Submission_Date").val() == "") {
            alert('Validation Error:Please Select Submission Date..!');
            document.getElementById("txt_Submission_Date").focus();
            return false;
        }
        else if ($("#ddl_Email_Type").val() == "") {
            alert('Validation Error:Please Select Email Type..!');
            document.getElementById("ddl_Email_Type").focus();
            return false;
        }
        else if ($("#ddl_Approver").val() == "") {
            alert('Validation Error:Please Select Approver Name..!');
            document.getElementById("ddl_Approver").focus();
            return false;
        } 
        else if ($("#txt_Task_Type").val() == "Manualy") {
            if ($("#ddl_Assign_to").val() == "") {
                alert('Validation Error:Please Select Assign To..!');
                document.getElementById("ddl_Assign_to").focus();
                return false;
            }
        }
        return true;
    });



</script>
</html>
