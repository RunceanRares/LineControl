using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControllerCore.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class ActivityTypeController : Controller
  {
    private readonly IActivityTypeService service;

    public ActivityTypeController(IActivityTypeService service) 
    {
      this.service = service;
    }
    public IActionResult Index()
    {
      return View();
    }

    public ActionResult GetActivityType([DataSourceRequest] DataSourceRequest request)
    {
      var activity = service.GetSelectViewModels();
      return Json(activity.ToList().ToDataSourceResult(request));
    }

    public ActionResult Edit(int id)
    {
      return View(service.GetActivityById(id));
    }


    public async Task<ActionResult> Create()
    {
      return View();
    }
  }
}
