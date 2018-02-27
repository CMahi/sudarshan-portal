<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Car_Expense_document_verification.aspx.cs" Inherits="Car_Expense_document_verification" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Car Expense</title>
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
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>CAR EXPENSE DOCUMENT VERIFICATION APPROVAL</h3>

                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Mode </label>
                                <div class="col-md-3">
                                    <div id="Div7">
                                        <span id="span_paymode" runat="server"></span>
                                    </div>

                                </div>
                                <div id="div_loc" runat="server">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Payment Location</label>
                                    <div class="col-md-3">
                                        <div id="Div11">
                                            <span id="span_payloc" runat="server"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Expense Amount</label>
                                <div class="col-md-3">
                                    <div id="Div8">
                                        <span id="expnsamt" runat="server"></span>
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
            <div class="col-md-12" id="tab_CarExpense">
                <div class="panel" style="background-color: #717B85;">
                    <ul class="nav nav-tabs">

                        <li id="Li7" class="active"><a aria-expanded="true" href="#tab_fuel" data-toggle="tab"><b>Fuel</b></a></li>
                        <li id="Li8"><a aria-expanded="true" href="#tab_maitaince" data-toggle="tab"><b>Maintenance</b></a></li>
                        <li id="Li9"><a aria-expanded="true" href="#tab_driver" data-toggle="tab"><b>Driver</b></a></li>
                        <li id="Li16"><a aria-expanded="true" href="#tab_expense" onclick="getexpensedtl()" data-toggle="tab"><b>Expense</b></a></li>

                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="tab_fuel">
                            <div class="table-responsive">
                                <table id="tbl_Fuel" class="table table-striped table-bordered">
                                    <tr>
                                        <td>
                                            <div id="divfuel" runat="server"></div>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="tab_maitaince">
                            <div class="table-responsive">

                                <table id="tbl_Maintenance" class="table table-striped table-bordered">
                                    <tr>
                                        <td>
                                            <div id="divmaintenance" runat="server"></div>
                                        </td>
                                    </tr>

                                </table>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="tab_driver">
                            <div class="table-responsive">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                    <table id="tbl_Driver" class="table table-striped table-bordered">
                                        <tr>
                                            <td>
                                                <div id="div_driver" runat="server"></div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="tab_expense">
                            <div class="table-responsive">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                    <table id="tbl_expns" class="table table-striped table-bordered">
                                        <thead>
                                            <tr>
                                                <th style="width: 5%">#</th>
                                                <th style="width: 20%">Date</th>
                                                <th style="width: 20%">Expense Head</th>
                                                <th style="width: 20%">Amount</th>
                                            </tr>
                                        </thead>
                                    </table>
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
                                    <asp:Button ID="btnRequest" runat="server" class="btn btn-danger  btn-rounded" Text="Submit" OnClick="btnApprove_Click" OnClientClick="return prepareData()" />
                                    <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded" Text="Cancel" OnClick="btnClose_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12" style="margin-top: 10px;" id="div1">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">Audittrail</h4>
                    </div>
                    <div class="panel-body">
                        <div id="Div_Audit_Details" runat="server">
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

        <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="json_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_designation" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Step" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_xml_data_driver" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_sub_xml_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtexpnsamt" runat="server"></asp:TextBox>
            <asp:TextBox ID="Txt_File_xml" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Condition" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Remark" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtexpensno" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Initiator" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="doa_user" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
            <asp:TextBox ID="doa_email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
        </div>
        <!-- ================== BEGIN BASE JS ================== -->
        <!--<script src="../../assets/plugins/jquery/jquery-1.9.1.min.js"></script>-->
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script src="../../JS/Car_Expense_Approval.js"></script>


    </form>


</body>
</html>
