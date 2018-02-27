using System;
using System.Text;
using System.Data;
using System.Net;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using FSL.Controller;
using FSL.Logging;
using System.IO;
using System.Xml;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            
                if (!IsPostBack)
                {
                    if (Session["USER_ADID"] != null)
                    {
                        Response.Write("<script>alert('User already logged in!!!');{ window.close(); }</script>");

                    }
                    else
                    {
                        try
                        {
                            alert_message.Visible = false;
                            if (Request.QueryString[2].ToString() != null && Request.QueryString[2].ToString() != "")
                            {
                                FSL.Cryptography.BlowFish BFish = new FSL.Cryptography.BlowFish();
                                BFish.Initialize(getPublickey());
                                txtlogin.Value = Request.QueryString[2].ToString();
                                txtlogin.Attributes.Add("readonly", "readonly");


                                string INS = Server.UrlEncode(Request.QueryString[0]).Replace("%3d", "=");
                                INS = INS.Replace("%2f", "/");
                                string PROCID = Server.UrlEncode(Request.QueryString[1]).Replace("%3d", "=");
                                PROCID = PROCID.Replace("%2f", "/");
                            }


                        }
                        catch (Exception Exx)
                        {
                            FSL.Logging.Logger.WriteEventLog(false, Exx);
                        }
                        finally
                        {

                            Hashtable[] DomainHash = Application["DOMAINHASH"] != null ? (Hashtable[])Application["DOMAINHASH"] : ActionController.GetDomains(Page);
                            if (DomainHash != null)
                            {
                                foreach (Hashtable hash in DomainHash)
                                    if ((hash.ContainsKey("domain-name")) && (hash.ContainsKey("display-name")))
                                    {
                                        //cboDomain.Items.Add(new ListItem(hash["display-name"].ToString(), hash["domain-name"].ToString()));
                                    }
                            }
                            if (Application["DOMAINHASH"] == null) Application["DOMAINHASH"] = DomainHash;


                            string AuthenticationStatus = Request.Params.Get("Message");
                            if (!string.IsNullOrEmpty(AuthenticationStatus))
                            {
                                if (AuthenticationStatus.StartsWith("ValidationErr-"))
                                {
                                    errMsg.InnerText = AuthenticationStatus;
                                    alert_message.Visible = true;

                                }

                                 else
                                    if (AuthenticationStatus.ToLower().Equals("duplicate session"))
                                        Response.Write("<script language='JavaScript'  type='text/javascript' src='JS/login.js'></script><script language='javascript'>{doValidation();}</script>");
                                    else
                                    {
                                        if ((AuthenticationStatus.StartsWith("AD1001")) || (AuthenticationStatus.StartsWith("AD1002")) || (AuthenticationStatus.Equals("Invalid user or password!")))
                                            errMsg.InnerText = "Invalid user or password!";
                                        else if ((AuthenticationStatus.StartsWith("AD1001")) || (AuthenticationStatus.StartsWith("AD1002")) || (AuthenticationStatus.Equals("Your login is blocked please contact Vendor Portal Admin")))
                                            errMsg.InnerText = "Your login is blocked please contact Vendor Portal Admin";
                                        else if ((AuthenticationStatus.StartsWith("AD1001")) || (AuthenticationStatus.StartsWith("AD1002")) || (AuthenticationStatus.Equals("2 Attempts are remaining ,otherwise your login will be blocked please attempt with correct Password")))
                                            errMsg.InnerText = "2 Attempts are remaining ,otherwise your login will be blocked please attempt with correct Password";
                                        else if ((AuthenticationStatus.StartsWith("AD1001")) || (AuthenticationStatus.StartsWith("AD1002")) || (AuthenticationStatus.Equals("1 Attempts are remaining ,otherwise your login will be blocked please attempt with correct Password")))
                                            errMsg.InnerText = "1 Attempts are remaining ,otherwise your login will be blocked please attempt with correct Password";
                                        else if ((AuthenticationStatus.StartsWith("AD1001")) || (AuthenticationStatus.StartsWith("AD1002")) || (AuthenticationStatus.Equals("Email is send successfully please check your email account...!")))
                                            errMsg.InnerText = "Email is send successfully please check your email account...!";
                                        else
                                            errMsg.InnerText = "Unable to connect! Please try later.";
                                        alert_message.Visible = true;
                                    }
                            }
                            if ((string.IsNullOrEmpty(AuthenticationStatus)) || (!AuthenticationStatus.ToLower().Equals("duplicate session")))
                                Response.SetCookie(new HttpCookie("ASP.NET_SessionId", string.Empty));
                        }
                    }
                }
            

        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    private string getPublickey()
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "config\\";
        DirectoryInfo dirinfo = new DirectoryInfo(path);
        FileInfo[] fileInf = dirinfo.GetFiles("PublicKey.config");
        StreamReader sr = new StreamReader(path + "PublicKey.config");
        XmlDocument xdoc = new XmlDocument();
        xdoc.LoadXml(sr.ReadToEnd());
        string strPublicKey = "";
        if (xdoc.HasChildNodes == true)
        {
            XmlNodeList nodes = xdoc.SelectNodes("/configuration/db-key");
            foreach (XmlNode node in nodes)
            {
                strPublicKey = node.InnerText;
            }

        }
        //strPublicKey = "";
        return strPublicKey;
    }

    public bool True { get; set; }
}