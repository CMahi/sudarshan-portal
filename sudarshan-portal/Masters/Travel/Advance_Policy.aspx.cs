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

public partial class Advance_Policy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Policy));
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                    bindCountry();
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

    protected void bindCountry()
    {
        try
        {
            string isdata = string.Empty;
            int condition = 4;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "country.aspx", "psearch", ref isdata, "0", "", "", "", "", condition, "");
            ddlCountry.Items.Clear();
            ddlEditCountry.Items.Clear();
            if (dt != null)
            {
                ddlCountry.DataSource = dt;
                ddlCountry.DataTextField = "COUNTRY_NAME";
                ddlCountry.DataValueField = "PK_COUNTRY_ID";
                ddlCountry.DataBind();

                ddlEditCountry.DataSource = dt;
                ddlEditCountry.DataTextField = "COUNTRY_NAME";
                ddlEditCountry.DataValueField = "PK_COUNTRY_ID";
                ddlEditCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, "---Select One---");
            ddlEditCountry.Items.Insert(0, "---Select One---");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
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
                int ctype = Convert.ToInt16(ddlCountry.SelectedValue);
                string created_by = txt_Username.Text.Trim();
                string isSaved = (string)ActionController.ExecuteAction("", "City.aspx", "pamd", ref isdata, 0, txt_City_Name.Text, int.Parse(ddlCountry.SelectedValue.ToString()), created_by, 1, "");
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('New City Has Been Inserted...!');}</script>");
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
        ddlCountry.SelectedIndex = ddlEditCountry.SelectedIndex = 0;
        txt_City_Name.Text = string.Empty;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string bindCityData()
    {
        StringBuilder html = new StringBuilder();
        try
        {
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "City.aspx", "psearch", ref isdata, 0, "", 0, "", 4, "");
            html.Append("<table class='table table-bordered table-hover'>");
            html.Append("<thead>");
            html.Append("<tr class='grey'>");
            html.Append("<th>#</th><th>City Name</th><th>Country</th><th colspan='2'>Action</th></tr></thead><tbody>");
            if (dt != null)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html.Append("<tr><td><input type='text' id='pk_city_id" + (i + 1) + "' value=" + Convert.ToString(dt.Rows[i]["PK_CITY_ID"]) + " style='display:none'>" + (i + 1) + "</td><td>" + Convert.ToString(dt.Rows[i]["Name"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["Country_Name"]) + "</td><td><a  href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='getCityData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a></td><td><a  href='#DeletePanel' role='button' data-toggle='modal' Title='Delete' onclick='deleteCity(" + (i + 1) + ")'><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td></tr>");
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
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "City.aspx", "psearch", ref isdata, city_id, "", 0, "", 5, "");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    html.Append(Convert.ToString(dt.Rows[0]["Name"]) + "||" + Convert.ToString(dt.Rows[0]["fk_country_id"]));
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(html);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string updateCityData(int pk_id, string cname, int ctype, int condition)
    {
        string isUpdated = string.Empty;
        string isdata = string.Empty;
        try
        {
            isUpdated = (string)ActionController.ExecuteAction("", "City.aspx", "pamd", ref isdata, pk_id, cname, ctype, "", condition, "");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return isUpdated;
    }
}