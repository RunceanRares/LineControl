using AutoMapper;
using AutoMapper.QueryableExtensions;
using LineControl.Models;

using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerCore.Models;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;

namespace LineControllerCore.Service
{
  public class CalibrationOrderService : BaseService<DeviceCalibrationOrder>, IDeviceCalibrationService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CalibrationOrderService(LineContextDb context, IMapper mapper, ILogger<CalibrationOrderService> logger, IHttpContextAccessor httpContextAccessor)
          : base(context, mapper, logger)
    {
      this._httpContextAccessor = httpContextAccessor;
    }

    public IQueryable<DeviceCalibrationOrderViewModel> GetSelectViewModels()
    {
      return Context.CalibrationOrders.ProjectTo<DeviceCalibrationOrderViewModel>(Mapper.ConfigurationProvider);
    }

    public DeviceCalibrationOrderViewModel GetDeviceCalibrationById(int id)
    {
      var query = Context.CalibrationOrders.Include(c => c.Device)
                                           .ThenInclude(d => d.CreatedBy)
                                           .Include(c => c.Device)
                                           .ThenInclude(d => d.CreatedBy)
                                           .Include(c => c.Device.Status)   
                                           .Include(c => c.Root)
                                           .ThenInclude(r => r.Action).Where(s => s.Id == id).FirstOrDefault();
      if (query is null)
      {
        return null;
      }
      else
      {
        var calibrationOrder = Mapper.Map<DeviceCalibrationOrderViewModel>(query);
        return calibrationOrder;
      }          
    }

    public DeviceCalibrationOrderViewModel Update(DeviceCalibrationOrderViewModel model)
    {
      if (model == null || model.Id == 0)
      {
        throw new ArgumentException("Model is invalid or ID is not provided.");
      }

      DeviceCalibrationOrder entity = Context.CalibrationOrders.Include(s => s.Device).Include(c => c.Root).FirstOrDefault(s => s.Id == model.Id);

      if (entity is null)
      {
        throw new KeyNotFoundException($"DeviceCalibrationOrder with ID {model.Id} not found.");
      }


      model.CreatedById = (int)entity.Device.CreatedById;
      var rootBackup = entity.Root;
      var rootIdBackup = entity.RootId;
      var existingDeviceId = entity.DeviceId;

      // 🔹 mapăm doar câmpurile CalibrationOrder (fără Root)
      Mapper.Map(model, entity);

      // 🔹 restaurăm DeviceId-ul valid
      entity.DeviceId = existingDeviceId;

      // 🔹 asigurăm consistența EF
      entity.Device = Context.Devices.FirstOrDefault(d => d.Id == existingDeviceId);
      entity.Root = rootBackup;
      entity.RootId = rootIdBackup;

      Context.Update(entity);
      Context.SaveChanges();

      return Mapper.Map<DeviceCalibrationOrderViewModel>(entity);
    }

    public DeviceCalibrationOrderViewModel AddCalibratioOrder(DeviceCalibrationOrderViewModel model)
    {
      var deviceCalibration = Context.CalibrationOrders
                                     .Where(o => o.Device != null && o.Device.ItemNumber == model.ItemNumber)
                                     .FirstOrDefault();
      //var deviceIds = Context.Devices.Where(d => d.Id == model.DeviceId);
      var root = Mapper.Map<DeviceCalibrationOrderRoot>(model);
      if (deviceCalibration != null)
      {
        Logger.LogWarning("Unable to add the Device Calibration. Id already exists.");
        return null;
      }
      else if (string.IsNullOrEmpty(model.AccountingNumber))
      {
        throw new InvalidOperationException("AccountingNumber is required.");
      }
      else
      {
        var activityType = Context.CalibrationLocations.Where(cl => cl.Id == model.CalibrationLocationId)
                                                          .Select(cl => new
                                                          {
                                                             cl.Code,
                                                             cl.CostCenter,
                                                             IsRequired = !string.IsNullOrEmpty(cl.Code) && !string.IsNullOrEmpty(cl.CostCenter),
                                                          });
        //var deviceCalibrationModel = Mapper.Map<DeviceCalibrationOrder>(model);
        //deviceCalibrationModel.Root.AccountingNumber = model.AccountingNumber ?? throw new InvalidOperationException("AccountingNumber is required.");

          var deviceCalibrationModel = new DeviceCalibrationOrder
          {
             Root = root,
          };

          Context.CalibrationOrders.Add(deviceCalibrationModel);
          Context.SaveChanges();
        return model;
      }
    }

    public async Task<IEnumerable<DeviceViewModel>> GetItemNumbers(string itemNumber)
    {
      var devices = await Context.Devices.Where(s => s.ItemNumber != null)
                                   .Select(s => new DeviceViewModel 
                                   {
                                     Id = s.Id,
                                     ItemNumber = s.ItemNumber,
                                   }).ToListAsync();
      return devices;
    }
  }
}
