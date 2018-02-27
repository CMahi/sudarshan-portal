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

public partial class UserSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ClientScript.RegisterStartupScript(typeof(Page), "Error Message", "<script language='javascript'>{ParentLogOut();}</script>");
            else
            {
                if (!IsPostBack)
                {
                    txt_ActionBy.Value = (string)Session["User_ADID"];
                    ActionController.SetControlAttributes(this);
                    txt_SearchPattern.Focus();
                }
                HideValidator();
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    protected void btn_Search_onClick(object sender, EventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (ActionController.ValidateForm(this, new Hashtable()))
                {
                    string ActionStatus = string.Empty;
                    DataTable Dt_Roles = (DataTable)ActionController.ExecuteAction(this, "select users", ref ActionStatus);
                    if (Dt_Roles != null)
                    {
                        Session["SearchResultSets"] = Dt_Roles;
                        dgSearchResult.DataSource = Dt_Roles;
                        dgSearchResult.DataBind();
                    }
                    else ClientScript.RegisterStartupScript(typeof(Page), "Alert Message", "<script language='javascript'>{alert('" + ActionStatus.Split(':')[1] + "')}</script>");

                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }    
    }
    protected void CreateLink(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataTable dtSearchDisplay = (DataTable)Session["SearchResultSets"];
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes.Add("onclick", "return setParent('" + dtSearchDisplay.Rows[e.Row.DataItemIndex][0].ToString() + "');");
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
                dgSearchResult.DataSource = (DataTable)Session["SearchResultSets"];
                dgSearchResult.DataBind();
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    private void   HideValidator()
    {
        try
        {
            div_txt_SearchPattern.Visible = false;
        }
        catch (Exception) { }
    }
}
