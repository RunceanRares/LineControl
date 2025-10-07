using LineControllerCore.Model;

namespace LineControllerCore.Interface
{
  public interface ICompanyLocationService
  {
    List<CompanyLocationViewModel> GetCompaniesLocation();

    CompanyLocationViewModel GetLocationCompanyById(int id);

    CompanyLocationViewModel Update(CompanyLocationViewModel model);

    CompanyLocationViewModel CreateCompany(CompanyLocationViewModel company);
  }
}
