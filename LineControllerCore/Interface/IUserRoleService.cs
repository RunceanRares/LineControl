namespace LineControllerCore.Interface
{
  public interface IUserRoleService
  {
    bool IsMember(IEnumerable<string> roleNames);

    bool IsMember(string userName, string roleName);
    Task<bool> HasRightsAsync(string userAction);
  }
}
