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

public partial class Bulk_Account_Approval : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Bulk_Account_Approval));
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
    public string fillSearch(string name, int rpp,string pmode)
    {
        string tblHTML = getUserData(name, 1, rpp, pmode);
        return Convert.ToString(tblHTML);
    }


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillGoToPage1(string name, int pageno, int rpp, string pmode)
    {
        string tblHTML = getUserData(name, pageno, rpp,pmode);
        return Convert.ToString(tblHTML);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillGoToPage(int pageno, int rpp,string pmode)
    {
        string tblHTML = getUserData("", pageno, rpp, pmode);
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
        string tblHTML = getUserData(txt_Search.Text.Trim(), pageno, rpp,ddlPay_Mode.SelectedItem.Text);
        div_InvoiceDetails.InnerHtml = tblHTML;
        
        ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);
    }

    protected string getUserData(string name, int pageno, int rpp, string pmode)
    {
        string username = Convert.ToString(Session["USER_ADID"]);
        string html = get_BulkRequests(username, name, pageno, rpp, pmode);
        return html;
    }

   
    public string get_BulkRequests(string username, string name, int pageno, int rpp,string pmode)
    {
        StringBuilder tblHeader = new StringBuilder();
        StringBuilder tblBody = new StringBuilder();
        StringBuilder tblData = new StringBuilder();
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
            DT = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getbulkrequests", ref isData, username, name, 2);

            //tblHeader.Append("<th>#</th><th>Initiator</th><th>Header Info</th><th>Payment Mode</th><th>Process Name</th><th>Step Name</th><th>Assigned Date</th><th>Documents</th>");
            if (pmode.ToUpper() == "BANK")
            {
                tblHeader.Append("<th><input type='checkbox' id='select_all'></th><th>Initiator</th><th>Header Info</th><th>Payment Mode</th><th>Process Name</th><th>Step Name</th><th>Assigned Date</th><th>Documents</th>");
            }
            else
            {
                tblHeader.Append("<th>#</th><th>Initiator</th><th>Header Info</th><th>Payment Mode</th><th>Process Name</th><th>Step Name</th><th>Assigned Date</th><th>Documents</th>");
            }
            
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
                    if (pmode.Trim().ToUpper() == "BANK")
                    {
                        bcnt = 1;
                        ccnt = 0;
                    }
                    else
                    {
                        bcnt = 0;
                        ccnt = 1;
                    }
                    if (Convert.ToString(DT.Rows[i]["payment_mode"]).Trim().ToUpper() == "BANK" && bcnt==1)
                    {
                        tblBody.Append("<tr><td align='center'><input type='checkbox' name='bulk' id='chkbox" + (row_index + 1) + "' onclick='javascript:check_uncheck(this," + (row_index + 1) + ");'><input type='text' id='travelFromDate" + (row_index + 1) + "' Value='" + fdate + "' style='display:none'/><input type='text' id='travelToDate" + (row_index + 1) + "' Value='" + tdate + "' style='display:none'/><input type='text' id='fk_process" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_PROCESSID"]) + " style='display:none'></input><input type='text' id='PK_Dispatch_Note_ID" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + " style='display:none'></input><input type='text' id='iid" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["INSTANCE_ID"]) + " style='display:none'></input><input type='text' id='wiid" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + " style='display:none'></input></td><td align='left'>" + DT.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td><td><input type='text' id='h_info" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + " style='display:none'></input><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (row_index + 1) + "' onclick='getRequestDetails(" + (row_index + 1) + ")'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='center'>" + Convert.ToString(DT.Rows[i]["payment_mode"]) + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'>" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='left'>" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td><td align='center'><a href='#Documents' role='button' data-toggle='modal' onclick='Bind_Documents(" + (row_index + 1) + ")'>View</a><input type='text' id='pname" + (row_index + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["PROCESS_NAME"]) + "' style='display:none'/><input type='text' id='header" + (row_index + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "' style='display:none'/></td></tr>");

                        if (row_index == 0)
                        {
                            tblData.Append((row_index + 1) + "$" + Convert.ToString(DT.Rows[i]["PK_PROCESSID"]) + "$" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "$" + Convert.ToString(DT.Rows[i]["INSTANCE_ID"]) + "$" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]));
                        }
                        else
                        {
                            tblData.Append("@" + (row_index + 1) + "$" + Convert.ToString(DT.Rows[i]["PK_PROCESSID"]) + "$" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "$" + Convert.ToString(DT.Rows[i]["INSTANCE_ID"]) + "$" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]));
                        }

                        row_index = row_index + 1;
                        no_rec =row_index;
                    }
                    else if (Convert.ToString(DT.Rows[i]["payment_mode"]).Trim().ToUpper() == "CASH" && ccnt == 1)
                    {
                        tblBody.Append("<tr><td align='center'><input type='radio' name='single' id='rad_id" + (row_index + 1) + "' onclick=check_radio(this,'" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "'," + Convert.ToString(DT.Rows[i]["PK_PROCESSID"]) + "," + Convert.ToString(DT.Rows[i]["INSTANCE_ID"]) + "," + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + ");><input type='text' id='travelFromDate" + (row_index + 1) + "' Value='" + fdate + "' style='display:none'/><input type='text' id='travelToDate" + (row_index + 1) + "' Value='" + tdate + "' style='display:none'/><input type='text' id='fk_process" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_PROCESSID"]) + " style='display:none'></input><input type='text' id='PK_Dispatch_Note_ID" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + " style='display:none'></input><input type='text' id='iid" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["INSTANCE_ID"]) + " style='display:none'></input><input type='text' id='wiid" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + " style='display:none'></input></td><td align='left'>" + DT.Rows[i]["EMPLOYEE_NAME"].ToString() + "</td><td><input type='text' id='h_info" + (row_index + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + " style='display:none'></input><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (row_index + 1) + "' onclick='getRequestDetails(" + (row_index + 1) + ")'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='center'>" + Convert.ToString(DT.Rows[i]["payment_mode"]) + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'>" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='left'>" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td><td align='center'><a href='#Documents' role='button' data-toggle='modal' onclick='Bind_Documents(" + (row_index + 1) + ")'>View</a><input type='text' id='pname" + (row_index + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["PROCESS_NAME"]) + "' style='display:none'/><input type='text' id='header" + (row_index + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "' style='display:none'/></td></tr>");
                        row_index = row_index + 1;
                        no_rec = row_index;
                    }
                   
                }
            }
            tblHTML = "<table id='data-table1' class='table table-bordered' align='center' width='100%'>" +
                              "<thead><tr  class='grey' >" + tblHeader.ToString() + "</tr></thead>" +
                              "<tbody>" + tblBody.ToString() + "</tbody>" +
                              "</table>";

        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        if (bcnt == 1)
        {
            return Convert.ToString(tblHTML) + "@@" + Convert.ToString(row_index) + "@@" + Convert.ToString(tblData);
        }
        else
        {
            return Convert.ToString(tblHTML);
        }
    }

    #endregion userdefined

    #region events
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try{
            
                Response.Redirect("../../Master.aspx?M_ID=" + 2);
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
		divIns.Style.Add("display","none");
                string data = split_data.Text;
                string[] sp_data = data.Split('@');
                int succeed = 0;
                string alert_message = "Data Posted Successfully : ";
                string error_message = "";
                for (int i = 0; i < sp_data.Length; i++)
                    {
                    string[] row_data = sp_data[i].Split('$');
                    txt_Dispatch.Text = Convert.ToString(row_data[0]);
                    txtProcessID.Text = Convert.ToString(row_data[1]);
                    txtInstanceID.Text = Convert.ToString(row_data[2]);
                    txtWIID.Text = Convert.ToString(row_data[3]);
                    txt_Request.Text = Convert.ToString(row_data[4]);
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                    string adv_id = "";

                    string remark = string.Empty;
                    string refData = string.Empty;
                    string isInserted = string.Empty;
                    string ISValid = string.Empty;
                    string isSaved = string.Empty;
                    #region domestic
                    if (txtProcessID.Text == "18")
                    {
                        string cash_flag, cash_no, bank_flag,bank_no;
                        bank_flag = cash_no = cash_flag = bank_no = "";
                        adv_id = "";
                        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "pgetrequestdata", ref refData, txtWIID.Text);
                        if (dsData != null)
                        {
                            adv_id = Convert.ToString(dsData.Tables[0].Rows[0]["advance_id"]);
                        }

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

                                string rfc_string = string.Empty;
                                DataSet dt_sap_rfc = new DataSet();
                                if (mode.ToUpper() == "BANK")
                                {
                                    rfc_string = "S";
                                }
                                if (mode.ToUpper() == "CASH")
                                {
                                    string rfc_action = return_msg(txt_Dispatch.Text,adv_id);
                                    dt_sap_rfc = (DataSet)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getcashrfc", ref refData, rfc_action, txt_Dispatch.Text);
                                    if (dt_sap_rfc != null)
                                    {
 						DataTable dtManage = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata1", ref refData, txt_Dispatch.Text, "SELECT", "", "", "", "","","","");
                                                if (dtManage != null && dtManage.Rows.Count > 0)
                                                {
                                                    cash_flag = Convert.ToString(dtManage.Rows[0]["CASH_FLAG"]);
                                                    cash_no = Convert.ToString(dtManage.Rows[0]["CASH_NO"]);
                                                    bank_flag = Convert.ToString(dtManage.Rows[0]["BANK_FLAG"]);
                                                    bank_no = Convert.ToString(dtManage.Rows[0]["BANK_NO"]);
                                                }
                                        if (dt_sap_rfc.Tables[1].Rows.Count > 0)
                                        {
  						
                                            if (Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][0]).ToUpper() == "NIL")
                                            {
                                                rfc_string = "S";
                                            }
                                            else
                                            {
                                             
                                                if (cash_flag == "E" || cash_flag=="")
                                                {
                                                    Vendor_Portal.Vendor_Portal_DetailsService Vendor = new Vendor_Portal.Vendor_Portal_DetailsService();
                                                    string[] Vendor_data_array = new string[3];
                                                    Vendor_data_array = Vendor.CASH_LEGDER(Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][0]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][1]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][2]),
                                                                Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][3]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][4]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][5]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][6]),
                                                                Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][7]), "", Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][9]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][10]),
                                                                Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][11]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][12]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][13]),
                                                                Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][14]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][15]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][16]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][17]));
                                                    rfc_string = Vendor_data_array[1];
                                                    string rfc_no = "";
                                                    if (rfc_string == "S")
                                                    {
                                                        string[] sp_data1 = Convert.ToString(Vendor_data_array[0]).Split(' ');
                                                        for (int k = 0; k < sp_data1.Length; k++)
                                                        {
                                                            if (Convert.ToString(sp_data1[k]).Length==10 && rfc_no=="")
                                                            {
                                                                cash_no = rfc_no = Convert.ToString(sp_data1[k]);
                                                                alert_message = "Data Posted Successfully : " + Convert.ToString(rfc_no);
                                                            }
                                                        }
                                                    }
                                                    else 
                                                    { 
                                                        alert_message = Vendor_data_array[0];
                                                    }
                                                    string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, txt_Dispatch.Text, "CASH", Convert.ToString(Vendor_data_array[1]).ToUpper(), rfc_no, Convert.ToString(Vendor_data_array[0]), "", "", "", txt_Dispatch.Text);
                                                }
                                                else
                                                {
                                                    rfc_string = "S";
                                                    alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                                }
                                            }
                                            if (rfc_string != "E" && dt_sap_rfc.Tables[2].Rows.Count > 0 && (bank_flag=="E" || bank_flag==""))
                                            {
                                                Vendor_Portal.Vendor_Portal_DetailsService Vendor1 = new Vendor_Portal.Vendor_Portal_DetailsService();
                                                string[] Vendor_data_array1 = new string[3];
                                                string bank_string = return_bank_string(dt_sap_rfc.Tables[2]);
                                                if (bank_string != "")
                                                {
                                                    Vendor_data_array1 = Vendor1.BANK_DETAILS(bank_string, "");
                                                    string rfc_no = "";
                                                    rfc_string = Convert.ToString(Vendor_data_array1[1]);
                                                    if (Vendor_data_array1[1] == "S")
                                                    {
                                                        rfc_string = "S";
                                                        string[] sp_data1 = Convert.ToString(Vendor_data_array1[0]).Split(' ');
                                                        for (int k = 0; k < sp_data1.Length; k++)
                                                        {
                                                            if (Convert.ToString(sp_data1[k]).ToUpper().Contains("SCIL"))
                                                            {
                                                                rfc_no = Convert.ToString(sp_data1[k]).Substring(0, 10);
                                                                //alert_message = rfc_no;
                                                                alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        alert_message = Vendor_data_array1[0];
                                                    }
                                                    string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, txt_Dispatch.Text, "BANK", "", "", "", Convert.ToString(Vendor_data_array1[1]), rfc_no, Convert.ToString(Vendor_data_array1[0]), txt_Dispatch.Text);
                                                }
                                            }
					    
                                            else if (rfc_string != "E" && dt_sap_rfc.Tables[2].Rows.Count == 0)
                                            {
                                                rfc_string = "S";
                                                alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                            }

                                        }
                                    }
                                }

                                if (rfc_string != "E")
                                {
                                    txt_Condition.Text = "1";

                                    isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "update", ref refData, PK_ID, txt_Username.Text, 4);
                                    //isSaved = "false";
                                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                                    {
                                        string[] errmsg = refData.Split(':');
                                    }
                                    else
                                    {
                                        string[] Dval = new string[1];
                                        Dval[0] = txt_Username.Text;
                                        string txtApproverEmail = txtEmailID.Text;

                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "182", "TRAVEL EXPENSE PAYMENT PROCESS", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Dispatch.Text, txtWIID.Text, ref isInserted);
                                        if (isCreate)
                                        {
                                            succeed = succeed + 1;
                                            try
                                            {
                                                string auditid = (string)ActionController.ExecuteAction(initiator, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE PAYMENT PROCESS", "USER", txt_Username.Text, "SUBMIT", "Approved", "0", "0");

                                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Domestic Travel Request Has Been Completed Successfully.</font></pre><pre><font size='3'>Request No: " + txt_Dispatch.Text + "</font></pre> <pre><font size='3'>Created By: " + initiator.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE PAYMENT PROCESS", "SUBMIT", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Dispatch.Text);
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                if (mode.Trim().ToUpper() == "CASH")
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_message + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }
                                        }
                                    }//
                                }
                                else
                                {
                                    if (mode.Trim().ToUpper() == "CASH")
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_message + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                    }
                                }
                            }
                        }
                    }
                       #endregion domestic
                    /*=====================================================================================================================================================*/
                    #region mobile
                    //else 
                    else if (txtProcessID.Text == "16")
                    {
                        string cash_flag, cash_no, bank_flag, bank_no;
                        bank_flag = cash_no = cash_flag = bank_no = "";
                         DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "pgetrequestdata", ref refData, txtWIID.Text);
                         if (dsData != null)
                         {
                             txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_MobileCard_Expense_HDR_Id"]);
                             string initiator = Convert.ToString(dsData.Tables[0].Rows[0]["CREATED_BY"]);
                             string user_mail = Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);
                             string mode = Convert.ToString(dsData.Tables[0].Rows[0]["pay_mode"]);
                             
                                string rfc_string = string.Empty;
                                DataSet dt_sap_rfc = new DataSet();
                                if (mode.ToUpper() == "BANK")
                                {
                                    rfc_string = "S";
                                }
                                if (mode.ToUpper() == "CASH")
                                {
                                    adv_id = "";
                                    string rfc_action = return_msg(txt_Dispatch.Text,adv_id);
                                    dt_sap_rfc = (DataSet)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getcashrfc", ref refData, rfc_action, txt_Dispatch.Text);
                                    if (dt_sap_rfc != null)
                                    {
						DataTable dtManage = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata1", ref refData, txt_Dispatch.Text, "SELECT", "", "", "", "", "", "", "");
                                                if (dtManage != null && dtManage.Rows.Count > 0)
                                                {
                                                    cash_flag = Convert.ToString(dtManage.Rows[0]["CASH_FLAG"]);
                                                    cash_no = Convert.ToString(dtManage.Rows[0]["CASH_NO"]);
                                                    bank_flag = Convert.ToString(dtManage.Rows[0]["BANK_FLAG"]);
                                                    bank_no = Convert.ToString(dtManage.Rows[0]["BANK_NO"]);
                                                }
                                        if (dt_sap_rfc.Tables[1].Rows.Count > 0)
                                        {
					
                                            if (Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][0]).ToUpper() == "NIL")
                                            {
                                                rfc_string = "S";
                                            }
                                            else
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
                                                    string rfc_no = "";
                                                    if (rfc_string == "S")
                                                    {
                                                        string[] sp_data1 = Convert.ToString(Vendor_data_array[0]).Split(' ');
                                                        for (int k = 0; k < sp_data1.Length; k++)
                                                        {
                                                            if (Convert.ToString(sp_data1[k]).Length == 10 && rfc_no == "")
                                                            {
                                                                cash_no = rfc_no = Convert.ToString(sp_data1[k]);
                                                                alert_message = "Data Posted Successfully : " + cash_no;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        alert_message = Vendor_data_array[0];
                                                    }
                                                    string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, txt_Dispatch.Text, "CASH", Convert.ToString(Vendor_data_array[1]).ToUpper(), rfc_no, Convert.ToString(Vendor_data_array[0]), "", "", "", txt_Dispatch.Text);
                                                }
                                                else
                                                {
                                                    rfc_string = "S";
                                                    alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                                }
                                            }
                                            if (rfc_string != "E" && dt_sap_rfc.Tables[2].Rows.Count > 0 && (bank_flag == "E" || bank_flag == ""))
                                            {
                                                Vendor_Portal.Vendor_Portal_DetailsService Vendor1 = new Vendor_Portal.Vendor_Portal_DetailsService();
                                                string[] Vendor_data_array1 = new string[3];
                                                string bank_string = return_bank_string(dt_sap_rfc.Tables[2]);
                                                if (bank_string != "")
                                                {
                                                    Vendor_data_array1 = Vendor1.BANK_DETAILS(bank_string, "");
                                                    rfc_string = Convert.ToString(Vendor_data_array1[1]);
                                                    string rfc_no = "";
                                                    if (Vendor_data_array1[1]=="S")
                                                    {
                                                        rfc_string = "S";
                                                        string[] sp_data1 = Convert.ToString(Vendor_data_array1[0]).Split(' ');
                                                        for (int k = 0; k < sp_data1.Length; k++)
                                                        {
                                                            if (Convert.ToString(sp_data1[k]).ToUpper().Contains("SCIL"))
                                                            {
                                                                rfc_no = Convert.ToString(sp_data1[k]).Substring(0, 10);
                                                                alert_message = rfc_no;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        alert_message = Vendor_data_array1[0];
                                                    }
                                                    string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, txt_Dispatch.Text, "BANK", "", "", "", Convert.ToString(Vendor_data_array1[1]), rfc_no, Convert.ToString(Vendor_data_array1[0]), txt_Dispatch.Text);
                                                }
                                            }
                                            else if (rfc_string == "S" && dt_sap_rfc.Tables[2].Rows.Count == 0)
                                            {
                                                rfc_string = "S";
                                                alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                            }
                                        }
                                    }
                                }

                                if (rfc_string != "E")
                                {
                                    txt_Condition.Text = "1";
                                    string audit = "ACCOUNT PAYABLE  APPROVAL";
                                    //isSaved = "false";
                                    isSaved = (string)ActionController.ExecuteAction("", "Acccount_Payable_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, audit, txt_Username.Text, remark, "Approve");
                                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                                    {
                                        string[] errmsg = refData.Split(':');
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                                    }
                                    else
                                    {
                                        string[] Dval = new string[1];
                                        Dval[0] = txt_Initiator.Text;

                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "149", "ACCOUNT PAYABLE  APPROVAL", "APPROVE", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                        if (isCreate)
                                        {
                                            succeed = succeed + 1;
                                            try
                                            {
                                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Account Payable Request has been Approved.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + initiator.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "MOBILE DATACARD EXPENSE", "SUBMIT", user_mail, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                if (mode.Trim().ToUpper() == "CASH")
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_message + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }
                                        }
                                        //}
                                        
                                    }
                                }
                                else
                                {
                                    if (mode.Trim().ToUpper() == "CASH")
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_message + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                    }
                                }
                         }
                    }
                    #endregion mobile
                    /*=====================================================================================================================================================*/
                    #region Local_C
                    else if (txtProcessID.Text == "15")
                    {
                        string cash_flag, cash_no, bank_flag, bank_no;
                        bank_flag = cash_no = cash_flag = bank_no = "";

                        string ref_data = string.Empty;
                        try
                        {
                            ActionController.DisablePageCaching(this);
                            if (ActionController.IsSessionExpired(this))
                                ActionController.RedirctToLogin(this);
                            else
                            {
                                string audit = "LOCAL CONVEYANCE PAYMENT APPROVAL";
                                DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "pgetrequestdata", ref ref_data, txtWIID.Text);
                                if (dsData != null)
                                {
                                    adv_id = "";
                                    string pk_id = Convert.ToString(dsData.Tables[0].Rows[0]["PK_Local_Conveyance_HDR_Id"]);
                                    string initiator = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                                    string Init_mail = Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);
                                    string pmode = Convert.ToString(dsData.Tables[0].Rows[0]["PAY_MODE"]);
                                    adv_id = Convert.ToString(dsData.Tables[0].Rows[0]["advance_id"]);
                                 string rfc_string = string.Empty;
                                DataSet dt_sap_rfc = new DataSet();
                                if (pmode.ToUpper() == "BANK")
                                {
                                    rfc_string = "S";
                                }
                                if (pmode.ToUpper() == "CASH")
                                {
                                    
                                    string rfc_action = return_msg(txt_Dispatch.Text,adv_id);
                                    dt_sap_rfc = (DataSet)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getcashrfc", ref refData, rfc_action, txt_Dispatch.Text);
                                    if (dt_sap_rfc != null)
                                    {
DataTable dtManage = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata1", ref refData, txt_Dispatch.Text, "SELECT", "", "", "", "", "", "", "");
                                                if (dtManage != null && dtManage.Rows.Count > 0)
                                                {
                                                    cash_flag = Convert.ToString(dtManage.Rows[0]["CASH_FLAG"]);
                                                    cash_no = Convert.ToString(dtManage.Rows[0]["CASH_NO"]);
                                                    bank_flag = Convert.ToString(dtManage.Rows[0]["BANK_FLAG"]);
                                                    bank_no = Convert.ToString(dtManage.Rows[0]["BANK_NO"]);
                                                }	
                                        if (dt_sap_rfc.Tables[1].Rows.Count > 0)
                                        {
						
                                            if (Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][0]).ToUpper() == "NIL")
                                            {
                                                rfc_string = "S";
                                            }
                                            else
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
                                                    string rfc_no = "";
                                                    if (rfc_string == "S")
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
                                                    string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, txt_Dispatch.Text, "CASH", Convert.ToString(Vendor_data_array[1]).ToUpper(), rfc_no, Convert.ToString(Vendor_data_array[0]), "", "", "", txt_Dispatch.Text);
                                                }
                                                else
                                                {
                                                    rfc_string = "S";
                                                    alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                                }
                                            }
                                            if (rfc_string != "E" && dt_sap_rfc.Tables[2].Rows.Count > 0 && (bank_flag == "E" || bank_flag == ""))
                                            {
                                                Vendor_Portal.Vendor_Portal_DetailsService Vendor1 = new Vendor_Portal.Vendor_Portal_DetailsService();
                                                string[] Vendor_data_array1 = new string[3];
                                                string bank_string = return_bank_string(dt_sap_rfc.Tables[2]);
                                                if (bank_string != "")
                                                {
                                                    Vendor_data_array1 = Vendor1.BANK_DETAILS(bank_string, "");
                                                    rfc_string = Convert.ToString(Vendor_data_array1[1]);
                                                    string rfc_no = "";
                                                    if (Vendor_data_array1[1]=="S")
                                                    {
                                                        rfc_string = "S";
                                                        
                                                        string[] sp_data1 = Convert.ToString(Vendor_data_array1[0]).Split(' ');
                                                        for (int k = 0; k < sp_data1.Length; k++)
                                                        {
                                                            if (Convert.ToString(sp_data1[k]).ToUpper().Contains("SCIL"))
                                                            {
                                                                rfc_no = Convert.ToString(sp_data1[k]).Substring(0, 10);
                                                                alert_message = cash_no;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        alert_message = Vendor_data_array1[0];
                                                    }
                                                    string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, txt_Dispatch.Text, "BANK", "", "", "", Convert.ToString(Vendor_data_array1[1]), rfc_no, Convert.ToString(Vendor_data_array1[0]), txt_Dispatch.Text);
                                                }
                                            }
                                            else if (rfc_string == "S" && dt_sap_rfc.Tables[2].Rows.Count == 0)
                                            {
                                                rfc_string = "S";
                                                alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                            }
                                        }
                                    }
                                }

                                if (rfc_string != "E")
                                {
                                    txt_Condition.Text = "1";
                                    isSaved = (string)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Username.Text, "Approve", "Approve", audit);
                                    if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                                    {
                                        string[] errmsg = ref_data.Split(':');
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                                    }
                                    else
                                    {
                                        string[] Dval = new string[1];
                                        Dval[0] = txt_Username.Text;
                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "137", "LOCAL CONVEYANCE PAYMENT APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                        if (isCreate)
                                        {
                                            succeed = succeed + 1;
                                            try
                                            {
                                                string auditid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Local_Conveyance.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "LOCAL CONVEYANCE PAYMENT APPROVAL", "USER", txt_Username.Text, "SUBMIT", "Approved", "0", "0");

                                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Local Conveyance Request has been Approved.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Approved By: " + txt_Username.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE PAYMENT APPROVAL", "SUBMIT", Init_mail, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                if (pmode.Trim().ToUpper() == "CASH")
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_message + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }
                                        }
                                        
                                    }
                                }
                                else
                                {
                                    if (pmode.Trim().ToUpper() == "CASH")
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_message + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                    }
                                }

                                }
                            }
                        }
                        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
                    }
                    #endregion Local_C
                    /*=====================================================================================================================================================*/

                    #region Other_Exp
                    
                    else if (txtProcessID.Text == "19")
                    {
                        string cash_flag, cash_no, bank_flag, bank_no;
                        bank_flag = cash_no = cash_flag = bank_no = "";

                        DataSet dtPay = (DataSet)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetrequestdata", ref refData, txtWIID.Text);
                        if (dtPay != null)
                        {
                            if (dtPay.Tables[0].Rows.Count > 0)
                            {
                                adv_id = "";
                                string PK_ID = Convert.ToString(dtPay.Tables[0].Rows[0]["pk_other_expense_hdr_id"]);
                                string req_no = Convert.ToString(dtPay.Tables[0].Rows[0]["voucher_id"]);
                                string initiator = Convert.ToString(dtPay.Tables[0].Rows[0]["user_adid"]);
                                Init_Email.Text = Convert.ToString(dtPay.Tables[0].Rows[0]["EMAIL_ID"]);
                                string pmode = Convert.ToString(dtPay.Tables[0].Rows[0]["P_MODE"]);
                                string loc = Convert.ToString(dtPay.Tables[0].Rows[0]["P_LOCATION"]);
                                string EMPLOYEE_NAME = Convert.ToString(dtPay.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                                adv_id = Convert.ToString(dtPay.Tables[0].Rows[0]["advance_id"]);
                                

                                DataTable DTAP = new DataTable();
                               
                                 string rfc_string = string.Empty;
                                DataSet dt_sap_rfc = new DataSet();
                                if (pmode.ToUpper() == "BANK")
                                {
                                    rfc_string = "S";
                                }
                                if (pmode.ToUpper() == "CASH")
                                {
                                    
                                    string rfc_action = return_msg(txt_Dispatch.Text,adv_id);
                                    dt_sap_rfc = (DataSet)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getcashrfc", ref refData, rfc_action, txt_Dispatch.Text);
                                    if (dt_sap_rfc != null)
                                    {
                                         DataTable dtManage = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata1", ref refData, txt_Dispatch.Text, "SELECT", "", "", "", "", "", "", "");
                                                    if (dtManage != null && dtManage.Rows.Count > 0)
                                                    {
                                                        cash_flag = Convert.ToString(dtManage.Rows[0]["CASH_FLAG"]);
                                                        cash_no = Convert.ToString(dtManage.Rows[0]["CASH_NO"]);
                                                        bank_flag = Convert.ToString(dtManage.Rows[0]["BANK_FLAG"]);
                                                        bank_no = Convert.ToString(dtManage.Rows[0]["BANK_NO"]);
							
                                                    }
                                        /**************************************************Without Advance************************************************************/
                                        if (rfc_action == "OTHER EXPENSE WITHOUT ADVANCE")
                                        {
                                            if (dt_sap_rfc.Tables[1].Rows.Count > 0)
                                            {
						
                                                if (Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][0]).ToUpper() == "NIL")
                                                {
                                                    rfc_string = "S";
                                                }
                                                else
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
                                                        string rfc_no = "";
                                                        if (rfc_string == "S")
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
                                                        string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, txt_Dispatch.Text, "CASH", Convert.ToString(Vendor_data_array[1]).ToUpper(), rfc_no, Convert.ToString(Vendor_data_array[0]), "", "", "", txt_Dispatch.Text);
                                                    }
                                                    else
                                                    {
                                                        rfc_string = "S";
                                                        alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                                    }
                                                }
                                                if (rfc_string != "E" && dt_sap_rfc.Tables[2].Rows.Count > 0 && (bank_flag == "E" || bank_flag == ""))
                                                {
                                                    Vendor_Portal.Vendor_Portal_DetailsService Vendor1 = new Vendor_Portal.Vendor_Portal_DetailsService();
                                                    string[] Vendor_data_array1 = new string[3];
                                                    string bank_string = return_bank_string(dt_sap_rfc.Tables[2]);
                                                    if (bank_string != "")
                                                    {
                                                        //bank_string = "asd";
                                                        Vendor_data_array1 = Vendor1.BANK_DETAILS(bank_string, "");
                                                        rfc_string = Convert.ToString(Vendor_data_array1[1]);
                                                        string rfc_no = "";
                                                        if (Vendor_data_array1[1] == "S")
                                                        {
                                                            rfc_string = "S";
                                                            string[] sp_data1 = Convert.ToString(Vendor_data_array1[0]).Split(' ');
                                                            for (int k = 0; k < sp_data1.Length; k++)
                                                            {
                                                                if (Convert.ToString(sp_data1[k]).ToUpper().Contains("SCIL"))
                                                                {
                                                                    rfc_no = Convert.ToString(sp_data1[k]).Substring(0, 10);
                                                                    alert_message = cash_no;
                                                                }
                                                            }

                                                        }
                                                        else
                                                        {
                                                            alert_message = Vendor_data_array1[0];
                                                        }
                                                        string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, txt_Dispatch.Text, "BANK", "", "", "", Convert.ToString(Vendor_data_array1[1]), rfc_no, Convert.ToString(Vendor_data_array1[0]), txt_Dispatch.Text);
                                                        alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                                    }
                                                }
                                                else if (rfc_string == "S" && dt_sap_rfc.Tables[2].Rows.Count == 0)
                                                {
                                                    rfc_string = "S";
                                                    alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                                }
                                            }
                                        }
                                        /**************************************************Without Advance************************************************************/
                                        /**************************************************With Advance************************************************************/
                                        else
                                        {
                                            if (dt_sap_rfc.Tables[1].Rows.Count > 0)
                                            {
                                                //if (Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][0]).ToUpper() == "NIL")
                                                //{
                                                //    rfc_string = "S";
                                                //}
                                                //else
                                                //{
                                                  /*  DataTable dtManage = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata1", ref refData, txt_Dispatch.Text, "SELECT", "", "", "", "", "", "", "");
                                                    if (dtManage != null && dtManage.Rows.Count > 0)
                                                    {
                                                        cash_flag = Convert.ToString(dtManage.Rows[0]["CASH_FLAG"]);
                                                        cash_no = Convert.ToString(dtManage.Rows[0]["CASH_NO"]);
                                                        bank_flag = Convert.ToString(dtManage.Rows[0]["BANK_FLAG"]);
                                                        bank_no = Convert.ToString(dtManage.Rows[0]["BANK_NO"]);
							rfc_string = bank_flag;
                                                    }*/
                                                    
                                               // }
                                                if (dt_sap_rfc.Tables[2].Rows.Count > 0 && (bank_flag == "E" || bank_flag == ""))
                                                {
                                                    Vendor_Portal.Vendor_Portal_DetailsService Vendor1 = new Vendor_Portal.Vendor_Portal_DetailsService();
                                                    string[] Vendor_data_array1 = new string[3];
                                                    string bank_string = return_bank_string(dt_sap_rfc.Tables[2]);
                                                    if (bank_string != "")
                                                    {
                                                        //bank_string = "asd";
                                                        Vendor_data_array1 = Vendor1.BANK_DETAILS(bank_string, "");
                                                        rfc_string = Convert.ToString(Vendor_data_array1[1]);
                                                        string rfc_no = "";
                                                        if (Vendor_data_array1[1] == "S")
                                                        {
                                                            rfc_string = "S";
                                                            string[] sp_data1 = Convert.ToString(Vendor_data_array1[0]).Split(' ');
                                                            for (int k = 0; k < sp_data1.Length; k++)
                                                            {
                                                                if (Convert.ToString(sp_data1[k]).ToUpper().Contains("SCIL"))
                                                                {
                                                                    bank_no = rfc_no = Convert.ToString(sp_data1[k]).Substring(0, 10);
                                                                    alert_message = "Data Posted Successfully : " + bank_no;
                                                                }
                                                            }

                                                        }
                                                        else
                                                        {
                                                            alert_message = Vendor_data_array1[0];
                                                        }
                                                        string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, txt_Dispatch.Text, "BANK", "", "", "", Convert.ToString(Vendor_data_array1[1]), rfc_no, Convert.ToString(Vendor_data_array1[0]), txt_Dispatch.Text);
                                                        alert_message = "Data Posted Successfully : " + Convert.ToString(bank_no);
                                                    }
                                                }
                                                else if (rfc_string == "S" && dt_sap_rfc.Tables[2].Rows.Count > 0)
                                                {
                                                    rfc_string = "S";
                                                    alert_message = "Data Posted Successfully : " + Convert.ToString(bank_no);
                                                }

                                                if (rfc_string != "E" && (cash_flag == "E" || cash_flag == ""))
                                                {
						    if(Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][0]) == "NIL")
						    {
                                                        rfc_string = "S";
                                                    }
else
{
                                                    Vendor_Portal.Vendor_Portal_DetailsService Vendor = new Vendor_Portal.Vendor_Portal_DetailsService();
                                                    string[] Vendor_data_array = new string[3];
                                                    Vendor_data_array = Vendor.CASH_LEGDER(Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][0]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][1]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][2]),
                                                                Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][3]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][4]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][5]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][6]),
                                                                Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][7]), "", Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][9]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][10]),
                                                                Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][11]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][12]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][13]),
                                                                Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][14]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][15]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][16]), Convert.ToString(dt_sap_rfc.Tables[1].Rows[0][17]));
                                                    rfc_string = Vendor_data_array[1];
                                                    string rfc_no = "";
                                                    if (rfc_string == "S")
                                                    {
                                                        string[] sp_data1 = Convert.ToString(Vendor_data_array[0]).Split(' ');
                                                        for (int k = 0; k < sp_data1.Length; k++)
                                                        {
                                                            if (Convert.ToString(sp_data1[k]).Length == 10 && rfc_no == "")
                                                            {
                                                                bank_no = rfc_no = Convert.ToString(sp_data1[k]);
                                                                alert_message = "Data Posted Successfully : " + Convert.ToString(bank_no);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        alert_message = Vendor_data_array[0];
                                                    }
                                                    string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, txt_Dispatch.Text, "CASH", Convert.ToString(Vendor_data_array[1]).ToUpper(), rfc_no, Convert.ToString(Vendor_data_array[0]), "", "", "", txt_Dispatch.Text);
}
                                                }
                                                else
                                                {
                                                    rfc_string = "S";
                                                    alert_message = "Data Posted Successfully : " + Convert.ToString(bank_no);
                                                }
                                            }
                                        }
                                        /**************************************************With Advance************************************************************/
                                    }
                                }

                                if (rfc_string != "E")
                                {

                                    txt_Condition.Text = "4";
                                    isSaved = (string)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "update", ref refData, PK_ID, txt_Username.Text, 4);

                                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                                    {
                                        string[] errmsg = refData.Split(':');
                                    }
                                    else
                                    {

                                        string txtApproverEmail = "";
                                        string[] Dval = new string[1];
                                        Dval[0] = "";

                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "199", "OTHER EXPENSE PAYMENT PROCESS", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, req_no, txtWIID.Text, ref isInserted);
                                        if (isCreate)
                                        {

                                            succeed = succeed + 1;
                                            try
                                            {
                                                string auditid = (string)ActionController.ExecuteAction(initiator, "Other_Expenses_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "OTHER EXPENSE PAYMENT PROCESS", "USER", txt_Username.Text, "SUBMIT", "Approved", "0", "0");

                                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Payment Has Been Processed Successfully For Other Expense Request.</font></pre><pre><font size='3'>Request No: " + req_no + "</font></pre> <pre><font size='3'>Created By: " + EMPLOYEE_NAME.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "OTHER EXPENSE PAYMENT PROCESS", "SUBMIT", Init_Email.Text, "", msg, "Request No: " + req_no);
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                if (pmode.Trim().ToUpper() == "CASH")
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_message + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                }
                                            }

                                        }
                                        
                                    }//
                                }
                                else
                                {
                                    if (pmode.Trim().ToUpper() == "CASH")
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_message + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                    }
                                }
                            }
                        }
                    }
                    
                    #endregion 
                    /*=====================================================================================================================================================*/

                    #region Advance

                    else if (txtProcessID.Text == "17")
                    {
                        string cash_flag, cash_no, bank_flag, bank_no;
                        bank_flag = cash_no = cash_flag = bank_no = "";

                        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getdetails", ref ISValid, txtProcessID.Text,txtInstanceID.Text);
                        if (dsData != null)
                        {
                            string PK_ID = Convert.ToString(dsData.Tables[0].Rows[0]["PK_ADVANCE_HDR_Id"]);
                            string initiator = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                            Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMAIL_ID"]);
                            string mode= Convert.ToString(dsData.Tables[0].Rows[0]["pay_mode"]);

                            adv_id = "";
                              string rfc_string = string.Empty;
                                DataSet dt_sap_rfc = new DataSet();
                                if (mode.ToUpper() == "BANK")
                                {
                                    rfc_string = "S";
                                }
                                if (mode.ToUpper() == "CASH")
                                {
                                    
                                    string rfc_action = return_msg(txt_Dispatch.Text,adv_id);
                                    dt_sap_rfc = (DataSet)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getcashrfc", ref refData, rfc_action, txt_Dispatch.Text);
                                    if (dt_sap_rfc != null)
                                    {
DataTable dtManage = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata1", ref refData, txt_Dispatch.Text, "SELECT", "", "", "", "", "", "", "");
                                                if (dtManage != null && dtManage.Rows.Count > 0)
                                                {
                                                    cash_flag = Convert.ToString(dtManage.Rows[0]["CASH_FLAG"]);
                                                    cash_no = Convert.ToString(dtManage.Rows[0]["CASH_NO"]);
                                                    bank_flag = Convert.ToString(dtManage.Rows[0]["BANK_FLAG"]);
                                                    bank_no = Convert.ToString(dtManage.Rows[0]["BANK_NO"]);
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
                                                    string rfc_no = "";
                                                    if (rfc_string == "S")
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
                                                    string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref refData, txt_Dispatch.Text, "CASH", Convert.ToString(Vendor_data_array[1]).ToUpper(), rfc_no, Convert.ToString(Vendor_data_array[0]), "", "", "", txt_Dispatch.Text);
                                                }
                                                else
                                                {
                                                    rfc_string = "S";
                                                    alert_message = "Data Posted Successfully : " + Convert.ToString(cash_no);
                                                }
                                        }
                                    }
                                }

                                if (rfc_string != "E")
                                {
                                    string audit = "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";

                                    //isSaved = "false";
                                    isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "update", ref ISValid, 1, txtProcessID.Text, txtInstanceID.Text, audit, txt_Username.Text, remark, "Approve");
                                    if (isSaved == null || ISValid.Length > 0 || isSaved == "false")
                                    {
                                        string[] errmsg = ISValid.Split(':');
                                    }
                                    else
                                    {
                                        string[] Dval = new string[1];
                                        Dval[0] = txt_Username.Text;

                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "168", "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "APPROVE", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                        if (isCreate)
                                        {
                                            succeed = succeed + 1;
                                            try
                                            {
                                                //string auditid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Advance_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "USER", txt_Username.Text, "APPROVE", txtRemark.Value, "0", "0");

                                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request Request has been Approved.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "APPROVE", Init_Email.Text, "", msg, "Request No: " + txt_Request.Text);
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                if (mode.Trim().ToUpper() == "CASH")
                                                {
                                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_message + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                                } 
                                            }
                                        }
                                    }//
                                }
                                else
                                {
                                    if (mode.Trim().ToUpper() == "CASH")
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + alert_message + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                    }
                                }

                            
                        }
                    }

                    #endregion Advance

                    /*=====================================================================================================================================================*/
		            /*=====================================================================================================================================================*/

		            #region car_expense
                    else if (txtProcessID.Text == "22")
                    {
                        string isdata = string.Empty;
                        DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getpkcarexpns", ref isdata, txtProcessID.Text, txtInstanceID.Text);
                        if (dsData != null && dsData.Rows.Count > 0)
                        {

                            string rno = Convert.ToString(dsData.Rows[0]["CAR_EXPENSE_NO"]);
                            string init = Convert.ToString(dsData.Rows[0]["AD_ID"]);
                            string email_id = Convert.ToString(dsData.Rows[0]["INIT_MAIL"]);
                            string emp_name = "";

                            DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Car_Expense.aspx", "pgettraveluser", ref isdata, init);
                            if (dtUser.Rows.Count > 0)
                            {
                                emp_name = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);

                            }

                            isSaved = (string)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "update", ref refData, txtProcessID.Text, txtInstanceID.Text, "", txt_Username.Text, "15");
                            if (isSaved == null || refData.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = refData.Split(':');
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                            }
                            else
                            {
                                string[] Dval = new string[1];
                                Dval[0] = "";
                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "238", "CAR EXPENSE PAYMENT APPROVAL", "SUBMIT", "", txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, rno, txtWIID.Text, ref isInserted);
                                if (isCreate)
                                {
                                    try
                                    {
                                        succeed = succeed + 1;
                                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/><pre><font size='3'>The Car Expense request has been Approved Successfully.</font></pre><pre><font size='3'>Car Expense No: " + rno  + "</font></pre><pre><font size='3'>Created By: " + emp_name + "</font></pre></p><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                        string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "CAR EXPENSE PAYMENT APPROVAL", "SUBMIT", email_id, "", msg, "Car Expense No: " + rno);
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }
                                    finally
                                    {
                                        //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense Request has been Approved Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                    }
                                }
                            }
                        }
                    
                    }

                    #endregion car_expense

                    /*=====================================================================================================================================================*/
                }
                if (ddlPay_Mode.SelectedItem.Text.Trim().ToUpper() == "BANK")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + succeed + " out of " + sp_data.Length + " - Expense Request Has Been Completed Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                }
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + ex.ToString() + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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

                                isSaved = (string)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "update", ref refData, PK_ID, txt_Username.Text, 3);
                                if (isSaved == null || refData.Length > 0 || isSaved == "false")
                                {
                                    string[] errmsg = refData.Split(':');
                                }
                                else
                                {

                                    string[] Dval = new string[1];
                                    Dval[0] = initiator;

                                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "184", "TRAVEL EXPENSE PAYMENT PROCESS", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Dispatch.Text, txtWIID.Text, ref isInserted);
                                    if (isCreate)
                                    {
                                        succeed = succeed + 1;
                                        try
                                        {
                                            string auditid = (string)ActionController.ExecuteAction(initiator, "Domestic_Travel_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "USER", txt_Username.Text, "REJECT", "Rejected", "0", "0");

                                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Travel Request Has Been Rejected.</font></pre><pre><font size='3'>Request No: " + txt_Dispatch.Text + "</font></pre> <pre><font size='3'>Created By: " + initiator.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                            string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Domestic_Travel_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "TRAVEL EXPENSE DOCUMENT VERIFICATION", "REJECT", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Dispatch.Text);
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
                       #endregion domestic
                    /****************************************************************************************************************************************************/
                    //else 
                    else if (txtProcessID.Text == "16")
                    {
                        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "pgetrequestdata", ref refData, txtWIID.Text);
                        if (dsData != null)
                        {
                            txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_MobileCard_Expense_HDR_Id"]);
                            string initiator = Convert.ToString(dsData.Tables[0].Rows[0]["CREATED_BY"]);
                            string user_mail = Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);

                            txt_Condition.Text = "2";
                            string audit = "ACCOUNT PAYABLE  APPROVAL";
                            isSaved = (string)ActionController.ExecuteAction("", "Acccount_Payable_Approval.aspx", "update", ref refData, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, audit, txt_Username.Text, "", "Reject");
                            if (isSaved == null || refData.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = refData.Split(':');
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                            }
                            else
                            {

                                string[] Dval = new string[1];
                                Dval[0] = txt_Initiator.Text;
                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "151", "ACCOUNT PAYABLE  APPROVAL", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                if (isCreate)
                                {
                                    succeed = succeed + 1;
                                    try
                                    {
                                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Account Payable Request has been Rejected.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + initiator.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                        string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Mobile_DataCard_Expense_Approval.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ACCOUNT PAYABLE  APPROVAL", "REJECT", user_mail, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }
                                    finally
                                    {
                                       
                                    }
                                }
                                // }
                            }
                        }
                    }

                     /****************************************************************************************************************************************************/
                    else if (txtProcessID.Text == "15")
                    {
                        try
                        {
                            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "pgetrequestdata", ref isInserted, txtWIID.Text);
                            if (dsData != null)
                            {
                                string pk_id = Convert.ToString(dsData.Tables[0].Rows[0]["PK_Local_Conveyance_HDR_Id"]);
                                string initiator = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                                string user_mail = Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);

                                isSaved = (string)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "update", ref isInserted, pk_id, "", 3);
                                if (isSaved == null || isInserted.Length > 0 || isSaved == "false")
                                {
                                    string[] errmsg = isInserted.Split(':');
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                                }
                                else
                                {
                                    string[] Dval = new string[1];
                                    Dval[0] = txt_Username.Text;
                                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "139", "LOCAL CONVEYANCE PAYMENT APPROVAL", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                    if (isCreate)
                                    {
                                        succeed = succeed + 1;
                                        try
                                        {
                                            //string auditid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Local_Conveyance.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "LOCAL CONVEYANCE PAYMENT APPROVAL", "USER", txt_Username.Text, "REJECT", txt_Remark.Value, "0", "0");

                                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Local Conveyance Request has been Rejected.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + initiator.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                            string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE PAYMENT APPROVAL", "REJECT", user_mail, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                        }
                                        catch (Exception)
                                        {
                                            throw;
                                        }
                                        finally
                                        {
                                         
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    /****************************************************************************************************************************************************/

                    #region Other_Exp

                    else if (txtProcessID.Text == "19")
                    {
                        DataSet dtPay = (DataSet)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "pgetrequestdata", ref refData, txtWIID.Text);
                        if (dtPay != null)
                        {
                            if (dtPay.Tables[0].Rows.Count > 0)
                            {
                                string PK_ID = Convert.ToString(dtPay.Tables[0].Rows[0]["pk_other_expense_hdr_id"]);
                                string req_no = Convert.ToString(dtPay.Tables[0].Rows[0]["voucher_id"]);
                                string initiator = Convert.ToString(dtPay.Tables[0].Rows[0]["user_adid"]);
                                Init_Email.Text = Convert.ToString(dtPay.Tables[0].Rows[0]["EMAIL_ID"]);
                                string EMPLOYEE_NAME = Convert.ToString(dtPay.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                                isSaved = (string)ActionController.ExecuteAction("", "Other_Expenses_Request_Approval.aspx", "update", ref refData, PK_ID, txt_Username.Text, 3);
                                if (isSaved == null || refData.Length > 0 || isSaved == "false")
                                {
                                    string[] errmsg = refData.Split(':');
                                }
                                else
                                {

                                    string[] Dval = new string[1];
                                    Dval[0] = initiator;

                                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "198", "OTHER EXPENSE PAYMENT PROCESS", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, req_no, txtWIID.Text, ref isInserted);
                                    if (isCreate)
                                    {
                                        succeed = succeed + 1;
                                        try
                                        {
                                            string auditid = (string)ActionController.ExecuteAction(initiator, "Other_Expenses_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "OTHER EXPENSE PAYMENT PROCESS", "USER", txt_Username.Text, "REJECT", "Rejected", "0", "0");

                                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><pre><font size='3'>Other Expense Request Has Been Rejected.</font></pre><pre><font size='3'>Request No: " + req_no + "</font></pre> <pre><font size='3'>Created By: " + EMPLOYEE_NAME.Trim() + "</font></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                            string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Other_Expenses_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "OTHER EXPENSE PAYMENT PROCESS", "REJECT", Init_Email.Text, "", msg, "Request No: " + req_no);
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
                        }
                    }
                    

                    #endregion
                    /****************************************************************************************************************************************************/

                    else if (txtProcessID.Text == "17")
                    {
                        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getdetails", ref ISValid, txtProcessID.Text,txtInstanceID.Text);
                        if (dsData != null)
                        {
                            string PK_ID = Convert.ToString(dsData.Tables[0].Rows[0]["PK_ADVANCE_HDR_Id"]);
                            string initiator = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                            Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMAIL_ID"]);

                            string audit = "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL";
                            isSaved = (string)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "update", ref isInserted, 2, txtProcessID.Text, txtInstanceID.Text, audit, txt_Username.Text, remark, "Reject");
                            if (isSaved == null || isInserted.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = isInserted.Split(':');
                            }
                            else
                            {
                                string[] Dval = new string[1];
                                Dval[0] = txt_Username.Text;
                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "166", "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                                if (isCreate)
                                {
                                    succeed = succeed + 1;
                                    try
                                    {
                                        // string auditid = (string)ActionController.ExecuteAction(txt_Initiator.Text, "Advance_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "USER", txt_Username.Text, "REJECT", txtRemark.Value, "0", "0");

                                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been Rejected.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                        string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", "REJECT", Init_Email.Text, "", msg, "Request No: " + txt_Request.Text);
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }
                                    finally
                                    {
                                     
                                    }
                                }
                            }
                        }
                    }
                

                /****************************************************************************************************************************************************/
		 
		#region car_expense
                    else if (txtProcessID.Text == "22")
                    {
                        string isdata = string.Empty;
                        DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getpkcarexpns", ref isdata, txtProcessID.Text, txtInstanceID.Text);
                        if (dsData != null && dsData.Rows.Count > 0)
                        {

                            string rno = Convert.ToString(dsData.Rows[0]["CAR_EXPENSE_NO"]);
                            string init = Convert.ToString(dsData.Rows[0]["AD_ID"]);
                            string email_id = Convert.ToString(dsData.Rows[0]["INIT_MAIL"]);
                            string emp_name = "";

                            DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Car_Expense.aspx", "pgettraveluser", ref isdata, init);
                            if (dtUser.Rows.Count > 0)
                            {
                                emp_name = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);

                            }

                            isSaved = (string)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "update", ref refData, txtProcessID.Text, txtInstanceID.Text, "", txt_Username.Text, "15");
                            if (isSaved == null || refData.Length > 0 || isSaved == "false")
                            {
                                string[] errmsg = refData.Split(':');
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                            }
                            else
                            {
                                string[] Dval = new string[1];
                                Dval[0] = "";
                                bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "236", "CAR EXPENSE PAYMENT APPROVAL", "REJECT", "", txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, rno, txtWIID.Text, ref isInserted);
                                if (isCreate)
                                {
                                    try
                                    {
                                        succeed = succeed + 1;
                                        string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/><pre><font size='3'>The Car Expense request has been Rejected.</font></pre><pre><font size='3'>Car Expense No: " + rno  + "</font></pre><pre><font size='3'>Created By: " + emp_name + "</font></pre></p><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                        string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Early_Payment_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "CAR EXPENSE PAYMENT APPROVAL", "REJECT", email_id, "", msg, "Car Expense No: " + rno);
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }
                                    finally
                                    {
                                        //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense Request has been Approved Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                    }
                                }
                            }
                        }
                    
                    }

                    #endregion car_expense
}
                    /*=====================================================================================================================================================*/
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + succeed + " out of " + sp_data.Length + " - Travel Expense Request Has Been Rejected...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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
    public string fillDocumentDetails(string proc_name, string data)
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
//                    DisplayData += "<tr><td><a href='#' onclick=downloadfiles('" + (i + 1) + "')>" + Convert.ToString(dsData.Rows[i]["filename"]) + "</a></td><td style='display:none'><input type='text' id='request_no_" + (i + 1) + "' value='" + Convert.ToString(dsData.Rows[i]["object_value"]) + "'></td></tr>";
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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getDetails(string req_no, string process_name)
    {
        string sb = "";
        try
        {
            Expense_Details ed = new Expense_Details();
            sb = ed.Expense_Request_Details(req_no,process_name);
        }
        catch (Exception ex)
        {
            sb="";
        }
        return sb.ToString();
    }

    protected string return_msg(string req_no,string adv_id)
    {
        string rfc_msg = "";
        if (req_no.Contains("ADV") == true)
        {
            rfc_msg = "ADVANCE PAID";
        }
        else if (req_no.Contains("DTE") == true)
        {
            if (adv_id == "" || adv_id=="0")
            {
                rfc_msg = "DOMESTIC TRAVEL WITHOUT ADVANCE";
            }
            else
            {
                rfc_msg = "DOMESTIC TRAVEL WITH ADVANCE";
            }
        }
        else if (req_no.Contains("MCE") == true)
        {
            rfc_msg = "MOBILE EXPENSE WITHOUT ADVANCE";
        }
        else if (req_no.Contains("OE") == true)
        {
            if (adv_id == "" || adv_id == "0")
            {
                rfc_msg = "OTHER EXPENSE WITHOUT ADVANCE";
            }
            else
            {
                rfc_msg = "OTHER EXPENSE WITH ADVANCE";
            }
        }
        else if (req_no.Contains("LC") == true)
        {
            if (adv_id == "" || adv_id == "0")
            {
                rfc_msg = "LOCAL CONVEYANCE WITHOUT ADVANCE";
            }
            else
            {
                rfc_msg = "LOCAL CONVEYANCE WITH ADVANCE";
            }
        }
        return rfc_msg;

    }

    protected string return_bank_string(DataTable dt_sap_rfc)
    {
        string rfc_string = "";
        try
        {
            if (dt_sap_rfc != null)
            {
                if (dt_sap_rfc.Rows.Count > 0)
                {
                    for (int index = 0; index < dt_sap_rfc.Rows.Count; index++)
                    {
                        if (rfc_string == "")
                        {
                            rfc_string += Convert.ToString(dt_sap_rfc.Rows[index]["COMP_CODE"]);
                        }
                        else
                        {
                            rfc_string += "|" + Convert.ToString(dt_sap_rfc.Rows[index]["COMP_CODE"]);
                        }
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["DOC_DATE"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["PSTNG_DATE"]);
                        rfc_string += "$";
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["DOC_TYPE"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["REF_DOC_NO"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["ITEMNO_ACC"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["GL_ACCOUNT"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["REF_KEY_1"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["VENDOR_NO"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["ALLOC_NMBR"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["ITEM_TEXT"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["BUS_AREA"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["COSTCENTER"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["PROFIT_CTR"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["CURRENCY"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["AMT_DOCCUR"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["ZLSCH"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["PERSON_NO"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["SECCO"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["BUPLA"]);
                        rfc_string += "$" + Convert.ToString(dt_sap_rfc.Rows[index]["ZFBDT"]);
                        rfc_string += "$ ";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            rfc_string = "";
        }
        return rfc_string;
    }

}