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


public class Expense_Data
{
	public Expense_Data()
	{
		
	}

    public string fillPayment_Mode()
    {
        string disp_data = string.Empty;
        try
        {
            string isdata = string.Empty;
            DataTable dtPayment = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "selectlocation", ref isdata, "", "", "M_TRAVEL_PAYMENT_MODE", "");

            if (dtPayment != null)
            {
                disp_data += "0||---Select One---";
                if (dtPayment.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPayment.Rows.Count; i++)
                    {
                        disp_data += "@" + Convert.ToString(dtPayment.Rows[i]["PK_PAYMENT_MODE"]) + "||" + Convert.ToString(dtPayment.Rows[i]["PAYMENT_MODE"]);
                    }
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return disp_data;
    }

    public string fillLocation()
    {
        string disp_data = string.Empty;
        try
        {
            string isdata = string.Empty;
            DataTable dtLocation = (DataTable)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "selectlocation", ref isdata, "", "", "PAYMENT_LOCATION", "");
            if (dtLocation != null)
            {
                disp_data += "0||---Select One---";
                if (dtLocation.Rows.Count > 0)
                {
                    for (int i = 0; i < dtLocation.Rows.Count; i++)
                    {
                        disp_data += "@" + Convert.ToString(dtLocation.Rows[i]["PK_LOCATION_ID"]) + "||" + Convert.ToString(dtLocation.Rows[i]["LOCATION NAME"]);
                    }
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return disp_data;
    }

    public string get_Travel_Policy_Data(string search_data, int pageno, int rpp, string desg,string division)
    {
        StringBuilder str = new StringBuilder();
        try
        {
            DataTable DTS;
            string IsValid = string.Empty;
            string procedure = "X_Domestic_Policy";
            DTS = (DataTable)ActionController.ExecuteAction("", "Domestic_Policy.aspx", "select", ref IsValid, division, desg, procedure, search_data);
            DTS.Columns[0].ColumnName = "Sr.No";
            //Session["ResultData"] = DTS;

            str.Append("<table id='data-table1'  runat='server' class='table table-bordered table-hover'> <thead><tr class='grey'><th style='width:5%'>#</th><th>Designation</th><th>Journey Type</th><th>Travel Mode</th><th>Travel Class</th><th>Plant From</th><th>Plant To</th><th>Expense Head</th><th>Amount</th></tr></thead>");
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
                        str.Append("<tr>");
                        str.Append("<td align='left'><input type='text' Id='pk_dom_id" + (i + 1) + "' value='" + DTS.Rows[i]["PK_DOMESTIC_ID"].ToString() + "' style='display:none'/> " + (i + 1) + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["DESG_NAME"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["JOURNEY_TYPE"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["TRAVEL_NAME"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["TRAVEL_MAPPING_CLASS"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["PLANT_NAME"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["PLANT_NAME1"].ToString() + "</td>");
                        str.Append("<td align='left'>" + DTS.Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
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

    public string get_Journey_Data(string jFDate, string jTDate, int wiid)
    {
        string jdata = string.Empty;
        DateTime fdate = Convert.ToDateTime(jFDate);
        DateTime tdate = Convert.ToDateTime(jTDate);
        StringBuilder html = new StringBuilder();
        try
        {
            string IsValid = string.Empty;
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "pgetrequestdata", ref IsValid, wiid);

            int rno = 1;
            //while (fdate <= tdate)
            for (int from = 0; from < dsData.Tables[1].Rows.Count; from++)
            {
                html.Append("<tr>");
                html.Append("<td><input class='form-control input-sm' type='text' id='jrno" + rno + "' value=" + rno + " style='display:none'><span id='spn" + rno + "'><input class='form-control input-sm' type='text' id='jrny_date" + rno + "' value=" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + " style='display:none'>" + Convert.ToDateTime(fdate).ToString("dd-MMM-yyyy") + "</span></td>");
                DataSet dtJourney = (DataSet)ActionController.ExecuteAction("", "Travel_Request.aspx", "select", ref IsValid);
                if (dtJourney.Tables[0] != null)
                {
                    html.Append("<td><select ID='journey_Type" + rno + "' runat='server' name='jt' class='form-control input-sm' onchange='check_journey_Type(" + rno + "," + dtJourney.Tables[0].Rows.Count + ")' disabled='true'>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["journey_type"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[0].Rows[i]["PK_JOURNEY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[0].Rows[i]["JOURNEY_TYPE"]) + "</option>");
                        }
                    }
                    html.Append("</select></td>");
                }

                if (dtJourney.Tables[1] != null)
                {
                    html.Append("<td class='modetravel'>");
                    html.Append("<select ID='Travel_Mode" + rno + "' runat='server' class='form-control input-sm' style='display:normal' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[1].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["travel_mode"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[1].Rows[i]["TRAVEL_NAME"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[1].Rows[i]["PK_TRAVEL_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[1].Rows[i]["TRAVEL_NAME"]) + "</option>");
                        }
                    }
                    html.Append("</select>");

                    html.Append("<select ID='From_Plant" + rno + "' runat='server' class='form-control input-sm' style='display:normal' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[3].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["plant_from"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                        }
                    }
                    html.Append("</select>");

                    html.Append("<select ID='default_From" + rno + "' runat='server' class='form-control input-sm' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");
                    html.Append("</select>");

                    html.Append("</td>");
                }

                if (dtJourney.Tables[2] != null)
                {
                    html.Append("<td class='modetravel'>");
                    html.Append("<select ID='Travel_Class" + rno + "' runat='server' class='form-control input-sm' style='display:normal' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    DataTable dtClass = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "pgettravelclass", ref IsValid, Convert.ToString(dsData.Tables[1].Rows[from]["travel_mode"]));

                    for (int i = 0; i < dtClass.Rows.Count; i++)
                    {
                        if (Convert.ToString(dtClass.Rows[i]["PK_TRAVEL_MAPPING_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["travel_class"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtClass.Rows[i]["PK_TRAVEL_MAPPING_ID"]) + "' selected='true'>" + Convert.ToString(dtClass.Rows[i]["TRAVEL_MAPPING_CLASS"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtClass.Rows[i]["PK_TRAVEL_MAPPING_ID"]) + "'>" + Convert.ToString(dtClass.Rows[i]["TRAVEL_MAPPING_CLASS"]) + "</option>");
                        }
                    }
                    html.Append("</select>");

                    html.Append("<select ID='To_Plant" + rno + "' runat='server' class='form-control input-sm' style='display:normal' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[3].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["plant_to"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PK_TRAVEL_PLANT_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[3].Rows[i]["PLANT_NAME"]) + "</option>");
                        }
                    }
                    html.Append("</select>");

                    html.Append("<select ID='default_To" + rno + "' runat='server' class='form-control input-sm' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");
                    html.Append("</select>");

                    html.Append("</td>");
                }

                if (dtJourney.Tables[5] != null)
                {
                    html.Append("<td><select ID='From_City" + rno + "' runat='server' class='form-control input-sm' onchange='chk_class_From(" + rno + ")' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["from_city"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                        }
                    }

                    if (Convert.ToString(dsData.Tables[1].Rows[from]["from_city"]) != "-1")
                    {
                        html.Append("<option Value='-1'>Other</option>");
                        html.Append("</select><input type='text' class='form-control input-sm' id='txt_f_city" + rno + "' style='display:none' disabled></td>");
                    }
                    else
                    {
                        html.Append("<option Value='-1' selected='true'>Other</option>");
                        html.Append("</select><input type='text' class='form-control input-sm' id='txt_f_city" + rno + "' value='" + Convert.ToString(dsData.Tables[1].Rows[from]["other_f_city"]).Replace("'", "") + "' style='display:normal' disabled></td>");
                    }

                    html.Append("<td><select ID='To_City" + rno + "' runat='server' class='form-control input-sm' onchange='chk_class_To(" + rno + ")' disabled>");
                    html.Append("<option Value='0'>---Select One---</option>");

                    for (int i = 0; i < dtJourney.Tables[5].Rows.Count; i++)
                    {
                        if (Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) == Convert.ToString(dsData.Tables[1].Rows[from]["to_city"]))
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "' selected='true'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                        }
                        else
                        {
                            html.Append("<option Value='" + Convert.ToString(dtJourney.Tables[5].Rows[i]["PK_CITY_ID"]) + "'>" + Convert.ToString(dtJourney.Tables[5].Rows[i]["NAME"]) + "</option>");
                        }
                    }
                    string CLASSIFICATION = Convert.ToString(dsData.Tables[1].Rows[from]["CLASSIFICATION"]);
                    if (Convert.ToString(dsData.Tables[1].Rows[from]["to_city"]) != "-1")
                    {
                        html.Append("<option Value='-1'>Other</option>");
                        html.Append("</select><input type='text' id='cls" + rno + "' value='" + CLASSIFICATION + "' style='display:none'><input type='text' class='form-control input-sm' id='txt_t_city" + rno + "' style='display:none' disabled></td>");
                    }
                    else
                    {
                        html.Append("<option Value='-1' selected='true'>Other</option>");
                        html.Append("</select><input type='text' id='cls" + rno + "' value='" + CLASSIFICATION + "' style='display:none'><input type='text' class='form-control input-sm' id='txt_t_city" + rno + "' value='" + Convert.ToString(dsData.Tables[1].Rows[from]["other_t_city"]).Replace("'", "") + "' style='display:normal' disabled></td>");
                    }

                }

                html.Append("</tr>");
                rno = rno + 1;
                fdate = fdate.AddDays(1);
            }

            jdata = Convert.ToString(html);

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return jdata;
    }

    public string get_Hotel_Booking(string jFDate, int cnt, string concat_data, string cls_val, int wiid)
    {
        StringBuilder hotel_data = new StringBuilder();
        string dt = string.Empty;
        string IsValid = string.Empty;
        try
        {
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "pgetrequestdata", ref IsValid, wiid);
            hotel_data.Append("<table id='tbl_hotelBooking' class='table table-striped table-bordered'>");
            hotel_data.Append("<thead><tr class='grey'><th style='width: 15%'>Date</th><th style='width: 15%'>Journey Type</th><th style='width: 15%'>Place Class</th><th style='width: 15%'>Room Type</th><th style='width: 15%'>Hotel Name</th><th style='width: 15%'>Contact No</th><th style='width: 15%; display:none'>Amount</th></tr></thead>");
            hotel_data.Append("<tbody>");

            string[] sp_data = concat_data.Split('@');
            string[] cls_data = cls_val.Split('@');
            for (int i = 1; i <= cnt; i++)
            {
                for (int j = 1; j <= sp_data.Length - 1; j++)
                {
                    if (i == Convert.ToInt32(sp_data[j - 1]))
                    {
                        int row = Convert.ToInt32(j - 1);
                        dt = Convert.ToDateTime(jFDate).AddDays(i - 1).ToString("dd-MMM-yyyy");

                        hotel_data.Append("<tr>");
                        hotel_data.Append("<td><input class='form-control input-sm' type='text' name='htl' id='hotel_rno" + i + "' value=" + sp_data[j - 1] + " style='display:none'>" + dt + "</td>");
                        hotel_data.Append("<td>Outside Plant</td>");
                        hotel_data.Append("<td>");
                        hotel_data.Append("<span id='placeclass" + i + "'>" + cls_data[j - 1] + "</span> Class");
                        hotel_data.Append("</td>");
                        hotel_data.Append("<td>");
                        hotel_data.Append("<select ID='roomType" + i + "' class='form-control input-sm'  disabled>");
                        hotel_data.Append("<option value='0'>---Select One---</option>");
                        if (Convert.ToString(dsData.Tables[1].Rows[i - 1]["room_type"]).ToUpper() == "SINGLE BED OCCUPANCY")
                        { hotel_data.Append("<option value='1' selected='true'>Single Bed Occupancy</option>"); }
                        else
                        { hotel_data.Append("<option value='1'>Single Bed Occupancy</option>"); }

                        if (Convert.ToString(dsData.Tables[1].Rows[i - 1]["room_type"]).ToUpper() == "DOUBLE BED OCCUPANCY")
                        { hotel_data.Append("<option value='2' selected='true'>Double Bed Occupancy</option></select>"); }
                        else
                        { hotel_data.Append("<option value='2'>Double Bed Occupancy</option></select>"); }
                        hotel_data.Append("</td>");
                        hotel_data.Append("<td><input id='hotel_name" + i + "' class='form-control input-sm' type='text' value='" + Convert.ToString(dsData.Tables[1].Rows[i - 1]["hotel_name"]).Replace("'", "") + "' disabled></td>");
                        hotel_data.Append("<td><input id='hotel_no" + i + "' class='form-control input-sm' type='text' value='" + Convert.ToString(dsData.Tables[1].Rows[i - 1]["hotel_no"]).Replace("'", "") + "' disabled></td>");
                        hotel_data.Append("<td style='display:none'><input id='hotel_amount" + i + "' class='form-control input-sm' type='number' min='0' value='" + Convert.ToString(dsData.Tables[1].Rows[i - 1]["hotel_charge"]) + "' disabled></td>");
                        hotel_data.Append("</tr>");

                    }
                }
            }


            hotel_data.Append("</tbody></table>");
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(hotel_data);
    }

    public string get_Final_Data(string jFDate, string jTDate, int jr, string jt_data, string concat_data, string jt_val, string jt_amt, string to_city, string desg, int wiid, string travel_class_id, string plant_id)
    {
        StringBuilder hotel_data = new StringBuilder();
        string dt = string.Empty;
        string is_data = string.Empty;
        string doa = "";
        try
        {
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "pgetrequestdata", ref is_data, wiid);

            string[] j_type = jt_data.Split('@');
            string[] j_val = jt_val.Split('@');
            string[] row = concat_data.Split('@');
            string[] h_amt = jt_amt.Split('@');
            string[] pk_id = to_city.Split('@');
            string[] travel_class = travel_class_id.Split('@');
            string[] travel_plant = plant_id.Split('@');
            string ids = "";
            for (int x = 0; x < j_val.Length - 1; x++)
            {
                if (j_val[x + 1] != "")
                {
                    ids += j_val[x] + ",";
                }
                else
                {
                    ids += j_val[x];
                }

            }

            DataTable ejm = (DataTable)ActionController.ExecuteAction("", "Travel_Request.aspx", "getexpenseids", ref is_data, ids);
            hotel_data.Append("<table id='tbl_hotelBooking' class='table table-striped table-bordered'>");
            hotel_data.Append("<thead>");
            hotel_data.Append("<tr class='grey' style='font-weight:bold'><th style='width:5%'>Date</th><th>Journey Type</th><th>Particulars</th>");
            if (ejm.Rows.Count > 0)
            {
                for (int y = 0; y < ejm.Rows.Count; y++)
                {
                    hotel_data.Append("<th><input type='text' id='compare_id" + (y + 1) + "' value='" + ejm.Rows[y]["COMPARE_ID"] + "' style='color:black; display:none'><input type='text' id='compare_name" + (y + 1) + "' value='" + Convert.ToString(ejm.Rows[y]["COMP_NAME"]).Replace(" ", "_") + "' style='color:black; display:none'><input type='text' name='eh' id='expenses" + (y + 1) + "' value='" + ejm.Rows[y]["FK_EXPENSE_HEAD"] + "' style='color:black; display:none'><input type='text' id='exp_name" + (y + 1) + "' value='" + ejm.Rows[y]["EXPENSE_HEAD"] + "' style='color:black; display:none'><input type='text' id='e_name" + (y + 1) + "' value='" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "' style='color:black; display:none'>" + ejm.Rows[y]["EXPENSE_HEAD"] + "</th>");
                }
            }
            hotel_data.Append("<th style='display:none'>Hotel Charge</th><th>Total</th></tr>");
            hotel_data.Append("</thead>");
            hotel_data.Append("<tbody>");


            for (int i = 1; i <= j_type.Length; i++)
            {
                dt = Convert.ToDateTime(jFDate).AddDays(i - 1).ToString("dd-MMM-yyyy");

                int journey_Type = Convert.ToInt32(j_val[i - 1]);

                string val = (string)ActionController.ExecuteAction("", "Travel_Request.aspx", "gettravelclass", ref is_data, pk_id[i - 1]);
                if (pk_id[i - 1] == "0")
                {
                    val = "A";
                }
                hotel_data.Append("<tr>");
                hotel_data.Append("<td>" + dt + "</td>");
                hotel_data.Append("<td>" + j_type[i - 1] + "</td>");
                hotel_data.Append("<td><input type='text' id='remark_note" + i + "' value='" + Convert.ToString(dsData.Tables[1].Rows[i - 1]["remark_note"]).Replace("'", "") + "' class='form-control input-sm' disabled></td>");
                string value = "0";
                if (ejm.Rows.Count > 0)
                {
                    for (int y = 0; y < ejm.Rows.Count; y++)
                    {
                        string cnt = (string)ActionController.ExecuteAction("", "Travel_Request.aspx", "getjmcount", ref is_data, j_val[i - 1], ejm.Rows[y]["FK_EXPENSE_HEAD"]);
                        value = (string)ActionController.ExecuteAction("", "Travel_Request.aspx", "getpolicyamount", ref is_data, desg, journey_Type, ejm.Rows[y]["FK_EXPENSE_HEAD"], "A", travel_class[i - 1], travel_plant[i - 1]);
                        string perc = (string)ActionController.ExecuteAction("", "Travel_Request.aspx", "getclasswiseperc", ref is_data, val);
                        //if (value !="" && val == "B")
                        //{
                        //    value = Convert.ToString(Convert.ToDouble(value)*85/100);
                        //}
                        //else if (value != "" && val == "C")
                        //{
                        //    value = Convert.ToString(Convert.ToDouble(value) * 70 / 100);
                        //}
                        if (value == "")
                        {
                            value = "0";
                        }
                        value = Convert.ToString(Convert.ToDouble(value) * Convert.ToDouble(perc) / 100);
                        DataTable dtamt = (DataTable)ActionController.ExecuteAction("", "Travel_Request_Approval.aspx", "getexpamount", ref is_data, Convert.ToString(dsData.Tables[0].Rows[0]["pk_travel_expense_hdr_id"]), dt, ejm.Rows[y]["FK_EXPENSE_HEAD"]);
                        string iamt = "0";
                        string is_cr = "0";
                        if (dtamt != null)
                        {
                            if (dtamt.Rows.Count > 0)
                            {
                                iamt = Convert.ToString(dtamt.Rows[0]["amount"]);
                                is_cr = Convert.ToString(dtamt.Rows[0]["IS_CR"]);
                            }
                        }

                        if (value != "")
                        {
                            if (Convert.ToDouble(value) < Convert.ToDouble(iamt))
                            {
                                doa = "1";
                            }
                        }
                        if (Convert.ToString(ejm.Rows[y]["IS_ALLOW"]) == "1" && cnt != "0")
                        {
                            if (is_cr == "0")
                            {
                                hotel_data.Append("<td><table><tr><td><input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + Convert.ToString(ejm.Rows[y]["DIRECT_ALLOW"]) + "' style='display:none'><input type='checkbox' onchange='calculate_Amount();' id='IS_CR_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' disabled></td><td><input type='number' min='0' id='" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + iamt + "' class='form-control input-sm' style='text-align:right; width:70px' onkeyup='calculate_Amount();' disabled><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'></td></tr></table></td>");
                            }
                            else
                            {
                                hotel_data.Append("<td><table><tr><td><input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + Convert.ToString(ejm.Rows[y]["DIRECT_ALLOW"]) + "' style='display:none'><input type='checkbox' onchange='calculate_Amount();' id='IS_CR_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' checked='true' disabled></td><td><input type='number' min='0' id='" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + iamt + "' class='form-control input-sm' style='text-align:right; width:70px' onkeyup='calculate_Amount();' disabled><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; display:none'></td></tr></table></td>");
                            }
                        }
                        else
                        {

                            if (value == "")
                            {
                                value = "0";
                            }
                            hotel_data.Append("<td><table><tr><td><input type='text' id='D_ALLOW_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + Convert.ToString(ejm.Rows[y]["DIRECT_ALLOW"]) + "' style='display:none'><input type='checkbox' onchange='calculate_Amount();' id='IS_CR_" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' disabled></td><td><input type='number' min='0' id='" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:righ; width:70pxt' readonly><input type='number' min='0' id='h" + Convert.ToString(ejm.Rows[y]["EXPENSE_HEAD"]).Replace(" ", "_") + "_" + i + "_" + (y + 1) + "' value='" + value + "' class='form-control input-sm' style='text-align:right; width:70px; display:none'></td></tr></table></td>");
                        }
                    }
                }


                hotel_data.Append("<td style='text-align:right; display:none'><span id='Hotel_Charge" + i + "'>" + h_amt[i - 1] + "</span></td>");
                hotel_data.Append("<td style='text-align:right'><span id='Total" + i + "'>0</span></td>");
                hotel_data.Append("</tr>");
            }

            hotel_data.Append("<tr>");
            hotel_data.Append("<td colspan='" + (ejm.Rows.Count + 3) + "' style='text-align:right'><b>Final Amount : </b></td>");
            hotel_data.Append("<td style='text-align:right'><b><span id='Final_Amtt'>0</span></b></td>");
            hotel_data.Append("</tr>");
            hotel_data.Append("</tbody></table>");
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return Convert.ToString(hotel_data) + "||" + doa;
    }


}