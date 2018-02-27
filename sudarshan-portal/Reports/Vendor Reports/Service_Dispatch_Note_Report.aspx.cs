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

public partial class Service_Dispatch_Note_Report : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Service_Dispatch_Note_Report));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    fillddl_Vendor();

                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    private void fillddl_Vendor()
    {
        string isdata1 = string.Empty;
        ListItem li = new ListItem("--Select One--", "0");
        DataSet ds = (DataSet)ActionController.ExecuteAction("", "Service_Dispatch_Note_Report.aspx", "getvendor", ref isdata1);
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
            data = getData.get_Service_Dispatch_Details(name);
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
            filename = (string)ActionController.ExecuteAction("", "Service_Dispatch_Note_Report.aspx", "getname", ref isdata, name);
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
            DataTable dtvalue = (DataTable)ActionController.ExecuteAction("", "Service_Dispatch_Note_Report.aspx", "getservicedata", ref isdata, ddl_Vendor.SelectedValue, ddl_Plant.SelectedValue, ddl_PO.SelectedValue, txt_f_date.Value, txt_t_date.Value);
            Session["dtData"] = dtvalue;
            html.Append("<table class='table table-bordered table-hover' id='disp' >");
            html.Append("<thead><tr class='grey center'>");
            html.Append("<th>Vendor Name</th>");
            html.Append("<th>PO No</th>");
            html.Append("<th>PO Gross Amt</th><th>Plant</th><th>Unique No</th><th>Service No</th><th>Invoice No</th><th>Invoice Amt</th><th>Dispatch Quantity</th><th>Invoice Date</th><th style='display:none'>Status</th></tr></thead>");
            html.Append("<tbody>");

            if (dtvalue != null && dtvalue.Rows.Count > 0)
            {
                for (int i = 0; i < dtvalue.Rows.Count; i++)
                {
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtvalue.Rows[i]["PO_Number"]));
                    html.Append("<tr><td><a href='#vendorinfo' role='button' data-toggle='modal' id='veninfo" + (i + 1) + "' onclick='viewVendor(" + (i + 1) + ")' >" + Convert.ToString(dtvalue.Rows[i]["vendor_name"]) + "</a></td><td><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dtvalue.Rows[i]["PO_Number"]) + "</a></td><td style='text-align: right;'>" + Convert.ToString(dtvalue.Rows[i]["PO_GV"]) + "</td><td>" + Convert.ToString(dtvalue.Rows[i]["Plant"]) + "</td><td>" + Convert.ToString(dtvalue.Rows[i]["UNIQUE_ID"]) + "</td><td><a href='#paymentterm' role='button' data-toggle='modal' onclick='setSelectedNote(" + Convert.ToString(dtvalue.Rows[i]["PK_SERV_PO_HDR_ID"]) + ")'>" + Convert.ToString(dtvalue.Rows[i]["SERV_PO_NO"]) + "</a></td><td>" + Convert.ToString(dtvalue.Rows[i]["Invoice_No"]) + "</td><td style='text-align: right;'>" + Convert.ToString(dtvalue.Rows[i]["Invoice_Amount"]) + "</td><td>" + Convert.ToString(dtvalue.Rows[i]["QUANTITY"]) + "</td><td style='text-align: right;'>" + Convert.ToString(dtvalue.Rows[i]["INVOICE_DATE"]) + "</td><td style='text-align: right; display:none'><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'><input type='text' id='venCode" + (i + 1) + "' value=" + Convert.ToString(dtvalue.Rows[i]["vendor_code"]) + " style='display:none'></td></tr>");
                }
            }

            html.Append("</tbody>");
            html.Append("</table>");
            strData = Convert.ToString(html);

            ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#disp').dataTable({'bSort': false,'pageLength': 100});", true);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        div_ReportDetails.InnerHtml = Convert.ToString(strData);
        divIns.Style.Add("display", "none");
    }

    protected void btn_Export_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcel(str, "Service Dispatch Note Report");
        }
    }

    protected void ExportToExcel(StringBuilder dgview, string filename)
    {
        try
        {
            string strData = string.Empty;
            string isdata = string.Empty;
            string isdata1 = string.Empty;
            StringBuilder html = new StringBuilder();
            DataTable dtvalue = (DataTable)ActionController.ExecuteAction("", "Service_Dispatch_Note_Report.aspx", "getservicedata", ref isdata, ddl_Vendor.SelectedValue, ddl_Plant.SelectedValue, ddl_PO.SelectedValue, txt_f_date.Value, txt_t_date.Value);
            Session["dtData"] = dtvalue;
            html.Append("<table class='table table-bordered table-hover' id='disp' >");
            html.Append("<thead><tr class='grey center'>");
            html.Append("<th>Vendor Name</th>");
            html.Append("<th>PO No</th>");
            html.Append("<th>PO Gross Amt</th><th>Plant</th><th>Unique No</th><th>Service No</th><th>Invoice No</th><th>Invoice Amt</th><th>Dispatch Quantity</th><th>Invoice Date</th></tr></thead>");
            html.Append("<tbody>");

            if (dtvalue != null && dtvalue.Rows.Count > 0)
            {
                for (int i = 0; i < dtvalue.Rows.Count; i++)
                {
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtvalue.Rows[i]["PO_Number"]));
                    html.Append("<tr><td>" + Convert.ToString(dtvalue.Rows[i]["vendor_name"]) + "</td><td>" + Convert.ToString(dtvalue.Rows[i]["PO_Number"]) + "</td><td style='text-align: right;'>" + Convert.ToString(dtvalue.Rows[i]["PO_GV"]) + "</td><td>" + Convert.ToString(dtvalue.Rows[i]["Plant"]) + "</td><td>" + Convert.ToString(dtvalue.Rows[i]["UNIQUE_ID"]) + "</td><td>" + Convert.ToString(dtvalue.Rows[i]["SERV_PO_NO"]) + "</td><td>" + Convert.ToString(dtvalue.Rows[i]["Invoice_No"]) + "</td><td style='text-align: right;'>" + Convert.ToString(dtvalue.Rows[i]["Invoice_Amount"]) + "</td><td>" + Convert.ToString(dtvalue.Rows[i]["QUANTITY"]) + "</td><td style='text-align: right;'>" + Convert.ToString(dtvalue.Rows[i]["INVOICE_DATE"]) + "</td></tr>");
                }
            }

            html.Append("</tbody>");
            html.Append("</table>");
            strData = Convert.ToString(html);
            string dt = string.Empty;
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            dt = strData.ToString();
            if (dt != null)
            {
                Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                                   "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
                Response.Write(dt);
            }
            Response.End();
        }

        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');</script>");
            return;
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public void btnClear_Click()
    {
    }

    //[AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    //public DataTable selectedpo(string vname)
    //{
    //     string isdata1 = string.Empty;
    //    DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_Dispatch_Note_Report.aspx", "getselpo", ref isdata1, vname);
    //    string dtvalu=dt.ToString();
    //    return dt;
    //}
    protected void selectedpo(object sender, EventArgs e)
    {
        string isdata1 = string.Empty;
        ListItem li = new ListItem("--Select One--", "0");
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_Dispatch_Note_Report.aspx", "getselpo", ref isdata1, ddl_Vendor.SelectedValue);

        ddl_PO.DataSource = dt;
        ddl_PO.DataTextField = "PO_NUMBER";
        ddl_PO.DataValueField = "PO_NUMBER";
        ddl_PO.DataBind();
        ddl_PO.Items.Insert(0, li);
        div_ReportDetails.InnerHtml = null;

    }

}