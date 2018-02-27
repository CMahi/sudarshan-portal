using System;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
//using log4net;
//using log4net.Config;

public static class Logger1
{
   // private static log4net.ILog Log { get; set; }
 //   private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
 //   private static readonly ILog log = LogManager.GetLogger("FileAppender");
  //  private static readonly ILog activityLog = LogManager.GetLogger("File1Appender");
  
    public static void Error(Exception ex)
    {
        /*
        //log4net.Config.BasicConfigurator.Configure();
        //ILog log = log4net.LogManager.GetLogger(typeof(Logger1));
        
        ILog log = LogManager.GetLogger("FileAppender");

        
       // log.Error(ex);
        log.Error("***--------ErrorLogger--------***");
        log.Error("Source		: " + ex.Source.ToString().Trim());
        log.Error("Method		: " + ex.TargetSite.Name.ToString());
        log.Error("Date : " + DateTime.Now.ToShortDateString() + "(MM/DD/YYYY)");
        log.Error("Time : " + DateTime.Now.ToLongTimeString());
        log.Error("Computer : " + Dns.GetHostName().ToString());
        log.Error("Error		: " + ex.Message.ToString().Trim());
        log.Error("Stack Trace	: " + ex.StackTrace.ToString().Trim());
        log.Error("***-----------------------------------------------------------------------------------------------------------------------***");
        Console.ReadLine();


        //StreamWriter log = new StreamWriter("E:\\Ranjit\\Sudarshan-Portal\\Logger\\ErrorLogger.log");

        //// log.Error(ex);
        //log.WriteLine("***--------ErrorLogger--------***");
        //log.WriteLine("Source		: " + ex.Source.ToString().Trim());
        //log.WriteLine("Method		: " + ex.TargetSite.Name.ToString());
        //log.WriteLine("Date : " + DateTime.Now.ToShortDateString() + "(MM/DD/YYYY)");
        //log.WriteLine("Time : " + DateTime.Now.ToLongTimeString());
        //log.WriteLine("Computer : " + Dns.GetHostName().ToString());
        //log.WriteLine("Error		: " + ex.Message.ToString().Trim());
        //log.WriteLine("Stack Trace	: " + ex.StackTrace.ToString().Trim());
        //log.WriteLine("***-----------------------------------------------------------------------------------------------------------------------***");
        //// Console.ReadLine();
        //log.Close();
       */
    }
    public static void Activity(object Method)
    {
        /*
        ILog activityLog = LogManager.GetLogger("File1Appender");
        //log4net.Config.BasicConfigurator.Configure();
        //ILog log = log4net.LogManager.GetLogger(typeof(Logger));
        activityLog.Info("***--------ActivityLogger--------***");
        activityLog.Info("Date: " + DateTime.Now.ToShortDateString() + "(MM/DD/YYYY)");
        activityLog.Info("Time: " + DateTime.Now.ToLongTimeString());
        activityLog.Info("Computer: " + Dns.GetHostName().ToString());     
      //  activityLog.Info("Dbprovider: " + AuditInfo["dbprovider-name"].ToString());
       // activityLog.Info("Datasource: " + AuditInfo["datasource-name"].ToString());
      //  activityLog.Info("Command: " + AuditInfo["command-name"].ToString());
     //   activityLog.Info("Output-Type: " + AuditInfo["output-type"].ToString());
        activityLog.Info("Method : " + Method);
        activityLog.Info("***-----------------------------------------------------------------------------------------------------------------------***");
       
        Console.ReadLine();
        */
    }

}