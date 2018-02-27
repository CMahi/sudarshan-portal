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

public partial class Vendor_Listing_Report : System.Web.UI.Page
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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Vendor_Listing_Report));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    txtCreatedBy.Text = ((string)Session["User_ADID"]);
                    FillMSED();
                    getVendorFirst();
                }               
            }           
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    private void FillMSED()
    {
       // ddlMSED.Items.Insert(0, new ListItem("--Select One--", "0"));
        ddlMSED.Items.Insert(0, new ListItem("Yes", "Yes"));
        ddlMSED.Items.Insert(1, new ListItem("No", "No"));
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
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Listing_Report.aspx", "selectpodata", ref isValid, ddlMSED.SelectedValue);            

                if (dt.Rows.Count > 0)
                {
                    str.Append("<table id='data-table1' class='table table-bordered table-hover' > <thead><tr class='grey'><th> #</th><th>Vendor Code</th><th>Vendor Name</th><th>Address</th><th>City</th><th>Region</th><th>Postal Code</th> <th>Telephone1</th><th>Telephone2</th><th>FAX No</th>	<th>PAN No</th><th>ECC No</th><th>LocalSales TaxNo</th><th>CentralSales TaxNo</th>	<th>Excise RegNo</th><th>Email</th>	<th>website</th><th>Bank</th><th>ACC No</th><th>IFSC Code</th><th>BRANCH</th><th>MSED RegnNo</th></tr> ");
                    str.Append("</thead><tbody>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str.Append(" <tr>");
                        str.Append("<td>"+ (i+1) +"</td>");
                        str.Append("<td>" + dt.Rows[i]["Vendor_Code"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Vendor_Name"].ToString() + "</td>");                      
                        str.Append("<td>" + dt.Rows[i]["Address"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["City"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Region"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["Postal_Code"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["Telephone1"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["Telephone2"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["FAX_NO"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["PAN_NO"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["ECC_NO"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["Local_Sales_Tax_No"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Central_Sales_Tax_No"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["Excise_Reg_No"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["Email"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["website"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["FK_BANK_ID"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["ACC_NO"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["IFSC_CODE"].ToString() + "</td>"); 
                        str.Append("<td>" + dt.Rows[i]["BRANCH"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["MSMED_REG_NO"].ToString() + "</td>"); 
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
    private void getVendorFirst()
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                string isValid = string.Empty;
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Listing_Report.aspx", "selectpodata", ref isValid, "Yes");

                if (dt.Rows.Count > 0)
                {
                    str.Append("<table id='data-table1' class='table table-bordered table-hover' > <thead><tr class='grey'><th> #</th><th>Vendor Code</th><th>Vendor Name</th><th>Address</th><th>City</th><th>Region</th><th>Postal Code</th> <th>Telephone1</th><th>Telephone2</th><th>FAX No</th>	<th>PAN No</th><th>ECC No</th><th>LocalSales TaxNo</th><th>CentralSales TaxNo</th>	<th>Excise RegNo</th><th>Email</th>	<th>website</th><th>Bank</th><th>ACC No</th><th>IFSC Code</th><th>BRANCH</th><th>MSED RegnNo</th></tr> ");
                    str.Append("</thead><tbody>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str.Append(" <tr>");
                        str.Append("<td>" + (i + 1) + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Vendor_Code"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Vendor_Name"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Address"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["City"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Region"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Postal_Code"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Telephone1"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Telephone2"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["FAX_NO"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["PAN_NO"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["ECC_NO"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Local_Sales_Tax_No"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Central_Sales_Tax_No"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Excise_Reg_No"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Email"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["website"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["FK_BANK_ID"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["ACC_NO"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["IFSC_CODE"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["BRANCH"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["MSMED_REG_NO"].ToString() + "</td>");
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
           ExportToExcel(str, "Vendor Listing Report");
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
               Response.Write("<table border=\"1\"><tr><td colspan='8' align='center' style=\"font-weight:bold;font-size:'20';\">Vendor Listing Report</td></tr>");
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

   protected void ddlMSED_SelectedIndexChanged(object sender, EventArgs e)
   {
       getVendorData();
   }
}