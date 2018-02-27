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

public partial class Acccount_Payable_Approval : System.Web.UI.Page
{

    StringBuilder str = new StringBuilder();
    ListItem Li = new ListItem("--Select One--", "");
    DataTable dt = new DataTable();
    string suppdoc = string.Empty;
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Acccount_Payable_Approval));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Initiator.Text = Convert.ToString(Session["USER_ADID"]);
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                    }
                    fillAuditTrailData();
                    fillAction();
                    Initialization();
                    fillPolicy_Details();
                }
            }
        }
        catch (Exception Exc) { }
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

                DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getpolicy", ref isValid, lbl_Grade.Text);
                if (dtamt != null && dtamt.Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Grade Name</th><th>Grade Expense</th><th>Effective From</th><th>Effective To</th></tr></thead>";

                    for (int i = 0; i < dtamt.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dtamt.Rows[i]["GRADE_NAME"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["Grade_Expense"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["from_Date"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["to_Date"]) + "</td></tr>";

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
    private void Initialization()
    {
        string IsData = string.Empty;
        string IsData1 = string.Empty;
        string IsValid = string.Empty;
        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "pgetrequestdata", ref IsData1, txtWIID.Text);
        if (dsData != null)
        {
            txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_MobileCard_Expense_HDR_Id"]);
            txt_Username.Text = Convert.ToString(dsData.Tables[0].Rows[0]["CREATED_BY"]);
            Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);
            lbl_expensetype.Text = dsData.Tables[0].Rows[0]["FK_EXPENSEHEAD_ID"].ToString();
        }
        DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "pgettraveluser", ref IsData, txt_Username.Text);
        if (dtUser != null && dtUser.Rows.Count > 0)
        {
            lbl_EmpCode.Text = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
            lbl_EmpName.Text = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
            lbl_Dept.Text = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
            lbl_Grade.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
            lbl_CostCenter.Text = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
            lbl_date.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            lbl_desgnation.Text = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
            lbl_division.Text = "NA";
            if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]) != "")
            {
                lbl_bankAccNo.Text = Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]);
            }
            else
            {
                lbl_bankAccNo.Text = "NA";
            }
            if (Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]).Trim() != "")
            {
                span_Ifsc.InnerHtml = Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]);
            }
            DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "pgettravelrequestapprover", ref IsData1, txt_Username.Text);
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
                        //doa_user.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                        span_DApprover.InnerHtml = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                        span_Dapp_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);
                    }
                    else
                    {
                        span_Dapp_name.InnerHtml = span_DApprover.InnerHtml = "NA";
                        //doa_user.Text = "NA";
                    }
                    // doa_email.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver_email"]);
                }

            }
        }
        DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "addetails", ref IsValid, txt_Username.Text, txtProcessID.Text, txtInstanceID.Text);
        if (DTS != null && DTS.Rows.Count > 0)
        {

            lbl_requestNo.Text = DTS.Rows[0]["REQUEST_NO"].ToString();
            if (lbl_expensetype.Text == "DataCard")
            {
                lbl_mobile.Text = DTS.Rows[0]["MOBILE_CARD_NO"].ToString();
                lbl_service.Text = DTS.Rows[0]["Provider_Name"].ToString();
                lbl_year.Text = DTS.Rows[0]["YEAR"].ToString();
                lbl_month.Text = DTS.Rows[0]["MonthName"].ToString();
                lbl_bill.Text = DTS.Rows[0]["BILL_NO"].ToString();
                lbl_billdate.Text = DTS.Rows[0]["Bill_Date"].ToString();
                lbl_billamt.Text = DTS.Rows[0]["BILL_AMOUNT"].ToString();
                lbl_reimburseamt.Text = DTS.Rows[0]["REIMBURSEMENT_AMOUNT"].ToString();
                lbl_tax.Text = DTS.Rows[0]["TAX"].ToString();
                lbl_paymentmode.Text = DTS.Rows[0]["PAYMENT_MODE"].ToString();
                lbl_PayLocation.Text = DTS.Rows[0]["LOCATION_NAME"].ToString();
                txt_PayMode.Text = DTS.Rows[0]["PAYMENT_MODE1"].ToString();
                txt_PayLocation.Text = DTS.Rows[0]["LOCATION"].ToString();
                txt_Request.Text = DTS.Rows[0]["REQUEST_NO"].ToString();
                //if (DTS.Rows[0]["SUPPORT_DOC"].ToString() == "Y")
                //{
                //    suppdoc = "Yes";
                //}
                //else if (DTS.Rows[0]["SUPPORT_DOC"].ToString() == "N")
                //{
                //    suppdoc = "No";
                //}
                //lblcardparticulars.Text = DTS.Rows[0]["SUPPORTING_PARTICULARS"].ToString();
                //lblsuppdoccard.Text = suppdoc;
            }
            else if (lbl_expensetype.Text == "Mobile")
            {
                lbl_MobileNo.Text = DTS.Rows[0]["MOBILE_CARD_NO"].ToString();
                lbl_paymentmode.Text = DTS.Rows[0]["PAYMENT_MODE"].ToString();
                lbl_PayLocation.Text = DTS.Rows[0]["LOCATION_NAME"].ToString();

                ShowMobileUser();

            }
        }
        DataTable dS = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "getfilenames", ref IsData1, "MOBILE DATACARD EXPENSE", Convert.ToString(DTS.Rows[0]["REQUEST_NO"]));
        if (dS.Rows.Count > 0)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>");
            str.Append("<tbody>");

            for (int i = 0; i < dS.Rows.Count; i++)
            {
                str.Append("<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dS.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a OnClick='downloadfiles(" + (i + 1) + ")'>" + Convert.ToString(dS.Rows[i]["FILENAME"]) + "</a></td></tr>");
            }
            str.Append("</tbody>");
            str.Append("</table>");
            divalldata.InnerHtml = str.ToString();
        }

    }
    private void ShowMobileUser()
    {
        try
        {
            string IsValid = string.Empty;
            string limit = string.Empty;
            string IsData = string.Empty;
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "addetails", ref IsValid, txt_Username.Text, txtProcessID.Text, txtInstanceID.Text);
            DTS.Columns[0].ColumnName = "Sr.No";
            Session["ResultData"] = DTS;
            decimal total_Amount = 0;
            decimal total = 0;
            string suppdoc = string.Empty;
            txt_Request.Text = Convert.ToString(DTS.Rows[0]["HEADER_INFO"]);
            if (DTS.Rows.Count > 0)
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref IsData, lbl_Grade.Text, "AdUser");
                if (dt != null && dt.Rows.Count > 0)
                {
                    limit = dt.Rows[0]["Grade_Expense"].ToString();
                }
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    DTS.Rows[i]["Sr.No"] = i + 1;
                }

                StringBuilder str = new StringBuilder();
                if (DTS.Rows[0]["TAX"].ToString() != "" && DTS.Rows[0]["REIMBURSEMENT_AMOUNT"].ToString() != "")
                {
                    str.Append("<table id='datatable1'  class='table table-bordered'> <thead><tr class='grey'><th>#</th><th>Mobile No</th><th>Service Provider</th><th>Year</th><th>Month</th><th>Bill No</th><th>Bill Date</th><th>Bill Amount</th><th>Rei.Amount</th><th>Limit</th><th>Tax</th><th>Total Reimb.</th><th>Supprting Doc</th><th>Remark</th></tr></thead>");
                }
                else
                {
                    str.Append("<table id='datatable1'  class='table table-bordered'> <thead><tr class='grey'><th>#</th><th>Mobile No</th><th>Service Provider</th><th>Year</th><th>Month</th><th>Bill No</th><th>Bill Date</th><th>Bill Amount</th><th>Rei.Amount</th><th>Limit</th><th>Supprting Doc</th><th>Remark</th></tr></thead>");
                }
                str.Append("<tbody>");
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td align='left'>" + (i + 1) + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["MOBILE_CARD_NO"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["Provider_Name"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["YEAR"].ToString() + "</td>");
                    str.Append("<td >" + DTS.Rows[i]["MonthName"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["BILL_NO"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["Bill_Date"].ToString() + "</td>");
                    str.Append("<td align='right'>" + DTS.Rows[i]["BILL_AMOUNT"].ToString() + "</td>");
                    str.Append("<td align='right'>" + DTS.Rows[i]["REIMBURSEMENT_AMOUNT"].ToString() + "</td>");
                    str.Append("<td align='right'>" + limit + "</td>");
                    if (DTS.Rows[0]["TAX"].ToString() != "" && DTS.Rows[0]["REIMBURSEMENT_AMOUNT"].ToString() != "")
                    {
                        total = Convert.ToDecimal(DTS.Rows[i]["REIMBURSEMENT_AMOUNT"]) + Convert.ToDecimal(DTS.Rows[i]["TAX"].ToString());
                        str.Append("<td align='right'>" + DTS.Rows[i]["TAX"].ToString() + "</td>");
                        str.Append("<td align='right'>" + total + "</td>");
                    }
                    if (DTS.Rows[i]["SUPPORT_DOC"].ToString() == "Y")
                    {
                        suppdoc = "Yes";
                    }
                    else if (DTS.Rows[i]["SUPPORT_DOC"].ToString() == "N")
                    {
                        suppdoc = "No";
                    }
                    str.Append("<td>" + suppdoc + "</td>");
                    //str.Append("<td>" + DTS.Rows[i]["SUPPORTING_PARTICULARS"].ToString() + "</td>");
                    str.Append("<td>" + DTS.Rows[i]["REMARK"].ToString() + "</td>");
                    if (DTS.Rows[0]["TAX"].ToString() != "" && DTS.Rows[0]["REIMBURSEMENT_AMOUNT"].ToString() != "")
                    {
                        total_Amount = total_Amount + total;
                    }
                    else
                    {
                        total_Amount = total_Amount + Convert.ToDecimal(DTS.Rows[i]["REIMBURSEMENT_AMOUNT"].ToString());
                    }

                    str.Append("</tr>");
                }
                str.Append("   </tbody>   </table> ");
                txt_Request.Text = DTS.Rows[0]["REQUEST_NO"].ToString();
                txt_PayMode.Text = DTS.Rows[0]["PAYMENT_MODE1"].ToString();
                txt_PayLocation.Text = DTS.Rows[0]["LOCATION"].ToString();
                div_userMobile.InnerHtml = str.ToString();
                spn_Total.InnerHtml = Convert.ToString(total_Amount);
                //  ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);    
                string data = string.Empty;
                try
                {
                    string isValid = string.Empty;
                    StringBuilder tblHTML = new StringBuilder();
                    DataSet dtded = (DataSet)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getdeddetail", ref isValid, lbl_EmpCode.Text, txt_pk_id.Text);
                    if (dtded != null && dtded.Tables[1].Rows.Count > 0)
                    {
                        tblHTML.Append("<table ID='tbldedu' class='table table-bordered'><thead><tr class='grey'><th>Deduction Year</th><th>Deduction Month</th><th>Amount</th></tr></thead>");
                        tblHTML.Append("<tbody>");
                        for (int Index = 0; Index < dtded.Tables[1].Rows.Count; Index++)
                        {
                            tblHTML.Append("<tr>");
                            tblHTML.Append("<td>" + Convert.ToString(dtded.Tables[1].Rows[Index]["DEDUCTION_YEAR"]) + "</td>");
                            tblHTML.Append("<td>" + Convert.ToString(dtded.Tables[1].Rows[Index]["DedMonthName"]) + " </td>");
                            tblHTML.Append("<td>" + Convert.ToString(dtded.Tables[1].Rows[Index]["DEDUCTION_AMOUNT"]) + " </td>");
                            tblHTML.Append("</tr>");
                        }
                        tblHTML.Append("</tbody>");
                        tblHTML.Append("</table>");
                    }


                    div_dedction.InnerHtml = tblHTML.ToString();
                }
                catch (Exception ex)
                {
                    Logger.WriteEventLog(false, ex);
                }
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Data Found !!')}</script>");
                div_userMobile.InnerHtml = null;
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
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
                string remark = txtRemark.Value;
                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;
                if (ddl_Action.SelectedItem.Text == "Approve")
                {
                    txt_Condition.Text = "1";
                    txt_Audit.Text = "ACCOUNT PAYABLE  APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Acccount_Payable_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Initiator.Text, remark, ddl_Action.SelectedItem.Text);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Initiator.Text;

                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "149", "ACCOUNT PAYABLE  APPROVAL", "APPROVE", txt_Initiator.Text, txt_Initiator.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
                                if (lbl_expensetype.Text == "Mobile")
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Mobile bill rental Expense Request has been Approved by Accounts.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    string emailid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ACCOUNT PAYABLE  APPROVAL", "APPROVE", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                }
                                else
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Datacard rental Expense Request has been Approved by Accounts.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    string emailid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ACCOUNT PAYABLE  APPROVAL", "APPROVE", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                           
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payable Request has been Approved Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");

                            }
                        }
                        //}

                    }
                }
                else if (ddl_Action.SelectedItem.Text == "Reject")
                {
                    txt_Condition.Text = "2";
                    txt_Audit.Text = "ACCOUNT PAYABLE  APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Acccount_Payable_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Initiator.Text, remark, ddl_Action.SelectedItem.Text);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {

                        string[] Dval = new string[1];
                        Dval[0] = txt_Initiator.Text;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "151", "ACCOUNT PAYABLE  APPROVAL", "REJECT", txt_Initiator.Text, txt_Initiator.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
                                if (lbl_expensetype.Text == "Mobile")
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Mobile bill rental Expense request has been Rejected by accounts..</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    string emailid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ACCOUNT PAYABLE  APPROVAL", "REJECT", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                }
                                else
                                {

                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Datacard rental Expense request has been Rejected by accounts..</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    string emailid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ACCOUNT PAYABLE  APPROVAL", "REJECT", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                               
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payable Request Rejected Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                        // }
                    }
                }
                else if (ddl_Action.SelectedItem.Text == "Send-Back")
                {
                    txt_Condition.Text = "3";
                    txt_Audit.Text = "ACCOUNT PAYABLE  APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Acccount_Payable_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Initiator.Text, remark, ddl_Action.SelectedItem.Text);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Username.Text;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "150", "ACCOUNT PAYABLE  APPROVAL", "SEND-BACK", txt_Initiator.Text, txt_Username.Text.Trim(), txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);

                        if (isCreate)
                        {
                            try
                            {
                                if (lbl_expensetype.Text == "Mobile")
                                {

                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Mobile bill rental Expense has been Sent back to Initiator by accounts.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                    string emailid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ACCOUNT PAYABLE  APPROVAL", "SEND-BACK", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                }
                                else
                                {

                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'Datacard rental Expense Request has been Sent back to Initiator by accounts.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                    string emailid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ACCOUNT PAYABLE  APPROVAL", "SEND-BACK", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                          

                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payable Request Send-Back Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }

                    }
                }
                else if (ddl_Action.SelectedValue == "Send-Back-Payment")
                {
                    txt_Condition.Text = "3";
                    txt_Audit.Text = "ACCOUNT PAYABLE  APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Acccount_Payable_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Initiator.Text, remark, ddl_Action.SelectedItem.Text);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Username.Text;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "210", "ACCOUNT PAYABLE  APPROVAL", "SEND-BACK-PAYMENT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
                                if ((Init_Email.Text != "") && (txtEmailID.Text != ""))
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Account Payable Request has been sent for payment mode change by Accounts...</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + txt_Request.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ACCOUNT PAYABLE  APPROVAL", "SEND-BACK-PAYMENT", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payable Request has been sent for payment mode change...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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
    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "saction", ref Isdata);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl_Action.DataSource = dt;
                ddl_Action.DataTextField = "ACTION_NAME";
                ddl_Action.DataValueField = "ACTION_NAME";
                ddl_Action.DataBind();
                ddl_Action.Items.Insert(0, li);
                ListItem li1 = new ListItem("Send-Back-Payment-Mode-Change", "Send-Back-Payment");
                ddl_Action.Items.Insert(2, li1);
            }
        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
    }
}

