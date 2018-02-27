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

public partial class LC_Doc_Name : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(LC_Doc_Name));
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
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
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "LC_Doc_Name.aspx", "psearch", ref isdata, "0", "0", "0", "", "", 0,"");
            if (dt != null)
            {
                ddl_Add_Cat.DataSource = dt;
                ddl_Add_Cat.DataTextField = "CATEGORY_NAME";
                ddl_Add_Cat.DataValueField = "PK_CMP_CAT_ID";
                ddl_Add_Cat.DataBind();
                ddl_Add_Cat.Items.Insert(0, Li);

                ddl_Edit_Cat.DataSource = dt;
                ddl_Edit_Cat.DataTextField = "CATEGORY_NAME";
                ddl_Edit_Cat.DataValueField = "PK_CMP_CAT_ID";
                ddl_Edit_Cat.DataBind();
                ddl_Edit_Cat.Items.Insert(0, Li);
            }
            DataTable Cmp_Dt = (DataTable)ActionController.ExecuteAction("", "LC_Doc_Name.aspx", "psearch", ref isdata, "0", "0", "0", "", "", 8, "");
            if (Cmp_Dt != null)
            {
                ddl_Edit_Cmp.DataSource = Cmp_Dt;
                ddl_Edit_Cmp.DataTextField = "CMPL_TASK_NAME";
                ddl_Edit_Cmp.DataValueField = "PK_CMPL_TASK_ID";
                ddl_Edit_Cmp.DataBind();
                ddl_Edit_Cmp.Items.Insert(0, Li);
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
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "LC_Doc_Name.aspx", "psearch", ref isdata, "0", "0", "0", "", "", 6, "");
            if (DTS.Rows.Count > 0)
            {
                str.Append("<table  align='center' class='table table-bordered table-hover'>");
                str.Append("<thead>");
                str.Append("<tr class='grey'>");
                str.Append("<th>#</th><th>Category Name</th><th>Compliance Name</th><th>Document Name</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td>" + (i + 1) + "</td>");
                    str.Append("<td>" + DTS.Rows[i]["CATEGORY_NAME"].ToString() + "<input id='txt_Cat_id" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FK_CMP_TYPE_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["CMPL_TASK_NAME"].ToString() + "<input id='txt_Cmp_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FK_COMPLIANCE_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["DOC_NAME"].ToString() + "<input id='txt_Doc_Name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["DOC_NAME"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td><a href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='bindData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a>  <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_DOC_ID"].ToString() + "' style='display:none' /></td>");
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
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "LC_Doc_Name.aspx", "psearch", ref isdata, "0", "0", "0", "", "", 7, SerachString);
            str.Append("<table align='center' class='table table-bordered table-hover'>");
            str.Append("<thead>");
            str.Append("<tr class='grey'>");
            str.Append("<th>#</th><th>Category Name</th><th>Compliance Name</th><th>Document Name</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
            for (int i = 0; i < DTS.Rows.Count; i++)
            {
                str.Append(" <tr>");
                str.Append("<td>" + (i + 1) + "</td>");
                str.Append("<td>" + DTS.Rows[i]["CATEGORY_NAME"].ToString() + "<input id='txt_Cat_id" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FK_CMP_TYPE_ID"].ToString() + "' style='display:none' /></td>");
                str.Append("<td>" + DTS.Rows[i]["CMPL_TASK_NAME"].ToString() + "<input id='txt_Cmp_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FK_COMPLIANCE_ID"].ToString() + "' style='display:none' /></td>");
                str.Append("<td>" + DTS.Rows[i]["DOC_NAME"].ToString() + "<input id='txt_Doc_Name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["DOC_NAME"].ToString() + "' style='display:none' /></td>");
                str.Append("<td><a href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='bindData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a>  <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_DOC_ID"].ToString() + "' style='display:none' /></td>");
                str.Append("<td><a href='#DeletePanel'  role='button' data-toggle='modal' Title='Delete' onclick='deleteCity(" + (i + 1) + ")'><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td>");
                str.Append("</tr>");
            }
            str.Append("   </tbody>   </table> ");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(str);
    }
    [System.Web.Services.WebMethod]
    public static string[] getcompliance(string FK_CompType_ID)
    {
        string[] ResultData = null;
        string DisplayData = string.Empty;
        string isValid = string.Empty;

        ResultData = new string[5];
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "LC_Person_Mapping.aspx", "psearch", ref isValid, "0", Convert.ToInt32(FK_CompType_ID), "0", "0", "", "", 1, "");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DisplayData = dt.Rows[i][0].ToString() + "||" + dt.Rows[i][1].ToString() + "," + DisplayData;
                }

            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        ResultData[0] = DisplayData;
        return ResultData;
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
                string isSaved = (string)ActionController.ExecuteAction("", "LC_Doc_Name.aspx", "pamd", ref isdata, 0,ddl_Add_Cat.SelectedValue,txt_Add_Cmp_ID.Text,txt_Add_Doc_Name.Text, created_by, 3, "");
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Saved.');}</script>");
                    ddl_Add_Cat.SelectedIndex = 0;
                    txt_Add_Doc_Name.Text = "";
                    showall();
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + isSaved + "');}</script>");
                    ddl_Add_Cat.SelectedIndex = 0;
                    txt_Add_Doc_Name.Text = "";
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
                string isSaved = (string)ActionController.ExecuteAction("", "LC_Doc_Name.aspx", "pamd", ref isdata, txt_PK_ID.Text,ddl_Edit_Cat.SelectedValue, txt_Edit_Cmp_ID.Text,txt_Edit_Doc.Text, created_by, 4, "");
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Updated.');}</script>");
                    ddl_Edit_Cat.SelectedIndex = 0;
                    txt_Edit_Doc.Text = "";
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
                string isSaved = (string)ActionController.ExecuteAction("", "LC_Doc_Name.aspx", "pamd", ref isdata, txt_PK_ID.Text, 0, 0, 0, created_by, 5, "");
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

            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }
    #endregion

}