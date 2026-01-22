using AutoMapper;
using LineControllerCore.Interface;
using LineControllerCore.Model;

using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace LineControllerCore.Service
{
  public class DeviceIssuesService : BaseService<DeviceIssue>, IDeviceIssuesService
  {

    public DeviceIssuesService(LineContextDb context, IMapper mapper, ILogger<DeviceIssuesService> logger, IIdentityService identityService) : base(context, mapper, logger, identityService)
    {
    }

    public async Task<List<UserSelectViewModel>> GetActiveUsers()
    {
      var result = await Context.Users.Where(s => s.Id != 0).Select(s => new UserSelectViewModel()
      {
        Id = s.Id,
        FirstName = s.FirstName,
        LastName = s.LastName,
        Department = s.Department,
      }).ToListAsync().ConfigureAwait(false);

      return result;
    }

    public async Task<DeviceIssueStatusViewModel> GetDeviceIssuesStatus(string itemNumber)
    {
      return await Context.Devices
          .Include(d => d.Issues)
          .Where(d => d.ItemNumber == itemNumber)
          .Select(d => new DeviceIssueStatusViewModel
          {
            IsIssued = d.Issues.Any(i => i.ReturnDateActual == null),
            CanRetrieve = d.Issues.Any(i => i.ReturnDateActual == null),
            LastIssueId = d.Issues
                  .Where(i => i.ReturnDateActual == null)
                  .Select(i => i.Id)
                  .FirstOrDefault()
          })
          .FirstOrDefaultAsync();

    }

    public IEnumerable<object> SearchItemNumber(string itemNumber)
    {
      if (string.IsNullOrWhiteSpace(itemNumber))
        return Enumerable.Empty<object>();

      itemNumber = itemNumber.Trim();

      IQueryable<Device> query = Context.Devices;

      if (itemNumber.StartsWith("*") && itemNumber.EndsWith("*"))
      {
        // *text* → conține
        var term = itemNumber.Trim('*');
        query = query.Where(d => d.ItemNumber.Contains(term));
      }
      else if (itemNumber.StartsWith("*"))
      {
        // *text → se termină cu
        var term = itemNumber.TrimStart('*');
        query = query.Where(d => d.ItemNumber.EndsWith(term));
      }
      else if (itemNumber.EndsWith("*"))
      {
        // text* → începe cu
        var term = itemNumber.TrimEnd('*');
        query = query.Where(d => d.ItemNumber.StartsWith(term));
      }
      else
      {
        // fără * → conține
        query = query.Where(d => d.ItemNumber.Contains(itemNumber));
      }

      return query
          .Select(d => new
          {
            d.Id,
            d.ItemNumber,
            LastIssueId = Context.DeviceIssues
                                 .Where(i => i.DeviceId == d.Id)
                                 .OrderByDescending(i => i.IssueDate)
                                 .Select(i => i.Id)
                                 .FirstOrDefault()
          })
          .Take(20)
          .ToList();
    }

    public async Task<DeviceIssueEditViewModel> IssueAsync(DeviceIssueEditViewModel deviceIssue)
    {
      string rootItemNumber = string.Empty;
     
      try
      {
        var treeDevices = await Context.GetDeviceTree(deviceIssue.DeviceId)
                                       .Select(d => new { d.Id, d.ItemNumber })
                                       .ToListAsync().ConfigureAwait(false);

        // B. Rezervările selectate (care poate nu sunt în arbore)
        var reservationIds = deviceIssue.Reservations
                                        .Where(r => r.IsSelected && r.DeviceId != deviceIssue.DeviceId)
                                        .Select(r => r.DeviceId)
                                        .ToList();

        // Combinăm ID-urile din arbore cu cele din rezervări
        var allDeviceIdsToIssue = treeDevices.Select(d => d.Id)
                                             .Union(reservationIds)
                                             .Distinct()
                                             .ToList();

        // 2. VALIDARE: Verificăm dacă oricare dintre aceste dispozitive este deja dat (issued)
        var devicesAlreadyIssued = await Context.DeviceIssues
            .Where(i => allDeviceIdsToIssue.Contains(i.DeviceId) && i.ReturnDateActual == null)
            .Select(i => i.Device.ItemNumber)
            .ToListAsync()
            .ConfigureAwait(false);

        if (devicesAlreadyIssued.Any())
        {
          var message = $"Device(s) already issued: {string.Join(", ", devicesAlreadyIssued)}";
          Logger.LogError(message);
          // Sfat: Folosește o excepție custom sau gestionează mesajul în UI, 
          // InvalidOperationException e ok dar generic.
          throw new InvalidOperationException(message);
        }

        // 3. Pregătire date utilizator
        int? userId = IdentityService.UserId;

        var recipient = await Context.Users.Where(u => u.Id == deviceIssue.RecipientId)
                                           .Select(u => new
                                           {
                                             u.FirstName,
                                             u.LastName,
                                             u.Department,
                                           })
                                           .FirstAsync().ConfigureAwait(false);
        foreach (var devId in allDeviceIdsToIssue)
        {
          // Mapăm proprietățile comune din ViewModel
          var entity = Mapper.Map<DeviceIssue>(deviceIssue);

          entity.DeviceId = devId; // Setăm ID-ul curent
          entity.CreatedById = userId;
          entity.LastChangedDate = entity.IssueDate = DateTime.Now;
          entity.LastChangedUserId = userId;

          // Dacă ai nevoie de ItemNumber pentru logică, trebuie să îl iei din DB 
          // sau din lista treeDevices (dacă e acolo).
          if (devId == deviceIssue.DeviceId)
          {
            // Găsim ItemNumber pentru root dacă e nevoie de el undeva
            var rootDev = treeDevices.FirstOrDefault(d => d.Id == devId);
            if (rootDev != null) rootItemNumber = rootDev.ItemNumber;
          }

          await Context.DeviceIssues.AddAsync(entity).ConfigureAwait(false);
        }
        await Context.SaveChangesAsync().ConfigureAwait(false);

        return deviceIssue;
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Eroare la actualizarea problemei dispozitivului.", ex);
      }
    }

    public async Task<DeviceIssueEditViewModel> ReceiveAsync(DeviceIssueEditViewModel deviceIssue)
    {
      try
      {
        IEnumerable<Device> devices = await Context.GetDeviceDescendantTree(deviceIssue.DeviceId)
                                                   .ToListAsync().ConfigureAwait(false);
        if (!devices.Any())
        {
          var message = "Unable to receive the device. Device does not exist.";
          Logger.LogError(message);
          throw new InvalidOperationException(message);
        }

        var deviceIds = devices.Select(x => x.Id);
        var deviceIssues = await Context.DeviceIssues.Where(i => deviceIds.Contains(i.DeviceId) && i.ReturnDateActual == null)
                                                     .ToListAsync().ConfigureAwait(false);

        int? userId = IdentityService.UserId;
        Device rootDevice = null;
        DateTime dateTime = DateTime.Now;

        foreach (var device in devices)
        {
          var entity = deviceIssues.FirstOrDefault(i => i.DeviceId == device.Id);

          if (entity == null)
          {
            continue;
          }
          entity.LastChangedDate = entity.ReturnDateActual = dateTime;
          entity.LastChangedUserId = userId;

          if (device.Id == deviceIssue.DeviceId)
          {
            rootDevice = device;
            if (device.ParentId != null)
            {
              device.ParentId = null;
              device.LastChangedDate = dateTime;
              device.LastChangedUserId = userId;
              Context.Devices.Update(device);
            }
          }

          Context.DeviceIssues.Update(entity);
        }

        await Context.SaveChangesAsync().ConfigureAwait(false);


        return deviceIssue;

      }
      catch (Exception ex)
      {
        throw new ApplicationException("An unexpected Error occurred. Unable to retrieve the device.", ex);
      }
    }

    public IQueryable<DeviceIssueHierarchyListViewModel> GetIssueHierarchy(int? deviceId)
    {
      return Context.GetDeviceTree(deviceId)
                      .GroupJoin(Context.ActiveCalibrationOrders, d => d.Id, co => co.DeviceId, (d, co) => new { Device = d, CalibrationOrders = co })
                       .SelectMany(temp => temp.CalibrationOrders.DefaultIfEmpty(), (temp, co) => new DeviceIssueHierarchyListViewModel()
                       {
                         Id = temp.Device.Id,
                         ParentId = temp.Device.ParentId,
                         ItemNumber = temp.Device.ItemNumber,
                         Manufacturer = temp.Device.DeviceClass.Manufacturer.Name,
                         DeviceModel = temp.Device.DeviceClass.DeviceModel.Name,
                         CalibrationDue = temp.Device.CalibrationDate != null && temp.Device.CalibrationInterval != null ? temp.Device.CalibrationDate.Value.AddMonths(temp.Device.CalibrationInterval.Value) : null,
                         IsCalibrationDue = temp.Device.CalibrationDate != null && temp.Device.CalibrationInterval != null && temp.Device.CalibrationDate.Value.AddMonths(temp.Device.CalibrationInterval.Value) <= DateTime.Now,
                         InventoryLocationName = temp.Device.StoragePlace.InventoryLocation.Name,
                         ResponsibleFirstName = temp.Device.StoragePlace.InventoryLocation.Responsible.FirstName,
                         ResponsibleLastName = temp.Device.StoragePlace.InventoryLocation.Responsible.LastName,
                         ResponsibleDepartment = temp.Device.StoragePlace.InventoryLocation.Responsible.Department,
                         HasActiveCalibrationOrder = co != null,
                       });
    }

    public async Task<DeviceIssueEditViewModel> GetViewModelByItemNumberAsync(string itemNumber)
    {
      var device = await Context.Devices.Include(d => d.Parent).FirstOrDefaultAsync(d => d.ItemNumber == itemNumber);

      if (device == null) return null;
      var allIssues = await Context.DeviceIssues
                             .Where(d => d.DeviceId == device.Id)
                             .ToListAsync();
      var activeIssue = await Context.DeviceIssues.Include(d => d.Device).Include(d => d.Recipient).FirstOrDefaultAsync(d => d.DeviceId == device.Id && d.ReturnDateActual == null);
      if (activeIssue != null)
      {
        var model = Mapper.Map<DeviceIssueEditViewModel>(activeIssue);

        // Suprascrii logică specifică de UI
        model.IsRetrieve = true;
        model.CanIssue = false;
        model.CanRetrieve = true;

        return model;
      }
      else
      {
        var model = new DeviceIssueEditViewModel
        {
          DeviceId = device.Id,
          ItemNumber = device.ItemNumber,
          ParentId = device.ParentId,
          IsRetrieve = false,
          CanIssue = true,
          CanRetrieve = false,
          MinimReturnDate = DateTime.Today,
          ReturnDate = DateTime.Today,
          RecipientId = null,
          AccountingNumber = string.Empty,
          Comment = string.Empty
        };

        return model;
      }
    }
  }
}
