<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Advance_Return.aspx.cs" Inherits="Advance_Return" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Advance Return Request</title>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />

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
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>RETURN ADVANCE - REQUEST</h3>

                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">

                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Mode </label>
                                <div class="col-md-2">
                                    <span id="ddl_Payment_Mode" runat="server"></span>
                                </div>
                                <div class="col-md-1"></div>
                                <div id="div_payment" runat="server">
                                    <label class="col-md-2">Payment Location</label>
                                    <div class="col-md-2">
                                        <span id="ddlAdv_Location" runat="server"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Advance For </label>
                                <div class="col-md-2">
                                    <div id="Div8">
                                        <span id="span_advance" runat="server"></span>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" id="div_Local">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <div class="panel-heading-btn" id="divpol" style="display: none">
                            <h4 class="panel-title"><a href="#div_policy" data-toggle="modal">View Policy</a></h4>
                        </div>
                        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Request Details</h4>
                    </div>
                
                <div class="panel-body" id="div7">
                    <div class="form-horizantal table-responsive" style="width: 100%" id="div_LocalData" runat="server">
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
                                <label class="col-md-2">Return Location</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlReturnLocation" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <div id="div1" runat="server">
                                    <label class="col-md-1">Remark</label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="return_remark" runat="server" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2"></div>
                                <div class="col-md-8" id="tab_btn3" runat="server" style="text-align: center;">
                                    <asp:Button ID="btnRequest" runat="server" class="btn btn-danger  btn-rounded" Text="Submit" OnClientClick="return check_Location()" OnClick="btnRequest_Click" />
                                    <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded" Text="Cancel" OnClick="btnCancel_Click"/>
                                </div>
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


        <div style="display: none;" class="modal" id="div_policy" runat="server">
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
                                        <div id="div11" runat="server"></div>
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

        <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="stepname" runat="server"></asp:TextBox>
            <asp:TextBox ID="stepid" runat="server"></asp:TextBox>
            <asp:TextBox ID="req_no" runat="server"></asp:TextBox>
            <asp:TextBox ID="doa_user" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="doa_email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_adv_type" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtDummy" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Initiator" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_designation" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="pmode" runat="server"></asp:TextBox>
            <asp:TextBox ID="plocation" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtApproverEmail" runat="server"></asp:TextBox>
            <asp:TextBox ID="pre_pro" runat="server" Text="1"></asp:TextBox>
           <asp:TextBox ID="pre_ins" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_Deviate" runat="server" Text=""></asp:TextBox>
            <asp:TextBox ID="chk_records" runat="server"></asp:TextBox>
            <asp:TextBox ID="rec_data" runat="server"></asp:TextBox>
        </div>
        <div id="divins" runat="server" style="display:none">
            <div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent.gif" style="background-color:transparent;position:fixed; top:40%; left:46%;"/></div>
        </div>

        <!-- ================== BEGIN BASE JS ================== -->

        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>

        <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
        <script src="../../assets/js/demo.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>

             <script>
                 $(document).ready(function () {
                     App.init();
                     Demo.init();
                     PageDemo.init();
                 });

                 function check_Location()
                 {
                     if ($("#ddlReturnLocation option:selected").index() < 1) {
                         alert("Please Select Advance Return Location");
                         return false;
                     }
                     else {
                         if ($("#return_remark").val() == "") {
                             alert("Please Enter Advance Return Remark");
                             return false;
                         }
                         else
                             {
                             if (confirm("Are You Sure To Return This Advance?")) {
                                 $("#divins").show();
                                 return true;
                             }
                             else {
                                 return false;
                             }
                         }
                     }
                 }
            </script>
    </form>
</body>
</html>
