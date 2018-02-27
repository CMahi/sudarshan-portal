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

public partial class Service_PO_Approval : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    StringBuilder str = new StringBuilder();
    ListItem Li = new ListItem("--Select One--", "");
    DataTable dt = new DataTable();
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
    string DTL_RFC = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Service_PO_Approval));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                    txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    if (Request.QueryString["processid"] != null && Request.QueryString["instanceid"] != null && Request.QueryString["stepid"] != null && Request.QueryString["wiid"] != null)
                    {
                        txtProcessID.Text = Convert.ToString(Request.QueryString["processid"]);
                        txtInstanceID.Text = Convert.ToString(Request.QueryString["instanceid"]);
                        txt_StepId.Text = Convert.ToString(Request.QueryString["stepid"]);
                        txtWIID.Text = Convert.ToString(Request.QueryString["wiid"]);
                        step_name.Text = Convert.ToString(Request.QueryString["step"]);
                    }

                    fillAction();
                    Initialization();
                    fillDocument_Details();
                }
            }
        }
        catch (Exception Exc) { }
    }

    private void Initialization()
    {
        try
        {
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Service_PO_Approval.aspx", "getdetails", ref isdata, txtProcessID.Text, txtInstanceID.Text);
            if (dsData != null)
            {
                txt_pk_id.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PK_SERV_PO_HDR_ID"]);
                spn_req_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["SERV_PO_NO"]);
                txt_Request.Text = Convert.ToString(dsData.Tables[0].Rows[0]["UNIQUE_ID"]);
                spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["PO_SUBMITED_DATE"]).ToString("dd-MMM-yyyy");
                txt_Initiator.Text = Convert.ToString(dsData.Tables[0].Rows[0]["Vendor_Code"]);
                Init_Email.Text = Convert.ToString(dsData.Tables[0].Rows[0]["Email"]);
                Span_invoice_amount.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["INVOICE_AMOUNT"]);
                spn_vendor_name.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["Vendor_Name"]);
                spn_vendor_code.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["Vendor_Code"]);
                txt_Unique_ID.Text = Convert.ToString(dsData.Tables[0].Rows[0]["UNIQUE_ID"]);
                spn_invoice_no.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["INVOICE_NO"]);
                spn_invoice_date.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["INVOICE_DATE"]);
                spn_delvr_note.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["DELIVERY_NOTE"]);

                Span_From.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["FROM_DATE"]);
                Span_To.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["TO_DATE"]);
                Span_Location.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["SERVICE_LOCATION"]);
                Span_Remark.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["Remark"]);


                txt_Vendor.Text = Convert.ToString(dsData.Tables[0].Rows[0]["VENDOR_CODE"]);
                txt_PO.Text = Convert.ToString(dsData.Tables[0].Rows[0]["PO_NUMBER"]);

                StringBuilder HTML = new StringBuilder();
                StringBuilder HTML1 = new StringBuilder();


                HTML.Append("<table id='tble' class='table table-bordered'><thead><tr class='grey'><th>PO No</th><th>Date</th><th>Created By</th><th>PO Type</th><th>PO Value</th><th>PO GV</th><th>Cumulative Amount</th><th>Payment Terms</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dsData != null && dsData.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {
                        string encrypt_Str = crypt.Encryptdata(Convert.ToString(dsData.Tables[0].Rows[i]["PO_NUMBER"]));
                        HTML.Append("<tr><td class='hidden'><input class='form-control' id='txt_Vendor_Name' runat='server' type='text' value=" + dsData.Tables[0].Rows[i]["Vendor_Name"].ToString() + " ></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_Vendor_Code' type='text' value=" + dsData.Tables[0].Rows[i]["VENDOR_CODE"].ToString() + "></td>");
                        HTML.Append("<td><a href='#' role='button' id='anc1' data-toggle='modal' onclick='viewData(" + (i + 1) + ")'>" + dsData.Tables[0].Rows[i]["PO_NUMBER"].ToString() + "</a><input type='text' id='encrypt_po_" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td>");
                        HTML.Append("<td class='hidden'><input class='form-control' id='txt_PO_Number' type='text' value=" + dsData.Tables[0].Rows[i]["PO_NUMBER"].ToString() + "></td>");
                        HTML.Append("<td><input class='hidden' id='txt_PO_Date' type='text' value='" + dsData.Tables[0].Rows[i]["PO_DATE"].ToString() + "'>" + dsData.Tables[0].Rows[i]["PO_DATE"].ToString() + "</td>");
                        HTML.Append("<td><input class='hidden' id='txt_PO_Created_By' type='text' value=" + dsData.Tables[0].Rows[i]["PO_CREATED_BY"].ToString() + ">" + dsData.Tables[0].Rows[i]["PO_CREATED_BY"].ToString() + "</td>");
                        HTML.Append("<td><input class='hidden' id='txt_PO_Type' value='" + dsData.Tables[0].Rows[i]["PO_Type"].ToString() + "'>Service PO</td>");
                        HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_PO_Value' type='text' value=" + dsData.Tables[0].Rows[i]["PO_VALUE"].ToString() + ">" + dsData.Tables[0].Rows[i]["PO_VALUE"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'><input class='hidden' id='txt_tax' type='text' value=" + dsData.Tables[0].Rows[i]["Tax"].ToString() + ">" + dsData.Tables[0].Rows[i]["PO_GV"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'>" + dsData.Tables[0].Rows[i]["Cumulative"].ToString() + "</td>");
                        HTML.Append("<td><input class='hidden' id='txt_Payment_terms' type='text' value=" + dsData.Tables[0].Rows[i]["PAYMENT_TERMS"].ToString() + ">" + dsData.Tables[0].Rows[i]["PAYMENT_TERMS"].ToString() + "</td>");
                        HTML.Append("</tr>");
                    }
                }

                HTML.Append("</tbody>");
                HTML.Append("</table>");



                HTML.Append("<table id='tbl_Detail' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Sr.No</th><th>Material Number</th><th>Plant</th><th>Material Description</th><th>Quantity</th><th>UOM</th><th>Net Price</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dsData != null && dsData.Tables[1].Rows.Count > 0)
                {

                    for (int i = 0; i < dsData.Tables[1].Rows.Count; i++)
                    {

                        HTML.Append("<tr style='text-align:left'><td style='text-align:right'><img id='" + dsData.Tables[1].Rows[i]["FK_SERV_PO_HDR_ID"] + "newimgExpand" + (i + 1) + "' src='../../Img/MoveUp.gif' alt='' style='width:10px; height:auto;' onclick='imgChange(" + (i + 1) + "," + dsData.Tables[1].Rows[i]["FK_SERV_PO_HDR_ID"] + "," + dsData.Tables[1].Rows[i]["FK_DTL_ID"] + ")' </td>");
                        HTML.Append("<td>" + dsData.Tables[1].Rows[i]["PO_LINE_NUMBER"].ToString() + "<input type='hidden' id='txt_po_no" + (i + 1) + "' value='" + dsData.Tables[1].Rows[i]["FK_PO_NUMBER"] + "'><input type='hidden' id='tbl_Detail_tr" + (i + 1) + "' value=" + (i + 1) + "></td>");
                        HTML.Append("<td><input type='hidden' id='txt_Material" + (i + 1) + "' value='" + dsData.Tables[1].Rows[i]["MATERIAL_NO"].ToString() + "'>" + dsData.Tables[1].Rows[i]["MATERIAL_NO"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_plant" + (i + 1) + "' value='" + dsData.Tables[1].Rows[i]["PLANT"].ToString() + "'>" + dsData.Tables[1].Rows[i]["PLANT"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_storage" + (i + 1) + "' value='" + dsData.Tables[1].Rows[i]["MATERIAL_DESC"].ToString() + "'>" + dsData.Tables[1].Rows[i]["MATERIAL_DESC"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_Qunt" + (i + 1) + "' value='" + dsData.Tables[1].Rows[i]["QUANTITY"].ToString() + "'>" + dsData.Tables[1].Rows[i]["QUANTITY"].ToString() + "</td>");
                        HTML.Append("<td><input type='hidden' id='txt_UOM" + (i + 1) + "' value='" + dsData.Tables[1].Rows[i]["UOM"].ToString() + "'>" + dsData.Tables[1].Rows[i]["UOM"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'><input type='hidden' id='txt_NET_Price" + (i + 1) + "' value='" + dsData.Tables[1].Rows[i]["NET_PRICE"].ToString() + "'>" + dsData.Tables[1].Rows[i]["NET_PRICE"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right;display:none'><input type='hidden' id='txt_AMOUNT" + (i + 1) + "' value='" + dsData.Tables[1].Rows[i]["AMOUNT"].ToString() + "'></td>");
                        HTML.Append("</tr>");
                        HTML.Append("<tr style='text-align:left'><td colspan='10' id='" + dsData.Tables[1].Rows[i]["FK_SERV_PO_HDR_ID"] + "NewExpand1" + (i + 1) + "'style='display:none'><div id='" + dsData.Tables[1].Rows[i]["FK_SERV_PO_HDR_ID"] + "NewExpand" + (i + 1) + "' style='display: none'></div></tr>");

                    }
                }
                HTML.Append("</tbody>");
                HTML.Append("</table>");


                div_Header.InnerHtml = HTML.ToString();

                string[] RFCRowArray;
                txt_dtl_rfc.Text = "";
                for (int n = 0; n < dsData.Tables[2].Rows.Count; n++)
                {
                    //string[] RFC_DataArray;
                    //RFC_DataArray = RFCRowArray[n].Split('$');
                    txt_dtl_rfc.Text += txt_Unique_ID.Text + '$' + dsData.Tables[2].Rows[n]["PO_NO"] + '$' + dsData.Tables[2].Rows[n]["PO_LINE_NO"] + '$' + dsData.Tables[2].Rows[n]["MATNR"] + '$' + dsData.Tables[2].Rows[n]["WERKS"] + '$' + dsData.Tables[2].Rows[n]["QUANTITY"] + '$' + "" + '$' + "" + '$' + "" + '$' + "" + '$' + "" + '$' + dsData.Tables[2].Rows[n]["EXTROW"] + '$' + dsData.Tables[2].Rows[n]["SERVPOS"] + '$' + dsData.Tables[2].Rows[n]["SHORT_TEXT"] + '$' + dsData.Tables[2].Rows[n]["QUANTITY"] + '|';
                }

            }
            fillDocument_Details();
            fillAuditTrailData();

        }


        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }


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
                string remark = txtRemark.Value;



                string isInserted = string.Empty;
                string ref_data = string.Empty;
                txt_Audit.Text = "SERVICE PO APPROVAL";
                if (ddlAction.SelectedItem.Text == "Approve")
                {

                    string msg = "";
                    string emailid = string.Empty;
                    CryptoGraphy crypt = new CryptoGraphy();
                    string req_no = crypt.Encryptdata(txt_Request.Text);

                    string SAP_Messsage = RFC_CAll(txt_Vendor.Text, txt_PO.Text, "", "", "", "", "", "", spn_invoice_no.InnerHtml, Convert.ToDateTime(spn_invoice_date.InnerHtml).ToString("yyyyMMdd"), Span_invoice_amount.InnerHtml, spn_delvr_note.InnerHtml, txt_Unique_ID.Text, "", Span_Remark.InnerHtml, spn_invoice_no.InnerHtml, Span_Location.InnerHtml, Convert.ToDateTime(Span_From.InnerHtml).ToString("yyyyMMdd"), Convert.ToDateTime(Span_To.InnerHtml).ToString("yyyyMMdd"), txt_Username.Text, spn_vendor_code.InnerHtml, txt_dtl_rfc.Text);
                    if (SAP_Messsage != "")
                    {
                        string[] array_msg = new string[2];
                        array_msg = SAP_Messsage.Split('$');
                        if (array_msg[0] == "S")
                        {
                            txt_Condition.Text = "1";
                            string isSaved = (string)ActionController.ExecuteAction("", "Service_PO_Approval.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddlAction.SelectedItem.Text, txtApproverEmail.Text, Init_Email.Text);
                            if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                            {
                                divIns.Style["display"] = "none";
                                string[] errmsg = ref_data.Split(':');
                                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                            }
                            else
                            {
                                string[] Dval = new string[1];
                                Dval[0] = "";
                                string ref_data1 = string.Empty;
                                string release_id = (string)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, step_name.Text, "APPROVE");
                                if (release_id != "")
                                {
                                    bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "APPROVE", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);

                                    if (isCreate)
                                    {
                                        try
                                        {

                                            //msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Service PO has been Approved.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";
                                            //emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Service_PO_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "SERVICE PO APPROVAL", "APPROVE", txtApproverEmail.Text, Init_Email.Text, msg, "Request No: " + spn_req_no.InnerHtml);

                                        }
                                        catch (Exception)
                                        {
                                            throw;
                                        }
                                        finally
                                        {
                                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Service PO has been Approved.!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                        }
                                    }
                                }
                                else
                                {
                                    divIns.Style["display"] = "none";
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                                }
                            }

                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + array_msg[1].ToString() + "');window.open('../../Portal/HomePage.aspx','frmset_WorkArea');}</script>");

                        }

                    }


                }
                else if (ddlAction.SelectedItem.Text == "Reject")
                {
                    txt_Condition.Text = "2";
                    txt_Audit.Text = "SERVICE PO APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Service_PO_Approval.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddlAction.SelectedItem.Text, Init_Email.Text, txtEmailID.Text);
                    if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                    {
                        divIns.Style["display"] = "none";
                        string[] errmsg = ref_data.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Username.Text;
                        string ref_data1 = string.Empty;
                        string release_id = (string)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, step_name.Text, "REJECT");
                        if (release_id != "")
                        {
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "REJECT", txt_Username.Text, txt_Username.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    //string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Service PO has been Rejected.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                   // string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Service_PO_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "FOREIGN ADVANCE REQUEST APPROVAL", "REJECT", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Service PO has been Rejected.');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                        else
                        {
                            divIns.Style["display"] = "none";
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
                        }
                    }
                }
                else if (ddlAction.SelectedItem.Text == "Send-Back")
                {
                    txt_Condition.Text = "3";
                    txt_Audit.Text = "SERVICE PO APPROVAL";
                    string isSaved = (string)ActionController.ExecuteAction("", "Service_PO_Approval.aspx", "update", ref ref_data, Convert.ToInt32(txt_Condition.Text), txtProcessID.Text, txtInstanceID.Text, txt_Audit.Text, txt_Username.Text, remark, ddlAction.SelectedItem.Text);
                    if (isSaved == null || ref_data.Length > 0 || isSaved == "false")
                    {
                        divIns.Style["display"] = "none";
                        string[] errmsg = ref_data.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                    }
                    else
                    {
                        string[] Dval = new string[1];
                        Dval[0] = txt_Initiator.Text;
                        string ref_data1 = string.Empty;
                        string release_id = (string)ActionController.ExecuteAction("", "Service_PO_Request.aspx", "getreleaseid", ref ref_data1, txtProcessID.Text, step_name.Text, "SEND-BACK");
                        if (release_id != "")
                        {
                            bool isCreate = (bool)WFE.Action.ReleaseStep(txtProcessID.Text, txtInstanceID.Text, release_id, step_name.Text, "SEND-BACK", txt_Username.Text, txt_Approver.Text.Trim(), "", "", "", "", "", "", "", "", "", "", Dval, txt_Request.Text, txtWIID.Text, ref isInserted);
                            if (isCreate)
                            {
                                try
                                {
                                    string msg = "<pre><font size='3'>Dear Sir/Madam,</font></pre><p/> <pre><font size='3'>Foreign Advance Request has been Sent back to the initiator.</font></pre><p/> <pre><font size='3'>Request No: " + spn_req_no.InnerHtml + "</font></pre> <pre><font size='3'>Created By: " + txt_Initiator.Text.Trim() + "</font></pre></p><pre></b><pre>INTERNET URL:https://" + compname + "/Sudarshan-Portal/Login.aspx</pre><br/><pre>Regards</pre><pre><b>Reporting Admin<b></pre><br/><pre></pre><br/><pre><font size='3'  color='red'><i><b>This is a system generated message. We request you not to reply to this message.</b></i></font></pre>";

                                    string emailid = (string)ActionController.ExecuteAction(txt_Username.Text, "Service_PO_Request.aspx", "insetmaildata", ref isInserted, txtProcessID.Text, txtInstanceID.Text, 0, "SERVICE PO APPROVAL", "SEND-BACK", Init_Email.Text, txtEmailID.Text, msg, "Request No: " + spn_req_no.InnerHtml);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Service PO has been Sent back to the initiator...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                                }
                            }
                        }
                        else
                        {
                            divIns.Style["display"] = "none";
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Release Step Not Found ...!');window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
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

    protected void imgBtnRelease_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('../../Portal/SCIL/TaskDetails.aspx','frmset_WorkArea');}</script>");
        }
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


    private void fillAction()
    {
        try
        {
            string Isdata = string.Empty;
            ListItem li = new ListItem("--Select One--", "");
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_PO_Approval.aspx", "action", ref Isdata);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlAction.DataSource = dt;
                ddlAction.DataTextField = "ACTION_NAME";
                ddlAction.DataValueField = "ACTION_NAME";
                ddlAction.DataBind();
                ddlAction.Items.Insert(0, li);
            }
        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
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

                string isdata = string.Empty;
                string isValid = string.Empty;
                string DisplayData = string.Empty;
                DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getfilenames", ref isdata, "SERVICE PO", spn_req_no.InnerHtml);

                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>File Type</th><th>File Name</th></tr></thead>";
                if (dsData != null)
                {
                    for (int i = 0; i < dsData.Rows.Count; i++)
                    {
                        DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dsData.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a OnClick='downloadfiles(" + (i + 1) + ")'>" + Convert.ToString(dsData.Rows[i]["filename"]) + "</a></td><td style='display:none;'>" + Convert.ToString(dsData.Rows[i]["OBJECT_VALUE"]) + "</td></tr>";
                    }
                }
                DisplayData += "</table>";

                divalldata.InnerHtml = DisplayData.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteEventLog(false, ex);
            }

        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getservicepodtl(Int64 po_no, int id, int pkservid)
    {

        string DisplayData = string.Empty;
        string isValid = string.Empty;
        StringBuilder HTML = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_PO_Approval.aspx", "getservpoitmdtl", ref isValid, po_no, pkservid);
                HTML.Append("<table class='table table-bordered' id='tblservpodtl'><thead><tr class='grey'><th>Sr No.</th><th>Service No.</th><th>Short Text</th><th>Supplied Quantity</th><th>Total</th><th>Net Price</th></tr></thead>");
                HTML.Append("<tbody>");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HTML.Append("<tr>");
                        HTML.Append("<td> " + (i + 1) + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["SERVICE_NO"].ToString() + "</td>");
                        HTML.Append("<td>" + dt.Rows[i]["SHORT_TEXT"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'>" + dt.Rows[i]["QUANTITY"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'>" + dt.Rows[i]["VALUE"].ToString() + "</td>");
                        HTML.Append("<td style='text-align:right'>" + dt.Rows[i]["NET_PRICE"].ToString() + "</td>");
                        // HTML.Append("<td style='text-align:right'>" + dt.Rows[i]["GROSS"].ToString() + "</td>");
                        HTML.Append("</tr>");
                    }
                }
                HTML.Append("</tbody>");
                HTML.Append("</table>");
            }

            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }

        DisplayData = HTML.ToString() + "||" + po_no;
        return DisplayData;
    }

    public string RFC_CAll(string Vendor_code, string PO_Number, string Transpoter_Name, string Vehicle_No, string Contact_Person, string Contact_Person_No, string LR_No, string LR_Date, string Invoice_No, string Invoice_Date, string Invoice_Amount, string Delivery_Note, string Unique_Id, string BUDAT, string TXTZ01, string LBLNE, string DLORT, string LZVON, string LZBIS, string SBNAMAG, string SBNAMAN, string RFC_DTL)
    {

        string SAP_Message = string.Empty;
        Vendor_Portal.Vendor_Portal_DetailsService Dispatch_Note = new Vendor_Portal.Vendor_Portal_DetailsService();
        string[] Dispatch_Array = new string[2];
        Dispatch_Array = Dispatch_Note.PO_VENDOR_UNIQUE_ID(Vendor_code, PO_Number, Transpoter_Name, Vehicle_No, Contact_Person, Contact_Person_No, LR_No, LR_Date, Invoice_No, Invoice_Date, Invoice_Amount, Delivery_Note, Unique_Id, BUDAT, TXTZ01, LBLNE, DLORT, LZVON, LZBIS, SBNAMAG, SBNAMAN, RFC_DTL);
        SAP_Message = Dispatch_Array[1] + '$' + Dispatch_Array[0];
        return SAP_Message;
    }
}

