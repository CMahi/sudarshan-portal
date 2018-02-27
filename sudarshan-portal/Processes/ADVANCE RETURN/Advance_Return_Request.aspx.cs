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


public partial class Advance_Return_Request : System.Web.UI.Page
{
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Advance_Return_Request));
                if (!Page.IsPostBack)
                {
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                }

            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string bindReturnAdvance(string adv_type, string username)
    {
        string ret_data = string.Empty;
        string tblHeader = "";
        string tblBody = "";
        string isdata = string.Empty;
        try
        {
            if (adv_type == "1")
            {
                tblHeader = "<th>#</th><th>Request No.</th><th>Advance For</th><th>Advance Date</th><th>Payment Mode</th><th>Advance Amount</th>";
            }
            else
            {
                tblHeader = "<th>#</th><th>Request No.</th><th>Advance For</th><th>Advance Date</th><th>Base Currency</th><th>Advance Amount</th>";
            }

            DataTable dtadv = (DataTable)ActionController.ExecuteAction("", "Advance_Return_Request.aspx", "getpendingadvances", ref isdata, adv_type,username);
            if (dtadv != null)
            {
                for (int index = 0; index < dtadv.Rows.Count; index++)
                {
                    tblBody += "<tr>";
                    tblBody += "<td>" + (index+1) + "</td>";
                    if (adv_type == "1")
                    {
                        tblBody += "<td><a href='../../Portal/MenuRequestHandler.aspx?page=../Processes/ADVANCE RETURN/Advance_Return.aspx&processid=" + Convert.ToString(dtadv.Rows[index]["FK_PROCESS_ID"]) + "&paramname=wi&instanceid=" + Convert.ToString(dtadv.Rows[index]["FK_INSTANCE_ID"]) + "&stepname=" + Convert.ToString(dtadv.Rows[index]["PROCESS_NAME"]) + "&stepid=27&wiid=" + Convert.ToString(dtadv.Rows[index]["REQUEST_NO"]) + "' target='frmset_WorkArea' title='Click here to open the workitem.' runat='server'>" + Convert.ToString(dtadv.Rows[index]["REQUEST_NO"]) + "</a></td>";
                    }
                    else
                    {
                        tblBody += "<td><a href='../../Portal/MenuRequestHandler.aspx?page=../Processes/ADVANCE RETURN/Advance_Return_Foreign.aspx&processid=" + Convert.ToString(dtadv.Rows[index]["FK_PROCESS_ID"]) + "&paramname=wi&instanceid=" + Convert.ToString(dtadv.Rows[index]["FK_INSTANCE_ID"]) + "&stepname=" + Convert.ToString(dtadv.Rows[index]["REQUEST_NO"]) + "&stepid=27&wiid=" + Convert.ToString(dtadv.Rows[index]["WIID"]) + "' target='frmset_WorkArea' title='Click here to open the workitem.' runat='server'>" + Convert.ToString(dtadv.Rows[index]["REQUEST_NO"]) + "</a></td>";
                    }
                    tblBody += "<td>" + Convert.ToString(dtadv.Rows[index]["ADVANCE_TYPE_NAME"]) + "</td>";
                    tblBody += "<td>" + Convert.ToDateTime(dtadv.Rows[index]["CREATED_DATE"]).ToString("dd-MMM-yyyy") + "</td>";
                    tblBody += "<td>" + Convert.ToString(dtadv.Rows[index]["PAY_MODE"]) + "</td>";
                    tblBody += "<td style='text-align:right'>" + Convert.ToString(dtadv.Rows[index]["ADV_AMOUNT"]) + "</td>";
                    tblBody += "</tr>";
                }
            }

            ret_data = "<table id='data-table1' class='table table-bordered' align='center' width='100%'>" +
                              "<thead><tr  class='grey' >" + tblHeader + "</tr></thead>" +
                              "<tbody>" + tblBody + "</tbody>" +
                              "</table>";
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return ret_data;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=" + 2);
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }   
    }

        
}