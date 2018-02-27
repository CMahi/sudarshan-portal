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
using System.Web.Script.Services;

public partial class Vendor_Account_Statement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Vendor_Account_Statement));
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    app_Authority.Text = HttpContext.Current.Request.Url.Authority;

                    from_date.Attributes.Add("readonly", "readonly");
                    to_date.Attributes.Add("readonly", "readonly");
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Report_Master.aspx");

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected void ddlRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(comp_code.Text.Trim()=="")
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Enter Company Code!!!');}</script>");
        }
        else if (from_date.Text.Trim() == "")
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Enter From Date!!!');}</script>");
        }
        else if (to_date.Text.Trim() == "")
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Enter To Date!!!');}</script>");
        }
        else
        {
            string ccode = Convert.ToString(comp_code.Text.ToUpper()).Trim();
            string fdate = Convert.ToDateTime(from_date.Text).ToString("yyyy/MM/dd").Replace("/", "");
            string tdate = Convert.ToDateTime(to_date.Text).ToString("yyyy/MM/dd").Replace("/", "");
            string note_line = Noted_line.Text.Trim();
            string html = bindReport(ccode, fdate, tdate, txt_Search.Text.Trim(), note_line, 1, Convert.ToInt32(ddlRecords.SelectedItem.Text));
            div_ReportDetails.InnerHtml = Convert.ToString(html);
        }
    }

    protected string bindReport(string ccode,string fdate,string tdate,string note_line, string searchData, int pageno, int rpp)
    {
        StringBuilder str = new StringBuilder();
        try
        {
            DataTable dt = new DataTable();
            string vcode = Convert.ToString(Session["USER_ADID"]);
//            Vendor_Details.Vendor_Portal_DetailsService Vendor = new Vendor_Details.Vendor_Portal_DetailsService();
             Vendor_Portal.Vendor_Portal_DetailsService Vendor =new Vendor_Portal.Vendor_Portal_DetailsService();
            string[] Vendor_data_array = new string[2];
            Vendor_data_array = Vendor.VENDOR_AC_STATEMENT_DETAILS(ccode, vcode, fdate, tdate, note_line);
            //Vendor_data_array[0] = "d1$2$$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|d1$2$$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|d1$2$$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|1$2$2016-03-02$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|1$2$2016-03-02$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|1$2$2016-03-06$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|1$2$2016-03-02$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|d1$2$$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|d1$2$$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|d1$2$$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|1$2$2016-03-02$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|1$2$2016-03-02$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|1$2$2016-03-06$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20|1$2$2016-03-02$4$5$$v7$2016-03-01$9$10$11$12$$14$15$$17$18$19$20";
           // Vendor_data_array[1] = "";
            if (Convert.ToString(Vendor_data_array[0]) == "")
            {
                dt = null;
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + Convert.ToString(Vendor_data_array[1]) + "');}</script>");
            }
            else
            {

                dt.Columns.Add("CLEAR_DATE");
                dt.Columns.Add("CLR_DOC_NO");
                dt.Columns.Add("DOC_NO");
                dt.Columns.Add("PSTNG_DATE");
                dt.Columns.Add("REF_DOC_NO");
                dt.Columns.Add("BUS_AREA");
                dt.Columns.Add("LC_AMOUNT");
                dt.Columns.Add("W_TAX_BASE");


                string[] cmpanyArray;
                cmpanyArray = Vendor_data_array[0].Split('|');
                for (int j = 0; j < cmpanyArray.Length; j++)
                {
                    if (j < cmpanyArray.Length)
                    {
                        string[] val;
                        val = cmpanyArray[j].Split('$');
                        int flag=0;
                        for (int inc = 0; inc < val.Length; inc++)
                        {
                            string strData = Convert.ToString(val[inc]);
                            if (strData.ToLower().Contains(searchData.ToLower()))
                            {
                                flag = 1;
                            }
                        }
                        if (flag==1)
                        {
                            string[] CompanyCode_Data;
                            CompanyCode_Data = cmpanyArray[j].Split('$');
                            if (CompanyCode_Data.Length != 1)
                            {
                                DataRow dr = dt.NewRow();
                              
                                if (CompanyCode_Data[0] != "0000-00-00" && CompanyCode_Data[0] != "")
                                {
                                    dr["CLEAR_DATE"] = Convert.ToDateTime(CompanyCode_Data[0]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    dr["CLEAR_DATE"] = "";
                                }
                                dr["CLR_DOC_NO"] = CompanyCode_Data[1];
                                dr["DOC_NO"] = CompanyCode_Data[2];
                                if (CompanyCode_Data[3] != "0000-00-00" && CompanyCode_Data[3] != "")
                                {
                                    dr["PSTNG_DATE"] = Convert.ToDateTime(CompanyCode_Data[3]).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    dr["PSTNG_DATE"] = "";
                                }
                                dr["REF_DOC_NO"] = CompanyCode_Data[4];
                                dr["BUS_AREA"] = CompanyCode_Data[5];
                                dr["LC_AMOUNT"] = CompanyCode_Data[6];
                                dr["W_TAX_BASE"] = CompanyCode_Data[7];
                                dt.Rows.Add(dr);
                                dt.AcceptChanges();
                            }
                        }
                    }
                }
            }

            str.Append("<div class='row'>");
            str.Append("<table class='table table-bordered table-hover'> <thead>");
            str.Append("<tr class='grey'><th>Clearing Date</th><th>Clearing Doc No.</th><th>Acc Doc No</th><th>Posting Date</th><th>Ref Doc No</th><th>Business Area</th><th>Amt in Local Currency</th><th>Withholding Tax Base Amount</th></tr> ");
            str.Append("</thead><tbody>");
            if (dt != null)
            {

                int from = (pageno - 1) * rpp;
                int to = ((pageno - 1) * rpp) + rpp;
                for (int i = from; i < to; i++)
                {
                    if (i < dt.Rows.Count)
                    {

                        str.Append("<tr class='grey'><td>" + Convert.ToString(dt.Rows[i]["CLEAR_DATE"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["CLR_DOC_NO"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["DOC_NO"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["PSTNG_DATE"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["REF_DOC_NO"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["BUS_AREA"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["LC_AMOUNT"]) + "</td><td>" + Convert.ToString(dt.Rows[i]["W_TAX_BASE"]) + "</td></tr> ");

                    }
                }
            }
            str.Append("</tbody></table> ");


            double cnt = Convert.ToDouble(dt.Rows.Count) / rpp;
            if (cnt > Convert.ToInt16(Convert.ToInt32(dt.Rows.Count) / rpp))
            {
                cnt = (int)cnt + 1;
            }

            if (cnt > 1)
            {
                str.Append("<div class='dataTables_paginate paging_simple_numbers' style='text-align:center'>");
                str.Append("<ul class='pagination'>");
                for (int j = 1; j <= cnt; j++)
                {
                    str.Append("<li class='paginate_button' style='margin:2px;'><input type='button' value='" + j + "' class='btn btn-default buttons-copy buttons-flash' style=' font-weight:normal' onclick='gotopage(this," + rpp + ")'></li>");
                }
                str.Append("</ul></div>");
            }
            str.Append("</div>");
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return Convert.ToString(str);
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string ccode = Convert.ToString(comp_code.Text.ToUpper()).Trim();
        string fdate = Convert.ToDateTime(from_date.Text).ToString("yyyy/MM/dd").Replace("/", "");
        string tdate = Convert.ToDateTime(to_date.Text).ToString("yyyy/MM/dd").Replace("/", "");
        string note_line = Noted_line.Text.Trim();
        string html = bindReport(ccode,fdate,tdate,txt_Search.Text.Trim(),note_line, 1, Convert.ToInt32(ddlRecords.SelectedItem.Text));
        div_ReportDetails.InnerHtml = Convert.ToString(html);
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillSearch(string ccode, string fdate, string tdate, string noteline, string name, int rpp)
    {
        string html = bindReport(ccode, fdate, tdate, noteline,name, 1, rpp);
        return html;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string fillGoToPage1(string ccode, string fdate, string tdate, string noteline, string name, int pageno, int rpp)
    {
        string html = bindReport(ccode, fdate, tdate, noteline, name, pageno, rpp);
        return html;
    }

}