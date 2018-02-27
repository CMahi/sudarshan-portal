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

public partial class Domestic_Travel_Request_Doc_Verification : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Domestic_Travel_Request_Doc_Verification));
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
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "pgetrequestdata", ref isdata, txtWIID.Text);
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["pk_travel_expense_hdr_id"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["travel_voucher_id"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                txt_initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]);
                txt_init_mail.Text = Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);
                travelFromDate.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["from_date"]).ToString("dd-MMM-yyyy");
                travelToDate.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["to_date"]).ToString("dd-MMM-yyyy");
                ddlTravel_Type.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["travel_type"]);
                req_remark.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["remark"]);
                pay_mode.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["pay_mode"]);
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

                dev_travel_class.Text = Convert.ToString(dsData.Tables[0].Rows[0]["dev_travel_class"]);
                dev_policy_amt.Text = Convert.ToString(dsData.Tables[0].Rows[0]["dev_policy_amt"]);
                dev_supp_amt.Text = Convert.ToString(dsData.Tables[0].Rows[0]["dev_supp_amt"]);

                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgettraveluser", ref isdata, txt_initiator.Text);
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    empno.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
                    span_ename.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
                    span_cc.InnerHtml = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
                    span_dept.InnerHtml = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
                    span_grade.InnerHtml = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
                    span_mobile.InnerHtml = Convert.ToString(dtUser.Rows[0]["MOBILE_NO"]);
                    Desg_Name.Text = span_designation.InnerHtml = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
                    txt_designation.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
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
                    span_Division.InnerHtml = Convert.ToString(dtUser.Rows[0]["DIVISION_NAME"]);
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
                    checkDeviation();
                    fillDocument_Details(dsData.Tables[3]);
                    fillAdvanceAmount();
                    fillSupporting();
                    fillAuditTrail();
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void checkDeviation()
    {
        string dev_Policy = "", dev_supp = "", dev_Class = "", dev_string = "";
        if (dev_policy_amt.Text == "1")
        {
            dev_Policy = "Policy Amount";
        }
        if (dev_supp_amt.Text == "1")
        {
            dev_supp = "Supporting Amount";
        }
        if (dev_travel_class.Text == "1")
        {
            dev_Class = "Travel Class";
        }

        if (dev_Policy != "")
        {
            dev_string = dev_Policy;
            if (dev_supp != "" && dev_Class == "")
            {
                dev_string += " and " + dev_supp;
            }
            else if (dev_supp != "" && dev_Class != "")
            {
                dev_string += ", " + dev_supp + " and " + dev_Class;
            }
            else if (dev_supp == "" && dev_Class != "")
            {
                dev_string += " and " + dev_Class;
            }
        }
        else
        {
            dev_string = dev_supp;
            if (dev_supp != "")
            {
                if (dev_Class != "")
                {
                    dev_string += " and " + dev_Class;
                }
            }
            else
            {
                dev_string = dev_Class;
            }
        }
        if (dev_string != "")
        {
            dev_string = "Request Deviated Due To " + dev_string + " Exceeded.";
            div_deviated.Style.Add("display", "normal");
            span_deviate.InnerHtml = dev_string;
        }
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

    protected void fillSupporting()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtSupp = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getsupportingperc", ref isdata, "DOMESTIC_SUPPORTING");
            if (dtSupp != null)
            {
                if (dtSupp.Rows.Count > 0)
                {
                    supp_perc_no.Text = Convert.ToString(dtSupp.Rows[0]["S_NO"]);
                }
                else
                {
                    supp_perc_no.Text = "0";
                }
            }
            else
            {
                supp_perc_no.Text = "0";
            }
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
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgetadvancedetails", ref isValid, txt_initiator.Text, txt_pk_id.Text, 2);
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
                {
                    tblHTML.Append("<tr>");
                    tblHTML.Append("<td>" + (Index + 1) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["ADVANCE_DATE"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["amount"]) + "</td>");
                    //tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["Approved"]) + "</td>");
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
                            DisplayData += "<td>" + Convert.ToString(dt.Rows[i]["DOCUMENT_TYPE"]) + "</td>";
                            DisplayData += "<td><input type='text' id='anc_" + (i + 1) + "' value='" + Convert.ToString(dt.Rows[i]["FileName"]) + "' style='display:none'/>  <a href='#' onclick='downloadfiles(" + (i + 1) + ")'>" + Convert.ToString(dt.Rows[i]["FileName"]) + "</td>";
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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string[] get_Travel_Class(int pk_id, int i)
    {
        string[] ret_val = new string[2];
        string isdata = string.Empty;
        ret_val[0] = Convert.ToString(i);
        string val = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "gettravelclass", ref isdata, pk_id);
        ret_val[1] = val;
        return ret_val;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_Journey_Data(string jFDate, string jTDate, int wiid)
    {
        string jdata = string.Empty;
        DateTime fdate = Convert.ToDateTime(jFDate);
        DateTime tdate = Convert.ToDateTime(jTDate);
        StringBuilder html = new StringBuilder();
        try
        {
            string IsValid = string.Empty;

            html.Append("<div class='panel'>");
            html.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
            html.Append("<div class='panel-heading-btn'></div>");
            html.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' href='#collapse0'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spnSummary'>Summary</span></a></h3></div>");
            html.Append("<div id='collapse0' class='panel-collapse collapse in'><div class='panel-body' style='background-color:#f0f5f5'><div class='form-horizontal'>");

            html.Append("<div class='form-group'></div>");
            html.Append("<div class='col-md-12'>");

            html.Append("<table class='table table-bordered'>");
            html.Append("<thead><tr class='grey'>");
            html.Append("<th>Voucher Amount (<i class='fa fa-rupee'></i>)</th>");
            html.Append("<th>Non Required Supporting Amount (<i class='fa fa-rupee'></i>)</th>");
            html.Append("<th>Required Supporting Amount (<i class='fa fa-rupee'></i>)</th>");

            html.Append("<th>Non Supporting Excluded Percentage (<i class='fa fa-rupee'></i>)</th>");
            html.Append("<th>Advance Amount (<i class='fa fa-rupee'></i>)</th>");
            html.Append("<th>Remark (<i class='fa fa-rupee'></i>)</th>");
            html.Append("</tr></thead>");
            html.Append("<tbody><tr>");

            DataTable dtSummary = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "pgetsummary", ref IsValid, wiid);
            if (dtSummary != null || dtSummary.Rows.Count > 0)
            {
 		Decimal tot = Convert.ToInt32(dtSummary.Rows[0]["voucher_amount"]);
                Decimal nreq = Convert.ToInt32(dtSummary.Rows[0]["Tot_Req_Supp_Amt"]);
                Decimal req = tot - nreq;
                html.Append("<td align='right'>" + Convert.ToString(dtSummary.Rows[0]["voucher_amount"]) + "</td>");
                if(tot!=nreq)
{
		html.Append("<td align='right'>" + Convert.ToDouble(req).ToString("#.00") + "</td>");
}
else
{
		html.Append("<td align='right'>" + Convert.ToString("0.00") + "</td>");
}
                html.Append("<td align='right'>" + Convert.ToString(dtSummary.Rows[0]["Tot_Req_Supp_Amt"]) + "</td>");
                html.Append("<td align='right'>" + Convert.ToString(dtSummary.Rows[0]["Non_Supp_perc"]) + "</td>");
                html.Append("<td align='right'>" + Convert.ToString(dtSummary.Rows[0]["advance_amount"]) + "</td>");
                string[] rem = Convert.ToString(dtSummary.Rows[0]["remark"]).Split('@');
                if (rem[0] == "NIL")
                {
                    html.Append("<td>" + rem[0] + "</td>");
                }
                else
                {
                    html.Append("<td>" + rem[0] + " <i class='fa fa-rupee'></i> " + rem[1] + "</td>");
                }
            }
            else
            {
                html.Append("<td align='right'>0</td><td align='right'>0</td><td align='right'>0</td><td align='right'>0</td><td align='right'>0</td><td align='right'>0</td>");
            }
            html.Append("</tr></tbody></table>");
            html.Append("</div>");



            html.Append("</div></div></div></div>");

            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "pgetrequestdata", ref IsValid, wiid);
            if (dsData != null)
            {
                for (int i = 0; i < dsData.Tables[1].Rows.Count; i++)
                {
                    html.Append("<div class='panel'>");
                    html.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
                    html.Append("<div class='panel-heading-btn'><div>Amount : <span id='Total" + (i + 1) + "'>" + dsData.Tables[1].Rows[i]["total_amount"] + "</span> <i class='fa fa-rupee'></i></div></div>");
                    html.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' href='#collapse" + (i + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spn_date_" + (i + 1) + "'>" + Convert.ToDateTime(dsData.Tables[1].Rows[i]["travel_date"]).ToString("dd-MMM-yyyy") + "</span></a></h3></div>");

                    html.Append("<div id='collapse" + (i + 1) + "' class='panel-collapse collapse'><div class='panel-body' style='background-color:#f0f5f5'><div class='form-horizontal'>");

                    html.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Journey Type</label>");
                    html.Append("<div class='col-md-3'><span id='journey_Type" + (i + 1) + "' name='jt'>" + dsData.Tables[1].Rows[i]["journey"] + "</span></div>");
                    html.Append("<div class='col-md-1'></div><label class='col-md-2'>Particulars</label><div class='col-md-3'><span id='remark_note" + (i + 1) + "'>" + dsData.Tables[1].Rows[i]["remark_note"] + "</span></div>");
                    html.Append("</div>");


                    string tr_mode = Convert.ToString(dsData.Tables[1].Rows[i]["travel_name"]);
                    string tr_class = Convert.ToString(dsData.Tables[1].Rows[i]["travel_mapping_class"]);
                    if (Convert.ToString(dsData.Tables[1].Rows[i]["travel_mode"]) == "-1")
                    {
                        tr_mode = "Other";
                        tr_class = "--";
                    }
                    html.Append("<div class='form-group'><div id='div_Travel_Mode" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Travel Mode</label>");
                    html.Append("<div class='col-md-3'>" + tr_mode + "</div></div>");

                    html.Append("<div id='div_Travel_Class" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Travel Class</label><div class='col-md-3'>");
                    html.Append("<span ID='Travel_Class" + (i + 1) + "' runat='server'>" + tr_class + "</span>");
                    html.Append("</div></div></div>");

                    //html.Append("<div class='form-group'><div id='div_FPlant" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Plant From</label><div class='col-md-3'>");
                    if (Convert.ToString(dsData.Tables[1].Rows[i]["journey"]) == "Overnight Stay Within Plant")
                    {
                        html.Append("<div class='form-group'><div id='div_FPlant" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Plant To</label><div class='col-md-3'>");
                    }
                    else
                    {
                        html.Append("<div class='form-group'><div id='div_FPlant" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Plant From</label><div class='col-md-3'>");
                    }
                    html.Append("<span ID='From_Plant" + (i + 1) + "' runat='server'>" + dsData.Tables[1].Rows[i]["f_plant"] + "</span>");
                    html.Append("</div></div>");

                    html.Append("<div id='div_TPlant" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Plant To</label><div class='col-md-3'>");
                    html.Append("<span ID='To_Plant" + (i + 1) + "' runat='server'>" + dsData.Tables[1].Rows[i]["t_plant"] + "</span>");
                    html.Append("</div></div></div>");

                    html.Append("<div class='form-group'>");
                    if (Convert.ToString(dsData.Tables[1].Rows[i]["journey"]) == "Overnight Stay Within Plant" || Convert.ToString(dsData.Tables[1].Rows[i]["journey"]) == "One Day Outstation Within Plant")
                    {
                        if (Convert.ToString(dsData.Tables[1].Rows[i]["beyond_time"]) == "1")
                        {
                            html.Append("<div id='div_PM" + (i + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + (i + 1) + "' checked='checked' disabled/>Reached Beyond 10.00 PM?</label></div>");
                        }
                        else
                        {
                            html.Append("<div id='div_PM" + (i + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + (i + 1) + "' disabled/>Reached Beyond 10.00 PM?</label></div>");
                        }
                    }
                    else
                    {
                        html.Append("<div id='div_PM" + (i + 1) + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + (i + 1) + "' disabled/>Reached Beyond 10.00 PM?</label></div>");
                    }
                    if (Convert.ToString(dsData.Tables[1].Rows[i]["journey"]) == "Overnight Stay Within Plant")
                    {
                        if (Convert.ToString(dsData.Tables[1].Rows[i]["Stay_Guest_House"]) == "1")
                        {
                            html.Append("<div id='div_GH" + (i + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_guest_" + (i + 1) + "' checked='checked' disabled/>Stay at Guest House?</label></div>");
                        }
                        else
                        {
                            html.Append("<div id='div_GH" + (i + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_guest_" + (i + 1) + "' disabled/>Stay at Guest House?</label></div>");
                        }
                    }
                    else
                    {
                        html.Append("<div id='div_GH" + (i + 1) + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_guest_" + (i + 1) + "' disabled/>Stay at Guest House?</label></div>");
                    }
                    html.Append("</div>");

                    html.Append("<div class='form-group' id='div_City" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>From City</label>");
                    html.Append("<div class='col-md-3'>" + dsData.Tables[1].Rows[i]["f_city"] + "</div>");
                    html.Append("<div class='col-md-1'></div><label class='col-md-2'>To City</label><div class='col-md-3'>" + dsData.Tables[1].Rows[i]["t_city"] + "</div></div>");

                    html.Append("<div class='form-group' id='div_PlaceRoom" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Place Class</label>");
                    html.Append("<div class='col-md-3'><span id='placeclass" + (i + 1) + "'>" + dsData.Tables[1].Rows[i]["place_class"] + "</span> Class</div><div class='col-md-1'></div><label class='col-md-2' style='display:none'>Room Type</label>");
                    html.Append("<div class='col-md-3' style='display:none'>" + dsData.Tables[1].Rows[i]["Room_Type"] + "</div></div>");

                    html.Append("<div class='form-group' id='div_HotelContact" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Hotel Name</label><div class='col-md-3'>" + dsData.Tables[1].Rows[i]["hotel_name"] + "</div>");
                    html.Append("<div class='col-md-1'></div><label class='col-md-2'>Hotel Contact No</label><div class='col-md-3'>" + dsData.Tables[1].Rows[i]["hotel_no"] + "</div></div>");

                    string trdate = Convert.ToDateTime(dsData.Tables[1].Rows[i]["travel_date"]).ToString("dd-MMM-yyyy");

                    html.Append("<div id='exp_data" + (i + 1) + "'>");
                    html.Append("<div class='form-group'></div>");
                    html.Append("<div class='col-md-12'>");
                    html.Append("<div class='col-md-1'></div>");
                    html.Append("<div class='col-md-10'>");
                    html.Append("<table class='table table-bordered'>");
                    html.Append("<thead><tr class='grey'>");
                    html.Append("<th>Expense Head</th><th>GL Code</th><th>Reimbursement Type</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Supporting Doc</th><th>Supporting Particulars</th><th>Remark</th>");
                    html.Append("</tr></thead>");
                    html.Append("<tbody>");

                    for (int j = 0; j < dsData.Tables[2].Rows.Count; j++)
                    {
                        string trdate1 = Convert.ToDateTime(dsData.Tables[2].Rows[j]["fk_travel_date"]).ToString("dd-MMM-yyyy");

                        if (trdate == trdate1)
                        {
                            html.Append("<tr>");
                            html.Append("<td style='text-align:left'>" + dsData.Tables[2].Rows[j]["expense_head"] + "</td>");
                            html.Append("<td style='text-align:left'>" + dsData.Tables[2].Rows[j]["SAP_GLCode"] + "</td>");
                            html.Append("<td style='text-align:left'>" + dsData.Tables[2].Rows[j]["REIMBURSEMENT_TYPE"] + "</td>");
                            html.Append("<td style='text-align:right'>" + dsData.Tables[2].Rows[j]["exp_amt"] + "</td>");
                            html.Append("<td style='text-align:center'>" + dsData.Tables[2].Rows[j]["supp_doc1"] + "</td>");
                            html.Append("<td style='text-align:center'>" + dsData.Tables[2].Rows[j]["supp_remark"] + "</td>");
                            html.Append("<td style='text-align:center'>" + dsData.Tables[2].Rows[j]["other_remark"] + "</td>");
                            html.Append("</tr>");
                        }
                    }
                    html.Append("</tbody></table>");
                    html.Append("</div>");
                    html.Append("<div class='col-md-1'></div>");
                    html.Append("</div>");
                    html.Append("<div class='form-group'></div>");

                    html.Append("</div>");

                    html.Append("</div></div></div></div></div>");

                }
            }

            jdata = Convert.ToString(html);

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return jdata;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_dev_policy(int desg, int j_val, int travel_mode_id, int travel_class_id)
    {
        string html = "1";
        string is_data = string.Empty;
        try
        {
            DataTable ejm = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getpolicyclass", ref is_data, desg, j_val, travel_mode_id, travel_class_id);
            if (ejm != null)
            {
                if (ejm.Rows.Count > 0)
                {
                    html = "0";
                }
            }

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return html;
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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_travel_class(int travel_mode, int rno)
    {
        string ret_val = string.Empty;
        string is_data = string.Empty;
        try
        {
            DataTable dtClass = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgettravelclass", ref is_data, travel_mode);
            ret_val += Convert.ToString(rno) + "||";
            ret_val += "0$$---Select One---";
            if (dtClass != null)
            {
                for (int i = 0; i < dtClass.Rows.Count; i++)
                {
                    ret_val += "@@" + Convert.ToString(dtClass.Rows[i]["PK_TRAVEL_MAPPING_ID"]) + "$$" + Convert.ToString(dtClass.Rows[i]["TRAVEL_MAPPING_CLASS"]);
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return ret_val;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string showall(string search_data, int pageno, int rpp, string desg, string division)
    {
        GetData getData = new GetData();
        string str_data = getData.get_Travel_Policy_Data(search_data, pageno, rpp, desg,division);
        return str_data;
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

                        DataTable DTAP = new DataTable();
                        if (pay_mode.InnerHtml.ToUpper() == "CASH")
                        {
                            DTAP = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "getaccapprover", ref ref_data, "ACCOUNT PAYMENT PERSONNEL", pk_loc_id.Text, pk_mode_id.Text);
                        }
                        else
                        {
                            DTAP = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "getaccapprover", ref ref_data, "ACCOUNT PAYMENT PERSONNEL", 0, pk_mode_id.Text);
                        }
                        if (DTAP != null)
                        {
                            if (DTAP.Rows.Count > 0)
                            {

                                isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "update", ref ref_data, txt_pk_id.Text, txt_Document_Xml.Text, 1);
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
                                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "179", "TRAVEL EXPENSE DOCUMENT VERIFICATION", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                                    if (isCreate)
                                    {
                                        try
                                        {
                                            string auditid = (string)ActionController.ExecuteAction(txt_initiator.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "USER", txt_Username.Text, "SUBMIT", "Send For Payment Approval", "0", "0");

                                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Documents Verified For Domestic Travel Expense Request and Sent To Account Payment Approver.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                            string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "SUBMIT", txtApproverEmail.Text, txt_init_mail.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                        }
                                        catch (Exception)
                                        {
                                            throw;
                                        }
                                        finally
                                        {
                                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Documents Verified For Domestic Travel Expense Request and Sent To Account Payment Approver...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                        }
                                    }
                                }//
                            }
                            else
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payment Approver Not Available For " + pay_mode.InnerHtml + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payment Approver Not Available For " + pay_mode.InnerHtml + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                        }


                    }
                    else if (ddlAction.SelectedValue == "2")
                    {

                        isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "update", ref ref_data, txt_pk_id.Text, txt_Document_Xml.Text, "2");
                        if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                        {
                            string[] errmsg = ref_data.Split(':');
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                        }
                        else
                        {
                            string[] Dval = new string[1];
                            Dval[0] = txt_initiator.Text;
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "180", "TRAVEL EXPENSE DOCUMENT VERIFICATION", "SEND-BACK", txt_Username.Text, txt_initiator.Text.Trim(), txt_initiator.Text.Trim(), "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string auditid = (string)ActionController.ExecuteAction(txt_initiator.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "USER", txt_Username.Text, "SEND-BACK", txt_Remark.Text, "0", "0");

                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Domestic Travel Request Has Been Sent Back To Initiator Successfully.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "SEND-BACK", txt_init_mail.Text, "", msg, "Request No: " + spn_req_no.InnerHtml);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Has Been Sent Back To Initiator Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                    }
                    else if (ddlAction.SelectedValue == "3")
                    {
                        isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "update", ref ref_data, txt_pk_id.Text, txt_Document_Xml.Text, "3");
                        if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                        {
                            string[] errmsg = ref_data.Split(':');
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                        }
                        else
                        {
                            string[] Dval = new string[1];
                            Dval[0] = txt_Username.Text;
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "181", "TRAVEL EXPENSE DOCUMENT VERIFICATION", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string auditid = (string)ActionController.ExecuteAction(txt_initiator.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "USER", txt_Username.Text, "REJECT", txt_Remark.Text, "0", "0");

                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Domestic Travel Request Has Been Rejected.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "REJECT", txt_init_mail.Text, "", msg, "Request No: " + spn_req_no.InnerHtml);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Has Been Rejected...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
    }
}