<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Local_Conveyance.aspx.cs" Inherits="Local_Conveyance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Mobile Reimbursement</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />

</head>
<body style="overflow-x: hidden">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
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
					<h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>Employee Details</h3>
				</div>
				<div class="panel-body" id="div1">
					<div class="form-horizontal">
					
						<div class="form-group">
							<div class="col-md-1"></div>
                                <label class="col-md-2">Payment Mode</label>
                                <div class="col-md-3">
                                   <asp:DropDownList ID="ddlPayMode" runat="server" class="form-control  input-sm width-xs">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <div id="div_payment">
                                    <label class="col-md-2">Payment Location</label>
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
                                    <asp:Label ID="lbl_ExpenseHead" runat="server"></asp:Label>
                                </div>
							
							<div class="col-md-1"></div>
							<label class="col-md-2">Supporting Documents</label>
							<div class="col-md-3">
								<a href="#div_UploadDocument" data-toggle="modal"><img id="A_FileUpload1" src="../../images/attachment.png" alt="Click here to attach file." height="20" width="20" /></a>
							</div>
						</div>
                        <div class="form-group">
                   <div class="col-md-1"></div>
							<label class="col-md-2">Narration</label>
							<div class="col-md-3">
								<textarea id="txt_remark" class="form-control" rows="1"   runat="server" maxlength="200"></textarea>                      
							</div>
                            </div>
					</div>
				</div>
			</div>
		</div>
             <div class="col-md-12" id="advance_details">
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
          <div class="col-md-12" id="div_localconveyance">
			<div class="panel panel-grey">
				<div class="panel-heading">
                     <div class="panel-heading-btn">
                            <h4 class="panel-title"> &nbsp;/ Total Amount : <span id="spn_Total">0</span></h4>
                        </div>
					<div class="panel-heading-btn">
						<h4 class="panel-title"><a href="#div_Policy" data-toggle="modal">View Policy</a></h4>
					</div>
					<h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Local Conveyance</h4>
				</div>
				<div class="panel-body">
                    <p style="color: red">Note : <i class="fa fa-fw fa-angle-double-right"></i><i class="fa fa-fw fa-angle-double-right"></i>Kindly mention mode of travel in case of "Other"</p>
                    <div class="table-responsive">
					<table class="table table-bordered" id="tbl_Local">
						<thead>
							<tr>
								<th style="width:15%">Vehicle Type</th>
								<th style="width:15%">From Location</th>
								<th style="width:15%">To Location</th>
                                <th style="width:15%">From Date</th>
                                 <th style="width:15%">To Date</th>
								<th style="width:10%">KMs</th>                              
								<th style="width:10%">Amount</th>
								 <th>Add</th>
                                                <th>Delete</th>
							</tr>
						</thead>
						<tbody>
							<tr>
								<td><asp:DropDownList ID="ddlVehicleType1" runat="server" class="form-control  input-sm"  onchange="valuetypeamt(1)">
                                    </asp:DropDownList>									
								</td>

								<td><input id='txt_fromloc1' type='text' class="form-control input-sm" /></td>
								<td><input id='txt_toloc1' type='text' class="form-control input-sm" /></td>
                                <td><div class='input-group'>
                                      <input type='text' class="form-control datepicker-dropdown" id="txt_fdate1" runat="server" readonly="" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                   </div></td>
                                <td><div class='input-group'>
                                      <input type='text' class="form-control datepicker-dropdown" id="txt_tdate1" runat="server" readonly=""  />
                                        <span class="input-group-btn">
                                            <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                   </div></td>
								<td><input id='txt_kms1' type='text' class="form-control input-sm" onkeypress="return isNumberKey(event)"  onchange="valuechanamt(1)"  /></td>
								<td><input id='txt_amount1' type='text' class="form-control input-sm" onkeypress="return isNumberKey(event)" readonly="" onchange="calculate_Total()"/></td>								   
                                <td class="add_Local"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-plus"></i></td>
                                <td class="delete"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-trash" onclick='delete_Row(1)'></i></td>                              
							</tr>
						</tbody>
					</table>
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
                            <asp:Button ID="btn_Save" runat="server" class="btn btn-danger btn-rounded" Text="Submit"
                                OnClientClick="return createxml();"  OnClick="btnRequest_Click" />
                            <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded" Text="Cancel" OnClick="imgBtnRelease_Click" />
                                        </ContentTemplate>
                                </asp:UpdatePanel>
                        </div>
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
                                        <input class="form-control input-sm" id="txt_description" name="txt_description" type="text" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Attach File</label>
                                <asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-md-7">
                                            <span class="btn btn-grey fileinput-button">
                                                <asp:AsyncFileUpload ID="FileUpload1" runat="server" OnClientUploadError="uploadError"
                                                    OnClientUploadStarted="StartUpload" OnClientUploadComplete="UploadComplete" CompleteBackColor="Lime"
                                                    ErrorBackColor="Red" OnUploadedComplete="btnUpload_Click"
                                                    UploadingBackColor="#66CCFF" />
                                            </span>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel> 
                                          
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                  <p style="color: red"><label >Upload .pdf,.jpeg,.jpg,.png formats only.</label></p>
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
        <div style="display: none;" class="modal" id="div_Policy">
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
            <asp:TextBox ID="txt_Action" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Audit" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtDummy" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_xml_data_vehicle" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_approvar" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_twowhamt" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_fourwhamt" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_vehitype" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_adcount" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_advance_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_pkexpenseid" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_advance_amount" runat="server" Text="0"></asp:TextBox>
        </div>

        <!-- ================== BEGIN BASE JS ================== -->

        <script src="../../assets/plugins/jquery/jquery-2.1.4.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script type="text/javascript" src="../../JS/Local_Conveyance.js"></script>        
        <script src="../../JS/validator.js"></script>
        <script src="../../assets/js/Vaildation.js"></script>
    </form>
    <script type="text/javascript">
        var str = "";
        $(document).ready(function () {
           
            $(".datepicker-dropdown").datepicker({
                format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', 

            })
            if ($("#lbl_bankAccNo").html() == "NA" || $("#lbl_bankAccNo").html() == "") {
                alert("Bank Account Is Not Available! Unable To Claim Expense Request...!");
                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            }
            else {
                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');               
                if ($("#txt_approvar").val() == "NA" || $("#txt_approvar").val() == "") {
                    alert("Approver Is Not Available! Unable To Claim Expense Request...!");
                    $('input[Id="btn_Save"]').attr('disabled', 'disabled');
                }
                else {
                    $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
                }
            }
            if ($("#txt_adcount").val() == "0") {
                $("#advance_details").hide();
            }
            else {
                $("#advance_details").show();
            }
        });
      
        $('#txt_fdate1').datepicker({ format: 'dd-M-yy', endDate: new Date(), autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
            daydiffrence(1);
        });
        $('#txt_tdate1').datepicker({ format: 'dd-M-yy', endDate: new Date(), autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
            daydiffrence(1);
        });
        /***************** Company Provided Mobile**********************/
        $('body').on('click', '.add_Local', function () {
            addNewRowLocal();
        });
        function addNewRowLocal() {
            var lastRow = $('#tbl_Local tr').length;
            var lastRow1 = $('#tbl_Local tr').length - 1;
            var ddlvehitype = document.getElementById("txt_vehitype").value;

            var mrmk = document.getElementById("txt_remark").value;
            if (mrmk == "") {
                alert('Please Enter Remark');
                return false;
            }
            for (var q = 0; q < lastRow-1; q++) {
                var vtype = document.getElementById("ddlVehicleType" + (q + 1)).value;
                var from = document.getElementById("txt_fromloc" + (q + 1)).value;
                var to = document.getElementById("txt_toloc" + (q + 1)).value;
                var fdate = document.getElementById("txt_fdate" + (q + 1)).value;
                var tdate = document.getElementById("txt_tdate" + (q + 1)).value;
                var kms = document.getElementById("txt_kms" + (q + 1)).value;
                var mbillamt = document.getElementById("txt_amount" + (q + 1)).value;

                if (vtype == 0) {
                    alert('Please Select Vehicle Type at row :' + (q + 1) + '');
                    return false;
                }
                if (from == "") {
                    alert('Please Enter From Location at row :' + (q + 1) + '');
                    return false;
                }
                if (to == "") {
                    alert('Please Select To Location at row :' + (q + 1) + '');
                    return false;
                }
                if (fdate == 0) {
                    alert('Please Select From Date at row :' + (q + 1) + '');
                    return false;
                }
                if (tdate == 0) {
                    alert('Please Select To Date at row :' + (q + 1) + '');
                    return false;
                }
                if (vtype != "Other") {
                    if (kms == "") {
                        alert('Please Enter KMS at row :' + (q + 1) + '');
                        return false;
                    }
                }
                if (mbillamt == "") {
                    alert('Please Enter Amount at row :' + (q + 1) + '');
                    return false;
                }
               
            }          
            var html2 = "<tr>";
            var html3 = "<td><select id='ddlVehicleType" + (lastRow) + "' class='form-control input-sm' onchange='valuetypeamt(" + (lastRow) + ")'>" + ddlvehitype + "</select></td>";
            var html4 = "<td><input class='form-control input-sm' type='text' id='txt_fromloc" + (lastRow) + "'></input></td>";
            var html5 = "<td><input class='form-control input-sm' type='text' id='txt_toloc" + (lastRow) + "'></input></td>";
            var html6 = "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown' id='txt_fdate" + (lastRow) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html7 = "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown' id='txt_tdate" + (lastRow) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html8 = "<td><input class='form-control input-sm' type='text' id='txt_kms" + (lastRow) + "' onkeypress='return isNumberKey(event)' onchange='valuechanamt(" + (lastRow) + ");'  ></input></td>";
            var html9 = "<td><input class='form-control input-sm' type='text' id='txt_amount" + (lastRow) + "' onkeypress='return isNumberKey(event)' onchange='calculate_Total()' readonly=''></input></td>";
            var html10 = "<td class='add_Local' id='add_Row" + lastRow + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
            var html11 = "<td id='delete_Row" + lastRow + "' ><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_Row(" + lastRow + ")'></i></td></tr>";
            var htmlcontent = $(html2 + "" + html3 + "" + html4 + "" + html5 + "" + html6 + "" + html7 + "" + html8 + "" + html9 + "" + html10 + "" + html11);

             $('#tbl_Local').append(htmlcontent);
             $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, endDate: new Date(), todayBtn: 'linked' });
             $('#txt_fdate' + lastRow).datepicker({ format: 'dd-M-yy', autoclose: true, endDate: new Date(), todayBtn: 'linked' }).on('changeDate', function (ev) {
                 daydiffrence(lastRow);
             });
             $('#txt_tdate' + lastRow).datepicker({ format: 'dd-M-yy', autoclose: true, endDate: new Date(), todayBtn: 'linked' }).on('changeDate', function (ev) {
                 daydiffrence(lastRow);
             });
        }     
        function delete_Row(RowIndex) {
            try {
            var tbl = document.getElementById("tbl_Local");
            var lastRow = tbl.rows.length;
            if (lastRow <= 2) {
                alert("Validation Error:You have to Enter atleast one record..!");
                return false;
            }
            var IsDelete = confirm("Are you sure to delete this ?");
            if (IsDelete) {
                for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
                document.getElementById("ddlVehicleType" + (parseInt(contolIndex) + 1)).onchange = new Function("valuetypeamt(" + contolIndex + ")");
                document.getElementById("ddlVehicleType" + (parseInt(contolIndex) + 1)).id = "ddlVehicleType" + contolIndex;
                document.getElementById("txt_fromloc" + (parseInt(contolIndex) + 1)).id = "txt_fromloc" + contolIndex;
                document.getElementById("txt_toloc" + (parseInt(contolIndex) + 1)).id = "txt_toloc" + contolIndex;
                document.getElementById("txt_fdate" + (parseInt(contolIndex) + 1)).changeDate = new Function("valuechanamt(" + contolIndex + ")");
                document.getElementById("txt_fdate" + (parseInt(contolIndex) + 1)).id = "txt_fdate" + contolIndex;
                document.getElementById("txt_tdate" + (parseInt(contolIndex) + 1)).changeDate = new Function("valuechanamt(" + contolIndex + ")");
                document.getElementById("txt_tdate" + (parseInt(contolIndex) + 1)).id = "txt_tdate" + contolIndex;
                document.getElementById("txt_kms" + (parseInt(contolIndex) + 1)).onchange = new Function("valuechanamt(" + contolIndex + ")");
                document.getElementById("txt_kms" + (contolIndex + 1)).id = "txt_kms" + contolIndex;
                document.getElementById("txt_amount" + (parseInt(contolIndex) + 1)).onchange = new Function("calculate_Total()");
                document.getElementById("txt_amount" + (parseInt(contolIndex) + 1)).id = "txt_amount" + contolIndex;
                document.getElementById("add_Row" + (contolIndex + 1)).id = "add_Row" + contolIndex;
                document.getElementById("delete_Row" + (contolIndex + 1)).onclick = new Function("return delete_Row(" + contolIndex + ")");
                document.getElementById("delete_Row" + (contolIndex + 1)).id = "delete_Row" + contolIndex;                               
            }
                ///////////////////////////////////////////////////////////////////////
            tbl.deleteRow(RowIndex);
            var total = 0;
            var lastRow = $('#tbl_Local tr').length;
            for (var q = 0; q < lastRow ; q++) {
                var amount = 0;
                if ($("#txt_amount" + (q + 1)).val() != "" && $("#txt_amount" + (q + 1)).val() != undefined) {
                    amount = parseFloat($("#txt_amount" + (q + 1)).val());
                    $("#txt_amount" + (q + 1)).val(amount);
                }
                else {
                    $("#txt_amount" + (q + 1)).val(0);
                }
                total = total + amount;
            }
            $("#spn_Total").html(total);
          
            alert("Record Deleted Successfully..!");
            }
        }
        catch (Exc) { }
      }

      $("#ddlPayMode").change(function () {
            var mode = $("#ddlPayMode").val();
            if (mode == 2) {
                $("#div_payment").hide();
            }
            else {
                $("#div_payment").show();
            }

        });
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function valuetypeamt(index) {
            var mkms = $('#txt_kms' + index).val();
            var fdate = $('#txt_fdate' + index).val();
            var tdate = $('#txt_tdate' + index).val();
            var vtype = $("#ddlVehicleType" + index).val();           
            if (vtype=="Other")
            {
                $('#txt_amount' + index).val("");
                $('#txt_amount' + index).attr('readonly', false);
            }
            if (fdate != "" && tdate != "" && mkms != "" && vtype != "Other") {
                Local_Conveyance.fillAmount(fdate,tdate, index, callback_local_Detail);
                $('#txt_amount' + index).attr('readonly', true);
            }
        }      
        function valuechanamt(index) {
            var tdate = $('#txt_tdate' + index).val();
            var fdate = $('#txt_fdate' + index).val();
            var mkms = $('#txt_kms' + index).val();
            var vtype = $("#ddlVehicleType" + index).val();
            if (fdate == "") {
                if (tdate == "") {
                    alert("Please Select To Date..");
                }
            }
            else if (tdate == "") {
                if (fdate == "") {
                    alert("Please Select From Date..");
                }
            }
            if (fdate != "" && tdate != "" && vtype != "Other" && mkms != "") {
                Local_Conveyance.fillAmount(fdate, tdate, index, callback_local_Detail);
            }
        }
        function convertMonthNameToNumber(monthName) {
            var myDate = new Date(monthName + " 1, 2000");
            var monthDigit = myDate.getMonth();
            return isNaN(monthDigit) ? 0 : (monthDigit + 1);
        }
        function callback_local_Detail(response) {
            if (response.value != 1) {
                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');               
                var arr = response.value.split("/");
                $('#txt_amount' + arr[0]).attr('readonly',true);
                var mkms = $('#txt_kms' + arr[0]).val();
                if ($("#ddlVehicleType" + arr[0]).val() == "Two Wheeler") {
                    if (arr[2] == "Two") {
                        var amt = parseFloat(arr[1]);
                    }
                    if (arr[4] == "Two") {
                        var amt = parseFloat(arr[3]);
                    }
                }
                else if ($("#ddlVehicleType" + arr[0]).val() == "Four Wheeler") {
                    if (arr[2] == "Four") {
                        var amt = parseFloat(arr[1]);
                    }
                    if (arr[4] == "Four") {
                        var amt = parseFloat(arr[3]);
                    }
                }
                if (parseFloat(amt) != 0) {
                    $('#txt_amount' + arr[0]).val(parseFloat(mkms) * parseFloat(amt));
                    var total = 0;
                    var lastRow = $('#tbl_Local tr').length;
                    for (var q = 0; q < lastRow - 1; q++) {
                        var amount = 0;                     
                        if ($("#txt_amount" + (q + 1)).val() != "" && $("#txt_amount" + (q + 1)).val() != undefined) {
                            amount =Math.round(parseFloat($("#txt_amount" + (q + 1)).val()));
                            $("#txt_amount" + (q + 1)).val(amount);                           
                        }
                        else {
                            $("#txt_amount" + (q + 1)).val(0);
                        }
                        total = total + amount;
                    }
                    $("#spn_Total").html(total);
                    if($('#txt_amount' + arr[0]).val() == 'NaN')
                    {
                        $('input[Id="btn_Save"]').attr('disabled', 'disabled');
                        $('#txt_amount').val('');
                        alert("Expense cannot be submitted as policy rate is not defined!!");
                        return false;
                    }
                }
            }
            else {
                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
                alert("Expense cannot be submitted as policy rate is not defined!!");
                return false;
            }           
        }
        function daydiffrence(index) {
            var tdate = $('#txt_tdate' + index).val();
            var fdate = $('#txt_fdate' + index).val();
            if ($('#txt_fdate' + index).val() == "") {
                return false;
            }
            if ($('#txt_tdate' + index).val() == "") {
                return false;
            }
            var start = $('#txt_fdate' + index).val()
            var stard = start.substring(0, 2);
            var starm = start.substring(3, 6);
            var stary = start.substring(7, 11);

            var end = $('#txt_tdate' + index).val()
            var endd = end.substring(0, 2);
            var endm = end.substring(3, 6);
            var endy = end.substring(7, 11);
            var d1 = new Date(stary, getMonthFromString(starm) - 1, stard);
            var d2 = new Date(endy, getMonthFromString(endm) - 1, endd);
            if (d1 > d2) {
                alert('To Date must be greater than from Date');
                $('#txt_fdate' + index).focus();
                $('#txt_tdate' + index).val('');
                return false;
            }
            else {
                var tdate = $('#txt_tdate' + index).val();
                var fdate = $('#txt_fdate' + index).val();
                var mkms = $('#txt_kms' + index).val();
                var vtype = $("#ddlVehicleType" + index).val();
                if (fdate == "") {
                    if (tdate == "") {
                        alert("Please Select To Date..");
                    }
                }
                else if (tdate == "") {
                    if (fdate == "") {
                        alert("Please Select From Date..");
                    }
                }
                if (fdate != "" && tdate != "" && vtype != "Other" && mkms != "") {
                    Local_Conveyance.fillAmount(fdate, tdate, index, callback_local_Detail);
                }
            }
  
        }
        function calculate_Total() {
            try {
                
                var total = 0;
                var lastRow = $('#tbl_Local tr').length;

                for (var q = 0; q < lastRow - 1; q++) {
                    //var vtype = $("#ddlVehicleType" + (q + 1)).val();
                    //if (vtype == "Other") {
                        var amount = 0;
                        if ($("#txt_amount" + (q + 1)).val() != "" && $("#txt_amount" + (q + 1)).val() != undefined) {
                            amount = parseFloat($("#txt_amount" + (q + 1)).val());
                            $("#txt_amount" + (q + 1)).val(amount);
                        }
                        else {
                            $("#txt_amount" + (q + 1)).val(0);
                        }
                        total = total + amount;
                    }
                //}
                $("#spn_Total").html(total);
             
            }
        
            catch (ex)
            { }       

        }
     </script>
</body>
</html>

