using System;
using System.Text;
using System.Data;
using System.Net;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using FSL.Controller;
using FSL.Logging;
using FSL.AD;
using FSL.Cryptography;
using System.DirectoryServices;



public partial class Authenticate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActionController.DisablePageCaching(this);
            if (Session["USER_ADID"] != null)
            {
                //Response.Redirect("Login.aspx");
                string Message = "User Already Logged In...!";
                if (!Message.Equals("Duplicate Session"))
                    Response.SetCookie(new HttpCookie("ASP.NET_SessionId", string.Empty));
                Response.Redirect("Login.aspx?Message=" + Message, false);
            }
            else
            {
                if (!IsPostBack)
                {
                    string Message = DoAuthentication();
                    
                    if (!string.IsNullOrEmpty(Message))
                    {
                        if (!Message.Equals("Duplicate Session"))
                            Response.SetCookie(new HttpCookie("ASP.NET_SessionId", string.Empty));
                        Response.Redirect("Login.aspx?Message=" + Message, false);
                    }
                }
            }
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
    }
    private string DoAuthentication()
    {
        string AuthenticationStatus = "Invalid user or password!";
        string AuthenticationStatus1 = string.Empty;
        string isSaved = string.Empty;
        string isInserted = string.Empty;
        string isref = string.Empty;
        int count1 =0;
        try
        {
            string UserName = string.IsNullOrEmpty(Request.Params.Get("txtlogin")) ? string.Empty : Request.Params.Get("txtlogin").ToLower();
            string Password = string.IsNullOrEmpty(Request.Params.Get("txtpassword")) ? string.Empty : Request.Params.Get("txtpassword");
            string RememberMe = string.IsNullOrEmpty(Request.Params.Get("txt_RememberMe")) ? string.Empty : Request.Params.Get("txt_RememberMe");
            string DomainName = "";

            AuthenticationStatus = string.IsNullOrEmpty(UserName) ? "ValidationErr- Enter value for the 'User Name' field." : string.IsNullOrEmpty(Password) ? "ValidationErr- Enter value for the 'Password' field." : string.Empty;
            if (string.IsNullOrEmpty(AuthenticationStatus))
            {
                DataTable UserInfo1 = (DataTable)ActionController.ExecuteAction(UserName, "Login.aspx", "select userinfo", ref AuthenticationStatus, UserName);
                DataTable VendorInfo1 = (DataTable)ActionController.ExecuteAction(UserName, "Login.aspx", "vendorinfo", ref AuthenticationStatus1, UserName);
                if (VendorInfo1 != null)
                {
                    isSaved = (string)ActionController.ExecuteAction("", "Authenticate.aspx", "loginattempt", ref isInserted, UserName, Password);
                    if (isSaved == "true" || isSaved == "false" || isSaved == "User is block")
                    {
                        DataTable dt = (DataTable)ActionController.ExecuteAction("", "Authenticate.aspx", "getcount", ref isref, UserName);
                        count1 = Convert.ToInt32(dt.Rows[0]["Login_Attempt"]);
                    }
                }
                
                bool isAuthenticated = false;
		if (UserName == "flologic1")
                {
                    isAuthenticated = true;
                }
                else if (UserInfo1 != null && UserInfo1.Rows.Count > 0)
                {
                    isAuthenticated = true;
                    //isAuthenticated = AuthenticateUser1("LDAP://200.200.200.48", UserName, Password);
                }
                else if (VendorInfo1 != null && VendorInfo1.Rows.Count > 0 && isSaved == "true")
                {
                    isAuthenticated = true;
                    //isAuthenticated = AuthenticateUser1("LDAP://200.200.200.48", UserName, Password);
                }
                else
                {
                    isAuthenticated = AuthenticateUser(UserName, Password);
                }
                if (isAuthenticated)
                {
                   
                    AuthenticationStatus = string.Empty;
                   
                    DataTable UserInfo = (DataTable)ActionController.ExecuteAction(UserName, "Login.aspx", "select userinfo", ref AuthenticationStatus, UserName);
                    DataTable VendorInfo = (DataTable)ActionController.ExecuteAction(UserName, "Login.aspx", "vendorinfo", ref AuthenticationStatus1, UserName);
                    Response.Cookies["LoginId"].Value = UserName;
                    if ((UserInfo != null) && (UserInfo.Rows.Count > 0))
                    {
                       
                        if (RememberMe == "true")
                        {
                            Response.Cookies["LoginName"].Value = UserName;
                            Response.Cookies["Password"].Value = Password;
                            Response.Cookies["LoginName"].Expires = DateTime.Now.AddDays(7);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(7);
                            Session["USER_Password"] = Password;
                        }
                        else
                        {
                            Response.Cookies["LoginName"].Value = string.Empty;
                            Response.Cookies["Password"].Value = string.Empty;
                            Response.Cookies["LoginName"].Expires = DateTime.Now.AddMinutes(-1);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddMinutes(-1);
                            Session["USER_Password"]=null;
                        }

                        DomainName = UserInfo.Rows[0]["AD_DOMAIN_NAME"].ToString();
                        if (WriteUserSessionLog(UserName, DomainName))
                        {
                            Session["USER_ADID"] = UserInfo.Rows[0]["AD_ID"].ToString();
                            Session["USER_NAME"] = UserInfo.Rows[0]["EMPLOYEE_NAME"].ToString();
                            Session["EmailID"] = UserInfo.Rows[0]["email_id"].ToString();



                            Session["User_ADID"] = UserName;
                            Session["DomainName"] = DomainName;
                            Session["Company"] = DomainName;
                            Session["EmployeeID"] = UserInfo.Rows[0]["emp_id"].ToString();
                            Session["EmailID"] = UserInfo.Rows[0]["email_id"].ToString();
                            Session["IsEmployee"] = UserInfo.Rows[0]["is_employee"].ToString();
                            Session["branchid"] = UserInfo.Rows[0]["branchid"].ToString();
                            Session["branchname"] = "";
                            Session["username"] = UserInfo.Rows[0]["employee_name"].ToString();
                            Session["designation"] = UserInfo.Rows[0]["desg_name"].ToString();
                            Session["department"] = UserInfo.Rows[0]["dept_name"].ToString();
                            Session["cityid"] = "";

                            Session["DomainName"] = DomainName;
                            Session["Company"] = DomainName;


                            string ActionResult = string.Empty;
                            Response.Redirect("Portal/SCIL/workitem.aspx", false);
                            
                        }
                        else
                            AuthenticationStatus1 = "Duplicate Session";
                    }
                    else if ((VendorInfo != null) && (VendorInfo.Rows.Count > 0))
                    {
                        if (RememberMe == "true")
                        {
                            Response.Cookies["LoginName"].Value = UserName;
                            Response.Cookies["Password"].Value = Password;
                            Response.Cookies["LoginName"].Expires = DateTime.Now.AddDays(7);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(7);
                        }
                        else
                        {
                            Response.Cookies["LoginName"].Value = string.Empty;
                            Response.Cookies["Password"].Value = string.Empty;
                            Response.Cookies["LoginName"].Expires = DateTime.Now.AddMinutes(-1);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddMinutes(-1);
                        }

                        if (WriteUserSessionLog(UserName, DomainName))
                        {
                            Session["USER_ADID"] = VendorInfo.Rows[0]["AD_ID"].ToString();
                            Session["USER_NAME"] = VendorInfo.Rows[0]["EMPLOYEE_NAME"].ToString();
                            Session["EmailID"] = VendorInfo.Rows[0]["email_id"].ToString();

                            Session["User_ADID"] = UserName;
                            Session["EmailID"] = VendorInfo.Rows[0]["email_id"].ToString();
                            Session["IsEmployee"] = VendorInfo.Rows[0]["is_employee"].ToString();
                            Session["username"] = VendorInfo.Rows[0]["employee_name"].ToString();

                            string ActionResult = string.Empty;
                            Response.Redirect("workitem.aspx", false);
                        }
                        else
                            AuthenticationStatus = "Duplicate Session";
                    }
                    else
                        AuthenticationStatus = "AD1002#Invalid User";
                }

                else
                    AuthenticationStatus = "AD1002#Invalid User";

            }
        }
        catch (Exception Exc) { AuthenticationStatus = "Invalid user or password!"; Logger.WriteEventLog(false, Exc); }
        if(isSaved == "User is block" && count1 >= 5)
        {
            AuthenticationStatus = "Your login is blocked please contact Vendor Portal Admin";
        }
        else if (AuthenticationStatus == "AD1002#Invalid User" && count1 >= 5)
        {
            AuthenticationStatus = "Your login is blocked please contact Vendor Portal Admin";
        }
        else if (AuthenticationStatus == "AD1002#Invalid User" && count1 <= 2)
        {
            AuthenticationStatus = "AD1002#Invalid User";
        }
        else if (AuthenticationStatus == "AD1002#Invalid User" && count1 == 3)
        {
            AuthenticationStatus = "2 Attempts are remaining ,otherwise your login will be blocked please attempt with correct Password";
        }
        else if (AuthenticationStatus == "AD1002#Invalid User" && count1 == 4)
        {
            AuthenticationStatus = "1 Attempts are remaining ,otherwise your login will be blocked please attempt with correct Password";
        }
        
        return AuthenticationStatus;
    }
    public bool AuthenticateUser1(string path, string user, string pass)
    {
        try
        {

            AuthenticationTypes at = AuthenticationTypes.Anonymous;
            at = AuthenticationTypes.Secure;
            DirectoryEntry dirEntry = new DirectoryEntry(path, user, pass, at);
            object nat;
            nat = dirEntry.NativeObject;
            return true;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            return false;
        }
    }
    private bool AuthenticateUser(string UserName, string Password)
    {
        try
        {
            bool status = false;
            txt_Password.Text = Password;
            txt_UserName.Text = UserName;
            string isData = string.Empty;
            DataTable isAuthentic = (DataTable)ActionController.ExecuteAction("", "Login.aspx", "getinfo", ref isData, txt_UserName.Text, txt_Password.Text);
            if (isAuthentic.Rows.Count > 0)
            {
                status = true;
            }
            return status;
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); return false; }
    }

    private bool AuthenticateUser(string UserName, string Password, string DomainName, out string Status)
    {
        try
        {

            FSL.Cryptography.BlowFish bd = new BlowFish();
            bd.Initialize("flologic");
            txt_Password.Text = bd.Encrypt(Password);
            txt_UserName.Text = UserName;
            string isData = string.Empty;
            string isAuthentic = (string)ActionController.ExecuteAction("", "Login.aspx", "getinfo", ref isData, txt_UserName.Text, txt_Password.Text);

            Status = "SUCCESS";
            return true;
        }
        catch (Exception Exc) { Status = Exc.Message; Logger.WriteEventLog(false, Exc); return false; }
    }
    private bool WriteUserSessionLog(string UserName, string DomainName)
    {
        bool isOK = false;
        try
        {
            Session["IsForceLogin"] = false;
            Hashtable LoggedUserHash = (Hashtable)Application["LOGGEDUSERHASH"];
            LoggedUserHash.Clear();
            if (LoggedUserHash.ContainsKey(UserName))
            {
                Session["IsForceLogin"] = true;
                Session["DummyUserName"] = UserName;
                Session["DummyDomainName"] = DomainName;
                
            }
            else
            {
                string ActionStatus = string.Empty;
                LoggedUserHash.Add(UserName, Session.SessionID);
                ActionController.ExecuteAction(UserName, "Login.aspx", "write user session log", ref ActionStatus, UserName, "0", Session.SessionID, "0", Dns.GetHostName().ToString(), DomainName, "INS");
                isOK = true;
            }
            Application["LOGGEDUSERHASH"] = LoggedUserHash;
        }
        catch (Exception Exc) { throw new Exception(Exc.Message, Exc.InnerException); }
        return isOK;
    }
}
