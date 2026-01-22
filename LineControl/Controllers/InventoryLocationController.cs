using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControllerCore.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class InventoryLocationController : Controller
  {
    private readonly IInventoryLocationService service;

    public InventoryLocationController(IInventoryLocationService inventoryLocation)
    {
      this.service = inventoryLocation;
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<JsonResult> GetInventoryLocation()
    {
      var company = await service.GetInventoryLocationAsync().ConfigureAwait(false);
      return Json(company);
    }
  }
}
