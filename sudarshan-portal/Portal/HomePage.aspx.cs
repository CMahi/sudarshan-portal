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

public partial class HomePage : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(HomePage));
                
                    if (!IsPostBack)
                    {
                        txt_UserName.Text = Convert.ToString(Session["USER_ADID"]);
                    }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
      
    public string RenderWIDetails(DataTable DT)
    {
        string str = "";
        return str;
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