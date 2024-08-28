using LineControllerInfrastructure.Entities;

namespace LineControllerCore.Model
{
  public class CompanyLocationViewModel
  {
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();

  }
}
