<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit_Profile.aspx.cs" Inherits="Edit_Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Vendor Details</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style.min.css" rel="stylesheet" />
    
    <!-- ================== END BASE CSS STYLE ================== -->

</head>
<body onload="chk_Data();">

    <form id="form1" runat="server" class="form-horizontal">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div class="row" style="margin-top:1%">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h3 class="panel-title"><b>Personal Details</b></h3>
                    </div>
                    <div class="panel-body" style="width: 90%">
                        <form class="form-horizontal" data-parsley-validate="true" name="demo-form">
                            <div class="form-group">
                                <label class="control-label col-sm-2" for="email">Vendor Code</label>
                                <div class="control-label col-sm-4" style="text-align: left">
                                    <asp:Label ID="txt_VendorCode" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <label class="control-label col-sm-2" for="website">Vendor Name</label>
                                <div class="control-label col-sm-4" style="text-align: left">
                                    <asp:Label ID="txt_VendorName" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2" for="email">Email Address 1</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Email" runat="server" readonly="true" class="form-control" placeholder="Primary Email"></asp:TextBox>
                                </div>
                                 <label class="control-label col-sm-2" for="email">Email Address 2</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Email2" runat="server" readonly="true" class="form-control" placeholder="Secondary Email"></asp:TextBox>
                                </div>
                               
                            </div>
                             <div class="form-group">
                                 <label class="control-label col-sm-2" for="message">Mobile No</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Mobile" runat="server" readonly="true" class="form-control" placeholder="Mobile No"></asp:TextBox>
                                </div>
                                  <label class="control-label col-sm-2" for="website">Website</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Website" runat="server" readonly="true" class="form-control" placeholder="url"></asp:TextBox>
                                </div>
                             </div>
                            <div class="form-group">
                               
                                <label class="control-label col-sm-2" for="message">Contact No 1</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Contact" runat="server" readonly="true" class="form-control" placeholder="Primary Contact No"></asp:TextBox>
                                </div>
                                <label class="control-label col-sm-2" for="message">Contact No 2</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Contact2" runat="server" readonly="true" class="form-control" placeholder="Secondary Contact No"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2" for="message">Fax No</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Fax" runat="server" readonly="true" class="form-control" placeholder="Fax No"></asp:TextBox>
                                </div>
                                <label class="control-label col-sm-2">PAN No</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_PAN" runat="server" class="form-control" readonly="true" placeholder="PAN No"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2" for="message">Address</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Address" runat="server" readonly="true" class="form-control" placeholder="Address" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <label class="control-label col-sm-2">ECC NO</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_ECC" runat="server" class="form-control" readonly="true" placeholder="ECC NO"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">State</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_State" runat="server" readonly="true" class="form-control" placeholder="State"></asp:TextBox>
                                </div>

                                <label class="control-label col-sm-2">Central Sales Tax No</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Central" runat="server" class="form-control" readonly="true" placeholder="Central Sales Tax No"></asp:TextBox>
                                </div>

                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2" for="message">City</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_City" runat="server" readonly="true" class="form-control" placeholder="City"></asp:TextBox>
                                </div>
                                <label class="control-label col-sm-2">Local Sales Tax No</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Local" runat="server" class="form-control" readonly="true" placeholder="Local Sales Tax No"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">Pin Code</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Pin" runat="server" readonly="true" class="form-control" placeholder="Pin Code"></asp:TextBox>
                                </div>
                                <label class="control-label col-sm-2">Excise Reg No</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_Excise" runat="server" class="form-control" readonly="true" placeholder="Excise Reg No"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group" style="display:none">
                                <label class="control-label col-sm-2">Registered Under MSMED?</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="rblMSMED" runat="server" onchange="chk_Data()">
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No" Selected="True">No</asp:ListItem>
                                    </asp:DropDownList>
                                 </div>
                                <label class="control-label col-sm-2" id="lblchk" runat="server" style="display:none">Reg Certificate</label>
                                <div class="col-sm-4" id="divchk" runat="server" style="display:none" >
                                    <a href="#" id="uploaded_file" onclick="downloadfiles()"></a>
                                </div>
                            </div>
                            <div class="form-group" id="div_msmed_fileupload" style="display:none">
                                 <label class="control-label col-sm-2">MSMED Reg No</label>
                                <div class="col-sm-4" >
                                    <asp:TextBox ID="txt_msmed_rno" runat="server" class="form-control" placeholder="MSMED Reg No"></asp:TextBox>
                                </div>
                                <label class="control-label col-sm-2" id="lblUpload">Upload Reg Certificate</label>
                                <div class="col-sm-4" id="div_fileUpload">
                                    <div class="col-md-9">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                    <div class="col-md-3">
                                    <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="btnUpload_Click"  />
                                    </div>
                                    <asp:Label ID="lblFile" runat="server"></asp:Label>
                                     <asp:TextBox ID="txt_filename" runat="server" style="display:none"></asp:TextBox>
                                </div>
                            </div>
                           
                            <div style="display: none">
                                <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Website" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Mobile" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Contact" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Fax" runat="server"></asp:TextBox>
                                <asp:TextBox ID="PAN" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Address" runat="server"></asp:TextBox>
                                <asp:TextBox ID="ECC" runat="server"></asp:TextBox>
                                <asp:TextBox ID="City" runat="server"></asp:TextBox>
                                <asp:TextBox ID="State" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Pin" runat="server"></asp:TextBox>
                                <asp:TextBox ID="CentralSales" runat="server"></asp:TextBox>
                                <asp:TextBox ID="LocalSales" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Excise" runat="server"></asp:TextBox>
                                <asp:TextBox ID="bankid" runat="server"></asp:TextBox>
                                <asp:TextBox ID="accountno" runat="server"></asp:TextBox>
                                <asp:TextBox ID="ifsc" runat="server"></asp:TextBox>
                                <asp:TextBox ID="branch" runat="server"></asp:TextBox>
                               
                                <asp:TextBox ID="txt_uploaded" runat="server"></asp:TextBox>
                                <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
                                <asp:TextBox ID="last_updated" runat="server"></asp:TextBox>
                                 <asp:TextBox ID="is_changed" runat="server" Text="1"></asp:TextBox>
                            </div>
                        </form>
                    </div>
                </div>

            </div>
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h3 class="panel-title"><b>Bank Details</b></h3>
                    </div>
                    <div class="panel-body" style="width: 90%">
                        <%--<form class="form-horizontal" data-parsley-validate="true" name="demo-form">--%>
                            <div class="form-group">
                                <label class="control-label col-sm-2" for="email">Bank Name</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_BankName" readonly="true" runat="server" class="form-control" placeholder="Bank Name">
                                    </asp:TextBox>
                                </div>
                                <label class="control-label col-sm-2" for="website">Account Number</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_accno" readonly="true" runat="server" class="form-control" placeholder="Account Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2" for="email">IFSC Code</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_ifsc" readonly="true" runat="server" class="form-control" placeholder="IFSC Code"></asp:TextBox>
                                </div>
                                <label class="control-label col-sm-2" for="website">Branch</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_branch" readonly="true" runat="server" class="form-control" placeholder="Branch"></asp:TextBox>
                                </div>
                            </div>
                        <%--</form>--%>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h3 class="panel-title"><b>Action</b></h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group m-b-0">
                            <label class="control-label col-sm-5"></label>
                            <div class="col-sm-6">
                                  <%--      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>--%>
                                        <!--<asp:Button ID="btnUpdate" runat="server" class="btn btn-grey btn-rounded m-b-5" Text="Update" OnClientClick="return chkValidation();" OnClick="btnUpdate_Click"/>-->
                                        <asp:Button ID="btnClose" runat="server" class="btn btn-danger btn-rounded m-b-5" Text="Close" OnClick="btnClose_Click" />
                                          <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                    
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      
    </form>

    <!-- ================== BEGIN BASE JS ================== -->
    <script src="../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script src="../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <%--<script src="../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>--%>
    <script src="../JS/Edit_Vendor_Profile.js"></script>
    <!-- ================== END PAGE LEVEL JS ================== -->

</body>
</html>
