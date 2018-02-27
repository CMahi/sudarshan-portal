using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSL.Controller;
using AjaxPro;
using WFE;
using InfoSoftGlobal;
using FSL.Logging;
using System.Data;
using System.Collections;
using System.IO;
using FSL.Message;
using System.Text;
using System.Configuration;
using System.Net;
using System.Runtime.InteropServices;
using System.Web.Services;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

public partial class Po_List_Status_Report : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    string IsData = string.Empty;
    StringBuilder txtTablesNames = new StringBuilder();
    double[] BudgetSavings=new double[13];
    double[] ActualSavings=new double[13];
    StringBuilder str = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            app_Path.Text = HttpContext.Current.Request.ApplicationPath;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Po_List_Status_Report));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    txtCreatedBy.Text = ((string)Session["User_ADID"]);
                    FillType();
                    //getPoDataOpen();
                }               
            }           
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    private void FillType()
    {
        //ddlType.Items.Insert(0, new ListItem("--Select One--", "0"));
        ddlType.Items.Insert(0, new ListItem("Open", "Open"));
        ddlType.Items.Insert(1, new ListItem("Close", "Close"));
    }
  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
          
                if (ddlType.SelectedItem.Text == "Close")
                {
                    string isValid = string.Empty;
                    string dbcumm = string.Empty;
                    Double balamount;
                    string strbal = string.Empty;
                    string IsValid1 = string.Empty;
                    DataTable dt = (DataTable)ActionController.ExecuteAction("", "Po_List_Status_Report.aspx", "selectpodata", ref isValid, txtCreatedBy.Text, ddlType.SelectedValue, txt_f_date.Value, txt_t_date.Value);

                    if (dt.Rows.Count > 0)
                    {
                        str.Append("<table id='data-table1' runat='server'  class='table table-bordered table-hover'> <thead><tr class='grey'><th>PO No</th><th>PO Date</th><th>PO Net Value</th><th>PO GV</th>	<th>Cumulative Inv Amt</th><th>Balance Inv Amt</th><th>Dispatch Quantity</th></tr> ");
                        str.Append("</thead><tbody>");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string encrypt_Str = crypt.Encryptdata(dt.Rows[i]["PO_NUMBER"].ToString());
                            str.Append("<tr>");
                            str.Append("<td><a href='#' role='button' id='anc1' data-toggle='modal' onclick='viewData(" + (i + 1) + ")' >" + dt.Rows[i]["PO_NUMBER"].ToString() + "</a><input type='text' id='encrypt_po_" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                            str.Append("<td>" + dt.Rows[i]["PO_Date"].ToString() + "</td>");
                            str.Append("<td style='text-align: right'>" + dt.Rows[i]["PO_VALUE"].ToString() + "</td>");
                            str.Append("<td style='text-align: right'>" + dt.Rows[i]["PO_GV"].ToString() + "</td>");
                            txt_PONO.Text = dt.Rows[i]["PO_NUMBER"].ToString();

                            DataSet ds = (DataSet)ActionController.ExecuteAction("", "Po_List_Status_Report.aspx", "invoiceamount", ref IsValid1, txt_PONO.Text);
                            DataTable dt1 = ds.Tables[0];
                            DataTable dt2 = ds.Tables[1];

                            if (dt1 != null && dt1.Rows.Count > 0)
                            {
                                if (dt1.Rows[0]["CumulativeAmount"].ToString() == "")
                                {
                                    str.Append("<td  style='text-align: right'>" + 0.00 + "</td>");
                                    dbcumm = dt1.Rows[0]["CumulativeAmount"].ToString();
                                    dbcumm = "0.00";
                                }
                                else
                                {
                                    str.Append("<td  style='text-align: right' >" + dt1.Rows[0]["CumulativeAmount"].ToString() + "</td>");
                                    dbcumm = dt1.Rows[0]["CumulativeAmount"].ToString();
                                }
                            }
                            else
                            {
                                str.Append("<td  style='text-align: right' >" + 0.00 + "</td>");

                            }
                            if (dt.Rows[i]["PO_GV"].ToString() == "0.00" && dbcumm == "0.00")
                            {
                                str.Append("<td style='text-align: right'>" + 0.00 + "</td>");

                            }
                            else
                            {
                                balamount = Convert.ToDouble(dt.Rows[i]["PO_GV"].ToString()) - Convert.ToDouble(dbcumm);
                                str.Append("<td style='text-align: right'>" + balamount.ToString("0.00") + " </td>");

                                strbal = Convert.ToString(balamount);

                            }
                            if (dt2 != null && dt2.Rows.Count > 0)
                            {
                                if (dt2.Rows[0]["qut"].ToString() == "")
                                {
                                    str.Append("<td  style='text-align: right'>" + 0.00 + "</td>");
                                }
                                else
                                {
                                    str.Append("<td  style='text-align: right'>" + dt2.Rows[0]["qut"].ToString() + "</td>");
                                }
                                //if (dt2.Rows[0]["grqut"].ToString() == "")
                                //{
                                //    str.Append("<td  style='text-align: right'>" + 0.00 + "</td>");
                                //}
                                //else
                                //{
                                //    str.Append("<td  style='text-align: right'>" + dt2.Rows[0]["grqut"].ToString() + "</td>");
                                //}


                            }
                            //str.Append("<td>" + dt.Rows[i]["PO_Status"].ToString() + "</td>");
                            str.Append("</tr>");
                        }
                        str.Append("</tbody></table> ");
                       
                    }
                    else
                    {
                        div_po.InnerHtml = null;
                         Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
                    }
                   
                }
                else
                {
                    div_po.InnerHtml = "";
                    string isValid = string.Empty;
                    string dbcumm = string.Empty;
                    Double balamount;
                    string strbal = string.Empty;
                    string IsValid1 = string.Empty;
                    DataTable dt = (DataTable)ActionController.ExecuteAction("", "Po_List_Status_Report.aspx", "selectpodata", ref isValid, txtCreatedBy.Text, "Open", txt_f_date.Value, txt_t_date.Value);

                    if (dt.Rows.Count > 0)
                    {
                        str.Append("<table id='data-table1' runat='server'  class='table table-bordered table-hover'> <thead><tr class='grey'><th>PO No</th><th>PO Date</th><th>PO Net Value</th><th>PO GV</th><th>Cumulative Inv Amt</th><th>Balance Inv Amt</th><th>Dispatch Quantity</th></tr> ");
                        str.Append("</thead><tbody>");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string encrypt_Str = crypt.Encryptdata(dt.Rows[i]["PO_NUMBER"].ToString());
                            str.Append("<tr>");
                            str.Append("<td><a href='#' role='button' id='anc1' data-toggle='modal' onclick='viewData(" + (i + 1) + ")' >" + dt.Rows[i]["PO_NUMBER"].ToString() + "</a><input type='text' id='encrypt_po_" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                            str.Append("<td>" + dt.Rows[i]["PO_Date"].ToString() + "</td>");
                            str.Append("<td style='text-align: right'>" + dt.Rows[i]["PO_VALUE"].ToString() + "</td>");
                            str.Append("<td style='text-align: right'>" + dt.Rows[i]["PO_GV"].ToString() + "</td>");
                            txt_PONO.Text = dt.Rows[i]["PO_NUMBER"].ToString();

                            DataSet ds = (DataSet)ActionController.ExecuteAction("", "Po_List_Status_Report.aspx", "invoiceamount", ref IsValid1, txt_PONO.Text,txt_f_date.Value,txt_t_date.Value);
                            DataTable dt1 = ds.Tables[0];
                            DataTable dt2 = ds.Tables[1];

                            if (dt1 != null && dt1.Rows.Count > 0)
                            {
                                if (dt1.Rows[0]["CumulativeAmount"].ToString() == "")
                                {
                                    str.Append("<td  style='text-align: right'>" + 0.00 + "</td>");
                                    dbcumm = dt1.Rows[0]["CumulativeAmount"].ToString();
                                    dbcumm = "0.00";
                                }
                                else
                                {
                                    str.Append("<td  style='text-align: right' >" + dt1.Rows[0]["CumulativeAmount"].ToString() + "</td>");
                                    dbcumm = dt1.Rows[0]["CumulativeAmount"].ToString();
                                }
                            }
                            else
                            {
                                str.Append("<td  style='text-align: right' >" + 0.00 + "</td>");

                            }
                            if (dt.Rows[i]["PO_GV"].ToString() == "0.00" && dbcumm == "0.00")
                            {
                                str.Append("<td style='text-align: right'>" + 0.00 + "</td>");

                            }
                            else
                            {
                                balamount = Convert.ToDouble(dt.Rows[i]["PO_GV"].ToString()) - Convert.ToDouble(dbcumm);
                                str.Append("<td style='text-align: right'>" + balamount.ToString("0.00") + " </td>");

                                strbal = Convert.ToString(balamount);

                            }
                            if (dt2 != null && dt2.Rows.Count > 0)
                            {
                                if (dt2.Rows[0]["qut"].ToString() == "")
                                {
                                    str.Append("<td  style='text-align: right'>" + 0.00 + "</td>");
                                }
                                else
                                {
                                    str.Append("<td  style='text-align: right'>" + dt2.Rows[0]["qut"].ToString() + "</td>");
                                }
                                //if (dt2.Rows[0]["grqut"].ToString() == "")
                                //{
                                //    str.Append("<td  style='text-align: right'>" + 0.00 + "</td>");
                                //}
                                //else
                                //{
                                //    str.Append("<td  style='text-align: right'>" + dt2.Rows[0]["grqut"].ToString() + "</td>");
                                //}


                            }
                            //str.Append("<td>" + dt.Rows[i]["PO_Status"].ToString() + "</td>");
                            //str.Append("<td>" + dt.Rows[i]["REMARK"].ToString() + "</td>");
                            str.Append("</tr>");
                        }
                        str.Append("</tbody></table> ");
                    }
                    else
                    {
                        div_po.InnerHtml = null;
                         Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');}</script>");
                    }
                  
                }
                div_po.InnerHtml = str.ToString();
                txtdata.Text = str.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({'bSort': false})", true);      
            }

}
   protected void btn_Export_onClick(object sender, EventArgs e)
   {
       if (ActionController.IsSessionExpired(Page))
           ActionController.RedirctToLogin(Page);
       else
       {
           ExportToExcel(str, "PO List Status Report");
       }
   }
   protected void btnCancel_Click(object sender, EventArgs e)
   {
       try
       {
           Response.Redirect("../../Reports/Report_Master.aspx");

       }
       catch (Exception ex)
       {
           FSL.Logging.Logger.WriteEventLog(false, ex);
       }
   }
   protected void ExportToExcel(StringBuilder dgview, string filename)
   {
       try
       {
           string dt = string.Empty;
           string attachment = "attachment; filename=" + filename + ".xls";
           Response.ClearContent();
           Response.AddHeader("content-disposition", attachment);
           Response.ContentType = "application/ms-excel";
           dt = txtdata.Text;
           if (dt!="")
           {
               Response.Write("<table border=\"1\"><tr><td colspan='8' align='center' style=\"font-weight:bold;font-size:'20';\">PO List Status Report</td></tr>");
               Response.Write("<tr style=\"font-weight:bold\"><td colspan='8'></td></tr>");
               Response.Write("<tr style=\"font-weight:bold\"><td colspan='7' align='center'>" + dt + "</td></tr>");            
               Response.Write("</table>");
           }
           Response.End();
       }

       catch (Exception ex)
       {      
           Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Not Found!!');</script>");                 
           return;
       }
   }
    
}