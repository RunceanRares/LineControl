using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class ReservationController : Controller
  {
    private readonly IDeviceReservationService reservationService;
    private readonly IDeviceService deviceService;
    private readonly IDeviceIntegrationService integrationService;

    public ReservationController(IDeviceService deviceService, IDeviceIntegrationService deviceIntegrationService, IDeviceReservationService reservationService)
    {
      this.deviceService = deviceService;
      this.integrationService = deviceIntegrationService;
      this.reservationService = reservationService;
    }


    public IActionResult Index()
    {
      return View();
    }

    public async Task<JsonResult> GetPeriods()
    {
      var result = await reservationService.GetPeriodsAsync().ConfigureAwait(false);
      return Json(result);
    }
    

    public async Task<IActionResult> CheckReservationStatusAsync(string itemNumber)
    {
      
      var device = reservationService.GetReserveHierarchy(itemNumber);

      if (device == null)
      {
        return Json(new
        {
          deviceId = 0,
          manufacturer = "",
          model = "",
          designation = "",
          isUniversal = false,
          hasRanges = false,
          checkCalibration = false,
          isReserved = false
        });
      }
      else
      {
        bool hasReservation = await reservationService.HasActiveReservationAsync(device.Id).ConfigureAwait(false);
        bool hasIssue = await reservationService.HasActiveIssueAsync(device.Id).ConfigureAwait(false);
        if (hasIssue)
        {
          string redirectUrl = Url.Action("Issues", "DeviceIssues", new { itemNumber = device.ItemNumber });
          return Json(new
          {
            hasActiveIssue = true,
            redirectUrl = redirectUrl
          });
        }
        else
        {
          if (hasReservation)
          {
            TempData["SuccessMessage"] = $"Device {itemNumber} was successfully reserved.";
            string redirectUrl = Url.Action("CancelReservation", "Reservation", new { itemNumber = device.ItemNumber });

            return Json(new
            {
              hasActiveReservation = true,
              redirectUrl = redirectUrl
            });
          }
          else
          {
            var result = new
            {
              deviceId = device.Id,
              manufacturer = device.Manufacturer,
              model = device.DeviceModel,
              designation = device.Designation,
              isUniversal = false, // dacă vrei poți lua din DeviceClass.IsUniversal
              hasRanges = false,   // dacă ai câmpuri MeasurementRanges
              checkCalibration = device.IsCalibrationDue,
              isReserved = device.IsReserved,
              reservationDetails = device.IsReserved ? new
              {
                startDate = device.ReservationStartDate?.ToString("dd/MM/yyyy"),
                endDate = device.ReservationEndDate?.ToString("dd/MM/yyyy"),
                reservedBy = device.ReservedBy
              } : null
            };

            return Json(result);
          }
        }
      }
    }

    [HttpPost]
    public async Task<ActionResult> CancelReservation(DeviceReservationSelectViewModel viewModel)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
        // Pune un breakpoint aici și verifică variabila 'errors'
        return View("Issues", viewModel);
      }
      try
      {
        var result = await reservationService.ReservationCanceledAsync(viewModel);
        TempData["SuccessMessage"] = $"Device {viewModel.ItemNumber} was successfully canceled.";
        return RedirectToAction("Reservation", "Device");
      }
      catch (InvalidOperationException ex)
      {
        ModelState.AddModelError("", ex.Message);
        return View("CancelReservation", viewModel);
      }
    }

    [AcceptVerbs("GET", "POST")]
    public async Task<IActionResult> GetReserveHierarchyTree([DataSourceRequest] DataSourceRequest request, int deviceId)
    {
      var data = reservationService.GetReserveHierarchyList(deviceId);
      var result = await data.ToTreeDataSourceResultAsync(
                        request,
                        c => c.Id,
                        c => c.ParentId,
                        c => c
                    ).ConfigureAwait(false);

      return Json(result);
    }

    [HttpGet]
    public IActionResult CancelReservation(string itemNumber)
    {
      var device = reservationService.GetReserveHierarchyAsync(itemNumber);

      if (device == null)
      {
        return RedirectToAction("Index");
      }

      return View(device);
    }


    [HttpPost]
    public async Task<ActionResult> Reservation(DeviceReservationEditViewModel viewModel)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Select(x => x.Value.Errors)
                              .Where(y => y.Count > 0)
                              .ToList();

        return View("Reservation", viewModel);
      }
      try
      {
        var response = await reservationService.ReserveAsync(viewModel).ConfigureAwait(false);

        TempData["SuccessMessage"] = $"Device {viewModel.ItemNumber} was successfully reserved.";
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        ModelState.AddModelError("", ex.Message);
        return View("~/Views/Device/Reservation.cshtml", viewModel);
      }
    }

  }
}
