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


public partial class Advance_Request_Deviate_Approval_Foreign : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    StringBuilder str = new StringBuilder();
    ListItem Li = new ListItem("--Select One--", "");
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Request_Deviate_Approval_Foreign));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                        step_name.Text = Convert.ToString(Request.QueryString["step"]);
                    }

                    fillAction();
                    Initialization();

                }
            }
        }
        catch (Exception Exc) { }
    }

    private void Initialization()
    {
        try
        {
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "getdetails", ref isdata, txtProcessID.Text, txtInstanceID.Text);
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_ADVANCE_F_HDR_ID"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                txt_Initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMAIL_ID"]);


                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "pgettraveluser", ref isdata, txt_Initiator.Text);
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
                    span_division.InnerHtml = "NA";
                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "pgettravelrequestapprover", ref isdata, txt_Initiator.Text);
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
                                span_Dapp_name.InnerHtml = span_DApprover.InnerHtml = "NA";
                                doa_user.Text = "NA";
                            }
                            doa_email.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver_email"]);
                        }
                        else
                        {
                            span_Approver.InnerHtml = doa_user.Text = "NA";
                        }
                    }
                    fillPolicy_Details();
                    fillDocument_Details();
                    fillAuditTrailData();
                    fillData(dsData.Tables[0]);

                    //string ISValid = string.Empty;
                    //string str = string.Empty;
                    //DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref ISValid, span_advance.InnerHtml, "AdGLcode");

                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }

    }
    public void fillPolicy_Details()
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

                isValid = string.Empty;
                DataTable dtamt1 = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref isValid, txt_designation.Text, "AdDesignationForeign");

                if (dtamt1 != null && dtamt1.Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Designation</th><th>Country</th><th>Amount(Foreign Currency)</th><th>Effective Date</th></tr></thead>";

                    for (int i = 0; i < dtamt1.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + span_designation.InnerHtml + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["country"]) + "</td><td style='text-align:right'>" + Convert.ToString(dtamt1.Rows[i]["AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["EFFECTIVE_DATE"]) + "</td></tr>";

                    }
                    DisplayData += "</table>";
                }

                div_policy.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    public void fillData(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();

        //advance for foreign

        sb.Append("<div class='col-md-12' id='advance_Foreign'>");
        sb.Append("<div class='form-horizontal'>");
        sb.Append("<div class='form-group'><div class='col-md-1'></div>");
        sb.Append("<label class='col-md-2'>Region From</label>");
        sb.Append("<div class='col-md-3'>");
        if (Convert.ToString(dt.Rows[0]["FCOUNTRYNAME"]) != "")
        {
            sb.Append(Convert.ToString(dt.Rows[0]["FCOUNTRYNAME"]));
        }
        sb.Append("</div>");
        sb.Append("<label class='col-md-2'>Region To</label>");
        sb.Append("<div class='col-md-3'>");

        if (Convert.ToString(dt.Rows[0]["TCOUNTRYNAME"]) != "")
        {
            sb.Append(Convert.ToString(dt.Rows[0]["TCOUNTRYNAME"]));
        }
        sb.Append("</div>");
        sb.Append("</div></div>");
        sb.Append("<div class='form-group'><div class='col-md-1'></div>");
        sb.Append("<label class='col-md-2'>City From</label>");
        sb.Append("<div class='col-md-3'>");
        if (Convert.ToString(dt.Rows[0]["FK_CITY_FRM_ID"]) != "-1")
        {
            sb.Append(Convert.ToString(dt.Rows[0]["fcity"]));
        }
        else
        {
            sb.Append("Other");
        }

        sb.Append("</div>");

        sb.Append("<label class='col-md-2'>City To</label>");
        sb.Append("<div class='col-md-3'>");
        if (Convert.ToString(dt.Rows[0]["FK_CITY_TO_ID"]) != "-1")
        {
            sb.Append(Convert.ToString(dt.Rows[0]["tcity"]));
        }
        else
        {
            sb.Append("Other");
        }
        sb.Append("</div></div>");
        if (Convert.ToString(dt.Rows[0]["FK_CITY_FRM_ID"]) == "-1" || Convert.ToString(dt.Rows[0]["FK_CITY_TO_ID"]) == "-1")
        {
            sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'></label>");
            sb.Append("<div class='col-md-3'>");
            if (Convert.ToString(dt.Rows[0]["FK_CITY_FRM_ID"]) == "-1")
            {
                sb.Append(Convert.ToString(dt.Rows[0]["PLACE_FROM_OTHER"]));
            }

            sb.Append("</div>");


            sb.Append("<label class='col-md-2'></label>");
            sb.Append("<div class='col-md-3'>");
            if (Convert.ToString(dt.Rows[0]["FK_CITY_TO_ID"]) == "-1")
            {
                sb.Append(Convert.ToString(dt.Rows[0]["PLACE_TO_OTHER"]));
            }
            sb.Append("</div></div>");
        }
        sb.Append("<div class='form-group'><div class='col-md-1'></div> ");
        sb.Append("<label class='col-md-2'>Currency</label>");
        sb.Append("<div class='col-md-3'><div class='input-group' id='Div1'>" + (dt.Rows[0]["CURRENCY"]).ToString());
        sb.Append("</div></div></div>");


        sb.Append("<div class='form-group'><div class='col-md-1'></div> ");
        sb.Append("<label class='col-md-2'>From Date</label>");
        sb.Append("<div class='col-md-3'><div class='input-group' id='Div1'>" + Convert.ToDateTime(dt.Rows[0]["from_date"]).ToString("dd-MMM-yyyy"));
        sb.Append("</div></div>");
        sb.Append("<label class='col-md-2'>To Date</label><div class='col-md-3'><div class='input-group' id='Div6'>" + Convert.ToDateTime(dt.Rows[0]["to_date"]).ToString("dd-MMM-yyyy"));
        sb.Append("</div></div><div class='col-md-1'></div></div>");

        sb.Append("<div class='form-group'><div class='col-md-1'></div>");
        sb.Append("<label class='col-md-2'>Allowed Amount" + "(" + Convert.ToString(dt.Rows[0]["CURRENCY"]) + ")" + "</label><div class='col-md-3'>");
        string amount = string.Empty;

        amount = dt.Rows[0]["allowed_amount"].ToString();
        sb.Append(amount);
        sb.Append("</div>");
        sb.Append("<label class='col-md-2'>Amount(Foreign Currency)</label>");
        sb.Append("<div class='col-md-3'>" + Convert.ToString(dt.Rows[0]["amount"]) + "</div><div class='col-md-1'></div></div>");
        txtDummy.Text = Convert.ToString(dt.Rows[0]["amount"]);
        sb.Append("<div class='form-group'><div class='col-md-1'></div>");
        sb.Append("<label class='col-md-2'>Advance Mode(Currency)</label>");
        sb.Append("<div class='col-md-3'>" + Convert.ToString(dt.Rows[0]["CURRENCY_AMOUNT"]) + "</div>");


        sb.Append("<label class='col-md-2'>Advance Mode(Forex Card)</label>");
        sb.Append("<div class='col-md-3'>" + Convert.ToString(dt.Rows[0]["FOREX_CARD"]) + "</div><div class='col-md-1'></div></div>");

        if (Convert.ToInt32(dt.Rows[0]["amount"]) > Convert.ToInt32(dt.Rows[0]["allowed_amount"]))
        {
            txt_Deviate.Text = "1";
        }
        sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label>");
        sb.Append("<div class='col-md-8'>" + Convert.ToString(dt.Rows[0]["remark"]) + "</div></div>");
        sb.Append("</div></div>");

        div_LocalData.InnerHtml = Convert.ToString(sb);
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
                string remark = txtRemark.Value;

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
                    double z = Convert.ToDouble(txtDummy.Text);
                    //ADVANCE REQUEST FOREIGN ACCOUNT PAYABLE APPROVAL

                    DTAP = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "getaccapprover", ref ref_data, "ADVANCE REQUEST FOREIGN ACCOUNT PAYABLE APPROVAL", 0, txt_paymode.Text);

                    if (DTAP != null)
                    {
                        if (DTAP.Rows.Count > 0)
                        {
                            txt_Condition.Text = "3";
                            string isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddlAction.SelectedItem.Text);
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
                                    string release_id = (string)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, step_name.Text, "APPROVE");
                                    if (release_id != "")
                                    {
                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "APPROVE", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                        if (isCreate)
                                        {
                                            try
                                            {

                                                msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been sent to Accounts for payment approval.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST DEVIATION APPROVAL", "APPROVE", txtApproverEmail.Text, Init_Email.Text, msg, "Request No: " + spn_req_no.InnerHtml);

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
                            }//

                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Accounts For Payment Approval Not Found.!');}</script>");
                        }
                    }
                    //}
                }
                else if (ddlAction.SelectedItem.Text == "Reject")
                {
                    txt_Condition.Text = "2";
                    txt_Audit.Text = "FOREIGN ADVANCE REQUEST DEVIATION APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddlAction.SelectedItem.Text);
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
                        string release_id = (string)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, step_name.Text, "REJECT");
                        if (release_id != "")
                        {
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Rejected.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST DEVIATION APPROVAL", "REJECT", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been Rejected...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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
                    string isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddlAction.SelectedItem.Text);
                    if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = ref_data.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Initiator.Text;
                        string ref_data1 = string.Empty;
                        string release_id = (string)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, step_name.Text, "SEND-BACK");
                        if (release_id != "")
                        {
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "SEND-BACK", txt_Username.Text, txt_Approver.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Sent back to the initiator.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST DEVIATION APPROVAL", "SEND-BACK", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + spn_req_no.InnerHtml);
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
    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
        }
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
    private void fillAction()
    {
        try
        {
            string Isdata = string.Empty;
            ListItem li = new ListItem("--Select One--", "");
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "action", ref Isdata);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlAction.DataSource = dt;
                ddlAction.DataTextField = "ACTION_NAME";
                ddlAction.DataValueField = "ACTION_NAME";
                ddlAction.DataBind();
                ddlAction.Items.Insert(0, li);
            }
        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
    }

    private void fillDocument_Details()
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                GetData getdata = new GetData();
                divalldata.InnerHtml = getdata.fillDocumentDtl("ADVANCE", spn_req_no.InnerHtml);

            }
            catch (Exception ex)
            {
                // Logger.WriteEventLog(false, ex);
            }
        }
    }
}