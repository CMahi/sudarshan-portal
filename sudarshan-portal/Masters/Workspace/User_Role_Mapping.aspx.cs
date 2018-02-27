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

public partial class User_Role_Mapping : System.Web.UI.Page 
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(User_Role_Mapping));
                HideValidator();
                if (!IsPostBack)
                {
                    txt_Search_RoleName.Attributes.Add("readonly", "readonly");
                    txt_Search_UserName.Attributes.Add("readonly", "readonly");
                    txt_ActionBy.Value = (string)Session["User_ADID"];
                    txt_ActionObjectID.Value = Request.Params.Get("objID");
                    ActionController.SetControlAttributes(this);
                    ShowButtonPanel();
                    SetUserActions();
                    ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(true);}</script>");
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }   
    } 
    protected void btn_Add_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
                ShowSearchUserRole("ADD");
        }
        catch (Exception) { }
    }
    protected void btn_Modify_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
                ShowSearchUserRole("MODIFY");
        }
        catch (Exception) { }
    }
    protected void btn_ViewAll_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else ShowAllUserRoleMap();
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void ResetForm(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                txt_Search_RoleName.Value = string.Empty;
                txt_Search_UserName.Value = string.Empty;
                txt_RoleID.Value = string.Empty;
                div_ObjMap.InnerHtml = string.Empty;
                tbl_SearchResult.Style["display"] = "none";
            }
        }
        catch (Exception) { }
        ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
    }
    protected void btn_Show_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                if (isValidModRequest())
                {
                    string ActionStatus = string.Empty;
                    txt_RoleID.Value = string.IsNullOrEmpty(txt_RoleID.Value) ? "0" : txt_RoleID.Value;
                    DataTable UserRoleMap = (DataTable)ActionController.ExecuteAction((string)Session["User_ADID"], "User_Role_Mapping.aspx", "select user-role map", ref ActionStatus, (string)Session["User_ADID"], txt_Search_UserName.Value, txt_RoleID.Value);
                    if ((UserRoleMap != null) && (UserRoleMap.Rows.Count > 0))
                    {
                        StringBuilder HTML = new StringBuilder();
                        HTML.Append("<table id='tbl_ObjMap' align='center' cellspacing='0' cellpadding='0' width='97%'style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:1px solid #ADBBCA;' >");
                        HTML.Append("<thead><tr><th width='5%'>REMOVE<img src=/Sudarshan-Portal-NEW/Images/IsTrue.gif alt='Click here to remove all' border='0' onClick='SelectAll();'></th><th  width='50%'>ROLE NAME</th><th align='center' >USER NAME</th></tr></thead>");
                        HTML.Append("<tbody>");
                        int RowCnt = 0;
                        foreach (DataRow ObjectRow in UserRoleMap.Rows)
                        {
                            HTML.Append("<tr>");
                            HTML.Append("<td style='border: 1px solid #ADBBCA;' align='center' width='5%'  valign='middle'><input  type='checkbox' name='chk_SelectForDelete" + (++RowCnt) + "' id='chk_SelectForDelete" + RowCnt + "' ></td>");
                            HTML.Append("<td style='border: 1px solid #ADBBCA;' align='left'   width='50%' valign='middle'>&nbsp;&nbsp;&nbsp;<input type='text' id='txt_Search_RoleID" + RowCnt + "'  size='30' value='" + ObjectRow["access_role_id"].ToString() + "' disabled style='display:none;' />" + (string)ObjectRow["access_role_name"] + "</td>");
                            HTML.Append("<td style='border: 1px solid #ADBBCA;' align='center'             valign='middle'>&nbsp;&nbsp;&nbsp;<input type='text' id='txt_Search_UserID" + RowCnt + "'  size='30' value='" + ObjectRow["user_adid"].ToString() + "' disabled style='display:none;' />" + (string)ObjectRow["user_adid"] + "</td>");
                            HTML.Append("</tr>");
                        }
                        HTML.Append("</tbody>");
                        HTML.Append("</table>");
                        div_ObjMap.InnerHtml = HTML.ToString();
                        div_Save.Visible = true;
                    }
                    else
                    {
                        div_ObjMap.InnerHtml = "<h5 style='color:red;'>No Data Found!!!</h5>";
                        div_Save.Visible = false;
                    }
                    div_ObjMap.Visible = true;
                    tbl_SearchResult.Style["display"] = "block";
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
    }
    protected void btn_Save_onClick(object sender, EventArgs e)
    {
        Save("ADD");
    }
    protected void btn_ModifySave_onClick(object sender, EventArgs e)
    {
        Save("MODIFY");
    }
    
    protected void btn_ShowSearchResult_View_onClick(object sender, EventArgs e)
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
                    DataTable UserRoleMap = (DataTable)ActionController.ExecuteAction((string)Session["User_ADID"], "User_Role_Mapping.aspx", "select user role map request summary", ref ActionStatus, (string)Session["User_ADID"], txt_SearchUser_View.Value.Trim(), txt_SearchRole_View.Value.Trim());
                    if (UserRoleMap != null)
                    {
                        Session["ResultSet"] = UserRoleMap;
                        dgViewAll.DataSource = UserRoleMap;
                        dgViewAll.DataBind();
                        div_ShowUserRoles.Visible = true;
                    }
                    else
                        ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('" + ActionStatus.Split(':')[1].ToString() + "')}</script>");
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
    }
    protected void CreateServerSideLink(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataTable dtSearchDisplay = (DataTable)Session["ResultSet"];
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes.Add("onclick", "return showRequestDetail('" + dtSearchDisplay.Rows[e.Row.DataItemIndex][0].ToString() + "');");
        }
        catch (Exception) { }
    }
    protected void dgViewAll_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                dgViewAll.PageIndex = e.NewPageIndex;
                dgViewAll.DataSource = (DataTable)Session["ResultSet"];
                dgViewAll.DataBind();
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
    }

    private void Save(string Action)
    {
        string ActionStatus = string.Empty;
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                string XMLData = txt_XMLData.Value;
                if ((string.IsNullOrEmpty(XMLData)) || (XMLData.ToUpper().Equals("|ROWSET|||/ROWSET||")))
                {
                    div_txt_XMLData.Visible = true;
                    lbl_txt_XMLData.Text = "No more record to save!!!";
                    ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
                }
                else
                {
                    XMLData = XMLData.Replace("||", ">");
                    XMLData = XMLData.Replace("|", "<");
                    txt_XMLData.Value = XMLData;
                    if (ActionController.ValidateForm(this, "save user-role map", new Hashtable()))
                    {
                        object RequestID = ActionController.ExecuteAction(this, "save user-role map", ref ActionStatus);
                        if ((RequestID != null) && (!((string)RequestID).Equals("0")))
                        {
                            ShowButtonPanel();
                            ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('Transaction completed successfully! Request ID is " + (string)RequestID + "')}</script>");
                            ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{ResizeFrameWindows(false);onLoad(true);}</script>");
                        }
                        else
                        {
                            if (Action.Equals("ADD")) RenderTable();
                            ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('" + ActionStatus.Split(':')[1].ToString() + "')}</script>");
                            ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
                        }
                    }
                }
            }
        }
        catch (Exception Exc)
        {
            Logger.WriteEventLog(false, Exc);            
        }
        txt_XMLData.Value = string.Empty;
        //ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
    }
    private void RenderTable()
    {
        try
        {
            string HTML = txt_ActionDate.Value;
            txt_ActionDate.Value = "";
            HTML = HTML.Replace("|||", ">");
            HTML = HTML.Replace("||", "<");
            div_ObjMap.InnerHtml = HTML;
            tbl_SearchResult.Style["display"] = "block";
        }
        catch (Exception) { }
    }
    private bool isValidRequest()
    {
        bool isvalid = true;
        try
        {
            string RoleName = txt_SearchRole_View.Value.Trim();
            string UserName = txt_SearchUser_View.Value.Trim();
            if ((string.IsNullOrEmpty(RoleName)) && (string.IsNullOrEmpty(UserName)))
            {
                div_txt_Search.Visible = true;
                lbl_txt_Search.Text = "ValidationErr- Enter value for at least one search field!";
                isvalid = false;
            }
        }
        catch (Exception Exc) { isvalid = false; throw new Exception(Exc.Message, Exc); }
        return isvalid;
    }
    private bool isValidModRequest()
    {
        bool isvalid = true;
        try
        {
            string RoleName = txt_Search_RoleName.Value.Trim();
            string UserName = txt_Search_UserName.Value.Trim();
            if ((string.IsNullOrEmpty(RoleName)) && (string.IsNullOrEmpty(UserName)))
            {
                div_txt_XMLData.Visible = true;
                lbl_txt_XMLData.Text = "ValidationErr- Enter value for at least one search field!";
                div_ObjMap.Visible = false;
                div_Save.Visible = false;
                isvalid = false;
            }
        }
        catch (Exception Exc) { isvalid = false; throw new Exception(Exc.Message, Exc); }
        return isvalid;
    }

    private void ShowButtonPanel()
    {
        try
        {
            tbl_ShowButtonPanel.Style["display"] = "block";
            tbl_EditAccessObjectRoleMap.Style["display"] = "none";
            tbl_showViewAllPanel.Style["display"] = "none";
        }
        catch (Exception) { }
    }
    private void ShowSearchUserRole(string Action)
    {
        try
        {
            tbl_ShowButtonPanel.Style["display"] = "none";
            tbl_EditAccessObjectRoleMap.Style["display"] = "block";
            tbl_showViewAllPanel.Style["display"] = "none";
            tbl_Search.Style["display"] = "block";
            tbl_SearchResult.Style["display"] = "none";
            div_ObjMap.InnerHtml = string.Empty;
            txt_Search_RoleName.Value = string.Empty;
            txt_Search_UserName.Value = string.Empty;
            txt_RoleID.Value = string.Empty;
            if (Action.Equals("ADD")) { btn_AddToGrid.Visible = true; btn_ShowData.Visible = false; btn_Save.Visible = true; btn_ModifySave.Visible = false; txt_ActionCondn.Value = "0"; }
            else                      { btn_AddToGrid.Visible = false; btn_ShowData.Visible = true; btn_Save.Visible = false; btn_ModifySave.Visible = true; txt_ActionCondn.Value = "2"; }
            txt_ActionDate.Value = DateTime.Now.ToShortDateString();
            ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
        }
        catch (Exception) { }
    }
    private void ShowAllUserRoleMap()
    {
        try
        {
            tbl_ShowButtonPanel.Style["display"] = "none";
            tbl_EditAccessObjectRoleMap.Style["display"] = "none";
            tbl_showViewAllPanel.Style["display"] = "block";
            div_ShowUserRoles.Visible = false;
            txt_SearchRole_View.Value = string.Empty;
            txt_SearchUser_View.Value = string.Empty;
            ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
        }
        catch (Exception) { }
    }
    private void HideValidator()
    {
        try
        {
            div_txt_XMLData.Visible = false;
            div_txt_Search.Visible = false;
        }
        catch (Exception) { }
    }
    private void SetUserActions()
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
                        if ((UserAccess.Length >= 2) && (UserAccess.Substring(1, 1).Equals("1")))
                        {
                            btn_Add.Visible = true;
                            btn_Add.Enabled = true;
                        }
                        if ((UserAccess.Length >=4) && (UserAccess.Substring(2, 2).Equals("11")))
                        {
                            btn_Modify.Visible = true;
                            btn_Modify.Enabled = true;
                        }
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
                DataTable RequestDtls = (DataTable)ActionController.ExecuteAction(UserName, "User_Role_Mapping.aspx", "select user role map request summary detail", ref ActionStatus, UserName, RequestID);
                if (RequestDtls != null)
                {
                    if (RequestDtls.Rows.Count != 0)
                    {
                        StringBuilder HTMLData = new StringBuilder();
                        HTMLData.Append("<table id='tbl_RequestHdr' align='center' cellspacing='0' cellpadding='0' width='100%'style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:0px solid #ADBBCA;' >");
                        HTMLData.Append("<tr><td  align='right'><input class='ApScrnButton' name='btn_Home_ViewAll_ReqDtl' id='btn_Home_ViewAll_ReqDtl' type='button' value=' Back' onclick='return btn_Home_onClick();' onmouseout=\"this.className='ApScrnButton';\"  onmouseover=\"this.className='ApScrnButtonHover';\" />&nbsp;&nbsp;</td></tr>");
                        HTMLData.Append("<tr><td  align='center'><hr style='height: 1px; color: #ADBBCA;'/></td></tr>");
                        HTMLData.Append("<tr><td align='center'><table align='center' cellspacing='0' cellpadding='0' width='97%' style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:0px solid #ADBBCA;' >");
                        HTMLData.Append("<tr>");
                        HTMLData.Append("<td align='left' width='15%'>&nbsp;Request ID</td>");
                        HTMLData.Append("<td>: &nbsp;&nbsp;" + RequestID + "</td>");
                        HTMLData.Append("<td width='25%'></td>");
                        HTMLData.Append("<td width='15%'>Request Status</td>");
                        HTMLData.Append("<td >: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["approve_status"] is DBNull ? string.Empty : RequestDtls.Rows[0]["approve_status"].ToString()) + "&nbsp;</td>");
                        HTMLData.Append("</tr>");
                        HTMLData.Append("<tr><td><br /></td></tr>");
                        HTMLData.Append("<tr>");
                        HTMLData.Append("<td align='left' width='15%'>&nbsp;Request Date</td>");
                        HTMLData.Append("<td>: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["amd_date"] is DBNull ? string.Empty : RequestDtls.Rows[0]["amd_date"].ToString()) + "</td>");
                        HTMLData.Append("<td width='25%'></td>");
                        HTMLData.Append("<td width='15%'>Approved Date</td>");
                        HTMLData.Append("<td >: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["approve_date"] is DBNull ? string.Empty : RequestDtls.Rows[0]["approve_date"].ToString()) + "&nbsp;</td>");
                        HTMLData.Append("</tr>");
                        HTMLData.Append("<tr><td><br /></td></tr>");
                        HTMLData.Append("<tr>");
                        HTMLData.Append("<td align='left' width='15%'>&nbsp;Approver Name</td>");
                        HTMLData.Append("<td >: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["approve_by"] is DBNull ? string.Empty : RequestDtls.Rows[0]["approve_by"].ToString()) + "</td>");
                        HTMLData.Append("<td width='25%'></td>");
                        HTMLData.Append("<td width='15%'>Request For</td>");
                        HTMLData.Append("<td >: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["amd_condition"] is DBNull ? string.Empty : RequestDtls.Rows[0]["amd_condition"].ToString()) + "&nbsp;</td>");
                        HTMLData.Append("</tr>");
                        HTMLData.Append("<tr><td><br /></td></tr>");
                        HTMLData.Append("<tr>");
                        HTMLData.Append("<td align='left' width='15%'>&nbsp;Approver Remark</td>");
                        HTMLData.Append("<td colspan='4'>: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["approve_remark"] is DBNull ? string.Empty : RequestDtls.Rows[0]["approve_remark"].ToString()) + "</td>");
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
