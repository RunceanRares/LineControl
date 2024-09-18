using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControllerCore.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class DeviceCalibrationController : Controller
  {
    private readonly IDeviceCalibrationService service;

    public DeviceCalibrationController(IDeviceCalibrationService service)
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

    public async Task<ActionResult> Create()
    {
      return View();
    }
    public ActionResult Edit(int id)
    {
      return View();
    }
  }
}
