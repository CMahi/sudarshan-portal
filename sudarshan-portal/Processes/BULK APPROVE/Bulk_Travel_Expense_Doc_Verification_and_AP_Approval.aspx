<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.aspx.cs" Inherits="Bulk_Travel_Expense_Doc_Verification_and_AP_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />

    <title>Bulk Approve</title>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />
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
          <div class="modal fade" id="req_Details">
            <div class="modal-dialog" style="height: auto; width: 95%; margin-left: 3%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Request Details</font></h4>
                    </div>
                    <div class="modal-body" id="divReq_Details" runat="server">

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
                        <div class="btn-group pull-right" style="margin-top: -5px;">
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" OnClick="btnCancel_Click" Font-Bold="true" />
                        </div>
                        <h4 class="panel-title">BULK DOCUMENT VERIFICATION</h4>
                    </div>
                    <div class="panel-body">
                        <div class="panel pagination-danger">
                            <div class="row" style="display:none">
                                <div class="col-md-1">
                                    <asp:DropDownList ID="ddlRecords" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRecords_SelectedIndexChanged" CssClass="form-control" Style="padding: 5px; display:none">
                                        <%--<asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>--%>
                                        <asp:ListItem Selected="True">100</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    
                                </div>
                                <div class="col-md-6">
                                    &nbsp;
                                </div>
                                <div class="col-md-1" style="text-align: right">
                                    
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txt_Search" runat="server" class="form-control" placeholder="Process Name" onkeyup="searchData();" style="display:none"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-md-2" style="text-align:right; font-weight:bold">Payment Mode : </div>
                                <div class="col-md-1">
                                    <asp:DropDownList ID="ddlPay_mode" runat="server" CssClass="form-control input-sm" Style="padding: 5px;" onchange="searchData()">
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Bank</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4"></div>
                            </div>
                            <div class="table-responsive" id="div_InvoiceDetails" runat="server" style="height: auto; overflow: hidden">
                            </div>

                        </div>
                        <div class="col-md-12" style="text-align: center;">
                            <asp:Button ID="btnRequest" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Approve" OnClick="btnRequest_Click" OnClientClick="return prepareData()" Enabled="true" />
                            <asp:Button ID="btnReject" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Reject" OnClick="btnReject_Click" OnClientClick="return prepareData()" Enabled="true" />
                            <asp:Button ID="btnClose" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Cancel" OnClick="btnClose_Click" Enabled="true" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Username1" runat="server"></asp:TextBox>
            <asp:TextBox ID="lnkText" runat="server"></asp:TextBox>
            <asp:TextBox ID="ddlText" runat="server"></asp:TextBox>
            <asp:TextBox ID="ddlText1" runat="server"></asp:TextBox>
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
            <asp:TextBox ID="txtApproverEmail" runat="server"></asp:TextBox>
            <asp:TextBox ID="No_of_records" runat="server" Text="0"></asp:TextBox>
            <asp:TextBox ID="chk_records" runat="server"></asp:TextBox>
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

        <script src="../../JS/Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.js"></script>
                     <script>
                         $(document).ready(function () {
                             App.init();
                             Demo.init();
                             PageDemo.init();
                         });
            </script>
    </form>
</body>
</html>
