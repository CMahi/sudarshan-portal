<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Domestic_Travel_Request_Approval.aspx.cs" Inherits="Domestic_Travel_Request_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Domestic Travel Request</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
    <style type="text/css">
        .sample {
            background-color: #DC5807;
            border: 1px solid black;
            border-collapse: collapse;
            color: White;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        
        <div id="divIns" runat="server"></div>

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
                                        <div id="Div13"><span id="span_Ifsc" runat="server">NA</span></div>
                                    </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Base Location</label>
                                    <div class="col-md-3">
                                        <div id="Div15"><span id="spn_base_location" runat="server">NA</span></div>
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
                                            <div id="Div14"><span id="span_DApprover" runat="server" style="display: none"></span><span id="span_Dapp_name" runat="server"></span></div>
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
                            <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                        </div>
                        <h3 class="panel-title">DOMESTIC TRAVEL EXPENSE - APPROVAL</h3>
                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Request No</label>
                                <div class="col-md-3">
                                    <div id="Div4"><span id="spn_req_no" runat="server"></span></div>
                                </div>
                                <label class="col-md-2">Date</label>
                                <div class="col-md-3">
                                    <div id="Div6"><span id="spn_date" runat="server"></span></div>
                                </div>
                                <div class="col-md-1"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Mode</label>
                                <div class="col-md-2">
                                    <div id="Div10"><span id="pay_mode" runat="server"></span></div>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Location</label>
                                <div class="col-md-2">
                                    <div id="Div12"><span id="pay_location" runat="server"></span></div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Travel From Date<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-2">
                                    <div id="Div8"><span id="travelFromDate" runat="server"></span></div>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Travel To Date<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-2">
                                    <div id="Div11"><span id="travelToDate" runat="server"></span></div>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Remark<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-2">
                                    <span id="req_remark" runat="server"></span>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Supporting Documents</label>
                                <div class="col-md-3">
                                    <a href="#div_UploadDocument" data-toggle="modal">
                                        <img id="A_FileUpload1" src="../../images/attachment.png" alt="Click here to attach file." height="27px" width="27px"></a>
                                </div>
                            </div>
                               <div class="form-group" id="div_deviated" runat="server" style="display:none">
                                  
                                       <div class="col-md-1"></div>
                                       <div class="col-md-10">
                                         Deviated Reason : <span id="span_deviate" runat="server"></span>
                                       </div>
                                       <div class="col-md-1"></div>
                                
                                   </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="div_UploadDocument">
                <div class="modal-dialog" style="width: 45%; margin-left: 25%">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title"><font color="white"><b>File Attachment </b> </font></h4>
                        </div>

                        <div class="modal-body">
                            <div class="table-responsive" id="div_docs" runat="server">
                            </div>
                        </div>

                        <div class="modal-footer">
                            <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
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
                            <a href="#modal_policy_details" onclick="show_record();" role="button" data-toggle="modal" title="Policy Details" style="color: blue"><i class="fa fa-fw m-r-10 pull-left f-s-15 fa-automobile"></i></a>
                        </div>
                        <h3 class="panel-title">Request Details</h3>
                    </div>
                    <div class="panel-body">
                        <%--<p style="color: red">Note : <i class="fa fa-fw fa-angle-double-right"></i><i class="fa fa-fw fa-angle-double-right"></i>Y (Mandatory - Supporting Documents), N (Not Mandatory - Supporting Documents)</p>--%>
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
                        <div class="row">
                            <div class="col-md-2"></div>
                            <div class="col-md-2" style="text-align: right"><b>Action : </b></div>
                            <div class="col-md-2" style="text-align: left">
                                <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control input-sm" onchange="hide_show_remark()">
                                    <asp:ListItem Value="0">---Select One---</asp:ListItem>
                                    <asp:ListItem Value="1">Approve</asp:ListItem>
                                    <asp:ListItem Value="2">Send-Back</asp:ListItem>
                                    <%--<asp:ListItem Value="3">Reject</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6" id="div_remark" style="display: none">
                                <asp:TextBox ID="txt_Remark" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" placeholder="Enter Remark"></asp:TextBox>
                            </div>

                        </div>
                         <br />
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-3" style="text-align: left">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-grey btn-rounded" OnClientClick="return prepare_data()" OnClick="btnSubmit_Click" /><%----%>
                                <asp:Button ID="btn_Cancel" runat="server" CssClass="btn btn-danger btn-rounded" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                            <div class="col-md-5"></div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h3 class="panel-title">Audit Trail</h3>
                    </div>
                    <div class="panel-body">
                        <div id="div_audit" runat="server"></div>
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
                        <div class="row">
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlRecords" runat="server" class="form-control" onchange="show_record();">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2" style="text-align: left">
                                Records per page
                            </div>
                            <div class="col-md-4" style="text-align: right"></div>

                            <div class="col-md-1"></div>
                            <div class="col-md-1" style="text-align: right"><b>Search : </b></div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txt_search" runat="server" class="form-control" onkeyup="show_record();"></asp:TextBox>
                            </div>
                        </div>
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
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_advance_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="pageno" runat="server"></asp:TextBox>
            <asp:TextBox ID="pre_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="pre_data2" runat="server"></asp:TextBox>

            <asp:TextBox ID="dev_travel_class" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="dev_policy_amt" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="dev_supp_amt" runat="server" Text="0"></asp:TextBox>

            <asp:TextBox ID="supp_amt_no_db" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_amt_no_cur" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_amt_tot" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_perc_no" runat="server" Text="0"></asp:TextBox>

            <asp:TextBox ID="txt_initiator" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_init_mail" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Step" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="pk_mode_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="pk_loc_id" runat="server"></asp:TextBox>

            <asp:TextBox ID="doa_user" runat="server"></asp:TextBox>
            <asp:TextBox ID="doa_email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtApproverEmail" runat="server"></asp:TextBox>
        </div>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script src="../../JS/Domestic_Travel_Request_Approval.js"></script>
    </form>
</body>
</html>
