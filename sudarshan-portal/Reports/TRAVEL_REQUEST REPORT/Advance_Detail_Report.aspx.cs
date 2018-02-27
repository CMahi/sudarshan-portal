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

public partial class Advance_Detail_Report : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Detail_Report));
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
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Advance_Detail_Report.aspx", "getadvancereport", ref isvalid,  txt_Search.Text,"", txt_f_date.Value, txt_t_date.Value);
            if (dt != null && dt.Rows.Count > 0)
            {
              
                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("Sr.No.", typeof(int));
                dtNew.Columns.Add("Employee Code", typeof(string));
                dtNew.Columns.Add("Employee Name", typeof(string));
                dtNew.Columns.Add("Cost Center", typeof(string));
                dtNew.Columns.Add("Request No", typeof(string));     
                dtNew.Columns.Add("Advance Type", typeof(string));
                dtNew.Columns.Add("Amount", typeof(string));
                dtNew.Columns.Add("Approved Date", typeof(string));
               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtNew.Rows.Add((i + 1), Convert.ToString(dt.Rows[i]["EMP_ID"]), Convert.ToString(dt.Rows[i]["EMPLOYEE_NAME"]), Convert.ToString(dt.Rows[i]["kostt"]),Convert.ToString(dt.Rows[i]["REQUEST_NO"]), 
                        Convert.ToString(dt.Rows[i]["ADVANCE_TYPE_NAME"]), Convert.ToString(dt.Rows[i]["AMOUNT"]),
                        Convert.ToString(dt.Rows[i]["Approval_date"]));
                }
                DataGrid dg = new DataGrid();
                dg.DataSource = dtNew;
                dg.DataBind();

                string sFileName = "Report List - Advance Report - " + txt_Username.Text + ".xls";
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
            DT = (DataTable)ActionController.ExecuteAction("", "Advance_Detail_Report.aspx", "getadvancereport", ref isData, name, status, fdate, tdate);
            tblHeader.Append("<th>#</th><th>Emp Id</th><th align='center'>Employee Name</th><th>Cost Center</th><th>Request No</th><th>From City</th><th>To City</th><th>Amount</th><th>Advance Type</th><th>From Date</th><th>To Date</th><th>Approved Date</th>");
        
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
                    tblBody.Append("<tr><td align='center'><input type='text' id='pk_adv_id_" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_ADVANCE_HDR_Id"]) + " style='display:none'></input>" + (i + 1) + "</td><td>" + Convert.ToString(DT.Rows[i]["EMP_ID"]) + "</td><td align='center'>" + Convert.ToString(DT.Rows[i]["EMPLOYEE_NAME"]) + "</td><td align='center'>" + DT.Rows[i]["kostt"].ToString() + "</td><td align='center'>" + DT.Rows[i]["REQUEST_NO"].ToString() + "</td>");
                    if((Convert.ToString(DT.Rows[i]["fcity"])==null || Convert.ToString(DT.Rows[i]["fcity"])=="") && (Convert.ToString(DT.Rows[i]["tcity"])==null || Convert.ToString(DT.Rows[i]["tcity"])==""))
                    {
                        tblBody.Append("<td>" + Convert.ToString(DT.Rows[i]["OTHER_F_CITY"]) + "</td><td>" + Convert.ToString(DT.Rows[i]["OTHER_T_CITY"]) + "</td>");
                    }
                    else  if((Convert.ToString(DT.Rows[i]["OTHER_F_CITY"])==null || Convert.ToString(DT.Rows[i]["OTHER_F_CITY"])=="") && (Convert.ToString(DT.Rows[i]["OTHER_T_CITY"])==null || Convert.ToString(DT.Rows[i]["OTHER_T_CITY"])==""))
                    {
                        tblBody.Append("<td>" + Convert.ToString(DT.Rows[i]["fcity"]) + "</td><td>" + Convert.ToString(DT.Rows[i]["tcity"]) + "</td>");
                    }                    
                    tblBody.Append("<td align='right'>" + DT.Rows[i]["AMOUNT"].ToString() + "</td><td align='center'>" + DT.Rows[i]["ADVANCE_TYPE_NAME"].ToString() + "</td>");
                    if ((Convert.ToString(DT.Rows[i]["f_date"]) == "01-Jan-1900" || Convert.ToString(DT.Rows[i]["f_date"]) == "") && (Convert.ToString(DT.Rows[i]["t_date"]) == "01-Jan-1900" || Convert.ToString(DT.Rows[i]["t_date"]) == ""))
                    {
                        tblBody.Append("<td>" + Convert.ToString(DT.Rows[i]["adv_date"]) + "</td><td>"+ "" +"</td>");
                    }
                    else 
                    {
                        tblBody.Append("<td>" + Convert.ToString(DT.Rows[i]["f_date"]) + "</td><td>" + Convert.ToString(DT.Rows[i]["t_date"]) + "</td>");
                    }
                    tblBody.Append("<td align='center'>" + DT.Rows[i]["Approval_date"].ToString() + "</td></tr>");
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
}