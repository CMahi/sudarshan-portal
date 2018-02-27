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

public partial class Car_Data_For_HR : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Car_Data_For_HR));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = txt_Username.Text = Convert.ToString(Session["USER_ADID"]);


                    }
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StringBuilder html_Header = new StringBuilder();
        string isValid = string.Empty;
        decimal total_Amount = 0;
        decimal total_AmountRejected = 0;
        string isdata = string.Empty;
        html_Header.Append("<table class='table table-bordered' id='tblfuel' width='100%'>");
        html_Header.Append("<thead><tr class='grey'><th>Sr No</th><th style='text-align:center'>Portal Docket No</th><th style='display:none'></th><th style='display:none'></th><th style='text-align:center'>Voucher Date</th><th style='text-align:center'>Vehicle Number</th><th style='text-align:center'>Vehicle Purchase Date</th><th style='text-align:center'>Vehicle Age(Years)</th><th style='text-align:center'>Employee code</th><th style='text-align:center'>Employee Name</th><th style='text-align:center'>Expense Head</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Fuel in litters</th><th style='text-align:center'>Status</th><th style='text-align:center'>Pending with</th><th style='text-align:center'>SAP Document No</th></tr></thead>");
        html_Header.Append("<tbody>");
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Car_Data_For_HR.aspx", "getpkcarexpnshdr", ref isdata, ddl_Exp_Head.SelectedValue, txt_f_date.Value, txt_t_date.Value, ddlStatus.SelectedValue);
        if (dt.Rows.Count > 0 || dt!=null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html_Header.Append("<tr><td>" + (i + 1) + "</td>");
                html_Header.Append("<td><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "' onclick='getRequestDetails(" + (i + 1) + ")'>" + Convert.ToString(dt.Rows[i]["CAR_EXPENSE_NO"]) + "</a><input type='text' id='pname" + (i + 1) + "' Value='" + Convert.ToString(dt.Rows[i]["PROCESS_NAME"]) + "' style='display:none'/><input type='text' id='h_info" + (i + 1) + "' Value=" + Convert.ToString(dt.Rows[i]["CAR_EXPENSE_NO"]) + " style='display:none'></input></td>");
                  
               // html_Header.Append("<td class='reqno'><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "'>" + Convert.ToString(dt.Rows[i]["CAR_EXPENSE_NO"]) + "</a></td>");
                html_Header.Append("<td style='display:none'>" + dt.Rows[i]["CAR_EXPENSE_NO"].ToString() + "</td>");
                html_Header.Append("<td style='display:none'>" + dt.Rows[i]["PROCESS_NAME"].ToString() + "</td>");
                      
                html_Header.Append("<td style='text-align:center'>" + dt.Rows[i]["c_date"].ToString() + "</td><td style='text-align:center'>" + dt.Rows[i]["Vehical_No"] + "</td><td style='text-align:center'>" + dt.Rows[i]["car_pur_date"] + "</td><td style='text-align:center'>" + dt.Rows[i]["Car_Age"] + "</td><td style='text-align:center'>" + dt.Rows[i]["EMP_ID"] + "</td><td style='text-align:center'>" + dt.Rows[i]["EMPLOYEE_NAME"] + "</td><td style='text-align:center'>" + dt.Rows[i]["Flag"] + "</td><td style='text-align:right'>" + dt.Rows[i]["EXPENSE_AMOUNT"] + "</td><td>" + dt.Rows[i]["FUEL_LITRE"] + "</td><td>" + dt.Rows[i]["STATUS"] + "</td>");
                if (dt.Rows[i]["STATUS"].ToString() == "Rejected" || dt.Rows[i]["STATUS"].ToString() == "REJECTED")
                {
                    total_AmountRejected = total_AmountRejected + Convert.ToDecimal(dt.Rows[i]["EXPENSE_AMOUNT"].ToString());
                    total_Amount = total_Amount + Convert.ToDecimal(dt.Rows[i]["EXPENSE_AMOUNT"].ToString());
                }
                else
                {
                    total_Amount = total_Amount + Convert.ToDecimal(dt.Rows[i]["EXPENSE_AMOUNT"].ToString());
                }
                string apppending = (string)ActionController.ExecuteAction("", "Car_Data_For_HR.aspx", "pendingname", ref isValid, dt.Rows[i]["CAR_EXPENSE_NO"].ToString());
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
        txt_f_date.Value = "";
        txt_t_date.Value = "";
        spn_amount.InnerHtml = null;
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
            ExportToExcel(data, "YEAR END PROCESSING BY HR REPORT");
        }
    }
    protected void ExportToExcel(string dgview, string filename)
    {
        try
        {
            string isdata= string.Empty;
 	string isValid= string.Empty;

            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
          //  dtvalue = dgview.ToString();
     //       if (dtvalue != "")
      //      {
                Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                    "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
                Response.Write("<tr><td><b>Total Amount : " + txt_totalamt.Text + "</b></td></tr>");
                Response.Write("<tr><td><b> & Rejected Amount : " + txt_rejectedamt.Text + "</b></td></tr>");
                //Response.Write(dtvalue);
           
//
        Response.Write("<table class='table table-bordered' id='tblfuel' width='100%'>");
        Response.Write("<thead><tr class='grey'><th>Sr No</th><th style='text-align:center'>Portal Docket No</th><th style='text-align:center'>Voucher Date</th><th style='text-align:center'>Vehicle Number</th><th style='text-align:center'>Vehicle Purchase Date</th><th style='text-align:center'>Vehicle Age(Years)</th><th style='text-align:center'>Employee code</th><th style='text-align:center'>Employee Name</th><th style='text-align:center'>Expense Head</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Fuel in litters</th><th style='text-align:center'>Status</th><th style='text-align:center'>Pending with</th><th style='text-align:center'>SAP Document No</th></tr></thead>");
        Response.Write("<tbody>");
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Car_Data_For_HR.aspx", "getpkcarexpnshdr", ref isdata, ddl_Exp_Head.SelectedValue, txt_f_date.Value, txt_t_date.Value, ddlStatus.SelectedValue);
        if (dt.Rows.Count > 0 || dt!=null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               Response.Write("<tr><td>" + (i + 1) + "</td>");
                Response.Write("<td>" + Convert.ToString(dt.Rows[i]["CAR_EXPENSE_NO"]) + "</td>");                 
                      
                Response.Write("<td style='text-align:center'>" + dt.Rows[i]["c_date"].ToString() + "</td><td style='text-align:center'>" + dt.Rows[i]["Vehical_No"] + "</td><td style='text-align:center'>" + dt.Rows[i]["car_pur_date"] + "</td><td style='text-align:center'>" + dt.Rows[i]["Car_Age"] + "</td><td style='text-align:center'>" + dt.Rows[i]["EMP_ID"] + "</td><td style='text-align:center'>" + dt.Rows[i]["EMPLOYEE_NAME"] + "</td><td style='text-align:center'>" + dt.Rows[i]["Flag"] + "</td><td style='text-align:right'>" + dt.Rows[i]["EXPENSE_AMOUNT"] + "</td><td>" + dt.Rows[i]["FUEL_LITRE"] + "</td><td>" + dt.Rows[i]["STATUS"] + "</td>");
                 
                string apppending = (string)ActionController.ExecuteAction("", "Car_Data_For_HR.aspx", "pendingname", ref isValid, dt.Rows[i]["CAR_EXPENSE_NO"].ToString());
                if (apppending != null || apppending != "")
                {
                    Response.Write("<td>" + apppending.ToString() + "</td>");
                }
                else
                {
                    Response.Write("<td></td>");
                }
                Response.Write("<td>" + dt.Rows[i]["sap_no"].ToString() + "</td></tr>");
            }
            Response.Write("</tbody></table>");
}

///

            Response.End();
        }

        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
            return;
        }
    }


}