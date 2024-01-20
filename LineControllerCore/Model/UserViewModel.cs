using LineControllerCore.Model;
using System.ComponentModel.DataAnnotations;

namespace LineControl.Models
{
  public class UserViewModel
  {
    public int Id { get; set; }

    [Display(Name = "Personnel No")]
    [Required(ErrorMessage = "The 'Personnel No' field is required.")]
    public string? PersonalNumber { get; set; }

    [Display(Name = "Title")]
    public string? Title { get; set; }

    [Display(Name = "Surname")]
    [Required(ErrorMessage = "The 'Surname' field is required.")]
    public string? LastName { get; set; }

    [Display(Name = "First name")]
    [Required(ErrorMessage = "The 'First name' field is required.")]
    public string? FirstName { get; set; }

    [Display(Name = "User ID")]
    [Required(ErrorMessage = "The 'User ID' field is required.")]
    public string? UserName { get; set; }

    [Display(Name = "Department")]
    public string? Department { get; set; }

    [Display(Name = "Cost center")]
    public string? CostCenter { get; set; }

    [Display(Name = "Line manager")]
    public int? ManagerId { get; set; }

    [Display(Name = "E-mail")]
    [EmailAddress(ErrorMessage = "The 'E-mail' field is not a valid e-mail address.")]
    public string? Email { get; set; }

    [Display(Name = "Salutation")]
    [Required(ErrorMessage = "The 'Salutation' field is required.")]
    public bool Female { get; set; }

    [Display(Name = "Short name")]
    public string? ShortName { get; set; }

    [Display(Name = "Status")]
    public int Status { get; set; } = UserStatusViewModel.StatusActive;

    [Display(Name = "Company location")]
    [Required(ErrorMessage = "The 'Company location' field is required.")]
    public int CompanyLocationId { get; set; }

    [Display(Name = "Phone")]
    [StringLength(30, ErrorMessage = "The 'Phone' field must have only 30 characters.")]
    public string? Phone { get; set; }

    public string DisplayName
    {
      get
      {
        return $"{LastName}, {FirstName} {Department}";
      }
    }

  }
}
