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
using System.Net;
using System.IO;


public partial class Common_ViewDocument : System.Web.UI.Page
{
    string pono = string.Empty;
    string type = string.Empty;
    string str = string.Empty;
    string activeDir = string.Empty;
    CryptoGraphy crypt = new CryptoGraphy();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        if (Request.QueryString[0] == null)
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.close()}</script>");
        if (!IsPostBack)
        {
            if (Session["USER_ADID"] != null)
            {
                pono = Convert.ToString(Request.QueryString[0]);
                type = Convert.ToString(Request.QueryString[1]);
                GetUploadedFiles(pono,type);
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.close()}</script>");
            }
        }
    }
    private void GetUploadedFiles(string pono,string type)
    {
        try
        {
            string decrypt_Str = crypt.Decryptdata(pono);
            string isdata = string.Empty;
            string path = (string)ActionController.ExecuteAction("", "Early_Payment_Request.aspx", "getpath", ref isdata, type);
	    //path=@"\\\\200.200.200.43\\Vendor Portal\\PO";
//Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('"+path+"');}</script>");
//MsgBox.Show(@"\\\\200.200.200.43\\Vendor Portal\\PO\\4500196834.pdf");
//Response.Redirect("\\200.200.200.43\Vendor Portal\PO\4500196834.pdf",true);
Response.Redirect(path + decrypt_Str + ".pdf");
           /* if (Directory.Exists(path))
            {
                if (File.Exists(@"" + path + "" + decrypt_Str + ".pdf"))
                {
                   // string[] FileNames = Directory.GetFiles(path);
                    WebClient client = new WebClient();
                    string val = decrypt_Str + ".pdf";
                    string[] extension = val.Split(".".ToCharArray());
                    Byte[] buffer = client.DownloadData(path + val);
                    HttpContext.Current.Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("content-length", buffer.Length.ToString());
                    HttpContext.Current.Response.BinaryWrite(buffer);
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('File Does Not Exists');}</script>");
                }
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Directory Does Not Exists');}</script>");
            }*/
        }
        catch (Exception ex)
        { FSL.Logging.Logger.WriteEventLog(false, ex); }

    }
    
    
}
