using AspnetCoreMvcFull.Utilities;
using AspnetCoreMvcFull.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AspnetCoreMvcFull.Models.Logs
{
    public class CreateLogFiles
    {
        private string sLogFormat;
        private string sErrorTime;
        private static String ErrorlineNo, Errormsg, extype, exurl, hostIp, ErrorLocation, HostAdd;
        public static string AUDIT_TRAILS_FOLDER_LOC = "~/ServerLogs/LogsDetailsFiles/"; 

        public CreateLogFiles()
        {
            DateTime now_date = AspnetCoreMvcFull.Utils.Utilities.GetLocalDateTimeHre();
            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
          //  sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
            sLogFormat = now_date.ToShortDateString().ToString() + " " + now_date.ToLongTimeString().ToString() + " ==> ";
            
            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string sYear = now_date.Year.ToString(); ;// DateTime.Now.Year.ToString();
            string sMonth = now_date.Month.ToString(); ;//DateTime.Now.Month.ToString();
            string sDay = now_date.Day.ToString(); ;//DateTime.Now.Day.ToString();
            sErrorTime = sYear + sMonth + sDay;
        }

        public void ErrorLog(string sPathName, string sErrMsg)
        {
            StreamWriter sw = new StreamWriter(sPathName + sErrorTime, true);
            sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }

        public static void SaveLog(string exc)
        {
            try
            {
                FileStream objFS = null;
       // string strFilePath = System.Web.Hosting.HostingEnvironment.MapPath("/Model/Logs/ErrorLog/") + "Exceptions.log";

        var webRootPath = MainHelper._webHostEnvironment.WebRootPath;
        string strFilePath = Path.Combine(webRootPath, "/Model/Logs/ErrorLog/" + "Exceptions.log");
        if (!File.Exists(strFilePath))
                {
                    objFS = new FileStream(strFilePath, FileMode.Create);
                }
                else
                    objFS = new FileStream(strFilePath, FileMode.Append);

                using (StreamWriter Sr = new StreamWriter(objFS))
                {
                    Sr.WriteLine(System.DateTime.Now.ToShortTimeString() + "---" + exc);
                }

            }
            catch (Exception ex)
            {
                SendErrorExceptionToText(ex);
              //  CreateLogFiles Err = new CreateLogFiles();
              //  Err.ErrorLog(System.Web.HttpContext.Current.Server.MapPath("Logs/ErrorLog"), ex.Message);
                Console.WriteLine(ex.Message);
                //return ex.Message;
            }
        }

        public static void SendErrorExceptionToText(Exception ex)
        {
            var line = Environment.NewLine + Environment.NewLine;

            ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            Errormsg = ex.GetType().Name.ToString();
            extype = ex.GetType().ToString();
      exurl = MainHelper._httpContext.Request.GetEncodedUrl();// System.Web.HttpContext.Current.Request.Url.ToString();
            ErrorLocation = ex.Message.ToString();
            DateTime now_date = AspnetCoreMvcFull.Utils.Utilities.GetLocalDateTimeHre();

            try
            {
              //  string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ExceptionDetailsFile/");  //Text File Path

        var webRootPath = MainHelper._webHostEnvironment.WebRootPath;
        string filepath = Path.Combine(webRootPath, "~/ExceptionDetailsFile/");
        if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + now_date.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line + "User Host IP:" + " " + hostIp + line;
                    sw.WriteLine("-----------Exception Details on " + " " + now_date.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

        //Schools Log In logs
        public static void saveAccountLoggingInReports(string schoolName, string username)
        {
            var line = Environment.NewLine + Environment.NewLine;

            string exurl = MainHelper._httpContext.Request.GetEncodedUrl();//System.Web.HttpContext.Current.Request.Url.ToString();
            string ip = GetIP();
            DateTime now_date = AspnetCoreMvcFull.Utils.Utilities.GetLocalDateTimeHre();

            try
            {
        // string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ServerLogs/SchoolsLogsDetailsFiles/");  //Text File Path

        var webRootPath = MainHelper._webHostEnvironment.WebRootPath;
        string filepath = Path.Combine(webRootPath, "~/ServerLogs/SchoolsLogsDetailsFiles/");
        if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                string alphanumeric = schoolName.ToAlphaNumericOnly().ToUpper();

                //+ "_" + DateTime.Today.ToString("dd-MM-yy")
                filepath = filepath + alphanumeric  + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line +
                        "School Name :" + " " + schoolName + line +
                        "Using User name :" + " " + username + line +
                        " Error Page Url:" + " " + exurl + line + 
                        "User Host IP:" + " " + ip + line;
                    sw.WriteLine("-----------School Login Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

        //Payouts Log In logs
        public static void savePsAccountLoggingInReports(string schoolName, string username)
        {
            var line = Environment.NewLine + Environment.NewLine;

            string exurl = MainHelper._httpContext.Request.GetEncodedUrl();//System.Web.HttpContext.Current.Request.Url.ToString();
      string ip = GetIP();

            try
            {
            //    string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ServerLogs/PayoutsLogsDetailsFiles/");  //Text File Path

        var webRootPath = MainHelper._webHostEnvironment.WebRootPath;
        string filepath = Path.Combine(webRootPath, "~/ServerLogs/PayoutsLogsDetailsFiles/");
        if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                string alphanumeric = schoolName.ToAlphaNumericOnly().ToUpper();

                //+ "_" + DateTime.Today.ToString("dd-MM-yy")
                filepath = filepath + alphanumeric + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line +
                        "Full Name :" + " " + schoolName + line +
                        "UserName :" + " " + username + line +
                        " Page Url:" + " " + exurl + line +
                        "User Host IP:" + " " + ip + line;
                    sw.WriteLine("----------- Payouts Payments Login Details on " + " " + DateTime.Now.ToString() + "-----------------");
                  //  sw.WriteLine("-------------------------------------------------------------------------------------");
                   // sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                  //  sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }
      
        public static String GetIP()
        {
            String ip = MainHelper._httpContext.Connection.RemoteIpAddress.ToString();//HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
        ip = "NA";// HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return ip;
        }

        //NOTE ME EMAIL REQUESTS logs
        public static void saveNotMeRequestsReports(string requestData)
        {
            var line = Environment.NewLine + Environment.NewLine;
            DateTime now_date = AspnetCoreMvcFull.Utils.Utilities.GetLocalDateTimeHre();

            try
            {
             //   string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ServerLogs/SecureAlertLogFiles/");  //Text File Path

        var webRootPath = MainHelper._webHostEnvironment.WebRootPath;
        string filepath = Path.Combine(webRootPath, "~/ServerLogs/SecureAlertLogFiles/");
        if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                string alphanumeric = "NoteMeRequestsDetials";
                filepath = filepath + alphanumeric + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string log = "Log Written Date:" + " " + now_date.ToString() + line +
                        "REQUEST DATA :" + " " + requestData + line;
                    sw.WriteLine("-----------Request Time on " + " " + now_date.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(log);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }
        public static void fxsaveNotMeRequestsReports(string requestData)
        {
            var line = Environment.NewLine + Environment.NewLine;
            DateTime now_date = AspnetCoreMvcFull.Utils.Utilities.GetLocalDateTimeHre();

            try
            {
              //  string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ServerLogs/SecureAlertLogFiles/");  //Text File Path

        var webRootPath = MainHelper._webHostEnvironment.WebRootPath;
        string filepath = Path.Combine(webRootPath, "~/ServerLogs/SecureAlertLogFiles/");
        if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                string alphanumeric = "FxNoteMeRequestsDetials";
                filepath = filepath + alphanumeric + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string log = "Log Written Date:" + " " + now_date.ToString() + line +
                        "REQUEST DATA :" + " " + requestData + line;
                    sw.WriteLine("-----------Request Time on " + " " + now_date.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(log);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }


        //IVERI WALLET TOP UP REQUESTS logs
        public static void saveIveriRequestsReports(string requestData)
        {
            var line = Environment.NewLine + Environment.NewLine;
            DateTime now_date = AspnetCoreMvcFull.Utils.Utilities.GetLocalDateTimeHre();

            try
            {
             //   string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ServerLogs/SecureAlertLogFiles/");  //Text File Path

        var webRootPath = MainHelper._webHostEnvironment.WebRootPath;
        string filepath = Path.Combine(webRootPath, "~/ServerLogs/SecureAlertLogFiles/");
        if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                string alphanumeric = "IveriRequestsDetails";
                filepath = filepath + alphanumeric + ".txt";   //+ "_" + DateTime.Today.ToString("dd-MM-yy")
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string log = "Log Written Date:" + " " + now_date.ToString() + line +
                        "REQUEST DATA :" + " " + requestData + line;
                    sw.WriteLine("-----------Request Time on  " + " " + now_date.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(log);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

        //GENERAL REQUESTS logs
        public static void generalRequestsLogs(string requestData)
        {
            var line = Environment.NewLine + Environment.NewLine;
            DateTime now_date = AspnetCoreMvcFull.Utils.Utilities.GetLocalDateTimeHre();

            try
            {
            //    string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ServerLogs/SecureAlertLogFiles/");  //Text File Path

        var webRootPath = MainHelper._webHostEnvironment.WebRootPath;
        string filepath = Path.Combine(webRootPath, "~/ServerLogs/SecureAlertLogFiles/");
        if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                string alphanumeric = "ErrorLogsRequestsDetails";
                filepath = filepath + alphanumeric + ".txt";   //+ "_" + DateTime.Today.ToString("dd-MM-yy")
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string log = "Log Written Date:" + " " + now_date.ToString() + line +
                        "REQUEST DATA :" + " " + requestData + line;
                    sw.WriteLine("-----------Request Time on  " + " " + now_date.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(log);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

        //DOWNLOADS REQUESTS logs
        public static void downloadsRequestsLogs(string requestData)
        {
            var line = Environment.NewLine + Environment.NewLine;
            DateTime now_date = AspnetCoreMvcFull.Utils.Utilities.GetLocalDateTimeHre();

            try
            {
            //    string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ServerLogs/SecureAlertLogFiles/");  //Text File Path

        var webRootPath = MainHelper._webHostEnvironment.WebRootPath;
        string filepath = Path.Combine(webRootPath, "~/ServerLogs/SecureAlertLogFiles/");
        if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                string alphanumeric = "DownloadsLogsRequestsDetails";
                filepath = filepath + alphanumeric + ".txt";   //+ "_" + DateTime.Today.ToString("dd-MM-yy")
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string log = "Log Written Date:" + " " + now_date.ToString() + line +
                        "REQUEST DATA :" + " " + requestData + line;
                    sw.WriteLine("-----------Request Time on  " + " " + now_date.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(log);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

        //Fx Log In logs
        public static void saveAdminAccountLoggingInReports(string schoolName, string username)
        {
            var line = Environment.NewLine + Environment.NewLine;

      string exurl = MainHelper._httpContext.Request.GetEncodedUrl();// System.Web.HttpContext.Current.Request.Url.ToString();
            string ip = GetIP();
            DateTime now_date = AspnetCoreMvcFull.Utils.Utilities.GetLocalDateTimeHre();

            try
            {
              //  string filepath = System.Web.HttpContext.Current.Server.MapPath(AUDIT_TRAILS_FOLDER_LOC);  //Text File Path

        var webRootPath = MainHelper._webHostEnvironment.WebRootPath;
        string filepath = Path.Combine(webRootPath, AUDIT_TRAILS_FOLDER_LOC);
        if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                string alphanumeric = schoolName.ToAlphaNumericOnly().ToUpper();

                //+ "_" + DateTime.Today.ToString("dd-MM-yy")
                filepath = filepath + alphanumeric + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + now_date.ToString() + line +
                        "Full Name :" + " " + schoolName + line +
                        "UserName :" + " " + username + line +
                        " Page Url:" + " " + exurl + line +
                        "User Host IP:" + " " + ip + line;
                    sw.WriteLine("----------- FX ADMIN Login Details on " + " " + now_date.ToString() + "-----------------");
                    //  sw.WriteLine("-------------------------------------------------------------------------------------");
                    // sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    //  sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

        //Fx Agent logs
        public static void saveFxTrails(string fileName, string data)
        { 
            var line = Environment.NewLine + Environment.NewLine;

            string exurl = MainHelper._httpContext.Request.GetEncodedUrl();//System.Web.HttpContext.Current.Request.Url.ToString();
      string ip = GetIP(); 
            DateTime now_date = AspnetCoreMvcFull.Utils.Utilities.GetLocalDateTimeHre();

            try
            {

        var webRootPath = MainHelper._webHostEnvironment.WebRootPath;
        string filepath = Path.Combine(webRootPath, AUDIT_TRAILS_FOLDER_LOC);
        //string filepath = System.Web.HttpContext.Current.Server.MapPath(AUDIT_TRAILS_FOLDER_LOC);  //Text File Path
                saveFxTrails(fileName, data, exurl, ip, filepath);
            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

        public static void saveFxTrails(string fileName, string data, string exurl, string ip, string filepath)
        {
            try
            {
                CreateLogFiles.saveFxTrails(0, fileName, data, exurl, ip, filepath);
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public static void saveFxTrails(int req, string fileName, string data, string exurl, string ip, string filepath)
        {
            var line = Environment.NewLine + Environment.NewLine;

            DateTime now_date = AspnetCoreMvcFull.Utils.Utilities.GetLocalDateTimeHre();

            try
            {

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }

                //  string alphanumeric = "FxAgentAccCreation";// schoolName.ToAlphaNumericOnly().ToUpper();

                filepath = filepath + fileName + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string logDetails = "Log Written Date:" + " " + now_date.ToString() + line +
                        "Data :" + " " + data + line +
                        " Page Url:" + " " + exurl + line +
                        "User Host IP:" + " " + ip + line;
                    if (req == 0 || req == 1) { 
                    sw.WriteLine("----------- FX Details on " + " " + now_date.ToString() + "-----------------");
                    }
                    //  sw.WriteLine("-------------------------------------------------------------------------------------");
                    // sw.WriteLine(line);
                    sw.WriteLine(logDetails);
                    if (req == 0 || req == 2) {
                    sw.WriteLine("--------------------------------*End*------------------------------------------"); 
                    }
                    //  sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }
      


    }
}
