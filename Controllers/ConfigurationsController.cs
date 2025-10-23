using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers
{
  public class ConfigurationsController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
    public IActionResult Products()
    {
      return View();
    }
    public IActionResult Categories()
    {
      return View();
    }
  }
}
