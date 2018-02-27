<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SUB_CONTRACT_PO_DC _GENRATE.aspx.cs" Inherits="SUB_CONTRACT_PO_DC_GENRATE" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  	<meta charset="utf-8" />
	<title>SUDARSHAN</title>
	<link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
	<link href="../../assets/css/style.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
  
	
		<div class="row">
			<div class="col-md-12">
				<div class="panel panel-danger">
					<div class="panel-heading">
						
						<h3 class="panel-title"><b>Sub Contract PO Delivery Chalan Acknowledgement</b></h3>
					</div>
				</div>
			</div>
			<div class="col-md-12" style="margin-top:-8px;">
				<div class="panel panel-grey">
					<div class="panel-heading">
						<h4 class="panel-title">Delivery Chalan Details</h4>
					</div>
					<div class="panel-body">
						<div class="table-responsive" id="div_Details" runat="server">
						    
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-12" style="margin-top:-8px;" id="div_Action">
				<div class="panel panel-grey">
					<div class="panel-heading">
						<h4 class="panel-title">Action</h4>
					</div>
					<div class="panel-body">
						<div style="text-align:center">

                            <asp:Button ID="btn_updatedispatch" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Acknowledge" OnClick="btn_updatedispatch_Click" />
                            <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Cancel" OnClick="btn_Cancel_Click" />
						</div>
					</div>
				</div>
			</div>
		</div>
	 <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Dispatch" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Condition" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Step" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Remark" runat="server"></asp:TextBox>
            <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="PK_DC_ID" runat="server"></asp:TextBox>
          <asp:TextBox ID="CREATED_BY" runat="server"></asp:TextBox>
         <asp:TextBox ID="pono" runat="server"></asp:TextBox>
        </div>
	
    </form>
    <!-- ================== BEGIN BASE JS ================== -->
    <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
	<script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../../JS/SUB_CONTRACT_PO_DC_GENRATE.js"></script>
	<!-- ================== END PAGE LEVEL JS ================== -->
	<script>
	    $(document).ready(function () {
	        App.init();
	    });
	</script>
</body>
</html>
