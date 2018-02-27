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

public partial class AccessObject_Role_Mapping : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(AccessObject_Role_Mapping));
                HideValidator();
                if (!IsPostBack)
                {
                    txt_RoleName.Attributes.Add("readonly", "readonly");
                    txt_ActionBy.Value = (string)Session["User_ADID"];
                    txt_ActionObjectID.Value = Request.Params.Get("objID");
                    ActionController.SetControlAttributes(this);
                    ShowButtonPanel();
                    SetUserActions(); 
                    DataTable Dt_Panel = (DataTable)Application["PANEL"];
                    foreach (DataRow PanelRow in Dt_Panel.Rows)
                        ddl_Panel.Items.Add(new ListItem(PanelRow["PANE_TYPE"].ToString(), PanelRow["PANE_ID"].ToString()));
                    ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(true);}</script>");
                }
             }
         }
         catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }   
    }
    protected void btn_Search_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
               ShowSearchAccessObjectRole();
        }
        catch (Exception) { }
    }
    protected void btn_ShowSearchResult_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                string ActionStatus = string.Empty;
                DataTable Dt_Roles = (DataTable)ActionController.ExecuteAction((string)Session["User_ADID"], "AccessObject_Role_Mapping.aspx", "select roles", ref ActionStatus, 0, txt_SearchPattern.Value.Trim(), (string)Session["User_ADID"]);
                if (Dt_Roles != null)
                {
                    Session["SearchResultSet"] = Dt_Roles;
                    dgSearchResult.DataSource = Dt_Roles;
                    dgSearchResult.DataBind();
                    div_DiaplayResultSetAccessObjectRoleInfo.Style["display"] = "block";
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
                e.Row.Attributes.Add("onclick", "return showEditGrid('" + dtSearchDisplay.Rows[e.Row.DataItemIndex][0].ToString() + "','" + dtSearchDisplay.Rows[e.Row.DataItemIndex][1].ToString() + "');");
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

    protected void btn_Show_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                if (!ActionController.ValidateForm(this, "select panelwise access objects", new Hashtable())) { div_ObjMap.Visible = false; div_Save.Visible = false; }
                else
                {
                    string ActionStatus = string.Empty;
                    string RoleName = txt_RoleName.Text;
                    txt_RoleName.Text = txt_RoleID.Value;
                    DataTable PanelWiseObjects = (DataTable)ActionController.ExecuteAction(this, "select panelwise access objects", ref ActionStatus);
                    txt_RoleName.Text = RoleName;
                    if ((PanelWiseObjects != null) && (PanelWiseObjects.Rows.Count > 0))
                    {
                        const int NoOfAccessRights = 6;
                        StringBuilder HTML = new StringBuilder();
                        HTML.Append("<table id='tbl_ObjMap' align='center' cellspacing='0' cellpadding='0' width='100%'style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:1px solid #ADBBCA;' >");
                        HTML.Append("<thead><tr><th  width='50%'>ACCESS OBJECTS</th><th align='center' ><table align='center' cellspacing='0' cellpadding='0' width='100%' ><thead><tr ><th colspan='" + NoOfAccessRights + "' style='border-bottom: 1px solid #fff;' >ACCESS RIGHTS</th></tr><tr ><th width='17%'>VIEW</th><th width='17%'>ADD</th><th width='17%'>MODIFY</th><th width='17%'>DELETE</th><th width='17%'>APPROVE</th><th width='17%'>PRINT</th></tr></thead></table></th></tr></thead>");
                        HTML.Append("<tbody>");
                        int RowCnt = 0;
                        string[] AccessValues = new string[NoOfAccessRights] { "VIEW", "ADD", "MODIFY", "DELETE", "APPROVE", "PRINT" };
                        foreach (DataRow ObjectRow in PanelWiseObjects.Rows)
                        {
                            string Access_Type_Allowed = ObjectRow["ACCESS_TYPE_ALLOWED"] is DBNull ? null : (string)ObjectRow["ACCESS_TYPE_ALLOWED"];
                            string Access_Type_Given   = ObjectRow["ACCESS_TYPE"] is DBNull ? "000000"     : (string)ObjectRow["ACCESS_TYPE"];
                            HTML.Append("<tr>");
                            HTML.Append("<td style='border: 1px solid #ADBBCA;' align='left' width='50%' valign='middle'>&nbsp;&nbsp;&nbsp;" + (string)ObjectRow["relative_name"] + "<input type='hidden' id='txt_ObjectID" + (++RowCnt) + "' value='" + (ObjectRow["obj_id"].ToString() + "~" + ObjectRow["HASACCESS"].ToString() + "~" + Access_Type_Given) + "' readOnly></td>");
                            HTML.Append("<td style='border: 1px solid #ADBBCA;' align='center'  valign='middle' ><table align='center' cellspacing='0' cellpadding='0' width='100%' ><tr>");
                            if (string.IsNullOrEmpty(Access_Type_Allowed))
                                HTML.Append("<td colspan='" + NoOfAccessRights + "' style='color:red;'>Access Rights Not Permitted!!!</td>");
                            else
                                for (int BitIndex = 0; BitIndex < NoOfAccessRights ; BitIndex++)
                                    if (ObjectRow["HASACCESS"].ToString().Equals("0"))
                                        HTML.Append("<td style='border-right: 1px solid #ADBBCA;' align='center' valign='middle' width='17%' ><input  type='checkbox' id='AccessRights" + RowCnt + "' name='AccessRights" + RowCnt + "' " + (BitIndex < Access_Type_Allowed.Length ? (Access_Type_Allowed[BitIndex].Equals('1') ? "" : "disabled  style='visibility:hidden'") : "disabled  style='visibility:hidden'") + " style='valign:middle;'  value='" + AccessValues[BitIndex] + "' onclick='return chk_View(" + RowCnt + ");' ></td>");
                                    else
                                        HTML.Append("<td style='border-right: 1px solid #ADBBCA;' align='center' valign='middle' width='17%' ><input  type='checkbox' id='AccessRights" + RowCnt + "' name='AccessRights" + RowCnt + "' " + (BitIndex < Access_Type_Allowed.Length ? (Access_Type_Given[BitIndex].Equals('1') ? "checked" : (Access_Type_Allowed[BitIndex].Equals('1') ? "" : "disabled  style='visibility:hidden'")) : "disabled  style='visibility:hidden'") + " style='valign:middle;'  value='" + AccessValues[BitIndex] + "' onclick='return chk_View(" + RowCnt + ");'></td>");
                            HTML.Append("</tr></table></td>");
                            HTML.Append("</tr>");
                        }
                        HTML.Append("</tbody>");
                        HTML.Append("</table>");
                        div_ObjMap.InnerHtml = HTML.ToString();
                        div_Save.Visible = true;
                    }
                    else
                    {
                        div_ObjMap.InnerHtml = "<h5 style='color:red;'>Sorry, No Data Found!!!</h5>";
                        div_Save.Visible = false;
                    }
                    div_ObjMap.Visible = true;
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        tbl_Search.Style["display"] = "none";
        tbl_SearchResult.Style["display"] = "block";
        ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
    }
    protected void btn_Save_onClick(object sender, EventArgs e)
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
                    if (ActionController.ValidateForm(this, "save object-role map", new Hashtable()))
                    {
                        object RequestID = ActionController.ExecuteAction(this, "save object-role map", ref ActionStatus);
                        if ((RequestID != null) && (!((string)RequestID).Equals("0")))
                        {
                            ShowButtonPanel();
                            ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('Transaction completed successfully! Request ID is " + (string)RequestID + "')}</script>");
                            ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{ResizeFrameWindows(false);onLoad(true);}</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('" + ActionStatus.Split(':')[1].ToString() + "')}</script>");
                            ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
                        }
                    }
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        txt_XMLData.Value = string.Empty;
        //ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
    }
    protected void ResetForm(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                ddl_Panel.SelectedIndex = 0;
                div_ObjMap.Visible = false;
                div_Save.Visible = false;
            }
        }
        catch (Exception) { }
        ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
    }

    protected void btn_ViewAll_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else ShowAllAccessObjectRoleMap();
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    protected void btn_ShowSearchResult_View_onClick(object sender, EventArgs e) 
    {
        try
        {
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                string ActionStatus = string.Empty;
                DataTable Dt_AccessObjectRoleMap = (DataTable)ActionController.ExecuteAction((string)Session["User_ADID"], "AccessObject_Role_Mapping.aspx", "select access object request summary", ref ActionStatus, (string)Session["User_ADID"], Txt_SearchPattern_View.Value.Trim()); 
                if (Dt_AccessObjectRoleMap != null)
                {
                    Session["ResultSet"] = Dt_AccessObjectRoleMap;
                    dgViewAll.DataSource = Dt_AccessObjectRoleMap;
                    dgViewAll.DataBind();
                    div_ShowAccessObjectRoles.Visible = true;
                }
                else
                    ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('" + ActionStatus.Split(':')[1].ToString() + "')}</script>");
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
    private void ShowSearchAccessObjectRole()
    {
        try
        {
            tbl_ShowButtonPanel.Style["display"] = "none";
            tbl_EditAccessObjectRoleMap.Style["display"] = "block";
            tbl_showViewAllPanel.Style["display"] = "none";
            tbl_Search.Style["display"] = "block";
            div_DiaplayResultSetAccessObjectRoleInfo.Style["display"] = "none";
            tbl_SearchResult.Style["display"] = "none";
            txt_SearchPattern.Visible = true;
            txt_SearchPattern.Value = string.Empty;
            ddl_Panel.SelectedIndex = 0;
            div_ObjMap.Visible = false;
            div_Save.Visible = false;
            txt_ActionDate.Value = DateTime.Now.ToShortDateString();
            ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
        }
        catch (Exception) { }
    }
    private void ShowAllAccessObjectRoleMap()
    { 
        try
        {
            tbl_ShowButtonPanel.Style["display"] = "none";
            tbl_EditAccessObjectRoleMap.Style["display"] = "none";
            tbl_showViewAllPanel.Style["display"] = "block";
            div_ShowAccessObjectRoles.Visible = false;
            Txt_SearchPattern_View.Value = string.Empty;
            ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{onLoad(false);}</script>");
        }
        catch (Exception) { }
    }
    private void HideValidator()
    {
        try
        {
            div_txt_RoleName.Visible = false;
            div_ddl_Panel.Visible = false;
            div_txt_XMLData.Visible = false;
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
                        if ((UserAccess.Length >= 4) && (UserAccess.Substring(0, 4).Equals("1111"))) 
                        {
                            btn_Search.Visible = true;
                            btn_Search.Enabled = true; 
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
        string[] HTML = new string[]{"<strong style='text-align: Center; color: red;'>No Data Found!!!</strong>",string.Empty};
        try
        {
            if (ActionController.IsSessionExpired(this, true))
                HTML[0] = "Session Expired.";
            else
            {
                string UserName     = (string)Session["User_ADID"];
                string ActionStatus = string.Empty;
                DataTable RequestDtls = (DataTable)ActionController.ExecuteAction(UserName, "AccessObject_Role_Mapping.aspx", "select access object request detail", ref ActionStatus, UserName, RequestID);
                if (RequestDtls != null)
                {
                    if (RequestDtls.Rows.Count!= 0) 
                    {                           
                            StringBuilder HTMLData = new StringBuilder();
                            HTMLData.Append("<table id='tbl_RequestHdr' align='center' cellspacing='0' cellpadding='0' width='100%'style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:0px solid #ADBBCA;' >");
                            HTMLData.Append("<tr><td  align='right'><input class='ApScrnButton' name='btn_Home_ViewAll_ReqDtl' id='btn_Home_ViewAll_ReqDtl' type='button' value=' Back' onclick='return btn_Home_onClick();' onmouseout=\"this.className='ApScrnButton';\"  onmouseover=\"this.className='ApScrnButtonHover';\" />&nbsp;&nbsp;</td></tr>");
                            HTMLData.Append("<tr><td  align='center'><hr style='height: 1px; color: #ADBBCA;'/></td></tr>");
                            HTMLData.Append("<tr><td align='center'><table align='center' cellspacing='0' cellpadding='0' width='97%' style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:0px solid #ADBBCA;' >");
                            HTMLData.Append("<tr >");
                            HTMLData.Append("<td align='left' width='15%'>&nbsp;Role Name</td>");
                            HTMLData.Append("<td>: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["access_role_name"] is DBNull ? string.Empty : RequestDtls.Rows[0]["access_role_name"].ToString()) + "</td>");
                            HTMLData.Append("<td width='25%'></td>");
                            HTMLData.Append("<td width='15%'>Panel Name</td>");
                            HTMLData.Append("<td >: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["pane_desc"] is DBNull ? string.Empty : RequestDtls.Rows[0]["pane_desc"].ToString()) + "&nbsp;</td>");
                            HTMLData.Append("</tr>");
                            HTMLData.Append("<tr><td><br /></td></tr>");
                            HTMLData.Append("<tr>");
                            HTMLData.Append("<td align='left' width='15%'>&nbsp;Status</td>");
                            HTMLData.Append("<td>: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["approve_status"] is DBNull ? string.Empty : RequestDtls.Rows[0]["approve_status"].ToString()) + "</td>");
                            HTMLData.Append("<td width='25%'></td>");
                            HTMLData.Append("<td width='15%'>Approver</td>");
                            HTMLData.Append("<td >: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["approve_by"] is DBNull ? string.Empty : RequestDtls.Rows[0]["approve_by"].ToString()) + "&nbsp;</td>");
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
                            HTMLData.Append("<td align='left' width='15%'>&nbsp;Approver Remark</td>");
                            HTMLData.Append("<td colspan='4'>: &nbsp;&nbsp;" + (RequestDtls.Rows[0]["approve_remark"] is DBNull ? string.Empty : RequestDtls.Rows[0]["approve_remark"].ToString()) + "</td>");
                            HTMLData.Append("</tr>");
                            HTMLData.Append("</table></td></tr>");
                            HTMLData.Append("<tr><td><br /></td></tr>");
                            HTMLData.Append("<tr><td  align='center'><hr style='height: 1px; color: #ADBBCA;'/></td></tr>");
                            HTMLData.Append("</table>");
                            HTML[0] = HTMLData.ToString();

                            HTMLData = new StringBuilder();
                            const int NoOfAccessRights = 6;
                            HTMLData.Append("<table id='tbl_RequestDtl' align='center' cellspacing='0' cellpadding='0' width='97%'style='font-family:Verdana;font-size:10px;color:#000000;text-align:Left;border-collapse:collapse; border:1px solid #ADBBCA;' >");
                            HTMLData.Append("<thead><tr><th  width='50%'>ACCESS OBJECTS</th><th align='center' ><table align='center' cellspacing='0' cellpadding='0' width='100%' ><thead><tr ><th colspan='" + NoOfAccessRights + "' style='border-bottom: 1px solid #fff;' >ACCESS RIGHTS</th></tr><tr ><th width='17%'>VIEW</th><th width='17%'>ADD</th><th width='17%'>MODIFY</th><th width='17%'>DELETE</th><th width='17%'>APPROVE</th><th width='17%'>PRINT</th></tr></thead></table></th></tr></thead>");
                            HTMLData.Append("<tbody>");
                            foreach (DataRow ObjectRow in RequestDtls.Rows)
                            {
                                HTMLData.Append("<tr>");
                                HTMLData.Append("<td style='border: 1px solid #ADBBCA;' align='left' width='50%' valign='middle'>&nbsp;&nbsp;&nbsp;" + (string)ObjectRow["relative_name"] + "</td>");
                                HTMLData.Append("<td style='border: 1px solid #ADBBCA;' align='center'  valign='middle' ><table align='center' cellspacing='0' cellpadding='0' width='100%' ><tr>");
                                for(int BitIndex = 0; BitIndex < NoOfAccessRights; BitIndex++)
                                    HTMLData.Append("<td style='border-right: 1px solid #ADBBCA;' align='center' valign='middle' width='17%' ><input  type='checkbox' id='AccessRights' name='AccessRights' " + (((string)ObjectRow["access_type"])[BitIndex].Equals('1') ? "checked " : "") + " disabled style='valign:middle;' ></td>");
                                HTMLData.Append("</tr></table></td>");
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
