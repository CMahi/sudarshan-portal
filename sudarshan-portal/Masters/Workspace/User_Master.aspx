<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="User_Master.aspx.cs"
    Inherits="User_Master" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<%--    <link href="../../css/sbm_app01.css" rel="stylesheet" type="text/css" />
    <link href="../../css/helpdesk_style.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../CSS/body.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/utility.js"></script>

    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/validator.js"></script>

    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/User.js"></script>

    <title>User Master</title>
</head>
<body class="ApBody" onload="DisableBrowserClone();">
    <form id="form1" runat="server">
    <div class="col_fullwidth3">
    <h2>User</h2>
    <table id="tbl_ContentPane" cellpadding="0" cellspacing="0" style="width: 100%; text-align: left;"> 
        <tr id="tbl_showMyButtonPanel" runat="server">
            <td align="center">
                <div id="Div1" runat="server" style="display: block">
                    <table id="tbl_showMyButtonPanel1" width="100%" cellpadding="0" runat="server" cellspacing="0"
                        style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px; color: #000000;
                        text-align: Left; padding: 0px; border-collapse: collapse; border: 1px solid #ADBBCA;">
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table align="center" id="tbl_ButtonPanel" width="100%" cellpadding="0" cellspacing="0"
                                    style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px; color: #000000;
                                    text-align: Left; padding: 0px; border-collapse: collapse; border: 0px solid #ADBBCA;">
                                    <tr runat="server" id="tr_Buttons">
                                        <td>
                                        </td>
                                        <td align="left">
                                            &nbsp;&nbsp;
                                            <input class="btn_bl" runat="server" name="btn_AddNew_Go" id="btn_AddNew_Go"
                                                type="button" value="Add New" onserverclick="btn_Add_onClickServer" />
                                            <%--<input class="ApScrnButton" runat="server" name="btn_Search_Go" id="btn_Search_Go" type="button" value="Search" onclick="btn_Search_onClick(1);"  onMouseOut="this.className='ApScrnButton';" onmouseover="this.className='ApScrnButtonHover';"/>--%>
                                            <input class="btn_bl" runat="server" name="btn_ViewAll_Go" id="btn_ViewAll_Go"
                                                type="button" value="View All" onserverclick="btn_ViewAll_onClick" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="tr_Message" visible="false">
                                        <td align='center'>
                                            <h5 style='color: red;'>
                                                Sorry, you do not have access!!!</h5>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr id="tbl_showMyAddPanel" runat="server">
            <td align="center">
                <div id="Div2" runat="server">
                    <table id="tbl_showMyAddPanel1" runat="server" width="100%" cellpadding="0" cellspacing="0"
                        style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px; color: #000000;
                        text-align: Left; padding: 0px; border-collapse: collapse; border: 1px solid #ADBBCA;">
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table id="tbl_AddNewEmployee" cellpadding="0" cellspacing="0" style="width: 100%;
                                    text-align: left;">
                                    <tr>
                                        <td align="center">
                                            <table id="tbl_NewEmployee" width="100%" cellpadding="0" cellspacing="0" class="bodycontents"
                                                style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px; color: #000000;
                                                text-align: Left; padding: 0px; border-collapse: collapse; border: 0px solid #ADBBCA;">
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%" align="center">
                                                            <tr>
                                                                <td style="width: 18%" align="Left">
                                                                    Employee Name
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="lblMandatory1" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txt_Add_EmployeeName" runat="server" Height="20%" CssClass="inputbox"
                                                                        Width="98%"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 18%" align="right">
                                                                </td>
                                                                <td style="width: 2%">
                                                                </td>
                                                                <td style="width: 30%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 18%" align="Left">
                                                                    Address1
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="lblMandatory2" runat="server" ForeColor="Red" Visible="false" Text="*"
                                                                        Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txt_Add_Address1" TextMode="MultiLine" runat="server" Height="20%"
                                                                        CssClass="inputbox" Width="98%"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 18%" align="Left">
                                                                    Address2
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="lblMandatory3" runat="server" Visible="false" ForeColor="Red" Text="*"
                                                                        Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txt_Add_Address2" TextMode="MultiLine" runat="server" Height="20%"
                                                                        CssClass="inputbox" Width="98%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 18%" align="Left">
                                                                    City
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="lblMandatory4" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%; height: 30px">
                                                                    <asp:DropDownList ID="ddl_Add_City" runat="server" Height="65%" CssClass="inputbox"
                                                                        Width="98%" onchange="getLocations()">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 18%" align="Left">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 2%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%; height: 30px">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 18%" align="Left">
                                                                    Location
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%; height: 30px">
                                                                    <asp:DropDownList ID="ddl_Add_Location" runat="server" Height="65%" CssClass="inputbox"
                                                                        Width="98%" onchange="getBranch()">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 18%" align="Left">
                                                                    Branch
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%; height: 30px">
                                                                    <asp:DropDownList ID="ddl_Add_Branch" runat="server" Height="65%" CssClass="inputbox"
                                                                        Width="98%" onchange="setBranch()">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 18%" align="Left">
                                                                    Department
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="lblMandatory16" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%; height: 30px">
                                                                    <asp:DropDownList ID="ddl_Add_Department" runat="server" Height="65%" CssClass="inputbox"
                                                                        Width="98%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 18%" align="Left">
                                                                    Designation
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="lblMandatory17" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%; height: 30px">
                                                                    <asp:DropDownList ID="ddl_Add_Designation" runat="server" Height="65%" CssClass="inputbox"
                                                                        Width="98%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 18%" align="Left">
                                                                    UserName
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txt_Add_UserName" runat="server" Height="20%" CssClass="inputbox"
                                                                        Width="98%"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 18%" align="Left">
                                                                    Password
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txt_Add_Password" runat="server" Height="20%" TextMode="Password"
                                                                        CssClass="inputbox" Width="98%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="center">
                                                                    <hr style="height: 1px; color: #ADBBCA;" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 18%" align="Left">
                                                                    Is Active
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="lblMandatory6" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%; height: 30px">
                                                                    <asp:DropDownList ID="ddl_Add_IsActive" runat="server" CssClass="inputbox" Height="78%">
                                                                        <asp:ListItem Value="">---Select One---</asp:ListItem>
                                                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 18%" align="Left">
                                                                    Email ID
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="lblMandatory7" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txt_Add_Email" runat="server" Height="60%" CssClass="inputbox" Width="85%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 18%" align="Left">
                                                                    Reporting Manager
                                                                </td>
                                                                <td style="width: 2%">
                                                                    <asp:Label ID="lblMandatory18" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%; height: 30px">
                                                                    <asp:TextBox ID="txt_Add_Reporting" runat="server" Height="60%" CssClass="inputbox"
                                                                        Width="85%"></asp:TextBox>
                                                                    <a href="#" title="Click here to search." onclick="return btn_Search_onClick(3);">
                                                                        <img src="../../images/search.gif" style="border: 0" alt="Click here to search Employee." /></a>
                                                                </td>
                                                                <td style="width: 18%" align="Left">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 2%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <tr>
                                                            <td align="center">
                                                                <hr style="height: 1px; color: #ADBBCA;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 35%; background-color: white">
                                                                        </td>
                                                                        <td align="left" style="width: 65%">
                                                                            <asp:Button ID="btn_Add" runat="server" Text="Save" class="btn_rd" OnClick="btn_New_onClick"
                                                                                OnClientClick="return validateforms();"></asp:Button>
                                                                            <input class="btn_bl" runat="server" name="btn_Reset_Adds" id="btn_Reset_Adds"
                                                                                type="button" value="Clear" onclick="ClearField();" onmouseout="this.className='ApScrnButton';" />
                                                                            <asp:Button ID="btn_Home" runat="server" Text="Back" class="btn_bl" OnClick="btn_Home_onClick"
                                                                                OnClientClick="ClearField();"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- tbl_NewEmpployee -->
                                        </td>
                                    </tr>
                                </table>
                                <!-- End of tbl_AddNewEmployee -->
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <!-- End of tbl_showMyAddPanel-->
                </div>
            </td>
        </tr>
        <tr id="tbl_showMyEditPanel" runat="server">
            <td align="center">
                <div id="Div3" visible="true" runat="server">
                    <table id="tbl_showMyEditPanel1" width="100%" cellpadding="0" cellspacing="0" style="font-family: Verdana, Arial, Helvetica, sans-serif;
                        font-size: 10px; color: #000000; text-align: Left; padding: 0px; border-collapse: collapse;
                        border: 1px solid #ADBBCA;">
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table id="tbl_EditEmployeeData" cellpadding="0" cellspacing="0" style="width: 100%;
                                    text-align: left;">
                                    <tr>
                                        <td align="center">
                                            <table id="tbl_EditEmpData" width="100%" cellpadding="0" cellspacing="0" style="font-family: Verdana, Arial, Helvetica, sans-serif;
                                                font-size: 10px; color: #000000; text-align: Left; padding: 0px; border-collapse: collapse;
                                                border: 0px solid #ADBBCA;">
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="59%" align="center">
                                                            <tr>
                                                                <td style="width: 20%; background-color: white">
                                                                    Expense Type
                                                                </td>
                                                                <td style="width: 4%; background-color: white">
                                                                    <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                                </td>
                                                                <td style="width: 76%; background-color: white">
                                                                    <asp:TextBox ID="txt_Edit_ExpenseType" Width="80%" runat="server" Height="20%" ReadOnly="true"
                                                                        CssClass="inputbox"></asp:TextBox>
                                                                    <a href="#" title="Click here to search." onclick="return btn_Search_onClick(3);">
                                                                        <img src="/Sudarshan-Portal-NEW/images/search.gif" style="border: 0" alt="Click here to search role." /></a>
                                                                </td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; background-color: white">
                                                        Narration
                                                    </td>
                                                    <td style="width: 4%; background-color: white">
                                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*" Width="1px"></asp:Label>
                                                    </td>
                                                    <td style="width: 76%; background-color: white">
                                                        <asp:TextBox ID="txt_Edit_Narration" Width="80%" runat="server" Height="20%" CssClass="inputbox"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; background-color: white">
                                                        Description
                                                    </td>
                                                    <td style="width: 4%; background-color: white">
                                                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*" Visible="false" Width="1px"></asp:Label>
                                                    </td>
                                                    <td style="width: 76%; background-color: white">
                                                        <asp:TextBox ID="txt_Edit_Description" Width="80%" runat="server" Height="20%" TextMode="MultiLine"
                                                            CssClass="inputbox"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <tr>
                                                <td align="center">
                                                    <hr style="height: 1px; color: #ADBBCA;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table align="center" width="100%">
                                                        <tr>
                                                            <td style="width: 35%; background-color: white">
                                                            </td>
                                                            <td align="left" style="width: 65%">
                                                                <asp:Button ID="btn_Edit" runat="server" Text="Update" class="btn_rd" OnClick="btn_Edit_onClick"
                                                                    OnClientClick="IsMandotoryEdit();return validateforms();"></asp:Button>
                                                                <asp:Button ID="btn_Delete" runat="server" Text="Delete" class="btn_rd" OnClick="btn_Delete_onClick">
                                                                </asp:Button>
                                                                <asp:Button ID="btn_Back" runat="server" Text="Back" class="btn_bl" OnClick="btn_Back_Edit_onclick">
                                                                </asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <!-- tbl_EditEmpData -->
                     </div>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
    <!-- End of tbl_EditEmployeeData -->
    <tr>
        <td align="center">
            <div id="tbl_showMyViewAllPanel" runat="server" style="display: none">
                <table id="tbl_showMyViewAllPanel1" width="100%" cellpadding="0" cellspacing="0"
                    style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px; color: #000000;
                    text-align: Left; padding: 0px; border-collapse: collapse; border: 1px solid #ADBBCA;">
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table id="tbl_DisplayResultSetAllEmployee" cellpadding="0" cellspacing="0" style="width: 100%;
                                text-align: left;">
                                <tr>
                                    <td align="center">
                                        <table id="tbl_DisplayResultSetAllEmployeeInfo" width="100%" cellpadding="0" cellspacing="0"
                                            style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px; color: #000000;
                                            text-align: Left; padding: 0px; border-collapse: collapse; border: 0px solid #ADBBCA;">
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <div id="dgdiv" runat="server" style="width: 100%">
                                                        <asp:GridView ID="dgViewAll" runat="server" CellPadding="2" AllowPaging="true" BackColor="White"
                                                            OnPageIndexChanging="dgView_PageIndexChanging">
                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="white" ForeColor="#333333" />
                                                            <EditRowStyle BackColor="white" />
                                                            <SelectedRowStyle BackColor="white" Font-Bold="True" ForeColor="#333333" />
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Underline="True" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <hr style="height: 1px; color: #ADBBCA;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table align="center" width="100%">
                                            <tr>
                                                <td align="left" style="width: 100%">
                                                    <input class="ApScrnButton" name="btn_AllEmpInfo_Back" id="btn_AllEmpInfo_Back" type="button"
                                                        value="Back" runat="server" onserverclick="btn_AllEmpInfo_Back_onClick" onmouseout="this.className='ApScrnButton';"
                                                        onmouseover="this.className='ApScrnButtonHover';" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <!-- End of tbl_DisplayResultSetAllEmployeeInfo-->
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                </table>
                <!-- End of tbl_DisplayResultSetAllEmployee-->
                </div>       
        </td>
    </tr>
    <tr>
        <td>
            <div id="conditionButton" style="display: none">
                <asp:TextBox ID="txtCondition" runat="server" Width="0.5px"></asp:TextBox>
                <asp:TextBox ID="txtParameterID" runat="server" Width="1px"></asp:TextBox>
                <asp:TextBox ID="txtCreatedBy" runat="server" Width="1px"></asp:TextBox>
                <asp:TextBox ID="txtCreatedDate" runat="server" Width="1px"></asp:TextBox>
                <asp:TextBox ID="txtCity" runat="server" Width="1px"></asp:TextBox>
                <asp:TextBox ID="txt_AMDDate" runat="server" Width="1px"></asp:TextBox>
                <asp:TextBox ID="txt_Branch" runat="server" Width="1px"></asp:TextBox>
                <asp:TextBox ID="txtPassword" runat="server" Width="1px"></asp:TextBox>
                <asp:TextBox ID="txt_ActionObjectID" runat="server" Width="200px"></asp:TextBox>
                <asp:TextBox ID="txt_Password" runat="server" Width="200px"></asp:TextBox>
                <asp:TextBox ID="txt_reporting" runat="server" Width="200px"></asp:TextBox>
                <asp:TextBox ID="txt_Location" runat="server" Width="200px"></asp:TextBox>
                <asp:TextBox ID="txt_DomainName" runat="server" Width="200px"></asp:TextBox>
                <asp:TextBox ID="txt_procedure" runat="server" Width="0.5px"></asp:TextBox>
                <asp:TextBox ID="txt_searchstring" runat="server" Width="0.5px"></asp:TextBox>
                <br />
                <br />
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <div id="DivDisplayError" runat="server">
                <table>
                    <tr>
                        <td style="background-color: white; width: 10%">
                            <div id="div_txt_Add_Narration" visible="false" style="text-align: left; color: Red;"
                                runat="server">
                                <asp:Image ID="img_txt_Add_Narration" runat="server" ImageUrl="./Images_Header/error.gif" />
                                <asp:Label ID="lbl_txt_Add_Narration" runat="server" Text="Bye"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: white; width: 10%">
                            <div id="div_txt_Edit_Narration" visible="false" style="text-align: left; color: Red;"
                                runat="server">
                                <asp:Image ID="img_txt_Edit_Narration" runat="server" ImageUrl="~/Images_Header/error.gif" />
                                <asp:Label ID="lbl_txt_Edit_Narration" runat="server" Text="Bye"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>

<script language="javascript" type="text/javascript">

// <!CDATA[
var searchValue='';
SearchArray = new Array(3);
DisplayArray = new Array(3);
SearchArray [0] = new Array(5);
DisplayArray[0]=new Array(10);

function ClearField()
{
document.getElementById("txt_Add_EmployeeName").value="";
document.getElementById("txt_Add_Address1").value="";
document.getElementById("txt_Add_Address2").value="";
document.getElementById("ddl_Add_Location").value="";
document.getElementById("ddl_Add_Country").value="";
document.getElementById("txt_Add_ContactNo").value="";
document.getElementById("txt_Add_MobileNo").value="";
document.getElementById("txt_Add_Password").value="";
document.getElementById("txt_Add_EmployeeID").value="";
document.getElementById("txt_Add_ADID").value="";
document.getElementById("txt_Add_EmailID").value="";
document.getElementById("txt_Designation").value="";
document.getElementById("ddl_Add_Company").value="";
document.getElementById("ddl_Add_Department").value="";
document.getElementById("ddl_Add_IsEmployee").value="";
document.getElementById("ddl_Add_IsActive").value="";


}
function IsMandotoryAdd()
{
document.getElementById("txt_Add_EmployeeName").setAttribute("isMandatory","true"); 
document.getElementById("txt_Add_Address1").setAttribute("isMandatory","false"); 
document.getElementById("txt_Add_Address2").setAttribute("isMandatory","false"); 
document.getElementById("ddl_Add_Location").setAttribute("isMandatory","false"); 
document.getElementById("ddl_Add_Country").setAttribute("isMandatory","false"); 
document.getElementById("txt_Add_ContactNo").setAttribute("isMandatory","false"); 
document.getElementById("txt_Add_MobileNo").setAttribute("isMandatory","false"); 
document.getElementById("ddl_Add_Company").setAttribute("isMandatory","true"); 
document.getElementById("txt_Add_EmployeeID").setAttribute("isMandatory","true"); 
document.getElementById("txt_Add_ADID").setAttribute("isMandatory","true"); 
document.getElementById("txt_Add_EmailID").setAttribute("isMandatory","true"); 
document.getElementById("txt_Add_Department").setAttribute("isMandatory","true"); 
document.getElementById("txt_Add_Designation").setAttribute("isMandatory","true"); 
}
function IsMandotoryEdit()
{
//document.getElementById("txt_Add_ExpenseType").setAttribute("isMandatory","false");
//document.getElementById("txt_Add_Narration").setAttribute("isMandatory","false");
//document.getElementById("txt_Edit_ExpenseType").setAttribute("isMandatory","true");
//document.getElementById("txt_Edit_Narration").setAttribute("isMandatory","true");
}
function display()
{
var DisplayContent;
document.getElementById("lblMode").innerText="Edit";
if(searchValue==1)
{ 
document.getElementById("tbl_showMyButtonPanel").style.display="none";
document.getElementById("tbl_showMyEditPanel").style.display="block";
//document.getElementById("txtParameterID").value=DisplayArray[0][0];
//document.getElementById("txt_Edit_ExpenseType").value=DisplayArray[0][1];
//document.getElementById("txt_Edit_Narration").value=DisplayArray[0][2];
//document.getElementById("txt_Edit_Description").value=DisplayArray[0][3];
//document.getElementById("txtExpenseTypeID").value=DisplayArray[0][4];
//document.getElementById("txt_AMDDate").value=DisplayArray[0][5];
}
if(searchValue==2)
{
//document.getElementById("txt_Add_ExpenseType").value=DisplayArray[0][1];
//document.getElementById("txtExpenseTypeID").value=DisplayArray[0][0];
}
if(searchValue==3)
{
document.getElementById("txt_Add_Reporting").value=DisplayArray[0][0];
document.getElementById("txt_reporting").value=DisplayArray[0][0];
}

}
function btn_Search_onClick(id)
{
searchValue=id;
if(searchValue==1)
{
SearchArray [0][0] ="select";
SearchArray [0][1] = "EMPLOYEE";
SearchArray [0][2] = "Employee Name";
SearchArray [0][3] = "TextBox";
}
if(searchValue==3)
{
SearchArray [0][0] ="select";
SearchArray [0][1] = "USERS";
SearchArray [0][2] = "User Name";
SearchArray [0][3] = "TextBox";
}
//window.open('../Masters/Search.aspx','Search','left=150,top=100,width=700,height=500,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
OpenSearch();

}


// ]]>
</script>

</html>
