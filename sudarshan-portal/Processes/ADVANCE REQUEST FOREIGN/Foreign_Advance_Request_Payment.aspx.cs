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

public partial class Foreign_Advance_Request_Payment : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Foreign_Advance_Request_Payment));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Session["USER_ADID"].ToString();
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                        step_name.Text = Convert.ToString(Request.QueryString["step"]);
                    }
                    step_name.Text = "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";
                    vendor_bill.Attributes.Add("readonly", "true");
                    Initialization();
                    fillPolicy_Details();
                    fillAuditTrail();
                    fillApprover();
                }
            }
        }
        catch (Exception Exc) { }
    }

    public void fillApprover()
    {
        string ref_data = "";
        DataTable DTAP = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "getaccapprover", ref ref_data, "ACCOUNT PAYMENT PERSONNEL", 0, 2);
        if (DTAP != null && DTAP.Rows.Count > 0)
        {
            ddlApprover.DataSource = DTAP;
            ddlApprover.DataTextField = "EMPLOYEE_NAME";
            ddlApprover.DataValueField= "USER_ADID";
            ddlApprover.DataBind();
            ddlApprover.Items.Insert(0, "--Select One--");
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

    private void fillPolicy_Details()
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


                DataTable dtamt1 = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref isValid, lbl_Grade.Text, "AdDesignationForeign");

                if (dtamt1 != null && dtamt1.Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Grade</th><th>Country</th><th>Amount(Foreign Currency)</th><th>Currency</th></tr></thead>";

                    for (int i = 0; i < dtamt1.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dtamt1.Rows[i]["GRADE"]) + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["COUNTRY_NAME"]) + "</td><td style='text-align:right'>" + Convert.ToString(dtamt1.Rows[i]["FTE_AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["ADV_CURRENCY"]) + "</td></tr>";
                    }
                    DisplayData += "</table>";
                }

                policy_data.InnerHtml = DisplayData;
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
        string IsData = string.Empty;
        string IsDatam = string.Empty;
        string IsData1 = string.Empty;
        string IsData2 = string.Empty;
        string IsData3 = string.Empty;

        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "addetails", ref IsData, txtProcessID.Text, txtInstanceID.Text);
        if (dsData != null)
        {
            if (dsData.Tables[0].Rows.Count > 0)
            {
                pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_FTA_HDR_ID"]);
                initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["emp_adid"]);
                spn_req_no.Text = txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["request_no"]);
                req_date.Text = Convert.ToDateTime(dsData.Tables[0].Rows[0]["CREATION_DATE"]).ToString("dd-MMM-yyyy");
                spn_F_Date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["FROM_DATE"]).ToString("dd-MMM-yyyy");
                spn_T_Date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["TO_DATE"]).ToString("dd-MMM-yyyy");
                spn_F_Region.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["F_COUNTRY"]);
                spn_T_Region.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["T_COUNTRY"]);
                spn_F_City.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["F_CITY"]);
                spn_T_City.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["T_CITY"]);
                pk_base_currency.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["BASE_CURRENCY"]);
                base_currency.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY_NAME"]);
                base_currency_rate.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["BASE_CURRENCY_RATE"]);
                spn_Remark.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REMARK"]);
                txt_amt_limit.Text = Convert.ToString(dsData.Tables[0].Rows[0]["FTE_AMOUNT"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMAIL_ID"]);
                txt_Deviate.Text = Convert.ToString(dsData.Tables[0].Rows[0]["REQ_DEVIATE"]);

                bindFin_Year();
                bindVendor_List();

                int days = 0;
                DateTime fdate = Convert.ToDateTime(spn_F_Date.InnerHtml);
                DateTime tdate = Convert.ToDateTime(spn_T_Date.InnerHtml);
                days = Convert.ToInt32((tdate - fdate).TotalDays) + 1;
                lbl_allowedamt.Text = Convert.ToString(days * Convert.ToInt32(txt_amt_limit.Text));
                req_base_currency.InnerHtml = Convert.ToString(dsData.Tables[3].Rows[0]["REQ_AMOUNT"]);
                bind_Line_Item(dsData.Tables[1]);
                fillDocument_Details(dsData.Tables[2]);
            }
        }
        DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "pgettraveluser", ref IsData, initiator.Text);
        if (dtUser != null && dtUser.Rows.Count > 0)
        {
            lbl_EmpCode.Text = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
            lbl_EmpName.Text = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
            lbl_Dept.Text = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
            lbl_Grade.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
            lbl_CostCenter.Text = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
            lbl_MobileNo.Text = dtUser.Rows[0]["MOBILE_NO"].ToString();
            lbl_desgnation.Text = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
            txt_desg_id.Text = Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);
            // lbl_GLCode.Text = Convert.ToString(dtUser.Rows[0]["GL_CODE"]);

            lbl_division.Text = "NA";
            if (Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]).Trim() != "")
            {
                span_Ifsc.InnerHtml = Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]);
            }
            if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]) != "")
            {
                lbl_bankAccNo.Text = Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]);
            }
            else
            {
                lbl_bankAccNo.Text = "NA";
            }
            DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "pgettravelrequestapprover", ref IsData1, initiator.Text);
            if (dtApprover != null)
            {
                if (dtApprover.Rows.Count > 0)
                {
                    if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                    {
                        lbl_AppName.Text = Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                        txt_approvar.Text = Convert.ToString(dtApprover.Rows[0]["approver"]);
                    }
                    else
                    {
                        lbl_AppName.Text = "NA";
                        txt_approvar.Text = "NA";
                    }
                    txt_Approver_Email.Text = Convert.ToString(dtApprover.Rows[0]["approver_email"]);
                    if (Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "0")
                    {
                        doa_user.Text = span_DApprover.InnerHtml = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                        span_Dapp_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);

                    }
                    else
                    {
                        doa_user.Text = span_Dapp_name.InnerHtml = span_DApprover.InnerHtml = "NA";
                    }
                    doa_email.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver_email"]);
                }

            }
        }

    }

    protected void bindFin_Year()
    { 
	int mon=DateTime.Now.Month;
        ddlFin_Year.Items.Clear();
        ListItem li = new ListItem("--Select One--", "0");
	ListItem li1 = new ListItem();
	if(mon>3)
	{
        	li1 = new ListItem(DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString(), "1");
	}
	else
	{
        	li1 = new ListItem((DateTime.Now.Year - 1).ToString() + "-" + (DateTime.Now.Year).ToString(), "1");
	}
        ddlFin_Year.Items.Insert(0, li);
        ddlFin_Year.Items.Insert(1, li1);
        
    }

    protected void bindVendor_List()
    {
        string IsVendor = "";
        ListItem li = new ListItem("--Select One--", "0");
        DataTable dtvendor = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "getvendorpaymodewise", ref IsVendor, 1);
        ddlVendor.Items.Clear();
        if (dtvendor != null && dtvendor.Rows.Count > 0)
        {
            ddlVendor.DataSource = dtvendor;
            ddlVendor.DataTextField = "VENDOR_NAME";
            ddlVendor.DataValueField = "PK_ADVANCE_F_VENDOR_ID";
            ddlVendor.DataBind();
        }
        ddlVendor.Items.Insert(0, li);
    }

    protected void bind_Line_Item(DataTable dtLine)
    {
        decimal tot_amount = 0;
        string data = "";
        data = "<table class='table table-bordered' id='tbl_Data' style='border: 1px ridge grey'><thead>";
        data += "<tr class='grey'><th style='width: 2%; text-align: center'>#</th><th style='width: 10%; text-align: center'>Requested Currency</th>";
        data += "<th style='width: 10%; text-align: center'>Currency Mode</th><th style='width: 10%; text-align: center'>Currency Amount</th>";
        data += "<th style='width: 10%; text-align: center'>Exchange Rate</th><th style='width: 15%; text-align: center'>Equivalent To Base Currency</th>";
        data += "<th style='width: 10%; text-align: center'>Equivalent INR Amount</th><th style='width: 15%; text-align: center'>Service Charge</th>";
        data += "<th style='width: 10%; text-align: center'>Total Amount</th>";
        data += "</tr></thead><tbody>";
        if (dtLine != null && dtLine.Rows.Count > 0)
        {
            for (int index = 0; index < dtLine.Rows.Count; index++)
            {
                data += "<tr>";
                data += "<td>" + Convert.ToString(index + 1) + "</td>";
                data += "<td><span id='fk_currency" + (index + 1) + "' style='display:none'>" + Convert.ToString(dtLine.Rows[index]["FK_CURRENCY"]) + "</span><span id='span_cur_name" + (index + 1) + "'>" + Convert.ToString(dtLine.Rows[index]["CURRENCY_NAME"]) + "</span></td>";
                data += "<td><span id='currency_mode" + (index + 1) + "'>" + Convert.ToString(dtLine.Rows[index]["CURRENCY_MODE"]) + "</span></td>";
                data += "<td><span id='spn_amount" + (index + 1) + "'>" + Convert.ToInt32(dtLine.Rows[index]["CURRENCY_AMOUNT"]) + "</span></td>";
                data += "<td><input type='text' value='' id='exc_rate" + (index + 1) + "' style='text-align:right' class='form-control input-sm' onkeyup='calculate_Total_INR()' onchange='validateFloatKey(this);'></input></td>";
                data += "<td><span id='eq_base_currency"+(index+1)+"'>0</span></td>";
                data += "<td><span id='eq_inr_amount" + (index + 1) + "'>0</span></td>";
                data += "<td><input type='text' value='' id='ser_charge" + (index + 1) + "' style='text-align:right' class='form-control input-sm' onkeyup='calculate_Total()' onchange='validateFloatKey(this);'></input></td>";
                data += "<td><span id='total_Line" + (index + 1) + "'>0</span></td>";
                data += "</tr>";
                tot_amount = tot_amount + Convert.ToDecimal(dtLine.Rows[index]["CURRENCY_AMOUNT"]);
            }
            data += "<tr style='font-weight:bold'>";
            data += "<td colspan='3' style='text-align:right'><b>Total : </b></td>";
            data += "<td><span id='btm_curr_total'>" + tot_amount.ToString("#.000") + "</span></td>";
            data += "<td><span id='btm_exc_total'>0.000</span></td>";
            data += "<td><span id='btm_base_total'>0.000</span></td>";
            data += "<td><span id='btm_inr_total'>0.000</span></td>";
            data += "<td><span id='btm_serv_total'>0.000</span></td>";
            data += "<td><span id='btm_total'>0.000</span></td>";
            data += "</tr>";
        }
        data += "</tbody></table>";
        div_details.InnerHtml = Convert.ToString(data);
    }

    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "script", "window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');", true);
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
        }
    }

    private void fillDocument_Details(DataTable dtDocs)
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

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Type</th><th>File Name</th></tr></thead><tbody>";
                if (dtDocs != null && dtDocs.Rows.Count > 0)
                {
                    for (int index = 0; index < dtDocs.Rows.Count; index++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dtDocs.Rows[index]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick='download_files(" + (index + 1) + ")'>" + Convert.ToString(dtDocs.Rows[index]["FILENAME"]) + "</a></td></tr>";
                    }
                }
                DisplayData += "</tbody></table>";
                div_Doc.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
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

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                divIns.Style.Add("display","none");
                string isInserted = string.Empty;
                string ref_data = string.Empty;
                txt_Audit.Text = "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";
                if (ddlAction.SelectedItem.Text == "Approve")
                {
                    string rfc_flag_chk = string.Empty;
                    string rfc_flag_no = string.Empty;
                /**********************************************************************************************************************************************************/
                    if (txt_release.Text != "1")
                    {
                        string rfc_no = "";
                        string rfc_string = string.Empty;
                        string rfc_string1 = string.Empty;
                        string line_item = string.Empty;
                        string bank_flag = string.Empty;
                        string bank_no = string.Empty;
                        string ref_key_no = "";


                        DataTable dtManage = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata1", ref ref_data, spn_req_no.Text, "SELECT", "", "", "", "", "", "", "");
                        if (dtManage != null && dtManage.Rows.Count > 0)
                        {
                            bank_flag = Convert.ToString(dtManage.Rows[0]["BANK_FLAG"]);
                            bank_no = Convert.ToString(dtManage.Rows[0]["BANK_NO"]);
                            rfc_flag_chk = bank_flag;
                            rfc_flag_no = bank_no;
                        }
                        //bank_flag = "S";
                        if (bank_flag == "E" || bank_flag == "")
                        {
                            string rfc_action = "ADVANCE PAID FOREIGN";

                            DataSet dt_sap_rfc = (DataSet)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "getbankrfc1", ref ref_data, 1, rfc_action, spn_req_no.Text, 1, ddlVendor.SelectedValue, inr_amount.Text, serv_amount.Text, vendor_bill.Text);
                            if (dt_sap_rfc != null)
                            {
                                if (dt_sap_rfc.Tables[0].Rows.Count > 0)
                                {
                                    string ref_no = Convert.ToString(dt_sap_rfc.Tables[0].Rows[0][0]);
                                    ref_key_no = ref_no;
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
                                        rfc_string += "$" + vendor_no.Text;
                                        rfc_string += "$" + (index + 1).ToString();
                                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["GL_ACCOUNT"]);
                                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["REF_KEY_1"]);
                                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["VENDOR_NO"]);
                                        rfc_string += "$" + spn_req_no.Text;
                                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ITEM_TEXT"]);
                                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["BUS_AREA"]);
                                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["COSTCENTER"]);
                                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["PROFIT_CTR"]);
                                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["CURRENCY"]);
                                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["AMT_DOCCUR"]);
                                        rfc_string += "$" + txt_pay_mode_sap.Text.ToUpper();
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

                                line_item = getLine_Item();
				//Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + rfc_string1 + "');}</script>");
                                Vendor_Portal.Vendor_Portal_DetailsService Vendor = new Vendor_Portal.Vendor_Portal_DetailsService();
                                string[] Vendor_data_array = new string[3];
                                Vendor_data_array = Vendor.BANK_DETAILS(rfc_string1, line_item);
                                //Vendor_data_array[0] = "";
                                rfc_flag_chk = bank_flag = Convert.ToString(Vendor_data_array[1]);
                                string[] sp_data = Convert.ToString(Vendor_data_array[0]).Split(' ');

                                if (Convert.ToString(Vendor_data_array[1]) == "S")
                                {
                                    for (int k = 0; k < sp_data.Length; k++)
                                    {
                                        if (Convert.ToString(sp_data[k]).ToUpper().Contains("SCIL"))
                                        {
                                            rfc_flag_no = rfc_no = Convert.ToString(sp_data[k]).Substring(0, 10);
                                        }
                                    }
                                }
                                string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref ref_data, spn_req_no.Text, "BANK", "", "", "", Convert.ToString(Vendor_data_array[1]).ToUpper(), rfc_no, Convert.ToString(Vendor_data_array[0]), ref_key_no);
                            }
                        }
                    }
                    else
                    {
                        rfc_flag_chk = "S";
                    }
                        if (rfc_flag_chk != "E" && rfc_flag_chk != "")
                        {
                            txt_Condition.Text = "1";
                            string isSaved = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "updatepayment", ref ref_data, txtProcessID.Text, txtInstanceID.Text, txt_inp_rate.Text, ddlVendor.SelectedValue, vendor_no.Text, vendor_bill.Text, ddlFin_Year.SelectedItem.Text, payment_method.Text, tax_code.Text, txt_Username.Text, txt_xml_data_vehicle.Text, ddlApprover.SelectedValue);

                            if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = ref_data.Split(':');
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                            }
                            else
                            {
                                string[] Dval = new string[1];
                                if (txt_release.Text != "1")
                                {
                                    Dval[0] = txt_Username.Text;
                                }
                                else
                                {
                                    Dval[0] = acc_approver.Text;
                                }

                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "276", "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "APPROVE", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                if (isCreate)
                                {
                                    try
                                    {
                                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Approved.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                        string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "APPROVE", Init_Email.Text, "", msg, "Request No: " + spn_req_no.Text);
                                        if (txt_release.Text == "1")
                                        {
                                            string pay_email = "";
                                            DataTable DTAP = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "getaccapprover", ref ref_data, "ACCOUNT PAYMENT PERSONNEL", 0, 2);
                                            if (DTAP != null && DTAP.Rows.Count > 0)
                                            {
                                                for (int cnt = 0; cnt < DTAP.Rows.Count; cnt++)
                                                {
                                                    if (acc_approver.Text == Convert.ToString(DTAP.Rows[cnt]["USER_ADID"]))
                                                    {
                                                        pay_email = Convert.ToString(DTAP.Rows[cnt]["EMAIL_ID"]);
                                                    }
                                                }
                                            }
                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Approved and Pending for payment.</font></pre><p/><pre><font size='3'>Request No: " + spn_req_no.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "APPROVE", pay_email, "", msg, "Request No: " + spn_req_no.Text);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                    
                                    }
                                    finally
                                    {
                                        if (txt_release.Text != "1")
                                        {
                                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been Approved : " + rfc_flag_no + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                        }
                                        else
                                        {
                                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been Approved and sent for payment');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                        }
                                    }
                                }
                            }
                        }
                        /***************************************************************************************************************************************/
                        

                    
                /**********************************************************************************************************************************************************/
                }
                else if (ddlAction.SelectedItem.Text == "Reject")
                {
                    txt_Condition.Text = "2";
                    txt_Audit.Text = "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, spn_Remark.InnerHtml, ddlAction.SelectedItem.Text);
                    if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = ref_data.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Username.Text;
                        string ref_data1 = string.Empty;
                        string release_id = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, step_name.Text, "REJECT");
                        if (release_id != "")
                        {
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Rejected.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST APPROVAL", "REJECT", Init_Email.Text, "", msg, "Request No: " + spn_req_no.Text);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been Rejected.');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                }
                else //if (ddlAction.SelectedItem.Text == "Send-Back")
                {
                    txt_Condition.Text = "3";
                    txt_Audit.Text = "FOREIGN ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, spn_Remark.InnerHtml, ddlAction.SelectedItem.Text);
                    if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = ref_data.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = initiator.Text;
                        string ref_data1 = string.Empty;
                        string release_id = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, step_name.Text, "SEND-BACK");
                        if (release_id != "")
                        {
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "SEND-BACK", txt_Username.Text, txt_approvar.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Sent back to the initiator.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST APPROVAL", "SEND-BACK", Init_Email.Text, "", msg, "Request No: " + spn_req_no.Text);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been Sent back to the initiator...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                        }

                    }
                }
            }

        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + ex.ToString() + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static int GetDuplicate(string finyear, string vendorcode, string vendorbilno)
    {
        string IsData1 = string.Empty;
        int dta = 0;
	DataTable dts=new DataTable();
        dts = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "getcheckduplicate", ref IsData1, finyear, vendorcode,vendorbilno);
	if(dts!=null && dts.Rows.Count>0)
	{
		dta=Convert.ToInt32(dts.Rows[0][0]);
	}
        return dta;
    }

    public string getLine_Item()
    {
        string line_item = "";
        string isdata = "";
        decimal total_currency = 0;
        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "addetails", ref isdata, txtProcessID.Text, txtInstanceID.Text);
        if (dsData != null)
        {
            /*for (int index = 0; index < dsData.Tables[1].Rows.Count; index++)
            {
                total_currency = total_currency + Convert.ToDecimal(dsData.Tables[1].Rows[index]["EQ_BASE_AMOUNT"]);
            }*/
            if(bottom_amount.Text!="")
            {
		total_currency=Convert.ToDecimal(bottom_amount.Text);
            }
            
            line_item = "0001" + "$" + spn_req_no.Text;
            line_item += "|" + "0002" + "$" + "Foreign Travel Advance";
            line_item += "|" + "0003" + "$" + "Foreign Travel Advance";
            line_item += "|" + "0004" + "$" + "Visit To " + Convert.ToString(dsData.Tables[0].Rows[0]["T_COUNTRY"]);
            line_item += "|" + "0005" + "$" + lbl_EmpName.Text;
            line_item += "|" + "0006" + "$" + Convert.ToString(dsData.Tables[0].Rows[0]["T_COUNTRY"]);
            line_item += "|" + "0007" + "$" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["FROM_DATE"]).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(dsData.Tables[0].Rows[0]["TO_DATE"]).ToString("dd-MMM-yyyy");
            line_item += "|" + "0008" + "$" + Convert.ToInt32(total_currency);
            line_item += "|" + "0009" + "$" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY_NAME"]);
            line_item += "|" + "0010" + "$" + txt_inr_rate.Text;
            line_item += "|" + "0011" + "$" + "0";
            line_item += "|" + "0012" + "$" + "Visit To " + Convert.ToString(dsData.Tables[0].Rows[0]["T_COUNTRY"]);

        }
        return line_item;
    }
}

