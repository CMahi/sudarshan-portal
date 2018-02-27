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
public partial class HD_TaskType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(HD_TaskType));
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            if (!IsPostBack)
            {
                ActionController.SetControlAttributes(Page);

                txt_ActionObjectID.Text = Request.Params.Get("objID");
                Initialization();
                SetAccessibilty();
                getProcessList();
                txt_UserName.Text = ((string)Session["User_ADID"]);
                txt_DomainName.Text = ((string)Session["DomainName"]);
            }
        }
        //DisplayMode(0);

    }

    private void getProcessList()
    {
        try
        {
            string IsData = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "ReleaseNotes.aspx", "getprocesslist", ref IsData);
            if (dt.Rows.Count > 0)
            {
                ddl_ModuleName.DataSource = dt;
                ddl_ModuleName.DataTextField = dt.Columns[1].ColumnName;
                ddl_ModuleName.DataValueField = dt.Columns[0].ColumnName;
                ddl_ModuleName.DataBind();
                ListItem li = new ListItem("--SELECT ONE--","");
                ddl_ModuleName.Items.Insert(0,li);
            }
        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
    }



    private void SetAccessibilty()
    {
        try
        {
            string UserAccess = ActionController.ObjectAccesses(txtCreatedBy.Text, txt_ActionObjectID.Text);
            if (string.IsNullOrEmpty(UserAccess))
                Response.Write("<script>window.open('/Sudarshan-Portal-NEW/ErrorPages/Error.htm','frmset_WorkArea');</script>");
            else
            {
                if (UserAccess.IndexOf("1") == -1) { tr_Message.Visible = true; tr_Buttons.Visible = false; }
                else
                {

                    if ((UserAccess.Length >= 1) && (UserAccess.Substring(0, 1).Equals("0"))) { btn_ViewAll_Go.Visible = false; }
                    if ((UserAccess.Length >= 2) && (UserAccess.Substring(1, 1).Equals("0"))) { btn_AddNew_Go.Visible = false; }
                    if ((UserAccess.Length >= 3) && (UserAccess.Substring(2, 1).Equals("0"))) { }
                    if ((UserAccess.Length >= 4) && (UserAccess.Substring(3, 1).Equals("0"))) { }
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
            txtCreatedBy.Text = ((string)Session["User_ADID"]);
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
            ActionController.SetControlAttributes(Page);
            //DisplayMode(0);
        }
    }
    protected void btn_Add_onClickServer(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            tbl_showMyAddPanel.Style["display"] = "block";
            tbl_showMyButtonPanel.Style["display"] = "none";
            txt_Add_FromDate.Text = "";
            txt_UserID.Text = "";
           // DisplayMode(1);
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

    protected void btn_New_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {

            txtCondition.Text = "0";
            txtParameterID.Text = "0";
            txtCatID.Text = "0";
            txt_AMDDate.Text = DateTime.Now.Date.ToString();
            Hashtable validationHash = new Hashtable();
            string Result = string.Empty;
            string isInsert = string.Empty;
            try
            {
                bool flag = ActionController.ValidateForm(Page, "amd", validationHash);
                if (flag)
                {
                    string str = txtCreatedBy.Text;
                    Result = (string)ActionController.ExecuteAction("", "ReleaseNotes.aspx", "amd", ref isInsert,ddl_ModuleName.SelectedItem.Text,txt_Add_FromDate.Text,txt_XML.Text);
                    if (Result == null && isInsert.Length > 0)
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                    }
                    else
                    {
                        txt_Add_FromDate.Text = "";
                        txt_UserID.Text = "";
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Saved.');}</script>");
                        tbl_showMyAddPanel.Style["display"] = "none";
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
    //private void DisplayMode(int ModeID)
    //{
    //    switch (ModeID)
    //    {
    //        case 1:
    //            lblMode.Text = "Add";
    //            break;
    //        case 2:
    //            lblMode.Text = "Edit";
    //            break;
    //        case 3:
    //            lblMode.Text = "View All";
    //            break;
    //        case 0:
    //            lblMode.Text = "";
    //            break;
    //    }
    //}
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
    protected void btn_Search_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
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

                DTS = (DataTable)ActionController.ExecuteAction("", "ReleaseNotes.aspx", "search", ref IsValid, "", "", "RELEASE_NOTE", "%");
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
                   // DisplayMode(3);


                    tbl_showMyViewAllPanel.Style["display"] = "block";
                    tbl_showMyButtonPanel.Style["display"] = "none";
                }
                else
                {
                    dgViewAll.DataSource = null;
                    dgViewAll.DataBind();
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
            txt_Add_FromDate.Text = "";
            txt_UserID.Text = "";
            tbl_showMyButtonPanel.Style["display"] = "block";
            tbl_showMyAddPanel.Style["display"] = "none";
            DivDisplayError.Style["Display"] = "none";
            //DisplayMode(0);
        }
    }
    protected void btn_AllEmpInfo_Back_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            tbl_showMyButtonPanel.Style["display"] = "block";
            tbl_showMyViewAllPanel.Style["display"] = "none";
           // DisplayMode(0);
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
                //DisplayMode(3);
                setGridAllign((DataTable)Session["ResultData"]);
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
            ExportGridView(dgViewAll, "RELEASE NOTE");
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
    }
}


