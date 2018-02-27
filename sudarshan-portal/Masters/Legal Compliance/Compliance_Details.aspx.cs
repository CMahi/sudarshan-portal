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

public partial class Compliance_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Compliance_Details));
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                    txt_Complaince_Name.Text = string.Empty;
                    showall();
                    FillDates();
                    fillState();
                    fillOffice();
                    FillType();
                    FillAssign_To();
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    #region Pageload
    private void FillDates()
    {
        for (int i = 1; i <= 31; i++)
        {
            ddl_Add_SDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            ddl_Grace_Days.Items.Add(new ListItem(i.ToString(), i.ToString()));
            ddl_Edit_LDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            ddl_Edit_GDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            
        }
        ddl_Add_SDay.Items.Insert(0, new ListItem("---Select One---", "0"));
        ddl_Grace_Days.Items.Insert(0, new ListItem("---Select One---", "0"));
        ddl_Edit_LDay.Items.Insert(0, new ListItem("---Select One---", "0"));
        ddl_Edit_GDay.Items.Insert(0, new ListItem("---Select One---", "0"));

    }
    private void fillState()
    {
        try
        {
            ListItem Li = new ListItem("--Select One--", "");
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Compliance_Details.aspx", "psearch", ref isdata, "0", "", "", "", "0", "0", "", "", "", "", "", "4", "","0","");
            if (dt != null)
            {
                ddl_State.DataSource = dt;
                ddl_State.DataTextField = "STATE_NAME";
                ddl_State.DataValueField = "PK_STATE_ID";
                ddl_State.DataBind();
                ddl_State.Items.Insert(0, Li);

                ddl_Edit_State.DataSource = dt;
                ddl_Edit_State.DataTextField = "STATE_NAME";
                ddl_Edit_State.DataValueField = "PK_STATE_ID";
                ddl_Edit_State.DataBind();
                ddl_Edit_State.Items.Insert(0, Li);
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    private void FillType()
    {
        try
        {
            ListItem Li = new ListItem("--Select One--", "");
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Compliance_Details.aspx", "psearch", ref isdata, "0", "", "", "", "0", "0", "", "", "", "", "", "8", "","0","");
            if (dt != null)
            {
                ddl_Cmp_Cat.DataSource = dt;
                ddl_Cmp_Cat.DataTextField = "CATEGORY_NAME";
                ddl_Cmp_Cat.DataValueField = "PK_CMP_CAT_ID";
                ddl_Cmp_Cat.DataBind();
                ddl_Cmp_Cat.Items.Insert(0, Li);

                ddl_Edit_type.DataSource = dt;
                ddl_Edit_type.DataTextField = "CATEGORY_NAME";
                ddl_Edit_type.DataValueField = "PK_CMP_CAT_ID";
                ddl_Edit_type.DataBind();
                ddl_Edit_type.Items.Insert(0, Li);
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    private void FillAssign_To()
    {
        try
        {
            ListItem Li = new ListItem("--Select One--", "");
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Compliance_Details.aspx", "psearch", ref isdata, "0", "", "", "", "0", "0", "", "", "", "", "", "9", "", "0","");
            if (dt != null)
            {
                ddl_Add_Assign_to.DataSource = dt;
                ddl_Add_Assign_to.DataTextField = "EMPLOYEE_NAME";
                ddl_Add_Assign_to.DataValueField = "AD_ID";
                ddl_Add_Assign_to.DataBind();
                ddl_Add_Assign_to.Items.Insert(0, Li);

                ddl_Edit_Assign_to.DataSource = dt;
                ddl_Edit_Assign_to.DataTextField = "EMPLOYEE_NAME";
                ddl_Edit_Assign_to.DataValueField = "AD_ID";
                ddl_Edit_Assign_to.DataBind();
                ddl_Edit_Assign_to.Items.Insert(0, Li);
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    private void fillOffice()
    {
        try
        {
            ListItem Li = new ListItem("--Select One--", "");
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Compliance_Details.aspx", "psearch", ref isdata, "0", "", "", "", "0", "0", "", "", "", "", "", "7", "", "0","");
            if (dt != null)
            {
                ddl_EditOffice.DataSource = dt;
                ddl_EditOffice.DataTextField = "GOV_AUTHORITY_NAME";
                ddl_EditOffice.DataValueField = "PK_AUTHORITY_ID";
                ddl_EditOffice.DataBind();
                ddl_EditOffice.Items.Insert(0, Li);
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    private void showall()
    {
        try
        {
      
            StringBuilder str = new StringBuilder();
            string isdata = string.Empty;
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Compliance_Details.aspx", "psearch", ref isdata, "0", "", "", "", "0", "0", "", "", "", "","","0","","0","");
            if (DTS.Rows.Count > 0)
            {
                str.Append("<table class='table table-bordered table-hover'>");
                str.Append("<thead>");
                str.Append("<tr class='grey'>");
                str.Append("<th>Sr.No</th><th>Category Name</th><th>Compliance Name</th><th>Compliance Description</th><th>Compliance Section</th><th>State Name</th><th>Applicable Office Name</th><th>Frequency of Occurance</th><th>Last Date Of Submission</th><th>Grace Date</th><th>Consequences of Non-Compliance</th><th>Assign To</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td>" + (i + 1) + "</td>");
                    str.Append("<td>" + DTS.Rows[i]["CATEGORY_NAME"].ToString() + "<input id='txt_FK_CMP_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FK_CMP_CATEGORY_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["CMPL_TASK_NAME"].ToString() + "<input id='txt_Cmp_Task_Name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CMPL_TASK_NAME"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["CMPLDESCRIPTION"].ToString() + "<input id='txt_Cmp_Des" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CMPLDESCRIPTION"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["CMP_SECTION"].ToString() + "<input id='txt_Cmp_Section" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CMP_SECTION"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["STATE_NAME"].ToString() + "<input id='txt_FK_State_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FK_STATE_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["GOV_AUTHORITY_NAME"].ToString() + "<input id='txt_FK_Autho_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FK_AUTHORITY_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["FREQUENCY"].ToString() + "<input id='txt_Cmp_Frequency" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FREQUENCY"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["LAST_DT_OF_SUBMIT"].ToString() + "<input id='txt_Cmp_Last_Date" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["LAST_DT_OF_SUBMIT"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["GRESS_PERIOD"].ToString() + "<input id='txt_Cmp_Gress_Period" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["GRESS_PERIOD"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["CONSEQUENCES"].ToString() + "<input id='txt_Cmp_Consequence" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CONSEQUENCES"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["EMPLOYEE_NAME"].ToString() + "<input id='txt_Assign_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["VERIFICATION_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td><a href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='bindData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a>  <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_CMPL_TASK_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td><a href='#DeletePanel'  role='button' data-toggle='modal' Title='Delete' onclick='deleteData(" + (i + 1) + ")'><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td>");
                    str.Append("</tr>");
                }
                str.Append("   </tbody>   </table> ");
                div_Details.InnerHtml = str.ToString();
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Data Found !!')}</script>");
                div_Details.InnerHtml = null;
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

    }
    #endregion

    #region Ajax
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string bindData(string Condition, string SerachString)
    {
        StringBuilder str = new StringBuilder();
        try
        {
            string isdata = string.Empty;
             DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Compliance_Details.aspx", "psearch", ref isdata, "0", "", "", "", "0", "0", "", "", "", "", "", Condition, SerachString, "0","");
            if (DTS.Rows.Count > 0)
            {
                str.Append("<table class='table table-bordered table-hover'>");
                str.Append("<thead>");
                str.Append("<tr class='grey'>");
                str.Append("<th>#</th><th>Category Name</th><th>Compliance Name</th><th>Compliance Description</th><th>Compliance Section</th><th>State Name</th><th>Applicable Office Name</th><th>Frequency of Occurance</th><th>Last Date Of Submission</th><th>Grace Date</th><th>Consequences of Non-Compliance</th><th>Assign To</th><th>Update</th><th>Delete</th></tr></thead><tbody>");
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td>" + (i + 1) + "</td>");
                    str.Append("<td>" + DTS.Rows[i]["CATEGORY_NAME"].ToString() + "<input id='txt_FK_CMP_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FK_CMP_CATEGORY_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["CMPL_TASK_NAME"].ToString() + "<input id='txt_Cmp_Task_Name" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CMPL_TASK_NAME"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["CMPLDESCRIPTION"].ToString() + "<input id='txt_Cmp_Des" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CMPLDESCRIPTION"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["CMP_SECTION"].ToString() + "<input id='txt_Cmp_Section" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CMP_SECTION"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["STATE_NAME"].ToString() + "<input id='txt_FK_State_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FK_STATE_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["GOV_AUTHORITY_NAME"].ToString() + "<input id='txt_FK_Autho_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FK_AUTHORITY_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["FREQUENCY"].ToString() + "<input id='txt_Cmp_Frequency" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["FREQUENCY"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["LAST_DT_OF_SUBMIT"].ToString() + "<input id='txt_Cmp_Last_Date" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["LAST_DT_OF_SUBMIT"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["GRESS_PERIOD"].ToString() + "<input id='txt_Cmp_Gress_Period" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["GRESS_PERIOD"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["CONSEQUENCES"].ToString() + "<input id='txt_Cmp_Consequence" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["CONSEQUENCES"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td>" + DTS.Rows[i]["EMPLOYEE_NAME"].ToString() + "<input id='txt_Assign_ID" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["VERIFICATION_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td><a href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='bindData(" + (i + 1) + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit'></i></a>  <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_CMPL_TASK_ID"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td><a href='#DeletePanel'  role='button' data-toggle='modal' Title='Delete' onclick='deleteData(" + (i + 1) + ")'><i class='fa fa-fw pull-left f-s-18 fa-trash'></i><a></td>");
                    str.Append("</tr>");
                }
                str.Append("   </tbody>   </table> ");
                div_Details.InnerHtml = str.ToString();
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(str);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string[] getOffice(int Fk_Stateid)
    {
        string[] ResultData = null;
        string office_Data = string.Empty;
        string isValid = string.Empty;
        if (!ActionController.IsSessionExpired(this, true))
        {
            ResultData = new string[1];
            try
            {
                DataTable office_dt = (DataTable)ActionController.ExecuteAction("", "Compliance_Details.aspx", "psearch", ref isValid, "0", "", "", "", Fk_Stateid, "0", "", "", "", "", "", 5, "", "0","");
                if (office_dt.Rows.Count > 0)
                {
                    for (int i = 0; i < office_dt.Rows.Count; i++)
                    {

                        office_Data = office_dt.Rows[i][0].ToString() + "||" + office_dt.Rows[i][1].ToString() + "," + office_Data;

                    }
                }     
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
        ResultData[0] = office_Data;
        return ResultData;
    }
    #endregion

    #region Events
    protected void btn_AddNew_Click(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                string Last_Date = string.Empty;
                string Grace_Date = string.Empty;
                Last_Date = ddl_Add_SDay.SelectedValue + '-' + ddl_Add_SMonth.SelectedItem.Text;
                Grace_Date = ddl_Grace_Days.SelectedValue + '-' + ddl_GMonth.SelectedItem.Text;
                string isdata = string.Empty;
                string created_by = txt_Username.Text.Trim();
                string isSaved = (string)ActionController.ExecuteAction("", "Compliance_Details.aspx", "pamd", ref isdata, 0, txt_Complaince_Name.Text.Trim(), txt_Desc.Text.Trim(), txt_Section.Text.Trim(), ddl_State.SelectedValue, txt_Fk_Authority_ID.Text, ddl_Add_Frequency.SelectedValue, Last_Date,Grace_Date,txt_Consequences.Text,created_by,1,"",ddl_Cmp_Cat.SelectedValue,ddl_Add_Assign_to.SelectedValue);
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Saved.');}</script>");
                    txt_Complaince_Name.Text = ""; 
                    txt_Desc.Text = "";
                    txt_Section.Text = "";
                    ddl_State.SelectedIndex = 0;
                    ddl_Add_Frequency.SelectedIndex = 0;
                    ddl_Add_SDay.SelectedIndex = 0;
                    ddl_Add_SMonth.SelectedIndex = 0;
                    ddl_Grace_Days.SelectedIndex = 0;
                    ddl_Cmp_Cat.SelectedIndex = 0;
                    ddl_GMonth.SelectedValue = "0";
                    txt_Fk_Authority_ID.Text = "";
                    txt_Consequences.Text = "";
                    ddl_Add_Assign_to.SelectedIndex = 0;
                    showall();
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + isSaved + "');}</script>");
                    txt_Complaince_Name.Text = "";
                    txt_Desc.Text = "";
                    txt_Section.Text = "";
                    ddl_State.SelectedIndex = 0;
                    ddl_Add_Frequency.SelectedIndex = 0;
                    ddl_Add_SDay.SelectedIndex = 0;
                    ddl_Add_SMonth.SelectedIndex = 0;
                    ddl_Grace_Days.SelectedIndex = 0;
                    ddl_GMonth.SelectedValue = "0";
                    txt_Fk_Authority_ID.Text = "";
                    txt_Consequences.Text = "";
                    ddl_Edit_type.SelectedIndex = 0;
                    ddl_Add_Assign_to.SelectedIndex = 0;
                }


            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void btn_Edit_onClick(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                string Last_Date = string.Empty;
                string Grace_Date = string.Empty;
                Last_Date = ddl_Edit_LDay.SelectedValue + '-' + ddl_Edit_LMonth.SelectedItem.Text;
                Grace_Date = ddl_Edit_GDay.SelectedValue + '-' + ddl_Edit_GMonth.SelectedItem.Text;
                string isdata = string.Empty;
                string created_by = txt_Username.Text.Trim();
                if (txt_Edit_Fk_Authority_ID.Text=="")
                {
                    txt_Edit_Fk_Authority_ID.Text = ddl_EditOffice.SelectedValue;   
                }
                string isSaved = (string)ActionController.ExecuteAction("", "Compliance_Details.aspx", "pamd", ref isdata, txt_PK_ID.Text, txt_Edit_Complaince_Name.Text.Trim(), txt_Edit_Cmp_Desc.Text.Trim(), txt_EditSectiom.Text.Trim(), ddl_Edit_State.SelectedValue, txt_Edit_Fk_Authority_ID.Text, ddl_Edit_Frequency.SelectedValue, Last_Date, Grace_Date, txt_Edit_Consequences.Text, created_by, 2, "",ddl_Edit_type.SelectedValue,ddl_Edit_Assign_to.SelectedValue);
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Updated.');}</script>");
                    showall();
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + isSaved + "');}</script>");
                }


            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void btn_Delete_onClick(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                string isdata = string.Empty;
                string created_by = txt_Username.Text.Trim();
                string isSaved = (string)ActionController.ExecuteAction("", "Compliance_Details.aspx", "pamd", ref isdata, txt_PK_ID.Text,"","", "", "0","0","","","","", created_by, 3, "","0","");
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Deleted.');}</script>");
                    showall();
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + isSaved + "');}</script>");
                }


            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                txt_Complaince_Name.Text = "";
                txt_Desc.Text = "";
                txt_Section.Text = "";
                ddl_State.SelectedIndex = 0;
                ddl_Add_Frequency.SelectedIndex = 0;
                ddl_Add_SDay.SelectedIndex = 0;
                ddl_Add_SMonth.SelectedIndex = 0;
                ddl_Grace_Days.SelectedIndex = 0;
                ddl_Cmp_Cat.SelectedIndex = 0;
                ddl_GMonth.SelectedValue = "0";
                txt_Fk_Authority_ID.Text = "";
                txt_Consequences.Text = "";
                ddl_Add_Assign_to.SelectedIndex = 0;
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }
    #endregion

}