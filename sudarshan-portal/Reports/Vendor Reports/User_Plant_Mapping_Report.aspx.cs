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

public partial class User_Plant_Mapping_Report : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    StringBuilder str = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(User_Plant_Mapping_Report));
                    if (!Page.IsPostBack)
                    {
                        if (Session["USER_ADID"] != null)
                        {
                            txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                            txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        }
                        app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                        app_Authority.Text = HttpContext.Current.Request.Url.Authority;
                        fillddl_Vendor();
                       
                      
                    }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    private void fillddl_Vendor()
    {
        string isdata1 = string.Empty;
        ListItem li = new ListItem("--Select One--", "");
        DataSet ds = (DataSet)ActionController.ExecuteAction("", "User_Plant_Mapping_Report.aspx", "getvendor", ref isdata1);
        DataTable dl = ds.Tables[0];
        DataTable dd = ds.Tables[1];
        DataTable dm = ds.Tables[2];

        ddl_Vendor.DataSource = dl;
        ddl_Vendor.DataTextField = "Vendor_Name";
        ddl_Vendor.DataValueField = "Vendor_Name";
        ddl_Vendor.DataBind();
        ddl_Vendor.Items.Insert(0, li);

        ddl_PO.DataSource = dm;
        ddl_PO.DataTextField = "PO_NUMBER";
        ddl_PO.DataValueField = "PO_NUMBER";
        ddl_PO.DataBind();
        ddl_PO.Items.Insert(0, li);

        ddl_Plant.DataSource = dd;
        ddl_Plant.DataTextField = "Plant";
        ddl_Plant.DataValueField = "Plant";
        ddl_Plant.DataBind();
        ddl_Plant.Items.Insert(0, li);
    
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=4");

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string GetCurrentTime(int name)
    {
        string data = string.Empty;
        try
        {
            GetData getData = new GetData();
            data = getData.get_Dispatch_Details(name);
        }
        catch (Exception ex)
        {

        }
        return data;
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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string viewVendor(string vcode)
    {
        string data = string.Empty;
        try
        {
            GetData getData = new GetData();
            data = getData.viewVendor(vcode);
        }
        catch (Exception ex)
        {

        }
        return data;
    }

    protected void btn_SubmitData(object sender, EventArgs e)
    {
        string strData = string.Empty;
        string isdata = string.Empty;
        string isdata1 = string.Empty;

        StringBuilder html = new StringBuilder();
        try
        {
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "User_Plant_Mapping_Report.aspx", "getvendorname", ref isdata,ddl_Vendor.SelectedValue, ddl_Plant.SelectedValue, ddl_PO.SelectedValue, txt_form_Date.Text, text_To_Date.Text);
            Session["dtData"] = dtData;
            DataSet ds = (DataSet)ActionController.ExecuteAction("", "User_Plant_Mapping_Report.aspx", "getvendor", ref isdata1);
            DataTable dl = ds.Tables[0];
            DataTable dm = ds.Tables[1];
            DataTable dd = ds.Tables[2];
            html.Append("<table class='table table-bordered table-hover' id='disp' >");
            html.Append("<thead><tr class='grey center'>");
            html.Append("<th>Vendor Name</th>");
            html.Append("<th>PO No</th>");
            html.Append("<th>PO Gross Amt</th><th>Plant</th><th>Unique No</th><th>Dispatch Note</th><th>Invoice No</th><th>Invoice Amt</th><th>Dispatch Quantity</th><th>Dispatch Date</th><th style='display:none'>Status</th></tr></thead>");
            html.Append("<tbody>");

            if (dtData != null && dtData.Rows.Count > 0)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtData.Rows[i]["PO_Number"]));
                    html.Append("<tr><td><a href='#vendorinfo' role='button' data-toggle='modal' id='veninfo" + (i + 1) + "' onclick='viewVendor(" + (i + 1) + ")' >" + Convert.ToString(dtData.Rows[i]["vendor_name"]) + "</a></td><td><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dtData.Rows[i]["PO_Number"]) + "</a></td><td style='text-align: right;'>" + Convert.ToString(dtData.Rows[i]["PO_GV"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["Plant"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["Unique_No"]) + "</td><td><a href='#paymentterm' role='button' data-toggle='modal' onclick='setSelectedNote(" + Convert.ToString(dtData.Rows[i]["PK_Dispatch_Note_ID"]) + ")'>" + Convert.ToString(dtData.Rows[i]["request_id"]) + "</a></td><td>" + Convert.ToString(dtData.Rows[i]["Invoice_No"]) + "</td><td style='text-align: right;'>" + Convert.ToString(dtData.Rows[i]["Invoice_Amount"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["Dispatch_Quantity"]) + "</td><td style='text-align: right;'>" + Convert.ToString(dtData.Rows[i]["Invoice_Date"]) + "</td><td style='text-align: right; display:none'><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'><input type='text' id='venCode" + (i + 1) + "' value=" + Convert.ToString(dtData.Rows[i]["vendor_code"]) + " style='display:none'></td></tr>");
                }
            }
          
            html.Append("</tbody>");
            html.Append("</table>");
            strData = Convert.ToString(html);
            ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#disp').dataTable({'bSort': false});", true);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
					    divIns.Style.Add("display", "none");
        div_ReportDetails.InnerHtml = Convert.ToString(strData);

    }

    protected void btn_Export_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcel(str, "Vendor Dispatch Report");
        }
    }

    protected void ExportToExcel(StringBuilder dgview, string filename)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["dtData"];
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            //dt = txt_Export.Text;
            if (dt.Rows.Count > 0 )
            {
                Response.Write("<table border=\"1\"><tr><td colspan='8' align='center' style=\"font-weight:bold;font-size:'20';\">Vendor Dispatch Report</td></tr>");
                Response.Write("<tr style=\"font-weight:bold\"><td colspan='8'></td></tr>");
                //Response.Write("<table class='table table-bordered table-hover' >");
                Response.Write("<thead><tr class='grey center'>");
                Response.Write("<th>Vendor Name</th>");
                Response.Write("<th>PO No</th>");
                Response.Write("</select></th>");
                Response.Write("<th>PO Gross Amt</th><th>Plant</th><th>Unique No</th><th>Dispatch Note</th><th>Invoice No</th><th>Invoice Amt</th><th>Dispatch Quantity</th><th>Dispatch Date</th></tr></thead>");
                Response.Write("<tbody>");

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string encrypt_Str = crypt.Encryptdata(Convert.ToString(dt.Rows[i]["PO_Number"]));
                        Response.Write("<tr><td><a href='#vendorinfo' role='button' data-toggle='modal' id='veninfo" + (i + 1) + "' onclick='viewVendor(" + (i + 1) + ")' >" + Convert.ToString(dt.Rows[i]["vendor_name"]) + "</a></td><td><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dt.Rows[i]["PO_Number"]) + "</a></td><td style='text-align: right;'>" + Convert.ToString(dt.Rows[i]["PO_GV"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["Plant"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["Unique_No"]) + "</td><td><a href='#paymentterm' role='button' data-toggle='modal' onclick='setSelectedNote(" + Convert.ToString(dt.Rows[i]["PK_Dispatch_Note_ID"]) + ")'>" + Convert.ToString(dt.Rows[i]["request_id"]) + "</a></td><td>" + Convert.ToString(dt.Rows[i]["Invoice_No"]) + "</td><td style='text-align: right;'>" + Convert.ToString(dt.Rows[i]["Invoice_Amount"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["Dispatch_Quantity"]) + "</td><td style='text-align: right;'>" + Convert.ToString(dt.Rows[i]["Invoice_Date"]) + "</td></tr>");
                    }
                }

                Response.Write("</tbody>");
                Response.Write("</table>");
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
         div_ReportDetails.InnerHtml = "";
    }

    protected void selectedpo(object sender, EventArgs e)
    {
        string isdata1 = string.Empty;
        ListItem li = new ListItem("--Select One--", "");
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "User_Plant_Mapping_Report.aspx", "getselpo", ref isdata1,ddl_Vendor.SelectedValue);
         ddl_PO.DataSource = dt;
        ddl_PO.DataTextField = "PO_NUMBER";
        ddl_PO.DataValueField = "PO_NUMBER";
        ddl_PO.DataBind();
        ddl_PO.Items.Insert(0, li);
    }

}