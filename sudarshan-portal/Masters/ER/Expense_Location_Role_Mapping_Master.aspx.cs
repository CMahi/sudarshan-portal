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
using System.Web.Services;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class Expense_Location_Role_Mapping_Master : System.Web.UI.Page
{
    string IsData = string.Empty;
    StringBuilder str = new StringBuilder();
    ListItem Li = new ListItem("--Select One--", "");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Expense_Location_Role_Mapping_Master));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {                   
                    txtCreatedBy.Text = ((string)Session["User_ADID"]);
                    FillLocation();
                    FillPayment();
                    FillRole();
                    ddllocation.SelectedIndex = 0;
                    ddlpaymode.SelectedIndex = 0;
                    ddlrole.SelectedIndex = 0;
                    txt_user_Id.Text = "";
                    showAll();
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }

    private void FillLocation()
    {
        String IsData = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Expense_Location_Role_Mapping_Master.aspx", "fill", ref IsData, 0);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddllocation.DataSource = dt;
            ddllocation.DataTextField = "LOCATION_NAME";
            ddllocation.DataValueField = "PK_LOCATION_ID";
            ddllocation.DataBind();
            ddllocation.Items.Insert(0, Li);
        }
    }

    private void FillPayment()
    {
        String IsData = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Expense_Location_Role_Mapping_Master.aspx", "fill", ref IsData, 1);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlpaymode.DataSource = dt;
            ddlpaymode.DataTextField = "PAYMENT_MODE";
            ddlpaymode.DataValueField = "PK_PAYMENT_MODE";
            ddlpaymode.DataBind();
            ddlpaymode.Items.Insert(0, Li);
        }
    }

    private void FillRole()
    {
        String IsData = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Expense_Location_Role_Mapping_Master.aspx", "fill", ref IsData, 2);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlrole.DataSource = dt;
            ddlrole.DataTextField = "ACCESS_ROLE_NAME";
            ddlrole.DataValueField = "ACCESS_ROLE_ID";
            ddlrole.DataBind();
            ddlrole.Items.Insert(0, Li);
        }
    }

    private void showAll()
    {
        try
        {
            DataTable DTS;
            string IsValid = string.Empty;
            txt_procedure.Text = "Domestic_Adv_policy";
            DTS = (DataTable)ActionController.ExecuteAction("", "Expense_Location_Role_Mapping_Master.aspx", "select", ref IsValid, "", "", "EXPENSE_LOCATION_ROLE_MAPPING", "%");
            Session["ResultData"] = DTS;

            if (DTS.Rows.Count > 0)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<table id='data-table1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th align='left'>Sr.No</th><th align='left'>User ID</th> <th align='left'>Role</th><th align='left'>Paymode</th><th align='left'>Location</th><th align='left'>Edit</th><th align='left'>Action</th> </tr></thead>");
                str.Append("<tbody>");

                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td align='left'><input type='text' Id='pk_mapp_id_" + (i + 1) + "' value='" + DTS.Rows[i]["PK_LOCATION_ROLE_MAPPING_ID"].ToString() + "' style='display:none'/> " + (i + 1) + "</td>");
                    str.Append("<td align='left'><input type='text' id='emp_name_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["AD_ID"].ToString()) + "' />" + DTS.Rows[i]["Employee_Name"].ToString() + "</td>");
                    str.Append("<td align='left'><input type='text' id='accessRole_name_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["FK_ROLE_ID"].ToString()) + "' />" + DTS.Rows[i]["Access_Role_Name"].ToString() + "</td>");
                    str.Append("<td align='left'><input type='text' id='paymode_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["FK_PAYMENT_MODE_ID"].ToString()) + "' />" + DTS.Rows[i]["Payment_Mode"].ToString() + "</td>");
                    str.Append("<td align='left'><input type='text' id='cashLoc_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["FK_CASH_LOCATION_ID"].ToString()) + "' />" + DTS.Rows[i]["Cash_Location"].ToString() + "</td>");
                    str.Append("<td align='left' ><a  href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='getData(" + (i + 1) + ")'><span class='btn-cyan glyphicon glyphicon-pencil' id='spn" + (i + 1) + "'></span></a></td>");
                    str.Append("<td align='left' ><a  href='#DeletePanel' role='button' data-toggle='modal' Title='Delete' onclick='deleteData(" + (i + 1) + ")'><span class='btn-red glyphicon glyphicon-trash'></span></a></td>");
                    str.Append("</tr>");
                }
                str.Append("   </tbody>   </table> ");
                dv_dtl.InnerHtml = str.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Data Found !!')}</script>");
                dv_dtl.InnerHtml = null;
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected void btnHomeOnClick(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=3");
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected void btnSaveOnClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            txtCondition.Text = "0";
            txtParameterID.Text = "0";
            txtHeadIdID.Text = "0";
            Hashtable validationHash = new Hashtable();
            string Result = string.Empty;
            string isInsert = string.Empty;
            string location = "0";
            if (ddlpaymode.SelectedItem.Text.ToUpper() == "CASH")
            {
                location = ddllocation.SelectedValue;
            }
            try
            {
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag)
                {
                    Result = (string)ActionController.ExecuteAction("", "Expense_Location_Role_Mapping_Master.aspx", "insert", ref isInsert, txtHeadIdID.Text, txt_user_Id.Text, location, ddlpaymode.SelectedValue, ddlrole.SelectedValue, txtCreatedBy.Text, 0);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                        showAll();
                    }
                    else
                    {
                        txt_user_Id.Text = "";
                        ddllocation.SelectedIndex = 0;
                        ddlpaymode.SelectedIndex = 0;
                        ddlrole.SelectedIndex = 0;
                        showAll();
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Saved.');}</script>");
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> getUser(string prefixText)
    {
        string isvalid = "";
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Expense_Location_Role_Mapping_Master.aspx", "getusers", ref isvalid, prefixText);
        List<string> UserNames = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            UserNames.Add(dt.Rows[i][0].ToString());
        }
        return UserNames;
    }

    protected void btnUpdateOnClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ActionController.SetControlAttributes(Page);
            Hashtable validationHash = new Hashtable();
            string Result = string.Empty;
            string isInsert = string.Empty;

            txtParameterID.Text = "1";
            try
            {
                string location = "0";
                if (ddlpaymode.SelectedItem.Text.ToUpper() == "CASH")
                {
                    location = ddllocation.SelectedValue;
                }
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag)
                {
                    Result = (string)ActionController.ExecuteAction("", "Expense_Location_Role_Mapping_Master.aspx", "insert", ref isInsert, txt_PkId.Text, txt_user_Id.Text, location, ddlpaymode.SelectedValue, ddlrole.SelectedValue, txtCreatedBy.Text, 1);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                        showAll();
                    }
                    else
                    {
                        txt_user_Id.Text = "";
                        ddllocation.SelectedIndex = 0;
                        ddlpaymode.SelectedIndex = 0;
                        ddlrole.SelectedIndex = 0;
                        showAll();
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Update.');}</script>");
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string updateExpLocRoleMapping(int pk_id, string username)
    {
        string isUpdated = string.Empty;
        string isdata = string.Empty;

        try
        {
            isUpdated = (string)ActionController.ExecuteAction("", "Expense_Location_Role_Mapping_Master.aspx", "insert", ref isdata, pk_id, "", 0, 0, 0, "", 2);
            if (isUpdated == "true")
            {
                DataTable DTS;
                string IsValid = string.Empty;
                DTS = (DataTable)ActionController.ExecuteAction("", "Expense_Location_Role_Mapping_Master.aspx", "select", ref IsValid, "", "", "EXPENSE_LOCATION_ROLE_MAPPING", "%");
                if (DTS.Rows.Count > 0)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("<table id='tbl_Update'  class='table table-bordered table-hover'> <thead><tr class='grey'><th align='left'>Sr.No</th><th align='left'>User ID</th> <th align='left'>Role</th><th align='left'>Paymode</th><th align='left'>Location</th><th align='left'>Edit</th><th align='left'>Action</th> </tr></thead>");
                    str.Append("<tbody>");

                    for (int i = 0; i < DTS.Rows.Count; i++)
                    {
                        str.Append(" <tr>");
                        str.Append("<td align='left'><input type='text' Id='pk_mapp_id_" + (i + 1) + "' value='" + DTS.Rows[i]["PK_LOCATION_ROLE_MAPPING_ID"].ToString() + "' style='display:none'/> " + (i + 1) + "</td>");
                        str.Append("<td align='left'><input type='text' id='emp_name_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["AD_ID"].ToString()) + "' />" + DTS.Rows[i]["Employee_Name"].ToString() + "</td>");
                        str.Append("<td align='left'><input type='text' id='accessRole_name_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["FK_ROLE_ID"].ToString()) + "' />" + DTS.Rows[i]["Access_Role_Name"].ToString() + "</td>");
                        str.Append("<td align='left'><input type='text' id='paymode_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["FK_PAYMENT_MODE_ID"].ToString()) + "' />" + DTS.Rows[i]["Payment_Mode"].ToString() + "</td>");
                        str.Append("<td align='left'><input type='text' id='cashLoc_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["FK_CASH_LOCATION_ID"].ToString()) + "' />" + DTS.Rows[i]["Cash_Location"].ToString() + "</td>");
                        str.Append("<td align='left' ><a  href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='getData(" + (i + 1) + ")'><span class='btn-cyan glyphicon glyphicon-pencil' id='spn" + (i + 1) + "'></span></a></td>");
                        str.Append("<td align='left' ><a  href='#DeletePanel' role='button' data-toggle='modal' Title='Delete' onclick='deleteData(" + (i + 1) + ")'><span class='btn-red glyphicon glyphicon-trash'></span></a></td>");
                        str.Append("</tr>");
                    }
                    str.Append("   </tbody>   </table> ");
                    isUpdated = str.ToString();
                }
                else
                {
                    isUpdated = "false";
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return isUpdated;
    }
}