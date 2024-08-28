using System.ComponentModel.DataAnnotations;

namespace LineControllerCore.Model
{
  public class DeviceModelViewModel
  {
    [ScaffoldColumn(false)]
    public int Id { get; set; }

    [Display(Name = "Model")]
    [Required(ErrorMessage = "The 'Model' field is required.")]
    public string Name { get; set; }

    [Display(Name = "Last Change User")]
    public string LastChangeUser { get; set; }
  }
}
