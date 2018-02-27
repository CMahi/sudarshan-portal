<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSearch.aspx.cs" Inherits="UserSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Search User</title>
    <link href="/Sudarshan-Portal-NEW/css/helpdesk_style.css" rel="stylesheet" type="text/css" />
    <link href="/Sudarshan-Portal-NEW/css/innocent_flowers.css" rel="stylesheet" type="text/css" />
    <link href="/Sudarshan-Portal-NEW/css/sbm_app01.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/utility.js"></script>
    <script language="JavaScript" type="text/javascript" src="/Sudarshan-Portal-NEW/JS/validator.js"></script>
</head>
<body class="ApBody" onload="DisableBrowserClone();">
    <form id="frm_UserSearch" runat="server">
       <table id="tbl_ContentPlaceHolder" align="center" cellpadding="0" cellspacing="0"  width="80%"  >
         <tr>     
             <td align="center" >
                <table id="tbl_SearchUsers" runat="server" cellpadding="0" cellspacing="0" style="width:100%; text-align: left;">
                   <tr>
                     <td  >
                        <div id="Div2" >
	                        <div  style="height: 4%;background-image: url(/Sudarshan-Portal-NEW/Images/topmiddle.jpg);background-repeat: repeat-x;">
		                        <div  style="float: left;width: 8px;	background-image:  url(/Sudarshan-Portal-NEW/Images/topleft.jpg); background-repeat: no-repeat;"></div>
		                        <div  style="float: left;padding-top: 4px;"><strong style="text-align:Center; color:#9900FF;">Search User</strong></div>   
		                        <div  style="float: right;width: 9px;background-image:  url(/Sudarshan-Portal-NEW/Images/topright.jpg);background-repeat: no-repeat;"></div>
		                    </div>
                        </div>   
                     </td>
                   </tr>
                   <tr>
          			<td align="center" >
	      		        <table id="tbl_SearchUser" width="100%" cellpadding="0" cellspacing="0" style="font-family: Verdana, Arial, Helvetica, sans-serif;
             		     font-size: 10px;	color: #000000;	text-align:Left;	padding: 0px; border-collapse: collapse; border: 1px solid #ADBBCA;">
                            <tr > <td> <br /> </td></tr>
                            <tr > 
                                <td>
            		                <table id="tbl_Search" align="center" cellpadding="0" cellspacing="0" width="100%" runat="server" >
							           <tr>
          								 <td align="center"><font face="Verdana" size="2">Enter User</font>&nbsp;&nbsp;&nbsp; 
		      							     <input class="ApInptTxt" type="text" id="txt_SearchPattern" name="txt_SearchPattern" size="30"  runat="server">&nbsp;&nbsp;
										     <asp:Button  CssClass="ApScrnButton"  ID="btn_Search"  Text="Search" runat="server" OnClientClick="return validateforms();" Onclick="btn_Search_onClick" onmouseout="this.className='ApScrnButton';" onmouseover="this.className='ApScrnButtonHover';" />
										     <input class="ApScrnButton" name="btn_Back"  id="btn_Back"  type="button" value=" Back"  onclick="window.close();" onmouseout="this.className='ApScrnButton';" onmouseover="this.className='ApScrnButtonHover';" />
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
		     							    <div id="div_DiaplayResultSetUserInfo" class="innocent_flowers" style="width:95%"  runat="server">
		     							        <asp:GridView ID="dgSearchResult" runat="server" CellPadding="2" 
		     							                AllowPaging="true"  AutoGenerateColumns="false"
		     							                OnRowDataBound="CreateLink"
                                                        OnPageIndexChanging="dgSearchResult_PageIndexChanging" 
                                                        BackColor="White" Width="80%" BorderColor="#ADBBCA" BorderStyle="Solid" 
                                                        BorderWidth="1px" EmptyDataText="No Data Found!!!" CaptionAlign="Left">
                                                        <PagerSettings FirstPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageFirst.gif" 
                                                        FirstPageText="View First Page" LastPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageLast.gif" 
                                                        NextPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PageNext.gif" 
                                                        PreviousPageImageUrl="/Sudarshan-Portal-NEW/Images/GAT_TB_PagePrev.gif" 
                                                        LastPageText="View Last Page" Mode="NextPreviousFirstLast" 
                                                        NextPageText="View Next Page" PreviousPageText="View Prevoius Page" />
                                                    <EmptyDataRowStyle Font-Bold="True" Font-Size="X-Small" ForeColor="Red" HorizontalAlign="Center" />
                                                    <PagerStyle BackColor="White" ForeColor="Blue" HorizontalAlign="Left" 
                                                        Font-Underline="true" VerticalAlign="Middle" />
                                                    <HeaderStyle BackColor="#203250" Font-Bold="True" ForeColor="White" 
                                                        BorderColor="#ADBBCA" BorderStyle="Solid" BorderWidth="1px" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" 
                                                        BorderColor="#ADBBCA" BorderStyle="Solid" BorderWidth="1px" />
                                                   <Columns  >
                                                        <asp:BoundField HeaderText="USER NAME" DataField="ad_id"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"  ></asp:BoundField>
                                                        <asp:BoundField HeaderText="EMPLOYEE ID" DataField="emp_id"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
                                                        <asp:BoundField HeaderText="EMAIL ID" DataField="email_id" ItemStyle-Wrap="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"  ></asp:BoundField>
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
        </tr>
    </table>
    </form>
</body>
<script language="javascript" type="text/javascript">
<!--

  function setParent(User_ADID)
  {
     try
     {
         window.opener.document.getElementById("txt_Search_UserName").value=User_ADID;
         window.close(this);
     }
     catch(Exc){}
  }
  
-->
</script>
</html>
