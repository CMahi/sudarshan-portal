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
using System.Xml;
using System.Text;
using System.Runtime.InteropServices;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;

public partial class Local_Conveyance : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Local_Conveyance));
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
                    FillMode();
                    FillLocation();
                    fillDocument_Details();
                    FillVehicleType();
                    fillPolicy_Details();
                    fillAdvanceAmount();
                }
            }
        }
        catch (Exception Exc) { }
    }
    private void FillVehicleType()
    {
        ddlVehicleType1.Items.Insert(0, new ListItem("--Select One--", "0"));
        ddlVehicleType1.Items.Insert(1, new ListItem("Two Wheeler", "Two Wheeler"));
        ddlVehicleType1.Items.Insert(2, new ListItem("Four Wheeler", "Four Wheeler"));
        ddlVehicleType1.Items.Insert(3, new ListItem("Other", "Other"));

        StringBuilder str1 = new StringBuilder();
        str1.Append("<option value='0'>--Select One--</option>");
        str1.Append("<option value='Two Wheeler'>Two Wheeler</option>");
        str1.Append("<option value='Four Wheeler'>Four Wheeler</option>");
        str1.Append("<option value='Other'>Other</option>");
        txt_vehitype.Text = str1.ToString();
    }

    private void Initialization()
    {
        string IsData = string.Empty;
        string IsDatam = string.Empty;
        string IsData1 = string.Empty;
        string IsData2 = string.Empty;
        string IsData3 = string.Empty;
        DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "pgettraveluser", ref IsData, txt_Username.Text);
        if (dtUser != null && dtUser.Rows.Count > 0)
        {
            lbl_EmpCode.Text = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
            lbl_EmpName.Text = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
            lbl_Dept.Text = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
            lbl_Grade.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
            lbl_CostCenter.Text = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
            //lbl_date.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            lbl_desgnation.Text = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
            lbl_division.Text = "NA";
            lbl_MobileNo.Text = dtUser.Rows[0]["MOBILE_NO"].ToString();
            if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]) != "")
            {
                lbl_bankAccNo.Text = Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]);
            }
            else
            {
                lbl_bankAccNo.Text = "NA";
            }
            if (Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]).Trim() != "")
            {
                span_Ifsc.InnerHtml = Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]);
            }
            DataTable dtcode = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "selectdetails", ref IsData1, "Local Conveyance", "AdGLCode");
            if (dtcode != null && dtcode.Rows.Count > 0)
            {
                lbl_ExpenseHead.Text = dtcode.Rows[0]["EXPENSE_HEAD"].ToString();
                txt_pkexpenseid.Text = dtcode.Rows[0]["PK_EXPENSE_HEAD_ID"].ToString();
            }
            DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "pgettravelrequestapprover", ref IsData1, txt_Username.Text);
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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string fillAmount(string fromdate, string todate, int index)
    {
        string ISValid = string.Empty;
        string str = string.Empty;
        DataSet dtamt = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "getexpense", ref ISValid, fromdate, todate);
        if (dtamt != null && dtamt.Tables[1].Rows.Count > 0)
        {
            for (int i = 0; i < dtamt.Tables[1].Rows.Count; i++)
            {
                if (Convert.ToString(dtamt.Tables[1].Rows[i]["Vehicle_Type"]) == "Two Wheeler")
                {
                    str += dtamt.Tables[1].Rows[i]["Rate_Per_KM"].ToString() + "/Two/";
                }
                else if (dtamt.Tables[1].Rows[i]["Vehicle_Type"].ToString() == "Four Wheeler")
                {
                    str += dtamt.Tables[1].Rows[i]["Rate_Per_KM"].ToString() + "/Four/"; ;
                }
            }
        }
        str = index + "/" + str;
        return str;
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
    private void fillPolicy_Details()
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

                DataSet dtamt = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "getexpense", ref isValid, "");
                if (dtamt != null && dtamt.Tables[0].Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Type of Vehicle</th><th>Present rate per KM (Rs)</th><th>Effective From</th><th>Effective To</th></tr></thead>";

                    for (int i = 0; i < dtamt.Tables[0].Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dtamt.Tables[0].Rows[i]["Vehicle_Type"]) + "</td><td>" + Convert.ToString(dtamt.Tables[0].Rows[i]["Rate_Per_KM"]) + "</td><td>" + Convert.ToString(dtamt.Tables[0].Rows[i]["from_Date"]) + "</td><td>" + Convert.ToString(dtamt.Tables[0].Rows[i]["to_Date"]) + "</td></tr>";

                    }
                    DisplayData += "</table>";
                }

                div_policy.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }
    private void FillMode()
    {
        String IsData = string.Empty;
        dt = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdPaymentMode");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlPayMode.DataSource = dt;
            ddlPayMode.DataTextField = "PAYMENT_MODE";
            ddlPayMode.DataValueField = "PK_PAYMENT_MODE";
            ddlPayMode.DataBind();
            ddlPayMode.Items.Insert(0, Li);
        }

    }
    private void fillAdvanceAmount()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "pgetadvancedetails", ref isValid, txt_Username.Text, "", 1);
            txt_adcount.Text = Convert.ToString(dt.Tables[0].Rows.Count);
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Amount</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
                {
                    tblHTML.Append("<tr>");
                    if (Index == 0)
                    {
                        tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='local'><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["PK_ADVANCE_HDR_Id"]) + "' style='display:none'><input type='text' id='advance_amount" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["AMOUNT"]) + "' style='display:none'></td>");
                    }
                    else
                    {
                        tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='local' ><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["PK_ADVANCE_HDR_Id"]) + "' style='display:none'><input type='text' id='advance_amount" + (Index + 1) + "' value='" + Convert.ToString(dt.Tables[0].Rows[Index]["AMOUNT"]) + "' style='display:none'></td>");
                    }
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["ADVANCE_DATE"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["AMOUNT"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["APPROVED"]) + "</td>");
                    tblHTML.Append("</tr>");
                }
            }
            else
            {
                tblHTML.Append("<tr><td colspan='5' align='center'>Advance Not Available</td></tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            div_Advance.InnerHtml = tblHTML.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    private void FillLocation()
    {
        String IsData = string.Empty;
        dt = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdLocation");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlLocation.DataSource = dt;
            ddlLocation.DataTextField = "LOCATION_NAME";
            ddlLocation.DataValueField = "PK_LOCATION_ID";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, Li);
        }

    }
    protected void btnRequest_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
divIns.Style.Add("display","none");
                string ISValid1 = string.Empty;
                string ISValid2 = string.Empty;
                string success = string.Empty;
                success = "FALSE";
                string dtamt = string.Empty; string ss = string.Empty; string ss1 = string.Empty;
                //string vtype2 = string.Empty; string vfrom2 = string.Empty; string vto3 = string.Empty; string vdate3 = string.Empty;
                //string vtype3 = string.Empty; string vfrom3 = string.Empty; string vto2 = string.Empty; string vdate2 = string.Empty;
                string vehiclexml_string = txt_xml_data_vehicle.Text;
                vehiclexml_string = vehiclexml_string.Replace("&", "&amp;");
                vehiclexml_string = vehiclexml_string.Replace(">", "&gt;");
                vehiclexml_string = vehiclexml_string.Replace("<", "&lt;");
                vehiclexml_string = vehiclexml_string.Replace("||", ">");
                vehiclexml_string = vehiclexml_string.Replace("|", "<");
                vehiclexml_string = vehiclexml_string.Replace("[", "&lt;");
                vehiclexml_string = vehiclexml_string.Replace("]", "&lt;");
                vehiclexml_string = vehiclexml_string.Replace("'", "&apos;");
                txt_xml_data_vehicle.Text = vehiclexml_string.ToString();
                //int days1 = 0;
                //DataTable days = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref ISValid2, "Local Conveyance", "AdDays");
                //if (days != null && days.Rows.Count > 0)
                //{
                //    days1 = Convert.ToInt16(days.Rows[0]["Days"].ToString());
                //}
                string vtype = string.Empty;
                string vto = string.Empty;
                string vfrom = string.Empty;
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(txt_xml_data_vehicle.Text);
                XmlNodeList xnList = xml.SelectNodes("/ROWSET/ROW");
                for (int k = 0; k < xnList.Count; k++)
                {
                    //    vtype2 = vfrom2 = vto2 = vdate2= "";     
                    vtype = xnList[k].ChildNodes.Item(1).InnerText.Trim();
                    vfrom = xnList[k].ChildNodes.Item(5).InnerText.Trim();
                    vto = xnList[k].ChildNodes.Item(6).InnerText.Trim();

                    //    string vdate = xnList[k].ChildNodes.Item(5).InnerText.Trim(); 
                    //    if (k < xnList.Count - 1)
                    //    {
                    //        vtype3 = vfrom3 = vto3 = vdate3 ="";
                    //        vtype2 = xnList[k + 1].ChildNodes.Item(1).InnerText.Trim();
                    //        vfrom2 = xnList[k + 1].ChildNodes.Item(2).InnerText.Trim();
                    //        vto2 = xnList[k + 1].ChildNodes.Item(3).InnerText.Trim();
                    //        vdate2 = xnList[k + 1].ChildNodes.Item(5).InnerText.Trim();
                    //        if ((k + 1) < xnList.Count - 1)
                    //        {
                    //            vtype3 = xnList[k + 2].ChildNodes.Item(1).InnerText.Trim();
                    //            vfrom3 = xnList[k + 2].ChildNodes.Item(2).InnerText.Trim();
                    //            vto3 = xnList[k + 2].ChildNodes.Item(3).InnerText.Trim();
                    //            vdate3 = xnList[k + 2].ChildNodes.Item(5).InnerText.Trim();
                    //        }
                    //    }
                    //System.DateTime firstDate = DateTime.Today;
                    //System.DateTime secondDate = Convert.ToDateTime(vdate);
                    //System.TimeSpan diff = secondDate.Subtract(firstDate);
                    //System.TimeSpan diff1 = secondDate - firstDate;
                    //int diff2 = Convert.ToInt16((secondDate - firstDate).TotalDays.ToString());
                    //if (days1 < diff2)
                    //{
                    //    ss += vbillno + " / ";
                    //    success = "DIFFDAYS";

                    //}

                    //if ((vtype == vtype2 && vfrom == vfrom2 && vto == vto2 && vdate==vdate2) || (vtype3 == vtype2 && vfrom2 == vfrom3 && vto2 == vto3 && vdate2==vdate3) || (vtype == vtype3 && vfrom == vfrom3 && vto == vto3 && vdate==vdate3))
                    //{
                    //    if ((vtype2 != "" || vtype3 != "") && (vfrom2 != "" || vfrom3 != "") && (vto2 != "" || vto3 != "") && (vdate2 != "" || vdate3 != ""))
                    //    {
                    //        success = "SAME";
                    //    }
                    //}
                    //else
                    //{
                    dtamt = (string)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "checkduplicate", ref ISValid1, vto, vtype, vfrom, txt_Username.Text);
                    if (dtamt != "false")
                    {
                        ss1 += dtamt + " / ";
                        success = "TRUE";
                    }
                    //}

                }
                //if (success == "DIFFDAYS")
                //{
                //    string message = "alert('You do not have permission to submit the bill for :" + ss + "  bill no..!')";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //}
                if (success == "TRUE")
                {
                    string message = "alert('You have Already Submitted Bill For FromDate or TODate')";
                    //string message = "alert('You have Already Submitted Bill For This Period :" + ss1 + "')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                //if (success == "SAME")
                //{
                //    string message = "alert('You have entered same local coneyance for the same day!')";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //}  
                if (success == "FALSE")
                {
                    if (txt_approvar.Text != "NA")
                    {
                        string refData = string.Empty;
                        string isInserted = string.Empty;
                        string ISValid = string.Empty;
                        string isSaved = string.Empty;
                        txt_Condition.Text = "1";
                        txt_Action.Text = "Submit";
                        bool isCreate = false;

                        string inserXML = txt_Document_Xml.Text;
                        inserXML = inserXML.Replace("&", "&amp;");
                        txt_Document_Xml.Text = inserXML.ToString();
                        txt_Audit.Text = "LOCAL CONVEYANCE";
                        string instanceID = (string)WFE.Action.StartCase(isInserted, txtProcessID.Text, txt_Username.Text, txt_Username.Text, txtEmailID.Text, "128");
                        txtInstanceID.Text = instanceID;

                        if (ddlPayMode.SelectedValue == "2")
                        {
                            ddlLocation.SelectedValue = "0";
                        }
                        isSaved = (string)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "insert", ref refData, Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), ddlPayMode.SelectedValue, txt_xml_data_vehicle.Text, txt_Username.Text, txt_remark.Value, txt_Audit.Text, txt_Action.Text, txt_approvar.Text, txt_Document_Xml.Text, ddlLocation.SelectedValue, txt_advance_id.Text, txt_pkexpenseid.Text);
                        if (isSaved == null || refData.Length > 0 || isSaved == "false")
                        {
                            string[] errmsg = refData.Split(':');
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + errmsg[1].ToString() + "')", true);

                        }
                        else
                        {
                            string[] Request_Unique = isSaved.Split('=');
                            txt_Request.Text = Request_Unique[0];
                            string isValid1 = string.Empty;
                            string[] arr = new string[100];
                            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "getrequest_document", ref isValid1, "LOCAL CONVEYANCE", Request_Unique[0]);
                            if (dt.Rows.Count > 0)
                            {
                                string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString().Trim();
                                string path = string.Empty;
                                string foldername = Request_Unique[0];
                                foldername = foldername.Replace("/", "_");
                                path=activeDir + "\\" + "LOCAL CONVEYANCE\\" + foldername;
                                if (Directory.Exists(path))
                                {

                                }
                                else
                                {
                                    Directory.CreateDirectory(path);
                                    //string[] directories = Directory.GetDirectories(activeDir);
                                    string[] directories = Directory.GetFiles(activeDir + "\\" + "LOCAL CONVEYANCE\\");
                                    path = path + "\\";
                                    foreach (var directory in directories)
                                    {
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            var sections = directory.Split('\\');
                                            var fileName = sections[sections.Length - 1];
                                            if (dt.Rows[i]["filename"].ToString().Trim() == fileName)
                                            {
                                                System.IO.File.Move(activeDir + "\\" + "LOCAL CONVEYANCE\\" + fileName, path + fileName);
                                                if (System.IO.File.Exists(activeDir + "\\" + "LOCAL CONVEYANCE\\" + fileName))
                                                {
                                                    System.IO.File.Delete(activeDir + "\\" + "LOCAL CONVEYANCE\\" + fileName);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (lbl_EmpCode.Text == "10000" || lbl_EmpCode.Text == "10001")
                            {                            
                                string msg = "";
                                string ref_data = string.Empty;
                                string emailid = string.Empty;
                                CryptoGraphy crypt = new CryptoGraphy();
                                string process_name = crypt.Encryptdata("LOCAL CONVEYANCE");
                                string req_no = crypt.Encryptdata(txt_Request.Text);
                                DataTable DTAP = new DataTable();
                       
                                if (ddlPayMode.SelectedItem.Text.ToUpper() == "CASH")
                                {
                                    DTAP = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "getaccapprover", ref ref_data, "LOCAL CONVEYANCE ACCOUNT PAYMENT APPROVAL", ddlLocation.SelectedValue, ddlPayMode.SelectedValue);
                                }
                                else
                                {
                                    DTAP = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "getaccapprover", ref ref_data, "LOCAL CONVEYANCE ACCOUNT PAYMENT APPROVAL", 0, ddlPayMode.SelectedValue);
                                }
                                if (DTAP != null)
                                {
                                    if (DTAP.Rows.Count > 0)
                                    {
                                        txt_Condition.Text = "3";
                                        string isSaved1 = (string)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "inserttwi", ref ref_data, txtProcessID.Text, txtInstanceID.Text, txt_Username.Text, txt_Request.Text, "2");
                                        if (isSaved1 == null || ref_data.Length > 0 || isSaved1 == "false")
                                        {
                                            string[] errmsg = ref_data.Split(':');
                                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
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
                                            bool isCreate1 = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "130", "LOCAL CONVEYANCE APPROVAL", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, "0", ref isInserted);
                                            if (isCreate1)
                                            {
                                                try
                                                {
                                                    if ((txt_Approver_Email.Text != "") && (txtEmailID.Text != ""))
                                                    {
                                                        if (ddlPayMode.SelectedItem.Text.ToUpper() == "CASH")
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Local Conveyance Request has been sent to Accounts for payment approval..</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + txt_Request.Text.Trim() + "</font></pre></p><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE APPROVAL", "SUBMIT", txt_Approver_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                                        }
                                                        else
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Local Conveyance Request has been sent to Accounts for payment approval..</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + txt_Request.Text.Trim() + "</font></pre></p><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE APPROVAL", "SUBMIT", txt_Approver_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);

                                                        }
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    throw;
                                                }
                                                finally
                                                {
                                                    string msg2 = string.Empty;
                                                    msg2 = "alert('Local Conveyance has been sent for account payment approval and Request No. is : " + txt_Request.Text + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);

                                                }
                                            }
                                        }//
                                    }
                                    else
                                    {
                                        string msg2 = string.Empty;
                                        msg2 = "alert('Account Payment Approver Not Available For " + ddlPayMode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                    }
                                }
                                else
                                {
                                    string msg2 = string.Empty;
                                    msg2 = "alert('Account Payment Approver Not Available For " + ddlPayMode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                }
                            }
                            else
                            {
                                string[] Dval1 = new string[1];
                                Dval1[0] = txt_approvar.Text;

                                if (txt_Approver_Email.Text == "")
                                {
                                    txt_Approver_Email.Text = txtEmailID.Text;
                                }
                                isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "129", "LOCAL CONVEYANCE REQUEST", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, txt_Request.Text, "0", ref isInserted);
                                if (isCreate)
                                {
                                    try
                                    {
                                        if (((txt_Approver_Email.Text != "NA") || (txt_Approver_Email.Text != "")) && (txtEmailID.Text != ""))
                                        {
                                            string emailid = string.Empty;
                                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>The Local Conveyance Request has been sent for your approval.</font></pre><p/></b><pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre></b><pre>URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE REQUEST", "SUBMIT", txt_Approver_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        FSL.Logging.Logger.WriteEventLog(false, ex);
                                    }
                                    finally
                                    {
                                        string message = "alert('Local Conveyance Request has been Sent Successfully and Request No. is : " + txt_Request.Text + " ...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string msg = "alert('Approver Not Available...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                     }
                }

            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;

            path = activeDir + "\\" + "LOCAL CONVEYANCE\\";

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

}

