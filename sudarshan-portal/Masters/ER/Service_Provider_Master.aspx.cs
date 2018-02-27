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

public partial class Service_Provider_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Service_Provider_Master));
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            if (!IsPostBack)
            {
                ActionController.SetControlAttributes(Page);
                txt_provider_name.Value = "";
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
            DTS = (DataTable)ActionController.ExecuteAction("", "Service_Provider_Master.aspx", "select", ref IsValid, "", "", "SERVICE_PROVIDER", "%");
            DTS.Columns[0].ColumnName = "Sr.No";
            Session["ResultData"] = DTS;

            if (DTS.Rows.Count > 0)
            {
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    DTS.Rows[i]["Sr.No"] = i + 1;
                }

                StringBuilder str = new StringBuilder();
                str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th align='left'>Sr.No</th> <th align='left'>Service Provider Name</th> <th align='left'>Edit</th><th align='left'>Action</th> </tr></thead>");
                str.Append("<tbody>");

                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td align='left'><input type='text' id='pk_id_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["PK_Provider_ID"].ToString()) + "' />" + (i + 1) + "</td>");
                    str.Append("<td align='left'><input type='text' id='provider_name_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["Provider_Name"].ToString()) + "' />" + DTS.Rows[i]["Provider_Name"].ToString() + "</td>");
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
            String provider_name = txt_provider_name.Value;

            try
            {
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag)
                {
                    Result = (string)ActionController.ExecuteAction("", "Service_Provider_Master.aspx", "insert", ref isInsert, txtHeadIdID.Text, txt_provider_name.Value, txt_UserName.Text, 0);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                        showAll();
                    }
                    else
                    {
                        txt_provider_name.Value = "";
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
            txt_provider_name.Value = "";
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
                    Result = (string)ActionController.ExecuteAction("", "Service_Provider_Master.aspx", "insert", ref isInsert, txt_PkId.Text, txt_provider_name.Value, txt_UserName.Text, 1);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                        showAll();
                    }
                    else
                    {
                        txt_provider_name.Value = "";
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
    public string updateServiceProviderData(int pk_id, string username)
    {
        string isUpdated = string.Empty;
        string isdata = string.Empty;
        try
        {
            isUpdated = (string)ActionController.ExecuteAction("", "Service_Provider_Master.aspx", "insert", ref isdata, pk_id, "", username, 2);
            if (isUpdated == "true")
            {
                DataTable DTS;
                string IsValid = string.Empty;
                DTS = (DataTable)ActionController.ExecuteAction("", "Service_Provider_Master.aspx", "select", ref IsValid, "", "", "SERVICE_PROVIDER", "");
                if (DTS.Rows.Count > 0)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("<table id='tbl_update' class='table table-bordered table-hover'> <thead><tr class='grey'><th align='left'>Sr.No</th> <th align='left'>Service Provider Name</th> <th align='left'>Edit</th><th align='left'>Action</th> </tr></thead>");
                    str.Append("<tbody>");
                    for (int i = 0; i < DTS.Rows.Count; i++)
                    {
                        str.Append(" <tr>");
                        str.Append("<td align='left'><input type='text' id='pk_id_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["PK_Provider_ID"].ToString()) + "' />" + (i + 1) + "</td>");
                        str.Append("<td align='left'><input type='text' id='provider_name_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["Provider_Name"].ToString()) + "' />" + DTS.Rows[i]["Provider_Name"].ToString() + "</td>");
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