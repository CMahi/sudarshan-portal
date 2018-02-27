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

public partial class Forgot_Password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
     protected void btn_CancelOnclick(object sender, EventArgs e)
    {
	Response.Redirect("Login.aspx");
    }

    protected void btn_SendOnclick(object sender, EventArgs e)
    {
        string IsData = string.Empty;
        string IsInsert = string.Empty;
        string issaved = string.Empty;
        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Forgot_Password.aspx", "getmail", ref IsData, txt_UserName.Text);
        if (dt != null && dt.Rows.Count > 0)
        {
            txt_Email_ID.Text = Convert.ToString(dt.Rows[0]["Email"]);

            if (txt_Email.Text.Trim() == txt_Email_ID.Text.Trim())
            {
                issaved = (string)ActionController.ExecuteAction("", "Forgot_Password.aspx", "insertmaildata", ref IsInsert, txt_UserName.Text, txt_Email.Text);
                if (issaved != null && issaved != "")
                {
                    string Message = "Email is send successfully please check your email account...!";
                    //MsgBox.Show("Email is send successfully please check your email account...!");
                    // Response.Write("<script>alert('Email is send successfully please check your email account...!');</script>");
                    Response.Redirect("Login.aspx?Message=" + Message, false);
                }
                else
                {
                    MsgBox.Show("Email is not send please try again later...!");
                }
            }
            else
            {
                MsgBox.Show("Email ID is not correct...!");

            }
        }
	else
	{
		MsgBox.Show("User Name or Email ID is not correct");
		txt_UserName.Text = ""; 
		txt_Email.Text = "";
	}
    }
}