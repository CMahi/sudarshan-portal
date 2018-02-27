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

public partial class Advance_Return_Foreign_Approval : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Return_Foreign_Approval));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Session["USER_ADID"].ToString();
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                        step_name.Text = Convert.ToString(Request.QueryString["step"]);
                    }
                    step_name.Text = "FOREIGN ADVANCE REQUEST APPROVAL";

                    Initialization();


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

        DataTable predata = (DataTable)ActionController.ExecuteAction("", "Advance_Return_Request.aspx", "getpreids", ref IsData, txtProcessID.Text, txtInstanceID.Text, 2);
         if (predata != null && predata.Rows.Count > 0)
         {
             return_pk_id.Text = Convert.ToString(predata.Rows[0]["PK_RETURN_ADVANCE_Id"]);
             spn_ret_no.InnerHtml = Convert.ToString(predata.Rows[0]["REQUEST_NO"]);
             spn_ret_date.InnerHtml = Convert.ToDateTime(predata.Rows[0]["CREATION_DATE"]).ToString("dd-MMM-yyyy");
             string pre_pro = Convert.ToString(predata.Rows[0]["PROCESS_ID"]);
             string pre_ins = Convert.ToString(predata.Rows[0]["INSTANCE_ID"]);
             txt_ret_money.Text = Convert.ToString(predata.Rows[0]["return_money"]);
             txt_exc_rate.Text = Convert.ToString(predata.Rows[0]["exc_rate"]);

             DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "addetails", ref IsData, pre_pro,pre_ins);
             if (dsData != null)
             {
                 if (dsData.Tables[0].Rows.Count > 0)
                 {
                     txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_FTA_HDR_ID"]);
                     initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["emp_adid"]);
                     spn_req_no.Text = txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["request_no"]);
                     req_date.Text = Convert.ToDateTime(dsData.Tables[0].Rows[0]["CREATION_DATE"]).ToString("dd-MMM-yyyy");
                     spn_F_Date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["FROM_DATE"]).ToString("dd-MMM-yyyy");
                     spn_T_Date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["TO_DATE"]).ToString("dd-MMM-yyyy");
                     spn_F_Region.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["F_COUNTRY"]);
                     spn_T_Region.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["T_COUNTRY"]);
                     spn_F_City.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["F_CITY"]);
                     spn_T_City.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["T_CITY"]);
                     pk_base_currency.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["BASE_CURRENCY"]);
                     base_currency.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY_NAME"]);
                     base_currency_rate.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["BASE_CURRENCY_RATE"]);
                     //spn_Remark.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REMARK"]);
                     txt_amt_limit.Text = Convert.ToString(dsData.Tables[0].Rows[0]["FTE_AMOUNT"]);
                     Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMAIL_ID"]);
                     txt_Deviate.Text = Convert.ToString(dsData.Tables[0].Rows[0]["REQ_DEVIATE"]);

                     int days = 0;
                     DateTime fdate = Convert.ToDateTime(spn_F_Date.InnerHtml);
                     DateTime tdate = Convert.ToDateTime(spn_T_Date.InnerHtml);
                     days = Convert.ToInt32((tdate - fdate).TotalDays) + 1;
                     //lbl_allowedamt.Text = Convert.ToString(days * Convert.ToInt32(txt_amt_limit.Text));
                     balance.InnerHtml = req_base_currency.InnerHtml = Convert.ToString(dsData.Tables[3].Rows[0]["REQ_AMOUNT"]);
                     txt_adamount.Text = Convert.ToString(dsData.Tables[4].Rows[0]["REQ_INR_AMOUNT"]);

                     

                     //bind_Line_Item(dsData.Tables[1]);
                     //fillDocument_Details(dsData.Tables[2]);
                 }
             }
             DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "pgettraveluser", ref IsData, initiator.Text);
             if (dtUser != null && dtUser.Rows.Count > 0)
             {
                 lbl_EmpCode.Text = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
                 lbl_EmpName.Text = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
                 lbl_Dept.Text = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
                 lbl_Grade.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
                 lbl_CostCenter.Text = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
                 lbl_MobileNo.Text = dtUser.Rows[0]["MOBILE_NO"].ToString();
                 lbl_desgnation.Text = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
                 txt_desg_id.Text = Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);
                 // lbl_GLCode.Text = Convert.ToString(dtUser.Rows[0]["GL_CODE"]);

                 lbl_division.Text = "NA";
                 if (Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]).Trim() != "")
                 {
                     span_Ifsc.InnerHtml = Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]);
                 }
                 if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]) != "")
                 {
                     lbl_bankAccNo.Text = Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]);
                 }
                 else
                 {
                     lbl_bankAccNo.Text = "NA";
                 }
                 DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "pgettravelrequestapprover", ref IsData1, initiator.Text);
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
                             doa_user.Text = span_DApprover.InnerHtml = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                             span_Dapp_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);

                         }
                         else
                         {
                             doa_user.Text = span_Dapp_name.InnerHtml = span_DApprover.InnerHtml = "NA";
                         }
                         doa_email.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver_email"]);
                     }

                 }
             }
         }
    }

    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "script", "window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');", true);
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                divIns.Style.Add("display", "none");

                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;

                /***************************************************************************************************************************************************************/

                    string rfc_string = string.Empty;
                    string rfc_string1 = string.Empty;
                    string line_item = string.Empty;
                    string bank_flag = string.Empty;
                    string bank_no = string.Empty;
                    string rfc_no = "";
                    string rfc_flag = "";
                    string alert_msg = "";
                    DataTable dtManage = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata1", ref refData, spn_ret_no.InnerHtml, "SELECT", "", "", "", "", "", "", "");
                    if (dtManage != null && dtManage.Rows.Count > 0)
                    {
                        bank_flag = Convert.ToString(dtManage.Rows[0]["BANK_FLAG"]);
                        bank_no = Convert.ToString(dtManage.Rows[0]["BANK_NO"]);
                    }

                    if (bank_flag == "E" || bank_flag == "")
                    {
                        string rfc_action = "";
                        DataSet dt_sap_rfc = (DataSet)ActionController.ExecuteAction("", "Advance_Return_Request.aspx", "getbankrfc", ref refData, 0, rfc_action, spn_ret_no.InnerHtml, 1, a_adv_amount.Text, a_exp_amount.Text);
                        if (dt_sap_rfc != null)
                        {
                            if (dt_sap_rfc.Tables[0].Rows.Count > 0)
                            {
                                string ref_no = Convert.ToString(dt_sap_rfc.Tables[0].Rows[0][0]);
                            }
                            if (dt_sap_rfc.Tables[1].Rows.Count > 0)
                            {
                                for (int index = 0; index < dt_sap_rfc.Tables[1].Rows.Count; index++)
                                {
                                    if (rfc_string == "")
                                    {
                                        rfc_string += Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["COMP_CODE"]);
                                    }
                                    else
                                    {
                                        rfc_string += "|" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["COMP_CODE"]);
                                    }
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["DOC_DATE"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["PSTNG_DATE"]);
                                    rfc_string += "$";
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["DOC_TYPE"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["REF_DOC_NO"]);
                                    rfc_string += "$" + (index + 1).ToString();
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["GL_ACCOUNT"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["REF_KEY_1"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["VENDOR_NO"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ALLOC_NMBR"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ITEM_TEXT"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["BUS_AREA"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["COSTCENTER"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["PROFIT_CTR"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["CURRENCY"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["AMT_DOCCUR"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ZLSCH"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["PERSON_NO"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["SECCO"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["BUPLA"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ZFBDT"]);
                                    rfc_string += "$ ";
                                }
                            }
                        }
                        rfc_string1 = rfc_string;

                        if (rfc_string1 != "")
                        {
                            string ref_key_no = "";
                            
                            line_item = getLine_Item();
                            Vendor_Portal.Vendor_Portal_DetailsService Vendor = new Vendor_Portal.Vendor_Portal_DetailsService();
                            string[] Vendor_data_array = new string[3];
                            Vendor_data_array = Vendor.BANK_DETAILS(rfc_string1, line_item);
 //                           Vendor_data_array[0] = "";
                            rfc_flag = Convert.ToString(Vendor_data_array[1]);
                            alert_msg = Convert.ToString(Vendor_data_array[0]);
                            string[] sp_data = Convert.ToString(Vendor_data_array[0]).Split(' ');

                            if (Convert.ToString(Vendor_data_array[1]) == "S")
                            {
                                for (int k = 0; k < sp_data.Length; k++)
                                {
                                    if (Convert.ToString(sp_data[k]).ToUpper().Contains("SCIL"))
                                    {
                                        rfc_no = Convert.ToString(sp_data[k]).Substring(0, 10);
                                    }
                                }
                            }
                            string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, spn_ret_no.InnerHtml, "BANK", "", "", "", Convert.ToString(Vendor_data_array[1]).ToUpper(), rfc_no, Convert.ToString(Vendor_data_array[0]), ref_key_no);
                        }
                    }
                    else
                    {
                        rfc_flag = "S";
                        rfc_no = bank_no;
			alert_msg="Advance Return Request Has Been Completed Successfully : " + rfc_no;
                    }
                    //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_msg +  "...!');}</script>");
                    /***************************************************************************************************************************************/
                    if (Convert.ToString(rfc_flag) != "E" && Convert.ToString(rfc_flag) != "")
                    {
                        string isSaved = (string)ActionController.ExecuteAction("", "Advance_Return_Request.aspx", "insertfta", ref refData, txt_StepId.Text, txtInstanceID.Text, 2, return_pk_id.Text, txt_Username.Text, "", req_base_currency.InnerHtml, 0, 0, txt_ret_money.Text, txt_exc_rate.Text, tbl_adv.InnerHtml, tbl_exp.InnerHtml, (Convert.ToDouble(req_base_currency.InnerHtml) - Convert.ToDouble(txt_ret_money.Text)), 2, 0);
                        if (isSaved == null || isSaved == "" || refData.Length > 0 || isSaved == "false")
                        {
                            //string[] errmsg = refData.Split(':');
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + refData.ToString() + "')", true);
                        }
                        else
                        {
                            string[] Dval = new string[1];
                            Dval[0] = txt_Username.Text;

                            bool isCreate1 = (bool)WFE.Action.ReleaseStep(txt_StepId.Text, txtInstanceID.Text, "291", "ADVANCE RETURN FOREIGN APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, isSaved, txtWIID.Text, ref refData);
                            if (isCreate1)
                            {
                                try
                                {
                                    string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Return_Request.aspx", "insertaudittrail", ref isInserted, txt_StepId.Text, txtInstanceID.Text, "ADVANCE RETURN FOREIGN APPROVAL", "USER", txt_Username.Text, "SUBMIT", "", "0", "0");
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Return Request has been Completed Successfully.</font></pre><p/> <pre><font size='3'>Request No: " + isSaved + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text + "</font></pre></p><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Return_Request.aspx", "insetmaildata", ref isInserted, txt_StepId.Text, txtInstanceID.Text, 0, "ADVANCE RETURN FOREIGN APPROVAL", "SUBMIT", txtEmailID.Text, "", msg, "Request No: " + isSaved);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {

                                    string msg2 = "alert('Advance Return Request Has Been Completed Successfully : " + rfc_no + "...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                }
                            }
                        }
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_msg + "...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                    }
                

                /**************************************************************************************************************************************************************/
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void btn_Reject_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                divIns.Style.Add("display", "none");

                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;

                decimal return_amount = 0;


                string isSaved = (string)ActionController.ExecuteAction("", "Advance_Return_Request.aspx", "insertfta", ref refData, txt_StepId.Text, txtInstanceID.Text, 2, return_pk_id.Text, txt_Username.Text, "", 0, 0, 0, return_amount, 0, 0, 0, 0, 3, txt_pk_id.Text);
                if (isSaved == null || isSaved == "" || refData.Length > 0 || isSaved == "false")
                {
                    //string[] errmsg = refData.Split(':');
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + refData.ToString() + "')", true);
                }
                else
                {
                    string[] Dval = new string[1];
                    Dval[0] = txt_Username.Text;

                    bool isCreate1 = (bool)WFE.Action.ReleaseStep(txt_StepId.Text, txtInstanceID.Text, "292", "ADVANCE RETURN FOREIGN APPROVAL", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, isSaved, txtWIID.Text, ref refData);
                    if (isCreate1)
                    {
                        try
                        {
                            string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Return_Request.aspx", "insertaudittrail", ref isInserted, txt_StepId.Text, txtInstanceID.Text, "ADVANCE RETURN FOREIGN APPROVAL", "USER", txt_Username.Text, "REJECT", "", "0", "0");
                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Return Request has been Rejected Successfully.</font></pre><p/> <pre><font size='3'>Request No: " + spn_ret_no + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text + "</font></pre></p><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                            string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Return_Request.aspx", "insetmaildata", ref isInserted, txt_StepId.Text, txtInstanceID.Text, 0, "ADVANCE RETURN FOREIGN APPROVAL", "REJECT", txtEmailID.Text, "", msg, "Request No: " + spn_ret_no);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {

                            string msg2 = "alert('Advance Return Request Has Been Rejected Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                        }
                    }
                }

            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    public string getLine_Item()
    {
        string line_item = "";

        line_item = "0001" + "$" + spn_ret_no.InnerHtml;
        line_item += "|" + "0002" + "$" + "Foreign Travel Advance Return";
        line_item += "|" + "0003" + "$" + "Foreign Travel Advance Return";
        line_item += "|" + "0004" + "$" + "Visit To " + spn_T_Region.InnerHtml;
        line_item += "|" + "0005" + "$" + lbl_EmpName.Text;
        line_item += "|" + "0006" + "$" + spn_T_Region.InnerHtml;
        line_item += "|" + "0007" + "$" + spn_F_Date.InnerHtml + " To " + spn_T_Date.InnerHtml;
        line_item += "|" + "0008" + "$" + req_base_currency.InnerHtml;
        line_item += "|" + "0009" + "$" + base_currency.InnerHtml;
        line_item += "|" + "0010" + "$" + txt_exc_rate.Text;
        line_item += "|" + "0011" + "$" + tbl_exp.InnerHtml;
        line_item += "|" + "0012" + "$" + "Visit To " + spn_T_Region.InnerHtml;

        return line_item;
    }
}