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
using System.Web.Services;

public partial class Other_Expense_Payment_Mode : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Other_Expense_Payment_Mode));
                if (!Page.IsPostBack)
                {

                    if (Session["USER_ADID"] != null)
                    {
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
                    }
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    getUserInfo();
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void getUserInfo()
    {
        try
        {
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetrequestdata", ref isdata, txtWIID.Text);
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["pk_other_expense_hdr_id"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["voucher_id"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                txt_Initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["email_id"]);
                req_remark.Text = Convert.ToString(dsData.Tables[0].Rows[0]["remark"]);
                txt_advance_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["advance_id"]);


                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetuser", ref isdata, txt_Initiator.Text);
                if (dtUser.Rows.Count > 0)
                {
                    empno.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
                    span_ename.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
                    span_cc.InnerHtml = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
                    span_dept.InnerHtml = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
                    span_grade.InnerHtml = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
                    span_mobile.InnerHtml = Convert.ToString(dtUser.Rows[0]["MOBILE_NO"]);
                    span_designation.InnerHtml = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
                    txt_designation.Text = Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);
                    //span_branch.InnerHtml = Convert.ToString(dtUser.Rows[0]["BRANCH_NAME"]);
                    if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]) != "")
                    {
                        span_bank_no.InnerHtml = Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]);
                    }
                    else
                    {
                        span_bank_no.InnerHtml = "NA";
                    }
                    if (Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]).Trim() != "")
                    {
                        span_Ifsc.InnerHtml = Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]);
                    }
                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetrequestapprover", ref isdata, txt_Initiator.Text);
                    if (dtApprover != null)
                    {
                        if (dtApprover.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                            {
                                span_Approver.InnerHtml = Convert.ToString(dtApprover.Rows[0]["approver"]);
                                span_app_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                            }
                            else
                            {
                                span_Approver.InnerHtml = "NA";
                                span_app_name.InnerHtml = "NA";
                            }
                            txt_Approver_Email.Text = Convert.ToString(dtApprover.Rows[0]["approver_email"]);


                            if (Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "0")
                            {
                                doa_user.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                                span_DApprover.InnerHtml = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                                span_Dapp_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);
                            }
                            else
                            {
                                doa_user.Text = "NA";
                                span_Dapp_name.InnerHtml = span_DApprover.InnerHtml = "NA";
                            }
                            doa_email.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver_email"]);
                        }
                        else
                        {
                            span_Approver.InnerHtml = doa_user.Text = "NA";
                        }
                    }

                    fillDocument_Details();

                    //if (Convert.ToString(dsData.Tables[0].Rows[0]["P_LOCATION"]) != "")
                    //{
                    //    ddlAdv_Location.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["P_LOCATION"]);
                    //}
                    //else
                    //{
                    //    ddlAdv_Location.InnerHtml = "NA";
                    //}
                    //ddl_Payment_Mode.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["P_MODE"]);

                    pmode_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["Payment_mode"]);
                    loc_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["cash_location"]);
                    fillAdvanceAmount();
                    fillRequest_data(dsData.Tables[1]);
                    fillAuditTrail();
                    fillLocation();
                    fillPayment_Mode();
                    ddl_Payment_Mode.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["Payment_mode"]);
                    if (Convert.ToString(dsData.Tables[0].Rows[0]["cash_location"]) != "0")
                    {
                        pl.Style.Add("display","normal");
                        pld.Style.Add("display", "normal");
                        ddlAdv_Location.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["cash_location"]);
                    }
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void fillLocation()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtLocation = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "selectlocation", ref isdata, "", "", "PAYMENT_LOCATION", "");
            ddlAdv_Location.Items.Clear();
            if (dtLocation != null)
            {
                if (dtLocation.Rows.Count > 0)
                {
                    ddlAdv_Location.DataSource = dtLocation;
                    ddlAdv_Location.DataTextField = "LOCATION NAME";
                    ddlAdv_Location.DataValueField = "PK_LOCATION_ID";
                    ddlAdv_Location.DataBind();
                }
            }
            //ddlAdv_Location.Items.Insert(0, "---Select One---");
            ddlAdv_Location.Items.Insert(0, new ListItem("---Select One---", "0"));
            ddlAdv_Location.Enabled = false;
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void fillPayment_Mode()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtPayment = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "selectlocation", ref isdata, "", "", "M_TRAVEL_PAYMENT_MODE", "");
            ddl_Payment_Mode.Items.Clear();
            if (dtPayment != null)
            {
                if (dtPayment.Rows.Count > 0)
                {
                    ddl_Payment_Mode.DataSource = dtPayment;
                    ddl_Payment_Mode.DataTextField = "PAYMENT_MODE";
                    ddl_Payment_Mode.DataValueField = "PK_PAYMENT_MODE";
                    ddl_Payment_Mode.DataBind();
                }
            }
            ddl_Payment_Mode.Items.Insert(0, "---Select One---");
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    private void fillAdvanceAmount()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "pgetadvancedetails", ref isValid, txt_Initiator.Text, txt_pk_id.Text, 2, "Other Expense Advance");
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Amount</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            for (int Index = 0; Index < dt.Rows.Count; Index++)
            {
                tblHTML.Append("<tr>");
                tblHTML.Append("<td><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Rows[Index]["PK_ADVANCE_HDR_ID"]) + "' style='display:none'>" + (Index + 1) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["REQUEST_NO"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["ADVANCE_DATE"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["amount"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["Approved"]) + "</td>");
                advance_amount.Text = Convert.ToString(dt.Rows[Index]["amount"]);
                tblHTML.Append("</tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            div_Advance.InnerHtml = tblHTML.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }

    protected void fillDocument_Details()
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
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetfilenames", ref isData, "OTHER EXPENSES", spn_req_no.InnerHtml);
                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>Description</th><th>File Name</th></tr></thead>";
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DisplayData += "<tr>";
                        DisplayData += "<td>" + Convert.ToString(dt.Rows[i]["DOCUMENT_TYPE"]) + "</td>";
                        DisplayData += "<td><a id='a_downloadfiles" + i + "' style='cursor: pointer' onclick=\"return downloadfiles('" + Convert.ToString(dt.Rows[i]["FILENAME"]) + "');\" >" + Convert.ToString(dt.Rows[i]["FILENAME"]) + "</a></td>";
                        DisplayData += "</tr>";
                    }
                }
                DisplayData += "</table>";
                div_docs.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    protected void fillRequest_data(DataTable dt)
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
                decimal total_amount = 0;
                DisplayData = "<table class='table table-bordered' id='tbl_Data'><thead><tr class='grey'> <th style='width:2%;'>#</th><th style='width:10%; text-align:center'>Expense Head</th><th style='width:10%; text-align:center'>GL Code</th><th style='width:10%; text-align:center'>Date</th><th style='width:10%; text-align:center'>Bill No</th><th style='width:10%; text-align:center'>Bill Date</th><th style='width:15%; text-align:center'>Particulars</th><th style='width:5%; text-align:center'>Amount</th><th style='width:10%; text-align:center'>Supporting Attachment</th><th style='width:10%; text-align:center'>Supporting Particulars</th></tr></thead>";
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DisplayData += "<tr>";
                        DisplayData += "<td align='center'>" + (i + 1) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["EXPENSE_HEAD"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["SAP_GLCode"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["date"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["billno"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["bill_date"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["remark"]) + "</td>";
                        DisplayData += "<td style='text-align:right'>" + Convert.ToString(dt.Rows[i]["amount"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["supp_doc"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["supp_particulars"]) + "</td>";
                        DisplayData += "</tr>";
                        total_amount += Convert.ToDecimal(dt.Rows[i]["amount"]);
                    }
                }
                DisplayData += "</table>";
                div_req_data.InnerHtml = DisplayData;
                spn_Total.InnerHtml = Convert.ToString(total_amount);
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string isSaved = string.Empty;
        string isInserted = string.Empty;
        string ref_data = string.Empty;
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                DataTable DTAP = new DataTable();
                if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                {
                    DTAP = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "getaccapprover", ref ref_data, "OTHER EXPENSE PAYMENT APPROVAL", ddlAdv_Location.SelectedValue, ddl_Payment_Mode.SelectedValue);
                }
                else
                {
                    DTAP = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "getaccapprover", ref ref_data, "OTHER EXPENSE PAYMENT APPROVAL", 0, ddl_Payment_Mode.SelectedValue);
                }
                if (DTAP != null)
                {
                    if (DTAP.Rows.Count > 0)
                    {
                        isSaved = (string)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "insert", ref ref_data, txt_pk_id.Text, txt_Username.Text, ddlAdv_Location.SelectedValue, ddl_Payment_Mode.SelectedValue, "", "", "", 0, 0, "", 3, 0);
                        if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                        {
                            string[] errmsg = ref_data.Split(':');
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                        }
                        else
                        {
                            string[] Dval = new string[DTAP.Rows.Count];
                            Dval[0] = "";
                            if (DTAP.Rows.Count > 0)
                            {
                                for (int i = 0; i < DTAP.Rows.Count; i++)
                                {
                                    Dval[i] = Convert.ToString(DTAP.Rows[i]["USER_ADID"]);
                                    if (txtApproverEmail.Text == "")
                                    {
                                        txtApproverEmail.Text = Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                                    }
                                    else
                                    {
                                        txtApproverEmail.Text = txtApproverEmail.Text + ',' + Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                                    }
                                }
                            }
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "204", "OTHER EXPENSE PAYMENT MODE CHANGE", "SUBMIT", txt_Username.Text, txt_Initiator.Text.Trim(), txt_Initiator.Text.Trim(), "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string auditid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Other_Expenses_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "OTHER EXPENSE PAYMENT MODE CHANGE", "USER", txt_Username.Text, "SUBMIT", "Change Payment Mode", "0", "0");

                                    //string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Payment Mode Has Been Changed Successfully and Sent For Account Approval.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    string msg = "";
                                    CryptoGraphy crypt = new CryptoGraphy();
                                    string process_name = crypt.Encryptdata("OTHER EXPENSES");
                                    string req_no = crypt.Encryptdata(spn_req_no.InnerHtml);
                                    if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                                    {
                                        msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Payment Mode Has Been Changed Successfully and Sent For Account Approval.</span></pre><pre><span style='font-size: medium;'>Request No: " + spn_req_no.InnerHtml + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                    }
                                    else
                                    {
                                        msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Payment Mode Has Been Changed Successfully and Sent For Account Approval.</span></pre><pre><span style='font-size: medium;'>Request No: " + spn_req_no.InnerHtml + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                    }

                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "OTHER EXPENSE PAYMENT MODE CHANGE", "SUBMIT", txtEmailID.Text, txtApproverEmail.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Payment Mode Has Been Changed Successfully and Sent For Account Approval...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payment Approver Not Available For Payment Mode : " + ddl_Payment_Mode.SelectedItem.Text + "  ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                    }
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payment Approver Not Available For Payment Mode : " + ddl_Payment_Mode.SelectedItem.Text + "  ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    public void fillAuditTrail()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "getaudit", ref isValid, txtProcessID.Text, txtInstanceID.Text);
            tblHTML.Append("<table ID='tblAut' class='table table-bordered'><thead><tr class='grey'><th>Sr.No.</th><th>Step-Name</th><th>Performer</th><th>Date</th><th>Action-Name</th><th>Remark</th></tr></thead>");
            tblHTML.Append("<tbody>");
            for (int Index = 0; Index < dt.Rows.Count; Index++)
            {
                tblHTML.Append("<tr>");
                tblHTML.Append("<td>" + (Index + 1) + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["STEPNAME"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["ACTIONBYUSER"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["ACTIONDATE"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["ACTION"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["REMARK"].ToString() + "</td>");
                tblHTML.Append("</tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            div_Audit.InnerHtml = tblHTML.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
}