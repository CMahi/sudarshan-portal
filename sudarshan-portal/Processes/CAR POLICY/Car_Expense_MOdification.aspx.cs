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


public partial class Car_Expense_MOdification : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    StringBuilder str = new StringBuilder();
    ListItem Li = new ListItem("--Select One--", "0");
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Car_Expense_MOdification));
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
                    
                    getUserInfo();
                    bindData();
                    fillRemarkDtl();
                    fillDocument_Details();
                   
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
            string check = string.Empty;
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
                fillLocation(dsData.Rows[0]["FK_PAYMENT_LOCATION"].ToString());
                fillPayment_Mode(dsData.Rows[0]["FK_PAYMENT_MODE"].ToString());

                if (dsData.Rows[0]["FK_PAYMENT_MODE"].ToString() == "1")
                {
                    div_loc.Style["display"] = "";
                }
                else
                {

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

                    check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "check", ref isdata, span_grade.InnerHtml, txt_Username.Text, empno.InnerHtml, txt_designation.Text);
                    if (check != "")
                    {
                        string pol_ltrs = string.Empty;
                        string[] TempResultData = check.Split('~');
                        string user = TempResultData[0].ToString();
                        string Fuel = TempResultData[1].ToString();
                        string Maintenance = TempResultData[2].ToString();
                        string Driver = TempResultData[3].ToString();
                        txt_ex_grflg.Text = TempResultData[4].ToString();
                        txt_car_Age.Text = TempResultData[5].ToString();
                       // txt_CarAge1.Value = TempResultData[5].ToString();
                        if (TempResultData[6].ToString() != "0")
                            pol_ltrs = TempResultData[6].ToString();
                        txt_ex_grflg.Text = TempResultData[7].ToString();
                        txt_driversalary.Text = TempResultData[8].ToString();
                        if (user == "false")
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense claim cannot be submited ..!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                        }
                        if (Fuel == "false")
                        {
                            Li7.Style["display"] = "none";
                        }
                        else
                        {
                            StringBuilder html_Headerfuel = new StringBuilder();
                            html_Headerfuel.Append("<table class='table table-bordered' id='tblpolfuel' width='100%'>");
                            html_Headerfuel.Append("<thead><tr class='grey'><th style='text-align:center'>Grade</th><th style='text-align:center'>Fuel Eligibility per month in liters</th></tr></thead>");
                            html_Headerfuel.Append("<tbody>");
                            html_Headerfuel.Append("<tr><td style='text-align:center'>" + span_grade.InnerHtml + "</td><td style='text-align:center'>" + pol_ltrs + "</td></tr>");
                            html_Headerfuel.Append("</tbody></table>");
                            fuel_policy.InnerHtml = html_Headerfuel.ToString();
                        }
                        if (Maintenance == "false")
                        {
                            Li8.Style["display"] = "none";
                            Li1.Style["display"] = "none";
                            Li2.Style["display"] = "none";
                        }
                        if (Driver == "false")
                        {
                            Li9.Style["display"] = "none";
                        }
                       
                        
                        DataTable dtcar = (DataTable)ActionController.ExecuteAction("", "Car_Expense.aspx", "getcardtl", ref isdata, empno.InnerHtml);//empno.InnerHtml
                        if (dtcar.Rows.Count > 0)
                        {
                            txt_CarNumber.Text = Convert.ToString(dtcar.Rows[0]["Vehicle_No"]);
                            txt_CarDate.Text = Convert.ToString(dtcar.Rows[0]["PUT_Date"]);

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
                        else
                        {
                            Li8.Style["display"] = "none";
                            Li1.Style["display"] = "none";
                            Li2.Style["display"] = "none";
                        }
                        if (Int32.Parse(txt_car_Age.Text) > 6)
                        {
                            Li8.Style["display"] = "none";
                            Li1.Style["display"] = "none";
                            Li2.Style["display"] = "none";
                        }
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense claim cannot be submited ..!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                    }
                }

            }

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void fillPayment_Mode(string paymentmode)
    {
        try
        {
            
            ddl_Payment_Mode.SelectedValue = paymentmode.ToString();
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
    protected void fillLocation(string location)
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtLocation = (DataTable)ActionController.ExecuteAction("", "Car_Expense_MOdification.aspx", "selectdetails", ref isdata, txt_Username.Text, "AdLocation");
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
            ddlAdv_Location.Items.Insert(0, Li);
            ddlAdv_Location.SelectedValue = location.ToString();
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
    protected void bindData()
    {
        StringBuilder html_Header = new StringBuilder();
        StringBuilder html_Header1 = new StringBuilder();
        StringBuilder html_Header2 = new StringBuilder();
        StringBuilder html_Header3 = new StringBuilder();
        StringBuilder html_Header4 = new StringBuilder();
        StringBuilder html_Header5 = new StringBuilder();
        StringBuilder html_Header6 = new StringBuilder();
        try
        {
            string inco_terms = string.Empty;
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Car_Expense_MOdification.aspx", "getpkcarexpns", ref isdata, txtProcessID.Text, txtInstanceID.Text);
            isdata = "";
            DataSet ds = (DataSet)ActionController.ExecuteAction("", "Car_Expense_MOdification.aspx", "getcarexpnsdetails", ref isdata, dt.Rows[0]["PK_CAR_EXPNS_ID"].ToString());

            if (ds != null)
            {

                html_Header.Append("<table class='table table-bordered' id='tblfuel' width='100%'>");
                html_Header.Append("<tr style='background-color:grey; text-align:center; color:white;'><th>#</th><th style='text-align:center'>Date</th><th style='text-align:center'>Bill Details</th><th style='text-align:center'>Rate</th><th style='text-align:center'>Litre</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Add</th><th style='text-align:center'>Delete</th></tr>");
                html_Header.Append("<tbody>");


                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        html_Header.Append("<tr><td>" + (i + 1) + "</td><td style='text-align:center;width:15%;'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown' id='fuelDate" + (i + 1) + "' value='" + ds.Tables[0].Rows[i]["FUEL_DATE"] + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td><td style='text-align:center'><input class='form-control input-sm' type='text' id='fuelperticulars" + (i + 1) + "' value='" + ds.Tables[0].Rows[i]["PETROL_PUMP"] + "'></input></td><td style='text-align:center'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='rate" + (i + 1) + "' onchange='calculate(" + (i + 1) + ")' value='" + ds.Tables[0].Rows[i]["FUEL_RATE"] + "' ></input></td><td style='text-align:center'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='fuellitre" + (i + 1) + "' onchange='checkf(" + (i + 1) + ")' value='" + ds.Tables[0].Rows[i]["FUEL_LITRE"] + "' ></input></td><td style='text-align:right'><input class='form-control input-sm' style='text-align:right' onkeypress='return isNumberKey(event)' type='text' id='txtfuelamount" + (i + 1) + "'  runat='server' Value='" + ds.Tables[0].Rows[i]["AMOUNT"] + "' readonly='readonly' /></td><td class='add_Fuel'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td><td class='delete_Fuel' style='text-align:center;display:none;' ><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td><td style='text-align:center;display:none;'><input class='form-control input-sm' type='hidden' id='txtpk" + (i + 1) + "' value='" + ds.Tables[0].Rows[i]["PK_CAR_FUEL_ID"] + "'></input><input class='form-control input-sm' type='hidden' id='txtfk" + (i + 1) + "' value='" + ds.Tables[0].Rows[i]["FK_CAR_EXPNS_ID"] + "'></input><input class='form-control input-sm' type='hidden' id='txtaddnewrow" + (i + 1) + "' value=''></input></td></tr>");
                    }
                }

                else
                {
                    html_Header.Append("<tr><td>" + (1) + "</td>");
                    html_Header.Append("<td style='text-align:center;width:15%;'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown' id='fuelDate" + (1) + "' value='' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>");
                    html_Header.Append("<td style='text-align:center'><input class='form-control input-sm' type='text' id='fuelperticulars" + (1) + "' value=''></input></td>");
                    html_Header.Append("<td style='text-align:center'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='rate" + (1) + "' value='' onchange='calculate(" + (1) + ")' ></input></td>");
                    html_Header.Append("<td style='text-align:center'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='fuellitre" + (1) + "' value='' onchange='checkf(" + (1) + ")' ></input></td>");
                    html_Header.Append("<td style='text-align:right'><input class='form-control input-sm' style='text-align:right' onkeypress='return isNumberKey(event)' type='text' id='txtfuelamount" + (1) + "'  runat='server' Value='' readonly='readonly' /></td>");
                    html_Header.Append("<td class='add_Fuel'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>");
                    html_Header.Append("<td class='delete_Fuel' ><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i><input class='form-control input-sm' type='hidden' id='txtaddnewrow" + (1) + "' value='newRow'></input></td></tr>");

                }
                html_Header.Append("</tbody></table>");
                divfuel.InnerHtml = html_Header.ToString();

                html_Header1.Append("<table class='table table-bordered' id='tblmaintenance' width='100%'>");
                html_Header1.Append("<tr style='background-color:grey; text-align:center; color:white;'><th>#</th><th style='text-align:center'>Date</th><th style='text-align:center'>Car Age</th><th style='text-align:center'>Bill Details</th><th style='text-align:center'>Vehical No.</th><th style='text-align:center'>Date Of Purchase</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Add</th><th style='text-align:center'>Delete</th></tr>");
                html_Header1.Append("<tbody>");


                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                    {
                        html_Header1.Append("<tr><td>" + (j + 1) + "</td><td style='text-align:center;width:15%;'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='maitainancedate" + (j + 1) + "' value='" + ds.Tables[1].Rows[j]["MAINTAINCE_DATE"] + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td><td style='text-align:center'><input class='form-control input-sm' type='text' id='main_car_age" + (j + 1) + "' value='" + ds.Tables[1].Rows[j]["CAR_AGE"] + "' readonly></input></td><td style='text-align:center'><input class='form-control input-sm' type='text' id='main_particulars" + (j + 1) + "' value='" + ds.Tables[1].Rows[j]["BILL_DETAILS"] + "'></input></td><td style='text-align:center'><input class='form-control input-sm' type='text' id='vehical" + (j + 1) + "' value='" + ds.Tables[1].Rows[j]["VEHICLE_NO"] + "'></input></td><td style='text-align:center;width:15%'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='purachasedate" + (j + 1) + "' readonly value='" + ds.Tables[1].Rows[j]["DATE_OF_PURCHASE"] + "' /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td><td style='text-align:right'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='maintamount" + (j + 1) + "' onchange='checkm(" + (j + 1) + ")' value='" + ds.Tables[1].Rows[j]["AMOUNT"] + "'></input></td><td class='add_Maintenance'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td><td class='delete_Maitainance' style='text-align:center;display:none;'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td><td style='text-align:center;display:none;'><input class='form-control input-sm' type='hidden' id='txtpkmaintan" + (j + 1) + "' value='" + ds.Tables[1].Rows[j]["PK_CAR_MAINTENANCE_ID"] + "'></input><input class='form-control input-sm' type='hidden' id='txtfkmaintan" + (j + 1) + "' value='" + ds.Tables[1].Rows[j]["FK_CAR_EXPNS_ID"] + "'></input><input class='form-control input-sm' type='hidden' id='txtaddnewrowm" + (j + 1) + "' value=''></input></td></tr>");
                    }
                }
                else
                {
                    html_Header1.Append("<tr><td>" + (1) + "</td>");
                    html_Header1.Append("<td style='text-align:center;width:15%;'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='maitainancedate" + (1) + "' value='' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>");
                    html_Header1.Append("<td style='text-align:center'><input class='form-control input-sm' type='text' id='main_car_age" + (1) + "' value='" + txt_car_Age.Text + "' readonly></input></td>");
                    html_Header1.Append("<td style='text-align:center'><input class='form-control input-sm' type='text' id='main_particulars" + (1) + "' value=''></input></td>");
                    html_Header1.Append("<td style='text-align:center'><input class='form-control input-sm' type='text' id='vehical" + (1) + "' value='" + txt_CarNumber.Text + "'></input></td>");
                    html_Header1.Append("<td style='text-align:center;width:15%'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='purachasedate" + (1) + "' readonly value='" + txt_CarDate.Text + "' /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>");
                    html_Header1.Append("<td style='text-align:right'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='maintamount" + (1) + "' onchange='checkm(" + (1) + ")' value=''></input></td>");
                    html_Header1.Append("<td class='add_Maintenance'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>");
                    html_Header1.Append("<td class='delete_Maitainance'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i><input class='form-control input-sm' type='hidden' id='txtaddnewrowm" + (1) + "' value='newRowm'></input></td></tr>");
                }

                html_Header1.Append("</tbody></table>");
                divmaintenance.InnerHtml = html_Header1.ToString();

                html_Header2.Append("<table class='table table-bordered' id='tbldriver' width='100%'>");
                html_Header2.Append("<tr style='background-color:grey; text-align:center; color:white;'><th>#</th><th style='text-align:center'>Type</th><th style='text-align:center'>Date</th><th style='text-align:center'>Amount</th></tr>");
                html_Header2.Append("<tbody>");

                if (ds.Tables[2].Rows.Count > 0)
                {

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                        {
                            html_Header2.Append("<tr><td style='text-align:center;width:5%'>" + (k + 1) + "</td>");
                            html_Header2.Append("<td style='text-align:center;width:15%'><input type='text' class='form-control input-sm' style='text-align:left' id='txtdrivertype" + (k + 1) + "' readonly  runat='server' Value='" + ds.Tables[2].Rows[k]["DRIVER_TYPE"] + "'  /></td>");
                            html_Header2.Append("<td style='text-align:center;width:15%'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='driverdate" + (k + 1) + "' readonly value='" + ds.Tables[2].Rows[k]["DATE"] + "'  /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>");
                            html_Header2.Append("<td style='text-align:right;width:15%'><input type='text' class='form-control input-sm' style='text-align:right' onkeypress='return isNumberKey(event)' id='txtdriveramount" + (k + 1) + "'  runat='server' Value='" + ds.Tables[2].Rows[k]["AMOUNT"] + "'  onchange='checks(" + (k + 1) + ");' /></td>");
                            html_Header2.Append("<td style='text-align:center;display:none'><input class='form-control input-sm' type='hidden' id='txtpkdrv" + (k + 1) + "' value='" + ds.Tables[2].Rows[k]["PK_CAR_DRIVER_DTL_ID"] + "'></input><input class='form-control input-sm' type='hidden' id='txtfkdrv" + (k + 1) + "' value='" + ds.Tables[2].Rows[k]["FK_CAR_EXPNS_ID"] + "'></input><input class='form-control input-sm' type='hidden' id='txtaddnewrowd" + (k + 1) + "' value=''></input></td></tr>");
                        }
                    }
                }
                else
                {
                    html_Header2.Append("<tr><td style='text-align:center;width:5%'>" + (1) + "</td>");
                    html_Header2.Append("<td style='text-align:center;width:15%'><input type='text' class='form-control input-sm' style='text-align:left' id='txtdrivertype" + (1) + "'  runat='server' Value='Salary' readonly  /></td>");
                    html_Header2.Append("<td style='text-align:center;width:15%'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='driverdate" + (1) + "' readonly value='' /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>");
                    html_Header2.Append("<td style='text-align:center;width:15%'><input type='text' style='text-align:right' class='form-control input-sm' onkeypress='return isNumberKey(event)' id='txtdriveramount" + (1) + "'  runat='server' Value='0' onchange='checks(" + (1) + ");' /><input class='form-control input-sm' type='hidden' id='txtaddnewrowd" + (1) + "' value='newRowd'></input></td></tr>");

                    if (txt_uniformflg.Text == "false")
                    {
                        html_Header2.Append("<tr><td style='text-align:center;width:5%'>" + (2) + "</td>");
                        html_Header2.Append("<td style='text-align:center;width:15%'><input type='text' class='form-control input-sm' style='text-align:left' id='txtdrivertype" + (2) + "'  runat='server' Value='Uniform' readonly /></td>");
                        html_Header2.Append("<td style='text-align:center;width:15%'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='driverdate" + (2) + "' readonly value='' /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>");
                        html_Header2.Append("<td style='text-align:center;width:15%'><input type='text' style='text-align:right' class='form-control input-sm' onkeypress='return isNumberKey(event)' id='txtdriveramount" + (2) + "'  runat='server' Value='0' /><input class='form-control input-sm' type='hidden' id='txtaddnewrowd" + (2) + "' value='newRowd'></input></td></tr>");
                    }
                    if (txt_ex_grflg.Text == "false")
                    {
                        html_Header2.Append("<tr><td style='text-align:center;width:5%'>" + (3) + "</td>");
                        html_Header2.Append("<td style='text-align:center;width:15%'><input type='text' class='form-control input-sm' style='text-align:left' id='txtdrivertype" + (3) + "'  runat='server' Value='Ex-Gratia' readonly /></td>");
                        html_Header2.Append("<td style='text-align:center;width:15%'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='driverdate" + (3) + "' readonly value='' /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>");
                        html_Header2.Append("<td style='text-align:center;width:15%'><input type='text' style='text-align:right' class='form-control input-sm' onkeypress='return isNumberKey(event)' id='txtdriveramount" + (3) + "'  runat='server' Value='0' onchange='checkg(" + (1) + ");' /><input class='form-control input-sm' type='hidden' id='txtaddnewrowd" + (3) + "' value='newRowd'></input></td></tr>");
                    }
                }
                html_Header2.Append("</tbody></table>");
                div_driver.InnerHtml = html_Header2.ToString();


                html_Header5.Append("<table class='table table-bordered' id='tbltyre' width='100%'>");
                html_Header5.Append("<tr style='background-color:grey; text-align:center; color:white;'><th>#</th><th style='text-align:center'>Car Age</th><th style='text-align:center'>Date</th><th style='text-align:center'>Bill Details</th><th style='text-align:center'>KM Threshold Corssed </th><th style='text-align:center'>Kilometers</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Add</th><th style='text-align:center'>Delete</th></tr>");
                html_Header5.Append("<tbody>");

                string carage = txt_car_Age.Text;
                if (ds.Tables[3].Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Tables[3].Rows.Count; j++)
                    {
                        html_Header5.Append("<tr><td>" + (j + 1) + "</td>");
                        html_Header5.Append("<td style='text-align:right'><input class='form-control input-sm' type='text' value='" + carage + "' readonly ></input></td>");
                        html_Header5.Append("<td style='text-align:center;width:30%;'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='txt_tyredate_" + (j + 1) + "' value='" + ds.Tables[3].Rows[j]["TYRE_DATE"] + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>");
                        html_Header5.Append("<td style='text-align:center'><input class='form-control input-sm' type='text' style='text-align:center' id='tyre_details" + (j + 1) + "' value='" + ds.Tables[3].Rows[j]["BILL_DETAILS"] + "'></td>");
                        if (ds.Tables[3].Rows[j]["KM_THREHOLD_CROSEED"].ToString() == "Yes")
                            html_Header5.Append("<td style='text-align:center'><input id='km_chk" + (j + 1) + "' checked value='' type='checkbox'></td>");
                        else
                            html_Header5.Append("<td style='text-align:center'><input id='km_chk" + (j + 1) + "' value='' type='checkbox'></td>");
                        html_Header5.Append("<td style='text-align:center'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='txt_km" + (j + 1) + "' value='" + ds.Tables[3].Rows[j]["KILO_METRES"] + "' onblur='checkt(" + (j + 1) + ");'></td>");
                        html_Header5.Append("<td style='text-align:right'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='txt_amount_" + (j + 1) + "' value='" + ds.Tables[3].Rows[j]["AMOUNT"] + "' onblur='checkt(" + (1) + ");'></input></td>");
                        html_Header5.Append("<td class='add_tyre'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>");
                        html_Header5.Append("<td class='delete_tyre' style='text-align:center;display:none;'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td>");
                        html_Header5.Append("</tr>");
                    }
                }
                else
                {
                    html_Header5.Append("<tr><td>" + (1) + "</td>");
                    html_Header5.Append("<td style='text-align:right'><input class='form-control input-sm' type='text' value='" + carage + "' readonly ></input></td>");
                    html_Header5.Append("<td style='text-align:center;width:30%;'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='txt_tyredate_" + (1) + "' value='' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>");
                    html_Header5.Append("<td style='text-align:center'><input id='tyre_details" + (1) + "' value='' type='text' class='form-control input-sm'></td>");
                    html_Header5.Append("<td style='text-align:center'><input id='km_chk" + ( 1) + "' value='' type='checkbox'></td>");
                    html_Header5.Append("<td style='text-align:center'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='txt_km" + (1) + "' value='' onblur='checkt(" + ( 1) + ");'></td>");
                    html_Header5.Append("<td style='text-align:right'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='txt_amount_" + (1) + "' value='' onblur='checkt(" + (1) + ");'></input></td>");
                    html_Header5.Append("<td class='add_tyre'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>");
                    html_Header5.Append("<td class='delete_tyre' style='text-align:center;display:none;'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td>");
                    html_Header5.Append("</tr>");
                }
               
                html_Header5.Append("</tbody></table>");
                dv_tyre.InnerHtml = html_Header5.ToString();

                html_Header6.Append("<table class='table table-bordered' id='tblbattery' width='100%'>");
                html_Header6.Append("<tr style='background-color:grey; text-align:center; color:white;'><th>#</th><th style='text-align:center'>Date</th><th style='text-align:center'>Bill Details</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Add</th><th style='text-align:center'>Delete</th></tr>");
                html_Header6.Append("<tbody>");


                if (ds.Tables[4].Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Tables[4].Rows.Count; j++)
                    {
                        html_Header6.Append("<tr><td>" + (j + 1) + "</td>");
                        html_Header6.Append("<td style='text-align:center;width:30%;'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='txt_batterydt" + (j + 1) + "' value='" + ds.Tables[4].Rows[j]["BATTERY_DATE"] + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>");
                        html_Header6.Append("<td style='text-align:right'><input class='form-control input-sm' type='text' style='text-align:right' id='battery_particulars" + (j + 1) + "' value='" + ds.Tables[4].Rows[j]["BILL_DETAILS"] + "' ></input></td>");
                        html_Header6.Append("<td style='text-align:right'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='txt_batteryamt" + (j + 1) + "' value='" + ds.Tables[4].Rows[j]["AMOUNT"] + "' onblur='checkb(" + (j + 1) + ");'></input></td>");
                        html_Header6.Append("<td class='add_battery'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>");
                        html_Header6.Append("<td class='delete_battery' style='text-align:center;display:none;'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td>");
                        html_Header6.Append("</tr>");
                    }
                }
                else
                {
                    html_Header6.Append("<tr><td>" + (1) + "</td>");
                    html_Header6.Append("<td style='text-align:center;width:30%;'><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='txt_batterydt" + (1) + "' value='' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>");
                    html_Header6.Append("<td style='text-align:right'><input class='form-control input-sm' type='text' style='text-align:right' id='battery_particulars" + (1) + "' value='' ></input></td>");
                    html_Header6.Append("<td style='text-align:right'><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='txt_batteryamt" + (1) + "' value='' onblur='checkb(" + (1) + ");'></input></td>");
                    html_Header6.Append("<td class='add_battery'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>");
                    html_Header6.Append("<td class='delete_battery' style='text-align:center;display:none;'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td>");
                    html_Header6.Append("</tr>");
                }

                html_Header6.Append("</tbody></table>");
                dv_battery.InnerHtml = html_Header6.ToString();
            }


            isdata = "";


        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
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
                DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Car_Expense_MOdification.aspx", "getfilenames", ref isValid, "CAR POLICY", spn_req_no.InnerHtml);
                DisplayData = "<table class='table table-bordered' id='tbl_DocumentDtl'><thead><tr class='grey'><th>File Type</th><th>File Name</th><th></th></tr></thead>";
                if (dsData != null)
                {
                    for (int i = 0; i < dsData.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dsData.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a onclick='downloadfiles(" + (i + 1) + ")'>" + Convert.ToString(dsData.Rows[i]["FILENAME"]) + "</a></td><td><i id='del" + dsData.Rows.Count + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (i + 1) + "," + (dsData.Rows[i]["PK_ID"].ToString()) + ");\" ><input id='txtnewfile" + (i + 1) + "' type='hidden' value=''  class='form-control'/></td></tr>";
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
                string remark = txt_Remark.Text;
                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;

                string isdata = string.Empty;
                string isSaved = string.Empty;

                string fuelxml_string = txt_xml_data_fuel.Text;
                fuelxml_string = fuelxml_string.Replace("&", "&amp;");
                fuelxml_string = fuelxml_string.Replace(">", "&gt;");
                fuelxml_string = fuelxml_string.Replace("<", "&lt;");
                fuelxml_string = fuelxml_string.Replace("||", ">");
                fuelxml_string = fuelxml_string.Replace("|", "<");
                fuelxml_string = fuelxml_string.Replace("'", "&apos;");
                txt_xml_data_fuel.Text = fuelxml_string.ToString();

                string fuelxml_string_upd = txt_xml_data_fuel_upd.Text;
                fuelxml_string_upd = fuelxml_string_upd.Replace("&", "&amp;");
                fuelxml_string_upd = fuelxml_string_upd.Replace(">", "&gt;");
                fuelxml_string_upd = fuelxml_string_upd.Replace("<", "&lt;");
                fuelxml_string_upd = fuelxml_string_upd.Replace("||", ">");
                fuelxml_string_upd = fuelxml_string_upd.Replace("|", "<");
                fuelxml_string_upd = fuelxml_string_upd.Replace("'", "&apos;");
                txt_xml_data_fuel_upd.Text = fuelxml_string_upd.ToString();

                string maintainancexml_string = txt_xml_data_maitainance.Text;

                maintainancexml_string = maintainancexml_string.Replace("&", "&amp;");
                maintainancexml_string = maintainancexml_string.Replace(">", "&gt;");
                maintainancexml_string = maintainancexml_string.Replace("<", "&lt;");
                maintainancexml_string = maintainancexml_string.Replace("||", ">");
                maintainancexml_string = maintainancexml_string.Replace("|", "<");
                maintainancexml_string = maintainancexml_string.Replace("'", "&apos;");
                txt_xml_data_maitainance.Text = maintainancexml_string.ToString();

                string maintainancexml_string_upd = txt_xml_data_maitainance_upd.Text;

                maintainancexml_string_upd = maintainancexml_string_upd.Replace("&", "&amp;");
                maintainancexml_string_upd = maintainancexml_string_upd.Replace(">", "&gt;");
                maintainancexml_string_upd = maintainancexml_string_upd.Replace("<", "&lt;");
                maintainancexml_string_upd = maintainancexml_string_upd.Replace("||", ">");
                maintainancexml_string_upd = maintainancexml_string_upd.Replace("|", "<");
                maintainancexml_string_upd = maintainancexml_string_upd.Replace("'", "&apos;");
                txt_xml_data_maitainance_upd.Text = maintainancexml_string_upd.ToString();

                //string driverxml_string = txt_xml_data_driver.Text;
                string driverxml_string = txt_xml_data_driver.Text;
                driverxml_string = driverxml_string.Replace("&", "&amp;");
                driverxml_string = driverxml_string.Replace(">", "&gt;");
                driverxml_string = driverxml_string.Replace("<", "&lt;");
                driverxml_string = driverxml_string.Replace("||", ">");
                driverxml_string = driverxml_string.Replace("|", "<");
                driverxml_string = driverxml_string.Replace("'", "&apos;");
                txt_xml_data_driver.Text = driverxml_string.ToString();

                string driverxml_string_upd = txt_xml_data_driver_upd.Text;

                driverxml_string_upd = driverxml_string_upd.Replace("&", "&amp;");
                driverxml_string_upd = driverxml_string_upd.Replace(">", "&gt;");
                driverxml_string_upd = driverxml_string_upd.Replace("<", "&lt;");
                driverxml_string_upd = driverxml_string_upd.Replace("||", ">");
                driverxml_string_upd = driverxml_string_upd.Replace("|", "<");
                driverxml_string_upd = driverxml_string_upd.Replace("'", "&apos;");
                txt_xml_data_driver_upd.Text = driverxml_string_upd.ToString();

                string tyrexml_string = txt_xml_data_tyre.Text;

                tyrexml_string = tyrexml_string.Replace("&", "&amp;");
                tyrexml_string = tyrexml_string.Replace(">", "&gt;");
                tyrexml_string = tyrexml_string.Replace("<", "&lt;");
                tyrexml_string = tyrexml_string.Replace("||", ">");
                tyrexml_string = tyrexml_string.Replace("|", "<");
                tyrexml_string = tyrexml_string.Replace("'", "&apos;");
                txt_xml_data_tyre.Text = tyrexml_string.ToString();

                string batteryexml_string = txt_xml_data_battery.Text;

                batteryexml_string = batteryexml_string.Replace("&", "&amp;");
                batteryexml_string = batteryexml_string.Replace(">", "&gt;");
                batteryexml_string = batteryexml_string.Replace("<", "&lt;");
                batteryexml_string = batteryexml_string.Replace("||", ">");
                batteryexml_string = batteryexml_string.Replace("|", "<");
                batteryexml_string = batteryexml_string.Replace("'", "&apos;");
                txt_xml_data_battery.Text = batteryexml_string.ToString();

                string FILEXML = Txt_File_xml.Text;

                FILEXML = FILEXML.Replace("&", "&amp;");
                FILEXML = FILEXML.Replace(">", "&gt;");
                FILEXML = FILEXML.Replace("<", "&lt;");
                FILEXML = FILEXML.Replace("||", ">");
                FILEXML = FILEXML.Replace("|", "<");
                FILEXML = FILEXML.Replace("'", "&apos;");
                Txt_File_xml.Text = FILEXML.ToString();

                if (ddl_Payment_Mode.SelectedValue == "2")
                {
                    ddlAdv_Location.SelectedValue = "0";
                }

                DataTable dtRole = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getdocapprover", ref isInserted, "CAR EXPENSE PAYMENT APPROVAL");
                if (dtRole != null && dtRole.Rows.Count > 0)
                {
                    string[] Dval = new string[dtRole.Rows.Count];
                    string Emailids = "";
                    for (int i = 0; i < dtRole.Rows.Count; i++)
                    {
                        Dval[i] = dtRole.Rows[i]["USER_ADID"].ToString().Trim();
                        if (Emailids == "")
                        {
                            Emailids = dtRole.Rows[i]["EMAIL_ID"].ToString().Trim();
                        }
                        else
                        {
                            Emailids = Emailids + "," + dtRole.Rows[i]["EMAIL_ID"].ToString().Trim();
                        }
                    }
                    isSaved = (string)ActionController.ExecuteAction("", "Car_Expense_MOdification.aspx", "update", ref isdata, txtProcessID.Text, txtInstanceID.Text, txt_Username.Text, txt_xml_data_fuel.Text, txt_xml_data_maitainance.Text, txt_xml_data_driver.Text, Txt_File_xml.Text, txtexpnsamt.Text, txt_xml_data_fuel_upd.Text, txt_xml_data_maitainance_upd.Text, txt_xml_data_driver_upd.Text, remark, ddl_Payment_Mode.SelectedValue, ddlAdv_Location.SelectedValue, txt_fuel_flag.Text, txt_maintain_flag.Text, txt_xml_data_tyre.Text, txt_xml_data_battery.Text);
                    //string isSaved = (string)ActionController.ExecuteAction("", "Car_Expense_MOdification.aspx", "update", ref refData, txtProcessID.Text, txtInstanceID.Text, remark, txt_Username.Text,"4");
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = refData.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                       
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "226", "CAR EXPENSE MODIFICATION", "SUBMIT", "", txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_req_no.InnerHtml, txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
                                CryptoGraphy crypt = new CryptoGraphy();
                                string process_name = "CAR POLICY";
                                string req_no = spn_req_no.InnerHtml;

                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/><pre><font size='3'>The Car Expense request modified suucessfully and sent for your Approval.</font></pre></p><pre><font size='3'>Car Expense No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre><span style='font-size: medium;'>INTRANET URL:http://"+compname+"/Sudarshan-Portal/Vouchers/Car_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre></b><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";
                                
                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "CAR EXPENSE REQUEST APPROVAL", "SUBMIT", Emailids, txtEmailID.Text, msg, "Car Expense No: " + spn_req_no.InnerHtml);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense Request has been Submited...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;
            string foldername = txt_Request.Text.ToString();
            foldername = foldername.Replace("/", "_");
            string path = string.Empty;
            path = activeDir + "\\" + foldername + "\\";

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
            DataTable dt = (DataTable)Session["UploadedFiles"];

            FileUpload1.SaveAs(path + filename);

            //   ClearContents(sender as Control);

        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string deletedoctbl(int pkid)
    {
        string filename = string.Empty;
        string isdata = string.Empty;
        try
        {
            filename = (string)ActionController.ExecuteAction("", "Car_Expense_MOdification.aspx", "deletefile", ref isdata, pkid);
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return filename;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string checkfule(int ltr, string grad, string adid, int desig, string date, int id)
    {
        string check = string.Empty;
        string isdata = string.Empty;
        try
        {
            check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "getfule", ref isdata, ltr, grad, adid, date, desig);
            check = check + "#" + id;
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return check;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string checkm(int amt, string grad, string adid, int desig, string date, string empno, int id)
    {
        string check = string.Empty;
        string isdata = string.Empty;
        try
        {
            check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "getm", ref isdata, amt, grad, adid, date, desig, "4263");
            check = check + "#" + id;
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return check;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string checkb(string date, string adid, int amt, int id)
    {
        string check = string.Empty;
        string isdata = string.Empty;
        try
        {
            check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "getb", ref isdata, amt, adid, date);
            check = check + "#" + id;
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return check;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string checks(string date, string grad, string adid, int desig)
    {
        string check = string.Empty;
        string isdata = string.Empty;
        try
        {
            check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "gets", ref isdata, date, grad, adid, desig);

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return check;
    }
}