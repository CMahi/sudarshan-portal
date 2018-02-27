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

public partial class Foreign_Voucher : System.Web.UI.Page
{
    CryptoGraphy crypt = new CryptoGraphy();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Foreign_Voucher));
            if (!Page.IsPostBack)
            {
                if (Convert.ToString(Request.QueryString["R"]) != "")
                {
                    string process_name = Convert.ToString(Request.QueryString["P"]);
                    string req_no = Convert.ToString(Request.QueryString["R"]);

                    //process_name = crypt.Decryptdata(process_name);
                    //req_no = crypt.Decryptdata(req_no);
                    process_name = "FOREIGN TRAVEL EXPENSE";
                    //req_no = "FT-201617-35";
                    bindData(process_name, req_no);
                }
            }

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void bindData(string process, string req_no)
    {
        string isData = "";
        StringBuilder sb = new StringBuilder();
        DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Bank_Voucher.aspx", "getreqdetails", ref isData, req_no, process);
        if (dsData != null)
        {
            string process_id = Convert.ToString(dsData.Tables[0].Rows[0]["FK_PROCESS_ID"]);
            string instance_id = Convert.ToString(dsData.Tables[0].Rows[0]["FK_INSTANCE_ID"]);
            spn_Unq_No.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["voucher_id"]);
            spn_date.InnerHtml = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
            spn_Emp_Code.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["emp_id"]);
            spn_Emp_Name.InnerHtml = spn_Emp_Name2.InnerHtml = Convert.ToString(dsData.Tables[0].Rows[0]["employee_name"]);
            string userid = Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]);
            string cost_center = Convert.ToString(dsData.Tables[0].Rows[0]["KOSTL"]);
            string narration = Convert.ToString(dsData.Tables[2].Rows[0]["Narration"]);
            string deviated = Convert.ToString(dsData.Tables[0].Rows[0]["deviated"]);
            if (deviated == "1")
            {
                tdDev1.Style.Add("display", "normal");
                tdDev2.Style.Add("display", "normal");
            }
            else
            {
                tdDev1.Style.Add("display", "none");
                tdDev2.Style.Add("display", "none");
            }

            DataTable dtUser = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgettraveluser", ref isData, userid);
            if (dtUser.Rows.Count > 0)
            {
                spn_Emp_Code.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMP_ID"]);
                spn_Emp_Name.InnerHtml = spn_Emp_Name2.InnerHtml = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
                if (Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]).Trim() != "")
                {
                    spn_Acc_No.InnerHtml = Convert.ToString(dtUser.Rows[0]["BANK_ACCOUNT_NO"]);
                }
                else
                {
                    spn_Acc_No.InnerHtml = "NA";
                }
                if (Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]).Trim() != "")
                {
                    spn_IFSC_Code.InnerHtml = Convert.ToString(dtUser.Rows[0]["IFSC_CODE"]);
                }
                else
                {
                    spn_IFSC_Code.InnerHtml = "NA";
                }
                if (Convert.ToString(dtUser.Rows[0]["BANK_NAME"]).Trim() != "")
                {
                    spn_Bank_Name.InnerHtml = Convert.ToString(dtUser.Rows[0]["BANK_NAME"]);
                }
                else
                {
                    spn_Bank_Name.InnerHtml = "NA";
                }
            }
            DataTable dtApprover = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgettravelrequestapprover", ref isData, userid);
            if (dtApprover != null)
            {
                if (dtApprover.Rows.Count > 0)
                {
                    if (Convert.ToString(dtApprover.Rows[0]["approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["approver"]) != "0")
                    {
                        spn_Approver.InnerHtml = Convert.ToString(dtApprover.Rows[0]["approver_name"]);
                    }
                    else
                    {
                        spn_Approver.InnerHtml = "NA";
                    }

                    if (Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "" && Convert.ToString(dtApprover.Rows[0]["doa_approver"]) != "0")
                    {
                        spn_Dev_Approver.InnerHtml = Convert.ToString(dtApprover.Rows[0]["dapprover_name"]);
                    }
                    else
                    {
                        spn_Dev_Approver.InnerHtml = "NA";
                    }
                }
                else
                {
                    spn_Approver.InnerHtml = spn_Dev_Approver.InnerHtml = "NA";
                }
            }

            double tot_Amount1 = 0;
            for (int i = 0; i < dsData.Tables[1].Rows.Count; i++)
            {
                tot_Amount1 = tot_Amount1 + Convert.ToDouble(dsData.Tables[1].Rows[i]["Amount"]);
            }
            sb.Append("<table><tbody>");
            sb.Append("<tr><td colspan='3'><b>Expense Claim Summary : </b></td></tr>");

            sb.Append("<tr style='text-align:center'>");
            sb.Append("<td class='clsBorder td2' style='border: 1px solid black;'>Currency</td>");
            sb.Append("<td class='clsBorder td2' style='border: 1px solid black;'>Total expenses claimed</td>");
            sb.Append("<td class='clsBorder td2' style='border: 1px solid black;'>Advance, if any</td>");
            sb.Append("<td class='clsBorder td2' style='border: 1px solid black;'>Net Paid/Received</td></tr>");


            //if (netpay < 0)
            //{
            //    netpay = 0;
            //}
            decimal adv_amt = 0;
            if (Convert.ToString(dsData.Tables[0].Rows[0]["adv_amount"]) == "")
            {
                adv_amt = 0;
            }
            else
            {
                if (spn_Unq_No.InnerHtml.ToUpper().Contains("MCE"))
                {
                    adv_amt = 0;
                }
                else
                {
                    adv_amt = Convert.ToDecimal(dsData.Tables[0].Rows[0]["adv_amount"]);
                }
            }
            if (spn_Unq_No.InnerHtml.ToUpper().Contains("MCE") && Convert.ToString(dsData.Tables[0].Rows[0]["adv_amount"]) == "DataCard")
            {
                spn_Approver.InnerHtml = "Uday Hardikar";
            }

            decimal netpay = Convert.ToDecimal(tot_Amount1) - adv_amt;

            if (req_no.Contains("ADV"))
            {
                sb.Append("<tr style='text-align:center'>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black; text-align:center'>" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]) + "</td>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black; text-align:center'>0</td>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black; text-align:center'>" + Convert.ToDecimal(tot_Amount1).ToString("#.00") + "</td>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black; text-align:center'>0.00</td></tr>");
            }
            else
            {
                sb.Append("<tr style='text-align:center'>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black; text-align:center'>" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]) + "</td>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black; text-align:center'>" + Convert.ToDecimal(tot_Amount1).ToString("#.00") + "</td>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black; text-align:center'>" + Convert.ToDecimal(adv_amt).ToString("#.00") + "</td>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black; text-align:center'>" + Convert.ToDecimal(netpay).ToString("#.00") + "</td></tr>");
            }
            sb.Append("</tbody></table><br />");

            sb.Append("<table style='width:90%'>");
            sb.Append("<thead><tr class='grey' style='text-align: center'><td class='clsBorder td1' rowspan='2' style='border: 1px solid black;'>PARTICULARS</td>");
            sb.Append("<td class='clsBorder td1' rowspan='2' style='border: 1px solid black;'>BUSINESS AREA</td><td colspan='2' class='clsBorder td1' style='border: 1px solid black;'>ACCOUNT CODE</td><td class='clsBorder td1' style='border: 1px solid black;'>AMOUNT</td></tr>");
            sb.Append("<tr class='grey' style='text-align: center'><td class='clsBorder td1' style='border: 1px solid black;'>MAIN LEDGER</td><td class='clsBorder td1' style='border: 1px solid black;'>COST CENTER</td><td class='clsBorder td1' style='border: 1px solid black;'>" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]) + "</td></tr></thead>");
            double tot_Amount = 0;
            sb.Append("<tbody>");
            for (int i = 0; i < dsData.Tables[1].Rows.Count; i++)
            {
                sb.Append("<tr style='text-align:center'>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black;'>" + Convert.ToString(dsData.Tables[1].Rows[i]["EXPENSE_HEAD"]) + "</td>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black;'>" + Convert.ToString(dsData.Tables[0].Rows[0]["GSBER"]) + "</td>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black;'>" + Convert.ToString(dsData.Tables[1].Rows[i]["sap_GLCode"]) + "</td>");
                sb.Append("<td class='clsBorder td2' style='border: 1px solid black;'>" + cost_center + "</td>");
                sb.Append("<td class='clsBorder td2' style='text-align:center; border: 1px solid black;'>" + Convert.ToString(dsData.Tables[1].Rows[i]["Amount"]) + "</td>");
                sb.Append("</tr>");
                tot_Amount = tot_Amount + Convert.ToDouble(dsData.Tables[1].Rows[i]["Amount"]);
            }
            sb.Append("<tr><td colspan='4' class='clsBorder td2' style='text-align:right; font-weight:bold; border: 1px solid black; padding-right:1%'>" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]) + "</td><td class='clsBorder td2' style='text-align:center; border: 1px solid black;'>" + Convert.ToDecimal(tot_Amount).ToString("#.00") + "</td></tr>");

            sb.Append("<tr><td class='clsBorder td2' style='border: 1px solid black;'>Amount in Words (" + Convert.ToString(dsData.Tables[0].Rows[0]["CURRENCY"]) + ")</td><td colspan='4' class='clsBorder td2' style='border: 1px solid black;'><span id='spn_amt_in_word' runat='server'>" + ConvertNumbertoWords(Convert.ToInt32(tot_Amount)) + "Only</span></td></tr>");
            sb.Append("<tr><td class='clsBorder td2' style='border: 1px solid black;'>Paid To</td><td colspan='4' class='clsBorder td2' style='border: 1px solid black;'><span id='spn_Emp_Name1' runat='server'>" + spn_Emp_Name.InnerHtml + "</span></td></tr>");
            sb.Append("<tr><td class='clsBorder td2' style='border: 1px solid black;'>Purpose/Narration</td><td colspan='4' class='clsBorder td2' style='border: 1px solid black;'><span id='spn_Narration' runat='server'>" + narration + "</span></td></tr>");
            sb.Append("</tbody></table>");
            fillAuditTrail(process_id, instance_id);
        }
        tbl_Data.InnerHtml = Convert.ToString(sb);

    }

    public void fillAuditTrail(string process_id, string instance_id)
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "getaudit", ref isValid, process_id, instance_id);
            tblHTML.Append("<table ID='tblAut' style='width: 100%; text-align:center'><thead><tr style='background-color:grey;'><th class='clsBorder1 td1' style='border: 1px solid black;'>Sr.No.</th><th class='clsBorder1 td2' style='border: 1px solid black;'>Step-Name</th><th class='clsBorder1 td1' style='border: 1px solid black;'>Performer</th><th class='clsBorder1 td1' style='border: 1px solid black;'>Date</th><th class='clsBorder1 td2' style='border: 1px solid black;'>Action</th><th class='clsBorder1 td2' style='border: 1px solid black;'>Remark</th></tr></thead>");
            tblHTML.Append("<tbody>");
            for (int Index = 0; Index < dt.Rows.Count; Index++)
            {
                tblHTML.Append("<tr>");
                tblHTML.Append("<td class='clsBorder1 td1' style='border: 1px solid black;'>" + (Index + 1) + "</td>");
                tblHTML.Append("<td class='clsBorder1 td2' style='border: 1px solid black;'>" + dt.Rows[Index]["STEPNAME"].ToString() + "</td>");
                tblHTML.Append("<td class='clsBorder1 td1' style='border: 1px solid black;'>" + dt.Rows[Index]["ACTIONBYUSER"].ToString() + "</td>");
                tblHTML.Append("<td class='clsBorder1 td1' style='border: 1px solid black;'>" + dt.Rows[Index]["ACTIONDATE"].ToString() + "</td>");
                tblHTML.Append("<td class='clsBorder1 td2' style='border: 1px solid black;'>" + dt.Rows[Index]["ACTION"].ToString() + "</td>");
                tblHTML.Append("<td class='clsBorder1 td2' style='border: 1px solid black;'>" + dt.Rows[Index]["REMARK"].ToString() + "</td>");
                tblHTML.Append("</tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            div_audit.InnerHtml = tblHTML.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }

    public static string ConvertNumbertoWords(int number)
    {
        if (number == 0)
            return "Zero";
        if (number < 0)
            return "minus " + ConvertNumbertoWords(Math.Abs(number));
        string words = "";
        if ((number / 1000000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000000) + " Million ";
            number %= 1000000;
        }
if ((number / 100000) > 0)
        {
            words += ConvertNumbertoWords(number / 100000) + " Lakh ";
            number %= 100000;
        }
        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " Thousand ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + " Hundred ";
            number %= 100;
        }
        if (number > 0)
        {
            if (words != "")
                words += " ";
            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }
        }
        return words + " ";
    }
}