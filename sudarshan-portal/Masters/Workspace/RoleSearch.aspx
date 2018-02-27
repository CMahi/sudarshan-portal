<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleSearch.aspx.cs" Inherits="RoleSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Role</title>
<%--    <link href="/Sudarshan-Portal-NEW/css/helpdesk_style.css" rel="stylesheet" type="text/css" />
    <link href="/Sudarshan-Portal-NEW/css/innocent_flowers.css" rel="stylesheet" type="text/css" />
    <link href="/Sudarshan-Portal-NEW/css/sbm_app01.css" rel="stylesheet" type="text/css" />--%>
      <link href="../../CSS/body.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/utility.js"></script>
    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/validator.js"></script>
</head>
<body class="ApBody" onload="DisableBrowserClone();" >
    <form id="frm_RoleSearch" runat="server">
    <div class="col_fullwidth3">
    <h2>Search Role</h2>
       <table id="tbl_ContentPlaceHolder" align="center" cellpadding="0" cellspacing="0"  width="100%"  >
         <tr>     
             <td align="center" >
                <table id="tbl_SearchRoles" runat="server" cellpadding="0" cellspacing="0" style="width:100%; text-align: left;">
                   <tr>
          			<td align="center" >
	      		        <table id="tbl_SearchRole" width="100%" cellpadding="0" cellspacing="0" style="font-family: Verdana, Arial, Helvetica, sans-serif;
             		     font-size: 10px;	color: #000000;	text-align:Left;	padding: 0px; border-collapse: collapse; border: 1px solid #ADBBCA;">
                            <tr > <td> <br /> </td></tr>
                            <tr > 
                                <td>
            		                <table id="tbl_Search" align="center" cellpadding="0" cellspacing="0" width="100%" runat="server" >
							           <tr>
          								 <td align="center"><font face="Verdana" size="2">Enter Role</font>&nbsp;&nbsp;&nbsp; 
		      							     <input class="ApInptTxt" type="text" id="txt_SearchPattern" name="txt_SearchPattern" size="30"  runat="server" />&nbsp;&nbsp;
										     <asp:Button  CssClass="btn_rd"  ID="btn_Search"  Text="Search" runat="server" OnClientClick="return validateforms();" Onclick="btn_Search_onClick" />
										     <input class="btn_bl" name="btn_Back"  id="btn_Back"  type="button" value=" Back"  onclick="window.close();" />
										</td>
                                       </tr> 
                                       <tr>
          								 <td align="center">
          								     <div id="div_txt_SearchPattern" visible="false" style="text-align: center; color: Red; width: 100%" runat="server">
                                                <asp:Image ID="img_txt_SearchPattern" runat="server" 
                                                     ImageUrl="~/Images/error.gif" />
                                                <asp:Label ID="lbl_txt_SearchPattern" runat="server" Text=""></asp:Label>
                                            </div>
										</td>
                                       </tr> 
                                       <tr>
                                            <td  align="center" ><hr  style="height:1px; color:#ADBBCA;"  /></td>
                                       </tr>
                                       <tr>   
          							     <td align="center">
		     							    <div id="div_DiaplayResultSetRoleInfo" class="innocent_flowers" style="width:90%"  runat="server">
		     							        <asp:GridView ID="dgSearchResult" runat="server" CellPadding="2" 
		     							                AllowPaging="true"  AutoGenerateColumns="false"
		     							                OnRowDataBound="CreateLink"
                                                        OnPageIndexChanging="dgSearchResult_PageIndexChanging" 
                                                        BackColor="White" Width="98%" BorderColor="#ADBBCA" BorderStyle="Solid" 
                                                        BorderWidth="1px" EmptyDataText="No Data Found!!!" CaptionAlign="Left" CssClass="mgrid">
                                                        <PagerSettings FirstPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageFirst.gif" 
                                                        FirstPageText="View First Page" LastPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageLast.gif" 
                                                        NextPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageNext.gif" 
                                                        PreviousPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PagePrev.gif" 
                                                        LastPageText="View Last Page" Mode="NextPreviousFirstLast" 
                                                        NextPageText="View Next Page" PreviousPageText="View Prevoius Page" />
                                                    <%--<EmptyDataRowStyle Font-Bold="True" Font-Size="X-Small" ForeColor="Red" HorizontalAlign="Center" />
                                                    <PagerStyle BackColor="White" ForeColor="Blue" HorizontalAlign="Left" 
                                                        Font-Underline="true" VerticalAlign="Middle" />
                                                    <HeaderStyle BackColor="#203250" Font-Bold="True" ForeColor="White" 
                                                        BorderColor="#ADBBCA" BorderStyle="Solid" BorderWidth="1px" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" 
                                                        BorderColor="#ADBBCA" BorderStyle="Solid" BorderWidth="1px" />--%>
                                                   <Columns  >
                                                        <asp:BoundField HeaderText="ROLE NAME" DataField="Role Name"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"  ></asp:BoundField>
                                                        <asp:BoundField HeaderText="ROLE DESCRIPTION" DataField="Role Description"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                        <asp:BoundField HeaderText="COMPANY ID" DataField="Company ID"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                                   </Columns>
                                                 </asp:GridView>
                                             </div>
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
       <table id="tbl_Hideen" visible="false">
        <tr>
            <td>
                <input type="hidden" id="txt_ActionBy" name="txt_ActionBy" value="" runat="server"/>
            </td>
            <td>
                <input type="hidden" id="txt_ActionCondn" name="txt_ActionCondn " value="" runat="server" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">
<!--

  function setParent(RoleID,RoleName)
  {
     try
     {
         window.opener.document.getElementById("txt_RoleID").value=RoleID;
         window.opener.document.getElementById("txt_Search_RoleName").value=RoleName;
         window.close(this);
     }
     catch(Exc){}
  }
  
-->
</script>
</html>
