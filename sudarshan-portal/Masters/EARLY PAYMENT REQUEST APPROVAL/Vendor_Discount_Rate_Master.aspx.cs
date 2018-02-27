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
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class Vendor_Discount_Rate_Master : System.Web.UI.Page
{
    string IsData = string.Empty;
    StringBuilder str = new StringBuilder();
    ListItem Li = new ListItem("--Select One--", "");

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            AjaxPro.Utility.RegisterTypeForAjax(typeof(Vendor_Discount_Rate_Master));
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(Page))
                ActionController.RedirctToLogin(Page);
            else
            {
                if (!IsPostBack)
                {
                    div_Details.Visible = false;
                    txtCreatedBy.Text = ((string)Session["User_ADID"]);
                    FillVendor();
                    ddlvendor.SelectedIndex = 0;
                    txt_discount_rate.Text = "";
                    showall();
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
    }



    private void FillVendor()
    {
        String IsData = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Discount_Rate_Master.aspx", "select", ref IsData, "", "", "VENDOR_NAME", "%");
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlvendor.DataSource = dt;
            ddlvendor.DataTextField = "Vendor_Name";
            ddlvendor.DataValueField = "Vendor_Code";
            ddlvendor.DataBind();
            ddlvendor.Items.Insert(0, Li);

            ddleditvendor.DataSource = dt;
            ddleditvendor.DataTextField = "Vendor_Name";
            ddleditvendor.DataValueField = "Vendor_Code";
            ddleditvendor.DataBind();
            ddleditvendor.Items.Insert(0, Li);

        }
    }

    protected void btn_New_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            txtCondition.Text = "0";
            txtParameterID.Text = "0";
            txtHeadIdID.Text = "0";
            txt_AMDDate.Text = DateTime.Now.Date.ToString();
            Hashtable validationHash = new Hashtable();
            string Result = string.Empty;
            string isInsert = string.Empty;

            try
            {
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag)
                {
                    string str = txtCreatedBy.Text;
                    Result = (string)ActionController.ExecuteAction("", "Vendor_Discount_Rate_Master.aspx", "insert", ref isInsert, txtHeadIdID.Text, txt_discount_rate.Text, ddlvendor.SelectedValue, str, 0);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                    }
                    else
                    {
                        ddlvendor.SelectedIndex = 0;
                        txt_discount_rate.Text = "";
                        showall();
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Saved.');}</script>");

                    }

                }
                else
                {
                }

            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);

            }
        }
    }
    private void showall()
    {
        try
        {
            DataTable DTS;
            string IsValid = string.Empty;
            txt_procedure.Text = "VENDOR_DISCOUNT";
         //   DTS = (DataTable)ActionController.ExecuteAction(Page, "select", ref IsValid);
            DTS = (DataTable)ActionController.ExecuteAction("", "Vendor_Discount_Rate_Master.aspx", "getdata", ref IsData);
            DTS.Columns[0].ColumnName = "Sr.No";
            Session["ResultData"] = DTS;

            if (DTS.Rows.Count > 0)
            {
                div_Details.Visible = true;
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    DTS.Rows[i]["Sr.No"] = i + 1;
                }

                StringBuilder str = new StringBuilder();

                str.Append("<table id='data-table1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th align='left'>#</th><th align='left'>Vendor Code</th><th align='left'>Vendor Name</th><th align='left'>Discount Rate </th> <th align='left'>Edit</th><th align='left'>Action</th> </tr></thead>");
                str.Append("<tbody>");

                for (int i = 0; i < DTS.Rows.Count; i++)
                {

                    str.Append(" <tr>");
                    str.Append("<td align='left'>" + (i + 1) + "</td>");                   
                    str.Append("<td align='left'>" + DTS.Rows[i]["FK_VENDOR_CODE"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["Vendor_Name"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["DISCOUNT_RATE"].ToString() + "</td>");
                    str.Append("<td align='left'>  <p> <i id='btnedit" + (i + 1) + "' class='fa fa-fw m-r-10 pull-left f-s-18 fa-edit' data-title='Edit' data-toggle='modal' data-target='#edit' onclick='editdatails(" + Convert.ToString(DTS.Rows[i]["PK_DISCOUNT_RATE"]) + ");' data-placement='top' rel='tooltip'></i> </p> <input id='txt_pkid" + (i + 1) + "'  runat='server' Value='" + DTS.Rows[i]["PK_DISCOUNT_RATE"].ToString() + "' style='display:none' /></td>");
                    str.Append("<td align='left'><p><i id='btndelte" + (i + 1) + "' class='fa fa-fw pull-left f-s-18 fa-trash' data-title='Delete' data-toggle='modal' data-target='#delete' onclick='deletedetails(" + Convert.ToString(DTS.Rows[i]["PK_DISCOUNT_RATE"]) + ");'  data-placement='top' rel='tooltip'></i>  </p></td>");
                    str.Append("</tr>");
                }
                str.Append("   </tbody>   </table> ");
                divalldata.InnerHtml = str.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable({'bSort': false,'pageLength': 100});", true);
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Data Found !!')}</script>");
                div_Details.Visible = false;
                divalldata.InnerHtml = null;
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

    }
    protected void btn_Edit_onClick(object sender, EventArgs e)
    {

        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ActionController.SetControlAttributes(Page);
            Hashtable validationHash = new Hashtable();
            string isUpdated = string.Empty;
            string isComplete = string.Empty;

            txtParameterID.Text = "1";
            try
            {
                string str = txtCreatedBy.Text;
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag)
                {
                    isUpdated = (string)ActionController.ExecuteAction("", "Vendor_Discount_Rate_Master.aspx", "insert", ref isComplete, txtHeadIdID.Text, txt_edit_drate.Text, ddleditvendor.SelectedValue, str, 1);
                    if (isUpdated == null)
                    {
                        if (isComplete.Length > 0)
                        {
                            string[] errmsg = isComplete.Split(':');
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "')}</script>");
                        }

                    }
                    else
                    {
                        showall();
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Updated !!')}</script>");
                        txtCondition.Text = "";
                        txtParameterID.Text = "";
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

    protected void btn_Delete_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Hashtable validationHash = new Hashtable();

            string isDeleted = string.Empty;
            string isValid = string.Empty;


            txtParameterID.Text = "2";
            try
            {
                string str = txtCreatedBy.Text;
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag)
                {
                    isDeleted = (string)ActionController.ExecuteAction("", "Vendor_Discount_Rate_Master.aspx", "insert", ref isValid, txtHeadIdID.Text, txt_edit_drate.Text, ddleditvendor.SelectedValue, str, 2);
                    if (isDeleted == null && isValid.Length > 0)
                    {
                        string[] errmsg = isValid.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "')}</script>");
                    }
                    else
                    {
                        showall();
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Deleted !!')}</script>");
                        txtCondition.Text = "";
                        txtParameterID.Text = "";
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=" + 3);
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }


    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string getVendorData(int pk_id)
    {
        string isUpdated = string.Empty;
        string isdata = string.Empty;
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Vendor_Discount_Rate_Master.aspx", "vendordata", ref isdata, pk_id);
            if (dt != null && dt.Rows.Count > 0)
            {
                isUpdated = Convert.ToString(dt.Rows[0]["DISCOUNT_RATE"]) + "@@" + Convert.ToString(dt.Rows[0]["FK_VENDOR_CODE"]);
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return isUpdated;
    }

}