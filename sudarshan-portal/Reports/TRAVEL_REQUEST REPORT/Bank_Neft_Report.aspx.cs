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

public partial class Bank_Neft_Report : System.Web.UI.Page
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
                //Response.Write("<script>alert('"+Convert.ToDateTime(DateTime.Now).ToString("MM/dd/yyyy")+"');</script>");
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Bank_Neft_Report));
                if (!Page.IsPostBack)
                {

                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        Bind_bank_Data();
                    }

                    from_date.Attributes.Add("readonly", "readonly");
                    to_date.Attributes.Add("readonly", "readonly");
                }
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void Bind_bank_Data()
    {
        string isvalid = string.Empty;
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getbankdata", ref isvalid, txt_Username.Text, 0);
                ddlBank.Items.Clear();
                if (dt != null)
                {
                    ddlBank.DataSource = dt;
                    ddlBank.DataTextField = "BANK_NAME";
                    ddlBank.DataValueField = "PK_BANK_ID";
                    ddlBank.DataBind();
                }
                ddlBank.Items.Insert(0, "---Select One---");
            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected void BindGridview()
    {
        string isvalid = string.Empty;
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                json_data.Text = ddlBank.SelectedItem.Text;
                txt_f_date.Text = from_date.Text;
                txt_t_date.Text = to_date.Text;

                gvhdfc2.DataSource = null;
                gvhdfc2.DataBind();
                gvSelected.DataSource = null;
                gvSelected.DataBind();

                //DataTable dt = (DataTable)ActionController.ExecuteAction("", "Bank_Neft_Report.aspx", "getreport", ref isvalid, txt_Username.Text, ddlBank.SelectedItem.Text, 0);
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Bank_Neft_Report.aspx", "getreport", ref isvalid, txt_Username.Text, txt_f_date.Text, txt_t_date.Text, json_data.Text);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (ddlBank.SelectedItem.Text.ToUpper() == "HDFC")
                        {
                            gvhdfc2.DataSource = dt;
                            gvhdfc2.DataBind();
                        }
                        else
                        {
                            gvSelected.DataSource = dt;
                            gvSelected.DataBind();
                        }
                        
                        btnExp.Visible = true;
                        pk_bank_id.Text = ddlBank.SelectedValue;
                        json_data.Text = ddlBank.SelectedItem.Text;
                    }
                    else
                    {
                        gvSelected.DataSource = null;
                        gvSelected.DataBind();
                        gvhdfc2.DataSource = null;
                        gvhdfc2.DataBind();
                        btnExp.Visible = false;
                    }
                }
                else
                {
                    gvSelected.DataSource = null;
                    gvSelected.DataBind();
                    gvhdfc2.DataSource = null;
                    gvhdfc2.DataBind();
                    btnExp.Visible = false;
                }

            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

 
    protected void btnExp_Click(object sender, EventArgs e)
    {
        DataSet dt_sap_rfc = new DataSet();
        try
        {
            string data = string.Empty;
            string isvalid = string.Empty;
            DataTable dt = new DataTable();
            if (json_data.Text != "")
            {
                dt = (DataTable)ActionController.ExecuteAction("", "Bank_Neft_Report.aspx", "getreport", ref isvalid, txt_Username.Text, txt_f_date.Text, txt_t_date.Text, json_data.Text);
                /************************************************************************************************************************************************/
                Response.ClearContent();
                Response.Buffer = true;
                string fname = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") + " - " + txt_Username.Text + " - " + json_data.Text;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fname + ".xlsx"));
                  
//              Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fname+ "\".xls");
               Response.ContentType = "application/vnd.ms-excel";
 //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


                if (json_data.Text.ToUpper() == "HSBC")
                {
                    Response.Write("<table border=\"1\"><tr style=\"font-weight:bold\"><td>Transaction Type</td><td>Reference Number</td><td>Dr Account No</td><td>Bene Name</td><td>Remitter A/c no</td><td>Remitter Name</td><td>Narration</td><td>Value Date</td><td>Amount</td><td>eMail Address 1</td><td>eMail Address 2</td><td>eMail Address 3</td><td>Advise Col1</td><td>Advise Col2</td><td>Advise Col3</td><td>Advise Col4</td><td>Advise Col5</td><td>Bene Bank Account</td><td>BeneRTGSCodes</td><td>Bene Bank Name</td></tr>");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Response.Write("<tr><td>" + dt.Rows[i][0] + "</td><td>" + dt.Rows[i][1] + "</td><td>'" + Convert.ToString(dt.Rows[i][2]) + "</td><td>" + dt.Rows[i][3] + "</td><td>'" + Convert.ToString(dt.Rows[i][4]) + "</td><td>" + dt.Rows[i][5] + "</td><td>" + dt.Rows[i][6] + "</td><td>'" + dt.Rows[i][7] + "</td><td>" + dt.Rows[i][8] + "</td><td>" + dt.Rows[i][9] + "</td><td>" + dt.Rows[i][10] + "</td><td>" + dt.Rows[i][11] + "</td><td>" + dt.Rows[i][12] + "</td><td>" + dt.Rows[i][13] + "</td><td>" + dt.Rows[i][14] + "</td><td>" + dt.Rows[i][15] + "</td><td>" + dt.Rows[i][16] + "</td><td>'" + Convert.ToString(dt.Rows[i][17]) + "</td><td>" + dt.Rows[i][18] + "</td><td>" + dt.Rows[i][19] + "</td></tr>");
                    }
                    Response.Write("</table>");
                }
                else
                {
                    Response.Write("<table border=\"1\"><tr style=\"font-weight:bold\"><td>Transaction Type</td><td>Blank1</td><td>Beneficiary Account Number</td><td>Instrument Amount</td><td>Beneficiary Name</td><td>Blank2</td><td>Blank3</td><td>Bene Address 1</td><td>Bene Address 2</td><td>Bene Address 3</td><td>Bene Address 4</td><td>Bene Address 5</td><td>Instruction Reference Number</td><td>Customer Reference Number</td><td>Payment details 1</td><td>Payment details 2</td><td>Payment details 3</td><td>Payment details 4</td><td>Payment details 5</td><td>Payment details 6</td><td>Payment details 7</td><td>Blank5</td><td>Transaction Date</td><td>Blank1</td><td>IFSC Code</td><td>Bene Bank Name</td><td>Bene Bank Branch Name</td><td>Beneficiary email id</td></tr>");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Response.Write("<tr><td>" + dt.Rows[i][0] + "</td><td>" + dt.Rows[i][1] + "</td><td>'" + Convert.ToString(dt.Rows[i][2]) + "</td><td>" + dt.Rows[i][3] + "</td><td>" + dt.Rows[i][4] + "</td><td>" + dt.Rows[i][5] + "</td><td>" + dt.Rows[i][6] + "</td><td>" + dt.Rows[i][7] + "</td><td>" + dt.Rows[i][8] + "</td><td>" + dt.Rows[i][9] + "</td><td>" + dt.Rows[i][10] + "</td><td>" + dt.Rows[i][11] + "</td><td>" + dt.Rows[i][12] + "</td><td>" + dt.Rows[i][13] + "</td><td>" + dt.Rows[i][14] + "</td><td>" + dt.Rows[i][15] + "</td><td>" + dt.Rows[i][16] + "</td><td>" + dt.Rows[i][17] + "</td><td>" + dt.Rows[i][18] + "</td><td>" + dt.Rows[i][19] + "</td><td>" + dt.Rows[i][20] + "</td><td>" + dt.Rows[i][21] + "</td><td>'" + dt.Rows[i][22] + "</td><td>" + dt.Rows[i][23] + "</td><td>" + dt.Rows[i][24] + "</td><td>" + dt.Rows[i][25] + "</td><td>" + dt.Rows[i][26] + "</td><td>" + dt.Rows[i][27] + "</td></tr>");
                    }
                    Response.Write("</table>");
                }
                Response.End();
                /*******************************************************************************************************************************/
            }
            else
            { 
            
            }

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
    

    protected void btnData_Click(object sender, EventArgs e)
    {
        BindGridview();
    }
}