<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LC_Compliace_Request.aspx.cs" Inherits="LC_Compliace_Request" ValidateRequest="false" EnableEventValidation="false" %>

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
    <script src="../../JS/canvasjs.min.js"></script>
    <!-- ================== END BASE CSS STYLE ================== -->
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="row" style="margin-top: 10px">
            <div id="Div1" class="col-md-6" runat="server">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h3 class="panel-title">Report Selection</h3>
                    </div>
                    <div id="Div2" class="panel-body" runat="server">
                        <div data-scrollbar="true" data-height="200px">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-3">Compliance Category</label>
                                    <div class="col-md-9 m-b-10">
                                        <asp:DropDownList ID="ddl_Comp_Cat" CssClass="form-control" runat="server" Width="70%" BackColor="#ffffff"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3">Compliance Name</label>
                                    <div class="col-md-9 m-b-10">
                                        <asp:DropDownList ID="ddl_Compliance_Name" CssClass="form-control" runat="server" Width="70%" BackColor="#ffffff"></asp:DropDownList>
                                    </div>
                                </div>
                                      <div class="form-group">
                                    <label class="col-md-3">Status</label>
                                     <div class="col-md-9 m-b-10">
                                        <asp:DropDownList ID="ddl_Staus" CssClass="form-control" runat="server" Width="70%">
                                            <asp:ListItem Value="">---Select One---</asp:ListItem>
                                            <asp:ListItem Value="Not Started">Not Started</asp:ListItem>
                                            <asp:ListItem Value="On Hold">On Hold</asp:ListItem>
                                            <asp:ListItem Value="In Process">In Process </asp:ListItem>
                                            <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3">From Date</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_From_Date" runat="server" CssClass="form-control" BackColor="#ffffff"  onchange="return CompareDates()"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MMM/yyyy" TargetControlID="txt_From_Date">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3">To Date</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txt_To_Date" runat="server" CssClass="form-control" BackColor="#ffffff"  onchange="return CompareDates()"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MMM/yyyy" TargetControlID="txt_To_Date">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                          
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6" id="div_Graph">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-bar-chart"></i>Compliance Count</h3>
                    </div>
                    <div class="panel-body" id="div_GraphDetails" style="text-align: center">
                        <div data-scrollbar="true" data-height="200px">
                            <div id="chartContainer" class="table-responsive" style="height: 273px;">
                                <asp:Literal ID="Fusionchart" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="float: left">
                <asp:Button ID="btn_add" class="btn btn-grey btn-rounded m-b-5" runat="server" Text="Show" OnClick="GetReport" />&nbsp;
                  <asp:Button ID="btn_Clear" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Clear" OnClick="BtnClear_Click" />&nbsp;
                <div class="panel-body" id="div3" style="text-align: center">
                </div>
            </div>

            
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title"><b>Compliance Details</b></h4>
                    </div>
                    <div class="panel-body">
                        <div class="panel pagination-danger" id="div_Details" runat="server">
                        </div>
                    </div>
                </div>
            </div>

            <div style="display: none">
                <asp:TextBox ID="txt_amd_by" runat="server"></asp:TextBox>
                <asp:TextBox ID="txt_Created_by" runat="server"></asp:TextBox>
                <asp:TextBox ID="txt_Comp_ID" runat="server"></asp:TextBox>
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
                url: "LC_Compliace_Request.aspx/getcompliance",
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

    $("#ddl_Compliance_Name").change(function () {
        if ($("#ddl_Compliance_Name").val() != "") {
            $("$txt_Comp_ID").val($("#ddl_Compliance_Name").val());
        }
    });


    function CompareDates() {
        LC_Compliace_Request.checkdate($("#txt_From_Date").val(),$("#txt_To_Date").val(), callbacktabledates);
    }
    function callbacktabledates(response) {
        if (response.value != "true") {
            alert(response.value);
            document.getElementById("txt_To_Date").value = "";
        }
    }



</script>
</html>
