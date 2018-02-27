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


public partial class Expense_Head_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Expense_Head_Master));
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            if (!IsPostBack)
            {
                ActionController.SetControlAttributes(Page);
                txt_exp_head.Value = "";
                txt_exp_desc.Value = "";
                txt_glcode.Value = "";
                txt_ActionObjectID.Text = Request.Params.Get("objID");
                txt_UserName.Text = ((string)Session["User_ADID"]);
                txt_DomainName.Text = ((string)Session["DomainName"]);
                showAll();
            }
        }
    }

    private void showAll()
    {
        try
        {
            DataTable DTS;
            string IsValid = string.Empty;
            DTS = (DataTable)ActionController.ExecuteAction("", "Expense_Head_Master.aspx", "select", ref IsValid, "", "", "M_Expense", "%");
            DTS.Columns[0].ColumnName = "Sr.No";
            Session["ResultData"] = DTS;

            if (DTS.Rows.Count > 0)
            {
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    DTS.Rows[i]["Sr.No"] = i + 1;
                }

                StringBuilder str = new StringBuilder();
                str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th align='left'>Sr.No</th> <th align='left'>Expense Head</th> <th align='left'>Description</th> <th align='left'>GL Code</th> <th align='left'>Edit</th><th align='left'>Action</th> </tr></thead>");
                str.Append("<tbody>");

                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td align='left'><input type='text' id='pk_id_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["PK_EXPENSE_HEAD_ID"].ToString()) + "' />" + (i + 1) + "</td>");
                    str.Append("<td align='left'><input type='text' id='expHead_name_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["EXPENSE_HEAD"].ToString()) + "' />" + DTS.Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
                    str.Append("<td align='left'><input type='text' id='exp_desc_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["EXPENSE_DESCRIPTION"].ToString()) + "' />" + DTS.Rows[i]["EXPENSE_DESCRIPTION"].ToString() + "</td>");
                    str.Append("<td align='left'><input type='text' id='glCode_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["SAP_GLCode"].ToString()) + "' />" + DTS.Rows[i]["SAP_GLCode"].ToString() + "</td>");
                    str.Append("<td align='left' ><a  href='#CountryPanel' role='button' data-toggle='modal' Title='Update' onclick='getData(" + (i + 1) + ")'><span class='btn-cyan glyphicon glyphicon-pencil'></a></span></td>");
                    str.Append("<td align='left' ><a  href='#DeletePanel' role='button' data-toggle='modal' Title='Delete' onclick='deleteData(" + (i + 1) + ")'><span class='btn-red glyphicon glyphicon-trash'></a></span></td>");
                    str.Append("</tr>");
                }
                str.Append("   </tbody>   </table> ");
                dv_dtl.InnerHtml = str.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "", "$('#data-table1').dataTable();", true);
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

    protected void btnSaveOnClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            txtHeadIdID.Text = "0";
            Hashtable validationHash = new Hashtable();
            string Result = string.Empty;
            string isInsert = string.Empty;           

            try
            {
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag)
                {
                    Result = (string)ActionController.ExecuteAction("", "Expense_Head_Master.aspx", "insert", ref isInsert, txtHeadIdID.Text, txt_exp_head.Value, txt_exp_desc.Value, txt_glcode.Value, txt_UserName.Text, 0);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                        showAll();
                    }
                    else
                    {
                        txt_exp_head.Value = "";
                        txt_exp_desc.Value = "";
                        txt_glcode.Value = "";
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

    protected void btnHomeOnClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            txt_exp_head.Value = "";
            txt_exp_desc.Value = "";
            txt_glcode.Value = "";
            Response.Redirect("../../Master.aspx?M_ID=3");
        }
    }

    protected void btnUpdateOnClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Hashtable validationHash = new Hashtable();
            string Result = string.Empty;
            string isInsert = string.Empty;

            try
            {
                bool flag = ActionController.ValidateForm(Page, "Update", validationHash);
                if (flag)
                {
                    Result = (string)ActionController.ExecuteAction("", "Expense_Head_Master.aspx", "insert", ref isInsert, txt_PkId.Text, txt_exp_head.Value, txt_exp_desc.Value, txt_glcode.Value, txt_UserName.Text, 1);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                        showAll();
                    }
                    else
                    {
                        txt_exp_head.Value = "";
                        txt_exp_desc.Value = "";
                        txt_glcode.Value = "";
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

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string updateExpenseData(int pk_id, string username)
    {
        string isUpdated = string.Empty;
        string isdata = string.Empty;
        try
        {
            isUpdated = (string)ActionController.ExecuteAction("", "Expense_Head_Master.aspx", "insert", ref isdata, pk_id, "", "", "", username, 2);
            if (isUpdated == "true")
            {
                DataTable DTS;
                string IsValid = string.Empty;
                DTS = (DataTable)ActionController.ExecuteAction("", "Expense_Head_Master.aspx", "select", ref IsValid, "", "", "M_Expense", "");
                if (DTS.Rows.Count > 0)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("<table id='tbl_update' class='table table-bordered table-hover'> <thead><tr class='grey'><th align='left'>Sr.No</th> <th align='left'>Expense Head</th> <th align='left'>Description</th> <th align='left'>GL Code</th> <th align='left'>Edit</th><th align='left'>Action</th> </tr></thead>");
                    str.Append("<tbody>");
                    for (int i = 0; i < DTS.Rows.Count; i++)
                    {
                        str.Append(" <tr>");
                        str.Append("<td align='left'><input type='text' id='pk_id_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["PK_EXPENSE_HEAD_ID"].ToString()) + "' />" + (i + 1) + "</td>");
                        str.Append("<td align='left'><input type='text' id='expHead_name_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["EXPENSE_HEAD"].ToString()) + "' />" + DTS.Rows[i]["EXPENSE_HEAD"].ToString() + "</td>");
                        str.Append("<td align='left'><input type='text' id='exp_desc_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["EXPENSE_DESCRIPTION"].ToString()) + "' />" + DTS.Rows[i]["EXPENSE_DESCRIPTION"].ToString() + "</td>");
                        str.Append("<td align='left'><input type='text' id='glCode_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["SAP_GLCode"].ToString()) + "' />" + DTS.Rows[i]["SAP_GLCode"].ToString() + "</td>");
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