using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LineControllerCore.Interface;
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
  }
}
