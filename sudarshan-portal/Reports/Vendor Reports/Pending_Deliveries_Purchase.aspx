<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pending_Deliveries_Purchase.aspx.cs" Inherits="Pending_Deliveries_Purchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Pending Payment Report</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
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
                        <h4 class="modal-title"><font color="white"> Pending Payment Report</font></h4>
                    </div>

                    <div class="modal-body" id="div_header" runat="server" style='height:425px; overflow:auto'>
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
<div class="btn-group pull-right" style="margin-top:-5px;">
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" OnClick="btnCancel_Click"/>
						</div>
                        <h3 class="panel-title">Pending Payment Report</h3>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="margin-top: -8px;">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                         
                        <h4 class="panel-title">Pending Payment Report</h4>
                    </div>
                    <div class="panel-body">
                        <table class="table">
                            <tr>
                                <td class="col-md-1">
                                    <asp:DropDownList ID="ddlRecords" runat="server" CssClass="form-control" Style="padding: 5px" onchange="searchData()">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="col-md-2">records per page
                                </td>
                                <td class="col-md-5">&nbsp;
                                </td>
                                <td class="col-md-1" style="text-align: right; vertical-align: middle">
                                    <b>Search :</b>
                                </td>
                                <td class="col-md-2" align="left">
                                    <asp:TextBox ID="txt_Search" runat="server" class="form-control" Text="" onkeyup="searchData();"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="table-responsive" id="div_ReportDetails" runat="server" style="overflow: visible">
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
        </div>

        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../JS/Early_Payment_Request.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../JS/Pending_Deliveries_Purchase.js"></script>

        <script>
            $(document).ready(function () {
                App.init();

            });
        </script>
    </form>
</body>
</html>
