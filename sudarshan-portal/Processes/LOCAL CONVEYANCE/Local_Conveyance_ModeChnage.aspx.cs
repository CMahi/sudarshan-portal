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

public partial class Local_Conveyance_ModeChnage : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Local_Conveyance_ModeChnage));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        //txtEmailID.Text = "arti@flologic.in";
                        if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["step"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                        {
                            txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                            txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                            txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                            txt_Step.Text = Convert.ToString(Request.QueryString["step"]);
                            txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                        }
                    }
                    //span_vdate.InnerHtml = DateTime.Now.ToString("dd-MMM-yyyy");
                    getUserInfo();
                    ShowLocalUser();
                    fillAdvanceAmount();                
                    fillPolicy_Details();
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
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

                DataSet dtamt = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "getexpense", ref isValid, "");
                if (dtamt != null && dtamt.Tables[0].Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Type of Vehicle</th><th>Present rate per KM (Rs)</th><th>Effective From</th><th>Effective To</th></tr></thead>";

                    for (int i = 0; i < dtamt.Tables[0].Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dtamt.Tables[0].Rows[i]["Vehicle_Type"]) + "</td><td>" + Convert.ToString(dtamt.Tables[0].Rows[i]["Rate_Per_KM"]) + "</td><td>" + Convert.ToString(dtamt.Tables[0].Rows[i]["from_Date"]) + "</td><td>" + Convert.ToString(dtamt.Tables[0].Rows[i]["to_Date"]) + "</td></tr>";

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
    protected void getUserInfo()
    {
        try
        {
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "pgetrequestdata", ref isdata, txtWIID.Text);
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_Local_Conveyance_HDR_Id"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                txt_Initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);
                txt_tremark.Value = Convert.ToString(dsData.Tables[0].Rows[0]["REMARK"]);


                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "pgettraveluser", ref isdata, txt_Initiator.Text);
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
                    DataTable dtcode = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "selectdetails", ref isdata, "Local Conveyance", "AdGLCode");
                   
                    span_div.InnerHtml = "NA";
                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "pgettravelrequestapprover", ref isdata, txt_Initiator.Text);
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
                                span_DApprover.InnerHtml = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                                span_Dapp_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);
                            }
                            else
                            {
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
                    fillAuditTrail();
                    fillLocation();
                    fillPayment_Mode();                
                    ddlAdv_Location.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["LOCATION"]);
                    ddl_Payment_Mode.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["PAYMENT_MODE"]);                
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
    protected void fillPayment_Mode()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtPayment = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "selectdetails", ref isdata, txt_Username.Text, "AdPaymentMode");
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
    protected void fillLocation()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtLocation = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "selectdetails", ref isdata, txt_Username.Text, "AdLocation");
            ddlAdv_Location.Items.Clear();
            if (dtLocation != null)
            {
                if (dtLocation.Rows.Count > 0)
                {
                    ddlAdv_Location.DataSource = dtLocation;
                    ddlAdv_Location.DataTextField = "LOCATION_NAME";
                    ddlAdv_Location.DataValueField = "PK_LOCATION_ID";
                    ddlAdv_Location.DataBind();
                }
            }
            ddlAdv_Location.Items.Insert(0, "---Select One---");
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
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

                DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "pgetrequestdata", ref isValid, txtWIID.Text);

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>";
                if (dsData != null)
                {
                    for (int i = 0; i < dsData.Tables[1].Rows.Count; i++)
                    {
                    
                        DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dsData.Tables[1].Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles(" + (i + 1) + ")>" + Convert.ToString(dsData.Tables[1].Rows[i]["filename"]) + "</a></td></tr>";
                    }
                }
                DisplayData += "</table>";
                divalldata.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }
    private void ShowLocalUser()
    {
        try
        {
            string IsValid = string.Empty;
            DataSet DTS = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "pgetrequestdata", ref IsValid, txtWIID.Text);
            DTS.Tables[2].Columns[0].ColumnName = "Sr.No";
            Session["ResultData"] = DTS;
            decimal total_Amount = 0;
            if (DTS.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < DTS.Tables[2].Rows.Count; i++)
                {
                    DTS.Tables[2].Rows[i]["Sr.No"] = i + 1;
                }

                StringBuilder str = new StringBuilder();
                str.Append("<table id='datatable1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th>#</th><th>Vehicle Type</th><th>From Location</th><th>To Location</th><th>From Date</th><th>To Date</th><th>KMs</th><th>Bill Amount</th></tr></thead>");
                str.Append("<tbody>");
                for (int i = 0; i < DTS.Tables[2].Rows.Count; i++)
                {
                    string kms = string.Empty;
                    if (DTS.Tables[2].Rows[i]["KMS"].ToString() == "0")
                    {
                        kms = "";
                    }
                    else
                    {
                        kms = DTS.Tables[2].Rows[i]["KMS"].ToString();
                    }
                    str.Append(" <tr>");
                    str.Append("<td align='left'>" + (i + 1) + "</td>");
                    str.Append("<td align='left'>" + DTS.Tables[2].Rows[i]["VEHICLE_TYPE"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Tables[2].Rows[i]["FROM_LOACATION"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Tables[2].Rows[i]["TO_LOACATION"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Tables[2].Rows[i]["From_Date"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Tables[2].Rows[i]["To_Date"].ToString() + "</td>");
                    str.Append("<td align='right'>" + kms + "</td>");
                    str.Append("<td align='right'>" + DTS.Tables[2].Rows[i]["BILL_AMOUNT"].ToString() + "</td>");
                    str.Append("</tr>");
                    total_Amount = total_Amount + Convert.ToDecimal(DTS.Tables[2].Rows[i]["BILL_AMOUNT"]);
                    txt_tremark.Value = Convert.ToString(DTS.Tables[2].Rows[i]["REMARK"].ToString());
                }
               
                str.Append("   </tbody>   </table> ");
                div_LocalData.InnerHtml = str.ToString();
                spn_Total.InnerHtml = Convert.ToString(total_Amount); 
                // ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);      
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Data Found !!')}</script>");
                div_LocalData.InnerHtml = null;
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

    }
    private void fillAdvanceAmount()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "pgetadvancedetails", ref isValid, txt_Initiator.Text, txt_pk_id.Text, 2);
            txt_adcount.Text = Convert.ToString(dt.Tables[0].Rows.Count);
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Amount</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
            {
                tblHTML.Append("<tr>");
                tblHTML.Append("<td>" + (Index + 1) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToDateTime(dt.Tables[0].Rows[Index]["ADVANCE_DATE"]).ToString("dd-MMM-yyyy") + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["amount"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["APPROVED"]) + "</td>");
                tblHTML.Append("</tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            div_Advance.InnerHtml = tblHTML.ToString();
            if (dt.Tables[0] != null)
            {
                double adv = Convert.ToDouble(dt.Tables[0].Rows[0]["amount"]);
                double exp = Convert.ToDouble(spn_Total.InnerHtml);
                if (exp > adv)
                {
                    spn_hdr.InnerHtml = "Payable Amount : ";
                    spn_val.InnerHtml = Convert.ToString(exp - adv);
                }
                else
                {
                    spn_hdr.InnerHtml = "Recovery Amount : ";
                    spn_val.InnerHtml = Convert.ToString(adv - exp);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string isSaved = string.Empty;
        string isChange = string.Empty;
        string isInserted = string.Empty;
        string ref_data = string.Empty;
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                txt_Audit.Text = "LOCAL CONVEYANCE PAYMENT MODE CHANGE";
                     // if (doa.Text == "")
                       // {
                            DataTable DTAP=new DataTable();
                      
                            if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                            {
                                DTAP = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "getaccapprover", ref ref_data, "LOCAL CONVEYANCE ACCOUNT PAYMENT APPROVAL", ddlAdv_Location.SelectedValue, ddl_Payment_Mode.SelectedValue);
                            }
                            else
                            {
                                DTAP = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "getaccapprover", ref ref_data, "LOCAL CONVEYANCE ACCOUNT PAYMENT APPROVAL", 0, ddl_Payment_Mode.SelectedValue);
                            }
                            if (DTAP != null)
                            {
                                if (DTAP.Rows.Count > 0)
                                {
                                    txt_Condition.Text = "3";
                                    isSaved = (string)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "update", ref ref_data, "0", txtProcessID.Text, txtInstanceID.Text, txt_Username.Text, "", "Submit", txt_Audit.Text);
                                    isChange = (string)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "updatemode", ref ref_data, ddlAdv_Location.SelectedValue, ddl_Payment_Mode.SelectedValue, txt_pk_id.Text);

                                    if (isSaved == null || ref_data.Length > 0 || isChange == "false")
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
                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "207", "LOCAL CONVEYANCE PAYMENT MODE CHANGE", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                        if (isCreate)
                                        {
                                            try
                                            {
                                                if ((Init_Email.Text != "") && (txtEmailID.Text != ""))
                                                {
                                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Local Conveyance Request has been sent to Accounts for payment approval.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE APPROVAL", "SUBMIT", txt_Approver_Email.Text, Init_Email.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Local Conveyance has been sent for account payment approval...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                            }
                                        }
                                    }//
                                }
                                else
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Approver Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                            else
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Approver Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                            }
                
             }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
    
    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
    }
   

    public void fillAuditTrail()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "getaudit", ref isValid, txtProcessID.Text, txtInstanceID.Text);
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