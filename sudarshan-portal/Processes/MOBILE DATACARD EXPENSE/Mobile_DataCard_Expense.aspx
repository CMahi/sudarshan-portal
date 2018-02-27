<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Mobile_DataCard_Expense.aspx.cs" Inherits="Mobile_DataCard_Expense" %>

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
                            <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color: blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                        </div>
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>Employee Details</h3>
                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">

                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Mode<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlPayMode" runat="server" class="form-control  input-sm width-xs">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <div id="div_payment">
                                    <label class="col-md-2">Payment Location<font color="#ff0000"><b>*</b></font></label>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlLocation" runat="server" class="form-control  input-sm width-xs">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">

                                <div class="col-md-1"></div>
                                <label class="col-md-2">Expense Head<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlExpenseHead" runat="server" class="form-control input-sm width-xs">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Supporting Documents</label>
                                <div class="col-md-3">
                                    <a href="#div_UploadDocument" data-toggle="modal">
                                        <img id="Img1" src="../../images/attachment.png" alt="Click here to attach file." height="20" width="20" /></a>
                                </div>
                            </div>
                            <%--      <div class="form-group">                                
                                <div class="col-md-3">
                                      <asp:Label ID="lbl_appadid" runat="server" ></asp:Label>
                                </div>                               
                                <div class="col-md-3">
                                    <asp:Label ID="lbl_appemail" runat="server"></asp:Label>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" id="div_DataCard" runat="server">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Data Card (<span id="span_support">0</span>)</h4>
                    </div>
                    <div class="panel-body">
                        <p style="color: red">Note : <i class="fa fa-fw fa-angle-double-right"></i><i class="fa fa-fw fa-angle-double-right"></i>Y (Mandatory - Supporting Documents), N (Not Mandatory - Supporting Documents)</p>

                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Card No<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <input type='text' class="form-control" id="txtCardNo" runat="server" onkeypress="return isNumberKey(event)" />
                                </div>
                                <label class="col-md-2">Service Provider<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCardProvider" runat="server" class="form-control  input-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Year<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCardYear" runat="server" class="form-control  input-sm">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-2">Month<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCardMonth" runat="server" class="form-control  input-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Bill No<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <input type='text' class="form-control" id="txt_card_billno" runat="server" />
                                </div>
                                <label class="col-md-2">Bill Date<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <div class="input-group" id="Div6">
                                        <input type='text' class="form-control datepicker-dropdown" id="txt_card_billdate" runat="server" readonly />
                                        <span class="input-group-btn">
                                            <button class="btn btn-danger" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Bill Amount<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <input id="txt_card_billamt" type='text' class="form-control" runat="server" onkeypress="return isNumberKey(event)" onchange="valuedatacard(1);" />
                                </div>
                                <label class="col-md-2">Allowed Reimbursement </label>
                                <div class="col-md-3">
                                    <asp:Label ID="txt_Reimbursement" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                            <%--           <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Supporting Doc</label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlsupp_datacard" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <label class="col-md-2">Supporting Particulars</label>
                                <div class="col-md-3">
                                     <input type='text' class="form-control" id="txt_cardsuppremark" runat="server" />
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Tax<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <input id="txt_datacardtax" type='text' class="form-control" runat="server" onkeypress="return isNumberKey(event)" onchange="valuedatacard(1);" />
                                </div>
                                <label class="col-md-2">Total Reimbursement Amount</label>
                                <div class="col-md-3">
                                    <asp:Label ID="lbl_totaldataamt" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">

                                <div class="col-md-1"></div>
                                <label class="col-md-2">Remark<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-8">
                                    <textarea type='text' class="form-control" rows="1" id="txt_remark" runat="server"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" id="div_deduction" runat="server">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <h4 class="panel-title">Total Amount : <span id="span_dedamount">0</span></h4>
                        </div>
                        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Deduction Details</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div id="div_dedction" runat="server"></div>
                            <%--   <div class="form-group">
                                <label class="col-md-2">Deduction Year</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddldedYear" runat="server" class="form-control  input-sm" onchange="getDeductAmount()">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-2">Deduction Month</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlDeduMonth" runat="server" class="form-control  input-sm" onchange="getDeductAmount()">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-md-2">Deduction Amount</label>
                                <div class="col-md-2">
                                   <span id="txt_deductionamt" runat="server">0</span>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" id="div_MobileReimbursement">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <h4 class="panel-title">&nbsp;/ Total Amount : <span id="spn_Total">0</span></h4>
                        </div>
                        <div class="panel-heading-btn">
                            <h4 class="panel-title"><a href="#div_Policy1" data-toggle="modal">View Policy</a></h4>
                        </div>
                        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Mobile Reimbursement (<span id="span_support2">0</span>)</h4>
                    </div>
                    <div class="panel-body">
                        <p style="color: red">Note : <i class="fa fa-fw fa-angle-double-right"></i><i class="fa fa-fw fa-angle-double-right"></i>Y (Mandatory - Supporting Documents), N (Not Mandatory - Supporting Documents)</p>
                        <div class="table-responsive">
                            <table id="tbl_Mobile" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Add</th>
                                        <%-- <th>#</th>--%>
                                        <th>Service Provider</th>
                                        <th>Year</th>
                                        <th>Month</th>
                                        <th>Bill No</th>
                                        <th>Bill Date</th>
                                        <th>Bill Amount (<i class='fa fa-rupee'></i>)</th>
                                        <th>Rei. Amount (<i class='fa fa-rupee'></i>)</th>
                                        <th>Limit</th>
                                        <th>Supporting Doc</th>
                                        <%--  <th>Supporting Particulars</th>--%>
                                        <th>Remark</th>
                                        <th>Delete</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="add_Mobile"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-plus"></i></td>
                                        <%--  <td>1</td>--%>
                                        <td>
                                            <asp:DropDownList ID="ddlServiceProvider1" runat="server" class="form-control  input-sm width-xs"></asp:DropDownList></td>
                                        <td>
                                            <asp:DropDownList ID="ddlYear1" runat="server" class="form-control  input-sm width-100" onchange="comdedyear(1)"></asp:DropDownList></td>
                                        <td>
                                            <asp:DropDownList ID="ddlMonth1" runat="server" class="form-control  input-sm width-100" onchange="comdedmonth(1)"></asp:DropDownList></td>
                                        <td>
                                            <input type='text' class="form-control input-sm width-100" id="txt_mobile_bill_no1" runat="server" /></td>
                                        <td>
                                            <div class='input-group'>
                                                <input type='text' class="form-control input-sm width-100 datepicker-dropdown" id="txt_mobile_bill_date1" readonly="" runat="server" /><span class="input-group-btn">
                                                    <button class="btn btn-danger input-sm" type="button"><i class="fa fa-calendar"></i></button>
                                                </span>
                                            </div>
                                        </td>
                                        <td>
                                            <input type='text' id="txt_mobile_bill_amt1" class="form-control input-sm width-100" onkeypress="return isNumberKey(event)" runat="server" /></td>
                                        <td>
                                            <input type='text' id="txt_total1" class="form-control input-sm width-100" runat="server" onkeypress="return isNumberKey(event)" onchange="value1billamt(1)" /></td>

                                        <td>
                                            <asp:Label ID="txt_Mreimbursement" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddl_support1" runat="server" class="form-control  input-sm width-xs width-60"></asp:DropDownList></td>
                                        <%-- <td>
                                            <input type='text' class="form-control input-sm width-100" id="txt_supremark1" runat="server" /></td>--%>
                                        <td>
                                            <input type='text' class="form-control input-sm width-200" id="txt_Mremark1" runat="server" /></td>

                                        <td class="delete"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-trash" onclick="delete_Row(1)"></i></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12" id="div_UserMobileReimbursement" runat="server">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <div class="panel-heading-btn">
                            <h4 class="panel-title">&nbsp;/ Total Amount : <span id="spn_userTotal">0</span></h4>
                        </div>
                        <div class="panel-heading-btn">
                            <h4 class="panel-title"><a href="#div_Policy1" data-toggle="modal">View Policy</a></h4>
                        </div>
                        <h4 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone"></i>Mobile Reimbursement (<span id="span_support1">0</span>)</h4>
                    </div>
                    <div class="panel-body">
                        <p style="color: red">Note : <i class="fa fa-fw fa-angle-double-right"></i><i class="fa fa-fw fa-angle-double-right"></i>Y (Mandatory - Supporting Documents), N (Not Mandatory - Supporting Documents)</p>
                        <div class="table-responsive">
                            <table id="tbl_UserMobile" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Add</th>
                                        <%-- <th>#</th>--%>
                                        <th>Mobile No</th>
                                        <th>Service Provider</th>
                                        <th>Bill No</th>
                                        <th>Bill Date</th>
                                        <th>Bill Amount</th>
                                        <th>Rei. Amount</th>
                                        <th>Limit</th>
                                        <th>Tax</th>
                                        <th>Total Reimb.</th>
                                        <th>Supporting Doc</th>
                                        <%--   <th>Supporting Particulars</th>--%>
                                        <th>Remark</th>

                                        <th>Delete</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="add_UserMobile"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-plus"></i></td>
                                        <%-- <td>1</td>--%>
                                        <td>
                                            <input type='text' class="form-control input-sm width-xs" id="txt_User_MobileNo1" runat="server" onkeypress="return isNumberKey(event)" /></td>
                                        <td>
                                            <asp:DropDownList class="form-control  input-sm width-100" ID="ddlServiceProviderUser1" runat="server"></asp:DropDownList></td>
                                        <td>
                                            <input type='text' class="form-control input-sm width-100" id="txt_user_mobile_bill_no1" runat="server" /></td>
                                        <td>
                                            <div class='input-group'>
                                                <input type='text' class="form-control input-sm width-100 datepicker-dropdown" id="txt_mobile_user_bill_date1" readonly="" runat="server" /><span class="input-group-btn">
                                                    <button class="btn btn-danger input-sm" type="button"><i class="fa fa-calendar"></i></button>
                                                </span>
                                            </div>
                                        </td>
                                        <td>
                                            <input type='text' id="txt_mobile_user_bill_amt1" class="form-control input-sm width-100" onkeypress="return isNumberKey(event)" runat="server" /></td>
                                        <td>
                                            <input type='text' id="txt_user_total1" class="form-control input-sm width-100" runat="server" onkeypress="return isNumberKey(event)" onchange="valuebillamt(1);" /></td>
                                        <td>
                                            <asp:Label ID="txt_user_Mreimbursement" runat="server"></asp:Label></td>
                                        <td>
                                            <input type='text' class="form-control input-sm width-100" id="txt_user_tax1" runat="server" onkeypress="return isNumberKey(event)" onchange="valuechan(1);" /></td>
                                        <td>
                                            <asp:Label ID="lbl_amttax1" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlusersupp1" runat="server" class="form-control  input-sm width-xs width-60"></asp:DropDownList></td>
                                        <%--<td>
                                            <input type='text' class="form-control input-sm width-100" id="txtusersupport1" runat="server" /></td>--%>
                                        <td>
                                            <input type='text' class="form-control input-sm width-200" id="txt_user_Mremark1" runat="server" /></td>

                                        <td class="delete"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-trash" onclick="delete_user_Row(1)"></i></td>
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
        <div style="display: none;" class="modal" id="div_Policy1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Mobile DataCard Policy</h5>
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
                                    <label>Upload .pdf,.jpeg,.jpg,.png formats only.</label></p>
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
        <div id="div_txt" style="display: none" runat="server">
            <asp:TextBox ID="txt_doc" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_table_doc" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Doc_Xml" runat="server"></asp:TextBox>
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
            <asp:TextBox ID="lnkText" runat="server"></asp:TextBox>
            <asp:TextBox ID="ddlText" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Action" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Audit" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtDummy" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Datacode" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_xml_data_mobile" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_xml_data_datacard" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_year" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_month" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_serviceprovide" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ddlsupport" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_approvar" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_user_year" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_user_month" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_user_serviceprovide" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_cmobiole" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_pkexpense1" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_adid" runat="server"></asp:TextBox>
            <asp:TextBox ID="supp_amt_no_db" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_amt_no_tot" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_amt_tot" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_perc_no" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="supp_perc_no_d" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="dev_supp_amt" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="dev_supp_limit" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_Deduction_amt" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="txt_ded_year" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ded_month" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ded_count" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ded_amount" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_check_Nos" runat="server"></asp:TextBox>
             <asp:TextBox ID="txt_confirm" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_ded_total_amount" runat="server"></asp:TextBox>
        </div>

        <!-- ================== BEGIN BASE JS ================== -->
        <%--        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>--%>
        <script src="../../assets/plugins/jquery/jquery-2.1.4.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script type="text/javascript" src="../../JS/MobileCardExpense.js"></script>
    </form>
    <script type="text/javascript">


        $(document).ready(function () {
            if ($("#lbl_bankAccNo").html() == "NA" || $("#lbl_bankAccNo").html() == "") {
                alert("Bank Account Is Not Available! Unable To Claim Expense Request...!");
                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            }
            else {
                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
                if ($("#lbl_Grade").html() == "NA" || $("#lbl_Grade").html() == "") {
                    alert("Grade Is Not Available! Unable To Claim Expense Request...!");
                    $('input[Id="btn_Save"]').attr('disabled', 'disabled');
                }
                else {
                    $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
                }
            }

            var expenseHead = $("#ddlExpenseHead").val();
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, endDate: new Date(), todayBtn: 'linked' });
            $("#div_DataCard").hide();
            $("#div_MobileReimbursement").hide();
            $("#div_UserMobileReimbursement").hide();
            $("#div_deduction").hide();
            var mobile = $("#txt_cmobiole").val();
            if (expenseHead == "Mobile") {
                if (mobile != "") {
                    $("#div_MobileReimbursement").show();
                    $("#div_deduction").show();

                    $("#div_UserMobileReimbursement").hide();
                }
                else {
                    $("#div_UserMobileReimbursement").show();
                    $("#div_MobileReimbursement").hide();
                    $("#div_deduction").hide();
                }
            }
            else {
                $("#div_DataCard").show();
            }
            var mode = $("#ddlPayMode").val();
            if (mode == 2) {
                $("#div_payment").hide();
            }
            else {
                $("#div_payment").show();
            }
        });
        /***************** Company Provided Mobile**********************/
        $('body').on('click', '.add_Mobile', function () {
            addNewRowMobile();
        });

        function addNewRowMobile() {
            var lastRow = $('#tbl_Mobile tr').length;
            var lastRow1 = $('#tbl_Mobile tr').length - 1;
            var ddlye = document.getElementById("txt_year").value;
            var ddlmon = document.getElementById("txt_month").value;
            var ddlserve = document.getElementById("txt_serviceprovide").value;
            var ddlsupp = document.getElementById("txt_ddlsupport").value;
            for (var q = 1; q <= lastRow1; q++) {
                var provi = document.getElementById("ddlServiceProvider" + q).value;
                var year = document.getElementById("ddlYear" + q).value;
                var mnt = document.getElementById("ddlMonth" + q).value;
                var mbill = document.getElementById("txt_mobile_bill_no" + q).value;
                var mbilldate = document.getElementById("txt_mobile_bill_date" + q).value;
                var mbillamt = document.getElementById("txt_mobile_bill_amt" + q).value;
                var remamt = document.getElementById("txt_total" + q).value;
                var supp = document.getElementById("ddl_support" + q).value;
                //  var supprmk = document.getElementById("txt_supremark" + q).value;

                if (provi == 0) {
                    alert('Please Select Service Provider at row :' + q + '');
                    return false;
                }
                if (year == 0) {
                    alert('Please Select Year at row :' + q + '');
                    return false;
                }
                if (mnt == 0) {
                    alert('Please Select Month at row :' + q + '');
                    return false;
                }
                if (mbill == "") {
                    alert('Please Enter Bill No at row :' + q + '');
                    return false;
                }
                if (mbilldate == 0) {
                    alert('Please Select Bill Date at row :' + q + '');
                    return false;
                }
                if (mbillamt == "") {
                    alert('Please Enter Bill Amount at row :' + q + '');
                    return false;
                }
                if (remamt == "") {
                    alert('Please Enter Reimbursement Amount at row :' + q + '');
                    return false;
                }
                var mrmk = document.getElementById("txt_Mremark" + q).value;
                if (mrmk == "") {
                    alert('Please Enter Remark at row :' + q + '');
                    return false;
                }
                if (supp == 0) {
                    alert('Please Select Supporting Document at row :' + i + '');
                    return false;
                }
                //if (supp == "Y") {
                //    if (supprmk == "") {
                //        alert('Please Enter Supporting Particulars at row :' + i + '');
                //        return false;
                //    }
                //}

            }
            var html11 = "<tr> <td class='add_Mobile'  id='add_Row" + (lastRow1 + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
            //var html = "<td>" + (lastRow1 + 1) + "</td>";
            var html2 = "<td><select id='ddlServiceProvider" + (lastRow1 + 1) + "'  class='form-control input-sm width-xs'>" + ddlserve + "</select></td>";
            var html3 = "<td><select id='ddlYear" + (lastRow1 + 1) + "'  class='form-control input-sm width-100' onchange='comdedyear(" + (lastRow1 + 1) + ")'>" + ddlye + "</select></td>";
            var html4 = "<td><select id='ddlMonth" + (lastRow1 + 1) + "'  class='form-control input-sm width-100' onchange='comdedmonth(" + (lastRow1 + 1) + ")'>" + ddlmon + "</select></td>";
            var html5 = "<td><input class='form-control input-sm width-100' type='text' id='txt_mobile_bill_no" + (lastRow1 + 1) + "'></input></td>";
            var html6 = "<td><div class='input-group'><input type='text' class='form-control input-sm width-100 datepicker-dropdown' id='txt_mobile_bill_date" + (lastRow1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html7 = "<td><input class='form-control input-sm width-100' type='text' id='txt_mobile_bill_amt" + (lastRow1 + 1) + "' onkeypress='return isNumberKey(event)' ></input></td>";
            var html8 = "<td><input class='form-control input-sm width-100' type='text' id='txt_total" + (lastRow1 + 1) + "' onkeypress='return isNumberKey(event)' onchange='value1billamt(" + (lastRow1 + 1) + ");'></input></td>";
            var html9 = "<td><label id='txt_Mreimbursement" + (lastRow1 + 1) + "'>" + $("#txt_Mreimbursement").text() + "</label></td><td><select id='ddl_support" + (lastRow1 + 1) + "'  class='form-control  input-sm width-xs width-60'>" + ddlsupp + "</select></td>";
            var html10 = "<td><input type='text' class='form-control input-sm width-200' id='txt_Mremark" + (lastRow1 + 1) + "' /></td>";
            var html12 = "<td id='delete_Row" + (lastRow1 + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_Row(" + (lastRow1 + 1) + ")'></i></td></tr>";
            var htmlcontent = $(html11 + "" + html2 + "" + html3 + "" + html4 + "" + html5 + "" + html6 + "" + html7 + "" + html8 + "" + html9 + "" + html10 + "" + html12);

            $('#tbl_Mobile').append(htmlcontent);
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, endDate: new Date(), todayBtn: 'linked' });
        }

        function delete_Row(RowIndex) {
            try {
                var tbl = document.getElementById("tbl_Mobile");
                var lastRow = tbl.rows.length;
                if (lastRow <= 2) {
                    alert("Validation Error:You have to Enter atleast one record..!");
                    return false;
                }
                var IsDelete = confirm("Are you sure to delete this ?");
                if (IsDelete) {
                    for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {

                        document.getElementById("ddlServiceProvider" + (parseInt(contolIndex) + 1)).id = "ddlServiceProvider" + contolIndex;
                        document.getElementById("ddlYear" + (parseInt(contolIndex) + 1)).onchange = new Function("comdedyear(" + contolIndex + ")");
                        document.getElementById("ddlYear" + (parseInt(contolIndex) + 1)).id = "ddlYear" + contolIndex;
                        document.getElementById("ddlMonth" + (parseInt(contolIndex) + 1)).onchange = new Function("comdedmonth(" + contolIndex + ")");
                        document.getElementById("ddlMonth" + (parseInt(contolIndex) + 1)).id = "ddlMonth" + contolIndex;
                        document.getElementById("txt_mobile_bill_no" + (parseInt(contolIndex) + 1)).id = "txt_mobile_bill_no" + contolIndex;
                        document.getElementById("txt_mobile_bill_date" + (parseInt(contolIndex) + 1)).id = "txt_mobile_bill_date" + contolIndex;
                        //document.getElementById("txt_mobile_bill_amt" + (parseInt(contolIndex) + 1)).onchange = new Function("calculate_Total()");
                        document.getElementById("txt_mobile_bill_amt" + (contolIndex + 1)).id = "txt_mobile_bill_amt" + contolIndex;
                        document.getElementById("txt_total" + (parseInt(contolIndex) + 1)).onchange = new Function("value1billamt(" + contolIndex + ")");
                        document.getElementById("txt_total" + (parseInt(contolIndex) + 1)).id = "txt_total" + contolIndex;
                        document.getElementById("txt_Mreimbursement" + (parseInt(contolIndex) + 1)).id = "txt_Mreimbursement" + contolIndex;
                        document.getElementById("ddl_support" + (parseInt(contolIndex) + 1)).id = "ddl_support" + contolIndex;
                        //document.getElementById("txt_supremark" + (parseInt(contolIndex) + 1)).id = "txt_supremark" + contolIndex;
                        document.getElementById("txt_Mremark" + (parseInt(contolIndex) + 1)).id = "txt_Mremark" + contolIndex;
                        document.getElementById("add_Row" + (contolIndex + 1)).id = "add_Row" + contolIndex;
                        document.getElementById("delete_Row" + (contolIndex + 1)).onclick = new Function("return delete_Row(" + contolIndex + ")");
                        document.getElementById("delete_Row" + (contolIndex + 1)).id = "delete_Row" + contolIndex;
                    }
                    ///////////////////////////////////////////////////////////////////////
                    tbl.deleteRow(RowIndex);
                    var total = 0;
                    var lastRow = $('#tbl_Mobile tr').length;
                    for (var q = 0; q < lastRow; q++) {
                        var amount = 0;
                        if ($("#txt_total" + (q + 1)).val() != "" && $("#txt_total" + (q + 1)).val() != undefined) {
                            amount = parseFloat($("#txt_total" + (q + 1)).val());
                            $("#txt_total" + (q + 1)).val(amount);
                        }
                        else {
                            $("#txt_total" + (q + 1)).val(0);
                        }
                        total = total + amount;
                    }
                    $("#spn_Total").html(total);

                    alert("Record Deleted Successfully..!");
                }
            }
            catch (Exc) { }
        }

        $('body').on('click', '.add_UserMobile', function () {
            addNewRowMobileUser();
        });

        /***************** Users Own Mobile***************************/
        function addNewRowMobileUser() {
            var lastR = $('#tbl_UserMobile tr').length;
            var lastR1 = $('#tbl_UserMobile tr').length - 1;
            var ddlyeu = document.getElementById("txt_user_year").value;
            var ddlmonu = document.getElementById("txt_user_month").value;
            var ddlserveu = document.getElementById("txt_user_serviceprovide").value;
            var ddlsupp = document.getElementById("txt_ddlsupport").value;
            for (var q = 1; q <= lastR1; q++) {
                var mno = document.getElementById("txt_User_MobileNo" + q).value;
                var provi = document.getElementById("ddlServiceProviderUser" + q).value;
                var mbill = document.getElementById("txt_user_mobile_bill_no" + q).value;
                var mbilldate = document.getElementById("txt_mobile_user_bill_date" + q).value;
                var mbillamt = document.getElementById("txt_mobile_user_bill_amt" + q).value;
                var remamt = document.getElementById("txt_user_total" + q).value;
                var tax = document.getElementById("txt_user_tax" + q).value;
                var mrmk = document.getElementById("txt_user_Mremark" + q).value;
                var supp = document.getElementById("ddlusersupp" + q).value;
                // var supprmk = document.getElementById("txtusersupport" + q).value;

                if (mno == "") {
                    alert('Please Enter Mobile No at row :' + q + '');
                    return false;
                }
                if (mno.length < 10) {
                    alert('Mobile number must be 10 digits at row :' + q + '');
                    return false;
                }
                if (provi == 0) {
                    alert('Please Select Service Provider at row :' + q + '');
                    return false;
                }
                if (mbill == "") {
                    alert('Please Enter Bill No at row :' + q + '');
                    return false;
                }
                if (mbilldate == 0) {
                    alert('Please Select Bill Date at row :' + q + '');
                    return false;
                }
                if (mbillamt == "") {
                    alert('Please Enter Bill Amount at row :' + q + '');
                    return false;
                }
                if (remamt == "") {
                    alert('Please Enter Reimbursement Amount at row :' + q + '');
                    return false;
                }
                if (tax == "") {
                    alert('Please Enter Tax at row :' + q + '');
                    return false;
                }
                if (mrmk == "") {
                    alert('Please Enter Remark at row :' + q + '');
                    return false;
                }
                if (supp == 0) {
                    alert('Please Select Supporting Document at row :' + i + '');
                    return false;
                }
                //if (supp == "Y") {
                //    if (supprmk == "") {
                //        alert('Please Enter Supporting Particulars at row :' + i + '');
                //        return false;
                //    }
                //}
            }
            var html13 = "<tr> <td class='add_UserMobile'  id='add_user_Row" + (lastR1 + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
            //var html = "<td>" + (lastR1 + 1) + "</td>";
            var html1 = "<td><input class='form-control input-sm width-xs' type='text' id='txt_User_MobileNo" + (lastR1 + 1) + "' onkeypress='return isNumberKey(event)'></input></td>";
            var html2 = "<td><select id='ddlServiceProviderUser" + (lastR1 + 1) + "'  class='form-control input-sm width-100'>" + ddlserveu + "</select></td>";
            var html5 = "<td><input class='form-control input-sm width-100' type='text' id='txt_user_mobile_bill_no" + (lastR1 + 1) + "'></input></td>";
            var html6 = "<td><div class='input-group'><input type='text' class='form-control input-sm width-100 datepicker-dropdown' id='txt_mobile_user_bill_date" + (lastR1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html7 = "<td><input class='form-control input-sm width-100' type='text' id='txt_mobile_user_bill_amt" + (lastR1 + 1) + "' onkeypress='return isNumberKey(event)'></input></td>";
            var html8 = "<td><input class='form-control input-sm width-100' type='text' id='txt_user_total" + (lastR1 + 1) + "' onkeypress='return isNumberKey(event)' onchange='valuebillamt(" + (lastR1 + 1) + ");'></input></td><td><label id='txt_user_Mreimbursement" + (lastR1 + 1) + "'>" + $("#txt_user_Mreimbursement").text() + "</label></td>";
            var html10 = "<td><input class='form-control input-sm width-100' type='text' id='txt_user_tax" + (lastR1 + 1) + "'  onkeypress='return isNumberKey(event)' onchange='valuechan(" + (lastR1 + 1) + ");'></input></td>";
            var html11 = "<td><label id='lbl_amttax" + (lastR1 + 1) + "' ></label></td><td><select id='ddlusersupp" + (lastR1 + 1) + "'  class='form-control  input-sm width-xs width-60'>" + ddlsupp + "</select></td>";
            var html12 = "<td><input type='text' class='form-control input-sm width-200' id='txt_user_Mremark" + (lastR1 + 1) + "' /></td>";
            var html14 = "<td id='delete_user_Row" + (lastR1 + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_user_Row(" + (lastR1 + 1) + ")'></i></td></tr>";
            var htmlcontent = $(html13 + "" + html1 + "" + html2 + "" + html5 + "" + html6 + "" + html7 + "" + html8 + "" + html10 + "" + html11 + "" + html12 + "" + html14);

            $('#tbl_UserMobile').append(htmlcontent);
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, endDate: new Date(), todayBtn: 'linked' });

        }

        function delete_user_Row(RowIndex) {
            try {
                var tbl = document.getElementById("tbl_UserMobile");
                var lastRow = tbl.rows.length;
                if (lastRow <= 2) {
                    alert("Validation Error:You have to Enter atleast one record..!");
                    return false;
                }
                var IsDelete = confirm("Are you sure to delete this ?");
                if (IsDelete) {
                    for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
                        document.getElementById("txt_User_MobileNo" + (parseInt(contolIndex) + 1)).id = "txt_User_MobileNo" + contolIndex;
                        document.getElementById("ddlServiceProviderUser" + (parseInt(contolIndex) + 1)).id = "ddlServiceProviderUser" + contolIndex;
                        document.getElementById("txt_user_mobile_bill_no" + (parseInt(contolIndex) + 1)).id = "txt_user_mobile_bill_no" + contolIndex;
                        // document.getElementById("txt_mobile_bill_date" + (parseInt(contolIndex) + 1)).id = "txt_mobile_bill_date" + contolIndex;
                        document.getElementById("txt_mobile_user_bill_date" + (parseInt(contolIndex) + 1)).id = "txt_mobile_user_bill_date" + contolIndex;
                        //document.getElementById("txt_mobile_user_bill_amt" + (parseInt(contolIndex) + 1)).onchange = new Function("calculate_UserTotal()");
                        document.getElementById("txt_mobile_user_bill_amt" + (contolIndex + 1)).id = "txt_mobile_user_bill_amt" + contolIndex;
                        document.getElementById("txt_user_total" + (parseInt(contolIndex) + 1)).onchange = new Function("valuebillamt(" + contolIndex + ")");
                        document.getElementById("txt_user_total" + (parseInt(contolIndex) + 1)).id = "txt_user_total" + contolIndex;
                        document.getElementById("txt_user_tax" + (parseInt(contolIndex) + 1)).onchange = new Function("valuechan(" + contolIndex + ")");
                        document.getElementById("txt_user_Mreimbursement" + (parseInt(contolIndex) + 1)).id = "txt_user_Mreimbursement" + contolIndex;
                        document.getElementById("txt_user_tax" + (parseInt(contolIndex) + 1)).id = "txt_user_tax" + contolIndex;
                        document.getElementById("lbl_amttax" + (parseInt(contolIndex) + 1)).id = "lbl_amttax" + contolIndex;
                        document.getElementById("ddlusersupp" + (parseInt(contolIndex) + 1)).id = "ddlusersupp" + contolIndex;
                        // document.getElementById("txtusersupport" + (parseInt(contolIndex) + 1)).id = "txtusersupport" + contolIndex; 
                        document.getElementById("txt_user_Mremark" + (parseInt(contolIndex) + 1)).id = "txt_user_Mremark" + contolIndex;
                        document.getElementById("add_user_Row" + (contolIndex + 1)).id = "add_user_Row" + contolIndex;
                        document.getElementById("delete_user_Row" + (contolIndex + 1)).onclick = new Function("return delete_user_Row(" + contolIndex + ")");
                        document.getElementById("delete_user_Row" + (contolIndex + 1)).id = "delete_user_Row" + contolIndex;
                    }
                    ///////////////////////////////////////////////////////////////////////
                    tbl.deleteRow(RowIndex);
                    var total = 0;
                    var lastRow = $('#tbl_UserMobile tr').length;
                    for (var q = 0; q < lastRow - 1; q++) {
                        var amount = 0;
                        if ($("#lbl_amttax" + (q + 1)).html() != "" && $("#lbl_amttax" + (q + 1)).html() != undefined) {
                            amount = parseFloat($("#lbl_amttax" + (q + 1)).html());
                            $("#lbl_amttax" + (q + 1)).html(amount);
                        }
                        else {
                            $("#lbl_amttax" + (q + 1)).html(0);
                        }
                        total = total + amount;
                    }
                    $("#spn_userTotal").html(total);
                    alert("Record Deleted Successfully..!");
                }
            }
            catch (Exc) { }
        }

        function valuechan(index) {
            var biiamt = parseInt($("#txt_user_total" + index).val());
            var reimamt = parseInt($("#txt_user_tax" + index).val());
            if (biiamt != "" && reimamt != "") {
                var diff = biiamt + reimamt;
                $('#lbl_amttax' + (index)).html(diff);
            }
            var total = 0;
            var lastRow = $('#tbl_UserMobile tr').length;
            for (var q = 0; q < lastRow - 1; q++) {
                var amount = 0;
                if ($("#lbl_amttax" + (q + 1)).html() != "" && $("#lbl_amttax" + (q + 1)).html() != undefined) {
                    amount = parseFloat($("#lbl_amttax" + (q + 1)).html());
                    $("#lbl_amttax" + (q + 1)).html(amount);
                }
                else {
                    $("#lbl_amttax" + (q + 1)).html(0);
                }
                total = total + amount;
            }
            $("#spn_userTotal").html(total)
        }
        function valuebillamt(index) {
            var mbillamt = parseInt($("#txt_mobile_user_bill_amt" + index).val());
            var reimamt = parseInt($("#txt_user_total" + index).val());
            var tax = parseInt($("#txt_user_tax" + index).val());
            if (reimamt != "" && tax != "") {
                var diff = reimamt + tax;
                $('#lbl_amttax' + (index)).html(diff);
            }
            if (parseInt(mbillamt) < parseInt(reimamt)) {
                alert('Bill Amount should be greater than Reimbursement Amount at row :' + index + '');
                $("#txt_user_total" + index).val('');
                return false;
            }
            var total = 0;
            var lastRow = $('#tbl_UserMobile tr').length;
            for (var q = 0; q < lastRow - 1; q++) {
                var amount = 0;
                if ($("#lbl_amttax" + (q + 1)).html() != "" && $("#lbl_amttax" + (q + 1)).html() != undefined) {
                    amount = parseFloat($("#lbl_amttax" + (q + 1)).html());
                    $("#lbl_amttax" + (q + 1)).html(amount);
                }
                else {
                    $("#lbl_amttax" + (q + 1)).html(0);
                }
                total = total + amount;
            }
            $("#spn_userTotal").html(total);
        }
        function value1billamt(index) {
            try {
                var mbillamt = parseInt($("#txt_mobile_bill_amt" + index).val());
                var reimamt = parseInt($("#txt_total" + index).val());
                if (parseInt(mbillamt) < parseInt(reimamt)) {
                    alert('Bill Amount should be greater than Reimbursement Amount at row :' + index + '');
                    $("#txt_total" + index).val('');
                    return false;
                }
                var tbl = document.getElementById("tbldedu");
                document.getElementById("txt_check_Nos").value = "";
                document.getElementById("txt_ded_amount").value = 0;
                document.getElementById("txt_ded_total_amount").value = 0;
                var rowCount = $('#tbldedu tr').length;
                var vals = "";
                var j = 1;
                for (var k = 1; k < rowCount; k++) {
                    var checkboxes = document.getElementById("checkbox" + k + "");
                    if (checkboxes.checked) {
                        document.getElementById("txt_ded_amount").value += $("#ded_amount" + k).val() + ";";
                        document.getElementById("txt_ded_total_amount").value = parseInt(document.getElementById("txt_ded_total_amount").value) + parseInt($("#ded_amount" + k).val());
                        vals += "," + checkboxes.value;
                        document.getElementById("txt_check_Nos").value = j++;
                    }
                }
                if (document.getElementById("txt_check_Nos").value == "") {
                    alert("Select Deduction Amount for Claim...!");
                    $("#txt_total" + index).val('');
                    return false;
                }
                $("#span_dedamount").html($("#txt_ded_total_amount").val())
                var total = 0;
                var ded_Amount = 0;
                var index = 0;
                if ($("#txt_ded_total_amount").val() != "" || $("#txt_ded_total_amount").val() != undefined) {
                    ded_Amount = $("#txt_ded_total_amount").val();
                }
                var lastRow = $('#tbl_Mobile tr').length;
                for (var q = 0; q < lastRow - 1; q++) {
                    var amount = 0;
                    index = q + 1;
                    if ($("#txt_total" + (q + 1)).val() != "" && $("#txt_total" + (q + 1)).val() != undefined) {
                        amount = parseFloat($("#txt_total" + (q + 1)).val());
                        $("#txt_total" + (q + 1)).val(amount);
                    }
                    else {
                        $("#txt_total" + (q + 1)).val(0);
                    }
                    total = total + amount;
                }
                $("#spn_Total").html(total);
                if (parseInt(total) > parseInt(ded_Amount)) {
                    $("#txt_total" + index).val('');
                    alert("Reimbursement Amount should not be greater than Deduction Amount");
                    return false;
                }
                return true;
            }
            catch (ex)
            { }

        }
        function valuedatacard(index) {
            var cardbill = parseInt($("#txt_card_billamt").val());
            var dreamt = parseInt($("#txt_Reimbursement").text());
            var dtax = parseInt($("#txt_datacardtax").val());
            if (dreamt != "" && dtax != "" && cardbill != "") {
                if (cardbill >= dreamt) {
                    var diff = dreamt + dtax;
                    $('#lbl_totaldataamt').html(diff);
                }
                else {
                    var diff = cardbill + dtax;
                    $('#lbl_totaldataamt').html(diff);
                }
            }
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

        $("#ddlExpenseHead").change(function () {
            var expenseHead = $("#ddlExpenseHead").val();
            var empad_id = $("#txt_adid").val();
            var empcode = $("#txt_Datacode").val();

            if (expenseHead == "Mobile") {
                var mobile = $("#txt_cmobiole").val();

                if (mobile != "") {
                    $("#div_MobileReimbursement").show();
                    $("#div_deduction").show();
                    if ($("#txt_ded_count").val() == "NA" || $("#txt_ded_count").val() == "0") {
                        alert("Deduction Amount Are Not Available! Unable To Claim Expense Request...!");
                        $('input[Id="btn_Save"]').attr('disabled', 'disabled');
                        return false;
                    }
                    else {
                        $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
                    }
                    $("#div_UserMobileReimbursement").hide();
                }
                else {
                    $("#div_UserMobileReimbursement").show();
                    $("#div_MobileReimbursement").hide();
                    $("#div_deduction").hide();
                }
                $("#div_DataCard").hide();
                $("#div_DataCard").find("input,select,textarea,button").prop("enabled", true);
                // $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
            } else if (expenseHead == "DataCard") {
                $("#div_UserMobileReimbursement").hide();
                $("#div_MobileReimbursement").hide();
                $("#div_deduction").hide();
                $("#div_DataCard").show();
                if (empcode != "") {
                    $("#div_DataCard").removeAttr("disabled");
                    $("#div_DataCard").find("input,select,textarea,button").prop("disabled", false);
                    $("#btn_Save").removeAttr("disabled");
                }
                else {
                    $("#div_DataCard").attr('disabled', 'disabled');
                    $("#div_DataCard").find("input,select,textarea,button").prop("disabled", true);
                    $('input[Id="btn_Save"]').attr('disabled', 'disabled');
                    alert("You are not allowed to reimburse for Data Card!");
                    return false;
                }
            }
            Mobile_DataCard_Expense.fillGLCode(expenseHead, empad_id, callback_mobile_Detail);
        });
        function callback_mobile_Detail(response) {
            var arr = response.value.split("/");
            $('#lbl_GLCode').html(arr[0]);
            $('#lbl_AppName').html(arr[1]);
            $('#txt_approvar').val(arr[2]);
            $('#txtApproverEmail').val(arr[3]);
            if (arr[2] == "NA" || arr[2] == "") {
                alert("Approver Is Not Available! Unable To Claim Expense Request...!");
                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            }
            else {
                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
            }
            var expenseHead = $("#ddlExpenseHead").val();
            if (expenseHead == "DataCard") {
                $('#span_support').html(arr[4]);
            }
            else if (expenseHead == "Mobile") {
                $('#span_support1').html(arr[4]);
                $('#span_support2').html(arr[4]);
            }

        }
        function callback_app_Detail(response) {
            $('#lbl_AppName').html(response.value);
        }
        $("#ddlMonth1").change(function () {
            var grade = $("#lbl_Grade").text();
            // Mobile_DataCard_Expense.checkexpense(grade, callback_exp_Detail);            
        });
        function callback_amt_Detail(response) {
            $('#txt_Mreimbursement').text(response.value);
            //if (response.value == 0) {
            //    $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            //    alert("No value is assigned for reimbursement amount for this year so you are not allow to reimburse ");
            //}
            //else {
            //    $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
            //}
        }
        function callback_exp_Detail(response) {
            if (response.value == "false") {
                var month = $("#ddlMonth1").val();
                var year = $("#ddlYear1").val();
                var expenseHead = $("#ddlExpenseHead").val();
                var grade = $("#lbl_Grade").text();
                //Mobile_DataCard_Expense.fillReimburmentAmt(year, month, expenseHead, grade, callback_amt_Detail);
            }
            else {
                $('#txt_Mreimbursement').text("0");
            }
        }
        function comdedyear(index) {
            var dyear = parseInt($("#ddldedYear").val());
            var year = parseInt($("#ddlYear" + index).val());
            if (dyear < year) {
                alert("Year should be less than deduction Year!");
            }
        }
        function comdedmonth(index) {
            var dyear = parseInt($("#ddldedYear").val());
            var year = parseInt($("#ddlYear" + index).val());
            var dmonth = parseInt($("#ddlDeduMonth").val());
            var month = parseInt($("#ddlMonth" + index).val());
            if (dyear <= year) {
                if (dmonth < month) {
                    alert("Month should be less than deduction Month!");
                }
            }
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        //function isNumber(evt) {

        //        return false;
        //    return true;
        //}
        function calculate_UserTotal() {
            try {

            }
            catch (ex)
            { }
        }
    </script>
</body>
</html>

