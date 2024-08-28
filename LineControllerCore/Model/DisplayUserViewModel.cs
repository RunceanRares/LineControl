using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DisplayUserViewModel
  {
    [ScaffoldColumn(false)]
    [Required]
    public virtual int Id { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public string Department { get; set; }

    [Display(Name = "User")]
    public virtual string DisplayName
    {
      get
      {
        return $"{LastName}, {FirstName} {Department}";
      }
    }

    public string FullName
    {
      get
      {
        return $"{LastName} {FirstName}";
      }
    }
  }
}
