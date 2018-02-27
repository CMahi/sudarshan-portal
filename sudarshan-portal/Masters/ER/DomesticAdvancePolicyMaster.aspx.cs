using AjaxPro;
using FSL.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSL.Logging;
using FSL.Message;

public partial class DomesticAdvancePolicyMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(DomesticAdvancePolicyMaster));
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            if (!IsPostBack)
            {
                ActionController.SetControlAttributes(Page);
                fillDesignation();
                ddlDesignation.SelectedIndex = 0;
                ddlCityClass.SelectedIndex = 0;
                txt_Amount.Value = "0";
                txt_ActionObjectID.Text = Request.Params.Get("objID");
                txt_UserName.Text = ((string)Session["User_ADID"]);
                txt_DomainName.Text = ((string)Session["DomainName"]);
                showAll();
            }
        }
    }

    private void Initialization()
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            txtCreatedBy.Text = ((string)Session["User_ADID"]);
            ActionController.SetControlAttributes(Page);
        }
    }

    private void fillDesignation()
    {
        String IsData = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "DomesticAdvancePolicyMaster.aspx", "fill", ref IsData, 0);
        ddlDesignation.Items.Clear();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataTextField = "DESG_NAME";
            ddlDesignation.DataValueField = "PK_DESG_ID";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, "--Select One--");
        }
    }

    private void showAll()
    {
        try
        {
            DataTable DTS;
            string IsValid = string.Empty;
            txt_procedure.Text = "Domestic_Adv_policy";
            DTS = (DataTable)ActionController.ExecuteAction("", "DomesticAdvancePolicyMaster.aspx", "select", ref IsValid, "", "", txt_procedure.Text, "");
            Session["ResultData"] = DTS;

            if (DTS.Rows.Count > 0)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<table id='data-showall'  runat='server' class='table table-bordered table-hover'> <thead><tr class='grey'><th style='width:5%'>#</th><th>Designation</th><th>City Class</th><th>Amount</th><th align='left'>Action</th><th>Delete</th></tr></thead>");
                str.Append("<tbody>");

                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td align='left'><input type='text' Id='pk_dom_id_" + (i + 1) + "' value='" + DTS.Rows[i]["PK_DOMESTIC_ADV_ID"].ToString() + "' style='display:none'/> " + (i + 1) + "</td>");
                    str.Append("<td align='left'><input type='text' id='designation_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["FK_DESG_ID"].ToString()) + "' />" + DTS.Rows[i]["Designation_name"].ToString() + "</td>");
                    str.Append("<td align='left'><input type='text' id='cityClass_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["CITY_CLASS"].ToString()) + "' />" + DTS.Rows[i]["CITY_CLASS"].ToString() + "</td>");
                    str.Append("<td align='left'><input type='text' id='amount_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["AMOUNT"].ToString()) + "' />" + DTS.Rows[i]["AMOUNT"].ToString() + "</td>");
                    str.Append("<td align='left' ><a  href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='getData(" + (i + 1) + ")'><span class='btn-cyan glyphicon glyphicon-pencil' id='spn" + (i + 1) + " 'onclick='disablediv(" + (i + 1) + ")' ></span></a></td>");
                    str.Append("<td align='left' ><a  href='#DeletePanel' role='button' data-toggle='modal' Title='Delete' onclick='deleteData(" + (i + 1) + ")'><span class='btn-red glyphicon glyphicon-trash'></a></span></td>");
                    str.Append("</tr>");
                }
                str.Append("   </tbody>   </table> ");
                dv_dtl.InnerHtml = str.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-showall').dataTable();", true);
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Data Found !!')}</script>");
                dv_dtl.InnerHtml = null;
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected void btnHomeOnClick(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../../Master.aspx?M_ID=3");
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
    }

    protected void btnUpdateOnClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            txtCondition.Text = "0";
            txtParameterID.Text = "0";
            txtHeadIdID.Text = "0";
            Hashtable validationHash = new Hashtable();
            string Result = string.Empty;
            string isInsert = string.Empty;

            try
            {
                bool flag = ActionController.ValidateForm(Page, "Update", validationHash);
                if (flag)
                {
                    Result = (string)ActionController.ExecuteAction("", "DomesticAdvancePolicyMaster.aspx", "insert", ref isInsert, txt_PkId.Text, 0, " ", txt_Amount.Value, txt_UserName.Text, 1);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                        showAll();
                    }
                    else
                    {
                        ddlDesignation.SelectedIndex = 0;
                        ddlCityClass.SelectedIndex = 0;
                        txt_Amount.Value = "0";
                        showAll();
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Update.');}</script>");
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

    protected void btnSaveOnClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            var desg = ddlDesignation.SelectedValue;
            var cityClass = ddlCityClass.SelectedValue;
            Hashtable validationHash = new Hashtable();
            string Result = string.Empty;
            string isInsert = string.Empty;

            try
            {
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag)
                {
                    Result = (string)ActionController.ExecuteAction("", "DomesticAdvancePolicyMaster.aspx", "insert", ref isInsert, 0, desg, cityClass, txt_Amount.Value, txt_UserName.Text, 0);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                        showAll();
                    }
                    else
                    {
                        ddlDesignation.SelectedIndex = 0;
                        ddlCityClass.SelectedIndex = 0;
                        txt_Amount.Value = "0";
                        showAll();
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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string updateDomesticAdvData(int pk_id, string username)
    {
        string isUpdated = string.Empty;
        string isdata = string.Empty;
        try
        {
            isUpdated = (string)ActionController.ExecuteAction("", "DomesticAdvancePolicyMaster.aspx", "insert", ref isdata, pk_id, 0, "", 0, "", 2);
            if (isUpdated == "true")
            {
                DataTable DTS;
                string IsValid = string.Empty;
                DTS = (DataTable)ActionController.ExecuteAction("", "DomesticAdvancePolicyMaster.aspx", "select", ref IsValid, "", "", "Domestic_Adv_policy", "");
                if (DTS.Rows.Count > 0)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("<table id='tbl_Update'  runat='server' class='table table-bordered table-hover'> <thead><tr class='grey'><th style='width:5%'>#</th><th>Designation</th><th>City Class</th><th>Amount</th><th align='left'>Action</th><th>Delete</th></tr></thead>");
                    str.Append("<tbody>");

                    for (int i = 0; i < DTS.Rows.Count; i++)
                    {
                        str.Append(" <tr>");
                        str.Append("<td align='left'><input type='text' Id='pk_dom_id_" + (i + 1) + "' value='" + DTS.Rows[i]["PK_DOMESTIC_ADV_ID"].ToString() + "' style='display:none'/> " + (i + 1) + "</td>");
                        str.Append("<td align='left'><input type='text' id='designation_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["FK_DESG_ID"].ToString()) + "' />" + DTS.Rows[i]["Designation_name"].ToString() + "</td>");
                        str.Append("<td align='left'><input type='text' id='cityClass_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["CITY_CLASS"].ToString()) + "' />" + DTS.Rows[i]["CITY_CLASS"].ToString() + "</td>");
                        str.Append("<td align='left'><input type='text' id='amount_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["AMOUNT"].ToString()) + "' />" + DTS.Rows[i]["AMOUNT"].ToString() + "</td>");
                        str.Append("<td align='left' ><a  href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='getData(" + (i + 1) + ")'><span class='btn-cyan glyphicon glyphicon-pencil'></a></span></td>");
                        str.Append("<td align='left' ><a  href='#DeletePanel' role='button' data-toggle='modal' Title='Delete' onclick='deleteData(" + (i + 1) + ")'><span class='btn-red glyphicon glyphicon-trash'></a></span></td>");
                        str.Append("</tr>");
                    }
                    str.Append("   </tbody>   </table> ");
                    isUpdated = str.ToString();
                }
                else
                {
                    isUpdated = "false";
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return isUpdated;
    }
}