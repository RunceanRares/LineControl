using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceHistory
  {
    public Device Device { get; set; }

    [ForeignKey(nameof(Device))]
    public int DeviceId { get; set; }

    public DateTime ModificationDate { get; set; }

    [ForeignKey(nameof(ModificationUser))]
    public int ModificationUserId { get; set; }

    public User ModificationUser { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }
  }
}
