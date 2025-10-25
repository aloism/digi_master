using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;
using AspnetCoreMvcFull.Services;
using AspnetCoreMvcFull.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Security.Claims;

namespace AspnetCoreMvcFull.Controllers;

public class AuthController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private AspnetCoreMvcFullContext _appDbContext;
  public const string SessionKeyResponse = "_SRQResponse";
  public AuthController(AspnetCoreMvcFullContext appDbContext, ILogger<HomeController> logger)
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
    return RedirectToAction("Index", "Dashboards");
  }
  public IActionResult ForgotPasswordBasic() => View();
  public IActionResult ForgotPasswordCover() => View();
  public IActionResult LoginBasic() => View();
  public async Task<IActionResult> LoginAccountAsync() {
    // DbServices _adminService = new DbServices(_appDbContext);
    // int newAdminId = await _adminService.CreateAdminAsync();

    TempData["Alert"] = "";
    return View();
  }
  public IActionResult RegisterBasic() => View();
  public IActionResult RegisterAccount(){

    TempData["Alert"] = "";
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> RegisterAccount(RegisterViewModel model)
  {
    if (!ModelState.IsValid)
      return View(model);

    var _user = _appDbContext.Admin.FirstOrDefault(a => a.Username == model.Username);
    if (_user != null)
    {
      TempData["Alert"] = "Oops! Duplicate account linked to credentials found";
      return View(model);
    }
    else
    {
      DbServices _adminService = new DbServices(_appDbContext);
      int newAdminId = await _adminService.SaveAdminAsync(model);

      if (newAdminId > 0)
      {
       
        // Create History record
        var refCode = Guid.NewGuid().ToString("N").Substring(0, 16);
        var history = new History
        {
          UserId = newAdminId,
          Amount = 0,
          Ref = refCode,
          Main = 1,
          Type = GlobalConstants.SELF_REG_ADMIN,
          StripeId = "0",
          Charge = 0,
          CreatedAt = DateTime.Now
        };
        _appDbContext.History.Add(history);

        // Create Audit record
        var audit = new Audit
        {
          UserId = newAdminId,
          Trx = refCode,
          Log = $"Reg IP: {HttpContext.Connection.RemoteIpAddress}, who: {model.Username}",
          CreatedAt = DateTime.Now
        };
        _appDbContext.Audits.Add(audit);

        await _appDbContext.SaveChangesAsync();

        return RedirectToAction("Dashboard", "Auth");
      }


    }
    TempData["Alert"] = "Oops! You have entered invalid credentials";
    return View(model);
  }
  public IActionResult RegisterResponse() => View();
  public IActionResult RegisterMultiSteps() => View();
  public IActionResult ResetPasswordBasic() => View();
  public IActionResult ResetPasswordCover() => View();
  public IActionResult TwoStepsBasic() => View();
  public IActionResult TwoStepsCover() => View();
  public IActionResult VerifyEmailBasic() => View();
  public IActionResult VerifyEmailCover() => View();

  [HttpPost]
  public async Task<IActionResult> LoginAccount(LoginViewModel model)
  {
    if (!ModelState.IsValid)
      return View(model);
    
    var rememberMe = model.RememberMe;

    var _user = _appDbContext.Admin.FirstOrDefault(a => a.Username == model.Username);
    if (_user == null)
    {
      TempData["Alert"] = "Oops! Your we could not find any linked credentials";
      return View(model);
    }
    else
    {

    var user = _appDbContext.Admin.FirstOrDefault(a => a.Username == model.Username && a.Status == 1);

    var passwordHasher = new PasswordHasher<object>();
    var passwordService = new PasswordHasherhandler(passwordHasher);

    //string plainTextPassword = "MySecretPassword123";

    //// Hash a password to simulate a password stored in the database.
    //string hashedPassword = passwordHasher.HashPassword(null, plainTextPassword);

    //// Verify the provided plain-text password against the hashed password.
    //bool isCorrect = passwordService.VerifyHashedPassword(hashedPassword, "MySecretPassword123");
    //Console.WriteLine($"Is the password correct? {isCorrect}");

    //bool isIncorrect = passwordService.VerifyHashedPassword(hashedPassword, "WrongPassword");
    //Console.WriteLine($"Is the wrong password correct? {isIncorrect}");

   // if (user != null && passwordService.VerifyHashedPassword(user.Password, model.Password))
    if (user != null && user.Password.Equals(model.Password))
    {
      var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("AdminId", user.Id.ToString())
        };

      var identity = new ClaimsIdentity(claims, "AdminLogin");
      var principal = new ClaimsPrincipal(identity);

      await HttpContext.SignInAsync("AdminScheme", principal, new AuthenticationProperties
      {
        IsPersistent = rememberMe
      });

      // Create History record
      var refCode = Guid.NewGuid().ToString("N").Substring(0, 16);
      var history = new History
      {
        UserId = user.Id,
        Amount = 0,
        Ref = refCode,
        Main = 1,
        Type = GlobalConstants.LOGIN_ADMIN,
        StripeId = "0",
        Charge = 0,
        CreatedAt = DateTime.Now
      };
      _appDbContext.History.Add(history);

      // Create Audit record
      var audit = new Audit
      {
        UserId = user.Id,
        Trx = refCode,
        Log = $"Login IP: {HttpContext.Connection.RemoteIpAddress}, who: {model.Username}",
        CreatedAt = DateTime.Now
      };
      _appDbContext.Audits.Add(audit);

      await _appDbContext.SaveChangesAsync();
      string role = AccountAuthentication.getAccountAdminRole(_appDbContext, user.Username);

      HttpContext.Session.SetString("fx_id", user.Id.ToString());
      HttpContext.Session.SetString("fx_key", user.Username);
      //HttpContext.Session.SetString("fx_email_key", user.Email == null ? "" : user.Email.TrimEnd());
      HttpContext.Session.SetString("fx_logged_in", Constants.IS_LOGGED_IN);
      HttpContext.Session.SetInt32("sessionRole", Int32.Parse(role));
      DashboardsController.updateSessionTimeOut();

      return RedirectToAction("Index", "Dashboards");
    }else if(user == null)
      {
        TempData["Alert"] = "Your Admin account has got issues detected, please contact system Admin.";
      } else
      {
        TempData["Alert"] = "Oops! You have entered invalid credentials";
      }


    }
    return View(model);
  }

}
