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
using System.Web.Script.Services;
using System.Collections.Generic;

public partial class Advance_Request : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    StringBuilder str = new StringBuilder();
    ListItem Li = new ListItem("--Select One--", "0");
    DataTable dt = new DataTable();
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Request));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Session["USER_ADID"].ToString();
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["stepid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                    }
                    Initialization();
                    FillAdvanceDetails();
                    //FillAdvanceFor();
                    //FillCity();
                    
                    //FillMode();
                    //FillLocation();
                   // fillPolicy_Details();
                    fillDocument_Details();
                }
            }
        }
        catch (Exception Exc) { }
    }
    private void FillAdvanceDetails()
    {
     try
     {
        String IsData = string.Empty;
        string isValid = string.Empty;
	string Class= string.Empty;
        string DisplayData = string.Empty;
        DataSet ds = (DataSet)ActionController.ExecuteAction("", "Advance_Request.aspx", "advdetails", ref IsData, lbl_Grade.Text,txt_Username.Text);
        DataTable dta = ds.Tables[0];
        DataTable dtc = ds.Tables[1];
        DataTable dtm = ds.Tables[2];
        DataTable dtl = ds.Tables[3];
        DataTable dtamt = ds.Tables[4];
        DataTable dt = ds.Tables[5];
        if (dta != null && dta.Rows.Count > 0)
        {
            ddladvancetype.DataSource = dta;
            ddladvancetype.DataTextField = "ADVANCE_TYPE_NAME";
            ddladvancetype.DataValueField = "PK_ADVANCEID";
            ddladvancetype.DataBind();
            ddladvancetype.Items.Insert(0, Li);
        }
        if (dtc != null && dtc.Rows.Count > 0)
        {
            ddlFromPlace.DataSource = dtc;
            ddlFromPlace.DataTextField = "NAME";
            ddlFromPlace.DataValueField = "PK_CITY_ID";
            ddlFromPlace.DataBind();
            ddlFromPlace.Items.Insert(0, new ListItem("--Select One--", "0"));
            ddlFromPlace.Items.Insert((dtc.Rows.Count), new ListItem("Other", "-1"));
            ddlToPlace.DataSource = dtc;
            ddlToPlace.DataTextField = "NAME";
            ddlToPlace.DataValueField = "PK_CITY_ID";
            ddlToPlace.DataBind();
            ddlToPlace.Items.Insert(0, new ListItem("--Select One--", "0"));
            ddlToPlace.Items.Insert((dtc.Rows.Count), new ListItem("Other", "-1"));
        }
        if (dtm != null && dtm.Rows.Count > 0)
        {
            ddlPayMode.DataSource = dtm;
            ddlPayMode.DataTextField = "PAYMENT_MODE";
            ddlPayMode.DataValueField = "PK_PAYMENT_MODE";
            ddlPayMode.DataBind();
            ddlPayMode.Items.Insert(0, Li);
        }
        if (dtl != null && dtl.Rows.Count > 0)
        {
            ddlLocation.DataSource = dtl;
            ddlLocation.DataTextField = "LOCATION_NAME";
            ddlLocation.DataValueField = "PK_LOCATION_ID";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, Li);
        }

        if (dtamt != null)
        {
            if (dtamt != null && dtamt.Rows.Count > 0)
            {
                DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Grade</th><th>City Class</th><th>Amount(Rs)</th><th>Effective Date</th></tr></thead>";

                for (int i = 0; i < dtamt.Rows.Count; i++)
                {
                if (Convert.ToString(dtamt.Rows[i]["CITY_CLASS"])=="A")
		{
		  Class="Metro";
		}
		else if (Convert.ToString(dtamt.Rows[i]["CITY_CLASS"])=="B")
		{
		  Class="Min-Metro";
		}
		else if (Convert.ToString(dtamt.Rows[i]["CITY_CLASS"])=="C")
		{
		  Class="Other";
		}
                 DisplayData += "<tr><td>" + lbl_Grade.Text + "</td><td>" + Class + "</td><td>" + Convert.ToString(dtamt.Rows[i]["AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["EFFECTIVE_DATE"]) + "</td></tr>";

                }
                DisplayData += "</table>";
                txt_policycnt.Text = dtamt.Rows.Count.ToString();
            }
            else
            {
                txt_policycnt.Text = "0";
            }
            div_policy.InnerHtml = DisplayData;
            DisplayData = "";
        }


        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["PK_EXPENSE_ID"].ToString() == "1004")
                {
                    txt_adamount.Text = (dt.Rows[i]["VALUE"].ToString());
                }
                if (dt.Rows[i]["PK_EXPENSE_ID"].ToString() == "1006")
                {
                    if ((dt.Rows[i]["VALUE"]) != null || (dt.Rows[i]["VALUE"]) != "undefined")
                    {
                        txt_domeopen.Text = (dt.Rows[i]["VALUE"].ToString());
                    }
                }
                if (dt.Rows[i]["PK_EXPENSE_ID"].ToString() == "1005")
                {
                    txt_adperiod.Text = (dt.Rows[i]["VALUE"].ToString());
                }
                if (dt.Rows[i]["PK_EXPENSE_ID"].ToString() == "6")
                {
                    if ((dt.Rows[i]["VALUE"]) != null || (dt.Rows[i]["VALUE"]) != "undefined")
                    {
                        txt_otheropen.Text = (dt.Rows[i]["VALUE"].ToString());
                    }
                }
                if (dt.Rows[i]["PK_EXPENSE_ID"].ToString() == "5")
                {
                    txt_othperiod.Text = (dt.Rows[i]["VALUE"].ToString());
                }
            }
        }
    }
    catch (Exception ex)
    {
        FSL.Logging.Logger.WriteEventLog(false, ex);
    }
  }
    //private void FillAdvanceFor()
    //{
    //    String IsData = string.Empty;
    //    dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdAdvanceType");
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ddladvancetype.DataSource = dt;
    //        ddladvancetype.DataTextField = "ADVANCE_TYPE_NAME";
    //        ddladvancetype.DataValueField = "PK_ADVANCEID";
    //        ddladvancetype.DataBind();
    //        ddladvancetype.Items.Insert(0, Li);
    //    }
    //}
    
    //private void FillCity()
    //{
    //    String IsData = string.Empty;
    //    dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref IsData, "", "AdCity");
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ddlFromPlace.DataSource = dt;
    //        ddlFromPlace.DataTextField = "NAME";
    //        ddlFromPlace.DataValueField = "PK_CITY_ID";
    //        ddlFromPlace.DataBind();
    //        ddlFromPlace.Items.Insert(0, new ListItem("--Select One--", "0"));
    //        ddlFromPlace.Items.Insert((dt.Rows.Count), new ListItem("Other", "-1"));
    //        ddlToPlace.DataSource = dt;
    //        ddlToPlace.DataTextField = "NAME";
    //        ddlToPlace.DataValueField = "PK_CITY_ID";
    //        ddlToPlace.DataBind();
    //        ddlToPlace.Items.Insert(0, new ListItem("--Select One--", "0"));
    //        ddlToPlace.Items.Insert((dt.Rows.Count), new ListItem("Other", "-1"));
    //    }
    //}

    //private void FillMode()
    //{
    //    String IsData = string.Empty;
    //    dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdPaymentMode");
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ddlPayMode.DataSource = dt;
    //        ddlPayMode.DataTextField = "PAYMENT_MODE";
    //        ddlPayMode.DataValueField = "PK_PAYMENT_MODE";
    //        ddlPayMode.DataBind();
    //        ddlPayMode.Items.Insert(0, Li);
    //    }
    //}

    //private void FillLocation()
    //{
    //    String IsData = string.Empty;
    //    dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdLocation");
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ddlLocation.DataSource = dt;
    //        ddlLocation.DataTextField = "LOCATION_NAME";
    //        ddlLocation.DataValueField = "PK_LOCATION_ID";
    //        ddlLocation.DataBind();
    //        ddlLocation.Items.Insert(0, Li);
    //    }

    //}
    private void Initialization()
    {
        string IsData = string.Empty;
        string IsDatam = string.Empty;
        string IsData1 = string.Empty;
        string IsData2 = string.Empty;
        string IsData3 = string.Empty;
        DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "pgettraveluser", ref IsData, txt_Username.Text);
        if (dtUser != null && dtUser.Rows.Count > 0)
        {
            lbl_EmpCode.Text = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
            lbl_EmpName.Text = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
            lbl_Dept.Text = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
            lbl_Grade.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
            lbl_CostCenter.Text = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
            lbl_MobileNo.Text = dtUser.Rows[0]["MOBILE_NO"].ToString();
            lbl_desgnation.Text = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
            // lbl_GLCode.Text = Convert.ToString(dtUser.Rows[0]["GL_CODE"]);
           // lbl_division.Text = "NA";
 lbl_division.Text = Convert.ToString(dtUser.Rows[0]["DIVISION_NAME"]);
                spn_base_location.InnerHtml = Convert.ToString(dtUser.Rows[0]["BASE_LOCATION"]);

            if (Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]).Trim() != "")
            {
                span_Ifsc.InnerHtml = Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]);
            }
            if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]) != "")
            {
                lbl_bankAccNo.Text = Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]);
            }
            else
            {
                lbl_bankAccNo.Text = "NA";
            }
            DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "pgettravelrequestapprover", ref IsData1, txt_Username.Text);
            if (dtApprover != null)
            {
                if (dtApprover.Rows.Count > 0)
                {
                    if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                    {
                        lbl_AppName.Text = Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                        txt_approvar.Text = Convert.ToString(dtApprover.Rows[0]["approver"]);
                    }
                    else
                    {
                        lbl_AppName.Text = "NA";
                        txt_approvar.Text = "NA";
                    }
                    txt_Approver_Email.Text = Convert.ToString(dtApprover.Rows[0]["approver_email"]);
                    if (Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "0")
                    {
                        span_DApprover.InnerHtml = Convert.ToString(dtApprover.Rows[0]["doa_approver"]);
                        span_Dapp_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);
                    }
                    else
                    {
                        span_Dapp_name.InnerHtml = span_DApprover.InnerHtml = "NA";
                    }
                }

            }
        }

    }

    private void fillDocument_Details()
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                string isData = string.Empty;
                string isValid = string.Empty;
                string DisplayData = string.Empty;

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>Description</th><th>File Name</th><th>Delete</th></tr></thead>";
                DisplayData += "</table>";
                div_Doc.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }
    //private void fillPolicy_Details()
    //{
    //    if (ActionController.IsSessionExpired(Page))
    //        ActionController.RedirctToLogin(Page);
    //    else
    //    {
    //        try
    //        {
    //            string isData = string.Empty;
    //            string isValid = string.Empty;
    //            string DisplayData = string.Empty;

    //            DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref isValid, lbl_desgnation.Text, "AdDesignation");
    //            if (dtamt != null && dtamt.Rows.Count > 0)
    //            {
    //                DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Designation</th><th>City Class</th><th>Amount(Rs)</th><th>Effective Date</th></tr></thead>";

    //                for (int i = 0; i < dtamt.Rows.Count; i++)
    //                {
    //                    DisplayData += "<tr><td>" + lbl_desgnation.Text + "</td><td>" + Convert.ToString(dtamt.Rows[i]["CITY_CLASS"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt.Rows[i]["EFFECTIVE_DATE"]) + "</td></tr>";

    //                }
    //                DisplayData += "</table>";
    //                txt_policycnt.Text = dtamt.Rows.Count.ToString();
    //            }
    //            else
    //            {
    //                txt_policycnt.Text = dtamt.Rows.Count.ToString();
    //            }
    //            div_policy.InnerHtml = DisplayData;
    //            DisplayData = "";
    //        }
    //        catch (Exception ex)
    //        {
    //            FSL.Logging.Logger.WriteEventLog(false, ex);
    //        }
    //    }
    //}

    protected void btnRequest_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                //string ISValid1 = string.Empty;
                //string dtamt = (string)ActionController.ExecuteAction("", "Advance_Request.aspx", "checkduplicate", ref ISValid1, txt_fplace.Value, txt_tplace.Value, txt_fdate.Value, txt_tdate.Value, txt_Username.Text);
                //if (dtamt == "true")
                //{
                //    string message = "alert('You have already Submitted Bill For This Period')";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //}
                //else
                //{
		
                string confirmValue = txt_confirm.Text;
                if (confirmValue == "Yes" || confirmValue == "")
                {
                    string refData = string.Empty;
                    string isInserted = string.Empty;
                    string ISValid = string.Empty;
                    txt_Condition.Text = "1";
                    txt_Action.Text = "Submit";
                    txt_Audit.Text = "ADVANCE REQUEST";
                    string isSaved = string.Empty;

                    string vehiclexml_string = txt_xml_data_vehicle.Text;
                    vehiclexml_string = vehiclexml_string.Replace("&", "&amp;");
                    vehiclexml_string = vehiclexml_string.Replace(">", "&gt;");
                    vehiclexml_string = vehiclexml_string.Replace("<", "&lt;");
                    vehiclexml_string = vehiclexml_string.Replace("||", ">");
                    vehiclexml_string = vehiclexml_string.Replace("|", "<");
                    vehiclexml_string = vehiclexml_string.Replace("'", "&apos;");
                    txt_xml_data_vehicle.Text = vehiclexml_string.ToString();

                    string inserXML = txt_Document_Xml.Text;
                    inserXML = inserXML.Replace("&", "&amp;");
                    txt_Document_Xml.Text = inserXML.ToString();

                    string instanceID = (string)WFE.Action.StartCase(isInserted, txtProcessID.Text, txt_Username.Text, txt_Username.Text, txtEmailID.Text, "152");
                    txtInstanceID.Text = instanceID;
                    if (ddlPayMode.SelectedValue == "2")
                    {
                        ddlLocation.SelectedValue = "0";
                    }
                    isSaved = (string)ActionController.ExecuteAction("", "Advance_Request.aspx", "insert", ref refData, Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), ddlPayMode.SelectedValue, txt_xml_data_vehicle.Text, txt_Username.Text, txt_remark.Value, txt_Audit.Text, txt_Action.Text, txt_approvar.Text, txt_Document_Xml.Text, ddlLocation.SelectedValue, txt_pkexpenseid.Text);
                    if (isSaved == null || refData.Length > 0 || isSaved == "false")
                    {
                        //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                        string[] errmsg = refData.Split(':');               
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + errmsg[1].ToString() + "')", true);
                    }
                    else
                    {

                        string[] Request_Unique = isSaved.Split('=');
                        txt_Request.Text = Request_Unique[0];
                        ////upload file code///////////////////
                        string isValid1 = string.Empty;
                        string[] arr = new string[100];
                        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "getrequest_document", ref isValid1, "ADVANCE", Request_Unique[0]);
                        if (dt.Rows.Count > 0)
                        {
                            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString().Trim();
                            string path = string.Empty;
                            string foldername = Request_Unique[0];
                            foldername = foldername.Replace("/", "_");
                            path = activeDir + "\\" + "ADVANCE\\" + foldername;
                            if (Directory.Exists(path))
                            {

                            }
                            else
                            {
                                Directory.CreateDirectory(path);
                                string[] directories = Directory.GetFiles(activeDir + "\\" + "ADVANCE\\");
                                path = path + "\\";
                                foreach (var directory in directories)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        var sections = directory.Split('\\');
                                        var fileName = sections[sections.Length - 1];
                                        if (dt.Rows[i]["filename"].ToString().Trim() == fileName)
                                        {
                                            System.IO.File.Move(activeDir + "\\" + "ADVANCE\\" + fileName, path + fileName);
                                            if (System.IO.File.Exists(activeDir + "\\" + "ADVANCE\\" + fileName))
                                            {
                                                System.IO.File.Delete(activeDir + "\\" + "ADVANCE\\" + fileName);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        ///////////////////////////////////////
                        if (lbl_EmpCode.Text == "4263" || lbl_EmpCode.Text == "4262")
                        {
                            string msg = "";
                            string ref_data = string.Empty;
                            string emailid = string.Empty;
                            CryptoGraphy crypt = new CryptoGraphy();
                            string process_name = crypt.Encryptdata("ADVANCE EXPENSE");
                            string req_no = crypt.Encryptdata(txt_Request.Text);
                            DataTable DTAP = new DataTable();

                            if (ddlPayMode.SelectedItem.Text.ToUpper() == "CASH")
                            {
                                DTAP = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getaccapprover", ref ref_data, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", ddlLocation.Text, ddlPayMode.Text);
                            }
                            else
                            {
                                DTAP = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getaccapprover", ref ref_data, "ADVANCE REQUEST ACCOUNT PAYABLE APPROVAL", 0, ddlPayMode.Text);
                            }
                            //if (DTAP != null)
                            //{
                                if (DTAP.Rows.Count > 0 && DTAP != null)
                                {
                                    txt_Condition.Text = "3";
                                    string isSaved1 = (string)ActionController.ExecuteAction("", "Advance_Request.aspx", "inserttwi", ref ref_data, txtProcessID.Text, txtInstanceID.Text, txt_Username.Text, txt_Request.Text, "5");
                                    if (isSaved1 == null || ref_data.Length > 0 || isSaved1 == "false")
                                    {
                                        string[] errmsg = refData.Split(':');
                                        //  Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");                  
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + errmsg[1].ToString() + "')", true);
                                    }
                                    else
                                    {
                                        string[] Dval = new string[DTAP.Rows.Count];
                                        Dval[0] = "";
                                        if (DTAP.Rows.Count > 0)
                                        {
                                            for (int i = 0; i < DTAP.Rows.Count; i++)
                                            {
                                                Dval[i] = Convert.ToString(DTAP.Rows[i]["USER_ADID"]);
                                                if (txtApproverEmail.Text == "")
                                                {
                                                    txtApproverEmail.Text = Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                                                }
                                                else
                                                {
                                                    txtApproverEmail.Text = txtApproverEmail.Text + ',' + Convert.ToString(DTAP.Rows[i]["EMAIL_ID"]);
                                                }
                                            }
                                        }
                                        bool isCreate1 = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "158", "ADVANCE REQUEST APPROVAL", "APPROVE", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, "0", ref isInserted);
                                        if (isCreate1)
                                        {
                                            try
                                            {
                                                
                                                    if (ddlPayMode.SelectedItem.Text.ToUpper() == "CASH")
                                                    {
                                                        msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been sent to Accounts for payment approval.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text + "</font></pre></p><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTERNET URL:<a href='https://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>https://" + compname + "/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";
                                                        emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE APPROVAL", "SUBMIT", txtEmailID.Text, txtApproverEmail.Text, msg, "Request No: " + txt_Request.Text);
                                                    }
                                                    else
                                                    {
                                                        msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been sent to Accounts for payment approval.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text + "</font></pre></p><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTERNET URL:<a href='https://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "'>https://" + compname + "/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";
                                                        emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE APPROVAL", "SUBMIT", txtEmailID.Text, txtApproverEmail.Text, msg, "Request No: " + txt_Request.Text);

                                                    }
                                                
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                string msg2 = string.Empty;
                                                msg2 = "alert('Advance Request has been sent to account payment approval and Request No. is : " + txt_Request.Text + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                              //  Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Advance Request has been sent to account payment approval and Request No. is : " + txt_Request.Text + " ...!');window.open('../../portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                            }
                                        }
                                    }//
                                }
                                else
                                {
                                    string msg2 = string.Empty;
                                    msg2 = "alert('Account Payment Approver Not Available For " + ddlPayMode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                    //Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Approver Not Available For " + ddlPayMode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                }
                           // }
                            //else
                            //{
                            //    string msg2 = string.Empty;
                            //    msg2 = "alert('Account Payment Approver Not Available For " + ddlPayMode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                            //  //  Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Account Approver Not Available For " + ddlPayMode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                            //}
                        }
                        else
                        {
                            string[] Dval1 = new string[1];
                            Dval1[0] = txt_approvar.Text;
                            if (txt_Approver_Email.Text == "")
                            {
                                txt_Approver_Email.Text = txtEmailID.Text;
                            }
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "157", "ADVANCE REQUEST", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, txt_Request.Text, "0", ref isInserted);

                            if (isCreate)
                            {
                                try
                                {
                                    string emailid = string.Empty;
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Advance Request has been sent for your approval.</font></pre><p/>  <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "ADVANCE REQUEST", "SUBMIT", txt_Approver_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);

                                }
                                catch (Exception ex)
                                {
                                    // throw;
                                    FSL.Logging.Logger.WriteEventLog(false, ex);
                                }
                                finally
                                {
                                    if (txt_deviate.Text == "1")
                                    {
                                        string msg2 = string.Empty;
                                        msg2 = "alert('Advance Request has been sent for approval(under deviation) and Request No. is : " + txt_Request.Text + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                       // Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Advance Request has been sent for your approval(under deviation) and Request No. is : " + txt_Request.Text + " ...!');window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                    }
                                    else
                                    {
                                        string msg2 = string.Empty;
                                        msg2 = "alert('Advance Request has been sent for approval and Request No. is : " + txt_Request.Text + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                       // Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Advance Request has been sent for your approval and Request No. is : " + txt_Request.Text + " ...!');window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");

                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                   
                }

                divIns.Style.Add("display", "none");
            }


        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

  
    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            string msg = "window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
        }
    }

   
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;
            path = activeDir + "\\" + "ADVANCE\\";
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


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string changetype(string advtype, string user, int openadv)
    {
        string IsData = string.Empty;
        string strall = string.Empty;
        string strexid = string.Empty;
        string totalamount = string.Empty;
        string opencount = string.Empty;
        string domcount = string.Empty;
        string days = string.Empty;
        totalamount = "0";
        strexid = "0";
        days = "0";
        opencount = "0";
        DataSet ds = (DataSet)ActionController.ExecuteAction("", "Advance_Request.aspx", "advpoldetails", ref IsData, advtype, user);
        DataTable dteh = ds.Tables[0];
        DataTable dt = ds.Tables[1];
        DataTable dtd = ds.Tables[2];

        
        if (dteh != null && dteh.Rows.Count > 0)
        {
            strexid = dteh.Rows[0]["PK_EXPENSE_HEAD_ID"].ToString();
        }
        if (dt != null && dt.Rows.Count > 0)
        {
            totalamount = dt.Rows[0]["TOTAL_AMOUNT"].ToString();
            opencount = dt.Rows[0]["CNT"].ToString();
        }
        if (dtd != null && dtd.Rows.Count > 0)
        {
            days = dtd.Rows[0]["DAYS"].ToString();
        }
        

        ////showall(advtype);
        //string ISValid = string.Empty;
        //string strexid = string.Empty;
        //string strall = string.Empty;
        //string stradamount = string.Empty;
        //string strexpire = string.Empty;
        //string stradperiod = string.Empty;
        //string total_amount = string.Empty;
        //string opcount = string.Empty;
        //string opencount = string.Empty;
        //int openadv = 0;
        //DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref ISValid, advtype, "AdGLcode");
        //strexid = dtamt.Rows[0]["PK_EXPENSE_HEAD_ID"].ToString();
        //string Isdata1 = string.Empty;
        //DataTable dt = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "selectdetails", ref Isdata1, user, "AdExParameter");
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (advtype == "1")
        //        {
        //            if (dt.Rows[i]["PARAMETER"].ToString() == "Domestic Other Advance Amount")
        //            {
        //                stradamount = (dt.Rows[i]["VALUE"].ToString());
        //            }
        //            if (dt.Rows[i]["PARAMETER"].ToString() == "Domestic Open Adavances")
        //            {
        //                if ((dt.Rows[i]["VALUE"]) != null || (dt.Rows[i]["VALUE"]) != "undefined")
        //                {
        //                    openadv = Convert.ToInt32(dt.Rows[i]["VALUE"]);
        //                }
        //            }
        //            if (dt.Rows[i]["PARAMETER"].ToString() == "Domestic Advance Period")
        //            {
        //                stradperiod = (dt.Rows[i]["VALUE"].ToString());
        //            }
        //        }
        //        if (advtype == "3")
        //        {
        //            if (dt.Rows[i]["PARAMETER"].ToString() == "Domestic Other Advance Amount")
        //            {
        //                stradamount = (dt.Rows[i]["VALUE"].ToString());
        //            }
        //            if (dt.Rows[i]["PARAMETER"].ToString() == "Other Open Adavances")
        //            {
        //                if ((dt.Rows[i]["VALUE"]) != null || (dt.Rows[i]["VALUE"]) != "undefined")
        //                {
        //                    openadv = Convert.ToInt32(dt.Rows[i]["VALUE"]);
        //                }
        //            }
        //            if (dt.Rows[i]["PARAMETER"].ToString() == "Other Advance Period")
        //            {
        //                stradperiod = (dt.Rows[i]["VALUE"].ToString());
        //            }
        //        }
        //    }
        //}
        //string IsData6 = string.Empty;
        //int dts = 0; Double amttotal = 0;
        //DataTable dtid = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "getadvanids", ref IsData6, user, advtype);
        //if (dtid != null && dtid.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dtid.Rows.Count; i++)
        //    {
        //        string dtvalue = (string)ActionController.ExecuteAction("", "Advance_Request.aspx", "getperiod", ref IsData6, user, dtid.Rows[i]["PK_ADVANCE_HDR_Id"].ToString(), Convert.ToInt64(stradperiod));
        //        if (dtvalue != null)
        //        {
        //            string[] result = dtvalue.Split('=');
        //            if (result[0] != null)
        //            {
        //                dts = Convert.ToInt32(dts) + Convert.ToInt32(result[0]);
        //            }
        //            if (result[1] != null)
        //            {
        //                if (result[1] == "true")
        //                {
        //                    strexpire = "1";
        //                }
        //                else
        //                {
        //                    strexpire = "0";
        //                }
        //            }

        //        }
        //    }
        //}
        //DataTable dtid1 = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "getadvanids", ref IsData6, user, "");
        //if (dtid1 != null && dtid1.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dtid1.Rows.Count; i++)
        //    {
        //        string dtvalue1 = (string)ActionController.ExecuteAction("", "Advance_Request.aspx", "getperiod", ref IsData6, user, dtid1.Rows[i]["PK_ADVANCE_HDR_Id"].ToString(), Convert.ToInt64(stradperiod));
        //        if (dtvalue1 != null)
        //        {
        //            string[] result1 = dtvalue1.Split('=');
        //            if (result1[2] != null)
        //            {
        //                amttotal = Convert.ToDouble(amttotal) + Convert.ToDouble(result1[2]);
        //                total_amount = Convert.ToString(amttotal);

        //            }
        //        }
        //    }
        //}
        //if (dts != 0)
        //{
        //    if ((dts) >= (openadv - 1))
        //    {
        //        opcount = "1";
        //    }
        //}
        //if (dts >= openadv)
        //{
        //    opencount = "1";
        //}
        //else
        //{
        //    opencount = "0";
        //}
        strall = strexid + "#" + totalamount + "#" + opencount + "#" + days;
        return strall;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string showall(string advtype, string user)
    {
        DataTable DTS;
        string str1 = string.Empty;
        string IsValid = string.Empty;
        DTS = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "getappdetails", ref IsValid, user, advtype);
        DTS.Columns[0].ColumnName = "Sr.No";


        if (DTS.Rows.Count > 0)
        {

            for (int i = 0; i < DTS.Rows.Count; i++)
            {
                DTS.Rows[i]["Sr.No"] = i + 1;
            }

            StringBuilder str = new StringBuilder();
            if (advtype == "1")
            {
                str.Append("<table id='data-table1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th>#</th><th>Place From</th><th>Place To</th><th>From Date</th><th>To Date</th><th>Amount</th><th>Approved By</th><th>Approval Date</th> </tr></thead>");
            }
            else
            {
                str.Append("<table id='data-table1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th>#</th><th>Advance Date</th><th>Amount</th><th>Approved By</th><th>Approval Date</th> </tr></thead>");

            }
            str.Append("<tbody>");

            for (int i = 0; i < DTS.Rows.Count; i++)
            {
                str.Append(" <tr>");
                str.Append("<td align='left'>" + (i + 1) + "</td>");
                if (advtype == "1")
                {
                    if (Convert.ToString(DTS.Rows[i]["from_city"]) == "0" || Convert.ToString(DTS.Rows[i]["from_city"]) == "-1" || Convert.ToString(DTS.Rows[i]["from_city"]) == "")
                    {
                        str.Append("<td align='left'>" + Convert.ToString(DTS.Rows[i]["other_f_city"]) + "</td>");
                    }
                    else
                    {
                        str.Append("<td align='left'>" + Convert.ToString(DTS.Rows[i]["fname"]) + "</td>");
                    }

                    if (Convert.ToString(DTS.Rows[i]["to_city"]) == "0" || Convert.ToString(DTS.Rows[i]["to_city"]) == "-1" || Convert.ToString(DTS.Rows[i]["to_city"]) == "")
                    {
                        str.Append("<td align='left'>" + Convert.ToString(DTS.Rows[i]["other_t_city"]) + "</td>");
                    }
                    else
                    {
                        str.Append("<td align='left'>" + Convert.ToString(DTS.Rows[i]["tname"]) + "</td>");
                    }
                    str.Append("<td align='left'>" + DTS.Rows[i]["FROM_DATE"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["TO_DATE"].ToString() + "</td>");
                }
                else
                {
                    str.Append("<td align='left'>" + DTS.Rows[i]["ADVANCE_DATE"].ToString() + "</td>");
                }
                str.Append("<td align='right'>" + DTS.Rows[i]["AMOUNT"].ToString() + "</td>");
                str.Append("<td align='left'>" + DTS.Rows[i]["APPROVED_BY"].ToString() + "</td>");
                str.Append("<td align='left'>" + DTS.Rows[i]["APPROVED_ON"].ToString() + "</td>");
                str.Append("</tr>");
            }
            str.Append("   </tbody>   </table> ");
            str1 = str.ToString();
        }
        else
        {

        }
        return str1;


    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string fillAmountall(string grade, string tocity)
    {
        ////get amount from grade//////////////////////////////
        string IsData1 = string.Empty;
        string str = string.Empty;
    

        DataTable dta = (DataTable)ActionController.ExecuteAction("", "Advance_Request.aspx", "getallamount", ref IsData1, grade, tocity,"");
        if (dta != null && dta.Rows.Count > 0)
        {
            str = dta.Rows[0]["AMOUNT"].ToString();
        }

        return str;

    }
}

