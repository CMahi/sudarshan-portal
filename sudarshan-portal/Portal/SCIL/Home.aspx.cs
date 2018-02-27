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
using System.Data.OleDb;
using System.Collections.Generic;

public partial class Portal_SCIL_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string ActionStatus = string.Empty;
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Portal_SCIL_Home));
              
                    if (!IsPostBack)
                    {
                        txt_UserName.Text = Convert.ToString(Session["USER_ADID"]);
                        mid.Text = (string)ActionController.ExecuteAction("", "Home.aspx", "getpanelid", ref ActionStatus, "Masters");
                        rid.Text = (string)ActionController.ExecuteAction("", "Home.aspx", "getpanelid", ref ActionStatus, "Reports");
                        pid.Text = (string)ActionController.ExecuteAction("", "Home.aspx", "getpanelid", ref ActionStatus, "Processes");
                        EmpAttendance();
                    }
                    
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    private void EmpAttendance()
    {
           

            StringBuilder str = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
           
            int cnt = 0;
            
                DataTable table = null;
                String fileName = "InoutData.csv";
               string activeDir = ConfigurationManager.AppSettings["EXCElPATH"].ToString();
               string[] FileExtension = fileName.Split(".".ToCharArray());


               
                List<string> rows = new List<string>();
                string tableName = "InoutData";
                StreamReader reader = new StreamReader(activeDir, System.Text.Encoding.UTF8);
                string record = reader.ReadLine();
                while (record != null)
                {
                    rows.Add(record);
                    record = reader.ReadLine();
                }
                reader.Close();
                List<string[]> rowObjects = new List<string[]>();

                int maxColsCount = 0;
                foreach (string s in rows)
                {
                    string[] convertedRow = s.Split(new char[] { ',' });
                    if (convertedRow.Length > maxColsCount)
                        maxColsCount = convertedRow.Length;
                    rowObjects.Add(convertedRow);
                }

                // Then we build the table
                table = new DataTable(tableName);
                for (int i = 0; i < maxColsCount; i++)
                {
                    
                    table.Columns.Add(new DataColumn());
                }

                foreach (string[] rowArray in rowObjects)
                {
                    table.Rows.Add(rowArray);
                }
                table.AcceptChanges();


                try
                {
                   

                    str1.Append("<table id='tbl_Emp' class='table table-bordered table-hover' style='width:100%'> <thead><tr><th align='left'>Date</th><th align='left'>Intime</th><th align='left'>Outtime</th><th align='left'>Working(HRS)</th><th align='left'>Expected Outtime</th></tr> </thead>");
                    str1.Append("<tbody>");
                    
                     string strDate;
                     for (int b = 0; b < table.Rows.Count; b++)
                    {
                        if (txt_UserName.Text == table.Rows[b][2].ToString())//&& cnt < 30
                        {
                            
                            lbl_Emp_No.InnerText = table.Rows[b][0].ToString();
                            lbl_Emp_Name.InnerText = table.Rows[b][1].ToString();
                            if (table.Rows[b][3].ToString() != "")
                                strDate = table.Rows[b][3].ToString();
                            else
                                strDate = "N.A";

                            cnt++;
                            str1.Append(" <tr>");
                            str1.Append("<td>" + strDate + "</td>");
                            str1.Append("<td>" + DateTime.Parse(table.Rows[b][4].ToString()).ToString("HH:mm:ss") + "</td>");
                            str1.Append("<td>" + DateTime.Parse(table.Rows[b][5].ToString()).ToString("HH:mm:ss") + "</td>");
                            if (table.Rows[b][6].ToString() != "")
                                str1.Append("<td>" + DateTime.Parse(table.Rows[b][6].ToString()).ToString("HH:mm:ss") + "</td>");
                            else
                                str1.Append("<td>N.A</td>");
                            if (table.Rows[b][7].ToString() != "N.A.")
                                str1.Append("<td>" + DateTime.Parse(table.Rows[b][7].ToString()).ToString("HH:mm:ss") + "</td>");
                            else
                             str1.Append("<td>N.A</td>");
                             str1.Append("</tr>");
                        }
                    }
                    str1.Append("   </tbody>   </table> ");
//ScriptManager.RegisterStartupScript(this, GetType(),"","$('#tbl_Emp').dataTable({'bSort':false});",true);
                    dv_Emp.InnerHtml = str1.ToString();

                    
                }
                catch (Exception ex)
                {
                   
                    FSL.Logging.Logger.WriteEventLog(false, ex);
                }
       
    }

    protected void goToReport(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=" + rid.Text);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void goToProcess(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=" + pid.Text);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void goToMaster(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=" + mid.Text);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string GetData(string frmdate,string todate,string user)
    {
        string DisplatData = string.Empty;
        string isdata = string.Empty;
       
           StringBuilder str = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
           
            int cnt = 0;
            
                DataTable table = null;
                String fileName = "InoutData.csv";
               string activeDir = ConfigurationManager.AppSettings["EXCElPATH"].ToString();
               string[] FileExtension = fileName.Split(".".ToCharArray());


               
                List<string> rows = new List<string>();
                string tableName = "InoutData";
                StreamReader reader = new StreamReader(activeDir, System.Text.Encoding.UTF8);
                string record = reader.ReadLine();
                while (record != null)
                {
                    rows.Add(record);
                    record = reader.ReadLine();
                }
                reader.Close();
                List<string[]> rowObjects = new List<string[]>();

                int maxColsCount = 0;
                foreach (string s in rows)
                {
                    string[] convertedRow = s.Split(new char[] { ',' });
                    if (convertedRow.Length > maxColsCount)
                        maxColsCount = convertedRow.Length;
                    rowObjects.Add(convertedRow);
                }

                // Then we build the table
                table = new DataTable(tableName);
                for (int i = 0; i < maxColsCount; i++)
                {
                    
                    table.Columns.Add(new DataColumn());
                }

                foreach (string[] rowArray in rowObjects)
                {
                    table.Rows.Add(rowArray);
                }
                table.AcceptChanges();


                try
                {
                   

                    str1.Append("<table id='tbl_Emp' class='table table-bordered table-hover' style='width:100%'> <thead><tr><th align='left'>Date</th><th align='left'>Intime</th><th align='left'>Outtime</th><th align='left'>Working(HRS)</th><th align='left'>Expected Outtime</th></tr> </thead>");
                    str1.Append("<tbody>");
                    
                     string strDate;
                     for (int b = 0; b < table.Rows.Count; b++)
                    {
                        if (frmdate != "" && todate != "")
                        {
                            DateTime frmd = Convert.ToDateTime(frmdate);
                            DateTime tod = Convert.ToDateTime(todate);
                            DateTime date=new DateTime();
                            if (table.Rows[b][3].ToString() != "" && table.Rows[b][3].ToString() != "DATE")
                            {
                                 date = Convert.ToDateTime(table.Rows[b][3].ToString());
                            }
                            if (user == table.Rows[b][2].ToString() && frmd <= date)
                            {
                                if (tod >= date)
                                {
                               

                                     if (table.Rows[b][3].ToString() != "")
                                        strDate = table.Rows[b][3].ToString();
                                    else
                                        strDate = "N.A";

                                    cnt++;
                                    str1.Append(" <tr>");
                                    str1.Append("<td>" + strDate + "</td>");
                                    str1.Append("<td>" + DateTime.Parse(table.Rows[b][4].ToString()).ToString("HH:mm:ss") + "</td>");
                                    str1.Append("<td>" + DateTime.Parse(table.Rows[b][5].ToString()).ToString("HH:mm:ss") + "</td>");
                                    if (table.Rows[b][6].ToString() != "")
                                        str1.Append("<td>" + DateTime.Parse(table.Rows[b][6].ToString()).ToString("HH:mm:ss") + "</td>");
                                    else
                                        str1.Append("<td>N.A</td>");
                                    if (table.Rows[b][7].ToString() != "N.A.")
                                        str1.Append("<td>" + DateTime.Parse(table.Rows[b][7].ToString()).ToString("HH:mm:ss") + "</td>");
                                    else
                                     str1.Append("<td>N.A</td>");
                                     str1.Append("</tr>");

                                }
                            }
                        }
                        else if (user == table.Rows[b][2].ToString() && cnt < 30)
                        {
                           
                            if (table.Rows[b][3].ToString() != "")
                                strDate = table.Rows[b][3].ToString();
                            else
                                strDate = "N.A";

                            cnt++;
                            str1.Append(" <tr>");
                            str1.Append("<td>" + strDate + "</td>");
                            str1.Append("<td>" + DateTime.Parse(table.Rows[b][4].ToString()).ToString("HH:mm:ss") + "</td>");
                            str1.Append("<td>" + DateTime.Parse(table.Rows[b][5].ToString()).ToString("HH:mm:ss") + "</td>");
                            if (table.Rows[b][6].ToString() != "")
                                str1.Append("<td>" + DateTime.Parse(table.Rows[b][6].ToString()).ToString("HH:mm:ss") + "</td>");
                            else
                                str1.Append("<td>N.A</td>");
                            if (table.Rows[b][7].ToString() != "N.A.")
                                str1.Append("<td>" + DateTime.Parse(table.Rows[b][7].ToString()).ToString("HH:mm:ss") + "</td>");
                            else
                             str1.Append("<td>N.A</td>");
                             str1.Append("</tr>");
                        }
                    }
                    str1.Append("   </tbody>   </table> ");
                    DisplatData = str1.ToString();
                }
                catch (Exception ex)
                {
                    FSL.Logging.Logger.WriteEventLog(false, ex);
                }
            

        return DisplatData;
    }
}