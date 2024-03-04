using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControllerCore.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class UserController : Controller
  {
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
      this._service = service;
    }

    public IActionResult Index()
    {
      return View();
    }

    public ActionResult ReadUsers([DataSourceRequest] DataSourceRequest request)
    {
      var userViewModelsList = _service.Get();
      return Json(userViewModelsList.ToList().ToDataSourceResult(request));
    }

    public async Task<ActionResult> Create()
    {
      return View();
    }

    public async Task<ActionResult> Edit()
    {
      return View();
    }
  }
}
