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

public partial class Foreign_Travel_Request_Modification : System.Web.UI.Page
{
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();

    #region Pageload
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Foreign_Travel_Request_Modification));
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
                            txt_Amount.Text = "0.00";
                        }
                    }
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    travelFromDate.Attributes.Add("readonly", "true");
                    travelToDate.Attributes.Add("readonly", "true");
                    //span_vdate.InnerHtml = DateTime.Now.ToString("dd-MMM-yyyy");
                    getUserInfo();
                    FillDoctype();
                   
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
    private void FillDoctype()
    {
        ListItem Li = new ListItem("--Select One--", "0");
        String IsData = string.Empty;
        DataSet Exp_dat = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getexpenseids", ref IsData);
        if (Exp_dat != null)
        {
            DataTable dt = new DataTable();
            dt = Exp_dat.Tables[3];
            if (dt != null && dt.Rows.Count > 0)
            {
                doctype.DataSource = dt;
                doctype.DataTextField = "EXPENSE_HEAD";
                doctype.DataValueField = "FK_EXPENSE_HEAD";
                doctype.DataBind();
                doctype.Items.Insert(0, Li);
            }
        }
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
                travelFromDate.Text = ofdate.Text = Convert.ToDateTime(dsData.Tables[0].Rows[0]["from_date"]).ToString("dd-MMM-yyyy");
                travelToDate.Text = otdate.Text = Convert.ToDateTime(dsData.Tables[0].Rows[0]["to_date"]).ToString("dd-MMM-yyyy");
                req_remark.Text = Convert.ToString(dsData.Tables[0].Rows[0]["remark"]);
                txt_advance_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["advance_id"]);
                pk_country_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["fk_Country"]);
                exch_rate.Text = Convert.ToString(dsData.Tables[0].Rows[0]["exc_rate"]);
                txt_Return_Money.Value = Convert.ToString(dsData.Tables[0].Rows[0]["RETURN_MONEY"]);

                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "pgettraveluser", ref isdata, txt_Username.Text);
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

                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgettravelrequestapprover", ref isdata, txt_Username.Text);
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

                        }
                        else
                        {
                            span_Approver.InnerHtml = "NA";
                        }
                    }

                    // checkDeviation();

                    fillDocument_Details(dsData.Tables[3]);
                    fillAdvanceAmount();
                    fillCountry();
                    //fillSupporting();
                    fillAuditTrail();
                    Self_Approval();
                    ddlCountry.SelectedValue = pk_country_id.Text;
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void fillCountry()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtCountry = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "selectlocation", ref isdata, "", "", "FTE_COUNTRY", "");
            ddlCountry.Items.Clear();
            if (dtCountry != null)
            {
                if (dtCountry.Rows.Count > 0)
                {
                    ddlCountry.DataSource = dtCountry;
                    ddlCountry.DataTextField = "COUNTRY_NAME";
                    ddlCountry.DataValueField = "PK_COUNTRY_ID";
                    ddlCountry.DataBind();
                }
            }
            ddlCountry.Items.Insert(0, new ListItem("---Select One---", "0"));

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
    protected void Self_Approval()
    {
        try
        {
            string isdata = string.Empty;
            DataTable Self_Dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getincidentallimit", ref isdata, 0, txt_Username.Text, 4, 0);
            if (Self_Dt.Rows.Count > 0 && Self_Dt != null)
            {
                txt_Doc_Verifacation_status.Text = Self_Dt.Rows[0][0].ToString();
            }
            DataTable Class_Dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getincidentallimit", ref isdata, 0, txt_Username.Text, 5, 0);
            if (Class_Dt.Rows.Count > 0 && Class_Dt != null)
            {
                txt_Class.Text = "True";
            }
            else
            {
                txt_Class.Text = "False";
            }
            DataTable Supp_dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getincidentallimit", ref isdata, 0, "", 6, 0);
            if (Supp_dt.Rows.Count > 0 && Supp_dt != null)
            {
                txt_Supporting_Limit.Text = Supp_dt.Rows[0][0].ToString();
            }
            else
            {
                txt_Supporting_Limit.Text = "0";
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

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Type</th><th>File Name</th><th>Delete</th></tr></thead>";
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DisplayData += "<tr>";
                            DisplayData += "<td>" + Convert.ToString(dt.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><input type='text' id='anc_" + (i + 1) + "' value='" + Convert.ToString(dt.Rows[i]["FileName"]) + "' style='display:none'/>  <a href='#' onclick=\"return downloadfiles('" + (Convert.ToString(dt.Rows[i]["FileName"])) + "');\")'>" + Convert.ToString(dt.Rows[i]["FileName"]) + "</td>";
                            DisplayData += "<td><i id='del" + (i + 1) + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (i + 1) + ");\" ></td>";
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
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "pgetadvancedetails", ref isValid, txt_Username.Text, txt_pk_id.Text, 3);
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Currency</th><th>Advance Amount</th><th>Exchange Rate</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            if (dt != null && dt.Tables[0].Rows.Count > 0)
            {
                for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
                {
                    tblHTML.Append("<tr>");
                    if (txt_advance_id.Text == Convert.ToString(dt.Tables[0].Rows[Index]["PK_ADVANCE_F_HDR_ID"]))
                    {
                        tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='travel' onclick='change_radio(" + (Index + 1) + "); calculate_Amount();' checked='true'><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["PK_ADVANCE_F_HDR_ID"]) + "' style='display:none'><input type='text' id='exc_rate" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["EXC_RATE"]) + "' style='display:none'></td>");
                    }
                    else
                    {
                        tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='travel' onclick='change_radio(" + (Index + 1) + "); calculate_Amount();'><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["PK_ADVANCE_F_HDR_ID"]) + "' style='display:none'><input type='text' id='exc_rate" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["EXC_RATE"]) + "' style='display:none'></td>");
                    }
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["ADVANCE_DATE"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["CURRENCY"]) + "<input type='text' id='currency_adv" + (Index + 1) + "' value=" + Convert.ToString(dt.Tables[0].Rows[Index]["CURRENCY"]) + " style='display:none'></td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["CURRENCY_AMOUNT"]) + "<input type='text' id='txt_Currency_Amount" + (Index + 1) + "' value=" + Convert.ToString(dt.Tables[0].Rows[Index]["CURRENCY_AMOUNT"]) + " style='display:none'></td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["EXC_RATE"]) + "<input type='text' id='txt_Forex_card" + (Index + 1) + "' value=" + Convert.ToString(dt.Tables[0].Rows[Index]["FOREX_CARD"]) + " style='display:none'></td>");
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
    #endregion

    #region Ajax

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
    public string getDataRows(string jFDate, string jTDate, int country_id, string of, string ot, string wiid, string Designation, string travel_class, string Ex_Rate, string base_currency)
    {
        StringBuilder sb = new StringBuilder();
        string isdata = string.Empty;
        string row_data = "";
        DateTime fdate = Convert.ToDateTime(jFDate);
        DateTime tdate = Convert.ToDateTime(jTDate);
        DateTime ofdate = Convert.ToDateTime(of);
        DateTime otdate = Convert.ToDateTime(ot);

        try
        {
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "pgetrequestdata", ref isdata, wiid);
            if (dsData != null)
            {
                if (ofdate == fdate && otdate == tdate && dsData.Tables[1].Rows.Count > 0)
                {
                    int row_index = 1;
                    while (fdate <= tdate)
                    {

                        sb.Append("<div class='panel'>");
                        sb.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
                        sb.Append("<div class='panel-heading-btn'><div>Amount(<span id='spn_currency" + row_index + "'>" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]) + "</span>) : <span id='row_Total" + row_index + "'>0</span></div></div>");
                        sb.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' name='rs' href='#collapse" + row_index + "' onclick='copyData(" + row_index + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spn_date" + row_index + "'>" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + "</span></a></h3>");
                        sb.Append("</div>");
                        if (row_index == 1)
                        {
                            sb.Append("<div id='collapse" + row_index + "' class='panel-collapse collapse in'><div class='panel-body' style='background-color:#ffffff'><div class='form-horizontal'>");
                        }
                        else
                        {
                            sb.Append("<div id='collapse" + row_index + "' class='panel-collapse collapse'><div class='panel-body' style='background-color:#ffffff'><div class='form-horizontal'>");
                        }
                        sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>City</label>");
                        sb.Append("<div class='col-md-3'><select id='ddlCity" + row_index + "' runat='server' onchange='getCity(" + row_index + ")' class='form-control input-sm'><option value='0'>---Select One---</option>");
                        DataTable dtCity = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "selectlocation", ref isdata, "", "", "P_GET_CITY", country_id);
                        if (dtCity != null)
                        {
                            if (country_id == 3)
                            {
                                Ex_Rate = Convert.ToString(dtCity.Rows[0]["PK_CITY_ID"]);
                            }
                            if (dtCity.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtCity.Rows.Count; i++)
                                {
                                    if (Convert.ToString(dtCity.Rows[i]["PK_CITY_ID"]) == Convert.ToString(dsData.Tables[1].Rows[row_index - 1]["FK_CITY"]))
                                    {
                                        sb.Append("<option value='" + Convert.ToString(dtCity.Rows[i]["PK_CITY_ID"]) + "' Selected='true'>" + Convert.ToString(dtCity.Rows[i]["NAME"]) + "</option>");
                                    }
                                    else
                                    {
                                        sb.Append("<option value='" + Convert.ToString(dtCity.Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtCity.Rows[i]["NAME"]) + "</option>");
                                    }
                                }
                                sb.Append("<option value='-1' >Other</option><input type='text' id='other_f_city" + row_index + "' class='form-control input-sm' value='' style='display:none' >");
                            }
                            else
                            {
                                sb.Append("<option value='-1' selected>Other</option><input type='text' id='other_f_city" + row_index + "' class='form-control input-sm' value='" + Convert.ToString(dsData.Tables[1].Rows[row_index - 1]["Other_City"]) + "' >");
                            }

                        }
                        sb.Append("</select><input type='text' id='other_f_city" + row_index + "' class='form-control input-sm' style='display:none'></div>");
                        sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Particulars</label>");
                        sb.Append("<div class='col-md-3'><input ID='txt_Particulars" + row_index + "' class='form-control input-sm' runat='server' value='" + Convert.ToString(dsData.Tables[1].Rows[row_index - 1]["remark_note"]) + "'></input></div></div>");

                        sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Hotel Name</label>");
                        sb.Append("<div class='col-md-3'><input class='form-control input-sm' ID='hotel_name" + row_index + "' value='" + Convert.ToString(dsData.Tables[1].Rows[row_index - 1]["hotel_name"]) + "'></input></div>");
                        sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Hotel Contact No</label><div class='col-md-3'><input class='form-control input-sm' ID='contact" + row_index + "'  value='" + Convert.ToString(dsData.Tables[1].Rows[row_index - 1]["hotel_no"]) + "'></input></div></div>");
                        sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Travel Class<font color='#ff0000'><b>*</b></font></label>");
                        sb.Append("<div class='col-md-3'><select id='ddlTravel_class" + row_index + "' runat='server' class='form-control input-sm'><option value='0'>---Select One---</option>");
                        if (Convert.ToString(dsData.Tables[1].Rows[row_index - 1]["TRAVEL_CLASS"]) == "Business Class")
                        {
                            sb.Append("<option value='Business Class' selected>Business Class</option>");
                            sb.Append("<option value='Economy Class'>Economy Class</option>");
                        }
                        else
                        {
                            sb.Append("<option value='Business Class' >Business Class</option>");
                            sb.Append("<option value='Economy Class' selected>Economy Class</option>");
                        }

                        sb.Append("</select></div><div class='col-md-1'></div><label class='col-md-2'>Note<font color='#ff0000'><b>*</b></font></label><div class='col-md-3' style='color:red'>Please fill the Local Travel Details</div>");
                        sb.Append("<div id='exp_data'>");
                        sb.Append(bindExpense(row_index, wiid, country_id, Designation, fdate, Ex_Rate));
                        sb.Append("</div>");

                        if (dsData.Tables[5].Rows.Count > 0)
                        {
                            sb.Append("<div class='col-md-12'>");
                            sb.Append("<div class='col-md-12 grey'>Local Travel Details<span id='span_Local_Amount"+row_index+"' style='display:none'>0</span>");
                            sb.Append("<table class='table table-bordered' id='tbl_Local_Travel" + row_index + "'>");
                            //sb.Append("<thead><tr class='grey'><th>Add</th><th style='display:none'>Delete</th><th>Date</th><th>From</th><th>To</th><th>Mode of Travel</th><th>Expense amount in currency spent</th><th>Tax</th><th>Remark</th><th>Bills attached Yes/No</th></tr></thead>");
                            sb.Append("<thead><tr class='grey'><th>Add</th><th>Delete</th><th style='display:none'>Date</th><th>Reimbursement Type</th><th>From</th><th>To</th><th>Mode of Travel</th><th>Expense amount</th><th>Tax</th><th>Exchange Rate</th><th>Amount(<span id='spn_Detail_Base" + row_index + "'>" + base_currency + "</span>)</th><th>Amount(<i class='fa fa-rupee'></i>)</th><th>Remark</th><th>Bills attached Yes/No</th></tr></thead>");
                            sb.Append("<tbody>");
                            sb.Append("<tr>");
                            int m = 0;
                            int count = 0;
                            for (int k = 0; k < dsData.Tables[5].Rows.Count; k++)
                            {
                                if (Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") == Convert.ToDateTime(dsData.Tables[5].Rows[k]["fk_travel_date"]).ToString("dd-MMM-yyyy"))
                                {
                                    string re_data = getReim(Convert.ToInt32(dsData.Tables[5].Rows[k]["REIM_TYPE"]));
                                    sb.Append("<td><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus' id='add" + (row_index) + "" + (m + 1) + "' onclick='newRow(" + row_index + ")'></i></td><td class='delete'><i id='delete" + (row_index) + "" + (m + 1) + "' class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_Row(" + (row_index) + "," + (m + 1) + ")'></i></td>");
                                    sb.Append("<td style='display:none'><input id='txt_Date" + (row_index) + "" + (m + 1) + "' type='text'  class='form-control input-sm' readonly style='width:121%;' value='" + dsData.Tables[5].Rows[k]["fk_travel_date"].ToString() + "' /></td>");
                                    sb.Append("<td><select id='txt_ReimType" + (row_index) + "_" + (m + 1) + "' class='form-control input-sm' onchange=\"return change_detail_flag(" + row_index + "," + (m + 1) + ");\" >" + re_data + "</select></td>");
                                    sb.Append("<td><input id='txt_From" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm' value='" + dsData.Tables[5].Rows[k]["TRAVEL_FROM"].ToString() + "' /></td>");
                                    sb.Append("<td><input id='txt_To" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm' value='" + dsData.Tables[5].Rows[k]["TRAVEL_TO"].ToString() + "' /></td>");
                                    sb.Append("<td><input id='txt_Travel_Mode" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm' value='" + dsData.Tables[5].Rows[k]["TRAVEL_MODE"].ToString() + "'/></td>");
                                    if (Convert.ToString(dsData.Tables[5].Rows[k]["REIMBURSEMENT_TYPE"]) == "Corporate Credit Card" || Convert.ToString(dsData.Tables[5].Rows[k]["REIMBURSEMENT_TYPE"]) == "Personal Credit Card")
                                    {
                                        sb.Append("<td><input id='txt_Expenses" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm numbersOnly'  onkeyup='calculate_Amount();' value='" + dsData.Tables[5].Rows[k]["EXPENSES"].ToString() + "' readonly/></td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td><input id='txt_Expenses" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm numbersOnly'  onkeyup='calculate_Amount();' value='" + dsData.Tables[5].Rows[k]["EXPENSES"].ToString() + "'/></td>");
                                    }
                                    if (Convert.ToString(dsData.Tables[5].Rows[k]["REIMBURSEMENT_TYPE"]) == "Corporate Credit Card" || Convert.ToString(dsData.Tables[5].Rows[k]["REIMBURSEMENT_TYPE"]) == "Forex Card")
                                    {
                                        sb.Append("<td><input id='txt_L_Tax" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm numbersOnly'  onkeyup='calculate_Amount();' value='" + dsData.Tables[5].Rows[k]["TAX"].ToString() + "' readonly/></td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td><input id='txt_L_Tax" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm numbersOnly'  onkeyup='calculate_Amount();' value='" + dsData.Tables[5].Rows[k]["TAX"].ToString() + "'/></td>");
                                    }
                                    if (Convert.ToString(dsData.Tables[5].Rows[k]["REIMBURSEMENT_TYPE"]) == "Corporate Credit Card" || Convert.ToString(dsData.Tables[5].Rows[k]["REIMBURSEMENT_TYPE"]) == "Personal Credit Card")
                                    {
                                        sb.Append("<td><input id='txt_L_Rate" + (row_index) + "" + (m + 1) + "' value='" + Convert.ToString(dsData.Tables[5].Rows[k]["CONVERSION_RATE"]) + "' type='text' class='form-control input-sm numbersOnly' onkeyup='calculate_Amount();' readonly/></td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td><input id='txt_L_Rate" + (row_index) + "" + (m + 1) + "' value='" + Convert.ToString(dsData.Tables[5].Rows[k]["CONVERSION_RATE"]) + "' type='text' class='form-control input-sm numbersOnly' onkeyup='calculate_Amount();'/></td>");
                                    }
                                    
                                    if (Convert.ToString(dsData.Tables[5].Rows[k]["REIMBURSEMENT_TYPE"]) == "Corporate Credit Card" || Convert.ToString(dsData.Tables[5].Rows[k]["REIMBURSEMENT_TYPE"]) == "Personal Credit Card")
                                    {
                                        sb.Append("<td><span id='spn_L_BAmount" + row_index + "" + (m + 1) + "' style='display:none'>0</span><input id='txt_L_BAmount" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm numbersOnly' onkeyup='calculate_Amount();'/></td>");
                                        sb.Append("<td><span id='spn_L_Amount" + row_index + "" + (m + 1) + "' style='display:none'>0</span><input id='txt_L_Amount" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm numbersOnly'/></td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td><span id='spn_L_BAmount" + row_index + "" + (m + 1) + "'>0</span><input id='txt_L_BAmount" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm numbersOnly' style='display:none'  onkeyup='calculate_Amount();'/></td>");
                                        sb.Append("<td><span id='spn_L_Amount" + row_index + "" + (m + 1) + "'>0</span><input id='txt_L_Amount" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm numbersOnly' style='display:none'/></td>");
                                    }
                                    sb.Append("<td><input id='txt_Remark" + (row_index) + "" + (m + 1) + "' type='text' class='form-control input-sm' value='" + dsData.Tables[5].Rows[k]["REMARK"].ToString() + "'></td>");
                                    sb.Append("<td><select ID='txt_Bill" + (row_index) + "" + (m + 1) + "' class='form-control input-sm' onchange=\"return change_detail_flag(" + row_index + "," + (m + 1) + ");\" >");
                                    if (dsData.Tables[5].Rows[k]["BILL_STATUS"].ToString() == "Y")
                                    {
                                        sb.Append("<option Value='Y' Selected='true'>Yes</option>");
                                        sb.Append("<option Value='N'>No</option>");
                                    }
                                    else
                                    {
                                        sb.Append("<option Value='Y' >Yes</option>");
                                        sb.Append("<option Value='N' Selected='true'>No</option>");
                                    }
                                    sb.Append("</select></td>");
                                    sb.Append("</tr>");
                                    m++;
                                }
                                else
                                {
                                    if (Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") == Convert.ToDateTime(dsData.Tables[5].Rows[k]["fk_travel_date"]).ToString("dd-MMM-yyyy"))
                                    {
                                        count++;
                                    }
                                    
                                }
                            }
                            if (count != 0)
                            {
                                sb.Append("<td><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus' id='add11' onclick='newRow(" + row_index + ")'></i></td><td class='delete'><i id='delete11' class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_Row(" + row_index + ",1)'></i></td>");
                                sb.Append("<td><input id='txt_Date" + row_index + "1' type='text'  class='form-control input-sm' readonly style='width:121%;'  value='" + (fdate).ToString("dd-MMM-yyyy") + "' /></td>");
                                sb.Append("<td><input id='txt_From" + row_index + "1' type='text' class='form-control input-sm' value='' /></td>");
                                sb.Append("<td><input id='txt_To" + row_index + "1' type='text' class='form-control input-sm' value='' /></td>");
                                sb.Append("<td><input id='txt_Travel_Mode" + row_index + "1' type='text' class='form-control input-sm' value=''/></td>");
                                sb.Append("<td><input id='txt_Expenses" + row_index + "1' type='text' class='form-control input-sm numbersOnly'  onkeyup='calculate_Amount();' value=''/></td>");
                                sb.Append("<td><input id='txt_L_Tax" + row_index + "1' type='text' class='form-control input-sm numbersOnly'  onkeyup='calculate_Amount();' value=''/></td>");
                                sb.Append("<td><input id='txt_Remark" + row_index + "1' type='text' class='form-control input-sm' value='' /></td>");
                                sb.Append("<td><select ID='txt_Bill" + row_index + "1' class='form-control input-sm' value='' onchange=set_Do_Flag(" + row_index + ",1) >");
                                sb.Append("<option Value='Y'>Yes</option>");
                                sb.Append("<option Value='N'>No</option>");
                                sb.Append("</select></td>");
                                sb.Append("</tr>");
                            }
                            sb.Append("</tbody></table></div>"); sb.Append("</div>");
                        }
                        else
                        {
                            string reim = getReim(0);
                            sb.Append("<div class='col-md-12'>");
                            sb.Append("<div class='col-md-12 grey'>Local Travel Details<span id='span_Local_Amount" + row_index + "' style='display:none'>0</span>");
                            sb.Append("<table class='table table-bordered' id='tbl_Local_Travel" + row_index + "'>");
                            //sb.Append("<thead><tr class='grey'><th>Add</th><th>Delete</th><th>Date</th><th>From</th><th>To</th><th>Mode of Travel</th><th>Expense amount in currency spent</th><th>Tax</th><th>Remark</th><th>Bills attached Yes/No</th></tr></thead>");
                            sb.Append("<thead><tr class='grey'><th>Add</th><th>Delete</th><th style='display:none'>Date</th><th>Reimbursement Type</th><th>From</th><th>To</th><th>Mode of Travel</th><th>Expense amount</th><th>Tax</th><th>Exchange Rate</th><th>Amount(<span id='spn_Detail_Base" + row_index + "1'>" + base_currency + "</span>)</th><th>Amount(<i class='fa fa-rupee'></i>)</th><th>Remark</th><th>Bills attached Yes/No</th></tr></thead>");
                            sb.Append("<tbody>");
                            sb.Append("<tr>");
                            sb.Append("<td><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus' id='add11' onclick='newRow(" + row_index + ")'></i></td><td class='delete'><i id='delete11' class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_Row(" + row_index + ",1)'></i></td>");
                            sb.Append("<td style='display:none'><input id='txt_Date" + row_index + "1' type='text'  class='form-control input-sm' readonly style='width:121%;'  value='" + (fdate).ToString("dd-MMM-yyyy") + "' /></td>");
                            sb.Append("<td><select id='txt_ReimType" + (row_index) + "_1' class='form-control input-sm' onchange=\"return change_detail_flag(" + row_index + ",1);\" >" + reim + "</select></td>");
                            sb.Append("<td><input id='txt_From" + row_index + "1' type='text' class='form-control input-sm' value='' /></td>");
                            sb.Append("<td><input id='txt_To" + row_index + "1' type='text' class='form-control input-sm' value='' /></td>");
                            sb.Append("<td><input id='txt_Travel_Mode" + row_index + "1' type='text' class='form-control input-sm' value=''/></td>");
                            sb.Append("<td><input id='txt_Expenses" + row_index + "1' type='text' class='form-control input-sm numbersOnly'  onkeyup='calculate_Amount();' value=''/></td>");
                            sb.Append("<td><input id='txt_L_Tax" + row_index + "1' type='text' class='form-control input-sm numbersOnly'  onkeyup='calculate_Amount();' value=''/></td>");
                            sb.Append("<td><input id='txt_L_Rate" + (row_index) + "1' type='text' class='form-control input-sm numbersOnly' onkeyup='calculate_Amount();'/></td>");
                            sb.Append("<td><span id='spn_L_BAmount" + row_index + "1'>0</span><input id='txt_L_BAmount" + (row_index) + "1' type='text' class='form-control input-sm numbersOnly' style='display:none'  onkeyup='calculate_Amount();'/></td>");
                            sb.Append("<td><span id='spn_L_Amount" + row_index + "1'>0</span><input id='txt_L_Amount" + (row_index) + "1' type='text' class='form-control input-sm numbersOnly' style='display:none'/></td>");
                            sb.Append("<td><input id='txt_Remark" + row_index + "1' type='text' class='form-control input-sm' value='' /></td>");
                            sb.Append("<td><select ID='txt_Bill" + row_index + "1' class='form-control input-sm' value=''  onchange=\"return change_detail_flag(" + row_index + ",1);\"  >");
                            sb.Append("<option Value='Y'>Yes</option>");
                            sb.Append("<option Value='N' >No</option>");
                            sb.Append("</select></td>");
                            sb.Append("</tr>");
                            sb.Append("</tbody></table></div>"); sb.Append("</div>");
                        }


                        sb.Append("</div></div></div></div>");
                        fdate = fdate.AddDays(1);
                        row_index = row_index + 1;
                    }
                }
                else
                {
                    int row_index = 1;
                    while (fdate <= tdate)
                    {

                        sb.Append("<div class='panel'>");
                        sb.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
                        sb.Append("<div class='panel-heading-btn'><div>Amount(<span id='spn_currency" + row_index + "'>USD</span>) : <span id='row_Total" + row_index + "'>0</span></div></div>");
                        sb.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' name='rs' href='#collapse" + row_index + "' onclick='copyData(" + row_index + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spn_date" + row_index + "'>" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + "</span></a></h3>");
                        sb.Append("</div>");
                        if (row_index == 1)
                        {
                            sb.Append("<div id='collapse" + row_index + "' class='panel-collapse collapse in'><div class='panel-body' style='background-color:#ffffff'><div class='form-horizontal'>");
                        }
                        else
                        {
                            sb.Append("<div id='collapse" + row_index + "' class='panel-collapse collapse'><div class='panel-body' style='background-color:#ffffff'><div class='form-horizontal'>");
                        }
                        sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>City</label>");
                        sb.Append("<div class='col-md-3'><select id='ddlCity" + row_index + "' runat='server' onchange='getCity(" + row_index + ")' class='form-control input-sm'><option value='0'>---Select One---</option>");
                        DataTable dtCity = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "selectlocation", ref isdata, "", "", "P_GET_CITY", country_id);
                        if (dtCity != null)
                        {
                            if (dtCity.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtCity.Rows.Count; i++)
                                {
                                    sb.Append("<option value='" + Convert.ToString(dtCity.Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtCity.Rows[i]["NAME"]) + "</option>");
                                }

                            }

                        }
                        sb.Append("<option value='-1' >Other</option><input type='text' id='other_f_city" + row_index + "' class='form-control input-sm' value='' style='display:none' >");
                        sb.Append("</select></div>");
                        sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Particulars</label>");
                        sb.Append("<div class='col-md-3'><input ID='txt_Particulars" + row_index + "' class='form-control input-sm' runat='server'></input></div></div>");

                        sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Hotel Name</label>");
                        sb.Append("<div class='col-md-3'><input class='form-control input-sm' ID='hotel_name" + row_index + "'></input></div>");
                        sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Hotel Contact No</label><div class='col-md-3'><input class='form-control input-sm' ID='contact" + row_index + "'></input></div></div>");
                        sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Travel Class<font color='#ff0000'><b>*</b></font></label>");
                        sb.Append("<div class='col-md-3'><select id='ddlTravel_class" + row_index + "' runat='server' class='form-control input-sm'><option value='0'>---Select One---</option>");
                        if (travel_class == "True")
                        {
                            sb.Append("<option value='Business Class' selected>Business Class</option>");
                            sb.Append("<option value='Economy Class'>Economy Class</option>");
                        }
                        else
                        {
                            sb.Append("<option value='Business Class' >Business Class</option>");
                            sb.Append("<option value='Economy Class' selected>Economy Class</option>");
                        }

                        string reim = getReim(1);
                        sb.Append("</select></div><div class='col-md-1'></div><label class='col-md-2'>Note<font color='#ff0000'><b>*</b></font></label><div class='col-md-3' style='color:red'>Please fill the Local Travel Details</div>");
                        sb.Append("<div id='exp_data'>");
                        sb.Append(bindExpense(row_index, country_id, Designation, Ex_Rate));
                        sb.Append("</div>");
                        sb.Append("<div class='col-md-12'>");
                        sb.Append("<div class='col-md-12 grey'>Local Travel Details<span id='span_Local_Amount" + row_index + "' style='display:none'>0</span>");
                        sb.Append("<table class='table table-bordered' id='tbl_Local_Travel" + row_index + "'>");
                        sb.Append("<thead><tr class='grey'><th>Add</th><th>Delete</th><th style='display:none'>Date</th><th>Reimbursement Type</th><th>From</th><th>To</th><th>Mode of Travel</th><th>Expense amount</th><th>Tax</th><th>Exchange Rate</th><th>Amount(<span id='spn_Detail_Base" + row_index + "1'>" + base_currency + "</span>)</th><th>Amount(<i class='fa fa-rupee'></i>)</th><th>Remark</th><th>Bills attached Yes/No</th></tr></thead>");
                        sb.Append("<tbody>");
                        sb.Append("<tr>");
                        sb.Append("<td><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus' id='add11' onclick='newRow(" + row_index + ")'></i></td><td class='delete'><i id='delete11' class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_Row(" + row_index + ",1)'></i></td>");
                        sb.Append("<td style='display:none'><input id='txt_Date" + row_index + "1' type='text'  class='form-control input-sm'  readonly style='width:121%;background-color:#ffffff'  value='" + (fdate).ToString("dd-MMM-yyyy") + "' /></td>");
                        sb.Append("<td><select id='txt_ReimType" + (row_index) + "_1' class='form-control input-sm' onchange=\"return change_detail_flag(" + row_index + ",1);\" >" + reim + "</select></td>");
                        sb.Append("<td><input id='txt_From" + row_index + "1' type='text' class='form-control input-sm' value='' /></td>");
                        sb.Append("<td><input id='txt_To" + row_index + "1' type='text' class='form-control input-sm' value='' /></td>");
                        sb.Append("<td><input id='txt_Travel_Mode" + row_index + "1' type='text' class='form-control input-sm' value=''/></td>");
                        sb.Append("<td><input id='txt_Expenses" + row_index + "1' type='text' class='form-control input-sm numbersOnly'  onkeyup='calculate_Amount();' value=''/></td>");
                        sb.Append("<td><input id='txt_L_Tax" + row_index + "1' type='text' class='form-control input-sm numbersOnly'  onkeyup='calculate_Amount();' value=''/></td>");
                        sb.Append("<td><input id='txt_L_Rate" + (row_index) + "1' type='text' class='form-control input-sm numbersOnly' onkeyup='calculate_Amount();'/></td>");
                        sb.Append("<td><span id='spn_L_BAmount" + row_index + "1'>0</span><input id='txt_L_BAmount" + (row_index) + "1' type='text' class='form-control input-sm numbersOnly' style='display:none'  onkeyup='calculate_Amount();'/></td>");
                        sb.Append("<td><span id='spn_L_Amount" + row_index + "1'>0</span><input id='txt_L_Amount" + (row_index) + "1' type='text' class='form-control input-sm numbersOnly' style='display:none'/></td>");
                        sb.Append("<td><input id='txt_Remark" + row_index + "1' type='text' class='form-control input-sm' value='' /></td>");
                        sb.Append("<td><select ID='txt_Bill" + row_index + "1' class='form-control input-sm' value='' onchange=\"return change_detail_flag(" + row_index + ",1);\" >");
                        sb.Append("<option Value='Y'>Yes</option>");
                        sb.Append("<option Value='N' >No</option>");
                        sb.Append("</select></td>");
                        sb.Append("</tr>");
                        sb.Append("</tbody></table></div>"); sb.Append("</div>");
                        sb.Append("</div></div></div></div>");
                        fdate = fdate.AddDays(1);
                        row_index = row_index + 1;
                    }
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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string[] getReimTypes()
    {
        string[] dropData = new string[1];
        dropData[0] = "0";
        string is_data = "";
        try
        {
            DataTable rt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreimbursements", ref is_data, "International Travel Expenses");
            if (rt != null)
            {
                dropData = new string[rt.Rows.Count + 1];
                dropData[0] = "0";
                for (int i = 0; i < rt.Rows.Count; i++)
                {
                    dropData[i + 1] = Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]);
                }
            }
        }
        catch (Exception ex)
        {
            dropData = new string[1];
            dropData[0] = "0";
        }
        return dropData;
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

    [System.Web.Services.WebMethod]
    public static string check_do_approval(string Country_ID, string Desingnation, string exp_name, string Ex_Rate)
    {
        string limit = string.Empty;
        string is_data = string.Empty;
        int cond = 0;
        if (exp_name == "Hotel")
        {
            cond = 2;
        }
        else if (exp_name == "Boarding")
        {
            cond = 3;
        }
        else if (exp_name == "Internet")
        {
            cond = 7;
        }
        else if (exp_name == "Mobile")
        {
            cond = 8;
        }
        DataTable limit_Dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getincidentallimit", ref is_data, Convert.ToUInt32(Country_ID), Desingnation.Trim(), Convert.ToUInt32(cond), Ex_Rate);
        if (limit_Dt.Rows.Count > 0 && limit_Dt != null)
        {
            limit = limit_Dt.Rows[0][0].ToString();
        }
        else
        {
            limit = "0";
        }
        return limit;
    }


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string check_exist(string jFDate, string jTDate, string AD_ID, int Country_id)
    {
        string flag = string.Empty;
        string is_data = string.Empty;
        DataTable exist_dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "checkexist", ref is_data, Convert.ToDateTime(jFDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(jTDate).ToString("dd/MM/yyyy"), AD_ID, Country_id);
        if (exist_dt.Rows.Count > 0 && exist_dt != null)
        {
            if (exist_dt.Rows[0][0].ToString() != "0")
            {
                flag = "True";
            }

        }

        return flag;
    }


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string getmultiplerows(int index, int tab_rowid, int country_id, string Designation, string Ex_Rate)
    {
        string exp_data = "";
        string is_data = "";
        int rech = 0;
        int count = 0;
        StringBuilder reim = new StringBuilder();
        StringBuilder html = new StringBuilder();
        StringBuilder Exp_html = new StringBuilder();
        try
        {
            DataTable rt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreimbursements", ref is_data, "International Travel Expenses");
            if (rt != null)
            {
                for (int i = 0; i < rt.Rows.Count; i++)
                {
                    if (Convert.ToString(rt.Rows[i]["is_chargable"]) == "1")
                    {
                        if (rech == 0)
                        {
                            reim.Append("<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "' >" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>");
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
            DataSet Exp_dat = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getexpenseids", ref is_data);
            if (Exp_dat != null)
            {
                DataTable Inc_Con_Dt = new DataTable();
                DataTable ejm = new DataTable();
                ejm = Exp_dat.Tables[0];
                Inc_Con_Dt = Exp_dat.Tables[1];
                if (ejm != null)
                {
                    for (int j = 0; j < ejm.Rows.Count; j++)
                    {
                        if (Convert.ToString(ejm.Rows[j]["EXPENSE_HEAD"]) == "Hotel")
                        {
                            Exp_html.Append("<option Value='" + Convert.ToString(ejm.Rows[j]["FK_EXPENSE_HEAD"]) + "' Selected='true'>" + Convert.ToString(ejm.Rows[j]["EXPENSE_HEAD"]) + "</option>");
                        }
                        else
                        {
                            Exp_html.Append("<option Value='" + Convert.ToString(ejm.Rows[j]["FK_EXPENSE_HEAD"]) + "'>" + Convert.ToString(ejm.Rows[j]["EXPENSE_HEAD"]) + "</option>");
                        }
                    }
                }
                for (int i = 0; i < 1; i++)
                {
                    html.Append("<tr>");
                    string cnt = "1";
                    string value = "";
                    string cond = "0";

                    if (Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).ToUpper() == "HOTEL")
                    {
                        cond = "2";
                    }
                    DataTable limit_Dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getincidentallimit", ref is_data, Convert.ToUInt32(country_id), Designation, cond, Ex_Rate);
                    if (limit_Dt.Rows.Count > 0 && limit_Dt != null)
                    {
                        value = limit_Dt.Rows[0][0].ToString();
                    }
                    html.Append("<td><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus' id='Madd" + (index) + "" + tab_rowid + "' onclick='Add_Multiple_Row(" + (index) + ")'></i></td><td class='delete'><i id='Mdelete" + (index) + "" + tab_rowid + "' class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='Delete_Multiple_Row(" + (index) + "," + tab_rowid + ")'></i></td>");
                    html.Append("<td><select ID='expense_id" + index + "_" + tab_rowid + "' class='form-control input-sm' onchange=\"return get_Limit(" + index + "," + (tab_rowid) + ");\">");
                    html.Append("<option Value='0'>---Select One---</option>");
                    html.Append(Exp_html);
                    html.Append("</select>(<span id='IS_SUP" + (index) + "" + tab_rowid + "'>" + Convert.ToString(ejm.Rows[i]["IS_SUPPORTING"]) + "</span>)");
                    html.Append("</td>");
                    html.Append("<td>");
                    if (Convert.ToString(ejm.Rows[i]["IS_ALLOW"]) == "1" && cnt != "0")
                    {
                        html.Append("<select ID='ddlrem_" + index + "_" + tab_rowid + "' class='form-control input-sm' onchange=\"return change_flag(" + index + "," + tab_rowid + ");\">");
                    }
                    else
                    {
                        html.Append("<select ID='ddlrem_" + index + "_" + tab_rowid + "' class='form-control input-sm' onchange=\"return change_flag(" + index + "," + tab_rowid + ");\" style='display:none'>");
                    }
                    html.Append("<option Value='0'>---Select One---</option>");
                    html.Append(reim);
                    html.Append("</select>");
                    if (rech == 0)
                    {
                        html.Append("<input type='text' id='reim_val" + index + "_" + tab_rowid + "' value='0' style='display:none' />");
                    }
                    else
                    {
                        html.Append("<input type='text' id='reim_val" + index + "_" + tab_rowid + "' value='1' style='display:none' />");
                    }
                    html.Append("</td>");
                    html.Append("<td style='text-align:right;'>");

                    if (Convert.ToString(ejm.Rows[i]["IS_ALLOW"]) == "1" && cnt != "0")
                    {
                        html.Append("<input type='text' id='D_ALLOW_" + index + "_" + tab_rowid + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='txt_Expense_Amt" + index + "_" + tab_rowid + "' value=''  class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();' ><input type='number' min='0' id='hlimit" + index + "_" + tab_rowid + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                    }
                    else
                    {
                        html.Append("<input type='text' id='D_ALLOW_" + index + "_" + tab_rowid + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "'  style='display:none'><input type='text' id='txt_Expense_Amt" + index + "_" + tab_rowid + "' value='" + getIncidental(country_id, Designation, Ex_Rate) + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' readonly><input type='number' min='0' id='hlimit" + index + "_" + tab_rowid + "' value='" + value + "'  class='form-control input-sm' style='text-align:right; display:none'>");
                    }
                    html.Append("</td>");
                    html.Append("<td><input type='text' id='Tax_" + index + "_" + tab_rowid + "' value=''  class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'></td>");
                    html.Append("<td><input type='text' id='cur_" + index + "_" + tab_rowid + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'></td>");

                    if (Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]) == "Incidental")
                    {
                        decimal dollar = 0;
                        string Incidental_Amount = getIncidental(country_id, Designation, Ex_Rate);
                        dollar = Convert.ToDecimal(Incidental_Amount) * 1;
                        html.Append("<td><span id='dollar_" + "_" + index + "_" + tab_rowid + "'>" + dollar + "</span></td>");
                    }
                    else
                    {
                        html.Append("<td><span id='dollar_" + index + "_" + tab_rowid + "'>0</span><input type='text' id='dollar_txt" + index + "_" + tab_rowid + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;display:none' onkeyup='calculate_Amount();'/></td>");
                    }

                    html.Append("<td><span id='rupees_" + index + "_" + tab_rowid + "'>0</span><input type='text' id='rupees_txt" + +index + "_" + tab_rowid + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;display:none' onkeyup='calculate_Amount();'/></td>");

                    if (Convert.ToString(ejm.Rows[i]["IS_SUPPORTING"]) == "Y")
                    {
                        html.Append("<td>");
                        html.Append("<select ID='ddl_SUP" + "_" + index + "_" + tab_rowid + "' class='form-control input-sm' onchange='enable_disable_field(" + index + "," + tab_rowid + ")'>");
                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N'>No</option></select>");
                        html.Append("</td>");
                        html.Append("<td>");
                        html.Append("<input type='text' ID='particular_SUP" + "_" + index + "_" + tab_rowid + "' class='form-control input-sm'/><font ID='f_other" + "_" + index + "_" + tab_rowid + "' style='color:red'>*</font>");
                        html.Append("</td>");
                    }
                    else
                    {
                        html.Append("<td>");
                        html.Append("<select ID='ddl_SUP" + "_" + index + "_" + tab_rowid + "' class='form-control input-sm' style='display:none'><font style='display:none; color:red'>*</font>");
                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N' Selected='true'>No</option></select>");
                        html.Append("</td>");
                        html.Append("<td>");
                        html.Append("<input type='text' ID='particular_SUP" + "_" + index + "_" + tab_rowid + "' class='form-control input-sm' style='display:none'/><font ID='f_other" + "_" + index + "_" + tab_rowid + "' style='display:none; color:red'>*</font>");
                        html.Append("</td>");
                    }
                    string other_remark = Convert.ToString(ejm.Rows[i]["IS_OTHER"]);
                    if (other_remark == "0")
                    {
                        html.Append("<td><input type='text' ID='other_remark" + "_" + index + "_" + tab_rowid + "' class='form-control input-sm' style='display:none'/><input type='text' ID='fk_other_remark" + "_" + index + "_" + tab_rowid + "' value='0' class='form-control input-sm' style='display:none'/></td>");
                    }
                    else
                    {
                        html.Append("<td><input type='text' ID='other_remark" + "_" + index + "_" + tab_rowid + "' value='NA' class='form-control input-sm'/><input type='text' ID='fk_other_remark" + "_" + index + "_" + tab_rowid + "' value='1' class='form-control input-sm' style='display:none'/></td>");
                    }

                    html.Append("</tr>");

                }
                exp_data = Convert.ToString(html);
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return exp_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_inc_limits(int country_id, string Designation, string exp_txt, string data, string city_id)
    {
        string html = string.Empty;
        string is_data = string.Empty;
        string value = string.Empty;
        string cond = "0";
        try
        {
            if (exp_txt == "Hotel")
            {
                cond = "2";
            }
            else if (exp_txt == "Boarding")
            {
                cond = "3";
            }
            else if (exp_txt == "Internet")
            {
                cond = "7";
            }
            else if (exp_txt == "Mobile")
            {
                cond = "8";
            }
            DataTable limit_Dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getincidentallimit", ref is_data, Convert.ToUInt32(country_id), Designation, cond, city_id);
            if (limit_Dt.Rows.Count > 0 && limit_Dt != null)
            {
                value = limit_Dt.Rows[0][0].ToString();
            }
            html = data + "@@" + value;

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return html;
    }
    #endregion

    #region Event
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;

            //path = activeDir + "\\";
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string isSaved = string.Empty;
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                divIns.Style.Add("display", "none");
                string isdata = string.Empty;
                string isInserted = string.Empty;

                string adv_pk_id = txt_advance_id.Text;

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

                string Local_travel = txt_LocTrav_XML.Text;
                Local_travel = Local_travel.Replace("&", "&amp;");
                Local_travel = Local_travel.Replace("'", "&apos;");
                txt_LocTrav_XML.Text = Local_travel.ToString();

                DataTable DTAP = new DataTable();
                int pr = 0;
                DTAP = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getaccapprover", ref isInserted, "FOREIGN TRAVEL DOC VERIFIER", 0, 2);
                if (txt_Username.Text.ToUpper() != "PRRATHI" && txt_Username.Text.ToUpper() != "RBRATHI")
                {
                    pr = 1;
                }
                else if (DTAP != null && DTAP.Rows.Count > 0 && (txt_Username.Text.ToUpper() == "PRRATHI" || txt_Username.Text.ToUpper() == "RBRATHI"))
                {
                    pr = 1;
                }
                else
                {
                    pr = 0;
                }
                if (pr == 1)
                {
                    string release_id = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreleaseid", ref isdata, txtProcessID.Text, step_name.Text, "SUBMIT");
                    if (release_id != "")
                    {
                        isSaved = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "update", ref isdata, txt_pk_id.Text, txt_Username.Text, fdate, tdate, xml_string, file_attach, txt_sub_xml_data.Text, txtProcessID.Text, txtInstanceID.Text, txt_advance_id.Text, req_remark.Text, span_bank_no.InnerHtml, ddlCountry.SelectedValue, txt_DO_Status.Text, txt_Deviation_Reason.Text, txt_Return_Money.Value, txt_LocTrav_XML.Text);
                        if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                        {
                            string[] errmsg = isdata.Split(':');
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                        }
                        else
                        {
                            bool isCreate;
                            string msg = "";
                            string emailid = "";
                            string[] Dval = new string[1];
                            Dval[0] = span_Approver.InnerHtml;
                            string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, step_name.Text, "USER", txt_Username.Text, "SUBMIT", req_remark.Text, "0", "0");
                            if (txt_Doc_Verifacation_status.Text != "True")
                            {
                                txt_app_mail.Text = txt_Approver_Email.Text;
                                msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Foreign Travel Expense Request has been sent for your approval.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerText, txtWIID.Text, ref isInserted);
                            }
                            else
                            {
                                //if (txt_Username.Text.ToLower() == "rbrathi")
                                //{
                                //    //Dval[0] = "prrathi";
                                //    Dval[0] = "prrathi";
                                //    txt_app_mail.Text = "prrathi@sudarshan.com";
                                //    msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Foreign Travel Expense Request has been sent for your approval.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                //    isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerText, txtWIID.Text, ref isInserted);
                                //}
                                //else 
                                //if (txt_Username.Text.ToLower() == "prrathi")
                                //{

                                    Dval = new string[DTAP.Rows.Count];
                                    if (DTAP.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < DTAP.Rows.Count; i++)
                                        {
                                            Dval[i] = Convert.ToString(DTAP.Rows[i]["USER_ADID"]);
                                            if (txt_app_mail.Text == "")
                                            {
                                                txt_app_mail.Text = Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                                            }
                                            else
                                            {
                                                txt_app_mail.Text = txt_app_mail.Text + ',' + Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                                            }
                                        }
                                    }
                                    string auditid1 = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "FOREIGN TRAVEL EXPENSE APPROVAL", "USER", txt_Username.Text, "APPROVED", req_remark.Text, "0", "0");
                                    CryptoGraphy crypt = new CryptoGraphy();
                                    string process_name = crypt.Encryptdata("FOREIGN TRAVEL EXPENSE");
                                    string req_no = crypt.Encryptdata(isSaved);
                                    msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Foreign Travel Request Has Been Approved Succefully and Sent For Document Verification.</span></pre><pre><span style='font-size: medium;'>Request No: " + isSaved + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://"+compname+"/Sudarshan-Portal/Vouchers/Foreign_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://"+compname+"/Sudarshan-Portal/Vouchers/Foreign_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";
                                    isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "263", "FOREIGN TRAVEL EXPENSE", "SAVE-AS-SELF-APPROVAL", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerText, txtWIID.Text, ref isInserted);

                                //}
                                //else
                                //{
                                //    Dval[0] = span_Approver.InnerHtml;
                                //    txt_app_mail.Text = txt_Approver_Email.Text;
                                //    msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Foreign Travel Expense Request has been sent for your approval.</font></pre><pre><font size='3'>Request No: " + isSaved + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                //    isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerText, txtWIID.Text, ref isInserted);
                                //}

                            }

                            if (isCreate)
                            {
                                 emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, step_name.Text, "SUBMIT", txt_app_mail.Text, txtEmailID.Text, msg, "Request No:" + spn_req_no.InnerHtml);
                                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getfiles", ref isdata, "FOREIGN TRAVEL EXPENSE", spn_req_no.InnerHtml);
                                if (dt.Rows.Count > 0)
                                {
                                    string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                    string path = string.Empty;

                                    string foldername = spn_req_no.InnerHtml;
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
                                try
                                {

                                   

                                }
                                catch (Exception ex)
                                {
                                    FSL.Logging.Logger.WriteEventLog(false, ex);
                                }
                                finally
                                {

                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Request Submitted Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                    }
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Document Verifier Not Found...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void btnDraft_Click(object sender, EventArgs e)
    {
        string isSaved = string.Empty;
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                divIns.Style.Add("display", "none");
                string isdata = string.Empty;
                string isInserted = string.Empty;

                string travel_voucher_id = "";
                string adv_pk_id = txt_advance_id.Text;

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

                string Local_travel = txt_LocTrav_XML.Text;
                Local_travel = Local_travel.Replace("&", "&amp;");
                Local_travel = Local_travel.Replace("'", "&apos;");
                txt_LocTrav_XML.Text = Local_travel.ToString();

                string release_id = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreleaseid", ref isdata, txtProcessID.Text, "FOREIGN TRAVEL EXPENSE", "SAVE-AS-DRAFT");
                if (release_id != "")
                {
                    isSaved = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "update", ref isdata, txt_pk_id.Text, txt_Username.Text, fdate, tdate, xml_string, file_attach, txt_sub_xml_data.Text, txtProcessID.Text, txtInstanceID.Text, txt_advance_id.Text, req_remark.Text, span_bank_no.InnerHtml, ddlCountry.SelectedValue, txt_DO_Status.Text, txt_Deviation_Reason.Text, txt_Return_Money.Value, txt_LocTrav_XML.Text);
                    if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = isdata.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Username.Text;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "261", "FOREIGN TRAVEL EXPENSE", "SAVE-AS-DRAFT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, Convert.ToString(spn_req_no.InnerHtml), "0", ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
                                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getfiles", ref isdata, "FOREIGN TRAVEL EXPENSE", spn_req_no.InnerHtml);
                                if (dt.Rows.Count > 0)
                                {
                                    string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                    string path = string.Empty;

                                    string foldername = spn_req_no.InnerHtml;
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

                                string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, step_name.Text, "USER", txt_Username.Text, "SAVE AS DRAFT", req_remark.Text, "0", "0");
                            }
                            catch (Exception ex)
                            {
                                // throw;
                                FSL.Logging.Logger.WriteEventLog(false, ex);
                            }
                            finally
                            {

                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Travel Request Saved In Draft Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                    }
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
    #endregion

    #region Others
    
    protected static string bindExpense(int index, int country_id, string Designation, string Ex_Rate)
    {
        string exp_data = "";
        string is_data = "";
        int rech = 0;
        int count = 0;
        StringBuilder reim = new StringBuilder();
        StringBuilder html = new StringBuilder();
        StringBuilder Exp_html = new StringBuilder();
        StringBuilder IncExp_html = new StringBuilder();
        try
        {
            DataTable rt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreimbursements", ref is_data, "International Travel Expenses");
            if (rt != null)
            {
                for (int i = 0; i < rt.Rows.Count; i++)
                {
                    if (Convert.ToString(rt.Rows[i]["is_chargable"]) == "1")
                    {
                        if (rech == 0)
                        {
                            reim.Append("<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "' >" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>");
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

            rt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreimbursements", ref is_data, "International Travel Expenses");
            if (rt != null)
            {
                for (int i = 0; i < rt.Rows.Count; i++)
                {
                    if (Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) == "1")
                    {
                        IncExp_html.Append("<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "' Selected='true'>" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>");
                    }
                    else
                    {
                        IncExp_html.Append("<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "'>" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>");
                    }
                }
            }
            DataSet Exp_dat = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getexpenseids", ref is_data);
            if (Exp_dat != null)
            {
                DataTable Inc_Con_Dt = new DataTable();
                DataTable ejm = new DataTable();
                ejm = Exp_dat.Tables[0];
                Inc_Con_Dt = Exp_dat.Tables[1];
                if (ejm != null)
                {
                    for (int j = 0; j < ejm.Rows.Count; j++)
                    {
                        if (Convert.ToString(ejm.Rows[j]["EXPENSE_HEAD"]) == "Hotel")
                        {
                            Exp_html.Append("<option Value='" + Convert.ToString(ejm.Rows[j]["FK_EXPENSE_HEAD"]) + "' Selected='true'>" + Convert.ToString(ejm.Rows[j]["EXPENSE_HEAD"]) + "</option>");
                        }
                        else
                        {
                            Exp_html.Append("<option Value='" + Convert.ToString(ejm.Rows[j]["FK_EXPENSE_HEAD"]) + "'>" + Convert.ToString(ejm.Rows[j]["EXPENSE_HEAD"]) + "</option>");
                        }
                    }
                }

                html.Append("<div class='form-group'></div>");
                html.Append("<div class='col-md-12'>");
                html.Append("<div class='col-md-12'>");
                html.Append("<table class='table table-bordered' id=tab_Exp" + index + ">");
                html.Append("<thead><tr class='grey'>");
                html.Append("<th>Add</th><th>Delete</th><th>Expense Head</th><th>Reimbursement Type</th><th>Expense Amount</th><th>Tax</th><th>Exchange Rate</th><th>Amount (<span id='spn_currency_amt" + index + "' runat=server'>USD</span>)</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Supporting Doc</th><th>Supporting Particulars</th><th>Remark</th>");
                html.Append("</tr></thead>");
                html.Append("<tbody>");
                //****************************Incidental and Conveyance****************************//
                for (int i = 0; i < Inc_Con_Dt.Rows.Count; i++)
                {
                    html.Append("<tr>");
                    string cnt = "1";
                    string value = "";
                    string cond = "0";
                    if (Convert.ToString(Inc_Con_Dt.Rows[i]["EXPENSE_HEAD"]).ToUpper() == "INCIDENTAL")
                    {
                        cond = "1";
                    }
                    DataTable limit_Dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getincidentallimit", ref is_data, Convert.ToUInt32(country_id), Designation, cond, Ex_Rate);
                    if (limit_Dt.Rows.Count > 0 && limit_Dt != null)
                    {
                        value = limit_Dt.Rows[0][0].ToString();
                    }
                    html.Append("<td><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus' id='Madd" + (index) + "" + (i + 1) + "' style='display:none' onclick='Add_Multiple_Row(" + (index) + ")'></i></td><td class='delete'><i id='Mdelete" + (index) + "" + (i + 1) + "' style='display:none' class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='Delete_Multiple_Row(" + (index) + "," + (i + 1) + ")'></i></td>");
                    if (Convert.ToString(Inc_Con_Dt.Rows[i]["EXPENSE_HEAD"]).ToUpper() == "INCIDENTAL")
                    {
                        html.Append("<td><label>" + Inc_Con_Dt.Rows[i]["EXPENSE_HEAD"] + "</label>(<span id='IS_SUP" + (index) + "" + (i + 1) + "'>" + Convert.ToString(Inc_Con_Dt.Rows[i]["IS_SUPPORTING"]) + "</span>)");
                        html.Append("<select ID='expense_id" + index + "_" + (i + 1) + "' class='form-control input-sm' style='display:none' onchange=\"return get_Limit(" + index + "," + (i + 1) + ");\" >");
                        html.Append("<option Value='0'>---Select One---</option><option Value='41' Selected='true'>Incidental</option></select>");
                        html.Append("</td>");
                    }
                    else
                    {
                        html.Append("<td><label>" + Inc_Con_Dt.Rows[i]["EXPENSE_HEAD"] + "</label>(<span id='IS_SUP" + (index) + "" + (i + 1) + "'>" + Convert.ToString(Inc_Con_Dt.Rows[i]["IS_SUPPORTING"]) + "</span>)");
                        html.Append("<select ID='expense_id" + index + "_" + (i + 1) + "' class='form-control input-sm' style='display:none' onchange=\"return get_Limit(" + index + "," + (i + 1) + ");\">");
                        html.Append("<option Value='0'>---Select One---</option><option Value='40' Selected='true'>Conveyance</option></select>");
                        html.Append("</td>");
                    }

                    html.Append("<td>");
                    if (Convert.ToString(Inc_Con_Dt.Rows[i]["IS_ALLOW"]) == "1" && cnt != "0")
                    {
                        html.Append("<select ID='ddlrem_" + index + "_" + (i + 1) + "' class='form-control input-sm' onchange=\"return change_flag(" + index + "," + (i + 1) + ");\">");
                    }
                    else
                    {
                        html.Append("<select ID='ddlrem_" + index + "_" + (i + 1) + "' class='form-control input-sm' onchange=\"return change_flag(" + index + "," + (i + 1) + ");\" style='display:none'>");
                    }
                    html.Append("<option Value='0'>---Select One---</option>");
                    html.Append(IncExp_html);
                    html.Append("</select>");
                    if (rech == 0)
                    {
                        html.Append("<input type='text' id='reim_val" + index + "_" + (i + 1) + "' value='0' style='display:none' />");
                    }
                    else
                    {
                        html.Append("<input type='text' id='reim_val" + index + "_" + (i + 1) + "' value='1' style='display:none' />");
                    }
                    html.Append("</td>");
                    html.Append("<td style='text-align:right;'>");

                    if (Convert.ToString(Inc_Con_Dt.Rows[i]["IS_ALLOW"]) == "1" && cnt != "0")
                    {
                        html.Append("<input type='text' id='D_ALLOW_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(Inc_Con_Dt.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='txt_Expense_Amt" + index + "_" + (i + 1) + "' value=''  class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();' ><input type='number' min='0' id='hlimit" + index + "_" + (i + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                    }
                    else
                    {

                        html.Append("<input type='text' id='D_ALLOW_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(Inc_Con_Dt.Rows[i]["DIRECT_ALLOW"]) + "'  style='display:none'><input type='text' id='txt_Expense_Amt" + index + "_" + (i + 1) + "' value='" + getIncidental(country_id, Designation, Ex_Rate) + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' readonly><input type='number' min='0' id='hlimit" + index + "_" + (i + 1) + "' value='" + value + "'  class='form-control input-sm' style='text-align:right; display:none'>");
                    }
                    html.Append("</td>");
                    if (Convert.ToString(Inc_Con_Dt.Rows[i]["EXPENSE_HEAD"]) == "Incidental")
                    {
                        html.Append("<td><input type='text' id='Tax_" + index + "_" + (i + 1) + "' value=''  class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;display:none;background-color:#ffffff' onkeyup='calculate_Amount();'></td>");
                        html.Append("<td><input type='text' id='cur_" + index + "_" + (i + 1) + "' value='1'  class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();' readonly></td>");
                    }
                    else
                    {
                        html.Append("<td><input type='text' id='Tax_" + index + "_" + (i + 1) + "' value='' readonly  class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'></td>");
                        html.Append("<td><input type='text' id='cur_" + index + "_" + (i + 1) + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'></td>");

                    }
                    if (Convert.ToString(Inc_Con_Dt.Rows[i]["EXPENSE_HEAD"]) == "Incidental")
                    {
                        decimal dollar = 0;
                        string Incidental_Amount = getIncidental(country_id, Designation, Ex_Rate);
                        dollar = Convert.ToDecimal(Incidental_Amount) * 1;
                        html.Append("<td><span id='dollar_" + index + "_" + (i + 1) + "'>" + dollar + "</span></td>");
                    }
                    else
                    {
                        html.Append("<td><span id='dollar_" + index + "_" + (i + 1) + "'>0</span><input type='text' id='dollar_txt" + index + "_" + (i + 1) + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;display:none' onkeyup='calculate_Amount();'/></td>");
                    }

                    html.Append("<td><span id='rupees_" + index + "_" + (i + 1) + "'>0</span><input type='text' id='rupees_txt" + index + "_" + (i + 1) + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;display:none' onkeyup='calculate_Amount();'/></td>");

                    if (Convert.ToString(Inc_Con_Dt.Rows[i]["IS_SUPPORTING"]) == "Y")
                    {
                        html.Append("<td>");
                        html.Append("<select ID='ddl_SUP_" + index + "_" + (i + 1) + "' class='form-control input-sm' onchange='enable_disable_field(" + index + "," + (i + 1) + ")'>");
                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N'>No</option></select>");
                        html.Append("</td>");
                        html.Append("<td>");
                        html.Append("<input type='text' ID='particular_SUP" + "_" + index + "_" + (i + 1) + "' class='form-control input-sm'/><font ID='f_other" + "_" + index + "_" + (i + 1) + "' style='color:red'>*</font>");
                        html.Append("</td>");
                    }
                    else
                    {
                        html.Append("<td>");
                        html.Append("<select ID='ddl_SUP_" + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' style='display:none'><font style='display:none; color:red'>*</font>");
                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N' Selected='true'>No</option></select>");
                        html.Append("</td>");
                        html.Append("<td>");
                        html.Append("<input type='text' ID='particular_SUP" + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' style='display:none'/><font ID='f_other" + index + "_" + (i + 1) + "' style='display:none; color:red'>*</font>");
                        html.Append("</td>");
                    }
                    string other_remark = Convert.ToString(Inc_Con_Dt.Rows[i]["IS_OTHER"]);
                    if (other_remark == "0")
                    {
                        html.Append("<td><input type='text' ID='other_remark" + index + "_" + (i + 1) + "' class='form-control input-sm' style='display:none'/><input type='text' ID='fk_other_remark" + index + "_" + (i + 1) + "' value='0' class='form-control input-sm' style='display:none'/></td>");
                    }
                    else
                    {
                        html.Append("<td><input type='text' ID='other_remark" + "_" + index + "_" + (i + 1) + "' value='NA' class='form-control input-sm'/><input type='text' ID='fk_other_remark" + index + "_" + (i + 1) + "' value='1' class='form-control input-sm' style='display:none'/></td>");
                    }

                    html.Append("</tr>");

                }
                //****************************END****************************//
                for (int i = 0; i < 1; i++)
                {
                    html.Append("<tr>");
                    string cnt = "1";
                    string value = "";
                    string cond = "0";

                    if (Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).ToUpper() == "HOTEL")
                    {
                        cond = "2";
                    }
                    DataTable limit_Dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getincidentallimit", ref is_data, Convert.ToUInt32(country_id), Designation, cond, Ex_Rate);
                    if (limit_Dt.Rows.Count > 0 && limit_Dt != null)
                    {
                        value = limit_Dt.Rows[0][0].ToString();
                    }
                    html.Append("<td><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus' id='Madd" + (index) + "2' onclick='Add_Multiple_Row(" + (index) + ")'></i></td><td class='delete'><i id='Mdelete" + (index) + "2' class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='Delete_Multiple_Row(" + (index) + ",2)'></i></td>");
                    html.Append("<td><select ID='expense_id" + index + "_2' class='form-control input-sm' onchange=\"return get_Limit(" + index + ",2);\">");
                    html.Append("<option Value='0'>---Select One---</option>");
                    html.Append(Exp_html);
                    html.Append("</select>(<span id='IS_SUP" + (index) + "2'>" + Convert.ToString(ejm.Rows[i]["IS_SUPPORTING"]) + "</span>)");
                    html.Append("</td>");
                    html.Append("<td>");
                    if (Convert.ToString(ejm.Rows[i]["IS_ALLOW"]) == "1" && cnt != "0")
                    {
                        html.Append("<select ID='ddlrem_" + index + "_2' class='form-control input-sm' onchange=\"return change_flag(" + index + ",2);\">");
                    }
                    else
                    {
                        html.Append("<select ID='ddlrem_" + index + "_2' class='form-control input-sm' onchange=\"return change_flag(" + index + ",2);\" style='display:none'>");
                    }
                    html.Append("<option Value='0'>---Select One---</option>");
                    html.Append(reim);
                    html.Append("</select>");
                    if (rech == 0)
                    {
                        html.Append("<input type='text' id='reim_val" + index + "_2' value='0' style='display:none' />");
                    }
                    else
                    {
                        html.Append("<input type='text' id='reim_val" + index + "_2' value='1' style='display:none' />");
                    }
                    html.Append("</td>");
                    html.Append("<td style='text-align:right;'>");

                    if (Convert.ToString(ejm.Rows[i]["IS_ALLOW"]) == "1" && cnt != "0")
                    {
                        html.Append("<input type='text' id='D_ALLOW_" + index + "_2' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='txt_Expense_Amt" + index + "_2' value=''  class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();' ><input type='number' min='0' id='hlimit" + index + "_2' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                    }
                    else
                    {
                        html.Append("<input type='text' id='D_ALLOW_" + index + "_2' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "'  style='display:none'><input type='text' id='txt_Expense_Amt" + index + "_2' value='" + getIncidental(country_id, Designation, Ex_Rate) + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' readonly><input type='number' min='0' id='hlimit" + index + "_2' value='" + value + "'  class='form-control input-sm' style='text-align:right; display:none'>");
                    }
                    html.Append("</td>");
                    html.Append("<td><input type='text' id='Tax_" + index + "_2' value=''  class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;background-color:#ffffff' onkeyup='calculate_Amount();'></td>");
                    html.Append("<td><input type='text' id='cur_" + index + "_2' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'></td>");

                    if (Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]) == "Incidental")
                    {
                        decimal dollar = 0;
                        string Incidental_Amount = getIncidental(country_id, Designation, Ex_Rate);
                        dollar = Convert.ToDecimal(Incidental_Amount) * 1;
                        html.Append("<td><span id='dollar_" + "_" + index + "_2'>" + dollar + "</span></td>");
                    }
                    else
                    {
                        html.Append("<td><span id='dollar_" + index + "_2'>0</span><input type='text' id='dollar_txt" + index + "_2' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;display:none' onkeyup='calculate_Amount();'/></td>");
                    }

                    html.Append("<td><span id='rupees_" + index + "_2'>0</span><input type='text' id='rupees_txt" + +index + "_2' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;display:none' onkeyup='calculate_Amount();'/></td>");

                    if (Convert.ToString(ejm.Rows[i]["IS_SUPPORTING"]) == "Y")
                    {
                        html.Append("<td>");
                        html.Append("<select ID='ddl_SUP" + "_" + index + "_2' class='form-control input-sm' onchange='enable_disable_field(" + index + ",2)'>");
                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N'>No</option></select>");
                        html.Append("</td>");
                        html.Append("<td>");
                        html.Append("<input type='text' ID='particular_SUP" + "_" + index + "_2' class='form-control input-sm'/><font ID='f_other" + "_" + index + "_2' style='color:red'>*</font>");
                        html.Append("</td>");
                    }
                    else
                    {
                        html.Append("<td>");
                        html.Append("<select ID='ddl_SUP" + "_" + index + "_2' class='form-control input-sm' style='display:none'><font style='display:none; color:red'>*</font>");
                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N' Selected='true'>No</option></select>");
                        html.Append("</td>");
                        html.Append("<td>");
                        html.Append("<input type='text' ID='particular_SUP" + "_" + index + "_2' class='form-control input-sm' style='display:none'/><font ID='f_other" + "_" + index + "_2' style='display:none; color:red'>*</font>");
                        html.Append("</td>");
                    }
                    string other_remark = Convert.ToString(ejm.Rows[i]["IS_OTHER"]);
                    if (other_remark == "0")
                    {
                        html.Append("<td><input type='text' ID='other_remark" + "_" + index + "_2' class='form-control input-sm' style='display:none'/><input type='text' ID='fk_other_remark" + "_" + index + "_2' value='0' class='form-control input-sm' style='display:none'/></td>");
                    }
                    else
                    {
                        html.Append("<td><input type='text' ID='other_remark" + "_" + index + "_2' value='NA' class='form-control input-sm'/><input type='text' ID='fk_other_remark" + "_" + index + "_2' value='1' class='form-control input-sm' style='display:none'/></td>");
                    }

                    html.Append("</tr>");

                }
                html.Append("</tbody></table>");
                html.Append("</div>");
                html.Append("<div class='col-md-1'></div>");
                html.Append("</div>");
                html.Append("<div class='form-group'></div>");
                exp_data = Convert.ToString(html);
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return exp_data;
    }
    protected static string bindExpense(int index, string wiid, int country_id, string Designation, DateTime fdate, string Ex_Rate)
    {
        string exp_data = "";
        string is_data = "";
        int rech = 0;
        StringBuilder reim = new StringBuilder();
        StringBuilder html = new StringBuilder();
        StringBuilder Exp_html = new StringBuilder();
        try
        {
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "pgetrequestdata", ref is_data, wiid);
            if (dsData != null)
            {
                DataSet Exp_dat = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getexpenseids", ref is_data);
                if (Exp_dat != null)
                {
                    DataTable Inc_Con_Dt = new DataTable();
                    DataTable ejm = new DataTable();
                    ejm = Exp_dat.Tables[0];
                    Inc_Con_Dt = Exp_dat.Tables[1];

                    html.Append("<div class='form-group'></div>");
                    html.Append("<div class='col-md-12'>");
                    html.Append("<div class='col-md-12'>");
                    html.Append("<table class='table table-bordered' id=tab_Exp" + index + ">");
                    html.Append("<thead><tr class='grey'>");
                    html.Append("<th>Add</th><th>Delete</th><th>Expense Head</th><th>Reimbursement Type</th><th>Expense Amount</th><th>Tax</th><th>Exchange Rate</th><th>Amount (<span id='spn_currency_amt" + index + "' runat=server'>USD</span>)</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Supporting Doc</th><th>Supporting Particulars</th><th>Remark</th>");
                    html.Append("</tr></thead>");
                    html.Append("<tbody>");
                    int ri = 0;

                    for (int i = 0; i < dsData.Tables[2].Rows.Count; i++)
                    {
                        if (Convert.ToString(dsData.Tables[2].Rows[i]["EXPENSE_HEAD"]) != "Conveyance")
                        {
                            if (Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") == Convert.ToDateTime(dsData.Tables[2].Rows[i]["fk_travel_date"]).ToString("dd-MMM-yyyy"))
                            {
                                if (Convert.ToString(dsData.Tables[2].Rows[i]["EXPENSE_HEAD"]) != "Conveyance" && Convert.ToString(dsData.Tables[2].Rows[i]["EXPENSE_HEAD"]) != "Incidental")
                                {
                                    for (int j = 0; j < ejm.Rows.Count; j++)
                                    {
                                        if (Convert.ToString(ejm.Rows[j]["FK_EXPENSE_HEAD"]) == Convert.ToString(dsData.Tables[2].Rows[i]["FK_EXPENSE_HEAD_ID"]))
                                        {
                                            Exp_html.Append("<option Value='" + Convert.ToString(ejm.Rows[j]["FK_EXPENSE_HEAD"]) + "' Selected='true'>" + Convert.ToString(ejm.Rows[j]["EXPENSE_HEAD"]) + "</option>");
                                        }
                                        else
                                        {
                                            Exp_html.Append("<option Value='" + Convert.ToString(ejm.Rows[j]["FK_EXPENSE_HEAD"]) + "' >" + Convert.ToString(ejm.Rows[j]["EXPENSE_HEAD"]) + "</option>");
                                        }
                                    }

                                }
                                rech = 0;
                                reim = new StringBuilder();
                                DataTable rt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreimbursements", ref is_data, "International Travel Expenses");
                                if (rt != null)
                                {
                                    for (int j = 0; j < rt.Rows.Count; j++)
                                    {
                                        if (Convert.ToString(rt.Rows[j]["PK_REIMBURSEMNT_ID"]) == Convert.ToString(dsData.Tables[2].Rows[i]["reim_type"]))
                                        {
                                            reim.Append("<option Value='" + Convert.ToString(rt.Rows[j]["PK_REIMBURSEMNT_ID"]) + "' Selected='true'>" + Convert.ToString(rt.Rows[j]["REIMBURSEMENT_TYPE"]) + "</option>");
                                            if (Convert.ToString(rt.Rows[j]["is_chargable"]) == "1")
                                            {
                                                rech = rech + 1;
                                            }
                                        }
                                        else
                                        {
                                            reim.Append("<option Value='" + Convert.ToString(rt.Rows[j]["PK_REIMBURSEMNT_ID"]) + "'>" + Convert.ToString(rt.Rows[j]["REIMBURSEMENT_TYPE"]) + "</option>");
                                        }
                                    }
                                }

                                html.Append("<tr>");
                                string cnt = "1";
                                string ival = "0";
                                string value = "";
                                string cond = "0";
                                if (Convert.ToString(dsData.Tables[2].Rows[i]["EXPENSE_HEAD"]).ToUpper() == "HOTEL")
                                {
                                    cond = "2";
                                }
                                else if (Convert.ToString(dsData.Tables[2].Rows[i]["EXPENSE_HEAD"]).ToUpper() == "BOARDING")
                                {
                                    cond = "3";
                                }
                                else if (Convert.ToString(dsData.Tables[2].Rows[i]["EXPENSE_HEAD"]).ToUpper() == "INCIDENTAL")
                                {
                                    cond = "1";
                                }
                                DataTable limit_Dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getincidentallimit", ref is_data, Convert.ToUInt32(country_id), Designation, cond, "");
                                if (limit_Dt.Rows.Count > 0 && limit_Dt != null)
                                {
                                    value = limit_Dt.Rows[0][0].ToString();
                                }
                                if (Convert.ToString(dsData.Tables[2].Rows[i]["EXPENSE_HEAD"]) == "Incidental")
                                {
                                    html.Append("<td><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus' id='Madd" + (index) + "" + (ri + 1) + "' style='display:none' onclick='Add_Multiple_Row(" + (index) + ")'></i></td><td class='delete'><i id='Mdelete" + (index) + "" + (ri + 1) + "' style='display:none' class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='Delete_Multiple_Row(" + (index) + "," + (ri + 1) + ")'></i></td>");
                                    html.Append("<td><label>" + Convert.ToString(dsData.Tables[2].Rows[i]["EXPENSE_HEAD"]) + "</label>(<span id='IS_SUP" + (index) + "" + (i + 1) + "'>" + Convert.ToString(dsData.Tables[2].Rows[i]["IS_SUPPORTING"]) + "</span>)");
                                    html.Append("<select ID='expense_id" + index + "_" + (ri + 1) + "' class='form-control input-sm' style='display:none' onchange=\"return get_Limit(" + index + "," + (ri + 1) + ");\" >");
                                    html.Append("<option Value='0'>---Select One---</option><option Value='" + Convert.ToString(dsData.Tables[2].Rows[i]["FK_EXPENSE_HEAD_ID"]) + "' Selected='true'>" + Convert.ToString(dsData.Tables[2].Rows[i]["EXPENSE_HEAD"]) + "</option></select>");
                                    html.Append("</td>");
                                }
                                else
                                {
                                    html.Append("<td><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus' id='Madd" + (index) + "" + (ri + 1) + "' onclick='Add_Multiple_Row(" + (index) + ")'></i></td><td class='delete'><i id='Mdelete" + (index) + "" + (ri + 1) + "' class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='Delete_Multiple_Row(" + (index) + "," + (ri + 1) + ")'></i></td>");
                                    html.Append("<td><select ID='expense_id" + (index) + "_" + (ri + 1) + "' class='form-control input-sm' onchange=\"return get_Limit(" + index + "," + (ri + 1) + ");\">");
                                    html.Append("<option Value='0'>---Select One---</option>");
                                    html.Append(Exp_html);
                                    html.Append("</select>(<span id='IS_SUP" + (index) + "" + (ri + 1) + "'>" + Convert.ToString(dsData.Tables[2].Rows[i]["IS_SUPPORTING"]) + "</span>)");
                                }
                                html.Append("<td>");
                                if (Convert.ToString(dsData.Tables[2].Rows[i]["IS_ALLOW"]) == "1" && cnt != "0")
                                {
                                    html.Append("<select ID='ddlrem_" + index + "_" + (ri + 1) + "' class='form-control input-sm' onchange=\"return change_flag(" + index + "," + (ri + 1) + ");\" >");
                                }
                                else
                                {
                                    html.Append("<select ID='ddlrem_" + index + "_" + (ri + 1) + "' class='form-control input-sm' onchange=\"return change_flag(" + index + "," + (ri + 1) + ");\" style='display:none'>");
                                }
                                html.Append("<option Value='0'>---Select One---</option>");
                                html.Append(reim);
                                html.Append("</select>");
                                if (rech == 0)
                                {
                                    html.Append("<input type='text' id='reim_val" + index + "_" + (ri + 1) + "' value='0' style='display:none' />");
                                }
                                else
                                {
                                    html.Append("<input type='text' id='reim_val" + index + "_" + (ri + 1) + "' value='1' style='display:none' />");
                                }
                                html.Append("</td>");
                                html.Append("<td style='text-align:right;'>");

                                if (Convert.ToString(dsData.Tables[2].Rows[i]["IS_ALLOW"]) == "1" && cnt != "0")
                                {
                                    html.Append("<input type='text' id='D_ALLOW_" + index + "_" + (ri + 1) + "' value='" + Convert.ToString(dsData.Tables[2].Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='txt_Expense_Amt" + index + "_" + (ri + 1) + "' value='" + dsData.Tables[2].Rows[i]["c_currency"] + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'/><input type='number' min='0' id='hlimit" + index + "_" + (ri + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                                }
                                else
                                {
                                    html.Append("<input type='text' id='D_ALLOW_" + index + "_" + (ri + 1) + "' value='" + Convert.ToString(dsData.Tables[2].Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='txt_Expense_Amt" + index + "_" + (ri + 1) + "' value='" + getIncidental(country_id, Designation, Ex_Rate) + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' readonly /><input type='number' min='0' id='hlimit" + index + "_" + (ri + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");

                                }
                                if (Convert.ToString(dsData.Tables[2].Rows[i]["IS_ALLOW"]) == "1" && cnt != "0")
                                {
                                    html.Append("<td><input type='text' id='Tax_" + index + "_" + (ri + 1) + "' value='" + dsData.Tables[2].Rows[i]["Tax"] + "'   class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;background-color:#ffffff' onkeyup='calculate_Amount();'></td>");
                                    html.Append("<td><input type='text' id='cur_" + index + "_" + (ri + 1) + "' value='" + dsData.Tables[2].Rows[i]["e_rate"] + "'  class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'></td>");
                                }
                                else
                                {
                                    html.Append("<td><input type='text' id='Tax_" + index + "_" + (ri + 1) + "' value=''   class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;display:none;background-color:#ffffff' onkeyup='calculate_Amount();'></td>");
                                    html.Append("<td><input type='text' id='cur_" + index + "_" + (ri + 1) + "' value='1' readonly=readonly class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'></td>");
                                }
                                html.Append("</td>");
                                if ((dsData.Tables[2].Rows[i]["REIMBURSEMENT_TYPE"].ToString() == "Corporate Credit Card") || (dsData.Tables[2].Rows[i]["REIMBURSEMENT_TYPE"].ToString() == "Personal Credit Card"))
                                {
                                    html.Append("<td><span id='dollar_" + index + "_" + (ri + 1) + "' style='display:none'>0</span><input type='text' id='dollar_txt" + index + "_" + (ri + 1) + "' value=" + Convert.ToDouble(dsData.Tables[2].Rows[i]["D_CURRENCY"]) + " class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'/></td>");
                                    html.Append("<td><span id='rupees_" + index + "_" + (ri + 1) + "' style='display:none'>0</span><input type='text' id='rupees_txt" + index + "_" + (ri + 1) + "' value=" + Convert.ToDouble(dsData.Tables[2].Rows[i]["I_CURRENCY"]) + " class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'/></td>");
                                }
                                else
                                {
                                    double e_curr = Convert.ToDouble(dsData.Tables[2].Rows[i]["c_currency"]);
                                    double ex_rate = Convert.ToDouble(dsData.Tables[2].Rows[i]["e_rate"]);
                                    double adv_rate = Convert.ToDouble(dsData.Tables[0].Rows[0]["exc_rate"]);
                                    double dollar = e_curr * ex_rate;
                                    double inr = dollar * adv_rate;
                                    html.Append("<td><span id='dollar_" + index + "_" + (ri + 1) + "'>" + dollar + "</span><input type='text' id='dollar_txt" + index + "_" + (ri + 1) + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;display:none' onkeyup='calculate_Amount();'/></td>");
                                    html.Append("<td><span id='rupees_" + index + "_" + (ri + 1) + "'>" + inr + "</span><input type='text' id='rupees_txt" + index + "_" + (ri + 1) + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;display:none' onkeyup='calculate_Amount();'/></td>");
                                }


                                if (Convert.ToString(dsData.Tables[2].Rows[i]["IS_SUPPORTING"]) == "Y")
                                {
                                    html.Append("<td>");
                                    html.Append("<select ID='ddl_SUP_" + index + "_" + (ri + 1) + "' class='form-control input-sm' onchange='enable_disable_field(" + index + "," + (ri + 1) + ")'>");
                                    if (Convert.ToString(dsData.Tables[2].Rows[i]["supp_doc1"]) == "YES")
                                    {
                                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N'>No</option></select>");
                                    }
                                    else
                                    {
                                        html.Append("<option Value='Y'>Yes</option><option Value='N' Selected='true'>No</option></select>");
                                    }
                                    html.Append("</td>");
                                    html.Append("<td>");
                                    if (Convert.ToString(dsData.Tables[2].Rows[i]["supp_doc1"]) == "YES")
                                    {
                                        html.Append("<input type='text' ID='particular_SUP_" + index + "_" + (ri + 1) + "' value='" + Convert.ToString(dsData.Tables[2].Rows[i]["supp_remark"]) + "' class='form-control input-sm'/><font ID='f_other_" + index + "_" + (ri + 1) + "' style='color:red'>*</font>");
                                    }
                                    else
                                    {
                                        html.Append("<input type='text' ID='particular_SUP_" + index + "_" + (ri + 1) + "' class='form-control input-sm' style='display:none'/><font ID='f_other_" + index + "_" + (ri + 1) + "' style='color:red; display:none'>*</font>");
                                    }
                                    html.Append("</td>");
                                }
                                else
                                {
                                    html.Append("<td>");
                                    html.Append("<select ID='ddl_SUP_" + index + "_" + (ri + 1) + "' class='form-control input-sm' style='display:none'><font style='display:none; color:red'>*</font>");
                                    html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N' Selected='true'>No</option></select>");
                                    html.Append("</td>");
                                    html.Append("<td>");
                                    html.Append("<input type='text' ID='particular_SUP_" + index + "_" + (ri + 1) + "' class='form-control input-sm' style='display:none'/><font ID='f_other_" + index + "_" + (ri + 1) + "' style='display:none; color:red'>*</font>");
                                    html.Append("</td>");
                                }
                                string other_remark = Convert.ToString(dsData.Tables[2].Rows[i]["IS_OTHER"]);
                                if (other_remark == "0")
                                {
                                    html.Append("<td><input type='text' ID='other_remark_" + index + "_" + (ri + 1) + "' class='form-control input-sm' style='display:none'/><input type='text' ID='fk_other_remark_" + index + "_" + (ri + 1) + "' value='0' class='form-control input-sm' style='display:none'/></td>");
                                }
                                else
                                {
                                    html.Append("<td><input type='text' ID='other_remark_" + index + "_" + (ri + 1) + "'  value='" + Convert.ToString(dsData.Tables[2].Rows[i]["OTHER_REMARK"]) + "' class='form-control input-sm'/><input type='text' ID='fk_other_remark_" + index + "_" + (ri + 1) + "' value='1' class='form-control input-sm' style='display:none'/></td>");
                                }
                                html.Append("</tr>");
                                ri = ri + 1;
                            }
                        }
                    }
                    html.Append("</tbody></table>");
                    html.Append("</div>");
                    html.Append("<div class='col-md-1'></div>");
                    html.Append("</div>");
                    html.Append("<div class='form-group'></div>");
                    exp_data = Convert.ToString(html);
                }
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return exp_data;
    }

    public static string getIncidental(int countryid, string designation, string Ex_Rate)
    {
        string limit = string.Empty;
        string is_data = string.Empty;
        DataTable limit_Dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getincidentallimit", ref is_data, countryid, designation.Trim(), 1, Ex_Rate);
        if (limit_Dt.Rows.Count > 0 && limit_Dt != null)
        {
            limit = limit_Dt.Rows[0][0].ToString();
        }
        else
        {
            limit = "0";
        }
        return limit;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string getDropDownData(int reimtype)
    {
        string dropData = "";
        string is_data = "";
        try
        {
            dropData = getReim(reimtype);
        }
        catch (Exception ex)
        {
            dropData = "<option Value='0'>---Select One---</option>";
        }
        return dropData;
    }

    public static string getReim(int reimtype)
    {
        string dropData = "<option Value='0'>---Select One---</option>";
        string is_data = "";
        try
        {
            DataTable rt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreimbursements", ref is_data, "International Travel Expenses");
            if (rt != null)
            {
                for (int i = 0; i < rt.Rows.Count; i++)
                {
                    if (reimtype == Convert.ToInt32(rt.Rows[i]["PK_REIMBURSEMNT_ID"]))
                    {
                        dropData += "<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "' Selected='true'>" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>";
                    }
                    else
                    {
                        dropData += "<option Value='" + Convert.ToString(rt.Rows[i]["PK_REIMBURSEMNT_ID"]) + "'>" + Convert.ToString(rt.Rows[i]["REIMBURSEMENT_TYPE"]) + "</option>";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            dropData = "<option Value='0'>---Select One---</option>";
        }
        return dropData;
    }
    #endregion


}
