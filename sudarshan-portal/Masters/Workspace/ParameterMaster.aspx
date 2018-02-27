<%@ Page Language="C#" EnableEventValidation="false" ValidateRequest="false" AutoEventWireup="true"
    CodeFile="ParameterMaster.aspx.cs" Inherits="ParameterMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <link href="../../CSS/sbm_app01.css" rel="stylesheet" type="text/css" />
    <link href="../../css/helpdesk_style.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/innocent_flowers.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="/MQueue/JS/utility.js" type="text/javascript"></script>
    <script language="JavaScript" src="/MQueue/JS/validator.js" type="text/javascript"></script>
    <title>Parameter Master</title>
 <link href="../../CSS/body.css" rel="Stylesheet" type="text/css" /></head>
<body class="ApBody" onload="DisableBrowserClone();">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table id="tbl_ContentPane" cellpadding="0" cellspacing="0" style="width: 100%; text-align: left;">
        <tr>
            <td>
                <div id="content">
                    <div style="height: 4%; background-image: url(/MQueue/Images/topmiddle.jpg); background-repeat: repeat-x;">
                        <div style="float: left; width: 8px; background-image: url(/MQueue/Images/topleft.jpg);
                            background-repeat: no-repeat;">
                        </div>
                        <div style="float: left; padding-top: 2px;">
                            <strong style="font-family: Verdana; text-align: center; color: Blue;">Parameter Master</strong></div>
                        <div style="float: right; width: 9px; background-image: url(/MQueue/Images/topright.jpg);
                            background-repeat: no-repeat;">
                        </div>
                        <div style="float: right; padding-top: 2px; padding-right: 10px">
                            <asp:Label ID="lblMode" runat="server" Text="" Font-Bold="true" Height="3" ForeColor="Red"></asp:Label></div>
                    </div>
                </div>
            </td>
        </tr>
        <tr id="tbl_showMyButtonPanel" runat="server">
            <td align="center">
                <div runat="server" style="display: block">
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
                                            <input  runat="server" name="btn_AddNew_Go" id="btn_AddNew_Go"
                                                type="button" class="btn_bl" value="Add New" onserverclick="btn_Add_onClickServer" 
                                                 />
                                            <%--  <input  runat="server" name="btn_Search_Go" id="btn_Search_Go"
                                                type="button" class="btn_bl" value="Search" onclick="btn_Search_onClick(1)" 
                                                 />
                                            <input  runat="server" name="btn_ViewAll_Go" id="btn_ViewAll_Go"
                                                type="button" class="btn_bl" value="View All" onserverclick="btn_ViewAll_onClick" 
                                                 />--%>
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
                <div runat="server">
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
                                                        <table width="100%" align="center">
                                                            <tr>
                                                                <td style="background-color: white; width: 17%">
                                                                    Select Panal<font color="red">*</font>
                                                                </td>
                                                                <td style="background-color: white; width: 1%">
                                                                    :
                                                                </td>
                                                                <td style="background-color: white; width: 32%">
                                                                    <asp:DropDownList ID="ddl_Add_Panel" runat="server" CssClass="ddlinputbox" Width="80%"
                                                                        OnSelectedIndexChanged="getPanelObjects" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="background-color: white; width: 17%">
                                                                    Select Object<font color="red">*</font>
                                                                </td>
                                                                <td style="background-color: white; width: 1%">
                                                                    :
                                                                </td>
                                                                <td style="background-color: white; width: 32%">
                                                                    <asp:DropDownList ID="ddl_Object" runat="server" CssClass="ddlinputbox" Width="80%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="background-color: white; width: 17%" colspan="3" align="center">
                                                                    <asp:GridView ID="Grd_Viewall" DataKeyNames="PK_PREFERENCE_ID" CssClass="mGrid" HeaderStyle-CssClass="tbl_head" runat="server" CellPadding="2" AllowPaging="true"
                                                                         OnPageIndexChanging="dgView_PageIndexChanging" AutoGenerateColumns="false">
                                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                        <RowStyle  ForeColor="#333333" />
                                                                        <EditRowStyle  />
                                                                        <SelectedRowStyle  Font-Bold="True" ForeColor="#333333" />
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Underline="True" />
                                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                        <AlternatingRowStyle  ForeColor="#284775" />
                                                                        <Columns>
                                                                        <asp:BoundField HeaderText="pk" DataField="PK_PREFERENCE_ID" Visible="false" />
                                                                        <asp:BoundField HeaderText="OBJECT NAME" DataField="OBJ_NAME" />
                                                                            <asp:BoundField HeaderText="PANEL DESCRIPTION" DataField="PANE_DESC" />
                                                                        <asp:TemplateField ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="Btn_Delete" runat="server" ImageUrl="~/Images/Delete.GIF" OnClientClick="return confirmMain_delete(this.id)"
                                                                            OnClick="deleteRecord" />
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <hr class="hrClass" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 35%; background-color: white">
                                                                </td>
                                                                <td align="left" style="width: 65%">
                                                                    <asp:Button CssClass="btn_bl" ID="btn_Add" runat="server" Text="Set As Default Page" 
                                                                        OnClick="btn_New_onClick" OnClientClick="return IsMandotoryAdd();"></asp:Button>
                                                                    <input  runat="server" name="btn_Reset_Adds" id="btn_Reset_Adds"
                                                                        type="button" class="btn_bl" value="Clear" onclick="ClearField();"  />
                                                                    <asp:Button CssClass="btn_bl" ID="btn_Home" runat="server" Text="Back"  OnClick="btn_Home_onClick"
                                                                        OnClientClick="ClearField();"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
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
        <tr id="tbl_showMyEditPanel" runat="server">
            <td align="center">
                <div visible="true" runat="server">
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
                                                        <table width="50%" align="center">
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <hr class="hrClass" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td align="center" style="width: 65%" colspan="6">
                                                        <asp:Button CssClass="btn_bl" ID="btn_Edit" runat="server" Text="Update"  OnClick="btn_Edit_onClick"
                                                            OnClientClick="return IsMandotoryEdit();"></asp:Button>
                                                        <asp:Button CssClass="btn_bl" ID="btn_Delete" runat="server" Text="Delete"  OnClick="btn_Delete_onClick">
                                                        </asp:Button>
                                                        <asp:Button CssClass="btn_bl" ID="btn_Back" runat="server" Text="Back"  OnClick="btn_Back_Edit_onclick">
                                                        </asp:Button>
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
                                                    <td align="center" colspan="">
                                                        <div id="dgdiv" runat="server" style="width: 100%">
                                                            <asp:GridView ID="dgViewAll" CssClass="mGrid" HeaderStyle-CssClass="tbl_head" runat="server" CellPadding="2" AllowPaging="true"
                                                                 OnPageIndexChanging="dgView_PageIndexChanging">
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle  ForeColor="#333333" />
                                                                <EditRowStyle  />
                                                                <SelectedRowStyle  Font-Bold="True" ForeColor="#333333" />
                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Underline="True" />
                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                <AlternatingRowStyle  ForeColor="#284775" />
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
                                                        <input  name="btn_AllEmpInfo_Back" id="btn_AllEmpInfo_Back" type="button" class="btn_bl"
                                                            value="Back" runat="server" onserverclick="btn_AllEmpInfo_Back_onClick" 
                                                             />
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
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="conditionButton" style="display: none">
                    <asp:TextBox ID="txtSupplierID" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txtCityID" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txtParameterID" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txtCondition" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_UserName" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txt_DomainName" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txt_AMDDate" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txt_Description" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txt_ActionObjectID" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_ProcName" runat="server" Width="0.5px" Text="BRANCH_PREMISE_HDR"></asp:TextBox>
                    <asp:TextBox ID="txt_SearchString" runat="server" Width="0.5px" Text="%"></asp:TextBox>
                    <asp:TextBox ID="txt_Pk_id" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_LandLords" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_LandLords1" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_INS_XML" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_UPD_XML" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_PremiseHdrId" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_InnerHtml" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_Landlord3" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_Criterias" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_Criterias1" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_CriteriaXML" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_Upd_CriteriaXML" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_Activitydata" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_updatedata" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_branchId" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_XMLADDitionalDetails" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_CriteriaHTML" runat="server" Width="0.5px"></asp:TextBox>
                    <asp:TextBox ID="txt_ProductLineHTML" runat="server" Width="0.5px"></asp:TextBox>
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
    </form>
</body>
<script language="javascript" type="text/javascript">

    // <!CDATA[
    var searchValue = '';
    SearchArray = new Array(3);
    DisplayArray = new Array(3);
    SearchArray[0] = new Array(5);
    DisplayArray[0] = new Array(15);

    function ClearField() {
        document.getElementById("ddl_Add_Panel").value = "";
        document.getElementById("ddl_Object").value = "";
    }
    function IsMandotoryAdd() {
        document.getElementById("ddl_Add_Panel").setAttribute("isMandatory", "true");
        document.getElementById("ddl_Object").setAttribute("isMandatory", "true");
        if (validateforms()) {
            return true;
        }
        else
            return false;
    }

    function IsMandotoryEdit() {


        if (validateforms()) {
            return true;
        }
        else
            return false;
    }
    function display() {
        var DisplayContent;
        document.getElementById("lblMode").innerText = "Edit";
        if (searchValue == 1) {
            document.getElementById("tbl_showMyButtonPanel").style.display = "none";
            document.getElementById("tbl_showMyEditPanel").style.display = "block";
            document.getElementById("txt_PremiseHdrId").value = DisplayArray[0][0];
            document.getElementById("txt_Edit_BranchCode").value = DisplayArray[0][2];
            document.getElementById("txt_Edit_BranchCode").setAttribute("readOnly", "readOnly");
            document.getElementById("txt_Edit_Address").value = DisplayArray[0][3];
            document.getElementById("txt_Edit_PremiseCode").value = DisplayArray[0][4];
            document.getElementById("txt_Edit_PremiseCode").setAttribute("readOnly", "readOnly");
            for (var Index = 0; Index < document.getElementById("ddl_Edit_BranchName").options.length; Index++) {
                if (document.getElementById("ddl_Edit_BranchName").options[Index].text == DisplayArray[0][1]) {
                    document.getElementById("ddl_Edit_BranchName").selectedIndex = Index;
                }
            }
            if (DisplayArray[0][5] != "") {
                for (var Index = 0; Index < document.getElementById("ddl_Edit_PremiseType").options.length; Index++) {
                    if (document.getElementById("ddl_Edit_PremiseType").options[Index].text == DisplayArray[0][5]) {
                        document.getElementById("ddl_Edit_PremiseType").selectedIndex = Index;
                    }
                }
            }
            document.getElementById('txt_branchId').value = document.getElementById("ddl_Edit_BranchName").options[document.getElementById("ddl_Edit_BranchName").selectedIndex].value;
            document.getElementById("ddl_Edit_BranchName").disabled = true;
            if (DisplayArray[0][6] != "") {
                for (var Index = 0; Index < document.getElementById("ddl_Edit_RentType").options.length; Index++) {
                    if (document.getElementById("ddl_Edit_RentType").options[Index].text == DisplayArray[0][6]) {
                        document.getElementById("ddl_Edit_RentType").selectedIndex = Index;
                    }
                }
            }
        }
    }

    function btn_Search_onClick(id) {
        searchValue = id;
        if (searchValue == 1) {
            SearchArray[0][0] = "select";
            SearchArray[0][1] = "PREMISE";
            SearchArray[0][2] = "Branch Name";
            SearchArray[0][3] = "TextBox";

        }
        OpenSearch();
    }
    function CheckCode() {
        var code = document.getElementById('txt_Add_PremiseCode').value;
        Premises_Master.CheckCode(code, Call_check);
    }

    function Call_check(response) {
        if (response.value == "TRUE") {
            alert('Code Already Present. Kindly Check Details.');
            document.getElementById('txt_Add_PremiseCode').value = "";
        }
    }

    function GetBranchCode() {
        var ddlval = document.getElementById('ddl_Add_BranchName').options[document.getElementById('ddl_Add_BranchName').selectedIndex].value;
        Premises_Master.GetBranchCode(ddlval, Call_Branch);
    }

    function Call_Branch(response) {
        document.getElementById('txt_BranchCode').value = response.value;
        document.getElementById('txt_BranchCode').setAttribute("readOnly", "readOnly");
    }

    function confirmMain_delete(id) {

        if (confirm("Are you sure you want to delete ?") == true) {
            return true;
        }
        else
            return false;
    }

</script>
</html>
