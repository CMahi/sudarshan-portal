<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_Role_Mapping.aspx.cs"
    Inherits="User_Role_Mapping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User To Role Mapping</title>
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
<%--    <link href="/Sudarshan-Portal-NEW/CSS/helpdesk_style.css" rel="stylesheet" type="text/css" />
    <link href="/Sudarshan-Portal-NEW/css/innocent_flowers.css" rel="stylesheet" type="text/css" />
    <link href="/Sudarshan-Portal-NEW/css/sbm_app01.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../CSS/body.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/user_Role_Mapping.js"></script>
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
    <form id="frm_UserRoleMapping" runat="server">
    <div class="col_fullwidth3">
        <h2>User To Role Mapping</h2>
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
                                        <strong style="text-align: Center; color: #9900FF;">User - Role Management</strong></div>
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
                                                <asp:Button CssClass="btn_bl" ID="btn_Add" Text=" Add " Visible="false" Enabled="false"
                                                    runat="server" OnClientClick="ResizeFrameWindows(true);" OnClick="btn_Add_onClick"
                                                     />
                                                <asp:Button CssClass="btn_bl" ID="btn_Modify" Text="Modify" Visible="false"
                                                    Enabled="false" runat="server" OnClientClick="ResizeFrameWindows(true);" OnClick="btn_Modify_onClick"
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
                                        <strong style="text-align: Center; color: #9900FF;">User - Role Maping</strong></div>
                                    <div style="float: right; width: 9px; background-image: url(/Sudarshan-Portal-NEW/Images/topright.jpg);
                                        background-repeat: no-repeat;">
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>--%>
                        <tr>
                            <td align="center">
                                <table id="tbl_EditAccessObjectRole" width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 100%" colspan="2">
                                            <table id="tbl_Search" cellpadding="0" cellspacing="0" width="50%" runat="server">
                                                <tr>
                                                    <td align="right" style="width: 20%">
                                                        Select Role
                                                    </td>
                                                    <td style="width: 80%" align="center">
                                                        <input class="ApInptTxt" type="text" id="txt_Search_RoleName" name="txt_Search_RoleName"
                                                            size="30" runat="server"/>&nbsp;<a href="#" title="Click here to search role." onclick="window.open('RoleSearch.aspx','_blank','left=150,top=100,width=700,height=500,fullscreen=no,location=no,directories=no,copyhistory=no,toolbars=no,menubars=no,status=no,scrollbars=yes,resize=yes,dependent=yes');"><img
                                                                src="../../Images/search.gif" alt="Click here to search role." /></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 20%">
                                                        Select User
                                                    </td>
                                                    <td style="width: 80%" align="center">
                                                        <input class="ApInptTxt" type="text" id="txt_Search_UserName" name="txt_Search_UserName"
                                                            size="30" runat="server" />&nbsp;<a href="#" title="Click here to search user." onclick="window.open('UserSearch.aspx','_blank','left=150,top=100,width=600,height=500,fullscreen=no,location=no,directories=no,copyhistory=no,toolbars=no,menubars=no,status=no,scrollbars=yes,resize=yes,dependent=yes');"><img
                                                                src="../../Images/search.gif" alt="Click here to search user." /></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <div id="div_txt_XMLData" visible="false" style="text-align: center; color: Red;
                                                            width: 100%" runat="server">
                                                            <asp:Image ID="img_txt_XMLData" runat="server" ImageUrl="~/Images/error.gif" />
                                                            <asp:Label ID="lbl_txt_XMLData" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <input class="btn_rd" name="btn_AddToGrid" id="btn_AddToGrid" type="button"
                                                            runat="server" value=" Add " onclick="return makeUserRoleMapGrid();" 
                                                             />
                                                        <asp:Button CssClass="btn_bl" ID="btn_ShowData" Text="Show" runat="server"
                                                            OnClientClick="return chk_ModificationSearch();" OnClick="btn_Show_onClick" 
                                                             />
                                                        <input class="btn_bl" name="btn_Search_Back" id="btn_Search_Back" type="button"
                                                            value=" Back" onclick="return btn_Home_onClick();" 
                                                             />
                                                    </td>
                                                </tr>
                                            </table>
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
                                        <td align="center" colspan="2" style="width:100%">
                                            <hr style="height: 1px; color: #ADBBCA;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="width: 100%">
                                            <table id="tbl_SearchResult" width="100%" align="center" cellpadding="0" cellspacing="0"
                                                runat="server">
                                                <tr>
                                                    <td align="center">
                                                        <div id="div_ObjMap" class="" style="" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width:100%">
                                                        <div id="div_Save" style="" runat="server">
                                                            <br />
                                                            <asp:Button CssClass="btn_rd" ID="btn_Save" Text=" Save " runat="server" OnClientClick="return makeXML();"
                                                                OnClick="btn_Save_onClick"  />
                                                            <asp:Button CssClass="btn_bl" ID="btn_ModifySave" Text=" Save " runat="server"
                                                                OnClientClick="return makeModifyXML();" OnClick="btn_ModifySave_onClick" 
                                                                 />
                                                            <asp:Button CssClass="btn_bl" ID="btn_Reset" Text="Cancel" runat="server" OnClick="ResetForm"
                                                                 />
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
                                            <strong style="text-align: Center; color: #9900FF;">User - Role Map</strong></div>
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
                                            <div id="div_RequestHdr" style="width: 100%">
                                                <table align="center" width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center" style="width: 100%">
                                                            Enter Role&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input class="ApInptTxt"
                                                                type="text" id="txt_SearchRole_View" name="txt_SearchRole_View" size="30" maxlength="30"
                                                                runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="width: 100%">
                                                            Enter User&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input class="ApInptTxt"
                                                                type="text" id="txt_SearchUser_View" name="txt_SearchUser_View" size="30" maxlength="30"
                                                                runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="width: 100%">
                                                            <div id="div_txt_Search" visible="false" style="text-align: center; color: Red;"
                                                                runat="server">
                                                                <asp:Image ID="img_txt_Search" runat="server" ImageUrl="~/Images/error.gif" />
                                                                <asp:Label ID="lbl_txt_Search" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="width: 100%">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button CssClass="btn_rd" ID="btn_ShowSearchResult_View" Text="Search"
                                                                runat="server" OnClientClick="return chkSearch_View();" OnClick="btn_ShowSearchResult_View_onClick"
                                                                 />
                                                            <input class="btn_bl" name="btn_Home_ViewAll" id="btn_Home_ViewAll" type="button"
                                                                value=" Back" onclick="return btn_Home_onClick();" 
                                                                 />&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="width:100%">
                                                            <hr style="height: 1px; color: #ADBBCA;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <div id="div_ShowUserRoles" class="innocent_flowers" style="width: 90%" runat="server"
                                                                visible="false">
                                                                <asp:GridView ID="dgViewAll" runat="server" CellPadding="2" AutoGenerateColumns="false"
                                                                    PageSize="15" AllowPaging="true" OnPageIndexChanging="dgViewAll_PageIndexChanging"
                                                                    OnRowDataBound="CreateServerSideLink" BackColor="White" Width="95%" BorderColor="#ADBBCA"
                                                                    BorderStyle="Solid" BorderWidth="1px" EmptyDataText="No Data Found!!!" CaptionAlign="Left" CssClass="mgrid">
                                                                    <PagerSettings FirstPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageFirst.gif" FirstPageText="View First Page"
                                                                        LastPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageLast.gif" NextPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageNext.gif"
                                                                        PreviousPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PagePrev.gif" LastPageText="View Last Page"
                                                                        Mode="NextPreviousFirstLast" NextPageText="View Next Page" PreviousPageText="View Prevoius Page" />
                                                                   <%-- <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Blue" />
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
                                                                        <asp:BoundField HeaderText="REQUEST FOR" DataField="amd_condition" HeaderStyle-HorizontalAlign="Center"
                                                                            ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="REQUEST ISSUE DATE" DataField="amd_date" HeaderStyle-HorizontalAlign="Center"
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
                </td>
            </tr>
            <!-- End of tbl_showViewAllPanel -->
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
