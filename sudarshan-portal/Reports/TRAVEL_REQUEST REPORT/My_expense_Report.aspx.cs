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

public partial class My_expense_Report : System.Web.UI.Page
{
    Expense_Data ex_data = new Expense_Data();
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(My_expense_Report));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    lnkText.Text = "1";
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username1.Text = txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        txtProcessID.Text = "10";
                    }
                    txt_f_date.Attributes.Add("readonly", "readonly");
                    txt_t_date.Attributes.Add("readonly", "readonly");
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
            Response.Redirect("../../Master.aspx?M_ID=" + 4);
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string isvalid = "";
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Request_Status_Report.aspx", "getmyexpenserequest", ref isvalid, txt_Username.Text, txt_Search.Text, ddlStatus.SelectedValue, txt_f_date.Text, txt_t_date.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("Sr.No.", typeof(int));
                dtNew.Columns.Add("Request No.", typeof(string));
                dtNew.Columns.Add("Date", typeof(string));
                dtNew.Columns.Add("Payment Mode", typeof(string));
                dtNew.Columns.Add("Amount", typeof(string));
                dtNew.Columns.Add("Request Status", typeof(string));
                dtNew.Columns.Add("To Whom (Approver)", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtNew.Rows.Add((i + 1), Convert.ToString(dt.Rows[i]["request_no"]), Convert.ToString(dt.Rows[i]["created_date"]), Convert.ToString(dt.Rows[i]["PAYMENT_MODE"]), Convert.ToString(dt.Rows[i]["Amount"]), Convert.ToString(dt.Rows[i]["status"]), Convert.ToString(dt.Rows[i]["Approver"]));
                }

                DataGrid dg = new DataGrid();
                dg.DataSource = dtNew;
                dg.DataBind();

                string sFileName = "Report List - My Requests Status - " + txt_Username.Text + ".xls";
                sFileName = sFileName.Replace("/", "");

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
                Response.ContentType = "application/vnd.ms-excel";
                EnableViewState = false;

                System.IO.StringWriter objSW = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter objHTW = new System.Web.UI.HtmlTextWriter(objSW);

                dg.HeaderStyle.Font.Bold = true;     // SET EXCEL HEADERS AS BOLD.
                dg.RenderControl(objHTW);

                // STYLE THE SHEET AND WRITE DATA TO IT.
                Response.Write("<style> TABLE { border:dotted 1px #999; } " +
                    "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
                Response.Write(objSW.ToString());

                Response.End();
                dg = null;
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Records Found');}</script>");
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

   

    #region userdefined

  [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string check_avail(string req_no)
    {
	string isData = string.Empty;
	string ret = (string)ActionController.ExecuteAction("", "Request_Status_Report.aspx", "checkavail", ref isData, req_no);
	return ret;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string get_BulkRequests(string name, int pageno, int rpp, string status, string fdate, string tdate)
    {
        StringBuilder tblHeader = new StringBuilder();
        StringBuilder tblBody = new StringBuilder();
        string tblHTML = string.Empty;
        DataTable DT = new DataTable();
        string username = Convert.ToString(Session["USER_ADID"]);
        string isData = string.Empty;
        
        try
        {
            DT = (DataTable)ActionController.ExecuteAction("", "Request_Status_Report.aspx", "getmyexpenserequest", ref isData, username, name, status,fdate,tdate);

            tblHeader.Append("<th>#</th><th>Request No.</th><th>Date</th><th>Payment Mode</th><th>Payable(+)/Receivable(-) Amount</th><th>Request Status</th><th>To Whom (Approver)</th><th>Documents</th><th>Voucher</th>");
            int ddl = rpp;
            if (pageno == 0)
            {
                pageno = 1;
            }
            int from = (pageno - 1) * ddl;
            int to = ((pageno - 1) * ddl) + ddl;
            for (int i = from; i < to; i++)
            {
                if (i < DT.Rows.Count)
                {
                    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
                    string param_Pro = "";
                    string param_req = "";
                    string voucher_link = "";
                    string dummy = "";
                    if(Convert.ToString(DT.Rows[i]["process_name"]).ToUpper()=="CAR POLICY")
                    {
                        param_Pro = "";
                        param_req = Convert.ToString(DT.Rows[i]["REQUEST_NO"]);
                        voucher_link = "http://" + compname + "/Sudarshan-Portal/Vouchers/Car_Voucher.aspx?P=" + param_Pro + "&R=" + param_req;
                    }
                    else if (Convert.ToString(DT.Rows[i]["process_name"]).ToUpper() == "ADVANCE REQUEST FOREIGN")
                    {
                        param_Pro = "";
                        param_req = Convert.ToString(DT.Rows[i]["REQUEST_NO"]);
                        voucher_link = "http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Advance.aspx?P=" + param_Pro + "&R=" + param_req;
                    }
                    else if (Convert.ToString(DT.Rows[i]["process_name"]).ToUpper() == "FOREIGN TRAVEL EXPENSE")
                    {
                        param_Pro = "";
                        param_req = Convert.ToString(DT.Rows[i]["REQUEST_NO"]);
                        voucher_link = "http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Voucher.aspx?P=" + param_Pro + "&R=" + param_req;
                    }
                    else
                    {
                        CryptoGraphy crypt = new CryptoGraphy();
                        if (Convert.ToString(DT.Rows[i]["process_name"]) == "ADVANCE REQUEST")
                        {
                            dummy = "ADVANCE EXPENSE";
                        }
                        else
                        {
                            dummy = Convert.ToString(DT.Rows[i]["process_name"]);
                        }
                        param_Pro = crypt.Encryptdata(Convert.ToString(DT.Rows[i]["process_name"]));
                        param_req = crypt.Encryptdata(Convert.ToString(DT.Rows[i]["REQUEST_NO"]));
                        if (Convert.ToString(DT.Rows[i]["PAYMENT_MODE"]).ToUpper() == "CASH")
                        {
                            voucher_link = "http://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + param_Pro + "&R=" + param_req;
                        }
                        else
                        {
                            voucher_link = "http://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + param_Pro + "&R=" + param_req;
                        }
                    }
                    
                    
                    //<a href='https://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>https://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a>
                    
                    tblBody.Append("<tr><td align='center'><input type='text' id='fk_process" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["FK_PROCESS_ID"]) + " style='display:none'/><input type='text' id='pname" + (i + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["process_name"]) + "' style='display:none'/><input type='text' id='h_info" + (i + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["REQUEST_NO"]) + "' style='display:none'/></input><input type='text' id='iid" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["FK_INSTANCE_ID"]) + " style='display:none'></input><input type='text' id='wiid" + (i + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + "' style='display:none'></input>" + (i + 1) + "</td><td><a href='#req_Details' role='button' data-toggle='modal' id='req_no" + (i + 1) + "' onclick='getRequestDetails(" + (i + 1) + ")'>" + Convert.ToString(DT.Rows[i]["request_no"]) + "</a></td><td align='center'>" + Convert.ToString(DT.Rows[i]["created_date"]) + "</td><td align='center'>" + DT.Rows[i]["PAYMENT_MODE"].ToString() + "</td><td align='right'>" + DT.Rows[i]["Amount"].ToString() + "</td><td align='center'>" + DT.Rows[i]["status"].ToString() + "</td><td>" + DT.Rows[i]["Approver"].ToString() + "</td><td align='center'><a href='#Documents' role='button' data-toggle='modal' onclick='Bind_Documents(" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + "," + Convert.ToString(DT.Rows[i]["fk_process_id"]) + ")'>View</a></td><td><a href='#' onclick=download_Link('"+voucher_link+"','" + Convert.ToString(DT.Rows[i]["REQUEST_NO"]) + "')>Voucher Print</a></td></tr>");

                }
            }
            tblHTML = "<table id='tbl_WorkItems' class='table table-bordered' align='center' width='100%'>" +
                              "<thead><tr class='grey'>" + tblHeader.ToString() + "</tr></thead>" +
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
                    HTML.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage1(this," + ddl + ")'></li>");
                }
                HTML.Append("</ul></div>");
            }
            tblHTML = tblHTML + Convert.ToString(HTML);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(tblHTML);
    }

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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getRequestDetails(int wiid,int iid,int pid,string type)
    {
        string str_data = string.Empty;
        if (type == "exp")
        {
            Foreign_Expense_Details fed = new Foreign_Expense_Details();
            str_data = fed.Foreign_Expense_Request_Details(wiid);
        }
        else
        {
            Foreign_Expense_Details fed = new Foreign_Expense_Details();
            str_data = fed.Foreign_Advance_Details(pid,iid);
        }
        return str_data;
    }

    #endregion userdefined


    #region document
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillDocumentDetails(int request_no, int req_type)
    {
        string DisplayData = string.Empty;
        try
        {
            string isData = string.Empty;
            string isValid = string.Empty;


            //DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "pgetrequestdata", ref isValid, data);
            DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Request_Status_Report.aspx", "pgetdocuments", ref isValid, request_no, req_type);
            DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Name</th></tr></thead>";
            if (dsData != null)
            {
                for (int i = 0; i < dsData.Rows.Count; i++)
                {
                    DisplayData += "<tr><td><a href='#' onclick=downloadfiles('" + Convert.ToString(dsData.Rows[i]["object_value"]) + "','" + Convert.ToString(dsData.Rows[i]["filename"]) + "')>" + Convert.ToString(dsData.Rows[i]["filename"]) + "</a></td></tr>";
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
        string ret_data = string.Empty;
        try
        {
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "pgetrequestdata", ref isdata, wiid);
            if (dsData != null)
            {
                ret_data = Convert.ToString(dsData.Tables[0].Rows[0]["pk_travel_expense_hdr_id"]);
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["travel_voucher_id"]);
                ret_data += "||" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]);
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);
                ret_data += "||" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["from_date"]).ToString("dd-MMM-yyyy");
                ret_data += "||" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["to_date"]).ToString("dd-MMM-yyyy");
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["travel_type"]);
                ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["remark"]);

                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "pgettraveluser", ref isdata, Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]));
                if (dtUser.Rows.Count > 0)
                {
                    ret_data += "||" + Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
                    ret_data += "||" + Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
                    ret_data += "||" + Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
                    ret_data += "||" + Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
                    ret_data += "||" + Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
                    ret_data += "||" + Convert.ToString(dtUser.Rows[0]["MOBILE_NO"]);
                    ret_data += "||" + Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
                    ret_data += "||" + Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);

                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "pgettravelrequestapprover", ref isdata, Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]));
                    if (dtApprover != null)
                    {
                        if (dtApprover.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                            {
                                ret_data += "||" + Convert.ToString(dtApprover.Rows[0]["approver"]);
                                ret_data += "||" + Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                            }
                            else
                            {
                                ret_data += "||" + "NA";
                                ret_data += "||" + "NA";
                            }
                            ret_data += "||" + Convert.ToString(dtApprover.Rows[0]["approver_email"]);

                        }
                        else
                        {
                            ret_data += "||" + "NA";
                            ret_data += "||" + "NA";
                            ret_data += "||" + "NA";
                        }
                    }
                    else
                    {
                        ret_data += "||" + "NA";
                        ret_data += "||" + "NA";
                        ret_data += "||" + "NA";
                    }
                    ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["cash_location"]);
                    ret_data += "||" + Convert.ToString(dsData.Tables[0].Rows[0]["payment_mode"]);

                    //fillDocument_Details();
                    ////fillAuditTrail();

                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return ret_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillaudittrail(int processid, int instanceid)
    {
        GetData getData = new GetData();
        string str_data = getData.fillAuditTrail(processid.ToString(), instanceid.ToString());
        return str_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillPayment_Mode()
    {
       // Expense_Data ex_data = new Expense_Data();
      //  string disp_data = ex_data.fillPayment_Mode();
        return "";
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillLocation()
    {
        Expense_Data ex_data = new Expense_Data();
        string disp_data = ex_data.fillLocation();
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
       // Expense_Data ex_data = new Expense_Data();
      //  string jdata = ex_data.get_Journey_Data(jFDate, jTDate, wiid);
        return "";
    }


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_Final_Data(string jFDate, string jTDate, int jr, string jt_data, string concat_data, string jt_val, string jt_amt, string to_city, string desg, int wiid, string travel_class_id, string plant_id)
    {
      //  Expense_Data ex_data = new Expense_Data();
       // string jdata = ex_data.get_Final_Data(jFDate, jTDate, jr, jt_data, concat_data, jt_val, jt_amt, to_city, desg, wiid, travel_class_id, plant_id);
        return "";
    }


    #endregion request
}