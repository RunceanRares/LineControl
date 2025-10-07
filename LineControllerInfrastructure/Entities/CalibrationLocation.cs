using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerInfrastructure.Entities
{
  public class CalibrationLocation
  {
    public int Id { get; set; }


    [ForeignKey(nameof(CompanyLocation))]
    public int CompanyLocationId { get; set; }

    public CompanyLocation CompanyLocation { get; set; }

    public bool Active { get; set; }

    public string Code { get; set; }

    public string CostCenter { get; set; }
  }
}
