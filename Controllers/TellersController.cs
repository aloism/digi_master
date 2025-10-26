using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Interfaces;
using AspnetCoreMvcFull.Interfaces.stub;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;
using AspnetCoreMvcFull.Services;
using AspnetCoreMvcFull.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Controllers
{
  public class TellersController : Controller
  {
    private readonly ILogger<ApiController> _logger;
    private readonly AspnetCoreMvcFullContext _appDbContext;
    private readonly IAccountService _accountService; // Custom service for DB logic
    private readonly IAPIService _notificationService;

    public TellersController(ILogger<ApiController> logger, AspnetCoreMvcFullContext context, IAccountService accountService, IAPIService notificationService)
    {
      _logger = logger;
      _appDbContext = context;
      _accountService = accountService;
      _notificationService = notificationService;
    }
    public IActionResult Index()
    {
      return View();
    }
    public IActionResult List()
    {
      var branches = _appDbContext.Branches.ToList() ?? new List<Branch>();
      ViewBag.BranchesList = new SelectList(branches, "Id", "Name");
      return View();
    }
    public IActionResult Add()
    {
      var branches = _appDbContext.Branches.ToList() ?? new List<Branch>();
      ViewBag.BranchesList = new SelectList(branches, "Id", "Name");
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddCashierFormModel request)
    {
      var currentUser = await _accountService.GetUserLevelAsync(DashboardsController.getUserId());
      if (currentUser.AddCashier == 0)
      {
        TempData["ErrorMessage"] = "Not Allowed";
        //return RedirectToAction("Index");
        return View(request);
      }

      string phoneNumber = request.Phone;
      if (phoneNumber.StartsWith("0"))
      {
        phoneNumber = "+263" + phoneNumber.Substring(1);
      }
      else
      {
        ViewBag.Message = "Mobile Number should start with a Zero";
        return View(request);
      }

      var existingBranch = await _appDbContext.Branches.FirstOrDefaultAsync(t => t.Id == request.SelectedBranchId);
      if (existingBranch == null)
      {
        ViewBag.Message = "No Existing Branch found, Please verify your details.";
        return View(request);
      }

      var existingTrader = await _appDbContext.Cashiers.FirstOrDefaultAsync(t => t.Phone == phoneNumber);
      if (existingTrader != null)
      {
        ViewBag.Message = "Trader Existing Mobile Number found.";
        return View(request);
      }

      var existingAgents = await _appDbContext.Cashiers.Where(a => a.Phone == phoneNumber).ToListAsync();
      if (existingAgents.Count < 1)
      {
        //var objProcess = new ServicesRequests();
        string password = Utils.Utilities.intCodeRandom(4);
        var passwordHasher = new PasswordHasher<object>();
        var passwordService = new PasswordHasherhandler(passwordHasher);
        string hashedPassword = passwordHasher.HashPassword(null, password);

        var user = new Cashier
        {
          Image = "person.png",
          BranchId=existingBranch.Id,
          AccountType = request.AccountType,
          FirstName = request.FirstName,
          LastName = request.LastName,
          IdNumber = request.IdNumber,
          BusinessName = "",//request.Town,
          Country = "239",
          Phone = phoneNumber,
          Email = string.IsNullOrEmpty(request.Email) ? "info@builkit.co.zw" : request.Email,
          EmailVerify = false,
          VerificationCode = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper(),
          EmailTime = DateTime.UtcNow.AddMinutes(5),
          Balance = "0",
          IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
          Password = hashedPassword,//BCrypt.Net.BCrypt.HashPassword(password),
          LastLogin = DateTime.UtcNow,
          Expenses = 0,
          RequestMoney = 0,
          Cashbook = 1,
          //CabinId = "0",
          SupervisorAuth = 0
        };

        var result = CreateDriverAccount(user);
        if (result["resultStatus"] == "000" || result["resultStatus"] == "184")
        {
          await _appDbContext.Cashiers.AddAsync(user);
          await _appDbContext.SaveChangesAsync();

          string message = $"A New Cashier {request.Phone} account has been created. Your New PIN is {password}";

          var audit = new InAuthAudit
          {
            UserId = (int)currentUser.AdminId,
            Amount = 0,
            Ref = message,
            Main = 1,
            Type = GlobalConstants.CREATE_CASHIER,
            StripeId = "0",
            Charge ="0"
          };
          await _appDbContext.InAuthAudits.AddAsync(audit);
          await _appDbContext.SaveChangesAsync();

          await _notificationService.SendSMSNotificationAsync(phoneNumber, message);

          TempData["SuccessMessage"] = "Cashier was successfully created";
          return RedirectToAction("List", "Tellers");
        }
        else if (result["resultStatus"] == "014")
        {
          ViewBag.Message = "Cashier Account Creation Failed, Duplicate Account found.";
          //return RedirectToAction("Index");
          return View(request);
        }
        else
        {
          ViewBag.Message = "Cashier Account Creation Failed, Please Try again.";
          return View(request);
        }
      }
      else
      {
        ViewBag.Message = "Username already taken";
        return View(request);
      }
    }

    public Dictionary<string, string> CreateDriverAccount(Cashier user)
    {
      _logger.LogInformation("createDriverAccount");

      // Ensure phone number starts with "+"
      if (!user.Phone.StartsWith("+"))
      {
        user.Phone = "+" + user.Phone;
      }

      // Set UTC timezone and get current date
      var date = DateTime.UtcNow;

      // Assuming Utils is a helper class with these methods
     // var util = new Utils();
      var monthAdded = Utils.Utilities.GetMonthJoinedDateTime(date);
      var dayAdded = Utils.Utilities.GetDayJoinedDateTime(date);

      // Generate request ID
      string requestId = WebDataService.GenerateRandomString(5);

      // Prepare response
      var response = new Dictionary<string, string>
      {
        ["resultStatus"] = "000",
        ["res_message"] = "Request Successful"
      };

      return response;
    }

  }
}
