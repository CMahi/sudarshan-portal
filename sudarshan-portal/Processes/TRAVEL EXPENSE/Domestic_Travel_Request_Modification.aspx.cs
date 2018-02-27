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

public partial class Domestic_Travel_Request_Modification : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Domestic_Travel_Request_Modification));
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
                    travelFromDate.Attributes.Add("readonly", "true");
                    travelToDate.Attributes.Add("readonly", "true");
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
                preFDate.Text = ofdate.Text = travelFromDate.Text = Convert.ToDateTime(dsData.Tables[0].Rows[0]["from_date"]).ToString("dd-MMM-yyyy");
                preTDate.Text = otdate.Text = travelToDate.Text = Convert.ToDateTime(dsData.Tables[0].Rows[0]["to_date"]).ToString("dd-MMM-yyyy");
                ddlTravel_Type.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["travel_type"]);
                req_remark.Text = Convert.ToString(dsData.Tables[0].Rows[0]["remark"]);
               
                pk_mode_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["payment_mode"]);
                pk_loc_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["cash_location"]);

                dev_travel_class.Text = Convert.ToString(dsData.Tables[0].Rows[0]["dev_travel_class"]);
                dev_policy_amt.Text = Convert.ToString(dsData.Tables[0].Rows[0]["dev_policy_amt"]);
                dev_supp_amt.Text = Convert.ToString(dsData.Tables[0].Rows[0]["dev_supp_amt"]);
                txt_advance_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["advance_id"]);
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

                    fillDocument_Details(dsData.Tables[3]);
                    fillAdvanceAmount();
                    fillSupporting();
                    fillAuditTrail();
                    fillPayment_Mode();
                    fillLocation();

                    ddl_Payment_Mode.SelectedValue = pk_mode_id.Text;
                    ddlAdv_Location.SelectedValue = pk_loc_id.Text;
                    if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                    {
                        ddlAdv_Location.Enabled = true;
                        lblPay.Style.Add("display","normal");
                        ctrlPay.Style.Add("display", "normal");
                    }
                    else
                    {
                        ddlAdv_Location.Enabled = false;
                        lblPay.Style.Add("display", "none");
                        ctrlPay.Style.Add("display", "none");
                    }
                    decimal tot_amt = 0;
                    for (int rno = 0; rno < dsData.Tables[1].Rows.Count; rno++)
                    {
                        tot_amt += Convert.ToDecimal(dsData.Tables[1].Rows[rno]["total_amount"]);
                        if (Convert.ToString(dsData.Tables[1].Rows[rno]["beyond_time"]) == "1")
                        {
                            txt_chkbox.Text = (rno+1)+"@@1";
                        }
                    }
                    Final_Amtt.InnerHtml = Convert.ToString(tot_amt);
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
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

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Type</th><th>File Name</th><th>Action</th></tr></thead>";
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DisplayData += "<tr>";
                            DisplayData += "<td><span id='f_name" + (i + 1) + "'>" + Convert.ToString(dt.Rows[i]["DOCUMENT_TYPE"]) + "</span></td>";
                            DisplayData += "<td><input type='text' id='anc_" + (i + 1) + "' value='" + Convert.ToString(dt.Rows[i]["FileName"]) + "' style='display:none'/>  <a href='#' onclick=downloadfiles('" + Convert.ToString(dt.Rows[i]["FileName"]) + "')>" + Convert.ToString(dt.Rows[i]["FileName"]) + "</td>";
                            DisplayData += "<td><i id='del" + (i + 1) + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (i + 1) + ");\" ></td>";
                            //DisplayData += "<td><a class='btnDelete'><i id='del" + (i + 1) + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (i + 1) + ");\" ></a></td>";
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

    protected void fillLocation()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtLocation = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "selectlocation", ref isdata, "", "", "PAYMENT_LOCATION", "");
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
            DataTable dtPayment = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "selectlocation", ref isdata, "", "", "M_TRAVEL_PAYMENT_MODE", "");
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
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgetadvancedetails", ref isValid, txt_initiator.Text, txt_pk_id.Text, 3);
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Amount</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
            {
                tblHTML.Append("<tr>");
                if (txt_advance_id.Text == Convert.ToString(dt.Tables[0].Rows[Index]["PK_ADVANCE_HDR_ID"]))
                {
                    tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='travel' checked><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["PK_ADVANCE_HDR_ID"]) + "' style='display:none'><input type='text' id='advance_amount" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["amount"]) + "' style='display:none'></td>");
                    txt_advance_amount.Text = Convert.ToString(dt.Tables[0].Rows[Index]["amount"]);
                }
                else
                {
                    tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='travel' ><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["PK_ADVANCE_HDR_ID"]) + "' style='display:none'><input type='text' id='advance_amount" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["amount"]) + "' style='display:none'></td>");
                }
                tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["ADVANCE_DATE"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["amount"]) + "</td>");
                //tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["Approved"]) + "</td>");
                tblHTML.Append("<td>" + span_app_name.InnerHtml + "</td>");
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

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Name</th><th>Delete</th></tr></thead>";
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
    public static string[] get_Travel_Class1(int pk_id, int i)
    {
        string[] ret_val = new string[2];
        string isdata = string.Empty;
        ret_val[0] = Convert.ToString(i);
        string val = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "gettravelclass", ref isdata, pk_id);
        ret_val[1] = val;
        return ret_val;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_Journey_Data(string jFDate, string jTDate, int wiid, string desg, string ofd, string otd, string division)
    {
        string jdata = string.Empty;
        DateTime fdate = Convert.ToDateTime(jFDate);
        DateTime tdate = Convert.ToDateTime(jTDate);
        DateTime fd1 = fdate;
        DateTime td1 = tdate;
        DateTime ofdate = Convert.ToDateTime(ofd);
        DateTime otdate = Convert.ToDateTime(otd);
        StringBuilder html = new StringBuilder();
        try
        {
            string IsValid = string.Empty;
            //int rno = 0;
            DataSet dtJourney = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "select", ref IsValid);
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "pgetrequestdata", ref IsValid, wiid);
            if (dsData != null)
            {
                if (ofdate == fdate && otdate == tdate)
                {
                    for (int rno = 0; rno < dsData.Tables[1].Rows.Count; rno++)
                    {
                        try{
                            html.Append("<div class='panel' id='remove_" + (rno+1) + "'>");
                        html.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
                        try
                        {
                            html.Append("<div class='panel-heading-btn'><div>Amount : <span id='Total" + (rno + 1) + "'>" + Convert.ToString(dsData.Tables[1].Rows[rno]["total_amount"]) + "</span> <i class='fa fa-rupee'></i></div></div>");
                        }
                        catch
                        {
                            html.Append("<div class='panel-heading-btn'><div>Amount : <span id='Total" + (rno + 1) + "'>0</span> <i class='fa fa-rupee'></i></div></div>");
                        }
                        html.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' onclick='copyData(" + (rno + 1) + ")' href='#collapse" + (rno + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spn_date_" + (rno + 1) + "'>" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + "</span></a></h3></div>");

                        if ((rno + 1) == 1)
                        {
                            html.Append("<div id='collapse" + (rno + 1) + "' class='panel-collapse collapse in'><div class='panel-body' style='background-color:#f0f5f5'><div class='form-horizontal'>");
                        }
                        else
                        {
                            html.Append("<div id='collapse" + (rno + 1) + "' class='panel-collapse collapse'><div class='panel-body' style='background-color:#f0f5f5'><div class='form-horizontal'>");
                        }

                        html.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Journey Type</label>");
                        html.Append("<div class='col-md-3'>");
                        if (dtJourney.Tables[0] != null)
                        {
                            html.Append("<select ID='journey_Type" + (rno + 1) + "' runat='server' name='jt' class='form-control input-sm' onchange='check_journey_Type(" + (rno + 1) + "," + dtJourney.Tables[0].Rows.Count + ");'>");
                            html.Append("<option Value='0'>---Select One---</option>");
                                if (fd1 == td1)
                                {
                                    for (int i = 0; i < dtJourney.Tables[0].Rows.Count; i++)
                                    {
                                        if (Convert.ToString(dtJourney.Tables[0].Rows[i]["ONE_DAY"]) == "1")
                                        {
                                            try
                                            {
                                                if (Convert.ToString(dsData.Tables[1].Rows[rno]["journey_type"]) == Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]))
                                                {
                                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "' Selected='true'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                                                }
                                                else
                                                {
                                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                                                }
                                            }
                                            catch
                                            {
                                                html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < dtJourney.Tables[0].Rows.Count; i++)
                                    {
                                        if (Convert.ToString(dtJourney.Tables[0].Rows[i]["ONE_DAY"]) == "0")
                                        {
                                            try
                                            {
                                                if (Convert.ToString(dsData.Tables[1].Rows[rno]["journey_type"]) == Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]))
                                                {
                                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "' Selected='true'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                                                }
                                                else
                                                {
                                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                                                }
                                            }
                                            catch {
                                                html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                                            }
                                        }
                                    }
                                }
                            
                            html.Append("</select>");
                        }
                        html.Append("</div>");
                        try
                        {
                            html.Append("<div class='col-md-1'></div><label class='col-md-2'>Particulars</label><div class='col-md-3'><input id='remark_note" + (rno + 1) + "' class='form-control input-sm' type='text' value='" + Convert.ToString(dsData.Tables[1].Rows[rno]["remark_note"]) + "'></div>");
                        }
                        catch 
                        {
                            html.Append("<div class='col-md-1'></div><label class='col-md-2'>Particulars</label><div class='col-md-3'><input id='remark_note" + (rno + 1) + "' class='form-control input-sm' type='text' value=''></div>");
                        }
                        html.Append("</div>");

                        html.Append("<div class='form-group'><div id='div_Travel_Mode" + (rno + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Travel Mode</label>");
                        html.Append("<div class='col-md-3'>");
                        if (dtJourney.Tables[1] != null && dtJourney.Tables[1].Rows.Count > 0)
                        {
                            html.Append("<select ID='Travel_Mode" + (rno + 1) + "' runat='server' class='form-control input-sm' onchange='get_travel_class(" + (rno + 1) + ");get_exp_data(" + (rno + 1) + ");'>");
                            html.Append("<option Value='0'>---Select One---</option>");

                            for (int i = 0; i < dtJourney.Tables[1].Rows.Count; i++)
                            {
                                try
                                {
                                    if (Convert.ToString(dsData.Tables[1].Rows[rno]["travel_mode"]) == Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]))
                                    {
                                        html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) + "' Selected='true'>" + Convert.ToString(dtJourney.Tables[1].Rows[i]["TRAVEL_NAME"]) + "</option>");
                                    }
                                    else
                                    {
                                        html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[1].Rows[i]["TRAVEL_NAME"]) + "</option>");
                                    }
                                }
                                catch {
                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[1].Rows[i]["TRAVEL_NAME"]) + "</option>");
                                }
                            }
                            try
                            {
                                if (Convert.ToString(dsData.Tables[1].Rows[rno]["travel_mode"]) == "-1")
                                {
                                    html.Append("<option Value='-1' Selected='true'>Other</option>");
                                }
                                else
                                {
                                    html.Append("<option Value='-1'>Other</option>");
                                }
                            }
                            catch {
                                html.Append("<option Value='-1'>Other</option>");
                            }
                            html.Append("</select>");
                        }
                        else
                        {
                            html.Append("<select ID='Travel_Mode" + (rno + 1) + "' runat='server' class='form-control input-sm' onchange='get_travel_class(" + (rno + 1) + ");get_exp_data(" + (rno + 1) + ");'>");
                            html.Append("<option Value='0'>---Select One---</option>");

                            for (int i = 0; i < dtJourney.Tables[1].Rows.Count; i++)
                            {
                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[1].Rows[i]["TRAVEL_NAME"]) + "</option>");
                            }
                            html.Append("<option Value='-1'>Other</option>");
                            
                            html.Append("</select>");
                        }
                        html.Append("</div></div>");

                        try
                        {
                            if (Convert.ToString(dsData.Tables[1].Rows[rno]["travel_mode"]) == "-1")
                            {
                                html.Append("<div id='div_Travel_Class" + (rno + 1) + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Travel Class</label><div class='col-md-3'>");

                            }
                            else
                            {
                                html.Append("<div id='div_Travel_Class" + (rno + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Travel Class</label><div class='col-md-3'>");
                            }
                        }
                        catch
                        {
                            html.Append("<div id='div_Travel_Class" + (rno + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Travel Class</label><div class='col-md-3'>");
                        }
                        //html.Append("<select ID='Travel_Class" + (rno + 1) + "' class='form-control input-sm' onchange='get_exp_data(" + (rno + 1) + ");'>");
                        html.Append("<select ID='Travel_Class" + (rno + 1) + "' class='form-control input-sm'>");
                        html.Append("<option Value='0'>---Select One---</option>");

                        string ret_val = string.Empty;
                        string is_data = string.Empty;
                        try
                        {
                            DataTable dtClass = new DataTable();
                            if (dsData.Tables[1].Rows.Count > 0)
                            {
                                dtClass = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgettravelclass", ref is_data, Convert.ToString(dsData.Tables[1].Rows[rno]["travel_mode"]));
                            }

                            if (dtClass != null && dtClass.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtClass.Rows.Count; i++)
                                {
                                    if (Convert.ToString(dsData.Tables[1].Rows[rno]["travel_class"]) == Convert.ToString(dtClass.Rows[i]["PK_TRAVEL_MAPPING_ID"]))
                                    {
                                        html.Append("<option Value='" + Convert.ToString(dtClass.Rows[i]["PK_TRAVEL_MAPPING_ID"]) + "' Selected='true'>" + Convert.ToString(dtClass.Rows[i]["TRAVEL_MAPPING_CLASS"]) + "</option>");
                                    }
                                    else
                                    {
                                        html.Append("<option Value='" + Convert.ToString(dtClass.Rows[i]["PK_TRAVEL_MAPPING_ID"]) + "'>" + Convert.ToString(dtClass.Rows[i]["TRAVEL_MAPPING_CLASS"]) + "</option>");
                                    }
                                }
                            }
                        }
                        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }

                        html.Append("</select>");
                        html.Append("</div></div></div>");


                        //html.Append("<div class='form-group'><div id='div_FPlant" + (rno + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Plant From</label><div class='col-md-3'>");
                        if (Convert.ToString(dsData.Tables[1].Rows[rno]["journey"]) == "Overnight Stay Within Plant")
                        {
                            html.Append("<div class='form-group'><div id='div_FPlant" + (rno + 1) + "'><div class='col-md-1'></div><label class='col-md-2' id='dest_plant"+(rno+1)+"'>Plant To</label><div class='col-md-3'>");
                        }
                        else
                        {
                            html.Append("<div class='form-group'><div id='div_FPlant" + (rno + 1) + "'><div class='col-md-1'></div><label class='col-md-2' id='dest_plant" + (rno + 1) + "'>Plant From</label><div class='col-md-3'>");
                        }
                        html.Append("<select ID='From_Plant" + (rno + 1) + "' runat='server' class='form-control input-sm' onchange='get_exp_data(" + (rno + 1) + "); check_on_data("+(rno+1)+")'>");
                        html.Append("<option Value='0'>---Select One---</option>");

                        for (int i = 0; i < dtJourney.Tables[3].Rows.Count; i++)
                        {
                            try
                            {
                                if (Convert.ToString(dsData.Tables[1].Rows[rno]["plant_from"]) == Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]))
                                {
                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "' Selected='true'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                                }
                                else
                                {
                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                                }
                            }
                            catch
                            {
                                html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                            }
                        }
                        html.Append("</select>");
                        html.Append("</div></div>");

                        //html.Append("<div id='div_PM" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + rno + "' onchange='check_on_data("+rno+")' />Reached Beyond 10.00 PM?</label></div>");


                        html.Append("<div id='div_TPlant" + (rno + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Plant To</label><div class='col-md-3'>");
                        html.Append("<select ID='To_Plant" + (rno + 1) + "' runat='server' class='form-control input-sm' onchange='get_exp_data(" + (rno + 1) + ");'>");
                        html.Append("<option Value='0'>---Select One---</option>");
                        for (int i = 0; i < dtJourney.Tables[3].Rows.Count; i++)
                        {
                            try
                            {
                                if (Convert.ToString(dsData.Tables[1].Rows[rno]["plant_to"]) == Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]))
                                {
                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "' Selected='true'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                                }
                                else
                                {
                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                                }
                            }
                            catch
                            {
                                html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                            }

                        }
                        html.Append("</select>");
                        html.Append("</div></div></div>");

                        html.Append("<div class='form-group'>");
                        if (Convert.ToString(dsData.Tables[1].Rows[rno]["journey"]) == "Overnight Stay Within Plant" || Convert.ToString(dsData.Tables[1].Rows[rno]["journey"]) == "One Day Outstation Within Plant")
                        {
                            if ((rno + 1) == dsData.Tables[1].Rows.Count)
                            {
                                if (Convert.ToString(dsData.Tables[1].Rows[rno]["beyond_time"]) == "1")
                                {
                                    //html.Append("<div id='div_PM" + (rno + 1) + "' style='display:none'><div class='col-md-1'></div><div class='col-md-5'><input type='checkbox' checked='checked' onchange='check_on_data(" + (rno + 1) + ")' id='chk_reach_" + (rno + 1) + "' />Reached Beyond 10.00 PM?</div></div>");
                                    html.Append("<div id='div_PM" + (rno + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + (rno + 1) + "' checked='checked' onchange='check_on_data(" + (rno + 1) + ")'/>Reached Beyond 10.00 PM?</label></div>");
                                }
                                else
                                {
                                    html.Append("<div id='div_PM" + (rno + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + (rno + 1) + "' onchange='check_on_data(" + (rno + 1) + ")' />Reached Beyond 10.00 PM?</label></div>");
                                }
                            }
                            else
                            {
                                html.Append("<div id='div_PM" + (rno + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + (rno + 1) + "' onchange='check_on_data(" + (rno + 1) + ")' disabled />Reached Beyond 10.00 PM?</label></div>");
                            }

                            if (Convert.ToString(dsData.Tables[1].Rows[rno]["Stay_Guest_House"]) == "1")
                            {
                                html.Append("<div id='div_GH" + (rno + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_guest_" + (rno + 1) + "' onchange='check_on_guest(" + (rno + 1) + ")' checked/>Stay at Guest House?</label></div>");
                            }
                            else
                            {
                                html.Append("<div id='div_GH" + (rno + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_guest_" + (rno + 1) + "' onchange='check_on_guest(" + (rno + 1) + ")' />Stay at Guest House?</label></div>");
                            }

                        }
                        else
                        {
                            html.Append("<div id='div_PM" + (rno + 1) + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + (rno + 1) + "' onchange='check_on_data(" + (rno + 1) + ")' />Reached Beyond 10.00 PM?</label></div>");
                            html.Append("<div id='div_GH" + (rno+1) + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_guest_" + (rno + 1) + "' onchange='check_on_guest(" + (rno + 1) + ")' />Stay at Guest House?</label></div>");
                        }
                        html.Append("</div>");

                        html.Append("<div class='form-group' id='div_City" + (rno + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>From City</label>");
                        html.Append("<div class='col-md-3'>");
                        if (dtJourney.Tables[5] != null)
                        {
                            html.Append("<select ID='From_City" + (rno + 1) + "' runat='server' class='form-control input-sm' onchange='chk_class_From(" + (rno + 1) + ")'>");
                            html.Append("<option Value='0'>---Select One---</option>");

                            if (dsData.Tables[1].Rows.Count > 0)
                            {
                                if (Convert.ToString(dsData.Tables[1].Rows[rno]["from_city"]) == "-1")
                                {
                                    for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                                    {
                                        html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                                    }
                                    html.Append("<option Value='-1' Selected='true'>Other</option>");
                                    html.Append("</select>");
                                    html.Append("<input type='text' class='form-control input-sm' id='txt_f_city" + (rno + 1) + "' value='" + Convert.ToString(dsData.Tables[1].Rows[rno]["other_f_city"]) + "'>");
                                }
                                else
                                {
                                    for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                                    {
                                        if (Convert.ToString(dsData.Tables[1].Rows[rno]["from_city"]) == Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]))
                                        {
                                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "' Selected='true'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                                        }
                                        else
                                        {
                                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                                        }
                                    }
                                    html.Append("<option Value='-1'>Other</option>");
                                    html.Append("</select>");
                                    html.Append("<input type='text' class='form-control input-sm' id='txt_f_city" + (rno + 1) + "' style='display:none'>");
                                }
                            }
                            else
                            {
                                for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                                {

                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                                }

                                html.Append("<option Value='-1'>Other</option>");
                                html.Append("</select>");
                                html.Append("<input type='text' class='form-control input-sm' id='txt_f_city" + (rno + 1) + "' style='display:none'>");
                            }
                        }
                        html.Append("</div>");
                        html.Append("<div class='col-md-1'></div><label class='col-md-2'>To City</label><div class='col-md-3'>");
                        html.Append("<select ID='To_City" + (rno + 1) + "' runat='server' class='form-control input-sm' onchange='chk_class_To(" + (rno + 1) + ");get_exp_data(" + (rno + 1) + ");'>");
                        html.Append("<option Value='0'>---Select One---</option>");

                        if (dsData.Tables[1].Rows.Count > 0)
                        {
                            if (Convert.ToString(dsData.Tables[1].Rows[rno]["to_city"]) == "-1")
                            {
                                for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                                {
                                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                                }
                                html.Append("<option Value='-1' Selected='true'>Other</option>");
                                html.Append("</select>");
                                html.Append("<input type='text' class='form-control input-sm' id='txt_t_city" + (rno + 1) + "' value='" + Convert.ToString(dsData.Tables[1].Rows[rno]["other_t_city"]) + "'>");
                            }
                            else
                            {
                                for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                                {
                                    if (Convert.ToString(dsData.Tables[1].Rows[rno]["to_city"]) == Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]))
                                    {
                                        html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "' Selected='true'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                                    }
                                    else
                                    {
                                        html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                                    }
                                }
                                html.Append("<option Value='-1'>Other</option>");
                                html.Append("</select>");
                                html.Append("<input type='text' class='form-control input-sm' id='txt_t_city" + (rno + 1) + "' style='display:none'>");
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                            {
                                html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                            }
                            html.Append("<option Value='-1'>Other</option>");
                            html.Append("</select>");
                            html.Append("<input type='text' class='form-control input-sm' id='txt_t_city" + (rno + 1) + "' style='display:none'>");
                        }
                        html.Append("</div></div>");

                        html.Append("<div class='form-group' id='div_PlaceRoom" + (rno + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Place Class</label>");
                        try
                        {
                            html.Append("<div class='col-md-3'><span id='placeclass" + (rno + 1) + "'>" + Convert.ToString(dsData.Tables[1].Rows[rno]["place_class"]) + "</span> Class</div><div class='col-md-1'></div><label class='col-md-2' style='display:normal'>Room Type</label>");
                        }
                        catch {
                            html.Append("<div class='col-md-3'><span id='placeclass" + (rno + 1) + "'>NA</span> Class</div><div class='col-md-1'></div><label class='col-md-2' style='display:normal'>Room Type</label>");
                        }
                        try
                        {
                            html.Append("<div class='col-md-3' style='display:normal'><select id='roomType" + (rno + 1) + "' class='form-control input-sm'>");
                            html.Append("<option value='0'>---Select One---</option>");
                            if (Convert.ToString(dsData.Tables[1].Rows[rno]["Room_Type"]) == "Single Bed Occupancy")
                            {
                                html.Append("<option value='Single Bed Occupancy' Selected='true'>Single Bed Occupancy</option>");
                            }
                            else
                            {
                                html.Append("<option value='Single Bed Occupancy'>Single Bed Occupancy</option>");
                            }
                            if (Convert.ToString(dsData.Tables[1].Rows[rno]["Room_Type"]) == "Double Bed Occupancy")
                            {
                                html.Append("<option value='Single Bed Occupancy' Selected='true'>Double Bed Occupancy</option>");
                            }
                            else
                            {
                                html.Append("<option value='Single Bed Occupancy'>Double Bed Occupancy</option>");
                            }
                            html.Append("</select></div></div>");
                        }
                        catch {
                            html.Append("<div class='col-md-3' style='display:normal'><select id='roomType" + (rno + 1) + "' class='form-control input-sm'>");
                            html.Append("<option value='0'>---Select One---</option>");
                            html.Append("<option value='Single Bed Occupancy'>Single Bed Occupancy</option>");
                            html.Append("<option value='Single Bed Occupancy'>Double Bed Occupancy</option>");
                            html.Append("</select></div></div>");
                        }
                        try
                        {
                            html.Append("<div class='form-group' id='div_HotelContact" + (rno + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Hotel Name</label><div class='col-md-3'><input id='hotel_name" + (rno + 1) + "' class='form-control input-sm' type='text' value='" + Convert.ToString(dsData.Tables[1].Rows[rno]["hotel_name"]) + "'></div>");
                            html.Append("<div class='col-md-1'></div><label class='col-md-2'>Hotel Contact No</label><div class='col-md-3'><input id='hotel_no" + (rno + 1) + "' class='form-control input-sm' type='text' value='" + Convert.ToString(dsData.Tables[1].Rows[rno]["hotel_no"]) + "'></div></div>");
                        }
                        catch {
                            html.Append("<div class='form-group' id='div_HotelContact" + (rno + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Hotel Name</label><div class='col-md-3'><input id='hotel_name" + (rno + 1) + "' class='form-control input-sm' type='text' value=''></div>");
                            html.Append("<div class='col-md-1'></div><label class='col-md-2'>Hotel Contact No</label><div class='col-md-3'><input id='hotel_no" + (rno + 1) + "' class='form-control input-sm' type='text' value=''></div></div>");
                        }
                        

                        //==========================================================================================================================================================

                        html.Append("<div id='exp_data" + (rno + 1) + "'>");
                        if (dsData.Tables[2].Rows.Count > 0)
                        {
                            html.Append("<div class='form-group'></div>");
                            html.Append("<div class='col-md-12'>");
                            html.Append("<div class='col-md-1'></div>");
                            html.Append("<div class='col-md-10'>");
                            html.Append("<table class='table table-bordered'>");
                            html.Append("<thead><tr class='grey'>");
                            html.Append("<th>Expense Head</th><th>Reimbursement Type</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Supporting Doc</th><th>Supporting Particulars</th><th>Remark</th>");
                            html.Append("</tr></thead>");
                            html.Append("<tbody>");

                            string trdate = "";
                            try
                            {
                                trdate = Convert.ToDateTime(dsData.Tables[1].Rows[rno]["travel_date"]).ToString("dd-MMM-yyyy");
                            }
                            catch
                            {
                                trdate = Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy");
                            }

                            string val = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "gettravelclass", ref is_data, Convert.ToString(dsData.Tables[1].Rows[rno]["to_city"]));
                            DataTable ejm = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getexpenseids", ref is_data, Convert.ToString(dsData.Tables[1].Rows[rno]["journey_type"]));

                            int counter = 0;
                            for (int j = 0; j < dsData.Tables[2].Rows.Count; j++)
                            {
                                string trdate1 = Convert.ToDateTime(dsData.Tables[2].Rows[j]["fk_travel_date"]).ToString("dd-MMM-yyyy");

                                if (trdate == trdate1 && counter < ejm.Rows.Count)
                                {
                                    StringBuilder reim = new StringBuilder();
                                    int rech = 0;
                                    DataTable rt = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getreimbursements", ref is_data, "Domestic Travel Expenses");
                                    if (rt != null)
                                    {
                                        for (int i = 0; i < rt.Rows.Count; i++)
                                        {
                                            if (Convert.ToString(dsData.Tables[2].Rows[j]["reim_type"]) == Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]))
                                            {
                                                reim.Append("<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "' Selected='true'>" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>");
                                                rech = rech + 1;
                                            }
                                            else
                                            {
                                                if (Convert.ToString(rt.Rows[i]["is_chargable"]) == "1")
                                                {
                                                    if (rech == 0)
                                                    {
                                                        reim.Append("<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "' Selected='true'>" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>");
                                                    }
                                                    else
                                                    {
                                                        reim.Append("<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "'>" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>");
                                                    }
                                                    rech = rech + 1;
                                                }
                                                else
                                                {
                                                    reim.Append("<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "'>" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>");
                                                }
                                            }
                                        }
                                    }

                                    html.Append("<tr>");

                                    if (Convert.ToString(dsData.Tables[1].Rows[rno]["journey_type"]) == "3")
                                    {
                                        val = "A";
                                    }
                                    string cnt = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getjmcount", ref is_data, Convert.ToString(dsData.Tables[1].Rows[rno]["journey_type"]), ejm.Rows[counter]["FK_EXPENSE_HEAD"]);
                                    string value = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getpolicyamount", ref is_data, desg, Convert.ToString(dsData.Tables[1].Rows[rno]["journey_type"]), ejm.Rows[counter]["FK_EXPENSE_HEAD"], Convert.ToString(dsData.Tables[1].Rows[rno]["travel_mode"]), Convert.ToString(dsData.Tables[1].Rows[rno]["travel_class"]), Convert.ToString(dsData.Tables[1].Rows[rno]["plant_from"]), Convert.ToString(dsData.Tables[1].Rows[rno]["plant_to"]), val,division);
                                    string perc = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getclasswiseperc", ref is_data, val);
                                    string ival = "0";
                                    string oval = "0";
                                    //if (value != "")
                                    //{
                                    //    oval = value;
                                    //    value = Convert.ToString(Convert.ToInt32(value) * Convert.ToInt32(perc) / 100);
                                    //    ival = value;
                                    //}
                                    if (value == null || value == "")
                                    {
                                        oval = ival = "0";
                                    }
                                    else
                                    {
                                        oval = ival = value;
                                    }


                                    html.Append("<td><label>" + ejm.Rows[counter]["EXPENSE_HEAD"] + "</label>(<span id='IS_SUP" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "'>" + Convert.ToString(ejm.Rows[counter]["IS_SUPPORTING"]) + "</span>)");
                                    html.Append("<input type='text' id='compare_id" + (rno + 1) + "" + (counter + 1) + "' value='" + ejm.Rows[counter]["COMPARE_ID"] + "' style='color:black; display:none'><input type='text' id='compare_name" + (rno + 1) + "" + (counter + 1) + "' value='" + Convert.ToString(ejm.Rows[counter]["COMP_NAME"]).Replace(" ", "_") + "' style='color:black; display:none'><input type='text' name='eh" + (rno + 1) + "' id='expenses" + (rno + 1) + "" + (counter + 1) + "' value='" + ejm.Rows[counter]["FK_EXPENSE_HEAD"] + "' style='color:black; display:none'><input type='text' id='exp_name" + (rno + 1) + "" + (counter + 1) + "' value='" + ejm.Rows[counter]["EXPENSE_HEAD"] + "' style='color:black; display:none'><input type='text' id='e_name" + (rno + 1) + "" + (counter + 1) + "' value='" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "' style='color:black; display:none'><input type='text' id='e_fk_id" + (rno + 1) + "" + (counter + 1) + "' value='" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "' style='color:black; display:none'>");
                                    html.Append("</td>");
                                    //html.Append("<td>" + ejm.Rows[counter]["SAP_GLCode"] + "</td>");
                                    html.Append("<td>");

                                    if (Convert.ToString(ejm.Rows[counter]["IS_ALLOW"]) == "1" && cnt != "0")
                                    {
                                        html.Append("<select ID='ddlrem_" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' class='form-control input-sm' onchange='change_flag(" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "," + (rno + 1) + "," + (counter + 1) + ");'>");
                                    }
                                    else
                                    {
                                        html.Append("<select ID='ddlrem_" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' class='form-control input-sm' onchange='change_flag(" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "," + (rno + 1) + "," + (counter + 1) + ");' style='display:none'>");
                                    }
                                    html.Append("<option Value='0'>---Select One---</option>");
                                    html.Append(reim);
                                    html.Append("</select>");
                                    if (rech == 0)
                                    {
                                        html.Append("<input type='text' id='reim_val" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='0' style='display:none' />");
                                    }
                                    else
                                    {
                                        html.Append("<input type='text' id='reim_val" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='1' style='display:none' />");
                                    }
                                    html.Append("</td>");
                                    html.Append("<td style='text-align:right;'>");
                                    string amt = Convert.ToString(dsData.Tables[2].Rows[j]["amount"]);
                                    if (amt == "0")
                                    {
                                        amt = "";
                                    }
                                    if (Convert.ToString(ejm.Rows[counter]["IS_ALLOW"]) == "1" && cnt != "0")
                                    {
                                        if (Convert.ToString(dsData.Tables[1].Rows[rno]["JOURNEY_TYPE"]) != "3" && Convert.ToString(dsData.Tables[1].Rows[rno]["JOURNEY_TYPE"]) != "2")
                                        {
                                            html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + Convert.ToString(ejm.Rows[counter]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + amt + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                                        }
                                        else
                                        {
                                            html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + Convert.ToString(ejm.Rows[counter]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + amt + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                                            //if (Convert.ToString(dsData.Tables[1].Rows[rno]["beyond_time"]) == "1" || Convert.ToString(dsData.Tables[1].Rows[rno]["Stay_Guest_House"]) == "1")
                                            //{
                                            //    html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + Convert.ToString(ejm.Rows[counter]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + amt + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                                            //}
                                            //else
                                            //{
                                            //    html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + Convert.ToString(ejm.Rows[counter]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + amt + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='' class='form-control input-sm' style='text-align:right; display:none'>");
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + Convert.ToString(ejm.Rows[counter]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + amt + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' readonly><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[counter]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='" + oval + "' class='form-control input-sm' style='text-align:right; display:none'>");
                                        //html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><span id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' style='text-align:right; width:70px; padding-right:20px' readonly>" + ival + "</span><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                                    }
                                    html.Append("</td>");
                                    string rrem = Convert.ToString(dsData.Tables[2].Rows[j]["supp_remark"]);
                                    if (rrem == "--" || rrem == "undefined")
                                    {
                                        rrem = "";
                                    }
                                    if (Convert.ToString(dsData.Tables[2].Rows[j]["IS_SUPPORTING"]) == "Y" && Convert.ToString(dsData.Tables[2].Rows[j]["support_doc"]) == "Y")
                                    {
                                        html.Append("<td>");
                                        html.Append("<select ID='ddl_SUP" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' class='form-control input-sm' onchange='enable_disable_field(" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]) + "," + (rno + 1) + "," + (counter + 1) + ")'>");
                                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N'>No</option></select>");
                                        html.Append("</td>");
                                        html.Append("<td>");
                                        html.Append("<input type='text' ID='particular_SUP" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' class='form-control input-sm' value='" + rrem + "'/><font ID='f_other" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' style='color:red'>*</font>");
                                        html.Append("</td>");
                                    }
                                    else if (Convert.ToString(dsData.Tables[2].Rows[j]["IS_SUPPORTING"]) == "Y" && Convert.ToString(dsData.Tables[2].Rows[j]["support_doc"]) == "N")
                                    {
                                        html.Append("<td>");
                                        html.Append("<select ID='ddl_SUP" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' class='form-control input-sm' onchange='enable_disable_field(" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]) + "," + (rno + 1) + "," + (counter + 1) + ")'>");
                                        html.Append("<option Value='Y'>Yes</option><option Value='N' Selected='true'>No</option></select>");
                                        html.Append("</td>");
                                        html.Append("<td>");
                                        html.Append("<input type='text' ID='particular_SUP" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' class='form-control input-sm' value='' style='display:none'/><font ID='f_other" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' style='display:none; color:red'>*</font>");
                                        html.Append("</td>");
                                    }
                                    else
                                    {
                                        html.Append("<td>");
                                        html.Append("<select ID='ddl_SUP" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' class='form-control input-sm' style='display:none'>");
                                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N' Selected='true'>No</option></select>");
                                        html.Append("</td>");
                                        html.Append("<td>");
                                        html.Append("<input type='text' ID='particular_SUP" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' class='form-control input-sm' style='display:none'/><font ID='f_other" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' style='display:none; color:red'>*</font>");
                                        html.Append("</td>");
                                    }

                                    string other_remark = Convert.ToString(ejm.Rows[counter]["IS_OTHER"]);
                                    string o_rem = Convert.ToString(dsData.Tables[2].Rows[j]["other_remark"]);
                                    if (other_remark == "0")
                                    {
                                        html.Append("<td><input type='text' ID='other_remark" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' class='form-control input-sm' value='NA' style='display:none'/><input type='text' ID='fk_other_remark" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='0' class='form-control input-sm' style='display:none'/></td>");
                                    }
                                    else
                                    {
                                        html.Append("<td><input type='text' ID='other_remark" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' class='form-control input-sm' value='" + o_rem + "'/><input type='text' ID='fk_other_remark" + Convert.ToString(ejm.Rows[counter]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + (rno + 1) + "_" + (counter + 1) + "' value='1' class='form-control input-sm' style='display:none'/></td>");
                                    }
                                    html.Append("</tr>");
                                    counter = counter + 1;
                                }
                            }

                            html.Append("</tbody></table>");
                            html.Append("</div>");
                            html.Append("<div class='col-md-1'></div>");
                            html.Append("</div>");
                            html.Append("<div class='form-group'></div>");

                        }

                        html.Append("</div>");

                        //==========================================================================================================================================================

                        html.Append("</div></div></div></div></div>");

                        fdate = fdate.AddDays(1);
                        }
                    catch(Exception ex){}
                    }
                
                }
                else
                {
                    int rno = 1;
                    DateTime fd = fdate;
                    DateTime td = tdate;
                    while (fdate <= tdate)
                    {

                        html.Append("<div class='panel' id='remove_" + rno + "'>");
                        html.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
                        html.Append("<div class='panel-heading-btn'><div>Amount : <span id='Total" + rno + "'>0</span> <i class='fa fa-rupee'></i></div></div>");
                        html.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' onclick='copyData(" + rno + ")' href='#collapse" + rno + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spn_date_" + rno + "'>" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + "</span></a></h3></div>");

                        if (rno == 1)
                        {
                            html.Append("<div id='collapse" + rno + "' class='panel-collapse collapse in'><div class='panel-body' style='background-color:#f0f5f5'><div class='form-horizontal'>");
                        }
                        else
                        {
                            html.Append("<div id='collapse" + rno + "' class='panel-collapse collapse'><div class='panel-body' style='background-color:#f0f5f5'><div class='form-horizontal'>");
                        }

                        html.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Journey Type</label>");
                        html.Append("<div class='col-md-3'>");
                        if (dtJourney.Tables[0] != null)
                        {
                            html.Append("<select ID='journey_Type" + rno + "' runat='server' name='jt' class='form-control input-sm' onchange='check_journey_Type(" + rno + "," + dtJourney.Tables[0].Rows.Count + ");'>");
                            html.Append("<option Value='0'>---Select One---</option>");

                            //for (int i = 0; i < dtJourney.Tables[0].Rows.Count; i++)
                            //{
                            //    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                            //}
                            if (fd == td)
                            {
                                for (int i = 0; i < dtJourney.Tables[0].Rows.Count; i++)
                                {
                                    if (Convert.ToString(dtJourney.Tables[0].Rows[i]["ONE_DAY"]) == "1")
                                    {
                                        html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < dtJourney.Tables[0].Rows.Count; i++)
                                {
                                    if (Convert.ToString(dtJourney.Tables[0].Rows[i]["ONE_DAY"]) == "0")
                                    {
                                        html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                                    }
                                }
                            }
                            html.Append("</select>");
                        }
                        html.Append("</div>");
                        html.Append("<div class='col-md-1'></div><label class='col-md-2'>Particulars</label><div class='col-md-3'><input id='remark_note" + rno + "' class='form-control input-sm' type='text'></div>");
                        html.Append("</div>");

                        html.Append("<div class='form-group'><div id='div_Travel_Mode" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Travel Mode</label>");
                        html.Append("<div class='col-md-3'>");
                        if (dtJourney.Tables[1] != null)
                        {
                            html.Append("<select ID='Travel_Mode" + rno + "' runat='server' class='form-control input-sm' onchange='get_travel_class(" + rno + ");get_exp_data(" + rno + ");'>");
                            html.Append("<option Value='0'>---Select One---</option>");

                            for (int i = 0; i < dtJourney.Tables[1].Rows.Count; i++)
                            {
                                html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[1].Rows[i]["TRAVEL_NAME"]) + "</option>");
                            }
                            html.Append("</select>");
                        }
                        html.Append("</div></div>");

                        html.Append("<div id='div_Travel_Class" + rno + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-2'>Travel Class</label><div class='col-md-3'>");
                        //html.Append("<select ID='Travel_Class" + rno + "' class='form-control input-sm' onchange='get_exp_data(" + rno + ");'>");
                        html.Append("<select ID='Travel_Class" + rno + "' class='form-control input-sm'>");
                        html.Append("<option Value='0'>---Select One---</option>");
                        html.Append("</select>");
                        html.Append("</div></div></div>");


                        //html.Append("<div class='form-group'><div id='div_FPlant" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Plant From</label><div class='col-md-3'>");
                        html.Append("<div class='form-group'><div id='div_FPlant" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2' id='dest_plant"+rno+"'>Plant From</label><div class='col-md-3'>");
                        html.Append("<select ID='From_Plant" + rno + "' runat='server' class='form-control input-sm' onchange='get_exp_data(" + rno + ");'>");
                        html.Append("<option Value='0'>---Select One---</option>");

                        for (int i = 0; i < dtJourney.Tables[3].Rows.Count; i++)
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                        }
                        html.Append("</select>");
                        html.Append("</div></div>");

                        //html.Append("<div id='div_PM" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + rno + "' onchange='check_on_data(" + rno + ")' />Reached Beyond 10.00 PM?</label></div>");

                        html.Append("<div id='div_TPlant" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Plant To</label><div class='col-md-3'>");
                        html.Append("<select ID='To_Plant" + rno + "' runat='server' class='form-control input-sm' onchange='get_exp_data(" + rno + ");'>");
                        html.Append("<option Value='0'>---Select One---</option>");
                        for (int i = 0; i < dtJourney.Tables[3].Rows.Count; i++)
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                        }
                        html.Append("</select>");
                        html.Append("</div></div></div>");

                        html.Append("<div class='form-group'>");
                        html.Append("<div id='div_PM" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + rno + "' onchange='check_on_data(" + rno + ")' />Reached Beyond 10.00 PM?</label></div>");
                        html.Append("<div id='div_GH" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_guest_" + rno + "' onchange='check_on_guest(" + rno + ")' />Stay at Guest House?</label></div>");
                        html.Append("</div>");

                        html.Append("<div class='form-group' id='div_City" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>From City</label>");
                        html.Append("<div class='col-md-3'>");
                        if (dtJourney.Tables[5] != null)
                        {
                            html.Append("<select ID='From_City" + rno + "' runat='server' class='form-control input-sm' onchange='chk_class_From(" + rno + ")'>");
                            html.Append("<option Value='0'>---Select One---</option>");

                            for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                            {
                                html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                            }
                            html.Append("<option Value='-1'>Other</option>");
                            html.Append("</select><input type='text' class='form-control input-sm' id='txt_f_city" + rno + "' style='display:none'>");
                        }
                        html.Append("</div>");
                        html.Append("<div class='col-md-1'></div><label class='col-md-2'>To City</label><div class='col-md-3'>");
                        html.Append("<select ID='To_City" + rno + "' runat='server' class='form-control input-sm' onchange='chk_class_To(" + rno + ");get_exp_data(" + rno + ");'>");
                        html.Append("<option Value='0'>---Select One---</option>");

                        for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                        }
                        html.Append("<option Value='-1'>Other</option>");
                        html.Append("</select><input type='text' id='cls" + rno + "' value='0' style='display:none'><input type='text' class='form-control input-sm' id='txt_t_city" + rno + "' style='display:none'>");
                        html.Append("</div></div>");

                        html.Append("<div class='form-group' id='div_PlaceRoom" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Place Class</label>");
                        html.Append("<div class='col-md-3'><span id='placeclass" + rno + "'>NA</span> Class</div><div class='col-md-1'></div><label class='col-md-2'>Room Type</label>");
                        html.Append("<div class='col-md-3'><select id='roomType" + rno + "' class='form-control input-sm'><option value='0'>---Select One---</option><option value='Single Bed Occupancy'>Single Bed Occupancy</option><option value='Double Bed Occupancy'>Double Bed Occupancy</option></select></div></div>");

                        html.Append("<div class='form-group' id='div_HotelContact" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Hotel Name</label><div class='col-md-3'><input id='hotel_name" + rno + "' class='form-control input-sm' type='text'></div>");
                        html.Append("<div class='col-md-1'></div><label class='col-md-2'>Hotel Contact No</label><div class='col-md-3'><input id='hotel_no" + rno + "' class='form-control input-sm' type='text'></div></div>");


                        html.Append("<div id='exp_data" + rno + "'></div>");

                        html.Append("</div></div></div></div></div>");

                        rno = rno + 1;
                        fdate = fdate.AddDays(1);
                    }
                
                }
            }
            jdata = Convert.ToString(html);

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return jdata;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_Expense_Data(string desg, int j_val, int travel_mode_id, int travel_class_id, int fplant_id, int tplant_id, string pl_class, int index, int chkbox, string division)
    {
        StringBuilder html = new StringBuilder();
        StringBuilder reim = new StringBuilder();
        int rech = 0;
        string is_data = string.Empty;
        try
        {
            DataTable rt = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getreimbursements", ref is_data, "Domestic Travel Expenses");
            if (rt != null)
            {
                for (int i = 0; i < rt.Rows.Count; i++)
                {
                    if (Convert.ToString(rt.Rows[i]["is_chargable"]) == "1")
                    {
                        if (rech == 0)
                        {
                            reim.Append("<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "' Selected='true'>" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>");
                        }
                        else
                        {
                            reim.Append("<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "'>" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>");
                        }
                        rech = rech + 1;
                    }
                    else
                    {
                        reim.Append("<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "'>" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>");
                    }
                }
            }
            string val = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "gettravelclass", ref is_data, pl_class);
            DataTable ejm = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getexpenseids", ref is_data, j_val);

            html.Append(index + "@@");
            if (ejm != null)
            {
                html.Append("<div class='form-group'></div>");
                html.Append("<div class='col-md-12'>");
                html.Append("<div class='col-md-1'></div>");
                html.Append("<div class='col-md-10'>");
                html.Append("<table class='table table-bordered'>");
                html.Append("<thead><tr class='grey'>");
                html.Append("<th>Expense Head</th><th>Reimbursement Type</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Supporting Doc</th><th>Supporting Particulars</th><th>Remark</th>");
                html.Append("</tr></thead>");
                html.Append("<tbody>");
                for (int i = 0; i < ejm.Rows.Count; i++)
                {

                    html.Append("<tr>");
                    if (j_val == 3)
                    {
                        val = "A";
                    }
                    string cnt = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getjmcount", ref is_data, j_val, ejm.Rows[i]["FK_EXPENSE_HEAD"]);
                    string value = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getpolicyamount", ref is_data, desg, j_val, ejm.Rows[i]["FK_EXPENSE_HEAD"], travel_mode_id, travel_class_id, fplant_id, tplant_id, val,division);
                    string perc = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getclasswiseperc", ref is_data, val);
                    string ival = "0";
                    string oval = "0";
                    //if (value != "")
                    //{
                    //    oval = value;
                    //    value = Convert.ToString(Convert.ToInt32(value) * Convert.ToInt32(perc) / 100);
                    //    ival = value;
                    //}
                    if (value == null || value == "")
                    {
                        oval = ival = "0";
                    }
                    else
                    {
                        oval = ival = value;
                    }

                    
                    html.Append("<td><label>" + ejm.Rows[i]["EXPENSE_HEAD"] + "</label>(<span id='IS_SUP" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "'>" + Convert.ToString(ejm.Rows[i]["IS_SUPPORTING"]) + "</span>)");
                    html.Append("<input type='text' id='compare_id" + index + "" + (i + 1) + "' value='" + ejm.Rows[i]["COMPARE_ID"] + "' style='color:black; display:none'><input type='text' id='compare_name" + index + "" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["COMP_NAME"]).Replace(" ", "_") + "' style='color:black; display:none'><input type='text' name='eh"+index+"' id='expenses" + index + "" + (i + 1) + "' value='" + ejm.Rows[i]["FK_EXPENSE_HEAD"] + "' style='color:black; display:none'><input type='text' id='exp_name" + index + "" + (i + 1) + "' value='" + ejm.Rows[i]["EXPENSE_HEAD"] + "' style='color:black; display:none'><input type='text' id='e_name" + index + "" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "' style='color:black; display:none'><input type='text' id='e_fk_id" + index + "" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "' style='color:black; display:none'>");
                    html.Append("</td>");
                    //html.Append("<td style='text-align:left'>" + ejm.Rows[i]["SAP_GLCode"] + "</td>");
                    html.Append("<td>");
                    if (Convert.ToString(ejm.Rows[i]["IS_ALLOW"]) == "1" && cnt != "0")
                    {
                        html.Append("<select ID='ddlrem_" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' onchange='change_flag(" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "," + index + "," + (i + 1) + ");'>");
                    }
                    else
                    {
                        html.Append("<select ID='ddlrem_" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' onchange='change_flag(" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "," + index + "," + (i + 1) + ");' style='display:none'>");
                    }
                    html.Append("<option Value='0'>---Select One---</option>");
                    html.Append(reim);
                    html.Append("</select>");
                    if (rech == 0)
                    {
                        html.Append("<input type='text' id='reim_val" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='0' style='display:none' />");
                    }
                    else
                    {
                        html.Append("<input type='text' id='reim_val" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='1' style='display:none' />");
                    }
                    html.Append("</td>");
                    html.Append("<td style='text-align:right;'>");

                    if (Convert.ToString(ejm.Rows[i]["IS_ALLOW"]) == "1" && cnt != "0")
                    {
                        //if (chkbox == 1)
                        //{
                        //    html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='0' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                        //}
                        //else
                        //{
                        //    html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='0' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='' class='form-control input-sm' style='text-align:right; display:none'>");
                        //}

                        if (chkbox == 0 && j_val != 1)
                        {
                            html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='0' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='' class='form-control input-sm' style='text-align:right; display:none'>");
                        }
                        else
                        {
                            html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='0' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                        }
                    }
                    else
                    {
                        html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + oval + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' readonly><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + oval + "' class='form-control input-sm' style='text-align:right; display:none'>");
                        //html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + ival + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' readonly><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                    }
                    html.Append("</td>");

                    if (Convert.ToString(ejm.Rows[i]["IS_SUPPORTING"]) == "Y")
                    {
                        html.Append("<td>");
                        html.Append("<select ID='ddl_SUP" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' onchange='enable_disable_field(" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]) + "," + index + "," + (i + 1) + ")'>");
                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N'>No</option></select>");
                        html.Append("</td>");
                        html.Append("<td>");
                        html.Append("<input type='text' ID='particular_SUP" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm'/><font ID='f_other" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' style='color:red'>*</font>");
                        html.Append("</td>");
                    }
                    else
                    {
                        html.Append("<td>");
                        html.Append("<select ID='ddl_SUP" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' style='display:none'>");
                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N' Selected='true'>No</option></select>");
                        html.Append("</td>");
                        html.Append("<td>");
                        html.Append("<input type='text' ID='particular_SUP" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' style='display:none'/><font ID='f_other" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' style='display:none; color:red'>*</font>");
                        html.Append("</td>");
                    }
                    string other_remark = Convert.ToString(ejm.Rows[i]["IS_OTHER"]);
                    if (other_remark == "0")
                    {
                        html.Append("<td><input type='text' ID='other_remark" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' style='display:none' value=''/><input type='text' ID='fk_other_remark" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='0' class='form-control input-sm' style='display:none'/></td>");
                    }
                    else
                    {
                        html.Append("<td><input type='text' ID='other_remark" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' value='' placeholder='Please Enter Appropriate Remark'/><input type='text' ID='fk_other_remark" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='1' class='form-control input-sm' style='display:none'/></td>");
                    }
                    html.Append("</tr>");
                   
                }
                html.Append("</tbody></table>");
                html.Append("</div>");
                html.Append("<div class='col-md-1'></div>");
                html.Append("</div>");
                html.Append("<div class='form-group'></div>");
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return html.ToString();

    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_dev_policy(string desg, int j_val, int travel_mode_id, int travel_class_id)
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
		        divIns.Style.Add("display","none");
                string isdata = string.Empty;
                DateTime fdate = Convert.ToDateTime(travelFromDate.Text);
                DateTime tdate = Convert.ToDateTime(travelToDate.Text);
                string xml_string = txt_xml_data.Text;
                xml_string = xml_string.Replace("&", "&amp;");
                xml_string = xml_string.Replace("'", "&apos;");
                txt_xml_data.Text = xml_string.ToString();

                string file_attach = txt_Document_Xml.Text;
                file_attach = file_attach.Replace("&", "&amp;");
                file_attach = file_attach.Replace("'", "&apos;");
                txt_Document_Xml.Text = file_attach.ToString();

                string sub_xml = txt_sub_xml_data.Text;
                sub_xml = sub_xml.Replace("&", "&amp;");
                sub_xml = sub_xml.Replace("'", "&apos;");
                txt_sub_xml_data.Text = sub_xml.ToString();

                string adv_loc = "0";
                if (ddl_Payment_Mode.SelectedItem.Text.ToUpper().Trim() == "CASH")
                {
                    adv_loc = ddlAdv_Location.SelectedValue;
                }
                //if (Convert.ToInt32(empno.InnerHtml) != 4263 && Convert.ToInt32(empno.InnerHtml) != 4262)
                if (txt_Username.Text.ToUpper() != "PRRATHI" && txt_Username.Text.ToUpper() != "RBRATHI")
                {
                    isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request_Modification.aspx", "update", ref isdata, txt_pk_id.Text, txt_Username.Text, fdate, tdate, xml_string, file_attach, txt_sub_xml_data.Text, txtProcessID.Text, txtInstanceID.Text, adv_loc, ddl_Payment_Mode.SelectedValue, req_remark.Text, dev_travel_class.Text, dev_policy_amt.Text, dev_supp_amt.Text, txt_advance_id.Text);
                    if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = isdata.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = span_Approver.InnerHtml;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "178", "TRAVEL EXPENSE MODIFICATION", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
                                string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE MODIFICATION", "USER", txt_Username.Text, "SUBMIT", "OK", "0", "0");
                                string emailid = string.Empty;
                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Domestic Travel Expense Request Has Been Modified and Sent To Approver.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE MODIFICATION", "SUBMIT", txt_Approver_Email.Text, txt_init_mail.Text, msg, "Request No: " + spn_req_no.InnerHtml);

                                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getfiles", ref isdata, "TRAVEL EXPENSE", isSaved.ToString());
                                if (dt.Rows.Count > 0)
                                {
                                    string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                    string path = string.Empty;

                                    string foldername = spn_req_no.InnerHtml.ToString();
                                    //foldername = foldername.Replace("-", "_");
                                    path = activeDir + "\\" + foldername;
                                    if (Directory.Exists(path))
                                    {

                                    }
                                    else
                                    {
                                        Directory.CreateDirectory(path);
                                        string[] directories = Directory.GetFiles(activeDir);

                                        path = path + "\\";
                                        foreach (var directory in directories)
                                        {
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                var sections = directory.Split('\\');
                                                var fileName = sections[sections.Length - 1];
                                                if (dt.Rows[i]["FILENAME"].ToString() == fileName)
                                                {
                                                    System.IO.File.Move(activeDir + "\\" + fileName, path + fileName);
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                // throw;
                                FSL.Logging.Logger.WriteEventLog(false, ex);
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Modified Successfully and Sent To Approver...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                    }
                }
                else
                { 
                
                    /*******************************************************************************************************************/

                    DataTable DTAP = new DataTable();
                    if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                    {
                        DTAP = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "getaccapprover", ref isdata, "DOMESTIC TRAVEL DOC VERIFIER", ddlAdv_Location.SelectedValue, ddl_Payment_Mode.SelectedValue);
                    }
                    else
                    {
                        DTAP = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "getaccapprover", ref isdata, "DOMESTIC TRAVEL DOC VERIFIER", 0, ddl_Payment_Mode.SelectedValue);
                    }
                    if (DTAP != null)
                    {
                        if (DTAP.Rows.Count > 0)
                        {
                            isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request_Modification.aspx", "update", ref isdata, txt_pk_id.Text, txt_Username.Text, fdate, tdate, xml_string, file_attach, txt_sub_xml_data.Text, txtProcessID.Text, txtInstanceID.Text, adv_loc, ddl_Payment_Mode.SelectedValue, req_remark.Text, dev_travel_class.Text, dev_policy_amt.Text, dev_supp_amt.Text, txt_advance_id.Text);
                            if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = isdata.Split(':');
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                            }
                            else
                            {
                                string[] Dval = new string[1];
                                Dval[0] = span_Approver.InnerHtml;
                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "178", "TRAVEL EXPENSE MODIFICATION", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                                if (isCreate)
                                {
                                    try
                                    {
                                        string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE MODIFICATION", "USER", txt_Username.Text, "SUBMIT", "OK", "0", "0");
                                        /***********/
                                        string wiid1 = (string)ActionController.ExecuteAction(txt_Username.Text, "Bulk_Travel_Expense_Doc_Verification.aspx", "getpktransid", ref isInserted, txtProcessID.Text, txtInstanceID.Text);
                                        if (dev_policy_amt.Text != "1" && dev_supp_amt.Text != "1" && dev_travel_class.Text != "1")
                                        {
                                            string[] Dval1 = new string[DTAP.Rows.Count];
                                            if (DTAP.Rows.Count > 0)
                                            {
                                                for (int i = 0; i < DTAP.Rows.Count; i++)
                                                {
                                                    Dval1[i] = Convert.ToString(DTAP.Rows[i]["USER_ADID"]);
                                                    txtApproverEmail.Text = "";
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
                                            isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "172", "TRAVEL EXPENSE APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, spn_req_no.InnerHtml, wiid1, ref isInserted);
                                            if (isCreate)
                                            {
                                                try
                                                {
                                                    auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE APPROVAL", "USER", txt_Username.Text, "SUBMIT", "Send For Document Approval", "0", "0");
                                                    //string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Domestic Travel Request Has Been Approved Succefully and Sent For Document Verification.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                    string msg = "";
                                                    CryptoGraphy crypt = new CryptoGraphy();
                                                    string process_name = crypt.Encryptdata("TRAVEL EXPENSE");
                                                    string req_no = crypt.Encryptdata(spn_req_no.InnerHtml);
                                                    if (ddl_Payment_Mode.Text.ToUpper() == "CASH")
                                                    {
                                                        msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Domestic Travel Request Has Been Approved Succefully and Sent For Document Verification.</span></pre><pre><span style='font-size: medium;'>Request No: " + spn_req_no.InnerHtml + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://"+compname+"/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                    }
                                                    else
                                                    {
                                                        msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Domestic Travel Request Has Been Approved Succefully and Sent For Document Verification.</span></pre><pre><span style='font-size: medium;'>Request No: " + spn_req_no.InnerHtml + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://"+compname+"/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                    }

                                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE APPROVAL", "SUBMIT", txtEmailID.Text, txtApproverEmail.Text, msg, "Request No: " + spn_req_no.InnerHtml);

                                                    DataTable dt = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getfiles", ref isdata, "TRAVEL EXPENSE", spn_req_no.InnerHtml);
                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                                        string path = string.Empty;

                                                        string foldername = spn_req_no.InnerHtml.ToString();
                                                        //foldername = foldername.Replace("-", "_");
                                                        path = activeDir + "\\" + foldername;
                                                        if (Directory.Exists(path))
                                                        {

                                                        }
                                                        else
                                                        {
                                                            Directory.CreateDirectory(path);
                                                            string[] directories = Directory.GetFiles(activeDir);

                                                            path = path + "\\";
                                                            foreach (var directory in directories)
                                                            {
                                                                for (int i = 0; i < dt.Rows.Count; i++)
                                                                {
                                                                    var sections = directory.Split('\\');
                                                                    var fileName = sections[sections.Length - 1];
                                                                    if (dt.Rows[i]["FILENAME"].ToString() == fileName)
                                                                    {
                                                                        System.IO.File.Move(activeDir + "\\" + fileName, path + fileName);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    throw;
                                                }
                                                finally
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Has Been Modified Successfully and Sent For Document Verification...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            /*************************************************/

                                            string[] Dval1 = new string[1];
                                            Dval1[0] = txt_Username.Text;
                                            //txt_Approver_Email.Text = txtEmailID.Text;
                                            isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "172", "TRAVEL EXPENSE APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, spn_req_no.InnerHtml, wiid1, ref isInserted);
                                            if (isCreate)
                                            {
                                                try
                                                {
                                                    auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE APPROVAL", "USER", txt_Username.Text, "SUBMIT", "Send For Document Approval", "0", "0");
                                                    string wiid2 = (string)ActionController.ExecuteAction(txt_Username.Text, "Bulk_Travel_Expense_Doc_Verification.aspx", "getpktransid", ref isInserted, txtProcessID.Text, txtInstanceID.Text);
                                                    string[] Dval2 = new string[DTAP.Rows.Count];
                                                    if (DTAP.Rows.Count > 0)
                                                    {
                                                        for (int i = 0; i < DTAP.Rows.Count; i++)
                                                        {
                                                            Dval2[i] = Convert.ToString(DTAP.Rows[i]["USER_ADID"]);
                                                            txtApproverEmail.Text = "";
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
                                                    isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "175", "TRAVEL EXPENSE DEVIATION APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval2, spn_req_no.InnerHtml, wiid2, ref isInserted);
                                                    if (isCreate)
                                                    {

                                                        string msg = "";
                                                        CryptoGraphy crypt = new CryptoGraphy();
                                                        string process_name = crypt.Encryptdata("TRAVEL EXPENSE");
                                                        string req_no = crypt.Encryptdata(spn_req_no.InnerHtml);
                                                        if (ddl_Payment_Mode.Text.ToUpper() == "CASH")
                                                        {
                                                            msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Domestic Travel Request Has Been Modified Succefully and Sent For Document Verification.</span></pre><pre><span style='font-size: medium;'>Request No: " + spn_req_no.InnerHtml + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://"+compname+"/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                        }
                                                        else
                                                        {
                                                            msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Domestic Travel Request Has Been Modified Succefully and Sent For Document Verification.</span></pre><pre><span style='font-size: medium;'>Request No: " + spn_req_no.InnerHtml + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://"+compname+"/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                        }
                                                        auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE DEVIATION APPROVAL", "USER", txt_Username.Text, "SUBMIT", "Send For Document Approval", "0", "0");
                                                        string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE APPROVAL", "SUBMIT", txtEmailID.Text, txtApproverEmail.Text, msg, "Request No: " + spn_req_no.InnerHtml);

                                                        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getfiles", ref isdata, "TRAVEL EXPENSE", spn_req_no.InnerHtml);
                                                        if (dt.Rows.Count > 0)
                                                        {
                                                            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                                            string path = string.Empty;

                                                            string foldername = spn_req_no.InnerHtml.ToString();
                                                            //foldername = foldername.Replace("-", "_");
                                                            path = activeDir + "\\" + foldername;
                                                            if (Directory.Exists(path))
                                                            {

                                                            }
                                                            else
                                                            {
                                                                Directory.CreateDirectory(path);
                                                                string[] directories = Directory.GetFiles(activeDir);

                                                                path = path + "\\";
                                                                foreach (var directory in directories)
                                                                {
                                                                    for (int i = 0; i < dt.Rows.Count; i++)
                                                                    {
                                                                        var sections = directory.Split('\\');
                                                                        var fileName = sections[sections.Length - 1];
                                                                        if (dt.Rows[i]["FILENAME"].ToString() == fileName)
                                                                        {
                                                                            System.IO.File.Move(activeDir + "\\" + fileName, path + fileName);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }

                                                }
                                                catch (Exception)
                                                {
                                                    throw;
                                                }
                                                finally
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Has Been Modified Successfully and Sent For Document Verification...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }

                                            /*************************************************/
                                        }
                                        /***********/
                                    }
                                    catch (Exception ex)
                                    {
                                        // throw;
                                        FSL.Logging.Logger.WriteEventLog(false, ex);
                                    }
                                    finally
                                    {
                                        //divIns.InnerHtml = "";
                                        //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Modified Successfully and Request No. is : " + spn_req_no.InnerHtml + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                    }
                                }

                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Document Verifier Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Document Verifier Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                    }

                    /*******************************************************************************************************************/
                
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;

            path = activeDir + "\\" + spn_req_no.InnerHtml + "\\";

            if (Directory.Exists(path))
            {

            }
            else
            {
                Directory.CreateDirectory(path);
                string[] directories = Directory.GetFiles(activeDir);

                path = path + "\\";
                foreach (var directory in directories)
                {
                        var sections = directory.Split('\\');
                        var fileName = sections[sections.Length - 1];
                        System.IO.File.Move(activeDir + "\\" + fileName, path + fileName);
                }
            }
            string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());
            filename = filename.Replace("(", "");
            filename = filename.Replace(")", "");
            filename = filename.Replace("&", "");
            filename = filename.Replace("+", "");
            filename = filename.Replace("/", "");
            filename = filename.Replace("\\", "");
            filename = filename.Replace("'", "");
            filename = filename.Replace("  ", "");
            filename = filename.Replace(" ", "");
            filename = filename.Replace("#", "");
            filename = filename.Replace("$", "");
            filename = filename.Replace("~", "");
            filename = filename.Replace("%", "");
            filename = filename.Replace("''", "");
            filename = filename.Replace(":", "");
            filename = filename.Replace("*", "");
            filename = filename.Replace("?", "");
            filename = filename.Replace("<", "");
            filename = filename.Replace(">", "");
            filename = filename.Replace("{", "");
            filename = filename.Replace("}", "");
            filename = filename.Replace(",", "");

            FileUpload1.SaveAs(path + filename);

        }
        catch (Exception Ex)
        {
            Logger.WriteEventLog(false, Ex);

        }
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
    }

    protected void btnSave_Click(object sender, EventArgs e)
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
                div_Load.Style.Add("display", "none");
                string isdata = string.Empty;
                DateTime fdate = Convert.ToDateTime(travelFromDate.Text);
                DateTime tdate = Convert.ToDateTime(travelToDate.Text);
                string xml_string = txt_xml_data.Text;
                xml_string = xml_string.Replace("&", "&amp;");
                xml_string = xml_string.Replace("'", "&apos;");
                txt_xml_data.Text = xml_string.ToString();

                string file_attach = txt_Document_Xml.Text;
                file_attach = file_attach.Replace("&", "&amp;");
                file_attach = file_attach.Replace("'", "&apos;");
                txt_Document_Xml.Text = file_attach.ToString();

                string sub_xml = txt_sub_xml_data.Text;
                sub_xml = sub_xml.Replace("&", "&amp;");
                sub_xml = sub_xml.Replace("'", "&apos;");
                txt_sub_xml_data.Text = sub_xml.ToString();

                string adv_loc = "0";
                if (ddl_Payment_Mode.SelectedItem.Text.ToUpper().Trim() == "CASH")
                {
                    adv_loc = ddlAdv_Location.SelectedValue;
                }

                //isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request_Modification.aspx", "update", ref isdata, txt_pk_id.Text, txt_Username.Text, fdate, tdate, xml_string, file_attach, txt_sub_xml_data.Text, txtProcessID.Text, txtInstanceID.Text, adv_loc, ddl_Payment_Mode.SelectedValue, req_remark.Text, dev_travel_class.Text, dev_policy_amt.Text, dev_supp_amt.Text);
                isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request_Modification.aspx", "update", ref isdata, txt_pk_id.Text, txt_Username.Text, fdate, tdate, xml_string, file_attach, txt_sub_xml_data.Text, txtProcessID.Text, txtInstanceID.Text, adv_loc, ddl_Payment_Mode.SelectedValue, req_remark.Text, dev_travel_class.Text, dev_policy_amt.Text, dev_supp_amt.Text, txt_advance_id.Text);
                if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                {
                    string[] errmsg = isdata.Split(':');
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                }
                else
                {
                    string[] Dval = new string[1];
                    Dval[0] = txt_Username.Text;
                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "186", "TRAVEL EXPENSE MODIFICATION", "SAVE", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                    if (isCreate)
                    {
                        try
                        {
                            string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE MODIFICATION", "USER", txt_Username.Text, "SAVE", "OK", "0", "0");
                            string emailid = string.Empty;
                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Domestic Travel Request Has Been Successfully Saved In Draft.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                            //emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE MODIFICATION", "SAVE", txtEmailID.Text, "", msg, "");

  DataTable dt = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getfiles", ref isdata, "TRAVEL EXPENSE", isSaved.ToString());
                                if (dt.Rows.Count > 0)
                                {
                                    string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                    string path = string.Empty;

                                    string foldername = isSaved.ToString();
                                    //foldername = foldername.Replace("-", "_");
                                    path = activeDir + "\\" + foldername;
                                    if (Directory.Exists(path))
                                    {

                                    }
                                    else
                                    {
                                        Directory.CreateDirectory(path);
                                        string[] directories = Directory.GetFiles(activeDir);

                                        path = path + "\\";
                                        foreach (var directory in directories)
                                        {
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                var sections = directory.Split('\\');
                                                var fileName = sections[sections.Length - 1];
                                                if (dt.Rows[i]["FILENAME"].ToString() == fileName)
                                                {
                                                    System.IO.File.Move(activeDir + "\\" + fileName, path + fileName);
                                                }
                                            }
                                        }
                                    }
                                }

                        }
                        catch (Exception ex)
                        {
                            // throw;
                            FSL.Logging.Logger.WriteEventLog(false, ex);
                        }
                        finally
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Has Been Successfully Saved In Draft ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string check_Dates(string jfdate, string jtdate,string pk_id)
    {
        string isdata = "";
        string chkData = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "checkdates", ref isdata, Convert.ToString(Session["USER_ADID"]), jfdate, jtdate, 2, pk_id);
        return chkData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string row_data(int rno, DateTime fdate, string jFDate, string jTDate)
    {
        StringBuilder html = new StringBuilder();
        string IsValid = "";
        DataSet dtJourney = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "select", ref IsValid);
        html.Append("<div class='panel' id='remove_" + rno + "'>");
        html.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
        html.Append("<div class='panel-heading-btn'><div>Amount : <span id='Total" + rno + "'>0</span> <i class='fa fa-rupee'></i></div></div>");
        html.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' href='#collapse" + rno + "' onclick='copyData(" + rno + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spn_date_" + rno + "'>" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + "</span></a></h3></div>");

        if (rno == 1)
        {
            html.Append("<div id='collapse" + rno + "' class='panel-collapse collapse in'><div class='panel-body' style='background-color:#f0f5f5'><div class='form-horizontal'>");
        }
        else
        {
            html.Append("<div id='collapse" + rno + "' class='panel-collapse collapse'><div class='panel-body' style='background-color:#f0f5f5'><div class='form-horizontal'>");
        }

        html.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Journey Type</label>");
        html.Append("<div class='col-md-3'>");
        if (dtJourney.Tables[0] != null)
        {
            html.Append("<select ID='journey_Type" + rno + "' runat='server' name='jt' class='form-control input-sm' onchange='check_journey_Type(" + rno + "," + dtJourney.Tables[0].Rows.Count + ");'>");
            html.Append("<option Value='0'>---Select One---</option>");

            if (jFDate == jTDate)
            {
                for (int i = 0; i < dtJourney.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToString(dtJourney.Tables[0].Rows[i]["ONE_DAY"]) == "1")
                    {
                        html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                    }
                }
            }
            else
            {
                for (int i = 0; i < dtJourney.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToString(dtJourney.Tables[0].Rows[i]["ONE_DAY"]) == "0")
                    {
                        html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                    }
                }
            }
            html.Append("</select>");
        }
        html.Append("</div>");
        html.Append("<div class='col-md-1'></div><label class='col-md-2'>Particulars</label><div class='col-md-3'><input id='remark_note" + rno + "' class='form-control input-sm' type='text'></div>");
        html.Append("</div>");

        html.Append("<div class='form-group'><div id='div_Travel_Mode" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Travel Mode</label>");
        html.Append("<div class='col-md-3'>");
        if (dtJourney.Tables[1] != null)
        {
            html.Append("<select ID='Travel_Mode" + rno + "' runat='server' class='form-control input-sm' onchange='get_travel_class(" + rno + ");get_exp_data(" + rno + ");'>");
            html.Append("<option Value='0'>---Select One---</option>");

            for (int i = 0; i < dtJourney.Tables[1].Rows.Count; i++)
            {
                html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[1].Rows[i]["TRAVEL_NAME"]) + "</option>");
            }
            html.Append("<option Value='-1'>Other</option>");
            html.Append("</select>");
        }
        html.Append("</div></div>");

        html.Append("<div id='div_Travel_Class" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Travel Class</label><div class='col-md-3'>");
        //html.Append("<select ID='Travel_Class" + rno + "' runat='server' class='form-control input-sm' onchange='get_exp_data(" + rno + ");'>");
        html.Append("<select ID='Travel_Class" + rno + "' runat='server' class='form-control input-sm'>");
        html.Append("<option Value='0'>---Select One---</option>");
        html.Append("</select>");
        html.Append("</div></div></div>");


        html.Append("<div class='form-group'><div id='div_FPlant" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2' id='dest_plant" + rno + "'>Plant From</label><div class='col-md-3'>");
        html.Append("<select ID='From_Plant" + rno + "' runat='server' class='form-control input-sm' onchange='get_exp_data(" + rno + ");'>");
        html.Append("<option Value='0'>---Select One---</option>");

        for (int i = 0; i < dtJourney.Tables[3].Rows.Count; i++)
        {
            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
        }
        html.Append("</select>");
        html.Append("</div></div>");

        //html.Append("<div id='div_PM" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + rno + "' onchange='check_on_data(" + rno + ")' />Reached Beyond 10.00 PM?</label></div>");
        
        html.Append("<div id='div_TPlant" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Plant To</label><div class='col-md-3'>");
        html.Append("<select ID='To_Plant" + rno + "' runat='server' class='form-control input-sm' onchange='get_exp_data(" + rno + ");'>");
        html.Append("<option Value='0'>---Select One---</option>");
        for (int i = 0; i < dtJourney.Tables[3].Rows.Count; i++)
        {
            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
        }
        html.Append("</select>");
        html.Append("</div></div></div>");

        html.Append("<div class='form-group'>");
        html.Append("<div id='div_PM" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + rno + "' onchange='check_on_data(" + rno + ")' />Reached Beyond 10.00 PM?</label></div>");
        html.Append("<div id='div_GH" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_guest_" + rno + "' onchange='check_on_guest(" + rno + ")' />Stay at Guest House?</label></div>");
        html.Append("</div>");

        html.Append("<div class='form-group' id='div_City" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>From City</label>");
        html.Append("<div class='col-md-3'>");
        if (dtJourney.Tables[5] != null)
        {
            html.Append("<select ID='From_City" + rno + "' runat='server' class='form-control input-sm' onchange='chk_class_From(" + rno + ")'>");
            html.Append("<option Value='0'>---Select One---</option>");

            for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
            {
                html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
            }
            html.Append("<option Value='-1'>Other</option>");
            html.Append("</select><input type='text' class='form-control input-sm' id='txt_f_city" + rno + "' style='display:none'>");
        }
        html.Append("</div>");
        html.Append("<div class='col-md-1'></div><label class='col-md-2'>To City</label><div class='col-md-3'>");
        html.Append("<select ID='To_City" + rno + "' runat='server' class='form-control input-sm' onchange='chk_class_To(" + rno + ");get_exp_data(" + rno + ");'>");
        html.Append("<option Value='0'>---Select One---</option>");

        for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
        {
            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
        }
        html.Append("<option Value='-1'>Other</option>");
        html.Append("</select><input type='text' id='cls" + rno + "' value='0' style='display:none'><input type='text' class='form-control input-sm' id='txt_t_city" + rno + "' style='display:none'>");
        html.Append("</div></div>");

        html.Append("<div class='form-group' id='div_PlaceRoom" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Place Class</label>");
        html.Append("<div class='col-md-3'><span id='placeclass" + rno + "'>NA</span> Class</div><div class='col-md-1'></div><label class='col-md-2' style='display:none'>Room Type</label>");
        html.Append("<div class='col-md-3' style='display:none'><select id='roomType" + rno + "' class='form-control input-sm'><option value='0'>---Select One---</option><option Selected='true' value='Single Bed Occupancy'>Single Bed Occupancy</option><option value='Double Bed Occupancy'>Double Bed Occupancy</option></select></div></div>");

        html.Append("<div class='form-group' id='div_HotelContact" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Hotel Name</label><div class='col-md-3'><input id='hotel_name" + rno + "' class='form-control input-sm' type='text'></div>");
        html.Append("<div class='col-md-1'></div><label class='col-md-2'>Hotel Contact No</label><div class='col-md-3'><input id='hotel_no" + rno + "' class='form-control input-sm' type='text'></div></div>");


        html.Append("<div id='exp_data" + rno + "'></div>");

        html.Append("</div></div></div></div></div>");
        return html.ToString();
    }

}