using LineControl.Models;
using LineControllerCore.Interface;
using LineControllerInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Service
{
  public class UserService : IUserService
  {
    private readonly LineContextDb context;

    public UserService(LineContextDb context)
    {
      this.context = context;
    }

    public UserViewModel Add(UserViewModel userViewModel)
    {
      throw new NotImplementedException();
    }

    public List<UserViewModel> Get()
    {
      return context.Users.Select(s => new UserViewModel
      {
        Id = s.Id,
        UserName = s.UserName,
        FirstName = s.FirstName,
        LastName = s.LastName,
        Department = s.Department,
        CompanyLocationId = s.CompanyLocationId,
        CostCenter = s.CostCenter,
        Status = s.Status,
        Email = s.Email,
        ManagerId = s.ManagerId,
        PersonalNumber = s.PersonalNumber,
        Title = s.Title,
        Phone = s.Phone,
      }).ToList();
    }
  }
}
