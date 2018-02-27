<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuRequestHandler.aspx.cs" Inherits="MenuRequestHandler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
    <title>Case Management</title> 
     <link href="../css/helpdesk_style.css"   rel="stylesheet" type="text/css" />
     <link href="../css/innocent_flowers.css" rel="stylesheet" type="text/css" />
     <link href="../css/sbm_app01.css"        rel="stylesheet" type="text/css" />
     <script language="JavaScript" type="text/javascript" src="../JS/MenuRequestHandler.js"></script>
     <script language="JavaScript" type="text/javascript" src="../JS/utility.js"></script>
     <script language="JavaScript" type="text/javascript" src="../JS/validator.js"></script>
     <script language="javascript" type="text/javascript"> 
        document.attachEvent("onkeydown", my_onkeydown_handler); 
        function my_onkeydown_handler() 
        {  
            var KeyID = window.event ? event.keyCode : e.keyCode; 
            if ((window.event?(event.altKey==true):(e.altKey==true)) && (KeyID==115))
            { 
                 window.open('/MQueue/Logoff.aspx','_top');
            }
        } 
    </script>
</head>  
<body  class="ApBody" onload="onLoad(); DisableBrowserClone();"  >
    <form id="frm_CaseManager" runat="server" method="post">
      <div id="div_MenuHandler" runat="server">
        <table id="tbl_ContentPlaceHolder" align="center" cellpadding="0" cellspacing="0"  width="100%"  >
          <tr>
            <td align="center">
      		    <table id="tbl_Content" width="100%" cellpadding="0" runat="server" cellspacing="0" style="font-family: Verdana, Arial, Helvetica, sans-serif;
			     font-size: 10px;	color: #000000;	text-align:Left;	padding: 0px; border-collapse: collapse; border: 0px solid #ADBBCA;">
                    <tr>
             	       <td align="center">
             	         <table id="tbl_SetUserPref" cellpadding="0" cellspacing="0" runat="server" style="width:95%;text-align: left; display:none;" >
             	           <tr>
	                         <td  >
		                         <div id="content" >
			                        <div  style="height: 4%;background-image: url(../Images/topmiddle.jpg);background-repeat: repeat-x;">
				                    <div  style="float: left;width: 8px;	background-image:  url(../Images/topleft.jpg); background-repeat: no-repeat;"></div>
				                    <div  style="float: left;padding-top: 4px;"><strong id="str_UserPref" style=" font-family:Verdana;  text-align:center; color:#000000;" >Filter Selector</strong></div>   
				                    <div  style="float: right;width: 9px;background-image:  url(../Images/topright.jpg);background-repeat: no-repeat;"></div>
 				                  </div>
		                        </div>   
	                         </td>
	                       </tr>
			               <tr>     
                            <td align="center" >
	                          <table id="tbl_UserPref" width="100%" cellpadding="0" cellspacing="0" style="font-family: Verdana, Arial, Helvetica, sans-serif;
                                    font-size : 10px;	color: #000000;	text-align:Left;	padding: 0px; border-collapse: collapse; border: 1px solid #ADBBCA;">
                                 <tr>
 	                               <td align="center">
 	                                  <div id="Div_SetUserPref" class="innocent_flowers" style=""  runat="server"></div>
 	                                  <div id="Div_UserPrefButton"></div>
                                   </td>
                                </tr>
                              </table>  
                            </td>
                          </tr>
                        </table>	
			           </td>
				    </tr>
				    <tr>
             	       <td align="center"   >
             	          <div id="Div_WI" style="">
               	              <table id="tbl_WorkItemDetails" cellpadding="0" cellspacing="0" runat="server" style="width: 95%; text-align: left;display:none;" >
	                            <tr>
	                             <td  >
		                             <div id="Div1" >
			                            <div  style="height: 4%;background-image: url(../Images/topmiddle.jpg);background-repeat: repeat-x;">
				                        <div  style="float: left;width: 8px;	background-image:  url(../Images/topleft.jpg); background-repeat: no-repeat;"></div>
				                        <div  style="float: left;padding-top: 4px;"><strong style="font-family:Verdana; font-size:x-small;  text-align:center; color:#000000;" >Case Details</strong></div>   
				                        <div  style="float: right;width: 9px;background-image:  url(../Images/topright.jpg);background-repeat: no-repeat;"></div>
 				                      </div>
		                            </div>   
	                             </td> 
	                            </tr>
			                    <tr>     
                                   <td align="center" >
                                     <table id="tbl_WorkItemsList" align="center" width="100%"  cellpadding="0" cellspacing="0" style="border: 1px solid #ADBBCA;">
                                         <tr>
 	                                       <td  align="center">
 	                                          <div id="Div_ShowWorkItems" class="innocent_flowers" style="overflow:scroll;"  runat="server"></div>
                                           </td>
                                        </tr>
                                        <tr>
 	                                       <td  align="center">
 	                                          <div id="Div_Pagging" class="" style=""  runat="server"></div>
                                           </td>
                                        </tr>
                                        <tr>
 	                                       <td  align="center">
 	                                           <input class="ApScrnButton" name="btn_ShowFilter" id="btn_ShowFilter" type="button" value="Search" onclick="onLoad();"    title="Click here to search again."  onmouseout="this.className='ApScrnButton';" onmouseover="this.className='ApScrnButton';" />
                                           </td>
                                        </tr>
                                        <tr>
 	                                       <td  align="center"><br /></td>
                                        </tr>
                                      </table>
                                   </td>
                                </tr>   
				              </table>
				          </div>
					   </td>
				     </tr>
				    <tr> <td > <br /> </td></tr>  
                </table>	
            </td>
          </tr>
       </table>  
        <table id="tbl_Hideen" visible="false" >
         <tr>
            <td><input type="hidden" id="txt_ProcName"      name="txt_ProcName"  value=""  runat="server" /> </td>
             <td><input type="hidden" id="txt_StepName"     name="txt_StepName"    value=""  runat="server" /> </td>
            <td><input type="hidden" id="txt_Action"        name="txt_Action"    value=""  runat="server" /> </td>
         </tr>
       </table>   
      </div>
      <div runat="server" id="div_AccessMessage" visible="false"><br /><br /><br /><h5 style='color:red; text-align:center; '>Sorry, you do not have access!!!</h5></div>  
    </form>
</body>
