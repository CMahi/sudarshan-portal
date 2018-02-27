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

/// <summary>
/// Summary description for GetData
/// </summary>
public class GetData
{
    CryptoGraphy crypt = new CryptoGraphy();
	public GetData()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string get_InvoiceData(string username, string name, int pageno, int rpp,string status)
    {
        StringBuilder html = new StringBuilder();
        try
        {
            //html.Append("<div data-scrollbar='true' data-height='401px'>");
            html.Append("<div class='row headStyle'>");
            html.Append("<div class='col-md-1 headContent' style='text-align:right'>Sr. No.</div><div class='col-md-3 headContent'>PO Number</div><div class='col-md-3 headContent'>PO Date</div><div class='col-md-2 headContent' style='text-align:right'>Cum Inv Amt</div><div class='col-md-3 headContent'>PO GV</div></div>");

            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Invoice_Details_Report.aspx", "getinvoicedetails", ref isdata, username, name,status);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    int from = (pageno - 1) * rpp;
                    int to = ((pageno - 1) * rpp) + rpp;
                    for (int i = from; i < to; i++)
                    {
                        if (i < dt.Rows.Count)
                        {
                            /***********************************************************************************************************************/
                            string authority = HttpContext.Current.Request.Url.Authority;
                            string apath = HttpContext.Current.Request.ApplicationPath;
                            string img_path = "../../Img/plus.png";
                            html.Append("<div class='panel panel-default overflow-hidden m-b-10'>");
                            html.Append("<a class='accordion-toggle accordion-toggle-styled collapsed' data-toggle='collapse' data-parent='#accordion' onclick='change_Image(" + (i + 1) + ")' href='#collapseTwo" + (i + 1) + "'>");
                            html.Append("<div class='fc-events'>");
                            html.Append("<h6>");
                            html.Append("<div class='row' style='color:black'>");
                            html.Append("<div class='col-md-1 rowContent'><input type='text' id='pk_img" + (i + 1) + "' value='0' style='display:none'><input type='image' id='img" + (i + 1) + "' src='" + img_path + "');' style='margin-right:30px'/ >" + (i + 1) + "</div><div class='col-md-3 rowContent'>" + Convert.ToString(dt.Rows[i]["Po_Number"]) + "</div><div class='col-md-3 rowContent'>" + Convert.ToString(dt.Rows[i]["PO_Date"]) + "</div><div class='col-md-2' style='text-align:right'>" + Convert.ToString(dt.Rows[i]["Cum_Amount"]) + "</div><div class='col-md-3 rowContent'>" + Convert.ToString(dt.Rows[i]["PO_GV"]) + "</div></div>");
                            html.Append("</h6></div> </a>");
                            html.Append("<div id='collapseTwo" + (i + 1) + "' class='panel-collapse collapse'>");
                            html.Append("<div class='panel-body'>");
                            /***************************************************************************************************************************************************/

                            html.Append("<table class='table table-bordered' width='100%' id='tbl_InvoiceDtl'>");
                            html.Append("<tr class='btn-default' style='background-color:ButtonFace; text-align:center; color:Black;'><th style='text-align:center'>#</th><th style='text-align:center'>Dispatch Request No</th><th style='text-align:center'>Unique No</th><th style='text-align:center'>Invoice No</th><th style='text-align:center'>Invoice Date</th><th style='text-align:center'>Invoice Amount</th><th style='text-align:center'>Plant</th><th style='text-align:center'>Penalty(%)</th><th style='text-align:center'>Status</th></tr>");
                            html.Append("<tbody>");
                            DataTable dtPO = new DataTable();
                            dtPO = (DataTable)ActionController.ExecuteAction("", "Invoice_Details_Report.aspx", "getinvoicedetailspo", ref isdata, username, Convert.ToString(dt.Rows[i]["Po_Number"]), name,status);
                            if (dtPO != null)
                            {
                                for (int j = 0; j < dtPO.Rows.Count; j++)
                                {
                                    string is_early = Convert.ToString(dtPO.Rows[j]["Is_Early_Payment_Request"]);
                                    string str_status = Convert.ToString(dtPO.Rows[j]["Status"]);
                                    StringBuilder img_Data = new StringBuilder();

                                    string early = "";
                                    if (is_early == "0")
                                    {
                                        early = "<td><a href='#'><img src='../../images/1.png' title='Early Request'/></a></td>";
                                    }
                                    else
                                    {
                                        early = "<td><a href='#'><img src='../../images/2.png' title='Early Request'/></a></td>";
                                    }
                                    img_Data.Append("<table><tr>");

                                    if (str_status == "Quality Control Complete with Deviation")
                                    {
                                        img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QCD</th><th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>ER</th></tr><tbody>");
                                        img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='GRN Complete'/></a></td><td><a href='#'><img src='../../images/3.png' title='QC-Approved under Deviation'/></a></td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td>" + early + "</tr>");
                                    }
                                    else
                                    {
                                        if (str_status == "Rejected")
                                        {
                                            img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QC</th><th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>ER</th></tr><tbody>");
                                            img_Data.Append("<tr><td><a href='#'><img src='../../images/1.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/1.png' title='GRN Complete'/></a></td><td><a href='#'><img src='../../images/1.png' title='QC-Approved'/></a></td><td><a href='#'><img href='#' src='../../images/0.png' title='Rejected'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td>" + early + "</tr>");
                                        }
                                        else if (str_status == "Transition")
                                        {
                                            img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QC</th><th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>ER</th></tr><tbody>");
                                            img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/1.png' title='GRN Complete'/></a></td><td><a href='#'><img src='../../images/1.png' title='QC-Approved'/></a></td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td>" + early + "</tr>");
                                        }
                                        else if (str_status == "GRN Complete")
                                        {
                                            img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QC</th><th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>ER</th></tr><tbody>");
                                            img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='GRN Complete'/></a></td><td><a href='#'><img src='../../images/1.png' title='QC-Approved'/></a></td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td>" + early + "</tr>");
                                        }
                                        else if (str_status == "Quality Control Complete")
                                        {
                                            img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QC</th><th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>ER</th></tr><tbody>");
                                            img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='GRN Complete'/></a></td><td><a href='#'><img src='../../images/2.png' title='QC-Approved'/></a></td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td><td><a href='#'><img src='../../images/1.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td>" + early + "</tr>");
                                        }
                                        else
                                        {
                                            string uno=Convert.ToString(dtPO.Rows[j]["Unique_No"]);
                                            string count = (string)ActionController.ExecuteAction("", "Invoice_Details_Report.aspx", "getstatuscount", ref isdata, uno);
                                            string dtImg = string.Empty;
                                            if (count == "1")
                                            {
                                                img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QCD</th><th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>ER</th></tr><tbody>");
                                                dtImg = "<a href='#'><img src='../../images/3.png' title='QC-Approved under Deviation'/></a>";
                                            }
                                            else
                                            {
                                                img_Data.Append("<th class='imgBtn'>TR</th><th class='imgBtn'>GRN</th><th class='imgBtn'>QC</th><th class='imgBtn'>RJ</th><th class='imgBtn'>BB</th><th class='imgBtn'>PP</th><th class='imgBtn'>ER</th></tr><tbody>");
                                                dtImg = "<a href='#'><img src='../../images/2.png' title='QC-Approved'/></a>";
                                            }
                                            if (str_status == "Payment Processed")
                                            {
                                                img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='GRN Complete'/></a></td><td>" + dtImg + "</td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td><td><a href='#'><img src='../../images/2.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/2.png' title='Payment Processed'/></a></td>" + early + "</tr>");
                                            }
                                            //else if (str_status == "Bill Booked")
                                            else
                                            {
                                                img_Data.Append("<tr><td><a href='#'><img src='../../images/2.png' title='Transition'/></a></td><td><a href='#'><img src='../../images/2.png' title='GRN Complete'/></a></td><td>" + dtImg + "</td><td><a href='#'><img src='../../images/1.png' title='Rejected'/></a></td><td><a href='#'><img src='../../images/2.png' title='Bill Booked'/></a></td><td><a href='#'><img src='../../images/1.png' title='Payment Processed'/></a></td>" + early + "</tr>");
                                            }
                                        }
                                    }
                                    img_Data.Append("</tbody></table>");
                                    html.Append("<tr style='vertical-align:middle'><td style='text-align:center'>" + (j + 1) + "</td><td style='text-align:center'><a href='#paymentterm' role='button' data-toggle='modal' onclick='setSelectedNote(" + Convert.ToString(dtPO.Rows[j]["PK_Dispatch_Note_ID"]) + ")'>" + Convert.ToString(dtPO.Rows[j]["request_id"]) + "</a></td><td style='text-align:left'>" + Convert.ToString(dtPO.Rows[j]["Unique_No"]) + "</td><td style='text-align:left'>" + Convert.ToString(dtPO.Rows[j]["Invoice_No"]) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["Invoice_Date"]) + "</td><td style='text-align:right'>" + Convert.ToString(dtPO.Rows[j]["Invoice_Amount"]) + "</td><td style='text-align:center'>" + Convert.ToString(dtPO.Rows[j]["PLANT"]) + "</td><td style='text-align:right'>" + Convert.ToString(dtPO.Rows[j]["PRCEN"]) + "</td><td style='text-align:center;'>" + Convert.ToString(img_Data) + "<input type='text' id='pk_Dispatch" + (j + 1) + "' value=" + Convert.ToString(dtPO.Rows[j]["PK_Dispatch_Note_ID"]) + " style='display:none'></td></tr>");
                                }
                            }
                            html.Append("</tbody></table>");

                            /***************************************************************************************************************************************************/
                            html.Append("</div></div></div>");

                            /***********************************************************************************************************************/
                        }
                    }
                    //html.Append("</div>");
                    double cnt = Convert.ToDouble(dt.Rows.Count) / rpp;
                    if (cnt > Convert.ToInt16(Convert.ToInt32(dt.Rows.Count) / rpp))
                    {
                        cnt = (int)cnt + 1;
                    }

                    if (cnt > 1)
                    {
                        html.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
                        html.Append("<ul class='pagination'>");
                        for (int j = 1; j <= cnt; j++)
                        {
                            html.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + rpp + ")'></li>");
                        }
                        html.Append("</ul></div>");
                    }

                }


            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            html = str;
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return Convert.ToString(html);
    }

    public StringBuilder get_EarlyRequestData(string username, string name, int pageno, int rpp)
    {
        
        StringBuilder HTML = new StringBuilder();
        try
        {
            string isdata = string.Empty;
            HTML.Append("<table class='table table-bordered' width='100%' id='tbl_InvoiceDtl'>");
            HTML.Append("<tr style='background-color:grey; text-align:center; color:white;'><th style='text-align:center'>#</th><th style='text-align:center'>Dispatch Request No</th><th style='text-align:center'>Unique No</th><th style='text-align:center'>PO No</th><th style='text-align:center'>PO Date</th><th style='text-align:center'>PO GV</th><th style='text-align:center'>Invoice No</th><th style='text-align:center'>Invoice Date</th><th style='text-align:center'>Invoice Value</th><th style='text-align:center'>Discount Rate</th><th style='text-align:center; display:none'>Dispatch_Id</th></tr>");
            HTML.Append("<tbody>");
            DataTable dt = new DataTable();
            if (name == "")
            {
                dt = (DataTable)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getearlypaymentrequests", ref isdata, username);
            }
            else
            {
                dt = (DataTable)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getearlysearch", ref isdata, username, name);
            }

            int ddl = rpp;
            int from = (pageno - 1) * ddl;
            int to = ((pageno - 1) * ddl) + ddl;
            for (int i = from; i < to; i++)
            {
                if (i < dt.Rows.Count)
                {
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(dt.Rows[i]["PO_Number"]));
                    if (i == from)
                    {
                        HTML.Append("<tr><td style='text-align:center'><input type='radio' name='early' id='rad_id" + (i + 1) + "' checked></td><td style='text-align:center'><a href='#paymentterm' role='button' data-toggle='modal' onclick='setSelectedNote(" + Convert.ToString(dt.Rows[i]["PK_Dispatch_Note_ID"]) + ")'>" + Convert.ToString(dt.Rows[i]["request_id"]) + "</a></td><td style='text-align:center'>" + Convert.ToString(dt.Rows[i]["Unique_No"]) + "</td><td style='text-align:center'><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dt.Rows[i]["PO_Number"]) + "</a></td><td style='text-align:center'>" + Convert.ToString(dt.Rows[i]["PO_Date"]) + "</td><td style='text-align:right'>" + Convert.ToString(dt.Rows[i]["PO_GV"]) + "</td><td style='text-align:center'>" + Convert.ToString(dt.Rows[i]["Invoice_No"]) + "</td><td style='text-align:center'>" + Convert.ToString(dt.Rows[i]["Invoice_Date"]) + "</td><td style='text-align:right'>" + Convert.ToString(dt.Rows[i]["Invoice_Value"]) + "</td><td style='text-align:right'>" + Convert.ToString(dt.Rows[i]["Discount_Rate"]) + "</td><td style='text-align:center; display:none'><input type='text' id='Fk_dispatch_no" + (i + 1) + "' value=" + Convert.ToString(dt.Rows[i]["PK_Dispatch_Note_ID"]) + "><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td></tr>");
                    }
                    else
                    {
                        HTML.Append("<tr><td style='text-align:center'><input type='radio' name='early' id='rad_id" + (i + 1) + "'></td><td style='text-align:center'><a href='#paymentterm' role='button' data-toggle='modal' onclick='setSelectedNote(" + Convert.ToString(dt.Rows[i]["PK_Dispatch_Note_ID"]) + ")'>" + Convert.ToString(dt.Rows[i]["request_id"]) + "</a></td><td style='text-align:center'>" + Convert.ToString(dt.Rows[i]["Unique_No"]) + "</td><td style='text-align:center'><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dt.Rows[i]["PO_Number"]) + "</a></td><td style='text-align:center'>" + Convert.ToString(dt.Rows[i]["PO_Date"]) + "</td><td style='text-align:right'>" + Convert.ToString(dt.Rows[i]["PO_GV"]) + "</td><td style='text-align:center'>" + Convert.ToString(dt.Rows[i]["Invoice_No"]) + "</td><td style='text-align:center'>" + Convert.ToString(dt.Rows[i]["Invoice_Date"]) + "</td><td style='text-align:right'>" + Convert.ToString(dt.Rows[i]["Invoice_Value"]) + "</td><td style='text-align:right'>" + Convert.ToString(dt.Rows[i]["Discount_Rate"]) + "</td><td style='text-align:center; display:none'><input type='text' id='Fk_dispatch_no" + (i + 1) + "' value=" + Convert.ToString(dt.Rows[i]["PK_Dispatch_Note_ID"]) + "><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'></td></tr>");
                    }
                }
            }
            HTML.Append("</tbody></table>");

            double cnt = Convert.ToDouble(dt.Rows.Count) / ddl;
            if (cnt > Convert.ToInt16(Convert.ToInt32(dt.Rows.Count) / ddl))
            {
                cnt = (int)cnt + 1;
            }

            if (cnt > 1)
            {
                HTML.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
                HTML.Append("<ul class='pagination'>");
                for (int j = 1; j <= cnt; j++)
                {
                    
                    HTML.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + rpp + ")'></li>");
                }
                HTML.Append("</ul></div>");
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            HTML = str;
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return HTML;
    }

    public string get_TasksData(string username, string name, int pageno, int rpp)
    {
        StringBuilder tblHeader = new StringBuilder();
        StringBuilder tblBody = new StringBuilder();
        string tblHTML = string.Empty;
        DataTable DT = new DataTable();
        string isData = string.Empty;
        int processid, instanceid;
        try
        {
            if (name.Trim() == "")
            {
                DT = (DataTable)ActionController.ExecuteAction("", "TaskDetails.aspx", "select user wi", ref isData, username);
            }
            else
            {
                DT = (DataTable)ActionController.ExecuteAction("", "TaskDetails.aspx", "select user wi search", ref isData, username, name);
            }
            tblHeader.Append("<th>Sr.No.</th><th>INITIATOR</th><th >HEADER INFO</th> <th > PROCESS NAME</th><th >STEP NAME</th><th  >ASSIGN DATE</th>");
            int ddl = rpp;
            int from = (pageno - 1) * ddl;
            int to = ((pageno - 1) * ddl) + ddl;
            for (int i = from; i < to; i++)
            {
                if (i < DT.Rows.Count)
                {

                    processid = Int32.Parse(DT.Rows[i]["PK_PROCESSID"].ToString());
                    instanceid = Int32.Parse(DT.Rows[i]["INSTANCE_ID"].ToString());
                    string header = DT.Rows[i]["HEADER_INFO"].ToString();

                    tblBody.Append("<tr><td align='center' >" + Convert.ToString(i + 1) + "</td><td align='left' >" + DT.Rows[i]["INSTANCE_ID_DESCRIPTION"].ToString() + "</td><td><a href='../MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='frmset_WorkArea' title='Click here to open the workitem.' runat='server'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td></tr>");
                }
            }
            tblHTML = "<table id='tbl_WorkItems' class='table table-bordered' align='center' width='100%'>" +
                              "<thead><tr  class='grey' >" + tblHeader.ToString() + "</tr></thead>" +
                              "<tbody>" + tblBody.ToString() + "</tbody>" +
                              "</table>";

            StringBuilder HTML = new StringBuilder();
            double cnt = Convert.ToDouble(DT.Rows.Count) / ddl;
            if (cnt > Convert.ToInt16(Convert.ToInt32(DT.Rows.Count) / ddl))
            {
                cnt = (int)cnt + 1;
            }

            if (cnt > 1)
            {
                HTML.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
                HTML.Append("<ul class='pagination'>");
                for (int j = 1; j <= cnt; j++)
                {
                    HTML.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + ddl + ")'></li>");
                }
                HTML.Append("</ul></div>");
            }
            tblHTML = tblHTML + Convert.ToString(HTML);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(tblHTML);
    }

    public string get_Dispatch_Note_Report_Details(string username, string name, int pageno, int rpp,string plant,string Dept)
    {
        string strData = string.Empty;
        string isdata = string.Empty;
        StringBuilder html = new StringBuilder();
        try
        {
            html.Append("<table class='table table-bordered table-hover' >");
            html.Append("<thead><tr class='grey center'><th>Vendor Name</th><th>PO No</th><th>PO Gross Amt</th><th>Plant</th><th>Unique No</th><th>Dispatch Note</th><th>Invoice No</th><th>Invoice Amt</th><th>Dispatch Quantity</th><th style='display:none'>Status</th></tr></thead>");
			html.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "User_Plant_Mapping_Report.aspx", "getdispatchnoterpt", ref isdata, username, plant, Dept, name);
            if (dtData != null)
            {
                int ddl = rpp;
                int from = (pageno - 1) * ddl;
                int to = ((pageno - 1) * ddl) + ddl;
                for (int i = from; i < to; i++)
                {
                    if (i < dtData.Rows.Count)
                    {
                        string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtData.Rows[i]["PO_Number"]));
                        html.Append("<tr><td><a href='#vendorinfo' role='button' data-toggle='modal' id='veninfo" + (i + 1) + "' onclick='viewVendor(" + (i + 1) + ")' >" + Convert.ToString(dtData.Rows[i]["vendor_name"]) + "</a></td><td><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dtData.Rows[i]["PO_Number"]) + "</a></td><td style='text-align: right;'>" + Convert.ToString(dtData.Rows[i]["PO_GV"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["Plant"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["Unique_No"]) + "</td><td><a href='#paymentterm' role='button' data-toggle='modal' onclick='setSelectedNote(" + Convert.ToString(dtData.Rows[i]["PK_Dispatch_Note_ID"]) + ")'>" + Convert.ToString(dtData.Rows[i]["request_id"]) + "</a></td><td>" + Convert.ToString(dtData.Rows[i]["Invoice_No"]) + "</td><td style='text-align: right;'>" + Convert.ToString(dtData.Rows[i]["Invoice_Amount"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["Dispatch_Quantity"]) + "</td><td style='text-align: right; display:none'><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'><input type='text' id='venCode" + (i + 1) + "' value=" + Convert.ToString(dtData.Rows[i]["vendor_code"]) + " style='display:none'></td></tr>");

                    }
                }
            }
           
            html.Append("</tbody>");
            html.Append("</table>");

            double cnt = Convert.ToDouble(dtData.Rows.Count) / rpp;
            if (cnt > Convert.ToInt16(Convert.ToInt32(dtData.Rows.Count) / rpp))
            {
                cnt = (int)cnt + 1;
            }

            if (cnt > 1)
            {
                html.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
                html.Append("<ul class='pagination'>");
                for (int j = 1; j <= cnt; j++)
                {
                    html.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + rpp + ")'></li>");
                }
                html.Append("</ul></div>");
            }
            strData = Convert.ToString(html);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return strData;
    }



    public string get_Dispatch_Details(int name)
    {
        StringBuilder html_Header = new StringBuilder();
        DataTable dts = new DataTable();
        string userdata = string.Empty;
        try
        {
            string isdata = string.Empty;
            DataSet ds = (DataSet)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getdispatchdetails", ref isdata, name);
            if (ds != null)
            {
                
                /*******************************************************************************************************PO Header***********************************************************************************************/

                html_Header.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>PO Header</h4></div><div class='panel-body'><div class='table-responsive'>");
                html_Header.Append("<table class='table table-bordered' width='100%'>");
                html_Header.Append("<tr style='background-color:grey; text-align:center; color:white;'><th style='text-align:center'>Dispatch Req No</th><th style='text-align:center'>Vendor Name</th><th style='text-align:center'>Vendor Code</th><th style='text-align:center'>PO No</th><th style='text-align:center'>PO Date</th><th style='text-align:center'>Currency</th><th style='text-align:center'>Dispatch Date</th><th style='text-align:center'>PO Type</th><th style='text-align:center'>INCO Terms</th><th style='text-align:center'>PO Value</th><th style='text-align:center;'>PO GV</th><th style='text-align:center;'>Payment Terms</th><th style='text-align:center;'>Cumulative Inv Amt</th></tr>");
                html_Header.Append("<tbody>");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dts = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getinco", ref isdata, Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]));
                    string inco_terms = string.Empty;
                    if (dts != null && dts.Rows.Count > 0)
                    {
                        for (int i = 0; i < dts.Rows.Count; i++)
                        {
                            inco_terms = Convert.ToString(dts.Rows[i]["INCO_TERMS1"]) + " - ";
                            inco_terms += Convert.ToString(dts.Rows[i]["INCO_TERMS2"]);
                        }
                    }


                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]));

                    html_Header.Append("<tr><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["request_id"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["vendor_name"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["vendor_code"]) + "</td><td style='text-align:center'><a href='#' role='button' id='anc1' onclick='viewData1()'>" + Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]) + "</a></td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["date"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["currency"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["created_date"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["po_type"]) + "</td><td style='text-align:center'>" + inco_terms + "</td><td style='text-align:right'>" + Convert.ToString(ds.Tables[0].Rows[0]["po_value"]) + "</td><td style='text-align:right;'>" + Convert.ToString(ds.Tables[0].Rows[0]["po_gv"]) + "</td><td style='text-align:center;'><a href='#payterm' data-toggle='modal'>" + Convert.ToString(ds.Tables[0].Rows[0]["PAYMENT_TERMS"]) + "</a><input type='text' id='encrypt1' value=" + encrypt_Str + " style='display:none'></td><td style='text-align:center;'>" + Convert.ToString(ds.Tables[0].Rows[0]["cum_amt"]) + "</td></tr>");
                }

                html_Header.Append("</tbody></table>");

                /******************************************************************************Invoice Details**************************************************************************************************************************/

                html_Header.Append("<div class='row'>");
                html_Header.Append("<div class='col-md-1' style='text-align:right'>");
                html_Header.Append("<b>Invoice No :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-1'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Invoice_No"]));
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                html_Header.Append("<b>Invoice Date :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-1'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Invoice_Date"]));
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                html_Header.Append("<b>Invoice Amount :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-1'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Invoice_Amount"]));
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                html_Header.Append("<b>Delivery Note :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-1'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Delivery_Note"]));
                html_Header.Append("</div>");
                html_Header.Append("</div>");
                /*************************************************************************************Hide/Show Detail Link*******************************************************************************************************************/
                html_Header.Append("<div class='col-md-10'>&nbsp;</div>");
                html_Header.Append("<div class='col-md-2' style='text-align:right'><a id='showdtl' runat='server' onclick='Show_Dtl()' href='#'>Show Details</a>");
                html_Header.Append("<a id='hidedtl' runat='server' onclick='Hide_Dtl()' href='#' style='display:none'>Hide Details</a></div>");
                html_Header.Append("</div></div></div></div>");


                /****************************************************************************************PO Detail***************************************************************************************************************/

                html_Header.Append("<div class='col-md-12' id='div_dtl' runat='server' style='display:none'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>PO Details</h4></div><div class='panel-body'><div class='table-responsive'>");
                html_Header.Append("<table class='table table-bordered' width='100%'>");
                html_Header.Append("<tr style='background-color:grey; text-align:center; color:white;'><th style='text-align:center'>Material No</th><th style='text-align:center'>Plant</th><th style='text-align:center'>Storage Location</th><th style='text-align:center'>Quantity</th><th style='text-align:center'>UOM</th><th style='text-align:center'>Net Price</th><th style='text-align:center'>Amount</th><th style='text-align:center'>Material Group</th><th style='text-align:center'>Dispatch Qty</th><th style='text-align:center;'>Goods Received Qty</th><th style='text-align:center; display:none'>PK_VENDOR_PO_DTL</th></tr>");
                html_Header.Append("<tbody>");

                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        html_Header.Append("<tr><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["material_no"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["plant"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["storage_location"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["quantity"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["uom"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["net_price"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["amount"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["material_group"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["dispatch_quantity"]) + "</td><td style='text-align:center;'>" + Convert.ToString(ds.Tables[1].Rows[i]["gr_quantity"]) + "</td><td style='text-align:center; display:none'>" + Convert.ToString(ds.Tables[1].Rows[i]["PK_Dispatch_Note_HDR_ID"]) + "</td></tr>");
                    }
                }

                html_Header.Append("</tbody></table>");
                html_Header.Append("</div></div></div></div>");

                /*********************************************************************Dispatch Details*********************************************************************************************************************************/

                html_Header.Append("<div class='col-md-6'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>Dispatch Details</h4></div><div class='panel-body'>");
                html_Header.Append("<table class='table table-bordered' width='100%'>");
                html_Header.Append("<tr><td class='col-md-3'><b>Transporter Name</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["Transporter_Name"]) + " </td></tr>");
                html_Header.Append("<tr><td class='col-md-3'><b>Vehicle No</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["vehicle_no"]) + " </td></tr>");
                html_Header.Append("<tr><td class='col-md-3'><b>Contact Person Name</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["contact_person_name"]) + " </td></tr>");
                html_Header.Append("<tr><td class='col-md-3'><b>Contact No</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["contact_no"]) + " </td></tr>");
                html_Header.Append("<tr><td class='col-md-3'><b>LR No.</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["lr_no"]) + " </td></tr>");
                html_Header.Append("<tr><td class='col-md-3'><b>LR Date</b></td><td class='col-md-9'>" + Convert.ToString(ds.Tables[0].Rows[0]["lr_date"]) + " </td></tr>");
                html_Header.Append("</table></div></div></div>");

                /********************************************************************************Documents***********************************************************************************************************************/

                html_Header.Append("<div class='col-md-6'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>Documents</h4></div><div class='panel-body'>");
                html_Header.Append("<table class='table table-bordered' width='100%'>");
                html_Header.Append("<tbody>");
                html_Header.Append("<tr style='background-color:grey; text-align:center; color:white;'><td style='text-align:center'>Document Name</td><td style='text-align:center'>File Name</td></tr>");
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getfilenames", ref isdata, "DISPATCH NOTE", Convert.ToString(ds.Tables[0].Rows[0]["request_id"]));

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        html_Header.Append("<tr><td style='text-align:center'>" + Convert.ToString(dt.Rows[i]["DOCUMENT_TYPE"]) + "</td><td style='text-align:center'><a href='#' OnClick='downloadfiles(" + Convert.ToString(dt.Rows[i]["PK_ID"]) + ")'>" + Convert.ToString(dt.Rows[i]["FILENAME"]) + "</a></td></tr>");
                    }
                }
                html_Header.Append("</tbody>");
                html_Header.Append("</table></div></div></div>");

                /****************************************************************************Inco Term Details***************************************************************************************************************************/
                /*
                html_Header.Append("<div class='modal fade' id='incoterm'>");
                html_Header.Append("<div class='modal-dialog'>");
                html_Header.Append("<div class='modal-content' style='background-color:white'><div class='modal-header'>");
                html_Header.Append("<button type='button' class='close' aria-hidden='true' onclick='closeInco()'>×</button>");
                html_Header.Append("<h4 class='modal-title'>INCO Terms <font color='white'>  >> PO Number : <span  id=inco_span>" + Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]) + "</span></font></h4></div>");
                html_Header.Append("<div class='modal-body'>");

                DataTable dts = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getinco", ref isdata, Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]));
                html_Header.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Inco Terms (Part 1)</th><th>Inco Terms (Part 2)</th></tr></thead>");
                html_Header.Append("<tbody>");
                if (dts != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        html_Header.Append("<tr>");
                        html_Header.Append("<td>" + dts.Rows[i]["INCO_TERMS1"].ToString() + "</td>");
                        html_Header.Append("<td>" + dts.Rows[i]["INCO_TERMS2"].ToString() + "</td>");
                        html_Header.Append("</tr>");
                    }
                }
                html_Header.Append("</tbody>");
                html_Header.Append("</table>");


                html_Header.Append("</div>");
                html_Header.Append("<div class='modal-footer'><a href='javascript:;' class='btn btn-sm btn-danger' onclick='closeInco()'>Close</a></div>");
                html_Header.Append("</div></div></div>");

                */
                /********************************************************************Payment Terms Details***********************************************************************************************************************************/

                html_Header.Append("<div class='modal' id='payterm'>");
                html_Header.Append("<div class='modal-dialog'>");
                html_Header.Append("<div class='modal-content' style='background-color:white'><div class='modal-header'>");
                html_Header.Append("<button type='button' class='close' aria-hidden='true' onclick='closePayterm()'>×</button>");
                html_Header.Append("<h4 class='modal-title'>Payment Terms <font color='white'>  >> PO Number : <span  id='pay_span'>" + Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]) + "</span></font></h4></div>");
                html_Header.Append("<div class='modal-body'>");

                dts = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails.aspx", "getpayment_term", ref isdata, Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]));
                html_Header.Append("<table class='table table-bordered'><thead><tr class='grey'><th>Day Limit</th><th>Calendar Day for Payment</th><th>Days from Baseline Date for Payment</th></tr></thead>");
                html_Header.Append("<tbody>");
                if (dts != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        html_Header.Append("<tr>");
                        html_Header.Append("<td>" + dts.Rows[i]["ZTAGG"].ToString() + "</td>");
                        html_Header.Append("<td>" + dts.Rows[i]["ZFAEL"].ToString() + "</td>");
                        html_Header.Append("<td>" + dts.Rows[i]["ZTAG1"].ToString() + "</td>");
                        html_Header.Append("</tr>");
                    }
                }
                html_Header.Append("</tbody>");
                html_Header.Append("</table>");


                html_Header.Append("</div>");
                html_Header.Append("<div class='modal-footer'><a href='javascript:;' class='btn btn-sm btn-danger' onclick='closePayterm()'>Close</a></div>");
                html_Header.Append("</div></div></div>");

                /*******************************************************************************************************************************************************************************************************/

            }
            userdata = html_Header.ToString();

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return userdata;
    }

    public string viewVendor(string vcode)
    {
        string strData = string.Empty;
        StringBuilder html = new StringBuilder();
        string isdata = string.Empty;
        try
        {
            DataTable dtVendor = (DataTable)ActionController.ExecuteAction("", "Edit_Profile.aspx", "getvendordetails", ref isdata, vcode);

            if (dtVendor.Rows.Count > 0)
            {
                html.Append("<div class='row'>");
                html.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><h3 class='panel-title'><b>Personal Details</b></h3></div></div></div>");
                html.Append("<div class='col-md-12'>");
                html.Append("<div class='col-md-2'><b>Name :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["Vendor_Name1"]).Trim() + "</div>");
                html.Append("<div class='col-md-2'><b>Email :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["Email"]).Trim() + "</div>");
                html.Append("</div>");
                html.Append("<div class='col-md-12'>&nbsp;</div>");
                html.Append("<div class='col-md-12'>");
                html.Append("<div class='col-md-2'><b>Mobile No :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["Telephone1"]).Trim() + "</div>");
                html.Append("<div class='col-md-2'><b>Contact No :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["Telephone2"]).Trim() + "</div>");
                html.Append("</div><div class='col-md-12'>&nbsp;</div>");
                html.Append("<div class='col-md-12'><div class='col-md-2'><b>ECC NO :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["ECC_NO"]).Trim() + "</div>");
                html.Append("<div class='col-md-2'><b>Central Tax No :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["Central_Sales_Tax_No"]).Trim() + "</div>");
                html.Append("</div>");
                html.Append("<div class='col-md-12'>&nbsp;</div>");
                html.Append("<div class='col-md-12'><div class='col-md-2'><b>Local Tax No :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["Local_Sales_Tax_No"]).Trim() + "</div>");
                html.Append("<div class='col-md-2'><b>Excise Reg No :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["Excise_Reg_No"]).Trim() + "</div>");
                html.Append("</div>");
                html.Append("<div class='col-md-12'>&nbsp;</div>");
                html.Append("<div class='col-md-12'>");
                html.Append("<div class='col-md-2'><b>PAN No :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["PAN_NO"]).Trim() + "</div>");
                html.Append("<div class='col-md-2'><b>Fax No :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["FAX_NO"]).Trim() + "</div>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("<div class='row'>&nbsp;</div>");
                html.Append("<div class='row'>");
                html.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><h3 class='panel-title'><b>Bank Details</b></h3></div></div></div>");
                html.Append("<div class='col-md-12'>");
                html.Append("<div class='col-md-2'><b>Bank Name :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["FK_BANK_ID"]).Trim() + "</div>");
                html.Append("<div class='col-md-2'><b>Account No :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["ACC_NO"]).Trim() + "</div>");
                html.Append("</div>");
                html.Append("<div class='col-md-12'>&nbsp;</div>");
                html.Append("<div class='col-md-12'>");
                html.Append("<div class='col-md-2'><b>IFSC Code :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["IFSC_CODE"]).Trim() + "</div>");
                html.Append("<div class='col-md-2'><b>Branch :</b></div><div class='col-md-4'>" + Convert.ToString(dtVendor.Rows[0]["BRANCH"]).Trim() + "</div>");
                html.Append("</div>");
                html.Append("</div>");
                strData = Convert.ToString(html);
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        return strData;
    }
    
    public string fillAuditTrail(string processid, string instanceid)
    {
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            StringBuilder tblHTML = new StringBuilder();
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "PODispatchDeatails_Approval.aspx", "getaudit", ref isValid, processid, instanceid);
            tblHTML.Append("<table ID='tblAut' class='table table-bordered'><thead><tr class='grey'><th>Sr.No.</th><th>Step-Name</th><th>Performer</th><th>Date</th><th>Action-Name</th><th>Remark</th></tr></thead>");
            tblHTML.Append("<tbody>");
            for (int Index = 0; Index < dt.Rows.Count; Index++)
            {
                tblHTML.Append("<tr>");
                tblHTML.Append("<td>" + (Index + 1) + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["STEPNAME"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["ACTIONBYUSER"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["ACTIONDATE"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["ACTION"].ToString() + "</td>");
                tblHTML.Append("<td>" + dt.Rows[Index]["REMARK"].ToString() + "</td>");
                tblHTML.Append("</tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            data = tblHTML.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
        return data;
    }

    public string get_MultipleEarlyRequests(string username, string name, int pageno, int rpp)
    {
        StringBuilder tblHeader = new StringBuilder();
        StringBuilder tblBody = new StringBuilder();
        string tblHTML = string.Empty;
        DataTable DT = new DataTable();
        string isData = string.Empty;
        int processid, instanceid;
        try
        {
            DT = (DataTable)ActionController.ExecuteAction("", "Early_Payment_Request_Approval_Multiple.aspx", "getmultiple", ref isData, username, name);

            tblHeader.Append("<th>#</th><th>INITIATOR</th><th>HEADER INFO</th><th>UNIQUE NO</th><th>PROCESS NAME</th><th >STEP NAME</th><th>ASSIGN DATE</th>");
            int ddl = rpp;
            int from = (pageno - 1) * ddl;
            int to = ((pageno - 1) * ddl) + ddl;
            for (int i = from; i < to; i++)
            {
                if (i < DT.Rows.Count)
                {

                    processid = Int32.Parse(DT.Rows[i]["PK_PROCESSID"].ToString());
                    instanceid = Int32.Parse(DT.Rows[i]["INSTANCE_ID"].ToString());
                    string header = DT.Rows[i]["HEADER_INFO"].ToString();

                    //tblBody.Append("<tr><td align='center'><input type='checkbox' name='early' id='rad_id" + (i + 1) + "'><input type='PK_Dispatch_Note_ID" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_Dispatch_Note_ID"]) + " style='display:none'></input></td><td align='left'>" + DT.Rows[i]["INSTANCE_ID_DESCRIPTION"].ToString() + "</td><td><a href='../MenuRequestHandler.aspx?page=../Processes/" + DT.Rows[i]["PROCESS_NAME"].ToString() + "/" + DT.Rows[i]["PAGE"].ToString() + "&processid=" + DT.Rows[i]["PK_PROCESSID"].ToString() + "&paramname=wi&instanceid=" + DT.Rows[i]["INSTANCE_ID"].ToString() + "&stepname=" + DT.Rows[i]["STEP_NAME"].ToString() + "&stepid=" + DT.Rows[i]["FK_STEPID"].ToString() + "&wiid=" + DT.Rows[i]["PK_TRANSID"].ToString() + "' target='frmset_WorkArea' title='Click here to open the workitem.' runat='server'>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</a></td><td align='left' >" + DT.Rows[i]["UNIQUE_NO"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td></tr>");
                    tblBody.Append("<tr><td align='center'><input type='checkbox' name='early' id='rad_id" + (i + 1) + "'><input type='text' id='fk_process" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_PROCESSID"]) + " style='display:none'></input><input type='text' id='PK_Dispatch_Note_ID" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_Dispatch_Note_ID"]) + " style='display:none'></input><input type='text' id='iid" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["INSTANCE_ID"]) + " style='display:none'></input><input type='text' id='wiid" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["PK_TRANSID"]) + " style='display:none'></input></td><td align='left'>" + DT.Rows[i]["INSTANCE_ID_DESCRIPTION"].ToString() + "</td><td><input type='text' id='h_info" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + " style='display:none'></input>" + Convert.ToString(DT.Rows[i]["HEADER_INFO"]) + "</td><td align='left' >" + DT.Rows[i]["UNIQUE_NO"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["PROCESS_NAME"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["STEP_NAME"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["ASSIGN_DATE"].ToString() + "</td></tr>");
                }
            }
            tblHTML = "<table id='tbl_WorkItems' class='table table-bordered' align='center' width='100%'>" +
                              "<thead><tr  class='grey' >" + tblHeader.ToString() + "</tr></thead>" +
                              "<tbody>" + tblBody.ToString() + "</tbody>" +
                              "</table>";

            StringBuilder HTML = new StringBuilder();
            double cnt = Convert.ToDouble(DT.Rows.Count) / ddl;
            if (cnt > Convert.ToInt16(Convert.ToInt32(DT.Rows.Count) / ddl))
            {
                cnt = (int)cnt + 1;
            }

            if (cnt > 1)
            {
                HTML.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
                HTML.Append("<ul class='pagination'>");
                for (int j = 1; j <= cnt; j++)
                {
                    HTML.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + ddl + ")'></li>");
                }
                HTML.Append("</ul></div>");
            }
            tblHTML = tblHTML + Convert.ToString(HTML);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(tblHTML);
    }

    public string get_block_Vendor(string username, string name, int pageno, int rpp)
    {
        StringBuilder tblHeader = new StringBuilder();
        StringBuilder tblBody = new StringBuilder();
        string tblHTML = string.Empty;
        DataTable DT = new DataTable();
        string isData = string.Empty;
        //int processid, instanceid;
        try
        {
            DT = (DataTable)ActionController.ExecuteAction("", "Vendor_Unblock.aspx", "getmultiple", ref isData);

            tblHeader.Append("<th>#</th><th>Vendor Code</th><th>Vendor Name</th><th>Email ID</th><th>Telephone 1</th><th >Telephone 2</th>");
            int ddl = rpp;
            int from = (pageno - 1) * ddl;
            int to = ((pageno - 1) * ddl) + ddl;
            for (int i = from; i < to; i++)
            {
                if (i < DT.Rows.Count)
                {

                    tblBody.Append("<tr><td align='center'><input type='checkbox' name='early' id='rad_id" + (i + 1) + "'></td><td><input type='text' id='txt_Vendor_Code" + (i + 1) + "' value='" + Convert.ToString(DT.Rows[i]["Vendor_Code"]) + "' style='display:none'></input>" + Convert.ToString(DT.Rows[i]["Vendor_Code"]) + "</td><td align='left' >" + DT.Rows[i]["Vendor_Name"].ToString() + "</td><td align='left'><input type='text' id='txt_Vendor_Email" + (i + 1) + "' Value='" + Convert.ToString(DT.Rows[i]["Email"]) + "' style='display:none'></input>" + DT.Rows[i]["Email"].ToString() + "</td> <td align='left'  >" + DT.Rows[i]["Telephone1"].ToString() + "</td><td align='left'  >" + DT.Rows[i]["Telephone2"].ToString() + "</td><td class='hidden'><input type='text' id='txt_Vendor_Password" + (i + 1) + "' Value=" + Convert.ToString(DT.Rows[i]["password"]) + "></input></td></tr>");
                }
            }
            tblHTML = "<table id='tbl_WorkItems' class='table table-bordered' align='center' width='100%'>" +
                              "<thead><tr  class='grey' >" + tblHeader.ToString() + "</tr></thead>" +
                              "<tbody>" + tblBody.ToString() + "</tbody>" +
                              "</table>";

            StringBuilder HTML = new StringBuilder();
            double cnt = Convert.ToDouble(DT.Rows.Count) / ddl;
            if (cnt > Convert.ToInt16(Convert.ToInt32(DT.Rows.Count) / ddl))
            {
                cnt = (int)cnt + 1;
            }

            if (cnt > 1)
            {
                HTML.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
                HTML.Append("<ul class='pagination'>");
                for (int j = 1; j <= cnt; j++)
                {
                    HTML.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + ddl + ")'></li>");
                }
                HTML.Append("</ul></div>");
            }
            tblHTML = tblHTML + Convert.ToString(HTML);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(tblHTML);
    }

    public string get_Travel_Policy_Data(string search_data, int pageno, int rpp, string desg, string division)
    {
        StringBuilder str = new StringBuilder();
        try
        {
            DataTable DTS;
            string IsValid = string.Empty;
            string procedure = "X_Domestic_Policy";
            DTS = (DataTable)ActionController.ExecuteAction("", "Domestic_Policy.aspx", "select", ref IsValid,division, desg, procedure, search_data);
            DTS.Columns[0].ColumnName = "Sr.No";
            //Session["ResultData"] = DTS;

            str.Append("<table id='data-table1'  runat='server' class='table table-bordered table-hover'> <thead><tr class='grey'><th style='width:5%'>#</th><th>Designation</th><th>Journey Type</th><th>Travel Mode</th><th>Travel Class</th><th>Plant From</th><th>Plant To</th><th>Expense Head</th><th>City Class</th><th>Amount</th></tr></thead>");
            str.Append("<tbody>");

            if (DTS.Rows.Count > 0)
            {



                int ddl = rpp;
                int from = (pageno - 1) * ddl;
                int to = ((pageno - 1) * ddl) + ddl;
                for (int i = from; i < to; i++)
                {
                    if (i < DTS.Rows.Count)
                    {
			string city_class="Other";
			if(Convert.ToString(DTS.Rows[i]["CITY_CLASS"])=="A")
			{
				city_class="Metro";
			}
			else if(Convert.ToString(DTS.Rows[i]["CITY_CLASS"])=="B")
			{
				city_class="Mini-Metro";
			}
                        str.Append("<tr>");
                        str.Append("<td align='left'><input type='text' Id='pk_dom_id" + (i + 1) + "' value='" + DTS.Rows[i]["PK_DOMESTIC_ID"].ToString() + "' style='display:none'/> " + (i + 1) + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["DESG_NAME"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["JOURNEY_TYPE"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["TRAVEL_NAME"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["TRAVEL_MAPPING_CLASS"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["PLANT_NAME"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["PLANT_NAME1"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
			str.Append("<td align='left'>" + city_class + "</td>");
                        str.Append("<td align='right'>" + DTS.Rows[i]["AMOUNT"].ToString() + "</td>");
                        str.Append("</tr>");
                    }
                }

            }
            str.Append("</tbody> </table> ");

            double cnt = Convert.ToDouble(DTS.Rows.Count) / rpp;
            if (cnt > Convert.ToInt16(Convert.ToInt32(DTS.Rows.Count) / rpp))
            {
                cnt = (int)cnt + 1;
            }

            if (cnt > 1)
            {
                str.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
                str.Append("<ul class='pagination'>");
                for (int j = 1; j <= cnt; j++)
                {

                    str.Append("<li class='paginate_button'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal; margin: 3px' onclick='gotopage(this," + rpp + ")' style='margin:5px;'></li>");

                }
                str.Append("</ul></div>");
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return str.ToString();
    }

	 public string get_Early_Payment_Report_Details(string username, string name, int pageno, int rpp, string fdate, string tdate,string vendorname)
    {
        string strData = string.Empty;
        string isdata = string.Empty;
        StringBuilder html = new StringBuilder();
        try
        {
            html.Append("<table id='data-table1' runat='server' class='table table-bordered table-hover'>");
            html.Append("<thead><tr class='grey center'><th>PO No</th><th>Vendor Name</th><th>Unique No</th><th>Dispatch Note</th><th>Invoice No</th><th>Invoice Amt</th><th>Dispatch Quantity</th><th>Status</th><th>Date</th></tr></thead>");
            html.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "User_Plant_Mapping_Report.aspx", "getearlypaymentrpt", ref isdata, username, name, fdate, tdate,vendorname);
            if (dtData != null)
            {
                //int ddl = rpp;
                //int from = (pageno - 1) * ddl;
                //int to = ((pageno - 1) * ddl) + ddl;
                //for (int i = from; i < to; i++)
                //{
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtData.Rows[i]["PO_Number"]));
                    html.Append("<tr><td><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'><input type='text' id='venCode" + (i + 1) + "' value=" + Convert.ToString(dtData.Rows[i]["vendor_code"]) + " style='display:none'><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dtData.Rows[i]["PO_Number"]) + "</a></td>" +
                        "<td>" + Convert.ToString(dtData.Rows[i]["Vendor_Name"]) + "</td> " +
                        "<td>" + Convert.ToString(dtData.Rows[i]["Unique_No"]) + "</td> " +
                        "<td><a href='#paymentterm' role='button' data-toggle='modal' onclick='setSelectedNote(" + Convert.ToString(dtData.Rows[i]["PK_Dispatch_Note_ID"]) + ")'>" + Convert.ToString(dtData.Rows[i]["request_id"]) + "</a></td>" +
                        "<td>" + Convert.ToString(dtData.Rows[i]["Invoice_No"]) + "</td>" +
                        "<td style='text-align: right;'>" + Convert.ToString(dtData.Rows[i]["Invoice_Amount"]) + "</td>" +
                        "<td>" + Convert.ToString(dtData.Rows[i]["Dispatch_Quantity"]) + "</td>" +
                        "<td>" + Convert.ToString(dtData.Rows[i]["Status"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["modified_date"]) + "</td></tr>");
                }
                //}
            }

            html.Append("</tbody>");
            html.Append("</table>");

            //double cnt = Convert.ToDouble(dtData.Rows.Count) / rpp;
            //if (cnt > Convert.ToInt16(Convert.ToInt32(dtData.Rows.Count) / rpp))
            //{
            //    cnt = (int)cnt + 1;
            //}

            //if (cnt > 1)
            //{
            //    html.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
            //    html.Append("<ul class='pagination'>");
            //    for (int j = 1; j <= cnt; j++)
            //    {
            //        html.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + rpp + ")'></li>");
            //    }
            //    html.Append("</ul></div>");

            //}
            strData = Convert.ToString(html);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return strData;
    }

	    public string get_pending_material(string username, string name, int pageno, int rpp,string fdate, string tdate,string vendorname, string material_code)
    {
        string strData = string.Empty;
        string isdata = string.Empty;
        StringBuilder html = new StringBuilder();
        try
        {
            html.Append("<table id='data-table1' class='table table-bordered table-hover' >");
            html.Append("<thead><tr class='grey center'><th>Material Code</th><th>Material Text</th><th>Vendor Name</th><th>PO No</th><th>PO Date</th><th>Quantity</th><th>Date OF GR</th><th>GR Type</th><th>Date</th></tr></thead>");
            html.Append("<tbody>");
            DataTable dtData = (DataTable)ActionController.ExecuteAction("", "User_Plant_Mapping_Report.aspx", "getpendingmaterial", ref isdata, username, name, fdate, tdate,vendorname,material_code);
            if (dtData != null)
            {
                //int ddl = rpp;
                //int from = (pageno - 1) * ddl;
                //int to = ((pageno - 1) * ddl) + ddl;
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    //if (i < dtData.Rows.Count)
                    //{
                        string encrypt_Str = crypt.Encryptdata(Convert.ToString(dtData.Rows[i]["PO_Number"]));
                        html.Append("<tr><td><input type='text' id='encrypt_po" + (i + 1) + "' value=" + encrypt_Str + " style='display:none'><input type='text' id='venCode" + (i + 1) + "' value=" + Convert.ToString(dtData.Rows[i]["vendor_code"]) + " style='display:none'>" + Convert.ToString(dtData.Rows[i]["Material_No"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["Storage_Location"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["Vendor_Name"]) + "</td><td><a href='#' role='button' id='anc" + (i + 1) + "' onclick='viewData(" + (i + 1) + ")'>" + Convert.ToString(dtData.Rows[i]["PO_Number"]) + "</a></td><td style='text-align: right;'>" + Convert.ToString(dtData.Rows[i]["Date"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["Dispatch_Quantity"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["BUDAT"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["BWART"]) + "</td><td>" + Convert.ToString(dtData.Rows[i]["modified_date"]) + "</td></tr>");
                   // }
                }
            }

            html.Append("</tbody>");
            html.Append("</table>");

            //double cnt = Convert.ToDouble(dtData.Rows.Count) / rpp;
            //if (cnt > Convert.ToInt16(Convert.ToInt32(dtData.Rows.Count) / rpp))
            //{
            //    cnt = (int)cnt + 1;
            //}

            //if (cnt > 1)
            //{
            //    html.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
            //    html.Append("<ul class='pagination'>");
            //    for (int j = 1; j <= cnt; j++)
            //    {
            //        html.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + rpp + ")'></li>");
            //    }
            //    html.Append("</ul></div>");
            //}
            strData = Convert.ToString(html);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return strData;
    }

public string fillDocumentDtl(string ObjectType, string ObjectValue)
    {
        string data = string.Empty;
        try
        {
            string isdata = string.Empty;
            string isValid = string.Empty;
            string DisplayData = string.Empty;
            DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getfilenames", ref isdata, ObjectType, ObjectValue);

            DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>File Type</th><th>File Name</th></tr></thead>";
            if (dsData != null)
            {
                for (int i = 0; i < dsData.Rows.Count; i++)
                {
                    DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dsData.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles(" + (i + 1) + ")>" + Convert.ToString(dsData.Rows[i]["filename"]) + "</a></td><td style='display:none;'>" + Convert.ToString(dsData.Rows[i]["OBJECT_VALUE"]) + "</td></tr>";
                }
            }
            DisplayData += "</table>";

            data = DisplayData.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
        return data;
    }

   /*public string fillDocumentDtl(string ObjectType, string ObjectValue)
    {
        string data = string.Empty;
        try
        {
            string isdata = string.Empty;
            string isValid = string.Empty;
            string DisplayData = string.Empty;
            DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getfilenames", ref isdata, ObjectType, ObjectValue);

            DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>";
            if (dsData != null)
            {
                for (int i = 0; i < dsData.Rows.Count; i++)
                {
                    DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dsData.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles(" + (i + 1) + ")>" + Convert.ToString(dsData.Rows[i]["filename"]) + "</a></td></tr>";
                }
            }
            DisplayData += "</table>";

            data = DisplayData.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
        return data;
    }*/

/*public string fillDocumentDtl(string ObjectType, string ObjectValue)
    {
        string data = string.Empty;
        try
        {
            string isdata = string.Empty;
            string isValid = string.Empty;
            string DisplayData = string.Empty;
            DataTable dsData = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getfilenames", ref isdata, ObjectType, ObjectValue);

            DisplayData = "<table class='table table-bordered' id='uploadTable'><thead><tr class='grey'><th style='width:5%'>#</th><th>Description</th><th>File Name</th></tr></thead>";
            if (dsData != null)
            {
                for (int i = 0; i < dsData.Rows.Count; i++)
                {
                    DisplayData += "<tr><td align='left'>" + (i + 1) + "</td><td>" + Convert.ToString(dsData.Rows[i]["DOCUMENT_TYPE"]) + "</td><td><a href='#' onclick=downloadfiles(" + (i + 1) + ")>" + Convert.ToString(dsData.Rows[i]["filename"]) + "</a></td><td style='display:none;'>" + Convert.ToString(dsData.Rows[i]["OBJECT_VALUE"]) + "</td></tr>";
                }
            }
            DisplayData += "</table>";

            data = DisplayData.ToString();
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
        return data;
    }
*/

    public static string getDirPath(string IdentityNo)
    {
        string foldername = IdentityNo.Replace('/', '-');
        string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
        string newPath = System.IO.Path.Combine(activeDir, foldername);
        if (System.IO.Directory.Exists(newPath))
        {
            return newPath;
        }
        else
        {
            System.IO.Directory.CreateDirectory(newPath);
            return newPath;
        }
    }
   
 public string get_Service_Dispatch_Details(int name)
    {
        StringBuilder html_Header = new StringBuilder();
        StringBuilder HTML = new StringBuilder();
        DataTable dts = new DataTable();
        string isValid = string.Empty;
        string userdata = string.Empty;
        try
        {
            string isdata = string.Empty;
            DataSet ds = (DataSet)ActionController.ExecuteAction("", "Service_Dispatch_Note_Report.aspx", "getpodetails", ref isdata, name);
            if (ds != null)
            {

                /*******************************************************************************************************PO Header***********************************************************************************************/

                html_Header.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>PO Header</h4></div><div class='panel-body'><div class='table-responsive'>");
                html_Header.Append("<table class='table table-bordered' width='100%'>");
                html_Header.Append("<tr style='background-color:grey; text-align:center; color:white;'><th style='text-align:center'>PO No</th><th style='text-align:center'>PO Date</th><th style='text-align:center'>Created By</th><th style='text-align:center'>PO Type</th><th style='text-align:center'>PO Value</th><th style='text-align:center;'>PO GV</th><th style='text-align:center;'>Cumulative Inv Amt</th><th style='text-align:center;'>Payment Terms</th></tr>");
                html_Header.Append("<tbody>");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dts = (DataTable)ActionController.ExecuteAction("", "Service_Dispatch_Note_Report.aspx", "getdtl", ref isdata, Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]), name);
                    string encrypt_Str = crypt.Encryptdata(Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]));
                    html_Header.Append("<tr><td style='text-align:center'><a href='#' role='button' id='anc1' onclick='viewData1()'>" + Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]) + "</a></td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["PO_DATE"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["PO_CREATED_BY"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[0].Rows[0]["po_type"]) + "</td><td style='text-align:right'>" + Convert.ToString(ds.Tables[0].Rows[0]["po_value"]) + "</td><td style='text-align:right;'>" + Convert.ToString(ds.Tables[0].Rows[0]["po_gv"]) + "</td><td style='text-align:right;'>" + Convert.ToString(ds.Tables[0].Rows[0]["Cumulative"]) + "</td><td style='text-align:center;'>" + Convert.ToString(ds.Tables[0].Rows[0]["PAYMENT_TERMS"]) + "<input type='text' id='encrypt1' value=" + encrypt_Str + " style='display:none'></td></tr>");
                }

                html_Header.Append("</tbody></table>");

                /******************************************************************************Invoice Details**************************************************************************************************************************/

		html_Header.Append("<div class='row'>");
                html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                html_Header.Append("<b>Invoice No :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-1'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Invoice_No"]));
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                html_Header.Append("<b>Invoice Date :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-2'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Invoice_Date"]));
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                html_Header.Append("<b>Invoice Amount :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-1'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Invoice_Amount"]));
                html_Header.Append("</div>");
		html_Header.Append("</div>");
		html_Header.Append("<div class='row'>");
                html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                html_Header.Append("<b>Delivery Note :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-1'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["Delivery_Note"]));
                html_Header.Append("</div>");
		html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                html_Header.Append("<b>From Date :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-2'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["FROM_DATE"]));
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                html_Header.Append("<b>To Date :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-2'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["TO_DATE"]));
                html_Header.Append("</div class='col-md-2'>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='row'>");
                html_Header.Append("<div class='col-md-2' style='text-align:right'>");
                html_Header.Append("<b>Location :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-2'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["SERVICE_LOCATION"]));
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-1' style='text-align:right'>");
                html_Header.Append("<b>Remark :</b>");
                html_Header.Append("</div>");
                html_Header.Append("<div class='col-md-2'>");
                html_Header.Append(Convert.ToString(ds.Tables[0].Rows[0]["REMARK"]));
                html_Header.Append("</div>");
                html_Header.Append("</div>");

                /*************************************************************************************Hide/Show Detail Link*******************************************************************************************************************/
                html_Header.Append("<div class='col-md-10'>&nbsp;</div>");
                html_Header.Append("<div class='col-md-2' style='text-align:right'><a id='showdtl' runat='server' onclick='Show_Dtl()' href='#'>Show Details</a>");
                html_Header.Append("<a id='hidedtl' runat='server' onclick='Hide_Dtl()' href='#' style='display:none'>Hide Details</a></div>");
                html_Header.Append("</div></div></div></div>");

                /****************************************************************************************PO Detail***************************************************************************************************************/
                html_Header.Append("<div class='col-md-12' id='div_dtl' runat='server' style='display:none'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>PO Details</h4></div><div class='panel-body'><div class='table-responsive'>");
                html_Header.Append("<table class='table table-bordered' width='100%'>");
                html_Header.Append("<tr style='background-color:grey; text-align:center; color:white;'><th></th><th>Sr No.</th><th style='text-align:center'>Material No</th><th style='text-align:center'>Plant</th><th style='text-align:center'>Material Desc</th><th style='text-align:center'>Quantity</th><th style='text-align:center'>UOM</th><th style='text-align:center'>Net Price</th></tr>");
                html_Header.Append("<tbody>");

                if (ds!= null && ds.Tables[1].Rows.Count > 0)
                {
                    string po_no = Convert.ToString(ds.Tables[0].Rows[0]["PO_Number"]);

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        string pno = Convert.ToString(ds.Tables[1].Rows[i]["PO_LINE_NUMBER"]);
                        html_Header.Append("<tr><td><div class='panel-title'><a data-toggle='collapse' href='#collapse" + pno + "'><i class='fa fa-plus'></i></a></div></td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["PO_LINE_NUMBER"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["material_no"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["plant"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["MATERIAL_DESC"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["quantity"]) + "</td><td style='text-align:center'>" + Convert.ToString(ds.Tables[1].Rows[i]["uom"]) + "</td><td style='text-align:right'>" + Convert.ToString(ds.Tables[1].Rows[i]["net_price"]) + "</td></tr>");                    
                        html_Header.Append("</tr>");
                        html_Header.Append("<tr id='collapse" + pno + "' class='panel-collapse collapse'>");
                        html_Header.Append("<td colspan='" + (dts.Columns.Count + 2) + "'>");
                        int id = 0;

                        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Service_Dispatch_Note_Report.aspx","dis_dtl", ref isValid, Convert.ToString(ds.Tables[1].Rows[0]["FK_SERV_PO_HDR_ID"]),Convert.ToString(ds.Tables[1].Rows[0]["FK_DTL_ID"]));

                        html_Header.Append("<table class='table table-bordered' id='tblservpodtl'><thead><tr class='grey'><th>Sr No.</th><th>Service No.</th><th>Short Text</th><th>Total Qty</th><th>Supplied Qty</th><th>Net Price</th></tr></thead>");
                        html_Header.Append("<tbody>");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                               
                                 html_Header.Append("<tr>");
                       		 html_Header.Append("<td> " + (j + 1) + "</td>");
                        	 html_Header.Append("<td>" + dt.Rows[j]["SERVICE_NO"].ToString() + "</td>");
	                         html_Header.Append("<td>" + dt.Rows[j]["SHORT_TEXT"].ToString() + "</td>");
          	                 html_Header.Append("<td style='text-align:right'>" + dt.Rows[j]["QUANTITY"].ToString() + "</td>");
                   	         html_Header.Append("<td style='text-align:right'>" + dt.Rows[j]["VALUE"].ToString() + "</td>");
                        	 html_Header.Append("<td style='text-align:right'>" + dt.Rows[j]["NET_PRICE"].ToString() + "</td>");
                                 html_Header.Append("</tr>");
                                
                            }
                        }
                        html_Header.Append("</tbody>");
                        html_Header.Append("</table>");
                        html_Header.Append("</td></tr>");
                     
                    }
                }

                html_Header.Append("</tbody></table>");
                html_Header.Append("</div></div></div></div>");

                
/********************************************************************************Documents***********************************
************************************************************************************/

                html_Header.Append("<div class='col-md-6'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>Documents</h4></div><div class='panel-body'>");
                html_Header.Append("<table class='table table-bordered' width='100%'>");
                html_Header.Append("<tbody>");
                html_Header.Append("<tr style='background-color:grey; text-align:center; color:white;'><td style='text-align:center'>Document Name</td><td style='text-align:center'>File Name</td></tr>");                DataTable dtd = (DataTable)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getfilenames", ref isdata, "SERVICE PO", Convert.ToString(ds.Tables[0].Rows[0]["SERV_PO_NO"]));

                if (dtd.Rows.Count > 0)
                {
                    for (int i = 0; i < dtd.Rows.Count; i++)
                    {
                        html_Header.Append("<tr><td style='text-align:center'>" + Convert.ToString(dtd.Rows[i]["DOCUMENT_TYPE"]) + "</td><td style='text-align:center'><a href='#' OnClick='downloadfiles(" + Convert.ToString(dtd.Rows[i]["PK_ID"]) + ")'>" + Convert.ToString(dtd.Rows[i]["FILENAME"]) + "</a></td></tr>");
                    }
                }
                html_Header.Append("</tbody>");
                html_Header.Append("</table></div></div></div>");

            
/****************************************************************************************************************************
***************************************************************************/

            }
            userdata = html_Header.ToString();

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return userdata;
    }
}
