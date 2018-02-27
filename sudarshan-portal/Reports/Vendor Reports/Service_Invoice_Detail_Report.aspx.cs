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

public partial class Service_Invoice_Detail_Report : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Service_Invoice_Detail_Report));
                    if (!Page.IsPostBack)
                    {
                        if (Session["USER_ADID"] != null)
                        {
                            txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                            txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        }

                        app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                        app_Authority.Text= HttpContext.Current.Request.Url.Authority;
                    }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
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
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_Invoice_Detail_Report.aspx", "getinvoicedetails", ref isdata,txt_Username.Text, ddlStatus.SelectedValue, txt_form_Date.Text, text_To_Date.Text);
            
            if (dt != null && dt.Rows.Count > 0)
            {

                str.Append("<table id='tble' class='table table-bordered' name='data_tbl'><thead><tr class='grey'><th>#</th><th>Sr. No.</th><th>PO Number</th><th>PO Date</th><th>Invoice Date</th><th>Cum Inv Amt</th><th>PO GV</th></tr></thead>");
                str.Append("<tbody>");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   // str.Append("<tr><td><input type='text' id='pk_img" + (i + 1) + "' value='0' style='display:none'><img id='imgExpand" + (i + 1) + "' src='../../Img/plus.png' onclick='change_Image(" + (i + 1) + ",\"" + txt_Username.Text + "\",\"" + Convert.ToString(dt.Rows[i]["Po_Number"]) + "\",\"" + ddlStatus.SelectedValue + "\")' /></td>");//
                    str.Append("<tr><td><a href='#schedule' data-toggle='modal' onclick='change_Image(" + (i + 1) + ",\"" + txt_Username.Text + "\",\"" + Convert.ToString(dt.Rows[i]["Po_Number"]) + "\",\"" + ddlStatus.SelectedValue + "\",\"" + Convert.ToString(dt.Rows[i]["PK_SERV_PO_HDR_ID"]) + "\")'><img id='imgExpand" + (i + 1) + "' src='../../Img/plus.png'/></td>");
                    str.Append("<td align='left' style='width:0.5%'><input class='hidden' id='txt_id' type='text' value=" + (i + 1) + ">" + (i + 1) + "</td>");
                    str.Append("<td align='center'><input class='hidden' id='txt_PO_Number' type='text' value=" + dt.Rows[i]["PO_NUMBER"].ToString() + ">" + Convert.ToString(dt.Rows[i]["Po_Number"]) + "</td>");
                    str.Append("<td align='center'>" + Convert.ToString(dt.Rows[i]["PO_Date"]) + "</td>");
                    str.Append("<td align='center'>" + Convert.ToString(dt.Rows[i]["Invoice_Date"]) + "</td>");
                    str.Append("<td align='center'>" + Convert.ToString(dt.Rows[i]["Cum_Amount"]) + "</td>");
                    str.Append("<td align='center'>" + Convert.ToString(dt.Rows[i]["PO_GV"]) + "</td></tr>");
                    //str.Append("<tr><td colspan='6'><div id='Expand" + (i + 1) + "' ></div></td><td class='hidden'></td><td class='hidden'></td><td class='hidden'></td><td class='hidden'></td><td class='hidden'></td><td class='hidden'></td></tr>");
                }
                str.Append("</tbody></table>");
            }
            else
            {
                string message = "alert('Invoice Details Not Available....!')";
                ScriptManager.RegisterClientScriptBlock(update, this.GetType(), "alert", message, true);
            }
        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#tble').dataTable({'bSort': false,'pageLength' : 100});", true);
       // ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#tble').dataTable({ 'columns': [{'className':'details-control','orderable':false,'data':null,'defaultContent': ''},{ 'data': 'Sr. No.' },{ 'data': 'PO Number' },{ 'data': 'PO Date' },{ 'data': 'Cum Inv Amt' },{ 'data': 'PO GV' }],'bSort': false});", true);
        div_reportDetails.InnerHtml = Convert.ToString(str);

    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillDetail(int id, string username, string po, string status, string pk)
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
            dtPO = (DataTable)ActionController.ExecuteAction("", "Service_Invoice_Detail_Report.aspx", "getinvoicedetailspo", ref isdata, username, po, status, pk);
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
                    //<a href='#paymentterm' role='button' data-toggle='modal' onclick='setSelectedNote(" + Convert.ToString(dtPO.Rows[j]["PK_SERV_PO_HDR_ID"]) + ")'>
                }
            }
            html.Append("</tbody></table>");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        strData = Convert.ToString(html);
        returnData = strData;//+ '*' + id;
        return returnData;
    }

    protected void btn_Export_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportToExcel(str, "Service Invoice Detail Report");
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
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Service_Invoice_Detail_Report.aspx", "getinvoicedetails", ref isdata, txt_Username.Text, ddlStatus.SelectedValue, txt_form_Date.Text, text_To_Date.Text);
            if (DTS != null && DTS.Rows.Count > 0)
            {
                Response.Write("<table id='tbleexport' class='table table-bordered'><thead><tr class='grey'><th>Sr. No.</th><th>PO Number</th><th>PO Date</th><th>Cum Inv Amt</th><th>PO GV</th></tr></thead>");
                Response.Write("<tbody>");

                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    Response.Write("<td align='left' style='width:0.5%'>" + (i + 1) + "</td>");
                    Response.Write("<td align='center'>" + Convert.ToString(DTS.Rows[i]["Po_Number"]) + "</td>");
                    Response.Write("<td align='center'>" + Convert.ToString(DTS.Rows[i]["PO_Date"]) + "</td>");
                    Response.Write("<td align='center'>" + Convert.ToString(DTS.Rows[i]["Cum_Amount"]) + "</td>");
                    Response.Write("<td align='center'>" + Convert.ToString(DTS.Rows[i]["PO_GV"]) + "</td></tr>");

                    DataTable dtPO = (DataTable)ActionController.ExecuteAction("", "Service_Invoice_Detail_Report.aspx", "getinvoicedetailspo", ref isdata1,txt_Username.Text, Convert.ToString(DTS.Rows[i]["Po_Number"]), ddlStatus.SelectedValue);
                    if (dtPO != null && dtPO.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtPO.Rows.Count; j++)
                        {
                            string str_status = Convert.ToString(dtPO.Rows[j]["Status"]);
                            StringBuilder img_Data = new StringBuilder();

                            string phdr = "";
                            string pdtl = "";

                            img_Data.Append("<table><tbody><tr>");

                            if (str_status == "Rejected")
                            {
                                img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>SES</th>" + phdr + "<th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                                img_Data.Append("<tr><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Transition'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Service Entry Sheet'/></a></td>" + pdtl + "<td><a href='#'><img href='#' src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/0.png' title='Rejected'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Payment Processed'/></a></td></tr>");
                            }
                            else if (str_status == "Transition")
                            {
                                img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>SES</th>" + phdr + "<th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                                img_Data.Append("<tr><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/2.png' title='Transition'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Service Entry Sheet'/></a></td>" + pdtl + "<td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Rejected'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Payment Processed'/></a></td></tr>");
                            }
                            else if (str_status == "Service Entry Sheet")
                            {
                                img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>SES</th>" + phdr + "<th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                                img_Data.Append("<tr><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/2.png' title='Transition'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/2.png' title='Service Entry Sheet'/></a></td>" + pdtl + "<td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Rejected'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Payment Processed'/></a></td></tr>");
                            }
                            else
                            {
                                string dtImg = string.Empty;
                                if (str_status == "Payment Processed")
                                {
                                    img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>SES</th>" + phdr + "<th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                                    img_Data.Append("<tr><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/2.png' title='Transition'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/2.png' title='Service Entry Sheet'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Rejected'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/2.png' title='Bill Booked'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/2.png' title='Payment Processed'/></a></td></tr>");
                                }
                                else
                                {
                                    img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>SES</th>" + phdr + "<th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th></tr><tbody>");
                                    img_Data.Append("<tr><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/2.png' title='Transition'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/2.png' title='Service Entry Sheet'/></a></td><td></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Rejected'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/2.png' title='Bill Booked'/></a></td><td><a href='#'><img src='c:/Flologic/Projects/Development/Production/Sudarshan-Portal/images/1.png' title='Payment Processed'/></a></td></tr>");
                                }
                            }
                            img_Data.Append("</tbody></table>");
                            Response.Write("<tr style='vertical-align:middle'><td style='text-align:center'>" + (j + 1) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["SERV_PO_NO"]) + "</a></td><td style='text-align:left'>" + Convert.ToString(dtPO.Rows[j]["UNIQUE_ID"]) + "</td><td style='text-align:left'>" + Convert.ToString(dtPO.Rows[j]["Invoice_No"]) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["Invoice_Date"]) + "</td><td style='text-align:right'>" + Convert.ToString(dtPO.Rows[j]["Invoice_Amount"]) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["PLANT"]) + "</td><td style='text-align:center;'>" + Convert.ToString(img_Data) + "</td></tr>");

                        }
                    }
                }
                Response.Write("</tbody>");
                Response.Write("</table>");
            }
            Response.End();
        }

        catch (Exception ex)
        {
            
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