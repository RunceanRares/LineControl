using LineControllerCore.Model;
using System.ComponentModel.DataAnnotations;

namespace LineControl.Models
{
  public class UserViewModel
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "The 'Personnel No' field is required.")]
    public string? PersonalNumber { get; set; }

    public string? Title { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? UserName { get; set; }

    public string? Department { get; set; }

    public string? CostCenter { get; set; }

    public int? ManagerId { get; set; }

    public string? Email { get; set; }

    public bool Female { get; set; }

    public int Status { get; set; } = UserStatusViewModel.StatusActive;

    public int CompanyLocationId { get; set; }

    public string? Phone { get; set; }
  }
}
