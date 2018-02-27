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

public partial class Bulk_Travel_Expense_Doc_Verification_and_AP_Approval : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Bulk_Travel_Expense_Doc_Verification_and_AP_Approval));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    lnkText.Text = "1";
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username1.Text = txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        txtProcessID.Text = "10";
                        generateQueue();
                    }
                }
                ddlUser.Value = txt_Username.Text;

            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=" + 2);
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected void ddlRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlText1.Text = ddlText.Text = ddlRecords.SelectedItem.Text;
        generateQueue();
    }

    #region ajaxcall
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillSearch(string name, int rpp)
    {
        string tblHTML = getUserData(name, 1, rpp);
        return Convert.ToString(tblHTML);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillGoToPage1(string name, int pageno, int rpp)
    {
        string tblHTML = getUserData(name, pageno, rpp);
        return Convert.ToString(tblHTML);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillGoToPage(int pageno, int rpp)
    {
        string tblHTML = getUserData("", pageno, rpp);
        return Convert.ToString(tblHTML);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string showall(string search_data, int pageno, int rpp, string desg, string division)
    {
        GetData getData = new GetData();
        string str_data = getData.get_Travel_Policy_Data(search_data, pageno, rpp, desg, division);
        return str_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillaudittrail(int processid, int instanceid)
    {
        GetData getData = new GetData();
        string str_data = getData.fillAuditTrail(processid.ToString(), instanceid.ToString());
        return str_data;
    }

    #endregion ajaxcall

    #region userdefined

    protected void generateQueue()
    {
        int rpp = Convert.ToInt32(ddlRecords.SelectedItem.Text);
        int pageno = Convert.ToInt16(lnkText.Text);
        string tblHTML = getUserData(txt_Search.Text.Trim(), pageno, rpp);
        div_InvoiceDetails.InnerHtml = tblHTML;
        ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);
    }

    protected string getUserData(string name, int pageno, int rpp)
    {
        string username = Convert.ToString(Session["USER_ADID"]);
        string html = get_BulkRequests(username, name, pageno, rpp);
        return html;
    }

    public string get_BulkRequests(string username, string name, int pageno, int rpp)
    {
        StringBuilder tblHeader = new StringBuilder();
        StringBuilder tblBody = new StringBuilder();
        string tblHTML = string.Empty;
        DataTable DT = new DataTable();
        string isData = string.Empty;
        int processid, instanceid;
        int no_rec = 0;
        int row_index = 0;
        int bcnt = 0;
        int ccnt = 0;

        try
        {
            DT = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getbulkrequests", ref isData, username, "", 1);

            tblHeader.Append("<th>#</th><th>Initiator</th><th>Header Info</th><th>Payment Mode</th><th>Process Name</th><th>Step Name</th><th>Assigned Date</th><th>Documents</th>");
            int ddl = rpp;
            int from = (pageno - 1) * ddl;
            int to = ((pageno - 1) * ddl) + ddl;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (i < DT.Rows.Count)
                {

                    processid = Int32.Parse(DT.Rows[i]["PK_PROCESSID"].ToString());
                    instanceid = Int32.Parse(DT.Rows[i]["INSTANCE_ID"].ToString());
                    string header = Convert.ToString(DT.Rows[i]["HEADER_INFO"]);
                    string fdate = Convert.ToString(DT.Rows[i]["from_date"]);
                    string tdate = Convert.ToString(DT.Rows[i]["to_date"]);
                    if (fdate != "")
                    {
                        fdate = Convert.ToDateTime(DT.Rows[i]["from_date"]).ToString("dd-MMM-yyyy");
                    }
                    if (tdate != "")
                    {
                        tdate = Convert.ToDateTime(DT.Rows[i]["to_date"]).ToString("dd-MMM-yyyy");
                    }

                    if (name.Trim().ToUpper() == "BANK")
                    {
                        bcnt = 1;
                        ccnt = 0;
                    }
                    else
                    {
                        bcnt = 0;
                        ccnt = 1;
                    }

                    
             //       tblBody.Append("<tr><td align='center'><input type='checkbox' name='bulk' id='rad_id" + (i + 1) + "' onclick='check_uncheck(this," + (i + 1) + ")'><input type='text' id='travelFromDate" + (i + 1) + "' Value='" + fdate + "' style='display:none'/><input type='text' id='travelToDate" + (i + 1) + "' Value='" + tdate + "' style='display:none'/><input type='text' id='fk_process" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_PROCESSID"]) + " style='display:none'></input><input type='text' id='PK_Dispatch_Note_ID" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + " style='display:none'></input><input type='text' id='iid" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["INSTANCE_ID"]) + " style='display:none'></input><input type='text' id='wiid" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + " style='display:none'></input></td><td align='left'>" + DT.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td><td><input type='text' id='h_info" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + " style='display:none'></input><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "' onclick='getRequestDetails(" + (i + 1) + ")'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='center'>" + Convert.ToString(DT.Rows[i]["payment_mode"]) + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'>" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='left'>" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td><td align='center'><a href='#Documents' role='button' data-toggle='modal' onclick='Bind_Documents(" + (i + 1) + ")'>View</a><input type='text' id='pname" + (i + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["PROCESS_NAME"]) + "' style='display:none'/><input type='text' id='header" + (i + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "' style='display:none'/></td></tr>");
                    if (Convert.ToString(DT.Rows[i]["payment_mode"]).Trim().ToUpper() == "BANK" && bcnt == 1)
                    {
                        tblBody.Append("<tr><td align='center'><input type='checkbox' name='bulk' id='rad_id" + (row_index + 1) + "' onclick='check_uncheck(this," + (row_index + 1) + ")'><input type='text' id='travelFromDate" + (row_index + 1) + "' Value='" + fdate + "' style='display:none'/><input type='text' id='travelToDate" + (row_index + 1) + "' Value='" + tdate + "' style='display:none'/><input type='text' id='fk_process" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_PROCESSID"]) + " style='display:none'></input><input type='text' id='PK_Dispatch_Note_ID" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + " style='display:none'></input><input type='text' id='iid" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["INSTANCE_ID"]) + " style='display:none'></input><input type='text' id='wiid" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + " style='display:none'></input></td><td align='left'>" + DT.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td><td><input type='text' id='h_info" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + " style='display:none'></input><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (row_index + 1) + "' onclick='getRequestDetails(" + (row_index + 1) + ")'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='center'>" + Convert.ToString(DT.Rows[i]["payment_mode"]) + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'>" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='left'>" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td><td align='center'><a href='#Documents' role='button' data-toggle='modal' onclick='Bind_Documents(" + (row_index + 1) + ")'>View</a><input type='text' id='pname" + (row_index + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["PROCESS_NAME"]) + "' style='display:none'/><input type='text' id='header" + (row_index + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "' style='display:none'/></td></tr>");
                    }
                    else if (Convert.ToString(DT.Rows[i]["payment_mode"]).Trim().ToUpper() == "CASH" && ccnt == 1)
                    {
                        tblBody.Append("<tr><td align='center'><input type='checkbox' name='bulk' id='rad_id" + (row_index + 1) + "' onclick='check_uncheck(this," + (row_index + 1) + ")'><input type='text' id='travelFromDate" + (row_index + 1) + "' Value='" + fdate + "' style='display:none'/><input type='text' id='travelToDate" + (row_index + 1) + "' Value='" + tdate + "' style='display:none'/><input type='text' id='fk_process" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_PROCESSID"]) + " style='display:none'></input><input type='text' id='PK_Dispatch_Note_ID" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + " style='display:none'></input><input type='text' id='iid" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["INSTANCE_ID"]) + " style='display:none'></input><input type='text' id='wiid" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + " style='display:none'></input></td><td align='left'>" + DT.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td><td><input type='text' id='h_info" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + " style='display:none'></input><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (row_index + 1) + "' onclick='getRequestDetails(" + (row_index + 1) + ")'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='center'>" + Convert.ToString(DT.Rows[i]["payment_mode"]) + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'>" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='left'>" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td><td align='center'><a href='#Documents' role='button' data-toggle='modal' onclick='Bind_Documents(" + (row_index + 1) + ")'>View</a><input type='text' id='pname" + (row_index + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["PROCESS_NAME"]) + "' style='display:none'/><input type='text' id='header" + (row_index + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "' style='display:none'/></td></tr>");
                    }
                    row_index = row_index + 1;
                    no_rec = row_index;

                }
            }
            tblHTML = "<table id='data-table1' class='table table-bordered' align='center' width='100%'>" +
                              "<thead><tr  class='grey' >" + tblHeader.ToString() + "</tr></thead>" +
                              "<tbody>" + tblBody.ToString() + "</tbody>" +
                              "</table>";

            StringBuilder HTML = new StringBuilder();
            double cnt = Convert.ToDouble(DT.Rows.Count) / ddl;
            if (cnt > Convert.ToInt16(Convert.ToInt32(DT.Rows.Count) / ddl))
            {
                cnt = (int)cnt + 1;
            }

            if (cnt > 1)
            {
                HTML.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
                HTML.Append("<ul class='pagination'>");
                for (int j = 1; j <= cnt; j++)
                {
                    HTML.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + ddl + ")'></li>");
                }
                HTML.Append("</ul></div>");
            }
            tblHTML = tblHTML + Convert.ToString(HTML);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(tblHTML);
    }

    #endregion userdefined

    #region events
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Response.Redirect("../../Master.aspx?M_ID=" + 2);
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
                string data = split_data.Text;
                string[] sp_data = data.Split('@');
                int succeed = 0;
                for (int i = 0; i < sp_data.Length; i++)
                {
                    string[] row_data = sp_data[i].Split('$');
                    txt_Dispatch.Text = Convert.ToString(row_data[0]);
                    txtProcessID.Text = Convert.ToString(row_data[1]);
                    txtInstanceID.Text = Convert.ToString(row_data[2]);
                    txtWIID.Text = Convert.ToString(row_data[3]);
                    txt_Request.Text = Convert.ToString(row_data[4]);
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);

                    string remark = string.Empty;
                    string refData = string.Empty;
                    string isInserted = string.Empty;
                    string ISValid = string.Empty;
                    string isSaved = string.Empty;
                  
                    #region domestic
                                 if (txtProcessID.Text == "18")
                                {
                                    DataTable dtPay = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getpaymentmode", ref refData, txt_Dispatch.Text);
                                    if (dtPay != null)
                                    {
                                        if (dtPay.Rows.Count > 0)
                                        {
                                            string mode = Convert.ToString(dtPay.Rows[0]["Mode"]);
                                            string location = Convert.ToString(dtPay.Rows[0]["CASH_LOCATION"]);
                                            string pmode = Convert.ToString(dtPay.Rows[0]["PAYMENT_MODE"]);
                                            string PK_ID = Convert.ToString(dtPay.Rows[0]["pk_travel_expense_Hdr_Id"]);
                                            string initiator = Convert.ToString(dtPay.Rows[0]["user_adid"]);
                                            Init_Email.Text = Convert.ToString(dtPay.Rows[0]["EMAIL_ID"]);



                                            txt_Condition.Text = "1";
                                            DataTable DTAP = new DataTable();
                                            if (mode.ToUpper() == "CASH")
                                            {
                                                DTAP = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "getaccapprover", ref ISValid, "ACCOUNT PAYMENT PERSONNEL", location, pmode);
                                            }
                                            else
                                            {
                                                DTAP = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "getaccapprover", ref ISValid, "ACCOUNT PAYMENT PERSONNEL", 0, pmode);
                                            }
                                            if (DTAP != null)
                                            {
                                                if (DTAP.Rows.Count > 0)
                                                {
                                                    isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "update", ref refData, PK_ID, txt_Username.Text, 1);
                                                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                                                    {
                                                        string[] errmsg = refData.Split(':');
                                                    }
                                                    else
                                                    {
                                                        string approver_mail = string.Empty;
                                                        string[] Dval = new string[DTAP.Rows.Count];
                                                        Dval[0] = "";
                                                        if (DTAP.Rows.Count > 0)
                                                        {
                                                            for (int j = 0; j < DTAP.Rows.Count; j++)
                                                            {
                                                                Dval[j] = Convert.ToString(DTAP.Rows[j]["USER_ADID"]);
                                                                if (approver_mail == "")
                                                                {
                                                                    approver_mail = Convert.ToString(DTAP.Rows[j]["EMAIL_ID"]);
                                                                }
                                                                else
                                                                {
                                                                    approver_mail = approver_mail + ',' + Convert.ToString(DTAP.Rows[j]["EMAIL_ID"]);
                                                                }
                                                            }
                                                        }

                                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "179", "TRAVEL EXPENSE DOCUMENT VERIFICATION", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Dispatch.Text, txtWIID.Text, ref isInserted);
                                                        if (isCreate)
                                                        {
                                               
                                                            succeed = succeed + 1;
                                                            try
                                                            {
                                                                string auditid = (string)ActionController.ExecuteAction(initiator, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "USER", txt_Username.Text, "SUBMIT", "Approved", "0", "0");

                                                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Document Verified For Domestic Travel Expense Request Successfully and Sent To Approver.</font></pre><pre><font size='3'>Request No: " + txt_Dispatch.Text + "</font></pre> <pre><font size='3'>Created By: " + initiator.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "SUBMIT", approver_mail, Init_Email.Text, msg, "Request No: " + txt_Dispatch.Text);
                                                            }
                                                            catch (Exception)
                                                            {
                                                                throw;
                                                            }
                                                            finally
                                                            {
                                                    
                                                            }
                                                        }
                                                    }//
                                                }
                                                else
                                                {
                                                    //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payment Approver Not Available For " + mode + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }
                                            else
                                            {
                                              //  Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Payment Approver Not Available For " + mode + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                            }
                                        }
                                    }
                                }
                        #endregion domestic
                    /*=====================================================================================================================================================*/
                    
                }
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Documents Verified From " + succeed + " out of " + sp_data.Length + " - Expense Request and Sent To Account Approvers...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
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
                string data = split_data.Text;
                string[] sp_data = data.Split('@');
                int succeed = 0;
                for (int i = 0; i < sp_data.Length; i++)
                {
                    string[] row_data = sp_data[i].Split('$');
                    txt_Dispatch.Text = Convert.ToString(row_data[0]);
                    txtProcessID.Text = Convert.ToString(row_data[1]);
                    txtInstanceID.Text = Convert.ToString(row_data[2]);
                    txtWIID.Text = Convert.ToString(row_data[3]);
                    txt_Request.Text = Convert.ToString(row_data[4]);
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);

                    string remark = string.Empty;
                    string refData = string.Empty;
                    string isInserted = string.Empty;
                    string ISValid = string.Empty;
                    string isSaved = string.Empty;
                    #region Domestic
                    if (txtProcessID.Text == "18")
                    {
                        DataTable dtPay = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getpaymentmode", ref refData, txt_Dispatch.Text);
                        if (dtPay != null)
                        {
                            if (dtPay.Rows.Count > 0)
                            {
                                string mode = Convert.ToString(dtPay.Rows[0]["Mode"]);
                                string location = Convert.ToString(dtPay.Rows[0]["CASH_LOCATION"]);
                                string pmode = Convert.ToString(dtPay.Rows[0]["PAYMENT_MODE"]);
                                string PK_ID = Convert.ToString(dtPay.Rows[0]["pk_travel_expense_Hdr_Id"]);
                                string initiator = Convert.ToString(dtPay.Rows[0]["user_adid"]);
                                Init_Email.Text = Convert.ToString(dtPay.Rows[0]["EMAIL_ID"]);

                                txt_Condition.Text = "3";
                                isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "update", ref refData, PK_ID, "", 3);
                                if (isSaved == null || refData.Length > 0 || isSaved == "false")
                                {
                                    string[] errmsg = refData.Split(':');
                                }
                                else
                                {

                                    string[] Dval = new string[1];
                                    Dval[0] = initiator;

                                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "181", "TRAVEL EXPENSE DOCUMENT VERIFICATION", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Dispatch.Text, txtWIID.Text, ref isInserted);
                                    if (isCreate)
                                    {
                                        succeed = succeed + 1;
                                        try
                                        {
                                            string auditid = (string)ActionController.ExecuteAction(initiator, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "USER", txt_Username.Text, "REJECT", "Rejected", "0", "0");

                                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Domestic Travel Request Has Been Rejected.</font></pre><pre><font size='3'>Request No: " + txt_Dispatch.Text + "</font></pre> <pre><font size='3'>Created By: " + initiator.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                            string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "REJECT", Init_Email.Text, "", msg, "Request No: " + txt_Dispatch.Text);
                                        }
                                        catch (Exception)
                                        {
                                            throw;
                                        }
                                        finally
                                        {
                                            //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Travel Request has been Approved...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                        }
                                    }
                                }//

                            }
                        }
                    }
                    #endregion Domestic
                    /****************************************************************************************************************************************************/
                    
                }
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + succeed + " out of " + sp_data.Length + " - Expense Request Has Been Rejected...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
    #endregion events

    #region document
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillDocumentDetails(string proc_name,string data)
     {
         string DisplayData = string.Empty;
            try
            {
                string isData = string.Empty;
                string isValid = string.Empty;


                DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "pgetrequestdata", ref isValid, proc_name, data);

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Name</th></tr></thead>";
                if (dsData != null)
                {
                    for (int i = 0; i < dsData.Rows.Count; i++)
                    {
                         DisplayData += "<tr><td><a href='#' onclick=downloadfiles('" + Convert.ToString(dsData.Rows[i]["object_value"]) + "','" + Convert.ToString(dsData.Rows[i]["filename"]) + "')>" + Convert.ToString(dsData.Rows[i]["filename"]) + "</a></td><td style='display:none'><input type='text' id='request_no_" + (i + 1) + "' value='" + Convert.ToString(dsData.Rows[i]["object_value"]) + "'></td></tr>";
                    }
                }
                DisplayData += "</table>";
                
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        
         return DisplayData;
     }
    #endregion document

    #region request

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getUserInfo(int wiid)
    {
        StringBuilder sb = new StringBuilder();
        string ret_data = string.Empty;
        try
        {
            string ap_id, ap_name, ap_email;
            ap_id = ap_name = ap_email = "NA";
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "pgetrequestdata", ref isdata, wiid);
            if (dsData != null)
            {
                sb.Append("<div class='row'><div class='col-md-12'><div class='panel panel-danger'><div class='panel-body' id='div_hdr'><div class='form-horizontal'>");
                sb.Append("<div class='form-group'>");
                sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Request No</label><div class='col-md-3'><div id='Div12'><b><span id='spn_req_no' runat='server' >" + Convert.ToString(dsData.Tables[0].Rows[0]["travel_voucher_id"]) + "</span></b></div></div>");
                sb.Append("<label class='col-md-2'>Date</label><div class='col-md-3'><div id='Div13'><b><span id='spn_date' runat='server'>" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy") + "</span></b></div></div>");
                sb.Append("<div class='col-md-1'></div></div>");
                sb.Append("<div class='form-group'>");
                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "pgettraveluser", ref isdata, Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]));
                if (dtUser.Rows.Count > 0)
                {
                sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Employee No</label><div class='col-md-3'><div id='Div3'><b><span id='empno' runat='server'>" + Convert.ToString(dtUser.Rows[0]["EMP_ID"]) + "</span></b></div> </div>");


                sb.Append("<label class='col-md-2'>Employee Name</label><div class='col-md-3'><div id='EmployeeName'><b><span id='span_ename' runat='server'>" + Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]) + "</span></b></div></div>");
                    sb.Append("<div class='col-md-1'></div></div>");
                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Designation</label><div class='col-md-3'><div id='Div2'><b><span id='span_designation' runat='server'>" + Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]) + "</span></b></div></div>");
                    sb.Append("<label class='col-md-2'>Cost Center</label><div class='col-md-3'><div id='Div1'><b><span id='span_cc' runat='server'>" + Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]) + "</span></b></div></div><div class='col-md-1'></div></div>");
                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Department</label><div class='col-md-3'><div id='EmployeeDepartment'><b><span id='span_dept' runat='server'>" + Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]) + "</span></b></div></div><label class='col-md-2'>Grade</label><div class='col-md-3'><div id='grade'><b><span id='span_grade' runat='server'>" + Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]) + "</span></b></div></div></div>");

                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Payment Mode </label><div class='col-md-2'><b><select ID='ddl_Payment_Mode' runat='server' class='form-control input-sm' disabled>");
                    
                    DataTable dtPayment = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "selectlocation", ref isdata, "", "", "M_TRAVEL_PAYMENT_MODE", "");

                    if (dtPayment != null)
                    {
                        sb.Append("<Option Value='0'>---Select One---</Option>");
                        
                        if (dtPayment.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtPayment.Rows.Count; i++)
                            {
                                if (Convert.ToString(dsData.Tables[0].Rows[0]["payment_mode"]) == Convert.ToString(dtPayment.Rows[i]["PK_PAYMENT_MODE"]))
                                {
                                    sb.Append("<Option Value='" + Convert.ToString(dtPayment.Rows[i]["PK_PAYMENT_MODE"]) + "' Selected='true'>" + Convert.ToString(dtPayment.Rows[i]["PAYMENT_MODE"]) + "</Option>");
                                }
                                else
                                {
                                     sb.Append("<Option Value='" + Convert.ToString(dtPayment.Rows[i]["PK_PAYMENT_MODE"]) + "'>" + Convert.ToString(dtPayment.Rows[i]["PAYMENT_MODE"]) + "</Option>");
                                }
                            }
                        }
                    }
                    sb.Append("</select></b></div>");
                    sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Payment Location</label><div class='col-md-2'><b>");
                    sb.Append("<select ID='ddlAdv_Location' runat='server' class='form-control input-sm' disabled>");
                    
                    DataTable dtLocation = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "selectlocation", ref isdata, "", "", "TR_LOCATION", "");
                    if (dtLocation != null)
                    {
                        sb.Append("<Option Value='0'>---Select One---</Option>");
                        if (dtLocation.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtLocation.Rows.Count; i++)
                            {
                                if (Convert.ToString(dsData.Tables[0].Rows[0]["cash_location"]) == Convert.ToString(dtLocation.Rows[i]["PK_LOCATION_ID"]))
                                {
                                    sb.Append("<Option Value='" + Convert.ToString(dtLocation.Rows[i]["PK_LOCATION_ID"]) + "' Selected='true'>" + Convert.ToString(dtLocation.Rows[i]["LOCATION NAME"]) + "</Option>");
                                }
                                else
                                {
                                    sb.Append("<Option Value='" + Convert.ToString(dtLocation.Rows[i]["PK_LOCATION_ID"]) + "'>" + Convert.ToString(dtLocation.Rows[i]["LOCATION NAME"]) + "</Option>");
                                }
                            }
                        }
                    }

                    sb.Append("</select></b></div></div>");

                   
                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "pgettravelrequestapprover", ref isdata, Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]));
                    if (dtApprover != null)
                    {
                        if (dtApprover.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                            {
                                ap_id = Convert.ToString(dtApprover.Rows[0]["approver"]);
                                ap_name = Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                            }
                            else
                            {
                                ap_id = "NA";
                                ap_name = "NA";
                            }
                            ap_email = Convert.ToString(dtApprover.Rows[0]["approver_email"]);

                        }
                        else
                        {
                            ap_id = "NA";
                            ap_name = "NA";
                            ap_email = "NA";
                        }
                    }
                    else
                    {
                        ap_id = "NA";
                        ap_name = "NA";
                        ap_email = "NA";
                    }

                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Approver </label><div class='col-md-3'><div id='Div5'><b><span id='span_Approver' runat='server' style='display: none'>" + ap_id + "</span><span id='span_app_name' runat='server'>"+ap_name+"</span></b></div></div></div>");
                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Mobile No.</label><div class='col-md-3'><div id='EmployeePhoneNo'><b><span id='span_mobile' runat='server'>" + Convert.ToString(dtUser.Rows[0]["MOBILE_NO"]) + "</span></b></div></div><label class='col-md-2'>Travel Type</label><div class='col-md-2'><div id='Div10'><b><span id='ddlTravel_Type' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["travel_type"]) + "</span></b></div></div><div class='col-md-1'></div></div>");
                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Travel From Date<font color='#ff0000'><b>*</b></font></label><div class='col-md-2'><div id='Div7'><b><span id='travelFromDate' runat='server'>" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["from_date"]).ToString("dd-MMM-yyyy") + "</span></b></div></div><div class='col-md-1'></div><label class='col-md-2'>Travel To Date<font color='#ff0000'><b>*</b></font></label><div class='col-md-2'><div id='Div9'><b><span id='travelToDate' runat='server'>" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["to_date"]).ToString("dd-MMM-yyyy") + "</span></b></div></div><div class='col-md-1'></div></div>");
                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark<font color='#ff0000'><b>*</b></font></label><div class='col-md-2'><textarea  rows='3' cols='25' ID='req_remark' disabled>" + Convert.ToString(dsData.Tables[0].Rows[0]["remark"]) + "</textarea></div><div class='col-md-1'></div><label class='col-md-2'>Bank Account No.</label><div class='col-md-3'><b>N/A</b></div></div></div></div></div></div>");

                    sb.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><table style='width: 100%'><tr><td style='width: 30%'><h4 class='panel-title'>Advance Details</h4></td><td style='width: 30%; text-align: right'><b><span id='spn_hdr' runat='server'>Recovery Amount : </span></b><b><span id='spn_val' runat='server'>0</span></b></td></tr></table></div>");
                    sb.Append("<div class='panel-body'><div id='div_Advance' runat='server'></div></div></div></div>");
                    sb.Append("<div class='col-md-12' id='tab_TrvelRequert'><div class='panel' style='background-color: grey'>");
                    sb.Append("<ul class='nav nav-tabs'><li id='Li7' class='active'><a href='#'>Travel Request</a></li><li id='Li8'><a href='#'>Hotel Booking</a></li><li id='Li9'><a href='#'>Expense</a></li><li style='margin-left:63%;'><a href='#modal_policy_details' onclick='show_record();' role='button' data-toggle='modal' style='color:blue'>View Policy</a></li></ul>");
                    sb.Append("<div class='tab-content'><div class='tab-pane active in' id='tab1'><div id='div_travelDetails'></div>");
                    sb.Append("<div class='row' id='tab_btn1' runat='server' style='text-align: center;'><a href='#' data-toggle='tab' class='btn btn-grey btn-rounded' onclick='check_Outside_Plant()'>Next</a>");
                    sb.Append("</div></div></div></div></div>");

                    sb.Append("<div class='col-md-12' id='tab_HotelDetails' style='display: none'><div class='panel' style='background-color: grey'>");
                    sb.Append("<ul class='nav nav-tabs'><li id='Li1'><a href='#'>Travel Request</a></li><li id='Li2' class='active'><a href='#'>Hotel Booking</a></li><li id='Li3'><a href='#'>Expense</a></li><li style='margin-left:63%;'><a href='#modal_policy_details' onclick='show_record();' role='button' data-toggle='modal' style='color:blue'>View Policy</a></li></ul>");
                    sb.Append("<div class='tab-content'><div class='tab-pane active in' id='tab2'><div class='table-responsive' id='div_hoteldetails'></div>");
                    sb.Append("<div class='row' id='tab_btn2' runat='server' style='text-align: center;'><a href='#' data-toggle='tab' class='btn btn-grey btn-rounded' onclick='goto1()'>Previous</a><a href='#' data-toggle='tab' class='btn btn-grey btn-rounded' onclick='check_Final()'>Next</a>");
                    sb.Append("</div></div></div></div></div>");

                    sb.Append("<div class='col-md-12' id='tab_Expense' style='display: none'><div class='panel' style='background-color: grey'><ul class='nav nav-tabs'><li id='Li4'><a href='#' data-toggle='tab'>Travel Request</a></li><li id='Li5'><a href='#'>Hotel Booking</a></li><li id='Li6' class='active'><a href='#'>Expense</a></li><li style='margin-left:63%;'><a href='#modal_policy_details' onclick='show_record();' role='button' data-toggle='modal' style='color:blue'>View Policy</a></li></ul>");
                    sb.Append("<div class='tab-content'><div class='tab-pane active in' id='tab3'><div class='table-responsive'><div class='row' style='text-align: right'><font color='red'>Checked Expenses Paid by Corporate Credit Card</font></div><div class='table-responsive' id='div_finaldetails'></div></div>");
                    sb.Append("<div class='row' id='tab_btn3' runat='server' style='text-align: center;'><a href='#' data-toggle='tab' class='btn btn-grey btn-rounded' onclick='goto2()'>Previous</a>");
                    sb.Append("</div></div></div></div></div>");

                    sb.Append("<div class='col-md-12' id='tab_Doc' style='display: none'><div class='panel' style='background-color: grey'>");
                    sb.Append("<ul class='nav nav-tabs'><li id='Li10'><a href='#' data-toggle='tab'>Travel Request</a></li><li id='Li11'><a href='#'>Hotel Booking</a></li><li id='Li12'><a href='#'>Expense</a></li><li id='Li13' class='active'><a href='#'>Documents</a></li><li style='margin-left:63%;'><a href='#modal_policy_details' onclick='show_record();' role='button' data-toggle='modal' style='color:blue'>View Policy</a></li></ul>");
                    sb.Append("<div class='tab-content'><div class='tab-pane active in' id='Div6'><div class='table-responsive'><div class='col-md-6' style='border-left: 1px solid grey'><div class='table-responsive' id='div_docs' runat='server' style='height: 100px; overflow: auto'></div></div></div>");
                    sb.Append("<div class='row' id='Div4' runat='server' style='text-align: center;'><div class='col-md-5'></div><div class='col-md-1'><a href='#' data-toggle='tab' class='btn btn-grey btn-rounded' onclick='goto3()'>Previous</a></div><div class='col-md-6'></div></div></div></div></div></div>");

                    sb.Append("<div class='col-md-12' style='margin-top: 10px;' id='div11'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>Audittrail</h4></div><div class='panel-body'><div id='div_Audit' runat='server'></div></div></div></div></div>");
                }

                ret_data = Convert.ToString(dsData.Tables[0].Rows[0]["pk_travel_expense_hdr_id"]);
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]);
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);
                ret_data += "||" + Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
                ret_data += "||" + Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);
                ret_data += "||" + ap_email;
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["cash_location"]);
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["payment_mode"]);

                
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return sb.ToString() + "@@" + ret_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillPayment_Mode()
    {
        string disp_data = string.Empty;
        try
        {
            string isdata = string.Empty;
            DataTable dtPayment = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "selectlocation", ref isdata, "", "", "M_TRAVEL_PAYMENT_MODE", "");

            if (dtPayment != null)
            {
                disp_data += "0||---Select One---";
                if (dtPayment.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPayment.Rows.Count; i++)
                    {
                        disp_data += "@" + Convert.ToString(dtPayment.Rows[i]["PK_PAYMENT_MODE"]) + "||" + Convert.ToString(dtPayment.Rows[i]["PAYMENT_MODE"]);
                    }
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return disp_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillLocation()
    {
        string disp_data = string.Empty;
        try
        {
            string isdata = string.Empty;
            DataTable dtLocation = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "selectlocation", ref isdata, "", "", "TR_LOCATION", "");
            if (dtLocation != null)
            {
                disp_data += "0||---Select One---";
                if (dtLocation.Rows.Count > 0)
                {
                    for (int i = 0; i < dtLocation.Rows.Count; i++)
                    {
                        disp_data += "@" + Convert.ToString(dtLocation.Rows[i]["PK_LOCATION_ID"]) + "||" + Convert.ToString(dtLocation.Rows[i]["LOCATION NAME"]);
                    }
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return disp_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillAdvanceAmount(string init, string pkid)
    {
        string data = string.Empty;
        StringBuilder tblHTML = new StringBuilder();
        try
        {
            string isValid = string.Empty;
          
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Travel_Request.aspx", "pgetadvancedetails", ref isValid, init, pkid, 2);
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Amount</th><th>Remark</th></tr></thead>");
            tblHTML.Append("<tbody>");
            for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
            {
                tblHTML.Append("<tr>");
                tblHTML.Append("<td>" + (Index + 1) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToDateTime(dt.Tables[0].Rows[Index]["ADVANCE_DATE"]).ToString("dd-MMM-yyyy") + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["amount"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["Remark"]) + "</td>");
                tblHTML.Append("</tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            string adv = "0";
            string exp = "0";
            if (dt.Tables[1] != null)
            {
                adv = Convert.ToString(dt.Tables[1].Rows[0]["advance"]);
                exp = Convert.ToString(dt.Tables[1].Rows[0]["expense"]);               
            }
            tblHTML.Append("@" + adv);
            tblHTML.Append("@" + exp);
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
        return tblHTML.ToString();
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillDocument_Details(int wiid)
    {
        string DisplayData = string.Empty;
            try
            {
                string isData = string.Empty;
                string isValid = string.Empty;


                DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "pgetrequestdata", ref isValid, wiid);

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Name</th><th>Delete</th></tr></thead>";
                if (dsData != null)
                {
                    for (int i = 0; i < dsData.Tables[3].Rows.Count; i++)
                    {
                        DisplayData += "<tr><td><a href='#' onclick=downloadfiles('" + Convert.ToString(dsData.Tables[3].Rows[i]["filename"]) + "')>" + Convert.ToString(dsData.Tables[3].Rows[i]["filename"]) + "</a></td><td></td></tr>";
                    }
                }
                DisplayData += "</table>";
                //div_docs.InnerHtml = DisplayData;
                //DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
            return DisplayData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string get_Hotel_Booking(string jFDate, int cnt, string concat_data, string cls_val, int wiid)
    {
        StringBuilder hotel_data = new StringBuilder();
        string dt = string.Empty;
        string IsValid = string.Empty;
        try
        {
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "pgetrequestdata", ref IsValid, wiid);
            hotel_data.Append("<table id='tbl_hotelBooking' class='table table-striped table-bordered'>");
            hotel_data.Append("<thead><tr class='grey'><th style='width: 15%'>Date</th><th style='width: 15%'>Journey Type</th><th style='width: 15%'>Place Class</th><th style='width: 15%'>Room Type</th><th style='width: 15%'>Hotel Name</th><th style='width: 15%'>Contact No</th><th style='width: 15%; display:none'>Amount</th></tr></thead>");
            hotel_data.Append("<tbody>");

            string[] sp_data = concat_data.Split('@');
            string[] cls_data = cls_val.Split('@');
            for (int i = 1; i <= cnt; i++)
            {
                for (int j = 1; j <= sp_data.Length - 1; j++)
                {
                    if (i == Convert.ToInt32(sp_data[j - 1]))
                    {
                        int row = Convert.ToInt32(j - 1);
                        dt = Convert.ToDateTime(jFDate).AddDays(i - 1).ToString("dd-MMM-yyyy");

                        hotel_data.Append("<tr>");
                        hotel_data.Append("<td><input class='form-control input-sm' type='text' name='htl' id='hotel_rno" + i + "' value=" + sp_data[j - 1] + " style='display:none'>" + dt + "</td>");
                        hotel_data.Append("<td>Outside Plant</td>");
                        hotel_data.Append("<td>");
                        hotel_data.Append("<span id='placeclass" + i + "'>" + cls_data[j - 1] + "</span> Class");
                        hotel_data.Append("</td>");
                        hotel_data.Append("<td>");
                        hotel_data.Append("<select ID='roomType" + i + "' class='form-control input-sm'  disabled>");
                        hotel_data.Append("<option value='0'>---Select One---</option>");
                        if (Convert.ToString(dsData.Tables[1].Rows[i - 1]["room_type"]).ToUpper() == "SINGLE BED OCCUPANCY")
                        { hotel_data.Append("<option value='1' selected='true'>Single Bed Occupancy</option>"); }
                        else
                        { hotel_data.Append("<option value='1'>Single Bed Occupancy</option>"); }

                        if (Convert.ToString(dsData.Tables[1].Rows[i - 1]["room_type"]).ToUpper() == "DOUBLE BED OCCUPANCY")
                        { hotel_data.Append("<option value='2' selected='true'>Double Bed Occupancy</option></select>"); }
                        else
                        { hotel_data.Append("<option value='2'>Double Bed Occupancy</option></select>"); }
                        hotel_data.Append("</td>");
                        hotel_data.Append("<td><input id='hotel_name" + i + "' class='form-control input-sm' type='text' value='" + Convert.ToString(dsData.Tables[1].Rows[i - 1]["hotel_name"]).Replace("'", "") + "' disabled></td>");
                        hotel_data.Append("<td><input id='hotel_no" + i + "' class='form-control input-sm' type='text' value='" + Convert.ToString(dsData.Tables[1].Rows[i - 1]["hotel_no"]).Replace("'", "") + "' disabled></td>");
                        hotel_data.Append("<td style='display:none'><input id='hotel_amount" + i + "' class='form-control input-sm' type='number' min='0' value='" + Convert.ToString(dsData.Tables[1].Rows[i - 1]["hotel_charge"]) + "' disabled></td>");
                        hotel_data.Append("</tr>");

                    }
                }
            }


            hotel_data.Append("</tbody></table>");
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(hotel_data);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string[] get_Travel_Class(int pk_id, int i)
    {
        string[] ret_val = new string[2];
        string isdata = string.Empty;
        ret_val[0] = Convert.ToString(i);
        string val = (string)ActionController.ExecuteAction("", "Travel_Request.aspx", "gettravelclass", ref isdata, pk_id);
        ret_val[1] = val;
        return ret_val;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_Journey_Data(string jFDate, string jTDate, int wiid)
    {
        string jdata = string.Empty;
        DateTime fdate = Convert.ToDateTime(jFDate);
        DateTime tdate = Convert.ToDateTime(jTDate);
        StringBuilder html = new StringBuilder();
        try
        {
            string IsValid = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "pgetrequestdata", ref IsValid, wiid);

            int rno = 1;
            //while (fdate <= tdate)
            for (int from = 0; from < dsData.Tables[1].Rows.Count; from++)
            {
                html.Append("<tr>");
                html.Append("<td><input class='form-control input-sm' type='text' id='jrno" + rno + "' value=" + rno + " style='display:none'><span id='spn" + rno + "'><input class='form-control input-sm' type='text' id='jrny_date" + rno + "' value=" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + " style='display:none'>" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + "</span></td>");
                DataSet dtJourney = (DataSet)ActionController.ExecuteAction("", "Travel_Request.aspx", "select", ref IsValid);
                if (dtJourney.Tables[0] != null)
                {
                    html.Append("<td><select ID='journey_Type" + rno + "' runat='server' name='jt' class='form-control input-sm' onchange='check_journey_Type(" + rno + "," + dtJourney.Tables[0].Rows.Count + ")' disabled='true'>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["journey_type"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                        }
                    }
                    html.Append("</select></td>");
                }

                if (dtJourney.Tables[1] != null)
                {
                    html.Append("<td class='modetravel'>");
                    html.Append("<select ID='Travel_Mode" + rno + "' runat='server' class='form-control input-sm' style='display:normal' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[1].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["travel_mode"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[1].Rows[i]["TRAVEL_NAME"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[1].Rows[i]["TRAVEL_NAME"]) + "</option>");
                        }
                    }
                    html.Append("</select>");

                    html.Append("<select ID='From_Plant" + rno + "' runat='server' class='form-control input-sm' style='display:normal' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[3].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["plant_from"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                        }
                    }
                    html.Append("</select>");

                    html.Append("<select ID='default_From" + rno + "' runat='server' class='form-control input-sm' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");
                    html.Append("</select>");

                    html.Append("</td>");
                }

                if (dtJourney.Tables[2] != null)
                {
                    html.Append("<td class='modetravel'>");
                    html.Append("<select ID='Travel_Class" + rno + "' runat='server' class='form-control input-sm' style='display:normal' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    DataTable dtClass = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "pgettravelclass", ref IsValid, Convert.ToString(dsData.Tables[1].Rows[from]["travel_mode"]));

                    for (int i = 0; i < dtClass.Rows.Count; i++)
                    {
                        if (Convert.ToString(dtClass.Rows[i]["PK_TRAVEL_MAPPING_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["travel_class"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtClass.Rows[i]["PK_TRAVEL_MAPPING_ID"]) + "' selected='true'>" + Convert.ToString(dtClass.Rows[i]["TRAVEL_MAPPING_CLASS"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtClass.Rows[i]["PK_TRAVEL_MAPPING_ID"]) + "'>" + Convert.ToString(dtClass.Rows[i]["TRAVEL_MAPPING_CLASS"]) + "</option>");
                        }
                    }
                    html.Append("</select>");

                    html.Append("<select ID='To_Plant" + rno + "' runat='server' class='form-control input-sm' style='display:normal' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[3].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["plant_to"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                        }
                    }
                    html.Append("</select>");

                    html.Append("<select ID='default_To" + rno + "' runat='server' class='form-control input-sm' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");
                    html.Append("</select>");

                    html.Append("</td>");
                }

                if (dtJourney.Tables[5] != null)
                {
                    html.Append("<td><select ID='From_City" + rno + "' runat='server' class='form-control input-sm' onchange='chk_class_From(" + rno + ")' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["from_city"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                        }
                    }

                    if (Convert.ToString(dsData.Tables[1].Rows[from]["from_city"]) != "-1")
                    {
                        html.Append("<option Value='-1'>Other</option>");
                        html.Append("</select><input type='text' class='form-control input-sm' id='txt_f_city" + rno + "' style='display:none' disabled></td>");
                    }
                    else
                    {
                        html.Append("<option Value='-1' selected='true'>Other</option>");
                        html.Append("</select><input type='text' class='form-control input-sm' id='txt_f_city" + rno + "' value='" + Convert.ToString(dsData.Tables[1].Rows[from]["other_f_city"]).Replace("'", "") + "' style='display:normal' disabled></td>");
                    }

                    html.Append("<td><select ID='To_City" + rno + "' runat='server' class='form-control input-sm' onchange='chk_class_To(" + rno + ")' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["to_city"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                        }
                    }
                    string CLASSIFICATION = Convert.ToString(dsData.Tables[1].Rows[from]["CLASSIFICATION"]);
                    if (Convert.ToString(dsData.Tables[1].Rows[from]["to_city"]) != "-1")
                    {
                        html.Append("<option Value='-1'>Other</option>");
                        html.Append("</select><input type='text' id='cls" + rno + "' value='" + CLASSIFICATION + "' style='display:none'><input type='text' class='form-control input-sm' id='txt_t_city" + rno + "' style='display:none' disabled></td>");
                    }
                    else
                    {
                        html.Append("<option Value='-1' selected='true'>Other</option>");
                        html.Append("</select><input type='text' id='cls" + rno + "' value='" + CLASSIFICATION + "' style='display:none'><input type='text' class='form-control input-sm' id='txt_t_city" + rno + "' value='" + Convert.ToString(dsData.Tables[1].Rows[from]["other_t_city"]).Replace("'", "") + "' style='display:normal' disabled></td>");
                    }

                }

                html.Append("</tr>");
                rno = rno + 1;
                fdate = fdate.AddDays(1);
            }

            jdata = Convert.ToString(html);

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return jdata;
    }


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_Final_Data(string jFDate, string jTDate, int jr, string jt_data, string concat_data, string jt_val, string jt_amt, string to_city, string desg, int wiid, string travel_class_id, string plant_id)
    {
        StringBuilder hotel_data = new StringBuilder();
        string dt = string.Empty;
        string is_data = string.Empty;
        string doa = "";
        try
        {
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "pgetrequestdata", ref is_data, wiid);

            string[] j_type = jt_data.Split('@');
            string[] j_val = jt_val.Split('@');
            string[] row = concat_data.Split('@');
            string[] h_amt = jt_amt.Split('@');
            string[] pk_id = to_city.Split('@');
            string[] travel_class = travel_class_id.Split('@');
            string[] travel_plant = plant_id.Split('@');
            string ids = "";
            for (int x = 0; x < j_val.Length - 1; x++)
            {
                if (j_val[x + 1] != "")
                {
                    ids += j_val[x] + ",";
                }
                else
                {
                    ids += j_val[x];
                }

            }

            DataTable ejm = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "getexpenseids", ref is_data, ids);
            hotel_data.Append("<table id='tbl_hotelBooking' class='table table-striped table-bordered'>");
            hotel_data.Append("<thead>");
            hotel_data.Append("<tr class='grey' style='font-weight:bold'><th style='width:5%'>Date</th><th>Journey Type</th><th>Particulars</th>");
            if (ejm.Rows.Count > 0)
            {
                for (int y = 0; y < ejm.Rows.Count; y++)
                {
                    hotel_data.Append("<th><input type='text' id='compare_id" + (y + 1) + "' value='" + ejm.Rows[y]["COMPARE_ID"] + "' style='color:black; display:none'><input type='text' id='compare_name" + (y + 1) + "' value='" + Convert.ToString(ejm.Rows[y]["COMP_NAME"]).Replace(" ", "_") + "' style='color:black; display:none'><input type='text' name='eh' id='expenses" + (y + 1) + "' value='" + ejm.Rows[y]["FK_EXPENSE_HEAD"] + "' style='color:black; display:none'><input type='text' id='exp_name" + (y + 1) + "' value='" + ejm.Rows[y]["EXPENSE_HEAD"] + "' style='color:black; display:none'><input type='text' id='e_name" + (y + 1) + "' value='" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "' style='color:black; display:none'>" + ejm.Rows[y]["EXPENSE_HEAD"] + "</th>");
                }
            }
            hotel_data.Append("<th style='display:none'>Hotel Charge</th><th>Total</th></tr>");
            hotel_data.Append("</thead>");
            hotel_data.Append("<tbody>");


            for (int i = 1; i <= j_type.Length; i++)
            {
                dt = Convert.ToDateTime(jFDate).AddDays(i - 1).ToString("dd-MMM-yyyy");

                int journey_Type = Convert.ToInt32(j_val[i - 1]);

                string val = (string)ActionController.ExecuteAction("", "Travel_Request.aspx", "gettravelclass", ref is_data, pk_id[i - 1]);
                if (pk_id[i - 1] == "0")
                {
                    val = "A";
                }
                hotel_data.Append("<tr>");
                hotel_data.Append("<td>" + dt + "</td>");
                hotel_data.Append("<td>" + j_type[i - 1] + "</td>");
                hotel_data.Append("<td><input type='text' id='remark_note" + i + "' value='" + Convert.ToString(dsData.Tables[1].Rows[i - 1]["remark_note"]).Replace("'", "") + "' class='form-control input-sm' disabled></td>");
                string value = "0";
                if (ejm.Rows.Count > 0)
                {
                    for (int y = 0; y < ejm.Rows.Count; y++)
                    {
                        string cnt = (string)ActionController.ExecuteAction("", "Travel_Request.aspx", "getjmcount", ref is_data, j_val[i - 1], ejm.Rows[y]["FK_EXPENSE_HEAD"]);
                        value = (string)ActionController.ExecuteAction("", "Travel_Request.aspx", "getpolicyamount", ref is_data, desg, journey_Type, ejm.Rows[y]["FK_EXPENSE_HEAD"], "A", travel_class[i - 1], travel_plant[i - 1]);
                        string perc = (string)ActionController.ExecuteAction("", "Travel_Request.aspx", "getclasswiseperc", ref is_data, val);
                        //if (value !="" && val == "B")
                        //{
                        //    value = Convert.ToString(Convert.ToDouble(value)*85/100);
                        //}
                        //else if (value != "" && val == "C")
                        //{
                        //    value = Convert.ToString(Convert.ToDouble(value) * 70 / 100);
                        //}
                        if (value != "")
                        {
                            value = Convert.ToString(Convert.ToDouble(value) * Convert.ToDouble(perc) / 100);
                        }
                        DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "getexpamount", ref is_data, Convert.ToString(dsData.Tables[0].Rows[0]["pk_travel_expense_hdr_id"]), dt, ejm.Rows[y]["FK_EXPENSE_HEAD"]);
                        string iamt = "0";
                        string is_cr = "0";
                        if (dtamt != null)
                        {
                            if (dtamt.Rows.Count > 0)
                            {
                                iamt = Convert.ToString(dtamt.Rows[0]["amount"]);
                                is_cr = Convert.ToString(dtamt.Rows[0]["IS_CR"]);
                            }
                        }

                        if (value != "")
                        {
                            if (Convert.ToDouble(value) < Convert.ToDouble(iamt))
                            {
                                doa = "1";
                            }
                        }
                        if (Convert.ToString(ejm.Rows[y]["IS_ALLOW"]) == "1" && cnt != "0")
                        {
                            if (is_cr == "0")
                            {
                                hotel_data.Append("<td><table><tr><td><input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + Convert.ToString(ejm.Rows[y]["DIRECT_ALLOW"]) + "' style='display:none'><input type='checkbox' onchange='calculate_Amount();' id='IS_CR_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' disabled></td><td><input type='number' min='0' id='" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + iamt + "' class='form-control input-sm' style='text-align:right; width:70px; padding-right:0px' onkeyup='calculate_Amount();' disabled><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'></td></tr></table></td>");
                            }
                            else
                            {
                                hotel_data.Append("<td><table><tr><td><input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + Convert.ToString(ejm.Rows[y]["DIRECT_ALLOW"]) + "' style='display:none'><input type='checkbox' onchange='calculate_Amount();' id='IS_CR_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' checked='true' disabled></td><td><input type='number' min='0' id='" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + iamt + "' class='form-control input-sm' style='text-align:right; width:70px; padding-right:0px' onkeyup='calculate_Amount();' disabled><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'></td></tr></table></td>");
                            }
                        }
                        else
                        {

                            if (value == "")
                            {
                                value = "0";
                            }
                            hotel_data.Append("<td><table><tr><td><input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + Convert.ToString(ejm.Rows[y]["DIRECT_ALLOW"]) + "' style='display:none'><input type='checkbox' onchange='calculate_Amount();' id='IS_CR_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' disabled></td><td><input type='number' min='0' id='" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:righ; width:70px; padding-right:0px' readonly><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; width:70px; display:none'></td></tr></table></td>");
                        }
                    }
                }


                hotel_data.Append("<td style='text-align:right; display:none'><span id='Hotel_Charge" + i + "'>" + h_amt[i - 1] + "</span></td>");
                hotel_data.Append("<td style='text-align:right'><span id='Total" + i + "'>0</span></td>");
                hotel_data.Append("</tr>");
            }

            hotel_data.Append("<tr>");
            hotel_data.Append("<td colspan='" + (ejm.Rows.Count + 3) + "' style='text-align:right'><b>Final Amount : </b></td>");
            hotel_data.Append("<td style='text-align:right'><b><span id='Final_Amtt'>0</span></b></td>");
            hotel_data.Append("</tr>");
            hotel_data.Append("</tbody></table>");
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(hotel_data) + "||" + doa;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getOtherExpenseUser(int wiid)
    {
        StringBuilder sb = new StringBuilder();
        string ret_data = string.Empty;
        try
        {
            string ap_id, ap_name, ap_email;
            ap_id = ap_name = ap_email = "NA";
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetrequestdata", ref isdata, wiid);
            if (dsData != null)
            {
                sb.Append("<div class='row'><div class='col-md-12'><div class='panel panel-danger'><div class='panel-body' id='div_hdr'><div class='form-horizontal'>");
              
                
                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetuser", ref isdata, Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]));
                if (dtUser.Rows.Count > 0)
                {
                    sb.Append("<div class='form-group'>");
                    sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Request No</label><div class='col-md-3'><div id='Div12'><b><span id='spn_req_no' runat='server' >" + Convert.ToString(dsData.Tables[0].Rows[0]["voucher_id"]) + "</span></b></div></div>");
                    sb.Append("<label class='col-md-2'>Date</label><div class='col-md-3'><div id='Div13'><b><span id='spn_date' runat='server'>" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy") + "</span></b></div></div>");
                    sb.Append("<div class='col-md-1'></div></div>");

                    sb.Append("<div class='form-group'>");
                    sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Employee No</label><div class='col-md-3'><div id='Div3'><b><span id='empno' runat='server'>" + Convert.ToString(dtUser.Rows[0]["EMP_ID"]) + "</span></b></div> </div>");


                    sb.Append("<label class='col-md-2'>Employee Name</label><div class='col-md-3'><div id='EmployeeName'><b><span id='span_ename' runat='server'>" + Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]) + "</span></b></div></div>");
                    sb.Append("<div class='col-md-1'></div></div>");
                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Designation</label><div class='col-md-3'><div id='Div2'><b><span id='span_designation' runat='server'>" + Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]) + "</span></b></div></div>");
                    sb.Append("<label class='col-md-2'>Cost Center</label><div class='col-md-3'><div id='Div1'><b><span id='span_cc' runat='server'>" + Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]) + "</span></b></div></div><div class='col-md-1'></div></div>");
                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Department</label><div class='col-md-3'><div id='EmployeeDepartment'><b><span id='span_dept' runat='server'>" + Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]) + "</span></b></div></div><label class='col-md-2'>Grade</label><div class='col-md-3'><div id='grade'><b><span id='span_grade' runat='server'>" + Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]) + "</span></b></div></div></div>");

                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Payment Mode </label><div class='col-md-2'><b>" + Convert.ToString(dsData.Tables[0].Rows[0]["P_MODE"]) + "</b></div>");

                    string loc = "NA";
                    if (Convert.ToString(dsData.Tables[0].Rows[0]["P_LOCATION"]) != "")
                    {
                        loc = Convert.ToString(dsData.Tables[0].Rows[0]["P_LOCATION"]);
                    }

                    sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Payment Location</label><div class='col-md-2'><b>"+loc+"</b></div></div>");


                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetrequestapprover", ref isdata, Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]));
                    if (dtApprover != null)
                    {
                        if (dtApprover.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                            {
                                ap_id = Convert.ToString(dtApprover.Rows[0]["approver"]);
                                ap_name = Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                            }
                            else
                            {
                                ap_id = "NA";
                                ap_name = "NA";
                            }
                            ap_email = Convert.ToString(dtApprover.Rows[0]["approver_email"]);

                        }
                        else
                        {
                            ap_id = "NA";
                            ap_name = "NA";
                            ap_email = "NA";
                        }
                    }
                    else
                    {
                        ap_id = "NA";
                        ap_name = "NA";
                        ap_email = "NA";
                    }

                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Approver </label><div class='col-md-3'><div id='Div5'><b><span id='span_Approver' runat='server' style='display: none'>" + ap_id + "</span><span id='span_app_name' runat='server'>" + ap_name + "</span></b></div></div><label class='col-md-2'>Mobile No.</label><div class='col-md-3'><div id='EmployeePhoneNo'><b><span id='span_mobile' runat='server'>" + Convert.ToString(dtUser.Rows[0]["MOBILE_NO"]) + "</span></b></div></div></div>");
                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label><div class='col-md-2'><textarea  rows='3' cols='25' class='form-control input-sm' ID='req_remark' disabled>" + Convert.ToString(dsData.Tables[0].Rows[0]["remark"]) + "</textarea></div><div class='col-md-1'></div><label class='col-md-2'>Bank Account No.</label><div class='col-md-3'><b>N/A</b></div></div>");



                    sb.Append("<div class='row'><div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><div class='panel-heading-btn'><h4 class='panel-title'>Total Amount : <span id='spn_Total'>0</span></h4></div>");
                    sb.Append("<h4 class='panel-title'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone'></i>Expense Details</h4></div>");
                    sb.Append("<div class='panel-body'><div id='div_req_data' runat='server' class='table-responsive'></div></div></div></div></div>");

                    sb.Append("<div class='row'><div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'>");
                    sb.Append("<h4 class='panel-title'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone'></i>Audit Trail</h4></div><div class='panel-body'><div id='div_Audit' runat='server' class='table-responsive'></div></div></div></div></div>");

                }

                sb.Append("</div></div></div></div></div>");


              
                ret_data = Convert.ToString(dsData.Tables[0].Rows[0]["pk_travel_expense_hdr_id"]);
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]);
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);
                ret_data += "||" + Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
                ret_data += "||" + Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);
                ret_data += "||" + ap_email;
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["cash_location"]);
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["payment_mode"]);


            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return sb.ToString() + "@@" + ret_data;
    }


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillRequest_data(int wiid)
    {
        string DisplayData = string.Empty;
            try
            {
                string isData = string.Empty;
                string isValid = string.Empty;
                double total = 0;
                DisplayData = "<table class='table table-bordered' id='tbl_Data'><thead><tr class='grey'> <th style='width:5%'>#</th><th style='width:15%; text-align:center'>Date</th><th style='width:15%; text-align:center'>Bill No</th><th style='width:15%; text-align:center'>Bill Date</th><th style='width:25%; text-align:center'>Particulars</th><th style='width:15%; text-align:center'>Amount</th></tr></thead>";
                DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetrequestdata", ref isData, wiid);
                DataTable dt = new DataTable();
                if (dsData != null)
                {
                    dt = dsData.Tables[1];
                }
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DisplayData += "<tr>";
                        DisplayData += "<td align='center'>" + (i + 1) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["date"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["billno"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["bill_date"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["remark"]) + "</td>";
                        DisplayData += "<td style='text-align:right'>" + Convert.ToString(dt.Rows[i]["amount"]) + "</td>";
                        DisplayData += "</tr>";
                        total += Convert.ToDouble(dt.Rows[i]["amount"]);
                    }
                }
                DisplayData += "</table>" + "@@"+Convert.ToString(total);
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        
        return DisplayData;
    }
    #endregion request



    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getDetails(string req_no, string process_name)
    {
        string sb = "";
        try
        {
            Expense_Details ed = new Expense_Details();
            sb = ed.Expense_Request_Details(req_no, process_name);
        }
        catch (Exception ex)
        {
            sb = "";
        }
        return sb.ToString();
    }
}