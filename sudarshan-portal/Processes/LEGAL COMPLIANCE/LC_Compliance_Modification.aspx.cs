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


public partial class LC_Compliance_Modification : System.Web.UI.Page
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
        txt_wiid.Text = Request.Params["wiid"].ToString();
        txtInstanceID.Text = Request.Params["instanceid"].ToString();
        txt_Step_Name.Text = Request.Params["step"].ToString();
    }

    

    private void FillCompliance_Details()
    {
        string isValid = string.Empty;
        try
        {
            GetData obj = new GetData();
            Audit_trail.InnerHtml = obj.fillAuditTrail(txtProcessID.Text, txtInstanceID.Text);

            ListItem Li = new ListItem("--Select One--", "");
            string isdata = string.Empty;
            DataTable CMP_Dtl = (DataTable)ActionController.ExecuteAction("", "LC_Compliance_Completion.aspx", "psearch", ref isdata, txtProcessID.Text, txtInstanceID.Text, "", "", "", "0", "", "", "","");
            if (CMP_Dtl.Rows.Count > 0)
            {
                txt_Task_Type.Text = CMP_Dtl.Rows[0]["TASK_TYPE"].ToString();
                txt_Request_by.Text = CMP_Dtl.Rows[0]["REQUEST_BY"].ToString();
                lbl_Request_Date.InnerText = CMP_Dtl.Rows[0]["REQUEST_DATE"].ToString();
                lbl_Requester.InnerText = CMP_Dtl.Rows[0]["REQUEST_NAME"].ToString();
                txt_Approver.Text = CMP_Dtl.Rows[0]["APPROVER_LOGINID"].ToString();
                txt_Request_No.Text = CMP_Dtl.Rows[0]["REQUEST_NUMBER"].ToString();
                txt_Compliance_ID.Text = CMP_Dtl.Rows[0]["FK_CMPL_ID"].ToString();
                lbl_Compliance_Name.InnerText = CMP_Dtl.Rows[0]["CMPL_TASK_NAME"].ToString();
                lbl_Cmp_Desc.InnerText = CMP_Dtl.Rows[0]["CMPLDESCRIPTION"].ToString();
                lbl_Cmp_Section.InnerText = CMP_Dtl.Rows[0]["CMP_SECTION"].ToString();
                txt_Last_Date.Text = CMP_Dtl.Rows[0]["LAST_DT_OF_SUBMIT"].ToString();
                txt_Grace_Date.Text = CMP_Dtl.Rows[0]["GRACE_DATE"].ToString();
                txt_Submission_Date.Text = CMP_Dtl.Rows[0]["SUBMISSION_DATE"].ToString();
                Lbl_Category.InnerText = CMP_Dtl.Rows[0]["CATEGORY_NAME"].ToString();
                if (txt_Task_Type.Text != "Scheduler")
                {
                    lbl_Approver_Name.InnerText = CMP_Dtl.Rows[0]["EMPLOYEE_NAME"].ToString();
                    lbl_Request_Date.InnerText = CMP_Dtl.Rows[0]["REQUEST_DATE"].ToString();
                    lbl_assign_name.InnerText = CMP_Dtl.Rows[0]["VERIFY_NAME"].ToString();
                }
                else
                {
                    Sub_Appr_Div.Style["display"] = "none";
                    Assign_Name.Style["display"] = "none";
                    Assign_Value.Style["display"] = "none";

                }


        
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    #endregion
    #region Events
    protected void btn_Save_click(object sender, EventArgs e)
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
                if (txt_Task_Type.Text != "Manualy")
                {
                    DataTable App_Dt = (DataTable)ActionController.ExecuteAction("", "LC_Compliance_Task.aspx", "psearch", ref isdata, 0, 0, txt_Compliance_ID.Text, "", "", "", "", 0, "", "", "", "9", "0");
                    int len = App_Dt.Rows.Count;
                    Dval = new string[len];
                    if (App_Dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < App_Dt.Rows.Count; j++)
                        {
                            Dval[j] = App_Dt.Rows[j]["PERSON_NAME"].ToString();
                        }
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Approver is not found');}</script>");
                        return;
                    }
                }
                else
                {
                    Dval = new string[1];
                    Dval[0] = txt_Approver.Text;
                }
                isSaved = (string)ActionController.ExecuteAction("", "LC_Compliance_Task.aspx", "pamd", ref isdata, txtProcessID.Text, txtInstanceID.Text, txt_Compliance_ID.Text, "", txt_Last_Date.Text, txt_Grace_Date.Text, txt_Submission_Date.Text, "0", txt_Remarks.Text, "", txt_amd_by.Text, 10, "0");
                if (isSaved == "true")
                {
                    bool isCreate = false;
                    isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, txt_Step_ID.Text, txt_Step_Name.Text, "SUBMIT", "", txt_amd_by.Text, "", "", "", "", "", "", "", "", "", "", Dval, lbl_Compliance_Name.InnerText.ToString() + '-' + txt_Grace_Date.Text.ToString(), txt_wiid.Text, ref isdata);
                    if (isCreate)
                    {
                        Close_div.Style["display"] = "none";
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + lbl_Compliance_Name.InnerText + " Saved and  Sent to Legal Compliance Completetion ');}</script>");
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