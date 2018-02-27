﻿using System;
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

public partial class Early_Payment_Mapping_Report : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Early_Payment_Mapping_Report));
                    if (!Page.IsPostBack)
                    {
                        if (Session["USER_ADID"] != null)
                        {
                            txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                            txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                           
                        }
                        app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                        app_Authority.Text = HttpContext.Current.Request.Url.Authority;
                        fillddl_Vendor();
                    }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    private void fillddl_Vendor()
    {
        string isdata1 = string.Empty;
        ListItem li = new ListItem("--Select One--", "");
        DataSet ds = (DataSet)ActionController.ExecuteAction("", "User_Plant_Mapping_Report.aspx", "getvendor", ref isdata1);
        DataTable dl = ds.Tables[0];
  
        ddl_Vendor.DataSource = dl;
        ddl_Vendor.DataTextField = "Vendor_Name";
        ddl_Vendor.DataValueField = "Vendor_Name";
        ddl_Vendor.DataBind();
        ddl_Vendor.Items.Insert(0, li);
    }

    protected void bindReport()
    {
       // int ddl = Convert.ToInt32(ddlRecords.SelectedItem.Text);
        string html = getUserData("", 1, 0);
        //txtdata.Text = html.ToString();
        ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({'bSort': false})", true); 
        div_ReportDetails.InnerHtml = Convert.ToString(html);
        
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        bindReport();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txt_f_date.Value = "";
        txt_t_date.Value = "";
        div_ReportDetails.InnerHtml = "";
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
        string data = string.Empty;
        try
        {
            GetData getData = new GetData();

            string username = Convert.ToString(Session["USER_ADID"]);
            data = getData.get_Early_Payment_Report_Details(username, name, pageno, rpp,txt_f_date.Value, txt_t_date.Value,ddl_Vendor.SelectedValue);
        }
        catch (Exception ex)
        { 
        
        }
        return data;
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

    protected void btn_Export_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            StringBuilder str = new StringBuilder();
            ExportToExcel(str, "Early Payment Mapping Report");
        }
    }
    protected void ExportToExcel(StringBuilder dgview, string filename)
    {
        try
        {
            string dt = string.Empty;
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            string isdata = string.Empty;
            StringBuilder html = new StringBuilder();
            html.Append("<table id='data-table1' runat='server' class='table table-bordered table-hover'>");
            html.Append("<thead><tr class='grey center'><th>PO No</th><th>Vendor Name</th><th>Unique No</th><th>Dispatch Note</th><th>Invoice No</th><th>Invoice Amt</th><th>Dispatch Quantity</th><th>Status</th><th>Date</th></tr></thead>");
            html.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "User_Plant_Mapping_Report.aspx", "getearlypaymentrpt", ref isdata, txt_Username.Text, "", txt_f_date.Value, txt_t_date.Value,ddl_Vendor.SelectedValue);
            if (dtData != null)
            {
     
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    
                    html.Append("<tr><td>" + Convert.ToString(dtData.Rows[i]["PO_Number"]) + "</td>" +
                        "<td>" + Convert.ToString(dtData.Rows[i]["Vendor_Name"]) + "</td> " +
                        "<td>" + Convert.ToString(dtData.Rows[i]["Unique_No"]) + "</td> " +
                        "<td>" + Convert.ToString(dtData.Rows[i]["request_id"]) + "</td>" +
                        "<td>" + Convert.ToString(dtData.Rows[i]["Invoice_No"]) + "</td>" +
                        "<td style='text-align: right;'>" + Convert.ToString(dtData.Rows[i]["Invoice_Amount"]) + "</td>" +
                        "<td>" + Convert.ToString(dtData.Rows[i]["Dispatch_Quantity"]) + "</td>" +
                        "<td>" + Convert.ToString(dtData.Rows[i]["Status"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["modified_date"]) + "</td></tr>");
                }
                //}
            }

            html.Append("</tbody>");
            html.Append("</table>");
            txtdata.Text = html.ToString();
            dt=txtdata.Text;
            if (dt != null)
           {
               Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                  "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
               Response.Write(dt.ToString());
           }
            Response.End();
        }

        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');</script>");
            return;
        }
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