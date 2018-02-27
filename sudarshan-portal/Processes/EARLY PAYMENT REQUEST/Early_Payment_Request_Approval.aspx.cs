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


public partial class Early_Payment_Request_Approval : System.Web.UI.Page
{
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
         ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Early_Payment_Request_Approval));
            try
            {
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["step"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txt_Step.Text = Convert.ToString(Request.QueryString["step"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                    }
                    bindData();
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void bindData()
    {
        StringBuilder html_Header = new StringBuilder();
         try
         {
            string inco_terms = string.Empty;
             string isdata = string.Empty;
             txt_Dispatch.Text = (string)ActionController.ExecuteAction("", "Early_Payment_Request_Approval.aspx", "getpkdisp", ref isdata, txtInstanceID.Text, txtProcessID.Text);
             DataSet ds = (DataSet)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getdispatchdetails", ref isdata, txt_Dispatch.Text);

             if (ds != null)
             {
                 /*********************************************************************************************PO Header*********************************************************************************************************/

                 html_Header.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>PO Header</h4></div><div class='panel-body'><div class='table-responsive'>");
                 html_Header.Append("<table class='table table-bordered' width='100%'>");
                 html_Header.Append("<tr style='background-color:grey; text-align:center; color:white;'><th style='text-align:center'>Dispatch Req No</th><th style='text-align:center'>Vendor Name</th><th style='text-align:center'>Vendor Code</th><th style='text-align:center'>PO No</th><th style='text-align:center'>PO Date</th><th style='text-align:center'>Currency</th><th style='text-align:center'>Dispatch Date</th><th style='text-align:center'>PO Type</th><th style='text-align:center'>INCO Terms</th><th style='text-align:center'>PO Value</th><th style='text-align:center;'>PO GV</th><th style='text-align:center;'>Payment Terms</th><th style='text-align:center;'>Cumulative Inv Amt</th></tr>");
                 html_Header.Append("<tbody>");
                 txt_Request.Text = Convert.ToString(ds.Tables[0].Rows[0]["request_id"]);
                
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     initDate.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["early_date"]);
                     Init_Email.Text = Convert.ToString(ds.Tables[0].Rows[0]["Init_Email"]).Trim();
                     txt_PO.Text = Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]);
                     CryptoGraphy crypt = new CryptoGraphy();
                     string encrypt_Str = crypt.Encryptdata(Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]));

                     /**********************************************************************Inco Term Details*************************************************************************************/
                     div_PaymentTemSetPoNo.InnerText = div_IcoTemSetPoNo.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]);
                     DataTable dts = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getinco", ref isdata, Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]));
                     if (dts.Rows.Count > 0)
                     {
                         inco_terms = Convert.ToString(dts.Rows[0]["INCO_TERMS1"]) + " - " + Convert.ToString(dts.Rows[0]["INCO_TERMS2"]);
                         Inco1.Text = Convert.ToString(dts.Rows[0]["INCO_TERMS1"]);
                         Inco2.Text = Convert.ToString(dts.Rows[0]["INCO_TERMS2"]);
                     }

                     html_Header.Append("<tr><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["request_id"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["vendor_name"]) + "</td><td style='text-align:center'><a href='#vendorinfo' role='button' id='ven1' data-toggle='modal'>" + Convert.ToString(ds.Tables[0].Rows[0]["vendor_code"]) + "</a></td><td style='text-align:center'><a href='#' role='button' id='anc1' onclick='viewData(" + 1 + ")'>" + Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]) + "</a><input type='text' id='encrypt_po1' value=" + encrypt_Str + " style='display:none'></td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["date"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["currency"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["created_date"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["po_type"]) + "</td><td style='text-align:center'>" + inco_terms + "</td><td style='text-align:right'>" + Convert.ToString(ds.Tables[0].Rows[0]["po_value"]) + "</td><td style='text-align:right;'>" + Convert.ToString(ds.Tables[0].Rows[0]["po_gv"]) + "</td><td style='text-align:center;'>" + Convert.ToString(ds.Tables[0].Rows[0]["PAYMENT_TERMS"]) + "</td><td style='text-align:right;'>" + Convert.ToString(ds.Tables[0].Rows[0]["cum_amt"]) + "</td></tr>");

                    

                     /*********************************************************************Payment Term Details**************************************************************************************/
                     dts = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getpayment_term", ref isdata, Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]));
                     if (dts.Rows.Count > 0)
                     {
                         Pay1.Text = Convert.ToString(dts.Rows[0]["ZTAGG"]);
                         Pay2.Text = Convert.ToString(dts.Rows[0]["ZFAEL"]);
                         Pay3.Text = Convert.ToString(dts.Rows[0]["ZTAG1"]);
                     }

                     /***************************************************************************Vendor Details********************************************************************************/
                     DataTable dtVendor = (DataTable)ActionController.ExecuteAction("", "Edit_Profile.aspx", "getvendordetails", ref isdata, Convert.ToString(ds.Tables[0].Rows[0]["vendor_code"]));

                     if (dtVendor.Rows.Count > 0)
                     {
                         lblName.Text = Convert.ToString(dtVendor.Rows[0]["Vendor_Name1"]).Trim();
                         lblEmail.Text = Convert.ToString(dtVendor.Rows[0]["Email"]).Trim();
                         lblMobile.Text = Convert.ToString(dtVendor.Rows[0]["Telephone1"]).Trim();
                         lblPan.Text = Convert.ToString(dtVendor.Rows[0]["PAN_NO"]).Trim();

                         lblContact.Text = Convert.ToString(dtVendor.Rows[0]["Telephone2"]).Trim();
                         lblEcc.Text = Convert.ToString(dtVendor.Rows[0]["ECC_NO"]).Trim();
                         lblCentral.Text = Convert.ToString(dtVendor.Rows[0]["Central_Sales_Tax_No"]).Trim();
                         lblLocal.Text = Convert.ToString(dtVendor.Rows[0]["Local_Sales_Tax_No"]).Trim();
                         lblExcise.Text = Convert.ToString(dtVendor.Rows[0]["Excise_Reg_No"]).Trim();
                         lblFax.Text = Convert.ToString(dtVendor.Rows[0]["FAX_NO"]).Trim();

                         lblBank.Text = Convert.ToString(dtVendor.Rows[0]["FK_BANK_ID"]).Trim();
                         lblAccount.Text = Convert.ToString(dtVendor.Rows[0]["ACC_NO"]).Trim();
                         lblIFSC.Text = Convert.ToString(dtVendor.Rows[0]["IFSC_CODE"]).Trim();
                         lblBranch.Text = Convert.ToString(dtVendor.Rows[0]["BRANCH"]).Trim();
                     }

                 }

                 html_Header.Append("</tbody></table>");

                 /******************************************************************************Invoice Details**************************************************************************************************************************/

                 html_Header.Append("<div>");
                 html_Header.Append("<div class='col-md-1' style='text-align:right'>");
                 html_Header.Append("<b>Invoice No :</b>");
                 html_Header.Append("</div>");
                 html_Header.Append("<div class='col-md-1'>");
                 html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Invoice_No"]));
                 html_Header.Append("</div>");
                 html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                 html_Header.Append("<b>Invoice Date :</b>");
                 html_Header.Append("</div>");
                 html_Header.Append("<div class='col-md-1'>");
                 html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Invoice_Date"]));
                 html_Header.Append("</div>");
                 html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                 html_Header.Append("<b>Invoice Amount :</b>");
                 html_Header.Append("</div>");
                 html_Header.Append("<div class='col-md-1'>");
                 html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Invoice_Amount"]));
                 html_Header.Append("</div>");
                 html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                 html_Header.Append("<b>Delivery Note :</b>");
                 html_Header.Append("</div>");
                 html_Header.Append("<div class='col-md-1'>");
                 html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Delivery_Note"]));
                 html_Header.Append("</div>");
                 html_Header.Append("</div>");

                 /**************************************************************************Hide/Show links******************************************************************************************************************************/
                 html_Header.Append("<div class='row'>&nbsp;</div>");
                 html_Header.Append("<div class='col-md-10'>&nbsp;</div>");
                 html_Header.Append("<div class='col-md-2' style='text-align:right'><a id='showdtl' runat='server' onclick='Show_Dtl()' href='#'>Show Details</a>");
                 html_Header.Append("<a id='hidedtl' runat='server' onclick='Hide_Dtl()' href='#' style='display:none'>Hide Details</a></div>");
                 html_Header.Append("</div></div></div></div>");

                 
                 /*****************************************************************************PO Details**************************************************************************************************************************/

                 html_Header.Append("<div class='col-md-12' id='div_dtl' runat='server' style='display:none'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>PO Details</h4></div><div class='panel-body'><div class='table-responsive'>");
                 html_Header.Append("<table class='table table-bordered' width='100%'>");
                 html_Header.Append("<tr style='background-color:grey; text-align:center; color:white;'><th style='text-align:center'>Material No</th><th style='text-align:center'>Plant</th><th style='text-align:center'>Material Description</th><th style='text-align:center'>Quantity</th><th style='text-align:center'>UOM</th><th style='text-align:center'>Net Price</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Dispatch Qty</th><th style='text-align:center;'>Goods Received Qty</th><th style='text-align:center; display:none'>PK_VENDOR_PO_DTL</th></tr>");
                 html_Header.Append("<tbody>");

                 if (ds.Tables[1].Rows.Count > 0)
                 {
                     for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                     {
                         html_Header.Append("<tr><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["material_no"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["plant"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["storage_location"]) + "</td><td style='text-align:right'>" + Convert.ToString(ds.Tables[1].Rows[i]["quantity"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["uom"]) + "</td><td style='text-align:right'>" + Convert.ToString(ds.Tables[1].Rows[i]["net_price"]) + "</td><td style='text-align:right'>" + Convert.ToString(ds.Tables[1].Rows[i]["amount"]) + "</td><td style='text-align:right'>" + Convert.ToString(ds.Tables[1].Rows[i]["dispatch_quantity"]) + "</td><td style='text-align:right;'>" + Convert.ToString(ds.Tables[1].Rows[i]["gr_quantity"]) + "</td><td style='text-align:center; display:none'>" + Convert.ToString(ds.Tables[1].Rows[i]["PK_Dispatch_Note_HDR_ID"]) + "</td></tr>");
                     }
                 }

                 html_Header.Append("</tbody></table>");
                 html_Header.Append("</div></div></div></div>");

                 /*****************************************************************************Dispatch Details*************************************************************************************************************************/

                 html_Header.Append("<div class='col-md-6'><div class='panel panel-grey' data-height='290px'><div class='panel-heading'><h4 class='panel-title'>Dispatch Details</h4></div><div class='panel-body'>");
                 html_Header.Append("<table class='table table-bordered' width='100%'>");
                 html_Header.Append("<tr><td class='col-md-3'><b>Transporter Name</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["Transporter_Name"]) + "</td></tr>");
                 html_Header.Append("<tr><td class='col-md-3'><b>Vehicle No</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["vehicle_no"]) + " </td></tr>");
                 html_Header.Append("<tr><td class='col-md-3'><b>Contact Person Name</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["contact_person_name"]) + " </td></tr>");
                 html_Header.Append("<tr><td class='col-md-3'><b>Contact No</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["contact_no"]) + " </td></tr>");
                 html_Header.Append("<tr><td class='col-md-3'><b>LR No.</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["lr_no"]) + " </td></tr>");
                 html_Header.Append("<tr><td class='col-md-3'><b>LR Date</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["lr_date"]) + " </td></tr>");
                 html_Header.Append("</table></div></div></div>");

                 /**********************************************************************************Documents*********************************************************************************************************************/

                 html_Header.Append("<div class='col-md-6'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>Documents</h4></div><div class='panel-body' data-scrollbar='true' data-height='135px'>");
                 html_Header.Append("<table class='table table-bordered' width='100%'>");
                 html_Header.Append("<tbody>");
                 html_Header.Append("<tr style='background-color:grey; text-align:center; color:white;'><td style='text-align:center'>Document Name</td><td style='text-align:center'>File Name</td></tr>");
                 DataTable dt = (DataTable)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getfilenames", ref isdata, "DISPATCH NOTE", Convert.ToString(ds.Tables[0].Rows[0]["request_id"]));

                 if (dt.Rows.Count > 0)
                 {
                     for (int i = 0; i < dt.Rows.Count; i++)
                     {
                         html_Header.Append("<tr><td style='text-align:center'>" + Convert.ToString(dt.Rows[i]["DOCUMENT_TYPE"]) + "</td><td style='text-align:center'><a href='#' OnClick='downloadfiles(" + Convert.ToString(dt.Rows[i]["PK_ID"]) + ")'>" + Convert.ToString(dt.Rows[i]["FILENAME"]) + "</a></td></tr>");
                     }
                 }
                 html_Header.Append("</tbody>");
                 html_Header.Append("</table></div></div></div>");

                 /***********************************************************************************Remark********************************************************************************************************************/

                 html_Header.Append("<div class='col-md-6' style='margin-top:0.3%'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>Remarks</h4></div><div class='panel-body'>");
                 html_Header.Append("<table class='table table-bordered' width='100%'>");
                 html_Header.Append("<tbody>");
                 html_Header.Append("<tr><td style='text-align:center'><textarea name='txtRemark' cols='45' class='form-control' ID='txtRemark'></textarea></td></tr>");
                 html_Header.Append("</tbody>");
                 html_Header.Append("</table></div></div></div>");

                 /*******************************************************************************************************************************************************************************************************/
             }
             div_header.InnerHtml = Convert.ToString(html_Header);
         }
         catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
       
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
        }
    }

    protected void btnRequest_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                string remark = txt_Remark.Text;
                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;
                txt_Condition.Text = "2";
                string isSaved = (string)ActionController.ExecuteAction("", "Early_Payment_Request_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txt_Dispatch.Text,txt_Username.Text);
                if (isSaved == null || refData.Length > 0 || isSaved == "false")
                {
                    string[] errmsg = refData.Split(':');
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                }
                else
                {
                    string[] Dval = new string[1];
                    Dval[0] = txt_Username.Text;
                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "104", "EARLY PAYMENT REQUEST APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                    if (isCreate)
                    {
                        try
                        {
                            string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "EARLY PAYMENT REQUEST APPROVAL", "USER", txt_Username.Text, "SUBMIT", remark, "0", "0");
				string mail = "https://esp.sudarshan.com/Sudarshan-Portal/Login.aspx";
                            string uniqueno = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "getuniqueno", ref isInserted, txt_Dispatch.Text);
                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre> <pre><font size='3'>The Early Payment Request has been Approved.</font></pre> <pre><font size='3'>Unique No: " + uniqueno + "</font></pre> <pre><font size='3'>Dispatch No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Approved By: " + txt_Username.Text.Trim() + "</font></pre></p><pre><pre>INTERNET URL:<a data-cke-saved-href={" + mail + "} href={" + mail + "}>" + mail + "</a></font></pre></br><pre>Regards</pre><pre>Reporting Admin</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                            
                            DataTable db = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getmailid", ref isInserted, txt_PO.Text);
                            string[] Dval1 = new string[db.Rows.Count];
                            Dval1[0] = "";
                            string mailid = "";
                            if (db != null)
                            {
                                if (db.Rows.Count > 0)
                                {
                                    for (int l = 0; l < db.Rows.Count; l++)
                                    {
                                        if (mailid == "")
                                        {
                                            mailid = Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                        }
                                        else
                                        {
                                            mailid = mailid + ',' + Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                        }
                                    }
                                }
                            }
                            mailid = Init_Email.Text + mailid;
                            string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "EARLY PAYMENT REQUEST APPROVAL", "SUBMIT", Init_Email.Text, txtEmailID.Text, msg, "Dispatch No: " + txt_Request.Text);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Early Payment Request has been Approved...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                   
                }
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
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
                txt_Condition.Text = "3";
                string remark = txt_Remark.Text;
                string isSaved = (string)ActionController.ExecuteAction("", "Early_Payment_Request_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txt_Dispatch.Text,txt_Username.Text);
                if (isSaved == null || refData.Length > 0 || isSaved == "false")
                {
                    string[] errmsg = refData.Split(':');
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                }
                else
                {
                    string[] Dval = new string[1];
                    Dval[0] = "";
                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "105", "EARLY PAYMENT REQUEST APPROVAL", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                    if (isCreate)
                    {
                        try
                        {
                            string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "EARLY PAYMENT REQUEST APPROVAL", "USER", txt_Username.Text, "SUBMIT", remark, "0", "0");
			     string mail = "https://esp.sudarshan.com/Sudarshan-Portal/Login.aspx";
                            string uniqueno = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "getuniqueno", ref isInserted, txt_Dispatch.Text);
                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>The Early Payment Request has been Rejected.</font></pre><pre><font size='3'>Unique No: " + uniqueno + "</font></pre> <pre><font size='3'>Dispatch No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Rejected By: " + txt_Username.Text.Trim() + "</font></pre><pre>INTERNET URL:<a data-cke-saved-href={" + mail + "} href={" + mail + "}>" + mail + "</a></font></pre></br><pre>Regards</pre><pre>Reporting Admin</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                            DataTable db = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getmailid", ref isInserted, txt_PO.Text);
                            string[] Dval1 = new string[db.Rows.Count];
                            Dval1[0] = "";
                            string mailid = "";
                            if (db != null)
                            {
                                if (db.Rows.Count > 0)
                                {
                                    for (int l = 0; l < db.Rows.Count; l++)
                                    {
                                        if (mailid == "")
                                        {
                                            mailid = Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                        }
                                        else
                                        {
                                            mailid = mailid + ',' + Convert.ToString(db.Rows[l]["SMTP_ADDR"]);
                                        }
                                    }
                                }
                            }
                            mailid = Init_Email.Text + mailid;

                            string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "EARLY_PAYMENT_REQUEST", "SUBMIT", Init_Email.Text, txtEmailID.Text, msg, "Dispatch No: " + txt_Request.Text);
                        }
                        catch (Exception ex)
                        {
                           // throw;
                            FSL.Logging.Logger.WriteEventLog(false, ex);
                        }
                        finally
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Early Payment Request has been Rejected...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                }
            }
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
   
}