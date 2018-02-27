<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"   CodeFile="Vendor_Advance_Request.aspx.cs" Inherits="Vendor_Advance_Request"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <title>Vendor Adavance Details</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <!-- ================== END BASE CSS STYLE ================== -->
</head>
<body style="overflow-x: hidden">
    <form id="form1" class="form-horizontal bordered-group" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                            <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                        </div>
                        <h3 class="panel-title"><b>Vendor PO Details</b></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-2" style="margin-top: -8px;">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">List of Open PO</h4>
                    </div>
                    <div class="panel-body">
			        <div style="height:290px; overflow:auto">
                            <form class="form-horizontal bordered-group">
                                <div id="div_po" runat="server"></div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-10" style="margin-top: -8px;">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">PO Header</h4>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive" style="height:150px;"">
                            <div id="div_Header" runat="server"></div>
                        </div>
                        <div class='form-group' id='div_text'>
                            <label class="col-md-2 control-label ui-sortable">Advance Amount&nbsp<font color="red">*</font></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txt_advance_amount" class="form-control" runat="server" style="text-align:right" onkeypress="return isNumber(event)" onkeyup="return isNumber1(event)"></asp:TextBox>
                            </div>
                           
                            <label class="col-md-2 control-label ui-sortable">Remark&nbsp<font color="red">*</font></label>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_Remark" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox> 
                            </div>
                        </div>
                        <div class='form-group'></div>
                        <div class='form-group' id='div_button'>
                            <div class="text-center">
                               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                     <asp:Button ID="btn_submit" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Submit" OnClientClick="return getXML();"  />
                                     <asp:Button ID="btn_cancel" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Cancel" OnClick="imgBtnRelease_Click" />
                              </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                      </div>
                    </div>
                </div>
            </div>
       
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="div_txt" style="display: none" runat="server">
                            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Po_No" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_CCmail" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Tomail" runat="server"></asp:TextBox>
                            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
			    <asp:TextBox ID="txt_Current_Date" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Approver_Mail" runat="server"></asp:TextBox>
			    <asp:TextBox ID="txt_Vendor_Mail" runat="server"></asp:TextBox>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
          
    </form>
    
        <!-- ================== BEGIN BASE JS ================== -->
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery-cookie/jquery.cookie.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/js/apps.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../JS/Vendor_Advance_request.js"></script>
    <!-- ================== END PAGE LEVEL JS ================== -->

</body>
  

</html>

