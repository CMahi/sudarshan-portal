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


public partial class Common_FileDownload : System.Web.UI.Page
{
    string filename = string.Empty;
    string filetag = string.Empty;
    string requestno = string.Empty;
    string str = string.Empty;
    string activeDir = string.Empty;
    string vendorcode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActionController.DisablePageCaching(this);
        if (ActionController.IsSessionExpired(Page))
            ActionController.RedirctToLogin(Page);
        if (Request.QueryString[0] == null)
            Page.RegisterStartupScript("onclick", "<script language='javascript'>{window.close()}</script>");
        if (!IsPostBack)
        {
            requestno = Request.QueryString[0];
            if (requestno != "NA")
            {
                str = requestno.Substring(0, 4);
            }
            requestno = requestno.Replace("/", "_");
            filename = Request.QueryString[1];
            filetag = Request.QueryString[2];

	    if (filetag == "ServicePO")
            {
                vendorcode = Request.QueryString[3];

            }
            GetUploadedFiles();
        }
    }
    private void GetUploadedFiles()
    {
        if (str == "WEEK")
        {
             activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
        }
        else
        {
             activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
        }
        string path = string.Empty; 
        if (requestno == "NA")
        { 
            path = activeDir + "\\";
        }
        else
        {
            string str1 = requestno.Substring(0, 3);
            if(str1=="MCE")
            {
                path = activeDir + "\\MOBILE DATACARD\\" + requestno + "\\";
            }
            else if (str1 == "ADV")
            {
                path = activeDir + "\\ADVANCE\\" + requestno + "\\";
            }
            else if (str1 == "LC-")
            {
                path = activeDir + "\\LOCAL CONVEYANCE\\" + requestno + "\\";
            }
            else if (filetag=="ServicePO")
            {
                path = activeDir + "\\Service PO \\"+vendorcode +"\\"+ requestno +"\\";
            }
  	    else
            {
                path = activeDir + "\\" + requestno+"\\";
            }
        }
            
        
        downloadAllFiles(filename, path);

    }
    
    private void downloadAllFiles(string docs, string location)
    {
        try
        {
            if (Directory.Exists(location))
            {
                string[] FileNames = Directory.GetFiles(location);
                WebClient client = new WebClient();
                string[] extension = docs.Split(".".ToCharArray());
//Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Hii'+docs);}</script>");
                if (File.Exists(location + docs))
                {
                    Byte[] buffer = client.DownloadData(location + docs);
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.ClearContent();
                    Response.AddHeader("Accept-Ranges", "bytes");
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + docs);
                    Response.BinaryWrite(buffer);
                    Response.Flush();
                    Response.Close();
                }
                else
                {
                    Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('File Not Found!');window.close();}</script>");
                }
            }
            else
            {
                Page.RegisterStartupScript("onclick", "<script language='javascript'>{alert('Directory Not Found!');window.close();}</script>");

            }

        }
        catch (Exception Ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, Ex);
        }
    }
}
