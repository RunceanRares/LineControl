using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControl.Models;
using LineControllerCore.Model;
using LineControllerCore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using LineControllerCore.Interface;
using LineControllerInfrastructure.Entities;

namespace LineControl.Controllers
{
  public class DeviceController : Controller
  {
    private readonly IDeviceService service;
    //private readonly IUserRoleService userRoleService;
    //private readonly IUserService userService;
    private readonly IIdentityService identityService;
    private readonly IDeviceService deviceService;
    private readonly IDeviceIntegrationService integrationService;

    public DeviceController(IDeviceService service, IIdentityService identityService, IDeviceService deviceService, IDeviceIntegrationService deviceIntegrationService)// IUserRoleService userRoleService, IUserService userService)
    {
      this.service = service;
      this.identityService = identityService;
      this.deviceService = deviceService;
      this.integrationService = deviceIntegrationService;
      //this.userRoleService = userRoleService;
      //this.userService = userService;
    }

    public IActionResult Index()
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
      var model = new DeviceEditViewModel()
      {
        Id = 0,
        IsDisplay = false
      };

      return View("Create", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(DeviceEditViewModel model)
    {
      if (ModelState.IsValid)
      {
        service.AddDevice(model);
        return RedirectToAction("Index");
      }
      else
      {
        return View("Index");
      }
    }

    public ActionResult Edit(int id)
    {
      var device = service.GetDeviceById(id);
      var isUserAuthenticated = User.Identity.IsAuthenticated;
      if (!isUserAuthenticated)
      {
        return RedirectToAction("Details", new { id });
      }

      return View(device);
    }

    public ActionResult Details(int id)
    {
      var device = service.GetDeviceById(id);
      return View(device);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(DeviceEditViewModel model)
    {
      if (ModelState.IsValid)
      {
        service.Update(model);
        return RedirectToAction("Index");
      }
      
      else
      {
        return View("Edit", model);
      }
    }

    public ActionResult History()
    {
      return View();
    }

    public ActionResult Reservation()
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

    public async Task<ActionResult> Integrate([DataSourceRequest] DataSourceRequest request, [FromQuery] int parentId, DeviceChildViewModel device)
    {
      if (string.IsNullOrEmpty(device.ItemNumber))
      {
        ModelState.AddModelError(nameof(device.ItemNumber), "The 'Item number' field is required.");
      }

      if (ModelState.IsValid)
      {
        var response = await service.IntegrateAsync(parentId, device).ConfigureAwait(false);
        if (response != null)
        {
          return Json(response);
        }
      }
      var result = await new[] { device }.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false);
      return Json(result);
    }

    public async Task<IActionResult> GetHierarchy([DataSourceRequest] DataSourceRequest request, int deviceId)
    {
      var children = integrationService.GetHierarchy(deviceId);
      var result = await children.ToTreeDataSourceResultAsync(request, c => c.Id, c => c.ParentId, c => c).ConfigureAwait(false);
      return Json(result);
    }
  }
}
