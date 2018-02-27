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
using FSL.Cryptography;
public partial class User_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(User_Master));
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            if (!IsPostBack)
            {

                txt_ActionObjectID.Text = Request.Params.Get("objID");
                Initialization();
                SetAccessibilty();
                txt_DomainName.Text = ((string)Session["DomainName"]);
                fillcity();
                filldepartment();
                filldesignation();
            }
        }
        //DisplayMode(0);

    }

    private void fillcity()
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {

                string IsValid = string.Empty;
                txt_searchstring.Text = "%";
                txt_procedure.Text = "CITIES";
                DataTable dt = (DataTable)ActionController.ExecuteAction(this, "getrecords", ref IsValid);
                ddl_Add_City.DataSource = dt;
                ddl_Add_City.DataTextField = "CITY";
                ddl_Add_City.DataValueField = "CITY";
                ddl_Add_City.DataBind();
                ListItem li = new ListItem("---Select One---", "");
                ddl_Add_City.Items.Insert(0, li);

            }
            catch (Exception Ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, Ex);
            }
        }

    }

    private void filldesignation()
    {
        
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                
                string IsValid = string.Empty;
                txt_searchstring.Text = "%";
                txt_procedure.Text = "DESIGNATION";
                DataTable dt = (DataTable)ActionController.ExecuteAction(this, "getrecords", ref IsValid);
                ddl_Add_Designation.DataSource = dt;
                ddl_Add_Designation.DataTextField = "DESIGNATION";
                ddl_Add_Designation.DataValueField = "DESIGNATION";
                ddl_Add_Designation.DataBind();
                ListItem li = new ListItem("---Select One---", "");
                ddl_Add_Designation.Items.Insert(0, li);

            }
            catch (Exception Ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, Ex);
            }
        }
    }

    private void filldepartment()
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                string IsValid = string.Empty;
                txt_searchstring.Text = "%";
                txt_procedure.Text = "DEPARTMENT";
                DataTable dt = (DataTable)ActionController.ExecuteAction(this, "getrecords", ref IsValid);
                ddl_Add_Department.DataSource = dt;
                ddl_Add_Department.DataTextField = "DEPARTMENT";
                ddl_Add_Department.DataValueField = "DEPARTMENT";
                ddl_Add_Department.DataBind();
                ListItem li = new ListItem("---Select One---", "");
                ddl_Add_Department.Items.Insert(0, li);

            }
            catch (Exception Ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, Ex);
            }
        }
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string[] fillBranches(string location)
    {
        string[] ResultData = null;
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        //txtLocationID.Text = ddlLocation.SelectedItem.Value;
        if (!ActionController.IsSessionExpired(this, true))
        {
            ResultData = new string[2];
            try
            {
                // DataTable dt = (DataTable)ActionController.ExecuteAction(this, "getsublocationlist", ref isValid);
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "User_Master.aspx", "getbranches", ref isValid, location);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        DisplayData = dt.Rows[i][1].ToString() + "||" + dt.Rows[i][1].ToString() + ",," + DisplayData;

                    }
                }

            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
        ResultData[0] = DisplayData;
        return ResultData;
    }
    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string[] fillLocations(string city)
    {
        string[] ResultData = null;
        string DisplayData = string.Empty;
        string isValid = string.Empty;
        //txtLocationID.Text = ddlLocation.SelectedItem.Value;
        if (!ActionController.IsSessionExpired(this, true))
        {
            ResultData = new string[2];
            try
            {
                // DataTable dt = (DataTable)ActionController.ExecuteAction(this, "getsublocationlist", ref isValid);
                DataTable dt = (DataTable)ActionController.ExecuteAction("", "User_Master.aspx", "getlocations", ref isValid, city);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        DisplayData = dt.Rows[i][1].ToString() + "||" + dt.Rows[i][1].ToString() + ",," + DisplayData;

                    }
                }

            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
        ResultData[0] = DisplayData;
        return ResultData;
    } 
    private void SetAccessibilty()
    {
        try
        {
            string UserAccess = ActionController.ObjectAccesses(txtCreatedBy.Text, txt_ActionObjectID.Text);
            if (string.IsNullOrEmpty(UserAccess))
                Response.Write("<script>window.open('/Sudarshan-Portal-NEW/ErrorPĂges/Error.htm','frmset_WorkArea');</script>");
            else
            {
                if (UserAccess.IndexOf("1") == -1) { tr_Message.Visible = true; tr_Buttons.Visible = false; }
                else
                {

                    if ((UserAccess.Length >= 1) && (UserAccess.Substring(0, 1).Equals("0"))) { btn_ViewAll_Go.Visible = false; }
                    if ((UserAccess.Length >= 2) && (UserAccess.Substring(1, 1).Equals("0"))) { btn_AddNew_Go.Visible = false; }
                    if ((UserAccess.Length >= 3) && (UserAccess.Substring(2, 1).Equals("0"))) { btn_Edit.Visible = false; btn_Edit.Enabled = false; }
                    if ((UserAccess.Length >= 4) && (UserAccess.Substring(3, 1).Equals("0"))) { btn_Delete.Visible = false; btn_Delete.Enabled = false; }
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
            try
            {
                string Result = string.Empty;
                txtCreatedBy.Text = ((string)Session["User_ADID"]);
                tbl_showMyAddPanel.Visible = true;
                tbl_showMyEditPanel.Visible = true;
                tbl_showMyEditPanel.Style["display"] = "none";
                tbl_showMyAddPanel.Style["display"] = "none";
                ActionController.SetControlAttributes(Page);

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            tbl_showMyEditPanel.Visible = false;
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
            tbl_showMyAddPanel.Visible = true;
            tbl_showMyEditPanel.Visible = true;
            tbl_showMyAddPanel.Style["display"] = "block";
            tbl_showMyButtonPanel.Style["display"] = "none";
            //DisplayMode(1);

        }
    }
    protected void btn_New_onClick(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            txt_AMDDate.Text = DateTime.Now.Date.ToString();
            Hashtable validationHash = new Hashtable();
            FSL.Cryptography.BlowFish bw = new FSL.Cryptography.BlowFish();
            bw.Initialize("flologic");
            txt_Password.Text = bw.Encrypt(txt_Add_Password.Text);
            txtParameterID.Text = "0";
            string Result = string.Empty;
            string isInsert = string.Empty;
            try
            {
                bool flag = ActionController.ValidateForm(Page, "insert", validationHash);
                if (flag)
                {
                    string str = txtCreatedBy.Text;
                    Result = (string)ActionController.ExecuteAction(this, "insert", ref isInsert);
                    if ((Result == null && isInsert.Length > 0) || isInsert == "false")
                    {
                        string[] errmsg = isInsert.Split(':');
                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[1].ToString() + "');}</script>");
                    }
                    else
                    {

                        Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Saved.');}</script>");
                        tbl_showMyAddPanel.Style["display"] = "none";
                        tbl_showMyButtonPanel.Style["display"] = "block";
                        txt_Add_Address1.Text = "";
                        txt_Add_Address2.Text = "";
                        txt_Add_Email.Text = "";
                        txt_Add_EmployeeName.Text = "";
                        txt_Add_Password.Text = "";
                        ddl_Add_Department.SelectedIndex = 0;
                        ddl_Add_Designation.SelectedIndex = 0;
                        ddl_Add_IsActive.SelectedIndex = 0;
                        ddl_Add_City.SelectedIndex = 0;
                        txt_Add_UserName.Text = "";                   


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
            tbl_showMyAddPanel.Visible = false;
            tbl_showMyEditPanel.Visible = true;
            tbl_showMyEditPanel.Style["display"] = "none";
            tbl_showMyAddPanel.Style["display"] = "none";
            ActionController.SetControlAttributes(Page);
            //Response.Write("<script language='JavaScript' type='text/javascript'>btn_Search_onClick(1);</script>");
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
                txt_searchstring.Text = "%";
                txt_procedure.Text = "USERS";
                DTS = (DataTable)ActionController.ExecuteAction(Page, "getrecords", ref IsValid);
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
                    //DisplayMode(3);


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
            //DisplayMode(0);
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
            try
            {
                bool flag = ActionController.ValidateForm(Page, "update", validationHash);
                if (flag)
                {
                    isUpdated = (string)ActionController.ExecuteAction(this, "update", ref isComplete);
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
            try
            {
                bool flag = ActionController.ValidateForm(Page, "update", validationHash);
                if (flag)
                {
                    isDeleted = (string)ActionController.ExecuteAction(this, "update", ref isValid);
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
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
    }

}


