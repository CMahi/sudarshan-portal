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

public partial class SAP_Employee_Data : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(SAP_Employee_Data));
                if (!Page.IsPostBack)
                {
                    insertXML();
                }
            }
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
            if (dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Pur_num += dt.Rows[i]["EMP_ID"].ToString() + '='; 
                   Pur_num+='-';
                }

                Vendor_Portal.Vendor_Portal_DetailsService Vendor = new Vendor_Portal.Vendor_Portal_DetailsService();
                string[] Vendor_data_array = new string[2];
                Vendor_data_array = Vendor.CMA_EMPLOYEE_DETAILS(DateTime.Now.ToString("yyyyMMdd"), Pur_num.ToString());
                string[] cmpanyArray;
                cmpanyArray = Vendor_data_array[0].Split('|');
                if (cmpanyArray.Length > 0)
                {
                    for (int j = 0; j < cmpanyArray.Length - 1; j++)
                    {
                        string[] CompanyCode_Data;
                        CompanyCode_Data = cmpanyArray[j].Split('$');
			if(CompanyCode_Data[2]!="")
			{
		        XmlString += "|ROW||";
                        XmlString += "|PERNR||" + Convert.ToString(CompanyCode_Data[0]) + "|/PERNR||";
                        XmlString += "|TITLE||" + CompanyCode_Data[1] + "|/TITLE||";
                        XmlString += "|FNAME||" + CompanyCode_Data[2] + "|/FNAME||";
                        XmlString += "|LNAME||" + CompanyCode_Data[3] + "|/LNAME||";
                        XmlString += "|CARAL||" + CompanyCode_Data[4] + "|/CARAL||";
                        XmlString += "|MEDIR||" + CompanyCode_Data[5] + "|/MEDIR||";
                        XmlString += "|WAERS||" + Convert.ToString(CompanyCode_Data[6]) + "|/WAERS||";
                        XmlString += "|MBACC||" + CompanyCode_Data[7] + "|/MBACC||";
                        XmlString += "|MIFSC||" + Convert.ToString(CompanyCode_Data[8]) + "|/MIFSC||";
                        XmlString += "|TBACC||" + CompanyCode_Data[9] + "|/TBACC||";
                        XmlString += "|TIFSC||" + CompanyCode_Data[10] + "|/TIFSC||";
                        XmlString += "|BUKRS||" + CompanyCode_Data[11] + "|/BUKRS||";
                        XmlString += "|BUTXT||" + CompanyCode_Data[12] + "|/BUTXT||";
                        XmlString += "|GSBER||" + Convert.ToString(CompanyCode_Data[13]) + "|/GSBER||";
                        XmlString += "|GTEXT||" + CompanyCode_Data[14] + "|/GTEXT||";
                        XmlString += "|WERKS||" + CompanyCode_Data[15] + "|/WERKS||";
                        XmlString += "|WERKT||" + CompanyCode_Data[16] + "|/WERKT||";
                        XmlString += "|BTRTL||" + CompanyCode_Data[17] + "|/BTRTL||";
                        XmlString += "|BTRTT||" + CompanyCode_Data[18] + "|/BTRTT||";
                        XmlString += "|PERSG||" + CompanyCode_Data[19] + "|/PERSG||";
                        XmlString += "|PERST||" + CompanyCode_Data[20] + "|/PERST||";
                        XmlString += "|PERSK||" + CompanyCode_Data[21] + "|/PERSK||";
                        XmlString += "|PERKT||" + CompanyCode_Data[22] + "|/PERKT||";
                        XmlString += "|ABKRS||" + CompanyCode_Data[23] + "|/ABKRS||";
                        XmlString += "|ABKRT||" + CompanyCode_Data[24] + "|/ABKRT||";
                        XmlString += "|KOSTL||" + CompanyCode_Data[25] + "|/KOSTL||";
                        XmlString += "|KOSTT||" + CompanyCode_Data[26] + "|/KOSTT||";
                        XmlString += "|ORGEH||" + CompanyCode_Data[27] + "|/ORGEH||";
                        XmlString += "|ORGTX||" + CompanyCode_Data[28] + "|/ORGTX||";
                        XmlString += "|ZBYD||" + CompanyCode_Data[29] + "|/ZBYD||";
                        XmlString += "|ZCPM||" + CompanyCode_Data[30] + "|/ZCPM||";
                        XmlString += "|ZDCN||" + CompanyCode_Data[31] + "|/ZDCN||";
			XmlString += "|CARMN||" + CompanyCode_Data[32] + "|/CARMN||";
			XmlString += "|FUELR||" + CompanyCode_Data[33] + "|/FUELR||";
			XmlString += "|DRVSL||" + CompanyCode_Data[34] + "|/DRVSL||";
			XmlString += "|POSTED_DATE||"+DateTime.Now.ToString("dd.MM.yyy")+"|/POSTED_DATE||";
			XmlString += "|ZLOC||"+CompanyCode_Data[35]+"|/ZLOC||";
			XmlString += "|ZDIV||"+CompanyCode_Data[36]+"|/ZDIV||";
                        XmlString += "|/ROW||";
			}
                       
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
                isSaved = (string)ActionController.ExecuteAction("", "SAP_Employee_Data.aspx", "insertreimbursement", ref isdata, XmlString);
            }

        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
}