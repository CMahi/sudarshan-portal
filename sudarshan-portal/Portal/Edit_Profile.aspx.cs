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


public partial class Edit_Profile : System.Web.UI.Page
{
    string compname = ConfigurationManager.AppSettings["COMPNAME"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(Edit_Profile));
                if (!Page.IsPostBack)
                {
                    txt_VendorName.Text =  Session["USER_NAME"].ToString();
                    if (Session["USER_ADID"] != null)
                    {
                        txt_Username.Text = txt_VendorCode.Text = Convert.ToString(Session["USER_ADID"]);
                        getVendor_Details();
                    }
                    app_Path.Text = HttpContext.Current.Request.ApplicationPath;
                   
                }
               
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    public void getVendor_Details()
    {
        try
        {
            string IsData = string.Empty;
            string ad_id = Convert.ToString(Session["USER_ADID"]);

            DataTable dtH = (DataTable)ActionController.ExecuteAction("", "Edit_Profile.aspx", "getvendordetailshistory", ref IsData, ad_id);
            if (dtH.Rows.Count > 0)
            {
                last_updated.Text = Convert.ToString(dtH.Rows[0]["created_date"]).Trim();

                if (last_updated.Text == "")
                {
                    is_changed.Text = "1";
                }
                else
                {
                    DateTime new_date = Convert.ToDateTime(last_updated.Text).AddYears(1);
                    if (new_date < DateTime.Now)
                    {
                        is_changed.Text = "1";
                    }
                    else
                    {
                        is_changed.Text = "0";
                    }
                }
            }
            else
            {
                is_changed.Text = "1";
            }

            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Edit_Profile.aspx", "getvendordetails", ref IsData, ad_id);

            if (dt.Rows.Count > 0)
            {
               Email.Text = txt_Email.Text = Convert.ToString(dt.Rows[0]["Email"]).Trim();
               Website.Text = txt_Website.Text = Convert.ToString(dt.Rows[0]["website"]).Trim();
               Mobile.Text = txt_Mobile.Text = Convert.ToString(dt.Rows[0]["Telephone1"]).Trim();
               Contact.Text = txt_Contact.Text = Convert.ToString(dt.Rows[0]["Telephone2"]).Trim();
               Fax.Text = txt_Fax.Text = Convert.ToString(dt.Rows[0]["FAX_NO"]).Trim();
               PAN.Text = txt_PAN.Text = Convert.ToString(dt.Rows[0]["PAN_NO"]).Trim();
               Address.Text = txt_Address.Text = Convert.ToString(dt.Rows[0]["Address"]).Trim();
               ECC.Text = txt_ECC.Text = Convert.ToString(dt.Rows[0]["ECC_NO"]).Trim();
               City.Text = txt_City.Text = Convert.ToString(dt.Rows[0]["City"]).Trim();
               CentralSales.Text = txt_Central.Text = Convert.ToString(dt.Rows[0]["Central_Sales_Tax_No"]).Trim();
               State.Text = txt_State.Text = Convert.ToString(dt.Rows[0]["Region"]).Trim();
               LocalSales.Text = txt_Local.Text = Convert.ToString(dt.Rows[0]["Local_Sales_Tax_No"]).Trim();
               Pin.Text = txt_Pin.Text = Convert.ToString(dt.Rows[0]["Postal_Code"]).Trim();
               Excise.Text = txt_Excise.Text = Convert.ToString(dt.Rows[0]["Excise_Reg_No"]).Trim();
               bankid.Text = txt_BankName.Text = Convert.ToString(dt.Rows[0]["FK_BANK_ID"]).Trim();
               accountno.Text = txt_accno.Text = Convert.ToString(dt.Rows[0]["ACC_NO"]).Trim();
               ifsc.Text = txt_ifsc.Text = Convert.ToString(dt.Rows[0]["IFSC_CODE"]).Trim();
               branch.Text = txt_branch.Text = Convert.ToString(dt.Rows[0]["BRANCH"]).Trim();
               txt_msmed_rno.Text = Convert.ToString(dt.Rows[0]["MSMED_REG_NO"]).Trim();
               if (txt_msmed_rno.Text != "")
               {
                   rblMSMED.SelectedValue = "Yes";
                   rblMSMED.Enabled = false;
                   txt_uploaded.Text = Convert.ToString(dt.Rows[0]["CERTIFICATE_NAME"]).Trim();
               }
               else
               {
                  
               }
            }

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "chk_Data()", true);
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void Refresh(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ActionController.IsSessionExpired(this))
                ActionController.RedirctToLogin(this);
            else
            {

            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.open('HomePage.aspx','frmset_WorkArea');}</script>");
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
            if (FileUpload1.HasFile)
            {
                Int32 flength = FileUpload1.PostedFile.ContentLength;

                string path = string.Empty;

                path = activeDir + "\\";

                string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());
                filename = filename.Replace("(", "");
                filename = filename.Replace(")", "");
                filename = filename.Replace("&", "");
                filename = filename.Replace("+", "");
                filename = filename.Replace("/", "");
                filename = filename.Replace("\\", "");
                filename = filename.Replace("'", "");
                filename = filename.Replace("  ", "");
                filename = filename.Replace(" ", "");
                filename = filename.Replace("#", "");
                filename = filename.Replace("$", "");
                filename = filename.Replace("~", "");
                filename = filename.Replace("%", "");
                filename = filename.Replace("''", "");
                filename = filename.Replace(":", "");
                filename = filename.Replace("*", "");
                filename = filename.Replace("?", "");
                filename = filename.Replace("<", "");
                filename = filename.Replace(">", "");
                filename = filename.Replace("{", "");
                filename = filename.Replace("}", "");
                filename = filename.Replace(",", "");
                lblFile.Text = txt_filename.Text = filename;
                FileUpload1.SaveAs(path + filename);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "chk_Data()", true);
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Select MSMED Registration Certificate...!');}</script>");
            }
        }
        catch (Exception Ex)
        {
            Logger.WriteEventLog(false, Ex);

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        else
        {
            try
            {
                string IsData = string.Empty;
                int is_msmed = 0;
                int is_change = 1;
                string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
                
                if (rblMSMED.SelectedValue == "Yes")
                {
                    is_msmed = 1;
                }
                else
                {
                    is_msmed = 0;
                }
                if (is_changed.Text == "1")
                {
                    is_change = 1;
                    if (txt_filename.Text == "")
                    {
                        if (txt_uploaded.Text != "")
                        {
                            txt_filename.Text = txt_uploaded.Text;
                        }
                    }
                }
                else
                {
                 is_change = 0;
                }
                

                string result = (string)ActionController.ExecuteAction("", "Edit_Profile.aspx", "update", ref IsData, txt_Username.Text, txt_Address.Text, txt_City.Text, txt_State.Text, txt_Pin.Text, txt_Mobile.Text, txt_Contact.Text, txt_Fax.Text, txt_PAN.Text, txt_ECC.Text, txt_Local.Text, txt_Central.Text, txt_Excise.Text, txt_Email.Text, txt_Website.Text, txt_BankName.Text, txt_accno.Text, txt_ifsc.Text, txt_branch.Text, is_change,is_msmed, txt_msmed_rno.Text.Trim(), txt_filename.Text);
                
                if (result == null || IsData.Length > 0 || result == "false")
                {
                    string[] errmsg = IsData.Split(':');
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('" + errmsg[0].ToString() + "');}</script>");
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Vendor Details Updated Successfully...!');window.open('HomePage.aspx','frmset_WorkArea');}</script>");
                }
            }
            catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        }
    }

}