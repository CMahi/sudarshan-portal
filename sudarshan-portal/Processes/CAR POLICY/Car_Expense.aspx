<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Car_Expense.aspx.cs" Inherits="Car_Expense"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Car Expense</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <div class="panel-heading-btn">
                        <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color: blue">
                            <i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                    </div>
                    <h3 class="panel-title">
                        CAR EXPENSE - REQUEST</h3>
                </div>
                <div class="panel-body" id="div_hdr">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-1">
                            </div>
                            <label class="col-md-2">
                                Payment Mode
                            </label>
                            <div class="col-md-2">
                                <div id="Div7">
                                    <asp:DropDownList ID="ddl_Payment_Mode" runat="server" class="form-control  input-sm width-sm-6 ">
                                        <asp:ListItem Value="2">Bank</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1">
                                </div>
                                <label class="col-md-2">
                                    Supporting Documents<font color="#ff0000"><b>*</b></font></label>
                                <div class="col-md-3">
                                    <a href="#div_UploadDocument" data-toggle="modal">
                                        <img id="A_FileUpload1" src="../../Img/attachment3.png" alt="Click here to attach file."
                                            height="20" width="20"></a>
                                </div>
                            </div>
                        </div>
                        <div id="div_loc" runat="server" style="display: none">
                            <div class="col-md-1">
                            </div>
                            <label class="col-md-2">
                                Payment Location</label>
                            <div class="col-md-2">
                                <div id="Div11">
                                    <asp:DropDownList ID="ddlAdv_Location" runat="server" class="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
<div class="form-group">
                            <div class="col-md-1">
                            </div>
                            <label class="col-md-10">
                                Note:<font color="#ff0000">Please Attach Supporting Bills for Fuel/Maintenance/Tyre/Battery whichever is applicable.</font></label>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="div_user_data">
            <div class="modal-dialog" style="width: 90%; margin-left: 5%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            ×</button>
                        <h4 class="modal-title">
                            <font color="white">Requestor's Detail</font></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            Employee No</label>
                                        <div class="col-md-3">
                                            <div id="Div3">
                                                <span id="empno" runat="server"></span>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            Employee Name</label>
                                        <div class="col-md-3">
                                            <div id="EmployeeName">
                                                <span id="span_ename" runat="server"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            Designation</label>
                                        <div class="col-md-3">
                                            <div id="Div2">
                                                <span id="span_designation" runat="server"></span>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            Division</label>
                                        <div class="col-md-3">
                                            <div id="Div9">
                                                <span id="span_Division" runat="server"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            Department</label>
                                        <div class="col-md-3">
                                            <div id="EmployeeDepartment">
                                                <span id="span_dept" runat="server"></span>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            Grade</label>
                                        <div class="col-md-3">
                                            <div id="grade">
                                                <span id="span_grade" runat="server"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            Cost Center</label>
                                        <div class="col-md-3">
                                            <div id="Div2">
                                                <span id="span_cc" runat="server"></span>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            Mobile No.</label>
                                        <div class="col-md-3">
                                            <div id="EmployeePhoneNo">
                                                <span id="span_mobile" runat="server"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            Bank Account No.</label>
                                        <div class="col-md-3">
                                            <div id="Div3">
                                                <span id="span_bank_no" runat="server"></span><span id="ddlTravel_Type" runat="server"
                                                    style="display: none">Domestic</span></div>
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            IFSC No.</label>
                                        <div class="col-md-3">
                                            <div id="Div5">
                                                <span id="span_Ifsc" runat="server">NA</span></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            Approver
                                        </label>
                                        <div class="col-md-3">
                                            <div id="Div9">
                                                <span id="span_Approver" runat="server" style="display: none"></span><span id="span_app_name"
                                                    runat="server"></span>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <label class="col-md-2">
                                            Deviation Approver
                                        </label>
                                        <div class="col-md-3">
                                            <div id="Div10">
                                                <span id="span_DApprover" runat="server" style="display: none"></span><span id="span_Dapp_name"
                                                    runat="server"></span>
                                            </div>
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
        <div style="display: none;" class="modal in" id="div_UploadDocument">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">
                            Document Upload</h5>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1">
                                </div>
                                <label class="col-md-2">
                                    File Type</label>
                                <div class="col-md-6">
                                    <select id="doctype" class="form-control">
                                        <option value="0">---Select One---</option>
                                        <option value="1">Fuel</option>
                                        <option value="2">Maintenance</option>
                                        <option value="3">Driver</option>
                                        <option value="4">Battery</option>
                                        <option value="5">Tyre</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1">
                                </div>
                                <label class="col-md-2">
                                    Attach File</label>
                                <asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <span class="btn btn-grey fileinput-button">
                                                <asp:AsyncFileUpload ID="FileUpload1" Width="250px" runat="server" OnClientUploadError="uploadError"
                                                    OnClientUploadStarted="StartUpload" OnClientUploadComplete="UploadComplete" CompleteBackColor="Lime"
                                                    UploaderStyle="Traditional" ErrorBackColor="Red" OnUploadedComplete="btnUpload_Click"
                                                    UploadingBackColor="#66CCFF" />
                                            </span>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="table-responsive" style="text-align: left; overflow: auto">
                                                    <table id="tbl_DocumentDtl" class="table table-bordered">
                                                        <thead>
                                                            <tr class='grey'>
                                                                <th>
                                                                    File Name
                                                                </th>
                                                                <th>
                                                                    File Type
                                                                </th>
                                                                <th>
                                                                    Delete
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-danger btn-rounded width-100" data-dismiss="modal">
                            Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12" id="tab_CarExpense">
            <div class="panel" style="background-color: grey">
                <ul class="nav nav-tabs">
                    <li id="Li7" class="active" runat="server"><a aria-expanded="true" href="#tab_fuel"
                        data-toggle="tab"><b>Fuel</b></a></li>
                    <li id="li_Maintenance" runat="server"><a aria-expanded="true" href="#tab_maitaince"
                        data-toggle="tab"><b>Maintenance</b></a></li>
                    <li id="Li9" runat="server"><a aria-expanded="true" href="#tab_driver" data-toggle="tab">
                        <b>Driver</b></a></li>
                    <li id="Li1" runat="server"><a aria-expanded="true" href="#tab_battery" data-toggle="tab">
                        <b>Battery</b></a></li>
                    <li id="Li2" runat="server"><a aria-expanded="true" href="#tab_tyre" data-toggle="tab">
                        <b>Tyre</b></a></li>
                    <li id="Li16"><a aria-expanded="true" href="#tab_expense" onclick="getexpensedtl()"
                        data-toggle="tab"><b>Summary</b></a></li>
                    <div class="panel-heading-btn">
                        <a href="#div_car_policy_data" data-toggle="modal" title="Car Policy Details" style="color: blue">
                            <i class="fa fa-fw m-r-10 pull-left f-s-18 fa-automobile"></i></a>
                    </div>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane fade active in" id="tab_fuel">
                        <div class="table-responsive">
                            <table id="tbl_Fuel" class="table table-striped table-bordered">
                                <thead>
                                    <tr class='grey'>
                                        <th style="width: 5%">
                                            #
                                        </th>
                                        <th style="width: 20%">
                                            Date
                                        </th>
                                        <th>
                                            Bill Details
                                        </th>
                                        <th>
                                            Rate
                                        </th>
                                        <th>
                                            Litre
                                        </th>
                                        <th>
                                            Amount
                                        </th>
                                        <th>
                                            Add
                                        </th>
                                        <th>
                                            Delete
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            <div class="input-group">
                                                <input class="form-control input-sm datepicker-dropdown" readonly="" id="fuelDate1"
                                                    type="text">
                                                <span class="input-group-btn">
                                                    <button class="btn btn-danger input-sm" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="perticulars1" type="text">
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="rate1" type="text" onkeypress="return isNumberKey(event)"
                                                onblur="calculate(1);">
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="litre1" type="text" onkeypress="return isNumberKey(event)"
                                                onblur="checkf(1);">
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="amount1" type="text" readonly="readonly">
                                        </td>
                                        <td class="add_Fuel">
                                            <i class="fa fa-fw m-r-10 pull-left f-s-18 fa-plus"></i>
                                        </td>
                                        <td>
                                            <i class="fa fa-fw m-r-10 pull-left f-s-18 fa-trash"></i>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="tab_maitaince">
                        <div class="table-responsive">
                            <table id="tbl_Maintenance" class="table table-striped table-bordered">
                                <thead>
                                    <tr class='grey'>
                                        <th style="width: 5%">
                                            #
                                        </th>
                                        <th style="width: 20%">
                                            Date
                                        </th>
                                        
                                        <th style="width: 10%">
                                            Car Age
                                        </th>
                                        <th style="width: 20%">
                                            Bill Details
                                        </th>
                                        <th style="width: 18%">
                                            Vehicle No
                                        </th>
                                        <th style="width: 18%">
                                            Date Of Purchase
                                        </th>
                                        <th style="width: 20%">
                                            Amount
                                        </th>
                                        <th>
                                            Add
                                        </th>
                                        <th>
                                            Delete
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            <div class="input-group">
                                                <input class="form-control input-sm datepicker-dropdown" readonly="" id="maitainancedate1"
                                                    type="text">
                                                <span class="input-group-btn">
                                                    <button class="btn btn-danger input-sm" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="main_car_age1" runat="server" readonly="" type="text" />
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="main_particulars1" type="text" />
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="vehical1" type="text" />
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="purachasedate1" readonly="" type="text" />
                                        </td>
                                        
                                        <td>
                                            <input class="form-control input-sm" id="maintamount1" type="text" onkeypress="return isNumberKey(event)"
                                                onblur="checkm(1);" />
                                        </td>
                                        <td id="add_Maintenance">
                                            <i class="fa fa-fw m-r-10 pull-left f-s-18 fa-plus"></i>
                                        </td>
                                        <td>
                                            <i class="fa fa-fw m-r-10 pull-left f-s-18 fa-trash"></i>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="tab_driver">
                        <div class="table-responsive">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-8">
                                <table id="tbl_Driver" class="table table-striped table-bordered">
                                    <thead>
                                        <tr class='grey'>
                                            <th style="width: 5%">
                                                #
                                            </th>
                                            <th>
                                                Type
                                            </th>
                                            <th style="width: 30%">
                                                Date
                                            </th>
                                            <th>
                                                Amount
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <input id="chk1" value="" type="checkbox">
                                            </td>
                                            <td>
                                                Salary
                                            </td>
                                            <td>
                                                <div class="input-group" id="Div4">
                                                    <input class="form-control input-sm datepicker-dropdown" id="driver_date1" readonly=""
                                                        type="text">
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-danger input-sm" type="button">
                                                            <i class="fa fa-calendar"></i>
                                                        </button>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <input class="form-control input-sm" id="driver_amt1" type="text" onkeypress="return isNumberKey(event)"
                                                    onblur="checks(1);">
                                            </td>
                                        </tr>
                                        <tr id="tr_uniform" runat="server">
                                            <td>
                                                <input id="chk2" value="" type="checkbox">
                                            </td>
                                            <td>
                                                Uniform
                                            </td>
                                            <td>
                                                <div class="input-group" id="Div6">
                                                    <input class="form-control input-sm datepicker-dropdown" id="driver_date2" readonly=""
                                                        type="text">
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-danger input-sm" type="button">
                                                            <i class="fa fa-calendar"></i>
                                                        </button>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <input class="form-control input-sm" id="driver_amt2" type="text" onkeypress="return isNumberKey(event)"
                                                    onblur="checku(1);">
                                            </td>
                                        </tr>
                                        <tr id="tr_ex_gratia" runat="server">
                                            <td>
                                                <input id="chk3" value="" type="checkbox">
                                            </td>
                                            <td>
                                                Ex-Gratia
                                            </td>
                                            <td>
                                                <div class="input-group" id="Div12">
                                                    <input class="form-control input-sm datepicker-dropdown" id="txt_exdate2" readonly=""
                                                        type="text">
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-danger input-sm" type="button">
                                                            <i class="fa fa-calendar"></i>
                                                        </button>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <input class="form-control input-sm" id="txt_driver_gra2" type="text" onkeypress="return isNumberKey(event)"
                                                    onblur="checkg(1);">
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="tab_battery">
                        <div class="table-responsive">
                            <table id="tbl_battery" class="table table-striped table-bordered">
                                <thead>
                                    <tr class='grey'>
                                        <th style="width: 5%">
                                            #
                                        </th>
                                        <th style="width: 20%">
                                            Date
                                        </th>
                                        <th style="width: 20%">
                                            Bill Details
                                        </th>
                                        <th>
                                            Amount
                                        </th>
                                        <th>
                                            Add
                                        </th>
                                        <th>
                                            Delete
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            <div class="input-group">
                                                <input class="form-control input-sm datepicker-dropdown" readonly="" id="txt_batterydt1"
                                                    type="text">
                                                <span class="input-group-btn">
                                                    <button class="btn btn-danger input-sm" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="battery_particulars1" type="text" />
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="txt_batteryamt1" type="text" onkeypress="return isNumberKey(event)"
                                                onblur="checkb(1);" />
                                        </td>
                                        <td id="add_battery">
                                            <i class="fa fa-fw m-r-10 pull-left f-s-18 fa-plus"></i>
                                        </td>
                                        <td>
                                            <i class="fa fa-fw m-r-10 pull-left f-s-18 fa-trash"></i>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="tab_tyre">
                        <div class="table-responsive">
                            <table id="tbl_tyre" class="table table-striped table-bordered">
                                <thead>
                                    <tr class='grey'>
                                        <th style="width: 5%">
                                            #
                                        </th>
                                        <th style="width: 20%">
                                            Car Age
                                        </th>
                                        <th style="width: 20%">
                                            Date
                                        </th>
                                        <th style="width: 20%">
                                            Bill Details
                                        </th>
                                        <th>
                                            KM Threshold Crossed
                                        </th>
                                        <th>
                                            Kilometers
                                        </th>
                                        <th>
                                            Amount
                                        </th>
                                        <th>
                                            Add
                                        </th>
                                        <th>
                                            Delete
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="txt_CarAge1" runat="server" readonly=""
                                                type="text" />
                                        </td>
                                        <td>
                                            <div class="input-group">
                                                <input class="form-control input-sm datepicker-dropdown" readonly="" id="txt_tyredate_1"
                                                    type="text">
                                                <span class="input-group-btn">
                                                    <button class="btn btn-danger input-sm" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        </td>
                                        
                                        <td>
                                            <input class="form-control input-sm" id="tyre_details1" type="text"/>
                                        </td>
                                        <td>
                                            <input id="km_chk1" value="" type="checkbox">
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="txt_km1" type="text" onkeypress="return isNumberKey(event)"
                                                onblur="checkt(1);" />
                                        </td>
                                        <td>
                                            <input class="form-control input-sm" id="txt_amount_1" type="text" onblur="checkt(1);" onkeypress="return isNumberKey(event)" />
                                        </td>
                                        <td id="add_tyre">
                                            <i class="fa fa-fw m-r-10 pull-left f-s-18 fa-plus"></i>
                                        </td>
                                        <td>
                                            <i class="fa fa-fw m-r-10 pull-left f-s-18 fa-trash"></i>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="tab_expense">
                        <div class="table-responsive">
                            <table id="tbl_expns" class="table table-striped table-bordered">
                                <thead>
                                    <tr class='grey'>
                                        <th style="width: 5%">
                                            #
                                        </th>
                                        <th style="width: 50%">
                                            Expense Head
                                        </th>
                                        <th>
                                            Amount
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="tab-content">
                    <div class="tab-pane active in" id="tab1">
                        <div id="div_travelDetails">
                        </div>
                        <div class="row" id="tab_btn1" runat="server" style="text-align: center;">
                            <asp:Button ID="imgBtnKeep" TabIndex="-1" Text="Submit" CssClass="btn btn-danger  btn-rounded"
                                runat="server" OnClientClick="return createxml();" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12" id="tab_Doc" style="display: none">
            <div class="panel" style="background-color: grey">
                <ul class="nav nav-tabs">
                    <li id="Li10"><a href="#" data-toggle="tab">Travel Request</a></li>
                    <li id="Li11"><a href="#">Hotel Booking</a></li>
                    <li id="Li12"><a href="#">Expense</a></li>
                    <li id="Li13" class="active"><a href="#">Documents</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active in" id="Div1">
                        <div class="table-responsive">
                            <div class="col-md-6">
                            </div>
                            <div class="col-md-6">
                                <div class="table-responsive" id="div_docs" runat="server" style="height: 100px;
                                    overflow: auto">
                                </div>
                            </div>
                        </div>
                        <div class="row" id="Div8" runat="server" style="text-align: center; margin-top: 2%">
                            <a href="#" data-toggle="tab" class="btn btn-grey btn-rounded" onclick="goto3()">Previous</a>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-danger  btn-rounded"
                                OnClick="btnSubmit_Click" OnClientClick="return prepare_data()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="div_car_policy_data">
            <div class="modal-dialog" style="width: 90%; margin-left: 5%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            ×</button>
                        <h4 class="modal-title">
                            <font color="white">Car Policy Detail</font></h4>
                    </div>
                    <div class="modal-body" id="dv_polict_f" runat="server">
                        <div class="row">
                            <h4 class="modal-title">
                                <font color="black"><b>Fuel</b></font></h4>
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div id="fuel_policy" runat="server">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body" id="dv_polictm" runat="server">
                        <div class="row">
                            <h4 class="modal-title">
                                <font color="black"><b>Maintenance</b></font></h4>
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div id="mt_policy" runat="server">
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
    </div>
    <div id="hiddenContent">
        <asp:Button ID="btnTemps" runat="server" Style="display: none" OnClientClick="javascript:void(0);" />
        <asp:ModalPopupExtender ID="MP_Loading" DropShadow="false" runat="server" TargetControlID="btnTemps"
            PopupControlID="pnlPopups" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="pnlPopups" runat="server" Style="display: none; width: 30%; background-color: White"
            BorderColor="Red">
            <asp:UpdatePanel ID="updatePanels1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div align="center" style="border: solid 2px Black">
                        <div>
                            <img alt="Processing" id="img_Progress" src="../../Img/loading_transparent.gif" />
                        </div>
                        <div>
                            <font color="red"><b>Request Is Processing</font>
                        </div>
                    </div>
                    </b>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div style="display: none">
        <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
        <asp:TextBox ID="json_data" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_designation" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_xml_data_fuel" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_xml_data_maitainance" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_xml_data_driver" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_Document_Xml" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_sub_xml_data" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtexpnsamt" runat="server"></asp:TextBox>
        <asp:TextBox ID="Txt_File_xml" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_CarNumber" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_CarDate" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_fule_Dev" runat="server" Text="N"></asp:TextBox>
        <asp:TextBox ID="txt_maintain_Dev" runat="server" Text="N"></asp:TextBox>
        <asp:TextBox ID="txt_xml_data_tyre" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_xml_data_battery" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_car_Age" runat="server"></asp:TextBox>
        <asp:TextBox ID="txt_driversalary" runat="server"></asp:TextBox>
         <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
    </div>
    <!-- ================== BEGIN BASE JS ================== -->
    <script src="../../assets/js/Vaildation.js"></script>
    <script src="../../JS/Car_Expense.js"></script>
    <!--<script src="../../assets/plugins/jquery/jquery-1.9.1.min.js"></script>-->
    <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
    <%--<script src="../../JS/utility.js"></script>--%>
    </form>
    <script type="text/javascript">

        $(document).ready(function () {
            //  $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' });
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })
        });


        $('body').on('click', '#li_Maintenance', function () {
            var veh_no = $("#txt_CarNumber").val();
            var purdate = $("#txt_CarDate").val();

            $("#vehical1").val(veh_no);
            $("#purachasedate1").val(purdate);

        });

        $('body').on('click', '.fuelDate1', function () {
            //  $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' });
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })
        });

        $('body').on('click', '.add_Fuel', function () {
            addNewRowFuel();
        });

        function addNewRowFuel() {
            var lastRow = $('#tbl_Fuel tr').length;
            var lastRow1 = $('#tbl_Fuel tr').length - 1;

            for (var q = 1; q <= lastRow1; q++) {

                var fueldate = $("#fuelDate" + q).val();

                var fuelperticulares = $("#perticulars" + q).val();
                var fuelrate = $("#rate" + q).val();
                var fuellitre = $("#litre" + q).val();
                var fuelamount = $("#amount" + q).val();

                if (fueldate == "") {
                    alert('Please select Date at row :' + q + '');
                    return false;
                }
                if (fuelperticulares == "") {
                    alert('Please Enter perticulars at row :' + q + '');
                    return false;
                }
                if (fuelrate == "") {
                    alert('Please Enter rate at row :' + q + '');
                    return false;
                }
                if (fuellitre == "") {
                    alert('Please Enter Litre at row :' + q + '');
                    return false;
                }
                if (fuelamount == "") {
                    alert('Please Enter amount at row :' + q + '');
                    return false;
                }

            }

            var html = "<tr><td>" + (lastRow1 + 1) + "</td>";
            var html1 = "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown' id='fuelDate" + (lastRow1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html2 = "<td><input class='form-control input-sm' type='text' id='perticulars" + (lastRow1 + 1) + "'></input></td>";
            var html7 = "<td><input class='form-control input-sm' type='text' onkeypress='return isNumberKey(event)' id='rate" + (lastRow1 + 1) + "'  onblur='calculate(" + (lastRow1 + 1) + ")'></input></td>";
            var html3 = "<td><input class='form-control input-sm' type='text' onkeypress='return isNumberKey(event)' id='litre" + (lastRow1 + 1) + "' onblur='checkf(" + (lastRow1 + 1) + ")' ></input></td>";
            var html4 = "<td><input class='form-control input-sm' type='text'  id='amount" + (lastRow1 + 1) + "' readonly='readonly'></input></td>";
            var html5 = "<td class='add_Fuel'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
            var html6 = "<td class='delete_Fuel' ><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td></tr>";
            var htmlcontent = $(html + "" + html1 + "" + html2 + "" + html7 + "" + html3 + "" + html4 + "" + html5 + "" + html6);
            $('#tbl_Fuel').append(htmlcontent);
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })
        }



        $("#tbl_Fuel").on('click', '.delete_Fuel', function () {
            $(this).closest('tr').remove();
        });

        $("#add_Maintenance").click(function () {
            addNewRowMaintenance();
        });

        $("#add_battery").click(function () {
            addBattery();
        });

        $("#add_tyre").click(function () {
            addTyre();
        });


        $('body').on('click', '.add_Maintenance', function () {
            addNewRowMaintenance();
        });


        function addNewRowMaintenance() {
            var lastRow = $('#tbl_Maintenance tr').length;
            var lastRow1 = $('#tbl_Maintenance tr').length - 1;

            for (var w = 1; w <= lastRow1; w++) {
                var maintainancedate = $("#maitainancedate" + w).val();
                var maintainancevehical = $("#vehical" + w).val();
                var maintainancepurchasedate = $("#purachasedate" + w).val();
                var maintainanceamount = $("#maintamount" + w).val();
                
                if (maintainancedate == "") {
                    alert('Please select Date at row :' + w + '');
                    return false;
                }
                if (maintainancevehical == "") {
                    alert('Please Enter Vehical No. at row :' + w + '');
                    return false;
                }
                if (maintainancepurchasedate == "") {
                    alert('Please Select Purchase date at row :' + w + '');
                    return false;
                }
                if (maintainanceamount == "") {
                    alert('Please Enter amount at row :' + w + '');
                    return false;
                }
            }

            var html = "<tr><td>" + (lastRow1 + 1) + "</td>";
            var html1 = "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='maitainancedate" + (lastRow1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html7 = "<td><input class='form-control input-sm' type='text' id='main_car_age" + (lastRow1 + 1) + "' value='" + $("#txt_car_Age").val() + "' readonly></input></td>";
            var html8 = "<td><input class='form-control input-sm' type='text' id='main_particulars" + (lastRow1 + 1) + "' value=''></input></td>";
            var html2 = "<td><input class='form-control input-sm' type='text' id='vehical" + (lastRow1 + 1) + "' value='" + $("#txt_CarNumber").val() + "'></input></td>";
            var html3 = "<td><input type='text' class='form-control' id='purachasedate" + (lastRow1 + 1) + "' readonly value='" + $("#txt_CarDate").val() + " '/></td>";
            var html4 = "<td><input class='form-control input-sm' type='text' id='maintamount" + (lastRow1 + 1) + "' onkeypress='return isNumberKey(event)' onblur='checkm(" + (lastRow1 + 1) + ");'></input></td>";
            var html5 = "<td class='add_Maintenance'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
            var html6 = "<td class='delete_Maitainance'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td></tr>";
            var htmlcontent = $(html + "" + html1 + "" + html7 + "" + html8 + "" + html2 + "" + html3 + "" + html4 + "" + html5 + "" + html6);
            $('#tbl_Maintenance').append(htmlcontent);
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })

        }

        $("#tbl_Maintenance").on('click', '.delete_Maitainance', function () {
            $(this).closest('tr').remove();
        });

        function addTyre() {
            var lastRow = $('#tbl_tyre tr').length;
            var lastRow1 = $('#tbl_tyre tr').length - 1;

            for (var w = 1; w <= lastRow1; w++) {
                var date = $("#txt_tyredate_" + w).val();
                var amount = $("#txt_amount_" + w).val();
                var km = $("#txt_km" + w).val();
                var carage = $("#txt_car_Age").val();
                if (date == "") {
                    alert('Please select Date at row :' + w + '');
                    return false;
                }

                if (km == "") {
                    alert('Please Enter kolimeters at row :' + w + '');
                    return false;
                }

                if (amount == "") {
                    alert('Please Enter amount at row :' + w + '');
                    return false;
                }

                if (km > 39999 && carage < 4) {
                    if ($("#km_chk" + w)[0].checked == false) {
                        alert('Please select check at row :' + w + '')
                        return false;
                    }
                }
            }

            var html = "<tr><td>" + (lastRow1 + 1) + "</td>";
            var html7 = "<td><input class='form-control input-sm' type='text' id='" + $("#txt_car_Age").val() + "'  readonly ></input></td>";
            var html1 = "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='txt_tyredate_" + (lastRow1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html_tyre = "<td><input class='form-control input-sm' type='text' id='tyre_details" + (lastRow1 + 1) + "'></input></td>";
            var html6 = "<td><input id='km_chk" + (lastRow1 + 1) + "' value='' type='checkbox'></input></td>";
            var html5 = "<td><input class='form-control input-sm' type='text' id='txt_km" + (lastRow1 + 1) + "' onkeypress='return isNumberKey(event)' onblur='checkt(" + (lastRow1 + 1) + ");' ></input></td>";
            var html2 = "<td><input class='form-control input-sm' type='text' id='txt_amount_" + (lastRow1 + 1) + "' onkeypress='return isNumberKey(event)' ></input></td>";
            var html3 = "<td class='add_tyre'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
            var html4 = "<td class='delete_tyre'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td></tr>";
            var htmlcontent = $(html + "" + html7 + "" + html1 + html_tyre + "" + html6 + "" + html5 + "" + html2 + "" + html3 + "" + html4);
            $('#tbl_tyre').append(htmlcontent);
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })

        }

        $("#tbl_tyre").on('click', '.delete_tyre', function () {
            $(this).closest('tr').remove();
        });

        $("#tbl_tyre").on('click', '.add_tyre', function () {
            addTyre();
        });


        function addBattery() {
            var lastRow = $('#tbl_battery tr').length;
            var lastRow1 = $('#tbl_battery tr').length - 1;

            for (var w = 1; w <= lastRow1; w++) {
                var date = $("#txt_batterydt" + w).val();
                var amount = $("#txt_batteryamt" + w).val();

                if (date == "") {
                    alert('Please select Date at row :' + w + '');
                    return false;
                }

                if (amount == "") {
                    alert('Please Enter amount at row :' + w + '');
                    return false;
                }
            }

            var html = "<tr><td>" + (lastRow1 + 1) + "</td>";
            var html1 = "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='txt_batterydt" + (lastRow1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
            var html5 = "<td><input class='form-control input-sm' type='text' id='battery_particulars" + (lastRow1 + 1) + "' ></input></td>";
            var html2 = "<td><input class='form-control input-sm' type='text' id='txt_batteryamt" + (lastRow1 + 1) + "' onkeypress='return isNumberKey(event)' onblur='checkb(" + (lastRow1 + 1) + ");' ></input></td>";
            var html3 = "<td class='add_battery'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
            var html4 = "<td class='delete_battery'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td></tr>";
            var htmlcontent = $(html + "" + html1 + "" + html5 + "" + html2 + "" + html3 + "" + html4);
            $('#tbl_battery').append(htmlcontent);
            $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })

        }

        $("#tbl_battery").on('click', '.delete_battery', function () {
            $(this).closest('tr').remove();
        });

        $("#tbl_battery").on('click', '.add_battery', function () {
            addBattery();
        });

        function getexpensedtl() {

            var html = "";
            var html1 = "";
            var html2 = "";
            var html3 = "";
            var html4 = "";
            var html5 = "";
            var html6 = "";
            var html7 = "";
            var amt = 0;
            var amtf = 0;
            var amtm = 0;
            var amtt = 0;
            var amtb = 0;

            $('#tbl_expns tbody').remove();

            var lastRow = $('#tbl_Fuel tr').length;
            var lastRow1 = $('#tbl_Fuel tr').length - 1;

            if (lastRow > 1) {
                for (var n = 1; n <= lastRow1; n++) {

                    var fuelamount = $("#amount" + n).val();
                    if (fuelamount != "") {
                        amtf = parseFloat(amtf) + parseFloat($("#amount" + n).val());
                    }
                }
                if (amtf != 0) {
                    html += "<tr><td></td>";
                    html += "<td>Fuel</td>";
                    html += "<td align='left'>" + amtf + "</td></tr>";
                    amt = parseFloat(amt) + parseFloat(amtf);

                    $('#tbl_expns').append(html);
                }
            }

            var lastRowm = $('#tbl_Maintenance tr').length;
            var lastRow1m = $('#tbl_Maintenance tr').length - 1;

            if (lastRowm > 1) {
                for (var j = 1; j <= lastRow1m; j++) {
                    var maintainancedate = $("#maitainancedate" + j).val();
                    var maintainanceamount = $("#maintamount" + j).val();
                    if (maintainanceamount != "") {

                        amtm = parseFloat(amtm) + parseFloat($("#maintamount" + j).val());
                    }
                }
                if (amtm != 0) {
                    html1 += "<tr><td></td>";
                    html1 += "<td>Maintenance</td>";
                    html1 += "<td align='left'>" + amtm + "</td></tr>";
                    amt = parseFloat(amt) + parseFloat(amtm);
                    $('#tbl_expns').append(html1);
                }
            }
            var lastRow = $('#tbl_Driver tr').length;
            var lastRow1d = $('#tbl_Driver tr').length - 1;

            if ($("#chk1")[0].checked == true) {

                var driveramount = $("#driver_amt1").val();
                html2 += "<tr><td></td>";
                html2 += "<td>Driver Salary</td>";
                html2 += "<td align='left'>" + driveramount + "</td></tr>";
                amt = parseFloat(amt) + parseFloat($("#driver_amt1").val());
                $('#tbl_expns').append(html2);

            }

            if ($("#chk2")[0].checked == true) {

                var driveramount1 = $("#driver_amt2").val();
                html3 += "<tr><td></td>";
                html3 += "<td>Driver Uniform</td>";
                html3 += "<td align='left'>" + driveramount1 + "</td></tr>";
                amt = parseFloat(amt) + parseFloat($("#driver_amt2").val());
                $('#tbl_expns').append(html3);
            }
            if ($("#chk3")[0].checked == true) {

                var driveramount2 = $("#txt_driver_gra2").val();
                html6 += "<tr><td></td>";
                html6 += "<td>Driver Ex-Gratia</td>";
                html6 += "<td align='left'>" + driveramount2 + "</td></tr>";
                amt = parseFloat(amt) + parseFloat($("#txt_driver_gra2").val());
                $('#tbl_expns').append(html6);
            }

            var lastRowb = $('#tbl_battery tr').length;
            var lastRow1b = $('#tbl_battery tr').length - 1;
            if (lastRowb > 1) {
                for (var j = 1; j <= lastRow1b; j++) {
                    var batteryamt = $("#txt_batteryamt" + j).val();
                    if (batteryamt != "") {

                        amtb = parseFloat(amtb) + parseFloat($("#txt_batteryamt" + j).val());
                    }
                }
                if (amtb != 0) {
                    html7 += "<tr><td></td>";
                    html7 += "<td>Battery</td>";
                    html7 += "<td align='left'>" + amtb + "</td></tr>";
                    amt = parseFloat(amt) + parseFloat(amtb);
                    $('#tbl_expns').append(html7);
                }
            }

            var lastRowt = $('#tbl_tyre tr').length;
            var lastRow1t = $('#tbl_tyre tr').length - 1;
            if (lastRowt > 1) {
                for (var j = 1; j <= lastRow1t; j++) {
                    var maintainanceamount = $("#txt_amount_" + j).val();
                    if (maintainanceamount != "") {

                        amtt = parseFloat(amtt) + parseFloat($("#txt_amount_" + j).val());
                    }
                }
                if (amtt != 0) {
                    html5 += "<tr><td></td>";
                    html5 += "<td>Tyre</td>";
                    html5 += "<td align='left'>" + amtt + "</td></tr>";
                    amt = parseFloat(amt) + parseFloat(amtt);
                    $('#tbl_expns').append(html5);
                }
            }

            html4 += "<tr><td></td>";
            html4 += "<td align='right'>Total</td>";
            html4 += "<td align='left'>" + amt + "</td></tr>";
            $('#tbl_expns').append(html4);

        }

        $("#ddl_Payment_Mode").change(function () {

            var paymode = $("#ddl_Payment_Mode").val();
            if (paymode == 2) {
                $("#div_loc").hide();
            }
            else {
                $("#div_loc").show();
            }


        });


    </script>
</body>
<style type="text/css">
    .ms_header
    {
        margin: 0;
        font-family: 'Karla' , sans-serif;
        font-weight: bold;
        color: #317eac;
        text-rendering: optimizelegibility;
        background: linear-gradient(to bottom, rgba(255,255,255,0) 0%,rgba(0,0,0,0.1) 100%);
    }
    
    .modalBackground
    {
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
    
    .modalPopup
    {
        background-color: #ffffdd;
        border-width: 3px;
        border-style: solid;
        border-color: Gray;
        padding: 3px;
        width: 600px;
    }
</style>
</html>
