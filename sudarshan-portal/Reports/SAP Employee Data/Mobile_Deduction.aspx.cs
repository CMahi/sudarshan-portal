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

public partial class Mobile_Deduction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ActionController.DisablePageCaching(this);
            //if (ActionController.IsSessionExpired(this))
            //    ActionController.RedirctToLogin(this);
            //else
            //{
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Mobile_Deduction));
                if (!Page.IsPostBack)
                {
                    insertXML();
                }
            //}
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    private void insertXML()
    {
        StringBuilder Doc_Detail = new StringBuilder();
        string Pur_num = string.Empty;
        string rtr = string.Empty;
        string isdata = string.Empty;
        string XmlString = string.Empty;
        string isSaved;
        XmlString += "|ROWSET||";
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "SAP_Employee_Data.aspx", "getusers", ref isdata);
            if (dt.Rows.Count > 0)
            {
                int fy = 2016;
                string fm = "05";
                int curf = DateTime.Now.Year;
                string curm = (DateTime.Now.Month-3).ToString();
                if (Convert.ToInt32(curm) <= 0)
                {
                    curf = curf - 1;
                    curm = (12 + Convert.ToInt32(curm)).ToString();
                }
                if (Convert.ToInt32(curm) < 10)
                {
                    curm = "0" + curm;
                }
                
                //for (int ind = 1; fy <= curf && Convert.ToInt32(fm) <= Convert.ToInt32(curm); ind++)
                //{
                //    cnt = cnt + 1;
                //}


                string tempf = fy.ToString();
                string tempm = fm;
                string str = tempf + tempm;
                int months = (curf - fy) * 12 + Convert.ToInt32(curm) - Convert.ToInt32(fm);
                for (int j = 0; j < months; j++)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Pur_num += dt.Rows[i]["EMP_ID"].ToString() + '=';
                        Pur_num += str + '=';
                        Pur_num += '-';
                    }
                    if (tempm == "12")
                    {
                        tempf = (Convert.ToInt32(tempf) + 1).ToString();
                        tempm = "01";
                    }
                    else
                    {
                        tempm = (Convert.ToInt32(tempm) + 1).ToString();
                        if (Convert.ToInt32(tempm) < 10)
                        {
                            tempm = "0" + tempm;
                        }
                    }
                    str = tempf + tempm;
                }

                Vendor_Portal.Vendor_Portal_DetailsService Vendor = new Vendor_Portal.Vendor_Portal_DetailsService();
                string[] Vendor_data_array = new string[2];
                Vendor_data_array = Vendor.MOBILE_DEDUCTION(Pur_num.ToString());
                string[] cmpanyArray;
                cmpanyArray = Vendor_data_array[0].Split('|');
                if (cmpanyArray.Length > 0)
                {
                    for (int j = 0; j < cmpanyArray.Length - 1; j++)
                    {
                        string[] CompanyCode_Data;
                        CompanyCode_Data = cmpanyArray[j].Split('$');
                        XmlString += "|ROW||";
                        XmlString += "|PERNR||" + Convert.ToString(CompanyCode_Data[0]) + "|/PERNR||";
                        XmlString += "|YYMM||" + CompanyCode_Data[1] + "|/YYMM||";
                        XmlString += "|AMOUNT||" + CompanyCode_Data[2] + "|/AMOUNT||";
                        XmlString += "|WAERS||" + CompanyCode_Data[3] + "|/WAERS||";
                        XmlString += "|/ROW||";
                    }
                }

                XmlString += "|/ROWSET||";
                string inser_FileXML = XmlString;
                inser_FileXML = inser_FileXML.Replace("&", "&amp;");
                inser_FileXML = inser_FileXML.Replace(">", "&gt;");
                inser_FileXML = inser_FileXML.Replace("<", "&lt;");
                inser_FileXML = inser_FileXML.Replace("||", ">");
                inser_FileXML = inser_FileXML.Replace("|", "<");
                inser_FileXML = inser_FileXML.Replace("'", "&apos;");
                XmlString = inser_FileXML.ToString();
                //spn_data.InnerHtml = XmlString;
                isSaved = (string)ActionController.ExecuteAction("", "SAP_Employee_Data.aspx", "mobilededuction", ref isdata, XmlString);
                //spn_data.InnerText = Pur_num.ToString();
            }

        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
}