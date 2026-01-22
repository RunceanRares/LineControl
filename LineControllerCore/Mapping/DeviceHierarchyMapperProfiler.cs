using AutoMapper;

using LineControllerCore.Model;
using LineControllerInfrastructure.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Mapping
{
  public class DeviceHierarchyMapperProfiler : Profile
  {
    public DeviceHierarchyMapperProfiler()
    {
      CreateMap<Device, DeviceHierarchyListViewModel>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
         .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.DeviceClass.Manufacturer.Name))
         .ForMember(dest => dest.DeviceModel, opt => opt.MapFrom(src => src.DeviceClass.DeviceModel.Name))
         .ForMember(dest => dest.HasActiveCalibrationOrder, opt => opt.MapFrom(src =>
                src.CalibrationOrders.Any(co => co.CalibrationResult == null)))
         .ForMember(dest => dest.ItemNumber, opt => opt.MapFrom(src => src.ItemNumber));
    }
  }
}
