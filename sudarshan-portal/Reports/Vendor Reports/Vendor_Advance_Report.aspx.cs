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

public partial class Vendor_Advance_Report : System.Web.UI.Page
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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Vendor_Advance_Report));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    txtCreatedBy.Text = ((string)Session["User_ADID"]);

                    getVendorData();
                }               
            }           
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
  
    private void getVendorData()
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                string isValid = string.Empty;
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Advance_Report.aspx", "getdata", ref isValid, txtCreatedBy.Text);            

                if (dt.Rows.Count > 0)
                {
                    str.Append("<table id='data-table1' class='table table-bordered table-hover' > <thead><tr class='grey'><th> #</th><th>Request No.</th><th>Vendor Name</th><th>Vendor Code</th><th>PO Number</th><th>PO Date</th><th>Request Date</th><th>PO Type</th><th>PO Value</th><th>PO_GV</th><th>Advance Amount</th><th>Request Status</th></tr> ");
                    str.Append("</thead><tbody>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str.Append(" <tr>");
                        str.Append("<td>"+ (i+1) +"</td>");
                        str.Append("<td>" + dt.Rows[i]["Header_Info"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Vendor_Name"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Vendor_Code"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["PO_Number"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["PO_Date"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Request_Date"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["PO_Type"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["PO_Value"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["PO_GV"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["Advance_Amount"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["Request_Status"].ToString() + "</td>"); 
                        str.Append("</tr>");        
                    }
                    str.Append("</tbody></table> ");
          
                 }

                divdata.InnerHtml = str.ToString();
                txtdata.Text = str.ToString();                    
                ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);       
            }
    
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }
    
   protected void btn_Export_onClick(object sender, EventArgs e)
   {
       if (ActionController.IsSessionExpired(Page))
           ActionController.RedirctToLogin(Page);
       else
       {
           ExportToExcel(str, "Advance Request Report");
       }
   }
   //[AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
   //public string GetCurrentTime(int name)
   //{
   //    GetData getData = new GetData();
   //    string data = getData.get_Dispatch_Details(name);
   //    return data;
   //}
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
               Response.Write("<table border=\"1\"><tr><td colspan='8' align='center' style=\"font-weight:bold;font-size:'20';\">Advance Request Report</td></tr>");
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