<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Mobile_DataCard_Expense_Modification.aspx.cs" Inherits="Mobile_DataCard_Expense_Modification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Mobile Reimbursement</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker3.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
</head>
<body style="overflow-x: hidden">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<div id="divIns" runat="server" style="display:none">
            <div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent.gif" style="background-color:transparent;position:fixed; top:40%; left:46%;"/></div>
        </div>
        <div class="modal fade" id="div_user_data">
            <div class="modal-dialog" style="width: 90%; margin-left: 5%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Requestor's Detail</font></h4>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Request No.</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_requestNo" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Date</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_date" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Employee Code</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_EmpCode" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Employee Name</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_EmpName" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Designation</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_desgnation" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Division</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_division" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Department</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_Dept" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Grade</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_Grade" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Cost Center</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_CostCenter" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Mobile No.</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_MobileNo" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Bank Account No.</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_bankAccNo" runat="server"></asp:Label>
                                    </div>

                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">IFSC No.</label>
                                    <div class="col-md-3">
                                        <div id="Div4"><span id="span_Ifsc" runat="server">NA</span></div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Approver Name</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lbl_AppName" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Deviation Approver </label>
                                    <div class="col-md-3">
                                        <div id="Div7"><span id="span_DApprover" runat="server" style="display: none"></span><span id="span_Dapp_name" runat="server"></span></div>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="modal-footer">
                            <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color: blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                        </div>
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>MOBILE DATACARD MODIFICATION</h3>
                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">

                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Mode<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlPayMode" runat="server" class="form-control  input-sm width-xs">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <div id="div_payment">
                                    <label class="col-md-2">Payment Location<font color="#ff0000"><b>*</b></font></label>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlLocation" runat="server" class="form-control  input-sm width-xs">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Expense Head</label>
                                <div class="col-md-3">
                                    <asp:Label ID="lblExpenseHead" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Supporting Documents</label>
                                <div class="col-md-3">
                                    <a href="#div_UploadDocument" data-toggle="modal">
                                        <img id="Img1" src="../../images/attachment.png" alt="Click here to attach file." height="20" width="20" /></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12" id="div_DataCard" runat="server">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Data Card</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Card No<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <input type='text' class="form-control" id="txtCardNo" runat="server" onkeypress="return isNumberKey(event)" />
                                </div>
                                <label class="col-md-2">Service Provider<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCardProvider" runat="server" class="form-control  input-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Year<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCardYear" runat="server" class="form-control  input-sm">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-2">Month<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCardMonth" runat="server" class="form-control  input-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Bill No<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <input type='text' class="form-control" id="txt_card_billno" runat="server" />
                                </div>
                                <label class="col-md-2">Bill Date<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <div class="input-group" id="Div6">
                                        <input type='text' class="form-control datepicker-dropdown" id="txt_card_billdate" runat="server" readonly />
                                        <span class="input-group-btn">
                                            <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Bill Amount<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <input id="txt_card_billamt" type='text' class="form-control" runat="server" onkeypress="return isNumberKey(event)" onchange="valuedatacard(1);" />
                                </div>
                                <label class="col-md-2">Allowed Reimbursement </label>
                                <div class="col-md-3">
                                    <asp:Label ID="txt_Reimbursement" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Tax<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <input id="txt_datacardtax" type='text' class="form-control" runat="server" onkeypress="return isNumberKey(event)" onchange="valuedatacard(1);" />
                                </div>
                                <label class="col-md-2">Total Reimbursement Amount</label>
                                <div class="col-md-3">
                                    <asp:Label ID="lbl_totaldataamt" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">

                                <div class="col-md-1"></div>
                                <label class="col-md-2">Remark<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-8">
                                    <textarea type='text' class="form-control" rows="1" id="txt_remark" runat="server"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" id="div_deduction" runat="server">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <h4 class="panel-title">Total Amount : <span id="span_dedamount" runat="server">0</span></h4>
                        </div>
                        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Deduction Details</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div id="div_dedction" runat="server"></div>
                            <div class="table-responsive" style="width: 100%" id="div_displaydeduction" runat="server"></div>

                            <%-- <div class="form-group">
                                <label class="col-md-2">Deduction Year</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddldedYear" runat="server" class="form-control  input-sm" onchange="getDeductAmount()">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-2">Deduction Month</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlDeduMonth" runat="server" class="form-control  input-sm" onchange="getDeductAmount()">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-2">Deduction Amount</label>
                                <div class="col-md-2">
                                  <input type='text' class="form-control input-sm" id="txt_deductionamt" runat="server" onkeypress="return isNumberKey(event)" />
                                     <span id="txt_deductionamt" runat="server">0</span>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" id="div_MobileReimbursement">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <h4 class="panel-title">&nbsp;/ Total Amount : <span id="spn_Total" runat="server">0</span></h4>
                        </div>
                        <div class="panel-heading-btn">
                            <h4 class="panel-title"><a href="#div_Policy1" data-toggle="modal">View Policy</a></h4>
                        </div>
                        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Mobile Reimbursement (
                            <asp:Label ID="span_support2" runat="server"></asp:Label>)</h4>
                        <p style="color: white">Note : <i class="fa fa-fw fa-angle-double-right"></i><i class="fa fa-fw fa-angle-double-right"></i>Y (Mandatory - Supporting Documents), N (Not Mandatory - Supporting Documents)</p>

                    </div>

                    <div class="panel-body">
                        <div class="table-responsive">
                            <div id="tab_mobile" runat="server"></div>
                        </div>

                    </div>


                </div>
            </div>
            <div class="col-md-12" id="div_UserMobileReimbursement">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <h4 class="panel-title">&nbsp;/ Total Amount : <span id="span_usertotal" runat="server">0</span></h4>
                        </div>
                        <div class="panel-heading-btn">
                            <h4 class="panel-title"><a href="#div_Policy1" data-toggle="modal">View Policy</a></h4>

                        </div>
                        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Mobile Reimbursement (
                            <asp:Label ID="span_support1" runat="server"></asp:Label>)</h4>
                        <p style="color: white">Note : <i class="fa fa-fw fa-angle-double-right"></i><i class="fa fa-fw fa-angle-double-right"></i>Y (Mandatory - Supporting Documents), N (Not Mandatory - Supporting Documents)</p>

                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <div id="tab_usermobile" runat="server"></div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="col-md-12">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-thumbs-o-up"></i>Action</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-5"></div>
                        <div class="col-md-6">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btn_Save" runat="server" class="btn btn-danger btn-rounded" Text="Update" OnClientClick="return createxml();" OnClick="btnRequest_Click" />
                                    <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded" Text="Cancel" OnClick="imgBtnRelease_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-thumbs-o-up"></i>Audit Trail</h3>
                    </div>
                    <div class="panel-body">
                        <div id="div_Audit" runat="server">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="display: none;" class="modal" id="div_Policy1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Mobile DataCard Policy</h5>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div id="div_policy" runat="server"></div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-danger btn-rounded width-100" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div style="display: none;" class="modal" id="div_UploadDocument">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Document Upload</h5>
                    </div>

                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Description</label>
                                <div class="col-md-6 ">
                                    <div id="div_CustomerId">
                                        <input class="form-control input-sm" id="txt_description" name="txt_description" type="text" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Attach File</label>

                                <div class="col-md-7">
                                    <span class="btn btn-grey fileinput-button">
                                        <asp:AsyncFileUpload ID="FileUpload1" runat="server" OnClientUploadError="uploadError"
                                            OnClientUploadStarted="StartUpload" OnClientUploadComplete="UploadComplete" CompleteBackColor="Lime"
                                            ErrorBackColor="Red" OnUploadedComplete="btnUpload_Click"
                                            UploadingBackColor="#66CCFF" />
                                    </span>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <p style="color: red">
                                    <label>Upload .pdf,.jpeg,.jpg,.png formats only.</label>
                                </p>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div id="div_Doc" runat="server"></div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-danger btn-rounded width-100" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>

        <div id="div_txt" style="display: none" runat="server">
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
            <asp:TextBox ID="txt_Action" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Audit" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtDummy" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Datacode" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_xml_data_mobile" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_xml_data_datacard" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_year" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_month" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_year1" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_month1" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ddlsupport" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ddlsupport1" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ddlsuppddl" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ddlsupportuser" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_serviceprovide" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_serviceprovide1" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_approvar" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_user_year" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_user_month" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_user_serviceprovide" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_cmobiole" runat="server"></asp:TextBox>
            <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Mreimbursement" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_user_Mreimbursement" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_count" runat="server"></asp:TextBox>
            <asp:TextBox ID="supp_amt_no_db" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_amt_no_tot" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_amt_tot" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_perc_no" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_perc_no_d" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="dev_supp_amt" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="dev_supp_limit" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_pkexpense1" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ded_year" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ded_month" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ded_count" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ded_amount" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_check_Nos" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ded_total_amount" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_confirm" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ded_tamount" runat="server"></asp:TextBox>
        </div>

        <!-- ================== BEGIN BASE JS ================== -->
        <%--        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>--%>
        <script src="../../assets/plugins/jquery/jquery-2.1.4.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script type="text/javascript" src="../../JS/MobileCardExpenseModification.js"></script>
    </form>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, endDate: new Date(), todayBtn: 'linked' });
            var mobile = $("#txt_cmobiole").val();
            var expenseHead = $("#lblExpenseHead").text();
            if (expenseHead == "Mobile") {
                if (mobile != "") {
                    $("#div_MobileReimbursement").show();
                    $("#div_deduction").show();
                    $("#div_UserMobileReimbursement").hide();
                    $("#div_DataCard").hide();

                }
                else {

                    var total = 0;
                    $("#div_UserMobileReimbursement").show();
                    $("#div_MobileReimbursement").hide();
                    $("#div_deduction").hide();
                    $("#div_DataCard").hide();
                    var cnt = $("#txt_count").val();
                    for (var q = 1; q <= cnt; q++) {
                        var biiamt = parseInt($("#txt_user_total" + q).val());
                        var reimamt = parseInt($("#txt_user_tax" + q).val());
                        if (biiamt != "" && reimamt != "") {
                            var diff = biiamt + reimamt;
                            $('#lbl_amttax' + q).html(diff);
                        }
                        var amount = 0;
                        if ($("#lbl_amttax" + (q)).html() != "" && $("#lbl_amttax" + (q)).html() != undefined) {
                            amount = parseFloat($("#lbl_amttax" + (q)).html());
                            $("#lbl_amttax" + (q)).html(amount);
                        }
                        else {
                            $("#lbl_amttax" + (q)).html(0);
                        }
                        total = total + amount;
                    }

                    $("#span_usertotal").html(total);

                }
            }
            else {
                $("#div_UserMobileReimbursement").hide();
                $("#div_MobileReimbursement").hide();
                $("#div_deduction").hide();
                $("#div_DataCard").show();
                var dreamt = parseInt($("#txt_Reimbursement").text());
                var dtax = parseInt($("#txt_datacardtax").val());
                if (dreamt != "" && dtax != "") {
                    var diff = dreamt + dtax;
                    $('#lbl_totaldataamt').html(diff);
                }
            }
            var mode = $("#ddlPayMode").val();
            if (mode == 2) {
                $("#div_payment").hide();
            }
            else {
                $("#div_payment").show();
            }

        });

        //$("#txt_total").blur(function () {
        //    var biiamt = $("#txt_mobile_bill_amt").val();
        //    var reimamt = $("#txt_Mreimbursement").text();
        //    if (biiamt != "" && reimamt != "") {
        //        var diff = biiamt - reimamt;
        //        var tvalue = $('#txt_total').val();
        //        if (diff < tvalue) {
        //            alert("Enter amount less than bill amount");
        //        }
        //    }

        //});

        $("#ddlMonth").change(function () {
            var grade = $("#lbl_Grade").text();
            Mobile_DataCard_Expense.checkexpense(grade, callback_exp_Detail);
        });
        function callback_amt_Detail(response) {
            $('#txt_Mreimbursement').text(response.value);
            //if (response.value == 0) {
            //    $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            //    alert("No value is assigned for reimbursement amount for this year so you are not allow to reimburse ");
            //}
            //else {
            //    $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
            //}
        }
        function callback_exp_Detail(response) {
            if (response.value == "false") {
                var month = $("#ddlMonth").val();
                var year = $("#ddlYear").val();
                var expenseHead = $("#lblExpenseHead").text();
                var grade = $("#lbl_Grade").text();
                // Mobile_DataCard_Expense_Modification.fillReimburmentAmt(year, month, expenseHead, grade, callback_amt_Detail);
            }
            else {
                $('#txt_Mreimbursement').text("0");
            }
        }
        $('body').on('click', '.add_Mobile', function () {
            addNewRowMobile();
        });

        function addNewRowMobile() {
            var lastRow = $('#tbl_Mobile tr').length;
            var lastRow1 = $('#tbl_Mobile tr').length - 1;
            var ddlye = document.getElementById("txt_year1").value;
            var ddlmon = document.getElementById("txt_month1").value;
            var ddlserve = document.getElementById("txt_serviceprovide1").value;
            var ddlsupp = document.getElementById("txt_ddlsuppddl").value;
            for (var q = 1; q <= lastRow1; q++) {
                //var mno = document.getElementById("txt_MobileNo" + q).value;
                var provi = document.getElementById("ddlServiceProvider" + q).value;
                var year = document.getElementById("ddlYear" + q).value;
                var mnt = document.getElementById("ddlMonth" + q).value;
                var mbill = document.getElementById("txt_mobile_bill_no" + q).value;
                var mbilldate = document.getElementById("txt_mobile_bill_date" + q).value;
                var mbillamt = document.getElementById("txt_mobile_bill_amt" + q).value;
                var remamt = document.getElementById("txt_total" + q).value;
                var supp = document.getElementById("ddl_support" + q).value;
                //var supprmk = document.getElementById("txt_supremark" + q).value;

                if (provi == 0) {
                    alert('Please Select Service Provider at row :' + q + '');
                    return false;
                }
                if (year == 0) {
                    alert('Please Select Year at row :' + q + '');
                    return false;
                }
                if (mnt == 0) {
                    alert('Please Select Month at row :' + q + '');
                    return false;
                }
                if (mbill == "") {
                    alert('Please Enter Bill No at row :' + q + '');
                    return false;
                }
                if (mbilldate == 0) {
                    alert('Please Select Bill Date at row :' + q + '');
                    return false;
                }
                if (mbillamt == "") {
                    alert('Please Enter Bill Amount at row :' + q + '');
                    return false;
                }
                if (remamt == "") {
                    alert('Please Enter Reimbursement Amount at row :' + q + '');
                    return false;
                }
                var mrmk = document.getElementById("txt_Mremark" + q).value;
                if (mrmk == "") {
                    alert('Please Enter Remark at row :' + q + '');
                    return false;
                }
                if (supp == 0) {
                    alert('Please Select Supporting Document at row :' + q + '');
                    return false;
                }
                //if (supp == "Y") {
                //    if (supprmk == "") {
                //        alert('Please Enter Supporting Particulars at row :' + q + '');
                //        return false;
                //    }
                //}
            }
            //var html = "<tr><td>" + (lastRow1 + 1) + "</td>";
            var html11 = "<tr><td class='add_Mobile'  id='add_Row" + (lastRow1 + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
            var html2 = "<td><select id='ddlServiceProvider" + (lastRow1 + 1) + "'  class='form-control input-sm width-xs'>" + ddlserve + "</select></td>";
            var html3 = "<td><select id='ddlYear" + (lastRow1 + 1) + "'  class='form-control  input-sm width-100' onchange='comdedyear(" + (lastRow1 + 1) + ")'>" + ddlye + "</select></td>";
            var html4 = "<td><select id='ddlMonth" + (lastRow1 + 1) + "'  class='form-control  input-sm width-100' onchange='comdedmonth(" + (lastRow1 + 1) + ")'>" + ddlmon + "</select></td>";
            var html5 = "<td><input class='form-control input-sm width-100' type='text' id='txt_mobile_bill_no" + (lastRow1 + 1) + "'></input></td>";
            var html6 = "<td><div class='input-group'><input type='text' class='form-control input-sm width-100 datepicker-dropdown' id='txt_mobile_bill_date" + (lastRow1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html7 = "<td><input class='form-control input-sm width-100' type='text' id='txt_mobile_bill_amt" + (lastRow1 + 1) + "' onkeypress='return isNumberKey(event)'></input></td>";
            var html8 = "<td><input class='form-control input-sm width-100' type='text' id='txt_total" + (lastRow1 + 1) + "' onkeypress='return isNumberKey(event)' onchange='value1billamt(" + (lastRow1 + 1) + ");'></input></td>";
            var html9 = "<td><label id='txt_Mreimbursement" + (lastRow1 + 1) + "'>" + $("#txt_Mreimbursement").text() + "</label></td><td><select id='ddl_support" + (lastRow1 + 1) + "'  class='form-control  input-sm width-xs width-60'>" + ddlsupp + "</select></td>";
            var html10 = "<td><input type='text' class='form-control input-sm width-200' id='txt_Mremark" + (lastRow1 + 1) + "' /></td>";
            //var html11 = "<td class='add_Mobile'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
            var html12 = "<td id='delete_Row" + (lastRow1 + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_Row(" + (lastRow1 + 1) + ")'></i></td></tr>";
            var htmlcontent = $(html11 + "" + html2 + "" + html3 + "" + html4 + "" + html5 + "" + html6 + "" + html7 + "" + html8 + "" + html9 + "" + html10 + "" + html12);

            $('#tbl_Mobile').append(htmlcontent);
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, endDate: new Date(), todayBtn: 'linked' });
        }

        function delete_Row(RowIndex) {
            try {
                var tbl = document.getElementById("tbl_Mobile");
                var lastRow = tbl.rows.length;
                if (lastRow <= 2) {
                    alert("Validation Error:You have to Enter atleast one record..!");
                    return false;
                }
                var IsDelete = confirm("Are you sure to delete this ?");
                if (IsDelete) {
                    for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {

                        document.getElementById("ddlServiceProvider" + (parseInt(contolIndex) + 1)).id = "ddlServiceProvider" + contolIndex;
                        document.getElementById("ddlYear" + (parseInt(contolIndex) + 1)).onchange = new Function("comdedyear(" + contolIndex + ")");
                        document.getElementById("ddlYear" + (parseInt(contolIndex) + 1)).id = "ddlYear" + contolIndex;
                        document.getElementById("ddlMonth" + (parseInt(contolIndex) + 1)).onchange = new Function("comdedmonth(" + contolIndex + ")");
                        document.getElementById("ddlMonth" + (parseInt(contolIndex) + 1)).id = "ddlMonth" + contolIndex;
                        document.getElementById("txt_mobile_bill_no" + (parseInt(contolIndex) + 1)).id = "txt_mobile_bill_no" + contolIndex;
                        document.getElementById("txt_mobile_bill_date" + (parseInt(contolIndex) + 1)).id = "txt_mobile_bill_date" + contolIndex;
                        //  document.getElementById("txt_mobile_bill_amt" + (parseInt(contolIndex) + 1)).onchange = new Function("calculate_Total()");
                        document.getElementById("txt_mobile_bill_amt" + (contolIndex + 1)).id = "txt_mobile_bill_amt" + contolIndex;
                        document.getElementById("txt_total" + (parseInt(contolIndex) + 1)).onchange = new Function("value1billamt(" + contolIndex + ")");
                        document.getElementById("txt_total" + (parseInt(contolIndex) + 1)).id = "txt_total" + contolIndex;
                        document.getElementById("txt_Mreimbursement" + (parseInt(contolIndex) + 1)).id = "txt_Mreimbursement" + contolIndex;
                        document.getElementById("ddl_support" + (parseInt(contolIndex) + 1)).id = "ddl_support" + contolIndex;
                        //  document.getElementById("txt_supremark" + (parseInt(contolIndex) + 1)).id = "txt_supremark" + contolIndex;
                        document.getElementById("txt_Mremark" + (parseInt(contolIndex) + 1)).id = "txt_Mremark" + contolIndex;
                        document.getElementById("add_Row" + (contolIndex + 1)).id = "add_Row" + contolIndex;
                        document.getElementById("delete_Row" + (contolIndex + 1)).onclick = new Function("return delete_Row(" + contolIndex + ")");
                        document.getElementById("delete_Row" + (contolIndex + 1)).id = "delete_Row" + contolIndex;
                    }
                    ///////////////////////////////////////////////////////////////////////
                    tbl.deleteRow(RowIndex);
                    var total = 0;
                    var lastRow = $('#tbl_Mobile tr').length;
                    for (var q = 0; q < lastRow; q++) {
                        var amount = 0;
                        if ($("#txt_total" + (q + 1)).val() != "" && $("#txt_total" + (q + 1)).val() != undefined) {
                            amount = parseFloat($("#txt_total" + (q + 1)).val());
                            $("#txt_total" + (q + 1)).val(amount);
                        }
                        else {
                            $("#txt_total" + (q + 1)).val(0);
                        }
                        total = total + amount;
                    }
                    $("#spn_Total").html(total);

                    alert("Record Deleted Successfully..!");
                }
            }
            catch (Exc) { }
        }

        $('body').on('click', '.add_UserMobile', function () {
            addNewRowMobileUser();
        });

        /***************** Users Own Mobile***************************/
        function addNewRowMobileUser() {
            var lastR = $('#tbl_UserMobile tr').length;
            var lastR1 = $('#tbl_UserMobile tr').length - 1;
            var ddlyeu = document.getElementById("txt_user_year").value;
            var ddlmonu = document.getElementById("txt_user_month").value;
            var ddlserveu = document.getElementById("txt_user_serviceprovide").value;
            var ddlsupp = document.getElementById("txt_ddlsuppddl").value;
            for (var q = 1; q <= lastR1; q++) {
                var mno = document.getElementById("txt_User_MobileNo" + q).value;
                var provi = document.getElementById("ddlServiceProviderUser" + q).value;
                var mbill = document.getElementById("txt_user_mobile_bill_no" + q).value;
                var mbilldate = document.getElementById("txt_mobile_user_bill_date" + q).value;
                var mbillamt = document.getElementById("txt_mobile_user_bill_amt" + q).value;
                var remamt = document.getElementById("txt_user_total" + q).value;
                var tax = document.getElementById("txt_user_tax" + q).value;
                var mrmk = document.getElementById("txt_user_Mremark" + q).value;
                var supp = document.getElementById("ddlusersupp" + q).value;
                //var supprmk = document.getElementById("txtusersupport" + q).value;
                if (mno == "") {
                    alert('Please Enter Mobile No at row :' + q + '');
                    return false;
                }
                if (mno.length < 10) {
                    alert('Mobile number must be 10 digits at row :' + q + '');
                    return false;
                }
                if (provi == 0) {
                    alert('Please Select Service Provider at row :' + q + '');
                    return false;
                }
                if (mbill == "") {
                    alert('Please Enter Bill No at row :' + q + '');
                    return false;
                }
                if (mbilldate == 0) {
                    alert('Please Select Bill Date at row :' + q + '');
                    return false;
                }
                if (mbillamt == "") {
                    alert('Please Enter Bill Amount at row :' + q + '');
                    return false;
                }
                if (remamt == "") {
                    alert('Please Enter Reimbursement Amount at row :' + q + '');
                    return false;
                }
                if (tax == "") {
                    alert('Please Enter Tax at row :' + q + '');
                    return false;
                }
                if (mrmk == "") {
                    alert('Please Enter Remark at row :' + q + '');
                    return false;
                }
                if (supp == 0) {
                    alert('Please Select Supporting Document at row :' + q + '');
                    return false;
                }
                //if (supp == "Y") {
                //    if (supprmk == "") {
                //        alert('Please Enter Supporting Particulars at row :' + q + '');
                //        return false;
                //    }
                //}

            }
            var html13 = "<tr><td class='add_UserMobile'  id='add_user_Row" + (lastR1 + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
            //  var html = "<td>" + (lastR1 + 1) + "</td>";
            var html1 = "<td><input class='form-control input-sm width-xs' type='text' id='txt_User_MobileNo" + (lastR1 + 1) + "' onkeypress='return isNumberKey(event)'></input></td>";
            var html2 = "<td><select id='ddlServiceProviderUser" + (lastR1 + 1) + "'  class='form-control input-sm width-100'>" + ddlserveu + "</select></td>";
            var html5 = "<td><input class='form-control input-sm width-100' type='text' id='txt_user_mobile_bill_no" + (lastR1 + 1) + "'></input></td>";
            var html6 = "<td><div class='input-group'><input type='text' class='form-control input-sm width-100 datepicker-dropdown' id='txt_mobile_user_bill_date" + (lastR1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html7 = "<td><input class='form-control input-sm width-100' type='text' id='txt_mobile_user_bill_amt" + (lastR1 + 1) + "' onkeypress='return isNumberKey(event)'></input></td>";
            var html8 = "<td><input class='form-control input-sm width-100' type='text' id='txt_user_total" + (lastR1 + 1) + "' onkeypress='return isNumberKey(event)' onchange='valuebillamt(" + (lastR1 + 1) + ");'></input></td>";
            var html10 = "<td><label id='txt_user_Mreimbursement" + (lastR1 + 1) + "'>" + $("#txt_user_Mreimbursement").text() + "</label></td><td><input class='form-control input-sm width-100' type='text' id='txt_user_tax" + (lastR1 + 1) + "'  onkeypress='return isNumberKey(event)' onchange='valuechan(" + (lastR1 + 1) + ");'></input></td>";
            var html11 = "<td><label id='lbl_amttax" + (lastR1 + 1) + "' ></label></td><td><select id='ddlusersupp" + (lastR1 + 1) + "'  class='form-control  input-sm width-xs width-60'>" + ddlsupp + "</select></td>";
            var html12 = "<td><input type='text' class='form-control input-sm width-200' id='txt_user_Mremark" + (lastR1 + 1) + "' /></td>";
            var html14 = "<td id='delete_user_Row" + (lastR1 + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_user_Row(" + (lastR1 + 1) + ")'></i></td></tr>";
            var htmlcontent = $(html13 + "" + html1 + "" + html2 + "" + html5 + "" + html6 + "" + html7 + "" + html8 + "" + html10 + "" + html11 + "" + html12 + "" + html14);

            $('#tbl_UserMobile').append(htmlcontent);
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, endDate: new Date(), todayBtn: 'linked' });

        }
        function delete_user_Row(RowIndex) {
            try {
                var tbl = document.getElementById("tbl_UserMobile");
                var lastRow = tbl.rows.length;
                if (lastRow <= 2) {
                    alert("Validation Error:You have to Enter atleast one record..!");
                    return false;
                }
                var IsDelete = confirm("Are you sure to delete this ?");
                if (IsDelete) {
                    for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
                        document.getElementById("txt_User_MobileNo" + (parseInt(contolIndex) + 1)).id = "txt_User_MobileNo" + contolIndex;
                        document.getElementById("ddlServiceProviderUser" + (parseInt(contolIndex) + 1)).id = "ddlServiceProviderUser" + contolIndex;
                        document.getElementById("txt_user_mobile_bill_no" + (parseInt(contolIndex) + 1)).id = "txt_user_mobile_bill_no" + contolIndex;
                        document.getElementById("txt_mobile_user_bill_date" + (parseInt(contolIndex) + 1)).id = "txt_mobile_user_bill_date" + contolIndex;
                        //document.getElementById("txt_mobile_user_bill_amt" + (parseInt(contolIndex) + 1)).onchange = new Function("calculate_UserTotal()");
                        document.getElementById("txt_mobile_user_bill_amt" + (contolIndex + 1)).id = "txt_mobile_user_bill_amt" + contolIndex;
                        document.getElementById("txt_user_total" + (parseInt(contolIndex) + 1)).onchange = new Function("valuebillamt(" + contolIndex + ")");
                        document.getElementById("txt_user_total" + (parseInt(contolIndex) + 1)).id = "txt_user_total" + contolIndex;
                        document.getElementById("txt_user_Mreimbursement" + (parseInt(contolIndex) + 1)).id = "txt_user_Mreimbursement" + contolIndex;
                        document.getElementById("txt_user_tax" + (parseInt(contolIndex) + 1)).onchange = new Function("valuechan(" + contolIndex + ")");
                        document.getElementById("txt_user_tax" + (parseInt(contolIndex) + 1)).id = "txt_user_tax" + contolIndex;
                        document.getElementById("lbl_amttax" + (parseInt(contolIndex) + 1)).id = "lbl_amttax" + contolIndex;
                        document.getElementById("ddlusersupp" + (parseInt(contolIndex) + 1)).id = "ddlusersupp" + contolIndex;
                        //document.getElementById("txtusersupport" + (parseInt(contolIndex) + 1)).id = "txtusersupport" + contolIndex;
                        document.getElementById("txt_user_Mremark" + (parseInt(contolIndex) + 1)).id = "txt_user_Mremark" + contolIndex;
                        document.getElementById("add_user_Row" + (contolIndex + 1)).id = "add_user_Row" + contolIndex;
                        document.getElementById("delete_user_Row" + (contolIndex + 1)).onclick = new Function("return delete_user_Row(" + contolIndex + ")");
                        document.getElementById("delete_user_Row" + (contolIndex + 1)).id = "delete_user_Row" + contolIndex;
                    }
                    ///////////////////////////////////////////////////////////////////////
                    tbl.deleteRow(RowIndex);
                    var total = 0;
                    var lastRow = $('#tbl_UserMobile tr').length;
                    for (var q = 0; q < lastRow - 1; q++) {
                        var amount = 0;
                        if ($("#lbl_amttax" + (q + 1)).html() != "" && $("#lbl_amttax" + (q + 1)).html() != undefined) {
                            amount = parseFloat($("#lbl_amttax" + (q + 1)).html());
                            $("#lbl_amttax" + (q + 1)).html(amount);
                        }
                        else {
                            $("#lbl_amttax" + (q + 1)).html(0);
                        }
                        total = total + amount;
                    }
                    $("#span_usertotal").html(total);

                    alert("Record Deleted Successfully..!");
                }
            }
            catch (Exc) { }
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function valuechan(index) {
            var biiamt = parseInt($("#txt_user_total" + index).val());
            var reimamt = parseInt($("#txt_user_tax" + index).val());
            if (biiamt != "" && reimamt != "") {
                var diff = biiamt + reimamt;
                $('#lbl_amttax' + (index)).html(diff);
            }
            var total = 0;
            var lastRow = $('#tbl_UserMobile tr').length;
            for (var q = 0; q < lastRow - 1; q++) {
                var amount = 0;
                if ($("#lbl_amttax" + (q + 1)).html() != "" && $("#lbl_amttax" + (q + 1)).html() != undefined) {
                    amount = parseFloat($("#lbl_amttax" + (q + 1)).html());
                    $("#lbl_amttax" + (q + 1)).html(amount);
                }
                else {
                    $("#lbl_amttax" + (q + 1)).html(0);
                }
                total = total + amount;
            }
            $("#span_usertotal").html(total);
        }
        function valuebillamt(index) {
            var mbillamt = parseInt($("#txt_mobile_user_bill_amt" + index).val());
            var reimamt = parseInt($("#txt_user_total" + index).val());
            var tax = parseInt($("#txt_user_tax" + index).val());
            if (reimamt != "" && tax != "") {
                var diff = reimamt + tax;
                $('#lbl_amttax' + (index)).html(diff);
            }
            if (parseInt(mbillamt) < parseInt(reimamt)) {
                alert('Bill Amount should be greater than Reimbursement Amount at row :' + index + '');
                return false;
            }
            var total = 0;
            var lastRow = $('#tbl_UserMobile tr').length;
            for (var q = 0; q < lastRow - 1; q++) {
                var amount = 0;
                if ($("#lbl_amttax" + (q + 1)).html() != "" && $("#lbl_amttax" + (q + 1)).html() != undefined) {
                    amount = parseFloat($("#lbl_amttax" + (q + 1)).html());
                    $("#lbl_amttax" + (q + 1)).html(amount);
                }
                else {
                    $("#lbl_amttax" + (q + 1)).html(0);
                }
                total = total + amount;
            }
            $("#span_usertotal").html(total);
        }

        function valuedatacard(index) {
            var cardbill = parseInt($("#txt_card_billamt").val());
            var dreamt = parseInt($("#txt_Reimbursement").text());
            var dtax = parseInt($("#txt_datacardtax").val());
            if (dreamt != "" && dtax != "" && cardbill != "") {
                if (cardbill >= dreamt) {
                    var diff = dreamt + dtax;
                    $('#lbl_totaldataamt').html(diff);
                }
                else {
                    var diff = cardbill + dtax;
                    $('#lbl_totaldataamt').html(diff);
                }
            }
        }
        function comdedyear(index) {
            var dyear = parseInt($("#ddldedYear").val());
            var year = parseInt($("#ddlYear" + index).val());
            if (dyear < year) {
                alert("Select previous year of deduction year or same year");
            }
        }
        function comdedmonth(index) {
            var dyear = parseInt($("#ddldedYear").val());
            var year = parseInt($("#ddlYear" + index).val());
            var dmonth = parseInt($("#ddlDeduMonth").val());
            var month = parseInt($("#ddlMonth1").val());
            var month1 = $("#ddlMonth2").val();

            if (dyear <= year) {
                if (dmonth < month) {
                    alert("Select previous month  of deduction month or same month");
                }
            }
        }
        //function billamtdcard(index) {
        //    var mbillamt = parseInt($("#txt_card_billamt").val());
        //    var reimamt = parseInt($("#txt_Reimbursement").text());
        //    if (parseInt(mbillamt) < parseInt(reimamt)) {
        //        alert('Bill Amount should be grater than Reimbursement Amount');
        //        return false;
        //    }
        //}
        $("#ddlPayMode").change(function () {
            var mode = $("#ddlPayMode").val();
            if (mode == 2) {
                $("#div_payment").hide();
            }
            else {
                $("#div_payment").show();
            }

        });


    </script>
</body>
</html>

