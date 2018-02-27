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

public partial class Vendor_Advance_Request : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Vendor_Advance_Request));
                if (!Page.IsPostBack)
                {

                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Session["USER_ADID"].ToString();
                    if (Request.QueryString["processid"] != null && Request.QueryString["stepid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                    }
                    OpenPO();
                    Initialization();
                    
                    txt_Current_Date.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                }
                else
                {
                   
                }
            }
        }
        catch (Exception Exc) { }
    }

    private void Initialization()
    {
        string isValid = string.Empty;
        DisplayHeader();
        DataSet ds = (DataSet)ActionController.ExecuteAction("", "Vendor_Advance_Request.aspx", "getmailid", ref isValid, txt_Po_No.Text);
        DataTable dt = ds.Tables[0];
        DataTable DTS = ds.Tables[1];
        if(dt.Rows.Count > 0)
        {
            txt_Approver_Mail.Text = dt.Rows[0]["EMAIL_ID"].ToString();

        }
        else if(DTS.Rows.Count > 0){
            txt_Vendor_Mail.Text = DTS.Rows[0]["Email"].ToString();
        }
    }

    private void DisplayHeader()
    {
        string IsInsert = string.Empty;
        StringBuilder HTML = new StringBuilder();
        DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Vendor_Advance_Request.aspx", "getheader", ref IsInsert, txt_Username.Text.Trim(), txt_Po_No.Text);
        {
            HTML.Append("<table id='tble' class='table table-bordered'><thead><tr class='grey'><th>PO No</th><th>Date</th><th>Currency</th><th>Created By</th><th>PO Type</th><th>INCO Terms</th><th>PO Value</th><th>Tax</th><th>PO GV</th><th>Advance Amount</th><th>Payment Terms</th></tr></thead>");
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
                    HTML.Append("<td><input class='hidden' id='txt_PO_Date' type='text' value=" + DTS.Rows[i]["PO_Date"].ToString() + ">" + DTS.Rows[i]["PO_Date"].ToString() + "</td>");
                    HTML.Append("<td><input class='hidden' id='txt_PO_Currency' type='text' value=" + DTS.Rows[i]["CURRENCY"].ToString() + ">" + DTS.Rows[i]["CURRENCY"].ToString() + "</td>");
                    HTML.Append("<td><input class='hidden' id='txt_PO_Created_By' type='text' value=" + DTS.Rows[i]["CREATED_BY"].ToString() + ">" + DTS.Rows[i]["CREATED_BY"].ToString() + "</td>");

                    if (DTS.Rows[i]["PO_Type"].ToString() == "9")
                    {
                        HTML.Append("<td><input class='hidden' id='txt_PO_Type' value='Service PO'>Service PO</td>");
                    }
                    else
                    {
                        HTML.Append("<td><input class='hidden' id='txt_PO_Type' value='Material PO'>Material PO</td>");
                    }
                    HTML.Append("<td><input class='hidden' id='txt_PO_Inco' type='text' value=" + DTS.Rows[i]["Inco"].ToString() + ">" + DTS.Rows[i]["Inco"].ToString() + "</td>");
                    HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_PO_Value' type='text' value=" + DTS.Rows[i]["PO_VALUE"].ToString() + ">" + DTS.Rows[i]["PO_VALUE"].ToString() + "</td>");
                    HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_tax' type='text' value=" + DTS.Rows[i]["Tax"].ToString() + ">" + DTS.Rows[i]["Tax"].ToString() + "</td>");
                    HTML.Append("<td style='text-align:right'>" + DTS.Rows[i]["PO_GV"].ToString() + "</td>");
                    HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_Cumu_Amount' type='text' value=" + DTS.Rows[i]["Cumu_Amount"].ToString() + ">" + DTS.Rows[i]["Cumu_Amount"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_GV' type='text' value=" + DTS.Rows[i]["PO_GV"].ToString() + "></td>");
                    HTML.Append("<td><input class='hidden' id='txt_Payment_terms' type='text' value=" + DTS.Rows[i]["PAYMENT_TERMS"].ToString() + ">" + DTS.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Item_Category' type='text' value=" + DTS.Rows[i]["PO_Type"].ToString() + "></td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Advance_PO' type='text' value=" + DTS.Rows[i]["DPTYP"].ToString() + "></td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Advance_PO_Amount' type='text' value=" + DTS.Rows[i]["DPAMT"].ToString() + "></td>");
                    HTML.Append("<td class='hidden'><input class='form-control' id='txt_Advance_Amount_Date' type='text' value=" + DTS.Rows[i]["DPDAT"].ToString() + "></td>");
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
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Advance_Request.aspx", "getpo", ref IsData, txt_Username.Text.Trim());
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
    public string fillVendor(string po_no, string vendor_name)
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {

                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Advance_Request.aspx", "getheader", ref isValid, vendor_name, po_no);
                HTML.Append("<table id='tble' class='table table-bordered'><thead><tr class='grey'><th>PO No</th><th>Date</th><th>Currency</th><th>Created By</th><th>PO Type</th><th>INCO Terms</th><th>PO Value</th><th>Tax</th><th>PO GV</th><th>Advance Amount</th><th>Payment Terms</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dt != null && dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string encrypt_Str = crypt.Encryptdata(Convert.ToString(dt.Rows[i]["PO_Number"]));

                        HTML.Append("<tr class='demo1'><td class='hidden'><input class='form-control' id='txt_Vendor_Name' type='text' value=" + dt.Rows[i]["VENDOR_NAME"].ToString() + "></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Vendor_Code' type='text' value=" + dt.Rows[i]["VENDOR_CODE"].ToString() + "></td>");
                        HTML.Append("<td><a href='#po_number' data-toggle='modal' onclick='viewData(" + (i + 1) + ")'>" + dt.Rows[i]["PO_NUMBER"].ToString() + "<input type='text' id='encrypt_po_" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_Number' type='text' value=" + dt.Rows[i]["PO_NUMBER"].ToString() + "></td>");
                        HTML.Append("<td><input class='hidden' id='txt_PO_Date' type='text' value=" + dt.Rows[i]["PO_Date"].ToString() + ">" + dt.Rows[i]["PO_Date"].ToString() + "</td>");
                        HTML.Append("<td><input class='hidden' id='txt_PO_Currency' type='text' value=" + dt.Rows[i]["CURRENCY"].ToString() + ">" + dt.Rows[i]["CURRENCY"].ToString() + "</td>");
                        HTML.Append("<td><input class='hidden' id='txt_PO_Created_By' type='text' value=" + dt.Rows[i]["CREATED_BY"].ToString() + ">" + dt.Rows[i]["CREATED_BY"].ToString() + "</td>");
                        if (dt.Rows[i]["PO_Type"].ToString() == "9")
                        {
                            HTML.Append("<td><input class='hidden' id='txt_PO_Type' value='Service PO'>Service PO</td>");
                        }
                        else
                        {
                            HTML.Append("<td><input class='hidden' id='txt_PO_Type' value='Material PO'>Material PO</td>");
                        }
                        HTML.Append("<td><input class='hidden' id='txt_PO_Inco' type='text' value=" + dt.Rows[i]["INCO"].ToString() + ">" + dt.Rows[i]["INCO"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_PO_Value' type='text' value=" + dt.Rows[i]["PO_VALUE"].ToString() + ">" + dt.Rows[i]["PO_VALUE"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_tax' type='text' value=" + dt.Rows[i]["Tax"].ToString() + ">" + dt.Rows[i]["Tax"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'>" + dt.Rows[i]["PO_GV"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_Cumu_Amount' type='text' value=" + dt.Rows[i]["Cumu_Amount"].ToString() + ">" + dt.Rows[i]["Cumu_Amount"].ToString() + "</td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_GV' type='text' value=" + dt.Rows[i]["PO_GV"].ToString() + "></td>");
                        HTML.Append("<td><input class='hidden' id='txt_Payment_terms' type='text' value=" + dt.Rows[i]["PAYMENT_TERMS"].ToString() + ">" + dt.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Item_Category' type='text' value=" + dt.Rows[i]["PO_Type"].ToString() + "></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Advance_PO' type='text' value=" + dt.Rows[i]["DPTYP"].ToString() + "></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Advance_PO_Amount' type='text' value=" + dt.Rows[i]["DPAMT"].ToString() + "></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Advance_Amount_Date' type='text' value=" + dt.Rows[i]["DPDAT"].ToString() + "></td>");
                        HTML.Append("</tr>");

                    }
                }
                HTML.Append("</tbody>");
                HTML.Append("</table>");
                //txt_VendorName.Text = dt.Rows[0]["VENDOR_NAME"].ToString();
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
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "script", "window.open('../../portal/HomePage.aspx','frmset_WorkArea');", true); 
            //Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string save(int processid,int Stepid, string name,string code,string PO,string Date,string Currency,string creater,string type,string Inco,string PO_Value,string Tax,string PO_GV,string Payment,string Advance_Amt,string Remark,string appmail, string venmail)
    {
        string isSaved = string.Empty;
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                //Hashtable validationHash = new Hashtable();
                string IsData = string.Empty;
                string isRealese = string.Empty;
                string isInserted = string.Empty;
                string isInsert = string.Empty;
                string isValid = string.Empty;
                string instanceID = (string)WFE.Action.StartCase(IsData, processid, code, "", "", Stepid);
                string[] Dval = new string[1];
                Dval[0] = creater;

                isSaved = (string)ActionController.ExecuteAction("", "Vendor_Advance_Request.aspx", "insert", ref isInserted, processid, instanceID, name, code, PO, Date, Currency, creater, type, Inco, PO_Value, Tax, PO_GV, Payment, Advance_Amt, Remark,appmail,venmail);
                if (isSaved != null && isSaved != "" && isSaved != "false")
                {
                    //bool isCreate = (bool)WFE.Action.ReleaseStep(ProcessId, instanceID, StepId, "REQUEST FOR REGISTRATION", "SUBMIT", "", UserADID, "", "", "", "", "", "", "", "", "", "", Dval, (string)RequestNo, "0", ref isInserted);

                    //bool isCreate = (bool)WFE.Action.ReleaseStep("23", instanceID, "243", "VENDOR ADVANCE REQUEST", "SUBMIT", "", creater, "", "", "", "", "", "", "", "", "", "", Dval, isSaved, "0", ref isRealese);
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
        
            return isSaved;
        
    }

}

