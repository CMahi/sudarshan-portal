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

public partial class Reporting_Manager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Reporting_Manager));
            try
            {
                if (!Page.IsPostBack)
                {
     
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                    ed_emp_code.Attributes.Add("readonly", "true");
                    ed_rep_name.Attributes.Add("readonly", "true");
                    rep_emp_name.Attributes.Add("readonly", "true");
                    fillDesignation();
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void fillDesignation()
    {
        string isdata = string.Empty;
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("","Reporting_Manager.aspx","pgetdesignation",ref isdata);
            if (dt != null && dt.Rows.Count > 0)
            {
                emp_designation.Items.Clear();
                emp_designation.DataSource = dt;
                emp_designation.DataTextField = "DESG_NAME";
                emp_designation.DataValueField= "DESG_NAME";
                emp_designation.DataBind();
                emp_designation.Items.Insert(0,"--Select One--");

                ed_emp_designation.Items.Clear();
                ed_emp_designation.DataSource = dt;
                ed_emp_designation.DataTextField = "DESG_NAME";
                ed_emp_designation.DataValueField = "DESG_NAME";
                ed_emp_designation.DataBind();
                ed_emp_designation.Items.Insert(0, "--Select One--");
            }

            DataTable dtLevel = (DataTable)ActionController.ExecuteAction("", "Reporting_Manager.aspx", "pgetdata", ref isdata, 3, "", "", "", "", "");
            if (dtLevel != null && dtLevel.Rows.Count > 0)
            {
                emp_level.Items.Clear();
                emp_level.DataSource = dtLevel;
                emp_level.DataTextField = "EMP_LEVEL";
                emp_level.DataValueField = "EMP_LEVEL";
                emp_level.DataBind();
                emp_level.Items.Insert(0, "--Select One--");

                ed_emp_level.Items.Clear();
                ed_emp_level.DataSource = dtLevel;
                ed_emp_level.DataTextField = "EMP_LEVEL";
                ed_emp_level.DataValueField = "EMP_LEVEL";
                ed_emp_level.DataBind();
                ed_emp_level.Items.Insert(0, "--Select One--");
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void btn_AddNew_Click(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                divIns.Style.Add("display","none");
                string isdata = string.Empty;
                string isSaved = (string)ActionController.ExecuteAction("", "Reporting_Manager.aspx", "pinsdata", ref isdata, 2, emp_code.Text.Trim(), emp_name.Text.Trim(), emp_level.SelectedItem.Text.Trim(), emp_designation.SelectedItem.Text.Trim(), rep_emp_code.Text.Trim());
                //string isSaved = "true";
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Employee Added Successfully...!'); clearControls();}</script>");
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + isSaved + "');}</script>");
                    
                }


            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string bindCountryData()
    {
        StringBuilder html = new StringBuilder();
        try
        {
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Reporting_Manager.aspx", "pgetdata", ref isdata, 0, "", "", "", "", "");
            html.Append("<table id='data-table1' class='table table-bordered'>");
            html.Append("<thead>");
            html.Append("<tr class='grey'>");
            html.Append("<th>#</th><th>Emp Code</th><th>Employee Name</th><th>Designation</th><th>Level</th><th>Reporting Emp Code</th><th>Reporting Manager Name</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
            if (dt != null)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html.Append("<tr><td><input type='text' id='pk_id" + (i + 1) + "' value=" + Convert.ToString(dt.Rows[i]["PERSONAL_NUM_PERNR"]) + " style='display:none'>" + (i + 1) + "</td><td>" + Convert.ToString(dt.Rows[i]["PERSONAL_NUM_PERNR"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["EMP_NAME_ENAME"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["EMP_SUBGROUP_NAME_STEXT"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["EMP_GROUP_NAME_PTEXT"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["REPORTING_EMP_NUM_REPNR"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["REPORTING_EMP_NAME_REPNM"]) + "</td><td><a href='#UpdRepPanel' role='button' data-toggle='modal' Title='Update' onclick=getReportingData('" + Convert.ToString(dt.Rows[i]["PERSONAL_NUM_PERNR"]) + "')><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a></td><td><a href='#DeletePanel' role='button' data-toggle='modal' Title='Delete' onclick=deleteReporting('" + Convert.ToString(dt.Rows[i]["PERSONAL_NUM_PERNR"]) + "')><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td></tr>");
                    
                }
            }
            html.Append("</tbody></table>");
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        //ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({'bSort': false,'pageLength':100});", true);
        return Convert.ToString(html);

    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string updateReportingData(string emp_code, string emp_name, string level, string designation, string rep_code,int param)
    {
        string isUpdated = string.Empty;
        string isdata = string.Empty;
        try
        {
            isUpdated = (string)ActionController.ExecuteAction("", "Reporting_Manager.aspx", "pinsdata", ref isdata, param, emp_code, emp_name, level, designation, rep_code);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return isUpdated;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string[] getReporting(string emp_code,int param)
    {
        string isdata = "";
        string[] ret_data = new string[7];
        ret_data[0] = "0";
        ret_data[1] = ret_data[2] = ret_data[3] = ret_data[4] = ret_data[5] = ret_data[6] = "";
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Reporting_Manager.aspx", "pgetdata", ref isdata, param, emp_code, "", "", "", "");
               if (dt != null && dt.Rows.Count > 0)
               {
                   ret_data[0] = Convert.ToString(dt.Rows.Count);
                   ret_data[1] = Convert.ToString(dt.Rows[0]["EMP_NAME_ENAME"]);
                   ret_data[2] = Convert.ToString(dt.Rows[0]["PERSONAL_NUM_PERNR"]);
                   ret_data[3] = Convert.ToString(dt.Rows[0]["EMP_GROUP_NAME_PTEXT"]);
                   ret_data[4] = Convert.ToString(dt.Rows[0]["EMP_SUBGROUP_NAME_STEXT"]);
                   ret_data[5] = Convert.ToString(dt.Rows[0]["REPORTING_EMP_NUM_REPNR"]);
                   ret_data[6] = Convert.ToString(dt.Rows[0]["REPORTING_EMP_NAME_REPNM"]);
               }
        }
        catch (Exception Exc)
        {
            Logger.WriteEventLog(false, Exc); 
            ret_data[0] = "0";
            ret_data[1] = ret_data[2] = ret_data[3] = ret_data[4] = ret_data[5] = ret_data[6] = "";
        }
        return ret_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string[] getInMaster(string emp_code, int param)
    {
        string isdata = "";
        string[] ret_data = new string[7];
        ret_data[0] = "0";
        ret_data[1] = ret_data[2] = ret_data[3] = ret_data[4] = ret_data[5] = ret_data[6] = "";
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Reporting_Manager.aspx", "pgetdata", ref isdata, param, emp_code, "", "", "", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                ret_data[0] = Convert.ToString(dt.Rows.Count);
            }
        }
        catch (Exception Exc)
        {
            Logger.WriteEventLog(false, Exc);
            ret_data[0] = "0";
            ret_data[1] = ret_data[2] = ret_data[3] = ret_data[4] = ret_data[5] = ret_data[6] = "";
        }
        return ret_data;
    }
}