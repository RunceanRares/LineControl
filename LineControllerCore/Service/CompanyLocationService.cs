using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;

using System.Diagnostics;
namespace LineControllerCore.Service
{
  public class CompanyLocationService : ICompanyLocationService
  {
    private readonly LineContextDb context;

    public CompanyLocationService(LineContextDb contextDb)
    {
       this.context = contextDb;
    }

    public List<CompanyLocationViewModel> GetCompaniesLocation()
    {
      return context.CompanyLocations.Select(s => new CompanyLocationViewModel()
      {
        Id = s.Id,
        Code = s.Code,
        Name = s.Name,
        Country = s.Country,
      }).ToList();
    }

    public CompanyLocationViewModel CreateCompany(CompanyLocationViewModel company)
    {
      var companyLocationViewModel = new CompanyLocation() { Id = company.Id, Code = company.Code, Name = company.Name, Country = company.Country };

      context.CompanyLocations.Add(companyLocationViewModel);
      context.SaveChanges();
      return company;
    }

    public CompanyLocationViewModel GetLocationCompanyById(int id)
    {
      var locationCompany = context.CompanyLocations.Where(s => s.Id == id).Select( s => 
        new CompanyLocationViewModel 
        { 
          Id = s.Id,
          Name = s.Name,
          Code = s.Code,
          Country = s.Country,
        }).FirstOrDefault();

      return locationCompany ?? new CompanyLocationViewModel();
    }

    public CompanyLocationViewModel Update(CompanyLocationViewModel model)
    {
      var company = context.CompanyLocations.FirstOrDefault(x => x.Id == model.Id);
      if (company == null) return model;
      company.Name = model.Name;

      //context.CompanyLocations.Update(company);
      context.SaveChanges();

      return model;
    }
  }
}
