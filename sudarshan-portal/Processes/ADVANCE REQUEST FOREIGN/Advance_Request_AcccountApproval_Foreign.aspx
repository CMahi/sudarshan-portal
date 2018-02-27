<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Advance_Request_AcccountApproval_Foreign.aspx.cs" Inherits="Advance_Request_AcccountApproval_Foreign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Advance Request Approval</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker3.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <%--<link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />--%>
</head>
<body style="overflow-x: hidden">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

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
                                    <label class="col-md-2">Request No</label>
                                    <div class="col-md-3">
                                        <div id="Div12"><span id="spn_req_no" runat="server"></span></div>
                                    </div>
                                    <label class="col-md-2">Date</label>
                                    <div class="col-md-3">
                                        <div id="Div13"><span id="spn_date" runat="server"></span></div>
                                    </div>

                                    <div class="col-md-1"></div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Employee No</label>
                                    <div class="col-md-3">
                                        <div id="Div3"><span id="empno" runat="server"></span></div>
                                    </div>
                                    <label class="col-md-2">Employee Name</label>
                                    <div class="col-md-3">
                                        <div id="EmployeeName"><span id="span_ename" runat="server"></span></div>
                                    </div>

                                    <div class="col-md-1"></div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Designation</label>
                                    <div class="col-md-3">
                                        <div id="Div2"><span id="span_designation" runat="server"></span></div>
                                    </div>
                                    <label class="col-md-2">Division</label>
                                    <div class="col-md-3">
                                        <div id="Div4"><span id="span_division" runat="server"></span></div>
                                    </div>
                                    <div class="col-md-1"></div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Department</label>
                                    <div class="col-md-3">
                                        <div id="EmployeeDepartment"><span id="span_dept" runat="server"></span></div>
                                    </div>
                                    <label class="col-md-2">Grade</label>
                                    <div class="col-md-3">
                                        <div id="grade"><span id="span_grade" runat="server"></span></div>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Cost Center</label>
                                    <div class="col-md-3">
                                        <div id="Div5"><span id="span_cc" runat="server"></span></div>
                                    </div>
                                    <label class="col-md-2">Mobile No.</label>
                                    <div class="col-md-3">
                                        <div id="EmployeePhoneNo"><span id="span_mobile" runat="server"></span></div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Bank Account No.</label>
                                    <div class="col-md-3">
                                        <div id="BankNo"><span id="span_bank_no" runat="server"></span></div>
                                    </div>
                                    <label class="col-md-2">IFSC No.</label>
                                    <div class="col-md-3">
                                        <div id="Div9"><span id="span_Ifsc" runat="server">NA</span></div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Approver </label>
                                    <div class="col-md-3">
                                        <div id="Div6"><span id="span_Approver" runat="server" style="display: none"></span><span id="span_app_name" runat="server"></span></div>
                                    </div>
                                    <label class="col-md-2">Deviation Approver </label>
                                    <div class="col-md-3">
                                        <div id="Div10"><span id="span_DApprover" runat="server" style="display: none"></span><span id="span_Dapp_name" runat="server"></span></div>
                                    </div>
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
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div class="btn-group pull-right">
                            <div class="panel-heading-btn">
                                <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color: blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                            </div>
                        </div>
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>FOREIGN ADVANCE REQUEST PAYMENT APPROVAL</h3>

                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">
                            <div class="form-group">
                               
                                <div class="col-md-1"></div>
                                 <label class="col-md-2">Supporting Documents</label>
                                <div class="col-md-3">
                                    <a href="#div_UploadDocument" data-toggle="modal">
                                        <img id="A_FileUpload1" src="../../images/attachment.png" alt="Click here to attach file." height="20" width="20" /></a>

                                </div>
                              
                            </div>
                 
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <div class="panel-heading-btn" id="divpol" style="display: block">
                            <h4 class="panel-title"><a href="#div_Policy" data-toggle="modal">View Policy</a></h4>
                        </div>
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>Request Details</h3>

                    </div>
                    <div class="panel-body" id="div11">
                        <div class="form-horizontal">

                            <div class="table-responsive" style="width: 100%" id="div_LocalData" runat="server">
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12" id="div_vendor" style="display: none" runat="server">
                <div class="panel panel-grey">
                    <div class="panel-heading">

                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>Vendor Details</h3>

                    </div>
                    <div class="panel-body" id="div14">
                        <div class="form-horizontal">

                            <div class="table-responsive" style="width: 100%" id="div_vendordata" runat="server">
                            </div>

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
                        <div class="form-horizontal">

                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-1">Action</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlAction" runat="server" class="form-control input-sm" onchange="hide_show_remark()"></asp:DropDownList>
                                </div>
                                <label class="col-md-1" style="display: none" id="lbl_rmk">Remark</label>
                                <div class="col-md-6" style="display: none" id="div_remark">
                                    <textarea type='text' class="form-control" cols="10" rows="2" id="txtRemark" runat="server"></textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" id="tab_btn3" runat="server" style="text-align: center;">
                                    <%--<asp:Button ID="btnRequest" runat="server" class="btn btn-danger  btn-rounded" Text="Submit"  OnClientClick="return prepareData();" OnClick="btnRequest_Click"  />--%>
                                    <a id="btnProceed" runat="server" href="#div_Popup" data-toggle="modal" class="btn btn-danger  btn-rounded" onclick="PopupData();" >Proceed</a>
                                    <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded" Text="Cancel" OnClick="imgBtnRelease_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--======================================================================================================================================--%>
        
        <div class="modal fade" id="div_Popup">
<div id="divIns" runat="server" style="display:none">
            <div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent.gif" style="background-color:transparent;position:fixed; top:40%; left:46%;"/></div>
        </div>
            <div class="modal-dialog" style="width: 90%; margin-left: 5%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Summary</font></h4>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Currency</th>
                                                <th>Advance Taken</th>
                                                <th>Exchange Rate</th>
                                                <th>Advance in INR</th>
                                                <th>Service Charges</th>
                                                <th>Total Amount</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td><span id="tbl_Currency" runat="server">0</span></td>
                                                <td><span id="tbl_adv_curr" runat="server">0</span></td>
                                                <td><span id="tbl_Exc" runat="server">0</span></td>
                                                <td><span id="tbl_adv_inr" runat="server">0</span></td>
                                                <td><span id="tbl_Ser" runat="server">0</span></td>
                                                <td><span id="tbl_Total" runat="server">0</span></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                              
                            </div>
                            
                        </div>
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btnRequest" runat="server" class="btn btn-danger btn-rounded" Text="Submit"  OnClientClick="return prepareData();" OnClick="btnRequest_Click"  />
                        <a href="javascript:;" class="btn btn-danger btn-rounded" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        
        <%--======================================================================================================================================--%>
        <div style="display: none;" class="modal" id="div_UploadDocument">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Uploaded Document</h5>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div style="width: 100%" id="divalldata" runat="server"></div>
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

        <div class="col-md-12" style="margin-top: 10px;" id="div1">
            <div class="panel panel-grey">
                <div class="panel-heading">
                    <h4 class="panel-title">Audit Trail</h4>
                </div>
                <div class="panel-body">
                    <div id="div_Audit" runat="server">
                    </div>
                </div>
            </div>
        </div>

        <div style="display: none;" class="modal" id="div_Policy">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Advance Request Policy</h5>
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

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
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
                    <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Remark" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Approver" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_designation" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="doa" runat="server"></asp:TextBox>
                    <asp:TextBox ID="doa_user" runat="server"></asp:TextBox>
                    <asp:TextBox ID="doa_email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Mailid" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Initiator" runat="server"></asp:TextBox>
                    <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Deviate" runat="server" Text="0"></asp:TextBox>
                    <asp:TextBox ID="txt_adv_type" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="txt_paymode" runat="server" Text="0"></asp:TextBox>
                    <asp:TextBox ID="txt_location" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="txt_per_day_amt" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="txt_pk_dtl_id" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="txt_update_xmldata" runat="server" Text=""></asp:TextBox>
                     <asp:TextBox ID="txt_fin_year" runat="server" ></asp:TextBox>
                     <asp:TextBox ID="txt_vendor_code" runat="server" ></asp:TextBox>
                     <asp:TextBox ID="txt_vendor_bill_no_add" runat="server" ></asp:TextBox>
                      <asp:TextBox ID="txt_exchange_Rate" runat="server" ></asp:TextBox>
                      <asp:TextBox ID="txt_amount_inr" runat="server" ></asp:TextBox>
                      <asp:TextBox ID="txt_bill_date" runat="server" ></asp:TextBox>
                     <asp:TextBox ID="txt_payment_method_1" runat="server" ></asp:TextBox>
                     <asp:TextBox ID="txt_tax_code_1" runat="server" ></asp:TextBox>
                     <asp:TextBox ID="txt_service_charges_1" runat="server" ></asp:TextBox>
                    <asp:TextBox ID="pk_bank_id" runat="server" Text="1" ></asp:TextBox>
                    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <%--	<script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>--%>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
        <script src="../../assets/js/demo.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script type="text/javascript" src="../../assets/js/canvasjs.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../assets/js/Vaildation.js"></script>
    
        <script src="../../JS/Advance_account_Approve_Foreign.js"></script>
    </form>

    <script type="text/javascript">

    
    </script>
</body>
</html>
