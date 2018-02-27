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

public partial class Hard_Copy_Document_Approval_Account : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Hard_Copy_Document_Approval_Account));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Session["USER_ADID"].ToString();
                    txt_Email_ID.Text = Session["EmailID"].ToString();
                    if (Request.QueryString["processid"] != null && Request.QueryString["stepid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txt_wi.Text = Convert.ToString(Request.QueryString["wiid"]);
                    }
                    Initialization();
                    header_data();
                   
                }
            }
        }
        catch (Exception Exc) {  }
    }

    private void header_data()
    {
        StringBuilder HTML = new StringBuilder();
        string Isdata1 = string.Empty;
        string Isdata2 = string.Empty;
        string Isdata = string.Empty;
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Hard_Copy_Document_Approval_Account.aspx", "getdispatch_data_twi", ref Isdata1, txtProcessID.Text, txtInstanceID.Text);
            string Header_Info = DTS.Rows[0]["HEADER_INFO"].ToString();

            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Hard_Copy_Document_Approval_Account.aspx", "getdispatch_data", ref Isdata, Header_Info);
            txt_PKID.Text = dt.Rows[0]["PK_Dispatch_Note_ID"].ToString();
            txt_Vendor_Code.Text = dt.Rows[0]["Vendor_Code"].ToString();
            txt_Vendor_Name.Text = dt.Rows[0]["Vendor_Name"].ToString();
            txt_PO_NO.Text = dt.Rows[0]["PO_Number"].ToString();
            txt_Unique_NO.Text = dt.Rows[0]["Unique_No"].ToString();
            txt_header.Text = dt.Rows[0]["Request_ID"].ToString();
            txt_Invoice_No.Text = dt.Rows[0]["Invoice_No"].ToString();
            txt_Vendor_Mail_ID.Text = dt.Rows[0]["Email"].ToString();

            DataTable dl = (DataTable)ActionController.ExecuteAction("", "Hard_Copy_Document_Approval_Account.aspx", "get_document_account", ref Isdata2, Header_Info);
            HTML.Append("<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th></th><th>Document Type</th><th>File Name</th><th>Remark</th></tr></thead>");
                HTML.Append("<tbody>");
                if(dl.Rows.Count > 0 && dl != null)
                {
                    for(var i = 0; i < dl.Rows.Count; i++)
                    {
                        HTML.Append("<tr><td><input type='checkbox' id='chkk_" + (i + 1) + "'></td>");
                        HTML.Append("<td> "+ dl.Rows[i]["DOCUMENT_TYPE"].ToString() +"</td>");
                        HTML.Append("<td><a id='a_downloadfiles" + (i + 1) + "' style='cursor: pointer' onclick=\"return downloadfiles('" + (i + 1) + "');\" >"+ dl.Rows[i]["FILENAME"].ToString() +"</td>");
                        HTML.Append("<td class='col-md-5'><input id='txt_Remark" + (i + 1) + "' class='form-control' type='text' value=" + dl.Rows[i]["Remark"].ToString() + "></td></tr>");
                    }
                }
                HTML.Append("</tbody>");
                HTML.Append("</table>");
           }
        div_Doc.InnerHtml = HTML.ToString();
    
    }

    private void Initialization()
    {
       
    }

    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
        }
    }

    protected void btn_AckOnClick(object sender, System.EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                Hashtable validationHash = new Hashtable();
                string IsData = string.Empty;
                string isInserted = string.Empty;
                string isRealese = string.Empty;
                string refData = string.Empty;

                string inserXML = txt_Document_Xml.Text;
                inserXML = inserXML.Replace("&", "&amp;");
                txt_Document_Xml.Text = inserXML.ToString();

                string[] Dval = new string[1];
                Dval[0] = txt_Username.Text;
                string SAP_Messsage = ACCOUNT_RFC_CAll(txt_Unique_NO.Text,"X");
                if (SAP_Messsage == "Table Updated Successfuly")
                {
                string isSaved = (string)ActionController.ExecuteAction("", "Hard_Copy_Document_Approval_Account.aspx", "update", ref refData, txt_header.Text, txt_Unique_NO.Text, "Account", txt_Username.Text);
                    if (isSaved != null || isSaved != "false")
                    {
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "213", "HARD COPY DOCUMENT APPROVAL ACCOUNT", "SUBMIT", "", txt_Username.Text, txt_Username.Text, "", "", "", "", "", "", "", "", "", Dval, txt_header.Text, txt_wi.Text, ref isRealese);
                        if (isCreate)
                        {
                            try
                            {
                                string mail = "https://esp.sudarshan.com/Sudarshan-Portal/Login.aspx";
                                string emailid = (string)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "insertmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "HARD COPY DOCUMENT APPROVAL ACCOUNT", "SUBMIT", txt_Vendor_Mail_ID.Text, txt_Email_ID.Text, "<pre><font size='3'>Dear Sir/ Madam,</font></pre><p/><pre><font size='3'>Dispatch note document acknowledge successfully.</font></pre><p/><pre><font size='3'>Unique No: " + txt_Unique_NO.Text + "</font></pre><pre><font size='3'>Dispatch No: <b>" + txt_header.Text + "</font></pre><p/><pre><font size='3'>Vendor Code: " + txt_Vendor_Code.Text.Trim() + "</font></pre></p><pre><font size='3'>Vendor Name: <b>" + txt_Vendor_Name.Text + "</font></pre><p/><pre><font size='3'>PO Number: <b>" + txt_PO_NO.Text + "</font></pre><p/><pre></b><pre><font size='3'>INTERNET URL:<a data-cke-saved-href={" + mail + "} href={" + mail + "}>" + mail + "</a></font></pre></br><pre><font size='3'>Regards</font></pre><pre><font size='3'>Reporting Admin</font></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>", txt_header.Text);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Hard copy document acknowledge successfully...!');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Dispatch Data Not Updated');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                    }
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('SAP Data Not Updated');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                }
                                                
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }

    }

    protected void btn_SendBack(object sender, System.EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                Hashtable validationHash = new Hashtable();
                string IsData = string.Empty;
                string refData = string.Empty;
                string isInserted = string.Empty;


                string inserXML = txt_Document_Xml.Text;
                inserXML = inserXML.Replace("&", "&amp;");
                txt_Document_Xml.Text = inserXML.ToString();

                string isSaved = (string)ActionController.ExecuteAction("", "Hard_Copy_Document_Approval_Account.aspx", "updatedoc", ref refData, txt_header.Text, txt_Document_Xml.Text,"ACCOUNT");
                if (isSaved != null || isSaved != "false")
                {
                    string[] Dval = new string[1];
                    Dval[0] = txt_Vendor_Code.Text;
                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "214", "HARD COPY DOCUMENT APPROVAL ACCOUNT", "SEND-BACK", txt_Username.Text, txt_Vendor_Code.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_header.Text, txt_wi.Text, ref isInserted);
                    if (isCreate)
                    {
                        try
                        {
                            string mail = "https://esp.sudarshan.com/Sudarshan-Portal/Login.aspx";
                            string emailid = (string)ActionController.ExecuteAction("", "Hard_Copy_Document_Approval_Account.aspx", "insertmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "HARD COPY DOCUMENT APPROVAL ACCOUNT", "SUBMIT", txt_Vendor_Mail_ID.Text, txt_Email_ID.Text, "<pre><font size='3'>Dear Sir/ Madam,</font></pre><p/><pre><font size='3'>Dispatch note document send back successfully.</font></pre><p/><pre><font size='3'>Unique No: " + txt_Unique_NO.Text + "</font></pre><pre><font size='3'>Dispatch No: <b>" + txt_header.Text + "</font></pre><p/><pre><font size='3'>Vendor Code: " + txt_Vendor_Code.Text.Trim() + "</font></pre></p><pre><font size='3'>Vendor Name: <b>" + txt_Vendor_Name.Text + "</font></pre><p/><pre><font size='3'>PO Number: <b>" + txt_PO_NO.Text + "</font></pre><p/><pre></b><pre><font size='3'>INTERNET URL:<a data-cke-saved-href={" + mail + "} href={" + mail + "}>" + mail + "</a></font></pre></br><pre><font size='3'>Regards</font></pre><pre><font size='3'>Reporting Admin</font></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>", txt_header.Text);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Hard Copy Document Send-Back Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                        }
                    }

                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Document Data Not Updated');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
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


    public string ACCOUNT_RFC_CAll(string Unique_No,string Status)
    {

        string SAP_Message = string.Empty;
        Vendor_Portal.Vendor_Portal_DetailsService Dispatch_Note = new Vendor_Portal.Vendor_Portal_DetailsService();
        string[] Dispatch_Array = new string[2];
        Dispatch_Array = Dispatch_Note.TAX_INVOICE_HARD_COPY(Unique_No,Status);
        SAP_Message = Dispatch_Array[0];
        return SAP_Message;
    }
}

