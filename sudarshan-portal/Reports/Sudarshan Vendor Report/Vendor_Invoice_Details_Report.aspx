<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeFile="Vendor_Invoice_Details_Report.aspx.cs" Inherits="Vendor_Invoice_Details_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Invoice Details Report</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />
    <!-- ================== END BASE CSS STYLE ================== -->
    <style type="text/css">
        .headStyle {
            height: 36px;
            padding-top: 10px;
            background-color: grey;
        }

        .headContent {
            height: 100%;
            color: white;
            font-weight: bold;
            text-align: center;
        }

        .rowContent {
            text-align: center;
        }

        .m-b-10:hover {
            -moz-box-shadow: 0 0 10px pink;
            -webkit-box-shadow: 0 0 10px pink;
            box-shadow: 0 0 10px pink;
        }

        .excel {
            width: 100%;
            text-align: right;
        }

        .imgBtn {
            width: 50px;
            text-align: center;
        }
    </style>
</head>
<body style="overflow-x: hidden">
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

                    <div class="modal-body" id="div_header" runat="server" data-scrollbar="true" data-height="425px">
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
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" Style="margin-left: 10px" OnClick="btnCancel_Click" />
                        </div>
                        <h3 class="panel-title"><b>Invoice Details Report</b></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="margin-top: -8px;">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <div class="panel-heading">
                            <div class="btn-group pull-right" style="margin-top: -5px;">
                                <div class="col-md-6" style="text-align: right">
                                    <b>PO Status : </b>
                                </div>
                                <div class="col-md-6" style="text-align: left">
                                    <asp:DropDownList ID="ddlStatus" Width="105px" runat="server" CssClass="form-control input-sm" onchange="searchData()">
                                        <asp:ListItem Value="Open">Open</asp:ListItem>
                                        <asp:ListItem Value="Close">Close</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <h4 class="panel-title">Invoice Details</h4>
                        </div>
                    </div>
                    <div class="panel panel-body">
                        <div class="form-horizontal">
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
                                        <asp:Button ID="btn_Submit" runat="server" class="btn btn-danger btn-rounded m-b-3" Text="Submit" OnClick="submit" OnClientClick="return submitvalidation()" />
                                        <asp:Button ID="btn_Clear" runat="server" class="btn btn-danger btn-rounded m-b-3" Text="Clear" OnClientClick="return Clear()" OnClick="btnClear_Click" />

                                          <div class="table-responsive" id="div_reportDetails" runat="server">
                                         </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                          
                            <hr />
                            <div class="row">
                                <div class="col-md-2">
                                    <span><b>RJ : </b></span><span>Rejected</span>
                                </div>
                                <div class="col-md-2">
                                    <span><b>TR : </b></span><span>Transition</span>
                                </div>
                                <div class="col-md-2">
                                    <span><b>GRN : </b></span><span>GRN Complete</span>
                                </div>
                                <div class="col-md-2">
                                    <span><b>QC : </b></span><span>QC-Approved</span>
                                </div>
                                <div class="col-md-2">
                                    <span><b>QCD : </b></span><span>QC-Approved under Deviation</span>
                                </div>
                                <div class="col-md-2">
                                    <span><b>QCDD : </b></span><span>QC-Approved under Deviation with Deduction</span>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <span><b>PP : </b></span><span>Payment Processed</span>
                                </div>
                                <div class="col-md-2">
                                    <span><b>BB : </b></span><span>Bill Booked</span>
                                </div>
                                <div class="col-md-2">
                                    <span><b>ER : </b></span><span>Early Payment Request</span>
                                </div>

                                <div class="col-md-2">
                                    <span><b>P1E : </b></span><span>Part 1</span>
                                </div>
                                <div class="col-md-2">
                                    <span><b>P2E : </b></span><span>Part 2</span>
                                </div>
                            </div>
                            <div class="row"></div>
                            <div class="row">
                                <div class="col-md-2">
                                    <span>
                                        <img src="../../images/2.png" /><b> : </b></span><span>Completed</span>
                                </div>
                                <div class="col-md-2">
                                    <span>
                                        <img src="../../images/1.png" /><b> : </b></span><span>Not Completed</span>
                                </div>
                                <div class="col-md-2">
                                    <span>
                                        <img src="../../images/0.png" /><b> : </b></span><span>Rejected</span>
                                </div>
                                <div class="col-md-3">
                                    <span>
                                        <img src="../../images/3.png" /><b> : </b></span><span>QC-Approved under Deviation</span>
                                </div>
                                <div class="col-md-3">
                                    <span>
                                        <img src="../../images/4.png" /><b> : </b></span><span>QC-Approved under Deviation with Deduction</span>
                                </div>

                            </div>
                        </div>
                    </div>
            </div>
        </div>
        <div style="display: none">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
                    <asp:TextBox ID="app_Authority" runat="server"></asp:TextBox>
                    <asp:TextBox ID="ddlText" runat="server"></asp:TextBox>
                    <asp:TextBox ID="lnkText" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Data" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Export_HDR" runat="server"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
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
        <script src="../../JS/Vendor_Invoice_Details_Report.js"></script>
        <script>
            $(document).ready(function () {
                App.init();
            });
        </script>
    </form>
</body>
</html>
