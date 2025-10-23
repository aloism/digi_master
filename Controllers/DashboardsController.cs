using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspnetCoreMvcFull.Controllers;

public class DashboardsController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private AspnetCoreMvcFullContext _appDbContext;
  public const string SessionKeyResponse = "_SRQResponse";
  public DashboardsController(AspnetCoreMvcFullContext appDbContext, ILogger<HomeController> logger)
  {
    _appDbContext = appDbContext;
    if (string.IsNullOrEmpty(DashboardsController.getAccountName()) || !AccountAuthentication.instance().isAccountLoggedIn(DashboardsController.getLoggedInState()))
    {
      redirectAuthIndex();
    }
    DashboardsController.updateSessionTimeOut();
    _logger = logger;
  }
  public IActionResult redirectAuthIndex()
  {
    return RedirectToAction("Index", "Auth");
  }

  public IActionResult redirectToAdmin()
  {
    return RedirectToAction("Admin", "Dashboards");
  }
  public IActionResult Index()
  {

    string userName = getAccountName();

    if (string.IsNullOrEmpty(userName) || !AccountAuthentication.instance().isAccountLoggedIn(getLoggedInState()))
    {
     // return redirectAuthIndex();
    }
    var _lastDate = "2025-08-03 00:00:00";//yyyy-mm-dd hh:mm:ss
    DateTime lastDate = DateTime.Parse(_lastDate);
    ViewBag.LastDate = _lastDate;
    //var record = _appDbContext.RBZ_Report.FirstOrDefault(e => e.DATE == lastDate);
    //if (record != null)
    //{

    //}
    //ViewData["record"] = record;
    return View();
  }
  void saveSSData(string sessionKey, string sessionData)
  {
    if (string.IsNullOrEmpty(sessionData)) { } else { HttpContext.Session.SetString(sessionKey, sessionData); }
  }

  public IActionResult AddUser()
  {
    return View();
  }

  public static string getLoggedInState()
  {
    string isLoggedIn = MainHelper._httpContext.Session.GetString("fx_logged_in");
    return isLoggedIn;
  }
  public IActionResult CRM() => View();

  public static string getAccountName()
  {
    //  string userName = Session.GetDataFromSession<string>("fx_key");
    string userName = MainHelper._httpContext.Session.GetString("fx_key");
    // string fx_dt = Session.GetDataFromSession<string>("fx_dt");
    var fx_dt = MainHelper._httpContext.Session.GetString("fx_dt");
    var dt = AspnetCoreMvcFull.Utils.Utilities.getDTimeFromStrimg(fx_dt);
    var dtNow = DateTime.Now;
    TimeSpan ts = dtNow - dt;

    if (ts.TotalMinutes > 10)
    {
      userName = "";
    }
    return userName;
  }

  public static void updateSessionAuth(string TkAuth)
  {
    DateTime aDate = DateTime.UtcNow;
    MainHelper._httpContext.Session.SetString("access_token", TkAuth);
    MainHelper._httpContext.Session.SetString("access_token_dt", aDate.ToString("yyyy-MM-dd HH:mm:ss"));
  }

  public static void updateSessionTimeOut()
  {
    DateTime aDate = DateTime.Now;
    // Session.SetDataToSession<string>("fx_dt", aDate.ToString("yyyy-MM-dd HH:mm:ss"));

    //if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
    //{
    MainHelper._httpContext.Session.SetString("fx_dt", aDate.ToString("yyyy-MM-dd HH:mm:ss"));
    //  HttpContext.Session.SetInt32(SessionKeyAge, 73);
    //}
  }

}
