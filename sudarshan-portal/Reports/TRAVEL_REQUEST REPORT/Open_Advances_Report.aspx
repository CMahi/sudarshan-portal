<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Open_Advances_Report.aspx.cs" Inherits="Open_Advances_Report" %>

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
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
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
        <div class="row" style="margin-top: 10px">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">

                        <div class="panel-heading-btn">
                            <div class="btn-group pull-right" style="margin-top: -5px;">
                                <asp:Button ID="Button1" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_Export_onClick" />
                                <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" Style="margin-left: 10px" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                        <h4 class="panel-title">OPEN ADVANCES REPORT</h4>
                    </div>
                    <div class="panel-body">
                        <div class="panel pagination-danger">
                            <div class="row">

                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Adavance Type</b></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlAdvType" runat="server" class="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlAdvType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <b><span id="spn_amt" runat="server">Total Amount : </span></b>
                                        <b><span id="spn_amount" runat="server">0</span></b>
                                    </div>
                                </div>

                            </div>
                            <div class="table-responsive" id="divdata" runat="server">
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div id="conditionButton" style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_totalamt" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtdata" runat="server" Width="1px"></asp:TextBox>
            <input id="ddlUser" runat="Server" type="text" />
        </div>

        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-1.9.1.min.js"></script>
        <script src="../../assets/plugins/jquery/jquery-migrate-1.1.0.min.js"></script>
        <script src="../../assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>

        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
        <script src="../../assets/js/demo.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    </form>
</body>
</html>
