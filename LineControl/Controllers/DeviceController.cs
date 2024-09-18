using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControl.Models;
using LineControllerCore.Model;
using LineControllerCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class DeviceController : Controller
  {
    private readonly IDeviceService service;
    //private readonly IUserRoleService userRoleService;
    //private readonly IUserService userService;

    public DeviceController(IDeviceService service)// IUserRoleService userRoleService, IUserService userService)
    {
      this.service = service;
      //this.userRoleService = userRoleService;
      //this.userService = userService;
    }

    public async Task<ActionResult> Index()
    {
      return View();
    }

    public ActionResult GetDevices([DataSourceRequest] DataSourceRequest request)
    {
      var deviceViewModel = service.GetDevices();
      return Json(deviceViewModel.ToList().ToDataSourceResult(request));
    }

    public async Task<ActionResult> Create()
    {
      return View();
    }

    public ActionResult Edit(int id)
    {
      return View(service.GetDeviceById(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(DeviceEditViewModel model)
    {
      if (model.Id == 0)
      {
        ModelState.AddModelError("","Unable to save. Device does not exist.");
        return View(model);
      }

      if (ModelState.IsValid)
      {
        service.Update(model);
        return RedirectToAction("Index");
      }
      
      else
      {
        return View(model);
      }
    }

    public async Task<ActionResult> History()
    {
      return View();
    }

    public async Task<ActionResult> Reservation()
    {
      return View();
    }

    public async Task<JsonResult> GetMeasurementRanges(int deviceClassId)
    {
      var result = await service.GetMeasurementRangesAsync(deviceClassId).ConfigureAwait(false);
      result = result.ToList();
      return Json(result);
    }

    public async Task<JsonResult> GetAllDeviceStatuses()
    {
      var result = await service.GetStatusesAsync().ConfigureAwait(false);
      return Json(result);
    }
  }
}
