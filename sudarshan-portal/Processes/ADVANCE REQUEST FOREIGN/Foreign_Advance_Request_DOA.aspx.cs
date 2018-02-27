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

public partial class Foreign_Advance_Request_DOA : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    StringBuilder str = new StringBuilder();
    ListItem Li = new ListItem("--Select One--", "0");
    DataTable dt = new DataTable();
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Foreign_Advance_Request_DOA));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Session["USER_ADID"].ToString();
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                        step_name.Text = Convert.ToString(Request.QueryString["step"]);
                    }
                    step_name.Text = "FOREIGN ADVANCE REQUEST DEVIATION APPROVAL";

                    Initialization();
                    fillPolicy_Details();
                    fillAuditTrail();
                }
            }
        }
        catch (Exception Exc) { }
    }
    public void fillAuditTrail()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "getaudit", ref isValid, txtProcessID.Text, txtInstanceID.Text);
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
            div_audit.InnerHtml = tblHTML.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    private void fillPolicy_Details()
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


                DataTable dtamt1 = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref isValid, lbl_Grade.Text, "AdDesignationForeign");

                if (dtamt1 != null && dtamt1.Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Grade</th><th>Country</th><th>Amount(Foreign Currency)</th><th>Currency</th></tr></thead>";

                    for (int i = 0; i < dtamt1.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dtamt1.Rows[i]["GRADE"]) + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["COUNTRY_NAME"]) + "</td><td style='text-align:right'>" + Convert.ToString(dtamt1.Rows[i]["FTE_AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["ADV_CURRENCY"]) + "</td></tr>";
                    }
                    DisplayData += "</table>";
                }

                policy_data.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    private void Initialization()
    {
        string IsData = string.Empty;
        string IsDatam = string.Empty;
        string IsData1 = string.Empty;
        string IsData2 = string.Empty;
        string IsData3 = string.Empty;

        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "addetails", ref IsData, txtProcessID.Text, txtInstanceID.Text);
        if (dsData != null)
        {
            if (dsData.Tables[0].Rows.Count > 0)
            {
                initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["emp_adid"]);
                spn_req_no.Text = txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["request_no"]);
                req_date.Text = Convert.ToDateTime(dsData.Tables[0].Rows[0]["CREATION_DATE"]).ToString("dd-MMM-yyyy");
                spn_F_Date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["FROM_DATE"]).ToString("dd-MMM-yyyy");
                spn_T_Date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["TO_DATE"]).ToString("dd-MMM-yyyy");
                spn_F_Region.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["F_COUNTRY"]);
                spn_T_Region.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["T_COUNTRY"]);
                spn_F_City.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["F_CITY"]);
                spn_T_City.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["T_CITY"]);
                pk_base_currency.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["BASE_CURRENCY"]);
                base_currency.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY_NAME"]);
                base_currency_rate.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["BASE_CURRENCY_RATE"]);
                spn_Remark.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REMARK"]);
                txt_amt_limit.Text = Convert.ToString(dsData.Tables[0].Rows[0]["FTE_AMOUNT"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMAIL_ID"]);
                txt_Deviate.Text = Convert.ToString(dsData.Tables[0].Rows[0]["REQ_DEVIATE"]);

                int days = 0;
                DateTime fdate = Convert.ToDateTime(spn_F_Date.InnerHtml);
                DateTime tdate = Convert.ToDateTime(spn_T_Date.InnerHtml);
                days = Convert.ToInt32((tdate - fdate).TotalDays) + 1;
                lbl_allowedamt.Text = Convert.ToString(days * Convert.ToInt32(txt_amt_limit.Text));
                req_base_currency.InnerHtml = Convert.ToString(dsData.Tables[3].Rows[0]["REQ_AMOUNT"]);
                bind_Line_Item(dsData.Tables[1]);
                fillDocument_Details(dsData.Tables[2]);
            }
        }
        DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "pgettraveluser", ref IsData, initiator.Text);
        if (dtUser != null && dtUser.Rows.Count > 0)
        {
            lbl_EmpCode.Text = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
            lbl_EmpName.Text = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
            lbl_Dept.Text = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
            lbl_Grade.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
            lbl_CostCenter.Text = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
            lbl_MobileNo.Text = dtUser.Rows[0]["MOBILE_NO"].ToString();
            lbl_desgnation.Text = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
            txt_desg_id.Text = Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);
            // lbl_GLCode.Text = Convert.ToString(dtUser.Rows[0]["GL_CODE"]);

            lbl_division.Text = "NA";
            if (Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]).Trim() != "")
            {
                span_Ifsc.InnerHtml = Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]);
            }
            if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]) != "")
            {
                lbl_bankAccNo.Text = Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]);
            }
            else
            {
                lbl_bankAccNo.Text = "NA";
            }
            DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "pgettravelrequestapprover", ref IsData1, initiator.Text);
            if (dtApprover != null)
            {
                if (dtApprover.Rows.Count > 0)
                {
                    if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                    {
                        lbl_AppName.Text = Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                        txt_approvar.Text = Convert.ToString(dtApprover.Rows[0]["approver"]);
                    }
                    else
                    {
                        lbl_AppName.Text = "NA";
                        txt_approvar.Text = "NA";
                    }
                    txt_Approver_Email.Text = Convert.ToString(dtApprover.Rows[0]["approver_email"]);
                    if (Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "0")
                    {
                        doa_user.Text = span_DApprover.InnerHtml = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                        span_Dapp_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);

                    }
                    else
                    {
                        doa_user.Text = span_Dapp_name.InnerHtml = span_DApprover.InnerHtml = "NA";
                    }
                    doa_email.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver_email"]);
                }

            }
        }

    }

    protected void bind_Line_Item(DataTable dtLine)
    {
        string data = "";
        data = "<table class='table table-bordered' id='tbl_Data'><thead>";
        data += "<tr class='grey'><th style='width: 2%; text-align: center'>#</th><th style='width: 10%; text-align: center'>Requested Currency</th>";
        data += "<th style='width: 10%; text-align: center'>Currency Mode</th><th style='width: 10%; text-align: center'>Currency Amount</th>";
        data += "<th style='width: 10%; text-align: center'>Exchange Rate</th><th style='width: 15%; text-align: center'>Equivalent To Base Currency</th>";
        data += "</tr></thead><tbody>";
        if (dtLine != null && dtLine.Rows.Count > 0)
        {
            for (int index = 0; index < dtLine.Rows.Count; index++)
            {
                data += "<tr>";
                data += "<td>" + Convert.ToString(index + 1) + "</td>";
                data += "<td>" + Convert.ToString(dtLine.Rows[index]["CURRENCY_NAME"]) + "</td>";
                data += "<td>" + Convert.ToString(dtLine.Rows[index]["CURRENCY_MODE"]) + "</td>";
                data += "<td>" + Convert.ToString(dtLine.Rows[index]["CURRENCY_AMOUNT"]) + "</td>";
                //data += "<td>" + Convert.ToString(dtLine.Rows[index]["EXC_RATE_IN_INR"]) + "</td>";
if(Convert.ToDouble(dtLine.Rows[index]["CURRENCY_AMOUNT"])==0 || Convert.ToDouble(dtLine.Rows[index]["CURRENCY_AMOUNT"])==0)
		{
			data += "<td>0</td>";
		}
		else{
			if((Convert.ToDouble(dtLine.Rows[index]["EQ_BASE_AMOUNT"])/Convert.ToDouble(dtLine.Rows[index]["CURRENCY_AMOUNT"]))<1)
			{
				data += "<td>0" + (Convert.ToDouble(dtLine.Rows[index]["EQ_BASE_AMOUNT"])/Convert.ToDouble(dtLine.Rows[index]["CURRENCY_AMOUNT"])).ToString("#.00") + "</td>";
			}
			else{
				data += "<td>" + (Convert.ToDouble(dtLine.Rows[index]["EQ_BASE_AMOUNT"])/Convert.ToDouble(dtLine.Rows[index]["CURRENCY_AMOUNT"])).ToString("#.00") + "</td>";
			}
		}
                data += "<td>" + Convert.ToString(dtLine.Rows[index]["EQ_BASE_AMOUNT"]) + "</td>";
                data += "</tr>";
            }
        }
        data += "</tbody></table>";
        div_details.InnerHtml = Convert.ToString(data);
    }

    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "script", "window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');", true);
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
        }
    }

    private void fillDocument_Details(DataTable dtDocs)
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

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Type</th><th>File Name</th></tr></thead><tbody>";
                if (dtDocs != null && dtDocs.Rows.Count > 0)
                {
                    for (int index = 0; index < dtDocs.Rows.Count; index++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dtDocs.Rows[index]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick='download_files(" + (index + 1) + ")'>" + Convert.ToString(dtDocs.Rows[index]["FILENAME"]) + "</a></td></tr>";
                    }
                }
                DisplayData += "</tbody></table>";
                div_Doc.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
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

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {

                string isInserted = string.Empty;
                string ref_data = string.Empty;
                txt_Audit.Text = "FOREIGN ADVANCE REQUEST DEVIATION APPROVAL";
                if (ddlAction.SelectedItem.Text == "Approve")
                {
                    DataTable DTAP = new DataTable();
                    string msg = "";
                    string emailid = string.Empty;
                    CryptoGraphy crypt = new CryptoGraphy();
                    string req_no = crypt.Encryptdata(txt_Request.Text);
                    string process_name = crypt.Encryptdata("ADVANCE EXPENSE");
                    // txt_Deviate.Text = "1";
                    

                        DTAP = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "getaccapprover", ref ref_data, "ADVANCE REQUEST FOREIGN ACCOUNT PAYABLE APPROVAL", 0, 2);

                        if (DTAP != null)
                        {
                            if (DTAP.Rows.Count > 0)
                            {

                                txt_Condition.Text = "3";
                                string isSaved = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, spn_Remark.InnerHtml, ddlAction.SelectedItem.Text);
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
                                        string ref_data1 = string.Empty;
                                        string release_id = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, step_name.Text, "APPROVE");
                                        if (release_id != "")
                                        {
                                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "APPROVE", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);

                                            if (isCreate)
                                            {
                                                try
                                                {

                                                    //msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been sent to Accounts for payment approval.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Advance.aspx?P=ADVANCE REQUEST FOREIGN&R=" + spn_req_no.Text + "'>http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Advance.aspx?P=ADVANCE REQUEST FOREIGN&R=" + spn_req_no.Text + "</a></span></pre><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                    msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been sent to Accounts for payment approval.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Advance.aspx?P=ADVANCE REQUEST FOREIGN&R=" + spn_req_no.Text + "'>http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Advance.aspx?P=ADVANCE REQUEST FOREIGN&R=" + spn_req_no.Text + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";
                                                    emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST DEVIATION APPROVAL", "APPROVE", Init_Email.Text, txtApproverEmail.Text, msg, "Request No: " + spn_req_no.Text);

                                                }
                                                catch (Exception)
                                                {
                                                    throw;
                                                }
                                                finally
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been sent for account payment approval.!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Accounts For Payment Approval Not Found.!');}</script>");
                            }
                        }
                    
                    

                }
                else if (ddlAction.SelectedItem.Text == "Reject")
                {
                    txt_Condition.Text = "2";
                    txt_Audit.Text = "FOREIGN ADVANCE REQUEST DEVIATION APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, spn_Remark.InnerHtml, ddlAction.SelectedItem.Text);
                    if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = ref_data.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Username.Text;
                        string ref_data1 = string.Empty;
                        string release_id = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, step_name.Text, "REJECT");
                        if (release_id != "")
                        {
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Rejected.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST DEVIATION APPROVAL", "REJECT", Init_Email.Text, "", msg, "Request No: " + spn_req_no.Text);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been Rejected.');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                }
                else if (ddlAction.SelectedItem.Text == "Send-Back")
                {
                    txt_Condition.Text = "3";
                    txt_Audit.Text = "FOREIGN ADVANCE REQUEST DEVIATION APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, spn_Remark.InnerHtml, ddlAction.SelectedItem.Text);
                    if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = ref_data.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = initiator.Text;
                        string ref_data1 = string.Empty;
                        string release_id = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, step_name.Text, "SEND-BACK");
                        if (release_id != "")
                        {
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "SEND-BACK", txt_Username.Text, txt_approvar.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Sent back to the initiator.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST DEVIATION APPROVAL", "SEND-BACK", Init_Email.Text, "", msg, "Request No: " + spn_req_no.Text);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been Sent back to the initiator...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
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
}