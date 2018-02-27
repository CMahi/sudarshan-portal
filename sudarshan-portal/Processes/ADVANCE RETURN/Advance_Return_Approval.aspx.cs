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

public partial class Advance_Return_Approval : System.Web.UI.Page
{
    ListItem Li = new ListItem("--Select One--", "0");
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Return_Approval));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        stepname.Text = Convert.ToString(Request.QueryString["step"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                    }
                    Initialization();
                    
                }

            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Advance_Return_Request.aspx");
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    private void Initialization()
    {
        try
        {
            string isdata = string.Empty;
            DataTable predata = (DataTable)ActionController.ExecuteAction("", "Advance_Return_Request.aspx", "getpreids", ref isdata, txtProcessID.Text, txtInstanceID.Text,1);
            if (predata != null && predata.Rows.Count > 0)
            {
                return_pk_id.Text = Convert.ToString(predata.Rows[0]["PK_RETURN_ADVANCE_Id"]);
                spn_ret_no.InnerHtml = Convert.ToString(predata.Rows[0]["REQUEST_NO"]);
                spn_ret_date.InnerHtml = Convert.ToDateTime(predata.Rows[0]["CREATION_DATE"]).ToString("dd-MMM-yyyy");
                spn_ret_location.InnerHtml = Convert.ToString(predata.Rows[0]["LOCATION_NAME"]);
                spn_return_remark.InnerHtml = Convert.ToString(predata.Rows[0]["RETURN_REMARK"]);
                return_location_id.Text=Convert.ToString(predata.Rows[0]["PK_LOCATION_ID"]);

                DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getdetails", ref isdata, Convert.ToString(predata.Rows[0]["FK_PROCESS_ID"]), Convert.ToString(predata.Rows[0]["FK_INSTANCE_ID"]));
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                {
                    txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_ADVANCE_HDR_Id"]);
                    spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                    txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                    spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                    txt_Initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                    Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMAIL_ID"]);
                    span_advance.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["advance_type_name"]);
                    if (span_advance.InnerHtml == "Other Expense Advance")
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
                        span_division.InnerHtml = "NA";

                        DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Advance_Return_Request.aspx", "getapprovedperson", ref isdata, spn_req_no.InnerHtml);
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
                                //txt_Approver_Email.Text = Convert.ToString(dtApprover.Rows[0]["approver_email"]);

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
                                //doa_email.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver_email"]);

                            }
                            else
                            {
                                span_Approver.InnerHtml = doa_user.Text = "NA";
                            }
                        }

                        fillDocument_Details();

                        fillData(Convert.ToString(dsData.Tables[0].Rows[0]["advance_for"]), dsData.Tables[0]);
                        ddlAdv_Location.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["LOCATION_NAME"]);
                        ddl_Payment_Mode.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["PAY_MODE"]);
                        if (ddl_Payment_Mode.InnerHtml.ToUpper() == "BANK")
                        {
                            ddlAdv_Location.InnerHtml = "NA";
                        }
                        string ISValid = string.Empty;
                        string str = string.Empty;
                        // DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref ISValid, span_advance.InnerHtml, "AdGLcode");

                    }

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
            // string isvalid = "";
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

                DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "selectdetails", ref isValid, span_grade.InnerHtml, "AdDesignation");
                if (dtamt != null && dtamt.Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Grade</th><th>City Class</th><th>Amount(Rs)</th><th>Effective Date</th></tr></thead>";

                    for (int i = 0; i < dtamt.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + span_grade.InnerHtml + "</td><td>" + Convert.ToString(dtamt.Rows[i]["CITY_CLASS"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["EFFECTIVE_DATE"]) + "</td></tr>";

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
   
    protected void btnRequest_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                divins.Style.Add("display", "none");

                string refData = string.Empty;
                string alert_message = string.Empty;
                string rfc_string = string.Empty;
                string cash_flag = string.Empty;
                string cash_no = string.Empty;
                string bank_flag = string.Empty;
                string bank_no = string.Empty;
                string rfc_no = "";

                DataSet dt_sap_rfc = (DataSet)ActionController.ExecuteAction("", "Advance_Return_Request.aspx", "getcashrfc", ref refData, spn_ret_no.InnerHtml);
                if (dt_sap_rfc != null)
                {
                    DataTable dtManage = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata1", ref refData, spn_ret_no.InnerHtml, "SELECT", "", "", "", "", "", "", "");
                    if (dtManage != null && dtManage.Rows.Count > 0)
                    {
                        cash_flag = Convert.ToString(dtManage.Rows[0]["CASH_FLAG"]);
                        cash_no = Convert.ToString(dtManage.Rows[0]["CASH_NO"]);
                        rfc_string = cash_flag;
                        rfc_no = cash_no;
                    }
                    if (dt_sap_rfc.Tables[1].Rows.Count > 0)
                    {
                        if (cash_flag == "E" || cash_flag == "")
                        {
                            Vendor_Portal.Vendor_Portal_DetailsService Vendor = new Vendor_Portal.Vendor_Portal_DetailsService();
                            string[] Vendor_data_array = new string[3];
                            Vendor_data_array = Vendor.CASH_LEGDER(Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][0]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][1]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][2]),
                                        Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][3]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][4]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][5]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][6]),
                                        Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][7]), "", Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][9]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][10]),
                                        Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][11]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][12]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][13]),
                                        Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][14]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][15]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][16]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][17]));
                            rfc_string = Vendor_data_array[1];
                            
                            if (rfc_string != "E" && rfc_string!="")
                            {
                                string[] sp_data1 = Convert.ToString(Vendor_data_array[0]).Split(' ');
                                for (int k = 0; k < sp_data1.Length; k++)
                                {
                                    if (Convert.ToString(sp_data1[k]).Length == 10 && rfc_no == "")
                                    {
                                        cash_no = rfc_no = Convert.ToString(sp_data1[k]);
                                        alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                    }
                                }
                            }
                            else
                            {
                                alert_message = Vendor_data_array[0];
                            }
                            string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, spn_ret_no.InnerHtml, "CASH", Convert.ToString(Vendor_data_array[1]).ToUpper(), rfc_no, Convert.ToString(Vendor_data_array[0]), "", "", "", spn_ret_no.InnerHtml);
                        }
                        else
                        {
                            rfc_string = "S";
                            alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                        }
                    }
                }
                if (rfc_string != "E" && rfc_string != "")
                {
                    condition.Text = "1";
                    advtype.Text = "1";
                    string isSaved = (string)ActionController.ExecuteAction("", "Advance_Return_Request.aspx", "update", ref refData, return_pk_id.Text, txt_pk_id.Text, condition.Text, txt_Username.Text, advtype.Text);
                    if (isSaved == null || isSaved == "" || refData.Length > 0 || isSaved == "false")
                    {
                        //string[] errmsg = refData.Split(':');
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + refData.ToString() + "')", true);
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Username.Text;

                        bool isCreate1 = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "288", "ADVANCE RETURN APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_ret_no.InnerHtml, txtWIID.Text, ref refData);
                        if (isCreate1)
                        {
                            try
                            {
                                string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Return_Request.aspx", "insertaudittrail", ref refData, txtProcessID.Text, txtInstanceID.Text, "ADVANCE RETURN APPROVAL", "USER", txt_Username.Text, "SUBMIT", spn_return_remark.InnerHtml, "0", "0");
                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Return Request has been Processed Successfully.</font></pre><p/> <pre><font size='3'>Request No: " + spn_ret_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + span_ename.InnerHtml + "</font></pre></p><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Return_Request.aspx", "insetmaildata", ref refData, txtProcessID.Text, txtInstanceID.Text, 0, "ADVANCE RETURN APPROVAL", "SUBMIT", txtEmailID.Text, "", msg, "Request No: " + spn_ret_no.InnerHtml);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {

                                string msg2 = "alert('Advance Return Request Has Been Processed Successfully : " + rfc_no + "...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                            }
                        }
                    }
                }
                else
                {
                    string msg2 = alert_message;
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                divins.Style.Add("display", "none");

                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;

                condition.Text = "2";
                advtype.Text = "1";
                string isSaved = (string)ActionController.ExecuteAction("", "Advance_Return_Request.aspx", "update", ref refData, return_pk_id.Text, txt_pk_id.Text, condition.Text, txt_Username.Text, advtype.Text);
                if (isSaved == null || isSaved == "" || refData.Length > 0 || isSaved == "false")
                {
                    //string[] errmsg = refData.Split(':');
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + refData.ToString() + "')", true);
                }
                else
                {
                    string[] Dval = new string[1];
                    Dval[0] = txt_Username.Text;

                    bool isCreate1 = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "289", "ADVANCE RETURN APPROVAL", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, spn_ret_no.InnerHtml, txtWIID.Text, ref refData);
                    if (isCreate1)
                    {
                        try
                        {
                            string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Return_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "ADVANCE RETURN APPROVAL", "USER", txt_Username.Text, "REJECT", spn_return_remark.InnerHtml, "0", "0");
                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Return Request has been Rejected Successfully.</font></pre><p/> <pre><font size='3'>Request No: " + spn_ret_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + span_ename.InnerHtml + "</font></pre></p><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                            string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Return_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ADVANCE RETURN APPROVAL", "REJECT", txtEmailID.Text, "", msg, "Request No: " + spn_ret_no.InnerHtml);
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



}