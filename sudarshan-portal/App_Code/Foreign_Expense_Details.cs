using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AjaxPro;
using FSL.Logging;
using FSL.Controller;
using FSL.Message;

/// <summary>
/// Summary description for Foreign_Expense_Details
/// </summary>
public class Foreign_Expense_Details
{
	public Foreign_Expense_Details()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string Foreign_Expense_Request_Details(int wiid)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            string desg,desgid;
            string isdata = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "pgetrequestdata", ref isdata, wiid);
            if (dsData != null)
            {
                string pk_id = Convert.ToString(dsData.Tables[0].Rows[0]["pk_travel_expense_hdr_id"]);
                string initiator=Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]);
                string jfdate=Convert.ToDateTime(dsData.Tables[0].Rows[0]["from_date"]).ToString("dd-MMM-yyyy");
                string jtdate=Convert.ToDateTime(dsData.Tables[0].Rows[0]["to_date"]).ToString("dd-MMM-yyyy");
                
                int country_id = Convert.ToInt32(dsData.Tables[0].Rows[0]["fk_Country"]);


                DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "pgettraveluser", ref isdata, initiator);
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    desg = Convert.ToString(dtUser.Rows[0]["DESG_NAME"]);
                    desgid = Convert.ToString(dtUser.Rows[0]["FK_DESIGNATIONID"]);
                }



                sb.Append("<div class='row'>");
                sb.Append("<div class='col-md-12'><div class='panel panel-danger'>");
                sb.Append("<div class='panel-heading'><h3 class='panel-title'>FOREIGN TRAVEL REQUEST DETAILS</h3></div>");
                sb.Append("<div class='panel-body' id='div_hdr'><div class='form-horizontal'>");
                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Request No</label>");
                sb.Append("<div class='col-md-3'><div id='Div8'><span id='spn_req_no' runat='server'>"+Convert.ToString(dsData.Tables[0].Rows[0]["REQUEST_NO"])+"</span></div></div><label class='col-md-2'>Date</label>");
                sb.Append("<div class='col-md-3'><div id='Div11'><span id='spn_date' runat='server'>" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy") + "</span></div></div><div class='col-md-1'></div></div>");

                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Travel From Date</label>");
                sb.Append("<div class='col-md-2'><div id='Div13'><span id='travelFromDate' runat='server'>"+jfdate+"</span></div></div><div class='col-md-1'></div>");
                sb.Append("<label class='col-md-2'>Travel To Date</label><div class='col-md-2'><div id='Div14'><span id='travelToDate' runat='server'>"+jtdate+"</span></div>");
                sb.Append("</div><div class='col-md-1'></div></div>");

                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Country</label>");
                sb.Append("<div class='col-md-2'><div id='Div15'><span id='spn_Country' runat='server'>"+Convert.ToString(dsData.Tables[0].Rows[0]["COUNTRY_NAME"])+"</span></div></div><div class='col-md-1'></div>");
                sb.Append("<label class='col-md-2'>Supporting Documents</label><div class='col-md-3'>");
                sb.Append("<a href='#div_UploadDocument' data-toggle='modal'><img id='A_FileUpload1' src='../../images/attachment.png' alt='Click here to view attached file.' height='27px' width='27px'></a></div></div>");

                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label><div class='col-md-2'><span id='req_remark' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["remark"]) + "</span></div></div>");
                if (Convert.ToString(dsData.Tables[0].Rows[0]["DO_APPROVAL_FLAG"]).ToUpper() == "YES")
                {
                    string[] dev_string = Convert.ToString(dsData.Tables[0].Rows[0]["DEVIATION_REASON"]).Split(',');
                    string d_string = "";
                    for (int l = 0; l < dev_string.Length; l++)
                    {
                        if (dev_string[l].ToString().Trim() != "")
                        {
                            d_string += "<p>" + dev_string[l] + "</p>";
                        }
                    }
                    sb.Append("<div class='form-group' id='div_deviated' runat='server'><div class='col-md-1'></div>");
                    sb.Append("<label class='col-md-2'>Deviated Reason :</label><div class='col-md-8'><span id='span_deviate' runat='server'>" + d_string + "</span></div><div class='col-md-1'></div></div>");
                }
                sb.Append("</div></div></div></div>");

                sb.Append("<div class='modal fade' id='div_UploadDocument'><div class='modal-dialog' style='width: 45%; margin-left: 25%; margin-top:5%'><div class='modal-content'>");
                sb.Append("<div class='modal-header'><a href='javascript:;' class='close' onclick='close_Child_Popup()'>×</a><h4 class='modal-title'><font color='white'><b>File Attachment </b> </font></h4></div>");
                sb.Append("<div class='modal-body'><div class='table-responsive' id='div_docs' runat='server'>"+fillDocument_Details(dsData.Tables[3])+"</div></div>");
                sb.Append("<div class='modal-footer'><a href='javascript:;' class='btn btn-sm btn-danger' onclick='close_Child_Popup()'>Close</a></div></div></div></div>");


                sb.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>Advance Details</h4></div>");
                sb.Append("<div class='panel-body'><div id='div_Advance' runat='server'>"+fillAdvanceAmount(initiator,pk_id)+"</div></div></div</div>");

                sb.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><div class='panel-heading-btn'>");
                sb.Append("<span id='Final_Amtt' style='display:none'>0 <i class='fa fa-rupee'></i></span></div><h3 class='panel-title'>Request Details</h3></div>");
                sb.Append("<div class='panel-body'><div class='panel-group' id='accordion'>" + getDataRows(jfdate, jtdate, country_id, wiid, Convert.ToString(dtUser.Rows[0]["GRADE_NAME"])) + "</div></div></div></div></div>");
                    
            }
            
        
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return sb.ToString();
    }

    public string fillDocument_Details(DataTable dt)
    {
        string DisplayData = string.Empty;
            try
            {
                string isData = string.Empty;
                string isValid = string.Empty;
                
                DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th>File Name</th><th>Delete</th></tr></thead>";
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DisplayData += "<tr>";
                            DisplayData += "<td>" + (i + 1) + "</td>";
                            DisplayData += "<td><input type='text' id='anc_" + (i + 1) + "' value='" + Convert.ToString(dt.Rows[i]["FileName"]) + "' style='display:none'/>  <a href='#' onclick=download_files('" + Convert.ToString(dt.Rows[i]["OBJECT_VALUE"]) + "','" + Convert.ToString(dt.Rows[i]["FileName"]) + "')>" + Convert.ToString(dt.Rows[i]["FileName"]) + "</td>";
                            DisplayData += "</tr>";
                        }
                    }
                }
                DisplayData += "</table>";
                //div_docs.InnerHtml = DisplayData;
                //DisplayData = "";
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
            return DisplayData;
    }

    public string fillAdvanceAmount(string initiator,string pk_id)
    {
        string data = string.Empty;
        StringBuilder tblHTML = new StringBuilder();
        try
        {
            string isValid = string.Empty;
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "pgetadvancedetails", ref isValid, initiator, pk_id, 2);
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Currency</th><th>Currency Amount</th><th>Forex Card</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
                {
                    tblHTML.Append("<tr>");
                    tblHTML.Append("<td>" + (Index + 1) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["ADVANCE_DATE"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["CURRENCY"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["CURRENCY_AMOUNT"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["FOREX_CARD"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["Approved"]) + "</td>");
                    tblHTML.Append("</tr>");
                }
            }
            else
            {
                tblHTML.Append("<tr><td colspan='5' align='center'>Advance Not Available</td></tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            //div_Advance.InnerHtml = tblHTML.ToString();

        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
        return tblHTML.ToString();
    }

    public string getDataRows(string jFDate, string jTDate, int country_id, int wiid, string desg)
    {
        StringBuilder sb = new StringBuilder();
        string isdata = string.Empty;
        string row_data = "";
        DateTime fdate = Convert.ToDateTime(jFDate);
        DateTime tdate = Convert.ToDateTime(jTDate);
        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "pgetrequestdata", ref isdata, wiid);
        int noofdays = 0;
        try
        {
            while (fdate <= tdate)
            {
                noofdays = noofdays + 1;
                fdate = fdate.AddDays(1);
            }
            if (dsData != null)
            {
                string pk_id = Convert.ToString(dsData.Tables[0].Rows[0]["pk_travel_expense_hdr_id"]);
                string currency = Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]);
                double UEx_Rate = Convert.ToDouble(dsData.Tables[0].Rows[0]["exc_rate"]);
                sb.Append("<div class='panel table-responsive'>");
                sb.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
                sb.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' name='rs' href='#collapse0'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span>Summary</span></a></h3>");
                sb.Append("</div>");

                sb.Append("<div id='collapse0' class='panel-collapse collapse in'><div class='panel-body' style='background-color:#ffffff'><div class='form-horizontal'>");

                sb.Append("<div class='form-group'>");
                sb.Append("<table class='table table-bordered'>");
                sb.Append("<thead class='grey'><th>Particulars</th>");
                DataTable rt = (DataTable)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getreimbursements", ref isdata, "International Travel Expenses");
                string[] re_id = new string[0];
                decimal[] exp_total = new decimal[0];

                if (rt != null)
                {
                    re_id = new string[rt.Rows.Count];
                    exp_total = new decimal[rt.Rows.Count];
                    for (int r = 0; r < rt.Rows.Count; r++)
                    {
                        re_id[r] = Convert.ToString(rt.Rows[r]["PK_REIMBURSEMNT_ID"]);
                        sb.Append("<th>" + Convert.ToString(rt.Rows[r]["REIMBURSEMENT_TYPE"]) + "</th>");
                    }
                }
                sb.Append("<th>Total</th>");
                sb.Append("<th>Eligible</th>");
                sb.Append("<th>Excess</th>");
                sb.Append("</thead>");
                sb.Append("</tbody>");

                double total, el_total, ex_total;
                total = el_total = ex_total = 0;

                string[] exp = new string[0];
                DataSet Exp_dat = (DataSet)ActionController.ExecuteAction("", "Foreign_Travel_Request.aspx", "getexpenseids", ref isdata);
                if (Exp_dat != null)
                {
                    DataTable Inc_Con_Dt = new DataTable();
                    DataTable ejm = new DataTable();
                    ejm = Exp_dat.Tables[2];
                    exp = new string[ejm.Rows.Count];
                    for (int e = 0; e < ejm.Rows.Count; e++)
                    {
                        string exp_id = Convert.ToString(ejm.Rows[e]["FK_EXPENSE_HEAD"]);
                        string exp_name = Convert.ToString(ejm.Rows[e]["EXPENSE_HEAD"]).ToUpper();
                        sb.Append("<tr>");
                        sb.Append("<td>" + Convert.ToString(ejm.Rows[e]["EXPENSE_HEAD"]) + "</td>");

                        for (int r = 0; r < rt.Rows.Count; r++)
                        {
                            string rid = Convert.ToString(re_id[r]);
                            string amount = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getexpenseamount", ref isdata, exp_id, pk_id, rid);
                            sb.Append("<td style='text-align:right'>" + amount + "</td>");
                        }
                        string exp_amount = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getexpheadwiseamount", ref isdata, exp_id, pk_id);
                        if (Convert.ToDouble(exp_amount) != 0)
                        {
                            sb.Append("<td style='text-align:right'>" + Convert.ToDouble(exp_amount).ToString("#.00") + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style='text-align:right'>0.00</td>");
                        }
                        total = total + Convert.ToDouble(exp_amount);
                        string allowed_amount = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getallowedamount", ref isdata, country_id, desg, Convert.ToString(ejm.Rows[e]["EXPENSE_HEAD"]));
                        if (Convert.ToDouble(Convert.ToInt32(allowed_amount) * noofdays) != 0)
                        {
                            sb.Append("<td style='text-align:right'>" + Convert.ToDouble(Convert.ToInt32(allowed_amount) * noofdays).ToString("#.00") + "</td>");

                        }
                        else
                        {
                            sb.Append("<td style='text-align:right'>0.00</td>");
                        }

                        el_total = el_total + Convert.ToDouble(Convert.ToInt32(allowed_amount) * noofdays);

                        ex_total = total - el_total;

                        if (Convert.ToDouble(Convert.ToDouble(exp_amount) - Convert.ToDouble(Convert.ToDouble(allowed_amount) * Convert.ToDouble(noofdays))) != 0)
                        {
                            if (Convert.ToDouble(exp_amount) != 0)
                            {
                                if (Convert.ToDouble(Convert.ToDouble(exp_amount) - Convert.ToDouble(Convert.ToDouble(allowed_amount) * Convert.ToDouble(noofdays))) < 0)
                                {
                                    sb.Append("<td style='text-align:right;'>" + Convert.ToDouble(Convert.ToDouble(exp_amount) - Convert.ToDouble(Convert.ToDouble(allowed_amount) * Convert.ToDouble(noofdays))).ToString("#.00") + "</td>");
                                }
                                else
                                {
                                    sb.Append("<td style='text-align:right;color:red'>" + Convert.ToDouble(Convert.ToDouble(exp_amount) - Convert.ToDouble(Convert.ToDouble(allowed_amount) * Convert.ToDouble(noofdays))).ToString("#.00") + "</td>");
                                }
                            }
                            else
                            {
                                sb.Append("<td style='text-align:right'>0.00</td>");
                            }

                        }

                        else
                        {
                            sb.Append("<td style='text-align:right'>0.00</td>");
                        }
                        sb.Append("</tr>");
                    }
                }

                sb.Append("<tr style='font-weight:bold'>");
                sb.Append("<td>Total </td>");
                for (int r = 0; r < re_id.Length; r++)
                {
                    string rid = re_id[r];
                    string amount = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getreimbursementamount", ref isdata, pk_id, rid);
                    sb.Append("<td style='text-align:right'>" + amount + "</td>");
                }
                sb.Append("<td style='text-align:right'>" + Convert.ToDouble(total).ToString("#.00") + "</td>");
                sb.Append("<td style='text-align:right'>" + Convert.ToDouble(el_total).ToString("#.00") + "</td>");
                sb.Append("<td style='text-align:right'>" + Convert.ToDouble(ex_total).ToString("#.00") + "</td>");

                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");

                sb.Append("<table class='table table-bordered' style='width:100%'>");
                sb.Append("<tbody>");
                sb.Append("<tr style='font-weight:bold'>");
                sb.Append("<td>Advance Taken : </td>");
                sb.Append("<td>" + Convert.ToString(dsData.Tables[4].Rows[0]["adv_amount"]) + "</td>");
                sb.Append("<td>Total Reimbursement Amount <p>(Excluding Corporate Credit Card & Company Provided)</p> </td>");
                sb.Append("<td>" + Convert.ToString(dsData.Tables[4].Rows[0]["Tot_Amount"]) + "</td>");
                sb.Append("<td>Difference : </td>");
                sb.Append("<td>" + (Convert.ToDecimal(dsData.Tables[4].Rows[0]["adv_amount"]) - Convert.ToDecimal(dsData.Tables[4].Rows[0]["Tot_Amount"])) + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr style='font-weight:bold'>");
                sb.Append("<td>Non Supporting Amount : </td>");
                sb.Append("<td>" + Convert.ToString(dsData.Tables[4].Rows[0]["non_supporting"]) + "</td>");
                sb.Append("<td>Return Amount : </td>");
                if (Convert.ToString(dsData.Tables[0].Rows[0]["Return_Money"]) == "" || Convert.ToString(dsData.Tables[0].Rows[0]["Return_Money"]) == "0")
                {
                    sb.Append("<td>0.00</td>");
                }
                else
                {
                    sb.Append("<td>" + Convert.ToDecimal(dsData.Tables[0].Rows[0]["Return_Money"]).ToString("#.00") + "</td>");
                }

                sb.Append("</tr>");

                sb.Append("</tbody>");
                sb.Append("</table>");
                sb.Append("</div>");


                sb.Append("</div></div></div></div>");

                fdate = Convert.ToDateTime(jFDate);
                tdate = Convert.ToDateTime(jTDate);
                int index = 1;
                while (fdate <= tdate)
                {
                    sb.Append("<div class='panel table-responsive'>");
                    sb.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
                    string amount = (string)ActionController.ExecuteAction("", "Foreign_Travel_Request_Approval.aspx", "getexpenseamountdaywise", ref isdata, pk_id, fdate);
                    sb.Append("<div class='panel-heading-btn'><div>Amount (" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]) + ") : <span id='row_Total" + index + "'>" + amount + "</span></div></div>");
                    sb.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' name='rs' href='#collapse" + index + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spn_date" + index + "'>" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + "</span></a></h3>");
                    sb.Append("</div>");

                    sb.Append("<div id='collapse" + index + "' class='panel-collapse collapse'><div class='panel-body' style='background-color:#ffffff'><div class='form-horizontal'>");

                    sb.Append(bindExpense(index, fdate, dsData.Tables[2], currency, dsData.Tables[1], dsData.Tables[5]));
                    fdate = fdate.AddDays(1);
                    index = index + 1;


                    sb.Append("</div></div></div></div>");
                }
            }
            row_data = sb.ToString();
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return row_data;
    }

    protected string bindExpense(int index, DateTime fdate, DataTable dtExp, string currency, DataTable dtDay, DataTable Loc_travel)
    {
        string exp_data = "";
        StringBuilder reim = new StringBuilder();
        StringBuilder html = new StringBuilder();
        try
        {

            /*****************************************************************************************************************************/
            html.Append("<div class='form-group'>");
            html.Append("<div class='col-md-1'></div>");
            html.Append("<div class='col-md-2'>City</div>");
            if (Convert.ToString(dtDay.Rows[index - 1]["FK_CITY"]) != "-1")
            {
                html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["CITY_NAME"]) + "</div>");
            }
            else
            {
                html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["OTHER_CITY"]) + "</div>");
            }
            html.Append("<div class='col-md-3'></div>");
            html.Append("<div class='col-md-2'>Particulars</div>");
            html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["remark_note"]) + "</div>");
            html.Append("</div>");

            html.Append("<div class='form-group'>");
            html.Append("<div class='col-md-1'></div>");
            html.Append("<div class='col-md-2'>Hotel Name</div>");
            html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["Hotel_name"]) + "</div>");
            html.Append("<div class='col-md-3'></div>");
            html.Append("<div class='col-md-2'>Hotel Contact</div>");
            html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["Hotel_No"]) + "</div>");
            html.Append("</div>");

            html.Append("<div class='form-group'>");
            html.Append("<div class='col-md-1'></div>");
            html.Append("<div class='col-md-2'>Travel Class</div>");
            html.Append("<div class='col-md-2'>" + Convert.ToString(dtDay.Rows[index - 1]["Travel_Class"]) + "</div>");
            html.Append("</div>");
            /*****************************************************************************************************************************/


            html.Append("<div class='form-group'></div>");
            html.Append("<div class='col-md-12'>");
            html.Append("<div class='col-md-12'>");
            html.Append("<table class='table table-bordered'>");
            html.Append("<thead><tr class='grey'>");
            html.Append("<th>Expense Head</th><th>Reimbursement Type</th><th>Expense Amount</th><th>Tax</th><th>Exchange Rate</th><th>Amount (" + currency + ")</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Supporting Doc</th><th>Supporting Particulars</th><th>Remark</th>");
            html.Append("</tr></thead>");
            html.Append("<tbody>");

            for (int row = 0; row < dtExp.Rows.Count; row++)
            {
                if (Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") == Convert.ToDateTime(dtExp.Rows[row]["fk_travel_date"]).ToString("dd-MMM-yyyy"))
                {
                    html.Append("<tr>");

                    html.Append("<td>" + Convert.ToString(dtExp.Rows[row]["expense_head"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(dtExp.Rows[row]["reimbursement_type"]) + "</td>");
                    if (Convert.ToString(dtExp.Rows[row]["C_CURRENCY"]) != "0")
                    {
                        html.Append("<td style='text-align:right;'>" + Convert.ToString(dtExp.Rows[row]["C_CURRENCY"]) + "</td>");
                    }
                    else
                    {
                        html.Append("<td style='text-align:right;'></td>");
                    }
                    html.Append("<td style='text-align:right;'>" + Convert.ToString(dtExp.Rows[row]["TAX"]) + "</td>");
                    html.Append("<td style='text-align:right;'>" + Convert.ToString(dtExp.Rows[row]["E_RATE"]) + "</td>");
                    html.Append("<td style='text-align:right;'>" + Convert.ToString(dtExp.Rows[row]["D_CURRENCY"]) + "</td>");
                    html.Append("<td style='text-align:right;'>" + Convert.ToString(dtExp.Rows[row]["I_CURRENCY"]) + "</td>");
                    if (Convert.ToString(dtExp.Rows[row]["expense_head"]) != "Incidental")
                    {
                        if (Convert.ToString(dtExp.Rows[row]["IS_SUPPORTING"]) == "Y")
                        {
                            html.Append("<td>" + Convert.ToString(dtExp.Rows[row]["supp_doc1"]) + "</td>");
                            html.Append("<td>");
                            if (Convert.ToString(dtExp.Rows[row]["supp_doc1"]) == "YES")
                            {
                                html.Append(Convert.ToString(dtExp.Rows[row]["supp_remark"]));
                            }
                            else
                            {
                                html.Append("");
                            }
                            html.Append("</td>");
                        }
                        else
                        {
                            html.Append("<td>" + Convert.ToString(dtExp.Rows[row]["supp_doc1"]) + "</td>");
                            html.Append("<td></td>");

                        }
                    }
                    else
                    {
                        html.Append("<td></td>");
                        html.Append("<td></td>");
                    }

                    html.Append("<td>" + Convert.ToString(dtExp.Rows[row]["other_remark"]) + "</td>");
                    html.Append("</tr>");
                }
            }
            html.Append("</tbody></table>");
            html.Append("</div>");
            html.Append("<div class='col-md-1'></div>");
            html.Append("</div>");
            html.Append("<div class='form-group'></div>");
            if (Loc_travel.Rows.Count > 0)
            {
                html.Append("<div class='col-md-12'>");
                html.Append("<div class='col-md-12 grey'>Local Travel Details");
                html.Append("<table class='table table-bordered' id='tbl_Local_Travel" + index + "'>");
                html.Append("<thead><tr class='grey'><th>Date</th><th>Reimbursement Type</th><th>From</th><th>To</th><th>Mode of Travel</th><th>Expense Amount</th><th>Tax</th><th>Exchange Rate</th><th>Amount (" + currency + ")</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Remark</th><th>Bills attached Yes/No</th></tr></thead>");
                html.Append("<tbody>");

                for (int k = 0; k < Loc_travel.Rows.Count; k++)
                {
                    if (Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") == Convert.ToDateTime(Loc_travel.Rows[k]["fk_travel_date"]).ToString("dd-MMM-yyyy"))
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + Loc_travel.Rows[k]["fk_travel_date"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["REIMBURSEMENT_TYPE"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["TRAVEL_FROM"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["TRAVEL_TO"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["TRAVEL_MODE"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["EXPENSES"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["TAX"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["CONVERSION_RATE"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["BASE_EXP_AMOUNT"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["INR_EXP_AMOUNT"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["REMARK"].ToString() + "</td>");
                        html.Append("<td>" + Loc_travel.Rows[k]["BILL_STATUS"].ToString() + "</td>");
                        html.Append("</tr>");
                    }
                }
                html.Append("</tbody></table></div>"); html.Append("</div>");
            }
            exp_data = Convert.ToString(html);

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return exp_data;
    }

    /***************************************************************************************************************************************************/

    public string Foreign_Advance_Details(int processid, int instanceid)
    { 
         StringBuilder sb = new StringBuilder();
         try
         {
             string IsData = string.Empty;

             DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Foreign_Advance_Request_Approval.aspx", "addetails", ref IsData, processid, instanceid);
             if (dsData != null)
             {
                 if (dsData.Tables[0].Rows.Count > 0)
                 {
                     sb.Append("<div class='col-md-12' id='advance_Foreign' runat='server'>");
                     sb.Append("<div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-align-justify'></i>Advance Request Details</h4></div>");
                     sb.Append("<div class='panel-body'><div class='form-horizontal'>");

                     sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Request No.</label><div class='col-md-3'><span id='spn_req_no' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["request_no"]) + "</span></div>");
                     sb.Append("<label class='col-md-2'>Voucher Date</label><div class='col-md-2'><span id='vdate' runat='server'>" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["CREATION_DATE"]).ToString("dd-MMM-yyyy") + "</span></div><div class='col-md-1'></div></div>");

                     sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Region From</label><div class='col-md-3'><span id='spn_F_Region' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["F_COUNTRY"]) + "</span></div>");
                     sb.Append("<label class='col-md-2'>Region To</label><div class='col-md-2'><span id='spn_T_Region' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["T_COUNTRY"]) + "</span></div><div class='col-md-1'></div></div>");

                     sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>City From</label><div class='col-md-3'><span id='spn_F_City' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["F_CITY"]) + "</span></div>");
                     sb.Append("<label class='col-md-2'>City To</label><div class='col-md-3'><span id='spn_T_City' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["T_CITY"]) + "</span></div><div class='col-md-1'></div></div>");

                     sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Base Currency</label><div class='col-md-3'><span id='pk_base_currency' runat='server' style='display:none'></span>");
                     sb.Append("<span id='base_currency' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY_NAME"]) + "</span><span id='base_currency_rate' runat='server' style='display:none'>0</span></div></div>");

                     sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>From Date</label><div class='col-md-2'><span id='spn_F_Date' runat='server'>" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["FROM_DATE"]).ToString("dd-MMM-yyyy") + "</span></div>");
                     sb.Append("<div class='col-md-1'></div><label class='col-md-2'>To Date</label><div class='col-md-2'><span id='spn_T_Date' runat='server'>" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["TO_DATE"]).ToString("dd-MMM-yyyy") + "</span></div><div class='col-md-1'></div></div>");

                     int days = 0;
                     string limit = Convert.ToString(dsData.Tables[0].Rows[0]["FTE_AMOUNT"]);
                     var F_DATE = Convert.ToDateTime(dsData.Tables[0].Rows[0]["FROM_DATE"]).ToString("dd-MMM-yyyy");
                     var T_DATE = Convert.ToDateTime(dsData.Tables[0].Rows[0]["FROM_DATE"]).ToString("dd-MMM-yyyy");
                     DateTime fdate = Convert.ToDateTime(F_DATE);
                     DateTime tdate = Convert.ToDateTime(T_DATE);
                     days = Convert.ToInt32((tdate - fdate).TotalDays) + 1;
                     string allowed = Convert.ToString(days * Convert.ToInt32(limit));

                     sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Allowed Amount</label><div class='col-md-2' ><asp:Label ID='lbl_allowedamt' runat='server' Text='" + allowed + "'></asp:Label></div>");
                     sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Advance Amount</label><div class='col-md-2'><span id='req_base_currency' runat='server'>" + Convert.ToString(dsData.Tables[3].Rows[0]["REQ_AMOUNT"]) + "</span></div><div class='col-md-1'></div></div>");

                     sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label><div class='col-md-7'><span id='spn_Remark' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["REMARK"]) + "</span></div></div>");

                     sb.Append("<div class='form-group'><div class='col-md-12' style='text-align:center'><hr />");
                     sb.Append("<div class='table-responsive' id='div_details' runat='server'>"+bind_Line_Item(dsData.Tables[1])+"</div></div></div></div></div></div>");
                 }
             }
         }
         catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return sb.ToString();
    }

    public string bind_Line_Item(DataTable dtLine)
    {
        string data = "";
        data = "<table class='table table-bordered' id='tbl_Data'><thead>";
        data += "<tr class='grey'><th style='width: 2%; text-align: center'>#</th><th style='width: 10%; text-align: center'>Requested Currency</th>";
        data += "<th style='width: 10%; text-align: center'>Currency Mode</th><th style='width: 10%; text-align: center'>Currency Amount</th>";
        data += "<th style='width: 10%; text-align: center'>Exchange Rate</th><th style='width: 15%; text-align: center'>Equivalent To Base Currency</th>";
        data += "</tr></thead><tbody>";
        if (dtLine != null && dtLine.Rows.Count > 0)
        {
            for (int index = 0; index < dtLine.Rows.Count; index++)
            {
                data += "<tr>";
                data += "<td>" + Convert.ToString(index + 1) + "</td>";
                data += "<td>" + Convert.ToString(dtLine.Rows[index]["CURRENCY_NAME"]) + "</td>";
                data += "<td>" + Convert.ToString(dtLine.Rows[index]["CURRENCY_MODE"]) + "</td>";
                data += "<td>" + Convert.ToString(dtLine.Rows[index]["CURRENCY_AMOUNT"]) + "</td>";
                data += "<td>" + Convert.ToString(dtLine.Rows[index]["EXC_RATE_IN_INR"]) + "</td>";
                data += "<td>" + Convert.ToString(dtLine.Rows[index]["EQ_BASE_AMOUNT"]) + "</td>";
                data += "</tr>";
            }
        }
        data += "</tbody></table>";
        return data;
    }
}