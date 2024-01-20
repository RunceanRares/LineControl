using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class ActivityTypeViewModel
  {
    public int Id { get; set; }

    [Display(Name = "Code")]
    [Required(ErrorMessage = "The 'Code' field is required.")]
    public string? Code { get; set; }

    [Display(Name = "Activity type")]
    [Required(ErrorMessage = "The 'Activity type' field is required.")]
    public string? Name { get; set; }

    [Display(Name = "Cost center")]
    [Required(ErrorMessage = "The 'Cost center' field is required.")]
    public string? CostCenter { get; set; }

    [Display(Name = "Rate")]
    [DisplayFormat(DataFormatString = "{0:#.00}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "The 'Rate' field is required.")]
    public decimal? Rate { get; set; }

    [Display(Name = "Passive cost factor")]
    [DisplayFormat(DataFormatString = "{0:#.000}", ApplyFormatInEditMode = true)]
    public decimal? PassiveCostFactor { get; set; }

    [Display(Name = "Deactivated")]
    public bool Deactivated { get; set; }
  }
}
