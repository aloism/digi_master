using AspnetCoreMvcFull.Interfaces;
using AspnetCoreMvcFull.Models.db;
using AspnetCoreMvcFull.Services.dtos;
using System.Text.Json;

namespace AspnetCoreMvcFull.Services
{
  public class GoogleFunctionsService : IGoogleFunctionsService
  {
    private readonly HttpClient _httpClient;
    private readonly ILogger<GoogleFunctionsService> _logger;
    private const string AccessCode = "developers@bulkit.co.zw";
    private const string AccessPassword = "20@Blk005#";
    private const string BaseUrl = "https://us-central1-uware-bdb26.cloudfunctions.net/uware_ssqueries"; // Replace with actual URL
    private const string SendOtpEndpoint = "/api/v2/send_device_otp"; // Replace with actual endpoint

    public GoogleFunctionsService(HttpClient httpClient, ILogger<GoogleFunctionsService> logger)
    {
      _httpClient = httpClient;
      _logger = logger;
    }

    public async Task<Dictionary<string, string>> DeviceRegOTPAsync(string msisdn)
    {
      _logger.LogInformation("deviceRegOTP");

      if (!msisdn.StartsWith("+"))
      {
        msisdn = "+" + msisdn;
      }

      var responseArr = new Dictionary<string, string>
      {
        ["resultStatus"] = "FAILURE",
        ["res_message"] = "Request Failed"
      };

      string requestId = GenerateRandomString(5);

      var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl + SendOtpEndpoint);
      request.Headers.Add("x-access-code", AccessCode);
      request.Headers.Add("x-access-password", AccessPassword);
      request.Headers.Add("x-agent-reference", $"uwx-agx-{requestId}");

      var payload = new Dictionary<string, string>
      {
        ["mobileNumber"] = msisdn,
        ["cmobileNumber"] = msisdn,
        ["cfirstName"] = "",
        ["accountNumber"] = ""
      };

      request.Content = new FormUrlEncodedContent(payload);

      var response = await _httpClient.SendAsync(request);
      var responseContent = await response.Content.ReadAsStringAsync();

      _logger.LogInformation(responseContent);

      if (response.IsSuccessStatusCode)
      {
        var res = JsonSerializer.Deserialize<ApiResponse>(responseContent);

        if (res?.ResCode == "000")
        {
          responseArr["resultStatus"] = res.ResCode;
          responseArr["verification_code"] = res.DataId;
          responseArr["res_message"] = "Request Successful";

          var now = DateTime.UtcNow;
          var monthAdded = Utils.Utilities.GetMonthJoinedDateTime(now);
          var dayAdded = Utils.Utilities.GetDayJoinedDateTime(now);

          var notification = new InNotification
          {
            UserId = msisdn,
            Title = "SMS Notification",
            Message = $"Dev reg OTP {res.DataId}",
            UserType = 1,
            MonthAdded = monthAdded,
            DayAdded = dayAdded,
            RequestIp = GetUserIp(),
            CreatedAt = DateTime.UtcNow,
            Status = 1
          };

          // Save notification to DB
          // await _dbContext.InNotifications.AddAsync(notification);
          // await _dbContext.SaveChangesAsync();
        }
        else
        {
          responseArr["resultStatus"] = res?.ResCode ?? "FAILURE";
          responseArr["res_message"] = "Request Failed.";
        }
      }

      return responseArr;
    }

    private string GenerateRandomString(int length)
    {
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      var random = new Random();
      return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private string GetUserIp()
    {
      // Implement IP retrieval logic based on your hosting environment
      return "127.0.0.1";
    }

  }
}
