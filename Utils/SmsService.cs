using AspnetCoreMvcFull.Controllers;
using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;
using System;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json;

namespace AspnetCoreMvcFull.Utils
{
  public class SmsService
  {
    private static readonly Random random = new Random();
    private readonly ILogger<HomeController> _logger;
    private AspnetCoreMvcFullContext _appDbContext;
    private readonly IHttpClientFactory _httpClientFactory;

    public SmsService(AspnetCoreMvcFullContext appDbContext, ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
      _appDbContext = appDbContext;
      _logger = logger;
      _httpClientFactory = httpClientFactory;

    }

    public SmsService()
    {
    }

    public async Task<Dictionary<string, string>> SendSMSNotificationAsync(string msisdn, string message)
    {
      _logger.LogInformation("sendSMSNotification");
      _logger.LogInformation("msisdn: {Msisdn}, message: {Message}", msisdn, message);

      if (!msisdn.StartsWith("+"))
      {
        msisdn = "+" + msisdn;
      }

      var responseDict = new Dictionary<string, string>
    {
        { "resultStatus", GlobalConstants.OnFailureRes.ToString() },
        { "res_message", "Request Failed" }
    };

      string requestId = Utils.Utilities.intCodeRandom(5);

      var client = _httpClientFactory.CreateClient();
      client.DefaultRequestHeaders.Add("x-access-code", "developers@salesiq.co.zw");
      client.DefaultRequestHeaders.Add("x-access-password", "25@Sls007#");
      client.DefaultRequestHeaders.Add("x-agent-reference", $"uwx-agx-{requestId}");

      var payload = new Dictionary<string, string>
    {
        { UrlJsonParams.MOBILENUMBER, msisdn },
        { "message", message },
        { "platform", "Web" }
    };

      var response = await client.PostAsJsonAsync(Constants.URLs.URL_CF_SEND_UWARE_SMS_URL, payload);

      var now = DateTime.UtcNow;
      var monthAdded = Utils.Utilities.GetMonthJoinedDateTime(now);
      var dayAdded = Utils.Utilities.GetDayJoinedDateTime(now);

      var notification = new InNotification
      {
        UserId = msisdn,
        Title = "SMS Notification",
        Message = message,
        UserType = 1,
        MonthAdded = monthAdded,
        DayAdded = dayAdded,
        RequestIp = MainHelper._httpContext.Connection.RemoteIpAddress?.ToString(),
        CreatedAt = DateTime.UtcNow
      };

      if (response.IsSuccessStatusCode)
      {
        var resContent = await response.Content.ReadAsStringAsync();
        var res = JsonSerializer.Deserialize<SmsResponse>(resContent);

        if (res?.ResCode == "000")
        {
          notification.Status = 1;
          responseDict["resultStatus"] = res.ResCode;
          responseDict["msg"] = res.Msg;
          responseDict["res_message"] = "Request Successful";
        }
        else
        {
          notification.Status = 0;
          responseDict["resultStatus"] = res?.ResCode ?? "ERR";
          responseDict["res_message"] = "Request Failed.";
        }
      }

      _appDbContext.Notifications.Add(notification);
      await _appDbContext.SaveChangesAsync();

      return responseDict;
    }
    public static string GenerateString(string input, int strength = 16)
    {
      int inputLength = input.Length;
      string randomString = "";

      for (int i = 0; i < strength; i++)
      {
        int index = random.Next(0, inputLength);
        randomString += input[index];
      }

      return randomString;
    }

    public static int IntCodeRandom(int length = 8)
    {
      int intMin = (int)Math.Pow(10, length - 1); // e.g., 10000000 for length 8
      int intMax = (int)Math.Pow(10, length) - 1; // e.g., 99999999 for length 8

      return random.Next(intMin, intMax + 1);
    }


  }
}
