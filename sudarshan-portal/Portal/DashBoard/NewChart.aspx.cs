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

public partial class NewChart : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(NewChart));
                if (!Page.IsPostBack)
                {
                  
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
     [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string BindChart(string vcode)
    {
        StringBuilder str = new StringBuilder();
        string mon = string.Empty;
        string isdata = string.Empty;
        DataTable dt = new DataTable();
        DataSet ds = (DataSet)ActionController.ExecuteAction("", "NewChart.aspx", "pgetdata", ref isdata, vcode);
        if (ds != null)
        {
            DataTable dtAll = ds.Tables[0];
            DataTable dtPlant = ds.Tables[1];
            DataTable dtMonth = ds.Tables[2];
            
            try
            {
                
                for (int i = 0; i < dtPlant.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        mon += "||";
                    }
                    mon += Convert.ToString(dtPlant.Rows[i][0]);
                }
                mon += "$$";
                for (int i = 0; i < dtMonth.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        mon += "||";
                    }
                    mon += Convert.ToString(dtMonth.Rows[i][0]);
                }
                mon += "$$";
                for (int j = 0; j < dtPlant.Rows.Count; j++)
                {
                    if (j > 0)
                    {
                        mon += ";;";
                    }
                    string plant = Convert.ToString(dtPlant.Rows[j][0]);
                    for (int i = 0; i < dtMonth.Rows.Count; i++)
                    {
                        string month = Convert.ToString(dtMonth.Rows[i][0]);

                        for (int k = 0; k < dtAll.Rows.Count; k++)
                        {
                            if (plant == Convert.ToString(dtAll.Rows[k][2]) && month == Convert.ToString(dtAll.Rows[k][1]))
                            {
                                if (i > 0)
                                {
                                    mon += "||";
                                }
                                mon += Convert.ToString(dtAll.Rows[k][3]);
                                break;
                            }
                        }

                    }
                    
                }
            }
            catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }

        }
        return Convert.ToString(mon);
    }

     protected void btnCancel_Click(object sender, EventArgs e)
     {
         try
         {
             Response.Redirect("../SCIL/Home.aspx");

         }
         catch (Exception ex)
         {
             FSL.Logging.Logger.WriteEventLog(false, ex);
         }
     }
     protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
     {
         Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "view_chart()", true);
     }
}