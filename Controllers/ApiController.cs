using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Interfaces;
using AspnetCoreMvcFull.Models.api;
using AspnetCoreMvcFull.Models.db;
using AspnetCoreMvcFull.Services;
using AspnetCoreMvcFull.Services.dtos;
using AspnetCoreMvcFull.Utils;
using Azure.Core.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspnetCoreMvcFull.Controllers
{
  //[ApiController]
  //[Route("api/[controller]")]
  public class ApiController : Controller
  {

    private readonly ILogger<ApiController> _logger;
    private readonly AspnetCoreMvcFullContext _context;
    private readonly IGoogleFunctionsService _gfunctionsService;
    private readonly IAPIService _notificationService;

    public ApiController(ILogger<ApiController> logger, AspnetCoreMvcFullContext context, IGoogleFunctionsService GfunctionsService, IAPIService notificationService)
    {
      _logger = logger;
      _context = context;
      _gfunctionsService = GfunctionsService;
      _notificationService = notificationService;
    }

    // GET: api/getallcurrencies
    [HttpGet]
    public IActionResult GetAllCurrencies()
    {
      string rqfrom = "GetAllCurrencies";
      string reference = Utils.Utilities.intCodeRandom(8);
      var currencies = new[] { "USD", "Rands", "ZWG" } ;

      var resJson = apiMapRequestRes( "000","Success", currencies);

      // Replace with actual data retrieval logic
      return apiSuccessResLogWithReference( rqfrom, reference, resJson);// Ok(new[] { "USD", "Rands","ZWG" });
    }

    [HttpPost]
    public IActionResult AuthLogin([FromBody] LoginRequest model)
    {
      LogTransaction("LoginModel", null, "0770000000", model, 200);
      // Replace with actual data retrieval logic
      return Ok(new[] { "USD", "Rands", "ZWG" });
    }

    public async Task<IActionResult> LoginCashier(LoginRequest request)
    {
      const string from = "LoginCashier";
      _logger.LogDebug("LoginDriver called with data: {@Request}", request);

      string msisdn = request.Username.StartsWith("+") ? request.Username : "+" + request.Username;
      var responseCode = GlobalConstants.ON_FAILURE_RES;
      var responseMessage = "Error";

      try
      {
        var teller = await _context.Cashiers.FirstOrDefaultAsync(a => a.Phone == msisdn);
        if (teller == null)
        {
          responseCode = GlobalConstants.ON_NOT_FOUND_RES;
          responseMessage = "Failed";
          return apiSuccessRes(from, new { responseCode = responseCode, responseDescription = responseMessage } );
        }

        if (teller.Status != 1 && teller.Status != 2)
        {
          if (teller.TwoFaVerified == 0)
          {
            return apiResponseWithId(msisdn, from, new { responseCode = GlobalConstants.ON_ACC_NOT_AUTH, responseDescription = "2FA Account Issues" });
          }
          return apiResponseWithId(msisdn, from, new { responseCode = GlobalConstants.ON_ACC_NOT_AUTH, responseDescription = "Account Issues" } );
        }

        teller.TwoFaVerified = 0;
        await _context.SaveChangesAsync();

        if (request.AppType.HasValue && teller.SupervisorAuth != request.AppType.Value)
        {
          return apiResponseWithId(msisdn, from, new { responseCode = GlobalConstants.ON_ACC_NOT_AUTH, responseDescription = "Lv Not Authorised" } );
        }
        Device? prevDevice = null;
        var result = WebDataService.AuthTellerLocal(_logger,teller, request.Pincode);
        if (((int)result["resultStatus"]) == GlobalConstants.ON_SUCCESS_RES)
        {
          List<Device> devices = await _context.Devices
                                            .Where(d => d.Msisdn == msisdn)
                                            .ToListAsync();

          int isAlreadyRegistered = 0;

          if (devices.Any()) // Equivalent of: if($devices != null) and count check
          {
            int devicesCount = devices.Count;

            // Equivalent of: $prev_device = InDevices::wheremsisdn($msisdn)->latest()->limit(1)->get()->first();
            // This EF Core query gets the single latest device record by 'CreatedAt' or similar
             prevDevice = await _context.Devices
                                                .Where(d => d.Msisdn == msisdn)
                                                .OrderByDescending(d => d.CreatedAt) // Assuming 'CreatedAt' is used for latest()
                                                .FirstOrDefaultAsync();

            if (prevDevice != null)
            {
              // Check 1: Same model and name? -> Already Registered
              if (prevDevice.DeviceModel == request.DeviceModel && prevDevice.DeviceName == request.DeviceName)
              {
                isAlreadyRegistered = 1;
              }
              else // Different device model or name
              {
                // Check 2: Too many devices?
                if (devicesCount > 3)
                {
                  // Equivalent of: return $util->apiSuccessRes(...)
                  return apiSuccessRes(from, new { responseCode = GlobalConstants.ON_TOO_MANY_DEVICES, responseDescription = "Not Authorised. Too Many Devices." });
                }
              }

              // Check 3: Same device name? (If not handled above)
              if (prevDevice.DeviceName != request.DeviceName)
              {
                // Different device name picked (Logic runs even if limit check passes)
                string message = $"Alert-New Device {msisdn} is trying to login from a different device, {request.DeviceName}";

                // Equivalent of: $result = $obj_process->sendSMSNotification(...)
                // This should be fire-and-forget or handled with reliable queuing (like SendSMSs::dispatch)
                await _notificationService.SendSMSNotificationAsync(GlobalConstants.SMS_NOTIF_ONE, message);
              }

              // $device = $prev_device; // If you need to return or use prevDevice later
            }
            // else: No previous device found for MSISDN (should be caught by devices.Any() but kept for logic structure)
          }
          else
          {
            // Equivalent of: else {//new device login}
            // Logic for a truly new MSISDN/device registration goes here
          }

          string reference = Utils.Utilities.GetRandomNumber(5); // assuming this is a method that returns a string
          // Device registration logic
          var deviceId = Guid.NewGuid().ToString();
          var otpResult = await _gfunctionsService.DeviceRegOTPAsync(msisdn);

          if (otpResult["resultStatus"] == "000")
          {
            if (isAlreadyRegistered == 1){//update current device

                            prevDevice.VerificationCode = otpResult["verification_code"];
                            prevDevice.DeviceId = deviceId;
                            prevDevice.DeviceHardwareId = request.DeviceHardwareId;
                            prevDevice.UpdatedAt = DateTime.UtcNow;
              await _context.SaveChangesAsync();


                            string message = "A New device "+ request.Username+ " Link request. Device OTP "+ otpResult["verification_code"];
              var audit = new InAuthAudit
              {
                UserId = 2000,
                Amount = 0,
                Ref = message,
                Main = 1,
                Type = GlobalConstants.DEVICE_REG,
                StripeId = "0",
                Charge = "0"
              };

              _context.InAuthAudits.Add(audit);
              _context.SaveChanges();


            }
            else
            {
              // Save or update device
              var device = new Device
              {
                Msisdn = msisdn,
                DeviceId = deviceId,
                VerificationCode = otpResult["verification_code"],
                AuthToken = "0-0",
                DeviceName = request.DeviceName,
                DeviceOem = request.DeviceOem,
                DeviceType = request.DeviceType,
                DeviceModel = request.DeviceModel,
                DeviceHardwareId = request.DeviceHardwareId,
                PlatformOs = request.PlatformOs,
                PlatformOsVersion = request.PlatformOsVersion,
                UserType = request.UserType,
                SupervisorAuth = request.AppType ?? 0,
                MonthAdded = Utils.Utilities.GetMonthJoinedDateTime(DateTime.UtcNow),
                DayAdded = Utils.Utilities.GetDayJoinedDateTime(DateTime.UtcNow),
                RequestIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = 0,
                WasVerified = 0
              };

              _context.Devices.Add(device);
              await _context.SaveChangesAsync();

               prevDevice = _context.Devices
   .Where(d => d.DeviceId == deviceId)
   .FirstOrDefault();

            }
            if (teller != null)
            {
              int tellerId = (int)teller.Id;
              teller.DeviceId = deviceId;
              _context.Cashiers.Update(teller);
              _context.SaveChanges();

              var history = new History
              {
                UserId = tellerId,
                Amount = 0,
                Ref = $"{otpResult["verification_code"]}:{reference}",
                Main = 1,
                Type = GlobalConstants.SECURITY_DV_2FA,
                StripeId = "0",
                Charge = 0
              };

              _context.History.Add(history);
              _context.SaveChanges();
            }

            
                    var authToken = Guid.NewGuid().ToString("N");//without dashes
                    var deviceHardwareId = Guid.NewGuid().ToString();
            //  teller.DeviceId = deviceId;
            teller.AuthToken = "";
            teller.DeviceHardwareId = deviceHardwareId;
            teller.TwFaVrDate = DateTime.UtcNow;
            teller.PinRetriesCounter = 0;
            teller.Status = 1;
            teller.LastSeen = DateTime.UtcNow;
            teller.IsOnline = teller.SupervisorAuth == 0 ? 1 : 2;

            await _context.SaveChangesAsync();
            
                    prevDevice.AuthToken = authToken;
            prevDevice.LastAuthAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return apiResponseWithId(msisdn, from, new
            {
              responseCode = GlobalConstants.ON_SUCCESS_RES,
              responseDescription = "Success",
              deviceId = deviceId,
              reference = reference,
              accessToken = teller.AuthToken
            });
          }
        }
        else if (((int)result["resultStatus"]) == GlobalConstants.ON_WRONG_PINCODE)
        {
          teller.PinRetriesCounter++;
          if (teller.PinRetriesCounter > 3)
          {
            teller.Status = 0;
          }
          await _context.SaveChangesAsync();

          return apiResponseWithId(msisdn, from, new { responseCode = GlobalConstants.ON_WRONG_OTP, responseDescription = "Not Authorised, Please Check your PIN." });
        }

        return apiResponseWithId(msisdn, from, new { responseCode = result["resultStatus"], responseDescription = "Not Authorised" });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Exception in LoginCashier");
        return apiSuccessRes(from, new { responseCode = GlobalConstants.ON_FAILURE_RES, responseDescription = ex.Message });
      }
    }

    [HttpPost]
    public IActionResult CashbookTrans([FromBody] LoginRequest model)
    {
      LogTransaction("CashbookTrans", null, "0770000000", model, 200);
      // Replace with actual data retrieval logic
      return Ok(new[] { "USD", "Rands", "ZWG" });
    }

    [HttpGet("products")]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
      var products = await _context.Products.ToListAsync() ?? new List<Product>();
      return Ok(products);
    }
    public object apiMapRequestRes( string responseCode, string responseDescription, object _resJson)
    {

      var resJson = new
      {
        responseCode = responseCode,
        responseDescription = responseDescription,
        dynamicData = _resJson
      };
      return resJson;
    }

    public IActionResult apiResponseWithId(string msisdn,string rqfrom, object _resJson)
    {
      var resJson = objToDiction(_resJson);
      _logger.LogDebug("{RqFrom} -RES- {@ResJson}", rqfrom, resJson);

      var transLog = new InTransLog
      {
        UserId = 1,
        From = rqfrom,
        RequestBy = msisdn,
        Reference = "",
        ResponseCode = resJson.ContainsKey("responseCode") ? resJson["responseCode"]?.ToString() : "500",
        // Replace this line:
        // Result = JsonSerializer.Serialize(resJson),

        // With this line:
        Result = JsonConvert.SerializeObject(resJson),
        HttpCode = "200",
        CreatedAt = DateTime.UtcNow
      };

      _context.TransLogs.Add(transLog);
      _context.SaveChanges();

      return Ok(resJson);
    }

    public IActionResult apiSuccessRes(string rqfrom, object _resJson)
    {
      // Convert to Dictionary<string, object>
      var resJson = objToDiction(_resJson);

      //if (rqfrom != "CashbookTrans")
      //{
      _logger.LogDebug("{RqFrom} -RES- {@ResJson}", rqfrom, resJson);
      //}

      var transLog = new InTransLog
      {
        UserId = 1,
        From = rqfrom,
        //Reference = reference,
        ResponseCode = resJson.ContainsKey("responseCode") ? resJson["responseCode"]?.ToString() : "500",
        // Replace this line:
        // Result = JsonSerializer.Serialize(resJson),

        // With this line:
        Result = JsonConvert.SerializeObject(resJson),
        HttpCode = "200",
        CreatedAt = DateTime.UtcNow
      };

      _context.TransLogs.Add(transLog);
      _context.SaveChanges();

      return Ok(resJson);
    }

    public Dictionary<string, object> objToDiction(object Objobj)
    {
      string message = "An unexpected error occurred.";
      try
      {
        var json = System.Text.Json.JsonSerializer.Serialize(Objobj, new JsonSerializerOptions());
        return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(json);
      }
      catch (Exception ex)
      {
        message = ex.Message;
        _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
        // handle error
        _logger.LogDebug("Stack Trace: {StackTrace}", ex.StackTrace);
      }
      finally
      {
        // always runs, even if an exception occurs
       // Console.WriteLine("Cleaning up...");
      }

      return new Dictionary<string, object>
{
    { "responseCode", "500" },
    { "responseDescription", message },
    { "DynamicData", new Dictionary<string, object>{}}
};

    }

    public IActionResult apiSuccessResLogWithReference( string rqfrom, string reference,  object _resJson)
    {
      // Convert to Dictionary<string, object>
      var resJson = objToDiction(_resJson);

      //if (rqfrom != "CashbookTrans")
      //{
      _logger.LogDebug("{RqFrom} - Ref:{Reference} -RES- {@ResJson}", rqfrom, reference, resJson);
      //}

      var transLog = new InTransLog
      {
        UserId = 1,
        From = rqfrom,
        Reference = reference,
        ResponseCode = resJson.ContainsKey("responseCode") ? resJson["responseCode"]?.ToString() : "500",
          // Replace this line:
          // Result = JsonSerializer.Serialize(resJson),

          // With this line:
          Result = JsonConvert.SerializeObject(resJson),
        HttpCode = "200",
        CreatedAt = DateTime.UtcNow
      };

      _context.TransLogs.Add(transLog);
      _context.SaveChanges();

      return Ok(resJson);
    }

    private void LogTransaction(string rqfrom, string reference, string msisdn, object resJson, int httpCode)
    {
      if (rqfrom != "cashbooktrans")
      {
        var logMessage = string.IsNullOrEmpty(reference)
            ? $"{rqfrom} -RES- {JsonConvert.SerializeObject(resJson)}"
            : $"{rqfrom} - Ref:{reference} -RES- {JsonConvert.SerializeObject(resJson)}";

        _logger.LogDebug(logMessage);
      }

      var transLog = new InTransLog
      {
        UserId = 1,
        RequestBy = msisdn,
        From = rqfrom,
        Reference = reference,
        ResponseCode = resJson is IDictionary<string, object> dict && dict.ContainsKey("responseCode")
              ? dict["responseCode"]?.ToString()
              : null,
        Result = JsonConvert.SerializeObject(resJson),
        HttpCode = httpCode.ToString(),
        CreatedAt = DateTime.UtcNow
      };

      _context.TransLogs.Add(transLog);
      _context.SaveChanges();
    }

    [NonAction]
    public IActionResult ApiSuccessResWithId(string msisdn, string rqfrom, object resJson)
    {
      LogTransaction(rqfrom, null, msisdn, resJson, 200);
      return StatusCode(200, resJson);
    }

    [NonAction]
    public IActionResult ApiSuccessRes(string rqfrom, object resJson)
    {
      LogTransaction(rqfrom, null, null, resJson, 200);
      return StatusCode(200, resJson);
    }

    [NonAction]
    public IActionResult ApiSuccessResLogWithReference(string rqfrom, string reference, object resJson)
    {
      LogTransaction(rqfrom, reference, null, resJson, 200);
      return StatusCode(200, resJson);
    }

    [NonAction]
    public IActionResult ApiErrorRes(string rqfrom, object resJson, int httpCode)
    {
      LogTransaction(rqfrom, null, null, resJson, httpCode);
      return StatusCode(httpCode, resJson);
    }

    [NonAction]
    public IActionResult ApiErrorResWithReference(string rqfrom, string reference, object resJson, int httpCode)
    {
      LogTransaction(rqfrom, reference, null, resJson, httpCode);
      return StatusCode(httpCode, resJson);
    }
  
}
}
