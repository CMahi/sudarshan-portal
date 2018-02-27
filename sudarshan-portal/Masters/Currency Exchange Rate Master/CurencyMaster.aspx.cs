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
using System.Web.Services;


public partial class CurencyMaster : System.Web.UI.Page
{
   
  ListItem li = new ListItem("--Select One--", "");
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(CurencyMaster));
            try
            {
                if (!Page.IsPostBack)
                {

                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                    getcurrency();
                    showall();
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }


    private void getcurrency()
    {
        string isdata = string.Empty;
        DataTable currency = (DataTable)ActionController.ExecuteAction("", "CurencyMaster.aspx", "select", ref isdata, "","","",4,"", 0);
        if (currency != null && currency.Rows.Count > 0)
        {

            dll_Currency.DataSource = currency;
            dll_Currency.DataTextField = "CURRENCY";
            dll_Currency.DataValueField = "CURRENCY";
            dll_Currency.DataBind();
            dll_Currency.Items.Insert(0, li);

            Edit_ddl_currency.DataSource = currency;
            Edit_ddl_currency.DataTextField = "CURRENCY";
            Edit_ddl_currency.DataValueField = "CURRENCY";
            Edit_ddl_currency.DataBind();
            Edit_ddl_currency.Items.Insert(0, li);
        }
    }

    private void showall()
    {
        try
        {
            StringBuilder str = new StringBuilder();
            string isdata = string.Empty;
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "CurencyMaster.aspx", "selectall", ref isdata, "", "", "", 3,"", 0);
            if (DTS.Rows.Count > 0)
            {
                str.Append("<table style='width:100%' align='center' class='table table-bordered table-hover'>");
                str.Append("<thead>");
                str.Append("<tr class='grey'>");
                str.Append("<th>#</th><th>Currency Name</th><th>Exchange Rate</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td>" + (i + 1) + "</td>");
                    str.Append("<td>" + DTS.Rows[i]["CURRENCY"].ToString() + "<input id='txt_cur_name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CURRENCY"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["EXCHANGE_RATE"].ToString() + "<input id='txt_rate" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["EXCHANGE_RATE"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td><a href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='bindData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a>  <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_CURN_CONV_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td><a href='#DeletePanel'  role='button' data-toggle='modal' Title='Delete' onclick='deleteCur(" + (i + 1) + ")'><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td>");
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
                string isSaved = (string)ActionController.ExecuteAction("", "CurencyMaster.aspx", "insert", ref isdata, dll_Currency.SelectedValue, txt_cur_rate.Text, created_by, 0,"", 0);
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Saved.');}</script>");
                    dll_Currency.SelectedIndex = 0;
                    txt_cur_rate.Text = "";
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
                string isSaved = (string)ActionController.ExecuteAction("", "CurencyMaster.aspx", "update", ref isdata, Edit_ddl_currency.SelectedValue, txt_edit_cur_rate.Text,"",1,"",txt_PK_ID.Text);
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Updated.');}</script>");
                    showall();
                    txt_search.Value = "";
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
                string isSaved = (string)ActionController.ExecuteAction("", "CurencyMaster.aspx", "update", ref isdata, "","","",2,"", txt_PK_ID.Text);
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
                dll_Currency.SelectedIndex = 0;
                txt_cur_rate.Text = "";
               
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    [WebMethod]
    public static string bindData(string subCat_Name)
    {
        StringBuilder str = new StringBuilder();
        try
        {
            string isdata = string.Empty;
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "CurencyMaster.aspx", "selectall", ref isdata, "", "", "", 5,subCat_Name, 0);
            str.Append("<table style='width:100%' align='center' class='table table-bordered table-hover'>");
            str.Append("<thead>");
            str.Append("<tr class='grey'>");
            str.Append("<th>#</th><th>Currency Name</th><th>Exchange Rate</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
            for (int i = 0; i < DTS.Rows.Count; i++)
            {
                str.Append(" <tr>");
                str.Append("<td>" + (i + 1) + "</td>");
                str.Append("<td>" + DTS.Rows[i]["CURRENCY"].ToString() + "<input id='txt_cur_name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CURRENCY"].ToString() + "' style='display:none' /></td>");
                str.Append("<td>" + DTS.Rows[i]["EXCHANGE_RATE"].ToString() + "<input id='txt_rate" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["EXCHANGE_RATE"].ToString() + "' style='display:none' /></td>");
                str.Append("<td><a href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='bindData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a>  <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_CURN_CONV_ID"].ToString() + "' style='display:none' /></td>");
                str.Append("<td><a href='#DeletePanel'  role='button' data-toggle='modal' Title='Delete' onclick='deleteCur(" + (i + 1) + ")'><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td>");
                str.Append("</tr>");
            }
            str.Append("   </tbody>   </table> ");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(str);
    }



}