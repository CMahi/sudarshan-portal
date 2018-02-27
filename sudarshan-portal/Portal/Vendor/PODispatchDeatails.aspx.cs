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



public partial class PODispatchDeatails : System.Web.UI.Page
{
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(PODispatchDeatails));
                if (!IsPostBack)
                {
                   // sp_LoginUser.InnerHtml = Session["USER_NAME"].ToString();
                    //txt_Username.Text = sp_LoginUser.InnerHtml;
                    txt_Username.Text = Convert.ToString(Session["User_ADID"]);
                    OpenPO();
                    DisplayHeader();
                    GetDocument();
                    fillDocument_Details();
                }
            }
        }
        catch (Exception Exc) {  }
    }

    private void fillDocument_Details()
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                string isData = string.Empty;
                string isValid = string.Empty;
                string DisplayData = string.Empty;

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>Document Name</th><th>File Name</th><th>Delete</th></tr></thead>";
                DisplayData += "</table>";
                div_Doc.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    private void GetDocument()
    {
        string IsInsert = string.Empty;
        DataTable DTS = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getdocument", ref IsInsert);
        if(DTS != null && DTS.Rows.Count > 0)
        {
            ListItem li = new ListItem("--Select One--", "");
            ddl_Document.DataSource = DTS;
            ddl_Document.DataValueField = "DOCUMENT_NAME";
            ddl_Document.DataTextField = "DOCUMENT_NAME";
            ddl_Document.DataBind();
            ddl_Document.Items.Insert(0, li);

        }
    }

    private void DisplayHeader()
    {
        string IsInsert = string.Empty;
        StringBuilder HTML = new StringBuilder();
        DataTable DTS = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getheader", ref IsInsert, txt_Po_No.Text, txt_Username.Text.Trim());
        {
            HTML.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Vendor Name</th><th>Vendor Code</th><th>PO No</th><th>Date</th><th>Currency</th><th>Created By</th><th>PO Type</th><th>INCO Terms</th><th>PO Value</th><th>Tax</th><th>PO GV</th><th>Payment Terms</th></tr></thead>");
            HTML.Append("<tbody>");
            if (DTS != null && DTS.Rows.Count > 0)
            {
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    HTML.Append("<tr><td>" + DTS.Rows[i]["VENDOR_NAME"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["VENDOR_CODE"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["PO_NUMBER"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["CREATED_DATE"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["CURRENCY"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["CREATED_BY"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["PO_TYPE"].ToString() + "</td>");
                    HTML.Append("<td><a href='#incoterm' data-toggle='modal' onclick='getIncoTerm()'>INCO Terms</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["PO_VALUE"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["TAX"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["PO_GV"].ToString() + "</td>");
                    HTML.Append("<td><a href='#paymentterm' data-toggle='modal' onclick='getPaymentTerm()'>" + DTS.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
                    HTML.Append("</tr>");               
                }
            }
           
            HTML.Append("</tbody>");
		    HTML.Append("</table>");
        }
        div_Header.InnerHtml = HTML.ToString();
	
    }

    private void OpenPO()
    {
        string IsData = string.Empty;
        StringBuilder str = new StringBuilder();
        string ad_id = Convert.ToString(Session["USER_ADID"]);
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getpo", ref IsData, ad_id.Trim());
        txt_Po_No.Text = dt.Rows[0]["PO_NUMBER"].ToString();
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str.Append("<div class='form-group demodemo'>");
                 if (i == 0)
                 {
                     str.Append("<div class='col-md-2'><input id='optionsRadios'  name='optionsRadios' value='" + dt.Rows[i]["PO_NUMBER"].ToString() + "' type='radio' Onclick='getheader()' checked ></div>");
                 }
                 else
                 {
                     str.Append("<div class='col-md-2'><input id='optionsRadios'  name='optionsRadios' value='" + dt.Rows[i]["PO_NUMBER"].ToString() + "' type='radio' Onclick='getheader()'></div>");
                 }
                 str.Append("<div class='col-md-10'><input class='form-control' id='checkValue' placeholder='" + dt.Rows[i]["PO_NUMBER"].ToString() + "' value='" + dt.Rows[i]["PO_NUMBER"].ToString() + "' disabled='' type='text'></div>");
                 str.Append("</div>");
            }

        }
      
        div_po.InnerHtml = str.ToString();
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillVendor(string po_no,string vendor_name)
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getheader", ref isValid, po_no, vendor_name);
                HTML.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Vendor Name</th><th>Vendor Code</th><th>PO No</th><th>Date</th><th>Currency</th><th>Created By</th><th>PO Type</th><th>INCO Terms</th><th>PO Value</th><th>Tax</th><th>PO GV</th><th>Payment Terms</th></tr></thead>");
                HTML.Append("<tbody>");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            HTML.Append("<tr class='demo1'><td>" + dt.Rows[i]["VENDOR_NAME"].ToString() + "</td>");
                            HTML.Append("<td>" + dt.Rows[i]["VENDOR_CODE"].ToString() + "</td>");
                            HTML.Append("<td>" + dt.Rows[i]["PO_NUMBER"].ToString() + "</td>");
                            HTML.Append("<td>" + dt.Rows[i]["CREATED_DATE"].ToString() + "</td>");
                            HTML.Append("<td>" + dt.Rows[i]["CURRENCY"].ToString() + "</td>");
                            HTML.Append("<td>" + dt.Rows[i]["CREATED_BY"].ToString() + "</td>");
                            HTML.Append("<td>" + dt.Rows[i]["PO_TYPE"].ToString() + "</td>");
                            HTML.Append("<td><a href='#incoterm' data-toggle='modal' onclick='getIncoTerm()'>INCO Terms</td>");
                            HTML.Append("<td>" + dt.Rows[i]["PO_VALUE"].ToString() + "</td>");
                            HTML.Append("<td>" + dt.Rows[i]["TAX"].ToString() + "</td>");
                            HTML.Append("<td>" + dt.Rows[i]["PO_GV"].ToString() + "</td>");
                            HTML.Append("<td><a href='#paymentterm' data-toggle='modal'>" + dt.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
                            HTML.Append("</tr>");
                         
                        }
                    }
                    HTML.Append("</tbody>");
                    HTML.Append("</table>");     
             }

            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
       
        DisplayData = HTML.ToString();
        return DisplayData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillDetail(string po_no)
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getdetail", ref isValid, po_no);
                HTML.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Sr.NO</th><th>Material Number</th><th>Plant</th><th>Storage Location</th><th>Quantity</th><th>UOM</th><th>Net Price</th><th>Amount</th><th>Material Group</th><th>Dispatch Qty1</th><th>Dispatch Qty</th><th>GR Qty</th><th>Schedule</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HTML.Append("<tr><td>" + (i + 1) + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["MATERIAL_NO"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["PLANT"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["STORAGE_LOCATION"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["QUANTITY"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["UOM"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["NET_PRICE"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["MATERIAL_GROUP"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["DISPATCH_QUANTITY"].ToString() + "</td>");
                        HTML.Append("<td><input class='form-control' type='text'></td>");
                        HTML.Append("<td>" + dt.Rows[i]["GR_QUANTITY"].ToString() + "</td>");
                        HTML.Append("<td><a href='#schedule' data-toggle='modal' onclick='getSchedule()'><img src='../../Img/index.jpg' style='margin-left:10px' height='20' width='20' alt='Smiley face'  title='Question'></a></td>");
                        HTML.Append("</tr>");
                    }
                }
                HTML.Append("</tbody>");
                HTML.Append("</table>");
            }

            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }

        DisplayData = HTML.ToString();
        return DisplayData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillInco(string po_no)
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getinco", ref isValid, po_no);
                HTML.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Inco Terms (Part 1)</th><th>Inco Terms (Part 2)</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HTML.Append("<tr>");
                        HTML.Append("<td>" + dt.Rows[i]["INCO_TERMS1"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["INCO_TERMS2"].ToString() + "</td>");
                        HTML.Append("</tr>");
                    }
                }
                HTML.Append("</tbody>");
                HTML.Append("</table>");
            }

            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }

        DisplayData = HTML.ToString();
        return DisplayData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillPayment(string po_no)
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getpayment_term", ref isValid, po_no);
                HTML.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Day Limit</th><th>Calendar Day for Payment</th><th>Days from Baseline Date for Payment</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HTML.Append("<tr>");
                        HTML.Append("<td>" + dt.Rows[i]["ZTAGG"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["ZFAEL"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["ZTAG1"].ToString() + "</td>");
                        HTML.Append("</tr>");
                    }
                }
                HTML.Append("</tbody>");
                HTML.Append("</table>");
            }

            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }

        DisplayData = HTML.ToString();
        return DisplayData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillschedule(string po_no)
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getschedule", ref isValid, po_no);
                HTML.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Sr No.</th><th>Delivery Date</th><th>Scheduled Quantity</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HTML.Append("<tr>");
                        HTML.Append("<td> " + (i+1) + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["EINDT"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["S_MENGE"].ToString() + "</td>");
                        HTML.Append("</tr>");
                    }
                }
                HTML.Append("</tbody>");
                HTML.Append("</table>");
            }

            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }

        DisplayData = HTML.ToString();
        return DisplayData;
    }

    
}

