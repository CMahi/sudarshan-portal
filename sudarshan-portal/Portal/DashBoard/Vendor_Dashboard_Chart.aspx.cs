using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using FSL.Controller;
using AjaxPro;
using FSL.Logging;
using FSL.Message;
using System.Collections;
using WFE;
using InfoSoftGlobal;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;

public partial class Vendor_Dashboard_Chart : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Vendor_Dashboard_Chart));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_CREATED_BY.Text = txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                    }
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    ShowPODetails();
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    //[WebMethod]
        [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string GetChart(string vcode,int ch_type)
    {
        
        string isdata = string.Empty;
        DataSet dt = (DataSet)ActionController.ExecuteAction("", "Vendor_Dashboard_Chart.aspx", "getdata", ref isdata, vcode);

        StringBuilder sb = new StringBuilder();
        
        /**************************************Invoice Status Report*****************************************/
        sb.Append("[");
        for (int i = 0; i < dt.Tables[ch_type-1].Rows.Count; i++)
        {
            sb.Append("{");
            System.Threading.Thread.Sleep(50);
            string color = String.Format("#{0:X6}", new Random().Next(0x1000000));
            sb.Append(string.Format("text :'{0}', value:{1}, color: '{2}'", dt.Tables[ch_type - 1].Rows[i][0], dt.Tables[ch_type - 1].Rows[i][1], color));
            sb.Append("},");
        }
        sb = sb.Remove(sb.Length - 1, 1);
        sb.Append("]");
        /*****************************************************************************************************/
        return sb.ToString();

    }
  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../HomePage.aspx");

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
    protected void ddlPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPO.Attributes.Add("onchange", "LoadPOCount();");
    }
    protected void ddlInvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlInvoice.Attributes.Add("onchange", "LoadChart();");
    }
    protected void ddlPoAmt_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPoAmt.Attributes.Add("onchange", "LoadPOAmount();");
    }

    protected void ShowPODetails()
    {

        string IsValid = string.Empty;
        StringBuilder str = new StringBuilder();
        StringBuilder strdata = new StringBuilder();
        StringBuilder strmat = new StringBuilder();
        StringBuilder strpay = new StringBuilder();
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Dashboard.aspx", "selectpo", ref IsValid, txt_CREATED_BY.Text);
        txt_dt_count.Text = dt.Rows.Count.ToString();
        lblCount.Text = txt_dt_count.Text;
        if (dt != null)
        {
          
            str.Append("<table id='mytable' runat='server'  class='table table-bordered'> <thead><tr class='grey'><th style='width:2%'> #</th><th >PO NO</th> <th >PO Date</th><th>Quantity</th><th>Gross Value</th></tr> ");
            str.Append("</thead><tbody>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string encrypt_Str = crypt.Encryptdata(Convert.ToString(dt.Rows[i]["PO_NO"]));
                str.Append(" <tr>");
                str.Append("<td id = '1'>  <p><input id='open_po" + (i + 1) + "'  name='optionsChk' value='option" + (i + 1) + "' type='checkbox'  > </p> <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + dt.Rows[i]["PO_NO"].ToString() + "' style='display:none' /></td>");
                str.Append("<td id = '2'><a href='#' role='button' id='anc1' data-toggle='modal'  onclick='viewData(" + (i + 1) + ")'>" + dt.Rows[i]["PO_NO"].ToString() + "</a><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");

                str.Append("<td>" + dt.Rows[i]["PO_Date"].ToString() + " <input id='txt_date" + (i + 1) + "'  runat='server' Value='" + dt.Rows[i]["PO_Date"].ToString() + "' style='display:none' /></td>");
                txt_PONO.Text = dt.Rows[i]["PO_NO"].ToString();

                DataSet ds = (DataSet)ActionController.ExecuteAction("", "Dashboard.aspx", "selectgrossvalue", ref IsValid, txt_PONO.Text);
                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    if (dt2.Rows[0]["qut"].ToString() == "")
                    {
                        str.Append("<td  style='text-align: right' >" + 0.00 + "</td>");
                    }
                    else
                    {
                        str.Append("<td  style='text-align: right' >" + dt2.Rows[0]["qut"].ToString() + "  <input id='txt_quat" + (i + 1) + "'  runat='server' Value='" + dt2.Rows[0]["qut"].ToString() + "' style='display:none' /></td>");                       
                    }
                    //if (dt2.Rows[0]["PO_Value"].ToString() == "")
                    //{
                    //    str.Append("<td  style='text-align: right' >" + 0.00 + "</td>");
                    //}
                    //else
                    //{
                    //    str.Append("<td  style='text-align: right' >" + dt2.Rows[0]["PO_Value"].ToString() + "</td>");
                    //}
                }
               

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    str.Append("<td  style='text-align: right' >" + dt1.Rows[0]["po_gv"].ToString() + "<input id='txt_gross" + (i + 1) + "'  runat='server' Value='" + dt1.Rows[0]["po_gv"].ToString() + "' style='display:none' /></td>");
                }
                else
                {
                    str.Append("<td  style='text-align: right' >" + 0.00 + "</td>");
                }

              
                str.Append("</tr>");
        

            }
            str.Append("   </tbody>   </table> ");
        }
        div_po.InnerHtml = str.ToString();
      
        /********************************************Report for pending deliviries Data ***********************/
        DataTable dtreport = (DataTable)ActionController.ExecuteAction("", "Dashboard.aspx", "reportdata", ref IsValid, txt_CREATED_BY.Text,"PenDelivaery");
        strdata.Append("<table id='tbl_PendingDeliveries' runat='server'  class='table table-bordered'> <thead><tr class='grey'><th> #</th><th >PO NO</th> <th >Purchase Order Date</th><th>Material Text</th><th>Delivery Due on</th><th>Quantity</th><th>Plant</th></tr> ");
        strdata.Append("</thead><tbody>");
        if (dtreport != null && dtreport.Rows.Count > 0)
        {
            for (int i = 0; i < dtreport.Rows.Count; i++)
            {
                string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtreport.Rows[i]["EBELN"]));
                strdata.Append(" <tr>");
                strdata.Append("<td>" + (i + 1) + "</td>");
                strdata.Append("<td><a href='#' role='button' id='anc1' data-toggle='modal'  onclick='viewData(" + (i + 1) + ")'>" + dtreport.Rows[i]["EBELN"].ToString() + "</a><input type='text' id='encrypt_po_" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                strdata.Append("<td>" + dtreport.Rows[i]["PO_Date"].ToString() + "</td>");
                strdata.Append("<td>" + dtreport.Rows[i]["TXZ01"].ToString() + "</td>");
                strdata.Append("<td>" + dtreport.Rows[i]["End_date"].ToString() + "</td>");
                strdata.Append("<td style='text-align: center' >" + dtreport.Rows[i]["MENGE0"].ToString() + "</td>");
                strdata.Append("<td>" + dtreport.Rows[i]["WERKS"].ToString() + "</td>");
                strdata.Append("</tr>");
            }
          
        }
        strdata.Append("   </tbody>   </table> ");     
        div_PendingDeliveries.InnerHtml = strdata.ToString();
        ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#tbl_PendingDeliveries').dataTable();", true);

        /********************************************Report for Material Data ***********************/
        DataTable dtmat = (DataTable)ActionController.ExecuteAction("", "Dashboard.aspx", "reportdata", ref IsValid, txt_CREATED_BY.Text, "PenMaterial");
        strmat.Append("<table id='tbl_MaterialRecipet_Status' runat='server'  class='table table-bordered'> <thead><tr class='grey'><th> #</th><th >PO NO</th> <th>Purchase Order Date</th><th>Material Code</th><th>Material Text</th><th>Date of Dispatch</th><th>Date of GR</th><th>GRN No</th><th>Accepted Qty</th><th>Rejected Qty</th></tr> ");								
        strmat.Append("</thead><tbody>");
        if (dtmat != null && dtmat.Rows.Count > 0)
        {
            for (int i = 0; i < dtmat.Rows.Count; i++)
            {
                string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtmat.Rows[i]["PO_Number"]));
                strmat.Append(" <tr>");
                strmat.Append("<td>" + (i + 1) + "</td>");
                strmat.Append("<td><a href='#' role='button' id='anc1' data-toggle='modal'  onclick='viewData(" + (i + 1) + ")'>" + dtmat.Rows[i]["PO_Number"].ToString() + "</a><input type='text' id='encrypt_po_" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                strmat.Append("<td>" + dtmat.Rows[i]["PO_Date"].ToString() + "</td>");
                strmat.Append("<td>" + dtmat.Rows[i]["Material_No"].ToString() + "</td>");
                strmat.Append("<td>" + dtmat.Rows[i]["MATERIAL_DESC"].ToString() + "</td>");
                strmat.Append("<td>" + dtmat.Rows[i]["Dispatch_Date"].ToString() + "</td>");
                strmat.Append("<td>" + dtmat.Rows[i]["GR_DATE"].ToString() + "</td>");
                strmat.Append("<td>" + dtmat.Rows[i]["GRN_NO"].ToString() + "</td>");
                strmat.Append("<td  style='text-align: center' >" + dtmat.Rows[i]["GR_QUANTITY"].ToString() + "</td>");
                strmat.Append("<td  style='text-align: center' >" + dtmat.Rows[i]["GR_Rejected"].ToString() + "</td>");
                strmat.Append("</tr>");
            }

        }
        strmat.Append("   </tbody>   </table> ");
        div_MaterialRecipet_Status.InnerHtml = strmat.ToString();
        ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#tbl_MaterialRecipet_Status').dataTable();", true);
       
        /********************************************Reports for pending payment Data ***********************/
        DataTable dtpay = (DataTable)ActionController.ExecuteAction("", "Dashboard.aspx", "reportdata", ref IsValid, txt_CREATED_BY.Text, "PenPayment");
        string isdata = string.Empty;
        strpay.Append("<table id='tbl_PendingPayment' runat='server'  class='table table-bordered'> <thead><tr class='grey'><th> #</th><th >PO NO</th> <th>Purchase Order Date</th><th>Plant</th><th>Unique No</th><th>Date of Delivery</th><th>Payment Terms</th><th>Invoice No</th><th>Invoice Date</th><th>Invoice Amount</th><th>SAP Status</th></tr> ");
            strpay.Append("</thead><tbody>");
            if (dtpay != null && dtpay.Rows.Count > 0)
            {
           
            for (int i = 0; i < dtpay.Rows.Count; i++)
            {
                string str_status = dtpay.Rows[i]["SAP_Status"].ToString();
                StringBuilder img_Data = new StringBuilder();
                img_Data.Append("<table><tr>");
                                    if (str_status == "Quality Control Complete with Deviation")
                                    {
                                        img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QCD</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>RJ</th></tr><tbody>");
                                        img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='GRN Complete'/></a></td><td><a href='#'><img src='../../images/3.png' title='QC-Approved under Deviation'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td></tr>");
                                    }
                                    else
                                    {
                                        if (str_status == "Rejected")
                                        {
                                            img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QC</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>RJ</th></tr><tbody>");
                                            img_Data.Append("<tr><td><a href='#'><img src='../../images/1.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/1.png' title='GRN Complete'/></a></td><td><a href='#'><img src='../../images/1.png' title='QC-Approved'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td><td><a href='#'><img href='#' src='../../images/0.png' title='Rejected'/></a></td></tr>");
                                        }
                                        else if (str_status == "Transition")
                                        {
                                            img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QC</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>RJ</th></tr><tbody>");
                                            img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/1.png' title='GRN Complete'/></a></td><td><a href='#'><img src='../../images/1.png' title='QC-Approved'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td></tr>");
                                        }
                                        else if (str_status == "GRN Complete")
                                        {
                                            img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QC</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>RJ</th></tr><tbody>");
                                            img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='GRN Complete'/></a></td><td><a href='#'><img src='../../images/1.png' title='QC-Approved'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td></tr>");
                                        }
                                        else if (str_status == "Quality Control Complete")
                                        {
                                            img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QC</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>RJ</th></tr><tbody>");
                                            img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='GRN Complete'/></a></td><td><a href='#'><img src='../../images/2.png' title='QC-Approved'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td></tr>");
                                        }
                                        else
                                        {
                                            string uno = dtpay.Rows[i]["Unique_No"].ToString();
                                            string count = (string)ActionController.ExecuteAction("", "Invoice_Details_Report.aspx", "getstatuscount", ref isdata, uno);
                                            string dtImg = string.Empty;
                                            if (count == "1")
                                            {
                                                img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QCD</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>RJ</th></tr><tbody>");
                                                dtImg = "<a href='#'><img src='../../images/3.png' title='QC-Approved under Deviation'/></a>";
                                            }
                                            else
                                            {
                                                img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QC</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>RJ</th></tr><tbody>");
                                                dtImg = "<a href='#'><img src='../../images/2.png' title='QC-Approved'/></a>";
                                            }
                                            if (str_status == "Payment Processed")
                                            {
                                                img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='GRN Complete'/></a></td><td>" + dtImg + "</td><td><a href='#'><img src='../../images/2.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/2.png' title='Payment Processed'/></a></td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td></tr>");
                                            }
                                            //else if (str_status == "Bill Booked")
                                            else
                                            {
                                                img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='GRN Complete'/></a></td><td>" + dtImg + "</td><td><a href='#'><img src='../../images/2.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td></tr>");
                                            }
                                        }
                                    }
                img_Data.Append("</tr></tbody></table>");
                string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtpay.Rows[i]["PO_NUMBER"]));
                strpay.Append(" <tr>");
                strpay.Append("<td>" + (i + 1) + "</td>");
                strpay.Append("<td><a href='#' role='button' id='anc1' data-toggle='modal'  onclick='viewData(" + (i + 1) + ")'>" + dtpay.Rows[i]["PO_NUMBER"].ToString() + "</a><input type='text' id='encrypt_po_" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                strpay.Append("<td width='10%'>" + dtpay.Rows[i]["PO_Date"].ToString() + "</td>");
                strpay.Append("<td>" + dtpay.Rows[i]["PLANT"].ToString() + "</td>");
                strpay.Append("<td>" + dtpay.Rows[i]["Unique_No"].ToString() + "</td>");
                strpay.Append("<td>" + dtpay.Rows[i]["Creation_Date"].ToString() + "</td>");
                strpay.Append("<td>" + dtpay.Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
                strpay.Append("<td>" + dtpay.Rows[i]["Invoice_No"].ToString() + "</td>");
                strpay.Append("<td>" + dtpay.Rows[i]["invoice_date"].ToString() + "</td>");
                strpay.Append("<td style='text-align: right' >" + dtpay.Rows[i]["Cum_Amount"].ToString() + "</td>");
                strpay.Append("<td>" + img_Data.ToString() + "</td>");     
                strpay.Append("</tr>");
            }
          
        }
        strpay.Append("   </tbody>   </table> ");
        div_PendingPayment.InnerHtml = strpay.ToString();
        ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#tbl_PendingPayment').dataTable();", true); 
    }

    protected void btnsubmit_click(object sender, EventArgs e)
    {
        try
        {
            div_remark.Visible = false;
            string pno = string.Empty;
            string str = string.Empty;
            string Result = string.Empty;
            string resultupdate = string.Empty;
            string actionresultup = string.Empty;
            pno = txt_check_PO_Nos.Text;
            if (pno != "")
            {
                int parse = int.Parse(pno);
                string[] separators = { ";" };
                string Pono = txt_PONO.Text;
                string POdate = txt_ProjectXml.Text;
                string POGvalue = txt_GV_VALUE.Text;
                string[] words1 = Pono.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string[] words2 = POdate.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string[] words3 = POGvalue.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < parse; i++)
                {
                    resultupdate = (string)ActionController.ExecuteAction("", "Dashboard.aspx", "updatelock", ref actionresultup, words1[i], words2[i], words3[i], txt_CREATED_BY.Text);
                    if (resultupdate == "TRUE")
                    {
                        ScriptManager.RegisterClientScriptBlock(upModal, this.GetType(), "script", "alert('Selected POs Acknowledged successfully...!');", true);
                        // Label1.Text = "<b>Selected PO's Acknowledged successfully...!<b>";
                    }
                    else if (actionresultup.Length > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(upModal, this.GetType(), "script", "alert(\"" + actionresultup + "\");", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(upModal, this.GetType(), "script", "alert('Please Select PO');", true);
            }
        }
        catch (Exception Exc) { throw new Exception(Exc.Message, Exc.InnerException); }
        finally
        {
            ShowPODetails();
        }
    }
  
    protected void btn_sendback_click(object sender, EventArgs e)
    {
        try
        {
            
            if (txt_remark.Value == "")
            {
               // ScriptManager.RegisterClientScriptBlock(upModal, this.GetType(), "script", "alert('Please Enter Remark');", true);
            }
            else
            {
                string pno = string.Empty;
                string str = string.Empty;
                string Result = string.Empty;
                string resultupdate = string.Empty;
                string actionresultup = string.Empty;
                pno = txt_check_PO_Nos.Text;
                if (pno != "")
                {
                    int parse = int.Parse(pno);

                    string[] separators = { ";" };
                    string Pono = txt_PONO.Text;
                    string POdate = txt_ProjectXml.Text;
                    string POGvalue = txt_GV_VALUE.Text;
                    string[] words1 = Pono.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    string[] words2 = POdate.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    string[] words3 = POGvalue.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < parse; i++)
                    {
                        resultupdate = (string)ActionController.ExecuteAction("", "Dashboard.aspx", "sendbacklock", ref actionresultup, words1[i], words2[i], words3[i], txt_CREATED_BY.Text, txt_remark.Value);
                        if (resultupdate == "TRUE")
                        {
                            ScriptManager.RegisterClientScriptBlock(upModal, this.GetType(), "script", "alert('Selected POs Sent back Successfully...!');", true);
                           
                        }
                        else if (actionresultup.Length > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(upModal, this.GetType(), "script", "alert(\"" + actionresultup + "\");", true);
                        }
                    }
                    
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(upModal, this.GetType(), "script", "alert('Please Select PO');", true);
                }
            }
        }
        catch (Exception Exc) { throw new Exception(Exc.Message, Exc.InnerException); }
        finally
        {
             ShowPODetails();
             txt_remark.Value = "";
             ScriptManager.RegisterStartupScript(this, GetType(), "ShowMe", "ShowMe();", true);
             //ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#div_remark').hide();", true);      
            
        }
    }
}