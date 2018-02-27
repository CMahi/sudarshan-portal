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

public partial class Currency_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Currency_Master));
            try
            {
                if (!Page.IsPostBack)
                {

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

    

    protected void btn_AddNew_Click(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                divIns.Style.Add("display", "none");
                string isdata = string.Empty;
                string isSaved = (string)ActionController.ExecuteAction("", "Currency_Master.aspx", "pinsdata", ref isdata, 2,0, currency_name.Text.Trim(), exc_rate.Text.Trim());
                //string isSaved = "true";
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Currency Added Successfully...!'); clearControls();}</script>");
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + isSaved + "');}</script>");

                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string bindCountryData()
    {
        StringBuilder html = new StringBuilder();
        try
        {
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Currency_Master.aspx", "pgetdata", ref isdata, 0,0, "", 0);
            html.Append("<table id='data-table1' class='table table-bordered'>");
            html.Append("<thead>");
            html.Append("<tr class='grey'>");
            html.Append("<th>#</th><th>Currency</th><th>Exchange Rate In INR</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
            if (dt != null)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html.Append("<tr><td><input type='text' id='pk_id" + (i + 1) + "' value=" + Convert.ToString(dt.Rows[i]["PK_CURRENCY_ID"]) + " style='display:none'>" + (i + 1) + "</td><td>" + Convert.ToString(dt.Rows[i]["CURRENCY_NAME"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["EXC_RATE_IN_INR"]) + "</td><td><a href='#UpdRepPanel' role='button' data-toggle='modal' Title='Update' onclick=getReportingData('" + Convert.ToString(dt.Rows[i]["PK_CURRENCY_ID"]) + "')><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a></td><td><a href='#DeletePanel' role='button' data-toggle='modal' Title='Delete' onclick=deleteReporting('" + Convert.ToString(dt.Rows[i]["PK_CURRENCY_ID"]) + "')><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td></tr>");
                }
            }
            html.Append("</tbody></table>");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        //ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({'bSort': false,'pageLength':100});", true);
        return Convert.ToString(html);

    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string updateReportingData(string currency, string exc_rate, int param,int pk_id)
    {
        string isUpdated = string.Empty;
        string isdata = string.Empty;
        try
        {
            isUpdated = (string)ActionController.ExecuteAction("", "Currency_Master.aspx", "pinsdata", ref isdata, param, pk_id, currency, exc_rate);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return isUpdated;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string[] getReporting(string pk_id, int param)
    {
        string isdata = "";
        string[] ret_data = new string[4];
        ret_data[0] = "0";
        ret_data[1] = ret_data[2] = ret_data[3] = "";

        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Currency_Master.aspx", "pgetdata", ref isdata, param, pk_id, "",0);
            if (dt != null && dt.Rows.Count > 0)
            {
                ret_data[0] = Convert.ToString(dt.Rows.Count);
                ret_data[1] = Convert.ToString(dt.Rows[0]["CURRENCY_NAME"]);
                ret_data[2] = Convert.ToString(dt.Rows[0]["EXC_RATE_IN_INR"]);
                ret_data[3] = Convert.ToString(dt.Rows[0]["PK_CURRENCY_ID"]);
            }
        }
        catch (Exception Exc)
        {
            Logger.WriteEventLog(false, Exc);
            ret_data[0] = "0";
            ret_data[1] = ret_data[2] = ret_data[3] = "";
        }
        return ret_data;
    }

}