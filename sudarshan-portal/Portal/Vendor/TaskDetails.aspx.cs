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

public partial class TaskDetails : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(TaskDetails));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username1.Text=txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        generateQueue();
                    }
                    lnkText.Text = "1";
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
            Response.Redirect("../HomePage.aspx");

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected void generateQueue()
    {
        StringBuilder tblHeader = new StringBuilder();
        string isData = string.Empty;
        StringBuilder tblBody = new StringBuilder();
        string tblHTML = string.Empty;
        string imgURL = string.Empty;
        string queryImgUrl = string.Empty;
        string queryAlt = string.Empty;
        string queryOnClick = string.Empty;
        string escalation = string.Empty;
        int processid, instanceid;

        try
        {
            DataTable DT = new DataTable();
            string username = Convert.ToString(Session["USER_ADID"]);
            if (txt_Search.Text.Trim() == "")
            {
                DT = (DataTable)ActionController.ExecuteAction("", "TaskDetails.aspx", "select user wi", ref isData, username);
            }
            else
            {
                DT = (DataTable)ActionController.ExecuteAction("", "TaskDetails.aspx", "select user wi search", ref isData, username, txt_Search.Text.Trim());
            }
            if (lnkText.Text == "")
            {
                lnkText.Text = "1";
            }
            ddlText1.Text = ddlText.Text = ddlRecords.SelectedItem.Text;
            #region comment
            tblHeader.Append("<th>SR.NO.</th><th>INITIATOR</th><th >HEADER INFO</th> <th > PROCESS NAME</th><th >STEP NAME</th><th  >ASSIGN DATE</th>");


            // for (int i = 0; i < DT.Rows.Count; i++)
            int ddl = Convert.ToInt32(ddlRecords.SelectedItem.Text);
             int pageno = Convert.ToInt16(lnkText.Text);
             ddlText.Text = ddlRecords.SelectedItem.Text;
             int from=(pageno-1)*ddl;
             int to = ((pageno - 1) * ddl)+ ddl;
             for (int i = from; i < to; i++)
            {
                if (i < DT.Rows.Count)
                {
                    processid = Int32.Parse(DT.Rows[i]["PK_PROCESSID"].ToString());
                    instanceid = Int32.Parse(DT.Rows[i]["INSTANCE_ID"].ToString());
                    string header = DT.Rows[i]["HEADER_INFO"].ToString();
                    //Following block of code is added by Amol for providing facility for Reassign Task

                    //tblBody.Append("<tr><td align='center' >" + Convert.ToString(i + 1) + "</td><td align='left' >" + DT.Rows[i]["INSTANCE_ID_DESCRIPTION"].ToString() + "</td><td><a onclick='openWorkItem(" + DT.Rows[i]["INSTANCE_ID"].ToString() + "," + DT.Rows[i]["PK_TRANSID"].ToString() + ");' href='../MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='frmset_WorkArea' title='Click here to open the workitem.' runat='server'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='center'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td></tr>");
                    tblBody.Append("<tr><td align='center' >" + Convert.ToString(i + 1) + "</td><td align='left' >" + DT.Rows[i]["INSTANCE_ID_DESCRIPTION"].ToString() + "</td><td><a href='../MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='frmset_WorkArea' title='Click here to open the workitem.' runat='server'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='center'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td></tr>");
                }
            }
            tblHTML = "<table id='tbl_WorkItems' class='table table-bordered' align='center' width='100%'>" +
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
                    HTML.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + ddlText.Text + ")'></li>");
                }
                HTML.Append("</ul></div>");
            }
            tblHTML = tblHTML + Convert.ToString(HTML);
            div_InvoiceDetails.InnerHtml = tblHTML;
            #endregion
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

  
    protected void ddlRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlText1.Text = ddlText.Text = ddlRecords.SelectedItem.Text;
        generateQueue();
    }
   
[AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillSearch(string name, int rpp)
    {
        string username = Convert.ToString(Session["USER_ADID"]);
        StringBuilder tblHeader = new StringBuilder();
        StringBuilder tblBody = new StringBuilder();
        string tblHTML = string.Empty;
        DataTable DT = new DataTable();
        string isData = string.Empty;
        int processid, instanceid;
        try
        {
            if (name.Trim() == "")
            {
                DT = (DataTable)ActionController.ExecuteAction("", "TaskDetails.aspx", "select user wi", ref isData, username);
            }
            else
            {
                DT = (DataTable)ActionController.ExecuteAction("", "TaskDetails.aspx", "select user wi search", ref isData, username, name);
            }
            tblHeader.Append("<th>Sr.No.</th><th>INITIATOR</th><th >HEADER INFO</th> <th > PROCESS NAME</th><th >STEP NAME</th><th  >ASSIGN DATE</th>");
            //for (int i = 0; i < DT.Rows.Count; i++)
              int ddl = rpp;
              int pageno = 1;
             int from=(pageno-1)*ddl;
             int to = ((pageno - 1) * ddl)+ ddl;
             for (int i = from; i < to; i++)
            {
                if (i < DT.Rows.Count)
                {

                    processid = Int32.Parse(DT.Rows[i]["PK_PROCESSID"].ToString());
                    instanceid = Int32.Parse(DT.Rows[i]["INSTANCE_ID"].ToString());
                    string header = DT.Rows[i]["HEADER_INFO"].ToString();
                    //Following block of code is added by Amol for providing facility for Reassign Task

                    tblBody.Append("<tr><td align='center' >" + Convert.ToString(i + 1) + "</td><td align='left' >" + DT.Rows[i]["INSTANCE_ID_DESCRIPTION"].ToString() + "</td><td><a href='../MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='frmset_WorkArea' title='Click here to open the workitem.' runat='server'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='center'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td></tr>");
                }
            }
            tblHTML = "<table id='tbl_WorkItems' class='table table-bordered' align='center' width='100%'>" +
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

[AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
public string fillGoToPage1(string name, int pageno, int rpp)
{
    string username = Convert.ToString(Session["USER_ADID"]);
    StringBuilder tblHeader = new StringBuilder();
    StringBuilder tblBody = new StringBuilder();
    string tblHTML = string.Empty;
    DataTable DT = new DataTable();
    string isData = string.Empty;
    int processid, instanceid;
    try
    {
        if (name.Trim() == "")
        {
            DT = (DataTable)ActionController.ExecuteAction("", "TaskDetails.aspx", "select user wi", ref isData, username);
        }
        else
        {
            DT = (DataTable)ActionController.ExecuteAction("", "TaskDetails.aspx", "select user wi search", ref isData, username, name);
        }
        tblHeader.Append("<th>Sr.No.</th><th>INITIATOR</th><th >HEADER INFO</th> <th > PROCESS NAME</th><th >STEP NAME</th><th  >ASSIGN DATE</th>");
        //for (int i = 0; i < DT.Rows.Count; i++)
        int ddl = rpp;
        int from = (pageno - 1) * ddl;
        int to = ((pageno - 1) * ddl) + ddl;
        for (int i = from; i < to; i++)
        {
            if (i < DT.Rows.Count)
            {

                processid = Int32.Parse(DT.Rows[i]["PK_PROCESSID"].ToString());
                instanceid = Int32.Parse(DT.Rows[i]["INSTANCE_ID"].ToString());
                string header = DT.Rows[i]["HEADER_INFO"].ToString();
                //Following block of code is added by Amol for providing facility for Reassign Task

                tblBody.Append("<tr><td align='center' >" + Convert.ToString(i + 1) + "</td><td align='left' >" + DT.Rows[i]["INSTANCE_ID_DESCRIPTION"].ToString() + "</td><td><a href='../MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='frmset_WorkArea' title='Click here to open the workitem.' runat='server'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='center'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td></tr>");
            }
        }
        tblHTML = "<table id='tbl_WorkItems' class='table table-bordered' align='center' width='100%' >" +
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

[AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
public string fillGoToPage(int pageno, int rpp)
{
    string name = string.Empty;
    string username = Convert.ToString(Session["USER_ADID"]);
    StringBuilder tblHeader = new StringBuilder();
    StringBuilder tblBody = new StringBuilder();
    string tblHTML = string.Empty;
    DataTable DT = new DataTable();
    string isData = string.Empty;
    int processid, instanceid;
    try
    {
        if (name.Trim() == "")
        {
            DT = (DataTable)ActionController.ExecuteAction("", "TaskDetails.aspx", "select user wi", ref isData, username);
        }
        else
        {
            DT = (DataTable)ActionController.ExecuteAction("", "TaskDetails.aspx", "select user wi search", ref isData, username, name);
        }
        tblHeader.Append("<th>Sr.No.</th><th>INITIATOR</th><th >HEADER INFO</th> <th > PROCESS NAME</th><th >STEP NAME</th><th  >ASSIGN DATE</th>");
        //for (int i = 0; i < DT.Rows.Count; i++)
        int ddl = rpp;
        int from = (pageno - 1) * ddl;
        int to = ((pageno - 1) * ddl) + ddl;
        for (int i = from; i < to; i++)
        {
            if (i < DT.Rows.Count)
            {

                processid = Int32.Parse(DT.Rows[i]["PK_PROCESSID"].ToString());
                instanceid = Int32.Parse(DT.Rows[i]["INSTANCE_ID"].ToString());
                string header = DT.Rows[i]["HEADER_INFO"].ToString();
                //Following block of code is added by Amol for providing facility for Reassign Task

                tblBody.Append("<tr><td align='center' >" + Convert.ToString(i + 1) + "</td><td align='left' >" + DT.Rows[i]["INSTANCE_ID_DESCRIPTION"].ToString() + "</td><td><a href='../MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='frmset_WorkArea' title='Click here to open the workitem.' runat='server'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='center'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td></tr>");
            }
        }
        tblHTML = "<table id='tbl_WorkItems' class='table table-bordered' align='center' width='100%'>" +
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

}

