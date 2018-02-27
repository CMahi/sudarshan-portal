<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_Plant_Mapping_Report.aspx.cs" Inherits="User_Plant_Mapping_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Dispatch Note Report</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
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
        <div class="modal fade" id="paymentterm">
            <div class="modal-dialog" style="height: auto; width: 98%; margin-left: 1%">
                <div class="modal-content" style="background-color: ButtonFace">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white"> Dispatch Request Details </font></h4>
                    </div>

                    <div class="modal-body" id="div_header" runat="server" style='height: 425px; overflow: auto'>
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
                        <div class="btn-group pull-right" style="margin-top: -5px;">
                            <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-sm btn-inverse" OnClick="btn_Export_onClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" Style="margin-left: 10px" OnClick="btnCancel_Click" />
                        </div>
                        <h3 class="panel-title">Dispatch Note Report</h3>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="margin-top: -8px;">
                <div class="panel panel-grey">
                    <div class="panel-heading">

                        <h4 class="panel-title">Dispatch Note Details</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class='form-group'>
                                <label class="col-md-2 control-label ui-sortable">Vendor Name</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddl_Vendor" class="form-control" runat="server" OnSelectedIndexChanged="selectedpo" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <label class="col-md-2 control-label ui-sortable">PO Number</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddl_PO" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <label class="col-md-2 control-label ui-sortable">Plant</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddl_Plant" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class='form-group'>
                                <label class="col-md-2 control-label ui-sortable">From Date</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txt_form_Date" class="form-control" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalenderFromDate" runat="server" TargetControlID="txt_form_Date" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                </div>
                                <label class="col-md-2 control-label ui-sortable">To Date</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="text_To_Date" class="form-control" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarToDate" runat="server" TargetControlID="text_To_Date" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="text-center">
                                <asp:UpdatePanel ID="update" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btn_Submit" runat="server" class="btn btn-danger btn-rounded m-b-3" Text="Submit" OnClick="btn_SubmitData" OnClientClick="return Dispatch_Detail()"/>
                                        <asp:Button ID="btn_Clear" runat="server" class="btn btn-danger btn-rounded m-b-3" Text="Clear" OnClick="btnClear_Click" OnClientClick="return Clear()"/>

                                        <div class="table-responsive" id="div_ReportDetails" runat="server"> </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                             
                        </div>
                    </div>
                </div>
            </div>
            </div> 
       
            <div class="modal fade" id="vendorinfo">
                <div class="modal-dialog" style="width: 63%;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title"><font color="white"> Vendor Details </font></h4>
                        </div>
<div id="divIns" runat="server" style="display:none">
            <div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent.gif" style="background-color:Aqua;position:fixed; top:40%; left:46%;"/></div>
        </div>
                        <div class="modal-body" id="div_vendor" runat="server" data-scrollbar="true" data-height="400px">
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
                <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
                <asp:TextBox ID="app_Authority" runat="server"></asp:TextBox>
                <asp:TextBox ID="txt_Export" runat="server"></asp:TextBox>
            </div>

            <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
            <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
            <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
            <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
            <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
            <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
            <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
            <script src="../../assets/js/demo.min.js"></script>
            <script src="../../assets/js/apps.min.js"></script>

            <script src="../../JS/User_Plant_Mapping_Report.js"></script>

            <script>
                $(document).ready(function () {
                    App.init();

                });
            </script>
    </form>
</body>
</html>
