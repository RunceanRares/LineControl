using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControl.Models;
using LineControllerCore.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities;

namespace LineControl.Controllers
{
  public class UserController : Controller
  {
    private readonly IUserService service;

    public UserController(IUserService service)
    {
      this.service = service;
    }

    public IActionResult Index()
    {
      return View();
    }

    public ActionResult ReadUsers([DataSourceRequest] DataSourceRequest request)
    {
      var userViewModelsList = service.Get();
      return Json(userViewModelsList.ToList().ToDataSourceResult(request));
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(UserViewModel user)
    {
     if(ModelState.IsValid)
      {
        service.AddUser(user);
        return RedirectToAction("Index");
      }
      else
      {
        return View("Index");
      }
    }

    public ActionResult Edit(int id)
    {
      return View(service.GetUserById(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(UserViewModel user)
    {
      if (ModelState.IsValid)
      {
        service.Update(user);
        return RedirectToAction("Index");
      }
      else
      {
        return View(user);
      }
    }

    public async Task<ActionResult> GetManagers([DataSourceRequest] DataSourceRequest request)
    {
      var managers = service.GetManager();
      return Json(managers);
    }
  }
}
