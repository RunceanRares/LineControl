using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Service
{
  public class CompanyLocationService : ICompanyLocationService
  {
    private readonly LineContextDb context;

    public CompanyLocationService(LineContextDb contextDb)
    {
       this.context = contextDb;
    }

    public List<CompanyLocationViewModel> GetCompanies()
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
  }
}
