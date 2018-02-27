<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Early_Payment_Request.aspx.cs" Inherits="Early_Payment_Request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  	<!-- ================== BEGIN BASE CSS STYLE ================== -->
	<link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="../../assets/css/style.min.css" rel="stylesheet" />
	<!-- ================== END BASE CSS STYLE ================== -->
<%--<style type="text/css">
.sample
{
background-color:#DC5807;
border:1px solid black;
border-collapse:collapse;
color:White;
}
</style>--%>
    
</head>
<body style="overflow:hidden" >
    <form id="form1" runat="server">
       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
         <%-- <div id="Div1"> </div>--%>
        <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>--%>
        <div class="modal fade" id="paymentterm" >
		<div class="modal-dialog"  style="height:auto; width:98%; margin-left:1%">
			<div class="modal-content" style="background-color:ButtonFace">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
					<h4 class="modal-title"><font color="white"> Dispatch Request Details </font></h4>
				</div>
                
				<div class="modal-body" id="div_header" runat="server" data-scrollbar="true" data-height="425px">                 
                          
				</div>
                
				<div class="modal-footer">
					<a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal" >Close</a>
				</div>
			</div>
		</div>
	</div>

  	<div class="row" data-scrollbar="true" data-height="600px" >
			<div class="col-md-12">
				<div class="panel panel-danger">
					<div class="panel-heading">
                          <div class="btn-group pull-right" style="margin-top:-5px;">
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" OnClick="btnCancel_Click" Font-Bold="true"/>
						</div>
						<h3 class="panel-title"><b>Early Payment Request</b></h3>
					</div>
				</div>
			</div>
		
			<div class="col-md-12" style="margin-top:-8px;">
				<div class="panel panel-grey">
					<div class="panel-heading">
						<h4 class="panel-title">Dispatch Note Details </h4>
					</div>
					<div class="panel-body">
                        <table class="table" >
                            <tr>
                          <td class="col-md-1">
                              <asp:DropDownList ID="ddlRecords" runat="server" onchange="searchData()" CssClass="form-control" style="padding:5px">
                                  <asp:ListItem>10</asp:ListItem>
                                  <asp:ListItem>25</asp:ListItem>
                                  <asp:ListItem>50</asp:ListItem>
                                  <asp:ListItem>100</asp:ListItem>
                              </asp:DropDownList>
                            </td>
                                  <td class="col-md-2">
                                       records per page
                                      </td>
                            <td class="col-md-5">
                                &nbsp;
                            </td>
                            <td class="col-md-1" style="text-align:right; vertical-align:middle">
                                <b>Search :</b>
                            </td>
                            <td class="col-md-2" align="left">
                                  <asp:TextBox ID="txt_Search" runat="server" class="form-control" placeholder="Dispatch Request No/PO No/Invoice No/Unique No" onkeyup="searchData();" ></asp:TextBox>
                            </td>
                           </tr>
                        </table>
                		<div class="col-md-12">
                            <div class="table-responsive" id="div_InvoiceDetails" runat="server"  data-scrollbar='true' data-height='336px' >
						
						    </div>
                            
						</div>
                         <div class="col-md-12" id="div_btn" runat="server" style="margin-top:15px;">
								<div style="text-align:center">
                                     <asp:Button ID="btnRequest" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Early Payment Request" OnClientClick="return chkData()" OnClick="btnRequest_Click"/>
                                     <asp:Button ID="btnClose" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Close" OnClick="btnClose_Click"/>
                                    
								</div>
						</div>
					</div>
				</div>
			</div>
          
            <div style="display:none">
                  <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
                  <asp:TextBox ID="txt_Dispatch" runat="server"></asp:TextBox>
                <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
                  <asp:TextBox ID="txt_Condition" runat="server"></asp:TextBox>
                 <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
                 <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
                 <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
                 <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtApproverEmail" runat="server"></asp:TextBox>
                 <asp:TextBox ID="lnkText" runat="server"></asp:TextBox>
                <asp:TextBox ID="ddlText" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtDummy" runat="server"></asp:TextBox>
                <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
          </div>
		</div>
                <%--<div id="DisableDiv"> </div>--%>

         
    </form>
    <!-- ================== BEGIN BASE JS ================== -->
    <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
            <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>--%>
	<script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
	<script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../../JS/Early_Payment_Request.js"></script>
    <script src="../../assets/js/apps.min.js"></script>
	<!-- ================== END PAGE LEVEL JS ================== -->
        
    <script>
        $(document).ready(function () {
            App.init();
        });
	</script>
</body>
</html>
