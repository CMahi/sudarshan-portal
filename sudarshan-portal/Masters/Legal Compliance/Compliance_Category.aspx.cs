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

public partial class Compliance_Category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Compliance_Category));
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                    txt_Cat_Name.Text = string.Empty;
                    showall();
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    #region Pageload


    private void showall()
    {
        try
        {
            StringBuilder str = new StringBuilder();
            string isdata = string.Empty;
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Compliance_Category.aspx", "psearch", ref isdata, 0, "", "", 4, "");
            if (DTS.Rows.Count > 0)
            {
                str.Append("<table style='width:60%' align='center' class='table table-bordered table-hover'>");
                str.Append("<thead>");
                str.Append("<tr class='grey'>");
                str.Append("<th>#</th><th>Category Name</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td>" + (i + 1) + "</td>");
                    str.Append("<td>" + DTS.Rows[i]["CATEGORY_NAME"].ToString() + "<input id='txt_cat_name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CATEGORY_NAME"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td><a href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='bindData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a>  <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_CMP_CAT_ID"].ToString() + "' style='display:none' /></td>");
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
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Compliance_Category.aspx", "psearch", ref isdata, 0, "", "", 5, SerachString);
            str.Append("<table style='width:60%' align='center' class='table table-bordered table-hover'>");
            str.Append("<thead>");
            str.Append("<tr class='grey'>");
            str.Append("<th>#</th><th>Category Name</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
            for (int i = 0; i < DTS.Rows.Count; i++)
            {
                str.Append(" <tr>");
                str.Append("<td>" + (i + 1) + "</td>");
                str.Append("<td>" + DTS.Rows[i]["CATEGORY_NAME"].ToString() + "<input id='txt_cat_name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CATEGORY_NAME"].ToString() + "' style='display:none' /></td>");
                str.Append("<td><a href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='bindData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a>  <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_CMP_CAT_ID"].ToString() + "' style='display:none' /></td>");
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
                string isSaved = (string)ActionController.ExecuteAction("", "Compliance_Category.aspx", "pamd", ref isdata, 0, txt_Cat_Name.Text, created_by, 1, "");
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Saved.');}</script>");
                    txt_Cat_Name.Text = "";
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
                string isSaved = (string)ActionController.ExecuteAction("", "Compliance_Category.aspx", "pamd", ref isdata, txt_PK_ID.Text, txt_Edit_Cat_Name.Text, created_by, 2, "");
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
                string isSaved = (string)ActionController.ExecuteAction("", "Compliance_Category.aspx", "pamd", ref isdata, txt_PK_ID.Text, txt_Edit_Cat_Name.Text, created_by, 3, "");
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
                txt_Cat_Name.Text = string.Empty;
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }
    #endregion

}