using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;
using AspnetCoreMvcFull.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;

namespace AspnetCoreMvcFull.Controllers
{
  public class BranchesController : Controller
  {
    private readonly ILogger<ApiController> _logger;
    private readonly AspnetCoreMvcFullContext _context;

    public BranchesController(ILogger<ApiController> logger, AspnetCoreMvcFullContext context)
    {
      _logger = logger;
      _context = context;
    }
    public IActionResult Index()
    {
      return View();
    }
    public IActionResult List()
    {
      return View();
    }
    public IActionResult Add()
    {
      var businesses = _context.Users.ToList() ?? new List<User>();
      ViewBag.BusinessesList = new SelectList(businesses, "Id", "BusinessName");
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(BranchDto request)
    {
      if (string.IsNullOrWhiteSpace(request.Name))
      {
        TempData["alert"] = "Branch name should be entered";
        //return RedirectToAction("Create");
        return View(request);
      }

      if (request.Name.Length < 4)
      {
        TempData["alert"] = "Branch name should not be too short.";
        //return RedirectToAction("Create");
        return View(request);
      }

      //if (string.IsNullOrWhiteSpace(request.CabinNumber))
      //{
      //  TempData["alert"] = "Cabin Number should be entered";
      //  return RedirectToAction("Create");
      //}

      //if (request.CabinNumber.Length < 4)
      //{
      //  TempData["alert"] = "Cabin Number should not be too short. Only 6 digits allowed.";
      //  return RedirectToAction("Create");
      //}

      //if (request.FloatBalance > 0)
      //{
      //  var inventory = await _context.InInventories.FirstOrDefaultAsync(i => i.ProductId == -2023);
      //  if (inventory == null)
      //  {
      //    TempData["alert"] = "Please Notify Admin, Main Inventory Float not Declared.";
      //    return RedirectToAction("Create");
      //  }

      //  if (inventory.Amount <= 0 || inventory.FloatBalance < request.FloatBalance)
      //  {
      //    TempData["alert"] = "Please Notify Admin, Float Inventory not sufficient.";
      //    return RedirectToAction("Create");
      //  }
      //}
      var cabinNumber = Utils.Utilities.intCodeRandom(8);

      var existingCabinByName = await _context.Branches.Where(c => c.Name == request.Name).ToListAsync();
      var existingCabinByNumber = await _context.Branches.Where(c => c.AccountNumber == cabinNumber).ToListAsync();

      if (!existingCabinByName.Any() && !existingCabinByNumber.Any())
      {
        var now = DateTime.UtcNow;
        var monthAdded = Utils.Utilities.GetMonthJoinedDateTime(now);
        var dayAdded = Utils.Utilities.GetDayJoinedDateTime(now);

        var cabin = new Branch
        {
          Name = request.Name,
          PhysicalLocation = request.PhysicalLocationName,
          AccountNumber = cabinNumber,
          Balance = "0",//request.FloatBalance.ToString("F2"),
          MonthAdded = monthAdded,
          DayAdded = dayAdded,
          RequestIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
          CreatedAt = now,
          Status = 0,
          Location = request.GeoLocation
        };

        _context.Branches.Add(cabin);
        await _context.SaveChangesAsync();

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

        _context.History.Add(history);
        await _context.SaveChangesAsync();

        var savedCabin = await _context.Branches.FirstOrDefaultAsync(c => c.AccountNumber == cabinNumber);

        var audit = new Audit
        {
          UserId = savedCabin.Id,
          Trx = Guid.NewGuid().ToString("N").Substring(0, 16),
          Log = $"FLOAT on Create ${cabin.Balance}, Branch: {request.Name}",
          CreatedAt = now,
          UpdatedAt = now
        };

        _context.Audits.Add(audit);
        await _context.SaveChangesAsync();

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

        TempData["success"] = "Branch was successfully saved.";
        return RedirectToAction("List", "Branches");
      }
      else
      {
        TempData["alert"] = "Branch already saved";
        return View(request);
      }
    }

  }
}
