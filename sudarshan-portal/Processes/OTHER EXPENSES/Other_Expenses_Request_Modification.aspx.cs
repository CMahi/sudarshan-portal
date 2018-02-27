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

public partial class Other_Expenses_Request_Modification : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Other_Expenses_Request_Modification));
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
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
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
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetrequestdata", ref isdata, txtWIID.Text);
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["pk_other_expense_hdr_id"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["voucher_id"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                txt_Initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["email_id"]);
                req_remark.Text = Convert.ToString(dsData.Tables[0].Rows[0]["remark"]);
                txt_advance_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["advance_id"]);


                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetuser", ref isdata, txt_Initiator.Text);
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
                    //span_branch.InnerHtml = Convert.ToString(dtUser.Rows[0]["BRANCH_NAME"]);
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

                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetrequestapprover", ref isdata, txt_Initiator.Text);
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
                                span_Dapp_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);
                            }
                            else
                            {
                                span_Dapp_name.InnerHtml = doa_user.Text = "NA";
                            }
                            doa_email.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver_email"]);
                        }
                        else
                        {
                            span_Approver.InnerHtml = doa_user.Text = "NA";
                        }
                    }

                    fillDocument_Details();
                    //pmode_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["Payment_mode"]);
                    //loc_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["cash_location"]);
                    
                    fillPayment_Mode();
                    fillLocation();

                    ddl_Payment_Mode.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["Payment_mode"]);
                    if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                    {
                        ddlAdv_Location.Enabled = true;
                        pl.Style.Add("display","normal");
                        pld.Style.Add("display", "normal");
                    }
                    else
                    {
                        ddlAdv_Location.Enabled = false;
                        pl.Style.Add("display", "none");
                        pld.Style.Add("display", "none");
                    }
                    ddlAdv_Location.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["cash_location"]);

                    fillAdvanceAmount();
                    fillRequest_data(dsData.Tables[1]);
                    fillAuditTrail();
                }
            }
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
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "pgetadvancedetails", ref isValid, txt_Initiator.Text, txt_pk_id.Text, 3, "Other Expense Advance");
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Amount</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            for (int Index = 0; Index < dt.Rows.Count; Index++)
            {
                tblHTML.Append("<tr>");
                if (txt_advance_id.Text == Convert.ToString(dt.Rows[Index]["PK_ADVANCE_HDR_ID"]))
                {
                    tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='travel' checked ><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Rows[Index]["PK_ADVANCE_HDR_ID"]) + "' style='display:none'><input type='text' id='advance_amount" + (Index + 1) + "' value='" + Convert.ToString(dt.Rows[Index]["amount"]) + "' style='display:none'></td>");
                }
                else
                {
                    tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='travel' ><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Rows[Index]["PK_ADVANCE_HDR_ID"]) + "' style='display:none'><input type='text' id='advance_amount" + (Index + 1) + "' value='" + Convert.ToString(dt.Rows[Index]["amount"]) + "' style='display:none'></td>");
                }
                tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["REQUEST_NO"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["ADVANCE_DATE"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["amount"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["Approved"]) + "</td>");
                tblHTML.Append("</tr>");
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

    protected void fillDocument_Details()
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
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetfilenames", ref isData, "OTHER EXPENSES", spn_req_no.InnerHtml);
                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>Description</th><th>File Name</th></tr></thead>";
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DisplayData += "<tr>";
                        DisplayData += "<td><input type='text' id='descr" + (i + 1) + "' value='" + Convert.ToString(dt.Rows[i]["DOCUMENT_TYPE"]) + "' style='display:none'/>" + Convert.ToString(dt.Rows[i]["DOCUMENT_TYPE"]) + "</td>";
                        DisplayData += "<td><input class='hidden' type='text' name='txt_Region_Add" + (i + 1) + "' id='txt_Document_File" + (i + 1) + "' value=" + Convert.ToString(dt.Rows[i]["FILENAME"]) + " readonly ></input><a id='a_downloadfiles" + i + "' style='cursor: pointer' onclick=\"return downloadfiles('" + Convert.ToString(dt.Rows[i]["FILENAME"]) + "');\" >" + Convert.ToString(dt.Rows[i]["FILENAME"]) + "</a></td>";
                        DisplayData += "</tr>";
                    }
                }
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

    protected void fillRequest_data(DataTable dt)
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
                decimal total_Amount = 0;
                DisplayData = "<table class='table table-bordered' id='tbl_Data'><thead><tr class='grey'> <th style='width:2%'>#</th><th style='width:10%; text-align:center'>Expense Head</th><th style='width:10%; text-align:center'>Date</th><th style='width:10%; text-align:center'>Bill No</th><th style='width:10%; text-align:center'>Bill Date</th><th style='width:15%; text-align:center'>Particulars</th><th style='width:10%; text-align:center'>Amount</th><th colspan='2' style='width:10%'>Action</th></tr></thead><tbody>";
                if (dt != null)
                {
                   
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DisplayData += "<tr>";
                        DisplayData += "<td align='center'><span id='index" + (i + 1) + "'>" + (i + 1) + "</span></td>";
                        DisplayData += "<td align='center'><select id='ddlExp_Head" + (i + 1) + "' class='form-control input-sm' onchange='changeGL(" + (i + 1) + ")'><option Value='0'>---Select One---</option>";
                        DataTable dte = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "getreimbursements", ref isValid, "Other Expenses");

                        if (dte != null && dte.Rows.Count > 0)
                        {
                            for (int j = 0; j < dte.Rows.Count; j++)
                            {
                                if (Convert.ToString(dt.Rows[i]["PK_EXPENSE_HEAD_ID"]) == Convert.ToString(dte.Rows[j]["fk_expense_id"]))
                                {
                                    DisplayData += "<option Value='" + Convert.ToString(dte.Rows[j]["fk_expense_id"]) + "' Selected='true'>" + Convert.ToString(dte.Rows[j]["expense_head"]) + "</option>";
                                }
                                else
                                {
                                    DisplayData += "<option Value='" + Convert.ToString(dte.Rows[j]["fk_expense_id"]) + "'>" + Convert.ToString(dte.Rows[j]["expense_head"]) + "</option>";
                                }
                            }
                        }
                        DisplayData += "<select></td>";
                        DisplayData += "<td align='center' style='display:none'><span id='spn_GL" + (i + 1) + "'>" + Convert.ToString(dt.Rows[i]["SAP_GLCode"]) + "</span></td>";
                        DisplayData += "<td align='center'><input class='form-control input-sm datepicker-rtl ' type='text' id='date" + (i + 1) + "' Value='" + Convert.ToString(dt.Rows[i]["date"]) + "' readonly></input></td>";
                        DisplayData += "<td align='center'><input class='form-control input-sm' type='text' id='bill_no" + (i + 1) + "' Value='" + Convert.ToString(dt.Rows[i]["billno"]) + "'></input></td>";
                        DisplayData += "<td align='center'><input class='form-control input-sm datepicker-rtl ' type='text' id='bill_date" + (i + 1) + "' Value='" + Convert.ToString(dt.Rows[i]["bill_date"]) + "' readonly ></input></td>";
                        DisplayData += "<td align='center'><input class='form-control input-sm' type='text' id='remark" + (i + 1) + "' Value='" + Convert.ToString(dt.Rows[i]["remark"]) + "'></input></td>";
                        DisplayData += "<td style='text-align:right'><input class='form-control input-sm numbersOnly' type='text' Value='" + Convert.ToString(dt.Rows[i]["amount"]) + "' style='text-align:right' id='amount" + (i + 1) + "' onkeyup='calculate_Total()'></input></td>";

                        /*
                        DisplayData += "<td align='center'><select id='supp_doc" + (i + 1) + "' class='form-control input-sm' onchange='enable_disable_field(" + (i + 1) + ")'><option Value='0'>---Select One---</option>";
                        if (Convert.ToString(dt.Rows[i]["supp"]) == "Y")
                        {
                            DisplayData += "<option Value='Y' Selected='true'>Yes</option><option Value='N'>No</option>";
                        }
                        else
                        {
                            DisplayData += "<option Value='Y'>Yes</option><option Value='N' Selected='true'>No</option>";
                        }
                        DisplayData += "<select></td>";
                        if (Convert.ToString(dt.Rows[i]["supp"]) == "Y")
                        {
                            DisplayData += "<td align='center'><input class='form-control input-sm' type='text' id='supp_rem" + (i + 1) + "' Value='" + Convert.ToString(dt.Rows[i]["supp_particulars"]) + "'></input></td>";    
                        }
                        else
                        {
                            DisplayData += "<td align='center'><input class='form-control input-sm' type='text' id='supp_rem" + (i + 1) + "' Value='' style='display:none'></input></td>";
                        }
                            */
                        
                        if (i + 1 < dt.Rows.Count)
                        {
                            DisplayData += "<td id='add" + (i + 1) + "' style='display:none'><a id='add_row" + (i+1) + "' onclick='addnewrow()'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></a></td>";
                            DisplayData += "<td id='rem" + (i + 1) + "'><a id='del_row" + (i + 1) + "' onclick='delete_row(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></a></td></tr>";
                        }
                        else
                        {
                            DisplayData += "<td id='add" + (i + 1) + "'><a id='add_row" + (i+1) + "' onclick='addnewrow()'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></a></td>";
                            DisplayData += "<td id='rem" + (i + 1) + "' style='display:none'><a id='del_row" + (i + 1) + "' onclick='delete_row(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></a></td></tr>";
                        }
                         
                        DisplayData += "</tr>";
                        total_Amount = total_Amount + Convert.ToDecimal(dt.Rows[i]["amount"]);
                    }
                }
                DisplayData += "</tbody></table>";
                div_req_data.InnerHtml = DisplayData;
                spn_Total.InnerHtml = Convert.ToString(total_Amount); 
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
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
                if (Convert.ToInt32(empno.InnerHtml) != 4262 && Convert.ToInt32(empno.InnerHtml) != 4263)
                {
                    isSaved = (string)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "insert", ref isdata, spn_req_no.InnerHtml, txt_Username.Text, adv_loc, ddl_Payment_Mode.SelectedValue, req_remark.Text, xml_string, file_attach, txtProcessID.Text, txtInstanceID.Text, span_Approver.InnerHtml, 2, txt_advance_id.Text);
                    if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                    {
                        string[] errmsg = isdata.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = span_Approver.InnerHtml;
                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "192", "OTHER EXPENSE MODIFICATION", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, Convert.ToString(spn_req_no.InnerHtml), txtWIID.Text, ref isInserted);
                        if (isCreate)
                        {
                            try
                            {
                                string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "OTHER EXPENSE MODIFICATION", "USER", txt_Username.Text, "SUBMIT", req_remark.Text, "0", "0");
                                string emailid = string.Empty;
                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Other Expense Request Has Been Modified Successfully and Sent For Approval.</font></pre><pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "OTHER EXPENSE MODIFICATION", "SUBMIT", txt_Approver_Email.Text, txtEmailID.Text, msg, "Request No: " + spn_req_no.InnerHtml);

                            }
                            catch (Exception ex)
                            {
                                // throw;
                                FSL.Logging.Logger.WriteEventLog(false, ex);
                            }
                            finally
                            {
                               // divIns.InnerHtml = "";
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Other Expense Request Has Been Modified Successfully and Request No. is : " + spn_req_no.InnerHtml + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                            }
                        }
                        //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Travel Request Applied Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                    }
                }
                else {

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

                            isSaved = (string)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "insert", ref isdata, spn_req_no.InnerHtml, txt_Username.Text, adv_loc, ddl_Payment_Mode.SelectedValue, req_remark.Text, xml_string, file_attach, txtProcessID.Text, txtInstanceID.Text, span_Approver.InnerHtml, 2, txt_advance_id.Text);
                            if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = isdata.Split(':');
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                            }
                            else
                            {
                                string[] Dval = new string[1];
                                Dval[0] = span_Approver.InnerHtml;
                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "192", "OTHER EXPENSE MODIFICATION", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, Convert.ToString(spn_req_no.InnerHtml), txtWIID.Text, ref isInserted);
                                if (isCreate)
                                {
                                    try
                                    {
                                        string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "OTHER EXPENSE MODIFICATION", "USER", txt_Username.Text, "SUBMIT", req_remark.Text, "0", "0");
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
                                        isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "189", "OTHER EXPENSE APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, spn_req_no.InnerHtml, wiid, ref isInserted);
                                        if (isCreate)
                                        {
                                            try
                                            {
                                                auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "OTHER EXPENSE APPROVAL", "USER", txt_Username.Text, "SUBMIT", "Approve", "0", "0");

                                                string msg = "";
                                                CryptoGraphy crypt = new CryptoGraphy();
                                                string process_name = crypt.Encryptdata("OTHER EXPENSES");
                                                string req_no = crypt.Encryptdata(spn_req_no.InnerHtml);
                                                if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                                                {
                                                    msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Other Expense Request Has Been Modified Successfully and Sent For Payment Approval.</span></pre><pre><span style='font-size: medium;'>Request No: " + spn_req_no.InnerHtml + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://"+compname+"/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://"+compname+"/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                }
                                                else
                                                {
                                                    msg = "<pre><span style='font-size: medium;'>Dear Sir/Madam,</span></pre><pre><span style='font-size: medium;'>Other Expense Request Has Been Modified Successfully and Sent For Payment Approval.</span></pre><pre><span style='font-size: medium;'>Request No: " + spn_req_no.InnerHtml + "</span></pre><pre><span style='font-size: medium;'>Created By: " + span_ename.InnerHtml.Trim() + "</span></pre><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://"+compname+"/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>http://"+compname+"/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>&nbsp;</pre><pre><span style='color: red; font-size: medium;'><em><strong>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</strong></em></span></pre>";
                                                }

                                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "OTHER EXPENSE APPROVAL", "SUBMIT", txtEmailID.Text, txt_Approver_Email.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Other Expense Request Modified Successfully and Sent For Payment Process ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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

    public void fillAuditTrail()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "getaudit", ref isValid, txtProcessID.Text, txtInstanceID.Text);
            tblHTML.Append("<table ID='tblAut' class='table table-bordered'><thead><tr class='grey'><th>Sr.No.</th><th>Step-Name</th><th>Performer</th><th>Date</th><th>Action-Name</th><th>Remark</th></tr></thead>");
            tblHTML.Append("<tbody>");
            for (int Index = 0; Index < dt.Rows.Count; Index++)
            {
                tblHTML.Append("<tr>");
                tblHTML.Append("<td>" + (Index + 1) + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["STEPNAME"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["ACTIONBYUSER"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["ACTIONDATE"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["ACTION"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["REMARK"].ToString() + "</td>");
                tblHTML.Append("</tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            div_Audit.InnerHtml = tblHTML.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;

           path = activeDir + "\\" + spn_req_no.InnerHtml + "\\";

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
                        var sections = directory.Split('\\');
                        var fileName = sections[sections.Length - 1];
                        System.IO.File.Move(activeDir + "\\" + fileName, path + fileName);
                }
            }

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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string dropDownData()
    {
        string isvalid = "";
        string ret_data = "";
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "getreimbursements", ref isvalid, "Other Expenses");
        ret_data = "<option Value='0'>---Select One---</option>";
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
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
        if (gl_code == "")
        {
            gl_code = "---";
        }
        return gl_code;
    }
}