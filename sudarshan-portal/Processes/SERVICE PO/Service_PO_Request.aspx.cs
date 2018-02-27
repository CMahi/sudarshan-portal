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

public partial class Service_PO_Request : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Service_PO_Request));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Session["USER_ADID"].ToString();
                    txtEmailID.Text = (string)Session["EmailID"];
                    txtEmailID.Text.Trim();
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
                    txt_invoice_amount.Text = "0";
                }
            }
        }
        catch (Exception Exc) { }
    }

    private void Initialization()
    {

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
                div_Document.InnerHtml = DisplayData;
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
        DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getdocument", ref IsInsert);
        if (DTS != null && DTS.Rows.Count > 0)
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
        DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getheader", ref IsInsert, txt_Username.Text.Trim(), txt_Po_No.Text);
        {
            HTML.Append("<table id='tble' class='table table-bordered'><thead><tr class='grey'><th>PO No</th><th>Date</th><th>Created By</th><th>PO Type</th><th>PO Value</th><th>PO GV</th><th>Cumulative Amount</th><th>Payment Terms</th></tr></thead>");
            HTML.Append("<tbody>");
            if (DTS != null && DTS.Rows.Count > 0)
            {

                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(DTS.Rows[i]["PO_Number"]));
                    HTML.Append("<tr><td class='hidden'><input class='form-control' id='txt_Vendor_Name' runat='server' type='text' value=" + DTS.Rows[i]["VENDOR_NAME"].ToString() + " ></td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Vendor_Code' type='text' value=" + DTS.Rows[i]["VENDOR_CODE"].ToString() + "></td>");
                    HTML.Append("<td><a href='#' role='button' id='anc1' data-toggle='modal' onclick='viewData(" + (i + 1) + ")'>" + DTS.Rows[i]["PO_NUMBER"].ToString() + "</a><input type='text' id='encrypt_po_" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_Number' type='text' value=" + DTS.Rows[i]["PO_NUMBER"].ToString() + "></td>");
                    HTML.Append("<td><input class='hidden' id='txt_PO_Date' type='text' value='" + DTS.Rows[i]["PO_Date1"].ToString() + "'>" + DTS.Rows[i]["PO_Date"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'><input class='hidden' id='txt_PO_Currency' type='text' value=" + DTS.Rows[i]["CURRENCY"].ToString() + ">" + DTS.Rows[i]["CURRENCY"].ToString() + "</td>");
                    HTML.Append("<td><input class='hidden' id='txt_PO_Created_By' type='text' value=" + DTS.Rows[i]["CREATED_BY"].ToString() + ">" + DTS.Rows[i]["CREATED_BY"].ToString() + "</td>");
                    HTML.Append("<td><input class='hidden' id='txt_PO_Type' value='" + DTS.Rows[i]["PO_Type"].ToString() + "'>Service PO</td>");
                    HTML.Append("<td class='hidden'><input class='hidden' id='txt_PO_Inco' type='text' value=" + DTS.Rows[i]["Inco"].ToString() + ">" + DTS.Rows[i]["Inco"].ToString() + "</td>");
                    HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_PO_Value' type='text' value=" + DTS.Rows[i]["PO_VALUE"].ToString() + ">" + DTS.Rows[i]["PO_VALUE"].ToString() + "</td>");
                    HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_tax' type='text' value=" + DTS.Rows[i]["Tax"].ToString() + ">" + DTS.Rows[i]["PO_GV"].ToString() + "</td>");
                    HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_Cumulative_Amount' type='text' value=" + DTS.Rows[i]["CUMULATIVE_AMT"].ToString() + ">" + DTS.Rows[i]["CUMULATIVE_AMT"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_GV' type='text' value=" + DTS.Rows[i]["PO_GV"].ToString() + "></td>");
                    HTML.Append("<td><input class='hidden' id='txt_Payment_terms' type='text' value=" + DTS.Rows[i]["PAYMENT_TERMS"].ToString() + ">" + DTS.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Item_Category' type='text' value=''></td>");
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
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getpo", ref IsData, txt_Username.Text.Trim());

        if (dt != null && dt.Rows.Count > 0)
        {
            txt_Po_No.Text = dt.Rows[0]["EBELN"].ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str.Append("<div class='form-group demodemo'>");
                if (i == 0)
                {
                    str.Append("<div class='col-md-2'><input id='optionsRadios'  name='optionsRadios' value='" + dt.Rows[i]["EBELN"].ToString() + "' type='radio' Onclick='getheader()' checked ></div>");
                }
                else
                {
                    str.Append("<div class='col-md-2'><input id='optionsRadios'  name='optionsRadios' value='" + dt.Rows[i]["EBELN"].ToString() + "' type='radio' Onclick='getheader()'></div>");
                }
                str.Append("<div class='col-md-10'><input class='form-control' id='checkValue' placeholder='" + dt.Rows[i]["EBELN"].ToString() + "' value='" + dt.Rows[i]["EBELN"].ToString() + "' disabled='' type='text'></div>");
                str.Append("</div>");
            }

        }

        div_po.InnerHtml = str.ToString();
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillVendor(string po_no, string vendor_name)
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {

                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getheader", ref isValid, vendor_name, po_no);
                HTML.Append("<table id='tble' class='table table-bordered'><thead><tr class='grey'><th>PO No</th><th>Date</th><th>Created By</th><th>PO Type</th><th>PO Value</th><th>PO GV</th><th>Cumulative Amount</th><th>Payment Terms</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dt != null && dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string encrypt_Str = crypt.Encryptdata(Convert.ToString(dt.Rows[i]["PO_Number"]));
                        HTML.Append("<tr><td class='hidden'><input class='form-control' id='txt_Vendor_Name' runat='server' type='text' value=" + dt.Rows[i]["VENDOR_NAME"].ToString() + " ></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Vendor_Code' type='text' value=" + dt.Rows[i]["VENDOR_CODE"].ToString() + "></td>");
                        HTML.Append("<td><a href='#' role='button' id='anc1' data-toggle='modal' onclick='viewData(" + (i + 1) + ")'>" + dt.Rows[i]["PO_NUMBER"].ToString() + "</a><input type='text' id='encrypt_po_" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_Number' type='text' value=" + dt.Rows[i]["PO_NUMBER"].ToString() + "></td>");
                        HTML.Append("<td><input class='hidden' id='txt_PO_Date' type='text' value='" + dt.Rows[i]["PO_Date1"].ToString() + "'>" + dt.Rows[i]["PO_Date"].ToString() + "</td>");
                        HTML.Append("<td class='hidden'><input class='hidden' id='txt_PO_Currency' type='text' value=" + dt.Rows[i]["CURRENCY"].ToString() + ">" + dt.Rows[i]["CURRENCY"].ToString() + "</td>");
                        HTML.Append("<td><input class='hidden' id='txt_PO_Created_By' type='text' value=" + dt.Rows[i]["CREATED_BY"].ToString() + ">" + dt.Rows[i]["CREATED_BY"].ToString() + "</td>");
                        HTML.Append("<td><input class='hidden' id='txt_PO_Type' value='" + dt.Rows[i]["PO_Type"].ToString() + "'>Service PO</td>");
                        HTML.Append("<td class='hidden'><input class='hidden' id='txt_PO_Inco' type='text' value=" + dt.Rows[i]["Inco"].ToString() + ">" + dt.Rows[i]["Inco"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_PO_Value' type='text' value=" + dt.Rows[i]["PO_VALUE"].ToString() + ">" + dt.Rows[i]["PO_VALUE"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_tax' type='text' value=" + dt.Rows[i]["Tax"].ToString() + ">" + dt.Rows[i]["PO_GV"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_Cumulative_Amount' type='text' value=" + dt.Rows[i]["CUMULATIVE_AMT"].ToString() + ">" + dt.Rows[i]["CUMULATIVE_AMT"].ToString() + "</td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_GV' type='text' value=" + dt.Rows[i]["PO_GV"].ToString() + "></td>");
                        HTML.Append("<td><input class='hidden' id='txt_Payment_terms' type='text' value='" + dt.Rows[i]["PAYMENT_TERMS"].ToString() + "'>" + dt.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Item_Category' type='text' value=" + dt.Rows[i]["PO_Type"].ToString() + "></td>"); HTML.Append("</tr>");
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
        string dtscnt = string.Empty;
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getdetail", ref isValid, po_no);
                HTML.Append("<table id='tbl_Detail' class='table table-bordered'><thead><tr class='grey'><th></th><th></th><th>Sr.No</th><th>Material Number</th><th>Plant</th><th>Material Description</th><th>Quantity</th><th>UOM</th><th>Net Price</th></tr></thead>");
                HTML.Append("<tbody>");
                if (DTS != null && DTS.Rows.Count > 0)
                {
                    for (int i = 0; i < DTS.Rows.Count; i++)
                    {

                        HTML.Append("<tr><td><input type='checkbox' id='chk" + (i + 1) + "' name='check" + (i + 1) + "' onclick='changeAmount(" + (i + 1) + ")' value='chec" + (i + 1) + "' onkeypress='return enter(event)'></td>");
                        HTML.Append("<td style='text-align:right'><img id='" + po_no + "newimgExpand" + (i + 1) + "' src='../../Img/MoveUp.gif' alt='' style='width:10px; height:auto;' onclick='imgChange(" + (i + 1) + "," + po_no + ")' style='visibility: hidden;'></td>");
                        HTML.Append("<td>" + DTS.Rows[i]["PO_LINE_NUMBER"].ToString() + "<input type='hidden' id='txt_po_no" + (i + 1) + "' value='" + po_no + "'><input type='hidden' id='tbl_Detail_tr" + (i + 1) + "' value=" + (i + 1) + "></td>");
                        HTML.Append("<td><input type='hidden' id='txt_Material" + (i + 1) + "' value='" + DTS.Rows[i]["MATERIAL_NO"].ToString() + "'>" + DTS.Rows[i]["MATERIAL_NO"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_plant" + (i + 1) + "' value='" + DTS.Rows[i]["PLANT"].ToString() + "'>" + DTS.Rows[i]["PLANT"].ToString() + "</td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Plan' type='text' value=" + DTS.Rows[i]["PLANT"].ToString() + "></td>");
                        HTML.Append("<td><input type='hidden' id='txt_storage" + (i + 1) + "' value='" + DTS.Rows[i]["MATERIAL_DESC"].ToString() + "'>" + DTS.Rows[i]["MATERIAL_DESC"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_Qunt" + (i + 1) + "' value='" + DTS.Rows[i]["QUANTITY"].ToString() + "'>" + DTS.Rows[i]["QUANTITY"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_UOM" + (i + 1) + "' value='" + DTS.Rows[i]["UOM"].ToString() + "'>" + DTS.Rows[i]["UOM"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'><input type='hidden' id='txt_NET_Price" + (i + 1) + "' value='" + DTS.Rows[i]["NET_PRICE"].ToString() + "'>" + DTS.Rows[i]["NET_PRICE"].ToString() + "</td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Mat_no" + (i + 1) + "' type='text' value=" + DTS.Rows[i]["MATERIAL_NO"].ToString() + "></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_line_Item_No" + (i + 1) + "' type='text' value=" + DTS.Rows[i]["PO_LINE_NUMBER"].ToString() + "></td>");
                        HTML.Append("</tr>");
                        HTML.Append("<tr style='text-align:left'><td colspan='10' id='" + po_no + "NewExpand1" + (i + 1) + "'style='display:none'><div id='" + po_no + "NewExpand" + (i + 1) + "' style='display: none'></div></tr>");
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

        DisplayData = HTML.ToString() + "||" + dtscnt;
        return DisplayData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillschedule(string po_no, string mat_no)
    {

        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getschedule", ref isValid, po_no, mat_no);
                HTML.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Sr No.</th><th>Delivery Date</th><th>Scheduled Quantity</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HTML.Append("<tr>");

                        HTML.Append("<td> " + (i + 1) + "</td>");
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

            path = activeDir + "\\" + "Service PO" + "\\" + txt_Username.Text ;
            //path = activeDir + "\\" + "Service PO" + "\\";
            if (Directory.Exists(path))
            {

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

                FileUpload1.SaveAs(path + "\\" + filename);
                ClearContents(sender as Control);
            }
            else
            {
                string foldername = txt_Username.Text;
                foldername = foldername.Replace("/", "_");
                path = activeDir + "\\" + "Service PO" + "\\" + foldername;
                Directory.CreateDirectory(path);

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

                FileUpload1.SaveAs(path + "\\" + filename);
                ClearContents(sender as Control);
            }

            //path = activeDir + "\\" + "Service PO" + "\\" + txt_Username.Text + "\\";

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
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {

                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;
                txt_Action.Text = "Submit";
                txt_Audit.Text = "SERVICE PO REQUEST";
                string remark = string.Empty;
                string isSaved = string.Empty;
                divIns.Style["display"] = "block";
                btn_Submit.Enabled = false;
                //string pohdr_xml = txt_po_header_xml.Text;
                //pohdr_xml = pohdr_xml.Replace("&", "&amp;");
                //pohdr_xml = pohdr_xml.Replace(">", "&gt;");
                //pohdr_xml = pohdr_xml.Replace("<", "&lt;");
                //pohdr_xml = pohdr_xml.Replace("||", ">");
                //pohdr_xml = pohdr_xml.Replace("|", "<");
                //pohdr_xml = pohdr_xml.Replace("'", "&apos;");
                //txt_po_header_xml.Text = pohdr_xml.ToString();

                string podtl_xml = txt_po_dtl_xml.Text;
                podtl_xml = podtl_xml.Replace("&", "&amp;");
                podtl_xml = podtl_xml.Replace(">", "&gt;");
                podtl_xml = podtl_xml.Replace("<", "&lt;");
                podtl_xml = podtl_xml.Replace("||", ">");
                podtl_xml = podtl_xml.Replace("|", "<");
                podtl_xml = podtl_xml.Replace("'", "&apos;");
                txt_po_dtl_xml.Text = podtl_xml.ToString();

                string poservdtl_xml = txt_po_serv_dtl_xml.Text;
                poservdtl_xml = poservdtl_xml.Replace("&", "&amp;");
                poservdtl_xml = poservdtl_xml.Replace(">", "&gt;");
                poservdtl_xml = poservdtl_xml.Replace("<", "&lt;");
                poservdtl_xml = poservdtl_xml.Replace("||", ">");
                poservdtl_xml = poservdtl_xml.Replace("|", "<");
                poservdtl_xml = poservdtl_xml.Replace("'", "&apos;");
                txt_po_serv_dtl_xml.Text = poservdtl_xml.ToString();


                string inserXML = txt_Document_Xml.Text;
                inserXML = inserXML.Replace("&", "&amp;");
                txt_Document_Xml.Text = inserXML.ToString();

                string instanceID = (string)WFE.Action.StartCase(isInserted, txtProcessID.Text, txt_Username.Text, txt_Username.Text, "", "280");
                txtInstanceID.Text = instanceID;
                string isapprovermail = string.Empty;
                DataTable db = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getmailid", ref isapprovermail, txt_serv_po_created_by.Text);

                //Dval[0] = "flologic1";
                if (db != null && db.Rows.Count > 0)
                {
                    string[] Dval = new string[db.Rows.Count];
                    for (int l = 0; l < db.Rows.Count; l++)
                    {
                        Dval[l] = db.Rows[l]["AD_ID"].ToString().Trim();
                        if (txtApproverEmail.Text == "")
                        {
                            txtApproverEmail.Text = Convert.ToString(db.Rows[l]["EMAIL_ID"]);
                        }
                        else
                        {
                            txtApproverEmail.Text = txtApproverEmail.Text + ',' + Convert.ToString(db.Rows[l]["EMAIL_ID"]);
                        }
                    }
                    string isUnique = string.Empty;
                    DataTable dl = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getunique_id", ref isUnique);
                    if (dl.Rows.Count > 0 && dl != null)
                    {
                        txt_Unique_ID.Text = dl.Rows[0]["Unique_ID"].ToString();
                    }

                    isSaved = (string)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "insert", ref refData, Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), txt_serv_po_vendor_code.Text, txt_serv_po_po_no.Text, txt_serv_po_date.Text, txt_serv_po_created_by.Text, txt_serv_po_type.Text, txt_serv_po_value.Text, txt_serv_po_gv.Text, txt_serv_po_payterms.Text, txt_serv_po_remark.Text, txt_serv_po_item_total_value.Text, txt_po_dtl_xml.Text, txt_po_serv_dtl_xml.Text, txt_Document_Xml.Text, txt_Remark.Text, txt_Username.Text, txt_Audit.Text, txt_Action.Text, txt_Unique_ID.Text, txt_invoice_no.Text, txt_invoice_Date.Value, txt_delivery_note.Text, txt_From.Value, txt_To.Value, txt_Location.Text, txtApproverEmail.Text, txtEmailID.Text.Trim()); //txt_pkexpenseid.Text
                    if (isSaved == "" || refData.Length > 0 || isSaved == "false")
                    {
                        divIns.Style["display"] = "none";
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Request_Unique = isSaved.Split('=');
                        txt_Request.Text = Request_Unique[0];
                        //txt_Unique_ID.Text = Request_Unique[1];

                        string ref_data1 = string.Empty;
                        string release_id = (string)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, "SERVICE PO REQUEST", "SUBMIT");
                        if (release_id != "")
                        {
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, "SERVICE PO REQUEST", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Unique_ID.Text, "0", ref isInserted);
                            //bool isCreate = true;
                            if (isCreate)
                            {
                                try
                                {

                                    //string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Service PO Submited Successfully and sent to for your approval.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Unique_ID.Text + "</font></pre> <pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    //string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Service_PO_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "SERVICE PO REQUEST", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text.Trim(), msg, "Request No: " + txt_Unique_ID.Text);
                                    string isValid = string.Empty;
                                    DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getfilenames", ref isValid, "SERVICE PO", txt_Request.Text);
                                    if (dt.Rows.Count > 0)
                                    {
                                        string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                        string path = string.Empty;

                                        string foldername = txt_Unique_ID.Text.ToString();
                                        foldername = foldername.Replace("/", "_");
                                        path = activeDir + "\\" + "Service PO" + "\\" + txt_Username.Text + "\\" + foldername;
                                        if (Directory.Exists(path))
                                        {
                                            
                                        }
                                        else
                                        {
                                            Directory.CreateDirectory(path);
                                            string[] directories = Directory.GetFiles(activeDir + "\\" + "Service PO" + "\\" + txt_Username.Text);

                                            path = path + "\\";
                                            foreach (var directory in directories)
                                            {
                                                for (int i = 0; i < dt.Rows.Count; i++)
                                                {
                                                    var sections = directory.Split('\\');
                                                    var fileName = sections[sections.Length - 1];
                                                    if (dt.Rows[i]["filename"].ToString() == fileName)
                                                    {
                                                        System.IO.File.Move(activeDir + "\\" + "Service PO" + "\\" + txt_Username.Text + "\\" + fileName, path + fileName);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    divIns.Style["display"] = "none";
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Service PO Submited Successfully. " + txt_Unique_ID.Text + " ...!');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                        else
                        {
                            divIns.Style["display"] = "none";
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                        }

                    }
                }
                else
                {
                    divIns.Style["display"] = "none";
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Approver Not Found...!');}</script>");

                    return;
                }

            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
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
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getinvoice", ref isValid, invoice_no, vendor_Code);

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

    public string RFC_CAll(string Vendor_code, string PO_Number, string Transpoter_Name, string Vehicle_No, string Contact_Person, string Contact_Person_No, string LR_No, string LR_Date, string Invoice_No, string Invoice_Date, string Invoice_Amount, string Delivery_Note, string Unique_Id, string RFC_DTL)
    {

        string SAP_Message = string.Empty;
        Vendor_Portal.Vendor_Portal_DetailsService Dispatch_Note = new Vendor_Portal.Vendor_Portal_DetailsService();
        string[] Dispatch_Array = new string[2];
        Dispatch_Array = Dispatch_Note.VENDOR_UNIQUE_ID(Vendor_code, PO_Number, Transpoter_Name, Vehicle_No, Contact_Person, Contact_Person_No, LR_No, LR_Date, Invoice_No, Invoice_Date, Invoice_Amount, Delivery_Note, Unique_Id, RFC_DTL);
        SAP_Message = Dispatch_Array[0];
        return SAP_Message;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getservicepodtl(Int64 po_no, int id, string line_item)
    {

        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getservpoitmdtl", ref isValid, po_no, line_item);
                HTML.Append("<table class='table table-bordered' id='tblservpodtl'><thead><tr class='grey'><th>Sr No.</th><th>Service No.</th><th>Short Text</th><th>Total Qty</th><th>Quantity</th><th>Total Net Price</th><th>Supplied Qty</th><th>Net Price</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String Avl= dt.Rows[i]["MENGE2"].ToString();
			String Supp = dt.Rows[i]["Dispatch"].ToString();
			if(Convert.ToDouble(Avl) > Convert.ToDouble(Supp))
			{
                          HTML.Append("<tr>");
                          HTML.Append("<td><input type='hidden' style='text-align:right' id='txt_extrow" + "_" + id + "_" + (i + 1) + "' value='" + dt.Rows[i]["EXTROW0"].ToString() + "'> " + dt.Rows[i]["EXTROW0"].ToString() + "<input type='hidden' style='text-align:right' id='txt_tr_serv_poitm" + "_" + id + "_" + (i + 1) + "' value='" + "_" + id + "_" + (i + 1) + "'></td>");
                          HTML.Append("<td><input type='hidden' style='text-align:right' id='txt_po_serv_no" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["Service Order"].ToString() + "><input type='hidden' style='text-align:right' id='txt_po_no" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["pono"].ToString() + "><input type='hidden' style='text-align:right' id='txt_po_line_no" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["polinenumber"].ToString() + ">" + dt.Rows[i]["Service Order"].ToString() + "</td>");
                          HTML.Append("<td><input type='hidden' style='text-align:right' id='txt_po_short_text" + "_" + id + "_" + (i + 1) + "' value='" + dt.Rows[i]["Short Text"].ToString() + "'>" + dt.Rows[i]["Short Text"].ToString() + "</td>");
                          HTML.Append("<td><input type='hidden' style='text-align:right' id='txt_po_avl_qty" + "_" + id + "_" + (i + 1) + "' value='" + dt.Rows[i]["avlqty"].ToString() + "'>" + dt.Rows[i]["MENGE2"].ToString() + "</td>");
 			 // if(dt.Rows[i]["MEINS1"].ToString() == "NUM" || dt.Rows[i]["MEINS1"].ToString() == "EA" || dt.Rows[i]["MEINS1"].ToString() == "PC")
			//  {
                           //  HTML.Append("<td><input type='text' style='text-align:right' id='txt_serv_po_itm_qty" + "_" + id + "_" + (i + 1) + "' value='' onkeypress='return allowOnlyNumbersdot(event)' onchange='getupdatevalue(" + id + "," + (i + 1) + "," + dt.Rows[i]["MENGE2"].ToString() + ")'><input type='hidden' style='text-align:right' id='txt_act_qty" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["MENGE2"].ToString() + "><input type='hidden' style='text-align:right' id='txt_serv_po_item_value" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["NETWR0"] + "></td>");
                        //  }
			//  else
			//  {
                            HTML.Append("<td><input type='text' style='text-align:right' id='txt_serv_po_itm_qty" + "_" + id + "_" + (i + 1) + "' value='' onkeypress='return allowOnlyNumbers(event)' onchange='getupdatevalue(" + id + "," + (i + 1) + "," + dt.Rows[i]["MENGE2"].ToString() + ")'><input type='hidden' style='text-align:right' id='txt_act_qty" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["MENGE2"].ToString() + "><input type='hidden' style='text-align:right' id='txt_serv_po_item_value" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["NETWR0"] + "></td>");
                        //  }
			  HTML.Append("<td style='text-align:right'><input type='text' readonly style='text-align:right' id='txt_po_total_price" + "_" + id + "_" + (i + 1) + "' value='' onkeypress='return allowOnlyNumbersdot(event)'></td>");
                          HTML.Append("<td><input type='hidden' style='text-align:right' id='txt_DISPATCH_qty" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["Dispatch"].ToString() + ">" + dt.Rows[i]["Dispatch"].ToString() + "</td>");
                          HTML.Append("<td style='text-align:right'><input type='hidden' style='text-align:right' id='txt_po_net_price" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["NETWR0"].ToString() + ">" + dt.Rows[i]["NETWR0"] + "</td>");
                          HTML.Append("<td style='text-align:right;display:none'><input type='hidden' style='text-align:right' id='txt_po_gv1" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["PO_GV"].ToString() + ">" + dt.Rows[i]["PO_GV"] + "</td>");
                          HTML.Append("<td style='text-align:right;display:none;'><input type='hidden' style='text-align:right' id='txt_matnr" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["MATNR"].ToString() + "></td>");
                          HTML.Append("<td style='text-align:right;display:none;'><input type='hidden' style='text-align:right' id='txt_werks" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["WERKS"].ToString() + "></td>");
                          HTML.Append("<td style='text-align:right;display:none;'><input type='hidden' style='text-align:right' id='txt_menge" + "_" + id + "_" + (i + 1) + "' value=" + dt.Rows[i]["MENGE"].ToString() + "></td>");
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

        DisplayData = HTML.ToString() + "||" + po_no;
        return DisplayData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getremainingqty(Int64 pono, string polineno, int id, int index)
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        DisplayData = "false";
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getchkqtyvalue", ref isValid, pono, polineno);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DisplayData = dt.Rows[0]["DISPATCH"].ToString() + "||" + dt.Rows[0]["DISPATCH_value"].ToString() + "||" + id + "||" + index;
                }


            }

            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }

        return DisplayData;
    }

}

