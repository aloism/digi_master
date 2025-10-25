using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Services;
using Microsoft.VisualBasic;
using System.Collections;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspnetCoreMvcFull.Utils
{
  public class Utilities
  {
    //TODO add to web.config
    static readonly string PasswordHash = "H@hsdr0w@@P";
    static readonly string SaltKey = "Y3k@&Tl@S";
    static readonly string VIKey = "@9A3cQThwdzlkTE9";
    public static readonly string QueriesKey = "@9A3cQTh";

    public static string GetRandomNumber(int length)
    {
      // Log::info("getRandomNumber");
      return WebDataService.GenerateRandomString(length);
    }
    public static DateTime GetDayJoinedDateTime(DateTime dateTime)
    {
      var formatted = dateTime.ToString("yyyy-MM-dd") + " 00:00:00";
      return DateTime.ParseExact(formatted, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }
    public static string GetDayJoinedDateTimeStr(DateTime dateTime)
    {
      return dateTime.ToString("yyyy-MM-dd") + " 00:00:00";
    }
    public static DateTime GetYesterdayDateTime()
    {
      var yesterday = DateTime.UtcNow.AddDays(-1);
      var formatted = yesterday.ToString("yyyy-MM-dd") + " 00:00:00";
      return DateTime.ParseExact(formatted, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }
    public static string GetMonthJoinedDateTimeStr(DateTime dateTime)
    {
      int year = dateTime.Year;
      int month = dateTime.Month;
      int lastDay = DateTime.DaysInMonth(year, month);

      return new DateTime(year, month, lastDay, 0, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
    }
    public static DateTime GetMonthJoinedDateTime(DateTime dateTime)
    {
      int year = dateTime.Year;
      int month = dateTime.Month;
      int lastDay = DateTime.DaysInMonth(year, month);

      return new DateTime(year, month, lastDay, 0, 0, 0);
    }
    public static DateTime GetLastMonthJoinedDateTime(DateTime dateTime)
    {
      int year = dateTime.Year;
      int month = dateTime.Month;

      if (month > 1)
      {
        month -= 1;
      }
      else
      {
        month = 12;
        year -= 1;
      }

      int lastDay = DateTime.DaysInMonth(year, month);
      return new DateTime(year, month, lastDay, 0, 0, 0);
    }
    public static string getSubscriptionTypeDescription(int subType)
    {
      string subscriptionType = "Search Fee";
      if (subType == 1)
      {
        subscriptionType = "PROMO FEE";
      }
      else if (subType == 2)
      {
        subscriptionType = "SEARCH FEE";
      }
      else if (subType == 3)
      {
        subscriptionType = "SUBS FEE";
      }
      else if (subType == 4)
      {
        subscriptionType = "FINDER FEE";
      }
      return subscriptionType;
    }

    public static string getItemDescription(string itemType)
    {
      if (itemType.Equals("0"))
      {
        return "NATIONAL ID";
      }
      else
         if (itemType.Equals("1"))
      {
        return "PASSPORT DOCUMENT";
      }
      if (itemType.Equals("2"))
      {
        return "DRIVER'S LICENCE";
      }
      else if (itemType.Equals("3"))
      {
        return "MOBILE PHONE";
      }
      else if (itemType.Equals("4"))
      {
        return "ACADEMIC CERTIFICATES";
      }
      else if (itemType.Equals("5"))
      {
        return "LAPTOP";
      }
      else if (itemType.Equals("6"))
      {
        return "TABLET";
      }
      else if (itemType.Equals("12"))
      {
        return "LIVESTOCK";
      }
      else if (itemType.Equals("7"))
      {
        return "CAR/MOTORCYCLE";
      }
      else if (itemType.Equals("8"))
      {
        return "WEARABLES";
      }
      else if (itemType.Equals("9"))
      {
        return "KEYS";
      }
      else if (itemType.Equals("13"))
      {
        return "PETS";
      }
      else if (itemType.Equals("10"))
      {
        return "LUGGAGEWARE";
      }
      else if (itemType.Equals("11"))
      {
        return "HOUSE HOLD ITEMS";
      }
      else if (itemType.Equals("14"))
      {
        return "BANKING/DIGITAL CARDS";
      }
      else if (itemType.Equals("15"))
      {
        return "OTHER";
      }
      return "-1";
    }

    public static string getItemType(string itemDescription)
    {
      if (itemDescription.Equals("NATIONAL ID"))
      {
        return "0";
      }
      else
         if (itemDescription.Equals("PASSPORT DOCUMENT"))
      {
        return "1";
      }
      if (itemDescription.Equals("ACADEMIC CERTIFICATES"))
      {
        return "4";
      }
      else if (itemDescription.Equals("MOBILE PHONE"))
      {
        return "3";
      }
      else if (itemDescription.Equals("LAPTOP"))
      {
        return "5";
      }
      else if (itemDescription.Equals("CAR/MOTORCYCLE"))
      {
        return "7";
      }
      else if (itemDescription.Equals("TABLET"))
      {
        return "6";
      }
      else if (itemDescription.Equals("LIVESTOCK"))
      {
        return "12";
      }
      else if (itemDescription.Equals("DRIVER'S LICENCE"))
      {
        return "2";
      }
      else if (itemDescription.Equals("WEARABLES"))
      {
        return "8";
      }
      else if (itemDescription.Equals("KEYS"))
      {
        return "9";
      }
      else if (itemDescription.Equals("PETS"))
      {
        return "13";
      }
      else if (itemDescription.Equals("LUGGAGEWARE"))
      {
        return "10";
      }
      else if (itemDescription.Equals("HOUSE HOLD ITEMS"))
      {
        return "11";
      }
      else if (itemDescription.Equals("BANKING/DIGITAL CARDS"))
      {
        return "14";
      }
      else if (itemDescription.Equals("OTHER"))
      {
        return "15";
      }
      return "-1";
    }

    public static DateTime GetLocalDateTime(DateTime utcDateTime)
    {

      utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
      TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
      DateTime time = TimeZoneInfo.ConvertTime(utcDateTime, timeZone);

      return time;

    }
    public static WeeklyDateTime currentWeekDates()
    {
      var weeklyDateTime = new WeeklyDateTime();
      DateTime dt = Utilities.GetLocalDateTimeHre();
      return weeklyDateRange(dt);
    }
    public static WeeklyDateTime weeklyDateRange(DateTime dt)
    {
      var weeklyDateTime = new WeeklyDateTime();

      // DateTime reg_date = DateTime.UtcNow;
      //   TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
      // reg_date = Utils.GetLocalDateTimeHre();
      //   DateTime now_date = Utils.GetLocalDateTimeHre();
      var day = (int)dt.DayOfWeek;
      var n = day;//_today.getDay();
      var tdyDy = dt.Day;

      var month = dt.Month;//_today.getMonth() + 1;//_sdt["monthInt"];
                           // var day = _today.getUTCDate();//_sdt["dayInt"];
      var year = dt.Year;//_today.getFullYear();//_sdt["year"];
      Console.Write(year + "-" + month + "-" + day);

      var sdt = "";//_sdt["year"] + "/" + sdt["monthInt"] + "/" + (sdt["dayInt"]);
      var edt = "";//_sdt["year"] + "/" + sdt["monthInt"] + "/" + (sdt["dayInt"]);
      var str_dt = dt;
      if (n == 1)
      {
        sdt = Utilities.getPastDate(str_dt, 6);//year + "/" + month + "/" + day-6;
        edt = month + "/" + tdyDy + "/" + year;//year + "-" + month + "-" + day;
      }
      else if (n == 2)
      {
        sdt = month + "/" + (tdyDy - 0) + "/" + year;//year + "-" + month + "-" + (day - 0);
        edt = Utilities.getAheardDate(str_dt, 6);//year + "/" + month + "/" + (day+6);

      }
      else if (n == 3)
      {
        sdt = Utilities.getPastDate(str_dt, 1);//year + "/" + month + "/" + (day-1);
        edt = Utilities.getAheardDate(str_dt, 5);//year + "/" + month + "/" + (day+5);

      }
      else if (n == 4)
      {
        sdt = Utilities.getPastDate(str_dt, 2);//year + "/" + month + "/" + (day-2);
        edt = Utilities.getAheardDate(str_dt, 4);//year + "/" + month + "/" + (day+4);

      }
      else if (n == 5)
      {
        sdt = Utilities.getPastDate(str_dt, 3);//year + "/" + month + "/" + (day-3);
        edt = Utilities.getAheardDate(str_dt, 3);//year + "/" + month + "/" + (day+3);

      }
      else if (n == 6)
      {
        sdt = Utilities.getPastDate(str_dt, 4);//year + "/" + month + "/" + (day-4);
        edt = Utilities.getAheardDate(str_dt, 2);//year + "/" + month + "/" + (day+2);

      }
      else if (n == 0)
      {
        // newdate = year + "/" + month + "/" + day;
        sdt = Utilities.getPastDate(str_dt, 5);//_year + "/" + month + "/" + day;
        edt = Utilities.getAheardDate(str_dt, 1);//year + "/" + month + "/" + (day+1);

      }

      var dyWeek = sdt + " to " + edt;
      string[] dtsplit = sdt.Split(new[] { "/" }, StringSplitOptions.None);
      sdt = dtsplit[1] + "/" + dtsplit[0] + "/" + dtsplit[2];//MM/dd/yyyy
      DateTime startweek_date = new DateTime(Convert.ToInt32(dtsplit[2]), Convert.ToInt32(dtsplit[0]), Convert.ToInt32(dtsplit[1]));

      dtsplit = edt.Split(new[] { "/" }, StringSplitOptions.None);
      edt = dtsplit[1] + "/" + dtsplit[0] + "/" + dtsplit[2];//MM/dd/yyyy
      DateTime endweek_date = new DateTime(Convert.ToInt32(dtsplit[2]), Convert.ToInt32(dtsplit[0]), Convert.ToInt32(dtsplit[1]));

      weeklyDateTime.startweek_date = startweek_date;
      weeklyDateTime.endweek_date = endweek_date;
      weeklyDateTime.startwk_date = sdt;
      weeklyDateTime.endwk_date = edt;
      return weeklyDateTime;
    }

    public static string getPastDate(DateTime str_dt, int days)
    {
      //var _dateObj = this.formatStrToDate(str_dt);//.subtract(Duration(days: days));

      //var yesterday = new Date(_dateObj);
      //yesterday.setDate(_dateObj.getDate() - days);
      // _dateObj.setDate(_dateObj.getDate() - days);

      // var month = dateObj.getUTCMonth() + 1; //months from 1-12
      // var day = dateObj.getUTCDate();
      // var year = dateObj.getUTCFullYear();

      // var dt = year + "/" + month + "/" + _day;
      //let latest_date =datepipe.transform(yesterday, 'yyyy-MM-dd');
      //return latest_date;//DateFormat('yyyy-MM-dd').format(_dateObj);
      DateTime yesterday = DateTime.Now.Date.AddDays(-1 * days);

      var dt = yesterday.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
      return dt;

    }
    public static string getAheardDate(DateTime str_dt, int days)
    {
      //var dateObj = this.formatStrToDate(str_dt);//.subtract(Duration(days: -days));
      // var dateObj = new Date(str_dt);
      // dateObj.setDate(dateObj.getDate() + days);

      //var yesterday = new Date(dateObj);
      //yesterday.setDate(dateObj.getDate() + days);

      // var _month = dateObj.getUTCMonth() + 1; //months from 1-12
      // var _day = dateObj.getUTCDate();
      // var _year = dateObj.getUTCFullYear();

      // var dt = year + "/" + month + "/" + _day;
      //let latest_date =datepipe.transform(yesterday, 'yyyy-MM-dd');
      //return latest_date;//DateFormat('yyyy-MM-dd').format(dateObj);
      DateTime tomorrow = DateTime.Now.Date.AddDays(days);

      var dt = tomorrow.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
      return dt;
    }
    public static bool IsValidEmail(string email)
    {
      try
      {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == email;
      }
      catch
      {
        return false;
      }
    }
    public static string getDefaultImgPerFile(string fileName)
    {

      if (string.IsNullOrEmpty(fileName))
      {
        return "image-placeholder.jpg";
      }
      if (fileName.Equals("--"))
      {
        return "image-placeholder.jpg";
      }
      string ext = getFileExt(fileName);//ext.ToLower();
      ext = ext.ToLower();
      if (ext.Contains("jpg") || ext.Contains("jpeg") || ext.Contains("png"))
      {
        // fileName =;
      }
      else if (ext.Contains("pdf"))
      {
        fileName = "placeholder-pdf.jpg";
      }
      else
      {
        fileName = "fileplaceholder.jpg";
      }
      return fileName;
    }
    public static string getDefaultImgPerFile(string fileName, string ext)
    {
      ext = ext.ToLower();
      if (ext.Contains("jpg") || ext.Contains("jpeg") || ext.Contains("png"))
      {
        // fileName =;
      }
      else if (ext.Contains("pdf"))
      {
        fileName = "placeholder-pdf.jpg";
      }
      else
      {
        fileName = "fileplaceholder.jpg";
      }
      return fileName;
    }

    public static string nameFileWithExt(string filePrefix, string fileName, string _phoneNumber, string ext)
    {
      ext = ext.ToLower();
      if (ext.Contains("jpg") || ext.Contains("jpeg") || ext.Contains("png"))
      {
        fileName = filePrefix + "_IMG_" + _phoneNumber + "." + ext;
      }
      else if (ext.Contains("pdf"))
      {
        fileName = filePrefix + "_PDF_" + _phoneNumber + "." + ext;
      }
      else
      {
        fileName = filePrefix + "_FL_" + _phoneNumber + "." + ext;
      }
      return fileName;
    }
    public static string getFileExt(string FileName)
    {
      string fn = System.IO.Path.GetFileName(FileName);

      //to specify the filename
      string filename = fn.ToLower().ToString();
      string[] exts = filename.Split('.');
      //Int16 n = Convert.ToInt16(exts);
      //  string name = exts[0].ToString();
      string ext = exts[1].ToString();
      return ext;
    }
    public static string getFileNameOnly(string FileName)
    {
      string fn = System.IO.Path.GetFileName(FileName);

      //to specify the filename
      string filename = fn.ToLower().ToString();
      string[] exts = filename.Split('.');
      //Int16 n = Convert.ToInt16(exts);
      string name = exts[0].ToString();
      //    string ext = exts[1].ToString();
      return name;
    }
    public static string getRandomAgentCode()
    {
      Random r = new Random();
      int randNum = r.Next(1000000);
      string sixDigitNumber = randNum.ToString("D6");
      return sixDigitNumber;
    }

    public static ResponseMessage returnError(string message)
    {
      ResponseMessage err = new ResponseMessage();

      err.ResponseCode = "500";
      err.ResponseDescription = message;

      return err;
    }
    public static ResponseMessage returnError()
    {
      ResponseMessage err = new ResponseMessage();

      err.ResponseCode = "500";
      err.ResponseDescription = "REQUEST FAILED, PLEASE TRY AGAIN";

      return err;
    }
    public static ResponseMessage returnUnknownError()
    {
      ResponseMessage err = new ResponseMessage();

      err.ResponseCode = "500";
      err.ResponseDescription = "SYSTEM ERROR";

      return err;
    }
    public static ResponseMessage returnUnKnownPlatformError()
    {
      ResponseMessage err = new ResponseMessage();

      err.ResponseCode = "500";
      err.ResponseDescription = "UNKOWN UNAUTHORISED PLATFORM";

      return err;
    }
    public static ResponseMessage returnSuccess()
    {
      ResponseMessage err = new ResponseMessage();

      err.ResponseCode = "000";
      err.ResponseDescription = "SUCCESSFUL";

      return err;
    }

    public static string getDeviceTypeName(string platform)
    {
      string getDeviceTypeName = "unkown";
      if (platform.Equals(Constants.ANDROID_APP))
      {
        getDeviceTypeName = Constants.ANDROID_STR;
      }
      else if (platform.Equals(Constants.IOS_APP))
      {
        getDeviceTypeName = Constants.IOS_STR;

      }
      else if (platform.Equals(Constants.WEB_APP))
      {
        getDeviceTypeName = Constants.WINDOWS_STR;

      }
      else if (platform.Equals(Constants.E_WALLET_WEB))
      {
        getDeviceTypeName = Constants.EWALLET_STR;

      }
      return getDeviceTypeName;
    }

    public static string optionalValuetoStrDecimal(decimal? payment_amount)
    {
      //  decimal amount = Convert.ToDecimal(payment_amount);
      decimal amount = Math.Round(payment_amount ?? 0, 2, MidpointRounding.AwayFromZero);
      return "$" + amount;
    }
    public static decimal optionalValuetoStrDecimaldc(decimal? payment_amount)
    {
      //  decimal amount = Convert.ToDecimal(payment_amount);
      decimal amount = Math.Round(payment_amount ?? 0, 2, MidpointRounding.AwayFromZero);
      return amount;
    }
    public static DateTime datetimeToUtcLocalTimeDateTime(DateTime? dateTime)
    {
      try
      {
        DateTime dt = dateTime ?? DateTime.Now;
        TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
        return TimeZoneInfo.ConvertTimeToUtc(dt);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return DateTime.Now;
      }
    }


    public static string datetimeToUtcLocalTime(DateTime? dateTime)
    {
      try
      {
        DateTime dt = dateTime ?? DateTime.Now;
        TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
        //   // Console.WriteLine("The date and time are {0} UTC.",
        //  //     TimeZoneInfo.ConvertTimeToUtc(dt));
        //    System.Diagnostics.Debug.WriteLine("The date and time are {0} UTC.1",
        //     dt);
        //    DateTime utcconvertedDate = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        //   // Console.WriteLine("The date and time are {0} UTC.2",
        //   //    utcconvertedDate);
        //    System.Diagnostics.Debug.WriteLine("The date and time are {0} UTC.2",
        //       utcconvertedDate);
        //    //DateTime timezoneconvertedDate = DateTime.SpecifyKind(dt, DateTimeKind.);
        //    //utc time to local time
        //    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
        //    DateTime harareTime = TimeZoneInfo.ConvertTimeFromUtc(utcconvertedDate, cstZone);
        //    dt = harareTime;///.ToLocalTime();
        ////double ticks = double.Parse(dateTime);
        ////TimeSpan time = TimeSpan.FromMilliseconds(ticks);
        //    //DateTime dt = new DateTime(1970, 1, 1) + time;
        //    System.Diagnostics.Debug.WriteLine("The date and time are {0} UTC.3",
        //       dt);
        //return dt.ToString();

        //######################################## new 2rd way, using daylight savings time 
        //DateTime utcTm = dt.ToUniversalTime();
        ////    //utc time to local time
        //DateTime harareTime = TimeZoneInfo.ConvertTime(TimeZoneInfo.ConvertTimeToUtc(dt), cstZone);
        ////DateTime userTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
        ////TimeZoneInfo UserTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
        ////TimeZoneInfo TargetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        ////DateTime newTime = TimeZoneInfo.ConvertTime(userTime, UserTimeZone, TargetTimeZone);
        ////Console.WriteLine(newTime);
        //return harareTime.ToString();

        //######################################## new 3rd way, using daylight savings time 
        //DateTime harareTime = GetLocalDateTime(TimeZoneInfo.ConvertTimeToUtc(dt), cstZone);

        ////if (cstZone.IsDaylightSavingTime(harareTime))
        ////    harareTime = harareTime.AddHours(2);

        //return harareTime.ToString();

        //######################################## new 4th way, using daylight savings time 
        //  return TimeZoneInfo.ConvertTimeToUtc(dt).ToLocalTime().ToString();
        return TimeZoneInfo.ConvertTimeToUtc(dt).ToString();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return DateTime.Now.ToString();
      }
    }
    public static DateTime GetLocalDateTimeHre()
    {

      DateTime utcDateTime = DateTime.UtcNow;
      TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
      //  DateTime  = Utils.GetLocalDateTime(TimeZoneInfo.ConvertTimeToUtc(reg_date), cstZone);

      utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

      DateTime localtime = TimeZoneInfo.ConvertTime(utcDateTime, timeZone);

      return localtime;

    }
    public static DateTime GetLocalDateTime(DateTime utcDateTime, TimeZoneInfo timeZone)
    {

      utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

      DateTime time = TimeZoneInfo.ConvertTime(utcDateTime, timeZone);

      return time;

    }
    public static string DatetimeToAge(DateTime birthdate)
    {
      try
      {// Save today's date.
        var today = DateTime.Today;

        // Calculate the age.
        var age = today.Year - birthdate.Year;

        // Go back to the year in which the person was born in case of a leap year
        if (birthdate.Date > today.AddYears(-age)) age--;
        return age.ToString();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return "0";
      }
    }
    public static DateTime stringToDatetime(string dateTime)
    {
      try
      {
        // DateTime oDate = Convert.ToDateTime(iDate);
        DateTime dt = DateTime.Parse(dateTime);
        return dt;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return DateTime.Now;
      }
    }

    public static DateTime stringLongToDatetime(string dateTime)
    {
      try
      {
        double ticks = double.Parse(dateTime);
        TimeSpan time = TimeSpan.FromMilliseconds(ticks);
        DateTime dt = new DateTime(1970, 1, 1) + time;
        return dt;
        //            or simply

        //var date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(startdatetime));
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return DateTime.Now;
      }
    }

    public static string getNextChar(string str)
    {
      char letter = str[0];
      var nextChar = 'a';

      if (letter == 'z')
        nextChar = 'a';
      else if (letter == 'Z')
        nextChar = 'A';
      else
        nextChar = (char)(((int)letter) + 1);

      return Convert.ToString(nextChar).ToUpper().Trim();
    }

    public static string getPaymentNumber(string counter)
    {
      if (counter.Length == 1)
      {
        counter = "0000" + counter;
      }
      else if (counter.Length == 2)
      {
        counter = "000" + counter;
      }
      else if (counter.Length == 3)
      {
        counter = "00" + counter;
      }
      else if (counter.Length == 4)
      {
        counter = "0" + counter;
      }
      return counter;
    }

    //public static string ToAbsoluteUrl(string relativeUrl) //Use absolute URL instead of adding phycal path for CSS, JS and Images     
    //{
    //  if (string.IsNullOrEmpty(relativeUrl)) return relativeUrl;
    //  if (HttpContext.Current == null) return relativeUrl;
    //  if (relativeUrl.StartsWith("/")) relativeUrl = relativeUrl.Insert(0, "~");
    //  if (!relativeUrl.StartsWith("~/")) relativeUrl = relativeUrl.Insert(0, "~/");
    //  var url = HttpContext.Request.Host.Url;
    //  var port = url.Port != 80 ? (":" + url.Port) : String.Empty;
    //  return String.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port,VirtualPathUtility.ToAbsolute(relativeUrl));
    //}
    //************ ENCRYPTION *******************
    public static class EncryptDecryptQueryString
    {
      private static byte[] key = { };
      private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
      public static string Decrypt(string stringToDecrypt, string sEncryptionKey)
      {
        byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
        try
        {
          key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
          DESCryptoServiceProvider des = new DESCryptoServiceProvider();
          inputByteArray = Convert.FromBase64String(stringToDecrypt);
          MemoryStream ms = new MemoryStream();
          CryptoStream cs = new CryptoStream(ms,
            des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
          cs.Write(inputByteArray, 0, inputByteArray.Length);
          cs.FlushFinalBlock();
          System.Text.Encoding encoding = System.Text.Encoding.UTF8;
          return encoding.GetString(ms.ToArray());
        }
        catch (Exception e)
        {
          return e.Message;
        }
      }

      public static string Encrypt(string stringToEncrypt, string SEncryptionKey)
      {
        try
        {
          key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
          DESCryptoServiceProvider des = new DESCryptoServiceProvider();
          byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
          MemoryStream ms = new MemoryStream();
          CryptoStream cs = new CryptoStream(ms,
            des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
          cs.Write(inputByteArray, 0, inputByteArray.Length);
          cs.FlushFinalBlock();
          return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception e)
        {
          return e.Message;
        }
      }
    }
    static string GetMd5Hash(MD5 md5Hash, string input)
    {
      // Convert the input string to a byte array and compute the hash.
      byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
      // Create a new Stringbuilder to collect the bytes
      // and create a string.
      StringBuilder sBuilder = new StringBuilder();
      // Loop through each byte of the hashed data 
      // and format each one as a hexadecimal string.
      for (int i = 0; i < data.Length; i++)
      {
        sBuilder.Append(data[i].ToString("x2"));
      }
      // Return the hexadecimal string.
      return sBuilder.ToString();
    }

    // Verify a hash against a string.
    static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
    {
      // Hash the input.
      string hashOfInput = GetMd5Hash(md5Hash, input);
      // Create a StringComparer an compare the hashes.
      StringComparer comparer = StringComparer.OrdinalIgnoreCase;
      if (0 == comparer.Compare(hashOfInput, hash))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public static string Encrypt(string plainText)
    {
      byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

      byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
      var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
      var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

      byte[] cipherTextBytes;

      using (var memoryStream = new MemoryStream())
      {
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        {
          cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
          cryptoStream.FlushFinalBlock();
          cipherTextBytes = memoryStream.ToArray();
          cryptoStream.Close();
        }
        memoryStream.Close();
      }
      return Convert.ToBase64String(cipherTextBytes);
    }

    public static string Decrypt(string encryptedText)
    {
      byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
      byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
      var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

      var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
      var memoryStream = new MemoryStream(cipherTextBytes);
      var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
      byte[] plainTextBytes = new byte[cipherTextBytes.Length];

      int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
      memoryStream.Close();
      cryptoStream.Close();
      return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
    }

    public string EncryptWithoutKeyString(string inputString, int dwKeySize,
                         string xmlString)
    {
      RSACryptoServiceProvider rsaCryptoServiceProvider =
                                new RSACryptoServiceProvider(dwKeySize);
      rsaCryptoServiceProvider.FromXmlString(xmlString);
      int keySize = dwKeySize / 8;
      byte[] bytes = Encoding.UTF32.GetBytes(inputString);
      // RSACryptoServiceProvider here 
      int maxLength = keySize - 42;
      int dataLength = bytes.Length;
      int iterations = dataLength / maxLength;
      StringBuilder stringBuilder = new StringBuilder();
      for (int i = 0; i <= iterations; i++)
      {
        byte[] tempBytes = new byte[
                (dataLength - maxLength * i > maxLength) ? maxLength :
                                              dataLength - maxLength * i];
        Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0,
                          tempBytes.Length);
        byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes,
                                                                  true);
        stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
      }
      return stringBuilder.ToString();
    }
    public string DecryptWithoutKeyString(string inputString, int dwKeySize, string xmlString)
    {
      RSACryptoServiceProvider rsaCryptoServiceProvider
                                   = new RSACryptoServiceProvider(dwKeySize);
      rsaCryptoServiceProvider.FromXmlString(xmlString);
      int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ?
        (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
      int iterations = inputString.Length / base64BlockSize;
      ArrayList arrayList = new ArrayList();
      for (int i = 0; i < iterations; i++)
      {
        byte[] encryptedBytes = Convert.FromBase64String(
             inputString.Substring(base64BlockSize * i, base64BlockSize));
        Array.Reverse(encryptedBytes);
        arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(
                            encryptedBytes, true));
      }
      return Encoding.UTF32.GetString(arrayList.ToArray(
                                Type.GetType("System.Byte")) as byte[]);
    }

    public static string EncryptWithKeyTripleDESCryptoServiceProvider(string toEncrypt, bool useHashing)
    {
      byte[] keyArray;
      byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

      // System.Configuration.AppSettingsReader settingsReader =new AppSettingsReader();
      // Get the key from config file

      //  string key = (string)settingsReader.GetValue("AppSecurityKey",typeof(String));
      string key = System.Environment.GetEnvironmentVariable("AppSecurityKey", EnvironmentVariableTarget.Process);
      //System.Windows.Forms.MessageBox.Show(key);
      //If hashing use get hashcode regards to your key
      if (useHashing)
      {
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        //Always release the resources and flush data
        // of the Cryptographic service provide. Best Practice

        hashmd5.Clear();
      }
      else
        keyArray = UTF8Encoding.UTF8.GetBytes(key);

      TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
      //set the secret key for the tripleDES algorithm
      tdes.Key = keyArray;
      //mode of operation. there are other 4 modes.
      //We choose ECB(Electronic code Book)
      tdes.Mode = CipherMode.ECB;
      //padding mode(if any extra byte added)

      tdes.Padding = PaddingMode.PKCS7;

      ICryptoTransform cTransform = tdes.CreateEncryptor();
      //transform the specified region of bytes array to resultArray
      byte[] resultArray =
        cTransform.TransformFinalBlock(toEncryptArray, 0,
        toEncryptArray.Length);
      //Release resources held by TripleDes Encryptor
      tdes.Clear();
      //Return the encrypted data into unreadable string format
      return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public static string DecryptWithKeyTripleDESCryptoServiceProvider(string cipherString, bool useHashing)
    {
      byte[] keyArray;
      //get the byte code of the string

      byte[] toEncryptArray = Convert.FromBase64String(cipherString);

      // System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
      //Get your key from config file to open the lock!
      // string key = (string)settingsReader.GetValue("AppSecurityKey",typeof(String));
      string key = System.Environment.GetEnvironmentVariable("AppSecurityKey", EnvironmentVariableTarget.Process);
      if (useHashing)
      {
        //if hashing was used get the hash code with regards to your key
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        //release any resource held by the MD5CryptoServiceProvider

        hashmd5.Clear();
      }
      else
      {
        //if hashing was not implemented get the byte code of the key
        keyArray = UTF8Encoding.UTF8.GetBytes(key);
      }

      TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
      //set the secret key for the tripleDES algorithm
      tdes.Key = keyArray;
      //mode of operation. there are other 4 modes. 
      //We choose ECB(Electronic code Book)

      tdes.Mode = CipherMode.ECB;
      //padding mode(if any extra byte added)
      tdes.Padding = PaddingMode.PKCS7;

      ICryptoTransform cTransform = tdes.CreateDecryptor();
      byte[] resultArray = cTransform.TransformFinalBlock(
                           toEncryptArray, 0, toEncryptArray.Length);
      //Release resources held by TripleDes Encryptor                
      tdes.Clear();
      //return the Clear decrypted TEXT
      return UTF8Encoding.UTF8.GetString(resultArray);
    }

    //************ END ENCRYPTION **************

    internal static DateTime getLastMonthDayDateTime(DateTime device_date)
    {
      return new DateTime(device_date.Year, device_date.Month, DateTime.DaysInMonth(device_date.Year, device_date.Month));
    }
    private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


    public static long getDTimeMillis(DateTime? _dateTime)
    {
      DateTime dTime = _dateTime ?? DateTime.Now;
      return (long)(dTime - Jan1st1970).TotalMilliseconds;
    }

    public static DateTime getDTimeFromStrimg(string _dateTimeStr)
    {
      try
      {
        return DateTime.Parse(_dateTimeStr);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return DateTime.Now;
      }
    }


    public static string GenerateSecurityCode(int length, Random random)
    {
      string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
      StringBuilder result = new StringBuilder(length);
      for (int i = 0; i < length; i++)
      {
        result.Append(characters[random.Next(characters.Length)]);
      }
      return result.ToString();
    }

    public static byte[] GenerateRandomData(int length)
    {
      var rnd = new byte[length];
      using (var rng = new RNGCryptoServiceProvider())
        rng.GetBytes(rnd);
      return rnd;
    }

    public static string intCodeRandom(int length)
    {
      if (length <= 0) throw new ArgumentException("Length must be greater than zero.");

      var random = new Random();
      var result = new StringBuilder();

      // Ensure the first digit is not zero
      result.Append(random.Next(1, 10));

      for (int i = 1; i < length; i++)
      {
        result.Append(random.Next(0, 10));
      }

      return result.ToString();
    }

    public static int GenerateRandomInt(int minVal = 0, int maxVal = 100)
    {
      var rnd = new byte[4];
      using (var rng = new RNGCryptoServiceProvider())
        rng.GetBytes(rnd);
      var i = Math.Abs(BitConverter.ToInt32(rnd, 0));
      return Convert.ToInt32(i % (maxVal - minVal + 1) + minVal);
    }
    public static string GenerateRandomString(int length, string allowableChars = null)
    {
      if (string.IsNullOrEmpty(allowableChars))
        allowableChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";

      // Generate random data
      var rnd = new byte[length];
      using (var rng = new RNGCryptoServiceProvider())
        rng.GetBytes(rnd);

      // Generate the output string
      var allowable = allowableChars.ToCharArray();
      var l = allowable.Length;
      var chars = new char[length];
      for (var i = 0; i < length; i++)
        chars[i] = allowable[rnd[i] % l];

      return new string(chars);
    }
    private static string MakePassword(int pl)
    {
      //MakePassword(10)
      string possibles = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
      char[] passwords = new char[pl];
      Random rd = new Random();

      for (int i = 0; i < pl; i++)
      {
        passwords[i] = possibles[rd.Next(0, possibles.Length)];
      }
      return new string(passwords);
    }

  }
}
