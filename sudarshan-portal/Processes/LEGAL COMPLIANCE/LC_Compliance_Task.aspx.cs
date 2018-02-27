using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using FSL.Controller;
using AjaxPro;
using System.IO;
using System.Data.Common;
using System.Text;
using System.Net;
using FSL.Logging;


public partial class LC_Compliance_Task : System.Web.UI.Page
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
                FillCompliance_Details();
            }
        }
    }

    #region Pageload
    private void Initialization()
    {
        txt_amd_by.Text = (string)Session["User_ADID"];
        txtCreatedByEmail.Text = (string)Session["EmailID"];
        txtProcessID.Text = Request.Params["processid"].ToString();
        txt_Step_ID.Text = Request.Params["stepid"].ToString();
        txt_Last_Date.Attributes.Add("readOnly", "readOnly");
        txt_Grace_Date.Attributes.Add("readOnly", "readOnly");
        txt_Submission_Date.Attributes.Add("readOnly", "readOnly");
        
    }

    private void FillCompliance_Details()
    {
        string isValid = string.Empty;
        StringBuilder Doc_Detail = new StringBuilder();
        try
        {
            ListItem Li = new ListItem("--Select One--", "");
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "LC_Compliance_Task.aspx", "psearch", ref isdata, 0, 0, 0, "", "", "", "", 0, "", "", "", "11","0");
            if (dt != null)
            {
                ddl_Comp_Cat.DataSource = dt;
                ddl_Comp_Cat.DataTextField = "CATEGORY_NAME";
                ddl_Comp_Cat.DataValueField = "PK_CMP_CAT_ID";
                ddl_Comp_Cat.DataBind();
                ddl_Comp_Cat.Items.Insert(0, Li);
            }
            DataTable Approver = (DataTable)ActionController.ExecuteAction("", "LC_Compliance_Task.aspx", "psearch", ref isdata, 0, 0, 0, "", "", "", "", 0, "", "", "", "2","0");
            if (Approver != null)
            {
                ddl_Approver.DataSource = Approver;
                ddl_Approver.DataTextField = "EMPLOYEE_NAME";
                ddl_Approver.DataValueField = "AD_ID";
                ddl_Approver.DataBind();
                ddl_Approver.Items.Insert(0, Li);

                ddl_Assign_to.DataSource = Approver;
                ddl_Assign_to.DataTextField = "EMPLOYEE_NAME";
                ddl_Assign_to.DataValueField = "AD_ID";
                ddl_Assign_to.DataBind();
                ddl_Assign_to.Items.Insert(0, Li);
            }

            DataTable Email = (DataTable)ActionController.ExecuteAction("", "LC_Compliance_Task.aspx", "psearch", ref isdata, 0, 0, 0, "", "", "", "", 0, "", "", "", "3","0");
            if (Email != null)
            {
                ddl_Email_Type.DataSource = Email;
                ddl_Email_Type.DataTextField = "EMAIL_TYPE";
                ddl_Email_Type.DataValueField = "PK_EMAILTYPE_ID";
                ddl_Email_Type.DataBind();
                ddl_Email_Type.Items.Insert(0, Li);
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
      [System.Web.Services.WebMethod]
    public static string[] getcomplaincedata(int FK_Compliance_ID, int Complaice_Type)
    {
        string[] ResultData = null;
        string CMP_Desc = string.Empty;
        string CMP_Section = string.Empty;
        string CMP_Conseq = string.Empty;
        string isValid = string.Empty;

        ResultData = new string[5];
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "LC_Compliance_Task.aspx", "psearch", ref isValid, 0, 0, FK_Compliance_ID, "", "", "", "", "0", "", "", "", "1", Complaice_Type);
            if (dt.Rows.Count > 0)
            {
                CMP_Desc = dt.Rows[0]["CMPLDESCRIPTION"].ToString();
                CMP_Section = dt.Rows[0]["CMP_SECTION"].ToString();
                CMP_Conseq = dt.Rows[0]["CONSEQUENCES"].ToString();
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        ResultData[0] = CMP_Desc;
        ResultData[1] = CMP_Section;
        ResultData[2] = CMP_Conseq;
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
                string[] Dval = null;
                string isdata = string.Empty;
                string isSaved = string.Empty;
                string created_by = txt_amd_by.Text.Trim();
                string instanceID = string.Empty;
                Dval = new string[1];
                Dval[0] = ddl_Approver.SelectedValue;
                instanceID = (string)WFE.Action.StartCase(isSaved, txtProcessID.Text, txt_amd_by.Text, "", txtEmailID.Text, "108");
                txtInstanceID.Text = instanceID;
                isSaved = (string)ActionController.ExecuteAction("", "LC_Compliance_Task.aspx", "pamd", ref isdata, txtProcessID.Text, txtInstanceID.Text, txt_Compliance_ID.Text, ddl_Assign_to.SelectedValue, txt_Last_Date.Text, txt_Grace_Date.Text, txt_Submission_Date.Text, ddl_Email_Type.SelectedValue, txt_Remarks.Text, ddl_Approver.SelectedValue, txt_amd_by.Text, 5, ddl_Comp_Cat.SelectedValue);
                if (isSaved!="")
                {
                    string[] Request_Number = new string[2];
                    Request_Number = isSaved.Split('=');
                    if (Request_Number[1] == "true")
                    {
                        string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                        string path = string.Empty;
                        string foldername = Request_Number[0].ToString();
                        foldername = foldername.Replace("/", "_");
                        path = activeDir + "\\" + foldername;
                        if (Directory.Exists(path))
                        {

                        }
                        else
                        {
                            Directory.CreateDirectory(path);
                        }
                        bool isCreate = false;
                        isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, txt_Step_ID.Text, "LEGAL COMPLIANCE", "SUBMIT", "", txt_amd_by.Text, "", "", "", "", "", "", "", "", "", "", Dval, txt_Compliance_Text.Text.ToString() + '-' + txt_Grace_Date.Text.ToString(), "0", ref isdata);
                        if (isCreate)
                        {
                            Close_div.Style["display"] = "none";
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + txt_Compliance_Text.Text + "-" + txt_Grace_Date.Text + " Saved and  Sent to Legal Compliance Completion ');}</script>");
                        }


                    }
                    else
                    {
                        if (Request_Number[0].ToString() == "Duplicate Entry Not Allowed !!!")
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Duplicate Entry Not Allowed');}</script>");
                            ddl_Compliance_Name.SelectedIndex = 0;
                            ddl_Email_Type.SelectedIndex = 0;
                            ddl_Approver.SelectedIndex = 0;
                            lbl_Cmp_Conse.InnerText = "";
                            lbl_Cmp_Desc.InnerText = "";
                            txt_Grace_Date.Text = "";
                            txt_Last_Date.Text = "";
                            txt_Submission_Date.Text = "";
                            return;
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Error Occured While Saving. Please check Details');}</script>");
                            return;
                        }

                    }  
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Error Occured While Saving. Please check Details');}</script>");
                    return;
                }


            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }
    #endregion
}