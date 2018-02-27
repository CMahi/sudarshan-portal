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

public partial class Local_Conveyance_Modification : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Local_Conveyance_Modification));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["step"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                        {
                            txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                            txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                            txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                            txt_Step.Text = Convert.ToString(Request.QueryString["step"]);
                            txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                        }
                    }
                    FillMode();
                    FillLocation();
                    fillDocument_Details();
                    FillVehicleType();
                    Initialization();
                    ShowLocalUser();
                    fillAdvanceAmount();
                    fillAuditTrailData();
                    fillPolicy_Details();
                }
            }
        }
        catch (Exception Exc) { }
    }
    private void fillAuditTrailData()
    {
        try
        {
            GetData getdata = new GetData();
            div_Audit.InnerHtml = getdata.fillAuditTrail(txtProcessID.Text, txtInstanceID.Text);
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }
    private void FillVehicleType()
    {
        StringBuilder str1 = new StringBuilder();
        str1.Append("<option value='0'>--Select One--</option>");
        str1.Append("<option value='Two Wheeler'>Two Wheeler</option>");
        str1.Append("<option value='Four Wheeler'>Four Wheeler</option>");
        str1.Append("<option value='Other'>Other</option>");
        txt_vehitype.Text = str1.ToString();
        txt_vehitype1.Text += "Two Wheeler" + "~" + "Two Wheeler" + ";";
        txt_vehitype1.Text += "Four Wheeler" + "~" + "Four Wheeler" + ";";
        txt_vehitype1.Text += "Other" + "~" + "Other" + ";";
    }
    private void Initialization()
    {
        try
        {
            string isdata = string.Empty;
            string IsData = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "pgetrequestdata", ref isdata, txtWIID.Text);
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_Local_Conveyance_HDR_Id"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                txt_Initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["EMP_AD_ID"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["INIT_MAIL"]);
                // txt_remark.Value = Convert.ToString(dsData.Tables[0].Rows[0]["REMARK"]);

                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "pgettraveluser", ref isdata, txt_Initiator.Text);
                if (dtUser.Rows.Count > 0)
                {
                    empno.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
                    span_ename.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
                    span_cc.InnerHtml = Convert.ToString(dtUser.Rows[0]["COST_CENTER_NAME"]);
                    span_dept.InnerHtml = Convert.ToString(dtUser.Rows[0]["DEPT_NAME"]);
                    span_grade.InnerHtml = Convert.ToString(dtUser.Rows[0]["GRADE_NAME"]);
                    // span_mobile.InnerHtml = Convert.ToString(dtUser.Rows[0]["MOBILE_NO"]);
                    span_designation.InnerHtml = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
                    txt_designation.Text = Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);
                    if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]) != "")
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
                    DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "pgettravelrequestapprover", ref isdata, txt_Initiator.Text);
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
                            // doa_email.Text = Convert.ToString(dtApprover.Rows[0]["doa_approver_email"]);
                        }
                        else
                        {
                            span_Approver.InnerHtml = "NA";
                        }
                    }

                    ddlAdv_Location.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["LOCATION"]);
                    ddl_Payment_Mode.SelectedValue = Convert.ToString(dsData.Tables[0].Rows[0]["PAYMENT_MODE"]);
                }
            }

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }

    }
    private void ShowLocalUser()
    {
        try
        {
            StringBuilder html_Header = new StringBuilder();
            string IsValid = string.Empty;
            DataSet DTS = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "pgetrequestdata", ref IsValid, txtWIID.Text);
            DTS.Tables[2].Columns[0].ColumnName = "Sr.No";
            Session["ResultData"] = DTS;
            string from_loc = string.Empty;
            string to_loc = string.Empty;

            if (DTS.Tables[2].Rows.Count > 0)
            {
                html_Header.Append("<table class='table table-bordered hover' id='tbl_Local'><thead>");
                html_Header.Append("<tr><th style='width:2%'>#</th><th>Vehicle Type</th><th>From Location</th><th>To Location</th><th>From Date</th><th>To Date</th><th>KMs</th><th>Amount</th><th>Add</th><th>Delete</th></tr></thead> <tbody>");
                decimal total_Amount = 0;
                if (DTS.Tables[2].Rows.Count > 0)
                {
                    for (int i = 0; i < DTS.Tables[2].Rows.Count; i++)
                    {
                        if (DTS.Tables[2].Rows[i]["FROM_LOACATION"].ToString().Contains("'"))
                        {
                            from_loc = DTS.Tables[2].Rows[i]["FROM_LOACATION"].ToString().Replace("'", "&apos;");
                        }
                        else
                        {
                            from_loc = DTS.Tables[2].Rows[i]["FROM_LOACATION"].ToString();
                        }
                        if (DTS.Tables[2].Rows[i]["FROM_LOACATION"].ToString().Contains("'"))
                        {
                            to_loc = DTS.Tables[2].Rows[i]["TO_LOACATION"].ToString().Replace("'", "&apos;");
                        }
                        else
                        {
                            to_loc = DTS.Tables[2].Rows[i]["TO_LOACATION"].ToString();
                        }

                        string kms = string.Empty;
                        if (DTS.Tables[2].Rows[i]["KMS"].ToString() == "0")
                        {
                            kms = "";
                        }
                        else
                        {
                            kms = DTS.Tables[2].Rows[i]["KMS"].ToString();
                        }
                        html_Header.Append("<tr><td>" + (i + 1) + "</td><td><select id='ddlVehicleType" + (i + 1) + "'  class='form-control input-sm width-150' onchange='valuetypeamt(" + (i + 1) + ")'><option selected='selected' value='" + DTS.Tables[2].Rows[i]["VEHICLE_TYPE"].ToString() + "'>" + DTS.Tables[2].Rows[i]["VEHICLE_TYPE"].ToString() + "</option></select></td></td>");
                        html_Header.Append(" <td><input type='text' class='form-control input-sm width-150' id='txt_fromloc" + (i + 1) + "' value ='" + from_loc + "' runat='server' /></td><td ><input type='text' class='form-control input-sm width-150' id='txt_toloc" + (i + 1) + "' value ='" + to_loc + "' runat='server' /></td><td ><div class='input-group'> <input type='text' class='form-control input-sm width-100 datepicker-dropdown'  id='txt_fdate" + (i + 1) + "'  readonly=''  value='" + DTS.Tables[2].Rows[i]["From_Date"].ToString() + "' runat='server'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button> </span></div></td>");
                        html_Header.Append("<td ><div class='input-group'> <input type='text' class='form-control input-sm width-100 datepicker-dropdown'  id='txt_tdate" + (i + 1) + "' readonly=''  value='" + DTS.Tables[2].Rows[i]["To_Date"].ToString() + "' runat='server'/><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button> </span></div></td> <td><input type='text' class='form-control input-sm width-50' id='txt_kms" + (i + 1) + "' value ='" + kms + "' runat='server' onchange='valuechanamt(" + (i + 1) + ");'  onkeypress='return isNumberKey(event)'/></td><td ><input type='text' class='form-control input-sm width-100' id='txt_amount" + (i + 1) + "' value ='" + DTS.Tables[2].Rows[i]["BILL_AMOUNT"].ToString() + "' runat='server' onchange='calculate_Total()'/></td>");
                        html_Header.Append("<td class='add_Local' id='add_Row" + (i + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td><td id='delete_Row" + (i + 1) + "' ><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash' onclick='delete_Row(" + (i + 1) + ")'></i><input type='hidden' id='txtpk" + (i + 1) + "' value='" + DTS.Tables[0].Rows[0]["PK_Local_Conveyance_HDR_Id"] + "'></input><input type='hidden' id='txtfk" + (i + 1) + "' value='" + DTS.Tables[0].Rows[0]["PK_Local_Conveyance_HDR_Id"] + "'></input><input type='hidden' id='txtaddnewrow" + (i + 1) + "' value=''></input></td></tr>");
                        total_Amount = total_Amount + Convert.ToDecimal(DTS.Tables[2].Rows[i]["BILL_AMOUNT"]);
                        txt_remark.Value = Convert.ToString(DTS.Tables[2].Rows[i]["REMARK"].ToString());

                        ClientScript.RegisterStartupScript(this.GetType(), "Fillvehitype" + (i + 1) + "ddl", "FillVehicleType('ddlVehicleType" + (i + 1) + "');", true);
                    }
                    html_Header.Append(" </tbody></table>");
                    div_LocalData.InnerHtml = html_Header.ToString();
                    txt_advance_id.Text = Convert.ToString(DTS.Tables[0].Rows[0]["ADVANCE_ID"].ToString());
                    spn_Total.InnerHtml = Convert.ToString(total_Amount);
                }
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Data Found !!')}</script>");
                //div_LocalData.InnerHtml = null;
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

    }
    private void fillAdvanceAmount()
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataSet dt1 = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "pgetadvancedetails", ref isValid, txt_Initiator.Text, txt_pk_id.Text, 2);
            txt_adcount.Text = Convert.ToString(dt1.Tables[0].Rows.Count);
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Amount</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            for (int Index = 0; Index < dt1.Tables[0].Rows.Count; Index++)
            {
                tblHTML.Append("<tr>");
                //tblHTML.Append("<td>" + (Index + 1) + "</td>");
                if (txt_advance_id.Text == Convert.ToString(dt1.Tables[0].Rows[Index]["PK_ADVANCE_HDR_id"]))
                {
                    tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='local' checked><input type='text' id='PK_ADVANCE_HDR_id" + (Index + 1) + "' value='" + Convert.ToString(dt1.Tables[0].Rows[Index]["PK_ADVANCE_HDR_id"]) + "' style='display:none'><input type='text' id='advance_amount" + (Index + 1) + "' value='" + Convert.ToString(dt1.Tables[0].Rows[Index]["AMOUNT"]) + "' style='display:none'></td>");
                }
                else
                {
                    tblHTML.Append("<td><input type='radio' id='radio" + (Index + 1) + "'  name='local' ><input type='text' id='PK_ADVANCE_HDR_id" + (Index + 1) + "' value='" + Convert.ToString(dt1.Tables[0].Rows[Index]["PK_ADVANCE_HDR_id"]) + "' style='display:none'><input type='text' id='advance_amount" + (Index + 1) + "' value='" + Convert.ToString(dt1.Tables[0].Rows[Index]["AMOUNT"]) + "' style='display:none'></td>");
                }
                tblHTML.Append("<td>" + Convert.ToString(dt1.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt1.Tables[0].Rows[Index]["ADVANCE_DATE"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt1.Tables[0].Rows[Index]["AMOUNT"]) + "</td>");
                tblHTML.Append("<td>" + Convert.ToString(dt1.Tables[0].Rows[Index]["APPROVED"]) + "</td>");
                tblHTML.Append("</tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            div_Advance.InnerHtml = tblHTML.ToString();
            if (dt1.Tables[0] != null)
            {
                double adv = Convert.ToDouble(dt1.Tables[0].Rows[0]["AMOUNT"]);
                double exp = Convert.ToDouble(spn_Total.InnerHtml);
                if (exp > adv)
                {
                    spn_hdr.InnerHtml = "Payable Amount : ";
                    spn_val.InnerHtml = Convert.ToString(exp - adv);
                }
                else
                {
                    spn_hdr.InnerHtml = "Recovery Amount : ";
                    spn_val.InnerHtml = Convert.ToString(adv - exp);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
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

                DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "pgetrequestdata", ref isValid, txtWIID.Text);

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>";
                if (dsData != null)
                {
                    for (int i = 0; i < dsData.Tables[1].Rows.Count; i++)
                    {
                        DisplayData += "<tr><td>" + Convert.ToString(dsData.Tables[1].Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a onclick='downloadfiles(" + (i + 1) + ")'>" + Convert.ToString(dsData.Tables[1].Rows[i]["FILENAME"]) + "</a></td><td><i id='del" + dsData.Tables[1].Rows.Count + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (dsData.Tables[1].Rows.Count) + ");\" ></td></tr>";
                    }
                }
                DisplayData += "</table>";
                divalldata.InnerHtml = DisplayData;
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
            ddl_Payment_Mode.DataSource = dt;
            ddl_Payment_Mode.DataTextField = "PAYMENT_MODE";
            ddl_Payment_Mode.DataValueField = "PK_PAYMENT_MODE";
            ddl_Payment_Mode.DataBind();
            ddl_Payment_Mode.Items.Insert(0, Li);
        }

    }
    private void FillLocation()
    {
        String IsData = string.Empty;
        dt = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "selectdetails", ref IsData, txt_Username.Text, "AdLocation");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlAdv_Location.DataSource = dt;
            ddlAdv_Location.DataTextField = "LOCATION_NAME";
            ddlAdv_Location.DataValueField = "PK_LOCATION_ID";
            ddlAdv_Location.DataBind();
            ddlAdv_Location.Items.Insert(0, Li);
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
                string refData = string.Empty;
                string isInserted = string.Empty;
                string ISValid = string.Empty;
                string ISValid1 = string.Empty;
                string ISValid2 = string.Empty;
                string isSaved = string.Empty;
                txt_Condition.Text = "1";
                txt_Action.Text = "Submit";
                bool isCreate = false;

                string inserXML = txt_Document_Xml.Text;
                inserXML = inserXML.Replace("&", "&amp;");
                txt_Document_Xml.Text = inserXML.ToString();
                txt_Audit.Text = "LOCAL CONVEYANCE SEND BACK";

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
                vehiclexml_string = vehiclexml_string.Replace("'", "&apos;");
                txt_xml_data_vehicle.Text = vehiclexml_string.ToString();
                //int days1 = 0;
                //DataTable days = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense.aspx", "selectdetails", ref ISValid2, "Local Conveyance", "AdDays");
                //if (days != null && days.Rows.Count > 0)
                //{
                //    days1 = Convert.ToInt16(days.Rows[0]["Days"].ToString());
                //}

                //  XmlDocument xml = new XmlDocument();
                // xml.LoadXml(txt_xml_data_vehicle.Text);
                //  XmlNodeList xnList = xml.SelectNodes("/ROWSET/ROW");
                //for (int k = 0; k < xnList.Count; k++)
                // {
                //     vtype2 = vfrom2 = vto2 = "";     
                //     string vtype = xnList[k].ChildNodes.Item(1).InnerText.Trim(); 
                //     string vfrom = xnList[k].ChildNodes.Item(2).InnerText.Trim(); 
                //     string vto = xnList[k].ChildNodes.Item(3).InnerText.Trim();
                //     string vdate = xnList[k].ChildNodes.Item(5).InnerText.Trim(); 
                //     if (k < xnList.Count - 1)
                //     {
                //         vtype3 = vfrom3 = vto3 = "";
                //         vtype2 = xnList[k + 1].ChildNodes.Item(1).InnerText.Trim();
                //         vfrom2 = xnList[k + 1].ChildNodes.Item(2).InnerText.Trim();
                //         vto2 = xnList[k + 1].ChildNodes.Item(3).InnerText.Trim();
                //         vdate2 = xnList[k + 1].ChildNodes.Item(5).InnerText.Trim(); 
                //         if ((k + 1) < xnList.Count - 1)
                //         {
                //             vtype3 = xnList[k + 2].ChildNodes.Item(1).InnerText.Trim();
                //             vfrom3 = xnList[k + 2].ChildNodes.Item(2).InnerText.Trim();
                //             vto3 = xnList[k + 2].ChildNodes.Item(3).InnerText.Trim();
                //             vdate3 = xnList[k + 2].ChildNodes.Item(5).InnerText.Trim(); 
                //         }
                //     }

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

                //if ((vtype == vtype2 && vfrom == vfrom2 && vto == vto2 && vdate == vdate2) || (vtype3 == vtype2 && vfrom2 == vfrom3 && vto2 == vto3 && vdate2 == vdate3) || (vtype == vtype3 && vfrom == vfrom3 && vto == vto3 && vdate == vdate3))
                //{
                //    if ((vtype2 != "" || vtype3 != "") && (vfrom2 != "" || vfrom3 != "") && (vto2 != "" || vto3 != "") && (vdate2 != "" || vdate3 != ""))
                //    {
                //        success = "SAME";
                //    }
                //}
                //else
                //{
                //    dtamt = (string)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "checkduplicate", ref ISValid1, vdate, vtype, vbillno, txt_Username.Text);
                //    if (dtamt == "true")
                //    {
                //        ss1 += vbillno + " / ";
                //        success = "TRUE";
                //    }
                //}

                // }
                //if (success == "DIFFDAYS")
                //{
                //    string message = "alert('You do not have permission to submit the bill for :" + ss + "  bill no..!')";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //}
                //if (success == "TRUE")
                //{
                //    string message = "alert('You have Already Submitted Bill For This Month for :" + ss1 + "  bill no')";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //}
                //if (success == "SAME")
                //{
                //    string message = "alert('You have entered same local coneyance for the same day!')";
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //}
                if (success == "FALSE")
                {
                    if (span_Approver.InnerHtml != "NA")
                    {
                        if (ddl_Payment_Mode.SelectedValue == "2")
                        {
                            ddlAdv_Location.SelectedValue = "0";
                        }
                        isSaved = (string)ActionController.ExecuteAction("", "Local_Conveyance_Modification.aspx", "update", ref refData, Convert.ToInt32(txtProcessID.Text), Convert.ToInt32(txtInstanceID.Text), ddl_Payment_Mode.SelectedValue, txt_pk_id.Text, txt_xml_data_vehicle.Text, txt_Username.Text, txt_remark.Value, txt_Audit.Text, txt_Action.Text, span_Approver.InnerHtml, txt_Document_Xml.Text, ddlAdv_Location.SelectedValue);
                        if (isSaved == null || refData.Length > 0 || isSaved == "false")
                        {
                            string[] errmsg = refData.Split(':');
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + errmsg[1].ToString() + "')", true);
                        }
                        else
                        {
                           
                            if (empno.InnerHtml == "10000" || empno.InnerHtml == "10001")
                            {

                                string msg = "";
                                string ref_data = string.Empty;
                                string emailid = string.Empty;
                                CryptoGraphy crypt = new CryptoGraphy();
                                string process_name = crypt.Encryptdata("LOCAL CONVEYANCE");
                                string req_no = crypt.Encryptdata(txt_Request.Text);
                                DataTable DTAP = new DataTable();

                                if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                                {
                                    DTAP = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "getaccapprover", ref ref_data, "LOCAL CONVEYANCE ACCOUNT PAYMENT APPROVAL", ddlAdv_Location.SelectedValue, ddl_Payment_Mode.SelectedValue);
                                }
                                else
                                {
                                    DTAP = (DataTable)ActionController.ExecuteAction("", "Local_Conveyance_Approval.aspx", "getaccapprover", ref ref_data, "LOCAL CONVEYANCE ACCOUNT PAYMENT APPROVAL", 0, ddl_Payment_Mode.SelectedValue);
                                }
                                if (DTAP != null)
                                {
                                    if (DTAP.Rows.Count > 0)
                                    {
                                        txt_Condition.Text = "3";
                                        string isSaved1 = (string)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "inserttwi", ref ref_data, txtProcessID.Text, txtInstanceID.Text, txt_Username.Text, txt_Request.Text, "4");
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
                                                        if (ddl_Payment_Mode.SelectedItem.Text.ToUpper() == "CASH")
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Local Conveyance Request has been modified and sent to Accounts for payment approval..</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + txt_Request.Text.Trim() + "</font></pre></p><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Cash_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE APPROVAL", "SUBMIT", txt_Approver_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                                        }
                                                        else
                                                        {
                                                            msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Local Conveyance Request has been modified and sent to Accounts for payment approval.</font></pre><p/> <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre> <pre><font size='3'>Created By: " + txt_Request.Text.Trim() + "</font></pre></p><pre><span style='font-size: medium;'>Please Take Voucher Print By Link Given Below : </span></pre><pre><span style='font-size: medium;'>INTRANET URL:http://espuat/Sudarshan-Portal/Vouchers/Bank_Voucher.aspx?P=" + process_name + "&R=" + req_no + "</span></pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
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
                                                    msg2 = "alert('Local Conveyance has been modified and sent for account payment approval...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea')";
                                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);

                                                }
                                            }
                                        }//
                                    }
                                    else
                                    {
                                        string msg2 = string.Empty;
                                        msg2 = "alert('Account Payment Approver Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                    }
                                }
                                else
                                {
                                    string msg2 = string.Empty;
                                    msg2 = "alert('Account Payment Approver Not Available For " + ddl_Payment_Mode.SelectedItem.Text + " Payment Mode...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
                                }
                            }
                            else
                            {
                                string[] Dval1 = new string[1];
                                Dval1[0] = span_Approver.InnerHtml;
                                if (txtApproverEmail.Text == "")
                                {
                                    txtApproverEmail.Text = txtEmailID.Text;
                                }
                                isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, "132", "LOCAL CONVEYANCE MODIFICATION", "SUBMIT", "", txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval1, txt_Request.Text, txtWIID.Text, ref isInserted);


                                if (isCreate)
                                {
                                    try
                                    {
                                        if (((txt_Approver_Email.Text != "NA") || (txt_Approver_Email.Text != "")) && (txtEmailID.Text != ""))
                                        {
                                            string emailid = string.Empty;
                                            string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>The Local Conveyance Request has been modified and sent for your approval.</font></pre><p/>  <pre><font size='3'>Request No: " + txt_Request.Text + "</font></pre><pre><font size='3'>Created By: " + span_ename.InnerHtml.Trim() + "</font></pre></p><pre></b><pre>INTRANET URL:http://" + compname + "/Sudarshan-Portal/Login.aspx</pre><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                            emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Local_Conveyance.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "LOCAL CONVEYANCE MODIFICATION", "SUBMIT", txt_Approver_Email.Text, txtEmailID.Text, msg, "Request No: " + txt_Request.Text);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        FSL.Logging.Logger.WriteEventLog(false, ex);
                                    }
                                    finally
                                    {
                                        string message = "alert('Local Conveyance Request Modified Successfully...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string msg2 = string.Empty;
                        msg2 = "alert('Approver Not Available...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg2, true);
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
            path = activeDir + "\\" + "LOCAL CONVEYANCE\\" + txt_Request.Text + "\\"; ;
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

