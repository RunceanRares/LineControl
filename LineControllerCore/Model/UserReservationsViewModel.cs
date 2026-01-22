using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class UserReservationsViewModel
  {
    public string DisplayName { get; set; }

    public bool HasDeviceViewRight { get; set; }

    public DateTime MinimReturnDate { get; set; }

    public IList<DeviceReservationSelectViewModel> Reservations { get; init; } = new List<DeviceReservationSelectViewModel>();
  }
}
