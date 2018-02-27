using System;
using System.Web;
using System.Web.UI;
using FSL.Controller;
using AjaxPro;
using FSL.Logging;
using FSL.Message;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Configuration;
using WFE;
using InfoSoftGlobal;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

public partial class Government_Authority : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Government_Authority));
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                    txt_Authority_Name.Text = string.Empty;
                    showall();
                    fillState();
                  
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    #region Pageload
    private void fillState()
    {
        try
        {
            ListItem Li = new ListItem("--Select One--", "");
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Government_Authority.aspx", "psearch", ref isdata, "0", "0","", "", 0, "");
            if (dt != null)
            {
                ddl_State.DataSource = dt;
                ddl_State.DataTextField = "STATE_NAME";
                ddl_State.DataValueField = "PK_STATE_ID";
                ddl_State.DataBind();
                ddl_State.Items.Insert(0, Li);

                ddl_Edit_State.DataSource = dt;
                ddl_Edit_State.DataTextField = "STATE_NAME";
                ddl_Edit_State.DataValueField = "PK_STATE_ID";
                ddl_Edit_State.DataBind();
                ddl_Edit_State.Items.Insert(0, Li);
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    private void showall()
    {
        try
        {
            StringBuilder str = new StringBuilder();
            string isdata = string.Empty;
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Government_Authority.aspx", "psearch", ref isdata, 0, 0, "","", 4, "");
            if (DTS.Rows.Count > 0)
            {
                str.Append("<table style='width:60%' align='center' class='table table-bordered table-hover'>");
                str.Append("<thead>");
                str.Append("<tr class='grey'>");
                str.Append("<th>#</th><th>State Name</th><th>Authority Name</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td>" + (i + 1) + "</td>");
                    str.Append("<td>" + DTS.Rows[i]["STATE_NAME"].ToString() + "<input id='txt_state_name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["STATE_NAME"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["GOV_AUTHORITY_NAME"].ToString() + "<input id='txt_authority_name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["GOV_AUTHORITY_NAME"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td><a href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='bindData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a>  <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_AUTHORITY_ID"].ToString() + "' style='display:none' /><input id='txt_FkState_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_STATE_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td><a href='#DeletePanel'  role='button' data-toggle='modal' Title='Delete' onclick='deleteCity(" + (i + 1) + ")'><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td>");
                    str.Append("</tr>");
                }
                str.Append("   </tbody>   </table> ");
                div_Details.InnerHtml = str.ToString();
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Data Found !!')}</script>");
                div_Details.InnerHtml = null;
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

    }
    #endregion

    #region Ajax
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string bindData(string Condition, string SerachString)
    {
        StringBuilder str = new StringBuilder();
        try
        {
            string isdata = string.Empty;
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Government_Authority.aspx", "psearch", ref isdata, 0, 0,"", "", 5, SerachString);
            str.Append("<table style='width:60%' align='center' class='table table-bordered table-hover'>");
            str.Append("<thead>");
            str.Append("<tr class='grey'>");
            str.Append("<th>#</th><th>State Name</th><th>Authority Name</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
            for (int i = 0; i < DTS.Rows.Count; i++)
            {
                str.Append(" <tr>");
                str.Append("<td>" + (i + 1) + "</td>");
                str.Append("<td>" + DTS.Rows[i]["STATE_NAME"].ToString() + "<input id='txt_state_name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["STATE_NAME"].ToString() + "' style='display:none' /></td>");
                str.Append("<td>" + DTS.Rows[i]["GOV_AUTHORITY_NAME"].ToString() + "<input id='txt_authority_name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["GOV_AUTHORITY_NAME"].ToString() + "' style='display:none' /></td>");
                str.Append("<td><a href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='bindData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a>  <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_AUTHORITY_ID"].ToString() + "' style='display:none' /><input id='txt_FkState_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_STATE_ID"].ToString() + "' style='display:none' /></td>");
                str.Append("<td><a href='#DeletePanel'  role='button' data-toggle='modal' Title='Delete' onclick='deleteCity(" + (i + 1) + ")'><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td>");
                str.Append("</tr>");
            }
            str.Append("   </tbody>   </table> ");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(str);
    }
    #endregion

    #region Events
    protected void btn_AddNew_Click(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                string isdata = string.Empty;
                string created_by = txt_Username.Text.Trim();
                string isSaved = (string)ActionController.ExecuteAction("", "Government_Authority.aspx", "pamd", ref isdata, 0,ddl_State.SelectedValue, txt_Authority_Name.Text, created_by, 1, "");
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Saved.');}</script>");
                    txt_Authority_Name.Text = "";
                    ddl_State.SelectedValue = "";
                    showall();
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + isSaved + "');}</script>");
                }


            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void btn_Edit_onClick(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                string isdata = string.Empty;
                string created_by = txt_Username.Text.Trim();
                string isSaved = (string)ActionController.ExecuteAction("", "Government_Authority.aspx", "pamd", ref isdata, txt_PK_ID.Text, ddl_Edit_State.SelectedValue,txt_Edit_Authority.Text,created_by, 2, "");
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Updated.');}</script>");
                    showall();
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + isSaved + "');}</script>");
                }


            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void btn_Delete_onClick(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                string isdata = string.Empty;
                string created_by = txt_Username.Text.Trim();
                string isSaved = (string)ActionController.ExecuteAction("", "Government_Authority.aspx", "pamd", ref isdata, txt_PK_ID.Text, "0","", created_by, 3, "");
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Deleted.');}</script>");
                    showall();
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + isSaved + "');}</script>");
                }


            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                txt_Authority_Name.Text = string.Empty;
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }
    #endregion

}