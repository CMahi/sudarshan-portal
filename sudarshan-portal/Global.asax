<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="FSL.Controller" %>  

<script RunAt="server">
    
    private static Hashtable    LoggedUserHash;    
    void Application_Start(object sender, EventArgs e)
    {
        string ActionStatus=string.Empty;
        LoggedUserHash = Hashtable.Synchronized(new Hashtable());
        Application["SERVERPATH"] = "Sudarshan-Portal-NEW";
        Application["LOGGEDUSERHASH"] = LoggedUserHash;
        Application["IPROCESSHASH"] = ActionController.GetiProcessConfig(new Page());
        Application["DOMAINHASH"] = ActionController.GetDomains(new Page());
        Application["INBOX"] =(DataTable)ActionController.ExecuteAction("KotakBPM Application","Global.asax", "select inbox",ref ActionStatus);
        Application["PANEL"] = (DataTable)ActionController.ExecuteAction("KotakBPM Application", "Global.asax", "select panels", ref ActionStatus);
        //ssoDOTNET.StartUp();    
    }
    void Application_End(object sender, EventArgs e)
    {
        //ssoDOTNET.ShutDown();
        Application.Remove("SERVERPATH");
        Application.Remove("LOGGEDUSERHASH");
        Application.Remove("IPROCESSHASH");
        Application.Remove("DOMAINHASH");
        Application.Remove("INBOX");    
        Application.Remove("PANEL");
        LoggedUserHash = null;
    }

    void Application_Error(object sender, EventArgs e)
    {
        
    }
    void Session_Start(object sender, EventArgs e)
    {
        Session["LOGGEDUSERHASH"]   = (Hashtable)Application["LOGGEDUSERHASH"];
        Session["FilterExpression"] = string.Empty;
        Session["PageSize"]         = 10;
        Session["SearchPageSize"]   = 10;
    }
    void Session_End(object sender, EventArgs e)
    {
        
        try
        {
            string ActionStatus = string.Empty;
            string UserName   = (string)Session["User_ADID"];
            string DomainName = (string)Session["DomainName"];
            if (UserName != null)
            {
                Session["User_ADID"]="amol";
                //sSession UsrSession = (sSession)Session["UserIPSession"];
                //if (UsrSession != null)
                //{
                //    UsrSession.Disconnect(true);
                //    UsrSession.Dispose();
                //} 
                Hashtable LoggedUserHash = (Hashtable)Application["LOGGEDUSERHASH"];
                if ((LoggedUserHash.ContainsKey(UserName)) && (LoggedUserHash[UserName].ToString().Equals(Session.SessionID)))
                {
                    ((Hashtable)Application["LOGGEDUSERHASH"]).Remove(UserName);
                    ActionController.ExecuteAction(UserName, "Global.asax", "write user session log", ref ActionStatus, UserName, "0", Session.SessionID, "0", Dns.GetHostName().ToString(), DomainName, "UPD");
                }
            }
        }
        catch (Exception) { }
    }
           
</script>

