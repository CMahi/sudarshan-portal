<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="ReleaseNotes.aspx.cs" Inherits="HD_TaskType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<%--    <link href="../../CSS/sbm_app01.css" rel="stylesheet" type="text/css" />
    <link href="../../css/helpdesk_style.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/innocent_flowers.css" rel="stylesheet" type="text/css" />--%>
      <link href="../../CSS/body.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="/Sudarshan-Portal-NEW/JS/utility.js" type="text/javascript"></script>
    <script language="JavaScript" src="/Sudarshan-Portal-NEW/JS/validator.js" type="text/javascript"></script>
    <script language="JavaScript" src="../../JS/ReleaseNote.js" type="text/javascript"></script>
    <title>Release Notes</title>
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
</head>
<body class="ApBody" onload="DisableBrowserClone();">
    <form id="form1" runat="server">
    <div class="col_fullwidth3">
        <h2>Release Notes</h2>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <table id="tbl_ContentPane" cellpadding="0" cellspacing="0" style="width: 100%; text-align: left;">
            <tr id="tbl_showMyButtonPanel" runat="server">
                <td align="center">
                    <div id="Div1" runat="server" style="display: block">
                        <table id="tbl_showMyButtonPanel1" width="100%" cellpadding="0" runat="server" cellspacing="0">
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
                                                <%--  <input class="ApScrnButton" runat="server" name="btn_Search_Go" id="btn_Search_Go"
                                                type="button" value="Search" onclick="btn_Search_onClick(1)" onmouseout="this.className='ApScrnButton';"
                                                onmouseover="this.className='ApScrnButtonHover';" />--%>
                                                <input class="btn_bl" runat="server" name="btn_ViewAll_Go" id="btn_ViewAll_Go"
                                                    type="button" value="View All" onserverclick="btn_ViewAll_onClick"/>
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
                        <table id="tbl_showMyAddPanel1" runat="server" width="100%" cellpadding="0" cellspacing="0">
                            >
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
                                                <table id="tbl_NewEmployee" width="100%" cellpadding="0" cellspacing="0" class="bodycontents">
                                                    >
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <table width="59%" align="center">
                                                                <tr>
                                                                    <td style="background-color: white" class="style1">
                                                                        Module Name<font color="red">*</font>
                                                                    </td>
                                                                    <td style="width: 4%; background-color: white">
                                                                        :
                                                                    </td>
                                                                    <td style="width: 76%; background-color: white">
                                                                        <asp:DropDownList ID="ddl_ModuleName" runat="server" CssClass="ddlinputbox" Width="80%">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="txt_UserID" runat="server" Style="display: none"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="background-color: white" class="style1">
                                                                        Release Date<font color="red">*</font>
                                                                    </td>
                                                                    <td style="width: 4%; background-color: white">
                                                                        :
                                                                    </td>
                                                                    <td style="width: 76%; background-color: white">
                                                                        <asp:TextBox ID="txt_Add_FromDate" Width="30%" runat="server" CssClass="inputbox"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="Cal1" OnClientDateSelectionChanged="DateComparision" runat="server"
                                                                            Format="dd-MMM-yyyy" TargetControlID="txt_Add_FromDate">
                                                                        </asp:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="background-color: white" colspan="3" class="style1">
                                                                        <hr class="hrClass" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Object Name<font color="red">*</font>
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_ObjName" runat="server" CssClass="inputbox" Width="60%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Object Type<font color="red">*</font>
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddl_ObjType" runat="server" Width="60%" CssClass="ddlinputbox">
                                                                            <asp:ListItem Value="">--SELECT ONE--</asp:ListItem>
                                                                            <asp:ListItem Value="ASPX AND CS" Text="ASPX AND CS"></asp:ListItem>
                                                                            <asp:ListItem Value="PROCEDURE">PROCEDURE</asp:ListItem>
                                                                            <asp:ListItem Value="TABLE">TABLE</asp:ListItem>
                                                                            <asp:ListItem Value="JS">JS</asp:ListItem>
                                                                            <asp:ListItem Value="CSS">CSS</asp:ListItem>
                                                                            <asp:ListItem Value="CONFIG">CONFIG</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Action<font color="red">*</font>
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddl_Action" runat="server" Width="60%" CssClass="ddlinputbox">
                                                                            <asp:ListItem Value="">--SELECT ONE--</asp:ListItem>
                                                                            <asp:ListItem Value="ADD" Text="ADD"></asp:ListItem>
                                                                            <asp:ListItem Value="DELETE" Text="DELETE"></asp:ListItem>
                                                                            <asp:ListItem Value="MODIFY" Text="MODIFY"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Comment<font color="red">*</font>
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_DevloperComment" runat="server" TextMode="MultiLine" Height="30px"
                                                                            CssClass="inputbox" Width="80%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" align="center">
                                                                        <input id="Button1" type="button" runat="server" class="btn_bl" value="Add"
                                                                            onclick="AddToTable();" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="tdDtl" runat="server" align="center">
                                                        </td>
                                                    </tr>
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
                                                                        <input class="btn_rd" runat="server" name="btn_save" id="Btn_Save" type="button"
                                                                            value="Save" onclick="Save();" onserverclick="btn_New_onClick"/>
                                                                        <%--<asp:Button ID="btn_Add" runat="server" Text="Save" class="ApScrnButton" OnClick="btn_New_onClick"
                                                                        OnClientClick="IsMandotoryAdd();return validateforms();"></asp:Button>--%>
                                                                        <input class="btn_bl" runat="server" name="btn_Reset_Adds" id="btn_Reset_Adds"
                                                                            type="button" value="Clear" onclick="ClearField();"/>
                                                                        <asp:Button ID="btn_Home" runat="server" Text="Back" class="btn_bl" OnClick="btn_Home_onClick"
                                                                            OnClientClick="ClearField();"></asp:Button>
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
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr id="tbl_showMyViewAllPanel" runat="server" style="display: none">
                <td align="center" style="width: 100%">
                    <table id="tbl_showMyViewAllPanel1" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width:100%">
                                <table id="tbl_DisplayResultSetAllEmployee" cellpadding="0" cellspacing="0" style="width: 100%;
                                    text-align: left;">
                                    <tr>
                                        <td align="center">
                                            <table id="tbl_DisplayResultSetAllEmployeeInfo" width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <div id="dgdiv" runat="server" style="width: 100%">
                                                            <asp:GridView ID="dgViewAll" runat="server" CssClass="mgrid" CellPadding="2" AllowPaging="True"
                                                                BackColor="White" OnPageIndexChanging="dgView_PageIndexChanging" PagerStyle-CssClass="paging">
                                                               <%-- <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="white" ForeColor="#333333" />
                                                                <EditRowStyle BackColor="white" />
                                                                <SelectedRowStyle BackColor="white" Font-Bold="True" ForeColor="#333333" />
                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Underline="True" />
                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />--%>
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
                                                    <td align="left">
                                                        <asp:Button ID="btnExport" runat="server" Text="Export" class="btn_bl" OnClick="btn_Export_onClick">
                                                        </asp:Button>
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
                                                        <input class="btn_bl" name="btn_AllEmpInfo_Back" id="btn_AllEmpInfo_Back" type="button"
                                                            value="Back" runat="server" onserverclick="btn_AllEmpInfo_Back_onClick" />
                                                    </td>
                                                </tr>
                                            </table>
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
            <tr>
                <td>
                    <div id="conditionButton" style="display: none">
                        <asp:TextBox ID="txtCatID" runat="server" Width="0.5px"></asp:TextBox>
                        <asp:TextBox ID="txt_UserName" runat="server" Width="0.5px"></asp:TextBox>
                        <asp:TextBox ID="txt_DomainName" runat="server" Width="0.5px"></asp:TextBox>
                        <asp:TextBox ID="txt_ProcName" runat="server" Width="0.5px" Text="ITEMCAT"></asp:TextBox>
                        <asp:TextBox ID="txt_SearchString" runat="server" Width="0.5px" Text="%"></asp:TextBox>
                        <asp:TextBox ID="txtCondition" runat="server" Width="0.5px"></asp:TextBox>
                        <asp:TextBox ID="txtParameterID" runat="server" Width="1px"></asp:TextBox>
                        <asp:TextBox ID="txtCreatedBy" runat="server" Width="1px"></asp:TextBox>
                        <asp:TextBox ID="txtExpenseTypeID" runat="server" Width="1px"></asp:TextBox>
                        <asp:TextBox ID="txt_AMDDate" runat="server" Width="1px"></asp:TextBox>
                        <asp:TextBox ID="txt_Description" runat="server" Width="1px"></asp:TextBox>
                        <asp:TextBox ID="txt_ActionObjectID" runat="server" Width="200px"></asp:TextBox>
                        <asp:TextBox ID="txt_XML" runat="server" Width="200px"></asp:TextBox>
                        <br />
                        <br />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="DivDisplayError" runat="server">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">

    // <!CDATA[
    var searchValue = '';
    SearchArray = new Array(3);
    DisplayArray = new Array(3);
    SearchArray[0] = new Array(5);
    DisplayArray[0] = new Array(10);

    function ClearField() {
        document.getElementById("ddl_ModuleName").selectedIndex = 0;
        document.getElementById("txt_Add_FromDate").value = "";
    }
    function IsMandotoryAdd() {
        document.getElementById("ddl_ModuleName").setAttribute("isMandatory", "true");
        document.getElementById("txt_Add_FromDate").setAttribute("isMandatory", "true");
    }
    function display() {
        var DisplayContent;
        document.getElementById("lblMode").innerText = "Add";
        if (searchValue == 1) {
            document.getElementById("tbl_showMyButtonPanel").style.display = "none";
            document.getElementById("txt_Add_Users").value = DisplayArray[0][1];
            document.getElementById("txt_UserID").value = DisplayArray[0][0];
        }
    }
    function btn_Search_onClick(id) {
        searchValue = id;
        if (searchValue == 1) {
            SearchArray[0][0] = "select";
            SearchArray[0][1] = "USER_LEAVE";
            SearchArray[0][2] = "User Name";
            SearchArray[0][3] = "TextBox";
        }
        OpenSearch();
    }

    function DateComparision(sender, args) {
        var seldate = sender.get_selectedDate();
        var todaydate = new Date();

        if (seldate > todaydate) {
        }
        else {
            if (seldate.toLocaleDateString() != todaydate.toLocaleDateString()) {
                alert('Date cannot be the past Date');
                sender._textbox.set_Value('');
            }
        }
    }

    function CheckDate() {
        if (document.getElementById("txt_Add_ToDate").value != "" && document.getElementById("txt_Add_FromDate").value != "") {
            var cal1 = ConvertDatetimeCustom(document.getElementById("txt_Add_FromDate").value);
            var cal2 = ConvertDatetimeCustom(document.getElementById("txt_Add_ToDate").value);
            var myDate1 = new Date(cal1);
            var myDate2 = new Date(cal2);
            if (myDate1 > myDate2) {
                alert("Kindly Select Valid Date.");
                document.getElementById("txt_Add_ToDate").value = "";
                document.getElementById("txt_Add_ToDate").focus();
            }
        }
    }
    function ConvertDatetimeCustom(Datevalue) {
        arr = new Array(3);
        arr = Datevalue.split('-');
        var fromdatetocal = arr[1] + " " + arr[0] + " " + arr[2];
        return fromdatetocal;
    }

</script>
</html>
