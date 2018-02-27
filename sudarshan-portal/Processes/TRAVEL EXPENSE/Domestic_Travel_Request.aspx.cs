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

public partial class Domestic_Travel_Request : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Domestic_Travel_Request));
                if (!Page.IsPostBack)
                {

                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        if (Request.QueryString["processid"] != null && Request.QueryString["stepid"] != null)
                        {
                            txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                            txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);

                        }
                    }
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    travelFromDate.Attributes.Add("readonly", "true");
                    travelToDate.Attributes.Add("readonly", "true");
                    //span_vdate.InnerHtml = DateTime.Now.ToString("dd-MMM-yyyy");
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
            DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgettraveluser", ref isdata, txt_Username.Text);
            if (dtUser.Rows.Count > 0)
            {
                empno.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
                span_ename.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
                span_cc.InnerHtml = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
                span_dept.InnerHtml = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
                span_grade.InnerHtml = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
                span_mobile.InnerHtml = Convert.ToString(dtUser.Rows[0]["MOBILE_NO"]);
                Desg_Name.Text = span_designation.InnerHtml = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
                txt_designation.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
                if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]).Trim() != "")
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
                //span_Division.InnerHtml = "NA";
                span_Division.InnerHtml = Convert.ToString(dtUser.Rows[0]["DIVISION_NAME"]);
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
            }
            fillDocument_Details();
            fillAdvanceAmount();
            fillPayment_Mode();
            fillLocation();
            fillSupporting();
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
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
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgetadvancedetails", ref isValid, txt_Username.Text, "", 1);
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Advance Amount</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
                {
                    tblHTML.Append("<tr>");
                    //if (Index == 0)
                    //{
                    //    tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='travel' checked><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["PK_ADVANCE_HDR_Id"]) + "' style='display:none'></td>");
                    //}
                    //else
                    //{
                    //    tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='travel' ><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["PK_ADVANCE_HDR_Id"]) + "' style='display:none'></td>");
                    //}
                    tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='travel' ><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["PK_ADVANCE_HDR_Id"]) + "' style='display:none'><input type='text' id='advance_amount" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["AMOUNT"]) + "' style='display:none'></td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["ADVANCE_DATE"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["AMOUNT"]) + "<input type='text' id='adv_amount" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["AMOUNT"]) + "' style='display:none'></td>");
                    
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

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Type</th><th>File Name</th><th>Delete</th></tr></thead>";
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
    public static string get_Journey_Data(string jFDate, string jTDate)
    {
        string jdata = string.Empty;
        DateTime fdate = Convert.ToDateTime(jFDate);
        DateTime tdate = Convert.ToDateTime(jTDate);
        StringBuilder html = new StringBuilder();
        double noofdays = (tdate - fdate).TotalDays;
        try
        {
            string IsValid = string.Empty;
            int rno = 1;
            DataSet dtJourney = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "select", ref IsValid);
            while (fdate <= tdate)
            {

                html.Append("<div class='panel' id='remove_" + rno + "'>");
                html.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
                html.Append("<div class='panel-heading-btn'><div>Amount : <span id='Total" + rno + "'>0</span> <i class='fa fa-rupee'></i></div></div>");
                html.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' href='#collapse" + rno + "' onclick='copyData("+rno+")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spn_date_" + rno + "'>" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + "</span></a></h3></div>");

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
                html.Append("<select ID='Travel_Class" + rno + "' runat='server' class='form-control input-sm'>");
                html.Append("<option Value='0'>---Select One---</option>");
                html.Append("</select>");
                html.Append("</div></div></div>");


                html.Append("<div class='form-group'><div id='div_FPlant" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2' id='dest_plant"+rno+"'>Plant From</label><div class='col-md-3'>");
                html.Append("<select ID='From_Plant" + rno + "' runat='server' class='form-control input-sm' onchange='get_exp_data(" + rno + "); check_on_data("+rno+");'>");
                html.Append("<option Value='0'>---Select One---</option>");

                for (int i = 0; i < dtJourney.Tables[3].Rows.Count; i++)
                {
                    html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                }
                html.Append("</select>");
                html.Append("</div></div>");

                
                html.Append("<div id='div_TPlant" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Plant To</label><div class='col-md-3'>");
                html.Append("<select ID='To_Plant" + rno + "' runat='server' class='form-control input-sm' onchange='get_exp_data(" + rno + "); check_on_data(" + rno + ");'>");
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
                html.Append("<div class='col-md-3'><span id='city_class" + rno + "'>NA</span><span id='placeclass" + rno + "' style='display:none'>NA</span></div><div class='col-md-1'></div><label class='col-md-2' style='display:none'>Room Type</label>");
                html.Append("<div class='col-md-3' style='display:none'><select id='roomType" + rno + "' class='form-control input-sm'><option value='0'>---Select One---</option><option Selected='true' value='Single Bed Occupancy'>Single Bed Occupancy</option><option value='Double Bed Occupancy'>Double Bed Occupancy</option></select></div></div>");

                html.Append("<div class='form-group' id='div_HotelContact" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Hotel Name</label><div class='col-md-3'><input id='hotel_name" + rno + "' class='form-control input-sm' type='text'></div>");
                html.Append("<div class='col-md-1'></div><label class='col-md-2'>Hotel Contact No</label><div class='col-md-3'><input id='hotel_no" + rno + "' class='form-control input-sm' type='text'></div></div>");


                html.Append("<div id='exp_data" + rno + "'></div>");

                html.Append("</div></div></div></div></div>");

                rno = rno + 1;
                fdate = fdate.AddDays(1);
            }

            jdata = Convert.ToString(html);

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return jdata;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_Expense_Data(string desg, int j_val, int travel_mode_id, int travel_class_id, int fplant_id, int tplant_id, string pl_class, int index, int chk_box, string division)
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
                    html.Append("<input type='text' id='compare_id" + index + "" + (i + 1) + "' value='" + ejm.Rows[i]["COMPARE_ID"] + "' style='color:black; display:none'><input type='text' id='compare_name" + index + "" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["COMP_NAME"]).Replace(" ", "_") + "' style='color:black; display:none'><input type='text' name='eh" + index + "' id='expenses" + index + "" + (i + 1) + "' value='" + ejm.Rows[i]["FK_EXPENSE_HEAD"] + "' style='color:black; display:none'><input type='text' id='exp_name" + index + "" + (i + 1) + "' value='" + ejm.Rows[i]["EXPENSE_HEAD"] + "' style='color:black; display:none'><input type='text' id='e_name" + index + "" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "' style='color:black; display:none'><input type='text' id='e_fk_id" + index + "" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "' style='color:black; display:none'>");
                    html.Append("</td>");
                    //html.Append("<td>" + ejm.Rows[i]["SAP_GLCode"] + "</td>");
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
                        if (j_val != 3 && j_val != 2)
                        {
                            html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                        }
                        else
                        {
                            if (chk_box == 0 && j_val != 3 && j_val != 2)
                            {
                                html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='' class='form-control input-sm' style='text-align:right; display:none'>");
                            }
                            else
                            {
                                if (chk_box == 0 && Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"])=="Boarding")
                                {
                                    html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();' disabled><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                                }
                                else
                                {
                                    html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' onkeyup='calculate_Amount();'><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'>");
                                }
                            }
                        }
                    }
                    else
                    {
                        html.Append("<input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + Convert.ToString(ejm.Rows[i]["DIRECT_ALLOW"]) + "' style='display:none'><input type='text' id='" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + oval + "' class='form-control input-sm numbersOnly' style='text-align:right; padding-right:15px;' readonly><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[i]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='" + oval + "' class='form-control input-sm' style='text-align:right; display:none'>");
                        
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
                        html.Append("<select ID='ddl_SUP" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' style='display:none'><font style='display:none; color:red'>*</font>");
                        html.Append("<option Value='Y' Selected='true'>Yes</option><option Value='N' Selected='true'>No</option></select>");
                        html.Append("</td>");
                        html.Append("<td>");
                        html.Append("<input type='text' ID='particular_SUP" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' style='display:none'/><font ID='f_other" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' style='display:none; color:red'>*</font>");
                        html.Append("</td>");
                    }
                    string other_remark = Convert.ToString(ejm.Rows[i]["IS_OTHER"]);
                    if (other_remark == "0")
                    {
                        html.Append("<td><input type='text' ID='other_remark" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' style='display:none' value='NA'/><input type='text' ID='fk_other_remark" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='0' class='form-control input-sm' style='display:none'/></td>");
                    }
                    else
                    {
                        html.Append("<td><input type='text' ID='other_remark" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' class='form-control input-sm' value='NA'/><input type='text' ID='fk_other_remark" + Convert.ToString(ejm.Rows[i]["FK_EXPENSE_HEAD"]).Replace(" ", "_") + "_" + index + "_" + (i + 1) + "' value='1' class='form-control input-sm' style='display:none'/></td>");
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
            //if (travel_mode_id == -1)
            //{
            //    html = "0";
            //}
            //else
            //{
                DataTable ejm = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getpolicyclass", ref is_data, desg, j_val, travel_mode_id, travel_class_id);
                if (ejm != null)
                {
                    if (ejm.Rows.Count > 0)
                    {
                        html = "0";
                    }
                }
            //}
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return html;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string check_Dates(string jfdate,string jtdate)
    { 
        string isdata="";
        string chkData = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "checkdates", ref isdata, Convert.ToString(Session["USER_ADID"]), jfdate, jtdate,1,0);
        return chkData;
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

                string isdata = string.Empty;
                string isInserted = string.Empty;

                divIns.Style.Add("style", "none");
                string travel_voucher_id = "";
                string adv_pk_id = txt_advance_id.Text;
                string instanceID = (string)WFE.Action.StartCase(isInserted, txtProcessID.Text, txt_Username.Text, txt_Username.Text, txtEmailID.Text, "167");
                txtInstanceID.Text = instanceID;
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
                //if (Convert.ToInt32(empno.InnerHtml) != 4262 && Convert.ToInt32(empno.InnerHtml) != 4263)
                if (txt_Username.Text.ToUpper() != "RBRATHI" && txt_Username.Text.ToUpper() != "PRRATHI")
                {
                    isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "insert", ref isdata, travel_voucher_id, txt_Username.Text, fdate, tdate, xml_string, file_attach, txt_sub_xml_data.Text, txtProcessID.Text, txtInstanceID.Text, adv_loc, ddl_Payment_Mode.SelectedValue, txt_advance_id.Text, req_remark.Text, dev_travel_class.Text, dev_policy_amt.Text, dev_supp_amt.Text, span_bank_no.InnerHtml);
                    if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = isdata.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = span_Approver.InnerHtml;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "170", "TRAVEL EXPENSE REQUEST", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, Convert.ToString(isSaved), "0", ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
                                string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE REQUEST", "USER", txt_Username.Text, "SUBMIT", req_remark.Text, "0", "0");
                                string emailid = string.Empty;
                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Domestic Travel Expense Request Has Been Submitted Successfully.</font></pre><pre><font size='3'>Request No: " + isSaved + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE REQUEST", "SUBMIT", txt_Approver_Email.Text, txtEmailID.Text, msg, "Request No:" + isSaved);

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
                                //divIns.InnerHtml = "";
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Submitted Successfully and Request No. is : " + isSaved + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }

                    }
                }
                else
                {
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
                            isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "insert", ref isdata, travel_voucher_id, txt_Username.Text, fdate, tdate, xml_string, file_attach, txt_sub_xml_data.Text, txtProcessID.Text, txtInstanceID.Text, adv_loc, ddl_Payment_Mode.SelectedValue, txt_advance_id.Text, req_remark.Text, dev_travel_class.Text, dev_policy_amt.Text, dev_supp_amt.Text, span_bank_no.InnerHtml);
                            if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = isdata.Split(':');
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                            }
                            else
                            {
                                string[] Dval = new string[1];
                                Dval[0] = span_Approver.InnerHtml;
                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "170", "TRAVEL EXPENSE REQUEST", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, Convert.ToString(isSaved), "0", ref isInserted);
                                if (isCreate)
                                {
                                    try
                                    {
                                        string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE REQUEST", "USER", txt_Username.Text, "SUBMIT", req_remark.Text, "0", "0");
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
                                            isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "172", "TRAVEL EXPENSE APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, isSaved, wiid1, ref isInserted);
                                            if (isCreate)
                                            {
                                                try
                                                {
                                                    auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE APPROVAL", "USER", txt_Username.Text, "SUBMIT", "Send For Document Approval", "0", "0");
                                                    //string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Domestic Travel Request Has Been Approved Succefully and Sent For Document Verification.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                    string msg = "";
                                                    CryptoGraphy crypt = new CryptoGraphy();
                                                    string process_name = crypt.Encryptdata("TRAVEL EXPENSE");
                                                    string req_no = crypt.Encryptdata(isSaved);
                                                    if (ddl_Payment_Mode.Text.ToUpper() == "CASH")
                                                    {
                                                        msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Domestic Travel Request Has Been Approved Succefully and Sent For Document Verification.</span></pre><pre><span style='font-size: medium;'>Request No: " + isSaved + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                    }
                                                    else
                                                    {
                                                        msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Domestic Travel Request Has Been Approved Succefully and Sent For Document Verification.</span></pre><pre><span style='font-size: medium;'>Request No: " + isSaved + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                    }

                                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE APPROVAL", "SUBMIT", txtEmailID.Text, txtApproverEmail.Text, msg, "Request No: " + isSaved);

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
                                                catch (Exception)
                                                {
                                                    throw;
                                                }
                                                finally
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Has Been Submitted Successfully and Request No. is : " + isSaved + "...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            /*************************************************/

                                            string[] Dval1 = new string[1];
                                            Dval1[0] = txt_Username.Text;
                                            txt_Approver_Email.Text = txtEmailID.Text;
                                            isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "172", "TRAVEL EXPENSE APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, isSaved, wiid1, ref isInserted);
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
                                                    isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "175", "TRAVEL EXPENSE DEVIATION APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval2, isSaved, wiid2, ref isInserted);
                                                    if (isCreate)
                                                    {

                                                        string msg = "";
                                                        CryptoGraphy crypt = new CryptoGraphy();
                                                        string process_name = crypt.Encryptdata("TRAVEL EXPENSE");
                                                        string req_no = crypt.Encryptdata(isSaved);
                                                        if (ddl_Payment_Mode.Text.ToUpper() == "CASH")
                                                        {
                                                            msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Domestic Travel Request Has Been Approved Succefully and Sent For Document Verification.</span></pre><pre><span style='font-size: medium;'>Request No: " + isSaved + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                        }
                                                        else
                                                        {
                                                            msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Domestic Travel Request Has Been Approved Succefully and Sent For Document Verification.</span></pre><pre><span style='font-size: medium;'>Request No: " + isSaved + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                        }
                                                        auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE DEVIATION APPROVAL", "USER", txt_Username.Text, "SUBMIT", "Send For Document Approval", "0", "0");
                                                        string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE APPROVAL", "SUBMIT", txtEmailID.Text, txtApproverEmail.Text, msg, "Request No: " + isSaved);

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

                                                }
                                                catch (Exception)
                                                {
                                                    throw;
                                                }
                                                finally
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Has Been Approved Successfully and Sent For Document Verification...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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
                                        //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Submitted Successfully and Request No. is : " + isSaved + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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
                }

            }
        }
        catch (Exception Exc)
        {
            FSL.Logging.Logger.WriteEventLog(false, Exc);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;

            path = activeDir + "\\";

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
        Response.Redirect("../../Master.aspx?M_ID=2");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string isSaved = string.Empty;
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
		        div_Load.Style.Add("display","none");
                string isdata = string.Empty;
                string isInserted = string.Empty;
                string travel_voucher_id = "";
                string adv_pk_id = txt_advance_id.Text;
                string instanceID = (string)WFE.Action.StartCase(isInserted, txtProcessID.Text, txt_Username.Text, txt_Username.Text, txtEmailID.Text, "108");
                txtInstanceID.Text = instanceID;
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
                isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "insert", ref isdata, travel_voucher_id, txt_Username.Text, fdate, tdate, xml_string, file_attach, txt_sub_xml_data.Text, txtProcessID.Text, txtInstanceID.Text, adv_loc, ddl_Payment_Mode.SelectedValue, txt_advance_id.Text, req_remark.Text, dev_travel_class.Text, dev_policy_amt.Text, dev_supp_amt.Text, span_bank_no.InnerHtml);
                if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                {
                    string[] errmsg = isdata.Split(':');
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                }
                else
                {
                    string[] Dval = new string[1];
                    Dval[0] = txt_Username.Text;
                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "185", "TRAVEL EXPENSE REQUEST", "SAVE", "", txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, Convert.ToString(isSaved), "0", ref isInserted);
                    if (isCreate)
                    {
                        try
                        {
                            string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE REQUEST", "USER", txt_Username.Text, "SAVE", req_remark.Text, "0", "0");
                            string emailid = string.Empty;
                            //string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Domestic Travel Expense Request Has Been Successfully Saved In Draft</font></pre><p/><pre><font size='3'>Request No: " + isSaved + "</font></pre>";
                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Domestic Travel Expense Request Has Been Successfully Saved In Draft.</font></pre><pre><font size='3'>Request No: " + isSaved + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                            //emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE REQUEST", "SAVE", txtEmailID.Text, "", msg, "");

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
                            //divIns.InnerHtml = "";
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Domestic Travel Request Successfully Saved In Draft and Request No. is : " + isSaved + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                    //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Travel Request Applied Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                }

            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc);  }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string row_data(int rno, DateTime fdate, string jFDate, string jTDate)
    {
        StringBuilder html = new StringBuilder();
        string IsValid = "";
        DataSet dtJourney = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "select", ref IsValid);
        html.Append("<div class='panel' id='remove_"+rno+"'>");
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
        html.Append("<div class='col-md-3'><span id='placeclass" + rno + "' style='display:none'>NA</span> Class</div><div class='col-md-1'></div><label class='col-md-2' style='display:none'>Room Type</label>");
        html.Append("<div class='col-md-3' style='display:none'><select id='roomType" + rno + "' class='form-control input-sm'><option value='0'>---Select One---</option><option Selected='true' value='Single Bed Occupancy'>Single Bed Occupancy</option><option value='Double Bed Occupancy'>Double Bed Occupancy</option></select></div></div>");

        html.Append("<div class='form-group' id='div_HotelContact" + rno + "' style='display:none'><div class='col-md-1'></div><label class='col-md-2'>Hotel Name</label><div class='col-md-3'><input id='hotel_name" + rno + "' class='form-control input-sm' type='text'></div>");
        html.Append("<div class='col-md-1'></div><label class='col-md-2'>Hotel Contact No</label><div class='col-md-3'><input id='hotel_no" + rno + "' class='form-control input-sm' type='text'></div></div>");


        html.Append("<div id='exp_data" + rno + "'></div>");

        html.Append("</div></div></div></div></div>");
        return html.ToString();
    }

}