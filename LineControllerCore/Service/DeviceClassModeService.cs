using AutoMapper;
using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.Security.Claims;

namespace LineControllerCore.Service
{
  public class DeviceClassModeService : BaseService<DeviceClassMode>, IDeviceClassMode
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeviceClassModeService(LineContextDb context, IMapper mapper, ILogger<DeviceClassModeService> logger, IHttpContextAccessor httpContextAccessor) 
          : base(context, mapper, logger)
    {
      this._httpContextAccessor = httpContextAccessor;
    }

    public IQueryable<DeviceClassModeViewModel> GetDeviceClassModeAsync()
    {
      return Context.DeviceClassModes.Select(x => new DeviceClassModeViewModel
      {
        //DeviceClassId = x.DeviceClassId,
        ModeId = x.Id,
        MaterialNumber = x.MaterialNumber,
        MeasurementUnit = x.MeasurementUnit,
        MeasurementMax = x.MeasurementMax,
        MeasurementMin = x.MeasurementMin,
        OutputMax = x.OutputMax,
        OutputMin = x.OutputMin,
        Output = x.OutputMin + " / " + x.OutputMax + " / " + x.OutputUnit,
        OutputUnit = x.OutputUnit,
        Description = x.Description,
    });
    }

    public DeviceClassModeViewModel GetDeviceClassModelById(int id)
    {
      var deviceModel = Context.DeviceClassModes.Where(s => s.Id == id).FirstOrDefault();
      if (deviceModel is not null)
      {
        var mapDevice = Mapper.Map<DeviceClassModeViewModel>(deviceModel);
        return mapDevice ;
      }
      else
      {
        return null;
      }
    }

    public DeviceClassModeViewModel UpdateDeviceClassModel(DeviceClassModeViewModel model)
    {
      var device = new DeviceClassMode
      {
       // DeviceClassId = model.DeviceClassId,
        Id = model.ModeId,
        MaterialNumber = model.MaterialNumber,
        MeasurementUnit = model.MeasurementUnit,
        MeasurementMax = model.MeasurementMax,
        MeasurementMin = model.MeasurementMin,
        OutputMax = model.OutputMax,
        OutputMin = model.OutputMin,
        OutputUnit = model.OutputUnit,
        Description = model.Description,
      };

      Context.DeviceClassModes.Update(device);
      Context.SaveChanges();

      return model;
    }


    public List<DeviceModelViewModel> GetDeviceMode()
    {
      return Context.DeviceModels.Select(s => new DeviceModelViewModel
      {
        Id = s.Id,
        Name = s.Name,
      }).ToList();
    }

    public DeviceModelViewModel GetDeviceModelById(int id)
    {
      var deviceModel = Context.DeviceModels.Where(s => s.Id == id).FirstOrDefault();
      if(deviceModel is not null)
      {
        var mapDevice = Mapper.Map<DeviceModelViewModel>(deviceModel);
        return mapDevice;
      }
      else
      {
        return null;
      }
    }

    public DeviceModelViewModel UpdateDeviceModel(DeviceModelViewModel model)
    {
      //var device = new DeviceModel
      //{
      //  Id = model.Id,
      //  Name = model.Name,
      //};
      var device = Context.DeviceModels.FirstOrDefault(dm => dm.Id == model.Id);
      if (device != null)
      {
        device.Id = model.Id;
        device.Name = model.Name;

        Context.DeviceModels.Update(device);
        Context.SaveChanges();
      }
      return model;
    }
  }
}
