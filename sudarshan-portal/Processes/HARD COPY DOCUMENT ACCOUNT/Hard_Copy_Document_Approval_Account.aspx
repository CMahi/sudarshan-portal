﻿<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Hard_Copy_Document_Approval_Account.aspx.cs" Inherits="Hard_Copy_Document_Approval_Account" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Hard Copy Acknowledgement</title>
    
    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <!-- ================== END BASE CSS STYLE ================== -->
</head>
<body>
     <form id="form1" class="form-horizontal bordered-group" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <div class="modal fade" id="paymentterm" >
		        <div class="modal-dialog"  style="height:auto; width:98%; margin-left:1%">
			        <div class="modal-content" style="background-color:ButtonFace">
				        <div class="modal-header">
					        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
					        <h4 class="modal-title"><font color="white"> Dispatch Request Details </font></h4>
				        </div>
                
				        <div class="modal-body" id="div_header1" runat="server" data-scrollbar="true" data-height="425px">                 
                          
				        </div>
                
				        <div class="modal-footer">
					        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal" >Close</a>
				        </div>
			        </div>
		        </div>
	        </div>
           	<div class="row">
		        <div class="col-md-12">
			        <div class="panel panel-danger">
				        <div class="panel-heading">
					        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>Dispatch Note Document Details</h3>
				        </div>
				        <div class="panel-body">
					        <div class="form-horizontal">
						        <div class="form-group">
							        <div class="col-md-1"></div>
                                    <label class="col-md-2">Vendor Name</label>
							        <div class="col-md-3">
								        <asp:Label ID="txt_Vendor_Name" runat="server"></asp:Label>
							        </div>
							        <div class="col-md-1"></div>
							        <label class="col-md-2">Vendor Code</label>
							        <div class="col-md-3">
								        <asp:Label ID="txt_Vendor_Code" runat="server"></asp:Label>
							        </div>
						        </div>
						        <div class="form-group">
							        <div class="col-md-1"></div>
							        <label class="col-md-2">PO No</label>
							        <div class="col-md-3">
                                        <asp:HyperLink ID="HyperLink1" runat="server" role="button" data-toggle="modal" Font-Size="Small" Font-Bold="False" onclick='viewData(" + (i+1) + ")'>
								         <asp:Label ID="txt_PO_NO" runat="server"></asp:Label>
                                            </asp:HyperLink>
							        </div>
							        <div class="col-md-1"></div>
							        <label class="col-md-2">Unique Id</label>
							        <div class="col-md-3">
								         <asp:Label ID="txt_Unique_NO" runat="server"></asp:Label>
							        </div>
						        </div>
						        <div class="form-group">
							        <div class="col-md-1"></div>
							        <label class="col-md-2">Dispatch Note No</label>
							        <div class="col-md-3">
                                        <asp:HyperLink href='#paymentterm' role='button' data-toggle='modal' ID="HyperLink2" runat="server" Font-Size="Small" Font-Bold="False" onclick='setSelectedNote()'>
								            <asp:Label ID="txt_header" runat="server"></asp:Label>
                                        </asp:HyperLink>
							        </div>
							        <div class="col-md-1"></div>
							        <label class="col-md-2">Invoice No</label>
							        <div class="col-md-3">
								         <asp:Label ID="txt_Invoice_No" runat="server"></asp:Label>
							        </div>
						        </div>
					        </div>
				        </div>
			        </div>
		        </div>
		        <div class="col-md-2"></div>
		        <div class="col-md-8">
			        <div class="panel panel-grey">
				        <div class="panel-heading">
					        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Hard Copy Acknowledgement</h4>
				        </div>
				        <div class="panel-body">
					       <div class="form-group">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div id="div_Doc" runat="server"></div>
                                    </div>
                                </div>
                            </div>
					        <div style="text-align:center;">
						        <asp:Button ID="btn_Save" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Hard Copy Acknowledgement" OnClick="btn_AckOnClick"/>
                                <asp:Button ID="btn_Send_Back" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Send Back" OnClick="btn_SendBack" OnClientClick="return getXML()"/>
						        <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Cancel" OnClick="imgBtnRelease_Click" />
					        </div>
				        </div>
			        </div>
		        </div>
              </div>
              <div>
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div id="div_txt" style="display: none" runat="server">
                                    <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txt_wi" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txt_Email_ID" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txt_Vendor_Mail_ID" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txt_PKID" runat="server"></asp:TextBox>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
              </div>
    </form>
    
    
    <script src="../../JS/Hard_Copy_Document.js"></script>

       <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery/jquery-migrate-1.1.0.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery-cookie/jquery.cookie.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/js/apps.min.js"></script>

    <script type="text/javascript">
        
    </script>
</body>
</html>


