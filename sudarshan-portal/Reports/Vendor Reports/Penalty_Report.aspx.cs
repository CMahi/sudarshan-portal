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

public partial class Penalty_Report : System.Web.UI.Page
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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Penalty_Report));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    txtCreatedBy.Text = ((string)Session["User_ADID"]);
                    getPoData();                   
                }               
            }           
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    private void getPoData()
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                string isValid = string.Empty;
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Penalty_Report.aspx", "selectpodata", ref isValid, txtCreatedBy.Text);            

                if (dt.Rows.Count > 0)
                {
                    str.Append("<table id='data-table1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th> #</th><th>Unique Id</th><th>Dispatch Request No</th><th>PO No</th><th>PO Gross Value</th><th>Material No</th><th>Dispatch Quantity</th><th>Penalty(%)</th></tr> ");
                    str.Append("</thead><tbody>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string encrypt_Str = crypt.Encryptdata(dt.Rows[i]["PO_NUMBER"].ToString());
                        str.Append(" <tr>");
                        str.Append("<td>"+ (i+1) +"</td>");
                        str.Append("<td>" + dt.Rows[i]["Unique_No"].ToString() + "</td>");
                        str.Append("<td> <a href='#paymentterm' role='button' data-toggle='modal' onclick='setSelectedNote(" + Convert.ToString(dt.Rows[i]["PK_Dispatch_Note_ID"]) + ")'>" + Convert.ToString(dt.Rows[i]["Request_ID"]) + "</a></td>");
                        str.Append("<td><a href='#' role='button' id='anc1' data-toggle='modal' onclick='viewData(" + (i + 1) + ")' >" + dt.Rows[i]["PO_NUMBER"].ToString() + "</a><input type='text' id='encrypt_po_" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");                      
                        str.Append("<td style='text-align: right'>" + dt.Rows[i]["PO_GV"].ToString() + "</td>");                      
                        str.Append("<td>" + dt.Rows[i]["MATERIAL_NO"].ToString() + "</td>");
                        str.Append("<td style='text-align: right'>" + dt.Rows[i]["DISPATCH_QUANTITY"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["PRCEN"].ToString() + "</td>"); 
                        str.Append("</tr>");        
                    }
                    str.Append("</tbody></table> ");
          
                 }

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
           ExportToExcel(str, "Penalty Report");
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
               Response.Write("<table border=\"1\"><tr><td colspan='8' align='center' style=\"font-weight:bold;font-size:'20';\">Penalty Report</td></tr>");
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