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
using System.Drawing;
using System.Windows.Forms;
using System.Web.Services;


public partial class LocalConveyanceExpenseMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(LocalConveyanceExpenseMaster));
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            if (!IsPostBack)
            {
                ActionController.SetControlAttributes(Page);
                ddlVehicleType.SelectedValue = "";
                txt_Rate.Value = "";
                fromDate.Value = "";
                toDate.Value = "";
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

    private void showAll()
    {
        try
        {
            DataTable DTS;
            string IsValid = string.Empty;
            DTS = (DataTable)ActionController.ExecuteAction("", "LocalConveyanceExpenseMaster.aspx", "select", ref IsValid, "", "", "LOCAL_CONVEYANCE_EXPENSE", "%");
            DTS.Columns[0].ColumnName = "Sr.No";
            Session["ResultData"] = DTS;

            if (DTS.Rows.Count > 0)
            {
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    DTS.Rows[i]["Sr.No"] = i + 1;
                }

                StringBuilder str = new StringBuilder();
                str.Append("<table id='data-table1' class='table table-bordered table-hover'> <thead><tr class='grey'><th align='left'>Sr.No</th> <th align='left'>Vehicle Type</th> <th align='left'>Rate Per KM</th> <th align='left'>From Date</th> <th align='left'>To Date</th> <th align='left'>Edit</th><th align='left'>Action</th> </tr></thead>");
                str.Append("<tbody>");

                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td align='left'><input type='text' id='pk_id_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["PK_Id"].ToString()) + "' />" + (i + 1) + "</td>");
                    str.Append("<td align='left'><input type='text' id='vehicle_type_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["Vehicle_Type"].ToString()) + "' />" + DTS.Rows[i]["Vehicle_Type"].ToString() + "</td>");
                    str.Append("<td align='left'><input type='text' id='rateper_KM_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["Rate_Per_KM"].ToString()) + "' />" + DTS.Rows[i]["Rate_Per_KM"].ToString() + "</td>");
                    str.Append("<td align='left'><input type='text' id='from_date_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["From_Date"].ToString()) + "' />" + DTS.Rows[i]["From_Date"].ToString() + "</td>");
                    str.Append("<td align='left'><input type='text' id='to_date_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["To_Date"].ToString()) + "' />" + DTS.Rows[i]["To_Date"].ToString() + "</td>");
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
                    Result = (string)ActionController.ExecuteAction("", "LocalConveyanceExpenseMaster.aspx", "insert", ref isInsert, txtHeadIdID.Text, ddlVehicleType.SelectedValue, txt_Rate.Value, fromDate.Value, toDate.Value, txt_UserName.Text, 0);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                        showAll();
                    }
                    else
                    {
                        ddlVehicleType.SelectedValue = "";
                        txt_Rate.Value = "";
                        fromDate.Value = "";
                        toDate.Value = "";
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
                    Result = (string)ActionController.ExecuteAction("", "LocalConveyanceExpenseMaster.aspx", "insert", ref isInsert, txt_PkId.Text, ddlVehicleType.SelectedValue, txt_Rate.Value, fromDate.Value, toDate.Value, txt_UserName.Text, 1);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                        showAll();
                    }
                    else
                    {
                        ddlVehicleType.SelectedValue = "";
                        txt_Rate.Value = "";
                        fromDate.Value = "";
                        toDate.Value = "";
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

    [WebMethod]
    public static string getValidDates(string start, string end, string ddl_text)
    {
        string str = string.Empty;
        string isInserted = string.Empty;
        string DatesCheck = (string)ActionController.ExecuteAction("", "LocalConveyanceExpenseMaster.aspx", "checkalreadyleaveapply", ref isInserted, ddl_text, start, end);
        if (DatesCheck == "true")
        {
            str = "true";
        }
        return DatesCheck;
    }

    protected void btnHomeOnClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ddlVehicleType.SelectedValue = "";
            txt_Rate.Value = "";
            fromDate.Value = "";
            toDate.Value = "";
            Response.Redirect("../../Master.aspx?M_ID=3");
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string updateLocalConveyanceData(int pk_id, string username)
    {
        string isUpdated = string.Empty;
        string isdata = string.Empty;
        try
        {
            isUpdated = (string)ActionController.ExecuteAction("", "LocalConveyanceExpenseMaster.aspx", "insert", ref isdata, pk_id, "", 0, "", "", "", 2);
            if (isUpdated == "true")
            {
                DataTable DTS;
                string IsValid = string.Empty;
                DTS = (DataTable)ActionController.ExecuteAction("", "LocalConveyanceExpenseMaster.aspx", "select", ref IsValid, "", "", "LOCAL_CONVEYANCE_EXPENSE", "");
                if (DTS.Rows.Count > 0)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("<table id='tbl_Update' class='table table-bordered table-hover'> <thead><tr class='grey'><th align='left'>Sr.No</th> <th align='left'>Vehicle Type</th> <th align='left'>Rate Per KM</th> <th align='left'>From Date</th> <th align='left'>To Date</th> <th align='left'>Edit</th><th align='left'>Action</th> </tr></thead>");
                    str.Append("<tbody>");

                    for (int i = 0; i < DTS.Rows.Count; i++)
                    {
                        str.Append(" <tr>");
                        str.Append("<td align='left'><input type='text' id='pk_id_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["PK_Id"].ToString()) + "' />" + (i + 1) + "</td>");
                        str.Append("<td align='left'><input type='text' id='vehicle_type_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["Vehicle_Type"].ToString()) + "' />" + DTS.Rows[i]["Vehicle_Type"].ToString() + "</td>");
                        str.Append("<td align='left'><input type='text' id='rateper_KM_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["Rate_Per_KM"].ToString()) + "' />" + DTS.Rows[i]["Rate_Per_KM"].ToString() + "</td>");
                        str.Append("<td align='left'><input type='text' id='from_date_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["From_Date"].ToString()) + "' />" + DTS.Rows[i]["From_Date"].ToString() + "</td>");
                        str.Append("<td align='left'><input type='text' id='to_date_" + (i + 1) + "' style='display:none' value='" + (DTS.Rows[i]["To_Date"].ToString()) + "' />" + DTS.Rows[i]["To_Date"].ToString() + "</td>");
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