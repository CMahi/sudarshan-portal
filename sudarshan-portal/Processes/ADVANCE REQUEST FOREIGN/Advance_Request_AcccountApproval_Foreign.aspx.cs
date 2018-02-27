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

public partial class Advance_Request_AcccountApproval_Foreign : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    StringBuilder str = new StringBuilder();
    ListItem Li = new ListItem("--Select One--", "");
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Request_AcccountApproval_Foreign));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                    }

                    fillAction();
                    Initialization();

                }
                Initialization();
            }
        }
        catch (Exception Exc) { }
    }

    public void fillPolicy_Details()
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


                isValid = string.Empty;
                DataTable dtamt1 = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "selectdetails", ref isValid, txt_designation.Text, "AdDesignationForeign");

                if (dtamt1 != null && dtamt1.Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Designation</th><th>Country</th><th>Amount(Foreign Currency)</th><th>Effective Date</th></tr></thead>";

                    for (int i = 0; i < dtamt1.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + span_designation.InnerHtml + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["country"]) + "</td><td style='text-align:right'>" + Convert.ToString(dtamt1.Rows[i]["AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["EFFECTIVE_DATE"]) + "</td></tr>";

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
        try
        {
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "getdetails", ref isdata, txtProcessID.Text, txtInstanceID.Text);
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_ADVANCE_F_HDR_ID"]);
                txt_pk_dtl_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_ADVNC_FOREIGN_Id"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                txt_Initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMAIL_ID"]);

                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "pgettraveluser", ref isdata, txt_Initiator.Text);
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
                    span_division.InnerHtml = "NA";
                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "pgettravelrequestapprover", ref isdata, txt_Initiator.Text);
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
                    fillPolicy_Details();
                    fillDocument_Details();
                    fillAuditTrailData();
                    fillData(dsData.Tables[0]);

                    //string ISValid = string.Empty;
                    //string str = string.Empty;
                    //DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref ISValid, span_advance.InnerHtml, "AdGLcode");

                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }

    }

    public void fillData(DataTable dt)
    {
        StringBuilder html_Header = new StringBuilder();
        StringBuilder sb = new StringBuilder();

        //advance for foreign

        sb.Append("<div class='col-md-12' id='advance_Foreign'>");
        sb.Append("<div class='form-horizontal'>");
        sb.Append("<div class='form-group'><div class='col-md-1'></div>");
        sb.Append("<label class='col-md-2'>Region From</label>");
        sb.Append("<div class='col-md-3'>");
        if (Convert.ToString(dt.Rows[0]["FCOUNTRYNAME"]) != "")
        {
            sb.Append(Convert.ToString(dt.Rows[0]["FCOUNTRYNAME"]));
        }
        sb.Append("</div>");
        sb.Append("<label class='col-md-2'>Region To</label>");
        sb.Append("<div class='col-md-3'>");

        if (Convert.ToString(dt.Rows[0]["TCOUNTRYNAME"]) != "")
        {
            sb.Append(Convert.ToString(dt.Rows[0]["TCOUNTRYNAME"]));
        }
        sb.Append("</div>");
        sb.Append("</div></div>");
        sb.Append("<div class='form-group'><div class='col-md-1'></div>");
        sb.Append("<label class='col-md-2'>City From</label>");
        sb.Append("<div class='col-md-3'>");
        if (Convert.ToString(dt.Rows[0]["FK_CITY_FRM_ID"]) != "-1")
        {
            sb.Append(Convert.ToString(dt.Rows[0]["fcity"]));
        }
        else
        {
            sb.Append("Other");
        }

        sb.Append("</div>");

        sb.Append("<label class='col-md-2'>City To</label>");
        sb.Append("<div class='col-md-3'>");
        if (Convert.ToString(dt.Rows[0]["FK_CITY_TO_ID"]) != "-1")
        {
            sb.Append(Convert.ToString(dt.Rows[0]["tcity"]));
        }
        else
        {
            sb.Append("Other");
        }
        sb.Append("</div></div>");
        if (Convert.ToString(dt.Rows[0]["FK_CITY_FRM_ID"]) == "-1" || Convert.ToString(dt.Rows[0]["FK_CITY_TO_ID"]) == "-1")
        {
            sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'></label>");
            sb.Append("<div class='col-md-3'>");
            if (Convert.ToString(dt.Rows[0]["FK_CITY_FRM_ID"]) == "-1")
            {
                sb.Append(Convert.ToString(dt.Rows[0]["PLACE_FROM_OTHER"]));
            }

            sb.Append("</div>");
        
            sb.Append("<label class='col-md-2'></label>");
            sb.Append("<div class='col-md-3'>");
            if (Convert.ToString(dt.Rows[0]["FK_CITY_TO_ID"]) == "-1")
            {
                sb.Append(Convert.ToString(dt.Rows[0]["PLACE_TO_OTHER"]));
            }
            sb.Append("</div></div>");
        }

        sb.Append("<div class='form-group'><div class='col-md-1'></div> ");
        sb.Append("<label class='col-md-2'>Currency</label>");
        sb.Append("<div class='col-md-3'><div class='input-group' id='Div1'>" + (dt.Rows[0]["CURRENCY"]).ToString());
        sb.Append("</div></div></div>");

        sb.Append("<div class='form-group'><div class='col-md-1'></div> ");
        sb.Append("<label class='col-md-2'>From Date</label>");
        sb.Append("<div class='col-md-3'><div class='input-group' id='Div1'>" + Convert.ToDateTime(dt.Rows[0]["from_date"]).ToString("dd-MMM-yyyy"));
        sb.Append("</div></div>");
        sb.Append("<label class='col-md-2'>To Date</label><div class='col-md-3'><div class='input-group' id='Div6'>" + Convert.ToDateTime(dt.Rows[0]["to_date"]).ToString("dd-MMM-yyyy"));
        sb.Append("</div></div><div class='col-md-1'></div></div>");

        sb.Append("<div class='form-group'><div class='col-md-1'></div>");
        sb.Append("<label class='col-md-2'>Allowed Amount" + "(" + Convert.ToString(dt.Rows[0]["CURRENCY"]) + ")" + "</label><div class='col-md-3'>");
        string amount = string.Empty;

        amount = dt.Rows[0]["allowed_amount"].ToString();
        sb.Append(amount);
        sb.Append("</div>");
        sb.Append("<label class='col-md-2'>Amount(Foreign Currency)</label>");
        sb.Append("<div class='col-md-3'>" + Convert.ToString(dt.Rows[0]["amount"]) + "</div><div class='col-md-1'></div></div>");
        txtDummy.Text = Convert.ToString(dt.Rows[0]["amount"]);

        sb.Append("<div class='form-group'><div class='col-md-1'></div>");
        sb.Append("<label class='col-md-2'>Advance Mode(Currency)</label>");
        sb.Append("<div class='col-md-3'>" + Convert.ToString(dt.Rows[0]["CURRENCY_AMOUNT"]) + "</div>");


        sb.Append("<label class='col-md-2'>Advance Mode(Forex Card)</label>");
        sb.Append("<div class='col-md-3'>" + Convert.ToString(dt.Rows[0]["FOREX_CARD"]) + "</div><div class='col-md-1'></div></div>");


        if (Convert.ToInt32(dt.Rows[0]["amount"]) > Convert.ToInt32(dt.Rows[0]["allowed_amount"]))
        {
            txt_Deviate.Text = "1";
        }
        sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label>");
        sb.Append("<div class='col-md-8'>" + Convert.ToString(dt.Rows[0]["remark"]) + "</div></div>");
        sb.Append("</div></div>");


        //vendor details

        double perdaysamt1 = Convert.ToDouble(dt.Rows[0]["allowed_amount"]) / Convert.ToDouble(dt.Rows[0]["noofdays"]);
        txt_per_day_amt.Text = perdaysamt1.ToString();

        html_Header.Append("<table class='table table-bordered' width='100%' id='tbl_vendor' runat='server'>");
        html_Header.Append("<tr style='background-color:grey; text-align:center; color:white;'><th style='text-align:center'>Currency</th><th style='text-align:center'>Exchange Rate</th><th style='text-align:center'>Amount In FC</th><th style='text-align:center'>Amount In INR</th><th style='text-align:center'>Financial Year</th><th style='text-align:center'>Vendor Code</th><th style='text-align:center'>Vendor Bill No.</th><th style='text-align:center'>Vendor Bill Date</th><th style='text-align:center'>Payment Method</th><th style='text-align:center'>Tax Code</th><th style='text-align:center'>Service Charges</th></tr>");
        html_Header.Append("<tbody>");
        html_Header.Append("<tr><td style='text-align:center;width:5%'><input type='text' style='text-align:right;display:none' readonly='true' id='txt_curn_Fc' value=" + Convert.ToString(dt.Rows[0]["currency"]) + "  runat='server'/>" + Convert.ToString(dt.Rows[0]["currency"]) + "</td>");
        if (txt_exchange_Rate.Text == "")
        {
            html_Header.Append("<td style='text-align:center;width:10%'><input type='text' class='form-control datepicker-dropdown numbersOnly1' style='text-align:right'  id='txt_Exchng_Rate' value=''  onchange='validateFloatKey(this); checkamount();' onkeypress='return allowOnlyNumbers()'  runat='server'/></td>");
        }
        else
        {
            html_Header.Append("<td style='text-align:center;width:10%'><input type='text' class='form-control datepicker-dropdown numbersOnly1' style='text-align:right'  id='txt_Exchng_Rate' value=" + txt_exchange_Rate.Text + " onkeypress='return allowOnlyNumbers()' onchange='validateFloatKey(this); checkamount();'  runat='server'/></td>");
        }

        html_Header.Append("<td style='text-align:right;width:10%'><input type='text' style='text-align:right' readonly='true' class='form-control datepicker-dropdown'  id='txt_Amt_Fc' value=" + Convert.ToString(dt.Rows[0]["amount"]) + "  runat='server'/></td>");

        if (txt_amount_inr.Text == "")
        {
            html_Header.Append("<td style='text-align:center;width:10%'><input type='text' class='form-control datepicker-dropdown' style='text-align:right'  id='txt_Amt_INR' value='' readonly='true'  runat='server'/></td>");
        }
        else
        {
            html_Header.Append("<td style='text-align:center;width:10%'><input type='text' class='form-control datepicker-dropdown' style='text-align:right'  id='txt_Amt_INR' value=" + txt_amount_inr.Text + " readonly='true'  runat='server'/></td>");
        }
        if (txt_fin_year.Text == "")
        {
            html_Header.Append("<td style='text-align:center;width:9%'>");
            html_Header.Append("'<select ID='ddl_fin_year' runat='server' class='form-control input-sm' style='margin-top: -16px'>'");
            html_Header.Append("<option Value='0'>---Select One---</option>");
            html_Header.Append("<option Value='" + DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString() + "'>" + DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString() + "</option>");
            html_Header.Append("ddl_fin_year.SelectedIndex = 0");
            html_Header.Append("</td>");
        }
        else
        {
            html_Header.Append("<td style='text-align:center;width:9%'>");
            html_Header.Append("'<select ID='ddl_fin_year' runat='server' class='form-control input-sm' style='margin-top: -16px'>'");
            html_Header.Append("<option Value='0'>---Select One---</option>");
            html_Header.Append("<option selected='selected' value='" + DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString() + "' >" + DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString() + "</option>");
            html_Header.Append("ddl_fin_year.SelectedIndex = 0");
            html_Header.Append("</td>");
        }
        if (txt_vendor_code.Text == "")
        {
            html_Header.Append("<td style='text-align:center;width:9%'>");
            html_Header.Append("'<select ID='ddl_vendor_code' runat='server' class='form-control input-sm' style='margin-top: -16px'>'");
            html_Header.Append("<option Value='0'>---Select One---</option>");
            string IsVendor = string.Empty;
            DataTable dtvendor = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "getvendorpaymodewise", ref IsVendor, 1);
            if (dtvendor != null && dtvendor.Rows.Count > 0)
            {
                for (int i = 0; i < dtvendor.Rows.Count; i++)
                {
                    html_Header.Append("<option Value='" + Convert.ToString(dtvendor.Rows[i]["PK_ADVANCE_F_VENDOR_ID"]) + "'>" + Convert.ToString(dtvendor.Rows[i]["VENDOR_NAME"]) + "</option>");
                }

                html_Header.Append("</select>");
            }
            html_Header.Append("</td>");
        }
        else
        {
            html_Header.Append("<td style='text-align:center;width:9%'>");
            html_Header.Append("'<select ID='ddl_vendor_code' runat='server' class='form-control input-sm' style='margin-top: -16px'>'");
            html_Header.Append("<option Value='0'>---Select One---</option>");
            string IsVendor = string.Empty;
            DataTable dtvendor = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "getvendorpaymodewise", ref IsVendor, 1);
            if (dtvendor != null && dtvendor.Rows.Count > 0)
            {
                for (int i = 0; i < dtvendor.Rows.Count; i++)
                {
                    if (dtvendor.Rows[i]["PK_ADVANCE_F_VENDOR_ID"].ToString() == txt_vendor_code.Text)
                    {
                        html_Header.Append("<option selected='selected' value='" + txt_vendor_code.Text + "' >" + dtvendor.Rows[i]["VENDOR_NAME"] + "</option>");
                    }
                    else
                    {
                        html_Header.Append("<option Value='" + Convert.ToString(dtvendor.Rows[i]["PK_ADVANCE_F_VENDOR_ID"]) + "'>" + Convert.ToString(dtvendor.Rows[i]["VENDOR_NAME"]) + "</option>");
                    }

                }

                html_Header.Append("</select>");

            }
            html_Header.Append("</td>");
        }
        html_Header.Append("<td style='text-align:center;width:10%'><input type='text' class='form-control datepicker-dropdown'  id='txt_vendor_bill_no' value=''  runat='server'/></td>");
        if (txt_bill_date.Text == "")
        {
            html_Header.Append("<td style='text-align:center;width:10%'><div class='input-group'><input type='text' class='form-control datepicker-dropdown'  id='txt_vendor_date' readonly=''  value=''  runat='server'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button> </span></div></td>");
        }
        else
        {
            html_Header.Append("<td style='text-align:center;width:10%'><div class='input-group'><input type='text' class='form-control datepicker-dropdown'  id='txt_vendor_date' readonly=''  value=" + txt_bill_date.Text + "  runat='server'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button> </span></div></td>");
        }
        if (txt_payment_method_1.Text == "")
        {
            html_Header.Append("<td style='text-align:center;width:9%'><input type='text' class='form-control datepicker-dropdown' style='text-align:right'  id='txt_payment_method' value=''   runat='server'/></td>");
        }
        else
        {
            html_Header.Append("<td style='text-align:center;width:9%'><input type='text' class='form-control datepicker-dropdown' style='text-align:right'  id='txt_payment_method' value=" + txt_payment_method_1.Text + " runat='server'/></td>");
        }
        if (txt_tax_code_1.Text == "")
        {
            html_Header.Append("<td style='text-align:center;width:9%'><input type='text' class='form-control datepicker-dropdown' style='text-align:right'  id='txt_tax_code' value=''   runat='server'/></td>");
        }
        else
        {
            html_Header.Append("<td style='text-align:center;width:9%'><input type='text' class='form-control datepicker-dropdown' style='text-align:right'  id='txt_tax_code' value=" + txt_tax_code_1.Text + " runat='server'/></td>");
        }
        if (txt_service_charges_1.Text == "")
        {
            html_Header.Append("<td style='text-align:center;width:9%'><input type='text' class='form-control datepicker-dropdown' style='text-align:right'  id='txt_service_charges' value='' onkeypress='return isNumberKey(event)'   runat='server'/></td>");
        }
        else
        {
            html_Header.Append("<td style='text-align:center;width:9%'><input type='text' class='form-control datepicker-dropdown' style='text-align:right'  id='txt_service_charges' value=" + txt_service_charges_1.Text + " onkeypress='return isNumberKey(event)' runat='server'/></td>");
        }
        html_Header.Append("</tr></tbody></table>");

        div_vendordata.InnerHtml = Convert.ToString(html_Header);
        div_vendor.Style["display"] = "";

        div_LocalData.InnerHtml = Convert.ToString(sb);
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
		divIns.Style.Add("display","none");
                string remark = txtRemark.Value;

                string isInserted = string.Empty;
                string ref_data = string.Empty;
                string ref_data1 = string.Empty;
                txt_Audit.Text = "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";
                if (ddlAction.SelectedItem.Text == "Approve")
                {
                    //txt_fin_year.Text
                    //txt_vendor_bill_no.Text
                    //txt_vendor_code.Text
                    string IsData1 = string.Empty;

                    string str = string.Empty;

                    DataTable dta = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "getcheckduplicate", ref IsData1, txt_fin_year.Text, txt_vendor_code.Text);
                    if (dta != null && dta.Rows.Count > 0)
                    {
                        for (int n = 0; n < dta.Rows.Count; n++)
                        {
                            if (txt_vendor_bill_no_add.Text == dta.Rows[n]["VENDOR_BILL_NO"].ToString())
                            {

                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Bill No Already Exist...!');}</script>");
                                return;
                            }
                        }

                    }
                    /*******************************************************************************************************************************************/

                    string rfc_no = "";
                            string rfc_string = string.Empty;
                            string rfc_string1 = string.Empty;
                            string line_item = string.Empty;
                            string bank_flag = string.Empty;
                            string bank_no = string.Empty;

                            DataTable dtManage = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata1", ref ref_data, spn_req_no.InnerHtml, "SELECT", "", "", "", "", "", "", "");
                            if (dtManage != null && dtManage.Rows.Count > 0)
                            {
                                bank_flag = Convert.ToString(dtManage.Rows[0]["BANK_FLAG"]);
                                bank_no = Convert.ToString(dtManage.Rows[0]["BANK_NO"]);
                            }

                            if (bank_flag == "E" || bank_flag == "")
                            {
                                string rfc_action = "ADVANCE PAID FOREIGN";
                                DataSet dt_sap_rfc = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getbankrfc1", ref ref_data, 1, rfc_action, spn_req_no.InnerHtml, 1, txt_vendor_code.Text, txt_amount_inr.Text, txt_service_charges_1.Text, txt_bill_date.Text);
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
                                            rfc_string += "$" + txt_vendor_bill_no_add.Text;
                                            rfc_string += "$" + (index + 1).ToString();
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["GL_ACCOUNT"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["REF_KEY_1"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["VENDOR_NO"]);
                                            rfc_string += "$" +  spn_req_no.InnerHtml;
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ITEM_TEXT"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["BUS_AREA"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["COSTCENTER"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["PROFIT_CTR"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["CURRENCY"]);
                                            rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["AMT_DOCCUR"]);
                                            rfc_string += "$" + (txt_payment_method_1.Text.TrimStart()).Substring(0,1).ToUpper();
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
                                            ref_key_no = txt_vendor_bill_no_add.Text;
                                            if (rfc_string1 == "")
                                            {
                                                rfc_string1 += Convert.ToString(dtBank.Rows[l]["COMP_CODE"]);
                                            }
                                            else
                                            {
                                                rfc_string1 += "|" + Convert.ToString(dtBank.Rows[l]["COMP_CODE"]);
                                            }
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["DOC_DATE"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["PSTNG_DATE"]);
                                            rfc_string1 += "$";
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["DOC_TYPE"]);
                                            rfc_string1 += "$" + txt_vendor_bill_no_add.Text;
                                            rfc_string1 += "$" + Convert.ToString(li + l);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["GL_ACCOUNT"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["REF_KEY_1"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["VENDOR_NO"]);
                                            rfc_string1 += "$" + txt_vendor_bill_no_add.Text;
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["ITEM_TEXT"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["BUS_AREA"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["COSTCENTER"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["PROFIT_CTR"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["CURRENCY"]);
                                            rfc_string1 += "$" + Convert.ToInt32(dtBank.Rows[l]["AMT_DOCCUR"]);
                                            rfc_string1 += "$" + (txt_payment_method_1.Text.TrimStart()).Substring(0, 1).ToUpper();
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["PERSON_NO"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["SECCO"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["BUPLA"]);
                                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[l]["ZFBDT"]);
                                            rfc_string1 += "$ ";
                                        }
                                    }

                                    line_item = getLine_Item();
                                    Vendor_Portal.Vendor_Portal_DetailsService Vendor = new Vendor_Portal.Vendor_Portal_DetailsService();
                                    string[] Vendor_data_array = new string[3];
                                    Vendor_data_array = Vendor.BANK_DETAILS(rfc_string1, line_item);
                                    //Vendor_data_array[0] = "";
				   bank_flag=Convert.ToString(Vendor_data_array[1]);
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
                                    /***************************************************************************************************************************************/
                                    if (bank_flag != "E" && bank_flag!="")
                                    //if (Vendor_data_array[1] == "sjdj")
                                    {
                                        txt_Condition.Text = "1";
                                        string isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddlAction.SelectedItem.Text);

                                        string vehiclexml_string = txt_update_xmldata.Text;
                                        vehiclexml_string = vehiclexml_string.Replace("&", "&amp;");
                                        vehiclexml_string = vehiclexml_string.Replace(">", "&gt;");
                                        vehiclexml_string = vehiclexml_string.Replace("<", "&lt;");
                                        vehiclexml_string = vehiclexml_string.Replace("||", ">");
                                        vehiclexml_string = vehiclexml_string.Replace("|", "<");
                                        vehiclexml_string = vehiclexml_string.Replace("'", "&apos;");
                                        txt_update_xmldata.Text = vehiclexml_string.ToString();

                                        string isupdate = (string)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "updatevendor", ref ref_data1, txt_update_xmldata.Text);


                                        if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                                        {
                                            string[] errmsg = ref_data.Split(':');
                                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                                        }
                                        else
                                        {
                                            string[] Dval = new string[1];
                                            Dval[0] = txt_Username.Text;

                                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "276", "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "APPROVE", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                            if (isCreate)
                                            {
                                                try
                                                {

                                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Approved.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "APPROVE", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                                }
                                                catch (Exception)
                                                {
                                                    throw;
                                                }
                                                finally
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been Approved : " + rfc_no + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }
                                        }//
                                    
                                
                            }
                }
                else if (ddlAction.SelectedItem.Text == "Reject")
                {
                    txt_Condition.Text = "2";
                    txt_Audit.Text = "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddlAction.SelectedItem.Text);
                    if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = ref_data.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Username.Text;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "274", "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {

                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Rejected.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "REJECT", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been Rejected...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                    }
                }
                else if (ddlAction.SelectedItem.Text == "Send-Back")
                {
                    txt_Condition.Text = "3";
                    txt_Audit.Text = "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddlAction.SelectedItem.Text);
                    if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = ref_data.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Initiator.Text;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "275", "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "SEND-BACK", txt_Username.Text, txt_Approver.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);

                        if (isCreate)
                        {
                            try
                            {

                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Sent back to the initiator.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "SEND-BACK", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been Sent back to Initiator...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
        }
    }

    private void fillAction()
    {
        try
        {
            string Isdata = string.Empty;
            ListItem li = new ListItem("--Select One--", "");
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "action", ref Isdata);
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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string GetDuplicate(string finyear, string vendorbilno, string vendorcode)
    {

        string IsData1 = string.Empty;

        string str = string.Empty;

        DataTable dta = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getcheckduplicate", ref IsData1, finyear, vendorcode);
        if (dta != null && dta.Rows.Count > 0)
        {
            str = "";
            for (int n = 0; n < dta.Rows.Count; n++)
            {
                if (vendorbilno == dta.Rows[n]["VENDOR_BILL_NO"].ToString())
                {
                    str = "Entered Bill No. Already Exist";
                }

            }

        }
        return str;

    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string DataSave(string ddlaction, string processid, string instanceid, string username, string txtremark, string xmlother, string requestno, string txtwiid, string advtype, string Init_Email, string txt_Initiator)
    {

        string DisplayData = "";
        // string remark = txtRemark.Value;

        string isInserted = string.Empty;
        string ref_data = string.Empty;
        string ref_data1 = string.Empty;
        string audittext = "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";
        if (ddlaction == "Approve")
        {

            string IsData1 = string.Empty;

            string str = string.Empty;


            string isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "update", ref ref_data, "1", processid, instanceid, audittext, username, txtremark, ddlaction);

            if (advtype == "2")
            {

                string vehiclexml_string = xmlother.ToString();
                vehiclexml_string = vehiclexml_string.Replace("&", "&amp;");
                vehiclexml_string = vehiclexml_string.Replace(">", "&gt;");
                vehiclexml_string = vehiclexml_string.Replace("<", "&lt;");
                vehiclexml_string = vehiclexml_string.Replace("||", ">");
                vehiclexml_string = vehiclexml_string.Replace("|", "<");
                vehiclexml_string = vehiclexml_string.Replace("'", "&apos;");
                xmlother = vehiclexml_string.ToString();

                string isupdate = (string)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "updatevendor", ref ref_data1, xmlother.ToString());
            }

            if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
            {
                DisplayData = "Error";
            }
            else
            {
                string[] Dval = new string[1];
                Dval[0] = username;

                bool isCreate = (bool)WFE.Action.ReleaseStep(processid, instanceid, "276", "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "APPROVE", username, username, "", "", "", "", "", "", "", "", "", "", Dval, requestno, txtwiid, ref isInserted);
                if (isCreate)
                {
                    try
                    {
                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been Approved.</font></pre><p/> <pre><font size='3'>Request No: " + requestno + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                        string emailid = (string)ActionController.ExecuteAction(username, "Advance_Request.aspx", "insetmaildata", ref isInserted, processid, instanceid, 0, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "APPROVE", Init_Email, "", msg, "Request No: " + requestno);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        DisplayData = "Advance Request has been Approved...!";
                    }
                }
            }//

        }
        else if (ddlaction == "Reject")
        {

            string audittext1 = "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";
            string isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "update", ref ref_data, "2", processid, instanceid, audittext1, username, txtremark, ddlaction);
            if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
            {
                string[] errmsg = ref_data.Split(':');
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
            }
            else
            {
                string[] Dval = new string[1];
                Dval[0] = username;
                //bool isCreate = true;
                bool isCreate = (bool)WFE.Action.ReleaseStep(processid, instanceid, "274", "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "REJECT", username, username, "", "", "", "", "", "", "", "", "", "", Dval, requestno, txtwiid, ref isInserted);
                if (isCreate)
                {
                    try
                    {
                        // string auditid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Advance_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "USER", txt_Username.Text, "REJECT", txtRemark.Value, "0", "0");

                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been Rejected.</font></pre><p/> <pre><font size='3'>Request No: " + requestno + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                        string emailid = (string)ActionController.ExecuteAction(username, "Advance_Request.aspx", "insetmaildata", ref isInserted, processid, instanceid, 0, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "REJECT", Init_Email, "", msg, "Request No: " + requestno);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        DisplayData = "Advance Request has been Rejected...!";
                        // Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Advance Request has been Rejected...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                    }
                }
            }
        }
        else if (ddlaction == "Send-Back")
        {
            //txt_Condition.Text = "3";
            string audittext2 = "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";
            string isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "update", ref ref_data, "3", processid, instanceid, audittext2, username, txtremark, ddlaction);
            if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
            {
                DisplayData = "error";
            }
            else
            {
                string[] Dval = new string[1];
                Dval[0] = txt_Initiator;
                bool isCreate = (bool)WFE.Action.ReleaseStep(processid, instanceid, "275", "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "SEND-BACK", username, username, "", "", "", "", "", "", "", "", "", "", Dval, requestno, txtwiid, ref isInserted);

                if (isCreate)
                {
                    try
                    {
                        //string auditid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Advance_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "USER", txt_Username.Text, "SEND-BACK", txtRemark.Value, "0", "0");

                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been Sent back to Initiator.</font></pre><p/> <pre><font size='3'>Request No: " + requestno + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                        string emailid = (string)ActionController.ExecuteAction(username, "Advance_Request.aspx", "insetmaildata", ref isInserted, processid, instanceid, 0, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "SEND-BACK", Init_Email, "", msg, "Request No: " + requestno);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        DisplayData = "Advance Request has been Sent back to Initiator...!";
                        // Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Advance Request has been Sent back to Initiator...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                    }
                }

            }
        }
        return DisplayData.ToString();
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
                divalldata.InnerHtml = getdata.fillDocumentDtl("ADVANCE", spn_req_no.InnerHtml);

            }
            catch (Exception ex)
            {
                // Logger.WriteEventLog(false, ex);
            }
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


    public string getLine_Item()
    {
        string line_item = "";
        string isdata = "";
        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "getdetails", ref isdata, txtProcessID.Text, txtInstanceID.Text);
        if (dsData != null)
        {
            
            line_item = "0001" + "$" + spn_req_no.InnerHtml;
            line_item += "|" + "0002" + "$" + "Foreign Travel Advance";
            line_item += "|" + "0003" + "$" + "Foreign Travel Advance";
            line_item += "|" + "0004" + "$" + "Visit To "+Convert.ToString(dsData.Tables[0].Rows[0]["TCOUNTRYNAME"]);
            line_item += "|" + "0005" + "$" + span_ename.InnerHtml;
            line_item += "|" + "0006" + "$" + Convert.ToString(dsData.Tables[0].Rows[0]["TCOUNTRYNAME"]);
            line_item += "|" + "0007" + "$" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["FROM_DATE"]).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(dsData.Tables[0].Rows[0]["TO_DATE"]).ToString("dd-MMM-yyyy");
            line_item += "|" + "0008" + "$" + Convert.ToInt32(Convert.ToDecimal(dsData.Tables[0].Rows[0]["CURRENCY_AMOUNT"]) + Convert.ToDecimal(dsData.Tables[0].Rows[0]["FOREX_CARD"]));
            line_item += "|" + "0009" + "$" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]);
            line_item += "|" + "0010" + "$" + txt_exchange_Rate.Text;
            line_item += "|" + "0011" + "$" + "0";
            line_item += "|" + "0012" + "$" + "Visit To " + Convert.ToString(dsData.Tables[0].Rows[0]["TCOUNTRYNAME"]);
            
        }
        return line_item;
    }
}

