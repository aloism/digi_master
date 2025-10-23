using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;
using AspnetCoreMvcFull.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace AspnetCoreMvcFull.Controllers
{
  public class BusinessesController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private AspnetCoreMvcFullContext _appDbContext;
    public BusinessesController(AspnetCoreMvcFullContext appDbContext, ILogger<HomeController> logger)
    {
      _appDbContext = appDbContext;
      if (string.IsNullOrEmpty(DashboardsController.getAccountName()) || !AccountAuthentication.instance().isAccountLoggedIn(DashboardsController.getLoggedInState()))
      {
        //DashboardsController.redirectAuthIndex();
      }
      DashboardsController.updateSessionTimeOut();
      _logger = logger;
    }
    public IActionResult Index()
    {
      return View();
    }
    public IActionResult List()
    {
      ViewBag.Businesses = _appDbContext.Users.Count();
      int activeUserCount = _appDbContext.Users.Count(u => u.Status == 1);
      ViewBag.ActiveBusinesses = activeUserCount;
      int pendingUserCount = _appDbContext.Users.Count(u => u.Status == 0);
      ViewBag.PendingBusinesses = pendingUserCount;
      int inactiveUserCount = _appDbContext.Users.Count(u => u.Status == 2);
      ViewBag.InactiveBusinesses = inactiveUserCount;
      return View();
    }
    public IActionResult Add()
    {
      return View();
    }

    [HttpPost]
    public IActionResult BusAjaxRequest()
    {

      try
      {
        string userName = DashboardsController.getAccountName();

        if (string.IsNullOrEmpty(userName) || !AccountAuthentication.instance().isAccountLoggedIn(DashboardsController.getLoggedInState()))
        {
          //return Json(new { });
        }

        List<User> records = (from account in this._appDbContext.Users.Take(1000)
                                        select account).ToList();

        _logger.LogInformation("BusAjaxRequest action called.");
        _logger.LogInformation(string.Format("BusAjaxRequest records {0}.", records.Count()));
        var resJson = new
        {
          data = records//JsonSerializer.Serialize(records),
        };
        Console.WriteLine(resJson);
        //string jsonString = JsonSerializer.Serialize(weather);

        return Json(resJson);
      }
      catch (Exception ex)
      {
        throw;
      }
    }
    [HttpPost]
    public async Task<IActionResult> Add(UserDto request)
    {
     // TempData["Alert"] = "";
      var settings = await _appDbContext.Settings.FirstOrDefaultAsync();

      // Validation
      var validationErrors = new List<string>();
      if (settings?.Recaptcha == 1)
      {
        if (string.IsNullOrWhiteSpace(request.FirstName) || request.FirstName.Length > 255)
          validationErrors.Add("First name is required and must be less than 255 characters.");
        if (string.IsNullOrWhiteSpace(request.LastName) || request.LastName.Length > 255)
          validationErrors.Add("Last name is required and must be less than 255 characters.");
        if (string.IsNullOrWhiteSpace(request.IdNumber) || request.IdNumber.Length > 15)
          validationErrors.Add("ID number is required and must be less than 15 characters.");
        if (string.IsNullOrWhiteSpace(request.Town) || request.Town.Length < 3 || request.Town.Length > 25)
          validationErrors.Add("Town is required and must be between 3 and 25 characters.");
        // Add more validation as needed
      }

      if (validationErrors.Any())
        return BadRequest(new { Errors = validationErrors });

      // Format phone number
      string phoneNumber = request.Phone;
      if (phoneNumber.StartsWith("0"))
      {
        phoneNumber = "+263" + phoneNumber.Substring(1);
      }
      else
      {
        ViewBag.Message = "Mobile Number should start with a Zero";
        return View(request);
     //   return BadRequest("Mobile Number should start with a Zero");
      }

      // Check for duplicate email
      var existingUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
      //var existingUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email != null && u.Email == request.Email);

      if (existingUser != null)
      {
        //  return BadRequest("Duplicate Account Found");
        ViewBag.Message = "Duplicate Account Found";
        return View(request);
      }

      // Generate password and user code
      var password = Utils.Utilities.intCodeRandom(8);
      var userCode = Utils.Utilities.intCodeRandom(5);

      var now = DateTime.UtcNow;
      var monthAdded = Utils.Utilities.GetMonthJoinedDateTime(now);
      var dayAdded = Utils.Utilities.GetDayJoinedDateTime(now);

      var user = new User
      {
        Image = "person.png",
        FirstName = request.FirstName,
        LastName = request.LastName,
        BusinessName = request.BusinessName,
        PaymentType = request.PaymentType ?? "0",
        Country = "239",
        PaySupport = "6",
        Phone = phoneNumber,
        Email = request.Email,
        EmailVerify = true,
        FaStatus = 1,
        MainAdmin = 1,
        VerificationCode = Utils.Utilities.intCodeRandom(6).ToUpper(),
        EmailTime = DateTime.UtcNow.AddMinutes(5),
        Balance = "0",//settings?.BalanceReg,
        IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
        Password = DbServices.hashData(password),//BCrypt.Net.BCrypt.HashPassword(password),
        PublicKey = "PUB-" + Utils.Utilities.intCodeRandom(32),
        SecretKey = "SEC-" + Utils.Utilities.intCodeRandom(32),
        Town = request.Town,
        UserCode = userCode,
        ClientCode = int.Parse(Utils.Utilities.intCodeRandom(8)),
        //LastLogin = DateTime.UtcNow,
        LastLogin = DateTime.UtcNow.ToString("o"),
        LastChangePasswordAt = DateTime.UtcNow,
        Status = 0,
      };

      _appDbContext.Users.Add(user);
      await _appDbContext.SaveChangesAsync();

      // Create compliance record
    //  var compliance = new Compliance { UserId = user.Id };
     // _appDbContext.Compliances.Add(compliance);

      // Update company_id
      user.CompanyId = user.Id;
      await _appDbContext.SaveChangesAsync();

      // Send email if enabled
      if (settings?.EmailVerification == 1)
      {
        var text = $"Before you can start with Us, you need to confirm your email address. Your email verification code is {user.VerificationCode}";
        // EmailService.Send(user.Email, user.BusinessName, $"Welcome to {settings.SiteName}", text);
      }

      // Log audit
      var audit = new InAuthAudit
      {
        UserId = user.Id,//GetCurrentUserId(),
        Amount = 0,
        Ref = $"A New Business Account {request.Phone} ({user.Email}) has been created.",
        Main = 1,
        Type = GlobalConstants.CREATE_USER,
        StripeId = "0",
        Charge = "0"
      };
      _appDbContext.InAuthAudits.Add(audit);
      await _appDbContext.SaveChangesAsync();

      // Send SMS
      var message = $"A New Business {request.Phone} account has been created. Your New Password is {password}";
      //new SmsService().SendSMSNotificationAsync(phoneNumber, message);

      return RedirectToAction("List", "Businesses");
    }



  }
}
