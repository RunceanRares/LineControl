using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControl.Models;
using LineControllerCore.Interface;
using LineControllerCore.Model;
using Microsoft.AspNetCore.Mvc;

namespace LineControl.Controllers
{
  public class CompanyLocationController : Controller
  {
    private ICompanyLocationService service;

    public CompanyLocationController(ICompanyLocationService service)
    {
      this.service = service;
    }

    public IActionResult Index()
    {
      return View();
    }

    public ActionResult GetCompanies([DataSourceRequest] DataSourceRequest dataSourceRequest)
    {
      var company = service.GetCompanies();
      return Json(company.ToList().ToDataSourceResult(dataSourceRequest));
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CompanyLocationViewModel company)
    {
      if (ModelState.IsValid)
      {
        service.CreateCompany(company);
        return RedirectToAction("Index");
      }
      else
      {
        return View(company);
      }
    }
  }
}
