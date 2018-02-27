<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccessObject_Role_Mapping.aspx.cs"
    Inherits="AccessObject_Role_Mapping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Access Object To Role Mapping</title>
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
    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/AccessObject_Role_Mapping.js"></script>
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
    <style type="text/Css">
        th
        {
            background: #203250;
            color: #FFFFFF;
            font-weight: bold;
            text-align: center;
            border-top: 0px solid #fff;
            border-right: 1px solid #ADBBCA;
            border-bottom: 0px solid #c6cdd8;
            border-left: 0px solid #fff;
        }
    </style>
</head>
<body class="ApBody" onload="DisableBrowserClone();">
    <form id="frm_AccessObjectRoleMapping" runat="server">
    <div class="col_fullwidth3">
        <h2>Access Object To Role Mapping</h2>
        <table id="tbl_ContentPlaceHolder" align="center" cellpadding="0" cellspacing="0"
            width="100%">
            <tr>
                <td align="center">
                    <table id="tbl_ShowButtonPanel" runat="server" cellpadding="0" cellspacing="0" style="width: 98%;
                        text-align: left;">
                        <%--<tr>
                        <td>
                            <div>
                                <div style="height: 4%; background-image: url(/Sudarshan-Portal-NEW/Images/topmiddle.jpg); background-repeat: repeat-x;">
                                    <div style="float: left; width: 8px; background-image: url(/Sudarshan-Portal-NEW/Images/topleft.jpg);
                                        background-repeat: no-repeat;">
                                    </div>
                                    <div style="float: left; padding-top: 4px;">
                                        <strong style="text-align: Center; color: #9900FF;">Access Object - Role Management</strong></div>
                                    <div style="float: right; width: 9px; background-image: url(/Sudarshan-Portal-NEW/Images/topright.jpg);
                                        background-repeat: no-repeat;">
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>--%>
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
                                                <asp:Button CssClass="btn_rd" ID="btn_Search" Text="Search" Visible="false"
                                                    Enabled="false" runat="server" OnClientClick="ResizeFrameWindows(true);" OnClick="btn_Search_onClick"
                                                   />
                                                <asp:Button CssClass="btn_bl" ID="btn_ViewAll" Text="View" Visible="false"
                                                    Enabled="false" runat="server" OnClientClick="ResizeFrameWindows(true);" OnClick="btn_ViewAll_onClick"
                                                    />
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
                    <table id="tbl_EditAccessObjectRoleMap" runat="server" cellpadding="0" cellspacing="0"
                        style="width: 98%; text-align: left;">
                        <%--<tr>
                            <td>
                                <div id="Div2">
                                    <div style="height: 4%; background-image: url(/Sudarshan-Portal-NEW/Images/topmiddle.jpg); background-repeat: repeat-x;">
                                        <div style="float: left; width: 8px; background-image: url(/Sudarshan-Portal-NEW/Images/topleft.jpg);
                                            background-repeat: no-repeat;">
                                        </div>
                                        <div style="float: left; padding-top: 4px;">
                                            <strong style="text-align: Center; color: #9900FF;">Access Object - Role Map</strong></div>
                                        <div style="float: right; width: 9px; background-image: url(/Sudarshan-Portal-NEW/Images/topright.jpg);
                                            background-repeat: no-repeat;">
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="center">
                                <table id="tbl_EditAccessObjectRole" width="100%" cellpadding="0" cellspacing="0"
                                    >
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table id="tbl_Search" align="center" cellpadding="0" cellspacing="0" width="100%"
                                                runat="server">
                                                <tr>
                                                    <td align="center" style="width: 59%">
                                                        <font face="Verdana" size="2">Enter Role</font>&nbsp;&nbsp;&nbsp;
                                                        <input class="ApInptTxt" type="text" id="txt_SearchPattern" name="txt_SearchPattern"
                                                            size="30" runat="server" />&nbsp;&nbsp;
                                                        <asp:Button CssClass="btn_rd" ID="btn_ShowSearchResult" Text="Search" runat="server"
                                                            OnClientClick="return chkSearch();" OnClick="btn_ShowSearchResult_onClick" />
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
                                                        <div id="div_DiaplayResultSetAccessObjectRoleInfo" class="innocent_flowers" style="width: 90%"
                                                            runat="server">
                                                            <asp:GridView ID="dgSearchResult" runat="server" CellPadding="2" AllowPaging="true"
                                                                PageSize="15" AutoGenerateColumns="false" OnRowDataBound="CreateLink" OnPageIndexChanging="dgSearchResult_PageIndexChanging"
                                                                BackColor="White" Width="98%" BorderColor="#ADBBCA" BorderStyle="Solid" BorderWidth="1px"
                                                                EmptyDataText="No Data Found!!!" CaptionAlign="Left" CssClass="mgrid">
                                                                <PagerSettings FirstPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageFirst.gif" FirstPageText="View First Page"
                                                                    LastPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageLast.gif" NextPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageNext.gif"
                                                                    PreviousPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PagePrev.gif" LastPageText="View Last Page"
                                                                    Mode="NextPreviousFirstLast" NextPageText="View Next Page" PreviousPageText="View Prevoius Page" />
                                                                <%--<EmptyDataRowStyle Font-Bold="True" Font-Size="X-Small" ForeColor="Red" HorizontalAlign="Center" />
                                                                <PagerStyle BackColor="White" ForeColor="Blue" HorizontalAlign="Left" Font-Underline="true"
                                                                    VerticalAlign="Middle" />
                                                                <HeaderStyle BackColor="#203250" Font-Bold="True" ForeColor="White" BorderColor="#ADBBCA"
                                                                    BorderStyle="Solid" BorderWidth="1px" />
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" BorderColor="#ADBBCA"
                                                                    BorderStyle="Solid" BorderWidth="1px" />--%>
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
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table id="tbl_SearchResult" width="100%" align="center" cellpadding="0" cellspacing="0"
                                                runat="server">
                                                <tr>
                                                    <td align="center">
                                                        <table align="center" width="100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center">
                                                                    <div id="div_txt_XMLData" visible="false" style="text-align: center; color: Red;
                                                                        width: 100%" runat="server">
                                                                        <asp:Image ID="img_txt_XMLData" runat="server" ImageUrl="/Sudarshan-Portal-NEW/Images/error.gif" />
                                                                        <asp:Label ID="lbl_txt_XMLData" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <table align="center" width="50%">
                                                                        <tr align="center">
                                                                            <td>
                                                                                Select Role&nbsp;&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <font color="#ff0000">*&nbsp;</font>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox CssClass="ApInptTxt" ID="txt_RoleName" TextMode="SingleLine" Columns="30"
                                                                                    runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <div id="div_txt_RoleName" visible="false" style="text-align: left; color: Red;"
                                                                                    runat="server">
                                                                                    <asp:Image ID="img_txt_RoleName" runat="server" ImageUrl="/Sudarshan-Portal-NEW/Images/error.gif" />
                                                                                    <asp:Label ID="lbl_txt_RoleName" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td>
                                                                                Select Panel
                                                                            </td>
                                                                            <td>
                                                                                <font color="#ff0000">*</font>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList CssClass="ApInptSelect" ID="ddl_Panel" runat="server" Width="195px">
                                                                                    <asp:ListItem>---SELECT ONE---</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <div id="div_ddl_Panel" visible="false" style="text-align: left; color: Red;" runat="server">
                                                                                    <asp:Image ID="img_ddl_Panel" runat="server" ImageUrl="/Sudarshan-Portal-NEW/Images/error.gif" />
                                                                                    <asp:Label ID="lbl_ddl_Panel" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Button CssClass="btn_rd" ID="btn_ShowData" Text="Show" runat="server"
                                                                                    OnClientClick="return chkHeader();" OnClick="btn_Show_onClick" />
                                                                                <input class="bt btn_bl" name="btn_AddHome" id="btn_AddHome" type="button" value=" Back"
                                                                                    onclick="return btn_Home_onClick();"/>
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
                                                        <div id="div_ObjMap" class="" style="" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <div id="div_Save" style="" runat="server">
                                                            <br />
                                                            <asp:Button CssClass="btn_rd" ID="btn_Save" Text=" Save " runat="server" OnClientClick="return makeXML();"
                                                                OnClick="btn_Save_onClick" />
                                                            <asp:Button CssClass="btn_bl" ID="btn_Reset" Text="Cancel" runat="server" OnClick="ResetForm" />
                                                        </div>
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
                <td align="center" style="width: 100%">
                    <table id="tbl_showViewAllPanel" runat="server" cellpadding="0" cellspacing="0" style="width: 98%;
                        text-align: left;">
                        <%--<tr>
                            <td>
                                <div>
                                    <div style="height: 4%; background-image: url(/Sudarshan-Portal-NEW/Images/topmiddle.jpg); background-repeat: repeat-x;">
                                        <div style="float: left; width: 8px; background-image: url(/Sudarshan-Portal-NEW/Images/topleft.jpg);
                                            background-repeat: no-repeat;">
                                        </div>
                                        <div style="float: left; padding-top: 4px;">
                                            <strong style="text-align: Center; color: #9900FF;">Access Object - Role Map</strong></div>
                                        <div style="float: right; width: 9px; background-image: url(/Sudarshan-Portal-NEW/Images/topright.jpg);
                                            background-repeat: no-repeat;">
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="center">
                                <table id="tbl_ViewAll" width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div id="div_RequestHdr">
                                                <table align="center" width="95%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center">
                                                            <font face="Verdana" size="2">Enter Role</font>&nbsp;&nbsp;&nbsp;
                                                            <input class="ApInptTxt" type="text" id="Txt_SearchPattern_View" name="Txt_SearchPattern_View"
                                                                size="30" maxlength="30" runat="server" />&nbsp;&nbsp;
                                                            <asp:Button CssClass="btn_rd" ID="btn_ShowSearchResult_View" Text="Search"
                                                                runat="server" OnClientClick="return chkSearch_View();" OnClick="btn_ShowSearchResult_View_onClick"/>
                                                            <input class="btn_bl" name="btn_Home_ViewAll" id="Button1" type="button" value=" Back"
                                                                onclick="return btn_Home_onClick();"/>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <hr style="height: 1px; color: #ADBBCA;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div id="div_ShowAccessObjectRoles" class="mGrid" style="" runat="server" visible="false">
                                                                <asp:GridView ID="dgViewAll" runat="server" CellPadding="2" AutoGenerateColumns="false"
                                                                    PageSize="15" AllowPaging="true" OnPageIndexChanging="dgViewAll_PageIndexChanging"
                                                                    OnRowDataBound="CreateServerSideLink" BackColor="White" Width="95%" BorderColor="#ADBBCA"
                                                                    BorderStyle="Solid" BorderWidth="1px" EmptyDataText="No Data Found!!!" CaptionAlign="Left" CssClass="mgrid">
                                                                    <PagerSettings FirstPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageFirst.gif" FirstPageText="View First Page"
                                                                        LastPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageLast.gif" NextPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageNext.gif"
                                                                        PreviousPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PagePrev.gif" LastPageText="View Last Page"
                                                                        Mode="NextPreviousFirstLast" NextPageText="View Next Page" PreviousPageText="View Prevoius Page" />
                                                                    <%--<FooterStyle BackColor="White" Font-Bold="True" ForeColor="Blue" />
                                                                    <EmptyDataRowStyle Font-Bold="True" Font-Size="X-Small" ForeColor="Red" HorizontalAlign="Center" />
                                                                    <PagerStyle BackColor="White" ForeColor="Blue" HorizontalAlign="Left" Font-Underline="true"
                                                                        VerticalAlign="Middle" />
                                                                    <HeaderStyle BackColor="#203250" Font-Bold="True" ForeColor="White" BorderColor="#ADBBCA"
                                                                        BorderStyle="Solid" BorderWidth="1px" />
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" BorderColor="#ADBBCA"
                                                                        BorderStyle="Solid" BorderWidth="1px" />--%>
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="REQUEST ID" DataField="request_id" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="right"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="ROLE NAME" DataField="ACCESS_ROLE_NAME" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="PANEL NAME" DataField="pane_desc" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="REQUEST ISSUE DATE" DataField="AMD_DATE" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="center"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="APPROVER NAME" DataField="approve_by" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="REQUEST APPROVED DATE" DataField="approve_date" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="center"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="REQUEST STATUS" DataField="approve_status" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="left"></asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div id="div_RequestDtl">
                                                <div id="Div_RequestDtl_Header">
                                                </div>
                                                <div id="Div_RequestDtl_Dtl">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
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
            <td>
                <input type="hidden" id="txt_XMLData" name="txt_XMLData" value="" runat="server" />
            </td>
        </tr>
    </table>
    <!-- End of Hideen Field -->
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">
<!--
  
-->
</script>
</html>
