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

public partial class Advance_Request_ModeChange : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Request_ModeChange));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                    }                   
                    Initialization();
                    //showall();
                    fillPolicy_Details();
                }
            }
        }
        catch (Exception Exc) {  }
    }
   
   
   private void Initialization()
    {
        try
        {
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getdetails", ref isdata, txtProcessID.Text, txtInstanceID.Text);
            Session["dtdoc"] = dsData.Tables[1];
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_ADVANCE_HDR_Id"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                txt_Initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMAIL_ID"]);               
                span_advance.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["advance_type_name"]);
                if (span_advance.InnerHtml=="Other Expense Advance")
                {
                }
                else
                {
                    fillPolicy_Details();
                }
                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "pgettraveluser", ref isdata, txt_Initiator.Text);
                if (dtUser.Rows.Count > 0)
                {
                    empno.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
                    span_ename.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
                    span_cc.InnerHtml = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
                    span_dept.InnerHtml = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
                    span_grade.InnerHtml = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
                    span_mobile.InnerHtml = Convert.ToString(dtUser.Rows[0]["MOBILE_NO"]);
                    span_designation.InnerHtml = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
                    txt_designation.Text = Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);
                    if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]) != "")
                    {
                        span_bank_no.InnerHtml = Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]);
                    }
                    else
                    {
                        span_bank_no.InnerHtml = "NA";
                    }
                    if (Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]).Trim() != "")
                    {
                        span_Ifsc.InnerHtml = Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]);
                    }
                  //  span_division.InnerHtml = "NA";
		span_division.InnerHtml = Convert.ToString(dtUser.Rows[0]["DIVISION_NAME"]);
                spn_base_location.InnerHtml = Convert.ToString(dtUser.Rows[0]["BASE_LOCATION"]);

                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "pgettravelrequestapprover", ref isdata, txt_Initiator.Text);
                    if (dtApprover != null)
                    {
                        if (dtApprover.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                            {
                                span_Approver.InnerHtml = Convert.ToString(dtApprover.Rows[0]["approver"]);
                                span_app_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                            }
                            else
                            {
                                span_Approver.InnerHtml = "NA";
                                span_app_name.InnerHtml = "NA";
                            }
                            txt_Approver_Email.Text = Convert.ToString(dtApprover.Rows[0]["approver_email"]);

                            if (Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "0")
                            {
                                doa_user.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                                span_DApprover.InnerHtml = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                                span_Dapp_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);
                            }
                            else
                            {
                                span_Dapp_name.InnerHtml = span_DApprover.InnerHtml = "NA";
                                doa_user.Text = "NA";
                            }
                            doa_email.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver_email"]);
                        }
                        else
                        {
                            span_Approver.InnerHtml = doa_user.Text = "NA";
                        }
                    }
                   
                    fillDocument_Details();
                    fillAuditTrailData();
                    fillLocation();
                    fillPayment_Mode();       
                    fillData(Convert.ToString(dsData.Tables[0].Rows[0]["advance_for"]),dsData.Tables[0]);
                    ddlAdv_Location.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["LOCATION"]);
                    ddl_Payment_Mode.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["PAYMENT_MODE"]);      
                    txt_location.Text = Convert.ToString(dsData.Tables[0].Rows[0]["LOCATION"]);
                    txt_paymode.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PAYMENT_MODE"]);
                    string ISValid = string.Empty;
                    string str = string.Empty;
                    //DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref ISValid, span_advance.InnerHtml, "AdGLcode");
                     
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
               
    }
   private void fillDocument_Details()
   {
       if (ActionController.IsSessionExpired(Page))
           ActionController.RedirctToLogin(Page);
       else
       {
           try
           {
               string DisplayData = string.Empty;
               DataTable dtd = (DataTable)Session["dtdoc"];

               DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>";
               if (dtd != null)
               {
                   for (int i = 0; i < dtd.Rows.Count; i++)
                   {
                       DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dtd.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles(" + (i + 1) + ")>" + Convert.ToString(dtd.Rows[i]["filename"]) + "</a></td></tr>";
                   }
               }
               DisplayData += "</table>";
               divalldata.InnerHtml = DisplayData;
               DisplayData = "";
           }
           catch (Exception ex)
           {
               FSL.Logging.Logger.WriteEventLog(false, ex);
           }
       }
   }
   protected void fillPayment_Mode()
   {
       try
       {
           string isdata = string.Empty;
           DataTable dtPayment = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "selectdetails", ref isdata, txt_Username.Text, "AdPaymentMode");
           ddl_Payment_Mode.Items.Clear();
           if (dtPayment != null)
           {
               if (dtPayment.Rows.Count > 0)
               {
                   ddl_Payment_Mode.DataSource = dtPayment;
                   ddl_Payment_Mode.DataTextField = "PAYMENT_MODE";
                   ddl_Payment_Mode.DataValueField = "PK_PAYMENT_MODE";
                   ddl_Payment_Mode.DataBind();
               }
           }
           ddl_Payment_Mode.Items.Insert(0, "---Select One---");
       }
       catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
   }
   protected void fillLocation()
   {
       try
       {
           string isdata = string.Empty;
           DataTable dtLocation = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "selectdetails", ref isdata, txt_Username.Text, "AdLocation");
           ddlAdv_Location.Items.Clear();
           if (dtLocation != null)
           {
               if (dtLocation.Rows.Count > 0)
               {
                   ddlAdv_Location.DataSource = dtLocation;
                   ddlAdv_Location.DataTextField = "LOCATION_NAME";
                   ddlAdv_Location.DataValueField = "PK_LOCATION_ID";
                   ddlAdv_Location.DataBind();
               }
           }
           ddlAdv_Location.Items.Insert(0, "---Select One---");
       }
       catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
   }
   public void fillData(string adv_type, DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        txt_adv_type.Text = adv_type;
        if (adv_type == "1")
        {
            sb.Append("<div class='col-md-12' id='advance_travel'>");
            sb.Append("<div class='form-horizontal'>");
            sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'>Place From</label>");
            sb.Append("<div class='col-md-3'>");
            if (Convert.ToString(dt.Rows[0]["from_city"]) == "0" || Convert.ToString(dt.Rows[0]["from_city"]) == "-1" || Convert.ToString(dt.Rows[0]["from_city"]) == "")
            {
                sb.Append(Convert.ToString(dt.Rows[0]["other_f_city"]));
            }
            else
            {
                sb.Append(Convert.ToString(dt.Rows[0]["fcity"]));
            }
            sb.Append("</div>");
            sb.Append("<label class='col-md-2'>Place To</label>");
            sb.Append("<div class='col-md-3'>");
            if (Convert.ToString(dt.Rows[0]["to_city"]) == "0" || Convert.ToString(dt.Rows[0]["to_city"]) == "-1" || Convert.ToString(dt.Rows[0]["to_city"]) == "")
            {
                sb.Append(Convert.ToString(dt.Rows[0]["other_t_city"]));
            }
            else
            {
                sb.Append(Convert.ToString(dt.Rows[0]["tcity"]));
            }
            sb.Append("</div></div>");

            sb.Append("<div class='form-group'><div class='col-md-1'></div> ");
            sb.Append("<label class='col-md-2'>From Date</label>");
            sb.Append("<div class='col-md-3'><div class='input-group' id='Div1'>" + Convert.ToDateTime(dt.Rows[0]["from_date"]).ToString("dd-MMM-yyyy"));
            sb.Append("</div></div>");
            sb.Append("<label class='col-md-2'>To Date</label><div class='col-md-3'><div class='input-group' id='Div6'>" + Convert.ToDateTime(dt.Rows[0]["to_date"]).ToString("dd-MMM-yyyy"));
            sb.Append("</div></div><div class='col-md-1'></div></div>");

            sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'>Allowed Amount</label><div class='col-md-3'>");

            string isvalid = "";
            string amount = (string)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getadvanceamount", ref isvalid, txt_pk_id.Text, Convert.ToString(dt.Rows[0]["to_city"]), span_grade.InnerHtml);
            if (amount == "")
            {
                amount = "0";
            }
            sb.Append(amount);
            sb.Append("</div>");
            sb.Append("<label class='col-md-2'>Amount</label>");
            sb.Append("<div class='col-md-3'>" + Convert.ToString(dt.Rows[0]["amount"]) + "</div><div class='col-md-1'></div></div>");
            txtDummy.Text = Convert.ToString(dt.Rows[0]["amount"]);
            if (Convert.ToInt32(dt.Rows[0]["amount"]) > Convert.ToInt32(amount))
            {
                txt_Deviate.Text = "1";
            }
            sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label>");
            sb.Append("<div class='col-md-8'>" + Convert.ToString(dt.Rows[0]["remark1"]) + "</div></div>");
            sb.Append("</div></div>");
        }
        else
        { 
            sb.Append("<div class='col-md-12' id='advance_other'>");
            sb.Append("<div class='form-horizontal'>");						
			sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'>Advance Date</label>");
            sb.Append("<div class='col-md-3'><div class='input-group' id='Div4'>" + Convert.ToDateTime(dt.Rows[0]["advance_date"]).ToString("dd-MMM-yyyy"));
            sb.Append("</div></div>");
            sb.Append("<label class='col-md-2'>Amount</label><div class='col-md-3'>" + Convert.ToString(dt.Rows[0]["amount"]) + "</div><div class='col-md-1'></div></div>");
            txtDummy.Text = Convert.ToString(dt.Rows[0]["amount"]);
            //string isvalid = "";
            //DataTable dtA = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getadvancedeviate", ref isvalid, Convert.ToString(dt.Rows[0]["amount"]));
            //if (dtA != null && dtA.Rows.Count > 0)
            //{
            //    txt_Deviate.Text = "1";
            //}
            sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label>");
            sb.Append("<div class='col-md-8'>" + Convert.ToString(dt.Rows[0]["remark1"]) + "</div></div></div></div>");
        }

        div_LocalData.InnerHtml = Convert.ToString(sb);
    }
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
string Class= string.Empty;

               DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "selectdetails", ref isValid, span_grade.InnerHtml, "AdDesignation");
               if (dtamt != null && dtamt.Rows.Count > 0)
               {
                   DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Grade</th><th>City Class</th><th>Amount(Rs)</th><th>Effective Date</th></tr></thead>";

                   for (int i = 0; i < dtamt.Rows.Count; i++)
                   {
		if (Convert.ToString(dtamt.Rows[i]["CITY_CLASS"])=="A")
		{
		  Class="Metro";
		}
		else if (Convert.ToString(dtamt.Rows[i]["CITY_CLASS"])=="B")
		{
		  Class="Min-Metro";
		}
		else if (Convert.ToString(dtamt.Rows[i]["CITY_CLASS"])=="C")
		{
		  Class="Other";
		}
                       DisplayData += "<tr><td>" + span_grade.InnerHtml + "</td><td>" + Class + "</td><td>" + Convert.ToString(dtamt.Rows[i]["AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["EFFECTIVE_DATE"]) + "</td></tr>";

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
   //private void showall()
   // {
   //     try
   //     {

   //         DataSet DTS;
   //         string IsValid = string.Empty;
   //         DTS = (DataSet)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "addetails", ref IsValid, txtWIID.Text);
   //         DTS.Tables[2].Columns[0].ColumnName = "Sr.No";
   //         Session["ResultData"] = DTS;      
   //         if (DTS.Tables[2].Rows.Count > 0)
   //         {
   //            // div_Details.Visible = true;
   //             for (int i = 0; i < DTS.Tables[2].Rows.Count; i++)
   //             {
   //                 DTS.Tables[2].Rows[i]["Sr.No"] = i + 1;
   //             }

   //             StringBuilder str = new StringBuilder();

   //             str.Append("<table id='datatable1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th>#</th><th>Travel Type</th><th>Country</th><th>From Place</th><th>To Place</th><th>From Date</th><th>To Date</th><th>Amount</th></tr></thead>");
   //             str.Append("<tbody>");

   //             for (int i = 0; i < DTS.Tables[2].Rows.Count; i++)
   //             {
   //                 str.Append(" <tr>");
   //                 str.Append("<td align='left'>" + (i + 1) + "</td>");
   //                 str.Append("<td align='left'>" + DTS.Tables[2].Rows[i]["COUNTRY_TYPE"].ToString() + "</td>");
   //                 str.Append("<td align='left'>" + DTS.Tables[2].Rows[i]["COUNTRY_NAME"].ToString() + "</td>");
   //                 str.Append("<td align='left'>" + DTS.Tables[2].Rows[i]["FROM_PLACE"].ToString() + "</td>");
   //                 str.Append("<td align='left'>" + DTS.Tables[2].Rows[i]["TO_PLACE"].ToString() + "</td>");
   //                 str.Append("<td align='left'>" + DTS.Tables[2].Rows[i]["FROM_DATE"].ToString() + "</td>");
   //                 str.Append("<td align='left'>" + DTS.Tables[2].Rows[i]["TO_DATE"].ToString() + "</td>");
   //                 str.Append("<td align='right'>" + DTS.Tables[2].Rows[i]["CURRENCY"].ToString() + "-" + DTS.Tables[2].Rows[i]["AMOUNT"].ToString() + "</td>");
   //                 str.Append("</tr>");
   //             }
   //             str.Append("   </tbody>   </table> ");
   //             div_LocalData.InnerHtml = str.ToString();
              
   //         }
   //         else
   //         {
   //             Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Data Found !!')}</script>");
   //             div_LocalData.InnerHtml = null;
   //         }
   //     }
   //     catch (Exception ex)
   //     {
   //         FSL.Logging.Logger.WriteEventLog(false, ex);
   //     }

   // }
   protected void btnRequest_Click(object sender, EventArgs e)
   {
       string isSaved = string.Empty;
       string isChange = string.Empty;
       string isInserted = string.Empty;
       string ref_data = string.Empty;
       try
       {
           ActionController.DisablePageCaching(this);
           if (ActionController.IsSessionExpired(this))
               ActionController.RedirctToLogin(this);
           else
           {
               txt_Audit.Text = "ADVANCE REQUEST PAYMENT MODE CHANGE";
               DataTable DTAP = new DataTable();
               if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
               {
                   DTAP = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getaccapprover", ref ref_data, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", txt_location.Text, ddl_Payment_Mode.SelectedValue);
               }
               else
               {
                   DTAP = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getaccapprover", ref ref_data, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", 0, ddl_Payment_Mode.SelectedValue);
               }
               if (DTAP != null)
               {
                   if (DTAP.Rows.Count > 0)
                   {
                       txt_Condition.Text = "3";
                       string req_no = crypt.Encryptdata(txt_Request.Text);
                       string process_name = crypt.Encryptdata("ADVANCE EXPENSE");
                       isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "update", ref ref_data, "0", txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, "", "Submit");                           
                       isChange = (string)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "updatemode", ref ref_data, ddlAdv_Location.SelectedValue, ddl_Payment_Mode.SelectedValue, txt_pk_id.Text);

                       if (isSaved == null || ref_data.Length > 0 || isChange == "false")
                       {
                           string[] errmsg = ref_data.Split(':');
                           Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                       }
                       else
                       {
                           string[] Dval = new string[DTAP.Rows.Count];
                         //  Dval[0] = "";
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
                           bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "239", "ADVANCE REQUEST PAYMENT MODE CHANGE", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
               
                           if (isCreate)
                           {
                               try
                               {
                                 //  if ((Init_Email.Text != "") && (txtEmailID.Text != ""))
                                 //  {
                                     //  string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been sent to Accounts for payment approval.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    //   string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ADVANCE REQUEST PAYMENT MODE CHANGE", "SUBMIT", txtApproverEmail.Text, Init_Email.Text, msg, "Request No: " + spn_req_no.InnerHtml);

                                   string emailid = string.Empty;
                                   string msg = string.Empty;
                                   if (ddl_Payment_Mode.SelectedValue == "1")
                                       {
                                           msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been sent to Accounts for payment approval...!</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTERNET URL:<a href='https://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>https://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";
                                           emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ADVANCE REQUEST PAYMENT MODE CHANGE", "APPROVE", Init_Email.Text, txtApproverEmail.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                       }
                                       else
                                       {
                                           msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been sent to Accounts for payment approval...!</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTERNET URL:<a href='https://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>https://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";
                                           emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ADVANCE REQUEST PAYMENT MODE CHANGE", "APPROVE", Init_Email.Text, txtApproverEmail.Text, msg, "Request No: " + spn_req_no.InnerHtml);

                                       }
                                   
                                   
                                   //  }
                               }
                               catch (Exception)
                               {
                                   throw;
                               }
                               finally
                               {
                                   Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Advance request has been sent for account payment approval...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                               }
                           }
                       }//
                   }
                   else
                   {
                       Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Approver Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                   }
               }
               else
               {
                   Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Approver Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
               }

           }
       }
       catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
   }
    protected void imgBtnRelease_Click(object sender, EventArgs e)
     {
         if (ActionController.IsSessionExpired(Page))
             ActionController.RedirctToLogin(Page);
         else
         {
             Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
         }
     }
    
    
}

