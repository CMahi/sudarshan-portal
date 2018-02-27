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
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

public partial class Pending_Payments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Pending_Payments));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        bindReport();
                    }
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    app_Authority.Text = HttpContext.Current.Request.Url.Authority;
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void bindReport()
    {
       // int ddl = Convert.ToInt32(ddlRecords.SelectedItem.Text);
        string html = getUserData("", 1, 0);
        ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true); 
        div_ReportDetails.InnerHtml = Convert.ToString(html);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=4");

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected string getUserData(string name, int pageno, int rpp)
    {
        string strData = string.Empty;
        string data = string.Empty;
        StringBuilder html = new StringBuilder();
        try
        {
            GetData getData = new GetData();
            string username = Convert.ToString(Session["USER_ADID"]);
            html.Append("<table id='data-table1' class='table table-bordered table-hover' >");
            html.Append("<thead><tr class='grey center'><th>#</th><th>Material Code</th><th>Material Text</th><th>Po No</th><th>Po Date</th><th>GR acceptance Date</th><th>Plant</th><th>Payment Terms</th><th>Invoice No</th></tr></thead>");
            html.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "Material_Transit_Report.aspx", "getpendingpayments", ref data, username, name);
            if (dtData != null)
            {
                int ddl = rpp;
                //int from = (pageno - 1) * ddl;
                //int to = ((pageno - 1) * ddl) + ddl;
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    //if (i < dtData.Rows.Count)
                    //{
                        CryptoGraphy crypt = new CryptoGraphy();
                        string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtData.Rows[i]["PO_Number"]));
                        html.Append("<tr>");
                        html.Append("<td>" + (i + 1) + "</td>");
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["Material_no"]) + "</td>");
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["material_desc"]) + "</td>");
                        html.Append("<td><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dtData.Rows[i]["PO_Number"]) + "</a><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["po_date"]) + "</td>");
                        html.Append("<td align='center'>" + Convert.ToString(dtData.Rows[i]["budat"]) + "</td>");
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["plant"]) + "</td>");
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["payment_terms"]) + "</td>");
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["invoice_no"]) + "</td>");
                        html.Append("</tr>");

                   // }
                }
            }

            html.Append("</tbody>");
            html.Append("</table>");

            //double cnt = Convert.ToDouble(dtData.Rows.Count) / rpp;
            //if (cnt > Convert.ToInt16(Convert.ToInt32(dtData.Rows.Count) / rpp))
            //{
            //    cnt = (int)cnt + 1;
            //}

            //if (cnt > 1)
            //{
            //    html.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
            //    html.Append("<ul class='pagination'>");
            //    for (int j = 1; j <= cnt; j++)
            //    {
            //        html.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + rpp + ")'></li>");
            //    }
            //    html.Append("</ul></div>");
            //}
            strData = Convert.ToString(html);

        }
        catch (Exception ex)
        {

        }
        return strData;
    }

    protected void ddlRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindReport();
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillSearch(string name, int rpp)
    {
        string html = getUserData(name, 1, rpp);
        return html;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillGoToPage1(string name, int pageno, int rpp)
    {
        string html = getUserData(name, pageno, rpp);
        return html;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string GetCurrentTime(int name)
    {
        string data = string.Empty;
        try
        {
            GetData getData = new GetData();
            data = getData.get_Dispatch_Details(name);
        }
        catch (Exception ex)
        {

        }
        return data;
    }



    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string GetFileNames(int name)
    {
        string filename = string.Empty;
        string isdata = string.Empty;
        try
        {
            filename = (string)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getname", ref isdata, name);
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return filename;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string viewVendor(string vcode)
    {
        string data = string.Empty;
        try
        {
            GetData getData = new GetData();
            data = getData.viewVendor(vcode);
        }
        catch (Exception ex)
        {

        }
        return data;
    }
}