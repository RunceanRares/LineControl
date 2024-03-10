using LineControl.Models;
using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using Microsoft.VisualBasic;
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

    public IQueryable<UserSelectViewModel> GetManager()
    {
      return context.Users.Where(s => s.Manager != null).Select(s => s.Manager).Distinct().Select(s => ConvertUserToUserSelectViewModel(s!)).AsQueryable();
    }

    public UserViewModel AddUser(UserViewModel user)
    {
#pragma warning disable CS8601 // Possible null reference assignment.
      var userModel = new User
      {
        Id = user.Id,
        UserName = user.UserName,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Department = user.Department,
        CompanyLocationId = user.CompanyLocationId,
        CostCenter = user.CostCenter,
        Status = user.Status,
        Email = user.Email,
        ManagerId = user.ManagerId,
        PersonalNumber = user.PersonalNumber,
        Title = user.Title,
        Phone = user.Phone,
      };
#pragma warning restore CS8601 // Possible null reference assignment.

      context.Users.Add(userModel);
      context.SaveChanges();
      return user;
    }

    public UserViewModel GetUserById(int id)
    {
      var user = context.Users.Where(s => s.Id == id).FirstOrDefault();
      if (user is not null)
      {
        return new UserViewModel
        {
          Id = user.Id,
          UserName = user.UserName,
          FirstName = user.FirstName,
          LastName = user.LastName,
          Department = user.Department,
          CompanyLocationId = user.CompanyLocationId,
          CostCenter = user.CostCenter,
          Email = user.Email,
          Phone = user.Phone,
          PersonalNumber = user.PersonalNumber,
          Title = user.Title,
          Status = user.Status,
          ManagerId = user.ManagerId,
        };
      }
      else
      {
        return null;
      }
    }

    public UserViewModel Update(UserViewModel user)
    {
      var userModel = new User
      {
        Id = user.Id,
        UserName = user.UserName,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Department = user.Department,
        CompanyLocationId = user.CompanyLocationId,
        CostCenter = user.CostCenter,
        Status = user.Status,
        Email = user.Email,
        ManagerId = user.ManagerId,
        PersonalNumber = user.PersonalNumber,
        Title = user.Title,
        Phone = user.Phone,
      };
      context.Users.Update(userModel);
      context.SaveChanges();
      return user;
    }

    private static UserSelectViewModel ConvertUserToUserSelectViewModel(User user)
    {
      return new UserSelectViewModel
      {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Department = user.Department
      };
    }
  }
}
