using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AjaxPro;
using FSL.Logging;
using FSL.Controller;
using FSL.Message;

public partial class Dashboard : System.Web.UI.Page
{
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Dashboard));
                if (!IsPostBack)
                {
                    //sp_LoginUser.InnerHtml = Session["USER_NAME"].ToString();
                    //txt_UserName.Value = (string)Session["User_ADID"];
                    //txt_LoginUserName.Text = (string)Session["username"];
                    //txt_CurrentDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                    //txt_WQTAG.Value = Request.Params.Get("WQTag") == null ? Session["WQTAG"].ToString() : Request.Params.Get("WQTag");
                    //Session["WQTAG"] = txt_WQTAG.Value;
                    //string ErrorMsg = Request.Params.Get("Error");
                    //if (ErrorMsg != null)
                    //    ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{alert('" + ErrorMsg + "')}</script>");
                    //if (txt_WQTAG.Value == null)
                    //{
                    //    //Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('/Sudarshan-Portal-NEW/portal/Treemenu.aspx','frmset_TreeMenu');}</script>");
                    //}
                    //else
                    //{
                    //    //Page.RegisterStartupScript("RefreshFooter", "<script language='javascript'>{window.open('/Sudarshan-Portal-NEW/portal/Footer.aspx?Url=0&InstanceId=0','div_Traverse');}</script>");
                    //}
                    //ShowWorkItems(txt_WQTAG.Value);

                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }


    private void ShowWorkItems(string WQTag)
    {
        try
        {

            string ActionStatus = string.Empty;
            if (!string.IsNullOrEmpty(WQTag))
            {
                DataTable Dt_GroupQItem = new DataTable();
                if (WQTag == "Personal Queue")
                    Dt_GroupQItem = (DataTable)ActionController.ExecuteAction(this, "select user wi", ref ActionStatus);
                else
                    Dt_GroupQItem = (DataTable)ActionController.ExecuteAction(this, "select group wi", ref ActionStatus);

                Session["INBOX"] = Dt_GroupQItem;
                Session["INBOX1"] = Dt_GroupQItem;
                RenderWIDetails(1, Dt_GroupQItem);
            }
            else
                throw new Exception("Work Queue Tag Not Found.");
        }
        catch (Exception Exc) { throw new Exception(Exc.Message, Exc.InnerException); }
    }
    protected void Refresh(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    private string RenderWIDetails(int CurrentPageNo, DataTable DT)
    {
        string tblHTML = string.Empty;
        string imgURL = string.Empty;
        string queryImgUrl = string.Empty;
        string queryAlt = string.Empty;
        string queryOnClick = string.Empty;
        string escalation = string.Empty;
        int processid, instanceid;
        StringBuilder tblHeader = new StringBuilder();
        StringBuilder tblBody = new StringBuilder();
        try
        {
           // Following block of code is added by Amol for providing facility for Reassign Task
            //if (txt_WQTAG.Value == "Personal Queue")
            //{
            //    img_TaskReassign.Style["display"] = "none";
            //    tblHeader.Append("<th></th><th></th><th>INITIATOR</th><th >HEADER INFO</th> <th > PROCESS NAME</th><th >STEP NAME</th><th  >ASSIGN DATE</th> <th >TARGET DATE</th>");
            //}
            //else
            //{
            //    img_TaskReassign.Style["display"] = "block";
            //    tblHeader.Append("<th></th><th></th><th>INITIATOR</th><th >HEADER INFO</th> <th > PROCESS NAME</th><th >STEP NAME</th><th  >ASSIGN DATE</th> <th >TARGET DATE</th>");//<th><input id='chk_HeaderChk' type='checkbox' name='chk_HeaderChk' onclick='SelectAllItems(this.id);' /></th>
            //}


            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (DT.Rows[i]["IS_ESCALATED"].ToString() == "1")
                {
                    imgURL = "../Img/ItemDeadlineExp.gif";
                    escalation = "Escalated";
                }
                else
                {
                    imgURL = "../Img/ItemDeadline.gif";
                    escalation = "In Time";
                }
                //if (DT.Rows[i]["QUERY_STATUS"].ToString() == "PENDING" && DT.Rows[i]["RAISED_BY"].ToString() == txt_UserName.Value)
                //{
                //    queryImgUrl = "../Img/question3.png";
                //    queryAlt = "Query is Pending for resolution";
                //    queryOnClick = "javascript:AccessAlert();";
                //}
                //else if (DT.Rows[i]["QUERY_STATUS"].ToString() == "COMPLETED" && DT.Rows[i]["RAISED_BY"].ToString() == txt_UserName.Value)
                //{
                //    queryImgUrl = "../Img/question2.png";
                //    queryAlt = "Query is being resolved,You can view query answer or log new query";
                //    queryOnClick = "loadQueryDtl(" + DT.Rows[i]["PK_PROCESSID"].ToString() + "," + DT.Rows[i]["INSTANCE_ID"].ToString() + "," + DT.Rows[i]["PK_TRANSID"].ToString() + ");";
                //}
                //else
                //{
                //    queryImgUrl = "../Img/question1.png";
                //    queryAlt = "Click here to log new query";
                //    queryOnClick = "loadQueryDtl(" + DT.Rows[i]["PK_PROCESSID"].ToString() + "," + DT.Rows[i]["INSTANCE_ID"].ToString() + "," + DT.Rows[i]["PK_TRANSID"].ToString() + ");";
                //}
                if (DT.Rows[i]["IS_QUERY_EXISTS"].ToString() == "QUERY EXISTS")
                {
                    queryImgUrl = "../Img/question2.png";
                    queryAlt = "Click here to view already logged query details or log new query";
                }
                else
                {
                    queryImgUrl = "../Img/question1.png";
                    queryAlt = "Click here to log new query";
                }
                queryOnClick = "loadQueryDtl(" + i.ToString() + "," + DT.Rows[i]["PK_PROCESSID"].ToString() + "," + DT.Rows[i]["INSTANCE_ID"].ToString() + "," + DT.Rows[i]["PK_TRANSID"].ToString() + ");";

                
                processid = Int32.Parse(DT.Rows[i]["PK_PROCESSID"].ToString());
                instanceid = Int32.Parse(DT.Rows[i]["INSTANCE_ID"].ToString());
                string header = DT.Rows[i]["HEADER_INFO"].ToString();
                //Following block of code is added by Amol for providing facility for Reassign Task
                //if (txt_WQTAG.Value == "Personal Queue")
                //{
                //    if (DT.Rows[i]["ISASSIGNEND"].ToString() == "1")
                //        tblBody.Append("<tr ><td><img src='" + queryImgUrl + "' title='" + queryAlt + "' style='vertical-align:middle; cursor:pointer;max-width:145%' onclick='" + queryOnClick + "' /></td><td><a onclick='openWorkItem(" + DT.Rows[i]["INSTANCE_ID"].ToString() + ");' href='MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='div_WorkArea' title='Click here to open the workitem.' runat='server' style='text-transform:capitalize;display:marker;text-decoration:blink;color:Blue; '></td><td><img src='../Img/OpenWorkItem.gif' style='vertical-align:middle;max-width:145%' /></a></td><td align='left' >" + DT.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["HEADER_INFO"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='center'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td> <td align='center'  >" + DT.Rows[i]["TARGET_DATE"].ToString() + "</td></tr>");
                //    else
                //        tblBody.Append("<tr><td><img src='" + queryImgUrl + "' title='" + queryAlt + "' style='vertical-align:middle; cursor:pointer;max-width:145%' onclick='" + queryOnClick + "' /></td><td><a  href='../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "?processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='div_WorkArea' title='Click here to open the workitem.' runat='server' style='text-transform:capitalize;display:marker;text-decoration:blink;color:Blue; '><img src='../Img/OpenWorkItem.gif' style='vertical-align:middle;max-width:145%' /></a></td><td align='left' >" + DT.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["HEADER_INFO"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='center'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td> <td align='center'  >" + DT.Rows[i]["TARGET_DATE"].ToString() + "</td></tr>");
                //}
                //else
                //{
                //    tblBody.Append("<tr ><td align='center'  ><img src='" + queryImgUrl + "' title='" + queryAlt + "' style='vertical-align:middle; cursor:pointer;max-width:145%' onclick='" + queryOnClick + "' /></td><td align='center' ><a href='MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='div_WorkArea' title='Click here to open the workitem.' runat='server' style='text-transform:capitalize;display:marker;text-decoration:blink;color:Blue; '><img src='../Img/OpenWorkItem.gif' style='vertical-align:middle;max-width:145%' /></a></td><td align='left' >" + DT.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["HEADER_INFO"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='center'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td> <td align='center'  >" + DT.Rows[i]["TARGET_DATE"].ToString() + "</td></tr>");//<td><input type='checkbox' id='chk_ReAssign" + i + "' runat='server' onclick='CheckHeadeCheck();' /><input type='hidden' id='txt_PkTransID" + i + "' value='" + DT.Rows[i]["PK_TRANSID"].ToString() + "' /></td>
                //}
            }
            tblHTML = "<table id='tbl_WorkItems' class='table table-striped table-bordered bootstrap-datatable datatable' >" +
                              "<thead><tr class='tbl_header'>" + tblHeader.ToString() + "</tr></thead>" +
                            
                              
                              "<tbody>"+ tblBody.ToString() + "</tbody>" +
                              "</table>";
            //Div_ShowWorkItems.InnerHtml = tblHTML;

        }
        catch (Exception Exc) { throw new Exception(Exc.Message, Exc.InnerException); }
        return tblHTML;
    }

    private void ShowPagging(int AvailableCnt)
    {
        StringBuilder PaggingHTML = new StringBuilder();
        if (AvailableCnt != 0)
        {
            int PageNo = 1;
            int PageSize = (int)Session["PageSize"];
            int MaxPages = (AvailableCnt / PageSize) + ((AvailableCnt % PageSize == 0) ? 0 : 1);
            PaggingHTML.Append("<table id='tbl_topPagging' align='left' ><tr><td>Page :  </td>");
            PaggingHTML.Append("<td><a href='#' onclick=\"showPage(1," + PageNo + "," + MaxPages + ");\" title='View First Page'><img src='../Img/GAT_TB_PageFirst.gif'  style='vertical-align:middle;'/></a>&nbsp;");
            PaggingHTML.Append("   <!-- <a href='#' onclick=\"showNextPage();\" title='View Next Page'><img src='../Img/GAT_TB_PageNext.gif'  style='vertical-align:middle;height:15px;'/></a>--></td>");
            for (; PageNo <= MaxPages; PageNo++)
                PaggingHTML.Append("<td>&nbsp;&nbsp;<a href='#' id='PageNo" + PageNo + "'  onclick=\"showPage(1," + PageNo + "," + MaxPages + ");\" style='color:" + (PageNo == 1 ? "Red" : "Blue") + ";text-decoration:underline;'>" + PageNo + "</a></td>");
            PaggingHTML.Append("<td>&nbsp;&nbsp;<!--<a href='#' onclick=\"showPreviousPage();\" title='View Previous Page'><img src='../Img/GAT_TB_PagePrev.gif'  style='vertical-align:middle;height:15px;'/></a>&nbsp;-->");
            PaggingHTML.Append("    <a href='#' onclick=\"showPage(1," + MaxPages + "," + MaxPages + ");\" title='View Last Page'><img src='../Img/GAT_TB_PageLast.gif'  style='vertical-align:middle;'/></a></td>");
            PaggingHTML.Append("</tr></table>");
        }
        //Div_Pagging.InnerHtml = PaggingHTML.ToString();
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string ShowNewPage(int PageNo)
    {
        string DisplayData = string.Empty;
        try
        {
            if (ActionController.IsSessionExpired(this, true))
                DisplayData = "Session Expired.";
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return DisplayData;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string[] getDisplayOrderData()
    {
        string[] DisplayData = null;
        try
        {
            DisplayData = new string[3];
            if (ActionController.IsSessionExpired(this, true))
                DisplayData[0] = "Session Expired.";
            else
            {
                StringBuilder AvailableCols = new StringBuilder();
                StringBuilder SelectedCols = new StringBuilder();
                StringBuilder UserPrefCols = new StringBuilder();

                DataSet DS_UserPref = (DataSet)Session["UserPref"];
                foreach (DataRow dtRow in DS_UserPref.Tables[0].Rows)
                    UserPrefCols.Append(dtRow["COLUMN_NAME"].ToString() + "~");
                DataTable DT_Inbox = (DataTable)Session["INBOX"];
                foreach (DataRow TblRow in DT_Inbox.Rows)
                {
                    if ((TblRow["DISPLAY_YN"].ToString().ToUpper().Equals("Y")) /*&& (TblRow["ALWAYS_DISPLAY_YN"].ToString().ToUpper().Equals("N"))*/)
                    {
                        string ColName = TblRow["COLUMN_NAME"].ToString();
                        if (!UserPrefCols.ToString().Contains(ColName + "~"))
                            AvailableCols.Append(ColName + "~");
                    }
                    if (TblRow["ALWAYS_DISPLAY_YN"].ToString().ToUpper().Equals("Y"))
                        SelectedCols.Append(TblRow["COLUMN_NAME"].ToString() + "~");
                }
                if (AvailableCols.Length > 1) AvailableCols.Remove(AvailableCols.Length - 1, 1);
                if (SelectedCols.Length > 1) SelectedCols.Remove(SelectedCols.Length - 1, 1);
                if (UserPrefCols.Length > 1) UserPrefCols.Remove(UserPrefCols.Length - 1, 1);

                DisplayData[0] = AvailableCols.ToString(); ;
                DisplayData[1] = SelectedCols.ToString();
                DisplayData[2] = UserPrefCols.ToString();
            }

        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return DisplayData;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string[] getSortData()
    {
        string[] SortData = null;
        try
        {
            //SortData = new string[2];
            //if (ActionController.IsSessionExpired(this, true))
            //    SortData[0] = "Session Expired.";
            //else
            //{
            //    StringBuilder AvailableCols = new StringBuilder();
            //    StringBuilder UserPrefCols = new StringBuilder();

            //    DataSet DS_UserPref = (DataSet)Session["UserPref"];
            //    foreach (DataRow dtRow in DS_UserPref.Tables[1].Rows)
            //        UserPrefCols.Append(dtRow["COLUMN_NAME"].ToString() + dtRow["SORT_TYPE"].ToString() + "~");

            //    DataTable DT_Inbox = (DataTable)Session["INBOX"];
            //    foreach (DataRow TblRow in DT_Inbox.Rows)
            //    {
            //        if (TblRow["SORT_YN"].ToString().ToUpper().Equals("Y"))
            //        {
            //            string ColName = TblRow["COLUMN_NAME"].ToString();
            //            if (!UserPrefCols.ToString().Contains(ColName + "~"))
            //                AvailableCols.Append(ColName + "~");
            //        }
            //    }
            //    if (AvailableCols.Length > 1) AvailableCols.Remove(AvailableCols.Length - 1, 1);
            //    if (UserPrefCols.Length > 1) UserPrefCols.Remove(UserPrefCols.Length - 1, 1);
            //    SortData[0] = AvailableCols.ToString();
            //    SortData[1] = UserPrefCols.ToString();
            //}
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return SortData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string assignQuery(string ProcessId, string InstanceId, string WIID, string AssignedBy, string AssignedToId, string CCMailID, string Desc, string docXML)
    {
        Hashtable validationHash = new Hashtable();
        string DisplayData = string.Empty;
        string isSaved = string.Empty;
        string isInserted = string.Empty;

        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                string QueryProcessID = "56";
                string QueryInstanceID = (string)WFE.Action.StartCase(isInserted, QueryProcessID.ToString(), Session["User_ADID"].ToString(), Session["EmployeeID"].ToString(), Session["EmailID"].ToString(), "396");//396 ID at Chola UAT
                string[] Dval = new string[1];
                Dval[0] = "";

                isSaved = (string)ActionController.ExecuteAction("", "WorkItem.aspx", "insertqueryrequest", ref isInserted, QueryProcessID, QueryInstanceID, ProcessId, InstanceId, WIID, AssignedBy, AssignedToId, CCMailID, Desc, docXML);
                if (isSaved == null || isInserted.Length > 0 || isSaved == "false")
                {
                    string[] errmsg = isInserted.Split(':');
                    DisplayData = errmsg[1].ToString();
                }
                else
                {
                    //move document in RequestNo folder
                    string isValid = string.Empty;
                    string[] arr = new string[100];
                    DataTable dt = (DataTable)ActionController.ExecuteAction("", "QueryResolution.aspx", "getqueryfilename", ref isValid, "QUERY MANAGEMENT", isSaved.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                        string path = string.Empty;
                        string foldername = isSaved.ToString();
                        foldername = foldername.Replace("/", "_");
                        path = activeDir + "\\" + foldername;
                        if (Directory.Exists(path))
                        {
                        }
                        else
                        {
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            //string[] directories = Directory.GetDirectories(activeDir);
                            string[] directories = Directory.GetFiles(activeDir);
                            path = path + "\\";
                            foreach (var directory in directories)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    var sections = directory.Split('\\');
                                    var fileName = sections[sections.Length - 1];
                                    if (dt.Rows[i]["filename"].ToString() == fileName)
                                    {
                                        System.IO.File.Move(activeDir + "\\" + fileName, path + fileName);
                                    }
                                }
                            }
                        }
                    }

                    //end

                    //397 ID at Chola UAT
                    bool isCreate = (bool)WFE.Action.ReleaseStep(QueryProcessID.ToString(), QueryInstanceID, "397", "QUERY MANAGEMENT", "SUBMIT", "", Session["User_ADID"].ToString().Trim(), AssignedToId.ToString(), "", "", "", "", "", "", "", "", "", Dval, (string)isSaved, "0", ref isInserted);
                    if (isCreate)
                    {
                        DisplayData = "Query Assigned Successfully, Query No: " + isSaved.ToString();
                    }
                    else
                    {
                        DisplayData = "Error occured while saving data,Kindly try latter !!";
                    }
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
        else
        {
            DisplayData = "Session Expired";
        }

        return DisplayData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string[] fillQueryLoggedDtl(string ProcessId, string InstanceId, string wiid)
    {
        string[] ResultData = new string[2];
        StringBuilder LoggTable = new StringBuilder();
        StringBuilder QueryDocTable = new StringBuilder();

        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                string queryImgUrl = string.Empty;
                string queryAlt = string.Empty;

                string isValid = string.Empty;

                DataTable dt = (DataTable)ActionController.ExecuteAction("", "QueryResolution.aspx", "getquerydtl", ref isValid, ProcessId, InstanceId, "1");
                if (dt != null && dt.Rows.Count > 0)
                {
                    LoggTable.Append("<table class='mGrid'  cellspacing='0' width='98%' cellpadding='0' border='0'>");
                    LoggTable.Append("<tr><th align='center' style='width:4%'></th><th align='center' style='width:10%'>Query No</th><th align='center' style='width:15%'>Raised By</th><th align='center' style='width:8%'>Raised Date</th><th align='center' style='width:17%'>Query</th><th align='center' style='width:17%'>Answer</th><th align='center' style='width:15%'>Assigned To</th><th align='center' style='width:8%'>Ans Date</th><th align='center' style='width:6%'>Status</th></tr>");
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dt.Rows[i]["QUERY_STATUS"].ToString() == "PENDING")
                        {
                            queryImgUrl = "../Img/question3.png";
                            queryAlt = "Query is Pending for resolution";
                        }
                        else
                        {
                            queryImgUrl = "../Img/question2.png";
                            queryAlt = "Query is being resolved";
                        }
                        LoggTable.Append("<tr>");
                        LoggTable.Append("<td align='center'><img src='" + queryImgUrl + "' title='" + queryAlt + "' style='vertical-align:middle;' /></td>");
                        LoggTable.Append("<td align='left'>" + dt.Rows[i]["QUERY_NO"].ToString() + "</td>");
                        LoggTable.Append("<td align='left'>" + dt.Rows[i]["QUERY_RAISED_BY_NAME"].ToString() + "</td>");
                        LoggTable.Append("<td align='center'>" + dt.Rows[i]["QUERY_RAISED_DATE"].ToString() + "</td>");
                        LoggTable.Append("<td align='left'>" + dt.Rows[i]["QUERY_DESC"].ToString() + "</td>");
                        LoggTable.Append("<td align='left'>" + dt.Rows[i]["QUERY_ANS"].ToString() + "</td>");
                        LoggTable.Append("<td align='left'>" + dt.Rows[i]["QUERY_ASSIGNED_TO_NAME"].ToString() + "</td>");
                        LoggTable.Append("<td align='center'>" + dt.Rows[i]["QUERY_ANSWER_DATE"].ToString() + "</td>");
                        if (dt.Rows[i]["QUERY_STATUS"].ToString() == "PENDING")
                        {
                            LoggTable.Append("<td align='left'><font color='red'>" + dt.Rows[i]["QUERY_STATUS"].ToString() + "</font></td>");
                        }
                        else
                        {
                            LoggTable.Append("<td align='left'><font color='green'>" + dt.Rows[i]["QUERY_STATUS"].ToString() + "</font></td>");
                        }
                        LoggTable.Append("</tr>");

                    }
                    LoggTable.Append("</table>");
                }
                else
                {
                    LoggTable.Append("No Data Found!!");
                }

                //Document Details
                QueryDocTable.Append("<table class='mGrid'  cellspacing='0' width='97%' cellpadding='4' border='0' id='tbl_DocumentDtl' style='color:#333333;border-collapse:collapse;'>");
                QueryDocTable.Append("<tr><th scope='col' style='width:12%'>SR NO.</th><th scope='col' style='width:23%'>Query No</th><th scope='col' style='width:60%'>FILE NAME</th><th scope='col' style='width:5%'>REMOVE</th></tr>");
                DataTable dt1 = (DataTable)ActionController.ExecuteAction("", "QueryResolution.aspx", "getquerydocdtl", ref isValid, "QUERY MANAGEMENT", wiid);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        QueryDocTable.Append("<tr>");
                        QueryDocTable.Append("<td style='border: 1px solid #ADBBCA;' valign='middle' width='7%'><input type='text' id='dsrno" + (i + 1) + "' readonly='readonly' value='" + (i + 1) + "' style='border:none;width:30%;border-width:0px;'  /></td>");
                        QueryDocTable.Append("<td align='left'>" + dt1.Rows[i]["OBJECT_VALUE"].ToString() + "</td>");
                        QueryDocTable.Append("<td align='left'><a id='a_downloadfiles" + (i + 1) + "' style='cursor: pointer' onclick=\"return downloadfiles('" + (i + 1) + "');\" >" + dt1.Rows[i]["FILENAME"].ToString() + "</a></td>");
                        QueryDocTable.Append("<td style='border: 1px solid #ADBBCA;' align='center' width='3%' valign='middle'><img id='del" + (i + 1) + "' alt='Not Allowed to Delete This Record.'  onclick=\"javascript:alert('Not Allowed to delete this Entry!');\" src='/Sudarshan-Portal-NEW/Img/delete2.png'  style='vertical-align:middle;'/></td>");
                        QueryDocTable.Append("</tr>");
                    }
                }
                QueryDocTable.Append("</table>");
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
        else
        {
            LoggTable.Append("Session Expired");
            QueryDocTable.Append("Session Expired");
        }

        ResultData[0] = LoggTable.ToString();
        ResultData[1] = QueryDocTable.ToString();
        return ResultData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string fillQueryDocDtl(string wiid)
    {
        StringBuilder QueryDocTable = new StringBuilder();

        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                string isValid = string.Empty;
                QueryDocTable.Append("<table class='mGrid'  cellspacing='0' width='97%' cellpadding='4' border='0' id='tbl_DocumentDtl' style='color:#333333;border-collapse:collapse;'>");
                QueryDocTable.Append("<tr><th scope='col' style='width:12%'>SR NO.</th><th scope='col' style='width:23%'>Query No</th><th scope='col' style='width:60%'>FILE NAME</th><th scope='col' style='width:5%'>REMOVE</th></tr>");
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "QueryResolution.aspx", "getquerydocdtl", ref isValid, "QUERY MANAGEMENT", wiid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        QueryDocTable.Append("<tr>");
                        QueryDocTable.Append("<td style='border: 1px solid #ADBBCA;' valign='middle' width='7%'><input type='text' id='dsrno" + (i + 1) + "' readonly='readonly' value='" + (i + 1) + "' style='border:none;width:30%;border-width:0px;'  /></td>");
                        QueryDocTable.Append("<td align='left'>" + dt.Rows[i]["OBJECT_VALUE"].ToString() + "</td>");
                        QueryDocTable.Append("<td align='left'><a id='a_downloadfiles" + (i + 1) + "' style='cursor: pointer' onclick=\"return downloadfiles('" + (i + 1) + "');\" >" + dt.Rows[i]["FILENAME"].ToString() + "</a></td>");
                        QueryDocTable.Append("<td style='border: 1px solid #ADBBCA;' align='center' width='3%' valign='middle'><img id='del" + (i + 1) + "' alt='Not Allowed to Delete This Record.'  onclick=\"javascript:alert('Not Allowed to delete this Entry!');\" src='/Sudarshan-Portal-NEW/Img/delete2.png'  style='vertical-align:middle;'/></td>");
                        QueryDocTable.Append("</tr>");
                    }
                }
                QueryDocTable.Append("</table>");
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
        else
        {
            QueryDocTable.Append("Session Expired");
        }

        return QueryDocTable.ToString();
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string getDocFolderName(string processId, string instanceId, string workStepId, string HeaderInfo)
    {
        string ResultData = string.Empty;

        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                string isValid = string.Empty;
                string result = (string)ActionController.ExecuteAction("", "WorkItem.aspx", "getdocfoldername", ref isValid, processId, instanceId, HeaderInfo);
                if (result != null || result != "")
                {
                    ResultData = result;
                }
                else
                {
                    ResultData = HeaderInfo;
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
        else
        {
            ResultData = "Session Expired";
        }

        return ResultData.ToString();
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string setOrderData(string[] DisplayOrder, string QueueName, string OrderName)
    {
        string ActionResult = string.Empty;
        try
        {
            string ActionStatus = string.Empty;
            if (ActionController.IsSessionExpired(this, true))
                ActionResult = "Session Expired.";
            else
            {
                string UserName = (string)Session["User_ADID"];
                StringBuilder DisplyData = new StringBuilder();
                foreach (string Column in DisplayOrder)
                    DisplyData.Append(Column + "~");
                if (DisplyData.Length > 1) DisplyData.Remove(DisplyData.Length - 1, 1);

                ActionResult = (string)ActionController.ExecuteAction(UserName, "WorkItem.aspx", OrderName.ToLower().Equals("display") ? "set displayorder" : "set sortorder", ref ActionStatus, UserName, QueueName, DisplyData.ToString());
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return ActionResult;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string setDefault(string QueueName, string OrderName)
    {
        string ActionResult = "false";
        try
        {
            string ActionStatus = string.Empty;
            if (ActionController.IsSessionExpired(this, true))
                ActionResult = "Session Expired.";
            else
            {
                string UserName = (string)Session["User_ADID"];
                if (OrderName.ToLower().Equals("filter"))
                {
                    Session["FilterExpression"] = string.Empty;
                    ActionResult = "true";
                }
                else
                    ActionResult = (string)ActionController.ExecuteAction(UserName, "WorkItem.aspx", "set default order", ref ActionStatus, UserName, QueueName, OrderName.ToLower());

            }
        }
        catch (Exception) { }
        return ActionResult;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string[] getFilterData()
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

                DataTable dtNew = new DataTable();
                dtNew.Columns.Add(new DataColumn("PROCESS_NAME", typeof(string)));
                dtNew.Columns.Add(new DataColumn("FK_STEPID", typeof(string)));

                for (int i = 0; i < DT_Inbox.Rows.Count; i++)
                {

                    DataRow[] dr = dtNew.Select("PROCESS_NAME='" + DT_Inbox.Rows[i]["PROCESS_NAME"].ToString() + "'");
                    if (dr.Length==0)
                    {
                        DataRow dr1 = dtNew.NewRow();
                        dr1["PROCESS_NAME"] = DT_Inbox.Rows[i]["PROCESS_NAME"].ToString();
                        dr1["FK_STEPID"] = DT_Inbox.Rows[i]["FK_STEPID"].ToString();
                        dtNew.Rows.Add(dr1);
                        dtNew.AcceptChanges();
                    }
                }


                foreach (DataRow TblRow in dtNew.Rows)
                {

                    AvailableCols.Append(TblRow["PROCESS_NAME"].ToString() + "^" + TblRow["FK_STEPID"].ToString() + "~");
                }
                if (AvailableCols.Length > 1) AvailableCols.Remove(AvailableCols.Length - 1, 1);
                if (Operator.Length > 1) Operator.Remove(Operator.Length - 1, 1);
                FilterData[0] = AvailableCols.ToString();
                FilterData[1] = Operator.ToString();
                FilterData[2] = (String)Session["FilterExpression"];
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
    public string setFilterData(string FiledValue, string Value, string FromDate, string ToDate, string BranchName)
    {
        string ActionResult = string.Empty;
        try
        {
            if (ActionController.IsSessionExpired(this, true))
                ActionResult = "Session Expired.";
            else
            {
                DataTable DT_Inbox = (DataTable)Session["INBOX"];
                DataTable dtNew = new DataTable();
                dtNew = DT_Inbox.Copy();

                dtNew.Clear();
                StringBuilder Condition = new StringBuilder();
                Condition.Append("PROCESS_NAME='" + FiledValue + "' and employee_name like '%" + Value + "%'");
                if (FromDate != "" && ToDate != "")
                {
                    DateTime d1 = Convert.ToDateTime(FromDate);
                    DateTime d2 = Convert.ToDateTime(ToDate);
                    Condition.Append(" and assign_date >= '" + d1.ToString() + "' and assign_date<= '" + d2.ToString() + "'");
                }
                if (BranchName != "")
                {
                    Condition.Append(" and HEADER_INFO like  '%" + BranchName + "%' ");
                }
                DataRow[] dr = DT_Inbox.Select(Condition.ToString());
                foreach (DataRow drnew in dr)
                {
                    dtNew.ImportRow(drnew);
                    dtNew.AcceptChanges();
                }
                Session["INBOX1"] = dtNew;
                ActionResult = RenderWIDetails(dtNew);
            }
        }
        catch (Exception) { ActionResult = "false"; }
        return ActionResult;
    }

    public string RenderWIDetails(DataTable DT)
    {
        string tblHTML = string.Empty;
        string imgURL = string.Empty;
        string queryImgUrl = string.Empty;
        string queryAlt = string.Empty;
        string queryOnClick = string.Empty;
        string escalation = string.Empty;
        int processid, instanceid;
        StringBuilder tblHeader = new StringBuilder();
        StringBuilder tblBody = new StringBuilder();
        try
        {
            if ((string)Session["WQTAG"] == "Personal Queue")
            {
                tblHeader.Append("<th >&nbsp;</th><th >&nbsp;</th><th style='width:2%'></th><th >INITIATOR</th><th >HEADER INFO</th> <th > PROCESS NAME</th><th >STEP NAME</th><th  >ASSIGN DATE</th> <th >TARGET DATE</th>");
            }
            else
            {
                tblHeader.Append("<th >&nbsp;</th><th >&nbsp;</th><th style='width:2%'></th><th ><input id='chk_HeaderChk' type='checkbox' name='chk_HeaderChk' onclick='SelectAllItems(this.id);' /></th><th >INITIATOR</th><th >HEADER INFO</th> <th > PROCESS NAME</th><th >STEP NAME</th><th  >ASSIGN DATE</th> <th >TARGET DATE</th>");
            }
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (DT.Rows[i]["IS_ESCALATED"].ToString() == "1")
                {
                    imgURL = "../Img/ItemDeadlineExp.gif";
                    escalation = "Escalated";
                }                
                else
                {
                    imgURL = "../Img/ItemDeadline.gif";
                    escalation = "In Time";
                }

                //if (DT.Rows[i]["QUERY_STATUS"].ToString() == "PENDING" && DT.Rows[i]["RAISED_BY"].ToString() == txt_UserName.Value)
                //{
                //    queryImgUrl = "../Img/question3.png";
                //    queryAlt = "Query is Pending for resolution";
                //    queryOnClick = "javascript:AccessAlert();";
                //}
                //else if (DT.Rows[i]["QUERY_STATUS"].ToString() == "COMPLETED" && DT.Rows[i]["RAISED_BY"].ToString() == txt_UserName.Value)
                //{
                //    queryImgUrl = "../Img/question2.png";
                //    queryAlt = "Query is being resolved,You can view query answer or log new query";
                //    queryOnClick = "loadQueryDtl(" + DT.Rows[i]["PK_PROCESSID"].ToString() + "," + DT.Rows[i]["INSTANCE_ID"].ToString() + "," + DT.Rows[i]["PK_TRANSID"].ToString() + ");";
                //}
                //else
                //{
                //    queryImgUrl = "../Img/question1.png";
                //    queryAlt = "Click here to log new query";
                //    queryOnClick = "loadQueryDtl(" + DT.Rows[i]["PK_PROCESSID"].ToString() + "," + DT.Rows[i]["INSTANCE_ID"].ToString() + "," + DT.Rows[i]["PK_TRANSID"].ToString() + ");";
                //}

                processid = Int32.Parse(DT.Rows[i]["PK_PROCESSID"].ToString());
                instanceid = Int32.Parse(DT.Rows[i]["INSTANCE_ID"].ToString());
                string header = DT.Rows[i]["HEADER_INFO"].ToString();

                if ((string)Session["WQTAG"] == "Personal Queue")
                {
                    if (DT.Rows[i]["ISASSIGNEND"].ToString() == "1")
                        tblBody.Append("<tr style='font-weight:bold;background-color:#F0F8FF;text-transform:capitalize' ><td align='center' ><img src='" + imgURL + "' title='" + escalation + "' style='vertical-align:middle;' /></td><td align='center' ><a onclick='openWorkItem(" + DT.Rows[i]["INSTANCE_ID"].ToString() + ");' href='MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='div_WorkArea' title='Click here to open the workitem.' runat='server' style='text-transform:capitalize;display:marker;text-decoration:blink;color:Blue; '><img src='../Img/OpenWorkItem.gif' style='vertical-align:middle;' /></a></td><td align='center'  ><img src='" + queryImgUrl + "' title='" + queryAlt + "' style='vertical-align:middle; cursor:pointer;' onclick='" + queryOnClick + "' /></td><td align='left' >" + DT.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["HEADER_INFO"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='center'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td> <td align='center'  >" + DT.Rows[i]["TARGET_DATE"].ToString() + "</td> </tr>");
                    else
                        tblBody.Append("<tr ><td align='center' ><img src='" + imgURL + "' title='" + escalation + "' style='vertical-align:middle;' /></td><td align='center' ><a onclick='openWorkItem(" + DT.Rows[i]["INSTANCE_ID"].ToString() + ");' href='MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='div_WorkArea' title='Click here to open the workitem.' runat='server' style='text-transform:capitalize;display:marker;text-decoration:blink;color:Blue; '><img src='../Img/OpenWorkItem.gif' style='vertical-align:middle;' /></a></td><td align='center'  ><img src='" + queryImgUrl + "' title='" + queryAlt + "' style='vertical-align:middle; cursor:pointer;' onclick='" + queryOnClick + "' /></td><td align='left' >" + DT.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["HEADER_INFO"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='center'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td> <td align='center'  >" + DT.Rows[i]["TARGET_DATE"].ToString() + "</td> </tr>");
                }
                else
                {
                    tblBody.Append("<tr ><td align='center' ><img src='" + imgURL + "' title='" + escalation + "' style='vertical-align:middle;' /></td><td align='center' ><a onclick='openWorkItem(" + DT.Rows[i]["INSTANCE_ID"].ToString() + ");' href='MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='div_WorkArea' title='Click here to open the workitem.' runat='server' style='text-transform:capitalize;display:marker;text-decoration:blink;color:Blue; '><img src='../Img/OpenWorkItem.gif' style='vertical-align:middle;' /></a></td><td align='center'  ><img src='" + queryImgUrl + "' title='" + queryAlt + "' style='vertical-align:middle; cursor:pointer;' onclick='" + queryOnClick + "' /></td><td><input type='checkbox' id='chk_ReAssign" + i + "' runat='server' onclick='CheckHeadeCheck();' /><input type='hidden' id='txt_PkTransID" + i + "' value='" + DT.Rows[i]["PK_TRANSID"].ToString() + "' /></td><td align='left' >" + DT.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["HEADER_INFO"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='center'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td> <td align='center'  >" + DT.Rows[i]["TARGET_DATE"].ToString() + "</td> </tr>");
                }
            }
            tblHTML = "<table id='tbl_WorkItems' class='mGrid' align='center' width='98%' cellspacing='1' cellpadding='1' style='border: 1px solid #ADBBCA;'>" +
                              "<thead><tr style='color: White; background-color: #708090; font-weight: bold; vertical-align:top;position:relative;top:expression(document.getElementById('Div_ShowWorkItems').scrollTop);'>" + tblHeader.ToString() + "</tr></thead>" +
                              "<tbody>" + tblBody.ToString() + "</tbody>" +
                              "</table>";
        }
        catch (Exception Exc) { throw new Exception(Exc.Message, Exc.InnerException); }
        return tblHTML;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string setPageSize(int PageSize)
    {
        string ActionResult = "false";
        try
        {
            if (ActionController.IsSessionExpired(this, true))
                ActionResult = "Session Expired.";
            else
            {
                Session["PageSize"] = PageSize;
                ActionResult = "true";
            }
        }
        catch (Exception) { }
        return ActionResult;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string getPageSize()
    {
        string PageSize = "10";
        try
        {
            if (ActionController.IsSessionExpired(this, true))
                PageSize = "Session Expired.";
            else
                PageSize = ((int)Session["PageSize"]).ToString();
        }
        catch (Exception) { }
        return PageSize;
    }

    //-----Block of code is used to Reassign the Task
    protected void Btn_BulkApprove(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            StringBuilder XMLDataslot = new StringBuilder();
            StringBuilder XMLUser = new StringBuilder();
            StringBuilder XMLGroup = new StringBuilder();

            try
            {
                XMLDataslot.Append("<ROWSET></ROWSET>");
                XMLUser.Append("<ROWSET></ROWSET>");
                //XMLGroup.Append(txt_GroupXml.Text);

                string IsData = string.Empty;
                string ISAssigned = (string)ActionController.ExecuteAction("", "Bulk_Reassignment.aspx", "assigntask", ref IsData, XMLUser.ToString(), XMLDataslot.ToString(), XMLGroup.ToString());
                if (ISAssigned == "true")
                {
                    //new Message().SendMail(txt_Emailids.Text + ",ranjit@flologic.in", "ranjit@flologic.in", "Reasssigned Task ", "<pre><font size='2'>Dear Sir/Madam,</font></pre><p/><br/> <pre><font size='2'>The Task/s has been reassigned to you. Activity is pending for your action.</font></pre><p/><br/> <pre><font size='2'>Request you to check the same.</font></pre><br/><pre><font size='2'>Assigned By: " + Session["username"].ToString().Trim() + "</font></pre><p/><pre><font size='2'>Assigned Date: " + System.DateTime.Now.ToString("dd-MMM-yyyy") + "</font></pre><p/><br><pre><font size='2'>URL:http://" + compname + "/Sudarshan-Portal-NEW/Login.aspx?instanceid=0&processid=0&UserId=" + txtAssigneeID.Text + "</font></pre><br/><pre><font size='2'>Regards</font></pre><br/><br/><pre><font size='2'>Reporting Admin</font></pre><br/><pre></pre><br/><pre></font><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Task Assignment Completed Successfully');", true);
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('/Sudarshan-Portal-NEW/portal/WorkItem.aspx', 'div_WorkArea');}</script>");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Error While Processing. Please Try Later!');", true);
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('/Sudarshan-Portal-NEW/portal/WorkItem.aspx', 'div_WorkArea');}</script>");
                }
            }
            catch (Exception Ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, Ex);
            }
            finally
            {
            }
        }
    }

    #region DocUpload

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string DeleteFile(string filename)
    {
        string Displaydata = string.Empty;
        try
        {
            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            string path = activeDir + "\\";
            if (DeleteFiles(filename, path.Replace("/", "_")))
            {
                Displaydata = "Document Deleted Successfully";
            }
        }
        catch (Exception Ex)
        {
            Logger.WriteEventLog(false, Ex);
        }
        return Displaydata;
    }

    private bool DeleteFiles(string filename, string newPath)
    {
        bool Flag = false;
        try
        {
            if (File.Exists(newPath + filename))
            {
                File.Delete(newPath + filename);
                Flag = true;
            }
        }
        catch (Exception Ex)
        {
            Logger.WriteEventLog(false, Ex);
        }
        return Flag;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            //Int32 flength= FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;
            
            path = activeDir + "\\";

            string dates = "QM";//+ DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;
           // string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());
            //filename = filename.Replace("(", "_");
            //filename = filename.Replace(")", "_");
            //filename = filename.Replace("&", "");
            //filename = filename.Replace("/", "");
            //filename = filename.Replace("'", "");
            //filename = filename.Replace("  ", "");
            //DataTable dt = (DataTable)Session["UploadedFiles"];

            //FileUpload1.SaveAs(path + filename);
            ClearContents(sender as Control);

        }
        catch (Exception Ex)
        {
            Logger.WriteEventLog(false, Ex);
        }
    }

    private void ClearContents(Control control)
    {
        for (var i = 0; i < Session.Keys.Count; i++)
        {
            if (Session.Keys[i].Contains(control.ClientID))
            {
                Session.Remove(Session.Keys[i]);
                break;
            }
        }
    }  

    #endregion
}

