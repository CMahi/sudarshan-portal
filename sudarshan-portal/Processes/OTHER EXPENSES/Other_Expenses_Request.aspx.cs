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
using System.Web.Services;

public partial class Other_Expenses_Request : System.Web.UI.Page
{
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Other_Expenses_Request));
                if (!Page.IsPostBack)
                {

                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        if (Request.QueryString["processid"] != null && Request.QueryString["stepid"] != null)
                        {
                            txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                            txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);

                        }
                    }
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                   // span_vdate.InnerHtml = DateTime.Now.ToString("dd-MMM-yyyy");
                    getUserInfo();
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void getUserInfo()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "pgettraveluser", ref isdata, txt_Username.Text);
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
                
                span_Division.InnerHtml = Convert.ToString(dtUser.Rows[0]["DIVISION_NAME"]);
                spn_base_location.InnerHtml = Convert.ToString(dtUser.Rows[0]["BASE_LOCATION"]);
                DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "pgettravelrequestapprover", ref isdata, txt_Username.Text);
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
                        if (Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "0")
                        {
                            span_DApprover.InnerHtml = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                            span_Dapp_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);
                        }
                        else
                        {
                            span_Dapp_name.InnerHtml = span_DApprover.InnerHtml = "NA";
                        }
                        txt_Approver_Email.Text = Convert.ToString(dtApprover.Rows[0]["approver_email"]);

                    }
                    else
                    {
                        span_Approver.InnerHtml = "NA";
                    }
                }

                fillPayment_Mode();
                fillLocation();
                fillDocument_Details();
                fillAdvanceAmount();
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void fillLocation()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtLocation = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "selectlocation", ref isdata, "", "", "PAYMENT_LOCATION", "");
            ddlAdv_Location.Items.Clear();
            if (dtLocation != null)
            {
                if (dtLocation.Rows.Count > 0)
                {
                    ddlAdv_Location.DataSource = dtLocation;
                    ddlAdv_Location.DataTextField = "LOCATION NAME";
                    ddlAdv_Location.DataValueField = "PK_LOCATION_ID";
                    ddlAdv_Location.DataBind();
                }
            }
            //ddlAdv_Location.Items.Insert(0, "---Select One---");
            ddlAdv_Location.Items.Insert(0, new ListItem("---Select One---", "0"));
            ddlAdv_Location.Enabled = false;
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void fillPayment_Mode()
    {
        try
        {
            string isdata = string.Empty;
            DataTable dtPayment = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "selectlocation", ref isdata, "", "", "M_TRAVEL_PAYMENT_MODE", "");
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

    private void fillAdvanceAmount()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "pgetadvancedetails", ref isValid, txt_Username.Text, "", 1, "Other Expense Advance");
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th align='center'>#</th><th align='center'>Request No</th><th align='center'>Advance Date</th><th align='center'>Amount</th><th align='center'>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int Index = 0; Index < dt.Rows.Count; Index++)
                {
                    tblHTML.Append("<tr>");
                    tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='travel' ><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Rows[Index]["PK_ADVANCE_HDR_Id"]) + "' style='display:none'><input type='text' id='advance_amount" + (Index + 1) + "' value='" + Convert.ToString(dt.Rows[Index]["amount"]) + "' style='display:none'></td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["REQUEST_NO"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["ADVANCE_DATE"]) + "</td>");
                    tblHTML.Append("<td align='right'>" + Convert.ToString(dt.Rows[Index]["AMOUNT"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["APPROVED"]) + "</td>");
                    tblHTML.Append("</tr>");
                }
            }
            else
            {
                tblHTML.Append("<tr><td colspan='5' align='center'>Advance Not Available</td></tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            div_Advance.InnerHtml = tblHTML.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string isSaved = string.Empty;
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
		divIns.Style.Add("display","none");
                string isdata = string.Empty;
                string isInserted = string.Empty;

                string instanceID = (string)WFE.Action.StartCase(isInserted, txtProcessID.Text, txt_Username.Text, txt_Username.Text, txtEmailID.Text, "187");
                txtInstanceID.Text = instanceID;

                string xml_string = txt_xml_data.Text;
                xml_string = xml_string.Replace("&", "&amp;");
                xml_string = xml_string.Replace("'", "&apos;");
                txt_xml_data.Text = xml_string.ToString();

                string file_attach = txt_Document_Xml.Text;
                file_attach = file_attach.Replace("&", "&amp;");
                file_attach = file_attach.Replace("'", "&apos;");
                txt_Document_Xml.Text = file_attach.ToString();

                string adv_loc = "0";
                if (ddl_Payment_Mode.SelectedItem.Text.ToUpper().Trim() == "CASH")
                {
                    adv_loc = ddlAdv_Location.SelectedValue;
                }
                if (txt_Username.Text.ToUpper() != "RBRATHI" && txt_Username.Text.ToUpper() != "PRRATHI")
                {
                    isSaved = (string)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "insert", ref isdata, "", txt_Username.Text, adv_loc, ddl_Payment_Mode.SelectedValue, req_remark.Text, xml_string, file_attach, txtProcessID.Text, txtInstanceID.Text, span_Approver.InnerHtml, 1, txt_advance_id.Text);
                    if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = isdata.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = span_Approver.InnerHtml;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "188", "OTHER EXPENSE REQUEST", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, Convert.ToString(isSaved), "0", ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
                                string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "OTHER EXPENSE REQUEST", "USER", txt_Username.Text, "SUBMIT", req_remark.Text, "0", "0");
                                string emailid = string.Empty;
                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Other Expense Request Has Been Saved Successfully.</font></pre><pre><font size='3'>Request No: " + isSaved + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "OTHER EXPENSE REQUEST", "SUBMIT", txt_Approver_Email.Text, txtEmailID.Text, msg, "Request No: " + isSaved);
DataTable dt = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getfiles", ref isdata, "OTHER EXPENSES", isSaved.ToString());
                                                        if (dt.Rows.Count > 0)
                                                        {
                                                            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                                            string path = string.Empty;

                                                            string foldername = isSaved.ToString();
                                                            //foldername = foldername.Replace("-", "_");
                                                            path = activeDir + "\\" + foldername;
                                                            if (Directory.Exists(path))
                                                            {

                                                            }
                                                            else
                                                            {
                                                                Directory.CreateDirectory(path);
                                                                string[] directories = Directory.GetFiles(activeDir);

                                                                path = path + "\\";
                                                                foreach (var directory in directories)
                                                                {
                                                                    for (int i = 0; i < dt.Rows.Count; i++)
                                                                    {
                                                                        var sections = directory.Split('\\');
                                                                        var fileName = sections[sections.Length - 1];
                                                                        if (dt.Rows[i]["FILENAME"].ToString() == fileName)
                                                                        {
                                                                            System.IO.File.Move(activeDir + "\\" + fileName, path + fileName);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                            }
                            catch (Exception ex)
                            {
                                // throw;
                                FSL.Logging.Logger.WriteEventLog(false, ex);
                            }
                            finally
                            {
                                //divIns.InnerHtml = "";
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Other Expense Request Applied Successfully and Request No. is : " + isSaved + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                        //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Travel Request Applied Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                    }
                }
                else
                { 
                    /***************************************************************/
                    DataTable DTAP = new DataTable();
                            if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                            {
                                DTAP = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "getaccapprover", ref isdata, "OTHER EXPENSE PAYMENT APPROVAL", ddlAdv_Location.SelectedValue, ddl_Payment_Mode.SelectedValue);

                            }
                            else
                            {
                                DTAP = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "getaccapprover", ref isdata, "OTHER EXPENSE PAYMENT APPROVAL", 0, ddl_Payment_Mode.SelectedValue);
                            }
                            if (DTAP != null)
                            {
                                if (DTAP.Rows.Count > 0)
                                {

                                    isSaved = (string)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "insert", ref isdata, "", txt_Username.Text, adv_loc, ddl_Payment_Mode.SelectedValue, req_remark.Text, xml_string, file_attach, txtProcessID.Text, txtInstanceID.Text, span_Approver.InnerHtml, 1, txt_advance_id.Text);
                                    if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                                    {
                                        string[] errmsg = isdata.Split(':');
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                                    }
                                    else
                                    {
                                        string[] Dval = new string[1];
                                        Dval[0] = span_Approver.InnerHtml;
                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "188", "OTHER EXPENSE REQUEST", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, Convert.ToString(isSaved), "0", ref isInserted);
                                        if (isCreate)
                                        {
                                            try
                                            {
                                                string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "OTHER EXPENSE REQUEST", "USER", txt_Username.Text, "SUBMIT", req_remark.Text, "0", "0");
                                                string wiid = (string)ActionController.ExecuteAction(txt_Username.Text, "Bulk_Travel_Expense_Doc_Verification.aspx", "getpktransid", ref isInserted, txtProcessID.Text, txtInstanceID.Text);
                                                string[] Dval1 = new string[DTAP.Rows.Count];
                                                if (DTAP.Rows.Count > 0)
                                                {
                                                    for (int i = 0; i < DTAP.Rows.Count; i++)
                                                    {
                                                        Dval1[i] = Convert.ToString(DTAP.Rows[i]["USER_ADID"]);
                                                        if (txt_Approver_Email.Text == "")
                                                        {
                                                            txt_Approver_Email.Text = Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                                                        }
                                                        else
                                                        {
                                                            txt_Approver_Email.Text = txt_Approver_Email.Text + ',' + Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                                                        }
                                                    }
                                                }
                                                isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "189", "OTHER EXPENSE APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, isSaved, wiid, ref isInserted);
                                                if (isCreate)
                                                {
                                                    try
                                                    {
                                                        auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "OTHER EXPENSE APPROVAL", "USER", txt_Username.Text, "SUBMIT", "Approve", "0", "0");

                                                        string msg = "";
                                                        CryptoGraphy crypt = new CryptoGraphy();
                                                        string process_name = crypt.Encryptdata("OTHER EXPENSES");
                                                        string req_no = crypt.Encryptdata(isSaved);
                                                        if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                                                        {
                                                            msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Other Expense Request Has Been Submitted Successfully and Sent For Payment Approval.</span></pre><pre><span style='font-size: medium;'>Request No: " + isSaved + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://"+compname+"/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://"+compname+"/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                        }
                                                        else
                                                        {
                                                            msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Other Expense Request Has Been Submitted Successfully and Sent For Payment Approval.</span></pre><pre><span style='font-size: medium;'>Request No: " + isSaved + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://"+compname+"/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://"+compname+"/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                        }

                                                        string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "OTHER EXPENSE APPROVAL", "SUBMIT", txtEmailID.Text, txt_Approver_Email.Text, msg, "Request No: " + isSaved);
DataTable dt = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getfiles", ref isdata, "OTHER EXPENSES", isSaved.ToString());
                                                        if (dt.Rows.Count > 0)
                                                        {
                                                            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                                            string path = string.Empty;

                                                            string foldername = isSaved.ToString();
                                                            //foldername = foldername.Replace("-", "_");
                                                            path = activeDir + "\\" + foldername;
                                                            if (Directory.Exists(path))
                                                            {

                                                            }
                                                            else
                                                            {
                                                                Directory.CreateDirectory(path);
                                                                string[] directories = Directory.GetFiles(activeDir);

                                                                path = path + "\\";
                                                                foreach (var directory in directories)
                                                                {
                                                                    for (int i = 0; i < dt.Rows.Count; i++)
                                                                    {
                                                                        var sections = directory.Split('\\');
                                                                        var fileName = sections[sections.Length - 1];
                                                                        if (dt.Rows[i]["FILENAME"].ToString() == fileName)
                                                                        {
                                                                            System.IO.File.Move(activeDir + "\\" + fileName, path + fileName);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                        throw;
                                                    }
                                                    finally
                                                    {
                                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Other Expense Request Submitted Successfully and Request No. is : " + isSaved + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                    }
                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                // throw;
                                                FSL.Logging.Logger.WriteEventLog(false, ex);
                                            }
                                            finally
                                            {
                                               //divIns.InnerHtml = "";
                                                
                                            }
                                        }
                                    
                                    }

                                }
                                else
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payment Approver Not Available For Payment Mode : " + ddl_Payment_Mode.SelectedItem.Text + "  ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                            else
                            {
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payment Approver Not Available For Payment Mode : " + ddl_Payment_Mode.SelectedItem.Text + "  ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                            }
                    /***************************************************************/
                }

            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc);  }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;

            path = activeDir + "\\";

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

            FileUpload1.SaveAs(path + filename);

        }
        catch (Exception Ex)
        {
            Logger.WriteEventLog(false, Ex);

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

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>Description</th><th>File Name</th><th>Delete</th></tr></thead>";
                DisplayData += "</table>";
                div_docs.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string dropDownData()
    {
        string isvalid = "";
        string ret_data = "";
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "getreimbursements", ref isvalid, "Other Expenses");
        ret_data = "<option Value='0'>---Select One---</option>";
        if (dt != null && dt.Rows.Count > 0)
        {
            for(int i=0;i<dt.Rows.Count;i++)
            {
                ret_data += "<option Value='" + Convert.ToString(dt.Rows[i]["fk_expense_id"]) + "'>" + Convert.ToString(dt.Rows[i]["expense_head"]) + "</option>";
            }
        }

        return ret_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getGLCode(int pk_id)
    {
        string isvalid = "";
        string gl_code = (string)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "getglcode", ref isvalid, pk_id);
        if (gl_code=="")
        {
            gl_code = "---";
        }
        return gl_code;
    }
}