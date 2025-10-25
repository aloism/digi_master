using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;
using AspnetCoreMvcFull.Utils;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Controllers
{
  public class BranchesController : Controller
  {
    private readonly ILogger<ApiController> _logger;
    private readonly AspnetCoreMvcFullContext _appDbContext;

    public BranchesController(ILogger<ApiController> logger, AspnetCoreMvcFullContext context)
    {
      _logger = logger;
      _appDbContext = context;
    }
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public IActionResult BranchesAjaxRequest()
    {

      try
      {
        string userName = DashboardsController.getAccountName();

        if (string.IsNullOrEmpty(userName) || !AccountAuthentication.instance().isAccountLoggedIn(DashboardsController.getLoggedInState()))
        {
          //return Json(new { });
        }

        List<Branch> records = (from account in this._appDbContext.Branches.Take(1000)
                              select account).ToList();

        _logger.LogInformation("BranchesAjaxRequest action called.");
        _logger.LogInformation(string.Format("BranchesAjaxRequest records {0}.", records.Count()));
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


    public IActionResult List()
    {
      ViewBag.Branches = _appDbContext.Branches.Count();
      int activeCount = _appDbContext.Branches.Count(u => u.Status == 1);
      ViewBag.Active = activeCount;
      int pendingCount = _appDbContext.Branches.Count(u => u.Status == 0);
      ViewBag.Pending = pendingCount;
      int inactiveCount = _appDbContext.Branches.Count(u => u.Status == 2);
      ViewBag.Inactive = inactiveCount;
      var businesses = _appDbContext.Users.ToList() ?? new List<User>();
      ViewBag.BusinessesList = new SelectList(businesses, "Id", "BusinessName");
      return View();
    }
    public IActionResult Add()
    {
      var businesses = _appDbContext.Users.ToList() ?? new List<User>();
      ViewBag.BusinessesList = new SelectList(businesses, "Id", "BusinessName");
      return View();
    }
    public async Task<IActionResult> ApproveBranch(int id)
    {
      if (id == 0)
      {
        TempData["InfoMessage"] = "Branch not existing.";
        return RedirectToAction("List", "Branches");
      //  return NotFound(); // Handle case where ID might be missing or invalid
      }
      var branch = await _appDbContext.Branches.FirstOrDefaultAsync(b => b.Id == id);
      if (branch == null)
      {
        TempData["ErrorMessage"] = "Branch not existing.";
        return RedirectToAction("List", "Branches");
      }

      TempData["SuccessMessage"] = "Branch was successfully Approved.";
      return RedirectToAction("List", "Branches");
      //return View();
    }
    public async Task<IActionResult> ViewBranch(int id)
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

    [HttpPost]
    public async Task<IActionResult> Add(BranchDto request)
    {
      if (string.IsNullOrWhiteSpace(request.Name))
      {
        ViewBag.Message = "Branch name should be entered";
        //return RedirectToAction("Create");
        return View(request);
      }

      if (request.Name.Length < 4)
      {
        ViewBag.Message = "Branch name should not be too short.";
        //return RedirectToAction("Create");
        return View(request);
      }
      var existingUser = await _appDbContext.Users.FirstOrDefaultAsync(b => b.Id == request.SelectedBusinessId);

      if (existingUser == null)
      {
        ViewBag.Message = "Business not found, Please Add Business to continue.";
        return View(request);
      }
      //if (string.IsNullOrWhiteSpace(request.CabinNumber))
      //{
      //  ViewBag.Message = "Cabin Number should be entered";
      //  return RedirectToAction("Create");
      //}

      //if (request.CabinNumber.Length < 4)
      //{
      //  ViewBag.Message = "Cabin Number should not be too short. Only 6 digits allowed.";
      //  return RedirectToAction("Create");
      //}

      //if (request.FloatBalance > 0)
      //{
      //  var inventory = await _context.InInventories.FirstOrDefaultAsync(i => i.ProductId == -2023);
      //  if (inventory == null)
      //  {
      //    ViewBag.Message = "Please Notify Admin, Main Inventory Float not Declared.";
      //    return RedirectToAction("Create");
      //  }

      //  if (inventory.Amount <= 0 || inventory.FloatBalance < request.FloatBalance)
      //  {
      //    ViewBag.Message = "Please Notify Admin, Float Inventory not sufficient.";
      //    return RedirectToAction("Create");
      //  }
      //}
      var cabinNumber = Utils.Utilities.intCodeRandom(8);

    //  var  = await _appDbContext.Branches.Where(c => c.Name == request.Name).ToListAsync();
      var existingCabinByName = await _appDbContext.Branches.FirstOrDefaultAsync(b => b.Name == request.Name);
    //  var  = await _appDbContext.Branches.Where(c => c.AccountNumber == cabinNumber).ToListAsync();
      var existingCabinByNumber = await _appDbContext.Branches.FirstOrDefaultAsync(b => b.AccountNumber == cabinNumber);

      if (existingCabinByName == null && existingCabinByNumber == null)
      {
        var now = DateTime.UtcNow;
        var monthAdded = Utils.Utilities.GetMonthJoinedDateTime(now);
        var dayAdded = Utils.Utilities.GetDayJoinedDateTime(now);

        var cabin = new Branch
        {
          ApprovedId = 0,
          Name = request.Name,
          OwnerId = request.SelectedBusinessId,
          CompanyId = request.SelectedBusinessId,
          PhysicalLocation = request.PhysicalLocationName,
          AccountNumber = cabinNumber,
          Currency = "$",
          Balance = "0",//request.FloatBalance.ToString("F2"),
          MonthAdded = monthAdded,
          DayAdded = dayAdded,
          RequestIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
          CreatedAt = now,
          Status = 0,
          Location = request.GeoLocation,
          Town = request.Town
        };

        _appDbContext.Branches.Add(cabin);
        await _appDbContext.SaveChangesAsync();

        var history = new History
        {
          UserId = DashboardsController.getUserId(), // Replace with your user ID logic
          Amount = 0,//request.FloatBalance.ToString("F2"),
          Ref = request.Name,
          Main = 1,
          Type = GlobalConstants.CREATE_BRANCH,
          StripeId = "0",
          Charge = 0
        };

        _appDbContext.History.Add(history);
        await _appDbContext.SaveChangesAsync();

        var savedCabin = await _appDbContext.Branches.FirstOrDefaultAsync(c => c.AccountNumber == cabinNumber);

        var audit = new Audit
        {
          UserId = savedCabin.Id,
          Trx = Guid.NewGuid().ToString("N").Substring(0, 16),
          Log = $"FLOAT on Create ${cabin.Balance}, Branch: {request.Name}",
          CreatedAt = now,
          UpdatedAt = now
        };

        _appDbContext.Audits.Add(audit);
        await _appDbContext.SaveChangesAsync();

        //if (request.FloatBalance > 0)
        //{
        //  var prevBalance = inventory.Amount;
        //  var newBalance = prevBalance - request.FloatBalance;

        //  inventory.Amount = newBalance;
        //  _context.InInventories.Update(inventory);
        //  await _context.SaveChangesAsync();

        //  var inventoryHistory = new InInventoryHistory
        //  {
        //    UserId = inventory.Id,
        //    ProductId = -2023,
        //    CabinId = savedCabin.Id,
        //    Amount = "-" + request.FloatBalance.ToString("F2"),
        //    NewBalance = newBalance.ToString("F2"),
        //    MonthAdded = monthAdded,
        //    DayAdded = dayAdded,
        //    RequestIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
        //    CreatedAt = now,
        //    Status = 1
        //  };

        //  _context.InInventoryHistory.Add(inventoryHistory);
        //  await _context.SaveChangesAsync();
        //}

        ViewBag.Message = "Branch was successfully saved.";
        return RedirectToAction("List", "Branches");
      }
      else
      {
        ViewBag.Message = "Branch already saved";
        return View(request);
      }
    }

  }
}
