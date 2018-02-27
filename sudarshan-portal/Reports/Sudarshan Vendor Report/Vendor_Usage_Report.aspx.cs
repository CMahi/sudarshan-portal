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

public partial class Vendor_Usage_Report : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    string IsData = string.Empty;
    StringBuilder txtTablesNames = new StringBuilder();
    double[] BudgetSavings = new double[13];
    double[] ActualSavings = new double[13];
    StringBuilder str = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            app_Path.Text = HttpContext.Current.Request.ApplicationPath;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Vendor_Usage_Report));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    txtCreatedBy.Text = ((string)Session["User_ADID"]);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string bindReport(string cdate, int number)
    {
        string strData = string.Empty;
        string data = string.Empty;
        StringBuilder html = new StringBuilder();
        if (number == 1)
        {
            string username = Convert.ToString(Session["USER_ADID"]);
            html.Append("<table class='table table-bordered table-hover' >");
            html.Append("<thead><tr class='grey center'><th>#</th><th>Vendor Code</th><th>PO Number</th><th>PO Date</th><th>PO Value</th><th>Gross Value</th><th>Acknowlegde Date</th></tr></thead>");
            html.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "Vendor_Usage_Report.aspx", "getacknowdata", ref data, cdate, 1);
            
            if (dtData != null)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    CryptoGraphy crypt = new CryptoGraphy();
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtData.Rows[i]["PO_NUMBER"]));
                    html.Append("<tr>");
                    html.Append("<td>" + (i + 1) + "</td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["VENDOR_CODE"]) + "</td>");
                    html.Append("<td><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dtData.Rows[i]["PO_NUMBER"]) + "</a><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["PO_Date"]) + "</td>");
                    html.Append("<td align='right'>" + Convert.ToString(dtData.Rows[i]["PO_VALUE"]) + "</td>");
                    html.Append("<td  align='right'>" + Convert.ToString(dtData.Rows[i]["PO_GV"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["ack_Date"]) + "</td>");
                    html.Append("</tr>");
                }
            }

            html.Append("</tbody>");
            html.Append("</table>");
        }
        else if (number == 2)
        {
            string username = Convert.ToString(Session["USER_ADID"]);
            html.Append("<table class='table table-bordered table-hover' >");
            html.Append("<thead><tr class='grey center'><th>#</th><th>Vendor Code</th><th>PO Number</th><th>Unique No</th><th>Date</th></tr></thead>");
            html.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "Vendor_Usage_Report.aspx", "getacknowdata", ref data, cdate, 2);
            if (dtData != null)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    CryptoGraphy crypt = new CryptoGraphy();
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtData.Rows[i]["PO_Number"]));
                    html.Append("<tr>");
                    html.Append("<td>" + (i + 1) + "</td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["Vendor_Code"]) + "</td>");
                    html.Append("<td><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dtData.Rows[i]["PO_Number"]) + "</a><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["Unique_No"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["dis_Date"]) + "</td>");
                    html.Append("</tr>");
                }
            }
            html.Append("</tbody>");
            html.Append("</table>");
        }
        else if (number == 3)
        {
            string username = Convert.ToString(Session["USER_ADID"]);
            html.Append("<table class='table table-bordered table-hover' >");
            html.Append("<thead><tr class='grey center'><th>#</th><th>Vendor Code</th><th>Date</th></tr></thead>");
            html.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "Vendor_Usage_Report.aspx", "getacknowdata", ref data, cdate, 3);
            if (dtData != null)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + (i + 1) + "</td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["USER_ADID"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["login_Date"]) + "</td>");
                    html.Append("</tr>");
                }
            }
            html.Append("</tbody>");
            html.Append("</table>");
        }
        strData = Convert.ToString(html);
        return strData;


    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                div_po.InnerHtml = null;
                string isValid = string.Empty;
                int diff = 0;
                if (txt_daydiff.Text != "")
                {
                    diff = Convert.ToInt16(txt_daydiff.Text);
                }
                string[] strdate = new string[diff];
                strdate[0] = txt_f_date.Value;
                str.Append("<table id='data-table1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th>Date</th><th>Acknowledged POs</th><th>Dispated POs</th><th>No. of Logged Vendors</th></tr>");   
                str.Append("</thead><tbody>");
               
                for (int i = 0; i < diff; i++)
                {                   
                    string isSaved = (string)ActionController.ExecuteAction("", "Vendor_Usage_Report.aspx", "selectdata", ref isValid, strdate[i]);
                    string[] Request_Unique = isSaved.Split('=');
                    if (isSaved != "")
                    {
                        str.Append("<tr>");                    
                        str.Append("<td>" + Convert.ToDateTime(Request_Unique[0]).ToString("dd-MMM-yyyy") + "</td>");
                        str.Append("<td class='acknow'><a href='#acknow' data-toggle='modal'>"+ Request_Unique[1] +"</a></td>");
                        str.Append("<td class='dispatch'><a href='#dispatch' data-toggle='modal'>" + Request_Unique[2] + "</a></td>");
                        str.Append("<td class='logindata'><a href='#login' data-toggle='modal'>" + Request_Unique[3] + "</a></td>");
                     }                   
                    DateTime dt = Convert.ToDateTime(strdate[i]);
                    dt = dt.AddDays(1);
                    if (i == diff-1)
                    {
                        strdate[i] = dt.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        strdate[i + 1] = dt.ToString("dd-MMM-yyyy");
                    }                   
                }
               
                str.Append("</tbody></table> ");
                div_po.InnerHtml = str.ToString();
                txtdata.Text = str.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
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
    protected void btn_Export_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcel(str, "Vendor Login Report");
        }
    }
    protected void btn_export_acknow_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelAck(div_acknow.InnerText, "Acknowlege PO's Details");
        }
    }
    protected void btn_export_dispatch_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDis(div_dispatch.InnerText, "Disaptch PO's Details");
        }
    }
    protected void btn_export_login_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelLogin(div_login.InnerText, "Login Details");
        }
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string GetCurrentTime(int name)
    {
        GetData getData = new GetData();
        string data = getData.get_Dispatch_Details(name);
        return data;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=" + 4);
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
    protected void ExportToExcelAck(string divack, string filename)
    {
        try
        {
            string data = string.Empty;
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringBuilder htmlack = new StringBuilder();
            string username = Convert.ToString(Session["USER_ADID"]);
            htmlack.Append("<table class='table table-bordered table-hover' >");
            htmlack.Append("<thead><tr class='grey center'><th>#</th><th>Vendor Code</th><th>PO Number</th><th>PO Date</th><th>PO Value</th><th>Gross Value</th><th>Acknowlegde Date</th></tr></thead>");
            htmlack.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "Vendor_Usage_Report.aspx", "getacknowdata", ref data, txt_dateac.Text, 1);

            if (dtData != null)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {

                    htmlack.Append("<tr>");
                    htmlack.Append("<td>" + (i + 1) + "</td>");
                    htmlack.Append("<td>" + Convert.ToString(dtData.Rows[i]["VENDOR_CODE"]) + "</td>");
                    htmlack.Append("<td>" + Convert.ToString(dtData.Rows[i]["PO_NUMBER"]) + "</td>");
                    htmlack.Append("<td>" + Convert.ToString(dtData.Rows[i]["PO_Date"]) + "</td>");
                    htmlack.Append("<td align='right'>" + Convert.ToString(dtData.Rows[i]["PO_VALUE"]) + "</td>");
                    htmlack.Append("<td  align='right'>" + Convert.ToString(dtData.Rows[i]["PO_GV"]) + "</td>");
                    htmlack.Append("<td>" + Convert.ToString(dtData.Rows[i]["ack_Date"]) + "</td>");
                    htmlack.Append("</tr>");
                }
            }

            htmlack.Append("</tbody>");
            htmlack.Append("</table>");
            txt_exp_ack_data.Text = htmlack.ToString();
            divack = txt_exp_ack_data.Text;
            if (divack != "")
            {
                Response.Write("<table border=\"1\"><tr><td colspan='8' align='center' style=\"font-weight:bold;font-size:'20';\">Acknowlege PO's Details</td></tr>");
                Response.Write("<tr style=\"font-weight:bold\"><td colspan='8'></td></tr>");
                Response.Write("<tr style=\"font-weight:bold\"><td colspan='7' align='center'>" + divack + "</td></tr>");
                Response.Write("</table>");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');</script>");
            return;
        }
    }
    protected void ExportToExcelDis(string divack, string filename)
    {
        try
        {
            string data = string.Empty;
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            string username = Convert.ToString(Session["USER_ADID"]);
            StringBuilder htdis = new StringBuilder();
            htdis.Append("<table class='table table-bordered table-hover' >");
            htdis.Append("<thead><tr class='grey center'><th>#</th><th>Vendor Code</th><th>PO Number</th><th>Unique No</th><th>Date</th></tr></thead>");
            htdis.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "Vendor_Usage_Report.aspx", "getacknowdata", ref data, txt_date.Text, 2);
            if (dtData != null)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    htdis.Append("<tr>");
                    htdis.Append("<td>" + (i + 1) + "</td>");
                    htdis.Append("<td>" + Convert.ToString(dtData.Rows[i]["Vendor_Code"]) + "</td>");
                    htdis.Append("<td>" + Convert.ToString(dtData.Rows[i]["PO_Number"]) + " </td>");
                    htdis.Append("<td>" + Convert.ToString(dtData.Rows[i]["Unique_No"]) + "</td>");
                    htdis.Append("<td>" + Convert.ToString(dtData.Rows[i]["dis_Date"]) + "</td>");
                    htdis.Append("</tr>");
                }
            }

            htdis.Append("</tbody>");
            htdis.Append("</table>");
            txt_exp_dis_data.Text=htdis.ToString();
            divack = txt_exp_dis_data.Text;
            if (divack != "")
            {
                Response.Write("<table border=\"1\"><tr><td colspan='8' align='center' style=\"font-weight:bold;font-size:'20';\">Dispatch PO's Details</td></tr>");
                Response.Write("<tr style=\"font-weight:bold\"><td colspan='8'></td></tr>");
                Response.Write("<tr style=\"font-weight:bold\"><td colspan='7' align='center'>" + divack + "</td></tr>");
                Response.Write("</table>");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');</script>");
            return;
        }
    }
    protected void ExportToExcelLogin(string divack, string filename)
    {
        try
        {
            string dt = string.Empty;
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            divack = txt_exp_login_data.Text;
            if (divack != "")
            {
                Response.Write("<table border=\"1\"><tr><td colspan='8' align='center' style=\"font-weight:bold;font-size:'20';\">Login Details</td></tr>");
                Response.Write("<tr style=\"font-weight:bold\"><td colspan='8'></td></tr>");
                Response.Write("<tr style=\"font-weight:bold\"><td colspan='7' align='center'>" + divack + "</td></tr>");
                Response.Write("</table>");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');</script>");
            return;
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
            string isValid = string.Empty;
            int diff = 0;
            if (txt_daydiff.Text != "")
            {
                diff = Convert.ToInt16(txt_daydiff.Text);
            }
            string[] strdate = new string[diff];
            strdate[0] = txt_f_date.Value;
            str.Append("<table id='data-table1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th>Date</th><th>Acknowledged POs</th><th>Dispated POs</th><th>No. of Logged Vendors</th></tr>");
            str.Append("</thead><tbody>");

            for (int i = 0; i < diff; i++)
            {
                string isSaved = (string)ActionController.ExecuteAction("", "Vendor_Usage_Report.aspx", "selectdata", ref isValid, strdate[i]);
                string[] Request_Unique = isSaved.Split('=');
                if (isSaved != "")
                {
                    str.Append("<tr>");
                    str.Append("<td>" + Convert.ToDateTime(Request_Unique[0]).ToString("dd-MMM-yyyy") + "</td>");
                    str.Append("<td >" + Request_Unique[1] + "</td>");
                    str.Append("<td>" + Request_Unique[2] + "</td>");
                    str.Append("<td >" + Request_Unique[3] + "</td>");
                }
                DateTime dt1 = Convert.ToDateTime(strdate[i]);
                dt1 = dt1.AddDays(1);
                if (i == diff - 1)
                {
                    strdate[i] = dt1.ToString("dd-MMM-yyyy");
                }
                else
                {
                    strdate[i + 1] = dt1.ToString("dd-MMM-yyyy");
                }
            }

            str.Append("</tbody></table> ");
            txtdata.Text = str.ToString();
            dt = txtdata.Text;
            if (dt!="")
            {
               Response.Write("<table border=\"1\"><tr><td colspan='8' align='center' style=\"font-weight:bold;font-size:'20';\">Vendor Login Report</td></tr>");
               Response.Write("<tr style=\"font-weight:bold\"><td colspan='8'></td></tr>");
               Response.Write("<tr style=\"font-weight:bold\"><td colspan='7' align='center'>" + dt + "</td></tr>");
               Response.Write("</table>");
            }
            Response.End();
        }
      catch (Exception ex)
      {      
         Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');</script>");                 
          return;
      }
    }

}