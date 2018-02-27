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

public partial class Pending_Service_PO : System.Web.UI.Page
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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Pending_Service_PO));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    txtCreatedBy.Text = ((string)Session["User_ADID"]);
                    fillVendor();
                }               
            }           
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    private void fillVendor()
    {
        string isdata1 = string.Empty;
        ListItem li = new ListItem("--Select One--", "0");
        DataSet ds = (DataSet)ActionController.ExecuteAction("", "Pending_Service_PO.aspx", "getvendor", ref isdata1);
        DataTable dl = ds.Tables[0];
        ddl_Vendor.DataSource = dl;
        ddl_Vendor.DataTextField = "Vendor_Name";
        ddl_Vendor.DataValueField = "Vendor_Code";
        ddl_Vendor.DataBind();
        ddl_Vendor.Items.Insert(0, li);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string isValid = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Pending_Service_PO.aspx", "getdata", ref isValid, txt_f_date.Value, txt_t_date.Value,ddl_Vendor.SelectedValue);
        if (dt.Rows.Count > 0 || dt!=null)
        {
            str.Append("<table id='data-table1' class='table table-bordered table-hover' > <thead><tr class='grey'><th> #</th><th>Vendor Name</th><th>Service No.</th><th>Service Text</th><th>PO Number</th><th>PO Date</th><th>Service Entry Date</th><th>Plant</th><th>Payment Terms</th></tr> ");
            str.Append("</thead><tbody>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string encrypt_Str = crypt.Encryptdata(Convert.ToString(dt.Rows[i]["PO_NO"]));
                str.Append(" <tr>");
                str.Append("<td>" + (i + 1) + "</td>");
                str.Append("<td>" + dt.Rows[i]["Vendor_Name"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["SERVICE_NO"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["SHORT_TEXT"].ToString() + "</td>");
                str.Append("<td><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dt.Rows[i]["PO_NO"]) + "</a><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                str.Append("<td>" + dt.Rows[i]["PO_SUBMITED_DATE"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["budat"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["PLANT"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
            }
            str.Append("</tbody></table> ");
            divdata.InnerHtml = str.ToString();
            txtdata.Text = str.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({'bSort': false,'pageLength': 100});", true);
        }
        else
        {
            divdata.InnerHtml = null;
            txtdata.Text = "";
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
        }
        divIns.Style.Add("display", "none");
    }

   protected void btn_Export_onClick(object sender, EventArgs e)
   {
       if (ActionController.IsSessionExpired(Page))
           ActionController.RedirctToLogin(Page);
       else
       {
           ExportToExcel(str, "Pending Service PO Report");
       }
   }
   protected void btnClear_Click(object sender, EventArgs e)
   {
       txt_f_date.Value = "";
       txt_t_date.Value = "";
       ddl_Vendor.SelectedIndex = 0;
       divdata.InnerHtml = null;
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
           DataTable dt = (DataTable)ActionController.ExecuteAction("", "Pending_Service_PO.aspx", "getdata", ref isValid, txt_f_date.Value, txt_t_date.Value,ddl_Vendor.SelectedValue);
           if (dt.Rows.Count > 0)
           {
               str.Append("<table id='data-table1' class='table table-bordered table-hover'><thead><tr class='grey'><th> #</th><th>Vendor Name</th><th>Service No.</th><th>Service Text</th><th>PO Number</th><th>PO Date</th><th>Service Entry Date</th><th>Plant</th><th>Payment Terms</th></tr> ");
               str.Append("</thead><tbody>");
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   str.Append(" <tr>");
                   str.Append("<td>" + (i + 1) + "</td>");
                   str.Append("<td>" + dt.Rows[i]["Vendor_Name"].ToString() + "</td>");
                   str.Append("<td>" + dt.Rows[i]["SERVICE_NO"].ToString() + "</td>");
                   str.Append("<td>" + dt.Rows[i]["SHORT_TEXT"].ToString() + "</td>");
                   str.Append("<td>" + dt.Rows[i]["PO_NO"].ToString() + "</td>");
                   str.Append("<td>" + dt.Rows[i]["PO_SUBMITED_DATE"].ToString() + "</td>");
                   str.Append("<td>" + dt.Rows[i]["budat"].ToString() + "</td>");
                   str.Append("<td>" + dt.Rows[i]["PLANT"].ToString() + "</td>");
                   str.Append("<td>" + dt.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
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