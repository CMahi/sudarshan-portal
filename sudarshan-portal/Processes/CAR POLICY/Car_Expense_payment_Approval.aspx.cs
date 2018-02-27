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


public partial class Car_Expense_payment_Approval : System.Web.UI.Page
{
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Car_Expense_payment_Approval));
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
                    getUserInfo();
                    fillRemarkDtl();
                 //   fillAction();
                   
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void getUserInfo()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getpkcarexpns", ref isdata, txtProcessID.Text, txtInstanceID.Text);
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Rows[0]["PK_CAR_EXPNS_ID"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Rows[0]["CAR_EXPENSE_NO"]);
                txt_Request.Text = Convert.ToString(dsData.Rows[0]["CAR_EXPENSE_NO"]);
                spn_date.InnerHtml = dsData.Rows[0]["CREATED_DATE"].ToString();
                txt_Initiator.Text = Convert.ToString(dsData.Rows[0]["AD_ID"]);
                Init_Email.Text = Convert.ToString(dsData.Rows[0]["INIT_MAIL"]);
                expnsamt.InnerHtml = dsData.Rows[0]["EXPENSE_AMOUNT"].ToString();
                if (dsData.Rows[0]["FK_PAYMENT_MODE"].ToString() == "1")
                {
                    span_payloc.InnerHtml = Convert.ToString(dsData.Rows[0]["LOCATION"]);
                    span_paymode.InnerHtml = Convert.ToString(dsData.Rows[0]["PAYMENT_MODE"]);
                }
                else
                {
                    span_paymode.InnerHtml = Convert.ToString(dsData.Rows[0]["PAYMENT_MODE"]);
                    div_loc.Style["display"] = "none";
                }
            }
            DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Car_Expense.aspx", "pgettraveluser", ref isdata, txt_Initiator.Text);
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
                span_division.InnerHtml = "NA";
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

                DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Car_Expense.aspx", "pgettravelrequestapprover", ref isdata, txt_Initiator.Text);
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
                    string  check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "check", ref isdata, span_grade.InnerHtml, txt_Username.Text, empno.InnerHtml, txt_designation.Text);
                    if (check != "")
                    {
                        string pol_ltrs = string.Empty;
                        string[] TempResultData = check.Split('~');
                        if (TempResultData[6].ToString() != "0")
                            pol_ltrs = TempResultData[6].ToString();
                        StringBuilder html_Headerfuel = new StringBuilder();
                        html_Headerfuel.Append("<table class='table table-bordered' id='tblpolfuel' width='100%'>");
                        html_Headerfuel.Append("<thead><tr class='grey'><th style='text-align:center'>Grade</th><th style='text-align:center'>Fuel Eligibility per month in liters</th></tr></thead>");
                        html_Headerfuel.Append("<tbody>");
                        html_Headerfuel.Append("<tr><td style='text-align:center'>" + span_grade.InnerHtml + "</td><td style='text-align:center'>" + pol_ltrs + "</td></tr>");
                        html_Headerfuel.Append("</tbody></table>");
                        fuel_policy.InnerHtml = html_Headerfuel.ToString();

                        StringBuilder html_Header = new StringBuilder();
                        html_Header.Append("<table class='table table-bordered' id='tblpolfuel' width='100%'>");
                        html_Header.Append("<thead><tr class='grey'><th style='text-align:center'>Grade</th><th style='text-align:center'>Year 1</th><th style='text-align:center'>Year 2</th><th style='text-align:center'>Year 3</th><th style='text-align:center'>Year 4</th><th style='text-align:center'>Year 5</th><th style='text-align:center'>Year 6</th></tr></thead>");
                        html_Header.Append("<tbody>");
                        if (span_grade.InnerHtml == "Director")
                            html_Header.Append("<tr><td style='text-align:center'>Director</td><td style='text-align:center'>4,000/-</td><td style='text-align:center'>10,000/-</td><td style='text-align:center'>20,000/-</td><td style='text-align:center'>30,000/-</td><td style='text-align:center'>40,000/-</td><td style='text-align:center'>-</td></tr>");
                        else if (span_grade.InnerHtml == "L1")
                            html_Header.Append("<tr><td style='text-align:center'>L1</td><td style='text-align:center'>3,000/-</td><td style='text-align:center'>7,500/-</td><td style='text-align:center'>15,000/-</td><td style='text-align:center'>22,500/-</td><td style='text-align:center'>30,000/-</td><td style='text-align:center'>-</td></tr>");
                        else if (span_grade.InnerHtml == "L2")
                            html_Header.Append("<tr><td style='text-align:center'>L2</td><td style='text-align:center'>2,000/-</td><td style='text-align:center'>5,000/-</td><td style='text-align:center'>6,000/-</td><td style='text-align:center'>10,000/-</td><td style='text-align:center'>15,000/-</td><td style='text-align:center'>20,000/-</td></tr>");
                        html_Header.Append("</tbody></table>");
                        mt_policy.InnerHtml = html_Header.ToString();
                    }
                }

                fillDocument_Details();
                
            }

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    private void fillAction()
    {
        try
        {
            string Isdata = string.Empty;
            ListItem li = new ListItem("--Select One--", "");
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "action", ref Isdata);
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
  

    protected void bindData()
    {
        StringBuilder html_Header = new StringBuilder();
        StringBuilder html_Header1 = new StringBuilder();
        StringBuilder html_Header2 = new StringBuilder();
        StringBuilder html_Header3 = new StringBuilder();
        StringBuilder html_Header4 = new StringBuilder();
        StringBuilder html_Header5 = new StringBuilder();
        try
        {
            string inco_terms = string.Empty;
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getpkcarexpns", ref isdata, txtProcessID.Text, txtInstanceID.Text);
            isdata = "";
            DataSet ds = (DataSet)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getcarexpnsdetails", ref isdata, dt.Rows[0]["PK_CAR_EXPNS_ID"].ToString());


            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    html_Header.Append("<table class='table table-bordered' id='tblfuel' width='100%'>");
                    html_Header.Append("<thead><tr class='grey'><th>#</th><th style='text-align:center'>Date</th><th style='text-align:center'>Bill Details</th><th style='text-align:center'>Rate</th><th style='text-align:center'>Litre</th><th style='text-align:center'>Amount</th></tr></thead>");
                    html_Header.Append("<tbody>");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            html_Header.Append("<tr><td>" + (i + 1) + "</td><td style='text-align:center'>" + ds.Tables[0].Rows[i]["FUEL_DATE"] + "<input type='hidden' id='txtfueldate" + (i + 1) + "'  runat='server' Value='" + ds.Tables[0].Rows[i]["FUEL_DATE"] + "'  /></td><td style='text-align:center'>" + ds.Tables[0].Rows[i]["PETROL_PUMP"] + "</td><td style='text-align:center'>" + ds.Tables[0].Rows[i]["FUEL_RATE"] + "</td><td style='text-align:center'>" + ds.Tables[0].Rows[i]["FUEL_LITRE"] + "</td><td style='text-align:right'>" + ds.Tables[0].Rows[i]["AMOUNT"] + "<input type='hidden' id='txtfuelamount" + (i + 1) + "'  runat='server' Value='" + ds.Tables[0].Rows[i]["AMOUNT"] + "'  /></td></tr>");
                        }
                    }

                    html_Header.Append("</tbody></table>");
                    divfuel.InnerHtml = html_Header.ToString();
                    txt_glcodef.Text = ds.Tables[0].Rows[0]["SAP_GLCode"].ToString();
                }
                else
                {
                    Li7.Style["display"] = "none";
                    dv_polict_f.Style["display"] = "none";
                }

                if (ds.Tables[1].Rows.Count > 0)
                {

                    html_Header1.Append("<table class='table table-bordered' id='tblmaintenance' width='100%'>");
                    html_Header1.Append("<thead><tr class='grey'><th>#</th><th style='text-align:center'>Date</th><th style='text-align:center'>Car Age</th><th style='text-align:center'>Bill Details</th><th style='text-align:center'>Vehical No.</th><th style='text-align:center'>Date Of Purchase</th><th style='text-align:center'>Amount</th></tr>  </thead>");
                    html_Header1.Append("<tbody>");

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            html_Header1.Append("<tr><td>" + (j + 1) + "</td><td style='text-align:center'>" + ds.Tables[1].Rows[j]["MAINTAINCE_DATE"] + "<input type='hidden' id='txtmaintenancedate" + (j + 1) + "'  runat='server' Value='" + ds.Tables[1].Rows[j]["MAINTAINCE_DATE"] + "'  /></td><td style='text-align:center'>" + ds.Tables[1].Rows[j]["CAR_AGE"] + "</td><td style='text-align:center'>" + ds.Tables[1].Rows[j]["BILL_DETAILS"] + "</td><td style='text-align:center'>" + ds.Tables[1].Rows[j]["VEHICLE_NO"] + "</td><td style='text-align:center'>" + ds.Tables[1].Rows[j]["DATE_OF_PURCHASE"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[j]["AMOUNT"] + "<input type='hidden' id='txtmaintenanceamount" + (j + 1) + "'  runat='server' Value='" + ds.Tables[1].Rows[j]["AMOUNT"] + "'  /></td></tr>");
                        }
                    }

                    html_Header1.Append("</tbody></table>");
                    divmaintenance.InnerHtml = html_Header1.ToString();
                    txt_glcodem.Text = ds.Tables[1].Rows[0]["SAP_GLCode"].ToString();
                }
                else
                {
                    Li8.Style["display"] = "none";
                    dv_polictm.Style["display"] = "none";
                }
                if (ds.Tables[2].Rows.Count > 0)
                {

                    html_Header2.Append("<table class='table table-bordered' id='tbldriver' width='100%'>");
                    html_Header2.Append("<thead><tr class='grey'><th>#</th><th style='text-align:center'>Type</th><th style='text-align:center'>Date</th><th style='text-align:center'>Amount</th></tr></thead>");
                    html_Header2.Append("<tbody>");

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                        {
                            html_Header2.Append("<tr><td>" + (k + 1) + "</td><td style='text-align:center'>" + ds.Tables[2].Rows[k]["DRIVER_TYPE"] + "</td><td style='text-align:center'>" + ds.Tables[2].Rows[k]["DATE"] + "<input type='hidden' id='txtdriverdate" + (k + 1) + "'  runat='server' Value='" + ds.Tables[2].Rows[k]["DATE"] + "'  /></td><td style='text-align:right'>" + ds.Tables[2].Rows[k]["AMOUNT"] + "<input type='hidden' id='txtdriveramount" + (k + 1) + "'  runat='server' Value='" + ds.Tables[2].Rows[k]["AMOUNT"] + "'  /></td></tr>");
                        }
                    }

                    html_Header2.Append("</tbody></table>");
                    div_driver.InnerHtml = html_Header2.ToString();
                    txt_glcoded.Text = ds.Tables[2].Rows[0]["SAP_GLCode"].ToString();
                }
                else
                {
                    Li9.Style["display"] = "none";
                }
                if (ds.Tables[3].Rows.Count > 0)
                {

                    html_Header4.Append("<table class='table table-bordered' id='tbltyre' width='100%'>");
                    html_Header4.Append("<thead><tr class='grey'><th>#</th><th style='text-align:center'>Date</th><th style='text-align:center'>Bill Details</th><th style='text-align:center'>KM Threshold Crossed</th><th style='text-align:center'>Kilometers</th><th style='text-align:center'>Amount</th></tr></thead>");
                    html_Header4.Append("<tbody>");

                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        for (int k = 0; k < ds.Tables[3].Rows.Count; k++)
                        {
                            html_Header4.Append("<tr><td>" + (k + 1) + "</td><td style='text-align:center'>" + ds.Tables[3].Rows[k]["TYRE_DATE"] + "<input type='hidden' id='txtdate" + (k + 1) + "'  runat='server' Value='" + ds.Tables[3].Rows[k]["TYRE_DATE"] + "'  /></td>");
                            html_Header4.Append("<td style='text-align:center'>" + ds.Tables[3].Rows[k]["BILL_DETAILS"] + "</td>");
                            if (ds.Tables[3].Rows[k]["KM_THREHOLD_CROSEED"].ToString() == "Yes")
                                html_Header4.Append("<td style='text-align:center'><input id='km_chk" + (k + 1) + "' checked value='' type='checkbox'></td>");
                            else
                                html_Header4.Append("<td style='text-align:center'><input id='km_chk" + (k + 1) + "' value='' type='checkbox'></td>");
                            html_Header4.Append("<td style='text-align:right'>" + ds.Tables[3].Rows[k]["KILO_METRES"] + "<input type='hidden' id='txtkm" + (k + 1) + "'  runat='server' Value='" + ds.Tables[3].Rows[k]["KILO_METRES"] + "'  /></td><td style='text-align:right'>" + ds.Tables[3].Rows[k]["AMOUNT"] + "<input type='hidden' id='txtamount" + (k + 1) + "'  runat='server' Value='" + ds.Tables[3].Rows[k]["AMOUNT"] + "'  /></td></tr>");
                        }
                    }

                    html_Header4.Append("</tbody></table>");
                    dv_tyre.InnerHtml = html_Header4.ToString();
                    txt_glcodet.Text = ds.Tables[3].Rows[0]["SAP_GLCode"].ToString();
                }
                else
                {
                    Li2.Style["display"] = "none";
                }
                if (ds.Tables[4].Rows.Count > 0)
                {

                    html_Header5.Append("<table class='table table-bordered' id='tblbattery' width='100%'>");
                    html_Header5.Append("<thead><tr class='grey'><th>#</th><th style='text-align:center'>Date</th><th style='text-align:center'>Bill Details</th><th style='text-align:center'>Amount</th></tr></thead>");
                    html_Header5.Append("<tbody>");

                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        for (int k = 0; k < ds.Tables[4].Rows.Count; k++)
                        {
                            html_Header5.Append("<tr><td>" + (k + 1) + "</td><td style='text-align:center'>" + ds.Tables[4].Rows[k]["BATTERY_DATE"] + "<input type='hidden' id='txtbdate" + (k + 1) + "'  runat='server' Value='" + ds.Tables[4].Rows[k]["BATTERY_DATE"] + "'  /></td><td style='text-align:Center'>" + ds.Tables[4].Rows[k]["BILL_DETAILS"] + "</td><td style='text-align:right'>" + ds.Tables[4].Rows[k]["AMOUNT"] + "<input type='hidden' id='txtbamount" + (k + 1) + "'  runat='server' Value='" + ds.Tables[4].Rows[k]["AMOUNT"] + "'  /></td></tr>");
                        }
                    }

                    html_Header5.Append("</tbody></table>");
                    dv_battery.InnerHtml = html_Header5.ToString();
                }
                else
                {
                    Li1.Style["display"] = "none";
                }
            }


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

    protected void btnApprove_Click(object sender, EventArgs e)
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
                if (ddlAction.SelectedItem.Text == "Approve")
                {

                    string isSaved = (string)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "update", ref refData, txtProcessID.Text, txtInstanceID.Text, remark, txt_Username.Text, "7");
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = "FLOLOGIC1";
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "230", "CAR EXPENSE PAYMENT APPROVAL", "SUBMIT", "", txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {

                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/><pre><font size='3'>The Car Expense request has been Approved Successfully.</font></pre><pre><font size='3'>Car Expense No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml + "</font></pre></p><pre></b><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "CAR EXPENSE PAYMENT APPROVAL", "SUBMIT", Init_Email.Text, txtEmailID.Text, msg, "Car Expense No: " + spn_req_no.InnerHtml);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense Request has been Approved Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                    }


                }
                else if (ddlAction.SelectedItem.Text == "Send-Back")
                {
                    refData = string.Empty;
                    isInserted = string.Empty;
                    ISValid = string.Empty;
                    txt_Condition.Text = "3";
                    remark = txtRemark.Value;

                    DataTable dtback = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getsendback", ref refData, txtProcessID.Text, txtInstanceID.Text);
                    if (dtback != null && dtback.Rows.Count > 0)
                    {
                        txtEmailID.Text = dtback.Rows[0]["EMAIL_ID"].ToString();
                        string isSaved = (string)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "update", ref refData, txtProcessID.Text, txtInstanceID.Text, remark, txt_Username.Text, "8");

                        if (isSaved == null || refData.Length > 0 || isSaved == "false")
                        {
                            string[] errmsg = refData.Split(':');
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                        }
                        else
                        {

                            string[] Dval = new string[1];
                            Dval[0] = dtback.Rows[0]["actionbyuser"].ToString();
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "229", "CAR EXPENSE PAYMENT APPROVAL", "SEND-BACK", dtback.Rows[0]["actionbyuser"].ToString(), txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>The Car Expense request has been Send Back To Initiator.</font></pre><p/><pre><font size='3'>Car Expense No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml + "</font></pre></p><pre></b><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "CAR EXPENSE PAYMENT APPROVAL", "SUBMIT", Init_Email.Text, "", msg, "Car Expense No: " + spn_req_no.InnerHtml);
                                }
                                catch (Exception ex)
                                {
                                    // throw;
                                    FSL.Logging.Logger.WriteEventLog(false, ex);
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense Request send-back Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Send-back Approver Not Found...!');}</script>");
                    }
                }
                else if (ddlAction.SelectedItem.Text == "Send For Payment Mode Change")
                {
                    refData = string.Empty;
                    isInserted = string.Empty;
                    ISValid = string.Empty;
                    txt_Condition.Text = "3";
                    remark = txtRemark.Value;

                    DataTable dtback = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getsendback", ref refData, txtProcessID.Text, txtInstanceID.Text);
                    if (dtback != null && dtback.Rows.Count > 0)
                    {
                        txtEmailID.Text = dtback.Rows[0]["EMAIL_ID"].ToString();
                        string isSaved = (string)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "update", ref refData, txtProcessID.Text, txtInstanceID.Text, remark, txt_Username.Text, "14");

                        if (isSaved == null || refData.Length > 0 || isSaved == "false")
                        {
                            string[] errmsg = refData.Split(':');
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                        }
                        else
                        {

                            string[] Dval = new string[1];
                            Dval[0] = dtback.Rows[0]["actionbyuser"].ToString();
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "237", "CAR EXPENSE PAYMENT APPROVAL", "SENDBACK-MODE", dtback.Rows[0]["actionbyuser"].ToString(), txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>The Car Expense request has been Send Back To Initiator For Payment Mode Change.</font></pre><p/><pre><font size='3'>Car Expense No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml + "</font></pre></p><pre></b><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "CAR EXPENSE PAYMENT APPROVAL", "SUBMIT", Init_Email.Text, "", msg, "Car Expense No: " + spn_req_no.InnerHtml);
                                }
                                catch (Exception ex)
                                {
                                    // throw;
                                    FSL.Logging.Logger.WriteEventLog(false, ex);
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense Request send-back Successfully For Payment Mode Change...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Send-back Approver Not Found...!');}</script>");
                    }
                }
                else if (ddlAction.SelectedItem.Text == "Reject")
                {
                    remark = txtRemark.Value;
                    refData = string.Empty;
                    string isSaved = (string)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "update", ref refData, txtProcessID.Text, txtInstanceID.Text, remark, txt_Username.Text, "11");
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Username.Text;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "236", "CAR EXPENSE PAYMENT APPROVAL", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Car Expense Request has been Rejected.</font></pre><p/> <pre><font size='3'>Car Expense No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "CAR EXPENSE PAYMENT APPROVAL", "REJECT", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense Request has been Rejected...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                    }
                }
            }
            MP_Loading.Hide();
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
            MP_Loading.Hide();
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string GetFileNames(string name)
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

    private void fillRemarkDtl()
    {
        try
        {
            GetData getdata = new GetData();
            Div_Audit_Details.InnerHtml = getdata.fillAuditTrail(txtProcessID.Text, txtInstanceID.Text);

        }
        catch (Exception ex)
        {
            // Logger.WriteEventLog(false, ex);
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
                divalldata.InnerHtml = getdata.fillDocumentDtl("CAR POLICY", spn_req_no.InnerHtml);

            }
            catch (Exception ex)
            {
                // Logger.WriteEventLog(false, ex);
            }
        }
    }


}