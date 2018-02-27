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

public partial class Foreign_Travel_Advance_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            app_Path.Text = HttpContext.Current.Request.ApplicationPath;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Foreign_Travel_Advance_Report));
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
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Expense_Report.aspx", "getadvancereport", ref isValid, status);

                if (dt.Rows.Count > 0)
                {
                    str.Append("<table id='data-table1' class='table table-bordered table-hover' > <thead><tr class='grey'><th> #</th><th>Voucher No.</th><th>Voucher Date</th><th>Employee Code</th><th>Employee Name</th><th>Foreign Currency</th><th>Advance Amount</th><th>Approval Date</th><th>Approved By</th></tr> ");
                    str.Append("</thead><tbody>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str.Append(" <tr>");
                        str.Append("<td>" + (i + 1) + "</td>");
                        str.Append("<td><a href='#div_Details' role='button' data-toggle='modal' onclick='view_details(" + Convert.ToString(dt.Rows[i]["PROCESS_ID"]) + ","+Convert.ToString(dt.Rows[i]["INSTANCE_ID"])+")'>" + dt.Rows[i]["REQUEST_NO"].ToString() + "</a></td>");
			str.Append("<td>" + Convert.ToDateTime(dt.Rows[i]["CREATION_DATE"]).ToString("dd-MMM-yyyy") + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["PERSONAL_NUM_PERNR"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["EMP_NAME_ENAME"]) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["CURRENCY_NAME"]) + "</td>");
                        str.Append("<td style='text-align:right'>" + Convert.ToString(dt.Rows[i]["ADVANCE_AMOUNT"]) + "</td>");
			if(Convert.ToString(dt.Rows[i]["APPROVAL_DATE"])=="")
			{
			 	str.Append("<td>NA</td>");
			}
			else
			{
			 	str.Append("<td>" + Convert.ToDateTime(dt.Rows[i]["APPROVAL_DATE"]).ToString("dd-MMM-yyyy") + "</td>");
			}
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["APPROVAL_BY"]) + "</td>");                        
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
    public string getRequestDetails(int processid,int instanceid)
    {
        string str_data = string.Empty;
        Foreign_Expense_Details fed=new Foreign_Expense_Details();
        str_data = fed.Foreign_Advance_Details(processid,instanceid);
        return str_data;
    }

    
}