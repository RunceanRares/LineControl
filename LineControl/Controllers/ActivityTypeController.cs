using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControl.Models;

using LineControllerCore.Interface;
using LineControllerCore.Model;

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ActivityTypeViewModel model)
    {
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

    public async Task<ActionResult> Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ActivityTypeViewModel activity)
    {
      if (ModelState.IsValid)
      {
        service.AddActivityType(activity);
        return RedirectToAction("Index");
      }
      else
      {
        return View("Index");
      }
    }
  }
}
