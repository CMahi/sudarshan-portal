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

public partial class Foreign_Non_Chargable_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            app_Path.Text = HttpContext.Current.Request.ApplicationPath;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Foreign_Non_Chargable_Report));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    getExpenseData(0);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }

    protected void getExpenseData(int charge)
    {
        StringBuilder str = new StringBuilder();
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
divIns.Style.Add("display","none");
                string isValid = string.Empty;
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Expense_Report.aspx", "getreportdata", ref isValid, charge);

                if (dt.Rows.Count > 0)
                {
                    str.Append("<table id='data-table1' class='table table-bordered table-hover' ><thead><tr class='grey'><th> #</th><th>Employee Code</th><th>Employee Name</th><th>Request No.</th><th>Area Visited</th><th>Currency</th>");
                    DataSet ejm = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getexpenseids", ref isValid);
                    if (ejm != null)
                    {
                        for (int e = 0; e < ejm.Tables[0].Rows.Count; e++)
                        {
                            str.Append("<th>" + Convert.ToString(ejm.Tables[0].Rows[e]["EXPENSE_HEAD"]) + "</th>");
                        }
                    }
                    str.Append("<th>Total</th>");
                    str.Append("</tr> ");
                    str.Append("</thead><tbody>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string req_no = Convert.ToString(dt.Rows[i]["REQUEST_NO"]);
                        decimal tot_amount = 0;
                        str.Append(" <tr>");
                        str.Append("<td>" + (i + 1) + "</td>");
                        str.Append("<td>" + dt.Rows[i]["EMP_CODE"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["EMP_NAME"].ToString() + "</td>");
                        str.Append("<td><a href='#div_Details' role='button' data-toggle='modal' onclick='view_details(" + dt.Rows[i]["WIID"].ToString() + ")'>" + dt.Rows[i]["REQUEST_NO"].ToString() + "</a></td>");
                        str.Append("<td>" + dt.Rows[i]["COUNTRY_NAME"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["CURRENCY"].ToString() + "</td>");
                        if (ejm != null)
                        {
                            for (int e = 0; e < ejm.Tables[0].Rows.Count; e++)
                            {
                                string amount = (string)ActionController.ExecuteAction("", "Foreign_Travel_Expense_Report.aspx", "getexpamount", ref isValid, req_no, Convert.ToString(ejm.Tables[0].Rows[e]["FK_EXPENSE_HEAD"]), charge);
                                str.Append("<td>" + amount + "</td>");
                                tot_amount = tot_amount + Convert.ToDecimal(amount);
                            }
                        }
                        str.Append("<td>" + tot_amount.ToString("#.00") + "</td>");
                        str.Append("</tr>");
                    }
                    str.Append("</tbody></table> ");

                }

                divdata.InnerHtml = str.ToString();

                ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);
            }

            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
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
            string data = divdata.InnerHtml;
            ExportToExcel(data, "FOREIGN TRAVEL NON CHARGABLE REPORT");
        }
    }

    protected void ExportToExcel(string dgview, string filename)
    {
        try
        {
            string dt = string.Empty;
            string attachment = "attachment; filename=" + filename + ".xls";
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
        Foreign_Expense_Details fed = new Foreign_Expense_Details();
        str_data = fed.Foreign_Expense_Request_Details(wiid);
        return str_data;
    }
}