using LineControllerCore.Interface;

using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class InventoryLocation : Controller
  {
    private readonly IInventoryLocationService service;

    private readonly IUserService userService;

    public IActionResult Index()
    {
      return View();
    }
  }
}
