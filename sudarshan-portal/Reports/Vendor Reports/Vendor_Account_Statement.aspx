<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vendor_Account_Statement.aspx.cs" Inherits="Vendor_Account_Statement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="utf-8" />
    <title>Vendor Account Statement Details</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

	<link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
	<link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
	<link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
	<link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
			  <div class="btn-group pull-right" style="margin-top:-5px;">
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" OnClick="btnCancel_Click"/>
						</div>
                        <h3 class="panel-title">Vendor Account Statement Details</h3>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="margin-top: -8px;">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                       
                        <h4 class="panel-title">Vendor Account Statement</h4>
                    </div>
                    <div class="panel-body">
                        <div class="row">
			<div class="col-md-1"></div>
                           
			                     
                            <div class="col-md-2">
                                    <b>From Date : <font color="red">*</font></b>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="from_date" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalenderFromDate" runat="server" TargetControlID="from_date" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                </div>
<div class="col-md-1"></div>   
  				<div class="col-md-2">
                                    <b>To Date : <font color="red">*</font></b>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="to_date" runat="server" CssClass="form-control"></asp:TextBox>                                    
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="to_date" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                </div>
			<div class="col-md-1"></div>                                
                                
                            </div>
                            <div class="hidden">
			<div class="col-md-1"></div>
                               <div class="col-md-2">
                                    <b>Company Code : <font color="red">*</font></b>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="comp_code" runat="server" Text="SCIL" CssClass="form-control"></asp:TextBox>
                                </div>    
			<div class="col-md-1"></div>
                                <div class="col-md-2">
                                    <b>Noted Line : </b>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="Noted_line" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
			
                            </div>
<div class="row"> &nbsp; </div>
			<div class="row">
                                <div class="col-md-5"></div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-sm btn-danger" Text="Show" OnClientClick="return checkControls()" OnClick="btnShow_Click"/>
                                </div>
                                <div class="col-md-5"></div>
			</div>

                        <hr />
                        <div  style="overflow: visible; margin-top:20px;">
                            <div class="row">
                                  <div class="col-md-1">
                                    <asp:DropDownList ID="ddlRecords" runat="server" AutoPostBack="true" CssClass="form-control" Style="padding: 5px" OnSelectedIndexChanged="ddlRecords_SelectedIndexChanged">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">records per page
                                </div>
                                <div class="col-md-5">&nbsp;
                                </div>
                                <div class="col-md-1" style="text-align: right; vertical-align: middle">
                                    <b>Search :</b>
                                </div>
                                <div class="col-md-2" align="left">
                                    <asp:TextBox ID="txt_Search" runat="server" class="form-control" Text="" onkeyup="searchData();"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                  <div id="div_ReportDetails" runat="server" class="table-responsive" >

                                  </div>
                               <%-- <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-bordered" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                <HeaderStyle BackColor="#717b85" ForeColor="White"/>
                                </asp:GridView>    --%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
       
        <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Authority" runat="server"></asp:TextBox>
             <asp:TextBox ID="ddlText1" runat="server"></asp:TextBox>
            <asp:TextBox ID="jccode" runat="server"></asp:TextBox>
            <asp:TextBox ID="jfdate" runat="server"></asp:TextBox>
            <asp:TextBox ID="jtdate" runat="server"></asp:TextBox>
            <asp:TextBox ID="jnoteline" runat="server"></asp:TextBox>

        </div>

	<script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
	<script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
	<script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
	<script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
    <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
    <script src="../../assets/js/demo.min.js"></script>
    <script src="../../assets/js/apps.min.js"></script>
        <script src="../../JS/Vendor_Account_Statement.js"></script>
        <script>
            $(document).ready(function () {
                App.init();

            });
        </script>
        <script>
            function checkControls() {
                if ($("#comp_code").val() == "") {
                    alert("Enter Company Code!!!");
                    return false;
                }
                else if ($("#from_date").val() == "") {
                    alert("Select From Date!!!");
                    return false;
                }
                else if ($("#to_date").val() == "") {
                    alert("Select To Date!!!");
                    return false;
                }
                else {
                    $("#jccode").val($("#comp_code").val());
                    $("#jfdate").val($("#from_date").val());
                    $("#jtdate").val($("#to_date").val());
                    $("#jnoteline").val($("#Noted_line").val());
                    return true;
                }
            }
        </script>
    </form>
</body>
</html>
