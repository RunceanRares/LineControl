using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class CalibrationOrderController : Controller
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
