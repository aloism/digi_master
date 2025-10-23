using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers
{
  public class SettingsController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
    public IActionResult ProfileUser()
    {
      return View();
    }
    public IActionResult SettingsSecurity()
    {
      return View();
    }
    public IActionResult SettingsBilling()
    {
      return View();
    }
  }
}
