using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class ActivityTypeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public async Task<ActionResult> Create()
    {
      return View();
    }

    public async Task<ActionResult> Edit()
    {
      return View();
    }
  }
}
