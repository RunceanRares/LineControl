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
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

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
    private readonly IDeviceReservationService reservationService;

    public DeviceController(IDeviceService service, IIdentityService identityService, IDeviceService deviceService, IDeviceIntegrationService deviceIntegrationService, IDeviceReservationService reservationService)// IUserRoleService userRoleService, IUserService userService)
    {
      this.service = service;
      this.identityService = identityService;
      this.deviceService = deviceService;
      this.integrationService = deviceIntegrationService;
      this.reservationService = reservationService;
      //this.userRoleService = userRoleService;
      //this.userService = userService;
    }

    public IActionResult Index()
    {
      return View();
    }

    public ActionResult GetDevices([DataSourceRequest] DataSourceRequest request)
    {
      var deviceQuery = service.GetDevices();

      // 2. .ToList() execută SQL-ul acum. 
      // Dacă Mapper-ul e reparat, aici nu va mai crăpa.
      var deviceList = deviceQuery.ToList();

      // 3. Trimite datele la Kendo Grid
      return Json(deviceList.ToDataSourceResult(request));
    }

    public async Task<ActionResult> Create()
    {
      var model = new DeviceEditViewModel()
      {
        Id = 0,
        IsDisplay = true

      };

      return View("Create", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DeviceEditViewModel model)
    {
      if (ModelState.IsValid)
      {
        // 2. Adaugă "await" aici!
        await service.AddDevice(model);
        return RedirectToAction("Index");
      }
      else
      {
        return View("Index");
      }
    }

    [HttpGet]
    public IActionResult GetAllDeviceClasses()
    {
      var classes = service.GettAllDeviceClass();

      // Returnezi JSON pentru Kendo UI
      return Json(classes);
    }

    [HttpGet]
    public IActionResult GetAllInventoryLocation()
    {
      var location = service.GetAllInventotyLocation();

      return Json(location);
    }

    [HttpGet]
    public ActionResult GetCalibrationTester()
    {
      var currentUserName = User.Identity.Name ?? "Unknown"; 
      var result = new[]
      {
        new
        { 
            Id = currentUserName,
            CalibrationTester = currentUserName
        }
    };
      return Json(result);
    }


    public ActionResult Edit(int id)
    {
      var device = service.GetDeviceById(id);

      // Verificare de siguranță crucială:
      if (device == null)
      {
        return NotFound($"Dispozitivul cu ID-ul {id} nu a fost găsit.");
      }

      device.IsDisplay = true;
      var isUserAuthenticated = User.Identity.IsAuthenticated;
      if (!isUserAuthenticated)
      {
        return RedirectToAction("Details", new { id });
      }

      device.HasEditRight = true;
      return View(device);
    }

    public ActionResult Details(int id)
    {
      var device = service.GetDeviceById(id);
      return View(device);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(DeviceEditViewModel model)
    {
      if (ModelState.IsValid)
      {
        await service.Update(model);
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

    //public ActionResult Reservation()
    //{
    //  return View();
    //}

    public async Task<IActionResult> Reservation(string? itemNumber)
    {
      var model = await service.GetDeviceReservationEditViewModelAsync(itemNumber);
      return View(model);
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


    [HttpPost]
    public async Task<ActionResult> Integrate([DataSourceRequest] DataSourceRequest request, DeviceChildViewModel device)
    {
      if (string.IsNullOrEmpty(device.ItemNumber))
      {
        ModelState.AddModelError(nameof(device.ItemNumber), "The 'Item number' field is required.");
      }

      try
      {
        var result = await service.IntegrateAsync(device.ParentId, device);
        return Json(new[] { result }.ToDataSourceResult(request, ModelState));
      }
      catch (ArgumentException ex)
      {
        ModelState.AddModelError("ItemNumber", ex.Message);
        return Json(new[] { device }.ToDataSourceResult(request, ModelState));
      }
    }

    public async Task<IActionResult> GetHierarchy([DataSourceRequest] DataSourceRequest request, int deviceId)
    {
      var children = integrationService.GetHierarchy(deviceId);
      var result = await children.ToTreeDataSourceResultAsync(request, c => c.Id, c => c.ParentId, c => c).ConfigureAwait(false);
      return Json(result);
    }

    public async Task<JsonResult> GetReservationMeasurementRanges(int id)
    {
      var result = await reservationService.GetMeasurementRangesAsync(id).ConfigureAwait(false);
      return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> History(string itemNumber)
    {
      // Modelul inițial (gol)
      var model = new DeviceHistoryViewModel();

      // Dacă nu s-a căutat nimic, returnăm pagina goală
      if (string.IsNullOrWhiteSpace(itemNumber))
      {
        return View(model);
      }

      // Apelăm serviciul
      var result = await service.GetDeviceHistoryAsync(itemNumber);

      if (result == null)
      {
        // Dacă nu am găsit, adăugăm eroare și păstrăm ItemNumber în input ca să vadă userul ce a tastat
        ModelState.AddModelError("", $"Device with Item Number '{itemNumber}' not found.");
        model.ItemNumber = itemNumber;
        return View(model);
      }

      return View(result);
    }

  }
}
