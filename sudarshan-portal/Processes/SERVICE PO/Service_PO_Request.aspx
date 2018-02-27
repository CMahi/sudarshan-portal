<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Service_PO_Request.aspx.cs" Inherits="Service_PO_Request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <a href="../../App_WebReferences/" class="hidden">../../App_WebReferences/</a>
    <meta charset="utf-8" />
    <title>Service PO Details</title>
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
        <div id="divIns" runat="server" style="display: none">
            <div style="background-color: #E6E6E6; position: absolute; top: 0; left: 0; width: 100%; height: 300%; z-index: 1001; -moz-opacity: 0.8; opacity: .80; filter: alpha(opacity=80);">
                <img src="../../images/loading_transparent.gif" style="background-color: Aqua; position: fixed; top: 40%; left: 46%;" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
                            <a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
                        </div>
                        <h3 class="panel-title"><b>Service PO Details</b></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-2" style="margin-top: -8px;">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">List of Open PO</h4>
                    </div>
                    <div class="panel-body">
                        <div data-scrollbar="true" data-height="290px">
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
                        <div class="table-responsive">
                            <div id="div_Header" runat="server"></div>
                        </div>
                        <div class='form-group'>
                            <label class="col-md-2 control-label ui-sortable">Invoice No.&nbsp<font color="red">*</font></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txt_invoice_no" class="form-control" runat="server" onchange="check_po()"></asp:TextBox>
                            </div>

                            <label class="col-md-2 control-label ui-sortable">Invoice Date&nbsp<font color="red">*</font></label>
                            <div class="col-md-2">
                                <div class="input-group" id="Div11">
                                    <input type='text' class="form-control datepicker-dropdown" id="txt_invoice_Date" runat="server" readonly="" />
                                    <span class="input-group-btn">
                                        <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                    </span>
                                </div>
                            </div>
                            <label class="col-md-2 control-label ui-sortable">Invoice Amount&nbsp<font color="red">*</font></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txt_invoice_amount" class="form-control" Style="text-align: right" onkeypress="return isNumber(event)" onchange="return isInvoice_Amt()" runat="server"></asp:TextBox>
                            </div>



                        </div>
                        <div class='form-group'>

                            <label class="col-md-2 control-label ui-sortable">Delivery Note&nbsp<font color="red">*</font></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txt_delivery_note" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <label class="col-md-2 control-label ui-sortable">Upload Document<font color="red">*</font></label>
                            <div class="col-md-2">
                                <a href="#div_UploadDocument" data-toggle="modal">
                                    <img id="A_FileUpload1" src="../../images/attachment.png" alt="Click here to attach file." height="20" width="20" />
                                </a>
                            </div>
                            <label class="col-md-2 control-label ui-sortable">Service Location</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txt_Location" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class='form-group'>
                            <label class="col-md-2 control-label ui-sortable">Service From Date</label>
                            <div class="col-md-2">
                                <div class="input-group" id="Div12">
                                    <input type='text' class="form-control datepicker-dropdown" id="txt_From" runat="server" readonly="" />
                                    <span class="input-group-btn">
                                        <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                    </span>
                                </div>
                            </div>
                            <label class="col-md-2 control-label ui-sortable">Service To Date</label>
                            <div class="col-md-2">
                                <div class="input-group" id="Div13">
                                    <input type='text' class="form-control datepicker-dropdown" id="txt_To" runat="server" readonly="" />
                                    <span class="input-group-btn">
                                        <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                    </span>
                                </div>
                            </div>
                            <label class="col-md-1 control-label ui-sortable">Remark</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt_Remark" class="form-control" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <div class='form-group'>

                            <div>

                                <div class="text-center">
                                    <asp:UpdatePanel ID="update" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_updatedispatch" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Update Service Details" OnClientClick="return getDetail()" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="margin-top: -8px;" id="div_main">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">PO Details</h4>
                    </div>

                    <div class="panel-body">
                        <div class="table-responsive">
                            <div id="div_detail" runat="server"></div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6" style="margin-top: -8px;" id="div_DocumentAttach">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">Document Upload</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                        </div>
                        <div class="form-group" data-scrollbar="true" data-height="150px">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <%--<div id="div_Doc" runat="server"></div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--<div class="col-md-6" style="margin-top: -8px;" id="Mandatory_doc">
                <div class="panel panel-grey">
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-md-12" style="text-align: left">
                                    <h4 class="panel-title"><font color="red">Note :- Following Documents are Mandatory and upload .pdf,.jpeg,.jpg,.png formats only.</font></h4>
                                </div>
                            </div>
                            <div class="col-md-12" style="text-align: left">
                                <div class="form-group">
                                    <h2 class="panel-title">Delivery Challan,&nbsp;Tax Invoice,&nbsp;LR</h2>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="col-md-12" style="margin-top: -8px;" id="div_Action">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">Action</h4>
                    </div>
                    <div class="panel-body">
                        <div style="text-align: center">
                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                            <asp:Button ID="btn_Submit" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Submit" OnClientClick="return getXML()" OnClick="btn_SubmitOnClick" />
                            <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Cancel" OnClick="imgBtnRelease_Click" />
                            <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                    <%--</div>--%>
                </div>
            </div>
            <%--</div>--%>

            <div class="modal fade" id="div_UploadDocument">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title">Upload Documents: <span id="div_Doc"></span></font></h4>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddl_Document" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-7">

                                    <span class="btn btn-grey fileinput-button">
                                        <asp:AsyncFileUpload ID="FileUpload1" runat="server" OnClientUploadError="uploadError" Width="250px"
                                            OnClientUploadStarted="StartUpload" OnClientUploadComplete="UploadComplete" CompleteBackColor="Lime"
                                            ErrorBackColor="Red" OnUploadedComplete="btnUpload_Click"
                                            UploadingBackColor="#66CCFF" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div id="div_Document" runat="server"></div>
                        </div>
			  
                        <div class="modal-footer">
			<div class="form-group">
			       <div class="col-md-12" style="text-align:left">
			          <h4 class="panel-title"><font color="red">Note :- Documents upload .pdf,.jpeg,.jpg,.png formats only.</font></h4>
			    	</div>
			    </div>
                            <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="div_txt" style="display: none" runat="server">
                            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Po_No" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Header_PO" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Material_No" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtCreatedDate" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_XML_DTL" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Vendor" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_PO" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Creation_Date" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_ProcessID_Early" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_InstanceID_Early" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtApproverEmail" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_flag" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Performer" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Audit" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Tomail" runat="server"></asp:TextBox>
                            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Action" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Unique_ID" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_RFC_Xml" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Account_Approver" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Store_Email" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Plat_Email" runat="server"></asp:TextBox>
                            <asp:TextBox ID="Line_Item_Amount" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Vendor_name_mail" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Hard_Process_ID_Account" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Hard_Process_ID_Store" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Hard_Instance_ID_Account" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Hard_Instance_ID_Store" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_po_header_xml" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_po_dtl_xml" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_po_serv_dtl_xml" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_hdncount" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_serv_po_vendor_code" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_serv_po_po_no" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_serv_po_date" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_serv_po_created_by" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_serv_po_type" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_serv_po_value" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_serv_po_gv" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_serv_po_payterms" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_serv_po_remark" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txt_serv_po_item_total_value" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
    </form>

    <!-- ================== BEGIN BASE JS ================== -->
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery/jquery-migrate-1.1.0.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery-cookie/jquery.cookie.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../assets/js/apps.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../JS/Service_PO_Request.js"></script>
    <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
    <script src="../../assets/js/Vaildation.js"></script>
    <!-- ================== END PAGE LEVEL JS ================== -->

</body>
</html>

