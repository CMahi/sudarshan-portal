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


public partial class PODispatchDeatails_Modification : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(PODispatchDeatails_Modification));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Session["USER_ADID"].ToString();
                    if (Request.QueryString["processid"] != null && Request.QueryString["stepid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txt_wi.Text = Convert.ToString(Request.QueryString["wiid"]);
                    }
                    Initialization();
                    DisplayHeader();
                    DisplayDetail();
                    GetDocument();
                    fillDocument_Details();
                    fillAuditTrailData();
                    txt_Invoice_Date.Attributes.Add("ReadOnly", "true");
                    txt_LR_Date.Attributes.Add("ReadOnly","true");
                }
            }
        }
        catch (Exception Exc) {  }
    }

    private void DisplayDetail()
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails_Modification.aspx", "getdetail", ref isValid, txt_Dispatch.Text);
                HTML.Append("<table id='tbl_Detail' class='table table-bordered'><thead><tr class='grey'><th>Sr.No</th><th>Material Number</th><th>Plant</th><th>Storage Location</th><th>Quantity</th><th>UOM</th><th>Net Price</th><th>Amount</th><th>Material Group</th><th>Dispatch Qty</th><th>GR Qty</th><th>Schedule</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        
                        HTML.Append("<tr><td>" + (i + 1) + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_Material" + (i + 1) + "' value='" + dt.Rows[i]["MATERIAL_NO"].ToString() + "'>" + dt.Rows[i]["MATERIAL_NO"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_plant" + (i + 1) + "' value='" + dt.Rows[i]["PLANT"].ToString() + "'>" + dt.Rows[i]["PLANT"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_storage" + (i + 1) + "' value='" + dt.Rows[i]["STORAGE_LOCATION"].ToString() + "'>" + dt.Rows[i]["STORAGE_LOCATION"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_Qunt" + (i + 1) + "' value='" + dt.Rows[i]["QUANTITY"].ToString() + "'>" + dt.Rows[i]["QUANTITY"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_UOM" + (i + 1) + "' value='" + dt.Rows[i]["UOM"].ToString() + "'>" + dt.Rows[i]["UOM"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'><input type='hidden' id='txt_NET_Price" + (i + 1) + "' value='" + dt.Rows[i]["NET_PRICE"].ToString() + "'>" + dt.Rows[i]["NET_PRICE"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'><input type='hidden' id='txt_AMOUNT" + (i + 1) + "' value='" + dt.Rows[i]["AMOUNT"].ToString() + "'>" + dt.Rows[i]["AMOUNT"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_MAt_group" + (i + 1) + "' value='" + dt.Rows[i]["MATERIAL_GROUP"].ToString() + "'>" + dt.Rows[i]["MATERIAL_GROUP"].ToString() + "</td>");
                        HTML.Append("<td class='col-md-1'><input id='txt_Dispatch_Qty" + (i + 1) + "' class='form-control' value= " + dt.Rows[i]["Dispatch_Quantity"].ToString() + " type='text' onkeypress='return isNumber(event)' onchange='return isTolerance()'></td>");
                        HTML.Append("<td class='hidden'><input type='hidden' id='txt_Cumulative_Dispatch" + (i + 1) + "' value='" + dt.Rows[i]["Dispatch"].ToString() + "'>" + dt.Rows[i]["Dispatch"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_grn" + (i + 1) + "' value='" + dt.Rows[i]["GR_QUANTITY"].ToString() + "'>" + dt.Rows[i]["GR_QUANTITY"].ToString() + "</td>");
                        HTML.Append("<td><a href='#schedule' data-toggle='modal' onclick='getSchedule("+(i + 1)+")'><img src='../../Img/index.jpg' style='margin-left:10px' height='20' width='20' alt='Smiley face'  title='Question'></a></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Mat_no"+(i + 1)+"' type='text' value=" + dt.Rows[i]["MATERIAL_NO"].ToString() + "></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Tolerance_Total" + (i + 1) + "' type='text' value=" + dt.Rows[i]["TOTAL"].ToString() + "></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Tolerance_Diff" + (i + 1) + "' type='text' value=" + dt.Rows[i]["DIFFERENCE"].ToString() + "></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Tolerance" + (i + 1) + "' type='text' value=" + dt.Rows[i]["PERCENTAGE"].ToString() + "></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_line_Item_No" + (i + 1) + "' type='text' value=" + dt.Rows[i]["PO_LINE_NUMBER"].ToString() + "></td>"); 
                        HTML.Append("</tr>");

                    }
                    txt_Dispatch_HDR.Text = dt.Rows[0]["Request_ID"].ToString();
                }
                HTML.Append("</tbody>");
                HTML.Append("</table>");
            }

            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }

         div_detail.InnerHtml = HTML.ToString();
    }

    private void Initialization()
    {
        txtInstanceID.Text = Request.Params["instanceid"].ToString();
    }

    private void fillDocument_Details()
    {
        StringBuilder HTML = new StringBuilder();
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
                string isData = string.Empty;
                string isValid = string.Empty;
                
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getfilenames", ref isData, "DISPATCH NOTE", txt_Dispatch_HDR.Text);
                HTML.Append("<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>Document Type</th><th>File Name</th><th>Delete</th></tr></thead>");
                HTML.Append("<tbody>");
                if(dt.Rows.Count > 0 && dt != null)
                {
                    for(var i = 0; i < dt.Rows.Count; i++)
                    {
                        HTML.Append("<tr><td> "+ dt.Rows[i]["DOCUMENT_TYPE"].ToString() +"</td>");
                        HTML.Append("<td><a id='a_downloadfiles" + (i + 1) + "' style='cursor: pointer' onclick=\"return downloadfiles('" + (i + 1) + "');\" >"+ dt.Rows[i]["FILENAME"].ToString() +"</td>");
                        HTML.Append("<td style='border: 1px solid #ADBBCA;' align='center' width='5%' valign='middle'><i id='del" + (i + 1) + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (i + 1) + ");\" /></td></tr>");
                    }
                }
                HTML.Append("</tbody>");
                HTML.Append("</table>");
           }
        div_Doc.InnerHtml = HTML.ToString();
    }

    private void GetDocument()
    {
        string IsInsert = string.Empty;
        DataTable DTS = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails_Modification.aspx", "getdocument", ref IsInsert);
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
        string isdata = string.Empty;
        string IsInsert = string.Empty;
        StringBuilder HTML = new StringBuilder();
        txt_Dispatch.Text = (string)ActionController.ExecuteAction("", "PODispatchDeatails_Modification.aspx", "getpkdisp", ref isdata, txtInstanceID.Text, txtProcessID.Text);
        DataTable DTS = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails_Modification.aspx", "getheader", ref IsInsert, txt_Dispatch.Text);
        {
            HTML.Append("<table id='tble' class='table table-bordered'><thead><tr class='grey'><th>PO No</th><th>Date</th><th>Currency</th><th>Created By</th><th>PO Type</th><th>INCO Terms</th><th>PO Value</th><th>PO GV</th><th>Cumulative Amount</th><th>Payment Terms</th></tr></thead>");
            HTML.Append("<tbody>");
            if (DTS != null && DTS.Rows.Count > 0)
            {
                
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(DTS.Rows[i]["PO_Number"]));
                    HTML.Append("<tr><td class='hidden'>" + DTS.Rows[i]["Vendor_Name"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'>" + DTS.Rows[i]["VENDOR_CODE"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Vendor_Code' type='text' value=" + DTS.Rows[i]["VENDOR_CODE"].ToString() + "></td>");
                    HTML.Append("<td><a href='#po_number' data-toggle='modal' onclick='viewData(" + (i + 1) + ")'>" + DTS.Rows[i]["PO_NUMBER"].ToString() + "<input type='text' id='encrypt_po_" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_Number' type='text' value=" + DTS.Rows[i]["PO_NUMBER"].ToString() + "></td>");
                    HTML.Append("<td>" + DTS.Rows[i]["CREATED_DATE"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Creation' type='text' value=" + DTS.Rows[i]["CREATED_DATE"].ToString() + "></td>");
                    HTML.Append("<td>" + DTS.Rows[i]["CURRENCY"].ToString() + "</td>");
                    HTML.Append("<td>" + DTS.Rows[i]["CREATED_BY"].ToString() + "</td>");
                    if (DTS.Rows[i]["ITEM_CATEGORY"].ToString() == "9")
                    {
                        HTML.Append("<td><input class='hidden' id='txt_NO_SE_PO' value='Service PO'>Service PO</td>");
                    }
                    else
                    {
                        HTML.Append("<td><input class='hidden' id='txt_NO_SE_PO' value='Normal PO'>Normal PO</td>");
                    }
                    //HTML.Append("<td>" + DTS.Rows[i]["PO_TYPE"].ToString() + "</td>");
                    HTML.Append("<td><a href='#incoterm' data-toggle='modal' onclick='getIncoTerm()'>INCO Terms</td>");
                    HTML.Append("<td style='text-align:right'>" + DTS.Rows[i]["PO_VALUE"].ToString() + "</td>");
                    HTML.Append("<td style='text-align:right'>" + DTS.Rows[i]["PO_GV"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_GV' type='text' value=" + DTS.Rows[i]["PO_GV"].ToString() + "></td>");
                    HTML.Append("<td style='text-align:right'>" + DTS.Rows[i]["cum_amt"].ToString() + "</td>");
                    HTML.Append("<td><a href='#paymentterm' data-toggle='modal' onclick='getPaymentTerm()'>" + DTS.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
                   
                    //" +DTS.Rows[i]["PAYMENT_TERMS"].ToString() +"
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Item_Category' type='text' value=" + DTS.Rows[i]["ITEM_CATEGORY"].ToString() + "></td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Cumulative_Amount' type='text' ReadOnly='true' value=" + DTS.Rows[i]["cum_amt"].ToString() + "></td>");
                    HTML.Append("</tr>");
                 
                }
                txt_Invoice_No.Text = DTS.Rows[0]["Invoice_No"].ToString();
                txt_Invoice_Date.Text = DTS.Rows[0]["Invoice_Date"].ToString();
                txt_Invoice_Amount.Text = DTS.Rows[0]["Invoice_Amount"].ToString();
                txt_Delivery_Note.Text = DTS.Rows[0]["Delivery_Note"].ToString();
                txt_transporter_Name.Text = DTS.Rows[0]["Transporter_Name"].ToString();
                txt_Vehicle_No.Text = DTS.Rows[0]["Vehicle_No"].ToString();
                txt_Contact_Person_Name.Text = DTS.Rows[0]["Contact_Person_Name"].ToString();
                txt_Contact_No.Text = DTS.Rows[0]["Contact_No"].ToString();
                txt_LR_NO.Text = DTS.Rows[0]["LR_No"].ToString();
                txt_LR_Date.Text = DTS.Rows[0]["LR_Date"].ToString();
            }
           
            HTML.Append("</tbody>");
		    HTML.Append("</table>");
        }
        div_Header.InnerHtml = HTML.ToString();
	
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
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails_Modification.aspx", "getinco", ref isValid, po_no);
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
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails_Modification.aspx", "getpayment_term", ref isValid, po_no);
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
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails_Modification.aspx", "getschedule", ref isValid, po_no);
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
               Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
	     //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "script", "window.open('../../portal/HomePage.aspx','frmset_WorkArea');", true); 
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
                string isRealese = string.Empty;
                string isapprovermail = string.Empty;
                string isUnique = string.Empty;
                txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                string RFC_Data = string.Empty;
                RFC_Data = txt_RFC_Xml.Text;
                string inserXML = txt_Document_Xml.Text;
                inserXML = inserXML.Replace("&", "&amp;");
                txt_Document_Xml.Text = inserXML.ToString();
                txt_Action.Text = "Submit";
                txt_Audit.Text = "DISPATCH CREATION NOTE MODIFICATION";
                DataTable DTS = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails_Modification.aspx", "getapprover", ref isInsert, "DISPATCH NOTE ACCOUNTS APPROVER");
                string[] Dval = new string[DTS.Rows.Count];
                Dval[0] = "";
                if (DTS.Rows.Count > 0)
                {
                    for (int i = 0; i < DTS.Rows.Count; i++)
                    {
                        Dval[i] = Convert.ToString(DTS.Rows[i]["USER_ADID"]);

                    }
                }
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag && txt_XML_DTL.Text != "" && txt_RFC_Xml.Text != "")
                {
                    DataTable dl = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails_Modification.aspx", "getunique_id", ref isUnique, txtProcessID.Text, txtInstanceID.Text, txt_Dispatch_HDR.Text);
                    if (dl.Rows.Count > 0 && dl != null)
                    {
                        txt_Unique_ID.Text = dl.Rows[0]["Unique_No"].ToString();
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
                            isSaved = (string)ActionController.ExecuteAction("", "PODispatchDeatails_Modification.aspx", "update", ref isInserted, txtProcessID.Text, txtInstanceID.Text, txt_Vendor.Text, txt_PO.Text, txt_Creation_Date.Text, txt_transporter_Name.Text, txt_Vehicle_No.Text, txt_Contact_Person_Name.Text, txt_Contact_No.Text, txt_LR_NO.Text, txt_LR_Date.Text, txt_Invoice_No.Text, txt_Invoice_Date.Text, txt_Invoice_Amount.Text, 0, txt_Document_Xml.Text, txt_XML_DTL.Text, txt_Delivery_Note.Text, txt_Audit.Text, txt_Username.Text, "", txt_Action.Text);
                            if (isSaved != null && isSaved != "" && isSaved != "false")
                            {
                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "101", "DISPATCH CREATION NOTE MODIFICATION", "SUBMIT", "", txt_Username.Text, "", "", "", "", "", "", "", "", "", "", Dval, isSaved, txt_wi.Text, ref isRealese);
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
                                        string emailid = (string)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "insertmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "DISPATCH CREATION NOTE MODIFICATION", "SUBMIT", txtApproverEmail.Text, "", "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/><pre><font size='3'>The Vendor Payment Request Send Successfully.</font></pre><p/><pre><font size='3'>Dispatch No: <b>" + isSaved + "</font></pre><p/><pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre><font size='3'>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</font></pre></br><pre><font size='3'>Regards</font></pre><pre><font size='3'>Reporting Admin</font></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>", isSaved);
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }
                                    finally
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Request modified successfully and your request no: " + isSaved + "');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
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
    public string fillInvoice(string po_no,string invoice_no,string vendor_code)
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        DisplayData = "false";
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails_Modification.aspx", "getinvoice", ref isValid, po_no, invoice_no, vendor_code);
               
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

    private void fillAuditTrailData()
    {
        try
        {
            GetData getdata = new GetData();
            div_Audit.InnerHtml = getdata.fillAuditTrail(txtProcessID.Text, txtInstanceID.Text);
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }

    public string RFC_CAll(string Vendor_code, string PO_Number, string Transpoter_Name, string Vehicle_No, string Contact_Person, string Contact_Person_No, string LR_No, string LR_Date, string Invoice_No, string Invoice_Date, string Invoice_Amount, string Delivery_Note, string Unique_Id, string RFC_DTL)
    {
        string SAP_Message = string.Empty;
        Vendor_Portal.Vendor_Portal_DetailsService Dispatch_Note = new Vendor_Portal.Vendor_Portal_DetailsService();
        string[] Dispatch_Array = new string[2];
        Dispatch_Array = Dispatch_Note.VENDOR_UNIQUE_ID(Vendor_code, PO_Number, Transpoter_Name, Vehicle_No, Contact_Person, Contact_Person_No, LR_No, LR_Date, Invoice_No, Invoice_Date, Invoice_Amount, Delivery_Note, Unique_Id, RFC_DTL);
        SAP_Message = Dispatch_Array[0];
        return SAP_Message;
    }
}

