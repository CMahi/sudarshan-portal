<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Advance_Return_Foreign_Approval.aspx.cs" Inherits="Advance_Return_Foreign_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Advance Request</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />

            <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />
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
                                    <label class="col-md-2">Advance Request No</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="spn_req_no" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Advance Date</label>
                                    <div class="col-md-3">
                                        <asp:Label ID="req_date" runat="server"></asp:Label>
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
                                        <div id="Div5"><span id="span_Ifsc" runat="server">NA</span></div>
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

        <div class="row" id="advance_Foreign" runat="server">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <h4 class="panel-title"><a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color: blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a></h4>
                        <%-- <h4 class="panel-title"><a href="#div_Policy" data-toggle="modal">View Policy</a></h4>--%>
                    </div>

                    <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-align-justify"></i>FOREIGN ADVANCE RETURN - APPROVAL</h4>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <div class="col-md-1"></div>
                            <label class="col-md-2">Request No</label>
                            <div class="col-md-3">
                                <span id="spn_ret_no" runat="server"></span>
                            </div>

                            <label class="col-md-2">Date</label>
                            <div class="col-md-2">
                                <span id="spn_ret_date" runat="server"></span>
                            </div>
                            <div class="col-md-1"></div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-1"></div>
                            <label class="col-md-2">Region From</label>
                            <div class="col-md-3">
                                <span id="spn_F_Region" runat="server"></span>
                            </div>

                            <label class="col-md-2">Region To</label>
                            <div class="col-md-2">
                                <span id="spn_T_Region" runat="server"></span>
                            </div>
                            <div class="col-md-1"></div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-1"></div>
                            <label class="col-md-2">City From</label>
                            <div class="col-md-3">
                                <span id="spn_F_City" runat="server"></span>
                            </div>
                            <label class="col-md-2">City To</label>
                            <div class="col-md-3">
                                <span id="spn_T_City" runat="server"></span>
                            </div>
                            <div class="col-md-1"></div>
                        </div>

                        <div class="form-group">
                        </div>
                        <div class="form-group">
                            <div class="col-md-1"></div>
                            <label class="col-md-2">From Date</label>
                            <div class="col-md-2">
                                <span id="spn_F_Date" runat="server"></span>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-2">To Date</label>
                            <div class="col-md-2">
                                <span id="spn_T_Date" runat="server"></span>
                            </div>
                            <div class="col-md-1"></div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-1"></div>
                            <label class="col-md-2">Base Currency</label>
                            <div class="col-md-3">
                                <span id="pk_base_currency" runat="server" style="display: none"></span>
                                <span id="base_currency" runat="server"></span>
                                <span id="base_currency_rate" runat="server" style="display: none">0</span>
                            </div>
                            <%--<label class="col-md-2">Advance Amount</label>
                            <div class="col-md-2">
                                <span id="req_base_currency" runat="server">0</span>
                            </div>
                            <div class="col-md-1"></div>--%>
                        </div>
                        <%--<div class="form-group">
                            <div class="col-md-1"></div>
                            <label class="col-md-2">Return Money</label>
                            <div class="col-md-3">
                                <span id="spn_ret_money" runat="server">0</span>
                            </div>
                        </div>--%>

                        <div class="form-group">
                            <div class="table-responsive" id="div_details" runat="server">
                                <table class="table table-bordered table-responsive">
                                    <thead>
                                        <tr>
                                            <th>Advance Taken</th>
                                            <th>Expenditure</th>
                                            <th>Balance</th>
                                            <th>Doc No</th>
                                            <th>Posting Date</th>
                                            <th>Return Money</th>
                                            <th>Payable/Refundable(+/-)</th>
                                            <th>Exchange Rate</th>
                                            <th>Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td style="text-align: right">
                                                <span id="req_base_currency" runat="server">0</span>
                                            </td>
                                            <td style="text-align: right">
                                                <span id="exp_amount" runat="server">0</span>
                                            </td>
                                            <td style="text-align: right">
                                                <span id="balance" runat="server">0</span>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:TextBox ID="txt_doc_no" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:TextBox ID="txt_post_date" runat="server" CssClass="form-control input-sm" ReadOnly></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_ret_money" runat="server" CssClass="form-control input-sm" Text="0" onchange="validateFloatKey(this); calculate_diff()" Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right">
                                                <span id="payable" runat="server">0</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_exc_rate" runat="server" CssClass="form-control input-sm" Text="0" Style="text-align: right" onchange="validateFloatKey(this); calculate_inr();"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right">
                                                <span id="ret_in_inr" runat="server">0</span>
                                            </td>
                                        </tr>
                                    </tbody>

                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>

            <div class="modal fade" id="div_Popup">

            <div id="divIns" runat="server" style="display: none">
                <div style="background-color: #E6E6E6; position: absolute; top: 0; left: 0; width: 100%; height: 300%; z-index: 1001; -moz-opacity: 0.8; opacity: .80; filter: alpha(opacity=80);">
                    <img src="../../images/loading_transparent.gif" style="background-color: transparent; position: fixed; top: 40%; left: 46%;" /></div>
            </div>
            <div class="modal-dialog" style="width: 70%; margin-left: 10%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Summary</font></h4>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center">Advance</th>
                                                <th style="text-align: center">Expense</th>
                                                <th style="text-align: center">Difference</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td style="text-align: center"><span id="tbl_adv" runat="server">0</span></td>
                                                <td style="text-align: center"><span id="tbl_exp" runat="server">0</span></td>
                                                <td style="text-align: center"><span id="tbl_diff" runat="server">0</span></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </div>

                        </div>
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-grey btn-rounded" OnClientClick="return PopupData()" OnClick="btn_Save_Click"/>
                        <a href="javascript:;" class="btn btn-danger btn-rounded" data-dismiss="modal">Close</a>
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
                            <div class="col-md-12" id="tab_btn3" runat="server" style="text-align: center;">
                                <%--<asp:Button ID="btn_Save" runat="server" class="btn btn-danger btn-rounded" Text="Submit" OnClientClick="check_Controls()" />--%>
                                <a id="btn_Save" href="#div_Popup" data-toggle="modal" class="btn btn-danger btn-rounded" onclick="check_Controls()" >Submit</a>
                                <asp:Button ID="btn_Reject" runat="server" class="btn btn-danger btn-rounded" Text="reject" OnClick="btn_Reject_Click" />
                                <asp:Button ID="btn_cancel" runat="server" class="btn btn-danger btn-rounded" Text="Cancel" OnClick="imgBtnRelease_Click" />
                            </div>
                        </div>
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
                    <asp:TextBox ID="txt_amt_limit" runat="server">0</asp:TextBox>
                    <asp:TextBox ID="txt_Audit" runat="server"></asp:TextBox>
                    <asp:TextBox ID="initiator" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_validamt" runat="server"></asp:TextBox>
                    <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_approvar" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_adamount" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_adperiod" runat="server"></asp:TextBox>
                    <asp:TextBox ID="total_amount" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_opencount" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
                    <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="doa_email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_sameapprover" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_desnamt" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_desg_id" runat="server"></asp:TextBox>
                    <asp:TextBox ID="return_pk_id" runat="server"></asp:TextBox>
                    <asp:TextBox ID="doa_user" runat="server"></asp:TextBox>
                    <asp:TextBox ID="step_name" runat="server"></asp:TextBox>
                    <asp:TextBox ID="next_id" runat="server"></asp:TextBox>
                    <asp:TextBox ID="tcity" runat="server" Text="0"></asp:TextBox>
                    <asp:TextBox ID="fcity" runat="server" Text="0"></asp:TextBox>
                    <asp:TextBox ID="pk_base_id" runat="server" Text="0"></asp:TextBox>
                    <asp:TextBox ID="base_curr_amt" runat="server" Text="0"></asp:TextBox>
                    <asp:TextBox ID="txt_Deviate" runat="server" Text="0"></asp:TextBox>

                    <asp:TextBox ID="a_adv_amount" runat="server" Text="0"></asp:TextBox>
                    <asp:TextBox ID="a_exp_amount" runat="server" Text="0"></asp:TextBox>
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
      
        <script type="text/javascript">

            $('#txt_post_date').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', endDate: new Date() });
            calculate_diff();

            function download_files(index) {
                var app_path = $("#app_Path").val();
                var req_no = $("#spn_req_no").text();
                window.open(app_path + '/Common/FileDownload.aspx?indentno=' + req_no + '&filename=' + $("#uploadTable tr")[index].cells[1].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
            }

            function check_Controls() {
                if ($("#txt_ret_money").val() == "" && $("#txt_ret_money").val() < 1) {
                    alert("Please Enter valid Advance Return Amount");
                    $("#div_Popup").hide();
                    return false;
                }
                else {
                    if ($("#txt_exc_rate").val() == "" && $("#txt_exc_rate").val() < 1) {
                        $("#div_Popup").modal();
                        alert("Please Enter valid Exchange Rate");
                        return false;
                    }
                    else {
                        //$("#div_Popup").modal('show');
                        //$("#div_Popup").show();

                    }
                }
            }

            function validateFloatKey(el) {
                var v = parseFloat(el.value);
                el.value = (isNaN(v)) ? '' : v.toFixed(2);
            }

            function calculate_diff() {
                var balance = $("#balance").html();
                var ret_money = $("#txt_ret_money").val();
                //var return_exc = $("#return_exc").val();
                if (balance == "" || balance == undefined) {
                    balance = 0;
                }
                if (ret_money == "" || ret_money == undefined) {
                    ret_money = 0;
                }
                $("#payable").html((parseFloat(balance) - parseFloat(ret_money)).toFixed(2) * (-1));
                //$("#span_diff").text(parseFloat(balance) - (parseFloat($("#return_money").val()) - parseFloat($("#pay_ref").text())).toFixed(2));
                calculate_inr();
            }

            function calculate_inr() {
                var ret_money = $("#txt_ret_money").val();
                var return_exc = $("#txt_exc_rate").val();
                if (isNaN(ret_money) || ret_money == undefined || ret_money == 0) {
                    ret_money = 0;
                }
                if (isNaN(return_exc) || return_exc == undefined || return_exc == 0) {
                    return_exc = 0;
                }
                var inr_ret = Math.round(parseFloat(parseFloat(ret_money) * (parseFloat(return_exc))));
                var inr_adv = $("#txt_adamount").val();
                var inr_exp = parseInt(inr_adv) - parseInt(inr_ret);

                $("#ret_in_inr").html(parseInt(inr_ret));
                $("#tbl_adv").html(inr_adv)
                $("#tbl_diff").html(inr_ret);
                $("#tbl_exp").html(inr_exp);

                $("#a_adv_amount").val(inr_adv);
                $("#a_exp_amount").val(inr_exp);
            }
	
	function PopupData()
	{
		$("#divIns").show();
		return true;
	}

        </script>
    </form>

</body>
</html>
