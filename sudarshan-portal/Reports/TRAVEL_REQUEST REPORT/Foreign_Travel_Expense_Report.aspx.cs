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

public partial class Foreign_Travel_Expense_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            app_Path.Text = HttpContext.Current.Request.ApplicationPath;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Foreign_Travel_Expense_Report));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    //getExpenseData();
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getExpenseData(string status)
    {
        StringBuilder str = new StringBuilder();
            try
            {
                string isValid = string.Empty;
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Expense_Report.aspx", "getexpensedata", ref isValid, status);

                if (dt.Rows.Count > 0)
                {
                    str.Append("<table id='data-table1' class='table table-bordered table-hover' > <thead><tr class='grey'><th> #</th><th>Employee Code</th><th>Employee Name</th><th>Request No</th><th>Voucher Date</th><th>Area Visited</th><th>Deviation Reason</th><th>Pending With</th><th>SAP No</th><th>Posting Date</th><th>Currency</th><th>Currency Taken</th><th>Currency Expenses</th><th>Return Money</th> <th>Payble/Refundable Money</th><th>Without Supporting Expenses</th></tr> ");
                    str.Append("</thead><tbody>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str.Append(" <tr>");
                        str.Append("<td>" + (i + 1) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["EMP_CODE"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["EMP_NAME"]) + "</td>");
                        str.Append("<td><a href='#div_Details' role='button' data-toggle='modal' onclick='view_details(" + dt.Rows[i]["WIID"].ToString() + ")'>" + dt.Rows[i]["REQUEST_NO"].ToString() + "</a></td>");
			str.Append("<td>" + Convert.ToDateTime(dt.Rows[i]["CREATED_DATE"]).ToString("dd-MMM-yyyy") + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["COUNTRY_NAME"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["DEVIATION_REASON"]) + "</td>");
			 str.Append("<td>" + Convert.ToString(dt.Rows[i]["PENDING_WITH"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["BANK_NO"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["POSTING_DATE"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["CURRENCY"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["ADVANCE"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["EXPENSE_CURRENCY"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["RETURN_MONEY"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["PAYBLE_REFUNDABLE"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["NON_SUPPORT"]) + "</td>");
                        str.Append("</tr>");
                    }
                    str.Append("</tbody></table> ");

                }

               // divdata.InnerHtml = str.ToString();
                
                //ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);
            }

            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
            return str.ToString();
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

    protected void btn_Export_onClick(object sender, EventArgs e)
    {

        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            string data = dData.Text;
            ExportToExcel(data, "FOREIGN TRAVEL EXPENSE REPORT");
        }
    }

    protected void ExportToExcel(string dgview, string filename)
    {
        try
        {
            string dt = string.Empty;
            string attachment = "attachment; filename=" + filename + " - " + ddlStatus.SelectedItem.Text + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            dt = dgview.ToString();
            if (dt != "")
            {
                Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                    "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
                Response.Write(dt);
            }
            Response.End();
        }

        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
            return;
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getRequestDetails(int wiid)
    {
        string str_data = string.Empty;
        Foreign_Expense_Details fed=new Foreign_Expense_Details();
        str_data = fed.Foreign_Expense_Request_Details(wiid);
        return str_data;
    }

    
}