using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class DeviceController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
