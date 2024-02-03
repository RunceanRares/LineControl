using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class DeviceController : Controller
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

    public async Task<ActionResult> History()
    {
      return View();
    }

    public async Task<ActionResult> Reservation()
    {
      return View();
    }
  }
}
