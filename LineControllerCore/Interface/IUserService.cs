using LineControl.Models;
using LineControllerCore.Model;

namespace LineControllerCore.Interface
{
  public interface IUserService
  {
    List<UserViewModel> Get();

    UserViewModel Add(UserViewModel userViewModel);
    IQueryable<UserSelectViewModel> GetManager();

    UserViewModel AddUser(UserViewModel user);

    UserViewModel GetUserById(int id);

    UserViewModel Update(UserViewModel user);

    Task<int?> GetUserIdAsync(string userName);
  }
}
