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

public partial class Portal_Change_Password : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Portal_Change_Password));
                if (!IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        checkPassword(txt_Username.Text.Trim());
                    }
                    
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('HomePage.aspx','frmset_WorkArea');}</script>");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                string IsData = string.Empty;
                string result = (string)ActionController.ExecuteAction("", "Change_Password.aspx", "update", ref IsData, txt_Username.Text.Trim(), txt_Old.Text.Trim(), txt_New.Text.Trim());
                if (result == null || IsData.Length > 0 || result == "false")
                {
                    string[] errmsg = IsData.Split(':');
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Password Updated Successfully...!');window.open('HomePage.aspx','frmset_WorkArea');}</script>");
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    public void checkPassword(string username)
    {
        string DisplayData = string.Empty;
        if (ActionController.IsSessionExpired(Page, true))
        {

        }
        else
        {
            string isValid = string.Empty;
            if (!ActionController.IsSessionExpired(this, true))
            {
                try
                {
                    DisplayData = "false";
                    DataTable dt = (DataTable)ActionController.ExecuteAction("", "Edit_Profile.aspx", "getvendordetails", ref isValid, username);
                    if (dt.Rows.Count > 0)
                    {
                        txt_password.Text = Convert.ToString(dt.Rows[0]["password"]);
                    }

                }
                catch (Exception ex)
                {
                    FSL.Logging.Logger.WriteEventLog(false, ex);
                }
            }

        }
    }
}