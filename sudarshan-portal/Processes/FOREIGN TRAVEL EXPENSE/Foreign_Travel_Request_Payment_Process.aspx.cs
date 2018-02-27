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

public partial class Foreign_Travel_Request_Payment_Process : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Foreign_Travel_Request_Payment_Process));
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
                            step_name.Text = Convert.ToString(Request.QueryString["step"]);
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
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "pgetrequestdata", ref isdata, txtWIID.Text);
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["pk_travel_expense_hdr_id"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                txt_initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]);
                txt_init_mail.Text = Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);
                travelFromDate.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["from_date"]).ToString("dd-MMM-yyyy");
                travelToDate.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["to_date"]).ToString("dd-MMM-yyyy");
                req_remark.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["remark"]);
                pay_mode.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["pay_mode"]);
                spn_Country.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["COUNTRY_NAME"]);
                pk_country_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["fk_Country"]);
                spn_Currency.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]);

		if(Convert.ToString(dsData.Tables[0].Rows[0]["sap_status"])!="1")
		{
			doc_original.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
		}
		else
		{
			doc_original.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["BANk_acc_no"]).ToString("dd-MMM-yyyy");
		}
                if (Convert.ToString(dsData.Tables[0].Rows[0]["DO_APPROVAL_FLAG"]).ToUpper() == "YES")
                {
                    div_deviated.Style.Add("display", "normal");
                    string[] dev_string = Convert.ToString(dsData.Tables[0].Rows[0]["DEVIATION_REASON"]).Split(',');
                    string d_string = "";
                    for (int l = 0; l < dev_string.Length; l++)
                    {
                        if (dev_string[l].ToString().Trim() != "")
                        {
                            d_string += "<p>" + dev_string[l] + "</p>";
                        }
                    }
                    span_deviate.InnerHtml = d_string.ToString();
                    //span_deviate.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["DEVIATION_REASON"]);
                }

                if (Convert.ToString(dsData.Tables[0].Rows[0]["location_name"]) != "")
                {
                    pay_location.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["location_name"]);
                }
                else
                {
                    pay_location.InnerHtml = "NA";
                }
                pk_mode_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["payment_mode"]);
                pk_loc_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["cash_location"]);

                //dev_travel_class.Text = Convert.ToString(dsData.Tables[0].Rows[0]["dev_travel_class"]);
                //dev_policy_amt.Text = Convert.ToString(dsData.Tables[0].Rows[0]["dev_policy_amt"]);
                //dev_supp_amt.Text = Convert.ToString(dsData.Tables[0].Rows[0]["dev_supp_amt"]);

                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "pgettraveluser", ref isdata, txt_initiator.Text);
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    empno.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
                    span_ename.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
                    span_cc.InnerHtml = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
                    span_dept.InnerHtml = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
                    span_grade.InnerHtml = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
                    span_mobile.InnerHtml = Convert.ToString(dtUser.Rows[0]["MOBILE_NO"]);
                    Desg_Name.Text = span_designation.InnerHtml = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
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
                    span_Division.InnerHtml =Convert.ToString(dtUser.Rows[0]["DIVISION_NAME"]);
                    spn_base_location.InnerHtml = Convert.ToString(dtUser.Rows[0]["BASE_LOCATION"]);

                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgettravelrequestapprover", ref isdata, txt_initiator.Text);
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

                    // checkDeviation();

                    fillDocument_Details(dsData.Tables[3]);
                    fillAdvanceAmount();
                    //fillSupporting();
                    fillAuditTrail();
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
    private void fillDocument_Details(DataTable dt)
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

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Type</th><th>File Name</th></tr></thead>";
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DisplayData += "<tr>";
                            DisplayData += "<td>" + Convert.ToString(dt.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><input type='text' id='anc_" + (i + 1) + "' value='" + Convert.ToString(dt.Rows[i]["FileName"]) + "' style='display:none'/>  <a href='#' onclick=\"return downloadfiles('" + (Convert.ToString(dt.Rows[i]["FileName"])) + "');\")'>" + Convert.ToString(dt.Rows[i]["FileName"]) + "</td>";

                            DisplayData += "</tr>";
                        }
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

    public void fillAdvanceAmount()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "pgetadvancedetails", ref isValid, txt_initiator.Text, txt_pk_id.Text, 2);
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Currency</th><th>Advance Amount</th><th>Exchange Rate</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
                {
                    tblHTML.Append("<tr>");
                    tblHTML.Append("<td>" + (Index + 1) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["ADVANCE_DATE"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["CURRENCY"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["CURRENCY_AMOUNT"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["EXC_RATE"]) + "</td>");
                    tblHTML.Append("<td>" + span_app_name.InnerHtml + "</td>");
                    tblHTML.Append("</tr>");
                    txt_ERate.Text = Convert.ToString(dt.Tables[0].Rows[Index]["EXC_RATE"]);
                }
            }
            else
            {
                tblHTML.Append("<tr><td colspan='5' align='center'>Advance Not Available</td></tr>");
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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string show_Policy(string desg)
    {
        string ret_data = "";
        string isdata = "";
        DataTable dtPolicy = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "pgetpolicydata", ref isdata, desg);
        ret_data = "<table id='data-table1' class='table table-bordered' align='center' width='100%'>";
        ret_data += "<thead><tr class='grey'><th>Country</th><th>Expense Head</th><th>Amount</th><th>Currency</th></tr></thead><tbody>";


        if (dtPolicy != null && dtPolicy.Rows.Count > 0)
        {
            for (int i = 0; i < dtPolicy.Rows.Count; i++)
            {
                ret_data += "<tr><td>" + Convert.ToString(dtPolicy.Rows[i]["Country_name"]) + "</td><td>" + Convert.ToString(dtPolicy.Rows[i]["expense_head"]) + "</td><td>" + Convert.ToString(dtPolicy.Rows[i]["amount"]) + "</td><td>" + Convert.ToString(dtPolicy.Rows[i]["currency"]) + "</td></tr>";
            }
        }
        ret_data += "</tbody></table>";
        return ret_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string getDataRows(string jFDate, string jTDate, int country_id, string wiid, string desg)
    {
        StringBuilder sb = new StringBuilder();
        string isdata = string.Empty;
        string row_data = "";
        DateTime fdate = Convert.ToDateTime(jFDate);
        DateTime tdate = Convert.ToDateTime(jTDate);
        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "pgetrequestdata", ref isdata, wiid);
        int noofdays = 0;
        try
        {
            while (fdate <= tdate)
            {
                noofdays = noofdays + 1;
                fdate = fdate.AddDays(1);
            }
            if (dsData != null && dsData.Tables[1].Rows.Count > 0)
            {
                string pk_id = Convert.ToString(dsData.Tables[0].Rows[0]["pk_travel_expense_hdr_id"]);
                string currency = Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]);
                double UEx_Rate = Convert.ToDouble(dsData.Tables[0].Rows[0]["exc_rate"]);
                sb.Append("<div class='panel table-responsive'>");
                sb.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
                sb.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' name='rs' href='#collapse0'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span>Summary</span></a></h3>");
                sb.Append("</div>");

                sb.Append("<div id='collapse0' class='panel-collapse collapse in'><div class='panel-body' style='background-color:#ffffff'><div class='form-horizontal'>");

                sb.Append("<div class='form-group'>");
                sb.Append("<table class='table table-bordered'>");
                sb.Append("<thead class='grey'><th>Particulars</th>");
                DataTable rt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreimbursements", ref isdata, "International Travel Expenses");
                string[] re_id = new string[0];
                decimal[] exp_total = new decimal[0];

                if (rt != null)
                {
                    re_id = new string[rt.Rows.Count];
                    exp_total = new decimal[rt.Rows.Count];
                    for (int r = 0; r < rt.Rows.Count; r++)
                    {
                        re_id[r] = Convert.ToString(rt.Rows[r]["PK_REIMBURSEMNT_ID"]);
                        sb.Append("<th>" + Convert.ToString(rt.Rows[r]["REIMBURSEMENT_TYPE"]) + "</th>");
                    }
                }
                sb.Append("<th>Total</th>");
                sb.Append("<th>Eligible</th>");
                sb.Append("<th>Excess</th>");
                sb.Append("</thead>");
                sb.Append("</tbody>");

                double total, el_total, ex_total;
                total = el_total = ex_total = 0;

                string[] exp = new string[0];
                DataSet Exp_dat = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getexpenseids", ref isdata);
                if (Exp_dat != null)
                {
                    DataTable Inc_Con_Dt = new DataTable();
                    DataTable ejm = new DataTable();
                    ejm = Exp_dat.Tables[2];
                    exp = new string[ejm.Rows.Count];
                    for (int e = 0; e < ejm.Rows.Count; e++)
                    {
                        //exp[e] = Convert.ToString(ejm.Rows[e]["FK_EXPENSE_HEAD"]);
                        string exp_id = Convert.ToString(ejm.Rows[e]["FK_EXPENSE_HEAD"]);
                        string exp_name = Convert.ToString(ejm.Rows[e]["EXPENSE_HEAD"]).ToUpper();
                        sb.Append("<tr>");
                        sb.Append("<td>" + Convert.ToString(ejm.Rows[e]["EXPENSE_HEAD"]) + "</td>");

                        for (int r = 0; r < rt.Rows.Count; r++)
                        {
                            string rid = Convert.ToString(re_id[r]);
                            string amount = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getexpenseamount", ref isdata, exp_id, pk_id, rid);
                            sb.Append("<td style='text-align:right'>" + amount + "</td>");
                        }
                        string exp_amount = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getexpheadwiseamount", ref isdata, exp_id, pk_id);
                        if (Convert.ToDouble(exp_amount) != 0)
                        {
                            sb.Append("<td style='text-align:right'>" + Convert.ToDouble(exp_amount).ToString("#.00") + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style='text-align:right'>0.00</td>");
                        }
                        total = total + Convert.ToDouble(exp_amount);
                        string allowed_amount = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getallowedamount", ref isdata, country_id, desg, Convert.ToString(ejm.Rows[e]["EXPENSE_HEAD"]));
                        if (Convert.ToDouble(Convert.ToInt32(allowed_amount) * noofdays) != 0)
                        {
                            sb.Append("<td style='text-align:right'>" + Convert.ToDouble(Convert.ToInt32(allowed_amount) * noofdays).ToString("#.00") + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style='text-align:right'>0.00</td>");
                        }

                        el_total = el_total + Convert.ToDouble(Convert.ToInt32(allowed_amount) * noofdays);
                        ex_total = total - el_total;

                        if (Convert.ToDouble(Convert.ToDouble(exp_amount) - Convert.ToDouble(Convert.ToDouble(allowed_amount) * Convert.ToDouble(noofdays))) != 0)
                        {
                            if (Convert.ToDouble(exp_amount) != 0)
                            {
                                if (Convert.ToDouble(Convert.ToDouble(exp_amount) - Convert.ToDouble(Convert.ToDouble(allowed_amount) * Convert.ToDouble(noofdays))) < 0)
                                {
                                    sb.Append("<td style='text-align:right;'>" + Convert.ToDouble(Convert.ToDouble(exp_amount) - Convert.ToDouble(Convert.ToDouble(allowed_amount) * Convert.ToDouble(noofdays))).ToString("#.00") + "</td>");
                                }
                                else
                                {
                                    sb.Append("<td style='text-align:right;color:red'>" + Convert.ToDouble(Convert.ToDouble(exp_amount) - Convert.ToDouble(Convert.ToDouble(allowed_amount) * Convert.ToDouble(noofdays))).ToString("#.00") + "</td>");
                                }
                            }
                            else
                            {
                                sb.Append("<td style='text-align:right'>0.00</td>");
                            }
                        }
                        else
                        {
                            sb.Append("<td style='text-align:right'>0.00</td>");
                        }
                        sb.Append("</tr>");
                    }
                }

                sb.Append("<tr style='font-weight:bold'>");
                sb.Append("<td>Total </td>");
                for (int r = 0; r < re_id.Length; r++)
                {
                    string rid = re_id[r];
                    string amount = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getreimbursementamount", ref isdata, pk_id, rid);
                    sb.Append("<td style='text-align:right'>" + amount + "</td>");
                }
                sb.Append("<td style='text-align:right'>" + Convert.ToDouble(total).ToString("#.00") + "</td>");
                sb.Append("<td style='text-align:right'>" + Convert.ToDouble(el_total).ToString("#.00") + "</td>");
                sb.Append("<td style='text-align:right'>" + Convert.ToDouble(ex_total).ToString("#.00") + "</td>");

                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");

                sb.Append("<table class='table table-bordered' style='width:100%'>");
                sb.Append("<tbody>");
                sb.Append("<tr style='font-weight:bold'>");
                sb.Append("<td>Advance Taken : </td>");
                sb.Append("<td>" + Convert.ToString(dsData.Tables[4].Rows[0]["adv_amount"]) + "</td>");
                sb.Append("<td>Total Expense : </td>");
                sb.Append("<td>" + Convert.ToString(dsData.Tables[4].Rows[0]["Tot_Amount"]) + "</td>");
                sb.Append("<td>Difference : </td>");
                sb.Append("<td>" + (Convert.ToDecimal(dsData.Tables[4].Rows[0]["adv_amount"]) - Convert.ToDecimal(dsData.Tables[4].Rows[0]["Tot_Amount"])) + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr style='font-weight:bold'>");
                //sb.Append("<td>Supporting Amount : </td>");
                //sb.Append("<td>" + Convert.ToString(dsData.Tables[4].Rows[0]["supporting"]) + "</td>");
                sb.Append("<td>Non Supporting Amount : </td>");
                sb.Append("<td>" + Convert.ToString(dsData.Tables[4].Rows[0]["non_supporting"]) + "</td>");
                sb.Append("<td>Return Money : </td>");
                if (Convert.ToString(dsData.Tables[0].Rows[0]["RETURN_MONEY"]) == "" || Convert.ToString(dsData.Tables[0].Rows[0]["RETURN_MONEY"]) == "0")
                {
                    sb.Append("<td>0.00</td>");
                }
                else
                {
                    sb.Append("<td>" + Convert.ToDecimal(dsData.Tables[0].Rows[0]["RETURN_MONEY"]).ToString("#.00") + "</td>");
                }

                sb.Append("</tr>");

                sb.Append("</tbody>");
                sb.Append("</table>");
                sb.Append("</div>");

                string approver = "NA";
                string dev_approver = "NA";
                DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgettravelrequestapprover", ref isdata, Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]));
                if (dtApprover != null)
                {
                    if (dtApprover.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                        {
                            approver = Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                        }

                        if (Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "0")
                        {
                            dev_approver = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);
                        }

                    }
                }
                sb.Append("<div class='form-group'>");
                sb.Append("<div class='col-md-4' style='text-align:center; font-weight:bold'>Prepared By</div>");
                sb.Append("<div class='col-md-4' style='text-align:center; font-weight:bold'>Approved By</div>");

                if (Convert.ToString(dsData.Tables[0].Rows[0]["DO_APPROVAL_FLAG"]).ToUpper() == "YES")
                {
                    sb.Append("<div class='col-md-4' style='text-align:center; font-weight:bold'>Approved Under Deviation</div>");
                }
                sb.Append("</div>");

                sb.Append("<div class='form-group'>");
                sb.Append("<div class='col-md-4' style='text-align:center; font-weight:bold'>" + dsData.Tables[0].Rows[0]["EMPLOYEE_NAME"] + "</div>");
                sb.Append("<div class='col-md-4' style='text-align:center; font-weight:bold'>" + approver + "</div>");

                if (Convert.ToString(dsData.Tables[0].Rows[0]["DO_APPROVAL_FLAG"]).ToUpper() == "YES")
                {
                    sb.Append("<div class='col-md-4' style='text-align:center; font-weight:bold'>" + dev_approver + "</div>");
                }
                sb.Append("</div>");

                sb.Append("<br/>");
                sb.Append("<div class='form-group'>");
                sb.Append("<p style='font-weight:bold'>Details Of Currency Returned : </p>");
                sb.Append("<table class='table table-bordered'>");
                sb.Append("<thead class='grey'><th>Currency</th><th>Currency Taken</th><th>Expenditure</th><th>Balance</th><th>Doc No</th><th>Posting Date</th><th>Return Money</th><th>Payable/Refundable(-)</th><th>Exchange Rate</th><th>Amount</th><th>Difference</th></thead>");
                sb.Append("<tbody>");
                sb.Append("<tr>");
                sb.Append("<td>" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]) + "</td>");
                sb.Append("<td><span id='adv_amount'>" + Convert.ToString(dsData.Tables[4].Rows[0]["adv_amount"]) + "</span></td>");
                sb.Append("<td><span id='Tot_Amount'>" + Convert.ToString(dsData.Tables[4].Rows[0]["Tot_Amount"]) + "</span><span id='exc_rate' style='display:none'>" + Convert.ToString(dsData.Tables[4].Rows[0]["exc_rate"]) + "</span></td>");

                sb.Append("<td><span id='balance'>" + (Convert.ToDecimal(dsData.Tables[4].Rows[0]["adv_amount"]) - Convert.ToDecimal(dsData.Tables[4].Rows[0]["Tot_Amount"])) + "</span></td>");
                sb.Append("<td><input type='text' id='doc_no' class='form-control input-sm'></input></td>");
                sb.Append("<td><input type='text' id='doc_date' class='form-control input-sm' readonly='' value='" + DateTime.Now.ToString("dd-MMM-yyyy") + "'></input></td>");
                string R_MONEY = Convert.ToString(dsData.Tables[0].Rows[0]["RETURN_MONEY"]);
                if (R_MONEY == "0" || R_MONEY=="")
                {
                    R_MONEY =Convert.ToString(Convert.ToInt32(dsData.Tables[4].Rows[0]["adv_amount"]) - Convert.ToInt32(dsData.Tables[4].Rows[0]["Tot_Amount"]));
                }
                sb.Append("<td><input type='text' id='return_money' class='form-control input-sm ' onchange='validateFloatKey(this); calculate_diff()' value='" + R_MONEY + "'></input></td>");
                sb.Append("<td><span id='pay_ref'>" + Convert.ToString(((Convert.ToDecimal(dsData.Tables[4].Rows[0]["adv_amount"]) - Convert.ToDecimal(dsData.Tables[4].Rows[0]["Tot_Amount"])) * (-1)) + Convert.ToDecimal(R_MONEY)) + "</span></td>");
                sb.Append("<td><input type='text' id='return_exc' class='form-control input-sm numbersOnly1' value=''  onchange='validateFloatKey(this); calculate_inr();'></input><input type='text' id='expense_amount' class='form-control input-sm numbersOnly' value='" + Convert.ToInt32(dsData.Tables[4].Rows[0]["exp_in_inr"]) + "' style='display:none'></input></td>");
                sb.Append("<td><span id='ret_exc_inr'>0</span></td>");
                sb.Append("<td><span id='span_diff'>0</span> <span id='advance_in_inr' style='display:none'>" + Convert.ToDecimal(dsData.Tables[4].Rows[0]["adv_in_inr"]) + "</span></td>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");
                sb.Append("</div>");

                sb.Append("</div></div></div></div>");

                fdate = Convert.ToDateTime(jFDate);
                tdate = Convert.ToDateTime(jTDate);
                int index = 1;
                while (fdate <= tdate)
                {
                    sb.Append("<div class='panel table-responsive'>");
                    sb.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
                    string amount = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getexpenseamountdaywise", ref isdata, pk_id, fdate);
                    sb.Append("<div class='panel-heading-btn'><div>Amount (" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]) + ") : <span id='row_Total" + index + "'>" + amount + "</span></div></div>");
                    sb.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' name='rs' href='#collapse" + index + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spn_date" + index + "'>" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + "</span></a></h3>");
                    sb.Append("</div>");

                    sb.Append("<div id='collapse" + index + "' class='panel-collapse collapse'><div class='panel-body' style='background-color:#ffffff'><div class='form-horizontal'>");

                    sb.Append(bindExpense(index, fdate, dsData.Tables[2], currency, dsData.Tables[1], dsData.Tables[5]));
                    fdate = fdate.AddDays(1);
                    index = index + 1;

                    sb.Append("</div></div></div></div>");
                }
            }
            row_data = sb.ToString();
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return row_data;
    }

    protected static string bindExpense(int index, DateTime fdate, DataTable dtExp, string currency, DataTable dtDay, DataTable Loc_travel)
    {
        string exp_data = "";
        StringBuilder reim = new StringBuilder();
        StringBuilder html = new StringBuilder();
        try
        {

            /*****************************************************************************************************************************/
            html.Append("<div class='form-group'>");
            html.Append("<div class='col-md-1'></div>");
            html.Append("<div class='col-md-2'>City</div>");
            if (Convert.ToString(dtDay.Rows[index - 1]["FK_CITY"]) != "-1")
            {
                html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["CITY_NAME"]) + "</div>");
            }
            else
            {
                html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["OTHER_CITY"]) + "</div>");
            }
            html.Append("<div class='col-md-3'></div>");
            html.Append("<div class='col-md-2'>Particulars</div>");
            html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["remark_note"]) + "</div>");
            html.Append("</div>");

            html.Append("<div class='form-group'>");
            html.Append("<div class='col-md-1'></div>");
            html.Append("<div class='col-md-2'>Hotel Name</div>");
            html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["Hotel_name"]) + "</div>");
            html.Append("<div class='col-md-3'></div>");
            html.Append("<div class='col-md-2'>Hotel Contact</div>");
            html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["Hotel_No"]) + "</div>");
            html.Append("</div>");

            html.Append("<div class='form-group'>");
            html.Append("<div class='col-md-1'></div>");
            html.Append("<div class='col-md-2'>Travel Class</div>");
            html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["Travel_Class"]) + "</div>");
            html.Append("</div>");
            /*****************************************************************************************************************************/


            html.Append("<div class='form-group'></div>");
            html.Append("<div class='col-md-12'>");
            html.Append("<div class='col-md-12'>");
            html.Append("<table class='table table-bordered'>");
            html.Append("<thead><tr class='grey'>");
            html.Append("<th>Expense Head</th><th>Reimbursement Type</th><th>Expense Amount</th><th>Tax</th><th>Exchange Rate</th><th>Amount (" + currency + ")</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Supporting Doc</th><th>Supporting Particulars</th><th>Remark</th>");
            html.Append("</tr></thead>");
            html.Append("<tbody>");

            for (int row = 0; row < dtExp.Rows.Count; row++)
            {
                if (Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") == Convert.ToDateTime(dtExp.Rows[row]["fk_travel_date"]).ToString("dd-MMM-yyyy"))
                {
                    html.Append("<tr>");

                    html.Append("<td>" + Convert.ToString(dtExp.Rows[row]["expense_head"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(dtExp.Rows[row]["reimbursement_type"]) + "</td>");
                    if (Convert.ToString(dtExp.Rows[row]["C_CURRENCY"]) != "0")
                    {
                        html.Append("<td style='text-align:right;'>" + Convert.ToString(dtExp.Rows[row]["C_CURRENCY"]) + "</td>");
                    }
                    else
                    {
                        html.Append("<td style='text-align:right;'></td>");
                    }
                    html.Append("<td style='text-align:right;'>" + Convert.ToString(dtExp.Rows[row]["TAX"]) + "</td>");
                    html.Append("<td style='text-align:right;'>" + Convert.ToString(dtExp.Rows[row]["E_RATE"]) + "</td>");
                    html.Append("<td style='text-align:right;'>" + Convert.ToString(dtExp.Rows[row]["D_CURRENCY"]) + "</td>");
                    html.Append("<td style='text-align:right;'>" + Convert.ToString(dtExp.Rows[row]["I_CURRENCY"]) + "</td>");
                    if (Convert.ToString(dtExp.Rows[row]["IS_SUPPORTING"]) == "Y")
                    {
                        html.Append("<td>" + Convert.ToString(dtExp.Rows[row]["supp_doc1"]) + "</td>");
                        html.Append("<td>");
                        if (Convert.ToString(dtExp.Rows[row]["supp_doc1"]) == "YES")
                        {
                            html.Append(Convert.ToString(dtExp.Rows[row]["supp_remark"]));
                        }
                        else
                        {
                            html.Append("");
                        }
                        html.Append("</td>");
                    }
                    else
                    {
                        html.Append("<td>" + Convert.ToString(dtExp.Rows[row]["supp_doc1"]) + "</td>");
                        html.Append("<td></td>");

                    }
                    html.Append("<td>" + Convert.ToString(dtExp.Rows[row]["other_remark"]) + "</td>");
                    html.Append("</tr>");
                }
            }
            html.Append("</tbody></table>");
            html.Append("</div>");
            html.Append("<div class='col-md-1'></div>");
            html.Append("</div>");
            html.Append("<div class='form-group'></div>");
            if (Loc_travel.Rows.Count > 0)
            {
                html.Append("<div class='col-md-12'>");
                html.Append("<div class='col-md-12 grey'>Local Travel Details");
                html.Append("<table class='table table-bordered' id='tbl_Local_Travel" + index + "'>");
                html.Append("<thead><tr class='grey'><th>Date</th><th>Reimbursement Type</th><th>From</th><th>To</th><th>Mode of Travel</th><th>Expense Amount</th><th>Tax</th><th>Exchange Rate</th><th>Amount (" + currency + ")</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Remark</th><th>Bills attached Yes/No</th></tr></thead>");
                html.Append("<tbody>");
                for (int k = 0; k < Loc_travel.Rows.Count; k++)
                {
                    if (Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") == Convert.ToDateTime(Loc_travel.Rows[k]["fk_travel_date"]).ToString("dd-MMM-yyyy"))
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + Loc_travel.Rows[k]["fk_travel_date"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["REIMBURSEMENT_TYPE"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["TRAVEL_FROM"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["TRAVEL_TO"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["TRAVEL_MODE"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["EXPENSES"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["TAX"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["CONVERSION_RATE"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["BASE_EXP_AMOUNT"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["INR_EXP_AMOUNT"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["REMARK"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["BILL_STATUS"].ToString() + "</td>");
                        html.Append("</tr>");
                    }
                }
                html.Append("</tbody></table></div>"); html.Append("</div>");
            }
            exp_data = Convert.ToString(html);

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return exp_data;
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
                divIns.Style.Add("display", "none");
                if (ddlAction.SelectedIndex > 0)
                {
                    if (ddlAction.SelectedValue == "1")
                    {

                        string release_id = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreleaseid", ref ref_data, txtProcessID.Text, step_name.Text, "SUBMIT");
                        if (release_id != "")
                        {
                            /***************************************************************************************************************************************/
                            string rfc_string = string.Empty;
                            string rfc_string1 = string.Empty;
                            string line_item = string.Empty;
                            string bank_flag = string.Empty;
                            string bank_no = string.Empty;
                            string rfc_no = "";
                            string rfc_flag = "";
                            string alert_msg = "";
                            DataTable dtManage = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata1", ref ref_data, spn_req_no.InnerHtml, "SELECT", "", "", "", "", "", "", "");
                            if (dtManage != null && dtManage.Rows.Count > 0)
                            {
                                bank_flag = Convert.ToString(dtManage.Rows[0]["BANK_FLAG"]);
                                bank_no = Convert.ToString(dtManage.Rows[0]["BANK_NO"]);
                            }

                            if (bank_flag == "E" || bank_flag == "")
                            {
                                string rfc_action = "FOREIGN TRAVEL";
                                DataSet dt_sap_rfc = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getbankrfc", ref ref_data, pk_bank_id.Text, rfc_action, spn_req_no.InnerHtml, 1, txt_Advance.Text, txt_Expense.Text);
                                if (dt_sap_rfc != null)
                                {
                                    if (dt_sap_rfc.Tables[0].Rows.Count > 0)
                                    {
                                        string ref_no = Convert.ToString(dt_sap_rfc.Tables[0].Rows[0][0]);
                                    }
                                    if (dt_sap_rfc.Tables[1].Rows.Count > 0)
                                    {
                                        for (int index = 0; index < dt_sap_rfc.Tables[1].Rows.Count; index++)
                                        {
                                            if (rfc_string == "")
                                            {
                                                rfc_string += Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["COMP_CODE"]);
                                            }
                                            else
                                            {
                                                rfc_string += "|" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["COMP_CODE"]);
                                            }
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["DOC_DATE"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["PSTNG_DATE"]);
                                            rfc_string += "$";
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["DOC_TYPE"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["REF_DOC_NO"]);
                                            rfc_string += "$" + (index + 1).ToString();
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["GL_ACCOUNT"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["REF_KEY_1"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["VENDOR_NO"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ALLOC_NMBR"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ITEM_TEXT"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["BUS_AREA"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["COSTCENTER"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["PROFIT_CTR"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["CURRENCY"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["AMT_DOCCUR"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ZLSCH"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["PERSON_NO"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["SECCO"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["BUPLA"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ZFBDT"]);
                                            rfc_string += "$ ";
                                        }
                                    }
                                }
                                rfc_string1 = rfc_string;

                                if (rfc_string1 != "")
                                {
                                    string ref_key_no = "";
                                    //DataTable dtBank = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getbankrfcsum", ref ref_data, pk_bank_id.Text);
                                    DataTable dtBank = new DataTable();
                                    if (dtBank != null && dtBank.Rows.Count > 0)
                                    {
                                        int li = dt_sap_rfc.Tables[1].Rows.Count + 1;
                                        for (int l = 0; l < dtBank.Rows.Count; l++)
                                        {
                                            ref_key_no = Convert.ToString(dtBank.Rows[0]["REF_DOC_NO"]);
                                            if (rfc_string1 == "")
                                            {
                                                rfc_string1 += Convert.ToString(dtBank.Rows[l]["COMP_CODE"]);
                                            }
                                            else
                                            {
                                                rfc_string1 += "|" + Convert.ToString(dtBank.Rows[l]["COMP_CODE"]);
                                            }
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["DOC_DATE"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["PSTNG_DATE"]);
                                            rfc_string1 += "$";
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["DOC_TYPE"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["REF_DOC_NO"]);
                                            rfc_string1 += "$" + Convert.ToString(li + l);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["GL_ACCOUNT"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["REF_KEY_1"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["VENDOR_NO"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["REF_DOC_NO"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["ITEM_TEXT"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["BUS_AREA"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["COSTCENTER"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["PROFIT_CTR"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["CURRENCY"]);
                                            rfc_string1 += "$" + Convert.ToInt32(dtBank.Rows[l]["AMT_DOCCUR"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["ZLSCH"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["PERSON_NO"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["SECCO"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["BUPLA"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["ZFBDT"]);
                                            rfc_string1 += "$ ";
                                        }
                                    }

                                    line_item = getLine_Item();
                                    //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + line_item + "');}</script>");
                                    Vendor_Portal.Vendor_Portal_DetailsService Vendor = new Vendor_Portal.Vendor_Portal_DetailsService();
                                    string[] Vendor_data_array = new string[3];
                                    Vendor_data_array = Vendor.BANK_DETAILS(rfc_string1, line_item);
                                    //Vendor_data_array[0] = "";
				    //Vendor_data_array[1]="";
                                    rfc_flag = Convert.ToString(Vendor_data_array[1]);
                                    alert_msg = Convert.ToString(Vendor_data_array[0]);
                                    string[] sp_data = Convert.ToString(Vendor_data_array[0]).Split(' ');

                                    if (Convert.ToString(Vendor_data_array[1]) == "S")
                                    {
                                        for (int k = 0; k < sp_data.Length; k++)
                                        {
                                            if (Convert.ToString(sp_data[k]).ToUpper().Contains("SCIL"))
                                            {
                                                rfc_no = Convert.ToString(sp_data[k]).Substring(0, 10);
                                            }
                                        }
                                    }
                                    string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref ref_data, spn_req_no.InnerHtml, "BANK", "", "", "", Convert.ToString(Vendor_data_array[1]).ToUpper(), rfc_no, Convert.ToString(Vendor_data_array[0]), ref_key_no);
                                }
                            }
                            else
                            {
                                rfc_flag = bank_flag;
                                rfc_no = bank_no;
                            }
                            /***************************************************************************************************************************************/

                            if (Convert.ToString(rfc_flag) != "E" && rfc_flag != "")
                            {
                                isSaved = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "updateamount", ref ref_data, txt_pk_id.Text, pk_bank_id.Text, adv_amount1.Text, Tot_Amount1.Text, exc_rate1.Text, balance1.Text, return_money1.Text, return_exc1.Text, adv_in_inr1.Text, exp_in_inr1.Text, return_in_inr1.Text, txt_Username.Text, pay_ref1.Text, doc_no1.Text, doc_date1.Text);
                                if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                                {
                                    string[] errmsg = ref_data.Split(':');
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                                }
                                else
                                {

                                    string[] Dval = new string[1];
                                    Dval[0] = txt_Username.Text;

                                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                                    if (isCreate)
                                    {
                                        try
                                        {
                                            string auditid = (string)ActionController.ExecuteAction(txt_initiator.Text, "Foreign_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, step_name.Text, "USER", txt_Username.Text, "SUBMIT", "Send For Payment Process", "0", "0");
                                            string msg = "";
                                            CryptoGraphy crypt = new CryptoGraphy();
                                            string process_name = crypt.Encryptdata("FOREIGN TRAVEL EXPENSE");
                                            string req_no = crypt.Encryptdata(spn_req_no.InnerHtml);
                                            msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Foreign Travel Request Has Been Approved Succefully.</span></pre><pre><span style='font-size: medium;'>Request No: " + spn_req_no.InnerHtml + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre></pre><pre>&nbsp;</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                            string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, step_name.Text, "SUBMIT", txt_init_mail.Text, "", msg, "Request No: " + spn_req_no.InnerHtml);
                                        }
                                        catch (Exception exc1)
                                        {
                                            //throw;
					Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + exc1.ToString() + "...!');}</script>");
                                        }
                                        finally
                                        {
                                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Travel Request Has Been Approved Successfully : " + rfc_no + "...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_msg + "...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                        }

                    }
                    else if (ddlAction.SelectedValue == "2")
                    {
                        string release_id = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreleaseid", ref ref_data, txtProcessID.Text, step_name.Text, "SEND-BACK");
                        if (release_id != "")
                        {
                            isSaved = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "update", ref ref_data, txt_pk_id.Text, txt_Document_Xml.Text, "2",0);
                            if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = ref_data.Split(':');
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                            }
                            else
                            {
                                string[] Dval = new string[1];
                                Dval[0] = txt_initiator.Text;
                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "SEND-BACK", txt_Username.Text, txt_initiator.Text.Trim(), txt_initiator.Text.Trim(), "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                                if (isCreate)
                                {
                                    try
                                    {
                                        string auditid = (string)ActionController.ExecuteAction(txt_initiator.Text, "Foreign_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, step_name.Text, "USER", txt_Username.Text, "SEND-BACK", txt_Remark.Text, "0", "0");

                                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Foreign Travel Request Has Been Sent Back To Initiator For Modification.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                        string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, step_name.Text, "SEND-BACK", txt_init_mail.Text, "", msg, "Request No: " + spn_req_no.InnerHtml);
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }
                                    finally
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Travel Request Has Been Sent Back To Initiator Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                    else if (ddlAction.SelectedValue == "3")
                    {
                        string release_id = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreleaseid", ref ref_data, txtProcessID.Text, step_name.Text, "REJECT");
                        if (release_id != "")
                        {
                            isSaved = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "update", ref ref_data, txt_pk_id.Text, txt_Username.Text, "3",0);
                            if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = ref_data.Split(':');
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                            }
                            else
                            {
                                string[] Dval = new string[1];
                                Dval[0] = txt_Username.Text;
                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                                if (isCreate)
                                {
                                    try
                                    {
                                        string auditid = (string)ActionController.ExecuteAction(txt_initiator.Text, "Foreign_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, step_name.Text, "USER", txt_Username.Text, "REJECT", txt_Remark.Text, "0", "0");

                                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Foreign Travel Request Has Been Rejected.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                        string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, step_name.Text, "REJECT", txt_init_mail.Text, "", msg, "Request No: " + spn_req_no.InnerHtml);
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }
                                    finally
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Travel Request Has Been Rejected...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Please Select Action...');}</script>");
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string chagable_or_not(string voucher, int fk_id, string data)
    {
        string html = string.Empty;
        string is_data = string.Empty;
        try
        {
            html = data + "@@";
            html += (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getreimbursementcharge", ref is_data, voucher, fk_id);

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return html;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
    }

    public string getLine_Item()
    {
        string line_item = "";
        line_item = "0001" + "$" + spn_req_no.InnerHtml;
        line_item += "|" + "0002" + "$" + "Foreign Travel Request";
        line_item += "|" + "0003" + "$" + "Foreign Travel Request";
        line_item += "|" + "0004" + "$" + "Visit To " + spn_Country.InnerHtml;
        line_item += "|" + "0005" + "$" + span_ename.InnerHtml;
        line_item += "|" + "0006" + "$" + spn_Country.InnerHtml;
        line_item += "|" + "0007" + "$" + travelFromDate.InnerHtml + " To " + travelToDate.InnerHtml;
        line_item += "|" + "0008" + "$" + Tot_Amount1.Text;
        line_item += "|" + "0009" + "$" + spn_Currency.InnerHtml;
        line_item += "|" + "0010" + "$" + exc_rate1.Text;
        line_item += "|" + "0011" + "$" + expense_amount1.Text;
        line_item += "|" + "0012" + "$" + "Visit To " + spn_Country.InnerHtml;
        return line_item;
    }
}