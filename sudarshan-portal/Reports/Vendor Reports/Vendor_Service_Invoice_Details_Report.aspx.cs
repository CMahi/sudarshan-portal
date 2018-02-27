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

public partial class Vendor_Service_Invoice_Details_Report : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Vendor_Service_Invoice_Details_Report));
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

        ddl_Vendor.DataSource = dl;
        ddl_Vendor.DataTextField = "Vendor_Name";
        ddl_Vendor.DataValueField = "Vendor_Name";
        ddl_Vendor.DataBind();
        ddl_Vendor.Items.Insert(0, li);
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

    protected void submit(object sender, EventArgs e)
    {
        StringBuilder str = new StringBuilder();

        try
        {
            string isdata = string.Empty;
            string img_path = "../../Img/plus.png";
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Service_Invoice_Details_Report.aspx", "getvendorinvoicedetails", ref isdata, ddl_Vendor.SelectedValue, ddlStatus.SelectedValue, txt_form_Date.Text, text_To_Date.Text);
            Session["dt"] = dt;
            if (dt != null && dt.Rows.Count > 0)
            {

                str.Append("<table id='tble' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Sr. No.</th><th>Vendor Name</th><th>PO Number</th><th>PO Date</th><th>Cum Inv Amt</th><th>PO GV</th></tr></thead>");
                str.Append("<tbody>");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str.Append("<tr><td><a href='#schedule' data-toggle='modal' onclick='change_Image(" + (i + 1) + ",\"" + Convert.ToString(dt.Rows[i]["Vendor_Code"]) + "\",\"" + Convert.ToString(dt.Rows[i]["Po_Number"]) + "\",\"" + ddlStatus.SelectedValue + "\")'><img id='imgExpand" + (i + 1) + "' src='../../Img/plus.png'/></td>");
                    str.Append("<td align='left' style='width:0.5%'><input class='hidden' id='txt_id' type='text' value=" + (i + 1) + ">" + (i + 1) + "</td>");
                    str.Append("<td align='left'><input class='hidden' id='txt_Vendor_Code' type='text' value=" + dt.Rows[i]["Vendor_Code"].ToString() + ">" + Convert.ToString(dt.Rows[i]["Vendor_Name"]) + "</td>");
                    str.Append("<td align='left'><input class='hidden' id='txt_PO_Number' type='text' value=" + dt.Rows[i]["PO_NUMBER"].ToString() + ">" + Convert.ToString(dt.Rows[i]["Po_Number"]) + "</td>");
                    str.Append("<td align='left'>" + Convert.ToString(dt.Rows[i]["PO_Date"]) + "</td>");
                    str.Append("<td align='left'>" + Convert.ToString(dt.Rows[i]["Cum_Amount"]) + "</td>");
                    str.Append("<td align='left'>" + Convert.ToString(dt.Rows[i]["PO_GV"]) + "</td></tr>");
                }
                str.Append("</tbody></table>");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(update, this.GetType(), "script", "{alert('Record Not Found...!')}", true);
                
            }
        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#tble').dataTable({'bSort': false,'pageLength' :100 });", true);
        txt_Export_HDR.Text = Convert.ToString(str);
       // divIns.Style.Add("display", "none");
        div_reportDetails.InnerHtml = Convert.ToString(str);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillDetail(int id, string username, string po, string status)
    {
        string returnData = string.Empty;
        string strData = string.Empty;
        string isdata = string.Empty;
        string isdata1 = string.Empty;

        StringBuilder html = new StringBuilder();
        try
        {
            html.Append("<table class='table table-bordered ' width='100%' id='tbl_InvoiceDtl' name='data_tbl'>");
            html.Append("<tr class='btn-default' style='background-color:ButtonFace; text-align:center; color:Black;'><th style='text-align:center'>#</th><th style='text-align:center'>Dispatch Request No</th><th style='text-align:center'>Unique No</th><th style='text-align:center'>Invoice No</th><th style='text-align:center'>Invoice Date</th><th style='text-align:center'>Invoice Amount</th><th style='text-align:center'>Plant</th><th style='text-align:center'>Status</th></tr>");
            html.Append("<tbody>");
            DataTable dtPO = new DataTable();
            dtPO = (DataTable)ActionController.ExecuteAction("", "Vendor_Service_Invoice_Details_Report.aspx", "getinvoicedetailspo", ref isdata, username, po, status);
            Session["dtData"] = dtPO;
            if (dtPO != null)
            {
                for (int j = 0; j < dtPO.Rows.Count; j++)
                {
                    string str_status = Convert.ToString(dtPO.Rows[j]["Status"]);
                    StringBuilder img_Data = new StringBuilder();

                    string phdr = "";
                    string pdtl = "";

                    img_Data.Append("<table><tbody><tr>");

                    if (str_status == "Transition")
                    {
                        img_Data.Append("<tr><th class='imgBtn'>TR</th><th class='imgBtn'>SES</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                        img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/1.png' title='Service Entry Sheet'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td></tr>");
                    }
                    else if (str_status == "Service Entry Sheet")
                    {
                        img_Data.Append("<tr><th class='imgBtn'>TR</th><th class='imgBtn'>SES</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                        img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='Service Entry Sheet'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td></tr>");
                    }
                    else
                    {
                        string dtImg = string.Empty;
                        if (str_status == "Payment Processed")
                        {
                            img_Data.Append("<tr><th class='imgBtn'>TR</th><th class='imgBtn'>SES</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                            img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='Service Entry Sheet'/></a></td><td><a href='#'><img src='../../images/2.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/2.png' title='Payment Processed'/></a></td></tr>");
                        }
                        else
                        {
                            img_Data.Append("<tr><th class='imgBtn'>TR</th><th class='imgBtn'>SES</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                            img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='Service Entry Sheet'/></a></td><td><a href='#'><img src='../../images/2.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td></tr>");
                        }
                    }
                    img_Data.Append("</tbody></table>");
                    html.Append("<tr style='vertical-align:middle'><td style='text-align:center'>" + (j + 1) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["SERV_PO_NO"]) + "</a></td><td style='text-align:left'>" + Convert.ToString(dtPO.Rows[j]["UNIQUE_ID"]) + "</td><td style='text-align:left'>" + Convert.ToString(dtPO.Rows[j]["Invoice_No"]) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["Invoice_Date"]) + "</td><td style='text-align:right'>" + Convert.ToString(dtPO.Rows[j]["Invoice_Amount"]) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["PLANT"]) + "</td><td style='text-align:center;'>" + Convert.ToString(img_Data) + "<input type='text' id='pk_Dispatch" + (j + 1) + "' value=" + Convert.ToString(dtPO.Rows[j]["PK_SERV_PO_HDR_ID"]) + " style='display:none'></td></tr>");
                }
            }
            html.Append("</tbody></table>");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        strData = Convert.ToString(html);
        returnData = strData;
        return returnData;
    }

    protected void btn_Export_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcel(str, "Vendor Invoice Detail Report");
        }
    }

    protected void ExportToExcel(StringBuilder dgview, string filename)
    {
        try
        {
            string isdata = string.Empty;
            string isdata1 = string.Empty;
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            Response.Write("<table id='tble' class='table table-bordered'><thead><tr class='grey'><th>Sr. No.</th><th>Vendor Name</th><th>PO Number</th><th>PO Date</th><th>Cum Inv Amt</th><th>PO GV</th></tr></thead>");
            Response.Write("<tbody>");
            DataTable dtPO = new DataTable();
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Service_Invoice_Details_Report.aspx", "getvendorinvoicedetails", ref isdata1, ddl_Vendor.SelectedValue, ddlStatus.SelectedValue, txt_form_Date.Text, text_To_Date.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Response.Write("<tr>");
                    Response.Write("<td align='left' style='width:0.5%'>" + (i + 1) + "</td>");
                    Response.Write("<td align='left'>" + Convert.ToString(dt.Rows[i]["Vendor_Name"]) + "</td>");
                    Response.Write("<td align='left'>" + Convert.ToString(dt.Rows[i]["Po_Number"]) + "</td>");
                    Response.Write("<td align='left'>" + Convert.ToString(dt.Rows[i]["PO_Date"]) + "</td>");
                    Response.Write("<td align='left'>" + Convert.ToString(dt.Rows[i]["Cum_Amount"]) + "</td>");
                    Response.Write("<td align='left'>" + Convert.ToString(dt.Rows[i]["PO_GV"]) + "</td></tr>");

                    Response.Write("<table class='table table-bordered ' width='100%' id='tbl_InvoiceDtl' name='data_tbl'>");
                    Response.Write("<tr class='btn-default' style='background-color:ButtonFace; text-align:center; color:Black;'><th style='text-align:center'>#</th><th style='text-align:center'>Dispatch Request No</th><th style='text-align:center'>Unique No</th><th style='text-align:center'>Invoice No</th><th style='text-align:center'>Invoice Date</th><th style='text-align:center'>Invoice Amount</th><th style='text-align:center'>Plant</th><th style='text-align:center'>Status</th></tr>");
                    Response.Write("<tbody>");
                    dtPO = (DataTable)ActionController.ExecuteAction("", "Vendor_Service_Invoice_Details_Report.aspx", "getinvoicedetailspo", ref isdata, Convert.ToString(dt.Rows[i]["Vendor_Code"]), Convert.ToString(dt.Rows[i]["PO_Number"]), ddlStatus.SelectedValue);
                    Session["dtData"] = dtPO;
                    if (dtPO != null)
                    {
                        for (int j = 0; j < dtPO.Rows.Count; j++)
                        {
                            string str_status = Convert.ToString(dtPO.Rows[j]["Status"]);
                            StringBuilder img_Data = new StringBuilder();

                            string phdr = "";
                            string pdtl = "";

                            img_Data.Append("<table><tbody><tr>");

                            if (str_status == "Transition")
                            {
                                img_Data.Append("<tr><th class='imgBtn'>TR</th><th class='imgBtn'>SES</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                                img_Data.Append("<tr><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/2.png' title='Transition'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/1.png' title='Service Entry Sheet'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/1.png' title='Payment Processed'/></a></td></tr>");
                            }
                            else if (str_status == "Service Entry Sheet")
                            {
                                img_Data.Append("<tr><th class='imgBtn'>TR</th><th class='imgBtn'>SES</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                                img_Data.Append("<tr><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/2.png' title='Transition'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/2.png' title='Service Entry Sheet'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/1.png' title='Payment Processed'/></a></td></tr>");
                            }
                            else
                            {
                                string dtImg = string.Empty;
                                if (str_status == "Payment Processed")
                                {
                                    img_Data.Append("<tr><th class='imgBtn'>TR</th><th class='imgBtn'>SES</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                                    img_Data.Append("<tr><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/2.png' title='Transition'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/2.png' title='Service Entry Sheet'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/2.png' title='Bill Booked'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/2.png' title='Payment Processed'/></a></td></tr>");
                                }
                                else
                                {
                                    img_Data.Append("<tr><th class='imgBtn'>TR</th><th class='imgBtn'>SES</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                                    img_Data.Append("<tr><td><a href='#'><img src='C:/Development/Sudarshan-Portal/images/2.png' title='Transition'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/2.png' title='Service Entry Sheet'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/2.png' title='Bill Booked'/></a></td><td><a href='#'><img src='file:///C:/Development/Sudarshan-Portal/images/1.png' title='Payment Processed'/></a></td></tr>");
                                }
                            }
                            img_Data.Append("</tbody></table>");
                            Response.Write("<tr style='vertical-align:middle'><td style='text-align:center'>" + (j + 1) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["SERV_PO_NO"]) + "</a></td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["UNIQUE_ID"]) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["Invoice_No"]) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["Invoice_Date"]) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["Invoice_Amount"]) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["PLANT"]) + "</td><td style='text-align:center;'>" + Convert.ToString(img_Data) + "</td></tr>");
                        }
                    }
                }
            }
            Response.Write("</tbody></table>");
            Response.End();
        }

        catch (Exception ex)
        {
            //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');</script>");
            //return;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        div_reportDetails.InnerHtml = "";
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
}