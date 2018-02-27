<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Role" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Role Master</title>
    <style type="text/css">
        .style2
        {
            width: 154px;
        }
        .style3
        {
            width: 700px;
        }
        .style4
        {
            width: 247px;
        }
    </style>
<%--    <link href="/Sudarshan-Portal-NEW/css/helpdesk_style.css" rel="stylesheet" type="text/css" />
    <link href="/Sudarshan-Portal-NEW/css/innocent_flowers.css" rel="stylesheet" type="text/css" />
    <link href="/Sudarshan-Portal-NEW/css/sbm_app01.css" rel="stylesheet" type="text/css" />--%>
     <link href="../../CSS/body.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/Role.js"></script>
    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/utility.js"></script>
    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/validator.js"></script>
    <script language="javascript" type="text/javascript">
        document.attachEvent("onkeydown", my_onkeydown_handler);
        function my_onkeydown_handler() {
            var KeyID = window.event ? event.keyCode : e.keyCode;
            if ((window.event ? (event.altKey == true) : (e.altKey == true)) && (KeyID == 115)) {
                window.open('/Sudarshan-Portal-NEW/Logoff.aspx', '_top');
            }
        } 
    </script>
</head>
<body class="ApBody" onload="DisableBrowserClone();">
    <form id="frm_RoleMaster" runat="server">
    <div class="col_fullwidth3">
        <h2>Role Master</h2>
        <table id="tbl_ContentPlaceHolder" align="center" cellpadding="0" cellspacing="0"
            width="100%">
            <!-- Start FieldSet No.1 for Button Panel -->
            <tr>
                <td align="center" style="width: 100%">
                    <table id="tbl_ShowButtonPanel" runat="server" cellpadding="0" cellspacing="0" style="width: 95%;
                        text-align: left;">
                       
                        <tr>
                            <td align="center">
                                <table align="center" width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <div id="div_Buttons" runat="server">
                                                &nbsp;&nbsp;
                                                <asp:Button CssClass="btn_bl" ID="btn_AddNew" Text="Add" runat="server" OnClick="btn_AddNew_onClick"
                                                    />
                                                <asp:Button CssClass="btn_bl" ID="btn_Search" Text="Search" runat="server"
                                                    OnClick="btn_Search_onClick" />
                                                <asp:Button CssClass="btn_bl" ID="btn_ViewAll" Text="View All" runat="server"
                                                    OnClick="btn_ViewAll_onClick" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align='center'>
                                            <div runat="server" id="div_AccessMessage" visible="false">
                                                <h5 style='color: red;'>
                                                    Sorry, you do not have access!!!</h5>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <!-- End of tbl_ShowButtonPanel-->
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table id="tbl_AddNewRole" runat="server" cellpadding="0" cellspacing="0" style="width: 95%;
                        text-align: left;">
                      
                        <tr>
                            <td align="center">
                                <table id="tbl_NewRole" width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table align="center" width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center">
                                                        <table align="center" width="50%">
                                                            <tr>
                                                                <td align="left">
                                                                    <font face="Verdana" font-size="8pt">Role Name&nbsp;</font>
                                                                </td>
                                                                <td>
                                                                    <font color="#ff0000">*</font>
                                                                </td>
                                                                <td>
                                                                    <input class="ApInptTxt" type="text" id="txt_RoleName" name="txt_RoleName" size="30"
                                                                        runat="server">
                                                                </td>
                                                                <td>
                                                                    <div id="div_txt_RoleName" visible="false" style="text-align: left; color: Red;"
                                                                        runat="server">
                                                                        <asp:Image ID="img_txt_RoleName" runat="server" ImageUrl="/Sudarshan-Portal-NEW/Images/error.gif" />
                                                                        <asp:Label ID="lbl_txt_RoleName" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <font face="Verdana" font-size="8pt">Role Description&nbsp;&nbsp;&nbsp;</font>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <input class="ApInptTxt" type="text" id="txt_RoleDesc" name="txt_RoleDesc" size="30"
                                                                        runat="server">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <font face="Verdana" font-size="8pt">Company ID&nbsp;&nbsp;&nbsp;</font>
                                                                </td>
                                                                <td>
                                                                    <font color="#ff0000">*</font>
                                                                </td>
                                                                <td>
                                                                    <select class="ApInptSelect" id="ddl_CompanyID" name="ddl_CompanyID" style="width: 200px;"
                                                                        runat="server">
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <div id="div_ddl_CompanyID" visible="false" style="text-align: left; color: Red;"
                                                                        runat="server">
                                                                        <asp:Image ID="Img_ddl_CompanyID" runat="server" ImageUrl="/Sudarshan-Portal-NEW/Images/error.gif" />
                                                                        <asp:Label ID="Lbl_ddl_CompanyID" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td style="background-color: white" class="style3">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="background-color: white">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="background-color: white">
                                                                    &nbsp;
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
                                                    <td align="center">
                                                        <asp:Button CssClass="btn_rd" ID="btn_Add" Text=" Save " runat="server" OnClientClick="return validateforms();"
                                                            OnClick="btn_Add_onClick" />
                                                        <input class="btn_bl" name="btn_Reset" id="btn_Reset" type="button" value="Cancel"
                                                            onclick="resetForm();"  />
                                                        <input class="btn_bl" name="btn_AddHome" id="btn_AddHome" type="button" value=" Back"
                                                            onclick="return btn_Home_onClick();"  />
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table id="tbl_EditRoles" runat="server" cellpadding="0" cellspacing="0" style="width: 95%;
                        text-align: left;">
                           
                            <tr>
                                <td align="center">
                                    <table id="tbl_EditRole" width="100%" cellpadding="0" cellspacing="0" style="font-family: Verdana, Arial, Helvetica, sans-serif;
                                        font-size: 10px; color: #000000; text-align: Left; padding: 0px; border-collapse: collapse;
                                        border: 1px solid #ADBBCA;">
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="tbl_Search" align="center" cellpadding="0" cellspacing="0" width="100%"
                                                    runat="server">
                                                    <tr>
                                                        <td align="center" style="width: 100%">
                                                            <font face="Verdana" font-size="8pt">Enter Role</font>&nbsp;&nbsp;&nbsp;
                                                            <input class="ApInptTxt" type="text" id="txt_SearchPattern" name="txt_SearchPattern"
                                                                size="30" runat="server"/>&nbsp;&nbsp;
                                                            <asp:Button CssClass="btn_rd" ID="btn_ShowSearchResult" Text="Search" runat="server"
                                                                OnClientClick="return validateforms();" OnClick="btn_ShowSearchResult_onClick" />
                                                            <input class="btn_bl" name="btn_Search_Back" id="btn_Search_Back" type="button"
                                                                value=" Back" onclick="return btn_Home_onClick();" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <hr style="height: 1px; color: #ADBBCA;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <div id="div_DiaplayResultSetRoleInfo" class="innocent_flowers" style="width: 90%"
                                                                runat="server">
                                                                <asp:GridView ID="dgSearchResult" runat="server" CellPadding="2" AllowPaging="true"
                                                                    AutoGenerateColumns="false" OnRowDataBound="CreateLink" OnPageIndexChanging="dgSearchResult_PageIndexChanging"
                                                                    BackColor="White" Width="98%" BorderColor="#ADBBCA" BorderStyle="Solid" BorderWidth="1px"
                                                                    EmptyDataText="No Data Found!!!" CaptionAlign="Left" CssClass="mgrid">
                                                                    <PagerSettings FirstPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageFirst.gif" FirstPageText="View First Page"
                                                                        LastPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageLast.gif" NextPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageNext.gif"
                                                                        PreviousPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PagePrev.gif" LastPageText="View Last Page"
                                                                        Mode="NextPreviousFirstLast" NextPageText="View Next Page" PreviousPageText="View Prevoius Page" />
                                                            
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="ROLE NAME" DataField="Role Name" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="ROLE DESCRIPTION" DataField="Role Description" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="COMPANY ID" DataField="COMPANY ID" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                        <td style="background-color: white" class="style3">
                                                            &nbsp;
                                                        </td>
                                                        <td style="background-color: white">
                                                            &nbsp;
                                                        </td>
                                                        <td style="background-color: white">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="tbl_SearchResult" width="100%" align="center" cellpadding="0" cellspacing="0"
                                                    runat="server">
                                                    <tr>
                                                        <td align="center">
                                                            <table align="center" style="width: 59%">
                                                                <tr>
                                                                    <td align="left">
                                                                        <font face="Verdana" font-size="8pt">Role Name&nbsp;</font>
                                                                    </td>
                                                                    <td>
                                                                        <font color="#ff0000">*</font>
                                                                    </td>
                                                                    <td>
                                                                        <input class="ApInptTxt" type="text" id="txt_Edit_Role" name="txt_Edit_Role" size="30"
                                                                            runat="server">
                                                                    </td>
                                                                    <td>
                                                                        <div id="div_txt_Edit_Role" visible="false" style="text-align: left; color: Red;"
                                                                            runat="server">
                                                                            <asp:Image ID="Img_txt_Edit_Role" runat="server" ImageUrl="/Sudarshan-Portal-NEW/Images/error.gif" />
                                                                            <asp:Label ID="Lbl_txt_Edit_Role" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <font face="Verdana" font-size="8pt">Role Description&nbsp;&nbsp;&nbsp;</font>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <input class="ApInptTxt" type="text" id="txt_Edit_RoleDesc" name="txt_Edit_RoleDesc"
                                                                            size="30" runat="server">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <font face="Verdana" font-size="8pt">Company ID&nbsp;&nbsp;&nbsp;</font>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <input class="ApInptTxt" type="text" id="txt_CompanyID" name="txt_CompanyID" size="30"
                                                                            readonly runat="server" />
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="background-color: white" class="style3">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="background-color: white">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="background-color: white">
                                                                        &nbsp;
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
                                                        <td align="center">
                                                            <asp:Button CssClass="btn_rd" ID="btn_Edit" Text=" Save " runat="server" OnClientClick="return validateforms();"
                                                                OnClick="btn_Edit_onClick"  />
                                                            <asp:Button CssClass="btn_rd" ID="btn_Delete" Text="Delete" runat="server"
                                                                OnClientClick="return validateforms();" OnClick="btn_Delete_onClick" onmouseout="this.className='ApScrnButton';"
                                                                onmouseover="this.className='ApScrnButtonHover';" />
                                                            <input class="btn_bl" name="btn_Reset" id="Button2" type="button" value="Cancel"
                                                                onclick="resetForm();"  />
                                                            <input class="btn_bl" name="btn_EditHome" id="btn_EditHome" type="button" value=" Back "
                                                                onclick="return btn_Home_onClick();"  />
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
                                    <!-- tbl_EditRole -->
                                </td>
                            </tr>
                    </table>
                    <!-- End of tbl_EditRoles -->
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table id="tbl_showViewAllPanel" runat="server" cellpadding="0" cellspacing="0" style="width: 95%;
                        text-align: left;">
                 
                        <tr>
                            <td align="center">
                                <table id="tbl_ViewAll" width="100%" cellpadding="0" cellspacing="0" style="font-family: Verdana, Arial, Helvetica, sans-serif;
                                    font-size: 10px; color: #000000; text-align: Left; padding: 0px; border-collapse: collapse;
                                    border: 1px solid #ADBBCA;">
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td align="right">
                                                        <input class="ApScrnButton" name="btn_Home_ViewAll" id="btn_Home_ViewAll" type="button"
                                                            value=" Back" onclick="return btn_Home_onClick();" onmouseout="this.className='ApScrnButton';"
                                                            onmouseover="this.className='ApScrnButtonHover';" />&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%">
                                                        <div id="div_ShowRoles" class="innocent_flowers" style="width: 100%" runat="server">
                                                            <asp:GridView ID="dgViewAll" runat="server" CellPadding="2" AutoGenerateColumns="false"
                                                                AllowPaging="true" OnPageIndexChanging="dgView_PageIndexChanging" BackColor="White"
                                                                Width="95%" BorderColor="#ADBBCA" BorderStyle="Solid" BorderWidth="1px" EmptyDataText="No Data Found!!!"
                                                                CaptionAlign="Left" CssClass="mgrid">
                                                                <PagerSettings FirstPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageFirst.gif" FirstPageText="View First Page"
                                                                    LastPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageLast.gif" NextPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageNext.gif"
                                                                    PreviousPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PagePrev.gif" LastPageText="View Last Page"
                                                                    Mode="NextPreviousFirstLast" NextPageText="View Next Page" PreviousPageText="View Prevoius Page" />
                                                            
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="ROLE NAME" DataField="Role Name" HeaderStyle-HorizontalAlign="Center"
                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="ROLE DESCRIPTION" DataField="Role Description" HeaderStyle-HorizontalAlign="Center"
                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                    <asp:BoundField HeaderText="COMPANY ID" DataField="COMPANY ID" HeaderStyle-HorizontalAlign="Center"
                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                    <td style="background-color: white" class="style3">
                                                        &nbsp;
                                                    </td>
                                                    <td style="background-color: white">
                                                        &nbsp;
                                                    </td>
                                                    <td style="background-color: white">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <!-- tbl_ViewAll -->
                            </td>
                        </tr>
                    </table>
                    <!-- End of tbl_showViewAllPanel -->
                </td>
            </tr>
        </table>
    <!-- Start of Hideen Field -->
    <table id="tbl_Hideen" visible="false">
        <tr>
            <td>
                <input type="hidden" id="txt_ActionBy" name="txt_ActionBy" value="" runat="server" />
            </td>
            <td>
                <input type="hidden" id="txt_ActionDate" name="txt_ActionDate" value="" runat="server" />
            </td>
            <td>
                <input type="hidden" id="txt_ActionCondn" name="txt_ActionCondn " value="" runat="server" />
            </td>
            <td>
                <input type="hidden" id="txt_ActionObjectID" name="txt_ActionObjectID" value="" runat="server" />
            </td>
            <td>
                <input type="hidden" id="txt_RoleID" name="txt_RoleID" value="" runat="server" />
            </td>
        </tr>
    </table>
    <!-- End of Hideen Field -->
    </div>
    </form>
</body>

</html>
