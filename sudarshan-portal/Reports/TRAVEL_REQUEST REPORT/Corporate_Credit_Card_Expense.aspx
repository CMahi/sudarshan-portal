﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Corporate_Credit_Card_Expense.aspx.cs" Inherits="Corporate_Credit_Card_Expense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />

    <title>Request Status Report</title>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="modal fade" id="Documents">
            <div class="modal-dialog" style="height: auto; width: 45%; margin-left: 25%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Documents</font></h4>
                    </div>
                    <div class="modal-body" id="DivDocs" runat="server" data-scrollbar="true" data-height="425px">
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 10px">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">

                        <div class="panel-heading-btn">
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" OnClick="btnCancel_Click" Font-Bold="true" />
                        </div>
                        <h4 class="panel-title">REIMBURSEMENT WISE REPORT</h4>
                    </div>
                    <div class="panel-body">
                        <div class="panel pagination-danger">
                            <div class="row">
                                <div class="col-md-1"></div>
                                <div class="col-md-1">
                                    From Date :
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txt_f_date" runat="server" CssClass="form-control input-sm datepicker-rtl"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    To Date :
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txt_t_date" runat="server" CssClass="form-control input-sm datepicker-rtl"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    Reim. Type :
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddl_Reim_Type" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <input type="button" id="btnSubmit" value="SUBMIT" class="btn btn-grey btn-rounded m-b-5" onclick="get_data()" />
                                </div>
                            </div>
                            <hr />

                            <div class="row" style="margin-top:2%">
                                <div class="col-md-1">
                                    <asp:DropDownList ID="ddlRecords" runat="server" onchange="get_data()" CssClass="form-control" Style="padding: 5px">
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>500</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    records per page
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-1" style="text-align: right">
                                    <b>Status :</b>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control input-sm" onchange="get_data()">
                                        <asp:ListItem Value="">---Select One---</asp:ListItem>
                                        <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                        <asp:ListItem Value="Inprogress">Inprogress</asp:ListItem>
                                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-1"></div>

                                <div class="col-md-1" style="text-align: right">
                                    <b>Search : </b>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txt_Search" runat="server" class="form-control" onkeyup="get_data();"></asp:TextBox>
                                </div>
                            </div>

                            <div class="table-responsive" id="div_InvoiceDetails" runat="server" style="height: auto; overflow: hidden">
                            </div>
                             <div class="row" style="float:right">
                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" class="btn btn-sm btn-danger" OnClick="btnExport_Click"/>
                        </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Username1" runat="server"></asp:TextBox>
            <asp:TextBox ID="lnkText" runat="server" Text="1"></asp:TextBox>
            <asp:TextBox ID="ddlText" runat="server" Text="1"></asp:TextBox>
            <asp:TextBox ID="ddlText1" runat="server" Text="1"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Condition" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Dispatch" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="split_data" runat="server"></asp:TextBox>
            <input id="ddlUser" runat="Server" type="text" />

            <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Initiator" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_designation" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
            <asp:TextBox ID="pmode" runat="server"></asp:TextBox>
            <asp:TextBox ID="plocation" runat="server"></asp:TextBox>
            <asp:TextBox ID="pageno" runat="server" Text="1"></asp:TextBox>
        </div>

        <!-- ================== BEGIN BASE JS ================== -->

        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script src="../../JS/Corporate_Credit_Card_Expense.js"></script>


    </form>
</body>
</html>
