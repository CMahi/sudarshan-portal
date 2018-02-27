using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using FSL.Controller;

using AjaxPro;
using System.IO;
using System.Data.Common;
using System.Configuration;
using System.Text;
public partial class ParameterMaster : System.Web.UI.Page
{
    DataTable Itab = new DataTable();
    string IsData = string.Empty;
    ListItem li = new ListItem("--Select One--", "");
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ParameterMaster));
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            if (!IsPostBack)
            {

                txt_ActionObjectID.Text = Request.Params.Get("objID");
                txt_UserName.Text = ((string)Session["User_ADID"]);
                txt_DomainName.Text = ((string)Session["DomainName"]);
                Initialization();
                SetAccessibilty();
                FillPanels();
               
            }
        }
        DisplayMode(0);

    }

    private void GetUserPortalsettings()
    {
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "ParameterMaster.aspx", "userportalsettings", ref IsData, (string)Session["User_ADID"]);
            if (dt.Rows.Count > 0 && dt != null)
            {
                Grd_Viewall.DataSource = dt;
                Grd_Viewall.DataBind();
            }
            else
            {
                Grd_Viewall.DataSource = null;
                Grd_Viewall.DataBind();
            }

        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
    }

    private void FillPanels()
    {
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "ParameterMaster.aspx", "select panels", ref IsData);
            if (dt.Rows.Count > 0 && dt != null)
            {
                ddl_Add_Panel.DataSource = dt;
                ddl_Add_Panel.DataTextField = "PANE_DESC";
                ddl_Add_Panel.DataValueField = "PANE_ID";
                ddl_Add_Panel.DataBind();
                ddl_Add_Panel.Items.Insert(0, li);
            }
        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
    }


    protected void getPanelObjects(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "ParameterMaster.aspx", "select menu tree", ref IsData, (string)Session["User_ADID"], ddl_Add_Panel.SelectedItem.Text);
                if (dt.Rows.Count > 0 && dt != null)
                {
                    ddl_Object.DataSource = dt;
                    ddl_Object.DataTextField = "OBJ_NAME";
                    ddl_Object.DataValueField = "OBJ_ID";
                    ddl_Object.DataBind();
                    ddl_Object.Items.Insert(0, li);
                    ddl_Object.Items.Insert(1, new ListItem("NONE", "0"));
                }
            }
            catch (Exception Ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, Ex);
            }
        }

    }

    private void SetAccessibilty()
    {
        try
        {
            string UserAccess = ActionController.ObjectAccesses(txtCreatedBy.Text, txt_ActionObjectID.Text);
            if (string.IsNullOrEmpty(UserAccess))
                Response.Write("<script>window.open('/MQueue/ErrorPĂges/Error.htm','frmset_WorkArea');</script>");
            else
            {
                if (UserAccess.IndexOf("1") == -1) { tr_Message.Visible = true; tr_Buttons.Visible = false; }
                else
                {

                    if ((UserAccess.Length >= 1) && (UserAccess.Substring(0, 1).Equals("0"))) { }
                    if ((UserAccess.Length >= 2) && (UserAccess.Substring(1, 1).Equals("0"))) { btn_AddNew_Go.Visible = false; }
                }
            }
        }
        catch (Exception) { }
    }
    private void Initialization()
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            txtCreatedBy.Text = ((string)Session["User_ADID"]).Split('@')[0].ToString();
            tbl_showMyAddPanel.Visible = true;
            tbl_showMyEditPanel.Visible = true;
            tbl_showMyEditPanel.Style["display"] = "none";
            tbl_showMyAddPanel.Style["display"] = "none";
            ActionController.SetControlAttributes(Page);
        }
    }
    protected void btn_Back_Edit_onclick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {

            DivDisplayError.Style["display"] = "none";
            tbl_showMyButtonPanel.Style["display"] = "block";
            tbl_showMyEditPanel.Style["display"] = "none";
            ActionController.SetControlAttributes(Page);
            DisplayMode(0);
        }
    }
    protected void btn_Add_onClickServer(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            tbl_showMyAddPanel.Visible = true;
            tbl_showMyEditPanel.Visible = false;
            tbl_showMyAddPanel.Style["display"] = "block";
            tbl_showMyButtonPanel.Style["display"] = "none";
            GetUserPortalsettings();
            DisplayMode(1);
        }
    }

    protected void btn_New_onClick(object sender, EventArgs e)
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
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag)
                {
                    Result = (string)ActionController.ExecuteAction("", "ParameterMaster.aspx", "save", ref isInsert,
                        ddl_Object.SelectedValue,
                        (string)Session["User_ADID"],
                        ddl_Add_Panel.SelectedValue
                    );
                    if (Result == null && isInsert.Length > 0)
                    {
                        tbl_showMyAddPanel.Visible = true;
                        tbl_showMyEditPanel.Visible = false;
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                    }
                    else
                    {

                        tbl_showMyAddPanel.Visible = true;
                        tbl_showMyEditPanel.Visible = true;
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Saved.');}</script>");
                        tbl_showMyAddPanel.Style["display"] = "none";
                        tbl_showMyEditPanel.Style["display"] = "none";
                        tbl_showMyButtonPanel.Style["display"] = "block";
                    }

                }
                else
                {
                    DivDisplayError.Style["Display"] = "block";
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }


    }
    private string ConvertToDatetimeFormat(string p)
    {
        string dateformat = string.Empty;
        if (p != "")
        {
            string[] Date = p.Split("-".ToCharArray());

            try
            {
                dateformat = Date[1] + " " + Date[0] + " " + Date[2];
            }
            catch (Exception Ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, Ex);
            }
        }
        else
            dateformat = "";
        return dateformat;
    }
    private void setGridAllign(DataTable dts)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            int k = 0;
            try
            {
                foreach (DataColumn col in dts.Columns)
                {
                    if (col.DataType.ToString() == "System.Decimal")
                    {
                        for (int j = 0; j < dgViewAll.Rows.Count; j++)
                        {

                            dgViewAll.Rows[j].Cells[k].HorizontalAlign = HorizontalAlign.Right;
                        }
                    }
                    else if (col.DataType.ToString() == "System.String")
                    {
                        for (int j = 0; j < dgViewAll.Rows.Count; j++)
                        {
                            dgViewAll.Rows[j].Cells[k].HorizontalAlign = HorizontalAlign.Left;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < dgViewAll.Rows.Count; j++)
                        {
                            dgViewAll.Rows[j].Cells[k].HorizontalAlign = HorizontalAlign.Center;
                        }
                    }
                    k++;
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }
    private void DisplayMode(int ModeID)
    {
        switch (ModeID)
        {
            case 1:
                lblMode.Text = "Add";
                break;
            case 2:
                lblMode.Text = "Edit";
                break;
            case 3:
                lblMode.Text = "View All";
                break;
            case 0:
                lblMode.Text = "";
                break;
        }
    }
    protected void btn_Add_onClicks(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            txtCondition.Text = "0";
            txtParameterID.Text = "0";
        }
    }
    protected void btn_Search_onserverClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            tbl_showMyAddPanel.Visible = false;
            tbl_showMyEditPanel.Visible = true;
            tbl_showMyEditPanel.Style["display"] = "block";
            tbl_showMyAddPanel.Style["display"] = "none";
            ActionController.SetControlAttributes(Page);
        }

    }
    protected void btn_ViewAll_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            tbl_showMyViewAllPanel.Style["display"] = "block";
            tbl_showMyButtonPanel.Style["display"] = "none";
            DivDisplayError.Style["display"] = "none";
            DataTable DTS;
            string IsValid = string.Empty;
            try
            {
                txt_ProcName.Text = "PREMISE";
                txt_SearchString.Text = "%";
                DTS = (DataTable)ActionController.ExecuteAction("", "Permises_Master.aspx", "select", ref IsValid, "", "", "PREMISE", "%");
                DTS.Columns[0].ColumnName = "Sr.No";
                Session["ResultData"] = DTS;
                if (DTS.Rows.Count > 0)
                {

                    for (int i = 0; i < DTS.Rows.Count; i++)
                    {
                        DTS.Rows[i]["Sr.No"] = i + 1;
                    }

                    dgViewAll.DataSource = DTS;
                    dgViewAll.DataBind();
                    dgViewAll.Visible = true;
                    setGridAllign(DTS);
                    DisplayMode(3);


                    tbl_showMyViewAllPanel.Style["display"] = "block";
                    tbl_showMyButtonPanel.Style["display"] = "none";
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('No Data Found !!')}</script>");
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }

    }
    protected void btn_Reset_Edit_onclick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            tbl_showMyButtonPanel.Style["display"] = "block";
            tbl_showMyAddPanel.Style["display"] = "none";
        }
    }
    protected void btn_Home_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            tbl_showMyAddPanel.Visible = true;
            tbl_showMyEditPanel.Visible = true;
            tbl_showMyButtonPanel.Style["display"] = "block";
            tbl_showMyAddPanel.Style["display"] = "none";
            DivDisplayError.Style["Display"] = "none";
            DisplayMode(0);
        }
    }
    protected void btn_AllEmpInfo_Back_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            tbl_showMyAddPanel.Visible = true;
            tbl_showMyEditPanel.Visible = true;
            tbl_showMyButtonPanel.Style["display"] = "block";
            tbl_showMyViewAllPanel.Style["display"] = "none";
            DisplayMode(0);
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
            txtCondition.Text = "1";
            txtParameterID.Text = "1";

            try
            {
                bool flag = ActionController.ValidateForm(Page, "update", validationHash);
                if (flag)
                {
                    isUpdated = (string)ActionController.ExecuteAction("", "Permises_Master.aspx", "update", ref isComplete,
                        txt_PremiseHdrId.Text,
                        txt_branchId.Text,

                        (string)Session["User_ADID"],
                        "1"
                        );

                    if (isUpdated == null)
                    {
                        if (isComplete.Length > 0)
                        {
                            string[] errmsg = isComplete.Split(':');
                            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "')}</script>");
                        }
                        tbl_showMyButtonPanel.Style["display"] = "none";
                        tbl_showMyEditPanel.Style["display"] = "block";
                    }
                    else
                    {

                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Updated !!')}</script>");
                        txtCondition.Text = "";
                        txtParameterID.Text = "";
                        tbl_showMyButtonPanel.Style["display"] = "block";
                        tbl_showMyEditPanel.Style["display"] = "none";
                    }
                }
                else
                {
                    DivDisplayError.Style["Display"] = "block";
                    tbl_showMyButtonPanel.Style["display"] = "none";
                    tbl_showMyEditPanel.Style["display"] = "block";
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

            txtCondition.Text = "2";
            txtParameterID.Text = "2";
            try
            {
                bool flag = ActionController.ValidateForm(Page, "update", validationHash);
                if (flag)
                {
                    isDeleted = (string)ActionController.ExecuteAction("", "Permises_Master.aspx", "update", ref isValid,
                        txt_PremiseHdrId.Text,
                        txt_branchId.Text,

                        (string)Session["User_ADID"],
                        "2"
                        );

                    if (isDeleted == null && isValid.Length > 0)
                    {
                        string[] errmsg = isValid.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "')}</script>");
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Deleted !!')}</script>");
                        txtCondition.Text = "";
                        txtParameterID.Text = "";
                    }
                }
                else
                {
                    DivDisplayError.Style["Display"] = "block";
                    tbl_showMyButtonPanel.Style["display"] = "none";
                    tbl_showMyEditPanel.Style["display"] = "block";
                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

     protected void deleteRecord(object sender,EventArgs e)
    {
        Hashtable validationHash = new Hashtable();
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                bool flag = ActionController.ValidateForm(Page, "update", validationHash);
                if (flag)
                {


                    string strPrimaryKey = Grd_Viewall.DataKeys[0].Value.ToString();
                    string isDeleted = string.Empty;
                    string isValid = string.Empty;

                    isDeleted = (string)ActionController.ExecuteAction("", "ParameterMaster.aspx", "del", ref isValid, strPrimaryKey);

                    if (isDeleted == null && isValid.Length > 0)
                    { 
                        string[] errmsg = isValid.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "')}</script>");
                    }
                    else
                    {
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Deleted !!')}</script>");
                        txtCondition.Text = "";
                        txtParameterID.Text = "";
                    }

                    GetUserPortalsettings();
                }
                if (Grd_Viewall.Rows.Count == 0)
                {

                }


            }
            catch (Exception Ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, Ex);
            }
        }
    }

    protected void dgView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                dgViewAll.PageIndex = e.NewPageIndex;
                dgViewAll.DataSource = (DataTable)Session["ResultData"];
                dgViewAll.DataBind();
                DisplayMode(3);
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }
    protected void btn_Export_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            ExportGridView(dgViewAll, "Supplier-Master");
        }
    }
    protected void ExportGridView(GridView grdView, string filename)
    {
        try
        {
            string IsValid = string.Empty;
            string attachment = "attachment; filename=" + filename + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdView.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Error While Exporting in Excel......!')}</script>");
            return;
        }
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string CalculateTodate(string date1, string period)
    {
        string Datetime1 = string.Empty;
        return (Convert.ToDateTime(date1).AddMonths(int.Parse(period))).AddDays(-1).ToString("dd-MMM-yyyy");
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string GetBranchCode(string branchid)
    {
        string Code = string.Empty;
        DataTable dt = (DataTable)Session["BR"];

        DataRow[] dr = dt.Select("PK_BRANCHID=" + branchid + "");
        if (dr.Length > 0)
        {
            Code = dr[0].ItemArray.GetValue(4).ToString();
        }
        return Code;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string CheckCode(string code)
    {
        string istrue = string.Empty;
        string isExists = (string)ActionController.ExecuteAction("", "Branch_PremiseMaster.aspx", "checkcodepl", ref istrue, code);
        return isExists;
    }


}


