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


public partial class PODispatchDeatails_Approval : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        
         ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(PODispatchDeatails_Approval));
            try
            {
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        //txt_Step.Text = Convert.ToString(Request.QueryString["step"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                    }
                    bindData();
                    fillAuditTrailData();
                    fillAction();
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    private void fillAction()
    {
        try
        {
            string Isdata = string.Empty;
            ListItem li = new ListItem("--Select One--", "");
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails_Approval.aspx", "action", ref Isdata);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl_Action.DataSource = dt;
                ddl_Action.DataTextField = "ACTION_NAME";
                ddl_Action.DataValueField = "ACTION_NAME";
                ddl_Action.DataBind();
                ddl_Action.Items.Insert(0, li);
            }
        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
    }

    protected void bindData()
    {
        StringBuilder html_Header = new StringBuilder();
        StringBuilder html_Detail = new StringBuilder();
        StringBuilder html_Document = new StringBuilder();
         try
         {
            
             string isdata = string.Empty;
             txt_Dispatch.Text = (string)ActionController.ExecuteAction("", "PODispatchDeatails_Approval.aspx", "getpkdisp", ref isdata, txtInstanceID.Text, txtProcessID.Text);
             DataSet ds = (DataSet)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getdispatchdetails", ref isdata, txt_Dispatch.Text);

             if (ds != null)
             {
                 /******************************************************************************************************************************************************************************************************/
                
                 //html_Header.Append("<div class='col-md-12'><div class='panel panel-success'><div class='panel-heading'><h4 class='panel-title'>PO Header</h4></div><div class='panel-body'><div class='table-responsive'>");
                 html_Header.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Dispatch Request No</th><th>Vendor Name</th><th>Vendor Code</th><th>PO No</th><th>PO Date</th><th>Currency</th><th>Dispatch Date</th><th>PO Type</th><th>INCO Terms</th><th>PO Value</th><th>PO GV</th><th>Cumulative Amount</th><th>Payment Terms</th></tr></thead>");
                 html_Header.Append("<tbody>");
                 txt_Request.Text = Convert.ToString(ds.Tables[0].Rows[0]["request_id"]);
                 txt_Vendor_MAilid.Text = Convert.ToString(ds.Tables[0].Rows[0]["Init_Email"]);
                 txt_Unique_No.Text = Convert.ToString(ds.Tables[0].Rows[0]["Unique_No"]);
                // txt_Invoice_No.Text = Convert.ToString(ds.Tables[0].Rows[0]["Invoice_No"]);
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     DataTable dts = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getinco", ref isdata, Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]));
                     string Inco_Terms = string.Empty; 
                     if (dts.Rows.Count > 0 && dts != null)
                     {
                         Inco_Terms = Convert.ToString(dts.Rows[0]["INCO_TERMS1"]);
                         Inco_Terms += " - " + Convert.ToString(dts.Rows[0]["INCO_TERMS2"]);
                     }
			
                     var ABC = crypt.Encryptdata(Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]));
                     html_Header.Append("<tr><td>" + Convert.ToString(ds.Tables[0].Rows[0]["request_id"]) + "</td>");
                     html_Header.Append("<td>" + Convert.ToString(ds.Tables[0].Rows[0]["vendor_name"]) + "</td>");
                     html_Header.Append("<td><a href='#vendorinfo' role='button' id='ven1' data-toggle='modal'>" + Convert.ToString(ds.Tables[0].Rows[0]["vendor_code"]) + "</a></td>");
                     html_Header.Append("<td><a href='#' role='button' id='anc1' onclick='viewData(" + (1) + ")'>" + Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]) + "</a><input type='text' id='encrypt_po_1' value=" + ABC + " style='display:none'></td>");
                     html_Header.Append("<td>" + Convert.ToString(ds.Tables[0].Rows[0]["date"]) + "</td>");
                     html_Header.Append("<td>" + Convert.ToString(ds.Tables[0].Rows[0]["currency"]) + "</td>");
                     html_Header.Append("<td>" + Convert.ToString(ds.Tables[0].Rows[0]["created_date"]) + "</td>");
                     if (ds.Tables[0].Rows[0]["ITEM_CATEGORY"].ToString() == "9")
                     {
                         html_Header.Append("<td><input class='hidden' id='txt_NO_SE_PO' value='Service PO'>Service PO</td>");
                     }
                     else
                     {
                         html_Header.Append("<td><input class='hidden' id='txt_NO_SE_PO' value='Normal PO'>Material PO</td>");
                     }
                     html_Header.Append("<td>" + Inco_Terms + "</td>");
                     html_Header.Append("<td style='text-align:right'>" + Convert.ToString(ds.Tables[0].Rows[0]["po_value"]) + "</td>");
                     html_Header.Append("<td style='text-align:right'>" + Convert.ToString(ds.Tables[0].Rows[0]["po_gv"]) + "</td>");
                     html_Header.Append("<td style='text-align:right'>" + Convert.ToString(ds.Tables[0].Rows[0]["cum_amt"]) + "</td>");
                     html_Header.Append("<td>" + Convert.ToString(ds.Tables[0].Rows[0]["PAYMENT_TERMS"]) + "</a></td></tr>");
                    
                     div_PaymentTemSetPoNo.InnerText = div_IcoTemSetPoNo.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]);
                    

                     dts = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getpayment_term", ref isdata, Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]));
                     if (dts.Rows.Count > 0)
                     {
                         Pay1.Text = Convert.ToString(dts.Rows[0]["ZTAGG"]);
                         Pay2.Text = Convert.ToString(dts.Rows[0]["ZFAEL"]);
                         Pay3.Text = Convert.ToString(dts.Rows[0]["ZTAG1"]);
                     }

                     DataTable dtVendor = (DataTable)ActionController.ExecuteAction("", "Edit_Profile.aspx", "getvendordetails", ref isdata, Convert.ToString(ds.Tables[0].Rows[0]["vendor_code"]));

                     if (dtVendor.Rows.Count > 0)
                     {
                         lblName.Text = Convert.ToString(dtVendor.Rows[0]["Vendor_Name"]).Trim();
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
                 txt_In_Date.Text = (Convert.ToString(ds.Tables[0].Rows[0]["Invoice_Date"]));
                 html_Header.Append("</tbody></table>");
                 //html_Header.Append("</div>");
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
                 //html_Header.Append("</div></div></div>");

                 /*******************************************************************************************************************************************************************************************************/

                 //html_Detail.Append("<div class='col-md-12'><div class='panel panel-success'><div class='panel-heading'><h4 class='panel-title'>PO Details</h4></div><div class='panel-body'><div class='table-responsive'>");
                 html_Detail.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Material No</th><th>Plant</th><th>Material Description</th><th>Quantity</th><th>UOM</th><th>Net Price</th><th>Amount</th><th>Dispatch Qty</th><th>Goods Received Qty</th><th>Schedule</th></tr></thead>");
                 html_Detail.Append("<tbody>");

                 if (ds.Tables[1].Rows.Count > 0)
                 {
                     for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                     {
                         html_Detail.Append("<tr><td>" + Convert.ToString(ds.Tables[1].Rows[i]["material_no"]) + "</td>");
                         html_Detail.Append("<td class='hidden'><input type='text' id='txt_Mat_no" + ( i + 1 ) + "' value=" + Convert.ToString(ds.Tables[1].Rows[i]["material_no"])+ "></td>");
                         html_Detail.Append("<td>" + Convert.ToString(ds.Tables[1].Rows[i]["plant"]) + "</td>");
                         html_Detail.Append("<td>" + Convert.ToString(ds.Tables[1].Rows[i]["storage_location"]) + "</td>");
                         html_Detail.Append("<td>" + Convert.ToString(ds.Tables[1].Rows[i]["quantity"]) + "</td>");
                         html_Detail.Append("<td>" + Convert.ToString(ds.Tables[1].Rows[i]["uom"]) + "</td>");
                         html_Detail.Append("<td style='text-align:right'>" + Convert.ToString(ds.Tables[1].Rows[i]["net_price"]) + "</td>");
                         html_Detail.Append("<td style='text-align:right'>" + Convert.ToString(ds.Tables[1].Rows[i]["amount"]) + "</td>");
                         //html_Detail.Append("<td>" + Convert.ToString(ds.Tables[1].Rows[i]["material_group"]) + "</td>");
                         html_Detail.Append("<td>" + Convert.ToString(ds.Tables[1].Rows[i]["dispatch_quantity"]) + "</td>");
                         html_Detail.Append("<td>" + Convert.ToString(ds.Tables[1].Rows[i]["gr_quantity"]) + "</td>");
                         html_Detail.Append("<td class='hidden'>" + Convert.ToString(ds.Tables[1].Rows[i]["PK_Dispatch_Note_HDR_ID"]) + "</td>");
                         html_Detail.Append("<td><a href='#schedule' data-toggle='modal' onclick='getSchedule("+( i + 1 ) + ")'><img src='../../Img/index.jpg' style='margin-left:10px' height='20' width='20' alt='Smiley face' title='Question'></a></td></tr>");
                      
                     }
                 }

                 html_Detail.Append("</tbody></table>");

                 //html_Detail.Append("</div></div></div></div>");

                 /******************************************************************************************************************************************************************************************************/

                 html_Document.Append("<div class='col-md-6'><div class='panel panel-success'><div class='panel-heading'><h4 class='panel-title'>Dispatch Details</h4></div><div class='panel-body'><div class='table-responsive'>");
                 html_Document.Append("<table class='table table-bordered'><thead></thead>");
                 html_Document.Append("<tbody>");
                 html_Document.Append("<tr><td class='col-md-3'>Transporter Name</td><td class='col-md-9'><lable>" + Convert.ToString(ds.Tables[0].Rows[0]["Transporter_Name"]) + " </lable></td></tr>");
                 html_Document.Append("<tr><td class='col-md-3'>Vehicle No</td><td class='col-md-9'><lable>" + Convert.ToString(ds.Tables[0].Rows[0]["vehicle_no"]) + " </lable></td></tr>");
                 html_Document.Append("<tr><td class='col-md-3'>Contact Person Name</td><td class='col-md-9'><lable>" + Convert.ToString(ds.Tables[0].Rows[0]["contact_person_name"]) + " </lable></td></tr>");
                 html_Document.Append("<tr><td class='col-md-3'>Contact No</td><td class='col-md-9'><lable>" + Convert.ToString(ds.Tables[0].Rows[0]["contact_no"]) + "</lable></td></tr>");
                 html_Document.Append("<tr><td class='col-md-3'>LR No.</td><td class='col-md-9'><lable>" + Convert.ToString(ds.Tables[0].Rows[0]["lr_no"]) + " </lable></td></tr>");
                 html_Document.Append("<tr><td class='col-md-3'>LR Date</td><td class='col-md-9'><lable>" + Convert.ToString(ds.Tables[0].Rows[0]["lr_date"]) + "</lable></td></tr>");
                 html_Document.Append("</tbody>");
                 html_Document.Append("</table></div></div></div></div>");

                 /*******************************************************************************************************************************************************************************************************/

                 html_Document.Append("<div class='col-md-6'><div class='panel panel-success'><div class='panel-heading'><h4 class='panel-title'>Documents</h4></div><div class='panel-body' style='height:190px; overflow:auto'><div class='table-responsive'>");
                 html_Document.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Document Name</th><th>File Name</th></thead></tr>");
                 html_Document.Append("<tbody>");
                 DataTable dt = (DataTable)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getfilenames", ref isdata, "DISPATCH NOTE", Convert.ToString(ds.Tables[0].Rows[0]["request_id"]));

                 if (dt.Rows.Count > 0)
                 {
                     for (int i = 0; i < dt.Rows.Count; i++)
                     {
                         html_Document.Append("<tr><td>" + Convert.ToString(dt.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a OnClick='downloadfiles(" + Convert.ToString(dt.Rows[i]["PK_ID"]) + ")'>" + Convert.ToString(dt.Rows[i]["FILENAME"]) + "</a></td></tr>");
                     }
                 }
                 html_Document.Append("</tbody>");
                 html_Document.Append("</table></div></div></div></div>");

                 /*******************************************************************************************************************************************************************************************************/

                 html_Document.Append("<div class='col-md-6'><div class='panel panel-success'><div class='panel-heading'><h4 class='panel-title'>Remarks</h4></div><div class='panel-body'>");
                 html_Document.Append("<textarea name='txtRemark' cols='60' class='form-control' ID='txtRemark'></textarea></div></div></div>");

                 /*******************************************************************************************************************************************************************************************************/
             }

             txt_Approver.Text = Convert.ToString(ds.Tables[0].Rows[0]["vendor_code"]);
            // txt_Mat_no.Text = Convert.ToString(ds.Tables[1].Rows[0]["material_no"]);
             txt_Po_Number.Text = Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]);
             div_header.InnerHtml = Convert.ToString(html_Header);
             div_detail.InnerHtml = Convert.ToString(html_Detail);
             div_doc.InnerHtml = Convert.ToString(html_Document);

         }
         catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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
		string isapprovermail = string.Empty;
		DataTable db = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getmailid", ref isapprovermail, txt_Po_Number.Text);
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
                if (ddl_Action.SelectedItem.Text == "Approve")
                {
                    txt_Condition.Text = "1";
                    txt_Audit.Text = "DISPATCH CREATION NOTE APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "PODispatchDeatails_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddl_Action.SelectedItem.Text);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Username.Text;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "98", "DISPATCH CREATION NOTE APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
				string mail = "https://esp.sudarshan.com/Sudarshan-Portal/Login.aspx";
                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "PODispatchDeatails.aspx", "insertmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "DISPATCH CREATION NOTE APPROVAL", "SUBMIT", txt_Vendor_MAilid.Text, txtEmailID.Text, "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/><pre><font size='3'>The Payment Request Approved Successfully.</font></pre><p/><pre><font size='3'>Unique No: " + txt_Unique_No.Text + "</font></pre><p/><pre><font size='3'>Dispatch No: <b>" + txt_Request.Text + "</font></pre><p/><pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre><font size='3'>INTERNET URL:<a data-cke-saved-href={"+mail+"} href={"+mail+"}>"+mail+"</a></font></pre></br><pre><font size='3'>Regards</font></pre><pre><font size='3'>Reporting Admin</font></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>", txt_Request.Text);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('PO Dispatch Request Approved Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }

                    }
                }
                else if (ddl_Action.SelectedItem.Text == "Reject")
                {
                    txt_Condition.Text = "2";
                    txt_Audit.Text = "DISPATCH CREATION NOTE APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "PODispatchDeatails_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddl_Action.SelectedItem.Text);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = "";
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "99", "DISPATCH CREATION NOTE APPROVAL", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
				string mail = "https://esp.sudarshan.com/Sudarshan-Portal/Login.aspx";
                                string emailid = (string)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "insertmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "DISPATCH CREATION NOTE APPROVAL", "SUBMIT", txt_Vendor_MAilid.Text,  txtApproverEmail.Text, "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/><pre><font size='3'>Dispatch Note Rejected. Please Contact Warehouse Manager.</font></pre><p/><pre><font size='3'>Dispatch No: <b>" + txt_Request.Text + "</font></pre><p/><pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre><font size='3'>INTERNET URL:<a data-cke-saved-href={"+mail+"} href={"+mail+"}>"+mail+"</a></font></pre></br><pre><font size='3'>Regards</font></pre><pre><font size='3'>Reporting Admin</font></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>", txt_Request.Text);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('PO Dispatch Request Rejected Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                    }
                }
                else if (ddl_Action.SelectedItem.Text == "Send-Back")
                {
                    txt_Condition.Text = "3";
                    txt_Audit.Text = "DISPATCH CREATION NOTE APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "PODispatchDeatails_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddl_Action.SelectedItem.Text);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Approver.Text;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "100", "DISPATCH CREATION NOTE APPROVAL", "SEND BACK", txt_Username.Text, txt_Approver.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
                                string emailid = (string)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "insertmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "DISPATCH CREATION NOTE APPROVAL", "SUBMIT", txt_Vendor_MAilid.Text, "", "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/><pre><font size='3'>The Payment Request Send-Back to Vendor.</font></pre><p/><pre><font size='3'>Dispatch No: <b>" + txt_Request.Text + "</font></pre><p/><pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre><font size='3'>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</font></pre></br><pre><font size='3'>Regards</font></pre><pre><font size='3'>Reporting Admin</font></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>", txt_Request.Text);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('PO Dispatch Request Send-Back Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
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

        }

        return filename;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillschedule(string PO_No,string mat_no)
    {
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getschedule", ref isValid,PO_No, mat_no);
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
}