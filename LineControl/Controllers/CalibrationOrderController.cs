using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControllerCore.Interface;
using LineControllerCore.Models;

using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class CalibrationOrderController : Controller
  {
    private readonly IDeviceCalibrationService service;

    public CalibrationOrderController(IDeviceCalibrationService service)
    {
      this.service = service;
    }

    public IActionResult Index()
    {
      return View();
    }

    public ActionResult GetDevicesCalibration([DataSourceRequest] DataSourceRequest request)
    {
      var deviceCalibration = service.GetSelectViewModels();
      return Json(deviceCalibration.ToList().ToDataSourceResult(request));
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(DeviceCalibrationOrderViewModel model)
    {
      if(model != null )
      {
        service.AddCalibrationOrder(model);
        return RedirectToAction("Index");
      }
      else
      {
        return View("Index");
      }
    }  
                              
    public ActionResult Edit(int id)
    {
      var model = service.GetDeviceCalibrationById(id);

      if (User.Identity?.IsAuthenticated == true)
      {
        // Exemplu: dacă folosești standardul Identity cu Claims
        var firstName = User.FindFirst("given_name")?.Value ?? string.Empty;
        var lastName = User.FindFirst("family_name")?.Value ?? string.Empty;
        var department = User.FindFirst("department")?.Value ?? string.Empty;

        // Dacă nu ai claims pentru prenume/nume, poți folosi User.Identity.Name direct:
        if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
        {
          model.Inspector = User.Identity.Name;
        }
        else
        {
          model.Inspector = $"{lastName}, {firstName} {department}".Trim();
        }
      }

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(DeviceCalibrationOrderViewModel model)
    {
      if (ModelState.IsValid)
      {
        if (User.Identity?.IsAuthenticated == true && string.IsNullOrEmpty(model.Inspector))
        {
          model.Inspector = User.Identity.Name;
        }

        service.Update(model);
        return RedirectToAction("Index");
      }

      return View(model);
    }

    public async Task<JsonResult> GetItemNumber(string itemNumber) 
    {
      var items = await service.GetItemNumbers(itemNumber).ConfigureAwait(false);
      return Json(items);
    }

    public async Task<JsonResult> GetAllDeviceLocation()
    {
      var result = await service.GetLocationAsync().ConfigureAwait(false);
      return Json(result);
    }

    public async Task<JsonResult> GetCalibrationAction()
    {
      var result = await service.GetCalibrationAction().ConfigureAwait(false);
      return Json(result);
    }

    public async Task<JsonResult> GetUserCalibration()
    {
      var result = await service.GetUserCalibration().ConfigureAwait(false);
      return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetDeviceCreatorByDeviceId(int deviceId)
    {
      var creator = await service.GetDeviceCreator(deviceId);
      return Json(new
      {
        createdBy = creator?.CreatedBy ?? ""
      });
    }

    [HttpGet]
    public async Task<IActionResult> GetDeviceTestLocation(int deviceId)
    {
      var testLocation = await service.GetDeviceTestLocation(deviceId);
      return Json(new
      {
        testLocation = testLocation?.StoragePlace ?? ""
      });
    }
  }
}
