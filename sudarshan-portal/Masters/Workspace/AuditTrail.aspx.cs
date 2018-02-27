using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using FSL;
using FSL.Controller;
using AjaxPro;


public partial class AuditTrail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AuditTrail));
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            if (!IsPostBack)
            {
                try
                {
                    get_ProcessList();
                }
                catch (Exception EX)
                {
                    FSL.Logging.Logger.WriteEventLog(false, EX);
                }
            }
        }
    }


    private void get_ProcessList()
    {
        try
        {
            string isValid = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction(this, "getprocesslist", ref isValid);
            ddl_ProcessName.DataSource = dt;
            ddl_ProcessName.DataBind();
            ListItem startSelection = new ListItem("---SELECT ONE---", "---SELECT ONE---");
            ddl_ProcessName.Items.Insert(0, startSelection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string[] RenderCaseDetails(int CurrentPageNo,DataTable dtAuditHeader)
    {
        string[] tblHTML = null;
        try
        {
            Session["PageSize"] = 10;
            StringBuilder tblHeader = new StringBuilder();
            StringBuilder tblBody = new StringBuilder();
            int PageSize = (int)Session["PageSize"];
           
            tblHeader.Append("<th>View Detail</th><th>STATUS</th><th>INSTANCE ID</th><th>DESCRIPTION</th><th>EMPID</th><th>START DATE</th><th>CLOSE DATE</th>");
            for (int Index = (CurrentPageNo * PageSize - PageSize); (Index < CurrentPageNo * PageSize) && (Index < dtAuditHeader.Rows.Count); Index++)
            {
                tblBody.Append("<tr>");
                tblBody.Append("<td align='center'><input type='image' style='height:11px;width:11px;' alt='Click Here To View Case Audit Trail'   id='img_plus" + (Index + 1) + "'  name='img_plus" + (Index + 1) + "' src='/Sudarshan-Portal-NEW/images/plus.gif'  onClick=\"expandCaseDtl(" + (Index + 1) + "," + dtAuditHeader.Rows[Index]["FK_PROCESSID"].ToString()+"," + dtAuditHeader.Rows[Index]["INSTANCE_ID"].ToString() + ");   return false;\"><input type='image' style='height:11px;width:11px;display: none;'  alt='Click Here To Hide Case Audit Trail' id='img_minus" + (Index + 1) + "'  name='img_minus" + (Index + 1) + "' src='/Sudarshan-Portal-NEW/images/minus.gif' onClick=\"collapseCaseDtl(" + (Index + 1) + ",'tbl_ExpenseHeadDtl'); return false;\"></td>");
                tblBody.Append("<td align='center'>");
                if (dtAuditHeader.Rows[Index]["STATUS"].ToString() == "CLOSED")
                    tblBody.Append("<a href='#' title='Status: Close'><img src='/Sudarshan-Portal-NEW/Images/closed.gif'  style='vertical-align:middle;'/></a>&nbsp;"); 
                else
                tblBody.Append("<a href='#' title='Status: Active'><img src='/Sudarshan-Portal-NEW/Images/Active.gif'  style='vertical-align:middle;'/></a>&nbsp;");
                    
                
                tblBody.Append("</td>");
                tblBody.Append("<td align='right'>" + dtAuditHeader.Rows[Index]["INSTANCE_ID"].ToString() + "</td>");
                tblBody.Append("<td align='left'>" + dtAuditHeader.Rows[Index]["INSTANCE_ID_DESCRIPTION"].ToString() + "</td>");
                tblBody.Append("<td align='left'>" + dtAuditHeader.Rows[Index]["INITIATOR_EMPID"].ToString() + "</td>");
                tblBody.Append("<td align='center'>" + dtAuditHeader.Rows[Index]["START_DATE"].ToString() + "</td>");
                tblBody.Append("<td align='center'>" + (string.IsNullOrEmpty(dtAuditHeader.Rows[Index]["END_DATE"].ToString()) ? "&nbsp;" : dtAuditHeader.Rows[Index]["END_DATE"].ToString()) + "</td>");
                tblBody.Append("</tr>");
                tblBody.Append("<tr><td colspan='7' align='center' valign='middle'><div id='div_CaseDtl" + (Index + 1) + "'  class='innocent_flowers' style='display:none; overflow:auto;' ></div></td></tr>");
            }
            if (tblBody.Length < 1)
                tblBody.Append("<tr><td colspan='7' style='text-align:Center;  font-family:verdana; font-size:11; color:red;background-color:#FFFFFF;'>Cases Not Found!!! </td></tr>");

            tblHTML = new string[2];
            tblHTML[0] = "<table id='tbl_WorkItems' align='center' width='100%' cellspacing='0' cellpadding='0' style='border: 0px solid #ADBBCA;'>" +
                              "<thead><tr>" + tblHeader.ToString() + "</tr></thead>" +
                              "<tbody>" + tblBody.ToString() + "</tbody>" +
                              "</table>";
            tblHTML[1] = ShowPagging(dtAuditHeader.Rows.Count);
        }
        catch (Exception Exc) { throw Exc; }
        return tblHTML;
    }

    private string ShowPagging(int AvailableCnt)
    {
        StringBuilder PaggingHTML = new StringBuilder();
        try
        {
            if (AvailableCnt != 0)
            {
                int PageNo = 1;
                int PageSize = (int)Session["PageSize"];
                int MaxPages = (AvailableCnt / PageSize) + ((AvailableCnt % PageSize == 0) ? 0 : 1);
                PaggingHTML.Append("<table id='tbl_topPagging' align='left' ><tr><td>Page :  </td>");
                PaggingHTML.Append("<td><a href='#' onclick=\"showPage(1," + PageNo + "," + MaxPages + ");\" title='View First Page'><img src='/Sudarshan-Portal-NEW/Images/GAT_TB_PageFirst.gif'  style='vertical-align:middle;'/></a>&nbsp;");
                PaggingHTML.Append("   <!-- <a href='#' onclick=\"showNextPage();\" title='View Next Page'><img src='/Sudarshan-Portal-NEW/Images/GAT_TB_PageNext.gif'  style='vertical-align:middle;height:15px;'/></a>--></td>");
                for (; PageNo <= MaxPages; PageNo++)
                    PaggingHTML.Append("<td>&nbsp;&nbsp;<a href='#' id='PageNo" + PageNo + "'  onclick=\"showPage(1," + PageNo + "," + MaxPages + ");\" style='color:" + (PageNo == 1 ? "Red" : "Blue") + ";text-decoration:underline;'>" + PageNo + "</a></td>");
                PaggingHTML.Append("<td>&nbsp;&nbsp;<!--<a href='#' onclick=\"showPreviousPage();\" title='View Previous Page'><img src='/Sudarshan-Portal-NEW/Images/GAT_TB_PagePrev.gif'  style='vertical-align:middle;height:15px;'/></a>&nbsp;-->");
                PaggingHTML.Append("    <a href='#' onclick=\"showPage(1," + MaxPages + "," + MaxPages + ");\" title='View Last Page'><img src='/Sudarshan-Portal-NEW/Images/GAT_TB_PageLast.gif'  style='vertical-align:middle;'/></a></td>");
                PaggingHTML.Append("</tr></table>");
            }
        }
        catch (Exception) { }
        return PaggingHTML.ToString();
    }

    protected void btn_ShowCases_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                if (ddl_ProcessName.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('Select Process Name')}</script>");
                    return;
                }
                Div_Cases.InnerHtml = string.Empty;
                Div_Pagging.InnerHtml = string.Empty;
                string ActionStatus = string.Empty;
                string isValid = string.Empty;
                string FilterExpression = string.Empty;
                DataTable dt_Result = (DataTable)ActionController.ExecuteAction(this, "getauditheader", ref isValid);
               if (dt_Result != null && dt_Result.Rows.Count > 0)
               {
                   Session["CaseList"] = dt_Result;
                   if (string.IsNullOrEmpty(ActionStatus))
                   {
                       string[] tblHTML = RenderCaseDetails(1,dt_Result);
                       Div_Cases.InnerHtml = tblHTML[0];
                       Div_Pagging.InnerHtml = tblHTML[1];
                   }
                   else
                       ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('" + ActionStatus + "')}</script>");
               }
               else
                   ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('Data not found')}</script>");
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{ResizeFrameWindows(true);}</script>");
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string ShowNewPage(int PageNo)
    {
        string DisplayData = string.Empty;
        try
        {
            if (ActionController.IsSessionExpired(this, true))
                DisplayData = "Session Expired.";
            else
                DisplayData = RenderCaseDetails(PageNo,(DataTable) Session["CaseList"])[0];
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return DisplayData;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string ShowCaseAuditDetails(int ProcessID,int InstanceID)
    {
        string DisplayData = string.Empty;
        try
        {
            if (ActionController.IsSessionExpired(this, true))
                DisplayData = "Session Expired.";
            else
                DisplayData = RenderAuditData(ProcessID.ToString(), InstanceID.ToString());
        }
        catch (Exception Exc) {FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return DisplayData;
    }

    private string RenderAuditData(string ProcessID,string InstanceID)
    {
        StringBuilder HTML = new StringBuilder();
        try
        {
            string ActionStatus = string.Empty;
            string Color = string.Empty;

            string isValid = string.Empty;
            
            DataTable dt = (DataTable)ActionController.ExecuteAction((string)Session["User_ADID"],"AuditTrail.aspx", "getauditdetails", ref isValid,ProcessID,InstanceID);


            if (dt != null && dt.Rows.Count>0)
            {
                HTML.Append("<br/><table id='tbl_CaseAuditInfo' align='center' width='98%' cellspacing='0' cellpadding='0' style='border: 1px solid #ADBBCA;'>");
                HTML.Append("<thead><tr><th>STEP NAME</th><th>PERFORMER NAME</th><th>ASSIGN DATE</th><th>TARGET DATE</th> <th>ACTUAL CLOSE DATE</th><th>STATUS</th><th>ESCALATION</th> <th>COMPLETED BY</th> <th>ACTION</th></tr></thead>");
                HTML.Append("<tbody>");
                for (int AuditStepIndex = 0; AuditStepIndex < dt.Rows.Count; AuditStepIndex++)
                {
                    string SubCaseID = string.Empty;
                    StringBuilder InnertHTML = new StringBuilder();
                  
                    Color = "blue";
                    HTML.Append("<tr>");
                    HTML.Append("<td align='center' style='color:" + Color + ";'  >" + dt.Rows[AuditStepIndex]["STEP_NAME"].ToString() + "</td>");
                    HTML.Append("<td align='Left'   style='color:" + Color + ";'  >" + dt.Rows[AuditStepIndex]["PERFORMER_NAME"].ToString() + "</td>");
                    HTML.Append("<td align='center' style='color:" + Color + ";'  >" + dt.Rows[AuditStepIndex]["ASSIGN_DATE"].ToString() + "</td>");
                    HTML.Append("<td align='center' style='color:" + Color + ";'  >" + dt.Rows[AuditStepIndex]["TARGET_DATE"].ToString() + "</td>");
                    HTML.Append("<td align='Left'   style='color:" + Color + ";'  >" + (string.IsNullOrEmpty(dt.Rows[AuditStepIndex]["END_DATE"].ToString()) ? "&nbsp;" : dt.Rows[AuditStepIndex]["END_DATE"].ToString()) + "</td>");
                    HTML.Append("<td align='Left'   style='color:" + Color + ";'  >" + dt.Rows[AuditStepIndex]["STATUS"].ToString() + "</td>");
                    HTML.Append("<td align='Left'   style='color:" + Color + ";'  >" + dt.Rows[AuditStepIndex]["IS_ESCALATED"].ToString() + "</td>");
                    HTML.Append("<td align='Left'   style='color:" + Color + ";'  >" + (string.IsNullOrEmpty(dt.Rows[AuditStepIndex]["COMPLETED_BY_NAME"].ToString())?"&nbsp;" :dt.Rows[AuditStepIndex]["COMPLETED_BY_NAME"].ToString()) + "</td>");
                    HTML.Append("<td align='Left'   style='color:" + Color + ";'  >" + (string.IsNullOrEmpty(dt.Rows[AuditStepIndex]["ACTION"].ToString())?"&nbsp;" :dt.Rows[AuditStepIndex]["ACTION"].ToString()) + "</td>");
                    HTML.Append("</tr>");                   
                  }
                    
                
                HTML.Append("</tbody></table><br/>");
            }
            else
                HTML.Append("<br/><h5 style='text-align:Center; color:red;'>" + ActionStatus + "</h5>");
        }
        catch (Exception Exc) { HTML = new StringBuilder(); throw Exc; }
        return HTML.ToString();
    }
}
