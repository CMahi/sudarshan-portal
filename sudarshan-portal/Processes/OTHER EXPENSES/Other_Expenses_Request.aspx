<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Other_Expenses_Request.aspx.cs" Inherits="Other_Expenses_Request" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Other Expense Request</title>
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
                                    <div id="Div9"><span id="span_Division" runat="server"></span></div>
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
                                
                            </div>
                                    <div class="form-group">
                                        <div class="col-md-1"></div>
                                <label class="col-md-2">Bank Account No. :</label>
                                <div class="col-md-3">
                                        <span id="span_bank_no" runat="server"></span>
                                </div>
                                    <label class="col-md-2">IFSC No.</label>
                                    <div class="col-md-3">
                                        <div id="Div4"><span id="span_Ifsc" runat="server">NA</span></div>
                                    </div>
                            </div>
                                 <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <label class="col-md-2">Base Location</label>
                                    <div class="col-md-3">
                                        <div id="Div8"><span id="spn_base_location" runat="server">NA</span></div>
                                    </div>
                                </div>
                            <div class="form-group">
                                 <div class="col-md-1"></div>
                                <label class="col-md-2">Approver : </label>
                                <div class="col-md-3">
                                    <div id="Div5"><span id="span_Approver" runat="server" style="display: none"></span><span id="span_app_name" runat="server"></span></div>
                                </div>
                                    <label class="col-md-2">Deviation Approver </label>
                                    <div class="col-md-3">
                                        <div id="Div6"><span id="span_DApprover" runat="server" style="display: none"></span><span id="span_Dapp_name" runat="server"></span></div>
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
                        <h3 class="panel-title">OTHER EXPENSE - REQUEST</h3>

                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">
                          
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Payment Mode :</label>
                                <div class="col-md-2">
                                    <div id="Div10">
                                            <asp:DropDownList ID="ddl_Payment_Mode" runat="server" CssClass="form-control input-sm" onchange="enable_disable()">
                                            </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                                <label class="col-md-2" id="pl" style="display:none">Payment Location :</label>
                                <div class="col-md-2" id="pld" style="display:none">
                                        <asp:DropDownList ID="ddlAdv_Location" runat="server" CssClass="form-control input-sm">
                                        </asp:DropDownList>

                                </div>
                            </div>
                        
                            <div class="form-group">
                                <div class="col-md-1"></div>
                                <label class="col-md-2">Remark<font color="#ff0000"><b>*</b></font> :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="req_remark" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Enter Remark"></asp:TextBox>
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
                            <h4 class="panel-title">Total Amount : <span id="spn_Total">0</span></h4>
                        </div>
                        <h4 class="panel-title">Expense Details</h4>
                    </div>
                    <div class="panel-body">
                       
                        <table class="table table-bordered" id="tbl_Data">
                            <thead>
                                <tr class='grey'>
                                    <th style="width: 2%; text-align: center">#</th>
                                    <th style="width: 10%; text-align: center">Expense Head</th>
                                    <th style="width: 10%; text-align: center">Date</th>
                                    <th style="width: 10%; text-align: center">Bill No</th>
                                    <th style="width: 10%; text-align: center">Bill Date</th>
                                    <th style="width: 15%; text-align: center">Particulars</th>
                                    <th style="width: 10%; text-align: center">Amount</th>
                                    <th style="width: 5%; text-align: center" colspan="2">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><span id="index1">1</span></td>
                                    <td><select id="ddlExp_Head1" runat="server" class="form-control input-sm" onchange="changeGL(1)"></select></td>
                                    <%--<td align="center" style="display:none"><span id="spn_GL1">---</span></td>--%>
                                    <td><input type='text' class="form-control input-sm datepicker-rtl" id="date1" readonly /></td>
                                    <td><input type='text' class="form-control input-sm abc" id="bill_no1" /></td>
                                    <td><input type='text' class="form-control input-sm datepicker-rtl" id="bill_date1" readonly /></td>
                                    <td><input type='text' class="form-control input-sm" id="remark1" /></td>
                                    <td><input type='text' class="form-control input-sm numbersOnly" id="amount1" value="0" style="text-align: right; padding-right:10px" onkeyup="calculate_Total()" /></td>
                                    <td id="add1"><a id='add_row1' onclick="addnewrow()"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-plus"></i></a></td>
                                    <td id="rem1" style="display: none"><a id="del_row1" onclick="delete_row(1)"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-trash"></i></a></td>
                                </tr>
                            </tbody>
                        </table>
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
                            <asp:Button ID="btn_Save" runat="server" CssClass="btn btn-danger btn-rounded" Text="Submit" OnClientClick="return PrepareData();" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btn_Cancel" runat="server" CssClass="btn btn-danger btn-rounded" Text="Cancel" OnClick="btnCancel_Click" />
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
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-3">
                                <b>Description :</b>
                            </div>
                            <div class="col-md-7">
                                <asp:TextBox ID="txt_desc" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-3">
                                <b>File :</b>
                            </div>
                            <div class="col-md-7">
                                <asp:AsyncFileUpload ID="FileUpload1" runat="server" OnClientUploadError="uploadError"
                                    OnClientUploadStarted="StartUpload" OnClientUploadComplete="UploadComplete" CompleteBackColor="Lime"
                                    ErrorBackColor="Red" OnUploadedComplete="btnUpload_Click"
                                    UploadingBackColor="#66CCFF" />
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                        <div class="table-responsive" id="div_docs" runat="server">
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
            <asp:TextBox ID="txt_advance_amount" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="dev_total" runat="server" Text="0"></asp:TextBox>
        </div>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../JS/Other_Expense_Request.js"></script>
    </form>
</body>
</html>
