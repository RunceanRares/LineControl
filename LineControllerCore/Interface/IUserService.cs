using LineControl.Models;

namespace LineControllerCore.Interface
{
  public interface IUserService
  {
    List<UserViewModel> Get();

    UserViewModel Add(UserViewModel userViewModel);
  }
}
