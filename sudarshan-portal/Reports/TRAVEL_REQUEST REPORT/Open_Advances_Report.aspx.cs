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

public partial class Open_Advances_Report : System.Web.UI.Page
{
    //ListItem Li = new ListItem("--Select One--", "0");
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Open_Advances_Report));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        FillAdvanceFor();
                        getAdvanceData();
                    }
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    private void FillAdvanceFor()
    {
        String IsData = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Open_Advances_Report.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdAdvanceType");
        if (dt != null && dt.Rows.Count > 0)
        {
            ListItem Li = new ListItem("--Select All--", "0");
            ddlAdvType.DataSource = dt;
            ddlAdvType.DataTextField = "ADVANCE_TYPE_NAME";
            ddlAdvType.DataValueField = "PK_ADVANCEID";
            ddlAdvType.DataBind();
            ddlAdvType.Items.Insert(0, Li);
        }
    }
    private void getAdvanceData()
    {
       string isValid = string.Empty;
       if (ddlAdvType.SelectedValue == "0")
       {
           DataSet dt = (DataSet)ActionController.ExecuteAction("", "Open_Advances_Report.aspx", "selectall", ref isValid, ddlAdvType.SelectedValue);
           str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th> #</th><th>Portal Docket No.</th><th>Voucher Date</th><th>Employee Code</th><th>Employee Name</th><th>Advance Type</th><th>Approved Date</th><th>Amount</th><th>Remark</th><th>Approved by(Reporting Manager/Deviation)</th><th>Mode of Payment</th><th>Cash Location</th><th>Status</th></tr> ");
           str.Append("</thead><tbody>");
           int cnt = 0;
           decimal total_Amount = 0;
           for (int k = 0; k < 2; k++)
           {
               if (dt != null && dt.Tables[k].Rows.Count > 0)
               {
                   for (int i = 0; i < dt.Tables[k].Rows.Count; i++)
                   {
                       str.Append("<tr>");
                       str.Append("<td>" + (cnt+1) + "</td>");
                       str.Append("<td>" + dt.Tables[k].Rows[i]["REQUEST_NO"].ToString() + "</td>");
                       str.Append("<td>" + dt.Tables[k].Rows[i]["created_date"].ToString() + "</td>");
                       str.Append("<td>" + dt.Tables[k].Rows[i]["EMP_ID"].ToString() + "</td>");
                       str.Append("<td>" + dt.Tables[k].Rows[i]["EMPLOYEE_NAME"].ToString() + "</td>");
                       str.Append("<td>" + dt.Tables[k].Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
                       str.Append("<td>" + dt.Tables[k].Rows[i]["approved_date"].ToString() + "</td>");
                       str.Append("<td align='right'>" + dt.Tables[k].Rows[i]["billamount"].ToString() + "</td>");
                       str.Append("<td>" + dt.Tables[k].Rows[i]["REMARK"].ToString() + "</td>");
                       string appname = (string)ActionController.ExecuteAction("", "Open_Advances_Report.aspx", "appname", ref isValid, dt.Tables[k].Rows[i]["REQUEST_NO"].ToString());
                       if (appname != null)
                       {
                           str.Append("<td>" + appname.ToString() + "</td>");
                       }
                       //if (dt.Tables[k].Rows[i]["REQUEST_N"].ToString() == "")
                       //{
                       str.Append("<td>" + dt.Tables[k].Rows[i]["PAYMENT_MODE"].ToString() + "</td>");
                       str.Append("<td>" + dt.Tables[k].Rows[i]["location"].ToString() + "</td>");
                           str.Append("<td>Completed</td>");
                         
                          
                       //}
                       //else
                       //{
                       //    str.Append("<td>Pending</td>");
                       //    string apppending = (string)ActionController.ExecuteAction("", "Open_Advances_Report.aspx", "pendingname", ref isValid, dt.Tables[k].Rows[i]["REQUEST_N"].ToString());
                       //    if (apppending != null)
                       //    {
                       //        str.Append("<td>" + apppending.ToString() + "</td>");
                       //    }
                       //    str.Append("<td>" + dt.Tables[k].Rows[i]["REQUEST_N"].ToString() + "</td>");
                          
                       //}
                       str.Append("</tr>");
                       cnt = cnt + 1;
                       total_Amount = total_Amount + Convert.ToDecimal(dt.Tables[k].Rows[i]["billamount"].ToString());
                   }
               }

               else
               {
                   divdata.InnerHtml = null;
                   // Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
               }
           }
           str.Append("</tbody></table> ");
           divdata.InnerHtml = str.ToString();
           ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({ 'bSort': false });", true);
           spn_amount.InnerHtml = Convert.ToString(total_Amount);
           txt_totalamt.Text = Convert.ToString(total_Amount);
       }
       else
       {
           int cnt1 = 0;
           decimal total_Amount = 0;
           DataSet dt = (DataSet)ActionController.ExecuteAction("", "Open_Advances_Report.aspx", "selectdata", ref isValid, ddlAdvType.SelectedValue);
           str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th> #</th><th>Portal Docket No.</th><th>Voucher Date</th><th>Employee Code</th><th>Employee Name</th><th>Advance Type</th><th>Approved Date</th><th>Amount</th><th>Remark</th><th>Approved by(Reporting Manager/Deviation)</th><th>Mode of Payment</th><th>Cash Location</th><th>Status</th></tr> ");
           if (dt.Tables[0].Rows.Count > 0 && dt.Tables[0] != null)
           {
               str.Append("</thead><tbody>");
               for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
               {
                   str.Append("<tr>");
                   str.Append("<td>" + (cnt1 + 1) + "</td>");
                   str.Append("<td>" + dt.Tables[0].Rows[i]["REQUEST_NO"].ToString() + "</td>");
                   str.Append("<td>" + dt.Tables[0].Rows[i]["created_date"].ToString() + "</td>");
                   str.Append("<td>" + dt.Tables[0].Rows[i]["EMP_ID"].ToString() + "</td>");
                   str.Append("<td>" + dt.Tables[0].Rows[i]["EMPLOYEE_NAME"].ToString() + "</td>");
                   str.Append("<td>" + dt.Tables[0].Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
                   str.Append("<td>" + dt.Tables[0].Rows[i]["approved_date"].ToString() + "</td>");
                   str.Append("<td align='right'>" + dt.Tables[0].Rows[i]["billamount"].ToString() + "</td>");
                   str.Append("<td>" + dt.Tables[0].Rows[i]["REMARK"].ToString() + "</td>");
                   string appname = (string)ActionController.ExecuteAction("", "Open_Advances_Report.aspx", "appname", ref isValid, dt.Tables[0].Rows[i]["REQUEST_NO"].ToString());
                   if (appname != null)
                   {
                       str.Append("<td>" + appname.ToString() + "</td>");
                   }
                   str.Append("<td>" + dt.Tables[0].Rows[i]["PAYMENT_MODE"].ToString() + "</td>");
                   str.Append("<td>" + dt.Tables[0].Rows[i]["location"].ToString() + "</td>");
                   str.Append("<td>Completed</td>");
                   str.Append("</tr>");
                   cnt1 = cnt1 + 1;
                   total_Amount = total_Amount + Convert.ToDecimal(dt.Tables[0].Rows[i]["billamount"].ToString());
               }
           }
           //if (dt.Tables[1].Rows.Count > 0 && dt.Tables[1] != null)
           //{               
           //   // decimal total_Amount1 = 0;
           //    for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
           //    {
           //        str.Append("<tr>");
           //        str.Append("<td>" + (cnt1+1) + "</td>");
           //        str.Append("<td>" + dt.Tables[1].Rows[i]["REQUEST_NO"].ToString() + "</td>");
           //        str.Append("<td>" + dt.Tables[1].Rows[i]["created_date"].ToString() + "</td>");
           //        str.Append("<td>" + dt.Tables[1].Rows[i]["EMP_ID"].ToString() + "</td>");
           //        str.Append("<td>" + dt.Tables[1].Rows[i]["EMPLOYEE_NAME"].ToString() + "</td>");
           //        //str.Append("<td>" + ddlAdvType.SelectedItem.Text + "</td>");
           //        if (dt.Tables[1].Rows[i]["EXPENSE_HEAD"].ToString() == "")
           //        {
           //            str.Append("<td> </td>");
           //        }
           //        else
           //        {
           //            str.Append("<td>" + dt.Tables[1].Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");

           //        }
                  
           //        str.Append("<td>" + dt.Tables[1].Rows[i]["approved_date"].ToString() + "</td>");
           //        str.Append("<td align='right'>" + dt.Tables[1].Rows[i]["billamount"].ToString() + "</td>");
           //        str.Append("<td>" + dt.Tables[1].Rows[i]["REMARK"].ToString() + "</td>");
           //        string appname1 = (string)ActionController.ExecuteAction("", "Open_Advances_Report.aspx", "appname", ref isValid, dt.Tables[1].Rows[i]["REQUEST_NO"].ToString());
           //        if (appname1 != null)
           //        {
           //            str.Append("<td>" + appname1.ToString() + "</td>");
           //        }
           //        str.Append("<td>Pending</td>");
           //        string apppending = (string)ActionController.ExecuteAction("", "Open_Advances_Report.aspx", "pendingname", ref isValid, dt.Tables[1].Rows[i]["REQUEST_N"].ToString());
           //        if (apppending != null)
           //        {
           //            str.Append("<td>" + apppending.ToString() + "</td>");
           //        }
           //        str.Append("<td>" + dt.Tables[1].Rows[i]["REQUEST_N"].ToString() + "</td>");
           //        str.Append("</tr>");
           //        total_Amount = total_Amount + Convert.ToDecimal(dt.Tables[1].Rows[i]["billamount"].ToString());
           //        cnt1 = cnt1 + 1;
           //    }

           //}
           else
           {
               divdata.InnerHtml = null;
               Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
           }
           str.Append("</tbody></table> ");
           divdata.InnerHtml = str.ToString();
           ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({ 'bSort': false });", true);
           spn_amount.InnerHtml = Convert.ToString(total_Amount);
           txt_totalamt.Text = Convert.ToString(total_Amount);
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

    protected void btn_Export_onClick(object sender, EventArgs e)
    {

            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                string data = divdata.InnerHtml; 
                ExportToExcel(data, "Open Advances Report");
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
                Response.Write("<tr><td><b>Total Amount : "+ txt_totalamt.Text +"</b></td></tr>");
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

    protected void ddlAdvType_SelectedIndexChanged(object sender, EventArgs e)
    {
        getAdvanceData();
    }

}