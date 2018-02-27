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

public partial class Compliance_Mail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Compliance_Mail));
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = Convert.ToString(Session["USER_ADID"]);
                        txtEmailID.Text = Convert.ToString(Session["EmailID"]);
                    }
                    fillDropDowns();
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    #region Pageload
    private void fillDropDowns()
    {
        try
        {
            txt_Created_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ListItem Li = new ListItem("--Select One--", "");
            string isdata = string.Empty;
            DataTable Cat_Dt = (DataTable)ActionController.ExecuteAction("", "Compliance_Mail.aspx", "psearch", ref isdata, "0", "", "", "2", "0");
            if (Cat_Dt != null)
            {
                ddl_Add_Cat.DataSource = Cat_Dt;
                ddl_Add_Cat.DataTextField = "CATEGORY_NAME";
                ddl_Add_Cat.DataValueField = "PK_CMP_CAT_ID";
                ddl_Add_Cat.DataBind();
                ddl_Add_Cat.Items.Insert(0, Li);
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    #endregion

    #region Ajax
   [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public string[] getLevel(int id)
    {
        string[] ResultData = null;
        string Level_Data = string.Empty;
        string Emal_Data = string.Empty;
        string isdata = string.Empty;
        if (!ActionController.IsSessionExpired(this, true))
        {
            ResultData = new string[3];
            try
            {
                DataTable Level = (DataTable)ActionController.ExecuteAction("", "Compliance_Mail.aspx", "psearch", ref isdata, 0, "", "", "0", 0);
                if (Level.Rows.Count > 0)
                {
                    for (int i = 0; i < Level.Rows.Count; i++)
                    {

                        Level_Data = Level.Rows[i][0].ToString() + "||" + Level.Rows[i][1].ToString() + "," + Level_Data;

                    }
                }

                DataTable Email = (DataTable)ActionController.ExecuteAction("", "Compliance_Mail.aspx", "psearch", ref isdata, 0, "", "", "1", 0);
                if (Email.Rows.Count > 0)
                {
                    for (int i = 0; i < Email.Rows.Count; i++)
                    {

                        Emal_Data = Email.Rows[i][0].ToString() + "||" + Email.Rows[i][1].ToString() + "," + Emal_Data;

                    }
                }

            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
        ResultData[0] = Level_Data;
        ResultData[1] = Emal_Data;
        ResultData[2] = Convert.ToString(id);
        return ResultData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
   public string[] getComplianceDetails(int Cat_ID, int Compliance_ID)
    {
        int m = 0;
        int n = 0;
        string[] ResultData = null;
        string isdata = string.Empty;
        int Before_Count = 0;
        int After_Count = 0;
        StringBuilder Before_Data = new StringBuilder();
        StringBuilder After_Data = new StringBuilder();
        if (!ActionController.IsSessionExpired(this, true))
        {
            ResultData = new string[3];
            try
            {
                DataTable Comp_Dtl = (DataTable)ActionController.ExecuteAction("", "Compliance_Mail.aspx", "psearch", ref isdata, Compliance_ID,  "", "", "3", Cat_ID);
                if (Comp_Dtl.Rows.Count > 0)
                {
                    for (int i = 0; i < Comp_Dtl.Rows.Count; i++)
                    {
                        if ( Comp_Dtl.Rows[i]["MAIL_CATEGORY"].ToString()=="Before")
                        {
                            Before_Count++;
                        }
                        else
                        {
                            After_Count++;
                        }
                    }
                    if (Before_Count>0)
                    {
                        Before_Data.Append("<table id='tbl_Before' style='width:70%' class='table table-bordered table-hover'>");
                        Before_Data.Append("<thead>");
                        Before_Data.Append("<tr class='grey'>");
                        Before_Data.Append("<th>Add</th><th>Delete</th><th>Days</th><th>To Client</th><th>Email Type</th></tr></thead><tbody>");
                        for (int i = 0; i < Comp_Dtl.Rows.Count; i++)
                        {
                            if (Comp_Dtl.Rows[i]["MAIL_CATEGORY"].ToString() == "Before")
                            {
                               
                                Before_Data.Append(" <tr>");
                                Before_Data.Append("<td  width='2%'><img id=Be_add" +(m+1) + "     src='/Sudarshan-Portal/Img/MoveUp.GIF'  onclick=newRow('Before') alt='Click here to Add New Row.' /></td>");
                                Before_Data.Append("<td  width='3%'><img id=Be_delete" + (m + 1) + "  src='/Sudarshan-Portal/Img/button_cancel.png' onclick=DeleteRow('Before'," + (m + 1) + ") alt='Click here to Delete Row.' /></td>");
                                Before_Data.Append("<td  width='8%'><input type='text' onkeyup='return chknumeric(this.id);'   id=txt_BeDays" + (m + 1) + " value=" + (Comp_Dtl.Rows[i]["DAYS"].ToString()) + "  class='form-control input-md'/></td>");
                                Before_Data.Append("<td  width='15%'><select name='select' id=ddl_BeLevels" + (m + 1) + "  class='form-control input-md' >" + getBeforeLevels(Comp_Dtl.Rows[i]["FK_LEVEL_ID"].ToString()) + "</select></td>");
                                Before_Data.Append("<td  width='15%'><select name='select' id=ddl_BeEmail_Type" + (m + 1) + "  class='form-control input-md' >" + getBeforeEmails(Comp_Dtl.Rows[i]["FK_EMAIL_TYPE"].ToString()) + "</select></td>");
                                Before_Data.Append("</tr>");
                                m++;
                            }
                      
                        }
                        Before_Data.Append("   </tbody>   </table> ");
                    }
                    if (After_Count > 0)
                    {
                        After_Data.Append("<table id='tbl_After' style='width:70%' class='table table-bordered table-hover'>");
                        After_Data.Append("<thead>");
                        After_Data.Append("<tr class='grey'>");
                        After_Data.Append("<th>Add</th><th>Delete</th><th>Days</th><th>To Client</th><th>Email Type</th></tr></thead><tbody>");
                        for (int i = 0; i < Comp_Dtl.Rows.Count; i++)
                        {
                            if (Comp_Dtl.Rows[i]["MAIL_CATEGORY"].ToString() == "After")
                            {
                                After_Data.Append(" <tr>");
                                After_Data.Append("<td  width='2%'><img id=Af_add" + (n + 1) + "     src='/Sudarshan-Portal/Img/MoveUp.GIF'  onclick=newRow('After') alt='Click here to Add New Row.' /></td>");
                                After_Data.Append("<td  width='3%'><img id=Af_delete" + (n + 1) + "  src='/Sudarshan-Portal/Img/button_cancel.png' onclick=DeleteRow('After'," + (n + 1) + ") alt='Click here to Delete Row.' /></td>");
                                After_Data.Append("<td  width='8%'><input type='text'  onkeyup='return chknumeric(this.id);'  id=txt_Af_Days" + (n + 1) + " value=" + (Comp_Dtl.Rows[i]["DAYS"].ToString()) + " class='form-control input-md'/></td>");
                                After_Data.Append("<td  width='15%'><select name='select' id=ddl_AfLevels" + (n + 1) + "  class='form-control input-md' >" + getBeforeLevels(Comp_Dtl.Rows[i]["FK_LEVEL_ID"].ToString()) + "</select></td>");
                                After_Data.Append("<td  width='15%'><select name='select' id=ddl_AfEmailType" + (n + 1) + "  class='form-control input-md' >" + getBeforeEmails(Comp_Dtl.Rows[i]["FK_EMAIL_TYPE"].ToString()) + "</select></td>");
                                After_Data.Append("</tr>");
                                n++;
                            }
                        
                        }
                        After_Data.Append("   </tbody>   </table> ");
                    }
                }
                else
                {
                    Before_Data.Append("<table id='tbl_Before' style='width:70%' class='table table-bordered table-hover'>");
                    Before_Data.Append("<thead>");
                    Before_Data.Append("<tr class='grey'>");
                    Before_Data.Append("<th>Add</th><th>Delete</th><th>Days</th><th>To Client</th><th>Email Type</th></tr></thead><tbody>");
                    Before_Data.Append(" <tr>");
                    Before_Data.Append("<td  width='2%'><img id=Be_add1     src='/Sudarshan-Portal/Img/MoveUp.GIF'  onclick=newRow('Before') alt='Click here to Add New Row.' /></td>");
                    Before_Data.Append("<td  width='3%'><img id=Be_delete1 src='/Sudarshan-Portal/Img/button_cancel.png' onclick=DeleteRow('Before','1') alt='Click here to Delete Row.' /></td>");
                    Before_Data.Append("<td  width='8%'><input type='text'  onkeyup='return chknumeric(this.id);'  id=txt_BeDays1 value=''  class='form-control input-md'/></td>");
                    Before_Data.Append("<td  width='15%'><select name='select' id=ddl_BeLevels1  class='form-control input-md' >" + getBLevels() + "</select></td>");
                    Before_Data.Append("<td  width='15%'><select name='select' id=ddl_BeEmail_Type1  class='form-control input-md' >" + getBEmails() + "</select></td>");
                    Before_Data.Append("</tr>");
                    Before_Data.Append("</tbody></table> ");

                    After_Data.Append("<table id='tbl_After' style='width:70%' class='table table-bordered table-hover'>");
                    After_Data.Append("<thead>");
                    After_Data.Append("<tr class='grey'>");
                    After_Data.Append("<th>Add</th><th>Delete</th><th>Days</th><th>To Client</th><th>Email Type</th></tr></thead><tbody>");
                    After_Data.Append(" <tr>");
                    After_Data.Append("<td  width='2%'><img id=Af_add1     src='/Sudarshan-Portal/Img/MoveUp.GIF'  onclick=newRow('After') alt='Click here to Add New Row.' /></td>");
                    After_Data.Append("<td  width='3%'><img id=Af_delete1  src='/Sudarshan-Portal/Img/button_cancel.png' onclick=DeleteRow('Before','1') alt='Click here to Delete Row.' /></td>");
                    After_Data.Append("<td  width='8%'><input type='text'   onkeyup='return chknumeric(this.id);' id=txt_Af_Days1 value='' class='form-control input-md'/></td>");
                    After_Data.Append("<td  width='15%'><select name='select' id=ddl_AfLevels1 class='form-control input-md' >" + getBLevels() + "</select></td>");
                    After_Data.Append("<td  width='15%'><select name='select' id=ddl_AfEmailType1  class='form-control input-md' >" + getBEmails() + "</select></td>");
                    After_Data.Append("</tr>");
                    After_Data.Append("   </tbody>   </table> ");

                }
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
        }
        ResultData[0] = Convert.ToString(Before_Data);
        ResultData[1] = Convert.ToString(After_Data);
        return ResultData;
    }

    [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public static string[] getcompliance(string FK_CompType_ID)
    {
        string[] ResultData = null;
        string DisplayData = string.Empty;
        string isdata = string.Empty;

        ResultData = new string[5];
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Compliance_Mail.aspx", "psearch", ref isdata, "0", "", "", "5", Convert.ToInt32(FK_CompType_ID));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DisplayData = dt.Rows[i][0].ToString() + "||" + dt.Rows[i][1].ToString() + "," + DisplayData;
                }

            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

        ResultData[0] = DisplayData;
        return ResultData;
    }
    #endregion

    #region Events
    protected void btn_AddNew_Click(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                string isdata = string.Empty;
                string created_by = txt_Username.Text.Trim();
                string isSaved = (string)ActionController.ExecuteAction("", "Compliance_Mail.aspx", "pamd", ref isdata,Convert.ToInt32(txt_Compliance_ID.Text), txt_Before_XML.Text, txr_After_XML.Text,4,0);
                if (isSaved == "true")
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Data Saved.');}</script>");
                    ddl_Add_Cat.SelectedIndex = 0;
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + isSaved + "');}</script>");
                }


            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(this))
            ActionController.RedirctToLogin(this);
        else
        {
            try
            {
                ddl_Add_Cat.SelectedIndex = 0;
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }
    #endregion

    #region Others
    private string getBeforeLevels(string Fk_ID)
    {
        string isdata = string.Empty;
        StringBuilder sb = new StringBuilder();
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Compliance_Mail.aspx", "psearch", ref isdata, 0, "", "", "0", 0);
            if (dt.Rows.Count>0)
            {
                sb.Append("<option value=''>----Select One----</option>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ( dt.Rows[i]["PK_LEVEL_ID"].ToString()==Fk_ID)
                    {
                        sb.Append("<option value=" + dt.Rows[i]["PK_LEVEL_ID"].ToString() + " selected='selected'>" + dt.Rows[i]["LEVEL_NAME"].ToString() + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value=" + dt.Rows[i]["PK_LEVEL_ID"].ToString() + " >" + dt.Rows[i]["LEVEL_NAME"].ToString() + "</option>");
                    }
                    
                }
               
            }
            
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return sb.ToString();
    }
    private string getBLevels()
    {
        string isdata = string.Empty;
        StringBuilder sb = new StringBuilder();
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Compliance_Mail.aspx", "psearch", ref isdata, 0, "", "", "0", 0);
            if (dt.Rows.Count > 0)
            {
                sb.Append("<option value=''>----Select One----</option>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   sb.Append("<option value=" + dt.Rows[i]["PK_LEVEL_ID"].ToString() + " >" + dt.Rows[i]["LEVEL_NAME"].ToString() + "</option>");
                }

            }

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return sb.ToString();
    }

    private string getBeforeEmails(string Fk_ID)
    {
        string isdata = string.Empty;
        StringBuilder sb = new StringBuilder();
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Compliance_Mail.aspx", "psearch", ref isdata, 0,"", "", "1", 0);
            if (dt.Rows.Count > 0)
            {
                sb.Append("<option value=''>----Select One----</option>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["PK_EMAILTYPE_ID"].ToString() == Fk_ID)
                    {
                        sb.Append("<option value=" + dt.Rows[i]["PK_EMAILTYPE_ID"].ToString() + " selected='selected'>" + dt.Rows[i]["EMAIL_TYPE"].ToString() + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value=" + dt.Rows[i]["PK_EMAILTYPE_ID"].ToString() + " >" + dt.Rows[i]["EMAIL_TYPE"].ToString() + "</option>");
                    }

                }

            }

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return sb.ToString();
    }

    private string getBEmails()
    {
        string isdata = string.Empty;
        StringBuilder sb = new StringBuilder();
        try
        {
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Compliance_Mail.aspx", "psearch", ref isdata, 0,"", "", "1", 0);
            if (dt.Rows.Count > 0)
            {
                sb.Append("<option value=''>----Select One----</option>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                        sb.Append("<option value=" + dt.Rows[i]["PK_EMAILTYPE_ID"].ToString() + " >" + dt.Rows[i]["EMAIL_TYPE"].ToString() + "</option>");
                }

            }

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return sb.ToString();
    }
    #endregion others
}