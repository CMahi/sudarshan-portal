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


public partial class LC_Compliance_Verification : System.Web.UI.Page
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
        txt_wiid.Text = Request.Params["wiid"].ToString();
        txtInstanceID.Text = Request.Params["instanceid"].ToString();
        txt_Step_Name.Text = Request.Params["step"].ToString();
    }

    private void FillCompliance_Details()
    {
        string isValid = string.Empty;
        StringBuilder Doc_Detail = new StringBuilder();
        try
        {
            string isdata = string.Empty;
            DataTable CMP_Dtl = (DataTable)ActionController.ExecuteAction("", "LC_Compliance_Completion.aspx", "psearch", ref isdata, txtProcessID.Text, txtInstanceID.Text, "", "", "", "0", "", "","","");
            if (CMP_Dtl.Rows.Count > 0)
            {
                txt_TaskType.Text = CMP_Dtl.Rows[0]["TASK_TYPE"].ToString();
                txt_Request_by.Text = CMP_Dtl.Rows[0]["REQUEST_BY"].ToString();
                lbl_Requester.InnerText = CMP_Dtl.Rows[0]["REQUEST_NAME"].ToString();
                lbl_Request_Date.InnerText = CMP_Dtl.Rows[0]["REQUEST_DATE"].ToString();
                txt_Approver.Text = CMP_Dtl.Rows[0]["APPROVER_LOGINID"].ToString();
                txt_Request_No.Text = CMP_Dtl.Rows[0]["REQUEST_NUMBER"].ToString();
                txt_Compliance_ID.Text = CMP_Dtl.Rows[0]["FK_CMPL_ID"].ToString();
                lbl_Compliance_Name.InnerText = CMP_Dtl.Rows[0]["CMPL_TASK_NAME"].ToString();
                lbl_Cmp_Desc.InnerText = CMP_Dtl.Rows[0]["CMPLDESCRIPTION"].ToString();
                lbl_Cmp_Section.InnerText = CMP_Dtl.Rows[0]["CMP_SECTION"].ToString();
                lbl_Last_Date.InnerText = CMP_Dtl.Rows[0]["LAST_DT_OF_SUBMIT"].ToString();
                lbl_Grace_Date.InnerText = CMP_Dtl.Rows[0]["GRACE_DATE"].ToString();
                lbl_Submission.InnerText = CMP_Dtl.Rows[0]["SUBMISSION_DATE"].ToString();
                Lbl_Category.InnerText = CMP_Dtl.Rows[0]["CATEGORY_NAME"].ToString();
                if (txt_TaskType.Text != "Scheduler")
                {
                    lbl_Approver_Name.InnerText = CMP_Dtl.Rows[0]["EMPLOYEE_NAME"].ToString();
                    txt_Category_Id.Text = CMP_Dtl.Rows[0]["FK_CMP_CATEGORY_ID"].ToString();
                    lbl_assign_name.InnerText = CMP_Dtl.Rows[0]["VERIFY_NAME"].ToString();
                    txt_Verifyid.Text = CMP_Dtl.Rows[0]["VERIFY_ID"].ToString();

                }
                else
                {
                    Sub_Appr_Div.Style["display"] = "none";
                    txt_Category_Id.Text = CMP_Dtl.Rows[0]["FK_CMP_CATEGORY_ID"].ToString();
                    lbl_assign_name.InnerText = CMP_Dtl.Rows[0]["EMPLOYEE_NAME"].ToString();
                    txt_Verifyid.Text = CMP_Dtl.Rows[0]["VERIFICATION_ID"].ToString();
                }

            }
            GetData obj = new GetData();
            Audit_trail.InnerHtml = obj.fillAuditTrail(txtProcessID.Text, txtInstanceID.Text);

            DataTable Doc_dt = (DataTable)ActionController.ExecuteAction("", "LC_Compliance_Completion.aspx", "psearch", ref isValid, txtProcessID.Text, txtInstanceID.Text, "", "", "", 2, "", "",  txt_Request_No.Text,"");
            if (Doc_dt.Rows.Count > 0)
            {
                Doc_Detail.Append("<table id='CMP_Docs' style='width:70%' align='center' class='table table-bordered table-hover'>");
                Doc_Detail.Append("<thead>");
                Doc_Detail.Append("<tr class='grey'>");
                Doc_Detail.Append("<th>#</th><th>Document Name</th><th>File Name</th><th>Upload</th><th>Delete</th></tr></thead><tbody>");
                for (int i = 0; i < Doc_dt.Rows.Count; i++)
                {
                    Doc_Detail.Append(" <tr>");
                    Doc_Detail.Append("<td>" + (i + 1) + "</td>");
                    Doc_Detail.Append("<td>" + Doc_dt.Rows[i]["DOC_NAME"].ToString() + "<input id='txt_PK_ID" + (i + 1) + "'  runat='server' Value='" + Doc_dt.Rows[i]["PK_DOC_ID"].ToString() + "' style='display:none' /></td>");
                    Doc_Detail.Append("<td><a id='a_downloadfiles" + (i + 1) + "' style='cursor: pointer' onclick=\"return downloadfiles('" + (i + 1) + "');\" >" + Doc_dt.Rows[i]["FILENAME"].ToString() + "</a></td>");
                    Doc_Detail.Append("<td style='border: 1px solid #ADBBCA;' align='center' width='5%' valign='middle'><img id='del" + (i + 1) + "' alt='Click Here To Upload The Documents.'  src='../../Img/attachment3.png'  style='vertical-align:middle;cursor: pointer'  onclick='loadDocuments(" + (i + 1).ToString() + ");' /></td>");
                    Doc_Detail.Append("<td style='border: 1px solid #ADBBCA;' align='center' width='5%' valign='middle'><img id='delete" + (i + 1) + "' alt='Click Here To Delete The Documents.'  src='../../Img/button_cancel.png'  style='vertical-align:middle;cursor: pointer'  onclick='delteDocuments(" + (i + 1).ToString() + ");' /></td>");
                    Doc_Detail.Append(" </tr>");
                }
                Doc_Detail.Append(" </tbody>");
                Doc_Detail.Append(" </table>");
                div_Details.InnerHtml = Doc_Detail.ToString();
            }



        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    #endregion
    
    #region Events
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            string path = string.Empty;
            string foldername = txt_Request_No.Text;
            string RequestNo = txt_Request_No.Text;
            if (txt_TaskType.Text == "Scheduler")
            {

                foldername = foldername.Replace("/", "_");
                path = activeDir + "\\" + foldername;
                if (Directory.Exists(path))
                {
                    Int32 flength = FileUpload1.PostedFile.ContentLength;
                    RequestNo = RequestNo.Replace("/", "_");
                    path = activeDir + "\\" + RequestNo + "\\";
                    string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());
                    filename = filename.Replace("(", "");
                    filename = filename.Replace(")", "");
                    filename = filename.Replace("&", "");
                    filename = filename.Replace("+", "");
                    filename = filename.Replace("/", "");
                    filename = filename.Replace("\\", "");
                    filename = filename.Replace("'", "");
                    filename = filename.Replace("  ", "");
                    filename = filename.Replace(" ", "");
                    filename = filename.Replace("#", "");
                    filename = filename.Replace("$", "");
                    filename = filename.Replace("~", "");
                    filename = filename.Replace("%", "");
                    filename = filename.Replace("''", "");
                    filename = filename.Replace(":", "");
                    filename = filename.Replace("*", "");
                    filename = filename.Replace("?", "");
                    filename = filename.Replace("<", "");
                    filename = filename.Replace(">", "");
                    filename = filename.Replace("{", "");
                    filename = filename.Replace("}", "");
                    filename = filename.Replace(",", "");
                    DataTable dt = (DataTable)Session["UploadedFiles"];
                    FileUpload1.SaveAs(path + filename);
                    ClearContents(sender as Control);
                }
                else
                {
                    Directory.CreateDirectory(path);

                }
            }
            else
            {
                Int32 flength = FileUpload1.PostedFile.ContentLength;
                RequestNo = RequestNo.Replace("/", "_");
                path = activeDir + "\\" + RequestNo + "\\";
                string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());
                filename = filename.Replace("(", "");
                filename = filename.Replace(")", "");
                filename = filename.Replace("&", "");
                filename = filename.Replace("+", "");
                filename = filename.Replace("/", "");
                filename = filename.Replace("\\", "");
                filename = filename.Replace("'", "");
                filename = filename.Replace("  ", "");
                filename = filename.Replace(" ", "");
                filename = filename.Replace("#", "");
                filename = filename.Replace("$", "");
                filename = filename.Replace("~", "");
                filename = filename.Replace("%", "");
                filename = filename.Replace("''", "");
                filename = filename.Replace(":", "");
                filename = filename.Replace("*", "");
                filename = filename.Replace("?", "");
                filename = filename.Replace("<", "");
                filename = filename.Replace(">", "");
                filename = filename.Replace("{", "");
                filename = filename.Replace("}", "");
                filename = filename.Replace(",", "");
                DataTable dt = (DataTable)Session["UploadedFiles"];
                FileUpload1.SaveAs(path + filename);
                ClearContents(sender as Control);
            }




        }
        catch (Exception Ex)
        {
            Logger.WriteEventLog(false, Ex);
        }
    }

    private void ClearContents(Control control)
    {
        for (var i = 0; i < Session.Keys.Count; i++)
        {
            if (Session.Keys[i].Contains(control.ClientID))
            {
                Session.Remove(Session.Keys[i]);
                break;
            }
        }
    }

    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                string isdata = string.Empty;
                string isSaved = string.Empty;
                string created_by = txt_amd_by.Text.Trim();
                string[] Dval = new string[1];
                Dval[0] = txt_amd_by.Text;

                string inser_FileXML = txtXMLFiles.Text;
                inser_FileXML = inser_FileXML.Replace("&", "&amp;");
                inser_FileXML = inser_FileXML.Replace(">", "&gt;");
                inser_FileXML = inser_FileXML.Replace("<", "&lt;");
                inser_FileXML = inser_FileXML.Replace("||", ">");
                inser_FileXML = inser_FileXML.Replace("|", "<");
                inser_FileXML = inser_FileXML.Replace("'", "&apos;");
                txtXMLFiles.Text = inser_FileXML.ToString();

                isSaved = (string)ActionController.ExecuteAction("", "LC_Compliance_Completion.aspx", "pamd", ref isdata, txtProcessID.Text, txtInstanceID.Text, ddl_Staus.SelectedValue, txt_Remarks.Text, txt_amd_by.Text, "1", txtXMLFiles.Text, txt_Step_Name.Text, txt_Request_No.Text,"Released by Verification");
                if (isSaved == "true")
                {
                    if (ddl_Staus.SelectedValue == "Completed")
                    {
                        bool isCreate = false;
                        isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, txt_Step_ID.Text, txt_Step_Name.Text, "APPROVE", "", txt_amd_by.Text, "", "", "", "", "", "", "", "", "", "", Dval, lbl_Compliance_Name.InnerText.ToString() + "-" + lbl_Grace_Date.InnerText.ToString(), txt_wiid.Text, ref isdata);
                        if (isCreate)
                        {
                            Close_div.Style["display"] = "none";
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + lbl_Compliance_Name.InnerText + "-" + lbl_Grace_Date.InnerText + "  Task Completed Successfully. ');}</script>");
                        }
                    }
                    else
                    {
                        Close_div.Style["display"] = "none";
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + lbl_Compliance_Name.InnerText + "-" + lbl_Grace_Date.InnerText + "  Saved');}</script>");
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
    protected void btn_Resubmit_Click(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                string isdata = string.Empty;
                string isSaved = string.Empty;
                string[] Dval = null;
                string inser_FileXML = txtXMLFiles.Text;
                inser_FileXML = inser_FileXML.Replace("&", "&amp;");
                inser_FileXML = inser_FileXML.Replace(">", "&gt;");
                inser_FileXML = inser_FileXML.Replace("<", "&lt;");
                inser_FileXML = inser_FileXML.Replace("||", ">");
                inser_FileXML = inser_FileXML.Replace("|", "<");
                inser_FileXML = inser_FileXML.Replace("'", "&apos;");
                txtXMLFiles.Text = inser_FileXML.ToString();
                if (txt_TaskType.Text != "Manualy")
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


                isSaved = (string)ActionController.ExecuteAction("", "LC_Compliance_Completion.aspx", "pamd", ref isdata, txtProcessID.Text, txtInstanceID.Text, "Resubmit", txt_Remarks.Text, txt_amd_by.Text, "1", txtXMLFiles.Text, txt_Step_Name.Text, txt_Request_No.Text,"Waiting for Compliance Completion");
                if (isSaved == "true")
                {

                    bool isCreate = false;
                    isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "114", txt_Step_Name.Text, "RESUBMIT", "", txt_amd_by.Text, "", "", "", "", "", "", "", "", "", "", Dval, lbl_Compliance_Name.InnerText.ToString() + "-" + lbl_Grace_Date.InnerText.ToString(), txt_wiid.Text, ref isdata);
                    if (isCreate)
                    {
                        Close_div.Style["display"] = "none";
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + lbl_Compliance_Name.InnerText + "-" + lbl_Grace_Date.InnerText + "  Saved and Sent to Legal Compliance Completion');}</script>");
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