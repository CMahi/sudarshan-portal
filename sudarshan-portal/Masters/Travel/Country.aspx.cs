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

public partial class Country : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Country));
            try
            {
                if (!Page.IsPostBack)
                {
                    clearControls();
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
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
                int condition = 1;
                string ctype=ddlCountry_Type.SelectedItem.Text.Trim();
                string curr=ddlCurrency.SelectedItem.Text.Trim();
                string created_by=txt_Username.Text.Trim();
                string isSaved = (string)ActionController.ExecuteAction("", "Country.aspx", "pamd", ref isdata, "0" ,txt_Country_Name.Text.Trim(),ctype,curr,created_by,condition,"");
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('New Country Has Been Inserted...!');}</script>");
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
        ddlCountry_Type.SelectedIndex = ddlCurrency.SelectedIndex = 0;
        txt_Country_Name.Text = string.Empty;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string bindCountryData()
    {
        StringBuilder html = new StringBuilder();
        try
        {
            string isdata=string.Empty;
            int condition = 4;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Country.aspx", "psearch", ref isdata, "0", "", "", "", "", condition, "");
            html.Append("<table class='table table-bordered table-hover'>");
			html.Append("<thead>");
			html.Append("<tr class='grey'>");
			html.Append("<th>#</th><th>Country Name</th><th>Country Type</th><th>Currency</th><th colspan='2'>Action</th></tr></thead><tbody>");
            if (dt != null)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html.Append("<tr><td><input type='text' id='pk_country_id" + (i + 1) + "' value=" + Convert.ToString(dt.Rows[i]["PK_COUNTRY_ID"]) + " style='display:none'>" + (i + 1) + "</td><td>" + Convert.ToString(dt.Rows[i]["Country_Name"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["Country_Type"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["Currency"]) + "</td><td><a  href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='getCountryData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a></td><td><a  href='#DeletePanel' role='button' data-toggle='modal' Title='Delete' onclick='deleteCountry(" + (i + 1) + ")'><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td></tr>");	
                }
            }
            html.Append("</tbody></table>");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(html);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getCountryData(int Country_Id)
    {
        StringBuilder html = new StringBuilder();
        try
        {
            string isdata = string.Empty;
            int condition = 5;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Country.aspx", "psearch", ref isdata, Country_Id, "", "", "", "", condition, "");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    html.Append(Convert.ToString(dt.Rows[0]["Country_Name"]) + "||" + Convert.ToString(dt.Rows[0]["country_Type"]) + "||" + Convert.ToString(dt.Rows[0]["Currency"]));
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(html);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string updateCountryData(int pk_id, string cname,string ctype,string curr,int condition)
    {
        string isUpdated = string.Empty;
        string isdata = string.Empty;
        try
        {
            isUpdated = (string)ActionController.ExecuteAction("", "Country.aspx", "pamd", ref isdata, pk_id, cname.Trim(), ctype, curr, "", condition, "");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
         return isUpdated;
    }
}