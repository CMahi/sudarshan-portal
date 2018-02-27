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


public partial class Car_Expense : System.Web.UI.Page
{
    StringBuilder str = new StringBuilder();
    ListItem Li = new ListItem("--Select One--", "0");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Car_Expense));
                if (!Page.IsPostBack)
                {                                      
                    Initialization();
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                   // span_vdate.InnerHtml = DateTime.Now.ToString("dd-MMM-yyyy");
                    getUserInfo();
                    
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
    private void Initialization()
    {
        ActionController.SetControlAttributes(Page);
        txt_Username.Text = ((string)Session["USER_ADID"]);                 
        txtEmailID.Text = (string)Session["EmailID"];        
        txtProcessID.Text = Request.Params["processid"].ToString();
        txt_StepId.Text = Request.Params["stepid"].ToString();
      
        FillLocation();
    }

    
    private void FillLocation()
    {
        String IsData = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Car_Expense_MOdification.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdLocation");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlAdv_Location.DataSource = dt;
            ddlAdv_Location.DataTextField = "LOCATION_NAME";
            ddlAdv_Location.DataValueField = "PK_LOCATION_ID";
            ddlAdv_Location.DataBind();
            ddlAdv_Location.Items.Insert(0, Li);
        }

    }

    protected void getUserInfo()
  
        {
            try
            {
                string isdata = string.Empty;
                 string check = string.Empty;
                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Car_Expense.aspx", "pgettraveluser", ref isdata, txt_Username.Text);
                if (dtUser.Rows.Count > 0)
                {
                    empno.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
                    span_ename.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
                    span_cc.InnerHtml = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
                    span_dept.InnerHtml = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
                    span_grade.InnerHtml = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
                    span_mobile.InnerHtml = Convert.ToString(dtUser.Rows[0]["MOBILE_NO"]);
                    span_designation.InnerHtml = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
                    txt_designation.Text = Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);
                    if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]).Trim() != "")
                    {
                        span_bank_no.InnerHtml = Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]);
                    }
                    else
                    {
                        span_bank_no.InnerHtml = "NA";
                    }
                    if (Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]).Trim() != "")
                    {
                        span_Ifsc.InnerHtml = Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]);
                    }
                    span_Division.InnerHtml = "NA";
                    //span_Division.InnerHtml = Convert.ToString(dtUser.Rows[0]["DIVISION_NAME"]);
                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Car_Expense.aspx", "pgettravelrequestapprover", ref isdata, txt_Username.Text);
                    if (dtApprover != null)
                    {
                        if (dtApprover.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                            {
                                span_Approver.InnerHtml = Convert.ToString(dtApprover.Rows[0]["approver"]);
                                span_app_name.InnerHtml = Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                            }
                            else
                            {
                                span_Approver.InnerHtml = "NA";
                                span_app_name.InnerHtml = "NA";
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
                        else
                        {
                            span_Approver.InnerHtml = "NA";
                        }
                    }
                    check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "check", ref isdata, span_grade.InnerHtml, txt_Username.Text, empno.InnerHtml, txt_designation.Text);
                    if (check != "")
                    {
                        string pol_ltrs=string.Empty;
                        string[] TempResultData = check.Split('~');
                        string user = TempResultData[0].ToString();
                        string Fuel = TempResultData[1].ToString();
                        string Maintenance = TempResultData[2].ToString();
                        string Driver = TempResultData[3].ToString();
                        string uniform = TempResultData[4].ToString();
                        txt_car_Age.Text = TempResultData[5].ToString();
                        main_car_age1.Value = txt_CarAge1.Value = TempResultData[5].ToString();
                        if (TempResultData[6].ToString() != "0")
                            pol_ltrs = TempResultData[6].ToString();
                        string ex_gratia = TempResultData[7].ToString();
                        txt_driversalary.Text = TempResultData[8].ToString();
                        if (user == "false")
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense claim cannot be submited ..!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                        }
                        if (Fuel == "false")
                        {
                            Li7.Style["display"] = "none";
                            dv_polict_f.Style["display"] = "none";
                        }
                        else
                        {
                            StringBuilder html_Headerfuel = new StringBuilder();
                            html_Headerfuel.Append("<table class='table table-bordered' id='tblpolfuel' width='100%'>");
                            html_Headerfuel.Append("<thead><tr class='grey'><th style='text-align:center'>Grade</th><th style='text-align:center'>Fuel Eligibility per month in liters</th></tr></thead>");
                            html_Headerfuel.Append("<tbody>");
                            html_Headerfuel.Append("<tr><td style='text-align:center'>" + span_grade.InnerHtml + "</td><td style='text-align:center'>" + pol_ltrs + "</td></tr>");
                            html_Headerfuel.Append("</tbody></table>");
                            fuel_policy.InnerHtml = html_Headerfuel.ToString();
                        }
                        if (Maintenance == "false")
                        {
                            li_Maintenance.Style["display"] = "none";
                            Li1.Style["display"] = "none";
                            Li2.Style["display"] = "none";
                            dv_polictm.Style["display"] = "none";
                        }
                        if (Driver == "false")
                        {
                            Li9.Style["display"] = "none";
                        }
                        if (uniform == "true")
                        {
                            tr_uniform.Style["display"] = "none";
                        }
                        if (ex_gratia == "true")
                        {
                            tr_ex_gratia.Style["display"] = "none";
                        }
                        DataTable dtcar = (DataTable)ActionController.ExecuteAction("", "Car_Expense.aspx", "getcardtl", ref isdata, empno.InnerHtml);//empno.InnerHtml
                        if (dtcar.Rows.Count > 0)
                        {
                            txt_CarNumber.Text = Convert.ToString(dtcar.Rows[0]["Vehicle_No"]);
                            txt_CarDate.Text = Convert.ToString(dtcar.Rows[0]["PUT_Date"]);

                            StringBuilder html_Header = new StringBuilder();
                            html_Header.Append("<table class='table table-bordered' id='tblpolfuel' width='100%'>");
                            html_Header.Append("<thead><tr class='grey'><th style='text-align:center'>Grade</th><th style='text-align:center'>Year 1</th><th style='text-align:center'>Year 2</th><th style='text-align:center'>Year 3</th><th style='text-align:center'>Year 4</th><th style='text-align:center'>Year 5</th><th style='text-align:center'>Year 6</th></tr></thead>");
                            html_Header.Append("<tbody>");
                            if (span_grade.InnerHtml == "Director")
                                html_Header.Append("<tr><td style='text-align:center'>Director</td><td style='text-align:center'>4,000/-</td><td style='text-align:center'>10,000/-</td><td style='text-align:center'>20,000/-</td><td style='text-align:center'>30,000/-</td><td style='text-align:center'>40,000/-</td><td style='text-align:center'>-</td></tr>");
                            else if (span_grade.InnerHtml == "L1")
                                html_Header.Append("<tr><td style='text-align:center'>L1</td><td style='text-align:center'>3,000/-</td><td style='text-align:center'>7,500/-</td><td style='text-align:center'>15,000/-</td><td style='text-align:center'>22,500/-</td><td style='text-align:center'>30,000/-</td><td style='text-align:center'>-</td></tr>");
                            else if (span_grade.InnerHtml == "L2")
                                html_Header.Append("<tr><td style='text-align:center'>L2</td><td style='text-align:center'>2,000/-</td><td style='text-align:center'>5,000/-</td><td style='text-align:center'>6,000/-</td><td style='text-align:center'>10,000/-</td><td style='text-align:center'>15,000/-</td><td style='text-align:center'>20,000/-</td></tr>");
                            html_Header.Append("</tbody></table>");
                            mt_policy.InnerHtml = html_Header.ToString();
                        }
                        else
                        {
                            li_Maintenance.Style["display"] = "none";
                            Li1.Style["display"] = "none";
                            Li2.Style["display"] = "none";
                        }
                        if (Int32.Parse(txt_car_Age.Text) > 6)
                        {
                            li_Maintenance.Style["display"] = "none";
                            Li1.Style["display"] = "none";
                            Li2.Style["display"] = "none";
                        }
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense claim cannot be submited ..!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                    }

                }
            }
            catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
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

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Name</th><th>Delete</th></tr></thead>";
                DisplayData += "</table>";
                div_docs.InnerHtml = DisplayData;
                DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                string isdata = string.Empty;
                string isSaved = string.Empty;

                string fuelxml_string = txt_xml_data_fuel.Text;
                fuelxml_string = fuelxml_string.Replace("&", "&amp;");
                fuelxml_string = fuelxml_string.Replace(">", "&gt;");
                fuelxml_string = fuelxml_string.Replace("<", "&lt;");
                fuelxml_string = fuelxml_string.Replace("||", ">");
                fuelxml_string = fuelxml_string.Replace("|", "<");
                fuelxml_string = fuelxml_string.Replace("'", "&apos;");
                txt_xml_data_fuel.Text = fuelxml_string.ToString();

                string maintainancexml_string = txt_xml_data_maitainance.Text;

                maintainancexml_string = maintainancexml_string.Replace("&", "&amp;");
                maintainancexml_string = maintainancexml_string.Replace(">", "&gt;");
                maintainancexml_string = maintainancexml_string.Replace("<", "&lt;");
                maintainancexml_string = maintainancexml_string.Replace("||", ">");
                maintainancexml_string = maintainancexml_string.Replace("|", "<");
                maintainancexml_string = maintainancexml_string.Replace("'", "&apos;");
                txt_xml_data_maitainance.Text = maintainancexml_string.ToString();

                string driverxml_string = txt_xml_data_driver.Text;

                driverxml_string = driverxml_string.Replace("&", "&amp;");
                driverxml_string = driverxml_string.Replace(">", "&gt;");
                driverxml_string = driverxml_string.Replace("<", "&lt;");
                driverxml_string = driverxml_string.Replace("||", ">");
                driverxml_string = driverxml_string.Replace("|", "<");
                driverxml_string = driverxml_string.Replace("'", "&apos;");
                txt_xml_data_driver.Text = driverxml_string.ToString();

                

                string tyrexml_string = txt_xml_data_tyre.Text;

                tyrexml_string = tyrexml_string.Replace("&", "&amp;");
                tyrexml_string = tyrexml_string.Replace(">", "&gt;");
                tyrexml_string = tyrexml_string.Replace("<", "&lt;");
                tyrexml_string = tyrexml_string.Replace("||", ">");
                tyrexml_string = tyrexml_string.Replace("|", "<");
                tyrexml_string = tyrexml_string.Replace("'", "&apos;");
                txt_xml_data_tyre.Text = tyrexml_string.ToString();

                string batteryexml_string = txt_xml_data_battery.Text;

                batteryexml_string = batteryexml_string.Replace("&", "&amp;");
                batteryexml_string = batteryexml_string.Replace(">", "&gt;");
                batteryexml_string = batteryexml_string.Replace("<", "&lt;");
                batteryexml_string = batteryexml_string.Replace("||", ">");
                batteryexml_string = batteryexml_string.Replace("|", "<");
                batteryexml_string = batteryexml_string.Replace("'", "&apos;");
                txt_xml_data_battery.Text = batteryexml_string.ToString();

                string FILEXML = Txt_File_xml.Text;

                FILEXML = FILEXML.Replace("&", "&amp;");
                FILEXML = FILEXML.Replace(">", "&gt;");
                FILEXML = FILEXML.Replace("<", "&lt;");
                FILEXML = FILEXML.Replace("||", ">");
                FILEXML = FILEXML.Replace("|", "<");
                FILEXML = FILEXML.Replace("'", "&apos;");
                Txt_File_xml.Text = FILEXML.ToString();

                 //txt_Document_Xml.Text="<ROWSET></ROWSET>";
                 string isInserted = string.Empty;
                 string instanceID = (string)WFE.Action.StartCase(isInserted, txtProcessID.Text, txt_Username.Text, txt_Username.Text, txtEmailID.Text, "15");
                 txtInstanceID.Text = instanceID;
                

                 DataTable dtRole = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getdocapprover", ref isInserted, "CAR EXPENSE PAYMENT APPROVAL");
                 if (dtRole != null && dtRole.Rows.Count > 0)
                 {
                     string[] Dval = new string[dtRole.Rows.Count];
                     string Emailids = "";
                     for (int i = 0; i < dtRole.Rows.Count; i++)
                     {
                         Dval[i] = dtRole.Rows[i]["USER_ADID"].ToString().Trim();
                         if (Emailids == "")
                         {
                             Emailids = dtRole.Rows[i]["EMAIL_ID"].ToString().Trim();
                         }
                         else
                         {
                             Emailids = Emailids + "," + dtRole.Rows[i]["EMAIL_ID"].ToString().Trim();
                         }
                     }
                     isSaved = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "insert", ref isdata, txtProcessID.Text, txtInstanceID.Text, txt_Username.Text, txt_xml_data_fuel.Text, txt_xml_data_maitainance.Text, txt_xml_data_driver.Text, Txt_File_xml.Text, txtexpnsamt.Text, ddl_Payment_Mode.SelectedValue, ddlAdv_Location.SelectedValue, txt_fule_Dev.Text, txt_maintain_Dev.Text, txt_xml_data_tyre.Text, txt_xml_data_battery.Text, txtEmailID.Text, Emailids);
                     if (isSaved == null || isdata.Length > 0 || isSaved == "false")
                     {
                         string[] errmsg = isdata.Split(':');
                         Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                     }
                     else
                     {
                         bool isCreate = false;

                         isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, txt_StepId.Text, "CAR EXPENSE REQUEST", "SUBMIT", "", txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, isSaved.ToString(), "0", ref isInserted);
                         if (isCreate)
                         {
                             try
                             {
                                 string isValid = string.Empty;
                                 DataTable dt = (DataTable)ActionController.ExecuteAction(txt_Username.Text, "Car_Expense.aspx", "getfilenames", ref isValid, "CAR POLICY", isSaved.ToString());
                                 if (dt.Rows.Count > 0)
                                 {
                                     string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                                     string path = string.Empty;

                                     string foldername = isSaved.ToString();
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
                             catch (Exception exs)
                             {
                                 Logger.WriteEventLog(false, exs);
                             }
                             finally
                             {

                                 Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Car Expense Submitted Successfully and Voucher No : " + isSaved.ToString() + "');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                             }
                         }

                         else
                         {
                             string[] errmsg = isInserted.Split(':');
                             if (errmsg[1].ToString().Trim() != "")
                             {
                                 Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                             }
                             else
                             {
                                 Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + (string)isSaved + "');}</script>");
                             }


                         }

                     }
                 }
            }
           MP_Loading.Hide();
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); MP_Loading.Hide(); }
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

         //   ClearContents(sender as Control);

        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string checkfule(int ltr, string grad, string adid, int desig, string date,int id)
    {
       string check = string.Empty;
        string isdata = string.Empty;
        try
        {
            check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "getfule", ref isdata, ltr, grad, adid, date, desig);
            check = check + "#" + id;
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return check;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string checkm(int amt, string grad, string adid, int desig, string date, string empno,int id,int carage)
    {
        string check = string.Empty;
        string isdata = string.Empty;
        try
        {
            check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "getm", ref isdata, amt, grad, adid, date, desig, empno, carage);
            check = check + "#" + id;
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return check;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string checks(string date, string grad, string adid, int desig)
    {
        string check = string.Empty;
        string isdata = string.Empty;
        try
        {
            check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "gets", ref isdata, date, grad, adid, desig);
            
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return check;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string checku(string date, string grad, string adid, int desig)
    {
        string check = string.Empty;
        string isdata = string.Empty;
        try
        {
            check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "getu", ref isdata, date, grad, adid, desig);
            
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return check;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string checkb(string date, string adid, int amt, int id)
    {
        string check = string.Empty;
        string isdata = string.Empty;
        try
        {
            check = (string)ActionController.ExecuteAction("", "Car_Expense.aspx", "getb", ref isdata, amt,adid, date);
            check = check + "#" + id;
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return check;
    }
}