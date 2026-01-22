using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControllerCore.Interface;
using LineControllerCore.Model;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace LineControl.Controllers
{
  public class DeviceClassModeController : Controller
  {
    private readonly IDeviceClassMode service;

    public DeviceClassModeController(IDeviceClassMode service)
    {
      this.service = service;
    }

    public IActionResult Index()
    {
      return View();
    }

    public ActionResult GetDeviceClassMode([DataSourceRequest] DataSourceRequest request)
    {
      var device = service.GetDeviceClassModeAsync();
      return Json(device.ToList().ToDataSourceResult(request));
    }

    public ActionResult EditDeviceClassMode(int id)
    {
      var model = service.GetDeviceClassModelById(id);
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditDeviceClassMode(DeviceClassModeViewModel model)
    {
      if (ModelState.IsValid)
      {
        service.UpdateDeviceClassModel(model);
        return RedirectToAction("Index");
      }
      else
      {
        return View(model);
      }
    }

    public ActionResult GetDeviceMode([DataSourceRequest] DataSourceRequest request)
    {
      var device = service.GetDeviceMode();
      return Json(device.ToList().ToDataSourceResult(request));
    }

    public ActionResult EditDeviceMode(int id)
    {
      var model = service.GetDeviceModelById(id);

      // Obține numele utilizatorului curent
      var currentUserName = User.Identity.Name;

      // Setează numele utilizatorului curent în model
      model.LastChangeUser = currentUserName;

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditDeviceMode(DeviceModelViewModel model)
    {
      if (ModelState.IsValid)
      {
        model.LastChangeUser = User.Identity.Name;
        service.UpdateDeviceModel(model);
        return RedirectToAction("Index");
      }
      else
      {
        return View("Index");
      }
    }

    public ActionResult CreateDeviceMode()
    {
      var model = new DeviceModelViewModel();
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDeviceMode(DeviceModelViewModel model)
    {
      if (ModelState.IsValid)
      {
        // 2. Adaugă "await" aici!
        await service.AddDeviceMode(model);
        return RedirectToAction("Index");
      }
      else
      {
        return View("Index");
      }
    }

    public ActionResult CreateDeviceClassMode()
    {
      var model = new DeviceClassModeViewModel();
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDeviceClassMode(DeviceClassModeViewModel model)
    {
      if (ModelState.IsValid)
      {
        // 2. Adaugă "await" aici!
        await service.AddDeviceClassMode(model);
        return RedirectToAction("Index");
      }
      else
      {
        return View("Index");
      }
    }

    [HttpGet]
    public ActionResult GetLastUser()
    {
      var currentUserName = User.Identity.Name ?? "Unknown";
      var result = new[]
      {
        new
        {
            Id = currentUserName,
            LastChangeUser = currentUserName
        }
    };
      return Json(result);
    }
  }
}
