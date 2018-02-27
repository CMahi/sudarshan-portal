<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Local_Conveyance_Modification.aspx.cs" Inherits="Local_Conveyance_Modification" %>

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
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>
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
                                <label class="col-md-2">Request No</label>
                                <div class="col-md-3">
                                    <div id="Div12">
                                        <span id="spn_req_no" runat="server"></span>
                                    </div>
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
                                    <div id="Div1"><span id="span_cc" runat="server"></span></div>
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
                                    <div id="Div4"><span id="span1" runat="server"></span></div>
                                </div>
                                
                                <label class="col-md-2">Mobile No.</label>
                                <div class="col-md-3">
                                    <asp:Label ID="lbl_MobileNo" runat="server"></asp:Label>
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
                                        <div id="Div7"><span id="span_Ifsc" runat="server">NA</span></div>
                                    </div>
                                        </div>
                              
                            <div class="form-group">
                                  <div class="col-md-1"></div>
                                 <label class="col-md-2">Approver </label>
                                <div class="col-md-3">
                                    <div id="Div5"><span id="span_Approver" runat="server" style="display: none"></span><span id="span_app_name" runat="server"></span></div>
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
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>LOCAL CONVEYANCE MODIFICATION</h3>
                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">
                           

                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Mode </label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddl_Payment_Mode" runat="server" class="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Location</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlAdv_Location" runat="server" class="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Narration </label>
                                <div class="col-md-2">
                                    <div id="Remark">
                                        <textarea id="txt_remark" class="form-control" rows="1" maxlength="200" runat="server"></textarea>
                                    </div>
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
            <div class="col-md-12" id="advance_details">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <b><span id="spn_hdr" runat="server">Recovery Amount : </span></b>
                            <b><span id="spn_val" runat="server">0</span></b>
                        </div>
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
                            <label>Total Amount :</label> <span id="spn_Total" runat="server">0</span> </div>
                            <div class="panel-heading-btn">
                            <a href="#div_Policy" data-toggle="modal">View Policy</a>
                        </div>
                        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Local Conveyance</h4>
                    </div>
                    <div class="panel-body">
                        <p style="color: red">Note : <i class="fa fa-fw fa-angle-double-right"></i><i class="fa fa-fw fa-angle-double-right"></i>Kindly mention mode of travel in case of "Other"</p>
                        <div class="table-responsive">
                        <div id="div_LocalData" runat="server"></div>
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
                                    <asp:Button ID="btn_Save" runat="server" class="btn btn-danger btn-rounded" Text="Update"
                                        OnClientClick="return createxml();" OnClick="btnRequest_Click" />

                                    <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded" Text="Cancel" OnClick="imgBtnRelease_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--OnClientClick="javascript:return TestCheckBox();javascript:return confirmdelete()"--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12" style="margin-top: 10px;" id="div6">
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
                                        <div id="divalldata" runat="server"></div>
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
            <asp:TextBox ID="txt_Action" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_vehitype" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_advance_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_vehitype1" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_xml_data_vehicle" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Audit" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_twowhamt" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_fourwhamt" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_adcount" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_advance_amount" runat="server" Text="0"></asp:TextBox>
        </div>

        <!-- ================== BEGIN BASE JS ================== -->

        <script src="../../assets/plugins/jquery/jquery-2.1.4.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script type="text/javascript" src="../../JS/Local_Conveyance_Modification.js"></script>
         <script src="../../JS/validator.js"></script>
        <script src="../../assets/js/Vaildation.js"></script>
    </form>
    <script type="text/javascript">
        var str = "";
        $(document).ready(function () {
            var mode = $("#ddl_Payment_Mode").val();
            if (mode == 2) {
                $("#ddlAdv_Location").hide();
            }
            else {
                $("#ddlAdv_Location").show();
            }
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' });
            if ($("#span_bank_no").html() == "NA" || $("#span_bank_no").html() == "") {
                alert("Bank Account Is Not Available! Unable To Claim Expense Request...!");
                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            }
            else {
                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
            }
            if ($("#txt_adcount").val() == "0") {
                $("#advance_details").hide();
            }
            else {
                $("#advance_details").show();
            }
        });
   
        /***************** Company Provided Mobile**********************/
        $('body').on('click', '.add_Local', function () {
            addNewRowLocal();
        });
        $('#txt_fdate1').datepicker({ format: 'dd-M-yy', autoclose: true, endDate: new Date(), todayBtn: 'linked' }).on('changeDate', function (ev) {
            daydiffrence(1);
        });
        $('#txt_tdate1').datepicker({ format: 'dd-M-yy', autoclose: true, endDate: new Date(), todayBtn: 'linked' }).on('changeDate', function (ev) {
            daydiffrence(1);
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
            for (var q = 1; q <= lastRow1; q++) {
                var vtype = document.getElementById("ddlVehicleType" + q).value;
                var from = document.getElementById("txt_fromloc" + q).value;
                var to = document.getElementById("txt_toloc" + q).value;
                var fdate = document.getElementById("txt_fdate" + q).value;
                var tdate = document.getElementById("txt_tdate" + q).value;
                var kms = document.getElementById("txt_kms" + q).value;
                var mbillamt = document.getElementById("txt_amount" + q).value;

                if (vtype == 0) {
                    alert('Please Select Vehicle Type at row :' + q + '');
                    return false;
                }
                if (from == "") {
                    alert('Please Enter From Location at row :' + q + '');
                    return false;
                }
                if (to == "") {
                    alert('Please Select To Location at row :' + q + '');
                    return false;
                }
                if (vtype != "Other") {
                    if (kms == "") {
                        alert('Please Enter KMS at row :' + q + '');
                        return false;
                    }
                }
                if (fdate == 0) {
                    alert('Please Select From Date at row :' + q + '');
                    return false;
                }
                if (tdate == 0) {
                    alert('Please Select To Date at row :' + q + '');
                    return false;
                }
                if (mbillamt == "") {
                    alert('Please Enter Amount at row :' + q + '');
                    return false;
                }

            }
            var html2 = "<tr><td>" + (lastRow1 + 1) + "</td>";
            var html3 = "<td><select id='ddlVehicleType" + (lastRow1 + 1) + "'  class='form-control input-sm' onchange='valuetypeamt(" + (lastRow1 + 1) + ")'>" + ddlvehitype + "</select></td>";
            var html4 = "<td><input class='form-control input-sm' type='text' id='txt_fromloc" + (lastRow1 + 1) + "'></input></td>";
            var html5 = "<td><input class='form-control input-sm' type='text' id='txt_toloc" + (lastRow1 + 1) + "'></input></td>";
            var html6 = "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown' id='txt_fdate" + (lastRow1 + 1) + "' readonly  onchange='valuechanamt(" + (lastRow1 + 1) + ");'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html7 = "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown' id='txt_tdate" + (lastRow1 + 1) + "' readonly  onchange='valuechanamt(" + (lastRow1 + 1) + ");'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html8 = "<td><input class='form-control input-sm' type='text' id='txt_kms" + (lastRow1 + 1) + "' onkeypress='return isNumberKey(event)' onchange='valuechanamt(" + (lastRow1 + 1) + ");'  ></input></td>";
            var html9 = "<td><input class='form-control input-sm' type='text' id='txt_amount" + (lastRow1 + 1) + "' onkeypress='return isNumberKey(event)' onchange='calculate_Total()'></input></td>";
            var html10 = "<td class='add_Local' id='add_Row" + lastRow + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
            var html11 = "<td id='delete_Row" + lastRow + "' ><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_Row(" + lastRow + ")'></i></td></tr>";
            var htmlcontent = $(html2 + "" + html3 + "" + html4 + "" + html5 + "" + html6 + "" + html7 + "" + html8 + "" + html9 + "" + html10 + "" + html11);
            
            $('#tbl_Local').append(htmlcontent);
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, endDate: new Date(), todayBtn: 'linked' });
            $('#txt_fdate' + (lastRow1 + 1)).datepicker({ format: 'dd-M-yy', autoclose: true, endDate: new Date(), todayBtn: 'linked' }).on('changeDate', function (ev) {
                daydiffrence(lastRow1 + 1);
            });
            $('#txt_tdate' + (lastRow1 + 1)).datepicker({ format: 'dd-M-yy', autoclose: true, endDate: new Date(), todayBtn: 'linked' }).on('changeDate', function (ev) {
                daydiffrence(lastRow1 + 1);
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
        $("#ddl_Payment_Mode").change(function () {
            var mode = $("#ddl_Payment_Mode").val();
            if (mode == 2) {
                $("#ddlAdv_Location").hide();
            }
            else {
                $("#ddlAdv_Location").show();
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
            if (vtype == "Other") {
                $('#txt_amount' + index).val("");
                $('#txt_amount' + index).attr('readonly', false);
            }
            if (fdate != "" && tdate != "" && mkms != "" && vtype != "Other") {
                Local_Conveyance_Modification.fillAmount(fdate, tdate, index, callback_local_Detail);
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
                Local_Conveyance_Modification.fillAmount(fdate, tdate, index, callback_local_Detail);
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
                    Local_Conveyance_Modification.fillAmount(fdate, tdate, index, callback_local_Detail);
                }
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
                var mkms = $('#txt_kms' + arr[0]).val();
                $('#txt_amount' + arr[0]).attr('readonly', true);
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
                            amount =Math.round((parseFloat($("#txt_amount" + (q + 1)).val())));
                            $("#txt_amount" + (q + 1)).val(amount);
                        }
                        else {
                            $("#txt_amount" + (q + 1)).val(0);
                        }
                        total = total + amount;
                    }
                    $("#spn_Total").html(total);
                    if ($('#txt_amount' + arr[0]).val() == 'NaN') {
                        $('#txt_amount').val("");
                        $('input[Id="btn_Save"]').attr('disabled', 'disabled');
                        alert("Expense cannot be submitted as policy rate is not defined!");
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
        function calculate_Total() {
            try {

                var total = 0;
                var lastRow = $('#tbl_Local tr').length;

                for (var q = 0; q < lastRow - 1; q++) {
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

