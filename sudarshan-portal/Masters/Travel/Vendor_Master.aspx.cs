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

public partial class Vendor_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Vendor_Master));
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                    clearControls();
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
                clearControls();
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
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
                string isSaved = (string)ActionController.ExecuteAction("", "Vendor_Master.aspx", "pamd", ref isdata, 0, vendor_code.Text, vendor_name.Text, created_by, 1);
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('New Vendor Has Been Inserted...!');}</script>");
                    clearControls();
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + isSaved + "');}</script>");
                }


            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void clearControls()
    {
        vendor_code.Text = vendor_name.Text = edit_vendor_code.Text = edit_vendor_name.Text = string.Empty;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string bindCityData()
    {
        StringBuilder html = new StringBuilder();
        try
        {
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Master.aspx", "psearch", ref isdata, 0, "", "", "", 4);
            html.Append("<table class='table table-bordered table-hover' id='data-table1'>");
            html.Append("<thead>");
            html.Append("<tr class='grey'>");
            html.Append("<th>#</th><th>Vendor Code</th><th>Vendor Name</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html.Append("<tr><td><input type='text' id='pk_city_id" + (i + 1) + "' value=" + Convert.ToString(dt.Rows[i]["PK_ADVANCE_F_VENDOR_ID"]) + " style='display:none'>" + (i + 1) + "</td><td>" + Convert.ToString(dt.Rows[i]["VENDOR_CODE"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["VENDOR_NAME"]) + "</td><td><a  href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='getCityData(" + Convert.ToString(dt.Rows[i]["PK_ADVANCE_F_VENDOR_ID"]) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a></td><td><a  href='#DeletePanel' role='button' data-toggle='modal' Title='Delete' onclick='deleteCity(" + Convert.ToString(dt.Rows[i]["PK_ADVANCE_F_VENDOR_ID"]) + ")'><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td></tr>");
                }
            }
            html.Append("</tbody></table>");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(html);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getCityData(int city_id)
    {
        StringBuilder html = new StringBuilder();
        try
        {
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Master.aspx", "psearch", ref isdata, city_id, "", "", "", 5);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    html.Append(Convert.ToString(dt.Rows[0]["Vendor_Code"]) + "||" + Convert.ToString(dt.Rows[0]["Vendor_Name"]));
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(html);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string updateCityData(int pk_id, string vendor_code, string vendor_name, int condition)
    {
        string isUpdated = string.Empty;
        string isdata = string.Empty;
        try
        {
            isUpdated = (string)ActionController.ExecuteAction("", "Vendor_Master.aspx", "pamd", ref isdata, pk_id, vendor_code, vendor_name, "", condition);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return isUpdated;
    }
}