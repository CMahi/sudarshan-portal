<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="WorkItem.aspx.cs" Inherits="WorkItem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8" />
	<title>SUDARSHAN</title>
	<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />


    <link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/style.min.css" rel="stylesheet" />
</head>
<body style="overflow:hidden;">
    <form id="f1" runat="server">
	
	<div id="page-container" class="page-container page-header-fixed page-sidebar-fixed page-with-two-sidebar page-with-footer">
        
		<div id="header" class="header navbar navbar-default navbar-fixed-top" style="height: 60px; background: #ffffff;">
        	<div class="container-fluid">
				<div class="navbar-header">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/Home.png" height="30" style="margin-top:12px;" OnClick="btngoto_home"/>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Img/133649.png" height="25" style="margin-top:15px;margin-left:10px; "  OnClick="btngoto_home"/>
                </div>
				<ul class="nav navbar-nav navbar-right">
					<li class="dropdown navbar-user">
						<a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown">
							<span class="hidden-xs" id="sp_LoginUser" runat="server"></span> <b class="caret"></b>
                		</a>
						<ul class="dropdown-menu pull-right">
							<li>
                                <asp:LinkButton ID="LinkButton1" runat="server" onclick="btnedit_profile">Edit Profile</asp:LinkButton>
							</li>
                            <li>
                                <asp:LinkButton ID="LinkButton2" runat="server" onclick="btnchange">Change Password</asp:LinkButton>
							</li>
							<li class="divider"></li>
                            <li>
                                 <asp:LinkButton ID="LinkButton3" runat="server" onclick="btnLogout">Log Out</asp:LinkButton>
                           </li>
						</ul>
					</li>
				</ul>
			</div>
             <%--<div style="background-image: url('Img/bg_strip.png'); height: 4px;"></div>--%>
		</div>
         
        
            <div style="height:100%; width:100%;">
                   <iframe id="frmset_WorkArea" name="frmset_WorkArea"  style="width:100%;" runat="server" frameborder="0"></iframe>
            </div>
     
	
	</div>
	<!-- ================== BEGIN BASE JS ================== -->
        <script src="assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>

	<!-- ================== END PAGE LEVEL JS ================== -->
	
<script language="JavaScript">
    $(document).ready(function () {
        var height = $(window).height();
        $('iframe').css('height', height * 0.9 | 0);
    });
</script>

      
        </form>
</body>
</html>
