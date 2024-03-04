using LineControllerInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
