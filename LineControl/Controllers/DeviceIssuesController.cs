using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerCore.Service;
using Microsoft.AspNetCore.Mvc;

using System.Net.NetworkInformation;
namespace LineControl.Controllers
{
  public class DeviceIssuesController : Controller
  {
    private readonly IDeviceIssuesService service;

    public DeviceIssuesController(IDeviceIssuesService service)
    {
      this.service = service;
    }

    [HttpGet]
    public JsonResult SearchItemNumbers(string itemNumber)
    {
      if (string.IsNullOrWhiteSpace(itemNumber))
        return Json(new List<object>());

      var result = service.SearchItemNumber(itemNumber);
      return Json(result);
    }

    [HttpGet]
    public async Task<JsonResult> CheckDeviceIssuesStatus(string itemNumber)
    {
      var device = await service.GetDeviceIssuesStatus(itemNumber).ConfigureAwait(false);
      return Json(device);
    }

    public async Task<JsonResult> GetActiveUsers()
    {
      var users = await service.GetActiveUsers().ConfigureAwait(false);

      var result = users.Select(u => new
      {
        u.Id,
        DisplayName = u.LastName + ", " + u.FirstName
      }).ToList();

      return Json(result);
    }

    [HttpGet]
    public async Task<ActionResult> Issues(string itemNumber = null)
    {
      if (string.IsNullOrWhiteSpace(itemNumber))
      {
        var emptyModel = new DeviceIssueEditViewModel
        {
          IssueId = 0,
          DeviceId = 0,
          ItemNumber = "",
          CanIssue = true,
          CanRetrieve = false,
          IsRetrieve = false,
          MinimReturnDate = DateTime.Today,
          HasReservations = false,
          IsAccountingMandatory = false
        };
        return View("Issues", emptyModel); // View-ul de Issue gol
      }

      // 2. Cazul în care s-a selectat un ItemNumber
      var model = await service.GetViewModelByItemNumberAsync(itemNumber);

      if (model == null)
      {
        ModelState.AddModelError("", "Device not found.");
        // Dacă nu găsește device-ul, rămânem pe pagina de Issues cu eroare
        var errorModel = new DeviceIssueEditViewModel { CanIssue = false };
        return View("Issues", errorModel);
      }

      // --- AICI ESTE LOGICA DE RUTARE ---
      if (model.IsRetrieve == true)
      {
        // Service-ul a zis că device-ul este deja dat -> Pagina Retrieve
        return View("RetrieveIssue", model);
      }
      else
      {
        // Service-ul a zis că device-ul este liber -> Pagina Issue
        return View("Issues", model);
      }
    }

    [HttpPost]
    public async Task<ActionResult> Issues(DeviceIssueEditViewModel viewModel)
    {
      //viewModel.Reservations = new List<DeviceReservationSelectViewModel>();
      ModelState.Remove(nameof(viewModel.IsRetrieve));

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
        if (viewModel.IsNew)
        {
          await service.IssueAsync(viewModel);
          TempData["SuccessMessage"] = $"Device {viewModel.ItemNumber} was successfully issued.";
        }
        else
        {
          await service.ReceiveAsync(viewModel);
          TempData["SuccessMessage"] = $"Device {viewModel.ItemNumber} was successfully retrieved.";
        }

        return RedirectToAction("Issues");
      }
      catch (InvalidOperationException ex)
      {
        ModelState.AddModelError("", ex.Message);
        return View("Issues", viewModel);
      }
    }

    public async Task<ActionResult> GetIssueHierarchy([DataSourceRequest] DataSourceRequest request, int? deviceId)
    {
      var children = service.GetIssueHierarchy(deviceId);
      var result = await children.ToTreeDataSourceResultAsync(request,
                                  c => c.Id,        
                                  c => c.ParentId,
                                  c => c).ConfigureAwait(false);
      return Json(result);
    }

  }
}
