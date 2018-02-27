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

public partial class Unapproved_Funds_Report : System.Web.UI.Page
{
    string IsData = string.Empty;
    StringBuilder str = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            app_Path.Text = HttpContext.Current.Request.ApplicationPath;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Unapproved_Funds_Report));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    txtCreatedBy.Text = ((string)Session["User_ADID"]);
                    FillVoucher();
                   // div_payment.Visible = true;
                    FillMode();
                    FillLocation();
                    getVoucherAllData();
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    private void FillMode()
    {
        String IsData = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Unapproved_Funds_Report.aspx", "selectdetails", ref IsData, "", "AdPaymentMode");
        if (dt != null && dt.Rows.Count > 0)
        {
            ListItem Li1 = new ListItem("--Select All--", "0");
            ddlPayMode.DataSource = dt;
            ddlPayMode.DataTextField = "PAYMENT_MODE";
            ddlPayMode.DataValueField = "PK_PAYMENT_MODE";
            ddlPayMode.DataBind();
            ddlPayMode.Items.Insert(0, Li1);
        }
    }
    private void FillLocation()
    {
        String IsData = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Unapproved_Funds_Report.aspx", "selectdetails", ref IsData, "", "AdLocation");
        if (dt != null && dt.Rows.Count > 0)
        {
            ListItem Li1 = new ListItem("--Select One--", "0");
            ddlLocation.DataSource = dt;
            ddlLocation.DataTextField = "LOCATION_NAME";
            ddlLocation.DataValueField = "PK_LOCATION_ID";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, Li1);
        }

    }
    private void FillVoucher()
    {
        String IsData = string.Empty;
        //ListItem Li = new ListItem("--Select One--", "0");
        ddlVoucherType.Items.Insert(0, new ListItem("--Select All--", "0"));
        ddlVoucherType.Items.Insert(1, new ListItem("Advance Request", "1"));
        ddlVoucherType.Items.Insert(2, new ListItem("Mobile and Data Card Expense", "2"));
        ddlVoucherType.Items.Insert(3, new ListItem("Car Expenses", "3"));
        ddlVoucherType.Items.Insert(3, new ListItem("Other Expenses", "4"));
        ddlVoucherType.Items.Insert(3, new ListItem("Domestic Travel Expenses", "5"));
        ddlVoucherType.Items.Insert(3, new ListItem("Local Conveyance Reimbursement", "6"));

    }
    private void getVoucherAllData()
    {
        double adv; int cnt = 0;
        txt_paylamt.Text = "";
        txt_recamt.Text = "";
        string isValid = string.Empty;
        spn_payable.InnerHtml = "0";
       // int cnt = 0;
        spn_recovery.InnerHtml = "0";
        if (ddlPayMode.SelectedValue == "0")
        {
            ddlPayMode.SelectedValue = "0";
            ddlLocation.SelectedValue = "0";
        }
        DataSet ds = (DataSet)ActionController.ExecuteAction("", "Unapproved_Funds_Report.aspx", "selectalldata", ref isValid,ddlPayMode.SelectedValue,ddlLocation.SelectedValue);
        string strval = string.Empty; string strval1 = string.Empty;
        str.Append("<table id='data-table2' class='table table-bordered table-hover'> <thead><tr class='grey'><th> #</th><th>Voucher Type</th><th>Voucher Date</th><th>Portal Docket No.</th><th>Employee Code</th><th>Employee Name</th><th>Payable Amount</th><th>Recovery Amount</th><th>Mode of Payment</th><th>Cash Location</th></tr> ");
        str.Append("</thead><tbody>");
        for (int k = 0; k < 6; k++)
        {
            if (ds != null && ds.Tables[k].Rows.Count > 0)
            {
                if (ds.Tables[k].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[k].Rows.Count; i++)
                    {
                        str.Append("<tr>");
                        str.Append("<td>" + (cnt + 1) + "</td>");
                        str.Append("<td>" + ds.Tables[k].Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
                        str.Append("<td>" + ds.Tables[k].Rows[i]["created_date"].ToString() + "</td>");
                        str.Append("<td>" + ds.Tables[k].Rows[i]["REQUEST_NO"].ToString() + "</td>");
                        str.Append("<td>" + ds.Tables[k].Rows[i]["EMP_ID"].ToString() + "</td>");
                        str.Append("<td>" + ds.Tables[k].Rows[i]["EMPLOYEE_NAME"].ToString() + "</td>");

                        if (ds.Tables[k].Rows[i]["EXPENSE_HEAD"].ToString() == "Advance Request" || ds.Tables[k].Rows[i]["EXPENSE_HEAD"].ToString() == "Mobile" || ds.Tables[k].Rows[i]["EXPENSE_HEAD"].ToString() == "DataCard" || ds.Tables[k].Rows[i]["EXPENSE_HEAD"].ToString() == "Car Expenses")
                        {
                            adv = 0.00;
                        }
                        else
                        {
                            adv = Convert.ToDouble(ds.Tables[k].Rows[i]["AMOUNT"]);
                        }
                        double exp = Convert.ToDouble(ds.Tables[k].Rows[i]["billamount"].ToString());
                        if (exp > adv)
                        {
                            strval = Convert.ToString(exp - adv);
                            str.Append("<td align='right'>" + strval + "</td>");
                            strval1 = "0";
                            str.Append("<td align='right'>0.00</td>");
                        }
                        else
                        {   //spn_hdr.InnerHtml = "Recovery Amount : ";
                            str.Append("<td align='right'>0.00</td>");
                            strval1 = Convert.ToString(adv - exp);
                            strval = "0";
                            str.Append("<td align='right'>" + strval1 + "</td>");
                        }
                           str.Append("<td>" + ds.Tables[k].Rows[i]["PAYMENT_MODE"].ToString() + "</td>");
                           str.Append("<td>" + ds.Tables[k].Rows[i]["location"].ToString() + "</td>");
                      
                        spn_payable.InnerHtml = Convert.ToString(Convert.ToDouble(strval) + Convert.ToDouble(spn_payable.InnerHtml));
                        spn_recovery.InnerHtml = Convert.ToString(Convert.ToDouble(strval1) + Convert.ToDouble(spn_recovery.InnerHtml));
                        txt_paylamt.Text = Convert.ToString(spn_payable.InnerHtml);
                        txt_recamt.Text = Convert.ToString(spn_recovery.InnerHtml);
                        str.Append("</tr>");
                        cnt = cnt + 1;
                    }
                }
                else
                {
                    divdata.InnerHtml = null;
                    divIns.Style.Add("display", "none");
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
                }
            }
        }
        str.Append("</tbody></table> ");
        divdata.InnerHtml = str.ToString();
        //txtdata.Text = str.ToString();
        ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table2').dataTable({ 'bSort': false });", true);
        divIns.Style.Add("display", "none");
    }
    private void getVoucherData()
    {
        string isValid = string.Empty;
        txt_paylamt.Text = "";
        txt_recamt.Text = "";
        double adv;
        int cnt1 = 0;
        spn_payable.InnerHtml = "0";
        spn_recovery.InnerHtml = "0";
        if (ddlPayMode.SelectedValue == "0")
        {
            ddlPayMode.SelectedValue = "0";
            ddlLocation.SelectedValue = "0";
        }
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Unapproved_Funds_Report.aspx", "selectdata", ref isValid, ddlVoucherType.SelectedValue,ddlPayMode.SelectedValue,ddlLocation.SelectedValue);
        string strval = string.Empty; string strval1 = string.Empty;
        if (dt.Rows.Count > 0)
        {
            str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th> #</th><th>Voucher Type</th><th>Voucher Date</th><th>Portal Docket No.</th><th>Employee Code</th><th>Employee Name</th><th>Payable Amount</th><th>Recovery Amount</th><th>Mode of Payment</th><th>Cash Location</th></tr> ");
            str.Append("</thead><tbody>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str.Append("<tr>");
                str.Append("<td>" + (cnt1 + 1) + "</td>");
                if (ddlVoucherType.SelectedValue != "2")
                {
                    str.Append("<td>" + ddlVoucherType.SelectedItem.Text + "</td>");
                }
                else
                {
                    str.Append("<td>" + dt.Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
                }
                str.Append("<td>" + dt.Rows[i]["created_date"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["REQUEST_NO"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["EMP_ID"].ToString() + "</td>");
                str.Append("<td>" + dt.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td>");
                if (ddlVoucherType.SelectedValue == "1" || ddlVoucherType.SelectedValue == "2" || ddlVoucherType.SelectedValue == "3")
                {
                    adv = 0.00;
                }
                else
                {
                    adv = Convert.ToDouble(dt.Rows[i]["AMOUNT"]);
                }
                double exp = Convert.ToDouble(dt.Rows[i]["billamount"].ToString());
                if (exp > adv)
                {
                    strval = Convert.ToString(exp - adv);
                    str.Append("<td align='right'>" + strval + "</td>");
                    str.Append("<td align='right'>0.00</td>");
                    strval1 = "0";
                }
                else
                {   //spn_hdr.InnerHtml = "Recovery Amount : ";
                    str.Append("<td align='right'>0.00</td>");
                    strval1 = Convert.ToString(adv - exp);
                    strval = "0";
                    str.Append("<td align='right'>" + strval1 + "</td>");
                }
                   str.Append("<td>" + dt.Rows[i]["PAYMENT_MODE"].ToString() + "</td>");
                   str.Append("<td>" + dt.Rows[i]["location"].ToString() + "</td>");
              
                spn_payable.InnerHtml = Convert.ToString(Convert.ToDouble(strval) + Convert.ToDouble(spn_payable.InnerHtml));
                spn_recovery.InnerHtml = Convert.ToString(Convert.ToDouble(strval1) + Convert.ToDouble(spn_recovery.InnerHtml));
                str.Append("</tr>");
                txt_paylamt.Text = Convert.ToString(spn_payable.InnerHtml);
                txt_recamt.Text = Convert.ToString(spn_recovery.InnerHtml);
                cnt1 = cnt1 + 1;
            }
            str.Append("</tbody></table> ");
            divdata.InnerHtml = str.ToString();
           // txtdata.Text = str.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({ 'bSort': false });", true);
            divIns.Style.Add("display", "none");
        }
        else
        {
            divdata.InnerHtml = null;
            divIns.Style.Add("display", "none");
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
        }
    }

    protected void btn_Export_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            string data = divdata.InnerHtml;
            ExportToExcel(data, "Unapproved Funds Report");
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
    protected void ExportToExcel(string dgview, string filename)
    {
        try
        {
            string dt = string.Empty;
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            dt = dgview.ToString();
            if (dt != "")
            {
                Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                                    "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
                Response.Write("<tr><td><b>Payable Amount : " + txt_paylamt.Text + "</b></td><td>  &  </td></tr>");
                Response.Write("<tr><td><b>Recovery Amount : " + txt_recamt.Text + "</b></td></tr>");
                Response.Write(dt);
            }
            Response.End();
        }

        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
            return;
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        divdata.InnerHtml = null;
        ddlVoucherType.SelectedIndex = 0;
        ddlPayMode.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        spn_payable.InnerHtml = null;
        spn_recovery.InnerHtml = null;
        txtdata.Text = "";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlVoucherType.SelectedValue == "0")
        {
           if (ddlPayMode.SelectedValue == "2")
           {
 		       ddlLocation.SelectedIndex = 0;
               div_payment.Style.Add("display","none");
            }
           else
           {
             div_payment.Style.Add("display","block");
         }
		
             getVoucherAllData();
        }
        else
        {
           if (ddlPayMode.SelectedValue == "2")
           {
 		       ddlLocation.SelectedIndex = 0;
               div_payment.Style.Add("display","none");
            }
           else
           {
               div_payment.Style.Add("display","block");
           }
		
            getVoucherData();
        }
    }

   
}