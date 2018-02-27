<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODispatchDeatails_Approval.aspx.cs" Inherits="PODispatchDeatails_Approval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <!-- ================== END BASE CSS STYLE ================== -->
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                       <h3 class="panel-title"><b>PO Dispatch Request Approval</b></h3>
                    </div>
                </div>
            </div>
              <div class="col-md-12" id="div3">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">PO Header</h4>
                    </div>
                    <div class="panel-body">
                         <div class="table-responsive">
                             <div id="div_header" runat="server">
                             </div>
                         </div>
                    </div>
                </div>
            </div>
              <div class="col-md-12" id="div4">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">PO Detail</h4>
                    </div>
                    <div class="panel-body">
                         <div class="table-responsive">
                            <div id="div_detail" runat="server">
                            </div>
                         </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" id="div5">
                <div class="panel panel-grey">
                   <div class="panel-body">
                         <div class="table-responsive">
                           <div id="div_doc" runat="server">
                           </div>
                         </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="margin-top: 10px;" id="div2">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">Action</h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-3"></div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddl_Action" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnRequest" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Submit" OnClick="btnRequest_Click" OnClientClick="return prepareData()" />
                            <asp:Button ID="btnClose" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Cancel" OnClick="btnClose_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="margin-top: 10px;" id="div1">
                <div class="panel panel-grey">
                    <div class="panel-heading">
                        <h4 class="panel-title">Audittrail</h4>
                    </div>
                    <div class="panel-body">
                        <div id="div_Audit" runat="server">
                        </div>
                    </div>
                </div>
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

                    <div class="modal-body" id="div_vendor" runat="server" style="height:400px; overflow:auto">
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
        <div class="modal fade" id="schedule">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Schedule<font color="white">  >> PO Number : <span id="div_SchedulePO"></span></font></h4>
                    </div>
                    <div class="modal-body">
                        <div style="height:225px; overflow:auto">
                            <div id="div_Schedule" runat="server"></div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>

                    </div>
                </div>
            </div>
            .
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
            <asp:TextBox ID="txt_Approver" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_In_Date" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Mat_no" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Po_Number" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Audit" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Vendor_MAilid" runat="server"></asp:TextBox>
            <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Unique_No" runat="server"></asp:TextBox>
	     <asp:TextBox ID="txtApproverEmail" runat="server"></asp:TextBox>
        </div>

    </form>
    <!-- ================== BEGIN BASE JS ================== -->
    <script language="JavaScript" type="text/javascript" src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script src="../../assets/plugins/jquery/jquery-migrate-1.1.0.min.js"></script>
    <script src="../../assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js"></script>
    <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../../assets/plugins/jquery-cookie/jquery.cookie.js"></script>
    <script src="../../assets/js/demo.min.js"></script>
    <script src="../../assets/js/apps.min.js"></script>
    <script src="../../JS/PODispatchDeatails_Approval.js"></script>
    <!-- ================== END PAGE LEVEL JS ================== -->
    <script>
        $(document).ready(function () {
            App.init();
        });

    </script>
</body>
</html>
