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

public partial class Foreign_Advance_Request : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Foreign_Advance_Request));
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
                    step_name.Text = "FOREIGN ADVANCE REQUEST";

                    Initialization();

                    fillPolicy_Details();
                    fillDocument_Details();
                    FillCountryFrom();
                    FillCountryTo();
                }
            }
        }
        catch (Exception Exc) { }
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


                DataTable dtamt1 = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref isValid, lbl_Grade.Text, "AdDesignationForeign");

                if (dtamt1 != null && dtamt1.Rows.Count > 0)
                {
                    DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Grade</th><th>Country</th><th>Amount In FC</th><th>Currency</th></tr></thead>";

                    for (int i = 0; i < dtamt1.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dtamt1.Rows[i]["GRADE"]) + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["COUNTRY_NAME"]) + "</td><td style='text-align:right'>" + Convert.ToString(dtamt1.Rows[i]["FTE_AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["ADV_CURRENCY"]) + "</td></tr>";
                    }
                    DisplayData += "</table>";
                }

                policy_data.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    private void Initialization()
    {
        string IsData = string.Empty;
        string IsDatam = string.Empty;
        string IsData1 = string.Empty;
        string IsData2 = string.Empty;
        string IsData3 = string.Empty;

        DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "pgettraveluser", ref IsData, txt_Username.Text);
        if (dtUser != null && dtUser.Rows.Count > 0)
        {
            lbl_EmpCode.Text = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
            lbl_EmpName.Text = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
            lbl_Dept.Text = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
            lbl_Grade.Text = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
            lbl_CostCenter.Text = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
            lbl_MobileNo.Text = dtUser.Rows[0]["MOBILE_NO"].ToString();
            lbl_desgnation.Text = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
            txt_desg_id.Text = Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);
            // lbl_GLCode.Text = Convert.ToString(dtUser.Rows[0]["GL_CODE"]);

            //lbl_division.Text = "NA";
            lbl_division.Text = Convert.ToString(dtUser.Rows[0]["DIVISION_NAME"]);
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
            DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "pgettravelrequestapprover", ref IsData1, txt_Username.Text);
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
    //added by pradeep

    private void FillCountryFrom()
    {
        String IsData = string.Empty;
        dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref IsData, "", "AdCountry");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddl_country_from.DataSource = dt;
            ddl_country_from.DataTextField = "COUNTRY_NAME";
            ddl_country_from.DataValueField = "PK_COUNTRY_ID";
            ddl_country_from.DataBind();
            ddl_country_from.Items.Insert(0, Li);
        }
    }



    private void FillCountryTo()
    {
        String IsData = string.Empty;
        dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref IsData, "", "AdCountryto");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddl_country_to.DataSource = dt;
            ddl_country_to.DataTextField = "COUNTRY_NAME";
            ddl_country_to.DataValueField = "PK_COUNTRY_ID";
            ddl_country_to.DataBind();
            ddl_country_to.Items.Insert(0, Li);
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string getCountryCurrency(int pk_id)
    {
        String IsData = string.Empty;
        string currency = "";
        string EXC_RATE = "";
        string pk_currency = "0";
        DataTable dtGrade = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref IsData, pk_id, "getCountryCurrency");
        if (dtGrade != null && dtGrade.Rows.Count > 0)
        {
            currency=Convert.ToString(dtGrade.Rows[0]["CURRENCY"]);
            EXC_RATE = Convert.ToString(dtGrade.Rows[0]["BASE_EXC_RATE"]);
            pk_currency = Convert.ToString(dtGrade.Rows[0]["PK_CURRENCY_ID"]);
        }
        return currency + "@@"+EXC_RATE + "@@"+pk_currency;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getCurrencyData()
    {
        string isvalid = "";
        string ret_data = "";
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref isvalid, "", "getCurrencies");
        ret_data = "<option Value='0'>---Select One---</option>";
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ret_data += "<option Value='" + Convert.ToString(dt.Rows[i]["pk_currency_id"]) + "'>" + Convert.ToString(dt.Rows[i]["currency_name"]) + "</option>";
            }
        }

        return ret_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getCurrencyRate(int curr)
    {
        string isvalid = "";
        string ret_data = "0.00";
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref isvalid, curr, "getCurrencyRate");
        if (dt != null && dt.Rows.Count > 0)
        {
            ret_data = Convert.ToString(dt.Rows[0][0]);
        }

        return ret_data;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string getCountryFromWiseCityFrom(int ddlcntfrm)
    {

        string ret_val = string.Empty;
        string is_data = string.Empty;
        string isValid = string.Empty;
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref isValid, ddlcntfrm, "AdCity");

            ret_val += "0$$---Select One---";
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ret_val += "@@" + Convert.ToString(dt.Rows[i]["PK_CITY_ID"]) + "$$" + Convert.ToString(dt.Rows[i]["NAME"]);
                }

            }
            ret_val += "@@" + "-1" + "$$" + "Other";
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return ret_val;

    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static double fillAmountForeign(string desg, string tocountry, string currency, string division)
    {

        string IsData1 = string.Empty;
        string IsData2 = string.Empty;
        double str = 0.00;
        double exchngrate = 0.00;
        double famount = 0.00;
        DataTable dta = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getallamountforeing", ref IsData1, desg, tocountry, division);
        if (dta != null && dta.Rows.Count > 0)
        {
            if (tocountry == "3")  //|| tocountry == "9" || tocountry == "15"
            {
                famount = Convert.ToDouble(dta.Rows[0]["FTE_AMOUNT"].ToString());
                DataTable dtexchrate = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getexchangerate", ref IsData2, currency);
                //exchngrate = Convert.ToDouble(dtexchrate.Rows[0]["EXCHANGE_RATE"].ToString());
                exchngrate = 1;
                str = famount / exchngrate;


            }
            else
            {
                str = Convert.ToDouble(dta.Rows[0]["FTE_AMOUNT"].ToString());
            }
        }
        return str;

    }

    //------------------------------------------------------------
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string fillAmountall(string desg, string tocity)
    {
        ////get amount from designation//////////////////////////////
        string IsData1 = string.Empty;
        string str = string.Empty;
        DataTable dta = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getallamount", ref IsData1, desg, tocity);
        if (dta != null && dta.Rows.Count > 0)
        {
            str = dta.Rows[0]["AMOUNT"].ToString();
        }
        return str;

    }

    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "script", "window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');", true);
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
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

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Type</th><th>File Name</th><th>Delete</th></tr></thead>";
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

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            Int32 flength = FileUpload1.PostedFile.ContentLength;

            string path = string.Empty;

            path = activeDir + "\\";

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
    public static string changetype(int advtype, string user, string desgid)
    {
        //showall(advtype);
        string ISValid = string.Empty;
        string strgl = string.Empty;
        string strall = string.Empty;
        string strexid = string.Empty;
        string stradamount = string.Empty;
        string strexpire = string.Empty;
        string stradperiod = string.Empty;
        string total_amount = string.Empty;
        string opcount = string.Empty;
        string opencount = string.Empty;
        int openadv = 0;
        DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref ISValid, advtype, "AdGLcode");
        strexid = dtamt.Rows[0]["PK_EXPENSE_HEAD_ID"].ToString();
        string Isdata1 = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref Isdata1, user, "AdExParameter");

        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (advtype == 2)  //Advance for foreign
                {
                    if (dt.Rows[i]["PARAMETER"].ToString() == "Foreign Advance Amount")
                    {
                        stradamount = (dt.Rows[i]["VALUE"].ToString());
                    }
                    if (dt.Rows[i]["PARAMETER"].ToString() == "Foreign Open Adavances")
                    {
                        if ((dt.Rows[i]["VALUE"]) != null || (dt.Rows[i]["VALUE"]) != "undefined")
                        {
                            openadv = Convert.ToInt32(dt.Rows[i]["VALUE"]);
                        }
                    }
                    if (dt.Rows[i]["PARAMETER"].ToString() == "Foreign Advance Period")
                    {
                        stradperiod = (dt.Rows[i]["VALUE"].ToString());
                    }
                }
            }
        }
        string IsData6 = string.Empty;
        int dts = 0; Double amttotal = 0;
        int count = 0;
        int hdrcnt = 0;
        DataTable dtid = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getadvanids", ref IsData6, user);
        if (dtid != null)
        {
            for (int i = 0; i < dtid.Rows.Count; i++)
            {
                string dtvalue = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getperiod", ref IsData6, user, dtid.Rows[i]["PK_FTA_HDR_ID"].ToString(), Convert.ToInt64(stradperiod));
                string[] result = dtvalue.Split('=');
                if (result[0] != null)
                {
                    dts = Convert.ToInt32(dts) + Convert.ToInt32(result[0]);
                }
                if (result[1] != null)
                {
                    if (result[1] == "true")
                    {
                        strexpire = "1";
                    }
                    else
                    {
                        strexpire = "0";
                    }
                }
                if (result[2] != null)
                {
                    amttotal = amttotal + Convert.ToDouble(result[2]);
                    total_amount = Convert.ToString(amttotal);
                }
                if (result[3] != null)
                {
                    count = Convert.ToInt32(result[3]);
                }
                hdrcnt++;
            }
        }

        if (dts != 0)
        {
            if ((dts) >= (openadv - 1))
            {
                opcount = "1";
            }
        }
        if (dts >= openadv)
        {
            opencount = "1";
        }
        else
        {
            opencount = "0";
        }

        string Isdata5 = string.Empty;
        string StrAccess = string.Empty;
        string dtaccess = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getaccessid", ref Isdata5, desgid);
        if (dtaccess != null)
        {
            StrAccess = dtaccess.ToString();
        }

        strall = stradamount + "," + stradperiod + "," + strexpire + "," + total_amount + "," + opcount + "," + opencount + "," + strexid + "," + count + "," + hdrcnt + "," + StrAccess;

        return strall;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string foreignpolicy(string desgid, string desg, string divid, string divmid)
    {

        string isData = string.Empty;
        string isValid = string.Empty;
        string DisplayData = string.Empty;
        divid = null;
        divmid = null;
        DisplayData = null;
        isValid = string.Empty;
        DataTable dtamt1 = (DataTable)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "selectdetails", ref isValid, desgid, "AdDesignationForeign");

        if (dtamt1 != null && dtamt1.Rows.Count > 0)
        {
            DisplayData = "<table class='table table-bordered' id='policyTable'><thead><tr class='grey'><th>Designation</th><th>Country</th><th>Amount(Foreign Currency)</th><th>Effective Date</th></tr></thead>";

            for (int i = 0; i < dtamt1.Rows.Count; i++)
            {
                DisplayData += "<tr><td>" + desg + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["country"]) + "</td><td style='text-align:right'>" + Convert.ToString(dtamt1.Rows[i]["AMOUNT"]) + "</td><td>" + Convert.ToString(dtamt1.Rows[i]["EFFECTIVE_DATE"]) + "</td></tr>";

            }
            DisplayData += "</table>";
        }

        return DisplayData;

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                divIns.Style.Add("display", "none");
                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;
                
                string remark = string.Empty;
                string isSaved = string.Empty;

                if (txt_foreign_remark.Value != "")
                {
                    remark = txt_foreign_remark.Value;
                }

                string inserXML = txt_Document_Xml.Text;
                inserXML = inserXML.Replace("&", "&amp;");
                txt_Document_Xml.Text = inserXML.ToString();

                string instanceID = (string)WFE.Action.StartCase(isInserted, txtProcessID.Text, txt_Username.Text, txt_Username.Text, txtEmailID.Text, txt_StepId.Text);
                txtInstanceID.Text = instanceID;

                isSaved = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "insert", ref refData, Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), txt_Username.Text, remark, txt_form_Date.Value, txt_to_date.Value, ddl_country_from.SelectedValue, ddl_country_to.SelectedValue, fcity.Text, tcity.Text, txt_fplace.Value, txt_tother.Value, pk_base_id.Text, base_curr_amt.Text, txt_xml_data_vehicle.Text, txt_Document_Xml.Text, txt_Deviate.Text); 
                if (isSaved == null || refData.Length > 0 || isSaved == "false")
                {
                    string[] errmsg = refData.Split(':');
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                }
                else
                {
                    if ((txt_Username.Text).ToLower() != "rbrathi" && (txt_Username.Text).ToLower() != "prrathi")
                    {
                        //string[] Request_Unique = isSaved.Split('=');
                        //txt_Request.Text = Request_Unique[0];
                        txt_Request.Text = isSaved;
                        string[] Dval1 = new string[1];
                        Dval1[0] = txt_approvar.Text;
                        
                        string ref_data = string.Empty;
                        string release_id = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getreleaseid", ref ref_data, txtProcessID.Text, "FOREIGN ADVANCE REQUEST", "SUBMIT");
                        if (release_id != "")
                        {
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, "FOREIGN ADVANCE REQUEST", "SUBMIT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, txt_Request.Text, "0", ref isInserted);

                            if (isCreate)
                            {

                                try
                                {
                                    string emailid = string.Empty;
                                    string isValid = string.Empty;
                                    string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Advance_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "FOREIGN ADVANCE REQUEST", "USER", txt_Username.Text, "SUBMIT", "Send For Approval", "0", "0");
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been sent for your approval.</font></pre><p/>  <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                    emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST", "SUBMIT", txt_Approver_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);

                                    DataTable dt = (DataTable)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "getfilenames", ref isValid, "ADVANCE", txt_Request.Text.ToString());
                                    if (dt.Rows.Count > 0)
                                    {
                                        string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                        string path = string.Empty;

                                        string foldername = txt_Request.Text.ToString();
                                        foldername = foldername.Replace("/", "_");
                                        path = activeDir + "\\" + foldername;
                                        if (Directory.Exists(path))
                                        {

                                        }
                                        else
                                        {
                                            Directory.CreateDirectory(path);
                                            string[] directories = Directory.GetFiles(activeDir);

                                            path = path + "\\";
                                            foreach (var directory in directories)
                                            {
                                                for (int i = 0; i < dt.Rows.Count; i++)
                                                {
                                                    var sections = directory.Split('\\');
                                                    var fileName = sections[sections.Length - 1];
                                                    if (dt.Rows[i]["filename"].ToString() == fileName)
                                                    {
                                                        System.IO.File.Move(activeDir + "\\" + fileName, path + fileName);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // throw;
                                    FSL.Logging.Logger.WriteEventLog(false, ex);
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been sent successfully and Request No. is : " + txt_Request.Text + " ...!');window.open('../../portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");

                                }
                            }
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                    else
                    {
                        //string[] Request_Unique = isSaved.Split('=');
                        txt_Request.Text = isSaved;
                        string ref_data = string.Empty;
                        DataTable DTAP = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval_Foreign.aspx", "getaccapprover", ref ref_data, "ADVANCE REQUEST FOREIGN ACCOUNT PAYABLE APPROVAL", 0, "");

                        if (DTAP != null)
                        {
                            if (DTAP.Rows.Count > 0)
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
                                    ref_data = string.Empty;
                                    string release_id = (string)ActionController.ExecuteAction("", "Foreign_Advance_Request.aspx", "getreleaseid", ref ref_data, txtProcessID.Text, "FOREIGN ADVANCE REQUEST", "SUBMIT-MD");
                                    if (release_id != "")
                                    {
                                        bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, "FOREIGN ADVANCE REQUEST", "SUBMIT-MD", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);

                                        if (isCreate)
                                        {
                                            try
                                            {
                                                string isValid = string.Empty;
                                                string auditid = (string)ActionController.ExecuteAction(txt_Username.Text, "Foreign_Advance_Request.aspx", "insertaudittrail", ref isInserted, txtProcessID.Text, txtInstanceID.Text, "FOREIGN ADVANCE REQUEST", "USER", txt_Username.Text, "SUBMIT", "Send For Payment Process", "0", "0");
                                                string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been sent to Accounts for payment approval.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + lbl_EmpName.Text.Trim() + "</font></pre></p><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:<a href='http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Advance.aspx?P=ADVANCE REQUEST FOREIGN&R=" + txt_Request.Text + "'>http://" + compname + "/Sudarshan-Portal/Vouchers/Foreign_Advance.aspx?P=ADVANCE REQUEST FOREIGN&R=" + txt_Request.Text + "</a></span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre></pre><br/><pre><font size='3'  color='red'><i><b>Kindly attach all the original supporting documents with the voucher print & submit to the Cashier in case mode of payment requested is cash otherwise, send the documents to the Cashier at GHO Pune.</b></i></font></pre>";

                                                string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST", "SUBMIT", txtEmailID.Text, txtApproverEmail.Text, msg, "Request No: " + txt_Request.Text);

                                                DataTable dt = (DataTable)ActionController.ExecuteAction(txt_Username.Text, "Advance_Request_Foreign.aspx", "getfilenames", ref isValid, "ADVANCE", txt_Request.Text.ToString());
                                                if (dt.Rows.Count > 0)
                                                {
                                                    string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                                    string path = string.Empty;

                                                    string foldername = txt_Request.Text.ToString();
                                                    foldername = foldername.Replace("/", "_");
                                                    path = activeDir + "\\" + foldername;
                                                    if (Directory.Exists(path))
                                                    {

                                                    }
                                                    else
                                                    {
                                                        Directory.CreateDirectory(path);
                                                        string[] directories = Directory.GetFiles(activeDir);

                                                        path = path + "\\";
                                                        foreach (var directory in directories)
                                                        {
                                                            for (int i = 0; i < dt.Rows.Count; i++)
                                                            {
                                                                var sections = directory.Split('\\');
                                                                var fileName = sections[sections.Length - 1];
                                                                if (dt.Rows[i]["filename"].ToString() == fileName)
                                                                {
                                                                    System.IO.File.Move(activeDir + "\\" + fileName, path + fileName);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            finally
                                            {
                                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Foreign Advance Request has been sent for account payment approval and Request No. is : " + txt_Request.Text + "!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                            }
                                        }
                                    }

                                    else
                                    {
                                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                    }

                                }
                            }
                        }
                    }
                }

            }

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }
}