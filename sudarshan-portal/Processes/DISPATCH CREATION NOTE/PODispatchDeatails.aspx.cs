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
using System.Web.Script.Services;
using System.Collections.Generic;

public partial class PODispatchDeatails : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
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
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Session["USER_ADID"].ToString();
                    if (Request.QueryString["processid"] != null && Request.QueryString["stepid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                    }
                    Initialization();
                    OpenPO();
                    DisplayHeader();
                    GetDocument();
                    fillDocument_Details();
                    txt_Invoice_Date.Attributes.Add("ReadOnly", "true");
                    txt_LR_Date.Attributes.Add("ReadOnly","true");
                }
            }
        }
        catch (Exception Exc) {  }
    }

    private void Initialization()
    {
       txt_Invoice_Date.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
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

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>Document Type</th><th>File Name</th><th>Delete</th></tr></thead>";
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
            HTML.Append("<table id='tble' class='table table-bordered'><thead><tr class='grey'><th>PO No</th><th>Date</th><th>Currency</th><th>Created By</th><th>PO Type</th><th>INCO Terms</th><th>PO Value</th><th>PO GV</th><th>Cumulative Amount</th><th>Payment Terms</th></tr></thead>");
            HTML.Append("<tbody>");
            if (DTS != null && DTS.Rows.Count > 0)
            {
                
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(DTS.Rows[i]["PO_Number"]));
                    HTML.Append("<tr><td class='hidden'>" + DTS.Rows[i]["VENDOR_NAME"].ToString() + "</td>");
		    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Vendor_Name' type='text' value=" + DTS.Rows[i]["VENDOR_NAME"].ToString() + "></td>");
                    HTML.Append("<td class='hidden'>" + DTS.Rows[i]["VENDOR_CODE"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Vendor_Code' type='text' value=" + DTS.Rows[i]["VENDOR_CODE"].ToString() + "></td>");
                    HTML.Append("<td><a href='#' role='button' id='anc1' data-toggle='modal' onclick='viewData(" + (i+1) + ")'>" + DTS.Rows[i]["PO_NUMBER"].ToString() + "</a><input type='text' id='encrypt_po_"+ ( i + 1 ) +"' value=" + encrypt_Str + " style='display:none'></td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_Number' type='text' value=" + DTS.Rows[i]["PO_NUMBER"].ToString() + "></td>");
                    HTML.Append("<td>" + DTS.Rows[i]["DATE"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Creation' type='text' value=" + DTS.Rows[i]["CREATED_DATE"].ToString() + "></td>");
                    HTML.Append("<td>" + DTS.Rows[i]["CURRENCY"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["CREATED_BY"].ToString() + "</td>");
                    if (DTS.Rows[i]["ITEM_CATEGORY"].ToString() == "9")
                    {
                        HTML.Append("<td><input class='hidden' id='txt_NO_SE_PO' value='Service PO'>Service PO</td>");
                    }
                    else
                    {
                        HTML.Append("<td><input class='hidden' id='txt_NO_SE_PO' value='Material PO'>Material PO</td>");
                    }
                    //HTML.Append("<td>" + DTS.Rows[i]["PO_TYPE"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["INCO_TERMS1"].ToString() + '-' + DTS.Rows[i]["INCO_TERMS2"].ToString() + "</td>");
                    HTML.Append("<td style='text-align:right'>" + DTS.Rows[i]["PO_VALUE"].ToString() + "</td>");
                    HTML.Append("<td style='text-align:right'>" + DTS.Rows[i]["PO_GV"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_GV' type='text' value=" + DTS.Rows[i]["PO_GV"].ToString() + "></td>");
                    HTML.Append("<td style='text-align:right'>" + DTS.Rows[i]["Invoice_Amount"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
                   //" +DTS.Rows[i]["PAYMENT_TERMS"].ToString() +"
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Item_Category' type='text' value=" + DTS.Rows[i]["ITEM_CATEGORY"].ToString() + "></td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Cumulative_Amount' type='text' ReadOnly='true' value=" + DTS.Rows[i]["Invoice_Amount"].ToString() + "></td>");
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
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getpo", ref IsData, txt_Username.Text.Trim());
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
                HTML.Append("<table id='tble' class='table table-bordered'><thead><tr class='grey'><th>PO No</th><th>Date</th><th>Currency</th><th>Created By</th><th>PO Type</th><th>INCO Terms</th><th>PO Value</th><th>PO GV</th><th>Cumulative Amount</th><th>Payment Terms</th></tr></thead>");
                HTML.Append("<tbody>");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                       
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
			    string encrypt_Str = crypt.Encryptdata(Convert.ToString(dt.Rows[i]["PO_Number"]));
                            HTML.Append("<tr class='demo1'><td class='hidden'>" + dt.Rows[i]["VENDOR_NAME"].ToString() + "</td>");
                            HTML.Append("<td class='hidden'>" + dt.Rows[i]["VENDOR_CODE"].ToString() + "</td>");
                            HTML.Append("<td class='hidden'><input class='form-control' id='txt_Vendor_Name' type='text' value=" + dt.Rows[i]["VENDOR_NAME"].ToString() + "></td>");
			    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Vendor_Code' type='text' value=" + dt.Rows[i]["VENDOR_CODE"].ToString() + "></td>");
                            //HTML.Append("<td><a href='#po_number' data-toggle='modal' onclick='viewData(" + Convert.ToString(dt.Rows[i]["PO_NUMBER"]) + ")'>" + dt.Rows[i]["PO_NUMBER"].ToString() + "</td>");
                            HTML.Append("<td><a href='#po_number' data-toggle='modal' onclick='viewData(" + ( i + 1 ) + ")'>" + dt.Rows[i]["PO_NUMBER"].ToString() + "<input type='text' id='encrypt_po_"+ ( i + 1 ) +"' value=" + encrypt_Str + " style='display:none'></td>");
			    HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_Number' type='text' value=" + dt.Rows[i]["PO_NUMBER"].ToString() + "></td>");
                            HTML.Append("<td>" + dt.Rows[i]["DATE"].ToString() + "</td>");
                            HTML.Append("<td class='hidden'><input class='form-control' id='txt_Creation' type='text' value=" + dt.Rows[i]["CREATED_DATE"].ToString() + "></td>");
                            HTML.Append("<td>" + dt.Rows[i]["CURRENCY"].ToString() + "</td>");
                            HTML.Append("<td>" + dt.Rows[i]["CREATED_BY"].ToString() + "</td>");
                            if (dt.Rows[i]["ITEM_CATEGORY"].ToString() == "9")
                            {
                                HTML.Append("<td><input class='hidden' id='txt_NO_SE_PO' value='Service PO'>Service PO</td>");
                            }
                            else
                            {
                                HTML.Append("<td><input class='hidden' id='txt_NO_SE_PO' value='Material PO'>Material PO</td>");
                            }
                           // HTML.Append("<td>" + dt.Rows[i]["PO_TYPE"].ToString() + "</td>");
                            HTML.Append("<td>" + dt.Rows[i]["INCO_TERMS1"].ToString() + '-' + dt.Rows[i]["INCO_TERMS2"].ToString() + "</td>");
                            HTML.Append("<td style='text-align:right'>" + dt.Rows[i]["PO_VALUE"].ToString() + "</td>");
                            HTML.Append("<td style='text-align:right'>" + dt.Rows[i]["PO_GV"].ToString() + "</td>");
                            HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_GV' type='text' value=" + dt.Rows[i]["PO_GV"].ToString() + "></td>");
                            HTML.Append("<td style='text-align:right'>" + dt.Rows[i]["Invoice_Amount"].ToString() + "</td>");
                            HTML.Append("<td>" + dt.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
                            HTML.Append("<td class='hidden'><input class='form-control' id='txt_Item_Category' type='text' value=" + dt.Rows[i]["ITEM_CATEGORY"].ToString() + "></td>");
                            HTML.Append("<td class='hidden'><input class='form-control' id='txt_Cumulative_Amount' type='text' ReadOnly='true' value=" + dt.Rows[i]["Invoice_Amount"].ToString() + "></td>");
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
                DataSet ds = (DataSet)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getdetail", ref isValid, po_no);
                DataTable dt = (DataTable)ds.Tables[0];
                DataTable DTS = (DataTable)ds.Tables[1];

                
                HTML.Append("<table id='tbl_Detail' class='table table-bordered'><thead><tr class='grey'><th>Sr.No</th><th>Material Number</th><th>Plant</th><th>Material Description</th><th>Quantity</th><th>UOM</th><th>Net Price</th><th>Amount</th><th>Dispatch Qty</th><th>Supplied Qty</th><th>Schedule</th></tr></thead>");
                HTML.Append("<tbody>");
                if (DTS != null && DTS.Rows.Count > 0)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var Quantity = DTS.Rows[i]["TOTAL"].ToString();
                            var Dispatch_Quantity = DTS.Rows[i]["Dispatch"].ToString();
                            var GR_QUAN = DTS.Rows[i]["GR_QUANTITY"].ToString();
                        
                            var Total = Convert.ToDouble(Dispatch_Quantity);// + Convert.ToDouble(GR_QUAN);
                            if (Convert.ToDouble(Quantity) > Convert.ToDouble(Total))
                            {
                                //HTML.Append("<tr><td>" + (i + 1) + "</td>");
				HTML.Append("<tr><td>" + DTS.Rows[i]["PO_LINE_NUMBER"].ToString() + "</td>");
                                HTML.Append("<td><input type='hidden' id='txt_Material" + (i + 1) + "' value='" + DTS.Rows[i]["MATERIAL_NO"].ToString() + "'>" + DTS.Rows[i]["MATERIAL_NO"].ToString() + "</td>");
                                HTML.Append("<td><input type='hidden' id='txt_plant" + (i + 1) + "' value='" + DTS.Rows[i]["PLANT"].ToString() + "'>" + DTS.Rows[i]["PLANT"].ToString() + "</td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Plan' type='text' value=" + DTS.Rows[i]["PLANT"].ToString() + "></td>");
				HTML.Append("<td><input type='hidden' id='txt_storage" + (i + 1) + "' value='" + DTS.Rows[i]["MATERIAL_DESC"].ToString() + "'>" + DTS.Rows[i]["MATERIAL_DESC"].ToString() + "</td>");
                                HTML.Append("<td><input type='hidden' id='txt_Qunt" + (i + 1) + "' value='" + DTS.Rows[i]["QUANTITY"].ToString() + "'>" + DTS.Rows[i]["QUANTITY"].ToString() + "</td>");
                                HTML.Append("<td><input type='hidden' id='txt_UOM" + (i + 1) + "' value='" + DTS.Rows[i]["UOM"].ToString() + "'>" + DTS.Rows[i]["UOM"].ToString() + "</td>");
                                HTML.Append("<td style='text-align:right'><input type='hidden' id='txt_NET_Price" + (i + 1) + "' value='" + DTS.Rows[i]["NET_PRICE"].ToString() + "'>" + DTS.Rows[i]["NET_PRICE"].ToString() + "</td>");
                                HTML.Append("<td style='text-align:right'><input type='hidden' id='txt_AMOUNT" + (i + 1) + "' value='" + DTS.Rows[i]["AMOUNT"].ToString() + "'>" + DTS.Rows[i]["AMOUNT"].ToString() + "</td>");
                               // HTML.Append("<td><input type='hidden' id='txt_MAt_group" + (i + 1) + "' value='" + DTS.Rows[i]["MATERIAL_GROUP"].ToString() + "'>" + DTS.Rows[i]["MATERIAL_GROUP"].ToString() + "</td>");
                                HTML.Append("<td class='col-md-2'><input id='txt_Dispatch_Qty" + (i + 1) + "' class='form-control' type='text' onkeypress='return isNumber(event)' onchange='return isTolerance()'></td>");
                                HTML.Append("<td><input type='hidden' id='txt_Cumulative_Dispatch" + (i + 1) + "' value='" + DTS.Rows[i]["Dispatch"].ToString() + "'>" + DTS.Rows[i]["Dispatch"].ToString() + "</td>");
                                HTML.Append("<td class='hidden'><input type='hidden' id='txt_grn" + (i + 1) + "' value='" + DTS.Rows[i]["GR_QUANTITY"].ToString() + "'>" + DTS.Rows[i]["GR_QUANTITY"].ToString() + "</td>");
                                HTML.Append("<td><a href='#schedule' data-toggle='modal' onclick='getSchedule("+ (i +1) +")'><img src='../../Img/index.jpg' style='margin-left:10px' height='20' width='20' alt='Smiley face' title='Question'></a></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Mat_no"+ (i + 1) +"' type='text' value=" + DTS.Rows[i]["MATERIAL_NO"].ToString() + "></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Tolerance_Total" + (i + 1) + "' type='text' value=" + DTS.Rows[i]["TOTAL"].ToString() + "></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Tolerance_Diff" + (i + 1) + "' type='text' value=" + DTS.Rows[i]["DIFFERENCE"].ToString() + "></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Tolerance" + (i + 1) + "' type='text' value=" + DTS.Rows[i]["PERCENTAGE"].ToString() + "></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_line_Item_No" + (i + 1) + "' type='text' value=" + DTS.Rows[i]["PO_LINE_NUMBER"].ToString() + "></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Tolerance_Amount" + (i + 1) + "' type='text' value=" + DTS.Rows[i]["Tole_Amount"].ToString() + "></td>");
				HTML.Append("<td class='hidden'><input class='form-control' id='txt_tax" + (i + 1) + "' type='text' value=" + DTS.Rows[i]["Tax1"].ToString() + "></td>");
                                HTML.Append("</tr>");
                            }
			     else
			    {
				HTML.Append("<td class='hidden'><input id='txt_Dispatch_Qty" + (i + 1) + "' class='form-control' type='text' onkeypress='return isNumber(event)' onchange='return isTolerance()'></td>");
			    }
                        }
                    }
                }
                else
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                           
                                //HTML.Append("<tr><td>" + (i + 1) + "</td>");
				HTML.Append("<tr><td>" + dt.Rows[i]["PO_LINE_NUMBER"].ToString() + "</td>");
                                HTML.Append("<td><input type='hidden' id='txt_Material" + (i + 1) + "' value='" + dt.Rows[i]["MATERIAL_NO"].ToString() + "'>" + dt.Rows[i]["MATERIAL_NO"].ToString() + "</td>");
                                HTML.Append("<td><input type='hidden' id='txt_plant" + (i + 1) + "' value='" + dt.Rows[i]["PLANT"].ToString() + "'>" + dt.Rows[i]["PLANT"].ToString() + "</td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Plan' type='text' value=" + dt.Rows[i]["PLANT"].ToString() + "></td>");
				HTML.Append("<td><input type='hidden' id='txt_storage" + (i + 1) + "' value='" + dt.Rows[i]["MATERIAL_DESC"].ToString() + "'>" + dt.Rows[i]["MATERIAL_DESC"].ToString() + "</td>");
                                HTML.Append("<td><input type='hidden' id='txt_Qunt" + (i + 1) + "' value='" + dt.Rows[i]["QUANTITY"].ToString() + "'>" + dt.Rows[i]["QUANTITY"].ToString() + "</td>");
                                HTML.Append("<td><input type='hidden' id='txt_UOM" + (i + 1) + "' value='" + dt.Rows[i]["UOM"].ToString() + "'>" + dt.Rows[i]["UOM"].ToString() + "</td>");
                                HTML.Append("<td style='text-align:right'><input type='hidden' id='txt_NET_Price" + (i + 1) + "' value='" + dt.Rows[i]["NET_PRICE"].ToString() + "'>" + dt.Rows[i]["NET_PRICE"].ToString() + "</td>");
                                HTML.Append("<td style='text-align:right'><input type='hidden' id='txt_AMOUNT" + (i + 1) + "' value='" + dt.Rows[i]["AMOUNT"].ToString() + "'>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
                                //HTML.Append("<td><input type='hidden' id='txt_MAt_group" + (i + 1) + "' value='" + dt.Rows[i]["MATERIAL_GROUP"].ToString() + "'>" + dt.Rows[i]["MATERIAL_GROUP"].ToString() + "</td>");
                                HTML.Append("<td class='col-md-2'><input id='txt_Dispatch_Qty" + (i + 1) + "' class='form-control' type='text' onkeypress='return isNumber(event)' onchange='return isTolerance()'></td>");
                                HTML.Append("<td class='hidden'><input type='hidden' id='txt_Cumulative_Dispatch" + (i + 1) + "' value='" + dt.Rows[i]["DISPATCH_QUANTITY"].ToString() + "'>" + dt.Rows[i]["DISPATCH_QUANTITY"].ToString() + "</td>");
                                HTML.Append("<td><input type='hidden' id='txt_grn" + (i + 1) + "' value='" + dt.Rows[i]["GR_QUANTITY"].ToString() + "'>" + dt.Rows[i]["GR_QUANTITY"].ToString() + "</td>");
                                HTML.Append("<td><a href='#schedule' data-toggle='modal' onclick='getSchedule("+ (i +1) +")'><img src='../../Img/index.jpg' style='margin-left:10px' height='20' width='20' alt='Smiley face' title='Question'></a></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Mat_no" + (i + 1) + "' type='text' value=" + dt.Rows[i]["MATERIAL_NO"].ToString() + "></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Tolerance_Total" + (i + 1) + "' type='text' value=" + dt.Rows[i]["TOTAL"].ToString() + "></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Tolerance_Diff" + (i + 1) + "' type='text' value=" + dt.Rows[i]["DIFFERENCE"].ToString() + "></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Tolerance" + (i + 1) + "' type='text' value=" + dt.Rows[i]["PERCENTAGE"].ToString() + "></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_line_Item_No" + (i + 1) + "' type='text' value=" + dt.Rows[i]["PO_LINE_NUMBER"].ToString() + "></td>");
                                HTML.Append("<td class='hidden'><input class='form-control' id='txt_Tolerance_Amount" + (i + 1) + "' type='text' value=" + dt.Rows[i]["Tole_Amount"].ToString() + "></td>");
				HTML.Append("<td class='hidden'><input class='form-control' id='txt_tax" + (i + 1) + "' type='text' value=" + dt.Rows[i]["Tax1"].ToString() + "></td>");
                                HTML.Append("</tr>");
                          }
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
    public string fillschedule(string po_no,string mat_no)
    {
			
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getschedule", ref isValid, po_no ,mat_no);
                HTML.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Sr No.</th><th>Delivery Date</th><th>Scheduled Quantity</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HTML.Append("<tr>");

                        HTML.Append("<td> " + (i+1) + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["EINDT"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["MENGE"].ToString() + "</td>");
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

    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "script", "window.open('../../portal/HomePage.aspx','frmset_WorkArea');", true); 
             Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
       
        try
        {
            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;

            path = activeDir + "\\";

            string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());
            filename = filename.Replace("(", "");
            filename = filename.Replace(")", "");
            filename = filename.Replace("&", "");
            filename = filename.Replace("+", "");
            filename = filename.Replace("/", "");
            filename = filename.Replace("\\", "");
            filename = filename.Replace("'", "");
            filename = filename.Replace("  ", "");
            filename = filename.Replace(" ", "");
            filename = filename.Replace("#", "");
            filename = filename.Replace("$", "");
            filename = filename.Replace("~", "");
            filename = filename.Replace("%", "");
            filename = filename.Replace("''", "");
            filename = filename.Replace(":", "");
            filename = filename.Replace("*", "");
            filename = filename.Replace("?", "");
            filename = filename.Replace("<", "");
            filename = filename.Replace(">", "");
            filename = filename.Replace("{", "");
            filename = filename.Replace("}", "");
            filename = filename.Replace(",", "");
            DataTable dt = (DataTable)Session["UploadedFiles"];

            FileUpload1.SaveAs(path + filename);
            ClearContents(sender as Control);

        }
        catch (Exception Ex)
        {
            Logger.WriteEventLog(false, Ex);

        }
    }
    private void ClearContents(Control control)
    {
        for (var i = 0; i < Session.Keys.Count; i++)
        {
            if (Session.Keys[i].Contains(control.ClientID))
            {
                Session.Remove(Session.Keys[i]);
                break;
            }
        }
    }

    protected void btn_SubmitOnClick(object sender, System.EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            { 
                Hashtable validationHash = new Hashtable();
                string IsData = string.Empty;
                string isSaved = string.Empty;
                string isInserted = string.Empty;
                string isInsert = string.Empty;
                string isStore = string.Empty;
                string isRealese = string.Empty;
                string ismail = string.Empty;
                string isapprovermail = string.Empty;
                string isUnique = string.Empty;
                txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                string RFC_Data = string.Empty;
                RFC_Data = txt_RFC_Xml.Text;
                string inserXML = txt_Document_Xml.Text;
                inserXML = inserXML.Replace("&", "&amp;");
                txt_Document_Xml.Text = inserXML.ToString();
                string instanceID = (string)WFE.Action.StartCase(IsData, txtProcessID.Text, txt_Username.Text, "", "", "96");
                txtInstanceID.Text = instanceID;
                txt_Action.Text = "Submit";
                txt_Audit.Text = "DISPATCH CREATION NOTE";

		string isRealese_Account = string.Empty;
                string isRealese_Store = string.Empty;
                string IsHard_Account = string.Empty;
                string IsHard_Store = string.Empty;
                txt_Hard_Process_ID_Account.Text = "20";
                txt_Hard_Process_ID_Store.Text = "21";
		string instanceID_Hard_Acoount = (string)WFE.Action.StartCase(IsHard_Account, txt_Hard_Process_ID_Account.Text, txt_Username.Text, "", "", "211");
                string instanceID_Hard_Store = (string)WFE.Action.StartCase(IsHard_Store, txt_Hard_Process_ID_Store.Text, txt_Username.Text, "", "", "216");
                txt_Hard_Instance_ID_Account.Text = instanceID_Hard_Acoount;
                txt_Hard_Instance_ID_Store.Text = instanceID_Hard_Store;
			

                 DataTable dss = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getstore", ref isStore, txt_Plat_Email.Text);
               
if (dss != null)
                {
                    if (dss.Rows.Count > 0)
                    {

                        string[] Dval = new string[dss.Rows.Count];
                        Dval[0] = "";
                        Dval[0] = txt_Performer.Text;
                        if (dss.Rows.Count > 0)
                        {
                            for (int m = 0; m < dss.Rows.Count; m++)
                            {
                                Dval[m] = Convert.ToString(dss.Rows[m]["USER_ID"]);
                                if (txt_Store_Email.Text == "")
                                {
                                    txt_Store_Email.Text = Convert.ToString(dss.Rows[m]["EMAIL_ID"]);
                                }
                                else
                                {
                                    txt_Store_Email.Text = txt_Store_Email.Text + ',' + Convert.ToString(dss.Rows[m]["EMAIL_ID"]);
                                }
                            }
                        }
                        bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                        if (flag && txt_XML_DTL.Text != "" && txt_RFC_Xml.Text != "")
                        {
                            DataTable dl = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getunique_id", ref isUnique);
                            if (dl.Rows.Count > 0 && dl != null)
                            {
                                txt_Unique_ID.Text = dl.Rows[0]["Unique_ID"].ToString();
                                string[] RFCRowArray;
                                RFCRowArray = RFC_Data.Split('|');
                                string DTL_RFC = string.Empty;
                                for (int x = 0; x < RFCRowArray.Length - 1; x++)
                                {
                                    string[] RFC_DataArray;
                                    RFC_DataArray = RFCRowArray[x].Split('$');
                                    DTL_RFC += txt_Unique_ID.Text + '$' + RFC_DataArray[0] + '$' + RFC_DataArray[1] + '$' + RFC_DataArray[2] + '$' + RFC_DataArray[3] + '$' + RFC_DataArray[4] + '|';
                                }

                                string SAP_Messsage = RFC_CAll(txt_Vendor.Text, txt_PO.Text, txt_transporter_Name.Text, txt_Vehicle_No.Text, txt_Contact_Person_Name.Text, txt_Contact_No.Text, txt_LR_NO.Text, Convert.ToDateTime(txt_LR_Date.Text).ToString("yyyyMMdd"), txt_Invoice_No.Text, Convert.ToDateTime(txt_Invoice_Date.Text).ToString("yyyyMMdd"), txt_Invoice_Amount.Text, txt_Delivery_Note.Text, txt_Unique_ID.Text, DTL_RFC);
                               

if (SAP_Messsage == "Table Updated Successfuly")
                                {
                                    isSaved = (string)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "insert", ref isInserted, txtProcessID.Text, txtInstanceID.Text, txt_Vendor.Text, txt_PO.Text, txt_Creation_Date.Text, txt_transporter_Name.Text, txt_Vehicle_No.Text, txt_Contact_Person_Name.Text, txt_Contact_No.Text, txt_LR_NO.Text, txt_LR_Date.Text, txt_Invoice_No.Text, txt_Invoice_Date.Text, txt_Invoice_Amount.Text, 0, txt_Document_Xml.Text, txt_XML_DTL.Text, txt_Delivery_Note.Text, txt_Audit.Text, txt_Username.Text, "", txt_Action.Text, txt_Unique_ID.Text);
                                    if (isSaved != null && isSaved != "" && isSaved != "false")
                                    {
                                        string[] Request_Unique = isSaved.Split('=');
					DataTable DTS1 = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getapprover", ref isInsert, "DISPATCH NOTE ACCOUNTS APPROVER");
                                        string[] Dval11 = new string[DTS1.Rows.Count];
                                        Dval11[0] = "";
                                        Dval11[0] = txt_Performer.Text;
                                        if (DTS1.Rows.Count > 0)
                                        {
                                            for (int i = 0; i < DTS1.Rows.Count; i++)
                                            {
                                                Dval11[i] = Convert.ToString(DTS1.Rows[i]["USER_ADID"]);
                                                if (txt_Account_Approver.Text == "")
                                                {
                                                    txt_Account_Approver.Text = Convert.ToString(DTS1.Rows[i]["EMAIL_ID"]);
                                                }
                                                else
                                                {
                                                    txt_Account_Approver.Text = txt_Account_Approver.Text + ',' + Convert.ToString(DTS1.Rows[i]["EMAIL_ID"]);
                                                }
                                            }
                                        }
                                        
                                        
                                        // txt_Unique_ID.Text = Request_Unique[1];
                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "97", "DISPATCH CREATION NOTE", "SUBMIT", "", txt_Username.Text, txt_Performer.Text, "", "", "", "", "", "", "", "", "", Dval, Request_Unique[0], "0", ref isRealese);
					bool isCreate1 = (bool)WFE.Action.ReleaseStep(txt_Hard_Process_ID_Account.Text, txt_Hard_Instance_ID_Account.Text, "212", "HARD COPY DOCUMENT CREATION ACCOUNT", "SUBMIT", "", txt_Username.Text, txt_Performer.Text, "", "", "", "", "", "", "", "", "", Dval11, Request_Unique[0], "0", ref isRealese_Account);
                                        bool isCreate2 = (bool)WFE.Action.ReleaseStep(txt_Hard_Process_ID_Store.Text, txt_Hard_Instance_ID_Store.Text, "217", "HARD COPY DOCUMENT CREATION STORE", "SUBMIT", "", txt_Username.Text, txt_Performer.Text, "", "", "", "", "", "", "", "", "", Dval, Request_Unique[0], "0", ref isRealese_Store);                                        

					DataTable db = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getmailid", ref isapprovermail, txt_PO.Text);
                                        string[] Dval1 = new string[db.Rows.Count];
                                        Dval1[0] = "";
                                        if (db.Rows.Count > 0)
                                        {
                                            for (int l = 0; l < db.Rows.Count; l++)
                                            {
                                                if (txtApproverEmail.Text == "")
                                                {
                                                    txtApproverEmail.Text = Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                                }
                                                else
                                                {
                                                    txtApproverEmail.Text = txtApproverEmail.Text + ',' + Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                                }
                                            }
                                        }
                                        if (isCreate)
                                        {
                                            try
                                            {
                                                //string mail = "http://"+compname+"/Sudarshan-Portal/Login.aspx";
                                                //string emailid = (string)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "insertmaildata", ref ismail, txtProcessID.Text, txtInstanceID.Text, 0, "DISPATCH CREATION NOTE", "SUBMIT", txtApproverEmail.Text, txt_Account_Approver.Text, "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/><pre><font size='3'>The Vendor Payment Request Send Successfully.</font></pre><p/><pre><font size='3'>Unique No: " + Request_Unique[1] + "</font></pre><pre><font size='3'>Dispatch No: <b>" + Request_Unique[0] + "</font></pre><p/><pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre><font size='3'>INTERNET URL:<a data-cke-saved-href={"+mail+"} href={"+mail+"}>"+mail+"</a></font></pre></br><pre><font size='3'>Regards</font></pre><pre><font size='3'>Reporting Admin</font></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>", Request_Unique[0]);

                                                string mail = "https://esp.sudarshan.com/Sudarshan-Portal/Login.aspx";
                                                string emailid = (string)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "insertmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "DISPATCH CREATION NOTE", "SUBMIT", txt_Store_Email.Text, txtApproverEmail.Text, "<pre><font size='3'>Dear Sir/ Madam,</font></pre><p/><pre><font size='3'>Dispatch note document received successfully.</font></pre><p/><pre><font size='3'>Unique No: " + Request_Unique[1] + "</font></pre><pre><font size='3'>Dispatch No: <b>" + Request_Unique[0] + "</font></pre><p/><pre><font size='3'>Vendor Code: " + txt_Username.Text.Trim() + "</font></pre></p><pre><font size='3'>Vendor Name: <b>" + txt_Vendor_name_mail.Text + "</font></pre><p/><pre><font size='3'>PO Number: <b>" + txt_PO.Text + "</font></pre><p/><pre></b><pre><font size='3'>INTERNET URL:<a data-cke-saved-href={" + mail + "} href={" + mail + "}>" + mail + "</a></font></pre></br><pre><font size='3'>Regards</font></pre><pre><font size='3'>Reporting Admin</font></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>", Request_Unique[0]);

                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Request sent successfully and your Unique no is: " + Request_Unique[1] + "');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Save');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                                    }
                                }
                                else
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Save');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                            else
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Save');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Save');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                    else
                    {
			Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Plant Details Not Available');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                        
                    }
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Plant Details Not Available');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
   }

    protected void btn_EarlyOnClick(object sender, System.EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                Hashtable validationHash = new Hashtable();
                string IsData = string.Empty;
                string IsData1 = string.Empty;
                string GetData = string.Empty;
                string isSaved = string.Empty;
                string isEarly = string.Empty;
                string isInserted = string.Empty;
                string isInsertEarly = string.Empty;
                string isInsert = string.Empty;
                string isRealese = string.Empty;
                string isUnique = string.Empty;
                string isRealeseEarly = string.Empty;
                string isapprovermail = string.Empty;
                txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                string RFC_Data = string.Empty;
                RFC_Data = txt_RFC_Xml.Text;
                string inserXML = txt_Document_Xml.Text;
                inserXML = inserXML.Replace("&", "&amp;");
                txt_Document_Xml.Text = inserXML.ToString();
                txt_ProcessID_Early.Text = "10";
                string instanceID = (string)WFE.Action.StartCase(IsData, txtProcessID.Text, txt_Username.Text, "", "", "96");
                string instanceID_Early = (string)WFE.Action.StartCase(IsData1, txt_ProcessID_Early.Text, txt_Username.Text, "", "","102");
                txtInstanceID.Text = instanceID;
                txt_InstanceID_Early.Text = instanceID_Early;
                txt_Action.Text = "Submit";
                txt_Audit.Text = "DISPATCH CREATION NOTE";

		string isStore = string.Empty;
                string isRealese_Account = string.Empty;
                string isRealese_Store = string.Empty;
                string IsHard_Account = string.Empty;
                string IsHard_Store = string.Empty;
                txt_Hard_Process_ID_Account.Text = "20";
                txt_Hard_Process_ID_Store.Text = "21";
                string instanceID_Hard_Acoount = (string)WFE.Action.StartCase(IsHard_Account, txt_Hard_Process_ID_Account.Text, txt_Username.Text, "", "", "211");
                string instanceID_Hard_Store = (string)WFE.Action.StartCase(IsHard_Store, txt_Hard_Process_ID_Store.Text, txt_Username.Text, "", "", "216");
                txt_Hard_Instance_ID_Account.Text = instanceID_Hard_Acoount;
                txt_Hard_Instance_ID_Store.Text = instanceID_Hard_Store;

                DataTable DTS = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getapprover", ref isInsert, "DISPATCH NOTE ACCOUNTS APPROVER");
                string[] Dval = new string[DTS.Rows.Count];
                Dval[0] = "";
                Dval[0] = txt_Performer.Text;
                if (DTS.Rows.Count > 0)
                {
                    for (int i = 0; i < DTS.Rows.Count; i++)
                    {
                        Dval[i] = Convert.ToString(DTS.Rows[i]["USER_ADID"]);
                        if (txt_Account_Approver.Text == "")
                        {
                            txt_Account_Approver.Text = Convert.ToString(DTS.Rows[i]["EMAIL_ID"]);
                        }
                        else
                        {
                            txt_Account_Approver.Text = txt_Account_Approver.Text + ',' + Convert.ToString(DTS.Rows[i]["EMAIL_ID"]);
                        }
                    }
                }

		DataTable dss = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getstore", ref isStore, txt_Plat_Email.Text);

                string[] DvalStore = new string[dss.Rows.Count];
                DvalStore[0] = "";
                DvalStore[0] = txt_Performer.Text;
                if (dss.Rows.Count > 0)
                {
                    for (int m = 0; m < dss.Rows.Count; m++)
                    {
                        DvalStore[m] = Convert.ToString(dss.Rows[m]["USER_ID"]);
                        if (txt_Store_Email.Text == "")
                        {
                            txt_Store_Email.Text = Convert.ToString(dss.Rows[m]["EMAIL_ID"]);
                        }
                        else
                        {
                            txt_Store_Email.Text = txt_Store_Email.Text + ',' + Convert.ToString(dss.Rows[m]["EMAIL_ID"]);
                        }
                    }
                }
		
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag && txt_XML_DTL.Text != "" && txt_RFC_Xml.Text != "")
                {
                    DataTable dl = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getunique_id", ref isUnique);
                    if (dl.Rows.Count > 0 && dl != null)
                    {
                        txt_Unique_ID.Text = dl.Rows[0]["Unique_ID"].ToString();
                        string[] RFCRowArray;
                        RFCRowArray = RFC_Data.Split('-');
                        string DTL_RFC = string.Empty;
                        for (int x = 0; x < RFCRowArray.Length - 1; x++)
                        {
                            string[] RFC_DataArray;
                            RFC_DataArray = RFCRowArray[x].Split('=');
                            DTL_RFC += txt_Unique_ID.Text + '=' + RFC_DataArray[0] + '=' + RFC_DataArray[1] + '=' + RFC_DataArray[2] + '=' + RFC_DataArray[3] + '=' + RFC_DataArray[4] + '-';
                        }

                          string SAP_Messsage = RFC_CAll(txt_Vendor.Text, txt_PO.Text, txt_transporter_Name.Text, txt_Vehicle_No.Text, txt_Contact_Person_Name.Text, txt_Contact_No.Text, txt_LR_NO.Text, Convert.ToDateTime(txt_LR_Date.Text).ToString("yyyyMMdd"), txt_Invoice_No.Text, Convert.ToDateTime(txt_Invoice_Date.Text).ToString("yyyyMMdd"), txt_Invoice_Amount.Text, txt_Delivery_Note.Text, txt_Unique_ID.Text, DTL_RFC);
                       
if (SAP_Messsage == "Table Updated Successfuly")
                          {
                            isSaved = (string)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "insert", ref isInserted, txtProcessID.Text, txtInstanceID.Text, txt_Vendor.Text, txt_PO.Text, txt_Creation_Date.Text, txt_transporter_Name.Text, txt_Vehicle_No.Text, txt_Contact_Person_Name.Text, txt_Contact_No.Text, txt_LR_NO.Text, txt_LR_Date.Text, txt_Invoice_No.Text, txt_Invoice_Date.Text, txt_Invoice_Amount.Text, 0, txt_Document_Xml.Text, txt_XML_DTL.Text, txt_Delivery_Note.Text, txt_Audit.Text, txt_Username.Text, "", txt_Action.Text, txt_Unique_ID.Text);
                            if (isSaved != null && isSaved != "" && isSaved != "false")
                            {
                                string[] Request_Unique = isSaved.Split('=');

                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "97", "DISPATCH CREATION NOTE", "SUBMIT", "", txt_Username.Text, "", "", "", "", "", "", "", "", "", "", Dval, Request_Unique[0], "0", ref isRealese);
                                bool isCreate1 = (bool)WFE.Action.ReleaseStep(txt_Hard_Process_ID_Account.Text, txt_Hard_Instance_ID_Account.Text, "212", "HARD COPY DOCUMENT CREATION ACCOUNT", "SUBMIT", "", txt_Username.Text, txt_Performer.Text, "", "", "", "", "", "", "", "", "", Dval, Request_Unique[0], "0", ref isRealese_Account);
                                bool isCreate2 = (bool)WFE.Action.ReleaseStep(txt_Hard_Process_ID_Store.Text, txt_Hard_Instance_ID_Store.Text, "217", "HARD COPY DOCUMENT CREATION STORE", "SUBMIT", "", txt_Username.Text, txt_Performer.Text, "", "", "", "", "", "", "", "", "", DvalStore, Request_Unique[0], "0", ref isRealese_Store);
				
				if (isCreate)
                                {
                                    DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "get_pk", ref GetData, Request_Unique[0]);
                                    isEarly = (string)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "insertearly", ref isInsertEarly, 1, dt.Rows[0]["PK_Dispatch_Note_ID"].ToString(), txt_ProcessID_Early.Text, txt_InstanceID_Early.Text);
                                    if (isEarly != null && isEarly != "" && isEarly != "false")
                                    {
                                        bool isCreateRelease = (bool)WFE.Action.ReleaseStep(txt_ProcessID_Early.Text, txt_InstanceID_Early.Text, "103", "EARLY PAYMENT REQUEST", "SUBMIT", "", txt_Username.Text, "", "", "", "", "", "", "", "", "", "", Dval, Request_Unique[0], "0", ref isRealeseEarly);
                                        if (isCreate && isCreateRelease)
                                        {
                                            DataTable db = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getmailid", ref isapprovermail, txt_PO.Text);
                                            string[] Dval1 = new string[db.Rows.Count];
                                            Dval1[0] = "";
                                            if (db.Rows.Count > 0)
                                            {
                                                for (int l = 0; l < db.Rows.Count; l++)
                                                {
                                                    if (txtApproverEmail.Text == "")
                                                    {
                                                        txtApproverEmail.Text = Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                                    }
                                                    else
                                                    {
                                                        txtApproverEmail.Text = txtApproverEmail.Text + ',' + Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                                    }
                                                }
                                            }
                                            try
                                            {
						string mail = "https://esp.sudarshan.com/Sudarshan-Portal/Login.aspx";
                                    string emailid = (string)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "insertmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "DISPATCH CREATION NOTE", "SUBMIT", txt_Account_Approver.Text, txtApproverEmail.Text, "<pre><font size='3'>Dear Sir/ Madam,</font></pre><p/><pre><font size='3'>The Vendor Early Payment Request Sent Successfully.</font></pre><p/><pre><font size='3'>Unique No: " + Request_Unique[1] + "</font></pre><pre><font size='3'>Dispatch No: <b>" + Request_Unique[0] + "</font></pre><p/><pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre><font size='3'>INTERNET URL:<a data-cke-saved-href={"+mail+"} href={"+mail+"}>"+mail+"</a></font></pre></br><pre><font size='3'>Regards</font></pre><pre><font size='3'>Reporting Admin</font></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>", Request_Unique[0]);
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Early payment request sent successfully and your Unique no is: " + Request_Unique[1] + "');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                               Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Save');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Save');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Save');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                    }
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Save');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                }
            }
             catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }      

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillInvoice(string invoice_no, string vendor_Code)
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        DisplayData = "false";
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getinvoice", ref isValid, invoice_no, vendor_Code);
               
                if (dt != null && dt.Rows.Count > 0)
                {
                    DisplayData = "true";
                }
                
               
            }

            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }

        return DisplayData;
    }

    public string RFC_CAll(string Vendor_code,string PO_Number,string Transpoter_Name,string Vehicle_No,string Contact_Person,string Contact_Person_No,string LR_No,string LR_Date,string Invoice_No,string Invoice_Date,string Invoice_Amount,string Delivery_Note,string Unique_Id,string RFC_DTL)
    {
	
        string SAP_Message = string.Empty;
        Vendor_Portal.Vendor_Portal_DetailsService Dispatch_Note=new Vendor_Portal.Vendor_Portal_DetailsService();
        string[] Dispatch_Array = new string[2];
        Dispatch_Array = Dispatch_Note.VENDOR_UNIQUE_ID(Vendor_code, PO_Number, Transpoter_Name, Vehicle_No, Contact_Person, Contact_Person_No, LR_No, LR_Date, Invoice_No, Invoice_Date, Invoice_Amount, Delivery_Note, Unique_Id, RFC_DTL);
        SAP_Message = Dispatch_Array[0];
	return SAP_Message;
    }
}

