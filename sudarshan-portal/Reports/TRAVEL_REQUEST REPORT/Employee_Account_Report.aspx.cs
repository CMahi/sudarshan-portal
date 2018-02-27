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

public partial class Employee_Account_Report : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    string IsData = string.Empty;
    StringBuilder txtTablesNames = new StringBuilder();
    double[] BudgetSavings=new double[13];
    double[] ActualSavings=new double[13];
    StringBuilder str = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            app_Path.Text = HttpContext.Current.Request.ApplicationPath;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Employee_Account_Report));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    txtCreatedBy.Text = ((string)Session["User_ADID"]);
                    FillVoucher();
                }               
            }           
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    private void FillVoucher()
    {

        String IsData = string.Empty;
        ListItem Li = new ListItem("--Select One--", "0");
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Employee_Account_Report.aspx", "vouchertype", ref IsData);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlVoucherType.DataSource = dt;
            ddlVoucherType.DataTextField = "VOUCHER_TYPE";
            ddlVoucherType.DataValueField = "PK_VOUCHER_TYPE_ID";
            ddlVoucherType.DataBind();
            ddlVoucherType.Items.Insert(0, Li);
        }
    } 
    private void getVoucherData()
    {
        string isValid = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Employee_Account_Report.aspx", "selectdata", ref isValid, ddlVoucherType.SelectedValue);

        if (dt.Rows.Count > 0)
        {
            if (ddlVoucherType.SelectedValue == "1")
            {
                str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th> #</th><th>Expense Head</th><th>Employee Name</th><th>Employee Code</th><th>Cost Centre</th><th>GL code</th><th>Total Expense Amount</th><th>Reimbursement Amount</th><th>Mode of Payment</th><th>Voucher Date</th></tr> ");
            }
            else
            {
                str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th> #</th><th>Expense Head</th><th>Employee Name</th><th>Employee Code</th><th>Cost Centre</th><th>GL code</th><th>Advance Amount</th><th>Total Expense Amount</th><th>Reimbursement Amount</th><th>Mode of Payment</th><th>Voucher Date</th></tr> ");
            }
            str.Append("</thead><tbody>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str.Append("<tr>");
                str.Append("<td>" + (i + 1) + "</td>");
                str.Append("<td>" + dt.Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["EMP_ID"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["COST_CENTER_NAME"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["SAP_GLCode"].ToString() + "</td>");
                if (ddlVoucherType.SelectedValue != "1")
                {
                    str.Append("<td align='right'>" + dt.Rows[i]["advamount"].ToString() + "</td>");
                }
                str.Append("<td align='right'>" + dt.Rows[i]["billamount"].ToString() + "</td>");
                //if (ddlVoucherType.SelectedValue != "4")
                //{
                    str.Append("<td align='right'>" + dt.Rows[i]["amount"].ToString() + "</td>");
                //}
                str.Append("<td>" + dt.Rows[i]["PAYMENT_MODE"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["created_date"].ToString() + "</td>");
                str.Append("</tr>");
            }
            str.Append("</tbody></table> ");
            divdata.InnerHtml = str.ToString();
            txtdata.Text = divdata.InnerHtml;
            ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);
        }
        else
        {
            divdata.InnerHtml = null;
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");                 
        }
    }
   
   protected void btn_Export_onClick(object sender, EventArgs e)
   {
       if (ActionController.IsSessionExpired(Page))
           ActionController.RedirctToLogin(Page);
       else
       {
           ExportToExcel(str, "Employee Account Report");
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
   protected void ExportToExcel(StringBuilder dgview, string filename)
   {
       try
       {
           string dt = string.Empty;
           string attachment = "attachment; filename=" + filename + ".xls";
           Response.ClearContent();
           Response.AddHeader("content-disposition", attachment);
           Response.ContentType = "application/ms-excel";
           dt = txtdata.Text;
           if (dt!="")
           {
               //Response.Write("<table border=\"1\"><tr><td colspan='8' align='center' style=\"font-weight:bold;font-size:'20';\">Employee Account Reportt</td></tr>");
               //Response.Write("<tr style=\"font-weight:bold\"><td colspan='8'></td></tr>");
              // Response.Write("<tr style=\"font-weight:bold\"><td colspan='7' align='center'>" + dt + "</td></tr>");            
              // Response.Write("</table>");
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

   protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
   {
       getVoucherData();
   }
}