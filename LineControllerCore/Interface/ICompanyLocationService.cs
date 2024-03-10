using LineControllerCore.Model;
using LineControllerInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Interface
{
  public interface ICompanyLocationService
  {
    List<CompanyLocationViewModel> GetCompanies();

    CompanyLocationViewModel CreateCompany(CompanyLocationViewModel company);
  }
}
