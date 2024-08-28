using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.QueryableExtensions;
using LineControl.Models;
using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerInfrastructure;
using LineControllerInfrastructure.ContextConfiguration;
using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LineControllerCore.Service
{
  public class UserService : BaseService<User>, IUserService
  {
    private readonly IUserRoleService userRoleService;

    public UserService(LineContextDb context, IMapper mapper, ILogger<UserService> logger, IUserRoleService userRoleService) : base(context, mapper, logger)
    {
      this.userRoleService = userRoleService;
    }

    public UserViewModel Add(UserViewModel userViewModel)
    {
      throw new NotImplementedException();
    }

    public List<UserViewModel> Get()
    {
      return Context.Users.Select(s => new UserViewModel
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
      return Context.Users.Where(s => s.Manager != null).Select(s => s.Manager).Distinct().Select(s => ConvertUserToUserSelectViewModel(s!)).AsQueryable();
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

      Context.Users.Add(userModel);
      Context.SaveChanges();
      return user;
    }

    public UserViewModel GetUserById(int id)
    {
      var user = Context.Users.Where(s => s.Id == id).FirstOrDefault();
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
      Context.Users.Update(userModel);
      Context.SaveChanges();
      return user;
    }

    public async Task<int?> GetUserIdAsync(string userName)
    {
      return await Context.Users.Where(u => u.UserName == userName && u.Status == UserStatusViewModel.StatusActive)
                                .Select(u => (int?)u.Id)
                                .FirstOrDefaultAsync().ConfigureAwait(false);
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

    private async Task<IEnumerable<DisplayUserViewModel>> getManager()
    {
      return await Context.Users.Where(s => s.Manager != null).Select(s => s.Manager).OrderBy(s => s.FirstName)
                                                              .ProjectTo<DisplayUserViewModel>(Mapper.ConfigurationProvider)
                                                              .ToListAsync().ConfigureAwait(false);
    }

    private async Task<UniqueValidation> existUserName(UserViewModel user)
    {
      if (await Context.Users.AnyAsync(s => s.UserName == user.UserName & s.Id != user.Id).ConfigureAwait(false))
      {
        return new UniqueValidation(nameof(user.UserName));
      }

      return new UniqueValidation();
    }

    private IEnumerable<UserRoleViewModel> getUserRole(string name)
    {
      var roles = Context.Roles.Where(s => !s.Deactivated).ToList();
      try
      {
        return roles.Select(s =>
        {
          bool member = false;
          try
          {
            member = userRoleService.IsMember(name, s.Name);
          }
          catch (InvalidOperationException)
          {
            throw;
          }
          catch (Exception ex)
          {
            Logger.LogError(ex.Message);
          }

          return new UserRoleViewModel()
          {
            Id = s.Id,
            Name = s.Name,
            IsSelected = member,
          };
        });

      }
      catch (InvalidOperationException ex)
      {
        if (ex.Message != $"The user {name} does not exist")
        {
          Logger.LogError(ex.Message);
        }

        return roles.Select(r => new UserRoleViewModel()
        {
          Id = r.Id,
          Name = r.Name,
          IsSelected = false,
        });
      }
    }
  }
}
