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

public partial class Advance_Request_Modification_Foreign : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Request_Modification_Foreign));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["step"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                        {
                            txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                            txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                            txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                            txt_Step.Text = Convert.ToString(Request.QueryString["step"]);
                            txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);

                        }
                    }

                    fillAuditTrailData();

                    Initialization();
                    fillDocument_Details();
                }

            }
        }
        catch (Exception Exc) { }
    }

    public void fillPolicy_Details()
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

                isValid = string.Empty;
                DataTable dtamt1 = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref isValid, txt_designation.Text, "AdDesignationForeign");

                if (dtamt1 != null && dtamt1.Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Designation</th><th>Country</th><th>Amount(Foreign Currency)</th><th>Effective Date</th></tr></thead>";

                    for (int i = 0; i < dtamt1.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + span_designation.InnerHtml + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["country"]) + "</td><td style='text-align:right'>" + Convert.ToString(dtamt1.Rows[i]["AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["EFFECTIVE_DATE"]) + "</td></tr>";

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

    private void fillDocument_Details()
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

                DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "getfilenames", ref isData, "ADVANCE", spn_req_no.InnerHtml);

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Type</th><th>File Name</th><th>Delete</th></tr></thead>";
                if (dsData != null)
                {
                    for (int i = 0; i < dsData.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dsData.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles(" + (i + 1) + ")>" + Convert.ToString(dsData.Rows[i]["filename"]) + "</a></td><td><i id='del" + (i + 1) + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (i + 1) + "," + (dsData.Rows[i]["PK_ID"]) + ");\" ></td><td style='display:none'>" + Convert.ToString(dsData.Rows[i]["OBJECT_VALUE"]) + "</td></tr>";
                    }
                }
                DisplayData += "</table>";
                div_Doc.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    private void Initialization()
    {
        try
        {
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "getdetails", ref isdata, txtProcessID.Text, txtInstanceID.Text);
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_ADVANCE_F_HDR_ID"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                txt_Initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMAIL_ID"]);

                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "pgettraveluser", ref isdata, txt_Initiator.Text);
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
                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "pgettravelrequestapprover", ref isdata, txt_Initiator.Text);
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
                    fillPolicy_Details();
                    fillData(dsData.Tables[0]);

                    //string ISValid = string.Empty;
                    //string str = string.Empty;
                    //DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref ISValid, span_advance.InnerHtml, "AdGLcode");
                    //showall(Convert.ToString(dsData.Tables[0].Rows[0]["advance_for"]));
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }

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
divIns.Style.Add("display","none");
                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;
                txt_Condition.Text = "1";
                txt_Action.Text = "Submit";
                txt_Audit.Text = "FOREIGN ADVANCE REQUEST MODIFY";
                string isSaved = string.Empty;
                string remark = string.Empty;

                string vehiclexml_string = txt_xml_data_vehicle.Text;
                vehiclexml_string = vehiclexml_string.Replace("&", "&amp;");
                vehiclexml_string = vehiclexml_string.Replace(">", "&gt;");
                vehiclexml_string = vehiclexml_string.Replace("<", "&lt;");
                vehiclexml_string = vehiclexml_string.Replace("||", ">");
                vehiclexml_string = vehiclexml_string.Replace("|", "<");
                vehiclexml_string = vehiclexml_string.Replace("'", "&apos;");
                txt_xml_data_vehicle.Text = vehiclexml_string.ToString();


                isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Modification_Foreign.aspx", "modify", ref refData, Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), txt_pk_id.Text, txt_xml_data_vehicle.Text, txt_Username.Text, txt_Audit.Text, txt_Action.Text, txt_Document_Xml.Text, txt_remark_hdr.Text);
                if (isSaved == null || refData.Length > 0 || isSaved == "false")
                {
                    string[] errmsg = refData.Split(':');
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                }
                else
                {
                    if ((txt_Username.Text).ToLower() != "rbrathi" && (txt_Username.Text).ToLower() != "prrathi")
                    {
                        string[] Dval1 = new string[1];
                        Dval1[0] = span_Approver.InnerHtml;
                        if (txtApproverEmail.Text == "")
                        {
                            txtApproverEmail.Text = txtEmailID.Text;
                        }
                        string ref_data2 = string.Empty;
                        string release_id = (string)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "getreleaseid", ref ref_data2, txtProcessID.Text, txt_Step.Text, "SUBMIT");
                        if (release_id != "")
                        {
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, txt_Step.Text, "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, txt_Request.Text, txtWIID.Text, ref isInserted);

                            if (isCreate)
                            {
                                try
                                {
                                    string emailid = string.Empty;
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been modified and sent for your approval.</font></pre><p/><pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MODIFY ADVANCE REQUEST", "SUBMIT", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                }
                                catch (Exception ex)
                                {
                                    // throw;
                                    FSL.Logging.Logger.WriteEventLog(false, ex);
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been modified and sent for approval....!'); window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                    else
                    {
                        //string[] Request_Unique = isSaved.Split('=');
                        //txt_Request.Text = Request_Unique[0];
                        string ref_data = string.Empty;
                        DataTable DTAP = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "getaccapprover", ref ref_data, "ADVANCE REQUEST FOREIGN ACCOUNT PAYABLE APPROVAL", 0, "");

                        if (DTAP != null)
                        {
                            if (DTAP.Rows.Count > 0)
                            {

                                string[] Dval = new string[DTAP.Rows.Count];
                                Dval[0] = "";
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
                                    string ref_data2 = string.Empty;

                                    string release_id = (string)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "getreleaseid", ref ref_data2, txtProcessID.Text, txt_Step.Text, "SUBMIT-MD");
                                    if (release_id != "")
                                    {
                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, txt_Step.Text, "SUBMIT-MD", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);

                                        if (isCreate)
                                        {
                                            try
                                            {
                                                string isValid = string.Empty;
                                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been modified and sent to Accounts for payment approval.</font></pre><p/><pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST", "APPROVE", txtApproverEmail.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);


                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been modified And sent to accounts for payment approval.!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    public void fillData(DataTable dt)
    {
        try
        {
            String IsData = string.Empty;
            StringBuilder sb = new StringBuilder();

            //advance for foreign

            string Isdata2 = string.Empty;

            DataTable dtcountry = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Modification_Foreign.aspx", "selectdetails", ref Isdata2, "", "AdCountry");
            {
                if (dtcountry == null && dtcountry.Rows.Count > 0)
                    sb.Append("<div class='col-md-12' id='advance_foreign'>");
                sb.Append("<div class='col-md-12' id='advance_foreign'>");
                sb.Append("<div class='form-horizontal'>");
                sb.Append("<div class='form-group'><div class='col-md-1'></div>");
                sb.Append("<label class='col-md-2'>Region From</label>");
                sb.Append("<div class='col-md-2'>");

                if (Convert.ToString(dt.Rows[0]["FCOUNTRYNAME"]) != "0" || Convert.ToString(dt.Rows[0]["FCOUNTRYNAME"]) != "")
                {
                    sb.Append("<select ID='ddl_country_from' runat='server' class='form-control input-sm' onchange='chk_country_From(1)'>");
                    sb.Append("<option Value='0'>---Select One---</option>");


                    for (int i = 0; i < dtcountry.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[0]["FK_COUNTRY_FRM_ID"]) == Convert.ToString(dtcountry.Rows[i]["PK_COUNTRY_ID"]))
                        {
                            sb.Append("<option Value='" + Convert.ToString(dtcountry.Rows[i]["PK_COUNTRY_ID"]) + "' Selected='true'>" + Convert.ToString(dtcountry.Rows[i]["COUNTRY_NAME"]) + "</option>");
                        }
                        else
                        {
                            sb.Append("<option Value='" + Convert.ToString(dtcountry.Rows[i]["PK_COUNTRY_ID"]) + "'>" + Convert.ToString(dtcountry.Rows[i]["COUNTRY_NAME"]) + "</option>");
                        }
                    }
                    sb.Append("</select>");
                }
                sb.Append("</div>");
                sb.Append("<div class='col-md-1'></div>");
                sb.Append("<label class='col-md-2'>Region To</label>");
                sb.Append("<div class='col-md-2'>");
                DataTable dtcountryTO = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Modification_Foreign.aspx", "selectdetails", ref Isdata2, "", "AdCountryto");
                if (Convert.ToString(dt.Rows[0]["TCOUNTRYNAME"]) != "0" || Convert.ToString(dt.Rows[0]["TCOUNTRYNAME"]) != "")
                {
                    sb.Append("<select ID='ddl_country_to' runat='server' class='form-control input-sm' onchange='chk_country_To()'>");
                    sb.Append("<option Value='0'>---Select One---</option>");


                    for (int i = 0; i < dtcountryTO.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[0]["FK_COUNTRY_TO_ID"]) == Convert.ToString(dtcountryTO.Rows[i]["PK_COUNTRY_ID"]))
                        {
                            sb.Append("<option Value='" + Convert.ToString(dtcountryTO.Rows[i]["PK_COUNTRY_ID"]) + "' Selected='true'>" + Convert.ToString(dtcountryTO.Rows[i]["COUNTRY_NAME"]) + "</option>");
                        }
                        else
                        {
                            sb.Append("<option Value='" + Convert.ToString(dtcountryTO.Rows[i]["PK_COUNTRY_ID"]) + "'>" + Convert.ToString(dtcountryTO.Rows[i]["COUNTRY_NAME"]) + "</option>");
                        }
                    }
                    sb.Append("</select>");
                }

                sb.Append("</div>");
                sb.Append("</div>");


            }


            if (dt != null && dt.Rows.Count > 0)
            {


                sb.Append("<div class='form-group'><div class='col-md-1'></div>");
                sb.Append("<label class='col-md-2'>City From</label>");
                sb.Append("<div class='col-md-2'>");
                sb.Append("<select ID='ddl_city_from' runat='server' class='form-control input-sm' onchange='chk_class_To(1);'>");
                sb.Append("<option Value='0'>---Select One---</option>");

                DataTable dtcity = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Modification_Foreign.aspx", "selectdetails", ref IsData, Convert.ToString(dt.Rows[0]["FK_COUNTRY_FRM_ID"]), "AdCityForeign");
                if (Convert.ToString(dt.Rows[0]["FK_CITY_FRM_ID"]) == "-1")
                {

                    for (int i = 0; i < dtcity.Rows.Count; i++)
                    {
                        sb.Append("<option Value='" + Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtcity.Rows[i]["NAME"]) + "</option>");
                    }
                    sb.Append("<option Value='-1' Selected='true'>Other</option>");
                    sb.Append("</select>");
                    sb.Append("<input type='text' class='form-control input-sm' id='txt_fplace' value='" + Convert.ToString(dt.Rows[0]["PLACE_FROM_OTHER"]) + "'>");
                }
                else
                {
                    for (int i = 0; i < dtcity.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[0]["FK_CITY_FRM_ID"]) == Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]))
                        {
                            sb.Append("<option Value='" + Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]) + "' Selected='true'>" + Convert.ToString(dtcity.Rows[i]["NAME"]) + "</option>");
                        }
                        else
                        {
                            sb.Append("<option Value='" + Convert.ToString(dtcity.Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtcity.Rows[i]["NAME"]) + "</option>");
                        }
                    }
                    sb.Append("<option Value='-1'>Other</option>");
                    sb.Append("</select>");
                    sb.Append("<input type='text' style='display:none' class='form-control input-sm' id='txt_fplace' value='" + Convert.ToString(dt.Rows[0]["PLACE_FROM_OTHER"]) + "'>");
                }

                sb.Append("</select>");
                sb.Append("</div>");


                sb.Append("<div class='col-md-1'></div><label class='col-md-2'>City To</label>");
                sb.Append("<div class='col-md-2'>");

                sb.Append("<select ID='ddl_city_to' runat='server' class='form-control input-sm' onchange='chk_city_To(1);'>");
                sb.Append("<option Value='0'>---Select One---</option>");

                DataTable dtcity1 = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Modification_Foreign.aspx", "selectdetails", ref IsData, Convert.ToString(dt.Rows[0]["FK_COUNTRY_TO_ID"]), "AdCityForeign");
                if (Convert.ToString(dt.Rows[0]["FK_CITY_TO_ID"]) == "-1")
                {

                    for (int i = 0; i < dtcity1.Rows.Count; i++)
                    {
                        sb.Append("<option Value='" + Convert.ToString(dtcity1.Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtcity1.Rows[i]["NAME"]) + "</option>");
                    }
                    sb.Append("<option Value='-1' Selected='true'>Other</option>");
                    sb.Append("</select>");
                    sb.Append("<input type='text' class='form-control input-sm' id='txt_tother' value='" + Convert.ToString(dt.Rows[0]["PLACE_TO_OTHER"]) + "'>");
                }
                else
                {
                    for (int i = 0; i < dtcity1.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[0]["FK_CITY_TO_ID"]) == Convert.ToString(dtcity1.Rows[i]["PK_CITY_ID"]))
                        {
                            sb.Append("<option Value='" + Convert.ToString(dtcity1.Rows[i]["PK_CITY_ID"]) + "' Selected='true'>" + Convert.ToString(dtcity1.Rows[i]["NAME"]) + "</option>");
                        }
                        else
                        {
                            sb.Append("<option Value='" + Convert.ToString(dtcity1.Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtcity1.Rows[i]["NAME"]) + "</option>");
                        }
                    }
                    sb.Append("<option Value='-1'>Other</option>");
                    sb.Append("</select>");
                    sb.Append("<input type='text'  style='display:none' class='form-control input-sm' id='txt_tother' value='" + Convert.ToString(dt.Rows[0]["PLACE_TO_OTHER"]) + "'>");
                }
                sb.Append("</select>");


            }

            sb.Append("</div>");
            sb.Append("</div>");


            sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'>Currency</label>");
            sb.Append("<div class='col-md-2'>");
            sb.Append("<select ID='ddl_currency' runat='server' class='form-control input-sm'>");
            sb.Append("<option Value='0'>---Select One---</option>");

            DataTable dtcurrency = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Modification_Foreign.aspx", "selectdetails", ref IsData, "", "AdCurrency");

            if (Convert.ToString(dt.Rows[0]["CURRENCY"]) != "")
            {

                for (int i = 0; i < dtcurrency.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[0]["CURRENCY"]) == Convert.ToString(dtcurrency.Rows[i]["CURRENCY"]))
                    {
                        sb.Append("<option Value='" + Convert.ToString(dtcurrency.Rows[i]["CURRENCY"]) + "' Selected='true'>" + Convert.ToString(dtcurrency.Rows[i]["CURRENCY"]) + "</option>");
                    }
                    else
                    {
                        sb.Append("<option Value='" + Convert.ToString(dtcurrency.Rows[i]["CURRENCY"]) + "'>" + Convert.ToString(dtcurrency.Rows[i]["CURRENCY"]) + "</option>");
                    }
                }

                sb.Append("</select>");
                sb.Append("</div>");

                sb.Append("</div>");

            }
            sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'>From Date</label><div class='col-md-2'>");
            sb.Append("<div class='input-group'> <input type='text' class='form-control datepicker-dropdown'  id='txt_form_Date' readonly=''  value='" + Convert.ToDateTime(dt.Rows[0]["from_date"]).ToString("dd-MMM-yyyy") + "'  runat='server'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button> </span></div>");
            sb.Append("</div>");

            sb.Append("<div class='col-md-1'></div><label class='col-md-2'>To Date</label><div class='col-md-2'>");
            sb.Append("<div class='input-group'> <input type='text' class='form-control datepicker-dropdown'  id='txt_to_date' readonly=''  value='" + Convert.ToDateTime(dt.Rows[0]["to_date"]).ToString("dd-MMM-yyyy") + "'  runat='server'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button> </span></div>");
            sb.Append("</div></div>");


            sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'>Allowed Amount</label><div class='col-md-2'>");
            string amount = string.Empty;
            amount = dt.Rows[0]["allowed_amount"].ToString();


            sb.Append("<label ID='lbl_allowedamt' runat='server' style='align:right' >" + Convert.ToInt32(dt.Rows[0]["allowed_amount"]) + "</label>");
            sb.Append("</div>");
            sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Amount(Foreign Currency)</label><div class='col-md-2'>");
            sb.Append("<div class='input-group'> <input type='text' class='form-control input-sm' id='txt_f_amount' style='text-align:right' runat='server' onkeypress='return isNumberKey(event)' onchange='checkamount();' value=' " + Convert.ToInt32(dt.Rows[0]["amount"]) + "'/></div><div class='col-md-1'></div></div></div>");
            txtDummy.Text = Convert.ToString(dt.Rows[0]["amount"]);


            sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'>Payment Mode(Currency)</label><div class='col-md-3'>");
            sb.Append("<div class='input-group'> <input type='text' class='form-control input-sm' id='txt_Curncy_amount' runat='server' style='text-align:right' onkeypress='return isNumberKey(event)' onchange='checkamount1();' value='" + Convert.ToInt32(dt.Rows[0]["CURRENCY_AMOUNT"]) + "'/></div><div class='col-md-1'></div>");

            sb.Append("</div>");
            sb.Append("<label class='col-md-2'>Payment Mode(Forex Card)</label><div class='col-md-3'>");
            sb.Append("<div class='input-group'> <input type='text' class='form-control input-sm' id='txt_forexcard_amount' runat='server' style='text-align:right' onkeypress='return isNumberKey(event)' onchange='checkamount1();' value='" + Convert.ToInt32(dt.Rows[0]["FOREX_CARD"]) + "'/></div><div class='col-md-1'></div></div></div>");


            sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label>");
            sb.Append("<div class='col-md-6'><textarea type='text' maxlength='400' class='form-control' rows='2' id='txt_F_remark' runat='server' >" + Convert.ToString(dt.Rows[0]["remark"]) + "</textarea></div>");
            //   txt_remark.Text = Convert.ToString(dt.Rows[0]["remark"]);
            sb.Append("</div></div></div>");
            div_Forein.InnerHtml = Convert.ToString(sb);

            ///////////////////////validation for parameter/////////////////////////////////////
            string Isdata1 = string.Empty;
            int openadv = 0;
            dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Modification_Foreign.aspx", "selectdetails", ref Isdata1, txt_Username.Text, "AdExParameter");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (dt.Rows[i]["PARAMETER"].ToString() == "Foreign Advance Amount")
                    {
                        txt_adamount.Text = (dt.Rows[i]["VALUE"].ToString());
                    }
                    if (dt.Rows[i]["PARAMETER"].ToString() == "Foreign Open Adavances")
                    {
                        if ((dt.Rows[i]["VALUE"]) != null || (dt.Rows[i]["VALUE"]) != "undefined")
                        {
                            openadv = Convert.ToInt32(dt.Rows[i]["VALUE"]);
                        }
                    }
                    if (dt.Rows[i]["PARAMETER"].ToString() == "Foreign Advance Period")
                    {
                        txt_adperiod.Text = (dt.Rows[i]["VALUE"].ToString());
                    }
                }
            }
            string IsData6 = string.Empty;
            int dts = 0; Double amttotal = 0;
            DataTable dtid = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "getadvanids", ref IsData6, txt_Username.Text);
            if (dtid != null)
            {
                for (int i = 0; i < dtid.Rows.Count; i++)
                {
                    string dtvalue = (string)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "getperiod", ref IsData6, txt_Username.Text, dtid.Rows[i]["PK_ADVANCE_F_HDR_ID"].ToString(), Convert.ToInt64(txt_adperiod.Text));
                    string[] result = dtvalue.Split('=');
                    if (result[0] != null)
                    {
                        dts = Convert.ToInt32(dts) + Convert.ToInt32(result[0]);
                    }
                    if (result[1] != null)
                    {
                        if (result[1] == "true")
                        {
                            txt_expire.Text = "1";
                        }
                        else
                        {
                            txt_expire.Text = "0";
                        }
                    }
                    if (result[2] != null)
                    {
                        amttotal = Convert.ToInt32(amttotal) + Convert.ToInt32(result[2]);
                        total_amount.Text = Convert.ToString(amttotal);
                    }

                    total_amount.Text = (Convert.ToDouble(total_amount.Text) - Convert.ToDouble(txtDummy.Text)).ToString();
                }
            }

            if (dts != 0)
            {
                if ((dts) >= (openadv - 1))
                {
                    txt_opcount.Text = "1";
                }
            }
            if (dts >= openadv)
            {
                txt_opencount.Text = "1";
            }
            else
            {
                txt_opencount.Text = "0";
            }

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string fillAmountall(string desg, string tocity)
    {
        ////get amount from designation//////////////////////////////
        string IsData1 = string.Empty;
        string str = string.Empty;
        DataTable dta = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Modification.aspx", "getallamount", ref IsData1, desg, tocity);
        if (dta != null && dta.Rows.Count > 0)
        {
            str = dta.Rows[0]["AMOUNT"].ToString();
        }
        return str;

    }

    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            Int32 flength = FileUpload1.PostedFile.ContentLength;
            if (flength == 0)
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Sorry cannot upload file ! File is Empty or file does not exist');}</script>");
            }
            else if (flength > 4268064)
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Sorry cannot upload file ! File Size Exceeded.');}</script>");
            }
            else if (FileUpload1.PostedFile.ContentType == "application/octet-stream/vnd.ms-outlook")
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Kindly Check File Type.');}</script>");
            }
            else
            {
                string activeDir = GetData.getDirPath(txt_Request.Text.ToString());
                if (activeDir != "" && activeDir != null)
                {
                    string path = string.Empty;
                    path = activeDir + "\\";
                    string dates = "";
                    string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());

                    filename = filename.Replace("(", "");
                    filename = filename.Replace(")", "");
                    filename = filename.Replace("&", "");
                    filename = filename.Replace("+", "");
                    filename = filename.Replace("/", "");
                    filename = filename.Replace("\\", "");
                    filename = filename.Replace("'", "");
                    filename = filename.Replace("  ", "");
                    filename = filename.Replace(" ", "");
                    filename = filename.Replace("#", "");
                    filename = filename.Replace("$", "");
                    filename = filename.Replace("~", "");
                    filename = filename.Replace("%", "");
                    filename = filename.Replace("''", "");
                    filename = filename.Replace(":", "");
                    filename = filename.Replace("*", "");
                    filename = filename.Replace("?", "");
                    filename = filename.Replace("<", "");
                    filename = filename.Replace(">", "");
                    filename = filename.Replace("{", "");
                    filename = filename.Replace("}", "");
                    filename = filename.Replace(",", "");
                    DataTable dt = (DataTable)Session["UploadedFiles"];

                    FileUpload1.PostedFile.SaveAs(path + filename);
                    ClearContents(sender as Control);


                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Document Uploaded Successfully');}</script>");
                }
            }
        }
        catch (Exception Ex)
        {
            Logger.WriteEventLog(false, Ex);
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

    //added by pradeep

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string getCountryFromWiseCityFrom(int ddlcntfrm)
    {

        string ret_val = string.Empty;
        string is_data = string.Empty;
        string isValid = string.Empty;
        try
        {
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "getcountrywisecity", ref isValid, ddlcntfrm);

            ret_val += "0$$---Select One---";
            if (dt != null)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    ret_val += "@@" + Convert.ToString(dt.Tables[0].Rows[i]["PK_CITY_ID"]) + "$$" + Convert.ToString(dt.Tables[0].Rows[i]["NAME"]);
                }
                ret_val += "@@" + "-1" + "$$" + "Other";
            }


        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return ret_val;

    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static double fillAmountForeign(string desg, string tocountry, string currency)
    {

        string IsData1 = string.Empty;
        string IsData2 = string.Empty;
        double str = 0.00;
        double exchngrate = 0.00;
        double famount = 0.00;
        DataTable dta = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "getallamountforeing", ref IsData1, desg, tocountry);
        if (dta != null && dta.Rows.Count > 0)
        {
            if (tocountry == "4") //|| tocountry == "9" || tocountry == "15"
            {
              
                famount = Convert.ToDouble(dta.Rows[0]["F_AMOUNT"].ToString());
                DataTable dtexchrate = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Foreign.aspx", "getexchangerate", ref IsData2, currency);
                exchngrate = Convert.ToDouble(dtexchrate.Rows[0]["EXCHANGE_RATE"].ToString());
              
                str = famount / exchngrate;


            }
            else
            {
                str = Convert.ToDouble(dta.Rows[0]["F_AMOUNT"].ToString());
            }
        }
        return str;

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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string deletedoctbl(int pkid)
    {
        string filename = string.Empty;
        string isdata = string.Empty;
        try
        {
            filename = (string)ActionController.ExecuteAction("", "Advance_Request_Modification_Foreign.aspx", "deletefile", ref isdata, pkid);
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return filename;
    }

}

