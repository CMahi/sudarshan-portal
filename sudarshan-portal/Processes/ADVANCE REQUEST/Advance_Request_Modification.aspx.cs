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

public partial class Advance_Request_Modification : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    StringBuilder str = new StringBuilder();
    ListItem Li = new ListItem("--Select One--", "0");
    DataTable dt = new DataTable();
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Request_Modification));
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
                    Initialization();
                   
                    //FillCity();
                    //FillMode();
                    //FillLocation();
                    //showDetails();
                    fillAuditTrailData();
                    fillDocument_Details();
                   
                    //fillPolicy_Details();
                }

            }
        }
        catch (Exception Exc) { }
    }
    //private void fillPolicy_Details()
    //{
    //    if (ActionController.IsSessionExpired(Page))
    //        ActionController.RedirctToLogin(Page);
    //    else
    //    {
    //        try
    //        {
    //            string isData = string.Empty;
    //            string isValid = string.Empty;
    //            string DisplayData = string.Empty;

    //            DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref isValid, span_designation.InnerHtml, "AdDesignation");
    //            if (dtamt != null && dtamt.Rows.Count > 0)
    //            {
    //                DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Designation</th><th>City Class</th><th>Amount(Rs)</th><th>Effective Date</th></tr></thead>";

    //                for (int i = 0; i < dtamt.Rows.Count; i++)
    //                {
    //                    DisplayData += "<tr><td>" + span_designation.InnerHtml + "</td><td>" + Convert.ToString(dtamt.Rows[i]["CITY_CLASS"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["EFFECTIVE_DATE"]) + "</td></tr>";

    //                }
    //                DisplayData += "</table>";
    //            }

    //            div_policy.InnerHtml = DisplayData;
    //            DisplayData = "";
    //        }
    //        catch (Exception ex)
    //        {
    //            FSL.Logging.Logger.WriteEventLog(false, ex);
    //        }
    //    }
    //}
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

                DataTable dtd = (DataTable)Session["dtdoc"];

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>Description</th><th>File Name</th><th>Delete</th></tr></thead>";
                if (dtd != null && dtd.Rows.Count > 0)
                {
                    for (int i = 0; i < dtd.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dtd.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles(" + (i + 1) + ")>" + Convert.ToString(dtd.Rows[i]["filename"]) + "</a></td><td><i id='del" + (i + 1) + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (i + 1) + ");\" ></td></tr>";
                    }
                }
                DisplayData += "</table>";
                div_Doc.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }
    //private void FillMode()
    //{
    //    String IsData = string.Empty;
    //    dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdPaymentMode");
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ddl_Payment_Mode.DataSource = dt;
    //        ddl_Payment_Mode.DataTextField = "PAYMENT_MODE";
    //        ddl_Payment_Mode.DataValueField = "PK_PAYMENT_MODE";
    //        ddl_Payment_Mode.DataBind();
    //        ddl_Payment_Mode.Items.Insert(0, Li);
    //    }

    //}
    //private void FillLocation()
    //{
    //    String IsData = string.Empty;
    //    dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdLocation");
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ddlAdv_Location.DataSource = dt;
    //        ddlAdv_Location.DataTextField = "LOCATION_NAME";
    //        ddlAdv_Location.DataValueField = "PK_LOCATION_ID";
    //        ddlAdv_Location.DataBind();
    //        ddlAdv_Location.Items.Insert(0, Li);
    //    }

    //}

    //private void FillType()
    //{
    //    //ddlType.Items.Insert(0, new ListItem("--Select One--", "0"));
    //    ddlType.Items.Insert(0, new ListItem("Domestic", "Domestic"));
    //    //ddlType.Items.Insert(2, new ListItem("International", "International"));
    //}
    //private void FillCountry()
    //{
    //    //String IsData = string.Empty;
    //    //dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref IsData, ddlType.SelectedValue, "AdCountry");
    //    //if (dt != null && dt.Rows.Count > 0)
    //    //{
    //    //    ddlCountry.DataSource = dt;
    //    //    ddlCountry.DataTextField = "COUNTRY_NAME";
    //    //    ddlCountry.DataValueField = "PK_COUNTRY_ID";
    //    //    ddlCountry.DataBind();
    //    //    ddlCountry.Items.Insert(0, Li);

    //    //}
    //    ddlCountry.Items.Insert(0, new ListItem("India", "1"));
    //}
    //private void FillCity()
    //{
    //    String IsData = string.Empty;
    //    dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref IsData, "", "AdCity");
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        //ddlFromPlace.DataSource = dt;
    //        //ddlFromPlace.DataTextField = "NAME";
    //        //ddlFromPlace.DataValueField = "PK_CITY_ID";
    //        //ddlFromPlace.DataBind();
    //        //ddlFromPlace.Items.Insert(0, new ListItem("--Select One--", "0"));
    //        //ddlFromPlace.Items.Insert((dt.Rows.Count), new ListItem("Other", "-1"));
    //        //ddlToPlace.DataSource = dt;
    //        //ddlToPlace.DataTextField = "NAME";
    //        //ddlToPlace.DataValueField = "PK_CITY_ID";
    //        //ddlToPlace.DataBind();
    //        //ddlToPlace.Items.Insert(0, new ListItem("--Select One--", "0"));
    //        //ddlToPlace.Items.Insert((dt.Rows.Count), new ListItem("Other", "-1"));
    //    }
    //}
    private void Initialization()
    {
        try
        {
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getdetails", ref isdata, txtProcessID.Text, txtInstanceID.Text);
            Session["dtdoc"] = dsData.Tables[1];
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_ADVANCE_HDR_Id"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                txt_Initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMAIL_ID"]);
                //txt_remark.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REMARK"]);
                span_advance.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["advance_type_name"]);
                if (span_advance.InnerHtml == "Other Expense Advance")
                {
                    advance_travel.Visible = false;
                    advance_other.Visible = true;
                }
                else
                {
                    advance_travel.Visible = true;
                    advance_other.Visible = false;
                    //fillPolicy_Details();
                }
                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "pgettraveluser", ref isdata, txt_Initiator.Text);
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
                   // span_division.InnerHtml = "NA";
		span_division.InnerHtml = Convert.ToString(dtUser.Rows[0]["DIVISION_NAME"]);
                spn_base_location.InnerHtml = Convert.ToString(dtUser.Rows[0]["BASE_LOCATION"]);

                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "pgettravelrequestapprover", ref isdata, txt_Initiator.Text);
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
                    }
                    fillData(Convert.ToString(dsData.Tables[0].Rows[0]["advance_for"]), dsData.Tables[0]);
                    FillAdvanceDetails();
                    ddlAdv_Location.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["LOCATION"]);
                    ddl_Payment_Mode.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["PAYMENT_MODE"]);
                    string ISValid = string.Empty;
                    string str = string.Empty;
                   // DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref ISValid, span_advance.InnerHtml, "AdGLcode");
                   // showall(Convert.ToString(dsData.Tables[0].Rows[0]["advance_for"]));
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }

    }

    private void FillAdvanceDetails()
    {
        try
        {
            String IsData = string.Empty;
            string isValid = string.Empty;
            string DisplayData = string.Empty;
            DataSet ds = (DataSet)ActionController.ExecuteAction("", "Advance_Request.aspx", "advdetails", ref IsData, span_grade.InnerHtml, txt_Username.Text);
            
            DataTable dtm = ds.Tables[2];
            DataTable dtl = ds.Tables[3];
            DataTable dtamt = ds.Tables[4];
            DataTable dt = ds.Tables[5];
           
            
            if (dtm != null && dtm.Rows.Count > 0)
            {
                ddl_Payment_Mode.DataSource = dtm;
                ddl_Payment_Mode.DataTextField = "PAYMENT_MODE";
                ddl_Payment_Mode.DataValueField = "PK_PAYMENT_MODE";
                ddl_Payment_Mode.DataBind();
                ddl_Payment_Mode.Items.Insert(0, Li);
            }
            if (dtl != null && dtl.Rows.Count > 0)
            {
                ddlAdv_Location.DataSource = dtl;
                ddlAdv_Location.DataTextField = "LOCATION_NAME";
                ddlAdv_Location.DataValueField = "PK_LOCATION_ID";
                ddlAdv_Location.DataBind();
                ddlAdv_Location.Items.Insert(0, Li);
            }

            if (dtamt != null)
            {
		string Class= string.Empty;
                if (dtamt != null && dtamt.Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Grade</th><th>City Class</th><th>Amount(Rs)</th><th>Effective Date</th></tr></thead>";

                    for (int i = 0; i < dtamt.Rows.Count; i++)
                    {
		if (Convert.ToString(dtamt.Rows[i]["CITY_CLASS"])=="A")
		{
		  Class="Metro";
		}
		else if (Convert.ToString(dtamt.Rows[i]["CITY_CLASS"])=="B")
		{
		  Class="Min-Metro";
		}
		else if (Convert.ToString(dtamt.Rows[i]["CITY_CLASS"])=="C")
		{
		  Class="Other";
		}
                        DisplayData += "<tr><td>" + span_grade.InnerHtml + "</td><td>" + Class + "</td><td>" + Convert.ToString(dtamt.Rows[i]["AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["EFFECTIVE_DATE"]) + "</td></tr>";

                    }
                    DisplayData += "</table>";
                    
                }
               
                div_policy.InnerHtml = DisplayData;
                DisplayData = "";
            }


            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["PK_EXPENSE_ID"].ToString() == "1004")
                    {
                        txt_adamount.Text = (dt.Rows[i]["VALUE"].ToString());
                    }
                    if (dt.Rows[i]["PK_EXPENSE_ID"].ToString() == "1006")
                    {
                        if ((dt.Rows[i]["VALUE"]) != null || (dt.Rows[i]["VALUE"]) != "undefined")
                        {
                            txt_domeopen.Text = (dt.Rows[i]["VALUE"].ToString());
                        }
                    }
                    if (dt.Rows[i]["PK_EXPENSE_ID"].ToString() == "1005")
                    {
                        txt_adperiod.Text = (dt.Rows[i]["VALUE"].ToString());
                    }
                    if (dt.Rows[i]["PK_EXPENSE_ID"].ToString() == "6")
                    {
                        if ((dt.Rows[i]["VALUE"]) != null || (dt.Rows[i]["VALUE"]) != "undefined")
                        {
                            txt_otheropen.Text = (dt.Rows[i]["VALUE"].ToString());
                        }
                    }
                    if (dt.Rows[i]["PK_EXPENSE_ID"].ToString() == "5")
                    {
                        txt_othperiod.Text = (dt.Rows[i]["VALUE"].ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    public void fillData(string adv_type, DataTable dt)
    {
        try
        {
            String IsData = string.Empty;
            StringBuilder sb = new StringBuilder();
            txt_adv_type.Text = adv_type;
            if (adv_type == "1")
            {
                DataSet ds = (DataSet)ActionController.ExecuteAction("", "Advance_Request.aspx", "advdetails", ref IsData, span_grade.InnerHtml, txt_Username.Text);
                DataTable dtcity = ds.Tables[1];
             
               // DataTable dtcity = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref IsData, "", "AdCity");
                if (dt != null && dt.Rows.Count > 0)
                    sb.Append("<div class='col-md-12' id='advance_travel'>");
                sb.Append("<div class='form-horizontal'>");
                sb.Append("<div class='form-group'><div class='col-md-1'></div>");
                sb.Append("<label class='col-md-2'>Place From</label>");
                sb.Append("<div class='col-md-3'>");
                if (Convert.ToString(dt.Rows[0]["from_city"]) != "0" || Convert.ToString(dt.Rows[0]["from_city"]) != "")
                {
                    sb.Append("<select ID='From_City' runat='server' class='form-control input-sm' onchange='chk_class_From(1)'>");
                    sb.Append("<option Value='0'>---Select One---</option>");

                    if (Convert.ToString(dt.Rows[0]["from_city"]) == "-1")
                    {
                        for (int i = 0; i < dtcity.Rows.Count; i++)
                        {
                            sb.Append("<option Value='" + Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtcity.Rows[i]["NAME"]) + "</option>");
                        }
                        sb.Append("<option Value='-1' Selected='true'>Other</option>");
                        sb.Append("</select>");
                        sb.Append("<input type='text' class='form-control input-sm' id='txt_f_city' value='" + Convert.ToString(dt.Rows[0]["other_f_city"]) + "'>");
                    }
                    else
                    {
                        for (int i = 0; i < dtcity.Rows.Count; i++)
                        {
                            if (Convert.ToString(dt.Rows[0]["from_city"]) == Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]))
                            {
                                sb.Append("<option Value='" + Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]) + "' Selected='true'>" + Convert.ToString(dtcity.Rows[i]["NAME"]) + "</option>");
                            }
                            else
                            {
                                sb.Append("<option Value='" + Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtcity.Rows[i]["NAME"]) + "</option>");
                            }
                        }
                        sb.Append("<option Value='-1'>Other</option>");
                        sb.Append("</select>");
                        sb.Append("<input type='text' class='form-control input-sm' id='txt_f_city' style='display:none'>");
                    }

                }
                sb.Append("</div>");
                sb.Append("<label class='col-md-2'>Place To</label>");
                sb.Append("<div class='col-md-3'>");
                sb.Append("<select ID='To_City' runat='server' class='form-control input-sm' onchange='chk_class_To(1);'>");
                sb.Append("<option Value='0'>---Select One---</option>");


                if (Convert.ToString(dt.Rows[0]["to_city"]) == "-1")
                {
                    for (int i = 0; i < dtcity.Rows.Count; i++)
                    {
                        sb.Append("<option Value='" + Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtcity.Rows[i]["NAME"]) + "</option>");
                    }
                    sb.Append("<option Value='-1' Selected='true'>Other</option>");
                    sb.Append("</select>");
                    sb.Append("<input type='text' class='form-control input-sm' id='txt_t_city'  value='" + Convert.ToString(dt.Rows[0]["other_t_city"]) + "'>");
                }
                else
                {
                    for (int i = 0; i < dtcity.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[0]["to_city"]) == Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]))
                        {
                            sb.Append("<option Value='" + Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]) + "' Selected='true'>" + Convert.ToString(dtcity.Rows[i]["NAME"]) + "</option>");
                        }
                        else
                        {
                            sb.Append("<option Value='" + Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtcity.Rows[i]["NAME"]) + "</option>");
                        }
                    }
                    sb.Append("<option Value='-1'>Other</option>");
                    sb.Append("</select>");
                    sb.Append("<input type='text' class='form-control input-sm' id='txt_t_city' style='display:none'>");

                }
                sb.Append("</div></div>");
                sb.Append("<div class='form-group'><div class='col-md-1'></div>");
                sb.Append("<label class='col-md-2'>From Date</label><div class='col-md-3'>");
                sb.Append("<div class='input-group'> <input type='text' class='form-control datepicker-dropdown'  id='txt_fdate' readonly=''  value='" + Convert.ToDateTime(dt.Rows[0]["from_date"]).ToString("dd-MMM-yyyy") + "'  runat='server'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button> </span></div>");
                sb.Append("</div>");

                sb.Append("<label class='col-md-2'>To Date</label><div class='col-md-3'>");
                sb.Append("<div class='input-group'> <input type='text' class='form-control datepicker-dropdown'  id='txt_tdate' readonly=''  value='" + Convert.ToDateTime(dt.Rows[0]["to_date"]).ToString("dd-MMM-yyyy") + "'  runat='server'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button> </span></div>");
                sb.Append("</div></div>");
                sb.Append("<div class='form-group'><div class='col-md-1'></div>");
                sb.Append("<label class='col-md-2'>Allowed Amount</label><div class='col-md-3'>");

                string isvalid = "";
                string amount = (string)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getadvanceamount", ref isvalid, txt_pk_id.Text, Convert.ToString(dt.Rows[0]["to_city"]), span_grade.InnerHtml);
                if (amount == "")
                {
                    amount = "0";
                }
                sb.Append("<asp:Label ID='lblallowedamt' runat='server' style='text-align:right' >" + Convert.ToInt32(amount) + "</asp:Label>");
                sb.Append("</div>");
                sb.Append("<label class='col-md-2'>Amount</label>");
                sb.Append("<div class='col-md-3'> <input type='text' class='form-control' id='txt_amount' runat='server' onkeypress='return isNumberKey(event)' value='" + Convert.ToInt32(dt.Rows[0]["amount"]) + "' onchange='checkamount();'/></div><div class='col-md-1'></div></div>");
                txtDummy.Text = Convert.ToString(dt.Rows[0]["amount"]);
                //if (Convert.ToInt32(dt.Rows[0]["amount"]) > Convert.ToInt32(amount))
                //{
                //    txt_Deviate.Text = "1";
                //}
                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label>");
                sb.Append("<div class='col-md-3'><textarea type='text' maxlength='200' class='form-control' rows='1' id='txt_remark' runat='server' >" + Convert.ToString(dt.Rows[0]["remark"]) + "</textarea></div>");
                sb.Append("</div></div>");
                div_travelData.InnerHtml = Convert.ToString(sb);
            }
            else
            {
                sb.Append("<div class='col-md-12' id='advance_other'>");
                sb.Append("<div class='form-horizontal'>");
                sb.Append("<div class='form-group'><div class='col-md-1'></div>");
                sb.Append("<label class='col-md-2'>Advance Date</label>");
                sb.Append("<div class='col-md-3'>");
                sb.Append("<div class='input-group'> <input type='text' class='form-control datepicker-dropdown'  id='txt_advance_date' readonly=''  value='" + Convert.ToDateTime(dt.Rows[0]["advance_date"]).ToString("dd-MMM-yyyy") + "'   runat='server'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button> </span></div>");
                sb.Append("</div></div>");
                sb.Append("<div class='form-group'><div class='col-md-1'></div>");
                sb.Append("<label class='col-md-2'>Amount</label>");
                sb.Append("<div class='col-md-3'><input type='text' class='form-control' id='txt_amount' runat='server' onkeypress='return isNumberKey(event)' value='" + Convert.ToInt32(dt.Rows[0]["amount"]) + "' onchange='checkamount();'/></div><div class='col-md-1'></div></div>");
                txtDummy.Text = Convert.ToString(dt.Rows[0]["amount"]);
                //if (Convert.ToInt32(dt.Rows[0]["amount"]) > Convert.ToInt32(amount))
                //{
                //    txt_Deviate.Text = "1";
                //}
                sb.Append("</div></div>");
                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label>");
                sb.Append("<div class='col-md-3'><textarea type='text' maxlength='200' class='form-control' rows='1' id='txt_remark' runat='server' >" + Convert.ToString(dt.Rows[0]["remark"]) + "</textarea></div>");
                sb.Append("</div></div>");
                div_otherData.InnerHtml = Convert.ToString(sb);
            }
            /////////////////////////validation for parameter/////////////////////////////////////
            //string Isdata1 = string.Empty;
            //int openadv = 0;
            //dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref Isdata1, txt_Username.Text, "AdExParameter");
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (txt_adv_type.Text == "1")
            //        {
            //            if (dt.Rows[i]["PARAMETER"].ToString() == "Domestic Other Advance Amount")
            //            {
            //                txt_adamount.Text = (dt.Rows[i]["VALUE"].ToString());
            //            }
            //            if (dt.Rows[i]["PARAMETER"].ToString() == "Domestic Open Adavances")
            //            {
            //                if ((dt.Rows[i]["VALUE"]) != null || (dt.Rows[i]["VALUE"]) != "undefined")
            //                {
            //                    openadv = Convert.ToInt32(dt.Rows[i]["VALUE"]);
            //                }
            //            }
            //            if (dt.Rows[i]["PARAMETER"].ToString() == "Domestic Advance Period")
            //            {
            //                txt_adperiod.Text = (dt.Rows[i]["VALUE"].ToString());
            //            }
            //        }
            //        if (txt_adv_type.Text == "3")
            //        {
            //            if (dt.Rows[i]["PARAMETER"].ToString() == "Domestic Other Advance Amount")
            //            {
            //                txt_adamount.Text = (dt.Rows[i]["VALUE"].ToString());
            //            }
            //            if (dt.Rows[i]["PARAMETER"].ToString() == "Other Open Adavances")
            //            {
            //                if ((dt.Rows[i]["VALUE"]) != null || (dt.Rows[i]["VALUE"]) != "undefined")
            //                {
            //                    openadv = Convert.ToInt32(dt.Rows[i]["VALUE"]);
            //                }
            //            }
            //            if (dt.Rows[i]["PARAMETER"].ToString() == "Other Advance Period")
            //            {
            //                txt_adperiod.Text = (dt.Rows[i]["VALUE"].ToString());
            //            }
            //        }
            //    }
            //}
            //string IsData6 = string.Empty;
            //int dts = 0; Double amttotal = 0;
            //DataTable dtid = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "getadvanids", ref IsData6, txt_Username.Text, txt_adv_type.Text);
            //if (dtid != null)
            //{
            //    for (int i = 0; i < dtid.Rows.Count; i++)
            //    {
            //        string dtvalue = (string)ActionController.ExecuteAction("", "Advance_Request.aspx", "getperiod", ref IsData6, txt_Username.Text, dtid.Rows[i]["PK_ADVANCE_HDR_Id"].ToString(), Convert.ToInt64(txt_adperiod.Text));
            //        string[] result = dtvalue.Split('=');
            //        if (result[0] != null)
            //        {
            //            dts = Convert.ToInt32(dts) + Convert.ToInt32(result[0]);
            //        }
            //        if (result[1] != null)
            //        {
            //            if (result[1] == "true")
            //            {
            //                txt_expire.Text = "1";
            //            }
            //            else
            //            {
            //                txt_expire.Text = "0";
            //            }
            //        }

            //    }
            //}
            //DataTable dtid1 = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "getadvanids", ref IsData6, txt_Username.Text, "");
            //if (dtid1 != null && dtid1.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtid1.Rows.Count; i++)
            //    {
            //        string dtvalue1 = (string)ActionController.ExecuteAction("", "Advance_Request.aspx", "getperiod", ref IsData6, txt_Username.Text, dtid1.Rows[i]["PK_ADVANCE_HDR_Id"].ToString(), Convert.ToInt64(txt_adperiod.Text));
            //        if (dtvalue1 != null)
            //        {
            //            string[] result1 = dtvalue1.Split('=');
            //            if (result1[2] != null)
            //            {
            //                amttotal = Convert.ToDouble(amttotal) + Convert.ToDouble(result1[2]);
            //            }
            //        }
            //    }
            //    amttotal = Convert.ToDouble(amttotal) - Convert.ToDouble(txtDummy.Text);
            //    total_amount.Text = Convert.ToString(amttotal);
            //}
            //if (dts != 0)
            //{
            //    if ((dts) >= (openadv - 1))
            //    {
            //        txt_opcount.Text = "1";
            //    }
            //}
            //if (dts >= openadv)
            //{
            //    txt_opencount.Text = "1";
            //}
            //else
            //{
            //    txt_opencount.Text = "0";
            //}

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

    }

    //private void showall(string adtype)
    //{
    //    try
    //    {
    //        DataTable DTS;
    //        string IsValid = string.Empty;
    //        DTS = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "getappdetails", ref IsValid, txt_Username.Text, adtype);
    //        DTS.Columns[0].ColumnName = "Sr.No";
    //        Session["ResultData"] = DTS;
    //        txt_adcount.Text = Convert.ToString(DTS.Rows.Count);
    //        if (DTS.Rows.Count > 0)
    //        {
    //            // div_Details.Visible = true;
    //            for (int i = 0; i < DTS.Rows.Count; i++)
    //            {
    //                DTS.Rows[i]["Sr.No"] = i + 1;
    //            }

    //            StringBuilder str = new StringBuilder();
    //            if (adtype == "1")
    //            {
    //                str.Append("<table id='data-table1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th>#</th><th>Place From</th><th>Place To</th><th>From Date</th><th>To Date</th><th>Amount</th><th>Approved By</th><th>Approval Date</th> </tr></thead>");
    //            }
    //            else
    //            {
    //                str.Append("<table id='data-table1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th>#</th><th>Advance Date</th><th>Amount</th><th>Approved By</th><th>Approval Date</th> </tr></thead>");

    //            }
    //            str.Append("<tbody>");

    //            for (int i = 0; i < DTS.Rows.Count; i++)
    //            {
    //                str.Append(" <tr>");
    //                str.Append("<td align='left'>" + (i + 1) + "</td>");
    //                if (adtype == "1")
    //                {
    //                    if (Convert.ToString(DTS.Rows[i]["from_city"]) == "0" || Convert.ToString(DTS.Rows[i]["from_city"]) == "-1" || Convert.ToString(DTS.Rows[i]["from_city"]) == "")
    //                    {
    //                        str.Append("<td align='left'>" + Convert.ToString(DTS.Rows[i]["other_f_city"]) + "</td>");
    //                    }
    //                    else
    //                    {
    //                        str.Append("<td align='left'>" + Convert.ToString(DTS.Rows[i]["fname"]) + "</td>");
    //                    }

    //                    if (Convert.ToString(DTS.Rows[i]["to_city"]) == "0" || Convert.ToString(DTS.Rows[i]["to_city"]) == "-1" || Convert.ToString(DTS.Rows[i]["to_city"]) == "")
    //                    {
    //                        str.Append("<td align='left'>" + Convert.ToString(DTS.Rows[i]["other_t_city"]) + "</td>");
    //                    }
    //                    else
    //                    {
    //                        str.Append("<td align='left'>" + Convert.ToString(DTS.Rows[i]["tname"]) + "</td>");
    //                    }
    //                    str.Append("<td align='left'>" + DTS.Rows[i]["FROM_DATE"].ToString() + "</td>");
    //                    str.Append("<td align='left'>" + DTS.Rows[i]["TO_DATE"].ToString() + "</td>");
    //                }
    //                else
    //                {
    //                    str.Append("<td align='left'>" + DTS.Rows[i]["ADVANCE_DATE"].ToString() + "</td>");
    //                }
    //                str.Append("<td align='right'>" + DTS.Rows[i]["AMOUNT"].ToString() + "</td>");
    //                str.Append("<td align='left'>" + DTS.Rows[i]["APPROVED_BY"].ToString() + "</td>");
    //                str.Append("<td align='left'>" + DTS.Rows[i]["APPROVED_ON"].ToString() + "</td>");
    //                str.Append("</tr>");
    //            }
    //            str.Append("   </tbody>   </table> ");
    //            divalldata.InnerHtml = str.ToString();
    //            ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);
    //        }
    //        else
    //        {
    //            //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Data Found !!')}</script>");
    //            //divalldata.InnerHtml = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        FSL.Logging.Logger.WriteEventLog(false, ex);
    //    }

    //}


    protected void btnRequest_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
		
                string confirmValue = txt_confirm.Text;
                if (confirmValue == "Yes" || confirmValue == "")
                {
                    string refData = string.Empty;
                    string isInserted = string.Empty;
                    string ISValid = string.Empty;
                    txt_Condition.Text = "1";
                    txt_Action.Text = "Submit";
                    txt_Audit.Text = "MODIFY ADVANCE REQUEST";
                    string isSaved = string.Empty;

                    string vehiclexml_string = txt_xml_data_vehicle.Text;
                    vehiclexml_string = vehiclexml_string.Replace("&", "&amp;");
                    vehiclexml_string = vehiclexml_string.Replace(">", "&gt;");
                    vehiclexml_string = vehiclexml_string.Replace("<", "&lt;");
                    vehiclexml_string = vehiclexml_string.Replace("||", ">");
                    vehiclexml_string = vehiclexml_string.Replace("|", "<");
                    vehiclexml_string = vehiclexml_string.Replace("'", "&apos;");
                    txt_xml_data_vehicle.Text = vehiclexml_string.ToString();

                    if (ddl_Payment_Mode.SelectedValue == "2")
                    {
                        ddlAdv_Location.SelectedValue = "0";
                    }
                    isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Modification.aspx", "modify", ref refData, Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), ddl_Payment_Mode.SelectedValue, txt_pk_id.Text, txt_xml_data_vehicle.Text, txt_Username.Text, txt_rmk.Text, txt_Audit.Text, txt_Action.Text, span_Approver.InnerHtml, txt_Document_Xml.Text, ddlAdv_Location.SelectedValue);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                        string[] errmsg = refData.Split(':');
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + errmsg[1].ToString() + "')", true);
                    }
                    else
                    {
                        //////upload file code///////////////////
                        //string isValid1 = string.Empty;
                        //string[] arr = new string[100];
                        //DataTable dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "getrequest_document", ref isValid1, "ADVANCE", txt_Request.Text);
                        //if (dt.Rows.Count > 0)
                        //{
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString().Trim();
                        //        if (System.IO.File.Exists(activeDir + "\\" + "ADVANCE\\" + txt_Request.Text + "\\" + dt.Rows[i]["filename"].ToString()))
                        //        {
                        //            if (System.IO.File.Exists(activeDir + "\\" + "ADVANCE\\" + dt.Rows[i]["filename"].ToString()))
                        //            {
                        //                System.IO.File.Delete(activeDir + "\\" + "ADVANCE\\" + dt.Rows[i]["filename"].ToString());
                        //            }
                        //        }
                        //        else
                        //        {
                        //            string path = string.Empty;
                        //            string foldername = txt_Request.Text;
                        //            foldername = foldername.Replace("/", "_");
                        //            path = activeDir + "\\" + "ADVANCE\\" + foldername;
                        //            if (Directory.Exists(path))
                        //            {
                        //                string[] directories = Directory.GetFiles(activeDir + "\\" + "ADVANCE\\");
                        //                path = path + "\\";
                        //                foreach (var directory in directories)
                        //                {
                        //                    // for (int i = 0; i < dt.Rows.Count; i++)
                        //                    // {
                        //                    var sections = directory.Split('\\');
                        //                    var fileName = sections[sections.Length - 1];
                        //                    if (dt.Rows[i]["filename"].ToString().Trim() == fileName)
                        //                    {
                        //                        System.IO.File.Move(activeDir + "\\" + "ADVANCE\\" + fileName, path + fileName);
                        //                        if (System.IO.File.Exists(activeDir + "\\" + "ADVANCE\\" + fileName))
                        //                        {
                        //                            System.IO.File.Delete(activeDir + "\\" + "ADVANCE\\" + fileName);
                        //                        }
                        //                    }
                        //                    // }
                        //                }
                        //            }
                        //            else
                        //            {
                        //                Directory.CreateDirectory(path);
                        //                string[] directories = Directory.GetFiles(activeDir + "\\" + "ADVANCE\\");
                        //                path = path + "\\";
                        //                foreach (var directory in directories)
                        //                {
                        //                   // for (int i = 0; i < dt.Rows.Count; i++)
                        //                   // {
                        //                        var sections = directory.Split('\\');
                        //                        var fileName = sections[sections.Length - 1];
                        //                        if (dt.Rows[i]["filename"].ToString().Trim() == fileName)
                        //                        {
                        //                            System.IO.File.Move(activeDir + "\\" + "ADVANCE\\" + fileName, path + fileName);
                        //                            if (System.IO.File.Exists(activeDir + "\\" + "ADVANCE\\" + fileName))
                        //                            {
                        //                                System.IO.File.Delete(activeDir + "\\" + "ADVANCE\\" + fileName);
                        //                            }
                        //                        }
                        //                    //}
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        /////////////////////////////////////////

                        if (empno.InnerHtml == "4263" || empno.InnerHtml == "4262")
                        {
                            string msg = "";
                            string ref_data = string.Empty;
                            string emailid = string.Empty;
                            CryptoGraphy crypt = new CryptoGraphy();
                            string process_name = crypt.Encryptdata("ADVANCE EXPENSE");
                            string req_no = crypt.Encryptdata(txt_Request.Text);
                            DataTable DTAP = new DataTable();

                            if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                            {
                                DTAP = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getaccapprover", ref ref_data, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", ddlAdv_Location.Text, ddl_Payment_Mode.Text);
                            }
                            else
                            {
                                DTAP = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getaccapprover", ref ref_data, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", 0, ddl_Payment_Mode.Text);
                            }
                            if (DTAP != null)
                            {
                                if (DTAP.Rows.Count > 0)
                                {
                                    txt_Condition.Text = "3";
                                    string isSaved1 = (string)ActionController.ExecuteAction("", "Advance_Request.aspx", "inserttwi", ref ref_data, txtProcessID.Text, txtInstanceID.Text, txt_Username.Text, txt_Request.Text, "6");
                                    if (isSaved1 == null || ref_data.Length > 0 || isSaved1 == "false")
                                    {
                                       // Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                                        string[] errmsg = refData.Split(':');
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + errmsg[1].ToString() + "')", true);
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
                                        bool isCreate1 = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "158", "ADVANCE REQUEST APPROVAL", "APPROVE", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, "0", ref isInserted);
                                        if (isCreate1)
                                        {
                                            try
                                            {
                                               
                                                    if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                                                    {
                                                        msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been modified and sent for account payment approval.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre></p><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTERNET URL:<a href='https://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>https://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";
                                                        emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE APPROVAL", "SUBMIT", txtEmailID.Text, txtApproverEmail.Text, msg, "Request No: " + txt_Request.Text);
                                                    }
                                                    else
                                                    {
                                                        msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been modified and sent for account payment approval.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre></p><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTERNET URL:<a href='https://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>https://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";
                                                        emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE APPROVAL", "SUBMIT", txtEmailID.Text, txtApproverEmail.Text, msg, "Request No: " + txt_Request.Text);

                                                    }
                                                
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                string msg2 = string.Empty;
                                                msg2 = "alert('Advance Request has been modified and sent for account payment approval...!);window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea')";
                                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                              // Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Advance Request has been modified and sent for account payment approval...!');window.open('../../portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    string msg2 = string.Empty;
                                    msg2 = "alert('Account Payment Approver Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                    // Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Approver Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                            else
                            {
                                string msg2 = string.Empty;
                                msg2 = "alert('Account Payment Approver Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                              //  Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Approver Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                        else
                        {
                            string[] Dval1 = new string[1];
                            Dval1[0] = span_Approver.InnerHtml;
                            if (txtApproverEmail.Text == "")
                            {
                                txtApproverEmail.Text = txtEmailID.Text;
                            }
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "160", "MODIFY ADVANCE REQUEST", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, txt_Request.Text, txtWIID.Text, ref isInserted);

                            if (isCreate)
                            {
                                try
                                {
                                    string emailid = string.Empty;
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been has been modified and sent for your approval..</font></pre><p/>  <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MODIFY ADVANCE REQUEST", "SUBMIT", txt_Approver_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                }
                                catch (Exception ex)
                                {
                                    // throw;
                                    FSL.Logging.Logger.WriteEventLog(false, ex);
                                }
                                finally
                                {
                                    string msg1 = string.Empty;
                                    if (txt_deviate.Text == "0")
                                    {
                                        msg1 = "alert('Advance Request has been modified and sent for your approval....!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg1, true);
                                    }
                                    else
                                    {
                                        msg1 = "alert('Advance Request has been modified successfully and sent for approval(under deviation)..!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg1, true);
                                    }
                                       // Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Advance Request has been modified and sent for your approval....!'); window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                    }
                }
                else
                {

                }
                divIns.Style.Add("display", "none");
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
    

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string fillAmountall(string grade, string tocity)
    {
        ////get amount from designation//////////////////////////////
        string IsData1 = string.Empty;
        string str = string.Empty;
        DataTable dta = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Modification.aspx", "getallamount", ref IsData1, grade, tocity,"");
        if (dta != null && dta.Rows.Count > 0)
        {
            str = dta.Rows[0]["AMOUNT"].ToString();
        }
        return str;

    }
    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;
            string RequestNo = spn_req_no.InnerHtml;
            RequestNo = RequestNo.Replace("/", "_");
            path = activeDir + "\\" + "ADVANCE\\" + RequestNo + "\\";
            if (Directory.Exists(path))
            {

            }
            else
            {
                Directory.CreateDirectory(path);
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
            DataTable dt = (DataTable)Session["UploadedFiles"];

            FileUpload1.SaveAs(path + filename);
            ClearContents(sender as Control);
        }
        catch (Exception Ex)
        {
            Logger.WriteEventLog(false, Ex);
        }

    }
    private void ClearContents(Control control)
    {
        for (var i = 0; i < Session.Keys.Count; i++)
        {
            if (Session.Keys[i].Contains(control.ClientID))
            {
                Session.Remove(Session.Keys[i]);
                break;
            }
        }
    }
}

