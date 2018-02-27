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
using System.Xml;
using System.Runtime.InteropServices;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;

public partial class Mobile_DataCard_Expense : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Mobile_DataCard_Expense));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Session["USER_ADID"].ToString();
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["stepid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                    }

                    Initialization();
                    FillExpenseHead();
                    FillMonth();
                    FillServiceProvider();
                    FillYear();
                    FillMode();
                    FillLocation();
                    fillDocument_Details();
                    fillPolicy_Details();
                    FillSupport();
                    fillSupporting();
                    fillDedction();
                }
            }
        }
        catch (Exception Exc) { }
    }

    private void Initialization()
    {
        string IsData = string.Empty;
        string IsDatam = string.Empty;
        string IsData1 = string.Empty;
        string IsData2 = string.Empty;
        string IsData3 = string.Empty;
        DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "pgettraveluser", ref IsData, txt_Username.Text);
        if (dtUser != null && dtUser.Rows.Count > 0)
        {
            lbl_EmpCode.Text = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
            txt_adid.Text = Convert.ToString(dtUser.Rows[0]["AD_ID"]);
            lbl_EmpName.Text = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
            lbl_Dept.Text = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
            lbl_Grade.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
            if (Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]) != "")
            {
                lbl_Grade.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
            }
            else
            {
                lbl_Grade.Text = "NA";
            }
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

        }
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref IsData, lbl_Grade.Text, "AdUser");
        if (dt != null && dt.Rows.Count > 0)
        {
            string expensetype = dt.Rows[0]["Expense_Type"].ToString();
            if (expensetype == "Mobile")
            {
                txt_Mreimbursement.Text = dt.Rows[0]["Grade_Expense"].ToString();
                txt_user_Mreimbursement.Text = dt.Rows[0]["Grade_Expense"].ToString();
            }
            DataTable dtg = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref IsData2, "DataCard", "AdGrade");
            if (dtg != null && dtg.Rows.Count > 0)
            {
                txt_Reimbursement.Text = dtg.Rows[0]["Grade_Expense"].ToString();
            }
            txt_Datacode.Text = "";
            DataTable dtd = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref IsData3, lbl_EmpCode.Text, "CheckDatacard");
            if (dtd != null && dtd.Rows.Count > 0)
            {
                txt_Datacode.Text = dtd.Rows[0]["Employee_Code"].ToString();
            }
        }
        DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "pgettravelrequestapprover", ref IsData1, txt_Username.Text);
        if (dtApprover != null)
        {
            if (dtApprover.Rows.Count > 0)
            {
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
    private void FillYear()
    {
        StringBuilder str1 = new StringBuilder();
        str1.Append("<option value='0'>--Select One--</option>");
        StringBuilder str2 = new StringBuilder();
        str2.Append("<option value='0'>--Select One--</option>");
        var currentYear = DateTime.Today.Year;
        // ddlYearUser1.Items.Insert(0, new ListItem("--Select One--", "0"));
        ddlYear1.Items.Insert(0, new ListItem("--Select One--", "0"));

        ddlCardYear.Items.Insert(0, new ListItem("--Select One--", "0"));
        for (int i = 1; i >= 0; i--)
        {
            ddlYear1.Items.Add((currentYear - i).ToString());
            // ddlYearUser1.Items.Add((currentYear - i).ToString());

            ddlCardYear.Items.Add((currentYear - i).ToString());
            str1.Append("<option value='" + (currentYear - i).ToString() + "'>" + (currentYear - i).ToString() + "</option>");
            str2.Append("<option value='" + (currentYear - i).ToString() + "'>" + (currentYear - i).ToString() + "</option>");
        }
        txt_year.Text = str1.ToString();
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
                string isValid = string.Empty;
                string DisplayData = string.Empty;

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>Description</th><th>File Name</th><th>Delete</th></tr></thead>";
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
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Grade Name</th><th>Grade Expense</th><th>Effective From</th><th>Effective To</th></tr></thead>";

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

        ddl_support1.Items.Insert(0, new ListItem("--Select One--", "0"));
        ddl_support1.Items.Insert(1, new ListItem("Yes", "Y"));
        ddl_support1.Items.Insert(2, new ListItem("No", "N"));

        //ddlsupp_datacard.Items.Insert(0, new ListItem("--Select One--", "0"));
        //ddlsupp_datacard.Items.Insert(1, new ListItem("Yes", "Y"));
        //ddlsupp_datacard.Items.Insert(2, new ListItem("No", "N"));

        ddlusersupp1.Items.Insert(0, new ListItem("--Select One--", "0"));
        ddlusersupp1.Items.Insert(1, new ListItem("Yes", "Y"));
        ddlusersupp1.Items.Insert(2, new ListItem("No", "N"));

        str1.Append("<option value='Y'>Yes</option>");
        str1.Append("<option value='N'>No</option>");
        txt_ddlsupport.Text = str1.ToString();
    }
    private void FillExpenseHead()
    {
        String IsData = string.Empty;
        dt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdExpenseHead");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlExpenseHead.DataSource = dt;
            ddlExpenseHead.DataTextField = "EXPENSE_HEAD";
            ddlExpenseHead.DataValueField = "EXPENSE_HEAD";
            ddlExpenseHead.DataBind();
            ddlExpenseHead.Items.Insert(0, Li);
        }
    }
    private void FillMonth()
    {
        StringBuilder str1 = new StringBuilder();
        str1.Append("<option value='0'>--Select One--</option>");
        StringBuilder str2 = new StringBuilder();

        str2.Append("<option value='0'>--Select One--</option>");
        ddlCardMonth.Items.Insert(0, new ListItem("--Select One--", "0"));

        ddlMonth1.Items.Insert(0, new ListItem("--Select One--", "0"));
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

        // ddlDeduMonth.Items.Insert(0, new ListItem("--Select One--", "0"));

        ddlMonth1.Items.Insert(0, new ListItem("--Select One--", "0"));
        ddlMonth1.Items.Insert(1, new ListItem("January", "1"));
        ddlMonth1.Items.Insert(2, new ListItem("February", "2"));
        ddlMonth1.Items.Insert(3, new ListItem("March", "3"));
        ddlMonth1.Items.Insert(4, new ListItem("April", "4"));
        ddlMonth1.Items.Insert(5, new ListItem("May", "5"));
        ddlMonth1.Items.Insert(6, new ListItem("June", "6"));
        ddlMonth1.Items.Insert(7, new ListItem("July", "7"));
        ddlMonth1.Items.Insert(8, new ListItem("August", "8"));
        ddlMonth1.Items.Insert(9, new ListItem("September", "9"));
        ddlMonth1.Items.Insert(10, new ListItem("October", "10"));
        ddlMonth1.Items.Insert(11, new ListItem("November", "11"));
        ddlMonth1.Items.Insert(12, new ListItem("December", "12"));
        txt_month.Text = str1.ToString();
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
            ddlServiceProvider1.DataSource = dt;
            ddlServiceProvider1.DataTextField = "Provider_Name";
            ddlServiceProvider1.DataValueField = "PK_Provider_ID";
            ddlServiceProvider1.DataBind();
            ddlServiceProvider1.Items.Insert(0, Li);

            ddlServiceProviderUser1.DataSource = dt;
            ddlServiceProviderUser1.DataTextField = "Provider_Name";
            ddlServiceProviderUser1.DataValueField = "PK_Provider_ID";
            ddlServiceProviderUser1.DataBind();
            ddlServiceProviderUser1.Items.Insert(0, Li);

            ddlCardProvider.DataSource = dt;
            ddlCardProvider.DataTextField = "Provider_Name";
            ddlCardProvider.DataValueField = "PK_Provider_ID";
            ddlCardProvider.DataBind();
            ddlCardProvider.Items.Insert(0, Li);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str1.Append("<option value='" + dt.Rows[i]["PK_Provider_ID"].ToString() + "'>" + dt.Rows[i]["Provider_Name"].ToString() + "</option>");
                str2.Append("<option value='" + dt.Rows[i]["PK_Provider_ID"].ToString() + "'>" + dt.Rows[i]["Provider_Name"].ToString() + "</option>");
            }
            txt_serviceprovide.Text = str1.ToString();
            txt_user_serviceprovide.Text = str2.ToString();
        }
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string fillGLCode(string expensev, string ad_id)
    {
        string ISValid = string.Empty;
        string IsData = string.Empty;
        string approvar = string.Empty;
        string approvar_name = string.Empty;
        string supprt_doc = string.Empty;
        string approvar_email = string.Empty;
        string str = string.Empty;

        DataTable dtcode = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref ISValid, expensev, "AdGLCode");

        if (expensev.ToString() == "DataCard")
        {
            //////For Supporting///////////////
            DataTable dtsupp = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "checksupport", ref IsData, expensev);
            supprt_doc = Convert.ToString(dtsupp.Rows[0]["IS_SUPPORTING"]);
            ///////////////////////////////////
            DataTable DTAP = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getaccapprover", ref ISValid, "DATACARD  EXPENSE  APPROVAR");
            string[] Dval = new string[DTAP.Rows.Count];
            Dval[0] = "";
            if (DTAP.Rows.Count > 0)
            {
                for (int i = 0; i < DTAP.Rows.Count; i++)
                {
                    approvar_name = Convert.ToString(DTAP.Rows[i]["EMPLOYEE_NAME"]);
                    approvar = Convert.ToString(DTAP.Rows[i]["USER_ADID"]);
                    if (approvar_email == "")
                    {
                        approvar_email = Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                    }
                    else
                    {
                        approvar_email = approvar_email + ',' + Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                    }
                }

            }
            else
            {
                approvar_name = "NA";
                approvar = "NA";
            }
        }
        else if (expensev.ToString() == "Mobile")
        {
            //////For Supporting///////////////
            DataTable dtsupp = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "checksupport", ref IsData, expensev);
            supprt_doc = Convert.ToString(dtsupp.Rows[0]["IS_SUPPORTING"]);
            ///////////////////////////////////////
            DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "pgettravelrequestapprover", ref ISValid, ad_id);
            if (dtApprover != null)
            {
                if (dtApprover.Rows.Count > 0)
                {
                    if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                    {
                        approvar_name = Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                        approvar = Convert.ToString(dtApprover.Rows[0]["approver"]);
                    }
                    else
                    {
                        approvar_name = "NA";
                        approvar = "NA";
                    }
                    if (Convert.ToString(dtApprover.Rows[0]["approver_email"]) == "")
                    {
                        approvar_email = "NA";
                    }
                    else
                    {
                        approvar_email = Convert.ToString(dtApprover.Rows[0]["approver_email"]);
                    }
                }

            }
        }
        str = str + "/" + approvar_name + "/" + approvar + "/" + approvar_email + "/" + supprt_doc;
        return str;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string fillApprovar(string expensev)
    {
        string IsData3 = string.Empty;
        string strapp = string.Empty;
        DataTable DTAP = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getaccapprover", ref IsData3, "DATACARD  EXPENSE  APPROVAR");
        if (DTAP != null && DTAP.Rows.Count > 0)
        {
            strapp = DTAP.Rows[0]["USER_ADID"].ToString();
        }
        return strapp;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string fillReimburmentAmt(string myear, string mmonth, string expensetype, string grade)
    {
        string ISValid = string.Empty;
        string str = string.Empty;
        DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getexpense", ref ISValid, myear, mmonth, expensetype, grade);
        if (dtamt != null && dtamt.Rows.Count > 0)
        {
            str = dtamt.Rows[0]["Grade_Expense"].ToString();
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
                string gg = txt_approvar.Text;
                string df = txtApproverEmail.Text;
                if (txt_approvar.Text != "NA")
                {
                    string ISValid2 = string.Empty;
                    string strpk = string.Empty;
                    DataTable dtcode = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref ISValid2, ddlExpenseHead.SelectedValue, "AdGLCode");
                    if (dtcode != null && dtcode.Rows.Count > 0)
                    {
                        txt_pkexpense1.Text = dtcode.Rows[0]["PK_EXPENSE_HEAD_ID"].ToString();
                    }
                    string mobilexml_string = txt_xml_data_mobile.Text;
                    mobilexml_string = mobilexml_string.Replace("&", "&amp;");
                    mobilexml_string = mobilexml_string.Replace(">", "&gt;");
                    mobilexml_string = mobilexml_string.Replace("<", "&lt;");
                    mobilexml_string = mobilexml_string.Replace("[", "&lt;");
                    mobilexml_string = mobilexml_string.Replace("]", "&lt;");
                    mobilexml_string = mobilexml_string.Replace("||", ">");
                    mobilexml_string = mobilexml_string.Replace("|", "<");
                    mobilexml_string = mobilexml_string.Replace("'", "&apos;");
                    txt_xml_data_mobile.Text = mobilexml_string.ToString();
                    string ISValid1 = string.Empty; int days1 = 0;
                    string success = string.Empty;
                    success = "FALSE";
                    string dtamt = string.Empty;
                    DataTable days = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref ISValid2, ddlExpenseHead.SelectedValue, "AdDays");
                    if (days != null && days.Rows.Count > 0)
                    {
                        days1 = Convert.ToInt16(days.Rows[0]["Days"].ToString());
                    }
                    if (ddlExpenseHead.SelectedValue == "DataCard")
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
                        dtamt = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "checkduplicate", ref ISValid1, ddlCardYear.SelectedValue, ddlCardMonth.SelectedValue, txt_card_billno.Value, ddlCardProvider.Text, txt_Username.Text);
                        if (dtamt == "true")
                        {
                            string message = "alert('You have Already Submitted Bill For This Month')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        }
                    }
                    else if (ddlExpenseHead.SelectedValue == "Mobile")
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
                            //string vyear = xnList[k].ChildNodes.Item(3).InnerText.Trim();
                            // string vmonth = xnList[k].ChildNodes.Item(4).InnerText.Trim();
                            string vbillno = xnList[k].ChildNodes.Item(5).InnerText.Trim();
                            // string vprovider = xnList[k].ChildNodes.Item(2).InnerText.Trim();
                            string vbilldate = xnList[k].ChildNodes.Item(6).InnerText.Trim();

                            if (k < xnList.Count - 1)
                            {
                                vyear3 = vmonth3 = vbillno3 = "";
                                //vyear2 = xnList[k + 1].ChildNodes.Item(3).InnerText.Trim();
                                //vmonth2 = xnList[k + 1].ChildNodes.Item(4).InnerText.Trim();
                                vbillno2 = xnList[k + 1].ChildNodes.Item(5).InnerText.Trim();
                                //vprovider2 = xnList[k + 1].ChildNodes.Item(2).InnerText.Trim();
                                vbilldate2 = xnList[k + 1].ChildNodes.Item(6).InnerText.Trim();
                                if ((k + 1) < xnList.Count - 1)
                                {
                                    //vyear3 = xnList[k + 2].ChildNodes.Item(3).InnerText.Trim();
                                    // vmonth3 = xnList[k + 2].ChildNodes.Item(4).InnerText.Trim();
                                    vbillno3 = xnList[k + 2].ChildNodes.Item(5).InnerText.Trim();
                                    //vprovider3 = xnList[k + 2].ChildNodes.Item(2).InnerText.Trim();
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
                            // else if ((vyear == vyear2 && vmonth == vmonth2 && vprovider == vprovider2) || (vyear3 == vyear2 && vmonth3 == vmonth2 && vprovider3 == vprovider2) || (vyear == vyear3 && vmonth == vmonth3 && vprovider == vprovider3))
                            else if ((vbillno == vbillno2 && vbilldate == vbilldate2) || (vbillno3 == vbillno2 && vbilldate3 == vbilldate2) || (vbillno == vbillno3 && vbilldate == vbilldate3))
                            {
                                if ((vbillno2 != "" || vbillno3 != "") && (vbilldate2 != "" || vbilldate3 != ""))
                                {
                                    success = "SAME";
                                }
                                else
                                {
                                    // dtamt = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "checkduplicate", ref ISValid1, vyear, vmonth, vprovider, txt_Username.Text);
                                    dtamt = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "checkduplicate", ref ISValid1, vbillno, vbilldate, txt_Username.Text);

                                    if (dtamt == "true")
                                    {
                                        ss1 += vbillno + " / ";
                                        success = "TRUE";
                                    }
                                }

                            }
                            else
                            {
                                dtamt = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "checkduplicate", ref ISValid1, vbillno, vbilldate, txt_Username.Text);
                                if (dtamt == "true")
                                {
                                    ss1 += vbillno + " / ";
                                    success = "TRUE";
                                }
                            }
                        }
                        if (success == "DIFFDAYS")
                        {
                            string message = "alert('You do not have permission to submit the bill for : " + ss + "  bill no..!')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        }
                        if (success == "TRUE")
                        {
                            string message = "alert('You Already Submitted Bill For This Month for : " + ss1 + "  bill no')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        }
                        if (success == "SAME")
                        {
                            string message = "alert('You can not apply for same bill no and bill date..!')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                        }
                    }
                    if (success == "FALSE")
                    {                        
                        string confirmValue =  txt_confirm.Text;
                        if (confirmValue == "Yes" || confirmValue == "")
                        {
                            string refData = string.Empty;
                            string isInserted = string.Empty;
                            string ISValid = string.Empty;
                            string isSaved = string.Empty;
                            txt_Condition.Text = "1";
                            txt_Action.Text = "Submit";
                            bool isCreate = false;
                            string chkno = string.Empty;
                            string inserXML = txt_Document_Xml.Text;
                            inserXML = inserXML.Replace("&", "&amp;");
                            txt_Document_Xml.Text = inserXML.ToString();
                            txt_Audit.Text = "MOBILE DATACARD EXPENSE";
                            string instanceID = (string)WFE.Action.StartCase(isInserted, txtProcessID.Text, txt_Username.Text, txt_Username.Text, txtEmailID.Text, "140");
                            txtInstanceID.Text = instanceID;

                            if (ddlExpenseHead.SelectedValue == "Mobile")
                            {
                                if (ddlPayMode.SelectedValue == "2")
                                {
                                    ddlLocation.SelectedValue = "0";
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
                                isSaved = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "insert", ref refData, Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), ddlExpenseHead.SelectedValue, ddlPayMode.SelectedValue, txt_xml_data_mobile.Text, txt_Username.Text, "", txt_Audit.Text, txt_Action.Text, txt_approvar.Text, txt_Document_Xml.Text, ddlLocation.SelectedValue, txt_pkexpense1.Text, dev_supp_limit.Text);
                                if (isSaved != "false")
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
                            else if (ddlExpenseHead.SelectedValue == "DataCard")
                            {

                                if (ddlPayMode.SelectedValue == "2")
                                {
                                    ddlLocation.SelectedValue = "0";
                                }
                                isSaved = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "insert", ref refData, Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), ddlExpenseHead.SelectedValue, ddlPayMode.SelectedValue, txt_xml_data_mobile.Text, txt_Username.Text, txt_remark.Value, txt_Audit.Text, txt_Action.Text, lbl_AppName.Text, txt_Document_Xml.Text, ddlLocation.SelectedValue, txt_pkexpense1.Text, "0");

                            }
                            if (isSaved == null || refData.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = refData.Split(':');
                                //  Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");                  
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + errmsg[0].ToString() + "')", true);
                            }
                            else
                            {
                                string[] Request_Unique = isSaved.Split('=');
                                txt_Request.Text = Request_Unique[0];
                                ////upload file code///////////////////
                                string isValid1 = string.Empty;
                                string[] arr = new string[100];
                                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getrequest_document", ref isValid1, "MOBILE DATACARD", Request_Unique[0]);
                                if (dt.Rows.Count > 0)
                                {
                                    string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString().Trim();
                                    string path = string.Empty;
                                    string foldername = Request_Unique[0];
                                    foldername = foldername.Replace("/", "_");
                                    path = activeDir + "\\" + "MOBILE DATACARD\\" + foldername;
                                    if (Directory.Exists(path))
                                    {

                                    }
                                    else
                                    {
                                        Directory.CreateDirectory(path);
                                        string[] directories = Directory.GetFiles(activeDir + "\\" + "MOBILE DATACARD\\");
                                        path = path + "\\";
                                        foreach (var directory in directories)
                                        {
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                var sections = directory.Split('\\');
                                                var fileName = sections[sections.Length - 1];
                                                if (dt.Rows[i]["filename"].ToString().Trim() == fileName)
                                                {
                                                    System.IO.File.Move(activeDir + "\\" + "MOBILE DATACARD\\" + fileName, path + fileName);
                                                    if (System.IO.File.Exists(activeDir + "\\" + "MOBILE DATACARD\\" + fileName))
                                                    {
                                                        System.IO.File.Delete(activeDir + "\\" + "MOBILE DATACARD\\" + fileName);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                ///////////////////////////////////////
                                if (lbl_EmpCode.Text == "10000" || lbl_EmpCode.Text == "10001")
                                {
                                    string isSaved1 = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "inserttwi", ref refData, txtProcessID.Text, txtInstanceID.Text, txt_Username.Text, txt_Request.Text, "1");
                                    DataTable DTAP = new DataTable();
                                    string msg = "";
                                    string emailid = string.Empty;
                                    CryptoGraphy crypt = new CryptoGraphy();
                                    string req_no = crypt.Encryptdata(txt_Request.Text);
                                    string process_name = crypt.Encryptdata("MOBILE DATACARD EXPENSE");
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
                                            bool isCreate1 = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "142", "MOBILE DATACARD EXPENSE APPROVAL", "APPROVE", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, "0", ref isInserted);
                                            if (isCreate1)
                                            {
                                                try
                                                {
                                                    if (ddlExpenseHead.SelectedValue == "DataCard")
                                                    {
                                                        if (ddlPayMode.SelectedItem.Text.ToUpper() == "CASH")
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Datacard rental Request has been sent for Accounts to payment approval with approval</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MOBILE DATACARD EXPENSE APPROVAL", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                                        }
                                                        else
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Datacard rental Request has been sent for Accounts to payment approval...</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MOBILE DATACARD EXPENSE APPROVAL", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (ddlPayMode.SelectedItem.Text.ToUpper() == "CASH")
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Mobile bill rental Request has been sent for Accounts to payment approval with approval</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MOBILE DATACARD EXPENSE APPROVAL", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                                        }
                                                        else
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Mobile bill rental Request has been sent for Accounts to payment approval...</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
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
                                                    if (ddlExpenseHead.SelectedValue == "DataCard")
                                                    {
                                                        string msg2 = string.Empty;
                                                        msg2 = "alert('Datacard rental Request has been sent for account payment approval and Request No. is : " + txt_Request.Text + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                                    }
                                                    else
                                                    {
                                                        string msg2 = string.Empty;
                                                        msg2 = "alert('Mobile bill rental Request has been sent for account payment approval and Request No. is : " + txt_Request.Text + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
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
                                else if ((lbl_EmpCode.Text != "10000" || lbl_EmpCode.Text != "10001") && ddlExpenseHead.SelectedValue == "DataCard")
                                {
                                    string[] Dval = new string[1];
                                    Dval[0] = txt_approvar.Text;
                                    if (txtApproverEmail.Text == "")
                                    {
                                        txtApproverEmail.Text = txtEmailID.Text;
                                    }

                                    isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "141", "MOBILE DATACARD EXPENSE", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, "0", ref isInserted);
                                    if (isCreate)
                                    {
                                        try
                                        {
                                            if (((txtApproverEmail.Text != "NA") || (txtApproverEmail.Text != "")) && (txtEmailID.Text != ""))
                                            {
                                                string emailid = string.Empty;
                                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Datacard rental Expense Request has been sent for your approval..</font></pre><p/>  <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MOBILE DATACARD EXPENSE", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            FSL.Logging.Logger.WriteEventLog(false, ex);
                                        }
                                        finally
                                        {
                                            string msg1 = string.Empty;
                                            if (dev_supp_limit.Text == "0")
                                            {
                                                msg1 = "alert('DataCard rental Request has been sent for your approval and Request No. is : " + txt_Request.Text + "...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg1, true);
                                            }
                                            else
                                            {
                                                msg1 = "alert('DataCard rental request has been sent for approval(under deviation) and Request No. is : " + txt_Request.Text + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg1, true);
                                            }
                                        }
                                    }
                                }
                                else if ((lbl_EmpCode.Text != "10000" || lbl_EmpCode.Text != "10001") && ddlExpenseHead.SelectedValue == "Mobile")
                                {
                                      if (txt_approvar.Text != "NA")
                                        {
                                            string[] Dval1 = new string[1];
                                            Dval1[0] = txt_approvar.Text;

                                            if (txtApproverEmail.Text == "")
                                            {
                                                txtApproverEmail.Text = txtEmailID.Text;
                                            }
                                            isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "141", "MOBILE DATACARD EXPENSE", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, txt_Request.Text, "0", ref isInserted);
                                            if (isCreate)
                                            {
                                                try
                                                {
                                                    if (((txtApproverEmail.Text != "NA") || (txtApproverEmail.Text != "")) && (txtEmailID.Text != ""))
                                                    {
                                                        string emailid = string.Empty;
                                                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Mobile bill rental Expense Request has been sent for your approval.</font></pre><p/>  <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                        emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MOBILE DATACARD EXPENSE", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    FSL.Logging.Logger.WriteEventLog(false, ex);
                                                }
                                                finally
                                                {
                                                    string msg1 = string.Empty;
                                                    if (dev_supp_limit.Text == "0")
                                                    {
                                                        msg1 = "alert('Mobile bill rental Request has been sent for your approval and Request No. is : " + txt_Request.Text + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg1, true);
                                                    }
                                                    else
                                                    {
                                                        msg1 = "alert('Mobile bill rental request has been sent for approval(under deviation) and Request No. is : " + txt_Request.Text + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg1, true);
                                                    }
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
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        try
        {
            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;

            path = activeDir + "\\" + "MOBILE DATACARD\\";

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
            // Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
            string msg = "window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
        }
    }
    private void fillDedction()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getdeddetail", ref isValid, lbl_EmpCode.Text, "");
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
    //  [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    //public static string getDeductionAmount(string username, string yrmn)
    // {
    //     string isvalid = string.Empty;
    //     string amount = (string)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "getdeductamount", ref isvalid, username, yrmn);
    //     if (amount == null || amount == "")
    //      {
    //          amount = "0";
    //      }
    //     return amount;
    // }
}

