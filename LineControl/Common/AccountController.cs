using Microsoft.AspNetCore.Mvc;

namespace LineControl.Common
{
  public class AccountController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
