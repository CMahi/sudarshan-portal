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

public partial class Advance_Report_All_Vendors : System.Web.UI.Page
{
    ListItem Li = new ListItem("--SELECT All--", "0");
    string IsData = string.Empty;
    CryptoGraphy crypt = new CryptoGraphy();
    StringBuilder str = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            app_Path.Text = HttpContext.Current.Request.ApplicationPath;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Report_All_Vendors));
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
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Advance_Report_All_Vendors.aspx", "selectvendor", ref IsData);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlVendor.DataSource = dt;
            ddlVendor.DataTextField = "Vendor_Name";
            ddlVendor.DataValueField = "Vendor_Code";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, Li);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string isValid = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Advance_Report_All_Vendors.aspx", "getdata", ref isValid, txt_f_date.Value, txt_t_date.Value, ddlVendor.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            str.Append("<table id='data-table1' class='table table-bordered table-hover' > <thead><tr class='grey'><th> #</th><th>Request No.</th><th style='display:none'></th><th>Vendor Name</th><th>Vendor Code</th><th>PO Number</th><th>PO Date</th><th>Request Date</th><th>PO Type</th><th>PO Value</th><th>PO Gross Value</th><th>Advance Amount</th><th>Request Status</th></tr> ");
            str.Append("</thead><tbody>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string encrypt_Str = crypt.Encryptdata(Convert.ToString(dt.Rows[i]["PO_Number"]));
                str.Append(" <tr>");
                str.Append("<td>" + (i + 1) + "</td>");
                str.Append("<td class='clspo'><a href='#po' data-toggle='modal'>" + dt.Rows[i]["Header_Info"].ToString() + "</a></td>");
                str.Append("<td style='display:none'>" + dt.Rows[i]["Header_Info"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["Vendor_Name"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["Vendor_Code"].ToString() + "</td>");
                str.Append("<td><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dt.Rows[i]["PO_Number"]) + "</a><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
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
            divdata.InnerHtml = str.ToString();
            txtdata.Text = str.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({ 'bSort': false });", true);
        }
        else
        {
            divdata.InnerHtml = null;
            txtdata.Text = "";
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
        }
        divIns.Style.Add("display", "none");
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getPODetails(string req_no)
    {
         string DisplayData = string.Empty;
        StringBuilder sb = new StringBuilder();
        DataTable dsData = new DataTable();
        try
        {
            string isData = "";
            dsData = (DataTable)ActionController.ExecuteAction("", "Advance_Report_All_Vendors.aspx", "getpodetail", ref isData, req_no);
            DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>Request No</th><th>Vendor Code</th><th>PO Number</th><th>PO Type</th><th>PO Gross Value</th><th>Amount</th><th>Status</th></tr></thead>";
                if (dsData != null)
                {
                    if (dsData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dsData.Rows.Count; i++)
                        {
                            DisplayData += "<tr><td>" + Convert.ToString(dsData.Rows[i]["Header_Info"]) + "</td><td>" + Convert.ToString(dsData.Rows[i]["Vendor_Code"]) + "</td><td>" + Convert.ToString(dsData.Rows[i]["PO_Number"]) + "</td><td>" + Convert.ToString(dsData.Rows[i]["PO_type"]) + "</td><td>" + Convert.ToString(dsData.Rows[i]["PO_GV"]) + "</td><td>" + Convert.ToString(dsData.Rows[i]["Advance_Amount"]) + "</td><td>" + Convert.ToString(dsData.Rows[i]["Request_Status"]) + "</td></tr>";
                        }
                    }
                }
                DisplayData += "</table>";
        }
        catch (Exception ex)
        {
            //DisplayData.ToString() = ""; 
        }
        return DisplayData.ToString();
    }
   protected void btn_Export_onClick(object sender, EventArgs e)
   {
       if (ActionController.IsSessionExpired(Page))
           ActionController.RedirctToLogin(Page);
       else
       {
           ExportToExcel(str, "Vendors Advance Request Report");
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
   protected void ExportToExcel(StringBuilder dgview, string filename)
   {
       try
       {
           string dtdata = string.Empty;
           string attachment = "attachment; filename=" + filename + ".xls";
           Response.ClearContent();
           Response.AddHeader("content-disposition", attachment);
           Response.ContentType = "application/ms-excel";
           string isValid = string.Empty;
           DataTable dt = (DataTable)ActionController.ExecuteAction("", "Advance_Report_All_Vendors.aspx", "getdata", ref isValid, txt_f_date.Value, txt_t_date.Value, ddlVendor.SelectedValue);
           if (dt.Rows.Count > 0)
           {
               str.Append("<table id='data-table1' class='table table-bordered table-hover' > <thead><tr class='grey'><th> #</th><th>Request No.</th></th><th>Vendor Name</th><th>Vendor Code</th><th>PO Number</th><th>PO Date</th><th>Request Date</th><th>PO Type</th><th>PO Value</th><th>PO Gross Value</th><th>Advance Amount</th><th>Request Status</th></tr> ");
               str.Append("</thead><tbody>");
               for (int i = 0; i < dt.Rows.Count; i++)
               {                  
                   str.Append(" <tr>");
                   str.Append("<td>" + (i + 1) + "</td>");
                   str.Append("<td>" + dt.Rows[i]["Header_Info"].ToString() + "</td>");
                   str.Append("<td>" + dt.Rows[i]["Vendor_Name"].ToString() + "</td>");
                   str.Append("<td>" + dt.Rows[i]["Vendor_Code"].ToString() + "</td>");
                   str.Append("<td>" + Convert.ToString(dt.Rows[i]["PO_Number"]) + "</td>");
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
               dtdata = str.ToString();
              
           }
           if (dtdata != "")
           {
               Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                        "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
               Response.Write(dtdata);
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