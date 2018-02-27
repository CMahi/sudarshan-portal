using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSL.Controller;
using AjaxPro;
using WFE;
using InfoSoftGlobal;
using FSL.Logging;
using System.Data;
using System.Collections;
using System.IO;
using FSL.Message;
using System.Text;
using System.Configuration;
using System.Net;
using System.Runtime.InteropServices;
using System.Web.Services;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

public partial class Vendor_Matrix_Report : System.Web.UI.Page
{
    ListItem Li = new ListItem("--SELECT All--", "0");

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            app_Path.Text = HttpContext.Current.Request.ApplicationPath;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Vendor_Matrix_Report));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    txtCreatedBy.Text = ((string)Session["User_ADID"]);
                    FillVendor();
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    private void FillVendor()
    {
        String IsData = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Matrix_Report.aspx", "selectvendor", ref IsData, "0");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlVendor.DataSource = dt;
            ddlVendor.DataTextField = "Vendor_Name";
            ddlVendor.DataValueField = "Vendor_Code";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, Li);
        }
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string getVendorData(string expensev, string fdate, string tdate)
    {
        StringBuilder str = new StringBuilder();
        StringBuilder strc = new StringBuilder();
        DataTable dt = new DataTable();
        try
        {
            /////////////for summary ///////////////
            string isValid2 = string.Empty;
            string strcnt = (string)ActionController.ExecuteAction("", "Vendor_Matrix_Report.aspx", "count", ref isValid2, fdate, tdate);

            if (strcnt != "")
            {
                strc.Append("<table id='table2'  runat='server' class='table table-bordered table-hover'> <thead><tr class='grey'><th>Portal Matrix</th><th>Count</th>");
                strc.Append("</thead><tbody>");

                string[] Request_Unique = strcnt.Split('=');
                if (strcnt != "")
                {
                 
                   // strc.Append("<tr><td>Total No of Vendors Present</td><td class='vendors'><a href='#vendor' data-toggle='modal'>" + Request_Unique[0] + "</a></td></tr>");
                    strc.Append("<tr><td>Logged Vendors</td><td class='loggeda'><a href='#login' data-toggle='modal'>" + Request_Unique[1] + "</a></td></tr>");
                    strc.Append("<tr><td>Not Logged Vendors</td><td class='nloggeda'><a href='#nlogin' data-toggle='modal'>" + Request_Unique[2] + "</a></td></tr>");
                    strc.Append("<tr><td>Used Portal Vendors</td><td class='UsePortala'><a href='#portal' data-toggle='modal'>" + Request_Unique[3] + "</a></td></tr>");
                    strc.Append("<tr><td>Total No of Dispatches</td><td class='dispatcha'><a href='#dispatch' data-toggle='modal'>" + Request_Unique[4] + "</a></td></tr>");
                    strc.Append("<tr><td>Total No of Rejected</td><td class='Rejecteda'><a href='#rejected' data-toggle='modal'>" + Request_Unique[5] + "</a></td></tr>");
                    strc.Append("<tr><td>Total No of GRN Completed</td><td class='grna'><a href='#grn' data-toggle='modal'>" + Request_Unique[6] + "</a></td></tr>");
                    strc.Append("<tr><td>Total No of Part 1 Completed</td><td class='part1a'><a href='#parta' data-toggle='modal'>" + Request_Unique[7] + "</a></td></tr>");
                    strc.Append("<tr><td>Total No of Part 2 Completed</td><td class='part2a'><a href='#partb' data-toggle='modal'>" + Request_Unique[8] + "</a></td></tr>");
                    strc.Append("<tr><td>Total No of Quality Checking Completed</td><td class='qualitya'><a href='#quality' data-toggle='modal'>" + Request_Unique[9] + "</a></td></tr>");
                    strc.Append("<tr><td>Total No of Bill Book Completed</td><td class='billa'><a href='#bill' data-toggle='modal'>" + Request_Unique[10] + "</a></td></tr>");
                    strc.Append("<tr><td>Total No of Payments Processed</td><td class='paymenta'><a href='#payment' data-toggle='modal'>" + Request_Unique[11] + "</a></td></tr>");
                    strc.Append("<tr><td>Total No Quality Control Complete with Deviation & Deduction</td><td class='deviationa'><a href='#deviation' data-toggle='modal'>" + Request_Unique[12] + "</a></td></tr>");
                    strc.Append("<tr><td>Total No of Null Status</td><td class='nullsa'><a href='#nulls' data-toggle='modal'>" + Request_Unique[13] + "</a></td></tr>");
                    strc.Append("<tr><td>Total Acknowledge PO</td><td class='acknowa'><a href='#acknow' data-toggle='modal'>" + Request_Unique[14] + "</a></td></tr>");
                    strc.Append("<tr><td>Total PO Created after 01-Jun-2016</td><td class='createda'><a href='#created' data-toggle='modal'>" + Request_Unique[15] + "</a></td></tr>");
                    //strc.Append("</tr>");
                }
                strc.Append("</tbody></table> ");
            }
            string str2 = strc.ToString();

            /////////////////////////////////////////
            string isValid = string.Empty;
            string IsValid1 = string.Empty;
             dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Matrix_Report.aspx", "selectvendor", ref isValid, expensev);

            if (dt.Rows.Count > 0)
            {
                str.Append("<table id='data-table1' runat='server' class='table table-bordered table-hover'> <thead><tr class='grey'><th>Vendor Code</th><th>Vendor Name</th><th>Logged into Portal</th><th>Used Portal</th><th>NEFT Details</th></tr> ");
                str.Append("</thead><tbody>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //txt_vendor.Text = dt.Rows[i]["Vendor_Code"].ToString();
                    string isSaved = (string)ActionController.ExecuteAction("", "Vendor_Matrix_Report.aspx", "selectdata", ref IsValid1, dt.Rows[i]["Vendor_Code"].ToString());

                    if (isSaved != "")
                    {
                        string[] vendor_cnt = isSaved.Split('=');
                        str.Append("<tr>");
                        str.Append("<td>" + dt.Rows[i]["Vendor_Code"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Vendor_Name"].ToString() + "</td>");

                        if (vendor_cnt[0] != "0")
                        {
                            str.Append("<td>Yes</td>");
                        }
                        else
                        {
                            str.Append("<td>No</td>");
                        }
                        if (vendor_cnt[1] != "0")
                        {
                            str.Append("<td>Yes</td>");
                        }
                        else
                        {
                            str.Append("<td>No</td>");
                        }
                        str.Append("<td> </td>");
                        str.Append("</tr>");
                    }
                }
                str.Append("</tbody></table> ");
            }
        }

        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        
        string str1 = str.ToString() + "&&&" + strc + "&&&" + dt.Rows.Count;
        return str1;
    }
   
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string bindReport(string fdate,string tdate, int number)
    {
        string strData = string.Empty;
        string data = string.Empty;
        StringBuilder html = new StringBuilder();
        if (number == 5 || number == 6 || number == 2 || number == 1)
        {
            string username = Convert.ToString(Session["USER_ADID"]);
            html.Append("<table class='table table-bordered table-hover' id='logintable" + number + "' >");
            html.Append("<thead><tr class='grey center'><th>#</th><th>Vendor Code</th><th>Vendor Name</th></tr></thead>");
            html.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "Vendor_Matrix_Report.aspx", "getacknowdata", ref data, fdate,tdate, number);
            if (dtData != null)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + (i + 1) + "</td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["Vendor_Code"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["Vendor_Name"]) + "</td>");
                    html.Append("</tr>");
                }
            }
            html.Append("</tbody>");
            html.Append("</table>");
        }
        else if (number == 4 || number == 7 || number == 8 || number == 9 || number == 10 || number == 11 || number == 12 || number == 13 || number == 14 || number == 15 || number == 16 || number == 17)
        {
            string username = Convert.ToString(Session["USER_ADID"]);
            html.Append("<table class='table table-bordered table-hover' id='vendordata" + number + "' >");
            if (number == 16)
            {
                html.Append("<thead><tr class='grey center'><th>#</th><th>PO Number</th><th>Vendor Code</th><th>Vendor Name</th><th>Date</th><th>Acknowledge Status</th></tr></thead>");
            }
            else if (number == 17)
            {
                html.Append("<thead><tr class='grey center'><th>#</th><th>PO Number</th><th>Vendor Code</th><th>Vendor Name</th><th>Date</th><th>Status</th></tr></thead>");
            }
            else
            {
                html.Append("<thead><tr class='grey center'><th>#</th><th>PO Number</th><th>Vendor Code</th><th>Vendor Name</th><th>Unique No</th><th>Date</th><th>Status</th></tr></thead>");

            }
            html.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "Vendor_Matrix_Report.aspx", "getacknowdata", ref data, fdate, tdate, number);
            if (dtData != null)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    CryptoGraphy crypt = new CryptoGraphy();
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtData.Rows[i]["PO_Number"]));
                    html.Append("<tr>");
                    html.Append("<td>" + (i + 1) + "</td>");
                    html.Append("<td><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dtData.Rows[i]["PO_Number"]) + "</a><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["Vendor_Code"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["Vendor_Name"]) + "</td>");
                    if (number != 16 && number != 17)
                    {
                        html.Append("<td><a href='#paymentterm' role='button' data-toggle='modal' onclick='setSelectedNote(" + Convert.ToString(dtData.Rows[i]["PK_Dispatch_Note_ID"]) + ")'>" + Convert.ToString(dtData.Rows[i]["Unique_No"]) + "</a></td>");
                    }
                    html.Append("<td>" + Convert.ToString(dtData.Rows[i]["dis_Date"]) + "</td>");
                    if (number == 16)
                    {
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["IS_ACK"]) + "</td>");
                    }
                    else
                    {
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["SAP_Status"]) + "</td>");
                    }
                    html.Append("</tr>");
                }
            }
            html.Append("</tbody>");
            html.Append("</table>");
        }

        strData = Convert.ToString(html);
        return strData;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string GetCurrentTime(int name)
    {
        GetData getData = new GetData();
        string data = getData.get_Dispatch_Details(name);
        return data;
    }
    protected void btn_Export_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            StringBuilder str = new StringBuilder();
            ExportToExcel(str, "Vendor Matrix Report");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Reports/Report_Master.aspx");
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
    protected void btn_export_vendor_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelLogin(txt_exp_vendor_data.Text, "Vendors Details");
        }
    }
    protected void btn_export_login_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelLogin(txt_exp_login_data.Text, "Login Details");
        }
    }
    protected void btn_export_nlogin_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelLogin(txt_exp_nlogin_data.Text, "Not Logged Vendors");
        }
    }
    protected void btn_export_portal_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelLogin(txt_exp_portal_data.Text, "Used Portal Vendors");
        }
    }
    protected void btn_export_dispatch_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("4", "Dispatch Vendor Details");
        }
    }
    protected void btn_export_reject_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("7", "Rejected Status Dispatch Details");
        }
    }
    protected void btn_export_grn_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("8", "GRN Completed Status Dispatch Details");
        }
    }
    protected void btn_export_part1_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("9", "Part1 Status Dispatch Details");
        }
    }
    protected void btn_export_part2_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("10", "Part2 Status Dispatch Details");
        }
    }
    protected void btn_export_quality_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("11", "Quality Checking Completed Status Dispatch Details");
        }
    }
    protected void btn_export_bill_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("12", "Bill Book Completed Status Dispatch Details");
        }
    }
    protected void btn_export_pay_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("13", "Payments Processed Status Dispatch Details");
        }
    }
    protected void btn_export_devia_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("14", "Quality Control Complete with Deviation & Deduction Status Dispatch Details");
        }
    }
    protected void btn_export_null_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("15", "Null Status Dispatch Details");
        }
    }
    protected void btn_export_ack_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("16", "Acknowledge PO Details");
        }
    }
    protected void btn_export_pod_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcelDispatch("17", "PO Details");
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
            //divack = txt_exp_login_data.Text;
            if (divack != "")
            {
                Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                     "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
                Response.Write(divack);
            }
            Response.End();
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');</script>");
            return;
        }
    }
    protected void ExportToExcelDispatch(string num, string filename)
    {
        try
        {
            string data = string.Empty;
            if (num != "")
            {
                int number = Convert.ToInt16(num);
                StringBuilder html = new StringBuilder();
                html.Append("<table class='table table-bordered table-hover' id='viewdata' >");
                if (number == 16)
                {
                    html.Append("<thead><tr class='grey center'><th>#</th><th>PO Number</th><th>Vendor Code</th><th>Vendor Name</th><th>Date</th><th>Acknowledge Status</th></tr></thead>");
                }
                else if (number == 17)
                {
                    html.Append("<thead><tr class='grey center'><th>#</th><th>PO Number</th><th>Vendor Code</th><th>Vendor Name</th><th>Date</th><th>Status</th></tr></thead>");
                }
                else
                {
                    html.Append("<thead><tr class='grey center'><th>#</th><th>PO Number</th><th>Vendor Code</th><th>Vendor Name</th><th>Unique No</th><th>Date</th><th>Status</th></tr></thead>");

                }
                html.Append("<tbody>");
                DataTable dtData = (DataTable)ActionController.ExecuteAction("", "Vendor_Matrix_Report.aspx", "getacknowdata", ref data, txt_f_date.Value, txt_t_date.Value, number);
                if (dtData != null)
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + (i + 1) + "</td>");
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["PO_Number"]) + "</td>");
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["Vendor_Code"]) + "</td>");
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["Vendor_Name"]) + "</td>");
                        if (number != 16 && number != 17)
                        {
                            html.Append("<td>" + Convert.ToString(dtData.Rows[i]["Unique_No"]) + "</td>");
                        }
                        html.Append("<td>" + Convert.ToString(dtData.Rows[i]["dis_Date"]) + "</td>");
                        if (number == 16)
                        {
                            html.Append("<td>" + Convert.ToString(dtData.Rows[i]["IS_ACK"]) + "</td>");
                        }
                        else
                        {
                            html.Append("<td>" + Convert.ToString(dtData.Rows[i]["SAP_Status"]) + "</td>");
                        }
                        html.Append("</tr>");
                    }
                }
                html.Append("</tbody>");
                html.Append("</table>");
                string dt = string.Empty;
                string attachment = "attachment; filename=" + filename + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                dt = html.ToString();
                if (dt != "")
                {
                    Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                         "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
                    Response.Write(dt);
                }
                Response.End();
            }
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
            string dtcnt = string.Empty;
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            dt = txtdata.Text;
            dtcnt = txtdatacnt.Text;
            if (dt != "")
            {
                Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                        "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
                Response.Write(dtcnt.ToString());
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
   
    //protected void btnClear_Click(object sender, EventArgs e)
    //{
    //    txt_f_date.Value = "";
    //    txt_t_date.Value = "";
    //    ddlVendor.SelectedIndex = 0;
    //    div_po.InnerHtml = null;
    //    div_vendor.InnerHtml = null;
    //    div_count.InnerHtml = null;
    //    div_dispatch.InnerHtml = null;
    //    div_login.InnerHtml = null;
    //    div_ack.InnerHtml = null;
    //    div_pod.InnerHtml = null;
    //    div_portal.InnerHtml = null;
    //    div_quality.InnerHtml = null;
    //    div_reject.InnerHtml = null;
    //    div_grn.InnerHtml = null;
    //    div_deviation.InnerHtml = null;
    //    div_bill.InnerHtml = null;
    //    //div_loginn.InnerHtml = "";
    //}
}