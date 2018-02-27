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

public partial class Corporate_Credit_Card_Expense : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Corporate_Credit_Card_Expense));
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
                    bindReim();
                }
                ddlUser.Value = txt_Username.Text;
                
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void bindReim()
    {
        try
        {
            string isdata = "";
            DataTable rt = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "getreimbursements", ref isdata, "Domestic Travel Expenses");
            ddl_Reim_Type.Items.Clear();
            if (rt != null && rt.Rows.Count>0)
            {
                ddl_Reim_Type.DataSource = rt;
                ddl_Reim_Type.DataTextField = "REIMBURSEMENT_TYPE";
                ddl_Reim_Type.DataValueField = "PK_REIMBURSEMNT_ID";
                ddl_Reim_Type.DataBind();
            }
            ddl_Reim_Type.Items.Insert(0,"---Select One---");
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
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Request_Status_Report.aspx", "getexpensebycorporatecard", ref isvalid, txt_Username.Text, txt_Search.Text, ddlStatus.SelectedValue, txt_f_date.Text, txt_t_date.Text, ddl_Reim_Type.SelectedValue);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("Sr.No.", typeof(int));
                dtNew.Columns.Add("Request No.", typeof(string));
                dtNew.Columns.Add("Date", typeof(string));
                dtNew.Columns.Add("Amount", typeof(string));
                dtNew.Columns.Add("Request Status", typeof(string));
                dtNew.Columns.Add("To Whom (Approver)", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtNew.Rows.Add((i + 1), Convert.ToString(dt.Rows[i]["request_no"]), Convert.ToString(dt.Rows[i]["created_date"]), Convert.ToString(dt.Rows[i]["Amount"]), Convert.ToString(dt.Rows[i]["status"]), Convert.ToString(dt.Rows[i]["Approver"]));
                }

                DataGrid dg = new DataGrid();
                dg.DataSource = dtNew;
                dg.DataBind();

                string sFileName = "Report List - " + split_data.Text + " - " + txt_Username.Text + ".xls";
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


    #region ajaxcall


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string showall(string search_data, int pageno, int rpp, int desg)
    {
        Expense_Data getData = new Expense_Data();
        string str_data = getData.get_Travel_Policy_Data(search_data, pageno, rpp, desg);
        return str_data;
    }
    #endregion ajaxcall

    #region userdefined

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string get_BulkRequests(string name, int pageno, int rpp, string status, string f_date, string t_date, int reim_type)
    {
        StringBuilder tblHeader = new StringBuilder();
        StringBuilder tblBody = new StringBuilder();
        string tblHTML = string.Empty;
        DataTable DT = new DataTable();
        string username = Convert.ToString(Session["USER_ADID"]);
        string isData = string.Empty;
        DateTime fd = Convert.ToDateTime(f_date);
        DateTime td = Convert.ToDateTime(t_date);
        try
        {
            DT = (DataTable)ActionController.ExecuteAction("", "Request_Status_Report.aspx", "getexpensebycorporatecard", ref isData, username, name, status, f_date, t_date, reim_type);

            tblHeader.Append("<th>#</th><th>Request No.</th><th>Date</th><th>Amount</th><th>Request Status</th><th>To Whom (Approver)</th><th>Documents</th>");
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
                    //tblBody.Append("<tr><td align='center'><input type='text' id='travelFromDate" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["from_date"]) + " style='display:none'/><input type='text' id='travelToDate" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["to_date"]) + " style='display:none'/><input type='text' id='fk_process" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["FK_PROCESS_ID"]) + " style='display:none'></input><input type='text' id='PK_Dispatch_Note_ID" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["pk_travel_expense_Hdr_Id"]) + " style='display:none'></input><input type='text' id='iid" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["FK_INSTANCE_ID"]) + " style='display:none'></input><input type='text' id='wiid" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + " style='display:none'></input>" + (i + 1) + "</td><td><a href='#popup_details' role='button' data-toggle='modal' onclick='getData(" + (i + 1) + ")' title='Click To Open WorkItem'>" + Convert.ToString(DT.Rows[i]["travel_voucher_id"]) + "</a></td><td align='center'>" + Convert.ToString(DT.Rows[i]["from_date"]) + "</td><td align='left'  >" + DT.Rows[i]["to_date"].ToString() + "</td> <td align='left'>" + DT.Rows[i]["PAYMENT_MODE"].ToString() + "</td><td align='right'>" + DT.Rows[i]["Amount"].ToString() + "</td><td align='left'>" + DT.Rows[i]["Current_Status"].ToString() + "</td><td align='center'><a href='#Documents' role='button' data-toggle='modal' onclick='Bind_Documents(" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + ")'>View</a></td></tr>");
                    tblBody.Append("<tr><td align='center'><input type='text' id='fk_process" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["FK_PROCESS_ID"]) + " style='display:none'></input><input type='text' id='iid" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["FK_INSTANCE_ID"]) + " style='display:none'></input><input type='text' id='wiid" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + " style='display:none'></input>" + (i + 1) + "</td><td>" + Convert.ToString(DT.Rows[i]["request_no"]) + "</td><td align='center'>" + Convert.ToString(DT.Rows[i]["created_date"]) + "</td><td align='right'>" + DT.Rows[i]["Amount"].ToString() + "</td><td align='center'>" + DT.Rows[i]["status"].ToString() + "</td><td>" + DT.Rows[i]["Approver"].ToString() + "</td><td align='center'><a href='#Documents' role='button' data-toggle='modal' onclick='Bind_Documents(" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + "," + Convert.ToString(DT.Rows[i]["fk_process_id"]) + ")'>View</a></td></tr>");
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


            DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Request_Status_Report.aspx", "pgetdocuments", ref isValid, request_no, req_type);
            DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Name</th></tr></thead>";
            if (dsData != null)
            {
                for (int i = 0; i < dsData.Rows.Count; i++)
                {
                    DisplayData += "<tr><td><a href='#' onclick=downloadfiles('" + Convert.ToString(dsData.Rows[i]["filename"]) + "')>" + Convert.ToString(dsData.Rows[i]["filename"]) + "</a></td></tr>";
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

}