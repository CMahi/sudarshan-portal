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

public partial class Expense_Status_Report : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Expense_Status_Report));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        FillVoucher();
                        fillstatus();
                    }
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    private void FillVoucher()
    {

        String IsData = string.Empty;
        ListItem Li = new ListItem("--Select All--", "0");
        ListItem Li1 = new ListItem("Advance Request", "29");
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "vouchertype", ref IsData);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlVoucherType.DataSource = dt;
            ddlVoucherType.DataTextField = "VOUCHER_TYPE";
            ddlVoucherType.DataValueField = "PK_VOUCHER_TYPE_ID";
            ddlVoucherType.DataBind();
            ddlVoucherType.Items.Insert(0, Li);
            ddlVoucherType.Items.Insert(dt.Rows.Count + 1, Li1);
        }
    }
    private void fillstatus()
    {
        ddlStatus.Items.Insert(0, new ListItem("--Select All--", "0"));
        ddlStatus.Items.Insert(1, new ListItem("Completed", "Completed"));
        ddlStatus.Items.Insert(2, new ListItem("Inprogress", "Inprogress"));
        ddlStatus.Items.Insert(2, new ListItem("Rejected", "Rejected"));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string isValid = string.Empty;
        string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
        if (ddlVoucherType.SelectedValue == "0")
        {
            decimal total_Amount = 0;
            decimal total_AmountRejected = 0;
            int cnt = 0;
            DataSet ds = (DataSet)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "selectall", ref isValid, ddlVoucherType.SelectedValue, txt_f_date.Value, txt_t_date.Value, ddlStatus.SelectedValue);
            str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th> #</th><th>Portal Docket No.</th><th style='display:none'></th><th style='display:none'></th><th>Voucher Date</th><th>Employee Code</th><th>Employee Name</th><th>Voucher Type</th><th>Amount</th><th>Status</th><th>Pending with</th><th>Deviation Reason</th><th>SAP Document No.</th><th>Posting Date</th><th>Documents</th><th>Voucher Link</th></tr> ");
            str.Append("</thead><tbody>");
            for (int k = 0; k < 5; k++)
            {
                if (ds != null && ds.Tables[k].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[k].Rows.Count; i++)
                    {
                        str.Append("<tr>");
                        str.Append("<td>" + (cnt + 1) + "</td>");
                       // str.Append("<td><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "' onclick='getRequestDetails(" + (i + 1) + ")'>" + Convert.ToString(ds.Tables[k].Rows[i]["REQUEST_NO"]) + "</a><input type='text' id='pname" + (i + 1) + "' Value='" + Convert.ToString(ds.Tables[k].Rows[i]["PROCESS_NAME"]) + "' style='display:none'/><input type='text' id='h_info" + (i + 1) + "' Value=" + Convert.ToString(ds.Tables[k].Rows[i]["REQUEST_NO"]) + " style='display:none'></input></td>");
                        str.Append("<td class='reqno'><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "'>" + Convert.ToString(ds.Tables[k].Rows[i]["REQUEST_NO"]) + "</a></td>");
                        str.Append("<td style='display:none'>" + ds.Tables[k].Rows[i]["REQUEST_NO"].ToString() + "</td>");
                        str.Append("<td style='display:none'>" + ds.Tables[k].Rows[i]["PROCESS_NAME"].ToString() + "</td>");
                        str.Append("<td>" + ds.Tables[k].Rows[i]["created_date"].ToString() + "</td>");
                        str.Append("<td>" + ds.Tables[k].Rows[i]["EMP_ID"].ToString() + "</td>");
                        str.Append("<td>" + ds.Tables[k].Rows[i]["EMPLOYEE_NAME"].ToString() + "</td>");
                        str.Append("<td>" + ds.Tables[k].Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
                        str.Append("<td align='right'>" + ds.Tables[k].Rows[i]["Amount"].ToString() + "</td>");
                        str.Append("<td>" + ds.Tables[k].Rows[i]["Current_Status"].ToString() + "</td>");
                        if (ds.Tables[k].Rows[i]["Current_Status"].ToString() == "COMPLETED" || ds.Tables[k].Rows[i]["Current_Status"].ToString() == "Completed")
                        {
                            str.Append("<td></td>");
                        }
                        else
                        {
                            string appname = (string)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "appname", ref isValid, ds.Tables[k].Rows[i]["PK_TRANSID"].ToString());
                            if (appname != null)
                            {
                                str.Append("<td>" + appname.ToString() + "</td>");
                            }
                        }
                        string deviate1 = (string)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "getdeviate", ref isValid, ds.Tables[k].Rows[i]["REQUEST_NO"].ToString());
                        if (deviate1 != null && deviate1 != "")
                        {
                            str.Append("<td>" + deviate1 + "</td>");
                        }
                        else
                        {
                            str.Append("<td></td>");
                        }
                        str.Append("<td>" + ds.Tables[k].Rows[i]["sap_no"].ToString() + "</td>");
                        if (Convert.ToString(ds.Tables[k].Rows[i]["Creation_Date"]) == "")
                        {
                            str.Append("<td></td>");
                        }
                        else
                        {
                            str.Append("<td>" + Convert.ToDateTime(ds.Tables[k].Rows[i]["Creation_Date"]).ToString("dd-MMM-yyyy") + "</td>");
                        }
                        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "getdocdetail", ref isValid, Convert.ToString(ds.Tables[k].Rows[i]["REQUEST_NO"]), Convert.ToString(ds.Tables[k].Rows[i]["PROCESS_NAME"]));
                        if (dsData.Tables[0].Rows.Count <= 0)
                        {
                            str.Append("<td></td>");
                        }
                        else
                        {
                            str.Append("<td class='doc'><a href='#doc_details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "'><img id='A_FileUpload1' src='../../images/attachment.png' alt='Click here to attach file.' height='20' width='20' /></a></td>");

                        }

                        
                        string param_Pro = "";
                        string param_req = "";
                        string voucher_link = "";
                        if (Convert.ToString(ds.Tables[k].Rows[i]["PROCESS_NAME"]).ToUpper() == "CAR POLICY")
                        {
                            //param_Pro = "CAR POLICY";
                            param_req = Convert.ToString(ds.Tables[k].Rows[i]["REQUEST_NO"]);
                            voucher_link = "http://" + compname + "/Sudarshan-Portal/Vouchers/Car_Voucher.aspx?P=" + param_Pro + "&R=" + param_req;
                        }
                        else
                        {
                            CryptoGraphy crypt = new CryptoGraphy();
                            param_Pro = crypt.Encryptdata(Convert.ToString(ds.Tables[k].Rows[i]["PROCESS_NAME"]));
                            param_req = crypt.Encryptdata(Convert.ToString(ds.Tables[k].Rows[i]["REQUEST_NO"]));
                            if (Convert.ToString(ds.Tables[k].Rows[i]["PAYMENT_MODE"]) == "1")
                            {
                                voucher_link = "http://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + param_Pro + "&R=" + param_req;
                            }
                            else
                            {
                                voucher_link = "http://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + param_Pro + "&R=" + param_req;
                            }
                        }
                        str.Append("<td><a href='#' onclick=download_Link('" + voucher_link + "','" + Convert.ToString(ds.Tables[k].Rows[i]["REQUEST_NO"]) + "')>Voucher Print</a></td>");

                        str.Append("</tr>");
                        cnt = cnt + 1;
                        if (ds.Tables[k].Rows[i]["Current_Status"].ToString() == "Rejected" || ds.Tables[k].Rows[i]["Current_Status"].ToString() == "REJECTED")
                        {
                            total_AmountRejected = total_AmountRejected + Convert.ToDecimal(ds.Tables[k].Rows[i]["Amount"].ToString());
                        }
                        else
                        {
                            total_Amount = total_Amount + Convert.ToDecimal(ds.Tables[k].Rows[i]["Amount"].ToString());
                        }
                        
                    }
                    //$("#divIns").show();

                }

                else
                {
                    divdata.InnerHtml = null;
                    //divIns.Style.Add("display","none");
                    // Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
                }
            }
            str.Append("</tbody></table> ");
            divdata.InnerHtml = str.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({ 'bSort': false });", true);
            spn_amount.InnerHtml = Convert.ToString(total_Amount);
            txt_totalamt.Text = Convert.ToString(total_Amount);
            spn_rejamount.InnerHtml = Convert.ToString(total_AmountRejected);
            txt_rejectedamt.Text = Convert.ToString(total_AmountRejected);
            //divIns.Style.Add("display", "none");
            //}
        }
        else
        {
            decimal total_Amount1 = 0;
            decimal total_AmountRejected1 = 0;
            int cnt1 = 0;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "selectdata", ref isValid, ddlVoucherType.SelectedValue, txt_f_date.Value, txt_t_date.Value, ddlStatus.SelectedValue);
            str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th> #</th><th>Portal Docket No.</th><th>Voucher Date</th><th>Employee Code</th><th>Employee Name</th><th>Voucher Type</th><th>Amount</th><th>Status</th><th>Pending with</th><th>Deviation Reason</th><th>SAP Document No.</th><th>Posting Date</th><th>Documents</th><th>Voucher Link</th></tr> ");
            if (dt != null && dt.Rows.Count > 0)
            {
                str.Append("</thead><tbody>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str.Append("<tr>");
                    str.Append("<td>" + (i + 1) + "</td>");
                    str.Append("<td><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "' onclick='getRequestDetails(" + (i + 1) + ")'>" + Convert.ToString(dt.Rows[i]["REQUEST_NO"]) + "</a><input type='text' id='pname" + (i + 1) + "' Value='" + Convert.ToString(dt.Rows[i]["PROCESS_NAME"]) + "' style='display:none'/><input type='text' id='h_info" + (i + 1) + "' Value=" + Convert.ToString(dt.Rows[i]["REQUEST_NO"]) + " style='display:none'></input></td>");
                    //  str.Append("<td>" + dt.Rows[i]["REQUEST_NO"].ToString() + "</td>");
                    str.Append("<td>" + dt.Rows[i]["created_date"].ToString() + "</td>");
                    str.Append("<td>" + dt.Rows[i]["EMP_ID"].ToString() + "</td>");
                    str.Append("<td>" + dt.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td>");
                    if (ddlVoucherType.SelectedValue == "1")
                    {
                        str.Append("<td>" + dt.Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
                    }
                    else
                    {
                        str.Append("<td>" + ddlVoucherType.SelectedItem.Text + "</td>");
                    }
                    str.Append("<td align='right'>" + dt.Rows[i]["Amount"].ToString() + "</td>");
                    str.Append("<td>" + dt.Rows[i]["Current_Status"].ToString() + "</td>");
                    if (dt.Rows[i]["Current_Status"].ToString() == "COMPLETED" || dt.Rows[i]["Current_Status"].ToString() == "Completed")
                    {
                        str.Append("<td></td>");
                    }
                    else
                    {
                        // str.Append("<td>" + dt.Rows[i]["PK_TRANSID"].ToString() + "</td>");
                        string appname = (string)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "appname", ref isValid, dt.Rows[i]["PK_TRANSID"].ToString());
                        if (appname != null)
                        {
                            str.Append("<td>" + appname.ToString() + "</td>");
                        }
                    }
                    string deviate1 = (string)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "getdeviate", ref isValid, dt.Rows[i]["REQUEST_NO"].ToString());
                    if (deviate1 != null && deviate1 != "")
                    {
                        str.Append("<td>" + deviate1 + "</td>");
                    }
                    else
                    {
                        str.Append("<td></td>");
                    }

                    str.Append("<td>" + dt.Rows[i]["sap_no"].ToString() + "</td>");
                    if (Convert.ToString(dt.Rows[i]["Creation_Date"]) == "")
                    {
                        str.Append("<td></td>");
                    }
                    else
                    {
                        str.Append("<td>" + Convert.ToDateTime(dt.Rows[i]["Creation_Date"]).ToString("dd-MMM-yyyy") + "</td>");
                    }
                    DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "getdocdetail", ref isValid, Convert.ToString(dt.Rows[i]["REQUEST_NO"]), Convert.ToString(dt.Rows[i]["PROCESS_NAME"]));
                    if (dsData.Tables[0].Rows.Count<=0)
                    {
                        str.Append("<td></td>");
                    }
                    else
                    {
                        str.Append("<td><a href='#doc_details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "' onclick='getDocDetails(" + (i + 1) + ")'><img id='A_FileUpload1' src='../../images/attachment.png' alt='Click here to attach file.' height='20' width='20' /></a><input type='text' id='pname" + (i + 1) + "' Value='" + Convert.ToString(dt.Rows[i]["PROCESS_NAME"]) + "' style='display:none'/><input type='text' id='h_info" + (i + 1) + "' Value='" + Convert.ToString(dt.Rows[i]["REQUEST_NO"]) + "' style='display:none'></input></td>");
                    }

                    string param_Pro = "";
                    string param_req = "";
                    string voucher_link = "";
                    if (Convert.ToString(dt.Rows[i]["PROCESS_NAME"]).ToUpper() == "CAR POLICY")
                    {
                        //param_Pro = "CAR POLICY";
                        param_req = Convert.ToString(dt.Rows[i]["REQUEST_NO"]);
                        voucher_link = "http://" + compname + "/Sudarshan-Portal/Vouchers/Car_Voucher.aspx?P=" + param_Pro + "&R=" + param_req;
                    }
                    else
                    {
                        CryptoGraphy crypt = new CryptoGraphy();
                        param_Pro = crypt.Encryptdata(Convert.ToString(dt.Rows[i]["PROCESS_NAME"]));
                        param_req = crypt.Encryptdata(Convert.ToString(dt.Rows[i]["REQUEST_NO"]));
                        if (Convert.ToString(dt.Rows[i]["PAYMENT_MODE"]) == "1")
                        {
                            voucher_link = "http://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + param_Pro + "&R=" + param_req;
                        }
                        else
                        {
                            voucher_link = "http://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + param_Pro + "&R=" + param_req;
                        }
                    }
                    str.Append("<td><a href='#' onclick=download_Link('" + voucher_link + "','" + Convert.ToString(dt.Rows[i]["REQUEST_NO"]) + "')>Voucher Print</a></td>");
                    str.Append("</tr>");
                    cnt1 = cnt1 + 1;
                    if (dt.Rows[i]["Current_Status"].ToString() == "Rejected" || dt.Rows[i]["Current_Status"].ToString() == "REJECTED")
                    {
                        total_AmountRejected1 = total_AmountRejected1 + Convert.ToDecimal(dt.Rows[i]["Amount"].ToString());
                    }
                    else
                    {
                        total_Amount1 = total_Amount1 + Convert.ToDecimal(dt.Rows[i]["Amount"].ToString());
                    }
                   
                }
            }

            else
            {
                divdata.InnerHtml = null;
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
            }
            str.Append("</tbody></table> ");
            divdata.InnerHtml = str.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({ 'bSort': false });", true);
            spn_amount.InnerHtml = Convert.ToString(total_Amount1);
            txt_totalamt.Text = Convert.ToString(total_Amount1);
            spn_rejamount.InnerHtml = Convert.ToString(total_AmountRejected1);
            txt_rejectedamt.Text = Convert.ToString(total_AmountRejected1);
            //}
            //  divIns.Style.Add("display","none");
        }
        divIns.Style.Add("display", "none");
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
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getDocDetails(string req_no, string process_name)
    {
        string DisplayData = string.Empty;
        StringBuilder sb = new StringBuilder();
        DataSet dsData = new DataSet();
        try
        {
            string isData = "";

             dsData = (DataSet)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "getdocdetail", ref isData, req_no, process_name);

            if (process_name == "OTHER EXPENSES")
            {
                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>";
                if (dsData != null)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                        {
                            DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dsData.Tables[0].Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles('" + (req_no) + "'," + (i + 1) + ")>" + Convert.ToString(dsData.Tables[0].Rows[i]["filename"]) + "</a></td></tr>";
                        }
                    }
                }
                DisplayData += "</table>";


            }

            else if (process_name == "TRAVEL EXPENSE")
            {
                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>";
                if (dsData != null)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                        {
                            DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dsData.Tables[0].Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles('" + (req_no) + "'," + (i + 1) + ")>" + Convert.ToString(dsData.Tables[0].Rows[i]["filename"]) + "</a></td></tr>";
                        }
                    }
                }
                DisplayData += "</table>";
            }

            else if (process_name == "LOCAL CONVEYANCE")
            {

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>";
                if (dsData != null)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                        {
                            DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dsData.Tables[0].Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles('" + (req_no) + "'," + (i + 1) + ")>" + Convert.ToString(dsData.Tables[0].Rows[i]["filename"]) + "</a></td></tr>";
                        }
                    }
                }
                DisplayData += "</table>";
            }

            else if (process_name == "MOBILE DATACARD EXPENSE")
            {

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>";
                if (dsData != null)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                        {
                            DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dsData.Tables[0].Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles('" + (req_no) + "'," + (i + 1) + ")>" + Convert.ToString(dsData.Tables[0].Rows[i]["filename"]) + "</a></td></tr>";
                        }
                    }
                }
                DisplayData += "</table>";
            }

            else if (process_name == "ADVANCE REQUEST")
            {

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>";
                if (dsData != null)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                        {
                            DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dsData.Tables[0].Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles('" + (req_no) + "'," + (i + 1) + ")>" + Convert.ToString(dsData.Tables[0].Rows[i]["filename"]) + "</a></td></tr>";
                        }
                    }
                }
                DisplayData += "</table>";
            }

            if (process_name == "CAR POLICY")
            {
                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>";
                if (dsData != null)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                        {
                            DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dsData.Tables[0].Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles('" + (req_no) + "'," + (i + 1) + ")>" + Convert.ToString(dsData.Tables[0].Rows[i]["filename"]) + "</a></td></tr>";
                        }
                    }
                }
                DisplayData += "</table>";

            }
        }
        catch (Exception ex)
        {
            //DisplayData.ToString() = ""; 
        }
        return DisplayData.ToString();

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
        ddlVoucherType.SelectedIndex = 0;
        txt_f_date.Value = "";
        txt_t_date.Value = "";
        spn_amount.InnerHtml = null;
        spn_rejamount.InnerHtml = null;
        txtdata.Text = "";
    }
    protected void btn_Export_onClick(object sender, EventArgs e)
    {

        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            string data = divdata.InnerHtml;
            ExportToExcel(data, "EXPENSE STATUS REPORT");
        }
    }
    protected void ExportToExcel(string dgview, string filename)
    {
        try
        {
            string dtvalue = string.Empty;
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            string isValid = string.Empty;
            if (ddlVoucherType.SelectedValue == "0")
            {
                decimal total_Amount = 0;
                decimal total_AmountRejected = 0;
                int cnt = 0;
                DataSet ds = (DataSet)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "selectall", ref isValid, ddlVoucherType.SelectedValue, txt_f_date.Value, txt_t_date.Value, ddlStatus.SelectedValue);
                str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th> #</th><th>Portal Docket No.</th><th>Voucher Date</th><th>Employee Code</th><th>Employee Name</th><th>Voucher Type</th><th>Amount</th><th>Status</th><th>Pending with</th><th>SAP Document No.</th></tr> ");
                str.Append("</thead><tbody>");
                for (int k = 0; k < 5; k++)
                {
                    if (ds != null && ds.Tables[k].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[k].Rows.Count; i++)
                        {
                            str.Append("<tr>");
                            str.Append("<td>" + (cnt + 1) + "</td>");
                            str.Append("<td>" + Convert.ToString(ds.Tables[k].Rows[i]["REQUEST_NO"]) + "</td>");
                            str.Append("<td>" + ds.Tables[k].Rows[i]["created_date"].ToString() + "</td>");
                            str.Append("<td>" + ds.Tables[k].Rows[i]["EMP_ID"].ToString() + "</td>");
                            str.Append("<td>" + ds.Tables[k].Rows[i]["EMPLOYEE_NAME"].ToString() + "</td>");
                            str.Append("<td>" + ds.Tables[k].Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
                            str.Append("<td align='right'>" + ds.Tables[k].Rows[i]["Amount"].ToString() + "</td>");
                            str.Append("<td>" + ds.Tables[k].Rows[i]["Current_Status"].ToString() + "</td>");
                            if (ds.Tables[k].Rows[i]["Current_Status"].ToString() == "COMPLETED" || ds.Tables[k].Rows[i]["Current_Status"].ToString() == "Completed")
                            {
                                str.Append("<td></td>");
                            }
                            else
                            {
                                // str.Append("<td>" + ds.Tables[k].Rows[i]["PK_TRANSID"].ToString() + "</td>");
                                // str.Append("<td>" + dt.Rows[i]["PK_TRANSID"].ToString() + "</td>");
                                string appname = (string)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "appname", ref isValid, ds.Tables[k].Rows[i]["PK_TRANSID"].ToString());
                                if (appname != null)
                                {
                                    str.Append("<td>" + appname.ToString() + "</td>");
                                }
                            }
                            str.Append("<td>" + ds.Tables[k].Rows[i]["sap_no"].ToString() + "</td>");
			    string deviate1 = (string)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "getdeviate", ref isValid, ds.Tables[k].Rows[i]["REQUEST_NO"].ToString());
                            if (deviate1 != null && deviate1 != "")
                            {
                                str.Append("<td>" + deviate1 + "</td>");
                            }
                            else
                            {
                                str.Append("<td></td>");
                            }
                            if (Convert.ToString(ds.Tables[k].Rows[i]["Creation_Date"]) == "")
                            {
                                str.Append("<td></td>");
                            }
                            else
                            {
                                str.Append("<td>" + Convert.ToDateTime(ds.Tables[k].Rows[i]["Creation_Date"]).ToString("dd-MMM-yyyy") + "</td>");
                            }
                            str.Append("</tr>");
                            cnt = cnt + 1;
                            if (ds.Tables[k].Rows[i]["Current_Status"].ToString() == "Rejected" || ds.Tables[k].Rows[i]["Current_Status"].ToString() == "REJECTED")
                            {
                                total_AmountRejected = total_AmountRejected + Convert.ToDecimal(ds.Tables[k].Rows[i]["Amount"].ToString());
                            }
                            else
                            {
                                total_Amount = total_Amount + Convert.ToDecimal(ds.Tables[k].Rows[i]["Amount"].ToString());
                            }
                        }
                    }

                    else
                    {
                        divdata.InnerHtml = null;
                        // Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
                    }
                }
                str.Append("</tbody></table> ");
                txt_totalamt.Text = Convert.ToString(total_Amount);
                //}
            }
            else
            {
                decimal total_Amount1 = 0;
                decimal total_AmountRejected1 = 0;
                int cnt1 = 0;
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "selectdata", ref isValid, ddlVoucherType.SelectedValue, txt_f_date.Value, txt_t_date.Value, ddlStatus.SelectedValue);
                str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th> #</th><th>Portal Docket No.</th><th>Voucher Date</th><th>Employee Code</th><th>Employee Name</th><th>Voucher Type</th><th>Amount</th><th>Status</th><th>Pending with</th><th>SAP Document No.</th></tr> ");
                if (dt != null && dt.Rows.Count > 0)
                {
                    str.Append("</thead><tbody>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str.Append("<tr>");
                        str.Append("<td>" + (i + 1) + "</td>");
                        str.Append("<td>" + Convert.ToString(dt.Rows[i]["REQUEST_NO"]) + "</td>");
                        //  str.Append("<td>" + dt.Rows[i]["REQUEST_NO"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["created_date"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["EMP_ID"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td>");
                        if (ddlVoucherType.SelectedValue == "1")
                        {
                            str.Append("<td>" + dt.Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
                        }
                        else
                        {
                            str.Append("<td>" + ddlVoucherType.SelectedItem.Text + "</td>");
                        }
                        str.Append("<td align='right'>" + dt.Rows[i]["Amount"].ToString() + "</td>");
                        str.Append("<td>" + dt.Rows[i]["Current_Status"].ToString() + "</td>");
                        if (dt.Rows[i]["Current_Status"].ToString() == "COMPLETED" || dt.Rows[i]["Current_Status"].ToString() == "Completed")
                        {
                            str.Append("<td></td>");
                        }
                        else
                        {
                            // str.Append("<td>" + dt.Rows[i]["PK_TRANSID"].ToString() + "</td>");
                            string appname = (string)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "appname", ref isValid, dt.Rows[i]["PK_TRANSID"].ToString());
                            if (appname != null)
                            {
                                str.Append("<td>" + appname.ToString() + "</td>");
                            }
                        }
                        str.Append("<td>" + dt.Rows[i]["sap_no"].ToString() + "</td>");
			string deviate1 = (string)ActionController.ExecuteAction("", "Expense_Status_Report.aspx", "getdeviate", ref isValid, dt.Rows[i]["REQUEST_NO"].ToString());
                            if (deviate1 != null && deviate1 != "")
                            {
                                str.Append("<td>" + deviate1 + "</td>");
                            }
                            else
                            {
                                str.Append("<td></td>");
                            }
                            if (Convert.ToString(dt.Rows[i]["Creation_Date"]) == "")
                            {
                                str.Append("<td></td>");
                            }
                            else
                            {
                                str.Append("<td>" + Convert.ToDateTime(dt.Rows[i]["Creation_Date"]).ToString("dd-MMM-yyyy") + "</td>");
                            }
                        str.Append("</tr>");
                        cnt1 = cnt1 + 1;
                        if (dt.Rows[i]["Current_Status"].ToString() == "Rejected" || dt.Rows[i]["Current_Status"].ToString() == "REJECTED")
                        {
                            total_AmountRejected1 = total_AmountRejected1 + Convert.ToDecimal(dt.Rows[i]["Amount"].ToString());
                        }
                        else
                        {
                            total_Amount1 = total_Amount1 + Convert.ToDecimal(dt.Rows[i]["Amount"].ToString());
                        }
                    }
                }

                else
                {
                    divdata.InnerHtml = null;
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
                }
                str.Append("</tbody></table> ");
                txt_totalamt.Text = Convert.ToString(total_Amount1);
                //}
            }
            dtvalue = str.ToString();
            if (dtvalue != "")
            {
                Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                    "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
                Response.Write("<tr><td><b>Total Amount : " + txt_totalamt.Text + "</b></td></tr>");
                Response.Write("<tr><td><b> & Rejected Amount : " + txt_rejectedamt.Text + "</b></td></tr>");
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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string check_avail(string req_no)
    {
        string isdata = "";
        string ret = (string)ActionController.ExecuteAction("","Request_Status_Report.aspx","checkavail",ref isdata,req_no);
            return ret;
    }

}