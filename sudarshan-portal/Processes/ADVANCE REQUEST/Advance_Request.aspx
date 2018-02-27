<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Advance_Request.aspx.cs" Inherits="Advance_Request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Advance Request</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker3.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />

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
                                        <div id="Div5"><span id="span_Ifsc" runat="server">NA</span></div>
                                    </div>
                                </div>
 <div class="form-group">
                                    <div class="col-md-12">
                                   
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Base Location</label>
                                    <div class="col-md-3">
                                        <div id="Div8"><span id="spn_base_location" runat="server">NA</span></div>
                                    </div>
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
                            <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color: blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                        </div>
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>Employee Details</h3>
                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">


                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Mode</label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlPayMode" runat="server" class="form-control  input-sm width-xs" onchange="enable_disable()">
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
                                <label class="col-md-2">Supporting Documents</label>
                                <div class="col-md-3">
                                    <a href="#div_UploadDocument" data-toggle="modal">
                                        <img id="Img1" src="../../images/attachment.png" alt="Click here to attach file." height="20" width="20" /></a>
                                </div>
                                
                                <div class="col-md-1"></div>
                                <div id="div2">
                                    <label class="col-md-2">Advance For</label>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddladvancetype" runat="server" class="form-control  input-sm width-xs">
                                        </asp:DropDownList>
                                    </div>
                                   
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" id="advance_travel" runat="server" style="display: none">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <h4 class="panel-title"><a href="#div_Policy" data-toggle="modal">View Policy</a></h4>
                        </div>

                        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-align-justify"></i>Advance Request For Domestic Travel</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal">

                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Place From</label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlFromPlace" runat="server" class="form-control  input-sm width-xs" onchange="chk_class_From(1)">
                                    </asp:DropDownList>
                                    <div id="fother">
                                        <input type='text' class="form-control" id="txt_fplace" runat="server" />
                                    </div>
                                </div>
                                <label class="col-md-2">Place To</label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlToPlace" runat="server" class="form-control  input-sm width-xs" onchange="chk_class_To(1)">
                                    </asp:DropDownList>
                                    <div id="tother">
                                        <input type='text' class="form-control" id="txt_tplace" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">From Date</label>
                                <div class="col-md-3">
                                    <div class="input-group" id="Div1">
                                        <input type='text' class="form-control datepicker-dropdown" id="txt_fdate" runat="server" readonly="" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                                <label class="col-md-2">To Date</label>
                                <div class="col-md-3">
                                    <div class="input-group" id="Div6">
                                        <input type='text' class="form-control datepicker-dropdown" id="txt_tdate" runat="server" readonly="" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Allowed Amount</label>
                                <div class="col-md-3" style="text-align: right">
                                    <asp:Label ID="lblallowedamt" runat="server"></asp:Label>

                                </div>

                                <label class="col-md-2">Amount</label>
                                <div class="col-md-3" style="text-align: right">
                                    <input type='text' class="form-control" id="txt_amount" runat="server" onkeypress="return isNumberKey(event)" onchange="checkamount();" />
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Remark</label>
                                <div class="col-md-8">
                                    <textarea type='text' maxlength="200" class="form-control" rows="1" id="txt_remark" runat="server"></textarea>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12" id="advance_other" runat="server" style="display: none">
            <div class="panel panel-grey">
                <div class="panel-heading">
                    <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-align-justify"></i>Advance Request For Other Expense</h4>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-1"></div>
                            <label class="col-md-2">Advance Date</label>
                            <div class="col-md-3">
                                <div class="input-group" id="Div4">
                                    <input type='text' class="form-control datepicker-dropdown" id="txt_advance_date" runat="server" readonly="" />
                                    <span class="input-group-btn">
                                        <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                    </span>
                                </div>
                            </div>
                            <label class="col-md-2">Amount</label>
                            <div class="col-md-3">
                                <input type='text' class="form-control" id="txtx_other_amount" runat="server" onkeypress="return isNumberKey(event)" onchange="checkamount();" />

                            </div>

                            <div class="col-md-1"></div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-1"></div>
                            <label class="col-md-2">Remark</label>
                            <div class="col-md-8">
                                <textarea type='text' maxlength="200" class="form-control" rows="1" style="width: 35%" id="txt_other_remark" runat="server"></textarea>
                            </div>

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
                    <div class="col-md-5"></div>
                    <div class="col-md-6">

                     <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                                <asp:Button ID="btn_Save" runat="server" class="btn btn-danger btn-rounded" Text="Submit"
                                    OnClientClick="return createxml();" OnClick="btnRequest_Click" />

                                <asp:Button ID="btn_cancel" runat="server" class="btn btn-danger btn-rounded" Text="Cancel" OnClick="imgBtnRelease_Click" />
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
        </div>
       <%-- <div class="col-md-12" id="div_details">
            <div class="panel panel-grey">
                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-align-justify"></i>Advance Details</h3>
                </div>
                <div class="panel-body" id="div3">
                    <div class="panel pagination-danger">
                        <div class="table-responsive" style="width: 100%" id="divalldata" runat="server">
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>

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
                    <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Condition" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtApproverEmail" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Action" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Audit" runat="server"></asp:TextBox>
                    <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_approvar" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_xml_data_vehicle" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_adamount" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_adperiod" runat="server"></asp:TextBox>
                    <asp:TextBox ID="total_amount" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_opencount" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_opcount" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_adcount" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_expire" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_policycnt" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_confirm" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_pkexpenseid" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_deviate" runat="server"></asp:TextBox>
                     <asp:TextBox ID="txt_domeopen" runat="server"></asp:TextBox>
                     <asp:TextBox ID="txt_otheropen" runat="server"></asp:TextBox>
                      <asp:TextBox ID="txt_othperiod" runat="server"></asp:TextBox>
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
        <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
        <script src="../../assets/js/demo.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../JS/Advance_Request.js"></script>
        <script src="../../assets/js/Vaildation.js"></script>
    </form>

</body>
</html>

