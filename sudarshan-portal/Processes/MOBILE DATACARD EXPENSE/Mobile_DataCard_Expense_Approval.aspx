<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Mobile_DataCard_Expense_Approval.aspx.cs" Inherits="Mobile_DataCard_Expense_Approval" %>

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
                                        <div id="Div8"><span id="span_Ifsc" runat="server">NA</span></div>
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
                                        <div id="Div10"><span id="span_DApprover" runat="server" style="display: none"></span><span id="span_Dapp_name" runat="server"></span></div>
                                    </div>
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
                       <div class="panel-heading-btn">
                           <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color: blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                       </div>
                       <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>MOBILE DATACARD REQUEST APPROVAL</h3>
                   </div>
                   <div class="panel-body" id="div_hdr">
                       <div class="form-horizontal">

                           <div class="form-group">
                               <div class="col-md-1"></div>
                               <label class="col-md-2">Payment Mode</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_paymentmode" runat="server"></asp:Label>
                               </div>
                               <div class="col-md-1"></div>
                               <div id="div_payment">
                                   <label class="col-md-2">Payment Location</label>
                                   <div class="col-md-3">
                                       <asp:Label ID="lbl_PayLocation" runat="server"></asp:Label>
                                   </div>
                               </div>
                           </div>
                           <div class="form-group">
                               <div class="col-md-1"></div>
                               <label class="col-md-2">Expense Head</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_expensetype" runat="server"></asp:Label>
                               </div>
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

           <div class="col-md-12" id="div_DataCard">
               <div class="panel panel-grey">
                   <div class="panel-heading">
                       <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>DataCard Approval</h4>
                   </div>
                   <div class="panel-body">
                       <div class="form-horizontal">
                           <div class="form-group">
                               <div class="col-md-1"></div>
                               <label class="col-md-2">Mobile No</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_mobile" runat="server"></asp:Label>
                               </div>
                               <label class="col-md-2">Service Provider</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_service" runat="server"></asp:Label>
                               </div>
                               <div class="col-md-1"></div>
                           </div>
                           <div class="form-group">
                               <div class="col-md-1"></div>
                               <label class="col-md-2">Year</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_year" runat="server"></asp:Label>
                               </div>
                               <label class="col-md-2">Month</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_month" runat="server"></asp:Label>
                               </div>
                               <div class="col-md-1"></div>
                           </div>
                           <div class="form-group">
                               <div class="col-md-1"></div>
                               <label class="col-md-2">Bill No</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_bill" runat="server"></asp:Label>
                               </div>
                               <label class="col-md-2">Bill Date</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_billdate" runat="server"></asp:Label>
                               </div>
                               <div class="col-md-1"></div>
                           </div>
                           <div class="form-group">
                               <div class="col-md-1"></div>
                               <label class="col-md-2">Bill Amount</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_billamt" runat="server"></asp:Label>
                               </div>
                               <label class="col-md-2">Reimbursement Amount</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_reimburseamt" runat="server"></asp:Label>
                               </div>

                               <div class="col-md-1"></div>
                           </div>
                           <%--<div class="form-group">
                            <div class="col-md-1"></div>
                           <label class="col-md-2">Supporting Doc</label>
							<div class="col-md-3">
								<asp:label id="lblsuppdoccard" runat="server"></asp:label>
							</div>
                             <label class="col-md-2">Supporting Particulars</label>
							<div class="col-md-3">
								<asp:label id="lblcardparticulars" runat="server"></asp:label>
							</div>
                            </div>--%>
                           <div class="form-group">
                               <div class="col-md-1"></div>
                               <label class="col-md-2">Tax</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_tax" runat="server"></asp:Label>
                               </div>
                               <label class="col-md-2">Total Amount</label>
                               <div class="col-md-3">
                                   <asp:Label ID="lbl_totaldataamt" runat="server"></asp:Label>
                               </div>
                           </div>
                           <%--<div class="form-group">
							<div class="col-md-1"></div>
							<label class="col-md-2">Remark</label>
							<div class="col-md-3">
								 <textarea type='text' class="form-control" rows="1" id="txtRemark" runat="server" ></textarea>
							</div>
						</div>--%>
                       </div>
                   </div>
               </div>
           </div>
           <div class="col-md-12" id="div_UserMobileA">
               <div class="panel panel-grey">
                   <div class="panel-heading">
                       <div class="panel-heading-btn">
                           <h4 class="panel-title">&nbsp;/ Total Amount : <span id="spn_Total" runat="server">0</span></h4>
                       </div>
                       <div class="panel-heading-btn">
                           <h4 class="panel-title"><a href="#div_Policy1" data-toggle="modal">View Policy</a></h4>
                       </div>
                       <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Mobile Approval</h4>
                   </div>

                   <div class="panel-body" id="div3">
                       <%--<div class="panel pagination-danger">--%>

                       <div class="table-responsive" style="width: 100%" id="div_dedction" runat="server"></div>
                       <div class="table-responsive">
                           <div id="div_userMobile" runat="server"></div>
                       </div>
                       <%---</div>--%>
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
                                <asp:DropDownList ID="ddl_Action" runat="server" class="form-control input-sm" onchange="hide_show_remark()"></asp:DropDownList>
                            </div>
                            <label class="col-md-1" style="display: none" id="lbl_rmk">Remark</label>
                            <div class="col-md-6" style="display: none" id="div_remark">
                                <textarea type='text' class="form-control" cols="10" rows="2" id="txtRemark" runat="server"></textarea>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-8" id="tab_btn3" runat="server" style="text-align: center;">
                                <asp:Button ID="btnRequest" runat="server" class="btn btn-danger  btn-rounded" Text="Submit" OnClick="btnRequest_Click" OnClientClick="return prepareData()" />
                                <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded" Text="Cancel" OnClick="imgBtnRelease_Click" />
                            </div>
                        </div>
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
                    <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_approvar" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Remark" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Approver" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Initiator" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_PayMode" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_PayLocation" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
                    <asp:TextBox ID="doa_user" runat="server"></asp:TextBox>
                    <asp:TextBox ID="doa_email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="doa_support" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Mailid" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_sameapp" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Audit1" runat="server"></asp:TextBox>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.4.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>

    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            var expensety = $("#lbl_expensetype").text();
            var dedamt = $("#lbl_dedamount").text();
            var dedmonth = $("#lbl_dedmonth").text();

            if (expensety == "Mobile") {
                $("#div_UserMobileA").show();
                $("#div_DataCard").hide();
                if (dedamt == "" && dedmonth == "") {
                    $("#div2").hide();
                }
                else {
                    $("#div2").show();
                }
            }
            else if (expensety == "DataCard") {
                $("#div_UserMobileA").hide();
                $("#div_DataCard").show();
                $("#div2").hide();
                var cardbill = parseInt($("#lbl_billamt").text());
                var reiamt = parseInt($("#lbl_reimburseamt").text());
                var tsx = parseInt($("#lbl_tax").text());
                if (reiamt != "" && tsx != "" && cardbill != "") {
                    if (cardbill >= reiamt) {
                        var diff = reiamt + tsx;
                        $('#lbl_totaldataamt').html(diff);
                    }
                    else {
                        var diff = cardbill + tsx;
                        $('#lbl_totaldataamt').html(diff);
                    }
                }
            }
            var mode = $("#lbl_paymentmode").text();
            if (mode == "BANK") {
                $("#div_payment").hide();
            }
            else {
                $("#div_payment").show();
            }

        });

        function prepareData() {
            var act = document.getElementById("ddl_Action").value;
            if (act == "") {
                alert("Please Select Action first...!");
                return false;
            }
            var remrk = document.getElementById("txtRemark").value;
            if (act != "Approve") {
                if (remrk == "") {
                    alert("Please Enter Remark...!");
                    return false;
                }
            }

        }
        function hide_show_remark() {
            $("#txtRemark").val("");
            if ($("#ddl_Action option:selected").index() < 2) {
                $("#div_remark").hide();
                $("#lbl_rmk").hide();
            }
            else {
                $("#div_remark").show();
                $("#lbl_rmk").show();
            }
        }
        g_serverpath = '/Sudarshan-Portal';
        function downloadfiles(index) {
            window.open(g_serverpath + '/Common/FileDownload.aspx?indentno=NA&filename=' + $("#uploadTable tr")[index].cells[2].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
        }
    </script>
</body>
</html>

