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
using System.Xml;
using InfoSoftGlobal;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;

public partial class Mobile_DataCard_Expense_Modification : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Mobile_DataCard_Expense_Modification));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Session["USER_ADID"].ToString();
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                    }
                    FillMonth();
                    FillServiceProvider();
                    FillYear();
                    FillMode();
                    FillLocation();
                    Initialization();
                    fillDocument_Details();
                    fillAuditTrailData();
                    fillPolicy_Details();
                    FillSupport();
                    fillSupporting();
                    fillDedction();
                }
            }
        }
        catch (Exception Exc) { }
    }
    protected void fillSupporting()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtSupp = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getsupportingperc", ref isdata, "Mobile Reimbursement");
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
            DataTable dtSuppda = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getsupportingperc", ref isdata, "DataCard Reimbursement");
            if (dtSuppda != null)
            {
                if (dtSuppda.Rows.Count > 0)
                {
                    supp_perc_no_d.Text = Convert.ToString(dtSupp.Rows[0]["S_NO"]);
                }
                else
                {
                    supp_perc_no_d.Text = "0";
                }
            }
            else
            {
                supp_perc_no_d.Text = "0";
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
    private void FillSupport()
    {
        StringBuilder str1 = new StringBuilder();
        str1.Append("<option value='0'>--Select One--</option>");
        txt_ddlsupport.Text += "Y" + "~" + "Yes" + ";";
        txt_ddlsupport.Text += "N" + "~" + "No" + ";";
        txt_ddlsupport.Text = txt_ddlsupport.Text.Remove(txt_ddlsupport.Text.Length - 1);
        txt_ddlsupport1.Text += "Y" + "~" + "Yes" + ";";
        txt_ddlsupport1.Text += "N" + "~" + "No" + ";";
        txt_ddlsupportuser.Text = txt_ddlsupport1.Text.Remove(txt_ddlsupport1.Text.Length - 1);

        str1.Append("<option value='Y'>Yes</option>");
        str1.Append("<option value='N'>No</option>");
        txt_ddlsuppddl.Text = str1.ToString();

    }
    private void Initialization()
    {
        string IsValid = string.Empty;
        string IsData = string.Empty;
        string IsDatam = string.Empty;
        string IsData1 = string.Empty;
        string IsData2 = string.Empty;
        string supprt_doc = string.Empty;
        string IsData3 = string.Empty;
        string suppdoc = string.Empty;
        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "pgetrequestdata", ref IsData, txtWIID.Text);
        if (dsData != null)
        {
            txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_MobileCard_Expense_HDR_Id"]);
            txt_Username.Text = Convert.ToString(dsData.Tables[0].Rows[0]["CREATED_BY"]);
            Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);
            lblExpenseHead.Text = dsData.Tables[0].Rows[0]["FK_EXPENSEHEAD_ID"].ToString();
        }
        DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "pgettraveluser", ref IsData, txt_Username.Text);
        if (dtUser != null && dtUser.Rows.Count > 0)
        {

            lbl_EmpCode.Text = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
            lbl_EmpName.Text = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
            lbl_Dept.Text = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
            lbl_Grade.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
            lbl_CostCenter.Text = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
            //lbl_date.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            lbl_desgnation.Text = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
            lbl_division.Text = "NA";
            if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]) != "")
            {
                lbl_bankAccNo.Text = Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]);
            }
            else
            {
                lbl_bankAccNo.Text = "NA";
            }
            if (Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]).Trim() != "")
            {
                span_Ifsc.InnerHtml = Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]);
            }
            if (lblExpenseHead.Text == "Mobile")
            {
                //////For Supporting///////////////

                DataTable dtsupp = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "checksupport", ref IsData, lblExpenseHead.Text);
                span_support2.Text = span_support1.Text = Convert.ToString(dtsupp.Rows[0]["IS_SUPPORTING"]);
                ///////////////////////////////////
                DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "pgettravelrequestapprover", ref IsData1, txt_Username.Text);
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
                            span_DApprover.InnerHtml = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                            span_Dapp_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);
                        }
                        else
                        {
                            span_Dapp_name.InnerHtml = span_DApprover.InnerHtml = "NA";
                        }

                    }

                }
            }
            else if (lblExpenseHead.Text == "DataCard")
            {
                //////For Supporting///////////////
                DataTable dtsupp = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "checksupport", ref IsData, lblExpenseHead.Text);
                supprt_doc = Convert.ToString(dtsupp.Rows[0]["IS_SUPPORTING"]);
                ///////////////////////////////////////
                DataTable DTAP = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getaccapprover", ref IsData1, "DATACARD  EXPENSE  APPROVAR");
                if (DTAP.Rows.Count > 0)
                {
                    for (int i = 0; i < DTAP.Rows.Count; i++)
                    {
                        lbl_AppName.Text = Convert.ToString(DTAP.Rows[i]["EMPLOYEE_NAME"]);
                        txt_approvar.Text = Convert.ToString(DTAP.Rows[i]["USER_ADID"]);
                        if (txt_Approver_Email.Text == "")
                        {
                            txt_Approver_Email.Text = Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                        }
                        else
                        {
                            txt_Approver_Email.Text = txtApproverEmail.Text + ',' + Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                        }
                    }

                }
                else
                {
                    lbl_AppName.Text = "NA";
                    txt_approvar.Text = "NA";
                }
            }
        }
        DataTable dtm = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref IsDatam, lbl_EmpCode.Text, "AdMobile");
        if (dtm != null && dtm.Rows.Count > 0)
        {
            lbl_MobileNo.Text = dtm.Rows[0]["Mobile_No"].ToString();
            txt_cmobiole.Text = dtm.Rows[0]["Mobile_No"].ToString();
        }
        else
        {
            lbl_MobileNo.Text = dtUser.Rows[0]["MOBILE_NO"].ToString();
        }

        DataTable ds = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Modification.aspx", "addetails", ref IsDatam, txt_Username.Text, txtProcessID.Text, txtInstanceID.Text);
        if (ds != null && ds.Rows.Count > 0)
        {
            StringBuilder html_Header = new StringBuilder();
            StringBuilder html_Header1 = new StringBuilder();
            txt_count.Text = ds.Rows.Count.ToString();
            lblExpenseHead.Text = ds.Rows[0]["FK_EXPENSEHEAD_ID"].ToString();
            ddlPayMode.Items.FindByValue(ds.Rows[0]["PAYMENT_MODE1"].ToString()).Selected = true;
            if (ds.Rows[0]["PAYMENT_MODE1"].ToString() != "2")
            {
                ddlLocation.Items.FindByValue(ds.Rows[0]["LOCATION"].ToString()).Selected = true;
            }
            if (lblExpenseHead.Text == "Mobile")
            {
                DataTable dtg = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref IsData, lbl_Grade.Text, "AdUser");
                if (dtg.Rows.Count > 0)
                {
                    txt_Mreimbursement.Text = dtg.Rows[0]["Grade_Expense"].ToString();
                    txt_user_Mreimbursement.Text = dtg.Rows[0]["Grade_Expense"].ToString();
                }
            }
            lbl_requestNo.Text = ds.Rows[0]["REQUEST_NO"].ToString();
            lbl_date.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            //ddldedYear.Items.FindByValue(ds.Rows[0]["DEDUCTION_YEAR"].ToString()).Selected = true;
            //ddlDeduMonth.Items.FindByValue(ds.Rows[0]["DEDUCTION_MONTH"].ToString()).Selected = true;
            //txt_deductionamt.InnerHtml = ds.Rows[0]["DEDUCTION_AMOUNT"].ToString();
            //lbl_GLCode.Text = ds.Rows[0]["SAP_GLCode"].ToString();
            decimal total_Amount = 0;
            decimal total_ded_Amount = 0;
            if (ds.Rows[0]["FK_EXPENSEHEAD_ID"].ToString() == "Mobile")
            {

                if (txt_cmobiole.Text != "")
                {
                    html_Header.Append("<table class='table table-bordered' id='tbl_Mobile' > <thead>");
                    html_Header.Append("<tr><th>Add</th><th >Service Provider</th><th >Year</th><th>Month</th><th>Bill No</th> <th>Bill Date</th><th>Bill Amount</th><th>Rei. Amount</th> <th>Limit</th><th>Supporting Doc</th><th>Remark</th><th>Delete</th></tr></thead> <tbody>");

                    if (ds.Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Rows.Count; i++)
                        {
                            if (ds.Rows[i]["SUPPORT_DOC"].ToString() == "Y")
                            {
                                suppdoc = "Yes";
                            }
                            else if (ds.Rows[i]["SUPPORT_DOC"].ToString() == "N")
                            {
                                suppdoc = "No";
                            }
                            html_Header.Append("<tr><td class='add_Mobile'  id='add_Row" + (i + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>");
                            html_Header.Append("<td><select id='ddlServiceProvider" + (i + 1) + "'  class='form-control  input-sm width-xs'><option selected='selected' value='" + ds.Rows[i]["FK_SERVICEPROVIDER_ID"] + "'>" + ds.Rows[i]["Provider_Name"] + "</option></select></td><td><select id='ddlYear" + (i + 1) + "'   class='form-control  input-sm width-100' onchange='comdedyear(" + (i + 1) + ")'><option selected='selected' value='" + ds.Rows[i]["YEAR"] + "'>" + ds.Rows[i]["YEAR"] + "</option></select></td><td><select id='ddlMonth" + (i + 1) + "' class='form-control  input-sm width-100' onchange='comdedmonth(" + (i + 1) + ")'><option selected='selected' value='" + ds.Rows[i]["Month"] + "'>" + ds.Rows[i]["MonthName"] + "</option></select></td>");
                            html_Header.Append(" <td><input type='text' class='form-control  input-sm width-100' id='txt_mobile_bill_no" + (i + 1) + "' value ='" + ds.Rows[i]["BILL_NO"] + "' runat='server' /></td><td ><div class='input-group'> <input type='text' class='form-control  input-sm width-100 datepicker-dropdown'  id='txt_mobile_bill_date" + (i + 1) + "' readonly=''  value='" + ds.Rows[i]["Bill_Date"] + "' runat='server'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button> </span></div></td> ");
                            html_Header.Append(" <td><input type='text' class='form-control  input-sm width-100' id='txt_mobile_bill_amt" + (i + 1) + "' value ='" + ds.Rows[i]["BILL_AMOUNT"] + "' runat='server' onkeypress='return isNumberKey(event)'/></td><td ><input type='text' class='form-control  input-sm width-100' id='txt_total" + (i + 1) + "' value ='" + ds.Rows[i]["REIMBURSEMENT_AMOUNT"] + "' runat='server' onkeypress='return isNumberKey(event)'  onchange='value1billamt(" + (i + 1) + ")' /></td> <td><label id='txt_Mreimbursement' >" + txt_Mreimbursement.Text + "</label></td><td><select id='ddl_support" + (i + 1) + "'  class='form-control  input-sm width-xs width-60'><option selected='selected' value='" + suppdoc + "'>" + suppdoc + "</option></select></td>");
                            html_Header.Append("<td><input type='text' class='form-control input-sm width-200' id='txt_Mremark" + (i + 1) + "' runat='server' value='" + ds.Rows[i]["REMARK"] + "'/></td>");
                            html_Header.Append("<td id='delete_Row" + (i + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_Row(" + (i + 1) + ")'></i></td>");
                            html_Header.Append("<td><input type='hidden' id='txtpk" + (i + 1) + "' value='" + ds.Rows[i]["PK_MobileCard_Expense_HDR_Id"] + "'></input><input type='hidden' id='txtfk" + (i + 1) + "' value='" + ds.Rows[i]["PK_MobileCard_Expense_HDR_Id"] + "'></input><input type='hidden' id='txtaddnewrow" + (i + 1) + "' value=''></input></td></tr>");
                            total_Amount = total_Amount + Convert.ToDecimal(ds.Rows[i]["REIMBURSEMENT_AMOUNT"]);
                            ClientScript.RegisterStartupScript(this.GetType(), "FillServicePro" + (i + 1) + "ddl", "FillServicePro('ddlServiceProvider" + (i + 1) + "');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "FillYe" + (i + 1) + "ddl", "FillYe('ddlYear" + (i + 1) + "');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "FillMon" + (i + 1) + "ddl", "FillMon('ddlMonth" + (i + 1) + "');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "FillSupp" + (i + 1) + "ddl", "FillSupp('ddl_support" + (i + 1) + "');", true);
                        }

                        html_Header.Append(" </tbody></table>");
                        tab_mobile.InnerHtml = html_Header.ToString();
                        spn_Total.InnerHtml = Convert.ToString(total_Amount);
                        string data = string.Empty;
                        try
                        {
                            string isValid = string.Empty;
                            StringBuilder tblHTML = new StringBuilder();
                            DataSet dtded = (DataSet)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getdeddetail", ref isValid, lbl_EmpCode.Text, txt_pk_id.Text);
                            if (dtded != null && dtded.Tables[1].Rows.Count > 0)
                            {
                                tblHTML.Append("<table ID='tbldedu1' class='table table-bordered'><thead><tr class='grey'><th>Deduction Year</th><th>Deduction Month</th><th>Amount</th></tr></thead>");
                                tblHTML.Append("<tbody>");
                                for (int Index = 0; Index < dtded.Tables[1].Rows.Count; Index++)
                                {
                                    tblHTML.Append("<tr>");
                                    tblHTML.Append("<td>" + Convert.ToString(dtded.Tables[1].Rows[Index]["DEDUCTION_YEAR"]) + "</td>");
                                    tblHTML.Append("<td>" + Convert.ToString(dtded.Tables[1].Rows[Index]["DedMonthName"]) + " </td>");
                                    tblHTML.Append("<td>" + Convert.ToString(dtded.Tables[1].Rows[Index]["DEDUCTION_AMOUNT"]) + " </td>");
                                    total_ded_Amount = total_ded_Amount + Convert.ToDecimal(dtded.Tables[1].Rows[Index]["DEDUCTION_AMOUNT"]);
                                    tblHTML.Append("</tr>");
                                }
                                tblHTML.Append("</tbody>");
                                tblHTML.Append("</table>");
                            }

                            span_dedamount.InnerHtml = Convert.ToString(total_ded_Amount);
                            txt_ded_tamount.Text = Convert.ToString(total_ded_Amount);
                            div_displaydeduction.InnerHtml = tblHTML.ToString();
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteEventLog(false, ex);
                        }
                    }
                }
                else
                {
                    html_Header1.Append("<table class='table table-bordered' id='tbl_UserMobile' > <thead>");
                    html_Header1.Append("<tr><th>Add</th><th>Mobile No</th><th>Service Provider</th><th>Bill No</th> <th>Bill Date</th><th>Bill Amount</th><th>Rei. Amount</th><th>Limit</th> <th>Tax</th><th>Total Reimb.</th><th>Supporting Doc</th> <th>Remark</th><th>Delete</th></tr></thead> <tbody>");

                    if (ds.Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Rows.Count; i++)
                        {
                            if (ds.Rows[i]["SUPPORT_DOC"].ToString() == "Y")
                            {
                                suppdoc = "Yes";
                            }
                            else if (ds.Rows[i]["SUPPORT_DOC"].ToString() == "N")
                            {
                                suppdoc = "No";
                            }
                            html_Header1.Append("<tr><td class='add_UserMobile'  id='add_user_Row" + (i + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>");
                            html_Header1.Append("<td><input type='text' class='form-control input-sm width-xs' id='txt_User_MobileNo" + (i + 1) + "' runat='server' value='" + ds.Rows[i]["MOBILE_CARD_NO"] + "'  onkeypress='return isNumberKey(event)' /></td>");
                            html_Header1.Append("<td><select id='ddlServiceProviderUser" + (i + 1) + "'  class='form-control  input-sm width-100'><option selected='selected' value='" + ds.Rows[i]["FK_SERVICEPROVIDER_ID"] + "'>" + ds.Rows[i]["Provider_Name"] + "</option></select></td>");
                            html_Header1.Append("<td><input type='text' class='form-control  input-sm width-100' id='txt_user_mobile_bill_no" + (i + 1) + "' value ='" + ds.Rows[i]["BILL_NO"] + "' runat='server' /></td><td ><div class='input-group'> <input type='text' class='form-control  input-sm width-100 datepicker-dropdown'  id='txt_mobile_user_bill_date" + (i + 1) + "' readonly=''  value='" + ds.Rows[i]["Bill_Date"] + "' runat='server'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button> </span></div></td> ");
                            html_Header1.Append("<td><input type='text' class='form-control  input-sm width-100' id='txt_mobile_user_bill_amt" + (i + 1) + "' value ='" + ds.Rows[i]["BILL_AMOUNT"] + "' runat='server' onkeypress='return isNumberKey(event)'  /></td><td ><input type='text' class='form-control  input-sm width-100' id='txt_user_total" + (i + 1) + "' value ='" + ds.Rows[i]["REIMBURSEMENT_AMOUNT"] + "' runat='server' onkeypress='return isNumberKey(event)'  onchange='valuebillamt(" + (i + 1) + ")' /></td> ");
                            html_Header1.Append(" <td><label id='txt_user_Mreimbursement' >" + txt_user_Mreimbursement.Text + "</label></td><td><input type='text' class='form-control input-sm width-100' id='txt_user_tax" + (i + 1) + "'  runat='server' onkeypress='return isNumberKey(event)' onchange='valuechan(" + (i + 1) + ")' value='" + ds.Rows[i]["TAX"] + "' /></td>");
                            html_Header1.Append("<td><asp:label ID='lbl_amttax" + (i + 1) + "' runat='server' ></asp:Label></td><td><select id='ddlusersupp" + (i + 1) + "'  class='form-control  input-sm width-xs width-60'><option selected='selected' value='" + suppdoc + "'>" + suppdoc + "</option></select></td>");
                            html_Header1.Append("<td><input type='text' class='form-control input-sm width-200' id='txt_user_Mremark" + (i + 1) + "' runat='server' value='" + ds.Rows[i]["REMARK"] + "'/></td>");
                            html_Header1.Append("<td id='delete_user_Row" + (i + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_user_Row(" + (i + 1) + ")'></i></td>");
                            html_Header1.Append("<td><input type='hidden' id='txtpk" + (i + 1) + "' value='" + ds.Rows[i]["PK_MobileCard_Expense_HDR_Id"] + "'></input><input type='hidden' id='txtfk" + (i + 1) + "' value='" + ds.Rows[i]["PK_MobileCard_Expense_HDR_Id"] + "'></input><input type='hidden' id='txtaddnewrow" + (i + 1) + "' value=''></input></td></tr>");
                            //total_Amount = total_Amount + Convert.ToDecimal(ds.Rows[i]["BILL_AMOUNT"]);
                            ClientScript.RegisterStartupScript(this.GetType(), "FillSuppUser" + (i + 1) + "ddl", "FillSuppUser('ddlusersupp" + (i + 1) + "');", true);
                            ClientScript.RegisterStartupScript(this.GetType(), "FillServicePro" + (i + 1) + "ddl", "FillServicePro('ddlServiceProviderUser" + (i + 1) + "');", true);
                        }

                        html_Header1.Append(" </tbody></table>");
                        tab_usermobile.InnerHtml = html_Header1.ToString();
                        //span_usertotal.InnerHtml = Convert.ToString(total_Amount); 
                    }

                }
            }
            else
            {
                txtCardNo.Value = ds.Rows[0]["MOBILE_CARD_NO"].ToString();
                ddlCardProvider.Items.FindByValue(ds.Rows[0]["FK_SERVICEPROVIDER_ID"].ToString()).Selected = true;
                ddlCardYear.Items.FindByValue(ds.Rows[0]["YEAR"].ToString()).Selected = true;
                ddlCardMonth.Items.FindByValue(ds.Rows[0]["MONTH"].ToString()).Selected = true;
                txt_card_billno.Value = ds.Rows[0]["BILL_NO"].ToString();
                txt_card_billdate.Value = ds.Rows[0]["Bill_Date"].ToString();
                txt_card_billamt.Value = ds.Rows[0]["BILL_AMOUNT"].ToString();
                txt_Reimbursement.Text = ds.Rows[0]["REIMBURSEMENT_AMOUNT"].ToString();
                txt_datacardtax.Value = ds.Rows[0]["TAX"].ToString();
                txt_remark.Value = ds.Rows[0]["REMARK"].ToString();
            }
            txt_Request.Text = ds.Rows[0]["REQUEST_NO"].ToString();
        }
        // }
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

                DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getpolicy", ref isValid, lbl_Grade.Text);
                if (dtamt != null && dtamt.Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Type of Vehicle</th><th>Present rate per KM (Rs)</th><th>Effective From</th><th>Effective To</th></tr></thead>";

                    for (int i = 0; i < dtamt.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dtamt.Rows[i]["GRADE_NAME"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["Grade_Expense"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["from_Date"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["to_Date"]) + "</td></tr>";

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
    private void FillMode()
    {
        String IsData = string.Empty;
        dt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdPaymentMode");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlPayMode.DataSource = dt;
            ddlPayMode.DataTextField = "PAYMENT_MODE";
            ddlPayMode.DataValueField = "PK_PAYMENT_MODE";
            ddlPayMode.DataBind();
            ddlPayMode.Items.Insert(0, Li);
        }

    }
    private void FillLocation()
    {
        String IsData = string.Empty;
        dt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdLocation");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlLocation.DataSource = dt;
            ddlLocation.DataTextField = "LOCATION_NAME";
            ddlLocation.DataValueField = "PK_LOCATION_ID";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, Li);
        }

    }
    private void FillYear()
    {
        StringBuilder str1 = new StringBuilder();
        str1.Append("<option value='0'>--Select One--</option>");
        StringBuilder str2 = new StringBuilder();
        str2.Append("<option value='0'>--Select One--</option>");
        var currentYear = DateTime.Today.Year;

        ddlCardYear.Items.Insert(0, new ListItem("--Select One--", "0"));
        for (int i = 1; i >= 0; i--)
        {

            ddlCardYear.Items.Add((currentYear - i).ToString());
            txt_year.Text += (currentYear - i).ToString() + "~" + (currentYear - i).ToString() + ";";
            str1.Append("<option value='" + (currentYear - i).ToString() + "'>" + (currentYear - i).ToString() + "</option>");
            str2.Append("<option value='" + (currentYear - i).ToString() + "'>" + (currentYear - i).ToString() + "</option>");
        }
        txt_year1.Text = str1.ToString();
        txt_year.Text = txt_year.Text.Remove(txt_year.Text.Length - 1);
        txt_user_year.Text = str2.ToString();
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
                DataTable dS = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Modification.aspx", "getfilenames", ref isData, "MOBILE DATACARD EXPENSE", txt_Request.Text);
                if (dS.Rows.Count > 0)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>Description</th><th>File Name</th><th>Delete</th></tr></thead>");
                    str.Append("<tbody>");

                    for (int i = 0; i < dS.Rows.Count; i++)
                    {
                        str.Append("<tr><td>" + Convert.ToString(dS.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a onclick='downloadfiles(" + (i + 1) + ")'>" + Convert.ToString(dS.Rows[i]["FILENAME"]) + "</a></td><td><i id='del" + dS.Rows.Count + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (dS.Rows.Count) + ");\" ></td></tr>");
                    }
                    str.Append("</tbody>");
                    str.Append("</table>");
                    div_Doc.InnerHtml = str.ToString();
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }
    private void FillMonth()
    {
        StringBuilder str1 = new StringBuilder();
        str1.Append("<option value='0'>--Select One--</option>");
        StringBuilder str2 = new StringBuilder();

        str2.Append("<option value='0'>--Select One--</option>");
        ddlCardMonth.Items.Insert(0, new ListItem("--Select One--", "0"));

        str1.Append("<option value='1'>January</option>");
        str1.Append("<option value='2'>February</option>");
        str1.Append("<option value='3'>March</option>");
        str1.Append("<option value='4'>April</option>");
        str1.Append("<option value='5'>May</option>");
        str1.Append("<option value='6'>June</option>");
        str1.Append("<option value='7'>July</option>");
        str1.Append("<option value='8'>August</option>");
        str1.Append("<option value='9'>September</option>");
        str1.Append("<option value='10'>October</option>");
        str1.Append("<option value='11'>November</option>");
        str1.Append("<option value='12'>December</option>");

        str2.Append("<option value='1'>January</option>");
        str2.Append("<option value='2'>February</option>");
        str2.Append("<option value='3'>March</option>");
        str2.Append("<option value='4'>April</option>");
        str2.Append("<option value='5'>May</option>");
        str2.Append("<option value='6'>June</option>");
        str2.Append("<option value='7'>July</option>");
        str2.Append("<option value='8'>August</option>");
        str2.Append("<option value='9'>September</option>");
        str2.Append("<option value='10'>October</option>");
        str2.Append("<option value='11'>November</option>");
        str2.Append("<option value='12'>December</option>");

        //ddlCardMonth.Items.Insert(0, new ListItem("--Select One--", "0"));
        ddlCardMonth.Items.Insert(1, new ListItem("January", "1"));
        ddlCardMonth.Items.Insert(2, new ListItem("February", "2"));
        ddlCardMonth.Items.Insert(3, new ListItem("March", "3"));
        ddlCardMonth.Items.Insert(4, new ListItem("April", "4"));
        ddlCardMonth.Items.Insert(5, new ListItem("May", "5"));
        ddlCardMonth.Items.Insert(6, new ListItem("June", "6"));
        ddlCardMonth.Items.Insert(7, new ListItem("July", "7"));
        ddlCardMonth.Items.Insert(8, new ListItem("August", "8"));
        ddlCardMonth.Items.Insert(9, new ListItem("September", "9"));
        ddlCardMonth.Items.Insert(10, new ListItem("October", "10"));
        ddlCardMonth.Items.Insert(11, new ListItem("November", "11"));
        ddlCardMonth.Items.Insert(12, new ListItem("December", "12"));

        txt_month.Text += 1 + "~" + "January" + ";";
        txt_month.Text += 2 + "~" + "February" + ";";
        txt_month.Text += 3 + "~" + "March" + ";";
        txt_month.Text += 4 + "~" + "April" + ";";
        txt_month.Text += 5 + "~" + "May" + ";";
        txt_month.Text += 6 + "~" + "June" + ";";
        txt_month.Text += 7 + "~" + "July" + ";";
        txt_month.Text += 8 + "~" + "August" + ";";
        txt_month.Text += 9 + "~" + "September" + ";";
        txt_month.Text += 10 + "~" + "October" + ";";
        txt_month.Text += 11 + "~" + "November" + ";";
        txt_month.Text += 12 + "~" + "December" + ";";
        txt_month.Text = txt_month.Text.Remove(txt_month.Text.Length - 1);
        txt_month1.Text = str1.ToString();
        txt_user_month.Text = str2.ToString();
    }
    private void FillServiceProvider()
    {
        String IsData = string.Empty;
        StringBuilder str1 = new StringBuilder();
        str1.Append("<option value='0'>--Select One--</option>");
        StringBuilder str2 = new StringBuilder();
        str2.Append("<option value='0'>--Select One--</option>");
        dt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref IsData, txt_Username.Text, "ExServicePro");
        if (dt != null && dt.Rows.Count > 0)
        {

            ddlCardProvider.DataSource = dt;
            ddlCardProvider.DataTextField = "Provider_Name";
            ddlCardProvider.DataValueField = "PK_Provider_ID";
            ddlCardProvider.DataBind();
            ddlCardProvider.Items.Insert(0, Li);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txt_serviceprovide.Text += dt.Rows[i]["PK_Provider_ID"].ToString() + "~" + dt.Rows[i]["Provider_Name"].ToString() + ";";
                str1.Append("<option value='" + dt.Rows[i]["PK_Provider_ID"].ToString() + "'>" + dt.Rows[i]["Provider_Name"].ToString() + "</option>");
                str2.Append("<option value='" + dt.Rows[i]["PK_Provider_ID"].ToString() + "'>" + dt.Rows[i]["Provider_Name"].ToString() + "</option>");
            }
            txt_serviceprovide.Text = txt_serviceprovide.Text.Remove(txt_serviceprovide.Text.Length - 1);
            txt_serviceprovide1.Text = str1.ToString();
            txt_user_serviceprovide.Text = str2.ToString();
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string fillReimburmentAmt(string myear, string mmonth, string expensetype, string grade)
    {
        string ISValid = string.Empty;
        string str = string.Empty;
        DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Modification.aspx", "getexpense", ref ISValid, myear, mmonth, expensetype, grade);
        if (dtamt != null && dtamt.Rows.Count > 0)
        {
            str = dtamt.Rows[0]["Grade_Expense"].ToString();
            // txt_rvalue.Text = dtamt.Rows[0]["Grade_Expense"].ToString();
        }
        else
        {
            str = "0";
        }

        return str;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string checkexpense(string grade)
    {
        string ISValid = string.Empty;
        string str = string.Empty;
        DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref ISValid, grade, "AdActual");
        if (dtamt != null && dtamt.Rows.Count > 0)
        {
            str = dtamt.Rows[0]["At_Actual"].ToString();
        }
        else
        {
            str = "true";
        }

        return str;
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
                if (txt_approvar.Text != "NA")
                {
                    string refData = string.Empty;
                    string isInserted = string.Empty;
                    string ISValid = string.Empty;
                    string ISValid1 = string.Empty;
                    string ISValid2 = string.Empty;
                    string chkno = string.Empty;
                    string isSaved = string.Empty;
                    txt_Condition.Text = "1";
                    txt_Action.Text = "Submit";
                    bool isCreate = false;
                    string loc = string.Empty;
                    string inserXML = txt_Document_Xml.Text;
                    inserXML = inserXML.Replace("&", "&amp;");
                    txt_Document_Xml.Text = inserXML.ToString();
                    txt_Audit.Text = "MOBILE DATACARD EXPENSE SEND BACK";
                    string ISValid3 = string.Empty;
                    string strpk = string.Empty;
                    DataTable dtcode = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref ISValid3, lblExpenseHead.Text, "AdGLCode");
                    if (dtcode != null && dtcode.Rows.Count > 0)
                    {
                        txt_pkexpense1.Text = dtcode.Rows[0]["PK_EXPENSE_HEAD_ID"].ToString();
                    }
                    string mobilexml_string = txt_xml_data_mobile.Text;
                    mobilexml_string = mobilexml_string.Replace("&", "&amp;");
                    mobilexml_string = mobilexml_string.Replace(">", "&gt;");
                    mobilexml_string = mobilexml_string.Replace("<", "&lt;");
                    mobilexml_string = mobilexml_string.Replace("||", ">");
                    mobilexml_string = mobilexml_string.Replace("[", "&lt;");
                    mobilexml_string = mobilexml_string.Replace("]", "&lt;");
                    mobilexml_string = mobilexml_string.Replace("|", "<");
                    mobilexml_string = mobilexml_string.Replace("'", "&apos;");
                    txt_xml_data_mobile.Text = mobilexml_string.ToString();
                    int days1 = 0;
                    string success = string.Empty;
                    success = "FALSE";
                    string dtamt = string.Empty;
                    DataTable days = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref ISValid2, lblExpenseHead.Text, "AdDays");
                    if (days != null && days.Rows.Count > 0)
                    {
                        days1 = Convert.ToInt16(days.Rows[0]["Days"].ToString());
                    }
                    if (lblExpenseHead.Text == "DataCard")
                    {
                        System.DateTime firstDate = DateTime.Today;
                        System.DateTime secondDate = Convert.ToDateTime(txt_card_billdate.Value);
                        System.TimeSpan diff = secondDate.Subtract(firstDate);
                        System.TimeSpan diff1 = secondDate - firstDate;
                        int diff2 = Convert.ToInt16((secondDate - firstDate).TotalDays.ToString());
                        if (days1 < diff2)
                        {
                            string message = "alert('You do not have permission to submit the bill..!')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        }
                        //dtamt = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "checkduplicate", ref ISValid1, ddlCardYear.SelectedValue, ddlCardMonth.SelectedValue, txt_card_billno.Value, ddlCardProvider.Text, txt_Username.Text);
                        //if (dtamt == "true")
                        //{
                        //    string message = "alert('You Already Submitted Bill For This Month')";
                        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        //}
                    }
                    else if (lblExpenseHead.Text == "Mobile")
                    {
                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(txt_xml_data_mobile.Text);
                        string ss = string.Empty; string vyear2 = string.Empty; string vmonth2 = string.Empty; string vbillno2 = string.Empty; string vbilldate2 = string.Empty;
                        string vyear3 = string.Empty; string vmonth3 = string.Empty; string vbillno3 = string.Empty; string vbilldate3 = string.Empty; string ss1 = string.Empty;
                        XmlNodeList xnList = xml.SelectNodes("/ROWSET/ROW");
                        //  foreach (XmlNode xn in xnList)
                        for (int k = 0; k < xnList.Count; k++)
                        {

                            vyear2 = vmonth2 = vbillno2 = "";
                            // string vyear = xnList[k].ChildNodes.Item(3).InnerText.Trim();
                            //string vmonth = xnList[k].ChildNodes.Item(4).InnerText.Trim();
                            string vbillno = xnList[k].ChildNodes.Item(5).InnerText.Trim();
                            // string vprovider = xnList[k].ChildNodes.Item(2).InnerText.Trim();
                            string vbilldate = xnList[k].ChildNodes.Item(6).InnerText.Trim();

                            if (k < xnList.Count - 1)
                            {
                                vyear3 = vmonth3 = vbillno3 = "";
                                //vyear2 = xnList[k + 1].ChildNodes.Item(3).InnerText.Trim();
                                // vmonth2 = xnList[k + 1].ChildNodes.Item(4).InnerText.Trim();
                                vbillno2 = xnList[k + 1].ChildNodes.Item(5).InnerText.Trim();
                                vbilldate2 = xnList[k + 1].ChildNodes.Item(6).InnerText.Trim();
                                // vprovider2 = xnList[k + 1].ChildNodes.Item(2).InnerText.Trim();
                                if ((k + 1) < xnList.Count - 1)
                                {
                                    // vyear3 = xnList[k + 2].ChildNodes.Item(3).InnerText.Trim();
                                    //vmonth3 = xnList[k + 2].ChildNodes.Item(4).InnerText.Trim();
                                    vbillno3 = xnList[k + 2].ChildNodes.Item(5).InnerText.Trim();
                                    vbilldate3 = xnList[k + 2].ChildNodes.Item(6).InnerText.Trim();
                                }
                            }
                            System.DateTime firstDate = DateTime.Today;
                            System.DateTime secondDate = Convert.ToDateTime(vbilldate);
                            System.TimeSpan diff = secondDate.Subtract(firstDate);
                            System.TimeSpan diff1 = secondDate - firstDate;
                            int diff2 = Convert.ToInt16((secondDate - firstDate).TotalDays.ToString());
                            if (days1 < diff2)
                            {
                                ss += vbillno + " / ";
                                success = "DIFFDAYS";

                            }
                            else if ((vbillno == vbillno2 && vbilldate == vbilldate2) || (vbillno3 == vbillno2 && vbilldate3 == vbilldate2) || (vbillno == vbillno3 && vbilldate == vbilldate3))
                            {
                                if ((vbillno2 != "" || vbillno3 != "") && (vbilldate2 != "" || vbilldate3 != ""))
                                {
                                    success = "SAME";
                                }
                            }
                            //else
                            //{
                            //    dtamt = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "checkduplicate", ref ISValid1, vyear, vmonth, vbillno, vprovider, txt_Username.Text);
                            //    if (dtamt == "true")
                            //    {
                            //        ss1 += vbillno + " / ";
                            //        success = "TRUE";
                            //    }
                            //}
                        }
                        if (success == "DIFFDAYS")
                        {
                            string message = "alert('You do not have permission to submit the bill for :" + ss + "  bill no..!')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                        }
                        //if (success == "TRUE")
                        //{
                        //    string message = "alert('You have Already Submitted Bill For This Month for :" + ss1 + "  bill no')";
                        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        //}
                        if (success == "SAME")
                        {
                            string message = "alert('You can not apply for same bill no and bill date..!')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        }
                    }
                    if (success == "FALSE")
                    {
                        string confirmValue = txt_confirm.Text;
                        if (confirmValue == "Yes" || confirmValue == "")
                        {
                            if (lblExpenseHead.Text == "Mobile")
                            {

                                if (ddlPayMode.SelectedValue == "2")
                                {
                                    loc = "0";
                                }
                                else
                                {
                                    loc = ddlLocation.SelectedValue;
                                }
                                if (dev_supp_amt.Text == "1")
                                {
                                    dev_supp_limit.Text = "1";
                                }

                                if (dev_supp_amt.Text == "0")
                                {
                                    if (dev_supp_limit.Text == "1")
                                    {
                                        dev_supp_limit.Text = "1";
                                    }
                                    else
                                    {
                                        dev_supp_limit.Text = "0";
                                    }
                                }
                                isSaved = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Modification.aspx", "update", ref refData, Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), lblExpenseHead.Text, ddlPayMode.SelectedValue, txt_pk_id.Text, txt_xml_data_mobile.Text, txt_Username.Text, "", txt_Audit.Text, txt_Action.Text, lbl_AppName.Text, txt_Document_Xml.Text, loc, txt_pkexpense1.Text, dev_supp_limit.Text);
                                if (isSaved == "true")
                                {
                                    if (txt_ded_count.Text != "0")
                                    {
                                        chkno = txt_check_Nos.Text;
                                        string refDataqq = string.Empty;
                                        if (chkno != "")
                                        {
                                            int parse = int.Parse(chkno);
                                            string[] separators = { ";" };
                                            string dedyear = txt_ded_year.Text;
                                            string dedmonth = txt_ded_month.Text;
                                            string dedamt = txt_ded_amount.Text;
                                            string[] words1 = dedyear.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                                            string[] words2 = dedmonth.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                                            string[] words3 = dedamt.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                                            for (int i = 0; i < parse; i++)
                                            {
                                                string result12 = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "insertded", ref refDataqq, words1[i], words2[i], words3[i], Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), lbl_EmpCode.Text);
                                            }
                                        }
                                    }
                                }
                            }

                            else if (lblExpenseHead.Text == "DataCard")
                            {
                                if (ddlPayMode.SelectedValue == "2")
                                {
                                    loc = "0";
                                }
                                else
                                {
                                    loc = ddlLocation.SelectedValue;
                                }
                                isSaved = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Modification.aspx", "update", ref refData, Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), lblExpenseHead.Text, ddlPayMode.SelectedValue, txt_pk_id.Text, txt_xml_data_mobile.Text, txt_Username.Text, txt_remark.Value, txt_Audit.Text, txt_Action.Text, lbl_AppName.Text, txt_Document_Xml.Text, loc, txt_pkexpense1.Text, "0");

                            }
                            if (isSaved == null || refData.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = refData.Split(':');
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + errmsg[1].ToString() + "')", true);
                            }
                            else
                            {
                                
                                if (lbl_EmpCode.Text == "10000" || lbl_EmpCode.Text == "10001")
                                {
                                    string isSaved1 = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "inserttwi", ref refData, txtProcessID.Text, txtInstanceID.Text, txt_Username.Text, txt_Request.Text, "3");
                                    DataTable DTAP = new DataTable();
                                    string msg = "";
                                    string emailid = string.Empty;
                                    CryptoGraphy crypt = new CryptoGraphy();
                                    string req_no = crypt.Encryptdata(txt_Request.Text);
                                    string process_name = crypt.Encryptdata("MODIFY MOBILE DATACARD EXPENSE");
                                    if (ddlPayMode.SelectedItem.Text.ToUpper() == "CASH")
                                    {
                                        DTAP = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "getaccapprover", ref ISValid, "MOBILE DATACARD ACCOUNT PAYMENT APPROVAL", ddlLocation.SelectedValue, ddlPayMode.SelectedValue);
                                    }
                                    else
                                    {
                                        DTAP = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "getaccapprover", ref ISValid, "MOBILE DATACARD ACCOUNT PAYMENT APPROVAL", 0, ddlPayMode.SelectedValue);
                                    }
                                    if (DTAP != null)
                                    {
                                        if (DTAP.Rows.Count > 0)
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
                                            bool isCreate1 = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "142", "MOBILE DATACARD EXPENSE APPROVAL", "APPROVE", "", txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                            if (isCreate1)
                                            {
                                                try
                                                {
                                                    if (lblExpenseHead.Text == "DataCard")
                                                    {
                                                        if (ddlPayMode.SelectedItem.Text.ToUpper() == "CASH")
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Datacard rental expense Request has been modified and sent for account payment approval with approval</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MOBILE DATACARD EXPENSE APPROVAL", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                                        }
                                                        else
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Datacard rental expense Request has been modified and sent for account payment approval with approval</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MOBILE DATACARD EXPENSE APPROVAL", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (ddlPayMode.SelectedItem.Text.ToUpper() == "CASH")
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Mobile bill rental Request has been modified and sent for account payment approval with approval</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MOBILE DATACARD EXPENSE APPROVAL", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                                        }
                                                        else
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Mobile bill rental Request has been modified and sent for account payment approval with approval</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MOBILE DATACARD EXPENSE APPROVAL", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                                        }
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    throw;
                                                }
                                                finally
                                                {
                                                    if (lblExpenseHead.Text == "DataCard")
                                                    {
                                                        string msg2 = string.Empty;
                                                        msg2 = "alert('Mobile bill rental expense Request has been modified and sent for account payment approval for approval...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                                    }
                                                    else
                                                    {
                                                        string msg2 = string.Empty;
                                                        msg2 = "alert('Datacard rental expense Request has been modified and sent for account payment approval for approval...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string msg2 = string.Empty;
                                            msg2 = "alert('Account Payment Approver Not Available For " + ddlPayMode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                        }
                                    }
                                    else
                                    {
                                        string msg2 = string.Empty;
                                        msg2 = "alert('Account Payment Approver Not Available For " + ddlPayMode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);

                                    }
                                }
                                else if ((lbl_EmpCode.Text != "10000" || lbl_EmpCode.Text != "10001") && lblExpenseHead.Text == "DataCard")
                                {
                                    string[] Dval = new string[1];
                                    Dval[0] = txt_approvar.Text;
                                    txtApproverEmail.Text = txt_Approver_Email.Text;
                                    isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "144", "MODIFY MOBILE DATACARD EXPENSE", "SUBMIT", "", txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                    if (isCreate)
                                    {
                                        try
                                        {
                                            string emailid = string.Empty;
                                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>The Mobile Card Expense Request has been Modified.</font></pre><p/>  <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MODIFY MOBILE DATACARD EXPENSE", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                        }
                                        catch (Exception ex)
                                        {
                                            // throw;
                                            FSL.Logging.Logger.WriteEventLog(false, ex);
                                        }
                                        finally
                                        {
                                            string msg1 = string.Empty;
                                            if (dev_supp_limit.Text == "0")
                                            {
                                                msg1 = "alert('DataCard rental expense request has been modified successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg1, true);
                                            }
                                            else
                                            {
                                                msg1 = "alert('DataCard rental expense request has been modified successfully and sent for approval(under deviation)..!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg1, true);
                                            }
                                        }
                                    }
                                }
                                else if ((lbl_EmpCode.Text != "10000" || lbl_EmpCode.Text != "10001") && lblExpenseHead.Text == "Mobile")
                                {
                                        string[] Dval1 = new string[1];
                                        Dval1[0] = txt_approvar.Text;
                                        txtApproverEmail.Text = txt_Approver_Email.Text;
                                        isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "144", "MODIFY MOBILE DATACARD EXPENSE", "SUBMIT", "", txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, txt_Request.Text, txtWIID.Text, ref isInserted);
                                        if (isCreate)
                                        {
                                            try
                                            {
                                                string emailid = string.Empty;
                                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>The Mobile Card Expense Request has been Modified.</font></pre><p/>  <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MODIFY MOBILE DATACARD EXPENSE", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                            }
                                            catch (Exception ex)
                                            {
                                                // throw;
                                                FSL.Logging.Logger.WriteEventLog(false, ex);
                                            }
                                            finally
                                            {
                                                string msg1 = string.Empty;
                                                if (dev_supp_limit.Text == "0")
                                                {
                                                    msg1 = "alert('Mobile bill rental expense request has been modified successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg1, true);
                                                }
                                                else
                                                {
                                                    msg1 = "alert('Mobile bill rental expense request has been modified successfully and sent for approval(under deviation)..!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg1, true);
                                                }
                                            }
                                        }
                                   
                                }

                            }
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    string msg = "alert('Approver Not Available...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                }
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
    private void fillDedction()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getdeddetail", ref isValid, lbl_EmpCode.Text, txt_pk_id.Text);
            tblHTML.Append("<table ID='tbldedu' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Deduction Year</th><th>Deduction Month</th><th>Amount</th></tr></thead>");
            tblHTML.Append("<tbody>");
            txt_ded_count.Text = Convert.ToString(dt.Tables[0].Rows.Count);
            if (dt != null && dt.Tables[0].Rows.Count > 0)
            {
                for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
                {
                    tblHTML.Append("<tr>");
                    if (Index == 0)
                    {
                        tblHTML.Append("<td><input type='checkbox' id='checkbox" + (Index + 1) + "'  name='mobile'><input type='text' id='PK_DED_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["PK_ID"]) + "' style='display:none'></td>");
                    }
                    else
                    {
                        tblHTML.Append("<td><input type='checkbox' id='checkbox" + (Index + 1) + "'  name='mobile' ><input type='text' id='PK_DED_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["PK_ID"]) + "' style='display:none'></td>");
                    }
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["dedyear"]) + " <input id='ded_year" + (Index + 1) + "'  runat='server' Value='" + Convert.ToString(dt.Tables[0].Rows[Index]["dedyear"]) + "' style='display:none' /></td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["DedMonthName"]) + " <input id='ded_month" + (Index + 1) + "'  runat='server' Value='" + Convert.ToString(dt.Tables[0].Rows[Index]["dedmonth"]) + "' style='display:none' /></td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["amount"]) + " <input id='ded_amount" + (Index + 1) + "'  runat='server' Value='" + Convert.ToString(dt.Tables[0].Rows[Index]["amount"]) + "' style='display:none' /></td>");
                    // tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["APPROVED"]) + "</td>");
                    tblHTML.Append("</tr>");
                }
            }
            else
            {
                tblHTML.Append("<tr><td colspan='5' align='center'>Deduction Details Not Available</td></tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            div_dedction.InnerHtml = tblHTML.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    [WebMethod]
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
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;

            path = activeDir + "\\" + "MOBILE DATACARD\\" +txt_Request.Text + "\\";
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
    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            string msg = "window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
        }
    }

    //[AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    //public static string getDeductionAmount(string username, string yrmn)
    //{
    //    string isvalid = string.Empty;
    //    string amount = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getdeductamount", ref isvalid, username, yrmn);
    //    if (amount == null || amount == "")
    //    {
    //        amount = "0";
    //    }
    //    return amount;
    //}

}

