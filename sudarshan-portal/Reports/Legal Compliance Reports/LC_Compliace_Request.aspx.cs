using AjaxPro;
using FSL.Controller;
using FSL.Logging;
using InfoSoftGlobal;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class LC_Compliace_Request : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            if (!IsPostBack)
            {
                Initialization();
                ShowChart();
                FillCompliance_Details();
            }
        }
    }

    #region Pageload
    protected void ShowChart()
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
                Fusionchart.Text = getChart(null);
        }
    }
    private void Initialization()
    {
        txt_Created_by.Text = (string)Session["User_ADID"];
    }

    private void FillCompliance_Details()
    {
        string isValid = string.Empty;
        StringBuilder Doc_Detail = new StringBuilder();
        try
        {
            ListItem Li = new ListItem("--Select One--", "");
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "LC_Compliance_Task.aspx", "psearch", ref isdata, 0, 0, 0, "", "", "", "", 0, "", "", "", "11", "0");
            if (dt != null)
            {
                ddl_Comp_Cat.DataSource = dt;
                ddl_Comp_Cat.DataTextField = "CATEGORY_NAME";
                ddl_Comp_Cat.DataValueField = "PK_CMP_CAT_ID";
                ddl_Comp_Cat.DataBind();
                ddl_Comp_Cat.Items.Insert(0, Li);
            }

        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    #endregion

    #region Ajax
    [System.Web.Services.WebMethod]
    public static string[] getcompliance(string FK_CompType_ID)
    {
        string[] ResultData = null;
        string DisplayData = string.Empty;
        string isValid = string.Empty;

        ResultData = new string[5];
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "LC_Compliance_Task.aspx", "psearch", ref isValid, 0, 0, 0, "", "", "", "", "0", "", "", "", "0", Convert.ToInt32(FK_CompType_ID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DisplayData = dt.Rows[i][0].ToString() + "||" + dt.Rows[i][1].ToString() + "," + DisplayData;
                }

            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        ResultData[0] = DisplayData;
        return ResultData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string checkdate(string fdate, string tdate)
    {
        string Displaydata = string.Empty;
        try
        {
            if (fdate != "")
            {
                if (tdate != "")
                {
                    DateTime dt1 = Convert.ToDateTime(fdate);
                    DateTime dt2 = Convert.ToDateTime(tdate);


                    if (dt1 <= dt2)
                    {
                        Displaydata = "true";
                    }
                    else
                    {
                        Displaydata = "Validation Error:From Date should not be greater than by To Date.";
                    }
                }
                else
                {
                    Displaydata = "true";
                }
            }
            else
            {
                Displaydata = "Validation Error:Please select From Date.";
            }
        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
        return Displaydata;
    }
    #endregion

    #region Events
    protected void GetReport(object sender, EventArgs e)
    {
        StringBuilder Doc_Detail = new StringBuilder();
        string IsData = string.Empty;
        try
        {
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "LC_Compliace_Request.aspx", "getdetails", ref IsData, ddl_Comp_Cat.SelectedValue, txt_Comp_ID.Text, txt_From_Date.Text, txt_To_Date.Text, ddl_Staus.SelectedValue, txt_Created_by.Text);
            DataTable data = new DataTable();
            DataTable datacount = new DataTable();
            data = dt.Tables[0];
            datacount = dt.Tables[1];
            Session["ResultData"] = data;
            if (data.Rows.Count > 0)
            {
                    Doc_Detail.Append("<table id='CMP_Docs'  align='center' class='table table-bordered table-hover'>");
                    Doc_Detail.Append("<thead>");
                    Doc_Detail.Append("<tr class='grey'>");
                    Doc_Detail.Append("<th>Sr.NO</th><th>Requested By</th><th>Requested Date</th><th>Category Name</th><th>Compliance Name</th><th>Grace Date</th><th>Last Date of Submission</th><th>Process Status</th><th>Status</th></tr></thead><tbody>");
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        Doc_Detail.Append(" <tr>");
                        Doc_Detail.Append("<td>" + (i + 1) + "</td>");
                        Doc_Detail.Append("<td>" + data.Rows[i]["REQUEST_BY"].ToString() + "</td>");
                        Doc_Detail.Append("<td>" + data.Rows[i]["REQUEST_DATE"].ToString() + "</td>");
                        Doc_Detail.Append("<td>" + data.Rows[i]["CATEGORY_NAME"].ToString() + "</td>");
                        Doc_Detail.Append("<td>" + data.Rows[i]["CMPL_TASK_NAME"].ToString() + "</td>");
                        Doc_Detail.Append("<td>" + data.Rows[i]["GRACE_DATE"].ToString() + "</td>");
                        Doc_Detail.Append("<td>" + data.Rows[i]["LAST_DT_OF_SUBMIT"].ToString() + "</td>");
           Doc_Detail.Append("<td>" + data.Rows[i]["PROCESS_STATUS"].ToString() + "</td>");             
           Doc_Detail.Append("<td>" + data.Rows[i]["REQUEST_STATUS"].ToString() + "</td>");
             
                        Doc_Detail.Append(" </tr>");
                    }
                    Doc_Detail.Append(" </tbody>");
                    Doc_Detail.Append(" </table>");
                    div_Details.InnerHtml = Doc_Detail.ToString();
                    Fusionchart.Text = getChart(datacount);
            }
            else
            {
                Page.RegisterStartupScript("", "<script language='javascript'>{alert('No Data Found !!')}</script>");
		div_Details.InnerHtml="";               
		Fusionchart.Text = getChart(datacount);
                data = null;
            }

        }

        catch (Exception Ex)
        {
            Logger.WriteEventLog(false, Ex);
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Fusionchart.Text = getChart(null);
            ddl_Comp_Cat.SelectedIndex = 0;
            ddl_Compliance_Name.SelectedIndex = 0;
            ddl_Staus.SelectedIndex = 0;
            txt_From_Date.Text = "";
            txt_To_Date.Text = "";
            txt_Comp_ID.Text="";
            div_Details.InnerHtml = "";
        }
    }
    
    #endregion

    public string getChart(DataTable result)
    {
        string chartData = null;
        string isValid = string.Empty;
        string IsData = string.Empty;
        DateTime date;
        string monthname = string.Empty;
        int flg = 0;
        int couriercount;
        try
        {
            string selet_Item_Val = string.Empty;

            string[] months = new string[12] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

            chartData = "<graph caption='Legal Compliance Details' xAxisName='Months' yAxisName='Compliance Count' decimalPrecision='0' formatNumberScale='0' numdivlines='5' showhovercap='1' lineThickness='1' animation='1' numVDivLines='12'>";
            chartData += "<categories>";
            for (int i = 1; i <= months.Length; i++)
            {
                chartData += "<category name='" + months[i - 1] + "' />";
            }
            chartData += "</categories>";
            chartData += "<dataset seriesName='No. Of Compliance Details' color='AFD8F8'  lineThickness='2'>";
            for (int j = 0; j < months.Length; j++)
            {
                int cnt = 0;
                if (result != null && result.Rows.Count > 0)
                {
                    for (int k = 0; k < result.Rows.Count; k++)
                    {
                        monthname = result.Rows[k]["MONTH"].ToString();
                        monthname = monthname.Substring(0, 3);
                        if (months[j] == monthname.ToUpper())
                        {
                            couriercount = Convert.ToInt32(result.Rows[k][0]);
                            chartData += "<set value='" + couriercount + "'/>";
                            flg = 1;
                            cnt++;
                            break;
                        }

                    }
                }
                if (flg == 0 || cnt == 0)
                {
                    chartData += "<set value='0'/>";
                    flg = 1;
                    continue;
                }
                else
                {
                    continue;
                }

            }
            chartData += "</dataset>";

            chartData += "</graph>";
            txt_amd_by.Text = chartData;



        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }

        return FusionCharts.RenderChartHTML("../../FusionCharts/MSColumn3D.swf", "", txt_amd_by.Text, "chart", "410", "270", false);

    }
}