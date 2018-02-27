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
//using System.Windows.Forms;
//using System.Web.Services;

public partial class Neft_Report : System.Web.UI.Page
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
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Neft_Report));
                if (!Page.IsPostBack)
                {

                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                        Bind_bank_Data();
                    }
                    btnExp.Enabled = false;
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
                    ddlBank.DataTextField= "BANK_NAME";
                    ddlBank.DataValueField = "PK_BANK_ID";
                    ddlBank.DataBind();
                }
                ddlBank.Items.Insert(0,"---Select One---");
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
                json_data.Text = "";
                gvDetails.DataSource = null;
                gvDetails.DataBind();
                gvhdfc1.DataSource = null;
                gvhdfc1.DataBind();
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getreport", ref isvalid, txt_Username.Text, ddlBank.SelectedItem.Text, 0);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (ddlBank.SelectedItem.Text.ToUpper() == "HDFC")
                        {
                            gvhdfc1.DataSource = dt;
                            gvhdfc1.DataBind();
                        }
                        else
                        {
                            gvDetails.DataSource = dt;
                            gvDetails.DataBind();
                        }
                        btnExport.Visible = true;
                        btnExp.Visible = true;
                        pk_bank_id.Text = ddlBank.SelectedValue;
                        json_data.Text = ddlBank.SelectedItem.Text;
                    }
                    else
                    {
                        gvDetails.DataSource = null;
                        gvDetails.DataBind();
                        gvhdfc1.DataSource = null;
                        gvhdfc1.DataBind();
                        btnExport.Visible = false;
                        btnExp.Visible = false;
                    }
                }
                else
                {
                    gvDetails.DataSource = null;
                    gvDetails.DataBind();
                    gvhdfc1.DataSource = null;
                    gvhdfc1.DataBind();
                    btnExport.Visible = false;
                    btnExp.Visible = false;
                }

            }
        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    //public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    //{
     

    //}

    protected void btnExp_Click(object sender, EventArgs e)
    {
        DataSet dt_sap_rfc = new DataSet();
        try
        {
            string data = string.Empty;
            DataTable dt = new DataTable();
            if (json_data.Text.ToUpper() == "HDFC")
            {
                dt.Columns.AddRange(new DataColumn[28] { new DataColumn("Transaction Type"), new DataColumn("Blank1"), new DataColumn("Beneficiary Account Number"), new DataColumn("Instrument Amount"), new DataColumn("Beneficiary Name"), new DataColumn("Blank2"), new DataColumn("Blank3"), new DataColumn("Bene Address 1"), new DataColumn("Bene Address 2"), new DataColumn("Bene Address 3"), new DataColumn("Bene Address 4"), new DataColumn("Bene Address 5"), new DataColumn("Instruction Reference Number"), new DataColumn("Customer Reference Number"), new DataColumn("Payment details 1"), new DataColumn("Payment details 2"), new DataColumn("Payment details 3"), new DataColumn("Payment details 4"), new DataColumn("Payment details 5"), new DataColumn("Payment details 6"), new DataColumn("Payment details 7"), new DataColumn("Blank4"), new DataColumn("Transaction Date"), new DataColumn("Blank5"), new DataColumn("IFSC Code"), new DataColumn("Bene Bank Name"), new DataColumn("Bene Bank Branch Name"), new DataColumn("Beneficiary email id") });

                foreach (GridViewRow grow in gvhdfc1.Rows)
                {
                    CheckBox chkdel = (CheckBox)grow.FindControl("chkCtrl");
                    if (chkdel.Checked)
                    {
                        string f1 = Convert.ToString(grow.Cells[1].Text);
                        string f2 = Convert.ToString(grow.Cells[2].Text);
                        string f3 = Convert.ToString(grow.Cells[3].Text);
                        string f4 = Convert.ToString(grow.Cells[4].Text);
                        string f5 = Convert.ToString(grow.Cells[5].Text);
                        string f6 = Convert.ToString(grow.Cells[6].Text);
                        string f7 = Convert.ToString(grow.Cells[7].Text);
                        string f8 = Convert.ToString(grow.Cells[8].Text);
                        string f9 = Convert.ToString(grow.Cells[9].Text);
                        string f10 = Convert.ToString(grow.Cells[10].Text);
                        string f11 = Convert.ToString(grow.Cells[11].Text);
                        string f12 = Convert.ToString(grow.Cells[12].Text);
                        string f13 = Convert.ToString(grow.Cells[13].Text);
                        string f14 = Convert.ToString(grow.Cells[14].Text);
                        string f15 = Convert.ToString(grow.Cells[15].Text);
                        string f16 = Convert.ToString(grow.Cells[16].Text);
                        string f17 = Convert.ToString(grow.Cells[17].Text);
                        string f18 = Convert.ToString(grow.Cells[18].Text);
                        string f19 = Convert.ToString(grow.Cells[19].Text);
                        string f20 = Convert.ToString(grow.Cells[20].Text);
                        string f21 = Convert.ToString(grow.Cells[21].Text);
                        string f22 = Convert.ToString(grow.Cells[22].Text);
                        string f23 = Convert.ToString(grow.Cells[23].Text);
                        string f24 = Convert.ToString(grow.Cells[24].Text);
                        string f25 = Convert.ToString(grow.Cells[25].Text);
                        string f26 = Convert.ToString(grow.Cells[26].Text);
                        string f27 = Convert.ToString(grow.Cells[27].Text);
                        string f28 = Convert.ToString(grow.Cells[28].Text);
                        dt.Rows.Add(f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18, f19, f20, f21, f22, f23, f24, f25, f26, f27, f28);
                        if (data == "")
                        {
                            data = f15;
                        }
                        else
                        {
                            data += "," + f15;
                        }
                    }
                }

                gvhdfc2.DataSource = dt;
                gvhdfc2.DataBind();
            }
            else if (json_data.Text.ToUpper() == "HSBC")
            {
                dt.Columns.AddRange(new DataColumn[21] { new DataColumn("Transaction Type"), new DataColumn("Reference Number1"), new DataColumn("Dr Account No"), new DataColumn("Bene Name"), new DataColumn("Remitter A/c no"), new DataColumn("Remitter Name"), new DataColumn("Narration"), new DataColumn("Value Date"), new DataColumn("Amount"), new DataColumn("eMail Address 1"), new DataColumn("eMail Address 2"), new DataColumn("eMail Address 3"), new DataColumn("Advise Col1"), new DataColumn("Advise Col2"), new DataColumn("Advise Col3"), new DataColumn("Advise Col4"), new DataColumn("Advise Col5"), new DataColumn("Bene Bank Account"), new DataColumn("BeneRTGSCodes"), new DataColumn("Bene Bank Name"), new DataColumn("Reference Number") });

                foreach (GridViewRow grow in gvDetails.Rows)
                {
                    CheckBox chkdel = (CheckBox)grow.FindControl("chkCtrl");
                    if (chkdel.Checked)
                    {
                        string f1 = Convert.ToString(grow.Cells[1].Text);
                        string f2 = Convert.ToString(grow.Cells[2].Text);
                        string f3 = Convert.ToString(grow.Cells[3].Text);
                        string f4 = Convert.ToString(grow.Cells[4].Text);
                        string f5 = Convert.ToString(grow.Cells[5].Text);
                        string f6 = Convert.ToString(grow.Cells[6].Text);
                        string f7 = Convert.ToString(grow.Cells[7].Text);
                        string f8 = Convert.ToString(grow.Cells[8].Text);
                        string f9 = Convert.ToString(grow.Cells[9].Text);
                        string f10 = Convert.ToString(grow.Cells[10].Text);
                        string f11 = Convert.ToString(grow.Cells[11].Text);
                        string f12 = Convert.ToString(grow.Cells[12].Text);
                        string f13 = Convert.ToString(grow.Cells[13].Text);
                        string f14 = Convert.ToString(grow.Cells[14].Text);
                        string f15 = Convert.ToString(grow.Cells[15].Text);
                        string f16 = Convert.ToString(grow.Cells[16].Text);
                        string f17 = Convert.ToString(grow.Cells[17].Text);
                        string f18 = Convert.ToString(grow.Cells[18].Text);
                        string f19 = Convert.ToString(grow.Cells[19].Text);
                        string f20 = Convert.ToString(grow.Cells[20].Text);
                        string f21 = Convert.ToString(grow.Cells[21].Text);
                        dt.Rows.Add(f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18, f19, f20, f21);
                        if (data == "")
                        {
                            data = f21;
                        }
                        else
                        {
                            data += "," + f21;
                        }
                    }
                }

                gvSelected.DataSource = dt;
                gvSelected.DataBind();
            }

            /************************************************************************************************************************************************/
            Response.ClearContent();
            Response.Buffer = true;
            //string fname = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") + " - " + txt_Username.Text + " - " + json_data.Text;
            //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fname + ".xls"));
            //Response.ContentType = "application/ms-excel";

            string fname = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") + " - " + txt_Username.Text + " - " + json_data.Text + ".xls";
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fname + "\"");



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
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet dt_sap_rfc = new DataSet();
        try
        {
		divIns.Style.Add("display","none");
            string data = string.Empty;
            DataTable dt = new DataTable();
            if (json_data.Text.ToUpper() == "HDFC")
            {
                dt.Columns.AddRange(new DataColumn[28] { new DataColumn("Transaction Type"), new DataColumn("Blank1"), new DataColumn("Beneficiary Account Number"), new DataColumn("Instrument Amount"), new DataColumn("Beneficiary Name"), new DataColumn("Blank2"), new DataColumn("Blank3"), new DataColumn("Bene Address 1"), new DataColumn("Bene Address 2"), new DataColumn("Bene Address 3"), new DataColumn("Bene Address 4"), new DataColumn("Bene Address 5"), new DataColumn("Instruction Reference Number"), new DataColumn("Customer Reference Number"), new DataColumn("Payment details 1"), new DataColumn("Payment details 2"), new DataColumn("Payment details 3"), new DataColumn("Payment details 4"), new DataColumn("Payment details 5"), new DataColumn("Payment details 6"), new DataColumn("Payment details 7"), new DataColumn("Blank4"), new DataColumn("Transaction Date"), new DataColumn("Blank5"), new DataColumn("IFSC Code"), new DataColumn("Bene Bank Name"), new DataColumn("Bene Bank Branch Name"), new DataColumn("Beneficiary email id") });

                foreach (GridViewRow grow in gvhdfc1.Rows)
                {
                    CheckBox chkdel = (CheckBox)grow.FindControl("chkCtrl");
                    if (chkdel.Checked)
                    {
                        string f1 = Convert.ToString(grow.Cells[1].Text);
                        string f2 = Convert.ToString(grow.Cells[2].Text);
                        string f3 = Convert.ToString(grow.Cells[3].Text);
                        string f4 = Convert.ToString(grow.Cells[4].Text);
                        string f5 = Convert.ToString(grow.Cells[5].Text);
                        string f6 = Convert.ToString(grow.Cells[6].Text);
                        string f7 = Convert.ToString(grow.Cells[7].Text);
                        string f8 = Convert.ToString(grow.Cells[8].Text);
                        string f9 = Convert.ToString(grow.Cells[9].Text);
                        string f10 = Convert.ToString(grow.Cells[10].Text);
                        string f11 = Convert.ToString(grow.Cells[11].Text);
                        string f12 = Convert.ToString(grow.Cells[12].Text);
                        string f13 = Convert.ToString(grow.Cells[13].Text);
                        string f14 = Convert.ToString(grow.Cells[14].Text);
                        string f15 = Convert.ToString(grow.Cells[15].Text);
                        string f16 = Convert.ToString(grow.Cells[16].Text);
                        string f17 = Convert.ToString(grow.Cells[17].Text);
                        string f18 = Convert.ToString(grow.Cells[18].Text);
                        string f19 = Convert.ToString(grow.Cells[19].Text);
                        string f20 = Convert.ToString(grow.Cells[20].Text);
                        string f21 = Convert.ToString(grow.Cells[21].Text);
                        string f22 = Convert.ToString(grow.Cells[22].Text);
                        string f23 = Convert.ToString(grow.Cells[23].Text);
                        string f24 = Convert.ToString(grow.Cells[24].Text);
                        string f25 = Convert.ToString(grow.Cells[25].Text);
                        string f26 = Convert.ToString(grow.Cells[26].Text);
                        string f27 = Convert.ToString(grow.Cells[27].Text);
                        string f28 = Convert.ToString(grow.Cells[28].Text);
                        dt.Rows.Add(f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18, f19, f20, f21, f22, f23, f24, f25, f26, f27, f28);
                        if (data == "")
                        {
                            data = f15;
                        }
                        else
                        {
                            data += "," + f15;
                        }
                    }
                }

                gvhdfc2.DataSource = dt;
                gvhdfc2.DataBind();
            }
            else if (json_data.Text.ToUpper() == "HSBC")
            {
                dt.Columns.AddRange(new DataColumn[21] { new DataColumn("Transaction Type"), new DataColumn("Reference Number1"), new DataColumn("Dr Account No"), new DataColumn("Bene Name"), new DataColumn("Remitter A/c no"), new DataColumn("Remitter Name"), new DataColumn("Narration"), new DataColumn("Value Date"), new DataColumn("Amount"), new DataColumn("eMail Address 1"), new DataColumn("eMail Address 2"), new DataColumn("eMail Address 3"), new DataColumn("Advise Col1"), new DataColumn("Advise Col2"), new DataColumn("Advise Col3"), new DataColumn("Advise Col4"), new DataColumn("Advise Col5"), new DataColumn("Bene Bank Account"), new DataColumn("BeneRTGSCodes"), new DataColumn("Bene Bank Name"), new DataColumn("Reference Number") });

                foreach (GridViewRow grow in gvDetails.Rows)
                {
                    CheckBox chkdel = (CheckBox)grow.FindControl("chkCtrl");
                    if (chkdel.Checked)
                    {
                        string f1 = Convert.ToString(grow.Cells[1].Text);
                        string f2 = Convert.ToString(grow.Cells[2].Text);
                        string f3 = Convert.ToString(grow.Cells[3].Text);
                        string f4 = Convert.ToString(grow.Cells[4].Text);
                        string f5 = Convert.ToString(grow.Cells[5].Text);
                        string f6 = Convert.ToString(grow.Cells[6].Text);
                        string f7 = Convert.ToString(grow.Cells[7].Text);
                        string f8 = Convert.ToString(grow.Cells[8].Text);
                        string f9 = Convert.ToString(grow.Cells[9].Text);
                        string f10 = Convert.ToString(grow.Cells[10].Text);
                        string f11 = Convert.ToString(grow.Cells[11].Text);
                        string f12 = Convert.ToString(grow.Cells[12].Text);
                        string f13 = Convert.ToString(grow.Cells[13].Text);
                        string f14 = Convert.ToString(grow.Cells[14].Text);
                        string f15 = Convert.ToString(grow.Cells[15].Text);
                        string f16 = Convert.ToString(grow.Cells[16].Text);
                        string f17 = Convert.ToString(grow.Cells[17].Text);
                        string f18 = Convert.ToString(grow.Cells[18].Text);
                        string f19 = Convert.ToString(grow.Cells[19].Text);
                        string f20 = Convert.ToString(grow.Cells[20].Text);
                        string f21 = Convert.ToString(grow.Cells[21].Text);
                        dt.Rows.Add(f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, f17, f18, f19, f20, f21);
                        if (data == "")
                        {
                            data = f21;
                        }
                        else
                        {
                            data += "," + f21;
                        }
                    }
                }

                gvSelected.DataSource = dt;
                gvSelected.DataBind();
            }
            if (data != "")
            {
                
                try
                {
                   // Response.End();
                }
                catch (Exception ex)
                { }
                finally
                {
                    string ref_key_no = "";
                    string rfc_string1 = string.Empty;
                    string ref_data = string.Empty;
                    string[] values = data.Split(',');
                    int cnt;
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (i == 0)
                        {
                            cnt = 1;
                        }
                        else
                        {
                            cnt = 0;
                        }
                        string rfc_string = string.Empty;
                        
                        string rfc_action = return_msg(values[i]);
                        dt_sap_rfc = (DataSet)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getbankrfc", ref ref_data, pk_bank_id.Text, rfc_action, values[i],cnt);
                        if (dt_sap_rfc != null)
                        {
                            if (dt_sap_rfc.Tables[0].Rows.Count > 0)
                            {
                                string ref_no = Convert.ToString(dt_sap_rfc.Tables[0].Rows[0][0]);
                            }
                            if (dt_sap_rfc.Tables[1].Rows.Count > 0)
                            {
                                for (int index = 0; index < dt_sap_rfc.Tables[1].Rows.Count; index++)
                                {
                                    if (rfc_string == "")
                                    {
                                        rfc_string += Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["COMP_CODE"]);
                                    }
                                    else
                                    {
                                        rfc_string += "|" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["COMP_CODE"]);
                                    }
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["DOC_DATE"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["PSTNG_DATE"]);
                                    rfc_string += "$";
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["DOC_TYPE"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["REF_DOC_NO"]);
                                    rfc_string += "$" + (index+1).ToString();
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["GL_ACCOUNT"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["REF_KEY_1"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["VENDOR_NO"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ALLOC_NMBR"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ITEM_TEXT"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["BUS_AREA"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["COSTCENTER"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["PROFIT_CTR"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["CURRENCY"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["AMT_DOCCUR"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ZLSCH"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["PERSON_NO"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["SECCO"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["BUPLA"]);
                                    rfc_string += "$" + Convert.ToString(dt_sap_rfc.Tables[1].Rows[index]["ZFBDT"]);
                                    rfc_string += "$ ";
                                }
                            }
                        }
                        rfc_string1 = rfc_string;
                    }
                    if (rfc_string1 != "")
                    {
                        DataTable dtBank = (DataTable)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getbankrfcsum", ref ref_data, pk_bank_id.Text);
                        if (dtBank != null && dtBank.Rows.Count > 0)
                        {
                            int li = dt_sap_rfc.Tables[1].Rows.Count + 1;
                            ref_key_no = Convert.ToString(dtBank.Rows[0]["REF_DOC_NO"]);
                            if (rfc_string1 == "")
                            {
                                rfc_string1 += Convert.ToString(dtBank.Rows[0]["COMP_CODE"]);
                            }
                            else
                            {
                                rfc_string1 += "|" + Convert.ToString(dtBank.Rows[0]["COMP_CODE"]);
                            }
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["DOC_DATE"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["PSTNG_DATE"]);
                            rfc_string1 += "$";
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["DOC_TYPE"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["REF_DOC_NO"]);
                            rfc_string1 += "$" + Convert.ToString(li);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["GL_ACCOUNT"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["REF_KEY_1"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["VENDOR_NO"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["REF_DOC_NO"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["ITEM_TEXT"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["BUS_AREA"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["COSTCENTER"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["PROFIT_CTR"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["CURRENCY"]);
                            rfc_string1 += "$" + Convert.ToInt32(dtBank.Rows[0]["AMT_DOCCUR"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["ZLSCH"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["PERSON_NO"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["SECCO"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["BUPLA"]);
                            rfc_string1 += "$" + Convert.ToString(dtBank.Rows[0]["ZFBDT"]);
                            rfc_string1 += "$ ";
                        }
                    }
                    Vendor_Portal.Vendor_Portal_DetailsService Vendor = new Vendor_Portal.Vendor_Portal_DetailsService();
                    string[] Vendor_data_array = new string[3];
                    Vendor_data_array = Vendor.BANK_DETAILS(rfc_string1,"");
                    //Vendor_data_array[0] = "";
                    string[] values1 = data.Split(',');
                    string[] sp_data = Convert.ToString(Vendor_data_array[0]).Split(' ');
                    string rfc_no = "";
                    if (Convert.ToString(Vendor_data_array[1]) == "S")
                    {
                        for (int k = 0; k < sp_data.Length; k++)
                        {
                            if (Convert.ToString(sp_data[k]).ToUpper().Contains("SCIL"))
                            {
                                rfc_no = Convert.ToString(sp_data[k]).Substring(0, 10);
                            }
                        }
                    }
                    for (int k = 0; k < values1.Length; k++)
                    {
                        string rfc = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "saverfcdata", ref ref_data, values1[k], "BANK", "", "", "", Convert.ToString(Vendor_data_array[1]).ToUpper(), rfc_no, Convert.ToString(Vendor_data_array[0]), ref_key_no);
                    }

                    if (Convert.ToString(Vendor_data_array[1]).ToUpper() != "E" && Convert.ToString(Vendor_data_array[1]) != "")
                    {

                        for (int k = 0; k < values1.Length; k++)
                        {
                            string dts = (string)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "changeflag", ref ref_data, txt_Username.Text, json_data.Text, values1[k]);
                        }
                        btnExp.Enabled = true;
                        btnExport.Enabled = false;
                        gvDetails.Style.Add("display","none");
                        gvhdfc1.Style.Add("display", "none");
                        if (Convert.ToString(Vendor_data_array[1]).ToUpper() == "S")
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Posted Successfully : " + rfc_no + "...!');}</script>");
                        }
                        else
                        {
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Posted Successfully : " + Convert.ToString(Vendor_data_array[0]) + "...!');}</script>");
                        }
                    }
                    else
                    {
gvSelected.Style.Add("display","none");	
	gvhdfc2.Style.Add("display","none");
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('"+ Convert.ToString(Vendor_data_array[0])+"...!');window.open('../../Portal/SCIL/Home.aspx','frmset_WorkArea');}</script>");
                    }
                    
                }
            }
            else
            {
		gvSelected.Style.Add("display","none");	
	gvhdfc2.Style.Add("display","none");
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Please Select atleast One Reference Number...!');}</script>");
            }
        }
        catch (Exception Exc) { 
gvSelected.Style.Add("display","none");	
	gvhdfc2.Style.Add("display","none");
FSL.Logging.Logger.WriteEventLog(false, Exc); }
    }

    protected string return_msg(string req_no)
    {
        string rfc_msg = "";
        if (req_no.Contains("ADV") == true)
        {
            rfc_msg = "ADVANCE PAID";
        }
        else if (req_no.Contains("DTE") == true)
        {
            rfc_msg = "DOMESTIC TRAVEL";
        }
        else if (req_no.Contains("MCE") == true)
        {
            rfc_msg = "MOBILE EXPENSE";
        }
        else if (req_no.Contains("OE") == true)
        {
            rfc_msg = "OTHER EXPENSE";
        }
        else if (req_no.Contains("LC") == true)
        {
            rfc_msg = "LOCAl CONVEYANCE";
        }
        else if (req_no.Contains("FA") == true)
        {
            rfc_msg = "FOREIGN ADVANCE";
        }
else
{
rfc_msg = "CAR EXPENSE";
}
        return rfc_msg;
    
    }

    protected void btnData_Click(object sender, EventArgs e)
    {
        BindGridview();
    }
}