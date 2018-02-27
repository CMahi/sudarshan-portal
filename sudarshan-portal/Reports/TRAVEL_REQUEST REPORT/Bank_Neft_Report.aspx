﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bank_Neft_Report.aspx.cs" Inherits="Bank_Neft_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Travel Request</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />

    <script type="text/javascript">
        function OnLoad_Date() {
            $(".datepicker-rtl").datepicker({
                format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked'
            })
        }

        function check_bank() {
            $("#gvhdfc2").html("");
            $("#gvSelected").html("");
            if ($("#ddlBank option:selected").index() == 0) {
                alert("Please Select Bank...!");
                return false;
            }
            else {
                if ($("#from_date").val() == "") {
                    alert("Please Select From Date...!");
                    return false;
                }
                else {
                    if ($("#to_date").val() == "") {
                        alert("Please Select To Date...!");
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            }
            return true;
        }
    </script>
</head>
<body onload="OnLoad_Date();">
    <form id="form1" runat="server">
       
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                          <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>BANK DOCUMENT GENERATION</h3>
                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="row">
                            
                            <div class="col-md-1"><b>From Date : </b></div>
                            <div class="col-md-2">
                                <asp:TextBox ID="from_date" runat="server" CssClass="form-control input-sm datepicker-rtl"></asp:TextBox>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1"><b>To Date</b></div>
                            <div class="col-md-2">
                                <asp:TextBox ID="to_date" runat="server" CssClass="form-control input-sm datepicker-rtl"></asp:TextBox>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1"><b>Select Bank : </b></div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnData" runat="server" Text="Submit" CssClass="btn btn-danger" OnClientClick="return check_bank()" OnClick="btnData_Click" />
                            </div>
                            <div class="col-md-3"></div>
                        </div>
                        <div id="hideshow" runat="server">
                        <div class="table-responsive">
                            <div class="form-group">
                                <div class="col-md-12">
            
                                    <asp:GridView ID="gvhdfc2" AutoGenerateColumns="false" CellPadding="5" runat="server" CssClass="table table-bordered" DataKeyNames="Instruction Reference Number" >
                                        <HeaderStyle BackColor="gray" ForeColor="White" />
                                        <Columns>
                                             <asp:BoundField DataField="Transaction Type" SortExpression="Transaction Type" HeaderText="Transaction Type" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Blank1" SortExpression="Blank1" HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Beneficiary Account Number" SortExpression="Beneficiary Account Number" HeaderText="Beneficiary Account Number" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Instrument Amount" SortExpression="Instrument Amount" HeaderText="Instrument Amount" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Beneficiary Name" SortExpression="Beneficiary Name" HeaderText="Beneficiary Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Blank2" SortExpression="Blank2" HeaderText="Blank2" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Blank3" SortExpression="Blank3" HeaderText="Blank3" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Bene Address 1" SortExpression="Bene Address 1" HeaderText="Bene Address 1" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Bene Address 2" SortExpression="Bene Address 2" HeaderText="Bene Address 2" ItemStyle-HorizontalAlign="right" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Bene Address 3" SortExpression="Bene Address 3" HeaderText="Bene Address 3" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Bene Address 4" SortExpression="Bene Address 4" HeaderText="Bene Address 4" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Bene Address 5" SortExpression="Bene Address 5" HeaderText="Bene Address 5" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Instruction Reference Number" SortExpression="Instruction Reference Number" HeaderText="Instruction Reference Number" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Customer Reference Number" SortExpression="Customer Reference Number" HeaderText="Customer Reference Number" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Payment details 1" SortExpression="Payment details 1" HeaderText="Payment details 1" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Payment details 2" SortExpression="Payment details 2" HeaderText="Payment details 2" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Payment details 3" SortExpression="Payment details 3" HeaderText="Payment details 3" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Payment details 4" SortExpression="Payment details 4" HeaderText="Payment details 4" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Payment details 5" SortExpression="Payment details 5" HeaderText="Payment details 5" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Payment details 6" SortExpression="Payment details 6" HeaderText="Payment details 6" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Payment details 7" SortExpression="Payment details 7" HeaderText="Payment details 7" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Blank4" SortExpression="Blank4" HeaderText="Blank4" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Transaction Date" SortExpression="Transaction Date" HeaderText="Transaction Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Blank5" SortExpression="Blank5" HeaderText="Blank5" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="IFSC Code" SortExpression="IFSC Code" HeaderText="IFSC Code" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Bene Bank Name" SortExpression="Bene Bank Name" HeaderText="Bene Bank Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Bene Bank Branch Name" SortExpression="Bene Bank Branch Name" HeaderText="Bene Bank Branch Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Beneficiary email id" SortExpression="Beneficiary email id" HeaderText="Beneficiary email id" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                        </Columns>
                                        </asp:GridView>

                                    <%--==========================================================================================================================================--%>
                                    <asp:GridView ID="gvSelected" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                        <HeaderStyle BackColor="gray" ForeColor="White" />
                                    <Columns>
                                         <asp:BoundField DataField="Transaction Type" SortExpression="Transaction Type" HeaderText="Transaction Type" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            
                                        <asp:BoundField DataField="Reference Number1" SortExpression="Reference Number1" HeaderText="Reference Number" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Dr Account No" SortExpression="Dr Account No" HeaderText="Dr Account No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Bene Name" SortExpression="Bene Name" HeaderText="Bene Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Remitter A/c no" SortExpression="Remitter A/c no" HeaderText="Remitter A/c no" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Remitter Name" SortExpression="Remitter Name" HeaderText="Remitter Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Narration" SortExpression="Narration" HeaderText="Narration" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Value Date" SortExpression="Value Date" HeaderText="Value Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Amount" SortExpression="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="right" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="eMail Address 1" SortExpression="eMail Address 1" HeaderText="eMail Address 1" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="eMail Address 2" SortExpression="eMail Address 2" HeaderText="eMail Address 2" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="eMail Address 3" SortExpression="eMail Address 3" HeaderText="eMail Address 3" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Advise Col1" SortExpression="Advise Col1" HeaderText="Advise Col1" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Advise Col2" SortExpression="Advise Col2" HeaderText="Advise Col2" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Advise Col3" SortExpression="Advise Col3" HeaderText="Advise Col3" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Advise Col4" SortExpression="Advise Col4" HeaderText="Advise Col4" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Advise Col5" SortExpression="Advise Col5" HeaderText="Advise Col5" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Bene Bank Account" SortExpression="Bene Bank Account" HeaderText="Bene Bank Account" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="BeneRTGSCodes" SortExpression="BeneRTGSCodes" HeaderText="BeneRTGSCodes" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Bene Bank Name" SortExpression="Bene Bank Name" HeaderText="Bene Bank Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                        <asp:BoundField DataField="Reference Number" SortExpression="Reference Number" HeaderText="Request No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false"/>
                                    </Columns>
                                </asp:GridView>
                                </div>
                            </div> 
                          
                        </div>
                          <div class="row" style="text-align:center">
                                 
                              <asp:Button ID="btnExp" runat="server" Text="Generate Excel" Visible="false" CssClass="btn btn-danger" onclick="btnExp_Click" />
                                <asp:Label ID="lblText" runat="server" Text="" style="display:none"></asp:Label>
                            </div>

                        </div>  
                    </div>
                </div>
            </div>


        </div>
        <div style="display: none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
            <asp:TextBox ID="json_data" runat="server"></asp:TextBox>
            <asp:TextBox ID="pk_bank_id" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_f_date" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_t_date" runat="server"></asp:TextBox>
        </div>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    </form>
</body>
</html>