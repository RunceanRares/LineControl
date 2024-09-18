using AutoMapper;
using AutoMapper.QueryableExtensions;
using LineControl.Models;
using LineControllerCore.Model;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LineControllerCore.Service
{
  public class DeviceService : BaseService<Device>, IDeviceService
  {
    //private readonly ILinkService linkService;
    //private readonly IUserRoleService userRoleService;

    public DeviceService(LineContextDb context, IMapper mapper, ILogger<DeviceService> logger)//, ILinkService linkService, IUserRoleService userRoleService) 
           : base(context, mapper, logger)
    {
      //this.linkService = linkService;
      //this.userRoleService = userRoleService;
    }

    public IQueryable<DeviceViewModel> GetDevices() 
    {
      var devices = Context.Devices.Where(s => s.ItemNumber != null);
      if (devices == null || !devices.Any())
      {
        Logger.LogWarning("Unable to save. Device does not exist.");
      }
      else
      {
        var deviceCount = devices.Count();
        Logger.LogInformation($"Found {deviceCount} devices.");
      }
      var deviceViewModel = devices.ProjectTo<DeviceViewModel>(Mapper.ConfigurationProvider);
      return deviceViewModel;
    }

    public DeviceEditViewModel GetDeviceById(int id) 
    {
      var device = Context.Devices.Where(s => s.Id == id).FirstOrDefault();
      if(device is not null)
      {
        var mapDevice = Mapper.Map<DeviceEditViewModel>(device);
        return mapDevice;
      }
      else
      {
        return null;
      }

    }

    public async Task<IEnumerable<MeasurementRangeViewModel>> GetMeasurementRangesAsync(int deviceClassId)
    {
      var measurements = Entities.Where(dc => dc.Id == deviceClassId)
                                 .Where(mode => mode.MeasurementMin != null &&
                                                mode.MeasurementMax != null &&
                                                mode.MeasurementUnit != null)
                                 .Select(mode => new
                                 {
                                   mode.Id,
                                   mode.MeasurementMin,
                                   mode.MeasurementMax,
                                   mode.MeasurementUnit,
                                   mode.MaterialNumber,
                                 })
                                 .GroupBy(mode => new
                                 {
                                   mode.MeasurementMin,
                                   mode.MeasurementMax,
                                   mode.MeasurementUnit,
                                 });
      if (deviceClassId != null)
      {
        return await measurements.Select(kvp => new MeasurementRangeViewModel()
        {
          Min = kvp.Key.MeasurementMin.Value,
          Max = kvp.Key.MeasurementMax.Value,
          Unit = kvp.Key.MeasurementUnit,
        }).ToListAsync().ConfigureAwait(false);
      }
      else
      {
        return await measurements.Select(kvp => new MeasurementRangeViewModel()
        {
          Min = kvp.Key.MeasurementMin.Value,
          Max = kvp.Key.MeasurementMax.Value,
          Unit = kvp.Key.MeasurementUnit,
        }).ToListAsync().ConfigureAwait(false);
      }
    }

    public async Task<IEnumerable<DeviceStatusViewModel>> GetStatusesAsync()
    {
      var result = await Context.DeviceStatuses.OrderBy(s => s.Id)
                                .ProjectTo<DeviceStatusViewModel>(Mapper.ConfigurationProvider, new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase))
                                .ToListAsync().ConfigureAwait(false);

      return result;
    }

    public async Task<DeviceEditViewModel> Update(DeviceEditViewModel model)
    {
      DateTime dateTime = DateTime.Now;
      bool? isRoot = await Context.Devices.AsNoTracking().Where(d => d.Id == model.Id).Select(d => d.ParentId == null).FirstOrDefaultAsync().ConfigureAwait(false);
      bool hasChangedStatus = false;

      var deviceDescendant = Context.GetDeviceDescendantTree(model.Id);
      
      foreach (var device in deviceDescendant)
      {
        Entities.Attach(device);
        if (device.Id == model.Id)
        {
          if (model.StatusId != device.StatusId)
          {
            hasChangedStatus = true;
          }
          Mapper.Map(model, device);
          device.LastChangedUserId = IdentityService.UserId;
          device.LastChangedDate = dateTime;
        }
        else if (isRoot == true)
        {
          device.StatusId = model.StatusId;
          device.LastChangedUserId = IdentityService.UserId;
          device.LastChangedDate = dateTime;
        }

        Entities.Update(device);
      }

      await Context.SaveChangesAsync().ConfigureAwait(false);

      var updatedDevice = await Context.Devices.AsNoTracking().Where(d => d.Id == model.Id).FirstOrDefaultAsync().ConfigureAwait(false);

      return Mapper.Map<DeviceEditViewModel>(updatedDevice);
    }

    //public async Task<IEnumerable<DeviceViewModel>> GetAsync(Func<LinkViewModel, string> getDeviceDetailsUrl, Func<LinkViewModel, string> getCalibrationOrderUrl)
    //{
    //  var user = Context.Users.Where(s => !string.IsNullOrEmpty(s.UserName)).Select(s => s.UserName).SingleOrDefault();
    //  var userId = Context.Users.Select(s => s.Id).SingleOrDefault();
    //  int? userIds = IdentityService.UserId;

    //  var device = await Entities.AsNoTracking()
    //                             .Include(s => s.ActivityType)
    //                             .Include(s => s.DeviceClass.DeviceModel)
    //                             .Include(s => s.Parent)
    //                             .Include(s => s.Issues).ThenInclude(s => s.Recipient)
    //                             .Include(s => s.Reservation)
    //                             .Where(d => d.Id != null)
    //                             .AsSplitQuery()
    //                             .SingleOrDefaultAsync().ConfigureAwait(false);

    //  DateTime now = DateTime.Now;
    //  var model = Mapper.Map<DeviceEditViewModel>(device, opts =>
    //  {
    //    opts.Items["now"] = now;
    //  });

    //  if (model == null)
    //  {
    //    model = new DeviceEditViewModel() { IsEmpty = true };
    //  }
    //  else
    //  {
    //    string? measureRange;

    //    if (model.StatusId == DeviceStatus.UsableId)
    //    {
    //      measureRange = await Context.DeviceClassModes.Where(m => m.DeviceClassId == model.DeviceClassId && !string.IsNullOrEmpty(m.MaterialNumber))
    //                                                         .OrderBy(m => m.Id)
    //                                                         .Select(m => m.MaterialNumber)
    //                                                         .FirstOrDefaultAsync().ConfigureAwait(false);
    //    }
    //    else
    //    {
    //      measureRange = null;
    //    }
    //    if (string.IsNullOrEmpty(model.MaterialNumber))
    //    {
    //      model.MaterialNumber = measureRange;
    //    }
    //    else if (!string.IsNullOrEmpty(measureRange))
    //    {
    //      Logger.LogWarning("The device {ItemNumber} has the material number {MaterialNumber} and the measurement range has the material number {MeasurementRangeMaterialNumber}", model.ItemNumber, model.MaterialNumber, measureRange);
    //    }
    //  }

    //  model.IsManagerOrSysAdmin = userRoleService.IsMember(new[] { RoleViewModel.DeviceMaster, RoleViewModel.SysAdmin });
    //  var fullStructureIds = Context.GetDeviceTree(model.Id)
    //                                    .Select(d => d.Id);
    //  model.HasCalibrationOrderOpened = await Context.ActiveCalibrationOrders
    //                                                 .AnyAsync(co => fullStructureIds.Contains(co.DeviceId)).ConfigureAwait(false);

    //  if (!model.IsEmpty && !model.IsManagerOrSysAdmin)
    //  {
    //    Logger.LogWarning("You are not authorized to make changes to devices in this storage location.");
    //  }
    //  else if (model.ParentId == null && !model.IsEmpty)
    //  {
    //    var deviceIds = await Context.GetDeviceTree(model.Id)
    //                                     .Select(d => d.Id)
    //                                     .ToListAsync().ConfigureAwait(false);
    //    var calibrationOrders = await Context.ActiveCalibrationOrders
    //                                         .Where(co => deviceIds.Contains(co.DeviceId))
    //                                         .Select(co => new
    //                                         {
    //                                           co.Id,
    //                                           co.DeviceId,
    //                                           co.Device.ItemNumber,
    //                                           co.IsRoot,
    //                                           co.RootId,
    //                                         })
    //                                         .ToListAsync().ConfigureAwait(false);

    //    foreach (var calibrationOrder in calibrationOrders)
    //    {
    //      int rootId = calibrationOrder.IsRoot ? calibrationOrder.Id : calibrationOrders.Where(co => co.IsRoot && co.RootId == calibrationOrder.RootId).Select(co => co.Id).Single();
    //      string calibrationOrderLink = getCalibrationOrderUrl(new LinkViewModel() { Id = rootId, Text = rootId.ToString(CultureInfo.CurrentCulture) });
    //      string deviceLink = getDeviceDetailsUrl(new LinkViewModel() { Id = calibrationOrder.DeviceId, Text = calibrationOrder.ItemNumber });
    //      Logger.LogWarning("The device {0} has the active calibration order {1}.", deviceLink, calibrationOrderLink);

    //    }
    //  }

    //  return new List<DeviceViewModel> { Mapper.Map<DeviceViewModel>(model) };
    //}
  }
}
