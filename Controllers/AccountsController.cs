using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Interfaces;
using AspnetCoreMvcFull.Interfaces.stub;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;
using AspnetCoreMvcFull.Services;
using AspnetCoreMvcFull.Utils;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace AspnetCoreMvcFull.Controllers
{
  public class AccountsController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private AspnetCoreMvcFullContext _appDbContext;
    private readonly IAccountService _accountService; // Custom service for DB logic
    private readonly IAuditService _auditService; // Custom service for logging/audit
    private readonly IAPIService _notificationService; 
    public AccountsController(AspnetCoreMvcFullContext appDbContext, ILogger<HomeController> logger, IAccountService accountService, IAuditService auditService, IAPIService notificationService)
    {
      _appDbContext = appDbContext;
      if (string.IsNullOrEmpty(DashboardsController.getAccountName()) || !AccountAuthentication.instance().isAccountLoggedIn(DashboardsController.getLoggedInState()))
      {
        //DashboardsController.redirectAuthIndex();
      }
      DashboardsController.updateSessionTimeOut();
      _logger = logger;
      _accountService = accountService;
      _auditService = auditService;
      _notificationService = notificationService;
    }
    public IActionResult Index()
    {
      return View();
    }
    public IActionResult Add()
    {
      var branches = _appDbContext.Branches.ToList() ?? new List<Branch>();
      ViewBag.BranchesList = new SelectList(branches, "Id", "Name");
      return View();
    }
    public IActionResult List()
    {
      var branches = _appDbContext.Branches.ToList() ?? new List<Branch>();
      ViewBag.BranchesList = new SelectList(branches, "Id", "Name");
      return View();
    }
    public async Task<IActionResult> ViewAccount(int id)
    {
      // The value '7' from the URL is automatically bound to the 'id' parameter.
      if (id == 0)
      {
        return NotFound(); // Handle case where ID might be missing or invalid
      }

      // Example: Fetch data using the ID
      // var account = await _userService.GetAccountDetails(id); 

      ViewBag.AccountId = id; // Example of passing the ID to the view

      return View();
    }

    public IActionResult History()
    {
      return View();
    }

    [HttpPost]
    public IActionResult AccsAjaxRequest()
    {

      try
      {
        string userName = DashboardsController.getAccountName();

        if (string.IsNullOrEmpty(userName) || !AccountAuthentication.instance().isAccountLoggedIn(DashboardsController.getLoggedInState()))
        {
          return Json(new { });
        }

        List<Accounts> records = (from account in this._appDbContext.Accounts.Take(1000)
                              select account).ToList();

        _logger.LogInformation("AccsAjaxRequest action called.");
        _logger.LogInformation(string.Format("AccsAjaxRequest records {0}.", records.Count()));
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
    [ValidateAntiForgeryToken] // Prevents Cross-Site Request Forgery (CSRF)
    public async Task<IActionResult> Add(AddAccountFormModel model)
    {
//    }
//    using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using System.Threading.Tasks;
//using System.Security.Claims; // For getting current user claims (like user id)

//[Authorize] // Requires an authenticated user
//  public class AccountController : Controller
//  {
//    private readonly UserManager<ApplicationUser> _userManager;
//    private readonly SignInManager<ApplicationUser> _signInManager;

//    // Dependency Injection via constructor
//    public AccountController(
//        UserManager<ApplicationUser> userManager,
//        SignInManager<ApplicationUser> signInManager,
//        IAccountService accountService,
//        IAuditService auditService)
//    {
//      _userManager = userManager;
//      _signInManager = signInManager;
//      _accountService = accountService;
//      _auditService = auditService;
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken] // Prevents Cross-Site Request Forgery (CSRF)
//    public async Task<IActionResult> CreateAccount(CreateAccountViewModel model)
//    {
      // --- 1. Basic Model Validation (replaces some PHP validation checks) ---
      //if (!ModelState.IsValid)
      //{
      //  // Use TempData to pass an alert message on redirect (like PHP's ->with('alert', ...))
      //  ViewBag.Message = "Invalid submission data.";
      //  return View(model); // Or return RedirectToAction("Index");
      //}

      // --- 2. Authorization Check (replaces if(Auth::user()->add_staff == 0)) ---
      // Assuming your ApplicationUser model has an 'AddStaff' property (like your PHP `add_staff` field)
      var user = await _accountService.GetAdminUserAsync(DashboardsController.getAccountName());
      //if (user == null || user.AddStaff == 0)//todo enable this check after testing
      //{
      //  TempData["Alert"] = "Not Allowed";
      //  return RedirectToAction("Accounts", "Admin"); // Or a more appropriate fallback
      //}

      // --- 3. Account Type Validation (PHP checks) ---
      // Note: The AccountType property in the ViewModel now uses [RegularExpression]
      // This check is mostly covered by the ViewModel's validation, but included for logic flow
      if (model.AccountType == 0 || (model.AccountType != 1 && model.AccountType != 2 && model.AccountType != 3))
      {
       // TempData["Alert"] = "Please Select a valid Account option";
        ViewBag.Message = "Please Select a valid Account option";
        return View(model); // Or return RedirectToAction("Index");
      }
      if (model.Currency == "")
      {
        ViewBag.Message = "Please Select a valid Currency option";
        return View(model);
      }
      var existingUser = await _appDbContext.Branches.FirstOrDefaultAsync(b => b.Id == model.SelectedBranchId);
      if (existingUser == null)
      {
        ViewBag.Message = "Business not found, Please Add Business to continue.";
        return View(model); // Or return RedirectToAction("Index");
      }

      // --- 4. Determine Account Details (Replaces PHP setup logic) ---
      int productId = -2023;
      string currencyCode = "$";
      string accountName = "Control Account";

      switch (model.AccountType)
      {
        case 1:
          productId = -2024;
          accountName = "Commission Account";
          break;
        case 2:
          productId = -2025;
          accountName = "General Account";
          break;
        case 0:
        default:
          // Defaults are already set
          break;
      }

      if (!string.IsNullOrEmpty(model.AccountName))
      {
        accountName = model.AccountName;
      }

      // --- 5. Inventory Checks (Replaces PHP $request->inv_amount > 0 block) ---
      if (model.InvAmount > 0)
      {
        // Placeholder for service call to check inventory
        var inventoryCheckResult = await _accountService.CheckInventoryAsync(productId, model.InvAmount);

        if (!inventoryCheckResult.IsSuccessful)
        {
          ViewBag.Message = inventoryCheckResult.Message;
          return View(model);
        }
      }

      // --- 6. Re-Authentication (Replaces PHP Auth::guard('admin')->attempt) ---
      //var reauthResult = await _accountService.CheckPasswordSignInAsync(DashboardsController.getAccountName(), model.AuthCode);

      //if (!reauthResult)
      //{
      //  ViewBag.Message = "Oops! You have entered invalid credentials";
      //  return View(model);
      //}

      // --- 7. Audit Log (Replaces PHP Audit::create) ---
      // Get user IP address (equivalent to user_ip() in PHP)
      var userIp = HttpContext.Connection.RemoteIpAddress.ToString();
      var auditMessage = $"Createaccount Auth IP:{userIp}, InvOptn:{model.AccountType}, Amount:{model.InvAmount}";

      await _auditService.LogAuditAsync(user.Id, "AI-" + WebDataService.GenerateRandomString(6), auditMessage);

      // --- 8. Business Logic and Database Operations ---
      // The complex logic involving InInventory, InAccounts, History, etc., should 
      // be moved into a dedicated service layer (`IAccountService`).

      var serviceResult = await _accountService.ProcessAccountCreationAsync(
          user.Id,
          model,
          productId,
          currencyCode,
          accountName,
          userIp
      );

      // --- 9. Final Redirect and Notification ---
      if (serviceResult.IsSuccessful)
      {
        // Use TempData["Success"] for success messages
        TempData["SuccessMessage"] = serviceResult.Message;

        // Placeholder for SMS/Notification service call (replaces $obj_process->sendSMSNotification)
         await _notificationService.SendSMSNotificationAsync(GlobalConstants.SMS_NOTIF_ONE, serviceResult.NotificationMessage);

        return RedirectToAction("List", "Accounts");
      }

      // If the service logic fails for any unhandled reason
      ViewBag.Message = serviceResult.Message ?? "Request failed, Please Try Again.";
      return View(model);
     /// return RedirectToAction("Add", "Accounts");
    }


// --- Placeholder for necessary service interfaces and models ---

// public interface IAccountService
// {
//     Task<ServiceResult> CheckInventoryAsync(int productId, decimal amount);
//     Task<ServiceResult> ProcessAccountCreationAsync(int userId, CreateAccountViewModel model, int productId, string currencyCode, string accountName, string userIp);
// }

// public class ApplicationUser : IdentityUser 
// {
//     public int AddStaff { get; set; } // Matches PHP auth check
// }

// public class ServiceResult
// {
//     public bool IsSuccessful { get; set; }
//     public string Message { get; set; }
//     public string NotificationMessage { get; set; }
// }
      // POST: /Accounts/Add
      // Accepts JSON payload from app-accounts-list.js and creates AccountBalance record.
      [HttpPost]
    public async Task<IActionResult> AjaxSubmit([FromBody] AddAccountFormModel request)
    {
      if (request == null)
        return BadRequest(new { success = false, message = "Invalid request payload." });

      if (string.IsNullOrWhiteSpace(request.AccountName))
        return BadRequest(new { success = false, message = "AccountName is required." });

      try
      {
        // Parse account type
        int accountType = 0;
        if (!string.IsNullOrWhiteSpace(request.AccountType.ToString()))
          int.TryParse(request.AccountType.ToString(), out accountType);

        var now = DateTime.UtcNow;
        // 1. Setup Utility Variables (Date/Time)
        var dateParts = AccountService.GetDateParts(now);

        // Create AccountBalance record with reasonable defaults for required columns.
        //var account = new Accounts
        //{
        //  CompanyId = 0,
        //  CabinId = 0,
        //  ProductId = 0,
        //  Name = ,
        //  AccountNumber = "ACC-" + Utils.Utilities.intCodeRandom(8),
        //  BalanceType = accountType,
        //  Balance = 0m,
        //  Currency = "$",
        //  CreatedAt = now,
        //  UpdatedAt = now,
        //  MonthAdded = Utils.Utilities.GetMonthJoinedDateTime(now),
        //  DayAdded = Utils.Utilities.GetDayJoinedDateTime(now),
        //  RequestIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0",
        //  Status = 1
        //};
        var newAccount = new Accounts
        {
          ParentId = 0,
          //ProductId = productId,
          Name = request.AccountName,
          Amount = request.InvAmount,
          AccountType = request.AccountType,
          AccountCategory = 1,
          MinRate = "0",//model.MinAccRate,
          MaxRate = "0", // Hardcoded '0' as in PHP
          Currency = request.Currency,
          MonthAdded = dateParts.MonthAdded,
          DayAdded = dateParts.DayAdded,
          RateDayAdded = Utils.Utilities.GetDayJoinedDateTime(now),
          RequestIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0",
          CreatedAt = DateTime.UtcNow,
          Status = 1
        };
        _appDbContext.Accounts.Add(newAccount);
        await _appDbContext.SaveChangesAsync();

        _logger.LogInformation("Account created. Id: {Id}, Name: {Name}", newAccount.Id, newAccount.Name);

        return Json(new { success = true, id = newAccount.Id, message = "Account created successfully." });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating account");
        return StatusCode(500, new { success = false, message = "Server error while creating account." });
      }
    }

  }
  }
