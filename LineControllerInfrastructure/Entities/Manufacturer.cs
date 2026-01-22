using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerInfrastructure.Entities
{
  public class Manufacturer : BaseModel
  {
    [Column("ManufacturerId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    public string Name { get; set; }
  }
}
