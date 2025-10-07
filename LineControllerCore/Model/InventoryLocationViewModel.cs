using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class InventoryLocationViewModel
  {
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    
    public string Name { get; set; }

    [Display(Name = "Responsible person")]
    [Required(ErrorMessage = "The 'Responsible person' field is required.")]
    public int ResponsibleId { get; set; }

    [Display(Name = "Country")]
    public string Country { get; set; }

    [Display(Name = "Company location")]
    public int CompanyLocationId { get; set; }

    [Display(Name = "Building")]
    public string Building { get; set; }

    [Display(Name = "Floor")]
    public string Floor { get; set; }

    [Display(Name = "Room number")]
    public string RoomNumber { get; set; }

    [Display(Name = "Active")]
    public bool Active { get; set; }

    [Display(Name = "Activity type")]
    public int? ActivityTypeId { get; set; }

    public decimal? ActivityTypeRate { get; set; }

    [Display(Name = "Cost factor")]
    [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
    public decimal? CostFactor { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only
    //public IList<InventoryLocationUserViewModel> Users { get; set; } = new List<InventoryLocationUserViewModel>();

    //public IList<StoragePlaceViewModel> StoragePlaces { get; set; } = new List<StoragePlaceViewModel>();

    public IEnumerable<CompanyLocationViewModel> StoragePlaceCompanyLocations { get; set; }

    public IEnumerable<DisplayUserViewModel> StoragePlaceResponsibles { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

    //public bool IsNew
    //{
    //  get
    //  {
    //    return Id < 1;
    //  }
    //}

    //public bool CanEdit { get; set; }
  }
}
