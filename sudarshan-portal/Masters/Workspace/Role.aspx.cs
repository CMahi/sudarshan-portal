using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts; 
using FSL.Logging;
using FSL.Controller;

public partial class Role : System.Web.UI.Page
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
               
                if (!IsPostBack)
                {
                    txt_ActionBy.Value = (string)Session["User_ADID"];
                    txt_ActionObjectID.Value = Request.Params.Get("objID");
                    ActionController.SetControlAttributes(this);
                    ShowButtonPanel();
                    SetUserActions();
                    string ActionStatus = string.Empty;
                    DataTable Company = (DataTable)ActionController.ExecuteAction(this, "select company", ref ActionStatus);
                    if (Company != null)
                    {
                        foreach (DataRow Row in Company.Rows)
                            ddl_CompanyID.Items.Add(new ListItem(Row[0].ToString(), Row[0].ToString()));
                    }
                }
                HideValidator();
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
 
    protected void btn_AddNew_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
                ShowAddNewRole();
        }
        catch (Exception) { }
    }
    protected void btn_Add_onClick(object sender, EventArgs e)
    {
        string ActionStatus = string.Empty;
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                if (ActionController.ValidateForm(this, "insert role", new Hashtable()))
                {
                    txt_ActionCondn.Value = "0";
                    txt_RoleID.Value      = "0";
                    txt_ActionDate.Value  = DateTime.Now.ToShortDateString();
                    object RoleID = ActionController.ExecuteAction(this, "insert role", ref ActionStatus);
                    if (RoleID != null)
                    {
                        ShowButtonPanel();
                        ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('Transaction completed!')}</script>");
                    }
                    else ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('" + ActionStatus.Split(':')[1] + "')}</script>");
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
                ShowSearchRole();
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
                DataTable Dt_Roles = (DataTable)ActionController.ExecuteAction((string)Session["User_ADID"], "Role.aspx", "select roles", ref ActionStatus, 0, txt_SearchPattern.Value.Trim(), (string)Session["User_ADID"]);
                if (Dt_Roles != null)
                {
                    Session["SearchResultSet"] = Dt_Roles;
                    dgSearchResult.DataSource = Dt_Roles;
                    dgSearchResult.DataBind();
                    div_DiaplayResultSetRoleInfo.Style["display"] = "block";
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    protected void CreateLink(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataTable dtSearchDisplay = (DataTable)Session["SearchResultSet"];
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes.Add("onclick", "return showEditGrid('" + dtSearchDisplay.Rows[e.Row.DataItemIndex][0].ToString() + "','" + dtSearchDisplay.Rows[e.Row.DataItemIndex][1].ToString() + "','" + dtSearchDisplay.Rows[e.Row.DataItemIndex][2].ToString() + "','" + dtSearchDisplay.Rows[e.Row.DataItemIndex][3].ToString() + "','" + dtSearchDisplay.Rows[e.Row.DataItemIndex][4].ToString() + "');");
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
    }
    protected void btn_Edit_onClick(object sender, EventArgs e)
    {
        string ActionStatus = string.Empty;
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                if (ActionController.ValidateForm(this, "update role", new Hashtable()))
                {
                    txt_ActionCondn.Value = "1";
                    object RoleID = ActionController.ExecuteAction(this, "update role", ref ActionStatus);
                    if (RoleID != null)
                    {
                        ShowButtonPanel();
                        ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('Transaction completed!')}</script>");
                    }
                    else
                    {
                        tbl_Search.Style["display"] = "none";
                        tbl_SearchResult.Style["display"] = "block";
                        ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('" + ActionStatus.Split(':')[1] + "')}</script>");
                    }
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    protected void btn_Delete_onClick(object sender, EventArgs e)
    {
        string ActionStatus = string.Empty;
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                if (ActionController.ValidateForm(this, "update role", new Hashtable()))
                {

                    txt_ActionCondn.Value = "2";
                    object RoleID = ActionController.ExecuteAction(this, "update role", ref ActionStatus);
                    if (RoleID != null)
                    {
                        ShowButtonPanel();
                        ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('Transaction completed!')}</script>");
                    }
                    else
                    {
                        tbl_Search.Style["display"] = "none";
                        tbl_SearchResult.Style["display"] = "block";
                        ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('"+ ActionStatus.Split(':')[1] + "')}</script>");
                    }
                }
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
            else
            {
                ShowAllRoles();
                string ActionStatus = string.Empty;
                DataTable Dt_Roles = (DataTable)ActionController.ExecuteAction((string)Session["User_ADID"], "Role.aspx", "select roles", ref ActionStatus, 1, "", (string)Session["User_ADID"]);
                if (Dt_Roles != null)
                {
                    dgViewAll.DataSource = Dt_Roles;
                    dgViewAll.DataBind();
                    Session["ResultSet"] = Dt_Roles;
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    protected void dgView_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
    }
        
    private void ShowButtonPanel()
    {
        try
        {
            tbl_ShowButtonPanel.Style["display"] = "block";
            tbl_AddNewRole.Style["display"] = "none";
            tbl_EditRoles.Style["display"] = "none";
            tbl_showViewAllPanel.Style["display"] = "none";
        }
        catch (Exception) { }
    }
    private void ShowAddNewRole()
    {
        try
        {
            tbl_ShowButtonPanel.Style["display"] = "none";
            tbl_AddNewRole.Style["display"] = "block";
            tbl_EditRoles.Style["display"] = "none";
            tbl_showViewAllPanel.Style["display"] = "none";
            txt_RoleName.Visible = true;
            txt_Edit_Role.Visible = false;
            txt_SearchPattern.Visible = false;
            txt_RoleName.Value = string.Empty;
            txt_RoleDesc.Value = string.Empty;
            txt_ActionDate.Value = DateTime.Now.ToShortDateString();
        }
        catch (Exception) { }
    }
    private void ShowSearchRole()
    {
        try
        {
            tbl_ShowButtonPanel.Style["display"] = "none";
            tbl_AddNewRole.Style["display"] = "none";
            tbl_EditRoles.Style["display"] = "block";
            tbl_showViewAllPanel.Style["display"] = "none";
            tbl_Search.Style["display"] = "block";
            tbl_SearchResult.Style["display"] = "none";
            div_DiaplayResultSetRoleInfo.Style["display"] = "none";
            txt_Edit_Role.Attributes.Remove("isMandatory");
            txt_RoleName.Visible = false;
            txt_SearchPattern.Visible = true;
            txt_Edit_Role.Visible = true;
            txt_Edit_Role.Value = string.Empty;
            txt_Edit_RoleDesc.Value = string.Empty;
            txt_CompanyID.Value = string.Empty;
            txt_SearchPattern.Value = string.Empty; 
        }
        catch (Exception) { }
    }
    private void ShowAllRoles()
    {
        try
        {
            tbl_ShowButtonPanel.Style["display"] = "none";
            tbl_AddNewRole.Style["display"] = "none";
            tbl_EditRoles.Style["display"] = "none";
            tbl_showViewAllPanel.Style["display"] = "block";
        }
        catch (Exception) { }
    } 
    private void HideValidator()
    {
        try
        {
            div_txt_RoleName.Visible = false;
            div_ddl_CompanyID.Visible = false;
            div_txt_Edit_Role.Visible = false;
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
                    if ((UserAccess.Length >= 1) && (UserAccess.Substring(0, 1).Equals("0"))) { btn_ViewAll.Visible = false; btn_ViewAll.Enabled = false; }
                    if ((UserAccess.Length >= 2) && (UserAccess.Substring(1, 1).Equals("0"))) { btn_AddNew.Visible = false; btn_AddNew.Enabled = false; }
                    if ((UserAccess.Length >= 3) && (UserAccess.Substring(2, 1).Equals("0"))) { btn_Edit.Visible = false; btn_Edit.Enabled = false; }
                    if ((UserAccess.Length >= 4) && (UserAccess.Substring(3, 1).Equals("0"))) { btn_Delete.Visible = false; btn_Delete.Enabled = false; }
               }
            }
        }
        catch (Exception) { }
    }
    
}
   