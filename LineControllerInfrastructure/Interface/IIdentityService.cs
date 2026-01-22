namespace LineControllerCore.Interface
{
  public interface IIdentityService
  {
    int? UserId { get; }

    bool IsAuthenticated { get; }

    bool IsMember(string roleName);
  }
}
