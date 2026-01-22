using AutoMapper;
using AutoMapper.QueryableExtensions;
using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LineControllerCore.Service
{
  public class DeviceReservationService : BaseService<DeviceReservation>, IDeviceReservationService
  {
    public DeviceReservationService(LineContextDb context, IMapper mapper, ILogger<DeviceReservation> logger, IIdentityService identityService) : base(context, mapper, logger, identityService)
    {
    }

    public async Task<IEnumerable<MeasurementRangeViewModel>> GetMeasurementRangesAsync(int deviceId)
    {
      var range = await Entities.Where(d => d.Id == deviceId)
                                .Select(d => new
                                {
                                  d.MeasurementMin,
                                  d.MeasurementMax,
                                  d.MeasurementUnit,
                                })
                                .FirstOrDefaultAsync().ConfigureAwait(false);
      if (range?.MeasurementMin == null)
      {
        return Array.Empty<MeasurementRangeViewModel>();
      }

      return new List<MeasurementRangeViewModel>()
         {
            new MeasurementRangeViewModel()
            {
               Min = (decimal)range.MeasurementMin,
               Max = range.MeasurementMax ?? 0,
               Unit = range.MeasurementUnit,
            },
         };
    }

    public async Task<IEnumerable<ReservationPeriodViewModel>> GetPeriodsAsync()
    {
      return await Context.ReservationPeriods.OrderBy(p => p.Min)
                          .ProjectTo<ReservationPeriodViewModel>(Mapper.ConfigurationProvider)
                          .ToListAsync().ConfigureAwait(false);
    }

    public DeviceReserveHierarchyListViewModel GetReserveHierarchy(string itemNumber)
    {
      var device = Context.Devices
             .Where(d => d.ItemNumber == itemNumber)
             .Select(d => new DeviceReserveHierarchyListViewModel
             {
               Id = d.Id,
               ParentId = d.ParentId,
               ItemNumber = d.ItemNumber,
               Manufacturer = d.DeviceClass.Manufacturer.Name,
               DeviceModel = d.DeviceClass.DeviceModel.Name,
               IsCalibrationDue = d.CalibrationOrders.Any(c => c.CalibrationDate <= DateTime.Today),
               HasActiveCalibrationOrder = d.CalibrationOrders.Any(c => c.CalibrationDate <= DateTime.Today),
               InventoryLocationName = d.StoragePlace.RoomDesignation,
               ResponsibleFirstName = "",
               ResponsibleLastName = "",
               ResponsibleDepartment = "",
               IsReserved = d.Reservations.Any(r => r.EndDate >= DateTime.Today),
               HasActiveIssue = d.Issues.Any(i => i.ReturnDateActual == null),

               // Adaugă detaliile rezervării active
               ReservationStartDate = d.Reservations
                .Where(r => r.EndDate >= DateTime.Today)
                .OrderByDescending(r => r.StartDate)
                .Select(r => (DateTime?)r.StartDate)
                .FirstOrDefault(),
               ReservationEndDate = d.Reservations
                .Where(r => r.EndDate >= DateTime.Today)
                .OrderByDescending(r => r.StartDate)
                .Select(r => (DateTime?)r.EndDate)
                .FirstOrDefault(),
               ReservedBy = d.Reservations
                .Where(r => r.EndDate >= DateTime.Today)
                .OrderByDescending(r => r.StartDate)
                .Select(r => r.CreatedBy.UserName)
                .FirstOrDefault()
             })
             .FirstOrDefault();

      return device;
    }

    public DeviceReservationSelectViewModel GetReserveHierarchyAsync(string itemNumber)
    {
      var device = Context.Devices
             .Where(d => d.ItemNumber == itemNumber)
             .Select(d => new DeviceReservationSelectViewModel
             {
               ReservationId = d.Id,
               DeviceId = d.Id,
               ItemNumber = d.ItemNumber,
               Manufacturer = d.DeviceClass.Manufacturer.Name,
               DeviceModel = d.DeviceClass.DeviceModel.Name,
               CalibrationDate = d.CalibrationDate,
               CalibrationInterval = d.CalibrationInterval,
               InventoryLocationName = d.StoragePlace.RoomDesignation + " " + d.StoragePlace.InventoryLocation.Name,
               ResponsibleFirstName = "",
               ResponsibleLastName = "",
               ResponsibleDepartment = "",
               Designation = d.Issues.Any(i => i.ReturnDateActual == null) ? "Has active issue" : "OK",
               IsSelected = false,
               IsSameDevice = false,
               HasDescendants = d.Children.Any()
             })
             .FirstOrDefault();

      return device;
    }

    public async Task<bool> HasActiveIssueAsync(int deviceId)
    {
      bool hasActiveIssue = await Context.DeviceIssues
          .AnyAsync(s => s.DeviceId == deviceId && s.ReturnDateActual == null);

      return hasActiveIssue;
    }

    public async Task<bool> HasActiveReservationAsync(int deviceId)
    {
      bool hasReservationActiv = await Context.DeviceReservations.AnyAsync(s => s.DeviceId == deviceId);

      return hasReservationActiv;
    }

    public List<DeviceReserveHierarchyListViewModel> GetReserveHierarchyList(int deviceId)
    {
      var result = Context.Devices
          .Where(d => d.ParentId == deviceId || d.Id == deviceId)
          .Select(d => new DeviceReserveHierarchyListViewModel
          {
            Id = d.Id,
            ParentId = d.ParentId == 0 ? (int?)null : d.ParentId,
            ItemNumber = d.ItemNumber,
            Manufacturer = d.DeviceClass.Manufacturer.Name,
            DeviceModel = d.DeviceClass.DeviceModel.Name,
            Designation = d.Issues.Any(i => i.ReturnDateActual == null) ? "Has active issue" : "OK",
            InventoryLocationName = d.StoragePlace.RoomDesignation + " " + d.StoragePlace.InventoryLocation.Name,
            IsCalibrationDue = d.CalibrationOrders.Any(c => c.CalibrationDate <= DateTime.Today),
            HasActiveCalibrationOrder = d.CalibrationOrders.Any(c => c.CalibrationDate <= DateTime.Today)
          })
          .ToList();

      return result;
    }


    public async Task<DeviceReservationEditViewModel> ReserveAsync(DeviceReservationEditViewModel deviceReservation)
    {
      try
      {
        List<DeviceReservationTreeItem> devices = null;
        int deviceClassId = deviceReservation.DeviceClassId ?? 0;
        int? reservationMax = await Context.ReservationPeriods.Where(p => p.Id == deviceReservation.PeriodId)
                                                                  .Select(p => p.Max)
                                                                  .FirstAsync().ConfigureAwait(false);

        DateTime dateTime = DateTime.Now;
        if (deviceReservation.DeviceId != null)
        {
          devices = await Context.GetDeviceTree(deviceReservation.DeviceId)
                                     .Select(d => new DeviceReservationTreeItem()
                                     {
                                       Id = d.Id,
                                       ItemNumber = d.ItemNumber,
                                       DeviceClassId = d.DeviceClassId,
                                       IsChild = d.Parent != null,
                                       StatusId = d.StatusId,
                                       Status = d.Status != null ? d.Status.Name : "Unknown",
                                       IssuePlannedDate = d.Issues.Where(i => i.ReturnDateActual == null)
                                                                   .Select(i => i.ReturnDatePlanned ?? (DateTime?)System.DateTime.MaxValue)
                                                                   .FirstOrDefault(),
                                       CalibrationDue = d.CalibrationInterval != null && d.CalibrationDate != null ? d.CalibrationDate.Value.AddMonths(d.CalibrationInterval.Value) : null,
                                       DeviceCostCenter = d.ActivityType != null && d.ActivityType.Rate != 0 ? d.ActivityType.CostCenter : null,
                                       DeviceCode = d.ActivityType != null && d.ActivityType.Rate != 0 ? d.ActivityType.Code : null,
                                       InventoryLocationCostCenter = (d.StoragePlace.InventoryLocation.GenerateCharge && d.StoragePlace.InventoryLocation.ActivityType != null ? d.StoragePlace.InventoryLocation.ActivityType.Rate : 0) != 0 ? d.StoragePlace.InventoryLocation.ActivityType.CostCenter : null,
                                       InventoryLocationCode = (d.StoragePlace.InventoryLocation.GenerateCharge && d.StoragePlace.InventoryLocation.ActivityType != null ? d.StoragePlace.InventoryLocation.ActivityType.Rate : 0) != 0 ? d.StoragePlace.InventoryLocation.ActivityType.Code : null,
                                     })
                                     .ToListAsync().ConfigureAwait(false);

          if (devices.Count == 0)
          {
            throw new ApplicationException("Unable to reserve the device. Device does not exist.");
          }

          var deviceIds = devices.Select(d => d.Id);
          var reservations = await Context.DeviceReservations.Where(r => deviceIds.Contains(r.DeviceId) &&
                                                                         r.StatusId == ReservationStatusViewModel.OpenId)
                                                             .Select(r => new DeviceReservationItem()
                                                             {
                                                               DeviceId = r.DeviceId,
                                                               ItemNumber = r.Device.ItemNumber,
                                                               IsChild = r.Device.ParentId != null,
                                                               StartDate = (DateTime)r.StartDate,
                                                               Max = r.ReservationPeriod.Max,
                                                             })
                                                             .ToListAsync().ConfigureAwait(false);
          DateTime endReservation = reservationMax == null ? System.DateTime.MaxValue : deviceReservation.StartDate.Value.AddDays(reservationMax.Value);

          var mainDevice = devices.First(d => d.Id == deviceReservation.DeviceId.Value);
          if (mainDevice.IsLocked)
          {
            throw new InvalidOperationException($"The device '{mainDevice.ItemNumber}' can not be reserved because it is locked.");
          }

          if (mainDevice.StatusId != DeviceStatus.UsableId)
          {
            throw new InvalidOperationException($"The device '{mainDevice.ItemNumber}' has the status '{mainDevice.Status}' and can not be reserved.");
          }

          if (mainDevice.CalibrationDue <= dateTime)
          {
            throw new InvalidOperationException($"The device '{mainDevice.ItemNumber}' has the status and can not be reserved.");
          }

          int userId = IdentityService.UserId.Value;
          DeviceReservation rootReservation = null;

          var deviceEntities = await Context.Devices.Include(d => d.StoragePlace).Include(d => d.Issues).Where(d => deviceIds.Contains(d.Id)).ToListAsync().ConfigureAwait(false);

          foreach (var deviceEntity in deviceEntities)
          {
            if (deviceEntity.StoragePlace == null)
              throw new InvalidOperationException($"Device {deviceEntity.ItemNumber} has no Storage Place.");

            if (deviceEntity.Issues.Any())
              throw new InvalidOperationException($"Device {deviceEntity.ItemNumber} is currently issued out (not returned).");
            
            var reservation = new DeviceReservation
            {
              DeviceId = deviceEntity.Id,
              UserId = userId,
              CreatedById = userId,
              ReservationPeriodId = deviceReservation.PeriodId.Value, // FK către Perioadă
              InventoryLocationId = deviceEntity.StoragePlace.InventoryLocationId, // Luat prin relația StoragePlace
              StatusId = ReservationStatusViewModel.OpenId,

              StartDate = deviceReservation.StartDate ?? dateTime,
              EndDate = endReservation,
              CreationDate = dateTime,

              DeviceClassId = deviceEntity.DeviceClassId,
              MeasurementMin = deviceEntity.MeasurementMin,
              MeasurementMax = deviceEntity.MeasurementMax,
              MeasurementUnit = deviceEntity.MeasurementUnit ?? string.Empty, // Protecție la null

              AccountingNumber = string.Empty, // Sau preluat din input dacă există
              IssueId = null // La creare nu avem încă Issue
            };
            //var deviceEntity = await Context.Devices.FirstAsync(d => d.Id == item.Id).ConfigureAwait(false);
            deviceEntity.LastChangedDate = dateTime;
            deviceEntity.LastChangedUserId = userId;
            deviceEntity.Reservation = true;

            //Context.Devices.Update(deviceEntity);
            Context.DeviceReservations.Add(reservation);
          }
          }

        await Context.SaveChangesAsync().ConfigureAwait(false);
        return deviceReservation;
      }
      catch (Exception ex)
      {
        throw new ApplicationException("An unexpected Error occurred. Unable to reserve the device.", ex);
      }
    }

    public async Task<bool> ReservationCanceledAsync(DeviceReservationSelectViewModel deviceReservation)
    {
      try
      {
        var treeDeviceIds = await Context.GetDeviceTree(deviceReservation.DeviceId)
                                          .Select(d => d.Id)
                                          .ToListAsync()
                                          .ConfigureAwait(false);

        int? userId = IdentityService.UserId;
        DateTime now = DateTime.Now;

        var activeReservations = await Context.DeviceReservations.Include(d => d.Device)
                                      .Where(r => treeDeviceIds.Contains(r.DeviceId)).Where(r => r.EndDate == null || r.EndDate > now)
                                      .ToListAsync()
                                      .ConfigureAwait(false);

        if (!activeReservations.Any())
        {
          return false;
        }

        foreach (var res in activeReservations)
        {
          res.EndDate = DateTime.Now; 
          res.LastChangedUserId = IdentityService.UserId;
          if (res.Device != null)
          {
            res.Device.Reservation = false; 
            res.Device.LastChangedDate = now;
            res.Device.LastChangedUserId = userId;
          }
        }

        await Context.SaveChangesAsync().ConfigureAwait(false);

        return true;
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Eroare la inchiderea rezervarii.", ex);
      }
    }

    private readonly struct DeviceReservationTreeItem
    {
      public int Id { get; init; }

      public string ItemNumber { get; init; }

      public int DeviceClassId { get; init; }

      public bool IsChild { get; init; }

      public int StatusId { get; init; }

      public string Status { get; init; }

      public bool IsLocked { get; init; }

      public DateTime? IssuePlannedDate { get; init; }

      public DateTime? CalibrationDue { get; init; }

      public string DeviceCostCenter { get; init; }

      public string DeviceCode { get; init; }

      public string InventoryLocationCostCenter { get; init; }

      public string InventoryLocationCode { get; init; }
    }

    [StructLayout(LayoutKind.Auto)]
    private readonly struct DeviceReservationItem
    {
      // ReSharper disable once UnusedAutoPropertyAccessor.Local
      public int DeviceId { get; init; }

      public string ItemNumber { get; init; }

      public bool IsChild { get; init; }

      public DateTime StartDate { get; init; }

      public int? Max { get; init; }
    }
  }
}
