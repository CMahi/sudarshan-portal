using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AjaxPro;
using FSL.Logging;
using FSL.Controller;
using FSL.Message;

public partial class Car_Data_For_Employee : System.Web.UI.Page
{
    ListItem Li = new ListItem("--Select One--", "0");
    StringBuilder str = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Car_Data_For_Employee));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        string isdata = string.Empty;
                        txt_Username.Text = txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "empdata", ref isdata, txt_Username.Text);
                        if (dt.Rows.Count > 0 || dt != null)
                        {
                            span_code.InnerHtml = dt.Rows[0]["EMP_ID"].ToString();
                            span_name.InnerHtml = dt.Rows[0]["EMPLOYEE_NAME"].ToString();
                        }

                    }
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StringBuilder html_Header = new StringBuilder();
        StringBuilder html_data = new StringBuilder();
        DataTable dt = new DataTable();
        string isValid = string.Empty;
        decimal total_Amount = 0;
        decimal cumu_Amount = 0;
        DataTable dtp = new DataTable();
        decimal total_AmountRejected = 0;
        string isdata = string.Empty;
        ///////////for driver/////////////
        if (ddl_Exp_Head.SelectedValue == "Driver")
        {
            html_data.Append("<table class='table table-bordered' id='tbldata' width='100%'>");
            html_data.Append("<thead><tr class='grey'><th>Driver Type</th><th>Eligibility as on date for current financial year (Rs)</th><th>Claimed so far as on date for current financial year (Rs)</th><th>Balance as on date (Rs)</th></tr></thead>");
            html_data.Append("<tbody>");
            string[] driver_exp = { "Salary", "Ex-Gratia", "Uniform" };
            for (int i = 0; i < 3; i++)
            {
                string dtva1 = (string)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "yearvalue", ref isdata, txt_Username.Text, driver_exp[i]);
                string[] Request_Data1 = dtva1.Split('=');
                html_data.Append("<tr><td>" + driver_exp[i] + "</td><td>" + Request_Data1[0] + "</td><td>" + Request_Data1[1] + "</td><td>" + Request_Data1[2] + "</td><tr>");
            }
            html_data.Append("</tbody></table>");
        }
        else if (ddl_Exp_Head.SelectedValue == "Fuel" || ddl_Exp_Head.SelectedValue == "Maintenance")
        {

            html_data.Append("<table class='table table-bordered' id='tbldata' width='100%'>");
            if (ddl_Exp_Head.SelectedValue == "Fuel")
            {
                html_data.Append("<thead><tr class='grey'><th>Expense Head</th><th>Eligibility in litters as on date for current financial year (Litters)</th><th>Claimed so far as on date for current financial year (Litters)</th><th>Balance as on date (Litters)</th></tr></thead>");
            }
            else
            {
                html_data.Append("<thead><tr class='grey'><th>Expense Head</th><th>Eligibility as on date (Rs)</th><th>Claimed so far as on date (Rs)</th><th>Balance as on date (Rs)</th></tr></thead>");
            }
            string dtva = (string)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "yearvalue", ref isdata, txt_Username.Text, ddl_Exp_Head.SelectedValue);
             if (dtva != null || dtva!="")            
            {
                string[] Request_Data = dtva.Split('=');
                html_data.Append("<tbody>");
                html_data.Append("<tr><td>" + ddl_Exp_Head.SelectedValue + "</td><td>" + Request_Data[0] + "</td><td>" + Request_Data[1] + "</td><td>" + Request_Data[2] + "</td><tr>");
                html_data.Append("</tbody></table>");
            }
        }
        //////////////////////////////////////
        divyear.InnerHtml = html_data.ToString();

        html_Header.Append("<table class='table table-bordered' id='tblfuel' width='100%'>");
        if (ddl_Exp_Head.SelectedValue == "Maintenance")
        {
            html_Header.Append("<thead><tr class='grey'><th>Sr No</th><th style='text-align:center'>Portal Docket No</th><th style='display:none'></th><th style='display:none'></th><th style='text-align:center'>Voucher Date</th><th style='text-align:center'>Vehicle No.</th><th style='text-align:center'>Aging Of Car</th><th style='text-align:center'>Expense Head</th><th>Year</th><th style='text-align:center'>Cumulative Eligibility Amount</th><th style='text-align:center'>Claimed Amount</th><th style='text-align:center'>Status</th><th style='text-align:center'>Pending with</th><th style='text-align:center'>SAP Document No</th></tr></thead>");
            dtp = (DataTable)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "policydata", ref isdata, txt_Username.Text);
        }
        else if (ddl_Exp_Head.SelectedValue == "Fuel")
        {
            html_Header.Append("<thead><tr class='grey'><th>Sr No</th><th style='text-align:center'>Portal Docket No</th><th style='display:none'></th><th style='display:none'></th><th style='text-align:center'>Voucher Date</th><th style='text-align:center'>Expense Head</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Fuel in litters</th><th style='text-align:center'>Status</th><th style='text-align:center'>Pending with</th><th style='text-align:center'>SAP Document No</th></tr></thead>");

        }
        else
        {
            html_Header.Append("<thead><tr class='grey'><th>Sr No</th><th style='text-align:center'>Portal Docket No</th><th style='display:none'></th><th style='display:none'></th><th style='text-align:center'>Voucher Date</th><th style='text-align:center'>Expense Head</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Status</th><th style='text-align:center'>Pending with</th><th style='text-align:center'>SAP Document No</th></tr></thead>");
        }
        html_Header.Append("<tbody>");
        dt = (DataTable)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "getdata", ref isdata, ddl_Exp_Head.SelectedValue, txt_Username.Text, ddlStatus.SelectedValue);
        int cnt = 1;
        if (dt.Rows.Count > 0 || dt != null || dtp.Rows.Count > 0 || dtp != null)
        {
            if (ddl_Exp_Head.SelectedValue == "Maintenance")
            {
                for (int j = 0; j < dtp.Rows.Count; j++)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Car_Age"].ToString() == dtp.Rows[j]["YEAR"].ToString())
                        {
                            html_Header.Append("<tr><td>" + (cnt) + "</td>");
                            html_Header.Append("<td><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "' onclick='getRequestDetails(" + (i + 1) + ")'>" + Convert.ToString(dt.Rows[i]["CAR_EXPENSE_NO"]) + "</a><input type='text' id='pname" + (i + 1) + "' Value='" + Convert.ToString(dt.Rows[i]["PROCESS_NAME"]) + "' style='display:none'/><input type='text' id='h_info" + (i + 1) + "' Value=" + Convert.ToString(dt.Rows[i]["CAR_EXPENSE_NO"]) + " style='display:none'></input></td>");
                
                            //html_Header.Append("<td class='reqno'><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "'>" + Convert.ToString(dt.Rows[i]["CAR_EXPENSE_NO"]) + "</a></td>");
                            html_Header.Append("<td style='display:none'>" + dt.Rows[i]["CAR_EXPENSE_NO"].ToString() + "</td>");
                            html_Header.Append("<td style='display:none'>" + dt.Rows[i]["PROCESS_NAME"].ToString() + "</td>");

                            html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["c_date"].ToString() + "</td>");
                            if (ddl_Exp_Head.SelectedValue == "Maintenance")
                            {
                                html_Header.Append("<td>" + dt.Rows[i]["Vehical_No"] + "</td><td style='text-align:center'>" + dt.Rows[i]["Car_Age"] + "</td>");
                            }
                            if (ddl_Exp_Head.SelectedValue == "Driver")
                            {
                                html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["DRIVER_TYPE"] + "</td>");
                            }
                            else
                            {
                                html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["Flag"] + "</td>");
                            }
                            if (ddl_Exp_Head.SelectedValue == "Maintenance")
                            {
                                html_Header.Append("<td>" + dtp.Rows[j]["YEAR"] + "</td>");
                                html_Header.Append("<td>" + dtp.Rows[j]["AMOUNT"] + "</td>");
                            }
                            html_Header.Append("<td style='text-align:right'>" + dt.Rows[i]["EXPENSE_AMOUNT"] + "</td>");
                            if (ddl_Exp_Head.SelectedValue == "Fuel")
                            {
                                html_Header.Append("<td>" + dt.Rows[i]["FUEL_LITRE"] + "</td>");
                            }
                            html_Header.Append("<td>" + dt.Rows[i]["STATUS"] + "</td>");
                            if (dt.Rows[i]["STATUS"].ToString() == "Rejected" || dt.Rows[i]["STATUS"].ToString() == "REJECTED")
                            {
                                total_AmountRejected = total_AmountRejected + Convert.ToDecimal(dt.Rows[i]["AMOUNT"].ToString());
                            }
                            else
                            {
                                cumu_Amount = cumu_Amount + Convert.ToDecimal(dtp.Rows[j]["AMOUNT"].ToString());
                            }
                            total_Amount =total_Amount + Convert.ToDecimal(dt.Rows[i]["EXPENSE_AMOUNT"].ToString());
                            txt_cumuamt.Text = Convert.ToString(cumu_Amount);
                            span_cumu.InnerHtml = Convert.ToString(cumu_Amount);

                            string apppending = (string)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "pendingname", ref isValid, dt.Rows[i]["CAR_EXPENSE_NO"].ToString());
                            if (apppending != null || apppending != "")
                            {
                                html_Header.Append("<td>" + apppending.ToString() + "</td>");
                            }
                            else
                            {
                                html_Header.Append("<td></td>");
                            }
                            html_Header.Append("<td>" + dt.Rows[i]["sap_no"].ToString() + "</td></tr>");
                            cnt = cnt + 1;
                        }
                        else
                        {
                            if (i == 0)
                            {
                                html_Header.Append("<tr><td>" + (cnt) + "</td>");
                                html_Header.Append("<td>-</td>");
                                html_Header.Append("<td style='display:none'>-</td>");
                                html_Header.Append("<td style='display:none'>-</td>");

                                html_Header.Append("<td style='text-align:center'>-</td>");
                                if (ddl_Exp_Head.SelectedValue == "Maintenance")
                                {
                                    html_Header.Append("<td style='text-align:center'>-</td><td style='text-align:center'>-</td>");
                                }
                                html_Header.Append("<td style='text-align:center'>-</td>");

                                if (ddl_Exp_Head.SelectedValue == "Maintenance")
                                {
                                    html_Header.Append("<td>" + dtp.Rows[j]["YEAR"] + "</td>");
                                    html_Header.Append("<td>" + dtp.Rows[j]["AMOUNT"] + "</td>");
                                }
                                html_Header.Append("<td style='text-align:right'>-</td>");
                                if (ddl_Exp_Head.SelectedValue == "Fuel")
                                {
                                    html_Header.Append("<td>-</td>");
                                }
                                html_Header.Append("<td>-</td>");
                                cumu_Amount = cumu_Amount + Convert.ToDecimal(dtp.Rows[j]["AMOUNT"].ToString());
                                txt_cumuamt.Text = Convert.ToString(cumu_Amount);
                                span_cumu.InnerHtml = Convert.ToString(cumu_Amount);
                                html_Header.Append("<td>-</td>");
                                html_Header.Append("<td>-</td></tr>");
                                cnt = cnt + 1;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html_Header.Append("<tr><td>" + (i + 1) + "</td>");
                    html_Header.Append("<td><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "' onclick='getRequestDetails(" + (i + 1) + ")'>" + Convert.ToString(dt.Rows[i]["CAR_EXPENSE_NO"]) + "</a><input type='text' id='pname" + (i + 1) + "' Value='" + Convert.ToString(dt.Rows[i]["PROCESS_NAME"]) + "' style='display:none'/><input type='text' id='h_info" + (i + 1) + "' Value=" + Convert.ToString(dt.Rows[i]["CAR_EXPENSE_NO"]) + " style='display:none'></input></td>");
                    html_Header.Append("<td style='display:none'>" + dt.Rows[i]["CAR_EXPENSE_NO"].ToString() + "</td>");
                    html_Header.Append("<td style='display:none'>" + dt.Rows[i]["PROCESS_NAME"].ToString() + "</td>");

                    html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["c_date"].ToString() + "</td>");
                    if (ddl_Exp_Head.SelectedValue == "Driver")
                    {
                        html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["DRIVER_TYPE"] + "</td>");
                    }
                    else
                    {
                        html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["Flag"] + "</td>");
                    }
                    if (ddl_Exp_Head.SelectedValue == "Maintenance")
                    {
                        html_Header.Append("<td>" + dt.Rows[i]["YEAR"] + "</td>");
                        html_Header.Append("<td>" + dt.Rows[i]["AMOUNT"] + "</td>");
                    }
                    html_Header.Append("<td style='text-align:right'>" + dt.Rows[i]["EXPENSE_AMOUNT"] + "</td>");
                    if (ddl_Exp_Head.SelectedValue == "Fuel")
                    {
                        html_Header.Append("<td>" + dt.Rows[i]["FUEL_LITRE"] + "</td>");
                    }
                    html_Header.Append("<td>" + dt.Rows[i]["STATUS"] + "</td>");
                    if (dt.Rows[i]["STATUS"].ToString() == "Rejected" || dt.Rows[i]["STATUS"].ToString() == "REJECTED")
                    {
                        total_AmountRejected = total_AmountRejected + Convert.ToDecimal(dt.Rows[i]["EXPENSE_AMOUNT"].ToString());
                        total_Amount = total_Amount + Convert.ToDecimal(dt.Rows[i]["EXPENSE_AMOUNT"].ToString());
                    }
                    else
                    {
                        total_Amount = total_Amount + Convert.ToDecimal(dt.Rows[i]["EXPENSE_AMOUNT"].ToString());
                    }
                    span_cumu.InnerHtml = "";

                    string apppending = (string)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "pendingname", ref isValid, dt.Rows[i]["CAR_EXPENSE_NO"].ToString());
                    if (apppending != null || apppending != "")
                    {
                        html_Header.Append("<td>" + apppending.ToString() + "</td>");
                    }
                    else
                    {
                        html_Header.Append("<td></td>");
                    }
                    html_Header.Append("<td>" + dt.Rows[i]["sap_no"].ToString() + "</td></tr>");
                }
            }

            html_Header.Append("</tbody></table>");
            divdata.InnerHtml = html_Header.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#tblfuel').dataTable({ 'bSort': false });", true);
            txt_totalamt.Text = Convert.ToString(total_Amount);
            spn_amount.InnerHtml = Convert.ToString(total_Amount);
            spn_Reject_amount.InnerHtml = Convert.ToString(total_AmountRejected);
            txt_rejectedamt.Text = Convert.ToString(total_AmountRejected);
            divIns.Style.Add("display", "none");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=" + 4);
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        divdata.InnerHtml = null;
        ddlStatus.SelectedIndex = 0;
        ddl_Exp_Head.SelectedIndex = 0;
        divyear.InnerHtml = null;
        spn_amount.InnerHtml = null;
        span_cumu.InnerHtml = null;
        spn_Reject_amount.InnerHtml = null;
        txtdata.Text = "";
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getDetails(string req_no, string process_name)
    {
        string sb = "";
        try
        {
            Expense_Details ed = new Expense_Details();
            sb = ed.Expense_Request_Details(req_no, process_name);
        }
        catch (Exception ex)
        {
            sb = "";
        }
        return sb.ToString();
    }
    protected void btn_Export_onClick(object sender, EventArgs e)
    {

        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            string data = divdata.InnerHtml;
            ExportToExcel(data, "YEAR END PROCESSING BY EMPLOYEE REPORT");
        }
    }
    protected void ExportToExcel(string dgview, string filename)
    {
        try
        {
            string dtvalue = string.Empty;
            string dtdata = string.Empty;
            string isValid = string.Empty;
            DataTable dt = new DataTable();
            DataTable dtp = new DataTable();
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringBuilder html_data = new StringBuilder();
            StringBuilder html_Header = new StringBuilder();
            string isdata = string.Empty;
            ///////////for driver/////////////
            if (ddl_Exp_Head.SelectedValue == "Driver")
            {
                html_data.Append("<table class='table table-bordered' id='tbldata' width='100%'>");
                html_data.Append("<thead><tr class='grey'><th>Driver Type</th><th>Eligibility in litters for current financial year</th><th>Claimed so far during this financial year</th><th>Balance</th></tr></thead>");
                html_data.Append("<tbody>");
                string[] driver_exp = { "Salary", "Ex-Gratia", "Uniform" };
                for (int i = 0; i < 2; i++)
                {
                    string dtva1 = (string)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "yearvalue", ref isdata, txt_Username.Text, driver_exp[i]);
                    string[] Request_Data1 = dtva1.Split('=');
                    html_data.Append("<tr><td>" + driver_exp[i] + "</td><td>" + Request_Data1[0] + "</td><td>" + Request_Data1[1] + "</td><td>" + Request_Data1[2] + "</td><tr>");

                }
                html_data.Append("</tbody></table>");
            }
            else if (ddl_Exp_Head.SelectedValue == "Fuel" || ddl_Exp_Head.SelectedValue == "Maintenance")
            {

                html_data.Append("<table class='table table-bordered' id='tbldata' width='100%'>");
                html_data.Append("<thead><tr class='grey'><th>Expense Head</th><th>Eligibility in litters for current financial year</th><th>Claimed so far during this financial year</th><th>Balance</th></tr></thead>");
                string dtva = (string)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "yearvalue", ref isdata, txt_Username.Text, ddl_Exp_Head.SelectedValue);

                if (dtva != null || dtva!="")
                {
                    string[] Request_Data = dtva.Split('=');
                    html_data.Append("<tbody>");
                    html_data.Append("<tr><td>" + ddl_Exp_Head.SelectedValue + "</td><td>" + Request_Data[0] + "</td><td>" + Request_Data[1] + "</td><td>" + Request_Data[2] + "</td><tr>");
                    html_data.Append("</tbody></table>");
                }
            }
            //////////////////////////////////////
            divyear.InnerHtml = html_data.ToString();
            dtdata = html_data.ToString();
            int cnt = 0;
            html_Header.Append("<table class='table table-bordered' id='tblfuel' width='100%'>");
            if (ddl_Exp_Head.SelectedValue == "Maintenance")
            {
                html_Header.Append("<thead><tr class='grey'><th>Sr No</th><th style='text-align:center'>Portal Docket No</th><th style='text-align:center'>Voucher Date</th><th style='text-align:center'>Vehicle No.</th><th style='text-align:center'>Aging Of Car</th><th style='text-align:center'>Expense Head</th><th>Year</th><th style='text-align:center'>Cumulative Amount</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Status</th><th style='text-align:center'>Pending with</th><th style='text-align:center'>SAP Document No</th></tr></thead>");
                dtp = (DataTable)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "policydata", ref isdata, txt_Username.Text);
    
            }
            else if (ddl_Exp_Head.SelectedValue == "Fuel")
            {
                html_Header.Append("<thead><tr class='grey'><th>Sr No</th><th style='text-align:center'>Portal Docket No</th><th style='text-align:center'>Voucher Date</th><th style='text-align:center'>Expense Head</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Fuel in litters</th><th style='text-align:center'>Status</th><th style='text-align:center'>Pending with</th><th style='text-align:center'>SAP Document No</th></tr></thead>");

            }
            else
            {
                html_Header.Append("<thead><tr class='grey'><th>Sr No</th><th style='text-align:center'>Portal Docket No</th><th style='text-align:center'>Voucher Date</th><th style='text-align:center'>Expense Head</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Status</th><th style='text-align:center'>Pending with</th><th style='text-align:center'>SAP Document No</th></tr></thead>");

            }
            html_Header.Append("<tbody>");
            dt = (DataTable)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "getdata", ref isdata, ddl_Exp_Head.SelectedValue, txt_Username.Text, ddlStatus.SelectedValue);

            if (dt.Rows.Count > 0 || dt != null)
            {
                if (ddl_Exp_Head.SelectedValue == "Maintenance")
                {
                    for (int j = 0; j < dtp.Rows.Count; j++)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            if (dt.Rows[i]["Car_Age"].ToString() == dtp.Rows[j]["YEAR"].ToString())
                            {
                                html_Header.Append("<tr><td>" + (cnt) + "</td>");
                                html_Header.Append("<td>" + Convert.ToString(dt.Rows[i]["CAR_EXPENSE_NO"]) + "</td>");
                                html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["c_date"].ToString() + "</td>");
                                html_Header.Append("<td>" + dt.Rows[i]["Vehical_No"] + "</td><td style='text-align:center'>" + dt.Rows[i]["Car_Age"] + "</td>");
                                html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["Flag"] + "</td>");
                                html_Header.Append("<td>" + dtp.Rows[j]["YEAR"] + "</td>");
                                html_Header.Append("<td>" + dtp.Rows[j]["AMOUNT"] + "</td>");
                                html_Header.Append("<td style='text-align:right'>" + dt.Rows[i]["EXPENSE_AMOUNT"] + "</td>");
                                if (ddl_Exp_Head.SelectedValue == "Fuel")
                                {
                                    html_Header.Append("<td>" + dt.Rows[i]["FUEL_LITRE"] + "</td>");
                                }
                                html_Header.Append("<td>" + dt.Rows[i]["STATUS"] + "</td>");
                                string apppending = (string)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "pendingname", ref isValid, dt.Rows[i]["CAR_EXPENSE_NO"].ToString());
                                if (apppending != null || apppending != "")
                                {
                                    html_Header.Append("<td>" + apppending.ToString() + "</td>");
                                }
                                else
                                {
                                    html_Header.Append("<td></td>");
                                }
                                html_Header.Append("<td>" + dt.Rows[i]["sap_no"].ToString() + "</td></tr>");
                                cnt = cnt + 1;
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    html_Header.Append("<tr><td>" + (cnt) + "</td>");
                                    html_Header.Append("<td>-</td>");
                                    html_Header.Append("<td style='text-align:center'>-</td>");
                                    html_Header.Append("<td style='text-align:center'>-</td><td style='text-align:center'>-</td>");
                                    html_Header.Append("<td style='text-align:center'>-</td>");
                                    html_Header.Append("<td>" + dtp.Rows[j]["YEAR"] + "</td>");
                                    html_Header.Append("<td>" + dtp.Rows[j]["AMOUNT"] + "</td>");
                                    html_Header.Append("<td style='text-align:right'>-</td>");
                                    if (ddl_Exp_Head.SelectedValue == "Fuel")
                                    {
                                        html_Header.Append("<td>-</td>");
                                    }
                                    html_Header.Append("<td>-</td>");
                                    html_Header.Append("<td>-</td>");
                                    html_Header.Append("<td>-</td></tr>");
                                    cnt = cnt + 1;
                                }
                            }
                        }
                    }
                }

                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        html_Header.Append("<tr><td>" + (i + 1) + "</td>");
                        html_Header.Append("<td>" + Convert.ToString(dt.Rows[i]["CAR_EXPENSE_NO"]) + "</td>");
                        html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["c_date"].ToString() + "</td>");
                        if (ddl_Exp_Head.SelectedValue == "Maintenance")
                        {
                            html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["Vehical_No"] + "</td><td style='text-align:center'>" + dt.Rows[i]["Car_Age"] + "</td>");
                        }
                        if (ddl_Exp_Head.SelectedValue == "Driver")
                        {
                            html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["DRIVER_TYPE"] + "</td>");
                        }
                        else
                        {
                            html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["Flag"] + "</td>");
                        }
                        html_Header.Append("<td style='text-align:right'>" + dt.Rows[i]["EXPENSE_AMOUNT"] + "</td>");
                        if (ddl_Exp_Head.SelectedValue == "Fuel")
                        {
                            html_Header.Append("<td>" + dt.Rows[i]["FUEL_LITRE"] + "</td>");
                        }
                        html_Header.Append("<td>" + dt.Rows[i]["STATUS"] + "</td>");

                        string apppending = (string)ActionController.ExecuteAction("", "Car_Data_For_Employee.aspx", "pendingname", ref isValid, dt.Rows[i]["CAR_EXPENSE_NO"].ToString());
                        if (apppending != null || apppending != "")
                        {
                            html_Header.Append("<td>" + apppending.ToString() + "</td>");
                        }
                        else
                        {
                            html_Header.Append("<td></td>");
                        }
                        html_Header.Append("<td>" + dt.Rows[i]["sap_no"].ToString() + "</td></tr>");
                        txt_cumuamt.Text = "0";
                    }
                }

                html_Header.Append("</tbody></table>");
                divdata.InnerHtml = html_Header.ToString();
            }
            dtvalue = html_Header.ToString();
            if (dtvalue != "")
            {
                Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                    "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
                Response.Write("<tr><td><b>Total Amount : " + txt_totalamt.Text + "</b></td></tr>");
                Response.Write("<tr><td><b> & Cumulative Amount : " + txt_cumuamt.Text + "</b></td></tr>");
                Response.Write("<tr><td><b> & Rejected Amount : " + txt_rejectedamt.Text + "</b></td></tr>");
                Response.Write(dtdata);
                Response.Write(dtvalue);
            }
            Response.End();
        }

        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
            return;
        }
    }


}