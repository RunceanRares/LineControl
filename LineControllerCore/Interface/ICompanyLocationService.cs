using LineControllerCore.Model;

namespace LineControllerCore.Interface
{
  public interface ICompanyLocationService
  {
    List<CompanyLocationViewModel> GetCompanies();

    CompanyLocationViewModel CreateCompany(CompanyLocationViewModel company);
  }
}
