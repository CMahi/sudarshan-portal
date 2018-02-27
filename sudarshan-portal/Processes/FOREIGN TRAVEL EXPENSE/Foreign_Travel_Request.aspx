<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Foreign_Travel_Request.aspx.cs" Inherits="Foreign_Travel_Request" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Foreign Travel Request</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
    <%--<link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />--%>
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
                                    <div class="col-md-12">
                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">Employee No</label>
                                        <div class="col-md-3">
                                            <div id="Div3"><span id="empno" runat="server"></span></div>
                                        </div>
                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">Employee Name</label>
                                        <div class="col-md-3">
                                            <div id="EmployeeName"><span id="span_ename" runat="server"></span></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">Designation</label>
                                        <div class="col-md-3">
                                            <div id="Div2"><span id="span_designation" runat="server"></span></div>
                                        </div>

                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">Division</label>
                                        <div class="col-md-3">
                                            <div id="Div9"><span id="span_Division" runat="server"></span></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">Department</label>
                                        <div class="col-md-3">
                                            <div id="EmployeeDepartment"><span id="span_dept" runat="server"></span></div>
                                        </div>
                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">Grade</label>
                                        <div class="col-md-3">
                                            <div id="grade"><span id="span_grade" runat="server"></span></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">Cost Center</label>
                                        <div class="col-md-3">
                                            <div id="Div1"><span id="span_cc" runat="server"></span></div>
                                        </div>
                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">Mobile No.</label>
                                        <div class="col-md-3">
                                            <div id="EmployeePhoneNo"><span id="span_mobile" runat="server"></span></div>
                                        </div>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">

                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">Bank Account No.</label>
                                        <div class="col-md-3">
                                            <div id="Div7"><span id="span_bank_no" runat="server"></span><span id="ddlTravel_Type" runat="server" style="display: none">Domestic</span></div>
                                        </div>
                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">IFSC No.</label>
                                        <div class="col-md-3">
                                            <div id="Div4"><span id="span_Ifsc" runat="server">NA</span></div>
                                        </div>
                                    </div>
                                </div>
				
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">Approver </label>
                                        <div class="col-md-3">
                                            <div id="Div5"><span id="span_Approver" runat="server" style="display: none"></span><span id="span_app_name" runat="server"></span></div>
                                        </div>
                                        <div class="col-md-1"></div>
                                        <label class="col-md-2">Deviation Approver </label>
                                        <div class="col-md-3">
                                            <div id="Div6"><span id="span_DApprover" runat="server" style="display: none"></span><span id="span_Dapp_name" runat="server"></span></div>
                                        </div>

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
                        <h3 class="panel-title">FOREIGN TRAVEL EXPENSE - REQUEST</h3>

                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Travel From Date<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-2">
                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="travelFromDate" runat="server" class="form-control datepicker-rtl" BackColor="#ffffff"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-grey" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Travel To Date<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-2">
                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="travelToDate" runat="server" class="form-control datepicker-rtl" BackColor="#ffffff"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-grey" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>

                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Region<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control input-sm" onchange="SelectData();"></asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Supporting Documents<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <a href="#div_UploadDocument" data-toggle="modal">
                                        <img id="A_FileUpload1" src="../../images/attachment.png" alt="Click here to attach file." height="27px" width="27px"></a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Remark<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="req_remark" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Enter Remark"></asp:TextBox>
                                </div>
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
                                    <label class="col-md-2">File Type</label>
                                    <div class="col-md-6 ">
                                    <asp:DropDownList ID="doctype" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
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
                                            <div id="div_docs" runat="server"></div>
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
                            <span id="Final_Amtt" style="display: none">0 <i class='fa fa-rupee'></i></span>
                            <a href="#modal_policy_details" onclick="show_record();" role="button" data-toggle="modal" title="Policy Details" style="color: blue"><i class="fa fa-fw m-r-10 pull-left f-s-15 fa-automobile"></i></a>
                        </div>
                        <h3 class="panel-title">Request Details</h3>
                    </div>
                    <div class="panel-body">                       
                        <div class="panel-group" id="accordion">
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
                        <div class="col-md-5"></div>
                        <div class="col-md-6">
                            <asp:Button ID="btnSubmit" runat="server" Text="Proceed" class="btn btn-grey btn-rounded" OnClientClick="return prepare_data()" Width="98px" />
                            <asp:Button ID="btn_SaveDraft" runat="server" Text="Save As Draft" class="btn btn-grey btn-rounded" OnClientClick="return Save_As_Draft()" OnClick="btnSaveasDraft_Click" />
                            <asp:Button ID="btn_Cancel" runat="server" CssClass="btn btn-danger btn-rounded" Text="Cancel" />
                        </div>
                    </div>
                </div>
            </div>


            <div class="modal fade" id="Return_Money_Div">
                <div id="divIns" runat="server" style="display: none">
                    <div style="background-color: #E6E6E6; position: absolute; top: 0; left: 0; width: 100%; height: 300%; z-index: 1001; -moz-opacity: 0.8; opacity: .80; filter: alpha(opacity=80);">
                        <img src="../../images/loading_transparent.gif" style="background-color: transparent; position: fixed; top: 40%; left: 46%;" />
                    </div>
                </div>
                <div class="modal-dialog" style="width: 90%; margin-left: 5%">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title"><font color="white">Request Summary</font></h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-1"></div>
                                            <label class="col-md-2">Currency</label>
                                            <div class="col-md-3">
                                                <div id="Div10"><span id="S_Currency" runat="server"></span></div>
                                            </div>
                                            <div class="col-md-1"></div>
                                            <label class="col-md-2">Advance</label>
                                            <div class="col-md-3">
                                                <div id="Div11"><span id="S_Advance" runat="server"></span></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-1"></div>
                                            <label class="col-md-2">Expense</label>
                                            <div class="col-md-3">
                                                <div id="Div12"><span id="S_Expense" runat="server"></span></div>
                                            </div>

                                            <div class="col-md-1"></div>
                                            <label class="col-md-2">Difference</label>
                                            <div class="col-md-3">
                                                <div id="Div13"><span id="S_Diff" runat="server"></span></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="Money_Div" style="display: none">
                                        <div class="col-md-12">
                                            <div class="col-md-1"></div>
                                            <label class="col-md-2">Return Money(In equivalent base currency)<font color='#ff0000'><b>*</b></font></label>
                                            <div class="col-md-3">
                                                <div id="Div14">
                                                    <input type="text" id="txt_Return_Money" class='form-control input-sm numbersOnly' runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btn_Save" runat="server" Text="Submit" class="btn btn-grey btn-rounded" OnClick="btnSubmit_Click" />
                            <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                        </div>
                    </div>
                </div>
            </div>
            <%--==============================================================================================================--%>
        </div>
        <div class="modal fade" id="modal_policy_details">
            <div class="modal-dialog" style="width: 75%; margin-left: 10%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Travel Policy Details >> <asp:Label ID="Desg_Name" runat="server" Text=""></asp:Label></font></h4>
                    </div>

                    <div class="modal-body">
                        <div class="row" id="div_header" runat="server" style="height: 370px; overflow: auto">
                        </div>

                    </div>

                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>

        <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="json_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_designation" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_xml_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_sub_xml_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
            <asp:TextBox ID="next_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="step_name" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_advance_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="pageno" runat="server"></asp:TextBox>
            <asp:TextBox ID="pre_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="pre_data2" runat="server"></asp:TextBox>
            <asp:TextBox ID="exch_rate" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_Do_Limit" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="dev_travel_class" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="dev_policy_amt" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="dev_supp_amt" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_amt_no_db" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_amt_no_cur" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_amt_tot" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_perc_no" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_DO_Status" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_Class" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_Doc_Verifacation_status" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_Supporting_Limit" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_Amount" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_Deviation_Reason" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="check_status" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_app_mail" runat="server" Text=""></asp:TextBox>
            <asp:TextBox ID="Non_supp_Yes_no" runat="server" Text=""></asp:TextBox>
            <asp:TextBox ID="txt_LocTrav_XML" runat="server" Text=""></asp:TextBox>
            <asp:TextBox ID="txt_Supp_Counter" runat="server" Text=""></asp:TextBox>
        </div>

        <div class="modal fade" id="myModal" style="margin-top: 10%">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="alert alert-warning">
                            Do you want Deviation Approval?
                        </div>
                    </div>
                    <div class="modal-footer ">
                        <button type="button" class="btn btn-warning" data-dismiss="modal" aria-hidden="true" onclick="Flag_Status('Yes')">
                            <span class="glyphicon glyphicon"></span>Yes</button>
                        <button type="button" class="btn btn-warning" data-dismiss="modal" aria-hidden="true" onclick="Flag_Status('No');">
                            <span class="glyphicon glyphicon-remove"></span>No</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>




        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>

      <%--  <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
        <script src="../../assets/js/demo.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>--%>


        <script src="../../JS/Foreign_Travel_Request.js"></script>
    </form>
    <script>
        $(document).ready(function () {
            //App.init();
            //Demo.init();
            //PageDemo.init();
        });
    </script>

</body>

</html>
