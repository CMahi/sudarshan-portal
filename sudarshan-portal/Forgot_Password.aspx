<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forgot_Password.aspx.cs" Inherits="Forgot_Password" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   	<meta charset="utf-8" />
	<title>Forgot_Password Master</title>
	<link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
	<link href="assets/css/style.min.css" rel="stylesheet" />

    <script src="assets/plugins/jquery/jquery-2.1.1.min.js"></script>
	<script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="JS/Forgot_Password.js"></script>
    <style type="text/css">
.sample
{
background-color:#DC5807;
border:1px solid black;
border-collapse:collapse;
color:White;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
           <div class="row" style="margin-top:150px" id="div_AddCountryDetails">
            <div class="col-lg-3"></div>
			<div class="col-lg-6">
				<div class="panel panel-grey">
					<div class="panel-heading">
                       <h4 class="panel-title"><b>Forgot Password</b></h4>
					</div>
					<div class="panel-body" >
                        <div class="col-md-12">
						<div class="form-horizontal bordered-group">
                            <div class="form-group" style="text-align:center;" >
								<label class="col-md-3 control-label"><b>User Name&nbsp<font color="red">*</font></b></label>
								<div class="col-md-5 ui-sortable">
                                    <asp:TextBox ID="txt_UserName" runat="server" CssClass="form-control"></asp:TextBox>
								</div>	
                            </div>
							<div class="form-group" style="text-align:center;">
								<label class="col-md-3 control-label"><b>Email ID&nbsp<font color="red">*</font></b></label>
								<div class="col-md-5 ui-sortable">
                                    <asp:TextBox ID="txt_Email" runat="server" CssClass="form-control"></asp:TextBox>
								</div>
							</div>
							</div>
                        </div>
					</div>
                    <div class="modal-footer">
				    	<div class="col-md-6"  >
                            <asp:Button ID="btn_Send" runat="server" Text="Send" class="btn btn-grey btn-rounded m-b-5" OnClientClick="return getvalidate()" OnClick="btn_SendOnclick"/>
			    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" class="btn btn-danger btn-rounded m-b-5" OnClick="btn_CancelOnclick"/>
                        </div>
                   	</div>
                     <div id="div_txt" style="display: none" runat="server">
                        <asp:TextBox ID="txt_Email_ID" runat="server"></asp:TextBox>
                     </div>
			   </div>
			</div>
		    </div>
          
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    </form>
</body>
</html>
