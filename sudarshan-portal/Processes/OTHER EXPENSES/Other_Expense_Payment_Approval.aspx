<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Other_Expense_Payment_Approval.aspx.cs" Inherits="Other_Expense_Payment_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Travel Request</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
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
       
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    
           <div id="divIns" runat="server"> </div>

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
                                <label class="col-md-2">Request No :</label>
                                <div class="col-md-3">
                                    <div id="Div4"><span id="spn_req_no" runat="server"></span></div>
                                </div>
                                <label class="col-md-2">Request Date :</label>
                                <div class="col-md-3">
                                    <div id="Div6"><span id="spn_date" runat="server"></span></div>
                                </div>
                                
                                <div class="col-md-1"></div>
                            </div>
                               <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Employee No :</label>
                                <div class="col-md-3">
                                    <div id="Div3"><span id="empno" runat="server"></span></div>
                                </div>
                                <label class="col-md-2">Employee Name :</label>
                                <div class="col-md-3">
                                    <div id="EmployeeName"><span id="span_ename" runat="server"></span></div>
                                </div>
                                
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Designation :</label>
                                <div class="col-md-3">
                                    <div id="Div2"><span id="span_designation" runat="server"></span></div>
                                </div>
                                   <label class="col-md-2">Division :</label>
                                <div class="col-md-3">
                                    <div id="Div9"><span id="span_Division" runat="server">NA</span></div>
                                </div>
                               
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Department :</label>
                                <div class="col-md-3">
                                    <div id="EmployeeDepartment"><span id="span_dept" runat="server"></span></div>
                                </div>
                                <label class="col-md-2">Grade :</label>
                                <div class="col-md-3">
                                    <div id="grade"><span id="span_grade" runat="server"></span></div>
                                </div>
                            </div>
                            <div class="form-group">
                             <div class="col-md-1"></div>
                               <label class="col-md-2">Cost Center :</label>
                                <div class="col-md-3">
                                    <div id="Div1"><span id="span_cc" runat="server"></span></div>
                                </div>
                                   <label class="col-md-2">Mobile No. :</label>
                                <div class="col-md-3">
                                    <div id="EmployeePhoneNo"><span id="span_mobile" runat="server"></span></div>
                                </div>
                               
                                 <div class="col-md-1"></div>
                            </div>
                                  <div class="form-group">
                                <div class="col-md-1"></div>
                                
                             <label class="col-md-2">Bank Account No. :</label>
                                <div class="col-md-3">                             
                                     <span id="span_bank_no" runat="server"></span>
                                </div>
                                        <label class="col-md-2">IFSC No.</label>
                                    <div class="col-md-3">
                                        <div id="Div7"><span id="span_Ifsc" runat="server">NA</span></div>
                                    </div>
                            </div>
                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Base Location</label>
                                    <div class="col-md-3">
                                        <div id="Div12"><span id="spn_base_location" runat="server">NA</span></div>
                                    </div>
                                </div>
                             <div class="form-group">
                                    <div class="col-md-1"></div>
                                   <label class="col-md-2">Approver : </label>
                                <div class="col-md-3">
                                    <div id="Div5"><span id="span_Approver" runat="server" style="display:none"></span><span id="span_app_name" runat="server"></span></div>
                                </div>
                                    <label class="col-md-2">Deviation Approver </label>
                                    <div class="col-md-3">
                                        <div id="Div8"><span id="span_DApprover" runat="server" style="display: none"></span><span id="span_Dapp_name" runat="server"></span></div>
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
                        <h3 class="panel-title">OTHER EXPENSE - PAYMENT PROCESS</h3>
                       
                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">
                            
                               <div class="form-group">
                                <div class="col-md-1"></div>                             
                                    <label class="col-md-2">Payment Mode :</label>
                                <div class="col-md-2">
                                    <div id="Div10"><b>
                                        <span id="ddl_Payment_Mode" runat="server"></span>
                                    </b></div>
                                </div>
                                    <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Location :</label>
                                <div class="col-md-2">
                                            <span id="ddlAdv_Location" runat="server"></span>
                                </div>
                            </div>
                          
                             <div class="form-group">
                                      <div class="col-md-1"></div>
                                 <label class="col-md-2">Remark :</label>
                                <div class="col-md-2">
                                        <asp:TextBox ID="req_remark" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>
                                  <div class="col-md-1"></div>
                                <label class="col-md-2">Supporting Documents :</label>
                                <div class="col-md-3">
                                    <a href="#div_UploadDocument" data-toggle="modal">
                                        <img id="A_FileUpload1" src="../../images/attachment.png" alt="Click here to attach file." height="27px" width="27px"></a>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            

                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">Advance Details</h4>
                    </div>
                    <div class="panel-body">
                        <div id="div_Advance" runat="server">
                        </div>
                    </div>
                </div>
            </div>

            	<div class="col-md-12">
			<div class="panel panel-grey">
				<div class="panel-heading">
					<div class="panel-heading-btn">
						<h4 class="panel-title">Total Amount : <span id="spn_Total" runat="server">0</span></h4>
					</div>
					<h4 class="panel-title">Expense Details</h4>
				</div>
				<div class="panel-body">
                    <div id="div_req_data" runat="server" class="table-responsive">
                    </div>
				    
				</div>
			</div>
		</div>
		<div class="col-md-12">
			<div class="panel panel-grey">
				<div class="panel-heading">
					<h3 class="panel-title">Action</h3>
				</div>
				<div class="panel-body">
                    <div class="row">
                    <div class="col-md-2"></div>
					<div class="col-md-2">
                        <b>Select Action : </b>
                    </div>
                        <div class="col-md-2">
                        <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control input-sm" onchange="checkRemark()">
                            <asp:ListItem Value="0">---Select One---</asp:ListItem>
                            <%--<asp:ListItem Value="1">Approve</asp:ListItem>--%>
                            <asp:ListItem Value="2">Send Back</asp:ListItem>
                            <%--<asp:ListItem Value="3">Reject</asp:ListItem>--%>
                             <asp:ListItem Value="4">Change Payment Mode</asp:ListItem>
                            <asp:ListItem Value="5">Send For L1 Approval</asp:ListItem>
                        </asp:DropDownList>
					</div>
                    <div class="col-md-5">
                        <asp:TextBox ID="txt_approver_Remark" runat="server" TextMode="MultiLine" placeholder="Enter Remark" CssClass="form-control input-sm" style="display:none"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4"></div>
					<div class="col-md-3">
                        <asp:Button ID="btn_Save" runat="server" CssClass="btn btn-danger btn-rounded" Text="Submit" OnClientClick="return PrepareData();" OnClick="btnSubmit_Click"/> <%----%>
						 
                        <input type="button" id="btn_Cancel" class="btn btn-danger btn-rounded" value="Cancel" onclick="gotoback()"  />
					</div>
                        <div class="col-md-4"></div>
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
                <div class="modal fade" id="div_UploadDocument" >
		<div class="modal-dialog"  style="width:45%; margin-left:25%">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
					<h4 class="modal-title"><font color="white"><b>File Attachment </b> </font></h4>
				</div>                
				<div class="modal-body">                                                   
                    <div class="table-responsive" id="div_docs" runat="server" >
                    </div>
				</div>                
				<div class="modal-footer">
					<a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal" >Close</a>
				</div>
			</div>
		</div>
	</div>

        <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_designation" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_xml_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_sub_xml_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Step" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
            <asp:TextBox ID="pageno" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Initiator" runat="server"></asp:TextBox>
            <asp:TextBox ID="doa_user" runat="server"></asp:TextBox>
            <asp:TextBox ID="doa_email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtApproverEmail" runat="server"></asp:TextBox>
            <asp:TextBox ID="loc_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="pmode_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_advance_id" runat="server"></asp:TextBox>
             
        </div>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
    </form>
    <script type="text/javascript">
       function downloadfiles(index) {
    var app_path = $("#app_Path").val();
    var req_no = $("#spn_req_no").text();
    window.open(app_path + '/Common/FileDownload.aspx?indentno=' + req_no + '&filename=' + index + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}
        function gotoback() {
            window.location.href = "../../Portal/SCIL/TaskDetails.aspx";
        }

        function checkRemark() {
            $("#txt_approver_Remark").val("");
            if ($("#ddlAction option:selected").val() < 1) {
                $("#txt_approver_Remark").hide();
            }
            else {
                $("#txt_approver_Remark").show();
            }
        }

        function PrepareData() {
            if ($("#ddlAction option:selected").val() == 0) {
                alert("Please Select Action...!");
                return false;
            }
            else {
                if ($("#ddlAction option:selected").val() > 0) {
                    if ($("#txt_approver_Remark").val() == "") {
                        alert("Please Enter Remark...!");
                        return false;
                    }
                }
                return true;
            }
        }
    </script>
</body>
</html>
