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
using FSL.Controller;
using FSL.Logging;
using AjaxPro;
public partial class MenuRequestHandler : System.Web.UI.Page
{
    protected void   Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(MenuRequestHandler));
            try
            {

                Session["iPAction"]      = null;
                Session["iPActionParam"] = null;
                string PageName   = Request.Params.Get("page");
                string ParamName  = Request.Params.Get("paramname");
                string ParamValue = Request.Params.Get("paramvalue");
                string ProcessID = Request.Params.Get("processid");
                string InstanceID = Request.Params.Get("instanceid");
                string step = Request.Params.Get("stepname");
                string stepid = Request.Params.Get("stepid");
                string wiid = Request.Params.Get("wiid");
                if ((!string.IsNullOrEmpty(PageName)) && (!string.IsNullOrEmpty(ParamName)) )
                {

                    switch (ParamName.ToUpper())
                    {
                        case "WI": OpenWorkItem(PageName + "?processid=" + ProcessID + "&instanceid=" + InstanceID + "&step=" + step + "&stepid=" + stepid + "&wiid=" + wiid);
                            break;
                            
                        case "PS"         : if (SetUserActions(ParamValue)) StartCase(ParamValue, PageName); break;
                        case "PT"         :
                        case "PC"         : if (SetUserActions(ParamValue))
                                            {
                                                tbl_SetUserPref.Style["Display"] = "block";
                                                tbl_WorkItemDetails.Style["Display"] = "none";
                                                Session["iPAction"] = ParamName.ToUpper();
                                                Session["iPActionParam"] = ParamValue;
                                                txt_Action.Value = ParamName.ToUpper();
                                                if (ParamName.ToUpper().Equals("PT"))
                                                {
                                                    txt_ProcName.Value = ParamValue.Split('|')[0].ToString();
                                                    txt_StepName.Value = ParamValue.Split('|')[1].ToString();
                                                }
                                                else
                                                    txt_ProcName.Value = ParamValue;
                                                string ActionStatus = string.Empty;
                                                Session["INBOX"] = Application["INBOX"] != null ? (DataTable)Application["INBOX"] : (DataTable)ActionController.ExecuteAction(Page, "select inbox", ref ActionStatus);
                                                if (Application["INBOX"] == null) Application["INBOX"] = (DataTable)Session["INBOX"];
                                            }
                                            break;
                        case "PU"          :
                                            txt_Action.Value = ParamName.ToUpper();
                                            txt_ProcName.Value = ParamValue;
                                            Response.Redirect("StartSMSCancellCases.aspx?action=" + ParamName.ToUpper() + "$" + ParamValue); break;
                        default: Response.Write("<script language='JavaScript' type='text/javascript'>alert('Invalid Request Parameters!');window.open('blank.htm','frmset_WorkArea');</script>"); break;
                    }
                }
                else
                    Response.Write("<script language='JavaScript' type='text/javascript'>alert('Invalid Request Parameters!');window.open('blank.htm','frmset_WorkArea');</script>");

                
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }
    private void     StartCase(string URL,string ObjID)
    {
        string ErrorMsg = "You can not start case of the process ";
        try
        {
            string[] ProcInfo = URL.Split('|');
            string ProcTag = string.Empty;
            string StepPath = string.Empty;
            if (ProcInfo.Length >= 2)
            {
                if (isAuthorisedProcess(ProcInfo[0], ref ProcTag, ref StepPath))
                    Server.Execute(StepPath + ProcInfo[1] + ".aspx?ProcTag=" + ProcTag + "&objID=" + ObjID);
                else
                {
                    ErrorMsg += "because requested process does not exists OR you are not an authorised person to start the case!!!";
                    Response.Write("<script language='JavaScript' type='text/javascript'>alert('" + ErrorMsg + "');window.open('blank.htm','frmset_WorkArea');</script>");
                }
            }
            else
            {
                ErrorMsg = "Step not found!!!";
                Response.Write("<script language='JavaScript' type='text/javascript'>alert('" + ErrorMsg + "');window.open('blank.htm','frmset_WorkArea');</script>");
            }
        }
        catch (Exception Exc)
        {
            Exception NewException = null;
            if (Exc.Message.Equals("One of the items in the array returned an error.  "))
            {
                ErrorMsg += "because Process not found!!!";
                NewException = Exc;
                //NewException = new Exception(((vException)Exc.GetBaseException()).ExceptionDetails[0].Message, Exc);
            }
            else
            {
                if (Exc.Message.Contains("Error executing child request for"))
                    ErrorMsg = "Step not found!!!";
                NewException = Exc;
            }
            Logger.WriteEventLog(false, NewException);
            Response.Write("<script>alert('" + ErrorMsg + "');window.open('blank.htm','frmset_WorkArea');</script>");
        }
    }


    private void     OpenWorkItem(string URL)
    {
        Response.Redirect(URL,false);
    }
    private string   TriggerEvent(string CaseTag,string EventName) 
    {
        string ErrorMsg = "You can not trigger an event ";
        //try
        //{
        //    EventName = Session["iPActionParam"]==null?string.Empty:((string)Session["iPActionParam"]).Split('|')[1].ToString();
        //    if ((!string.IsNullOrEmpty(CaseTag)) && (!string.IsNullOrEmpty(EventName)))
        //    {
        //        if (CaseTag.Contains("KPAY"))
        //            (((sSession)Session["UserIPSession"]).Create_sCaseManager()).TriggerEvent(CaseTag, EventName, new vField[] { new vField("KP_TE_PERFORMER", (string)Session["User_ADID"], SWFieldType.swText) });
        //        else
        //            (((sSession)Session["UserIPSession"]).Create_sCaseManager()).TriggerEvent(CaseTag, EventName);
        //        ErrorMsg = "Event triggered successfully!!!";
        //    }
        //    else
        //        ErrorMsg += "due to invalid arguments!!!'";
        //}
        //catch (Exception Exc)
        //{
        //    Exception NewException = null;
        //    if (Exc.Message.Equals("Step name not found.  "))
        //    {
        //        ErrorMsg = Exc.Message;
        //        NewException = Exc;
        //    }
        //    else
        //    {
        //        if (Exc.Message.Equals("One of the items in the array returned an error.  "))
        //        {
        //            ErrorMsg += "due to invalid arguments!!!";
        //            NewException = new Exception(((vException)Exc.GetBaseException()).ExceptionDetails[0].Message, Exc);
        //        }
        //        else
        //        {
        //            if (Exc.Message.Equals("User not authorized to perform request.  "))
        //                ErrorMsg += "because you are not an authorized person!!!";
        //            NewException = Exc;
        //        }
        //    }
        //    Logger.WriteEventLog(false, NewException);
        //}
        return ErrorMsg;
    }
    private string   CloseCase(string CaseTag)  
    {
        string ErrorMsg = "You can not close case of the process ";
        //try
        //{
        //    if ((!string.IsNullOrEmpty(CaseTag)) && (CaseTag.Length != 0))
        //    {
        //        (((sSession)Session["UserIPSession"]).Create_sCaseManager()).CloseCases(new string[1] { CaseTag });
        //        ErrorMsg="Case closed successfully!!!";
        //    }
        //    else
        //        ErrorMsg += "due to invalid arguments!!!'";
        //}
        //catch (Exception Exc)
        //{
        //    Exception NewException = null;
        //    if (Exc.Message.Equals("One of the items in the array returned an error.  "))
        //    {
        //        ErrorMsg += "due to invalid arguments!!!";
        //        NewException = new Exception(((vException)Exc.GetBaseException()).ExceptionDetails[0].Message, Exc);
        //    }
        //    else
        //    {
        //        if (Exc.Message.Equals("User not authorized to perform request.  "))
        //            ErrorMsg += "because you are not an authorized person!!!";
        //        NewException = Exc;
        //    }
        //    Logger.WriteEventLog(false, NewException);
        //}
        return ErrorMsg;
    }

    private bool     isAuthorisedProcess(string ProcName, ref string ProcTag, ref string StepPath)
    {
        bool isAuthorisedProc = false;
        //try
        //{
        //    vProcId[] ProcIDs = (((sSession)Session["UserIPSession"]).Create_sUser()).GetStartProcIds();
        //    IEnumerator Enumrator = ProcIDs.GetEnumerator();
        //    while (Enumrator.MoveNext())
        //    {
        //        vProcId ProcID = (vProcId)Enumrator.Current;
        //        if (ProcID.Name.ToUpper().Equals(ProcName.ToUpper()))
        //        {
        //            isAuthorisedProc = true;
        //            ProcTag = ProcID.Tag;
        //            StepPath = "../" + ProcID.HostingNode + "/" + ProcID.Name + ProcID.MajorVersion + "." + ProcID.MinorVersion + "/";
        //            break;
        //        }
        //    }
        //}
        //catch (Exception Exc) { throw Exc; }
        return isAuthorisedProc;
    }
    private string[] ShowCases(string ProcName,string Action)
    {
        string[] tblHTML = null;
        try
        {
           // sSession ipUser =(sSession)Session["UserIPSession"];
            //string ProcTag  =vProc.MakeTag(ipUser.GetNodeId().Name,ProcName);
            //Session["CaseList"] = ipUser.Create_sCaseManager().GetACaseList(new vACaseCriteria((string)Session["CaseFilterExpression"], new vSortField[] { }), ProcTag, new vACaseContent(false), (int)Session["PageSize"]);
            tblHTML = RenderCaseDetails(1, Action);
        } 
        catch (Exception Exc) {throw new Exception(Exc.Message, Exc);}
        return tblHTML;
    }
    private string[] RenderCaseDetails(int CurrentPageNo, string Action)
    {
        string[] tblHTML = null;
        //try
        //{
        //    StringBuilder tblHeader = new StringBuilder();
        //    StringBuilder tblBody = new StringBuilder();
        //    int PageSize = (int)Session["PageSize"];
        //   // sPageableList sPLCase = (sPageableList)Session["CaseList"];

        //    tblHeader.Append((string.IsNullOrEmpty(Action) ? "" : "<th>ACTION</th>") + "<th>STATUS</th><th>NUMBER</th><th>DESCRIPTION</th><th>STARTED BY</th><th>START DATE</th><th>TERMINATED DATE</th>");
        //    for (int Index = (CurrentPageNo * PageSize - PageSize); (Index < CurrentPageNo * PageSize) && (Index < sPLCase.AvailableCnt); Index++)
        //    {
        //     //   vACase Case = (vACase)sPLCase.GetItem(Index);
        //        tblBody.Append("<tr>");
        //        if (!string.IsNullOrEmpty(Action))
        //            tblBody.Append("<td align='center'><a href='#' title='Click here to " + (Action.ToUpper().Equals("PT") ? "trigger event." : "close the case.") + "' style='text-transform:capitalize;display:marker;text-decoration:blink;color:Blue; '>" + (((Action.ToUpper().Equals("PC")) && (Case.TimeTerminated.Year != 3000))?"&nbsp;":"<img src='../Images/" + (Action.ToUpper().Equals("PT") ? "Events" : "CloseCases") + ".gif' onclick=\"executeAction('" + Case.Tag + "'); \"    style='vertical-align:middle;' />" ) + "</a></td>");

        //        tblBody.Append("<td align='center'>");
        //        if (Case.TimeTerminated.Year == 3000)
        //        {
        //            if (Case.IsSuspended)
        //                tblBody.Append("<a href='#' title='Status: Suspended'><img src='../Images/Suspended.gif'  style='vertical-align:middle;'/></a>&nbsp;");
        //            else
        //                if (Case.IsActive)
        //                    tblBody.Append("<a href='#' title='Status: Active'><img src='../Images/Active.gif'  style='vertical-align:middle;'/></a>&nbsp;");
        //        }
        //        else
        //            tblBody.Append("<a href='#' title='Status: Close'><img src='../Images/closed.gif'  style='vertical-align:middle;'/></a>&nbsp;");
        //        tblBody.Append("</td>");

        //        tblBody.Append("<td align='right'>"  +  Case.CaseNumber+"</td>");
        //        tblBody.Append("<td align='left'>"   +  Case.Description + "</td>");
        //        tblBody.Append("<td align='left'>"   + (Case.StartedBy.Split('@')[0].ToString()) + "</td>");
        //        tblBody.Append("<td align='center'>" + (Case.TimeStarted.Day+"-"+Case.TimeStarted.Month+"-"+Case.TimeStarted.Year+"  " +Case.TimeStarted.ToShortTimeString()) + "</td>");
        //        tblBody.Append("<td align='center'>" + (Case.TimeTerminated.Year== 3000?"&nbsp;&nbsp;":(Case.TimeTerminated.Day + "-" + Case.TimeTerminated.Month + "-" + Case.TimeTerminated.Year + "  " + Case.TimeTerminated.ToShortTimeString())) + "</td>");
        //        tblBody.Append("</tr>");
        //    }
        //    if (tblBody.Length < 1)
        //        tblBody.Append("<tr><td colspan='7' style='text-align:Center;  font-family:verdana; font-size:11; color:red;background-color:#FFFFFF;'>No Cases Found!!! </td></tr>");

        //    tblHTML=new string[2];
        //    tblHTML[0] = "<table id='tbl_WorkItems' align='center' width='100%' cellspacing='0' cellpadding='0'>" +
        //                      "<thead><tr>" + tblHeader.ToString() + "</tr></thead>" +
        //                      "<tbody>" + tblBody.ToString() + "</tbody>" +
        //                      "</table>";
        //    tblHTML[1] = ShowPagging(sPLCase.AvailableCnt);
        //}
        //catch (Exception Exc) { throw new Exception(Exc.Message, Exc.InnerException); }
        return tblHTML;
    }
    private string   ShowPagging(int AvailableCnt)
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
                PaggingHTML.Append("<td><a href='#' onclick=\"showPage(1," + PageNo + "," + MaxPages + ");\" title='View First Page'><img src='../Images/GAT_TB_PageFirst.gif'  style='vertical-align:middle;'/></a>&nbsp;");
                PaggingHTML.Append("   <!-- <a href='#' onclick=\"showNextPage();\" title='View Next Page'><img src='../Images/GAT_TB_PageNext.gif'  style='vertical-align:middle;height:15px;'/></a>--></td>");
                for (; PageNo <= MaxPages; PageNo++)
                    PaggingHTML.Append("<td>&nbsp;&nbsp;<a href='#' id='PageNo" + PageNo + "'  onclick=\"showPage(1," + PageNo + "," + MaxPages + ");\" style='color:" + (PageNo == 1 ? "Red" : "Blue") + ";text-decoration:underline;'>" + PageNo + "</a></td>");
                PaggingHTML.Append("<td>&nbsp;&nbsp;<!--<a href='#' onclick=\"showPreviousPage();\" title='View Previous Page'><img src='../Images/GAT_TB_PagePrev.gif'  style='vertical-align:middle;height:15px;'/></a>&nbsp;-->");
                PaggingHTML.Append("    <a href='#' onclick=\"showPage(1," + MaxPages + "," + MaxPages + ");\" title='View Last Page'><img src='../Images/GAT_TB_PageLast.gif'  style='vertical-align:middle;'/></a></td>");
                PaggingHTML.Append("</tr></table>");
            }
        }
        catch (Exception) { }
        return PaggingHTML.ToString();
    }
    private bool     SetUserActions(string ObjectURL)
    {
        bool HasAccess = false;
        try
        {
            string UserAccess = ActionController.ObjectAccessesByURL((string)Session["User_ADID"], ObjectURL);
            
            if (string.IsNullOrEmpty(UserAccess))
                Response.Write("<script>window.open('/MQueue/ErrorPages/Error.htm','frmset_WorkArea');</script>");
            else
                if (UserAccess.IndexOf("1") == -1) { div_AccessMessage.Visible = true; div_MenuHandler.Visible = false; }
                else { div_AccessMessage.Visible = false; div_MenuHandler.Visible = true; HasAccess = true; }
        }
        catch (Exception) { }
        return HasAccess;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string    ShowNewPage(int PageNo, string Action)
    {
        string DisplayData = string.Empty;
        try
        {
            if (ActionController.IsSessionExpired(this, true))
                DisplayData = "Session Expired.";
            else
            {
                Action = Session["iPAction"] == null ? string.Empty : (string)Session["iPAction"];
                DisplayData = RenderCaseDetails(PageNo, Action)[0].ToString();
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return DisplayData;
    }    
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string[]  getFilterData()
    {
        string[] FilterData = null;
        try
        {
            FilterData = new string[3];
            if (ActionController.IsSessionExpired(this, true))
                FilterData[0] = "Session Expired.";
            else
            {
                StringBuilder AvailableCols = new StringBuilder();
                StringBuilder Operator = new StringBuilder();
                DataTable DT_Inbox = (DataTable)Session["INBOX"];
                foreach (DataRow TblRow in DT_Inbox.Rows)
                {
                    if ((int.Parse(TblRow["COLUMN_TYPE"].ToString()) > 0) && (int.Parse(TblRow["COLUMN_TYPE"].ToString()) < 3) && (TblRow["ISCASESEARCH"].ToString().ToUpper().Equals("Y")))
                        AvailableCols.Append(TblRow["COLUMN_NAME"].ToString() + "^" + TblRow["DB_FIELD"].ToString() + "^" + TblRow["VALIDATION_TYPE"].ToString() + "~");
                    if (!Operator.ToString().Contains(TblRow["OPERATOR_TYPE"].ToString()))
                        Operator.Append(TblRow["OPERATOR_TYPE"].ToString() + "~");
                }
                if (AvailableCols.Length > 1) AvailableCols.Remove(AvailableCols.Length - 1, 1);
                if (Operator.Length > 1) Operator.Remove(Operator.Length - 1, 1);
                FilterData[0] = AvailableCols.ToString();
                FilterData[1] = Operator.ToString();
                FilterData[2] = (String)Session["CaseFilterExpression"];
                if (FilterData[2] != null)
                {
                    FilterData[2] = FilterData[2].Replace("\"", "");
                    FilterData[2] = FilterData[2].Replace("!", "");
                    FilterData[2] = FilterData[2].Replace("#", "");
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return FilterData;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string[]  setFilterData(string[] FilterData,string ProcName,string Action)
    {
        string[] ActionResult = new string[2];
        try
        {
            if (ActionController.IsSessionExpired(this, true))
                ActionResult[0] = "Session Expired.";
            else
            {
                Action   = Session["iPAction"] == null  ? string.Empty  : (string)Session["iPAction"];
                ProcName = string.IsNullOrEmpty(Action) ? string.Empty  : (Action.ToUpper().Equals("PC") ? (string)Session["iPActionParam"] : ((string)Session["iPActionParam"]).Split('|')[0].ToString());
                string[,] Filters = new string[FilterData.Length, 4];
                for (int FilterIndex = 0; FilterIndex < FilterData.Length; FilterIndex++)
                {
                    string[] MetaData = FilterData[FilterIndex].Split('~');
                    Filters[FilterIndex, 0] = MetaData[0];
                    Filters[FilterIndex, 1] = MetaData[1];
                    Filters[FilterIndex, 2] = MetaData[2];
                    Filters[FilterIndex, 3] = MetaData[3];
                }
                string UserName = (string)Session["User_ADID"];
                StringBuilder FilterExp = new StringBuilder();
                for (int FilterIndex = 0; FilterIndex < FilterData.Length; )
                {
                    string RigthHandOperand = string.IsNullOrEmpty(Filters[FilterIndex, 2]) ? "\"" + Filters[FilterIndex, 3] + "\"" : (Filters[FilterIndex, 2].ToUpper().Equals("D") ? "!" + Filters[FilterIndex, 3] + "!" : Filters[FilterIndex, 2].ToUpper().Equals("B") ? "\"" + Filters[FilterIndex, 3] + "\"" : Filters[FilterIndex, 2].ToUpper().Equals("R") ? Filters[FilterIndex, 3] : Filters[FilterIndex, 2].ToUpper().Equals("A") ? "\"" + Filters[FilterIndex, 3] + "\"" : "#" + Filters[FilterIndex, 3] + "#");
                    FilterExp.Append(Filters[FilterIndex, 0] + " " + Filters[FilterIndex, 1] + " " + RigthHandOperand);
                    if (++FilterIndex < FilterData.Length)
                        FilterExp.Append(" AND ");
                }
                Session["CaseFilterExpression"] = FilterExp.ToString();
                ActionResult=ShowCases(ProcName,Action);
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return ActionResult;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string    executeAction(string Action,string CaseTag,string EventStepName)
    {
        string ActionResult = string.Empty;
        try
        {
            if (ActionController.IsSessionExpired(this, true))
                ActionResult = "Session Expired.";
            else
            {
                Action = (string)Session["iPAction"] == null ? string.Empty : (string)Session["iPAction"];
                ActionResult = Action.ToUpper().Equals("PT") ? TriggerEvent(CaseTag, EventStepName) : Action.ToUpper().Equals("PC") ? CloseCase(CaseTag) : "Transaction failed due to invalid arguments!!!";
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); ActionResult = "Transaction failed due to invalid arguments!!!"; }
        return ActionResult;
    }    
}
