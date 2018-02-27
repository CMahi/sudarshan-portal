
using System;
using Microsoft.VisualBasic;
using System.Text;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using FSL.Cryptography;
using System.Xml;
using System.IO;
using FSL.Logging;
using System.Net.Mime;
using FSL.Message;
using System.Net;
using System.Net.Mail;


public class MsgBox
{

    protected static Hashtable handlerPages = new Hashtable();
    public MsgBox()
    {

    }


    public static void Show(string Message)
    {

        if (!(handlerPages.Contains(HttpContext.Current.Handler)))
        {

            Page currentPage = (Page)HttpContext.Current.Handler;

            if (!((currentPage == null)))
            {

                Queue messageQueue = new Queue();

                messageQueue.Enqueue(Message);

                handlerPages.Add(HttpContext.Current.Handler, messageQueue);

                currentPage.Unload += new EventHandler(CurrentPageUnload);

            }

        }

        else
        {

            Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));

            queue.Enqueue(Message);

        }

    }



    private static void CurrentPageUnload(object sender, EventArgs e)
    {

        Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));

        if (queue != null)
        {

            StringBuilder builder = new StringBuilder();

            int iMsgCount = queue.Count;

            builder.Append("<script language='javascript'>");

            string sMsg;

            while ((iMsgCount > 0))
            {

                iMsgCount = iMsgCount - 1;

                sMsg = System.Convert.ToString(queue.Dequeue());

                sMsg = sMsg.Replace("\"", "'");

                builder.Append("alert( \"" + sMsg + "\" );");

            }

            builder.Append("</script>");

            handlerPages.Remove(HttpContext.Current.Handler);

            HttpContext.Current.Response.Write(builder.ToString());

        }

    }


    public static void Show()
    {
        throw new NotImplementedException();
    }
    public static string getDirPath(string IdentityNo)
    {
        string foldername = IdentityNo.Replace('/', '-');
        string activeDir = ConfigurationManager.AppSettings["DOCPATH"].ToString();
        string newPath = System.IO.Path.Combine(activeDir, foldername);
        if (System.IO.Directory.Exists(newPath))
        {
            return newPath;
        }
        else
        {
            System.IO.Directory.CreateDirectory(newPath);
            return newPath;
        }
    }

    public void SendHtmlMail(string to, string cc, string subjects, string bodymsg, string imgpath)
    {
        // bool result = false;
        try
        {
            string frommail = ConfigurationSettings.AppSettings["frommailid"].ToString();
            string mailpwd = ConfigurationSettings.AppSettings["frommailidpwd"].ToString();
            string hostname = ConfigurationSettings.AppSettings["mailhost"].ToString();
            string port = ConfigurationSettings.AppSettings["smtpport"].ToString();
            bool isSSLEnable = Convert.ToBoolean(ConfigurationSettings.AppSettings["isSSLEnable"].ToString());
            FSL.Cryptography.BlowFish BFISH = new FSL.Cryptography.BlowFish(GetPublicKey1());
            string password = BFISH.Decrypt(mailpwd);
            MailMessage msgMail = new MailMessage();
            MailMessage myMessage = new MailMessage();
            myMessage.From = new MailAddress(frommail);
            myMessage.To.Add(to);

            if (cc != "")
                myMessage.CC.Add(cc);
            myMessage.Subject = subjects;

            LinkedResource logo = new LinkedResource(imgpath, "images/gif");
            logo.ContentId = "companylogo";
            // done HTML formatting in the next line to display my logo
            AlternateView av1 = AlternateView.CreateAlternateViewFromString(bodymsg + "", null, MediaTypeNames.Text.Html);
            av1.LinkedResources.Add(logo);
            myMessage.AlternateViews.Add(av1);
            myMessage.IsBodyHtml = true;
            myMessage.Body = bodymsg;
            //Attachment imgAtt = new Attachment(imgpath);
            //myMessage.Attachments.Add(imgAtt);
            SmtpClient mySmtpClient = new SmtpClient();
            System.Net.NetworkCredential myCredential = new System.Net.NetworkCredential(frommail, password);
            mySmtpClient.Host = hostname;
            mySmtpClient.Port = int.Parse(port);
            mySmtpClient.UseDefaultCredentials = false;
            mySmtpClient.Credentials = myCredential;
            mySmtpClient.ServicePoint.MaxIdleTime = 1;
            mySmtpClient.EnableSsl = isSSLEnable;
            object state = myMessage;
            mySmtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendHtmlCompleted);

            mySmtpClient.SendAsync(myMessage, state);

        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }

    }


    private static string GetPublicKey1()
    {
        string PublicKey = string.Empty;
        try
        {
            string strXmlPath = AppDomain.CurrentDomain.BaseDirectory + "Config\\PublicKey.config";
            FileStream docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            XmlDocument contactDoc = new XmlDocument();
            contactDoc.Load(docIn);
            XmlNodeList UserList = contactDoc.GetElementsByTagName("db-key");
            PublicKey = UserList.Item(0).InnerText.ToString();
        }
        catch (Exception Error)
        {
            Logger.WriteEventLog(false, Error);
        }
        return PublicKey;
    }

    void smtpClient_SendHtmlCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {

        MailMessage mail = e.UserState as MailMessage;

        if (!e.Cancelled && e.Error != null)
        {
            mail.Dispose();
        }
    }
}







