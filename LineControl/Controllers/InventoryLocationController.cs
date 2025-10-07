using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControllerCore.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class InventoryLocationController : Controller
  {
    private readonly IInventoryLocationService inventoryLocation;

    public InventoryLocationController(IInventoryLocationService inventoryLocation)
    {
      this.inventoryLocation = inventoryLocation;
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<JsonResult> GetInventoryLocation()
    {
      var company = await inventoryLocation.GetInventoryLocationAsync();
      return Json(company);
    }
  }
}
