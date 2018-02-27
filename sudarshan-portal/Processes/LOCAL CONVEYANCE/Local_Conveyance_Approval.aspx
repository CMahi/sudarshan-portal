<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Local_Conveyance_Approval.aspx.cs" Inherits="Local_Conveyance_Approval"  EnableEventValidation="false" ValidateRequest="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Travel Request</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker3.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>

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
                                    <div id="Div1"><span id="span_div" runat="server"></span></div>
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
                                    <div id="Div4"><span id="span_cc" runat="server"></span></div>
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
                                        <div id="Div8"><span id="span_Ifsc" runat="server">NA</span></div>
                                    </div>
                                  
                                </div>
                            <div class="form-group">
                                 
                                       <div class="col-md-1"></div>
                               <label class="col-md-2">Approver </label>
                                <div class="col-md-3">
                                    <div id="Div5"><span id="span_Approver" runat="server" style="display:none"></span><span id="span_app_name" runat="server"></span></div>
                                </div>
                                  <label class="col-md-2">Deviation Approver </label>
                                    <div class="col-md-3">
                                        <div id="Div6"><span id="span_DApprover" runat="server" style="display: none"></span><span id="span_Dapp_name" runat="server"></span></div>
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
                            <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color:blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                        </div>
                                     <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>LOCAL CONVEYANCE REQUEST APPROVAL</h3>
                       
                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">
                              <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Mode </label>
                             <div class="col-md-2">
                                  <asp:DropDownList ID="ddl_Payment_Mode" runat="server" class="form-control input-sm" Enabled="false">
                                    </asp:DropDownList>                               
                                </div>
                                    <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Location</label>
                                <div class="col-md-2">
                                  <asp:DropDownList ID="ddlAdv_Location" runat="server" class="form-control input-sm" Enabled="false">
                                    </asp:DropDownList>                               
                                </div>
                            </div>
                                <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Narration </label>
                             <div class="col-md-2">
                                      <div id="Remark"><textarea id="txt_tremark" class="form-control" rows="1"  maxlength="200" runat="server" readonly=""></textarea></div>                      
                                </div>
                                           
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Supporting Documents</label>
                                <div class="col-md-3">
								<a href="#div_UploadDocument" data-toggle="modal"><img id="A_FileUpload1" src="../../images/attachment.png" alt="Click here to attach file." height="20" width="20" /></a>
						 
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                 <div class="col-md-12" id="advance_details">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                       
                         <table style="width: 100%">
                            <tr>
                                <td style="width: 30%">
                                     <h4 class="panel-title">Advance Details</h4>
                                </td>
                                <td style="width: 30%; text-align: right">
                                     <b> <span id="spn_hdr" runat="server">Recovery Amount : </span></b>
                                   <b> <span id="spn_val" runat="server">0</span></b>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="panel-body">
                        <div id="div_Advance" runat="server">
                        </div>                       
                    </div>
                </div>
            </div>
         <div class="col-md-12" id="div_Local">
			<div class="panel panel-grey">
				<div class="panel-heading">
                    <div class="panel-heading-btn">
						 <h4 class="panel-title"> &nbsp;/Total Amount : <span id="spn_Total" runat="server">0</span></h4>
					</div>
                    <div class="panel-heading-btn">
						<h4 class="panel-title"><a href="#div_policy1" data-toggle="modal">View Policy</a></h4>
					</div>
					<h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Local Conveyance Approval</h4>
			</div>
				</div>               		
				<div class="panel-body" id="div7">					
						<div class="table-responsive" style="width: 100%" id="div_LocalData" runat="server">
                        </div>
                   <%-- <div class="panel panel-grey">
                        <div class="panel-heading">
                        <h4>Summary</h4>
                            </div>
                        </div>   
                       <div class="table-responsive" style="width: 100%" id="div_summary" runat="server">
                                      <table ID='tblAdvance' class='table table-bordered'><tr class='grey'><th>#</th><th>Voucher Amount</th><th>Advance Amount</th></tr>
                                     <tr> <td style="width: 30%">
                                    <span id="Span1" runat="server">0</span>
                                     </td>
                                   <td><span id="Span2" runat="server">0</span></td>
                                   </tr>
     
                        </table>
                        </div>--%>
				</div>
			</div>
              
	
    
            <div style="display: none;" class="modal" id="div_policy1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Local Conveyance Policy</h5>
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
							</div >
                            <label class="col-md-1" style="display:none" id="lbl_rmk">Remark</label>
							<div class="col-md-6" style="display:none" id="div_remark">
								 <textarea type='text' class="form-control" cols="10" rows="2" id="txt_Remark" runat="server"></textarea>
							</div>
                           </div>
                        	<div class="form-group">
							<div class="col-md-8" id="tab_btn3" runat="server" style="text-align: center;">
								 <asp:Button ID="btnRequest" runat="server" class="btn btn-danger  btn-rounded" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return prepareData()" />
							     <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded" Text="Cancel" OnClick="imgBtnRelease_Click" />
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
          <div class="col-md-12" style="margin-top: 10px;" id="div11">
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
						      <div  style="width: 100%" id="divalldata" runat="server"></div>
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
 
        <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="json_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_designation" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_xml_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_sub_xml_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Step" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Initiator" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Condition" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtApproverEmail" runat="server"></asp:TextBox>
            <asp:TextBox ID="doa" runat="server"></asp:TextBox>
            <asp:TextBox ID="doa_user" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
            <asp:TextBox ID="doa_email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Audit" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_adcount" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
        </div>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            var mode = $("#ddl_Payment_Mode").val();
            if (mode == 2) {
                $("#ddlAdv_Location").hide();
            }
            else {
                $("#ddlAdv_Location").show();
            }
            if ($("#txt_adcount").val() == "0") {
                $("#advance_details").hide();
            }
            else {
                $("#advance_details").show();
            }
        });

        function prepareData() {
            var act = document.getElementById("ddlAction").value;
            if (act == "") {
                alert("Please Select Action first...!");
                return false;
            }
            var remrk = document.getElementById("txt_Remark").value;
            if (act != "Approve") {
                if (remrk == "") {
                    alert("Please Enter Remark...!");
                    return false;
                }
            }

        }


        function hide_show_remark() {
            $("#txt_Remark").val("");
            if ($("#ddlAction option:selected").index() < 2) {
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
            alert($("#uploadTable tr")[index].cells[2].innerText);
            window.open(g_serverpath + '/Common/FileDownload.aspx?indentno=NA&filename=' + $("#uploadTable tr")[index].cells[2].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
        }
</script>
</body>
</html>
