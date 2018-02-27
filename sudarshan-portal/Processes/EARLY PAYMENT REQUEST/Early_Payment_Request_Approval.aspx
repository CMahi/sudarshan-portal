<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Early_Payment_Request_Approval.aspx.cs" Inherits="Early_Payment_Request_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <!-- ================== END BASE CSS STYLE ================== -->
        <style type="text/css">
.sample
{
background-color:#DC5807;
border:1px solid black;
border-collapse:collapse;
color:White;
}
</style>
</head>
<body>
    <form id="form1" runat="server">

       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        
        <div class="row">            
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 30%">
                                    <h3 class="panel-title"><b>Early Payment Request Approval</b></h3>
                                </td>
                                <td style="width: 30%; text-align: right">
                                    <b>Request Initiation Date : </b>
                                    <span id="initDate" runat="server"></span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div id="div_header" runat="server">
                </div>
            </div>
            <div class="col-md-12" style="text-align: center;">
                <asp:Button ID="btnRequest" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Approve" OnClick="btnRequest_Click" OnClientClick="return prepareData()" />
                <asp:Button ID="btnReject" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Reject" OnClick="btnReject_Click" OnClientClick="return prepareData()" />
                <asp:Button ID="btnClose" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Cancel" OnClick="btnClose_Click" />
            </div>
        </div>
        <div class="modal fade" id="incoterm">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">INCO Terms <font color="white">  >> PO Number : <span  id="div_IcoTemSetPoNo" runat="server"></span></font></h4>
                    </div>
                    <div class="modal-body">
                        <div id="div_Inco" runat="server">
                            <table class='table table-bordered'>
                                <thead>
                                    <tr class='grey'>
                                        <th>Inco Terms (Part 1)</th>
                                        <th>Inco Terms (Part 2)</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Inco1" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="Inco2" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </tbody>

                            </table>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="paymentterm">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Payment Terms<font color="white">  >> PO Number : <span  id="div_PaymentTemSetPoNo" runat="server"></span></font></h4>
                    </div>
                    <div class="modal-body">
                        <div id="div_Payment" runat="server">
                            <table class='table table-bordered'>
                                <thead>
                                    <tr class='grey'>
                                        <th>Day Limit</th>
                                        <th>Calendar Day for Payment</th>
                                        <th>Days from Baseline Date for Payment</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Pay1" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="Pay2" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="Pay3" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-grey">
                                    <div class="panel-heading">
                                        <h3 class="panel-title"><b>Personal Details</b></h3>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <b>Name :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <b>Email :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">&nbsp;</div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <b>Mobile No :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <b>Contact No :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblContact" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">&nbsp;</div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <b>ECC NO :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblEcc" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <b>Central Tax No :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblCentral" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">&nbsp;</div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <b>Local Tax No :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblLocal" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <b>Excise Reg No :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblExcise" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">&nbsp;</div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <b>PAN No :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblPan" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <b>Fax No :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">&nbsp;</div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-grey">
                                    <div class="panel-heading">
                                        <h3 class="panel-title"><b>Bank Details</b></h3>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <b>Bank Name :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblBank" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <b>Account No :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblAccount" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-12">&nbsp;</div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <b>IFSC Code :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblIFSC" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <b>Branch :</b>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblBranch" runat="server" Text=""></asp:Label>
                                    
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
         
        <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Dispatch" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Condition" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Step" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Remark" runat="server"></asp:TextBox>
            <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
              <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_PO" runat="server"></asp:TextBox>
            
        </div>
        
         <div id="DisableDiv"> </div>
    </form>
    <!-- ================== BEGIN BASE JS ================== -->
    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>--%>
    <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
	<script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../../JS/Early_Payment_Request_Approval.js"></script>
    <script src="../../assets/js/apps.min.js"></script>
    
        

    <!-- ================== END PAGE LEVEL JS ================== -->
    
    <script>
        $(document).ready(function () {
            App.init();
        });
	</script>
</body>
</html>
