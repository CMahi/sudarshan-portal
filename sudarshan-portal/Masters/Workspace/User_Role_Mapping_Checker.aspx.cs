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
using FSL.Validation;
using AjaxPro;

public partial class User_Role_Mapping_Checker : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(User_Role_Mapping_Checker));
                if (!IsPostBack)
                {
                    txt_ActionBy.Value = (string)Session["User_ADID"];
                    txt_ActionObjectID.Value = Request.Params.Get("objID");
                    ShowButtonPanel();
                    SetUserActions();
                }
                HideValidator();
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    protected void btn_ViewAll_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else ShowSearchUserRole(); 
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    protected void btn_ShowSearchResult_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (isValidRequest())
                {
                    string ActionStatus = string.Empty;
                    string RoleName = txt_Search_RoleName.Value.Trim();
                    string UserName = txt_Search_UserName.Value.Trim();
                    string RequestID = string.IsNullOrEmpty(txt_RequestId.Value.Trim()) ? "0" : txt_RequestId.Value.Trim();
                    DataTable UserRoleMap = (DataTable)ActionController.ExecuteAction((string)Session["User_ADID"], "User_Role_Mapping_Checker.aspx", "select user role map request summary", ref ActionStatus, (string)Session["User_ADID"], UserName, RoleName, RequestID);
                    if (UserRoleMap != null)
                    {
                        Session["SearchResultSet"] = UserRoleMap;
                        dgSearchResult.DataSource = UserRoleMap;
                        dgSearchResult.DataBind();
                        div_DiaplayResultSetUserRoleInfo.Style["display"] = "block";
                    }
                    else
                    {
                        div_DiaplayResultSetUserRoleInfo.Style["display"] = "none";
                        if (!string.IsNullOrEmpty(ActionStatus)) ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{alert('" + ActionStatus.Split(':')[1] + "');}</script>");
                        else ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{alert('No Data Found!!!');}</script>");
                    }
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
    }
    protected void CreateLink(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataTable dtSearchDisplay = (DataTable)Session["SearchResultSet"];
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes.Add("onclick", "return showUserRoleRequestDetail('" + dtSearchDisplay.Rows[e.Row.DataItemIndex][0].ToString() + "');");
        }
        catch (Exception) { }
    }
    protected void dgSearchResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                dgSearchResult.PageIndex = e.NewPageIndex;
                dgSearchResult.DataSource = (DataTable)Session["SearchResultSet"];
                dgSearchResult.DataBind();
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
    }

    protected void btn_Approve_onClick(object sender, EventArgs e)
    {
        SaveCheckerAction("ACCEPTED");
    }
    protected void btn_Reject_onClick(object sender, EventArgs e)
    {
        SaveCheckerAction("REJECTED");
    }
    private   void SaveCheckerAction(string Action)
    {
        string ActionStatus = string.Empty;
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                if (isValidRequest(Action))
                {
                    string RequestID = txt_RequestID_Save.Value;
                    bool IsiPSuccessful = true;
                    //IsiPSuccessful = Action.Equals("ACCEPTED") ? UserManager.ExecuteAction(this, "addremove user", ref ActionStatus) : IsiPSuccessful;
                    //txt_RequestID_Save.Value = IsiPSuccessful ? "0" : RequestID;
                    //IsiPSuccessful = (Action.Equals("ACCEPTED") && (IsiPSuccessful)) ? UserManager.ExecuteAction(this, "addremove rights", ref ActionStatus) : IsiPSuccessful;
                    txt_RequestID_Save.Value = RequestID;

                    if (IsiPSuccessful)
                    {
                        ActionStatus = string.Empty;
                        txt_ActionStatus_Save.Value = Action;
                        Object isSuccess = ActionController.ExecuteAction(this, "save checker action", ref ActionStatus);
                        if (isSuccess != null)
                        {
                            ShowButtonPanel();
                            ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('Transaction completed!')}</script>");
                            ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{ResizeFrameWindows(false);onLoad(true);}</script>");
                        }
                        else
                        {
                            ShowUserRoleMap(txt_RequestID_Save.Value);
                            ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('" + ActionStatus.Split(':')[1] + "')}</script>");
                            ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
                         }
                    }
                    else
                    {
                        ShowUserRoleMap(txt_RequestID_Save.Value);
                        ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('" + ActionStatus + "')}</script>");
                        ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
                    }
                }
                else
                {
                    ShowUserRoleMap(txt_RequestID_Save.Value);
                    ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
       // ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
    }
    private   bool isValidRequest()
    {
        bool isvalid = true;
        try
        {
            string RoleName = txt_Search_RoleName.Value.Trim();
            string UserName = txt_Search_UserName.Value.Trim();
            string RequestID = txt_RequestId.Value.Trim();
            if ((string.IsNullOrEmpty(RoleName)) && (string.IsNullOrEmpty(UserName)) && (string.IsNullOrEmpty(RequestID)))
            {
                div_txt_Search.Visible = true;
                lbl_txt_Search.Text = "ValidationErr- Enter value for at least one search field!";
                isvalid = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(RequestID))
                    if (!Validator.validateNaturalNumber(RequestID))
                    {
                        div_txt_Search.Visible = true;
                        lbl_txt_Search.Text = "ValidationErr- Please enter a positive integer number for 'Request ID' field!";
                        isvalid = false;
                    }
            }
        }
        catch (Exception Exc) { isvalid = false; throw new Exception(Exc.Message, Exc); }
        return isvalid;
    }
    private   bool isValidRequest(string ActionStatus)
    {
        bool isvalid = true;
        try
        {
            string Remark = txt_Remark.Value.Trim();
            if ((ActionStatus.Equals("REJECTED")) && (string.IsNullOrEmpty(Remark)))
            {
                div_txt_Remark.Visible = true;
                lbl_txt_Remark.Text = "ValidationErr- Enter value for the remark field!";
                isvalid = false;
            }
            if (Remark.Length > 2000)
            {
                div_txt_Remark.Visible = true;
                lbl_txt_Remark.Text = "ValidationErr- Maximum size for the remark field is 2000 character!";
                isvalid = false;
            }
        }
        catch (Exception Exc) { isvalid = false; throw new Exception(Exc.Message, Exc); }
        return isvalid;
    }
    private   void ShowUserRoleMap(string RequestID)
    {
        try
        {
            DataTable RequestDtls = (DataTable)Session["AccessObjectRoleMap"];
            if (RequestDtls != null)
            {
                if (RequestDtls.Rows.Count != 0)
                {
                    StringBuilder HTMLData = new StringBuilder();
                    HTMLData.Append("<table id='tbl_RequestHdr' align='center' cellspacing='0' cellpadding='0' width='100%'style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:0px solid #ADBBCA;' >");
                    HTMLData.Append("<tr><td  align='center'><hr style='height: 1px; color: #ADBBCA;'/></td></tr>");
                    HTMLData.Append("<tr><td align='center'><table align='center' cellspacing='0' cellpadding='0' width='97%' style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:0px solid #ADBBCA;' >");
                    HTMLData.Append("<tr>");
                    HTMLData.Append("<td align='left' width='15%'>&nbsp;Request ID</td>");
                    HTMLData.Append("<td>: &nbsp;&nbsp;" + RequestID + "</td>");
                    HTMLData.Append("<td width='35%'></td>");
                    HTMLData.Append("<td width='15%'>Status</td>");
                    HTMLData.Append("<td >: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["approve_status"] is DBNull ? string.Empty : RequestDtls.Rows[0]["approve_status"].ToString()) + "&nbsp;</td>");
                    HTMLData.Append("</tr>");
                    HTMLData.Append("<tr><td><br /></td></tr>");
                    HTMLData.Append("<tr>");
                    HTMLData.Append("<td align='left' width='15%'>&nbsp;Created By</td>");
                    HTMLData.Append("<td>: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["AMD_BY"] is DBNull ? string.Empty : RequestDtls.Rows[0]["AMD_BY"].ToString()) + "</td>");
                    HTMLData.Append("<td width='35%'></td>");
                    HTMLData.Append("<td width='15%'>Request Date</td>");
                    HTMLData.Append("<td >: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["amd_date"] is DBNull ? string.Empty : RequestDtls.Rows[0]["amd_date"].ToString()) + "&nbsp;</td>");
                    HTMLData.Append("</tr>");
                    HTMLData.Append("<tr><td><br /></td></tr>");
                    HTMLData.Append("<tr>");
                    HTMLData.Append("<td align='left' width='15%'>&nbsp;Request For</td>");
                    HTMLData.Append("<td colspan='4'>: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["amd_condition"] is DBNull ? string.Empty : RequestDtls.Rows[0]["amd_condition"].ToString()) + "</td>");
                    HTMLData.Append("</tr>");
                    HTMLData.Append("</table></td></tr>");
                    HTMLData.Append("<tr><td><br /></td></tr>");
                    HTMLData.Append("<tr><td  align='center'><hr style='height: 1px; color: #ADBBCA;'/></td></tr>");
                    HTMLData.Append("</table>");
                    Div_RequestDtl_Header.InnerHtml = HTMLData.ToString();

                    HTMLData = new StringBuilder();
                    HTMLData.Append("<table id='tbl_RequestDtl' align='center' cellspacing='0' cellpadding='0' width='97%'style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:1px solid #ADBBCA;' >");
                    HTMLData.Append("<thead><tr><th  width='50%'>ROLE NAME</th><th align='center' >USER NAME</th></tr></thead>");
                    HTMLData.Append("<tbody>");
                    foreach (DataRow ObjectRow in RequestDtls.Rows)
                    {
                        HTMLData.Append("<tr>");
                        HTMLData.Append("<td style='border: 1px solid #ADBBCA;' align='left' width='50%' valign='middle'>&nbsp;&nbsp;&nbsp;" + (string)ObjectRow["access_role_name"] + "</td>");
                        HTMLData.Append("<td style='border: 1px solid #ADBBCA;' align='left' width='50%' valign='middle'>&nbsp;&nbsp;&nbsp;" + (string)ObjectRow["user_adid"] + "</td>");
                        HTMLData.Append("</tr>");
                    }
                    HTMLData.Append("</tbody>");
                    HTMLData.Append("</table>");
                    Div_RequestDtl_Dtl.InnerHtml = HTMLData.ToString();

                    tbl_Search.Style["display"] = "none";
                    tbl_SearchResult.Style["display"] = "block";
                    div_Save.Style["display"] = "block";
                }
            }
        }
        catch (Exception Exc) { throw new Exception(Exc.Message, Exc); }
    }

    private   void ShowButtonPanel()
    {
        try
        {
            tbl_ShowButtonPanel.Style["display"] = "block";
            tbl_EditAccessObjectRoleMap.Style["display"] = "none";
        }
        catch (Exception) { }
    } 
    private   void ShowSearchUserRole()
    {
        try
        {
            tbl_ShowButtonPanel.Style["display"] = "none";
            tbl_EditAccessObjectRoleMap.Style["display"] = "block";
            tbl_Search.Style["display"] = "block";
            tbl_SearchResult.Style["display"] = "none";
            div_DiaplayResultSetUserRoleInfo.Style["display"] = "none";
            div_Save.Style["display"] = "none";

            txt_Search_RoleName.Visible = true;
            txt_Search_UserName.Visible = true;
            txt_Search_RoleName.Value = string.Empty;
            txt_Search_UserName.Value = string.Empty;
            txt_RequestId.Visible = true;
            txt_RequestId.Value = string.Empty;
            txt_Remark.Value = string.Empty;
            ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
        }
        catch (Exception) { }
    } 
    private   void HideValidator()
    {
        try
        {
            div_txt_Search.Visible = false;
            div_txt_Remark.Visible = false;
        }
        catch (Exception) { }
    }
    private   void SetUserActions()
    {
        try
        {
            string UserAccess = ActionController.ObjectAccesses((string)Session["User_ADID"], Request.Params.Get("objID"));
            if (string.IsNullOrEmpty(UserAccess))
                Response.Write("<script>window.open('/Sudarshan-Portal-NEW/ErrorPages/Error.htm','frmset_WorkArea');</script>");
            else
            {
                if (UserAccess.IndexOf("1") == -1) { div_AccessMessage.Visible = true; div_Buttons.Visible = false; }
                else
                {
                    if ((UserAccess.Length >= 1) && (UserAccess.Substring(0, 1).Equals("1")))
                    {
                        btn_ViewAll.Visible = true;
                        btn_ViewAll.Enabled = true;
                        div_Save.Visible = ((UserAccess.Length >= 5) && (UserAccess.Substring(4, 1).Equals("1"))) ? true : false;
                    }
                }
            }
        }
        catch (Exception) { }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.Read)]
    public string[] getRequestDtl(string RequestID)
    {
        string[] HTML = new string[] { "<strong style='text-align: Center; color: red;'>No Data Found!!!</strong>", string.Empty };
        try
        {
            if (ActionController.IsSessionExpired(this, true))
                HTML[0] = "Session Expired.";
            else
            {
                string UserName = (string)Session["User_ADID"];
                string ActionStatus = string.Empty;
                DataTable RequestDtls = (DataTable)ActionController.ExecuteAction(UserName, "User_Role_Mapping_Checker.aspx", "select user role map request summary detail", ref ActionStatus, UserName, RequestID);
                if (RequestDtls != null)
                {
                    if (RequestDtls.Rows.Count != 0)
                    {
                        Session["AccessObjectRoleMap"] = RequestDtls;
                        StringBuilder HTMLData = new StringBuilder();
                        HTMLData.Append("<table id='tbl_RequestHdr' align='center' cellspacing='0' cellpadding='0' width='100%'style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:0px solid #ADBBCA;' >");
                        HTMLData.Append("<tr><td  align='center'><hr style='height: 1px; color: #ADBBCA;'/></td></tr>");
                        HTMLData.Append("<tr><td align='center'><table align='center' cellspacing='0' cellpadding='0' width='97%' style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:0px solid #ADBBCA;' >");
                        HTMLData.Append("<tr>");
                        HTMLData.Append("<td align='left' width='15%'>&nbsp;Request ID</td>");
                        HTMLData.Append("<td>: &nbsp;&nbsp;" + RequestID + "</td>");
                        HTMLData.Append("<td width='35%'></td>");
                        HTMLData.Append("<td width='15%'>Status</td>");
                        HTMLData.Append("<td >: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["approve_status"] is DBNull ? string.Empty : RequestDtls.Rows[0]["approve_status"].ToString()) + "&nbsp;</td>");
                        HTMLData.Append("</tr>");
                        HTMLData.Append("<tr><td><br /></td></tr>");
                        HTMLData.Append("<tr>");
                        HTMLData.Append("<td align='left' width='15%'>&nbsp;Created By</td>");
                        HTMLData.Append("<td>: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["AMD_BY"] is DBNull ? string.Empty : RequestDtls.Rows[0]["AMD_BY"].ToString()) + "</td>");
                        HTMLData.Append("<td width='35%'></td>");
                        HTMLData.Append("<td width='15%'>Request Date</td>");
                        HTMLData.Append("<td >: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["amd_date"] is DBNull ? string.Empty : RequestDtls.Rows[0]["amd_date"].ToString()) + "&nbsp;</td>");
                        HTMLData.Append("</tr>");
                        HTMLData.Append("<tr><td><br /></td></tr>");
                        HTMLData.Append("<tr>");
                        HTMLData.Append("<td align='left' width='15%'>&nbsp;Request For</td>");
                        HTMLData.Append("<td colspan='4'>: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["amd_condition"] is DBNull ? string.Empty : RequestDtls.Rows[0]["amd_condition"].ToString()) + "</td>");
                        HTMLData.Append("</tr>");
                        HTMLData.Append("</table></td></tr>");
                        HTMLData.Append("<tr><td><br /></td></tr>");
                        HTMLData.Append("<tr><td  align='center'><hr style='height: 1px; color: #ADBBCA;'/></td></tr>");
                        HTMLData.Append("</table>");
                        HTML[0] = HTMLData.ToString();

                        HTMLData = new StringBuilder();
                        HTMLData.Append("<table id='tbl_RequestDtl' align='center' cellspacing='0' cellpadding='0' width='97%'style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:1px solid #ADBBCA;' >");
                        HTMLData.Append("<thead><tr><th  width='50%'>ROLE NAME</th><th align='center' >USER NAME</th></tr></thead>");
                        HTMLData.Append("<tbody>");
                        foreach (DataRow ObjectRow in RequestDtls.Rows)
                        {
                            HTMLData.Append("<tr>");
                            HTMLData.Append("<td style='border: 1px solid #ADBBCA;' align='left' width='50%' valign='middle'>&nbsp;&nbsp;&nbsp;" + (string)ObjectRow["access_role_name"] + "</td>");
                            HTMLData.Append("<td style='border: 1px solid #ADBBCA;' align='left' width='50%' valign='middle'>&nbsp;&nbsp;&nbsp;" + (string)ObjectRow["user_adid"] + "</td>");
                            HTMLData.Append("</tr>");
                        }
                        HTMLData.Append("</tbody>");
                        HTMLData.Append("</table>");
                        HTML[1] = HTMLData.ToString();
                    }
                }
                else HTML[0] = "<strong style='text-align: Center; color:red;'>" + ActionStatus.Split(':')[1].ToString() + "</strong>";
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return HTML;
    }
}
