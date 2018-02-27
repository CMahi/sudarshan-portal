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

public partial class Advance_Report : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Report));
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
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Request_Status_Report.aspx", "getadvancereport", ref isvalid, txt_Username.Text, txt_Search.Text, "", txt_f_date.Text, txt_t_date.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("Sr.No.", typeof(int));
                dtNew.Columns.Add("Request No.", typeof(string));
                dtNew.Columns.Add("Date", typeof(string));
                dtNew.Columns.Add("Payment Mode", typeof(string));
                dtNew.Columns.Add("Amount", typeof(string));
                dtNew.Columns.Add("Request Status", typeof(string));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtNew.Rows.Add((i + 1), Convert.ToString(dt.Rows[i]["request_no"]), Convert.ToString(dt.Rows[i]["created_date"]), Convert.ToString(dt.Rows[i]["PAYMENT_MODE"]), Convert.ToString(dt.Rows[i]["Amount"]), Convert.ToString(dt.Rows[i]["status"]));
                }

                DataGrid dg = new DataGrid();
                dg.DataSource = dtNew;
                dg.DataBind();

                string sFileName = "Report List - Requests Status Report - " + txt_Username.Text + ".xls";
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
            DT = (DataTable)ActionController.ExecuteAction("", "Request_Status_Report.aspx", "getadvancereport", ref isData, name, status, fdate, tdate);

            tblHeader.Append("<th>#</th><th>Emp Id</th><th>Employee Name</th><th>Expense Head</th><th>GL Code</th><th>Cost Center</th><th>Request No</th><th>Amount</th><th>Advance Type</th><th>Approved Date</th><th>Used By</th>");
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
                    tblBody.Append("<tr><td align='center'><input type='text' id='pk_adv_id_" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_ADVANCE_HDR_Id"]) + " style='display:none'></input>" + (i + 1) + "</td><td>" + Convert.ToString(DT.Rows[i]["EMP_ID"]) + "</td><td align='center'>" + Convert.ToString(DT.Rows[i]["EMPLOYEE_NAME"]) + "</td><td align='center'>" + DT.Rows[i]["EXPENSE_HEAD"].ToString() + "</td><td align='right'>" + DT.Rows[i]["SAP_GLCode"].ToString() + "</td><td align='center'>" + DT.Rows[i]["kostt"].ToString() + "</td><td align='center'>" + DT.Rows[i]["REQUEST_NO"].ToString() + "</td><td align='center'>" + DT.Rows[i]["AMOUNT"].ToString() + "</td><td align='center'>" + DT.Rows[i]["ADVANCE_TYPE_NAME"].ToString() + "</td><td align='center'>" + DT.Rows[i]["Approval_date"].ToString() + "</td><td align='center'>" + DT.Rows[i]["Used_By"].ToString() + "</td></tr>");
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


 
    #region request


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
        Expense_Data ex_data = new Expense_Data();
        string disp_data = ex_data.fillPayment_Mode();
        return disp_data;
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
    public string get_Hotel_Booking(string jFDate, int cnt, string concat_data, string cls_val, int wiid)
    {
        string jdata = ex_data.get_Hotel_Booking(jFDate, cnt, concat_data, cls_val, wiid);
        return jdata;
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
        Expense_Data ex_data = new Expense_Data();
        string jdata = ex_data.get_Journey_Data(jFDate, jTDate, wiid);
        return jdata;
    }


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string get_Final_Data(string jFDate, string jTDate, int jr, string jt_data, string concat_data, string jt_val, string jt_amt, string to_city, int desg, int wiid, string travel_class_id, string plant_id)
    {
        Expense_Data ex_data = new Expense_Data();
        string jdata = ex_data.get_Final_Data(jFDate, jTDate, jr, jt_data, concat_data, jt_val, jt_amt, to_city, desg, wiid, travel_class_id, plant_id);
        return jdata;
    }


    #endregion request
}