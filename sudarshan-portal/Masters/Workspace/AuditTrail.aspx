<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="AuditTrail.aspx.cs" Inherits="AuditTrail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<%--    <link href="/Sudarshan-Portal-NEW/css/sbm_app01.css" rel="stylesheet" type="text/css" />
    <link href="/Sudarshan-Portal-NEW/css/helpdesk_style.css" rel="stylesheet" type="text/css" />       
    <link href="/Sudarshan-Portal-NEW/css/innocent_flowers.css" rel="stylesheet" type="text/css" /> --%>
      <link href="../../CSS/body.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../../JS/utility.js"></script>
    <script language="JavaScript"  type="text/javascript" src="../../JS/validator.js" ></script>
<script language="JavaScript" type="text/javascript" src="../../JS/AuditTrail1.js"></script> 
    <title>Audit Trail</title>

</head>
<body>
    <form id="form1" runat="server">
    <div class="col_fullwidth3">
    <h2>Audit Trail</h2>
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
                                <table align="center" id="tbl_ButtonPanel" width="100%" cellpadding="0" cellspacing="2"
                                    style="font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px; color: #000000;
                                     padding: 0px; border-collapse: collapse; border: 0px solid #ADBBCA;">
                                    <tr>
                                    <td  align="right" style="width:40%">Process Name</td>
                                    <td align="left" style="padding-left:10px;" style="width:60%; height:30px;"><asp:DropDownList ID="ddl_ProcessName"  runat="server" DataTextField="PROCESS_NAME" DataValueField="PK_PROCESSID"  CssClass="inputbox" Height="90%"></asp:DropDownList>  </td>
                                    </tr>
                                    
                                     <tr>
                                    <td align="right"  style="width:40%">Instance ID</td>
                                    <td align="left" style="padding-left:10px;"><asp:TextBox ID="txt_InstanceID" runat="server" CssClass="inputbox" Width="150px" ></asp:TextBox>  
                                    </td>
                                    </tr>
                                    <tr >
           			                <td colspan="2"><br /><asp:Button  CssClass="btn_rd"  ID="btn_ShowCases"  Text="Show Cases" runat="server"  OnClick="btn_ShowCases_onClick"  /></td>
           			            </tr>
                                    <tr><td  align="center" colspan="2"><hr style="height:1px; color:#ADBBCA;"  /></td></tr>
			                     <tr>
			                       <td colspan="2">
			                           <div id="Div_Cases"  class="innocent_flowers" style=""  runat="server"></div>
                                    </td> 
			                     </tr> 
			                     <tr>
                                   <td colspan="2" align="center">
                                      <div id="Div_Pagging" class="" style=""  runat="server"></div>
                                   </td>
                                 </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="conditionButton" style="display: none">
                                <asp:TextBox runat="server" ID="txt_ProcessID"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txt_InstanceIDDTL"></asp:TextBox>
                                </div>
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
    </table>
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">
<!--
var ROWNO;
function showPage(FirstPage,PageNo,MaxPage)
{  
   try
   {    
      AuditTrail.ShowNewPage(PageNo,callBack_PageNo);
      for(var Index=FirstPage;Index<=MaxPage;Index++)
      {
         if(document.getElementById("PageNo"+Index)!=undefined)
         {
              if(Index==PageNo)
                document.getElementById("PageNo"+Index).style.color="Red"; 
              else
                document.getElementById("PageNo"+Index).style.color="Blue";               
         }
      }
   }
   catch(Exc){}
}
function callBack_PageNo(Response)
{    
     try
     {
        if(Response.value.toLowerCase()=="session expired.")
        {
            logOut();
            return;
        }
        document.getElementById('Div_Cases').innerHTML=Response.value;
     }
     catch(Exc){}
}

function expandCaseDtl(RowNo,ProcessID,InstanceID)
{
   try
   {
      ROWNO=RowNo;
      AuditTrail.ShowCaseAuditDetails(ProcessID,InstanceID,callBack_ShowCaseAuditDetails);
      document.getElementById("img_minus"+RowNo).style.display="block";
      document.getElementById("img_plus"+RowNo).style.display="none";
      document.getElementById("div_CaseDtl"+RowNo).style.display="block";
      document.getElementById("div_CaseDtl"+ROWNO).innerHTML="<br/><h5 style='text-align:Center; color:red;'>Please wait while the audit trail is being populated...</h5>";
   }
   catch(Exc)
   {
   
   }
}
function callBack_ShowCaseAuditDetails(Response)
{

     try
     {
        if(Response.value.toLowerCase()=="session expired.")
        {
            logOut();
            return;
        }
        document.getElementById("div_CaseDtl"+ROWNO).innerHTML=Response.value;
     }
     catch(Exc){
     
     }
}
function collapseCaseDtl(RowNo)
{
   try
   {
      document.getElementById("img_plus"+RowNo).style.display="block";
      document.getElementById("img_minus"+RowNo).style.display="none";
      document.getElementById("div_CaseDtl"+RowNo).style.display="none";
      document.getElementById("div_CaseDtl"+RowNo).innerHTML="";
   }
   catch(Exc){}
}

function expandSubCaseDtl(RowNo)
{
   try
   {
      document.getElementById("img_plus_SubCase"+RowNo).style.display="none";
      document.getElementById("img_minus_SubCase"+RowNo).style.display="block";
      document.getElementById("div_SubCaseDtl"+RowNo).style.display="block";
   }
   catch(Exc){}
}
function collapseSubCaseDtl(RowNo)
{
   try
   {
      document.getElementById("img_plus_SubCase"+RowNo).style.display="block";
      document.getElementById("img_minus_SubCase"+RowNo).style.display="none";
      document.getElementById("div_SubCaseDtl"+RowNo).style.display="none";
   }
   catch(Exc){}
}
  
-->
</script>
</html>
