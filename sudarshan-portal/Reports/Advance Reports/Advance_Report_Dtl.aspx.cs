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
using System.Web.Script.Services;

public partial class Advance_Report_Dtl : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Report_Dtl));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    app_Authority.Text = HttpContext.Current.Request.Url.Authority;

                    from_date.Attributes.Add("readonly", "readonly");
                    to_date.Attributes.Add("readonly", "readonly");
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Report_Master.aspx");

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {

        string fdate = Convert.ToDateTime(from_date.Text).ToString("dd-MMM-yyyy");
        string tdate = Convert.ToDateTime(to_date.Text).ToString("dd-MMM-yyyy");    

        DataTable dt = null;
        string isDBValid = string.Empty;

        dt = (DataTable)ActionController.ExecuteAction("", "Advance_Report_Dtl.aspx", "getreport", ref isDBValid, fdate, tdate);

        StringBuilder HTML = new StringBuilder();
        if (dt != null)
        {
            string isvalid = string.Empty;
            string parameter = string.Empty;
            HTML.Append("<table class='table table-bordered table-hover' ><thead>");
            HTML.Append("<tr class='grey'><th>Sr.No</th><th>Emp.Name</th><th>Emp.Code</th><th>Cost Center</th><th>Currency</th><th>Advance</th><th>Exchange Rate</th><th>Expense In FC</th><th>Expense In INR</th><th>Request No.</th><th>Created Date</th></tr>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HTML.Append("<tr style='text-align:left'><td style='text-align:right'>" + (i + 1) + "</td><td style='text-align:left'>" + dt.Rows[i]["EMPLOYEE_NAME"] + "</td><td style='text-align:left'>" + dt.Rows[i]["EMP_ID"] + "</td><td style='text-align:left'>" + dt.Rows[i]["costcenter"] + "</td><td style='text-align:left'>" + dt.Rows[i]["CURRENCY"] + "</td><td style='text-align:right'>" + dt.Rows[i]["ADVANCE"] + "</td><td style='text-align:right'>" + dt.Rows[i]["EXCHANGE_RATE"] + "</td><td style='text-align:right'>" + dt.Rows[i]["EXPENSE_AMOUNT"] + "</td><td style='text-align:right'>" + dt.Rows[i]["INDIAN_CURRENCY"] + "</td><td style='text-align:left'>" + dt.Rows[i]["request_no"] + "</td><td style='text-align:left'>" + dt.Rows[i]["CREATED_DATE"] + "</td></tr></thead>");
            }

            HTML.Append("</table>");
            div_ReportDetails.InnerHtml = Convert.ToString(HTML);
        }
        else
        {
            div_ReportDetails.InnerHtml = "";
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No record found.')}</script>");
        }
       
    }

    //[AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    //public string fillSearch(string ccode, string fdate, string tdate, string noteline, string name, int rpp)
    //{
    //    string html = bindReport(ccode, fdate, tdate, noteline,name, 1, rpp);
    //    return html;
    //}

    //[AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    //public string fillGoToPage1(string ccode, string fdate, string tdate, string noteline, string name, int pageno, int rpp)
    //{
    //    string html = bindReport(ccode, fdate, tdate, noteline, name, pageno, rpp);
    //    return html;
    //}

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            if (div_ReportDetails.InnerHtml != "")
            {
                ExportToExcel("Advance Requisition Report");
            }
            else

                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No data found to export.');}</script>");

          
        }
    }
    protected void ExportToExcel(string filename)
    {
        try
        {
            string isDBValid = string.Empty;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string isValid = string.Empty;
            string expparam = string.Empty;
           
            string fdate = Convert.ToDateTime(from_date.Text).ToString("dd-MMM-yyyy");
            string tdate = Convert.ToDateTime(to_date.Text).ToString("dd-MMM-yyyy");

            dt = (DataTable)ActionController.ExecuteAction("", "Advance_Report_Dtl.aspx", "getreport", ref isDBValid, fdate, tdate);
            // Session["assetdashboardrpt"] = tbl;
            Session["assetdashboardrpt"] = dt;
            if (Session["assetdashboardrpt"] != null)
            {
                string attachment = "attachment; filename=" + filename + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                dt = (DataTable)Session["assetdashboardrpt"];
                int deci = 0;
                if (dt.Rows.Count > 0)
                {
                    string isvalid = string.Empty;
                    string parameter = string.Empty;
                    Response.Write("<table class='table table-bordred table-striped' border='1' cellspacing='0' width='100%' cellpadding='4' border='0' id='tbl_itmdtl' style='color: #333333; border-collapse: collapse;'>");
                    Response.Write("<tr style='text-align:center'><th scope='col' style='width: 1%; background: #424242 url(../Images/grd_head.png) repeat-x top;border: solid 1px #525252; color: Silver; font-size: 12px;'>SR.NO</th><th scope='col' style='width: 4%; background: #424242 url(../Images/grd_head.png) repeat-x top;border: solid 1px #525252; color: Silver; font-size: 12px;'>Employee Name</th> <th scope='col' style='width: 3%; background: #424242 url(../Images/grd_head.png) repeat-x top;border: solid 1px #525252; color: Silver; font-size: 12px;'>Cost Center</th><th scope='col' style='width: 3%; background: #424242 url(../Images/grd_head.png) repeat-x top;border: solid 1px #525252; color: Silver; font-size: 12px;'>Currency</th><th scope='col' style='width: 3%; background: #424242 url(../Images/grd_head.png) repeat-x top;border: solid 1px #525252; color: Silver; font-size: 12px;'>Advance</th><th scope='col' style='width: 3%; background: #424242 url(../Images/grd_head.png) repeat-x top;border: solid 1px #525252; color: Silver; font-size: 12px;'>Exchange Rate</th><th scope='col' style='width: 3%; background: #424242 url(../Images/grd_head.png) repeat-x top;border: solid 1px #525252; color: Silver; font-size: 12px;'>Expense Amount</th><th scope='col' style='width: 3%; background: #424242 url(../Images/grd_head.png) repeat-x top;border: solid 1px #525252; color: Silver; font-size: 12px;'>Indian Currency</th><th scope='col' style='width: 3%; background: #424242 url(../Images/grd_head.png) repeat-x top;border: solid 1px #525252; color: Silver; font-size: 12px;'>Request No.</th><th scope='col' style='width: 3%; background: #424242 url(../Images/grd_head.png) repeat-x top;border: solid 1px #525252; color: Silver; font-size: 12px;'>Created Date</th></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Response.Write("<tr style='text-align:left'><td style='text-align:right'>" + (i + 1) + "</td><td style='text-align:left'>" + dt.Rows[i]["EMPLOYEE_NAME"] + "</td><td style='text-align:left'>" + dt.Rows[i]["costcenter"] + "</td><td style='text-align:left'>" + dt.Rows[i]["CURRENCY"] + "</td><td style='text-align:left'>" + dt.Rows[i]["ADVANCE"] + "</td><td style='text-align:left'>" + dt.Rows[i]["EXCHANGE_RATE"] + "</td><td style='text-align:left'>" + dt.Rows[i]["EXPENSE_AMOUNT"] + "</td><td style='text-align:left'>" + dt.Rows[i]["INDIAN_CURRENCY"] + "</td><td style='text-align:left'>" + dt.Rows[i]["request_no"] + "</td><td style='text-align:left'>" + dt.Rows[i]["CREATED_DATE"] + "</td></tr>");

                    }

                    Response.Write("</table>");
                }
                Response.Flush();
                Response.SuppressContent = true;
                Response.End();
            }

            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Record Found.')}</script>");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Error found while exporting in excel.')}</script>");
            return;
        }

    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            from_date.Text = "";
            to_date.Text = "";
            div_ReportDetails.InnerHtml = "";

        }
    }

}