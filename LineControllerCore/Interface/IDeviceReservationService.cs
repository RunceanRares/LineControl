using LineControllerCore.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Interface
{
  public interface IDeviceReservationService
  {
    Task<IEnumerable<MeasurementRangeViewModel>> GetMeasurementRangesAsync(int deviceId);

    Task<IEnumerable<ReservationPeriodViewModel>> GetPeriodsAsync();

    DeviceReserveHierarchyListViewModel GetReserveHierarchy(string itemNumber);

    Task<bool> HasActiveIssueAsync(int deviceId);

    Task<bool> HasActiveReservationAsync(int deviceId);

    DeviceReservationSelectViewModel GetReserveHierarchyAsync(string itemNumber);

    List<DeviceReserveHierarchyListViewModel> GetReserveHierarchyList(int deviceId);

    Task<DeviceReservationEditViewModel> ReserveAsync(DeviceReservationEditViewModel deviceReservation);

    Task<bool> ReservationCanceledAsync(DeviceReservationSelectViewModel deviceReservation);
  }
}
