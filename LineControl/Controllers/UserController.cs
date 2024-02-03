using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class UserController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public async Task<ActionResult> CreateUser()
    {
      return View();
    }

    public async Task<ActionResult> Edit()
    {
      return View();
    }
  }
}
