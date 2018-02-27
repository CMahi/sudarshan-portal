<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Change_Password.aspx.cs" Inherits="Portal_Change_Password" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
	<title>Change Password</title>
	<meta content="width=device-width, initial-scale=1.0" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />
	
	<!-- ================== BEGIN BASE CSS STYLE ================== -->
    <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style.min.css" rel="stylesheet" />
        <script src="../JS/Change_Password.js"></script>
	<!-- ================== END BASE CSS STYLE ================== -->
</head>
<body>
      <form id="form1" runat="server" class="form-horizontal">
	
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
            <div class="row">
            <div class="col-md-3"></div>
			<div class="col-md-6">
				<div class="panel panel-danger">
					<div class="panel-heading">
						<h3 class="panel-title"><b>Change Password</b></h3>
					</div>
					<div class="panel-body">
						<form class="form-horizontal" data-parsley-validate="true" name="demo-form" >
							<div class="form-group">
								<label class="control-label col-sm-4" for="email">Enter Old Password</label>
								<div class="col-sm-4">
                                      <asp:TextBox ID="txt_Old" runat="server" class="form-control" placeholder="Old Password" onkeyup="checkPassword();" TextMode="Password"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom" ValidChars="@"
                                        TargetControlID="txt_Old" />
                                    
								</div>
								<div>
                                    <asp:Label ID="Oldmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>
							</div>
							<div class="form-group" id="new_pass" runat="server" style="display:none">
								<label class="control-label col-sm-4" for="message">Enter New Password</label>
								<div class="col-sm-4">
                                     <asp:TextBox ID="txt_New" runat="server" class="form-control" placeholder="New Password" onkeyup="chkNewPassword();" TextMode="Password"></asp:TextBox>
                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom" ValidChars="@"
                                        TargetControlID="txt_New" />
								</div>
                                 <div>
                                    <asp:Label ID="lblNew" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>
							</div>
                           
							<div class="form-group" id="comfirm_pass" runat="server" style="display:none">
								<label class="control-label col-sm-4" for="message">Re-Enter New Password</label>
								<div class="col-sm-4">
                                     <asp:TextBox ID="txt_Confirm" runat="server" class="form-control" placeholder="Confirm New Password" onkeyup="chkNewPassword();" TextMode="Password"></asp:TextBox>
                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom" ValidChars="@"
                                        TargetControlID="txt_Confirm" />
								</div>
								 <div>
                                    <asp:Label ID="lblConf" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>
							</div>
						
							<div class="form-group m-b-0" id="btn_pass" runat="server" style="display:none">
								<div class="col-sm-12" style="text-align:center">
                                    <asp:Button ID="btnUpdate" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Update" OnClientClick="return PasswordValidation();" OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btnClose" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Close" OnClick="btnClose_Click" />
								</div>
							</div>
                            <div style="display:none;">
                                <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txt_password" runat="server"></asp:TextBox>
                            </div>
						</form>
					</div>
				</div>
			</div>
		</div>
	
	</form>

	<!-- ================== BEGIN BASE JS ================== -->
   <script src="../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
	<script src="../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
	<script src="../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
	<!-- ================== END PAGE LEVEL JS ================== -->
    
</body>
</html>
