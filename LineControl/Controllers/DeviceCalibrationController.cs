//using Kendo.Mvc.Extensions;
//using Kendo.Mvc.UI;
//using LineControllerCore.Interface;
//using LineControllerCore.Models;

//using Microsoft.AspNetCore.Mvc;

//namespace LineControl.Controllers
//{
//  public class DeviceCalibrationController : Controller
//  {
//    private readonly IDeviceCalibrationService service;

//    public DeviceCalibrationController(IDeviceCalibrationService service)
//    {
//      this.service = service;
//    }

//    public IActionResult Index()
//    {
//      return View();
//    }

//    public ActionResult GetDevicesCalibration([DataSourceRequest] DataSourceRequest request)
//    {
//      var deviceCalibration = service.GetSelectViewModels();
//      return Json(deviceCalibration.ToList().ToDataSourceResult(request));
//    }

//    public async Task<ActionResult> Create()
//    {
//      var model = new DeviceCalibrationOrderViewModel();
//      return View(model);
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Create(DeviceCalibrationOrderViewModel model)
//    {
//      if (ModelState.IsValid)
//      {
//        service.AddCalibratioOrder(model);
//        return RedirectToAction("Index");
//      }
//      else
//      {
//        return View("Index");
//      }
//    }

//    public ActionResult Edit(int id)
//    {
//      var calibration = service.GetDeviceCalibrationById(id);
//      return View("Edit", calibration);
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Edit(DeviceCalibrationOrderViewModel model)
//    {
//      if (ModelState.IsValid)
//      {
//        service.Update(model);
//        return RedirectToAction("Index");
//      }
//      else
//      {
//        return View(model);
//      }
//    }
//  }
//}
