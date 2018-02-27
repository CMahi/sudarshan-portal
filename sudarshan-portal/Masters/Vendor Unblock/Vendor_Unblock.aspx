<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vendor_Unblock.aspx.cs" Inherits="Vendor_Unblock" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
  
	<title>Vendor Unblock Master</title>
	<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />

    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="row" style="margin-top:10px">
			<div class="col-lg-12">
				<div class="panel panel-danger">
					<div class="panel-heading">
                         <div class="btn-group pull-right" style="margin-top:-5px;">
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" OnClick="btnCancel_Click" Font-Bold="true"/>
						</div>
						<h4 class="panel-title">Vendor Unblock Master</h4>
					</div>
					<div class="panel-body">
						<div class="panel pagination-danger">
                            <div class="row">
                                <div class="col-md-1">
                                     <asp:DropDownList ID="ddlRecords" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRecords_SelectedIndexChanged" CssClass="form-control" style="padding:5px">
                                      <asp:ListItem>10</asp:ListItem>
                                      <asp:ListItem>25</asp:ListItem>
                                      <asp:ListItem>50</asp:ListItem>
                                      <asp:ListItem>100</asp:ListItem>
                                  </asp:DropDownList>
                                </div>
                                <div class="col-md-2" >
                                    records per page
                                </div>
                                <div class="col-md-6">
                                    &nbsp;
                                </div>
                                <div class="hidden" style="text-align:right">
                                    <b>Search : </b>
                                </div>
                                <div class="hidden">
                                     <asp:TextBox ID="txt_Search" runat="server" class="form-control" placeholder="Process Name" onkeyup="searchData();" ></asp:TextBox>
                                </div>
                            </div>
                      
                          	<div class="table-responsive" id="div_InvoiceDetails" runat="server" style="height:auto; overflow:hidden">

                          	</div>
                           
						</div>
                         <div class="col-md-12" style="text-align: center;">
                <asp:Button ID="btnRequest" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Unblock" OnClick="btnRequest_Click" OnClientClick="return prepareData()" /> <%----%>
                <asp:Button ID="btnClose" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Cancel" OnClick="btnClose_Click" />
            </div>
					</div>
				</div>
			</div>
		</div>
	    <div style="display:none">
             <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
             <asp:TextBox ID="txt_Username1" runat="server"></asp:TextBox>
            <asp:TextBox ID="lnkText" runat="server"></asp:TextBox>
            <asp:TextBox ID="ddlText" runat="server"></asp:TextBox>
            <asp:TextBox ID="ddlText1" runat="server"></asp:TextBox>

            <asp:TextBox ID="txt_Condition" runat="server"></asp:TextBox>

            <asp:TextBox ID="txt_Vendor_Code1" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Vendor_Email1" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Password1" runat="server"></asp:TextBox>

            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="split_data" runat="server"></asp:TextBox>

            <input id="ddlUser" runat="Server" type="text" />
	    </div>

	<!-- ================== BEGIN BASE JS ================== -->

        <script src="../../JS/Vendor_Unblock.js"></script>
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
     
    </form>
</body>
</html>
