using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;
using AspnetCoreMvcFull.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Controllers
{
  public class AccountsController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private AspnetCoreMvcFullContext _appDbContext;
    public AccountsController(AspnetCoreMvcFullContext appDbContext, ILogger<HomeController> logger)
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
    public IActionResult Add()
    {
      return View();
    }
    public IActionResult List()
    {
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

        List<AccountBalance> records = (from account in this._appDbContext.AccountBalances.Take(1000)
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

    // POST: /Accounts/Add
    // Accepts JSON payload from app-accounts-list.js and creates AccountBalance record.
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AccountCreateRequest request)
    {
      if (request == null)
        return BadRequest(new { success = false, message = "Invalid request payload." });

      if (string.IsNullOrWhiteSpace(request.AccountName))
        return BadRequest(new { success = false, message = "AccountName is required." });

      try
      {
        // Parse account type
        int accountType = 0;
        if (!string.IsNullOrWhiteSpace(request.AccountType))
          int.TryParse(request.AccountType, out accountType);

        var now = DateTime.UtcNow;

        // Create AccountBalance record with reasonable defaults for required columns.
        var account = new AccountBalance
        {
          CompanyId = 0,
          CabinId = 0,
          ProductId = 0,
          Name = request.AccountName,
          AccountNumber = "ACC-" + Utils.Utilities.intCodeRandom(8),
          BalanceType = accountType,
          Balance = 0m,
          Currency = "$",
          CreatedAt = now,
          UpdatedAt = now,
          MonthAdded = Utils.Utilities.GetMonthJoinedDateTime(now),
          DayAdded = Utils.Utilities.GetDayJoinedDateTime(now),
          RequestIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0",
          Status = 1
        };

        _appDbContext.AccountBalances.Add(account);
        await _appDbContext.SaveChangesAsync();

        _logger.LogInformation("Account created. Id: {Id}, Name: {Name}", account.Id, account.Name);

        return Json(new { success = true, id = account.Id, message = "Account created successfully." });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating account");
        return StatusCode(500, new { success = false, message = "Server error while creating account." });
      }
    }

  }
  }
