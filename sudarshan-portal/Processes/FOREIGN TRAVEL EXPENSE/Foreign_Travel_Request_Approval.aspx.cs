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

public partial class Foreign_Travel_Request_Approval : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Foreign_Travel_Request_Approval));
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
                dev_policy_amt.Text = Convert.ToString(dsData.Tables[0].Rows[0]["DO_APPROVAL_FLAG"]);
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

    private void fillAdvanceAmount()
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
            if (dsData != null)
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
                sb.Append("<td>Total Reimbursement Amount <p>(Excluding Corporate Credit Card & Company Provided)</p> </td>");
                sb.Append("<td>" + Convert.ToString(dsData.Tables[4].Rows[0]["Tot_Amount"]) + "</td>");
                sb.Append("<td>Difference : </td>");
                sb.Append("<td>" + (Convert.ToDecimal(dsData.Tables[4].Rows[0]["adv_amount"]) - Convert.ToDecimal(dsData.Tables[4].Rows[0]["Tot_Amount"])) + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr style='font-weight:bold'>");
                sb.Append("<td>Non Supporting Amount : </td>");
                sb.Append("<td>" + Convert.ToString(dsData.Tables[4].Rows[0]["non_supporting"]) + "</td>");
                sb.Append("<td>Return Amount : </td>");
                if (Convert.ToString(dsData.Tables[0].Rows[0]["Return_Money"]) == "" || Convert.ToString(dsData.Tables[0].Rows[0]["Return_Money"]) == "0")
                {
                    sb.Append("<td>0.00</td>");
                }
                else
                {
                    sb.Append("<td>" + Convert.ToDecimal(dsData.Tables[0].Rows[0]["Return_Money"]).ToString("#.00") + "</td>");
                }

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
                    if (Convert.ToString(dtExp.Rows[row]["expense_head"]) != "Incidental")
                    {
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
                    }
                    else
                    {
                        html.Append("<td></td>");
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
                if (ddlAction.SelectedIndex > 0)
                {
                    if (ddlAction.SelectedValue == "1")
                    {
                        if (dev_policy_amt.Text.ToUpper() != "YES")
                        {
                            string release_id = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreleaseid", ref ref_data, txtProcessID.Text, step_name.Text, "SUBMIT");
                            if (release_id != "")
                            {
                                DataTable DTAP = new DataTable();
                                DTAP = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getaccapprover", ref ref_data, "FOREIGN TRAVEL DOC VERIFIER", 0, pk_mode_id.Text);
                                //FOREIGN TRAVEL ACCOUNT APPROVAL
                                if (DTAP != null)
                                {
                                    if (DTAP.Rows.Count > 0)
                                    {
                                        isSaved = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "update", ref ref_data, txt_pk_id.Text, txt_Username.Text, 1,0);
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
                                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                                            if (isCreate)
                                            {
                                                try
                                                {
                                                    string auditid = (string)ActionController.ExecuteAction(txt_initiator.Text, "Foreign_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, step_name.Text, "USER", txt_Username.Text, "SUBMIT", "Send For Document Approval", "0", "0");
                                                    string msg = "";
                                                    CryptoGraphy crypt = new CryptoGraphy();
                                                    string process_name = crypt.Encryptdata("FOREIGN TRAVEL EXPENSE");
                                                    string req_no = crypt.Encryptdata(spn_req_no.InnerHtml);
                                                    msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Foreign Travel Request Has Been Approved Succefully and Sent For Document Verification.</span></pre><pre><span style='font-size: medium;'>Request No: " + spn_req_no.InnerHtml + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";

                                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, step_name.Text, "SUBMIT", txt_init_mail.Text, txtApproverEmail.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                                }
                                                catch (Exception)
                                                {
                                                    throw;
                                                }
                                                finally
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Travel Request Has Been Approved Successfully and Sent For Document Verification...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }
                                        }//
                                    }
                                    else
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Document Verifier Not Available...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                    }
                                }
                                else
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Document Verifier Not Available...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                            else
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                        else
                        {
                            if (doa_user.Text != "NA" && doa_user.Text != "")
                            {
                                /*************************************************************************************************************************/
                                if (doa_user.Text != span_Approver.InnerHtml)
                                {
                                    string release_id = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreleaseid", ref ref_data, txtProcessID.Text, step_name.Text, "DOA-SUBMIT");
                                    if (release_id != "")
                                    {
                                        isSaved = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "update", ref ref_data, txt_pk_id.Text, txt_Username.Text, "1",0);
                                        if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                                        {
                                            string[] errmsg = ref_data.Split(':');
                                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                                        }
                                        else
                                        {

                                            string[] Dval = new string[1];
                                            Dval[0] = doa_user.Text;

                                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "DOA-SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                                            if (isCreate)
                                            {
                                                try
                                                {
                                                    string auditid = (string)ActionController.ExecuteAction(txt_initiator.Text, "Foreign_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, step_name.Text, "USER", txt_Username.Text, "DOA-SUBMIT", "Send For Deviation Approval", "0", "0");

                                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Foreign Travel Request Has Been Approved and Sent For Deviation Approval.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, step_name.Text, "DOA-SUBMIT", doa_email.Text, txt_init_mail.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                                }
                                                catch (Exception)
                                                {
                                                    throw;
                                                }
                                                finally
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Travel Request Has Been Approved and Sent For Deviation Approval...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                    }
                                }
                                /*************************************************************************************************************************/
                                else
                                {
                                    string release_id = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreleaseid", ref ref_data, txtProcessID.Text, step_name.Text, "DOA-SUBMIT");
                                    if (release_id != "")
                                    {
                                        DataTable DTAP = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getaccapprover", ref ref_data, "FOREIGN TRAVEL DOC VERIFIER", 0, pk_mode_id.Text);
                                        if (DTAP != null && DTAP.Rows.Count > 0)
                                        {
                                            string[] Dval1 = new string[DTAP.Rows.Count];
                                            Dval1[0] = "";
                                            if (DTAP.Rows.Count > 0)
                                            {
                                                for (int i = 0; i < DTAP.Rows.Count; i++)
                                                {
                                                    Dval1[i] = Convert.ToString(DTAP.Rows[i]["USER_ADID"]);
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

                                            isSaved = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "update", ref ref_data, txt_pk_id.Text, txt_Username.Text, "1",0);
                                            if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                                            {
                                                string[] errmsg = ref_data.Split(':');
                                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                                            }
                                            else
                                            {

                                                string[] Dval = new string[1];
                                                Dval[0] = doa_user.Text;

                                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "DOA-SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                                                if (isCreate)
                                                {
                                                    try
                                                    {
                                                        string auditid = (string)ActionController.ExecuteAction(txt_initiator.Text, "Foreign_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, step_name.Text, "USER", txt_Username.Text, "DOA-SUBMIT", "Send For Deviation Approval", "0", "0");

                                                        string wiid = (string)ActionController.ExecuteAction(txt_initiator.Text, "Foreign_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text);

                                                        isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "250", "FOREIGN TRAVEL DEVIATION APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, spn_req_no.InnerHtml, wiid, ref isInserted);

                                                        CryptoGraphy crypt = new CryptoGraphy();
                                                        string process_name = crypt.Encryptdata("FOREIGN TRAVEL EXPENSE");
                                                        string req_no = crypt.Encryptdata(spn_req_no.InnerHtml);

                                                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Foreign Travel Request Has Been Approved Successfully and Sent For Document Verification.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";

                                                        string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, step_name.Text, "DOA-SUBMIT", txt_init_mail.Text, txtApproverEmail.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                                    }
                                                    catch (Exception)
                                                    {
                                                        throw;
                                                    }
                                                    finally
                                                    {
                                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Travel Request Has Been Approved Successfully and Sent For Document Verification...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Document Verifier Not Available...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                        }
                                    }
                                    else
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                    }
                                }
                                /*************************************************************************************************************************/
                            }
                            else
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Deviation Approver Not Found. Unable To Proceed Request Further...!');}</script>");
                            }

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
}