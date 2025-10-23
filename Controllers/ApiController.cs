using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models.db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json;
using System;

namespace AspnetCoreMvcFull.Controllers
{
  public class ApiController : Controller
  {

    private readonly ILogger<ApiController> _logger;
    private readonly AspnetCoreMvcFullContext _context;

    public ApiController(ILogger<ApiController> logger, AspnetCoreMvcFullContext context)
    {
      _logger = logger;
      _context = context;
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
